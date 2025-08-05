#include <uLib.h>
#include "code/z_play.h"
#include <code/z_scene_table.h>

//Version: 1.2
/*This version value is used by SharpOcarina to determine if it needs to update the Play.c of an old project
to use newly added features. Put a high value like 99 to stop it from ever asking to update it again.
*/

asm ("TitleSetup_Init = 0x80803CAC");
asm ("FileSelect_Init = 0x80811A20");

extern void z64rom_PrePlayUpdate(PlayState* play);
extern void z64rom_PostPlayUpdate(PlayState* play);
extern void z64rom_PrePlayDraw(PlayState* play);
extern void z64rom_PostPlayDraw(PlayState* play);

static void (*sSceneFunc)(PlayState*);

#if SEGMENT_0x06_FOR_SCENES
static u16 sSceneSegmentObj0x06 = 0;
Asm_VanillaHook(Scene_CommandSkyboxSettings);
void Scene_CommandSkyboxSettings(PlayState *play, SceneCmd *cmd) {
    u8 *cmd8 = (u8*)cmd;
    sSceneSegmentObj0x06 = (cmd8[1] << 8) | cmd8[2];
    if (sSceneSegmentObj0x06)
        sSceneSegmentObj0x06 = Object_Spawn(&play->objectCtx, sSceneSegmentObj0x06);
    play->skyboxId = cmd->skyboxSettings.skyboxId;
    play->envCtx.skyboxConfig = play->envCtx.changeSkyboxNextConfig = cmd->skyboxSettings.skyboxConfig;
    play->envCtx.lightMode = cmd->skyboxSettings.envLightMode;
}
#endif

static s32 Play_FrameAdvance(PlayState* play) {
#ifdef DEV_BUILD
    static s32 holdTimer;
    FrameAdvanceContext* frameAdvCtx = &play->frameAdvCtx;
    
    if (CHK_ALL(cur, BTN_R | BTN_L) && CHK_ALL(press, BTN_A))
        frameAdvCtx->enabled = !frameAdvCtx->enabled;
    
    if (!frameAdvCtx->enabled || play->pauseCtx.state)
        return true;
    
    if (CHK_ANY(cur, BTN_L | BTN_START))
        holdTimer++;
    else
        holdTimer = 1;
    
    if ((CHK_ANY(press, BTN_L | BTN_START) || (!(holdTimer % 2) && holdTimer > 20))) {
        play->state.input[0].press = play->state.input[0].cur;
        
        return true;
    }
    
    return false;
#else
    
    return true;
#endif
}

static void Play_Draw2(PlayState* play) {
    GraphicsContext* gfxCtx = play->state.gfxCtx;
    Vec3f sp21C;
    
    POLY_OPA_DISP = Play_SetFog(play, POLY_OPA_DISP);
    POLY_XLU_DISP = Play_SetFog(play, POLY_XLU_DISP);
    
    View_SetPerspective(&play->view, play->view.fovy, play->view.zNear, play->lightCtx.fogFar);
    View_Apply(&play->view, VIEW_ALL);
    
    // The billboard matrix temporarily stores the viewing matrix
    Matrix_MtxToMtxF(&play->view.viewing, &play->billboardMtxF);
    Matrix_MtxToMtxF(&play->view.projection, &play->viewProjectionMtxF);
    Matrix_Mult(&play->viewProjectionMtxF, MTXMODE_NEW);
    // The billboard is still a viewing matrix at this stage
    Matrix_Mult(&play->billboardMtxF, MTXMODE_APPLY);
    Matrix_Get(&play->viewProjectionMtxF);
    play->billboardMtxF.mf[0][3] = play->billboardMtxF.mf[1][3] = play->billboardMtxF.mf[2][3] =
        play->billboardMtxF.mf[3][0] = play->billboardMtxF.mf[3][1] = play->billboardMtxF.mf[3][2] =
        0.0f;
    // This transpose is where the viewing matrix is properly converted into a billboard matrix
    Matrix_Transpose(&play->billboardMtxF);
    play->billboardMtx = Matrix_MtxFToMtx(
        Matrix_CheckFloats(&play->billboardMtxF, "../z_play.c", 4005),
        Graph_Alloc(gfxCtx, sizeof(Mtx) * 2)
    );
    
    // cylindrical billboarding
    {
        u16 *cyl = (void*)(play->billboardMtx + 1);
        
        // cylinder = copy of sphere
        bcopy(play->billboardMtx, cyl, 0x40);
        
        // revert up vector to identity
        cyl[0x08 / 2] = 0; // x
        cyl[0x0A / 2] = 1; // y
        cyl[0x0C / 2] = 0; // z
        
        cyl[0x28 / 2] = 0; // x
        cyl[0x2A / 2] = 0; // y
        cyl[0x2C / 2] = 0; // z
    }
    
    gSPSegment(POLY_OPA_DISP++, 0x01, play->billboardMtx);
    
    if ((HREG(80) != 10) || (HREG(92) != 0)) {
        Gfx* gfxP;
        Gfx* sp1CC = POLY_OPA_DISP;
        
        gfxP = Graph_GfxPlusOne(sp1CC);
        gSPDisplayList(OVERLAY_DISP++, gfxP);
        
        if (
            (play->transitionMode == TRANS_MODE_INSTANCE_RUNNING) ||
            (play->transitionMode == TRANS_MODE_INSTANCE_WAIT) ||
            (play->transitionCtx.transitionType >= 56)
        ) {
            View view;
            
            View_Init(&view, gfxCtx);
            view.flags = VIEW_VIEWPORT | VIEW_PROJECTION_ORTHO;
            
            SET_FULLSCREEN_VIEWPORT(&view);
            
            View_ApplyTo(&view, VIEW_ALL, &gfxP);
            play->transitionCtx.draw(&play->transitionCtx.instanceData, &gfxP);
        }
        
        TransitionFade_Draw(&play->transitionFade, &gfxP);
        
        if (D_801614B0.a > 0) {
            D_80161498.primColor.rgba = D_801614B0.rgba;
            VisMono_Draw(&D_80161498, &gfxP);
        }
        
        gSPEndDisplayList(gfxP++);
        Graph_BranchDlist(sp1CC, gfxP);
        POLY_OPA_DISP = gfxP;
    }
    
    if (gTrnsnUnkState == 3) {
        Gfx* sp88 = POLY_OPA_DISP;
        
        TransitionUnk_Draw(&sTrnsnUnk, &sp88);
        POLY_OPA_DISP = sp88;
        goto Gameplay_Draw_DrawOverlayElements;
    } else {
        PreRender_SetValues(
            &play->pauseBgPreRender,
            SCREEN_WIDTH,
            SCREEN_HEIGHT,
            gfxCtx->curFrameBuffer,
            gZBuffer
        );
        
        if (R_PAUSE_MENU_MODE == 2) {
            Sched_FlushTaskQueue();
            PreRender_ApplyFilters(&play->pauseBgPreRender);
            R_PAUSE_MENU_MODE = 3;
        } else if (R_PAUSE_MENU_MODE >= 4) {
            R_PAUSE_MENU_MODE = 0;
        }
        
        if (R_PAUSE_MENU_MODE == 3) {
            Gfx* sp84 = POLY_OPA_DISP;
            
            PreRender_RestoreFramebuffer(&play->pauseBgPreRender, &sp84);
            POLY_OPA_DISP = sp84;
            goto Gameplay_Draw_DrawOverlayElements;
        } else {
            s32 sp80 = 0;
            
            if ((HREG(80) != 10) || (HREG(83) != 0)) {
                if (
                    play->skyboxId && (play->skyboxId != SKYBOX_UNSET_1D) &&
                    !play->envCtx.skyboxDisabled
                ) {
                    if (
                        (play->skyboxId == SKYBOX_NORMAL_SKY) ||
                        (play->skyboxId == SKYBOX_CUTSCENE_MAP)
                    ) {
                        Environment_UpdateSkybox(play->skyboxId, &play->envCtx, &play->skyboxCtx);
                        SkyboxDraw_Draw(
                            &play->skyboxCtx,
                            gfxCtx,
                            play->skyboxId,
                            play->envCtx.skyboxBlend,
                            play->view.eye.x,
                            play->view.eye.y,
                            play->view.eye.z
                        );
                    } else if (play->skyboxCtx.unk_140 == 0) {
                        SkyboxDraw_Draw(
                            &play->skyboxCtx,
                            gfxCtx,
                            play->skyboxId,
                            0,
                            play->view.eye.x,
                            play->view.eye.y,
                            play->view.eye.z
                        );
                    }
                }
            }
            
            if ((HREG(80) != 10) || (HREG(90) & 2)) {
                if (!play->envCtx.sunMoonDisabled) {
                    Environment_DrawSunAndMoon(play);
                }
            }
            
            if ((HREG(80) != 10) || (HREG(90) & 1)) {
                Environment_DrawSkyboxFilters(play);
            }
            
            if ((HREG(80) != 10) || (HREG(90) & 4)) {
                Environment_UpdateLightningStrike(play);
                Environment_DrawLightning(play, 0);
            }
            
            if ((HREG(80) != 10) || (HREG(84) != 0)) {
                if (VREG(94) == 0) {
                    if (HREG(80) != 10) {
                        sp80 = 3;
                    } else {
                        sp80 = HREG(84);
                    }
                    Profiler_Start(&gLibCtx.profiler.sceneDraw);
                    Scene_Draw(play);
                    
                    // embedded scene func
                    if (sSceneFunc)
                        sSceneFunc(play);
                    
                    NewRoom_Draw(play, &play->roomCtx.curRoom, sp80 & 3);
                    NewRoom_Draw(play, &play->roomCtx.prevRoom, sp80 & 3);
                    Profiler_End(&gLibCtx.profiler.sceneDraw);
                }
            }
            
            if ((HREG(80) != 10) || (HREG(83) != 0)) {
                if (
                    (play->skyboxCtx.unk_140 != 0) &&
                    (GET_ACTIVE_CAM(play)->setting != CAM_SET_PREREND_FIXED)
                ) {
                    Vec3f sp74;
                    
                    Camera_GetSkyboxOffset(&sp74, GET_ACTIVE_CAM(play));
                    SkyboxDraw_Draw(
                        &play->skyboxCtx,
                        gfxCtx,
                        play->skyboxId,
                        0,
                        play->view.eye.x + sp74.x,
                        play->view.eye.y + sp74.y,
                        play->view.eye.z + sp74.z
                    );
                }
            }
            
            if (play->envCtx.precipitation[PRECIP_RAIN_CUR] != 0) {
                Environment_DrawRain(play, &play->view, gfxCtx);
            }
            
            if ((HREG(80) != 10) || (HREG(84) != 0)) {
                Environment_FillScreen(gfxCtx, 0, 0, 0, play->unk_11E18, FILL_SCREEN_OPA);
            }
            
            if ((HREG(80) != 10) || (HREG(85) != 0)) {
                Profiler_Start(&gLibCtx.profiler.ovlDraw);
                func_800315AC(play, &play->actorCtx);
                Profiler_End(&gLibCtx.profiler.ovlDraw);
            }
            
            if ((HREG(80) != 10) || (HREG(86) != 0)) {
                if (!play->envCtx.sunMoonDisabled) {
                    sp21C.x = play->view.eye.x + play->envCtx.sunPos.x;
                    sp21C.y = play->view.eye.y + play->envCtx.sunPos.y;
                    sp21C.z = play->view.eye.z + play->envCtx.sunPos.z;
                    Environment_DrawSunLensFlare(play, &play->envCtx, &play->view, gfxCtx, sp21C, 0);
                }
                Environment_DrawCustomLensFlare(play);
            }
            
            if ((HREG(80) != 10) || (HREG(87) != 0)) {
                if (MREG(64) != 0) {
                    Environment_FillScreen(
                        gfxCtx,
                        MREG(65),
                        MREG(66),
                        MREG(67),
                        MREG(68),
                        FILL_SCREEN_OPA | FILL_SCREEN_XLU
                    );
                }
                
                switch (play->envCtx.fillScreen) {
                    case 1:
                        Environment_FillScreen(
                            gfxCtx,
                            play->envCtx.screenFillColor[0],
                            play->envCtx.screenFillColor[1],
                            play->envCtx.screenFillColor[2],
                            play->envCtx.screenFillColor[3],
                            FILL_SCREEN_OPA | FILL_SCREEN_XLU
                        );
                        break;
                    default:
                        break;
                }
            }
            
            if ((HREG(80) != 10) || (HREG(88) != 0)) {
                if (play->envCtx.sandstormState != 0) {
                    Environment_DrawSandstorm(play, play->envCtx.sandstormState);
                }
            }
            
            if ((R_PAUSE_MENU_MODE == 1) || (gTrnsnUnkState == 1)) {
                Gfx* sp70 = OVERLAY_DISP;
                
                play->pauseBgPreRender.fbuf = gfxCtx->curFrameBuffer;
                play->pauseBgPreRender.fbufSave = (u16*)gZBuffer;
                PreRender_SaveFramebuffer(&play->pauseBgPreRender, &sp70);
                if (R_PAUSE_MENU_MODE == 1) {
                    play->pauseBgPreRender.cvgSave = (u8*)gfxCtx->curFrameBuffer;
                    PreRender_DrawCoverage(&play->pauseBgPreRender, &sp70);
                    R_PAUSE_MENU_MODE = 3;
                } else {
                    gTrnsnUnkState = 2;
                }
                OVERLAY_DISP = sp70;
                play->unk_121C7 = 2;
                SREG(33) |= 1;
            } else {
Gameplay_Draw_DrawOverlayElements:
                if ((HREG(80) != 10) || (HREG(89) != 0)) {
                    Play_DrawOverlayElements(play);
                }
            }
        }
    }
}

void Play_SetFadeOut(PlayState* play) {
    s16 entranceIndex;
    
    if (gExitParam.isExit == false) {
        if (!IS_DAY) {
            if (!LINK_IS_ADULT) {
                entranceIndex = play->nextEntranceIndex + 1;
            } else {
                entranceIndex = play->nextEntranceIndex + 3;
            }
        } else {
            if (!LINK_IS_ADULT) {
                entranceIndex = play->nextEntranceIndex;
            } else {
                entranceIndex = play->nextEntranceIndex + 2;
            }
        }
        
        play->transitionType = gEntranceTable[entranceIndex].field & 0x7F;
    } else {
        play->transitionType = gExitParam.exit.fadeOut;
    }
}

Time Play_GetTime(void) {
    s16 clockCount[4];
    
    clockCount[0] = 0;
    f32 temp_f0_2 = gSaveContext.dayTime * (24.0f * 60.0f / 0x10000);
    
    clockCount[1] = temp_f0_2 / 60.0f;
    while (clockCount[1] >= 10) {
        clockCount[0]++;
        clockCount[1] -= 10;
    }
    clockCount[2] = 0;
    clockCount[3] = (s16)temp_f0_2 % 60;
    while (clockCount[3] >= 10) {
        clockCount[2]++;
        clockCount[3] -= 10;
    }
    
    //crustify
	return (Time) {
		.hour = clockCount[0] * 10 + clockCount[1],
		.minute = clockCount[2] * 10 + clockCount[3],
	};
    //uncrustify
}

Asm_VanillaHook(Play_SpawnScene);
void Play_SpawnScene(PlayState* play, s32 sceneNum, s32 spawn) {
    SceneTableEntry* scene = &gSceneTable[sceneNum];
    u32 roomSize;
    bool hasEmbeds = false;
    sSceneFunc = 0;
    
    scene->unk_13 = 0;
    play->loadedScene = scene;
    play->sceneId = sceneNum;
    play->sceneDrawConfig = scene->drawConfig;
    
    // this bit specifies embeds are present
    RomFile sceneFile = scene->sceneFile;
    if (sceneFile.vromStart & 0x80000000)
    {
        hasEmbeds = true;
        sceneFile.vromStart &= 0x7fffffff;
    }
    
    play->sceneSegment = Play_LoadFile(play, &sceneFile);
    
    // handle embeds
    if (hasEmbeds)
    {
        u32 *jumps = play->sceneSegment;
        u8 *scene = play->sceneSegment = &jumps[4];
        
        // overlay
        if (jumps[0])
        {
            u32 *footer = (u32*)(scene + jumps[0]);
            u8 *start  = scene + footer[2];
            u8 *end    = scene + footer[3];
            u8 *header = end - footer[0];
            u32 main   = footer[1];
            u32 size   = end - start;
            
            // relocate overlay from virtual ram to physical ram
            Overlay_Relocate(start, (void*)header, (void*)0x80800000);
            
            // clear instruction cache for memory region occupied by overlay
            osWritebackDCache(start, size);
            osInvalICache(start, size);
            
            // use main routine as scene func
            play->sceneDrawConfig = SDC_DEFAULT;
            sSceneFunc = (void (*)(PlayState*))(scene + main);
        }
        // TODO jumps[1..3] are still free: use them wisely! (minimaps??)
    }
    
    scene->unk_13 = 0;
    
    gSegments[2] = VIRTUAL_TO_PHYSICAL(play->sceneSegment);
    
    Play_InitScene(play, spawn);
    roomSize = func_80096FE8(play, &play->roomCtx);
    sSceneDrawConfigs[4] = SceneAnim_Update;
    
    osLibPrintf(
        "Scene "
        PRNT_YELW "0x%02X " PRNT_RSET
        "SceneEntry "
        PRNT_YELW "%08X " PRNT_RSET
        "EntryHead "
        PRNT_YELW "%08X " PRNT_RSET
        "Segment "
        PRNT_YELW "%08X ",
        sceneNum,
        scene,
        gSceneTable,
        play->sceneSegment
    );
    osLibPrintf(
        "Room Size "
        PRNT_YELW "%.1f " PRNT_RSET
        "KB",
        BinToKb(roomSize)
    );
}

Asm_VanillaHook(Play_Init);
void Play_Init(GameState* __play) {
    PlayState* play = (void*)__play;
    GraphicsContext* gfxCtx = play->state.gfxCtx;
    u32 zAlloc;
    u32 zAllocAligned;
    size_t zAllocSize;
    Player* player;
    s32 playerStartCamId;
    s32 i;
    u8 tempSetupIndex;
    
    // if array was changed to pointer, ensure it points to something!
    //
    // unfortunately, there is no way to check this using C preprocessor,
    // so casting to a pointer-to-pointer is used to achieve polymorphism
    //
    // the compiler will optimize if (false) away
    if (sizeof(play->objectCtx.status) == sizeof(void*))
    {
        // repurpose unused PAL text table for extended object status limit
        ObjectStatus **status = (ObjectStatus**)&play->objectCtx.status;
        *status = (void*)0x80153764; // tail of message entry table
        *status -= OBJECT_EXCHANGE_BANK_MAX;
    }
    
    gLibCtx.state.isPlayGameMode = true;
    
    osLibPrintf("Entrance Index: %04X", gExitParam.nextEntranceIndex);
    osLibPrintf("Cutscene Index: %04X", gSaveContext.cutsceneIndex);
    osLibPrintf(
        "gExitParam:\n"
        PRNT_RSET "\tflag:       %s\n"
        PRNT_RSET "\tmusicOn:    %s\n"
        PRNT_RSET "\ttitleCard:  %s\n"
        PRNT_RSET "\tfadeIn:     " PRNT_BLUE " 0x%02X\n"
        PRNT_RSET "\tfadeOut:    " PRNT_BLUE " 0x%02X\n"
        PRNT_RSET "\tspawnIndex: " PRNT_BLUE " 0x%02X\n"
        PRNT_RSET "\theaderIndex:" PRNT_BLUE " 0x%02X\n"
        PRNT_RSET "\tsceneIndex: " PRNT_BLUE " 0x%02X",
        gExitParam.isExit ? PRNT_GREN " true" : PRNT_REDD "false",
        gExitParam.exit.musicOn ? PRNT_GREN " true" : PRNT_REDD "false",
        gExitParam.exit.titleCard ? PRNT_GREN " true" : PRNT_REDD "false",
        gExitParam.exit.fadeIn,
        gExitParam.exit.fadeOut,
        gExitParam.exit.spawnIndex,
        gExitParam.exit.headerIndex,
        gExitParam.exit.sceneIndex
    );
    
    SystemArena_Display();
    GameState_Realloc(&play->state, 0x3D8000 + 0x3A60);
    KaleidoManager_Init(play);
    View_Init(&play->view, gfxCtx);
    Audio_SetExtraFilter(0);
    Quake_Init();
    
    for (i = 0; i < ARRAY_COUNT(play->cameraPtrs); i++) {
        play->cameraPtrs[i] = NULL;
    }
    
    Camera_Init(&play->mainCamera, &play->view, &play->colCtx, play);
    Camera_ChangeStatus(&play->mainCamera, CAM_STAT_ACTIVE);
    
    for (i = 0; i < 3; i++) {
        Camera_Init(&play->subCameras[i], &play->view, &play->colCtx, play);
        Camera_ChangeStatus(&play->subCameras[i], CAM_STAT_UNK100);
    }
    
    play->cameraPtrs[CAM_ID_MAIN] = &play->mainCamera;
    play->cameraPtrs[CAM_ID_MAIN]->uid = 0;
    play->activeCamId = CAM_ID_MAIN;
    func_8005AC48(&play->mainCamera, 0xFF);
    Sram_Init(play, &play->sramCtx);
    func_80112098(play);
    Message_Init(play);
    GameOver_Init(play);
    SfxSource_InitAll(play);
    Effect_InitContext(play);
    EffectSs_InitInfo(play, 0x55);
    CollisionCheck_InitContext(play, &play->colChkCtx);
    AnimationContext_Reset(&play->animationCtx);
    func_8006450C(play, &play->csCtx);
    
    if (gSaveContext.nextCutsceneIndex != 0xFFEF) {
        gSaveContext.cutsceneIndex = gSaveContext.nextCutsceneIndex;
        gSaveContext.nextCutsceneIndex = 0xFFEF;
    }
    
    if (gSaveContext.cutsceneIndex == 0xFFFD) {
        gSaveContext.cutsceneIndex = 0;
    }
    
    if (gSaveContext.nextDayTime != 0xFFFF) {
        gSaveContext.dayTime = gSaveContext.nextDayTime;
        gSaveContext.skyboxTime = gSaveContext.nextDayTime;
    }
    
    if (gSaveContext.dayTime > 0xC000 || gSaveContext.dayTime < 0x4555) {
        gSaveContext.nightFlag = 1;
    } else {
        gSaveContext.nightFlag = 0;
    }
    
    Cutscene_HandleConditionalTriggers(play);
    
    if (gSaveContext.gameMode != 0 || gSaveContext.cutsceneIndex >= 0xFFF0) {
        gSaveContext.nayrusLoveTimer = 0;
        Magic_Reset(play);
        gSaveContext.sceneLayer = (gSaveContext.cutsceneIndex & 0xF) + 4;
    } else if (!LINK_IS_ADULT && IS_DAY) {
        gSaveContext.sceneLayer = 0;
    } else if (!LINK_IS_ADULT && !IS_DAY) {
        gSaveContext.sceneLayer = 1;
    } else if (LINK_IS_ADULT && IS_DAY) {
        gSaveContext.sceneLayer = 2;
    } else {
        gSaveContext.sceneLayer = 3;
    }
    
    u32 sceneNum = 0;
    u32 sceneSpawn = 0;
    
    if (gExitParam.isExit == false) {
        tempSetupIndex = gSaveContext.sceneLayer;
        if (
            (gEntranceTable[gSaveContext.entranceIndex].sceneId == SCENE_SPOT00) && !LINK_IS_ADULT &&
            gSaveContext.sceneLayer < 4
        ) {
            if (
                CHECK_QUEST_ITEM(QUEST_KOKIRI_EMERALD) && CHECK_QUEST_ITEM(QUEST_GORON_RUBY) &&
                CHECK_QUEST_ITEM(QUEST_ZORA_SAPPHIRE)
            ) {
                gSaveContext.sceneLayer = 1;
            } else {
                gSaveContext.sceneLayer = 0;
            }
        } else if (
            (gEntranceTable[gSaveContext.entranceIndex].sceneId == SCENE_SPOT04) && LINK_IS_ADULT &&
            gSaveContext.sceneLayer < 4
        ) {
            gSaveContext.sceneLayer = (gSaveContext.eventChkInf[4] & 0x100) ? 3 : 2;
        }
        
        sceneNum = gEntranceTable[gSaveContext.entranceIndex + gSaveContext.sceneLayer].sceneId;
        sceneSpawn = gEntranceTable[gSaveContext.sceneLayer + gSaveContext.entranceIndex].spawn;
    } else {
        sceneNum = gExitParam.exit.sceneIndex;
        if (gExitParam.exit.headerIndex != 0xF)
            gSaveContext.sceneLayer = gExitParam.exit.headerIndex;
        sceneSpawn = gExitParam.exit.spawnIndex;
    }
    
    Play_SpawnScene(
        play,
        sceneNum,
        sceneSpawn
    );
    
    Cutscene_HandleEntranceTriggers(play);
    KaleidoScopeCall_Init(play);
    Interface_Init(play);
    
    if (gSaveContext.nextDayTime != 0xFFFF) {
        if (gSaveContext.nextDayTime == 0x8001) {
            gSaveContext.totalDays++;
            gSaveContext.bgsDayCount++;
            gSaveContext.dogIsLost = true;
            if (
                Inventory_ReplaceItem(play, ITEM_WEIRD_EGG, ITEM_CHICKEN) ||
                Inventory_ReplaceItem(play, ITEM_POCKET_EGG, ITEM_POCKET_CUCCO)
            ) {
                Message_StartTextbox(play, 0x3066, NULL);
            }
            gSaveContext.nextDayTime = 0xFFFE;
        } else {
            gSaveContext.nextDayTime = 0xFFFD;
        }
    }
    
    SREG(91) = -1;
    R_PAUSE_MENU_MODE = 0;
    PreRender_Init(&play->pauseBgPreRender);
    PreRender_SetValuesSave(&play->pauseBgPreRender, SCREEN_WIDTH, SCREEN_HEIGHT, 0, 0, 0);
    PreRender_SetValues(&play->pauseBgPreRender, SCREEN_WIDTH, SCREEN_HEIGHT, 0, 0);
    gTrnsnUnkState = 0;
    play->transitionMode = 0;
    FrameAdvance_Init(&play->frameAdvCtx);
    Rand_Seed((u32)osGetTime());
    Matrix_Init(&play->state);
    play->state.main = Play_Main;
    play->state.destroy = Play_Destroy;
    play->transitionTrigger = -0x14;
    play->unk_11E16 = 0xFF;
    play->unk_11E18 = 0;
    play->unk_11DE9 = 0;
    
    if (gSaveContext.gameMode != 1) {
        if (gSaveContext.nextTransitionType == TRANS_NEXT_TYPE_DEFAULT) {
            if (gExitParam.isExit == false)
                play->transitionType = (gEntranceTable[gSaveContext.entranceIndex + tempSetupIndex].field >> 7) & 0x7F;
            else
                play->transitionType = gExitParam.exit.fadeIn;
        } else {
            play->transitionType = gSaveContext.nextTransitionType;
            gSaveContext.nextTransitionType = TRANS_NEXT_TYPE_DEFAULT;
        }
    } else {
        play->transitionType = TRANS_TYPE_FADE_BLACK_SLOW;
    }
    
    Letterbox_Init();
    TransitionFade_Init(&play->transitionFade);
    TransitionFade_SetType(&play->transitionFade, 3);
    TransitionFade_SetColor(&play->transitionFade, RGBA8(160, 160, 160, 255));
    TransitionFade_Start(&play->transitionFade);
    VisMono_Init(&D_80161498);
    D_801614B0.a = 0;
    Flags_UnsetAllEnv(play);
    
    zAllocSize = THA_GetSize(&play->state.tha);
    zAlloc = (u32)GameState_Alloc(&play->state, zAllocSize, "../z_play.c", 2918);
    zAllocAligned = (zAlloc + 8) & ~0xF;
    ZeldaArena_Init((void*)zAllocAligned, zAllocSize - zAllocAligned + zAlloc);
    
    Fault_AddClient(&D_801614B8, ZeldaArena_Display, NULL, NULL);
    func_800304DC(play, &play->actorCtx, play->linkActorEntry);
    
    while (!func_800973FC(play, &play->roomCtx));
    
    player = GET_PLAYER(play);
    Camera_InitPlayerSettings(&play->mainCamera, player);
    Camera_ChangeMode(&play->mainCamera, CAM_MODE_NORMAL);
    
    playerStartCamId = player->actor.params & 0xFF;
    if (playerStartCamId != 0xFF) {
        Camera_ChangeBgCamIndex(&play->mainCamera, playerStartCamId);
    }
    
    if (R_SCENE_CAM_TYPE == SCENE_CAM_TYPE_FIXED_TOGGLE_VIEWPOINT) {
        play->viewpoint = VIEWPOINT_PIVOT;
    } else if (R_SCENE_CAM_TYPE == SCENE_CAM_TYPE_FIXED_SHOP_VIEWPOINT) {
        play->viewpoint = VIEWPOINT_LOCKED;
    } else {
        play->viewpoint = VIEWPOINT_NONE;
    }
    
    Interface_SetSceneRestrictions(play);
    Environment_PlaySceneSequence(play);
    gSaveContext.seqId = play->sequenceCtx.seqId;
    gSaveContext.natureAmbienceId = play->sequenceCtx.natureAmbienceId;
    func_8002DF18(play, GET_PLAYER(play));
    AnimationContext_Update(play, &play->animationCtx);
    gSaveContext.respawnFlag = 0;
    
    if (dREG(95) != 0) {
        D_8012D1F0 = D_801614D0;
        osSyncPrintf("\nkawauso_data=[%x]", D_8012D1F0);
        DmaMgr_DmaRomToRam(0x03FEB000, D_8012D1F0, sizeof(D_801614D0));
    }
    
    extern void SoundEffect_Init();
    SoundEffect_Init();
}

Asm_VanillaHook(Play_Draw);
void Play_Draw(PlayState* playState) {
    GraphicsContext* gfxCtx = playState->state.gfxCtx;
    
    gSegments[4] = VIRTUAL_TO_PHYSICAL(playState->objectCtx.status[playState->objectCtx.mainKeepIndex].segment);
    gSegments[5] = VIRTUAL_TO_PHYSICAL(playState->objectCtx.status[playState->objectCtx.subKeepIndex].segment);
    gSegments[2] = VIRTUAL_TO_PHYSICAL(playState->sceneSegment);
    
    gSPSegment(POLY_OPA_DISP++, 0x00, NULL);
    gSPSegment(POLY_XLU_DISP++, 0x00, NULL);
    gSPSegment(OVERLAY_DISP++, 0x00, NULL);
    
    gSPSegment(POLY_OPA_DISP++, 0x04, playState->objectCtx.status[playState->objectCtx.mainKeepIndex].segment);
    gSPSegment(POLY_XLU_DISP++, 0x04, playState->objectCtx.status[playState->objectCtx.mainKeepIndex].segment);
    gSPSegment(OVERLAY_DISP++, 0x04, playState->objectCtx.status[playState->objectCtx.mainKeepIndex].segment);
    
    gSPSegment(POLY_OPA_DISP++, 0x05, playState->objectCtx.status[playState->objectCtx.subKeepIndex].segment);
    gSPSegment(POLY_XLU_DISP++, 0x05, playState->objectCtx.status[playState->objectCtx.subKeepIndex].segment);
    gSPSegment(OVERLAY_DISP++, 0x05, playState->objectCtx.status[playState->objectCtx.subKeepIndex].segment);
    
#if SEGMENT_0x06_FOR_SCENES
    gSegments[6] = VIRTUAL_TO_PHYSICAL(playState->objectCtx.status[sSceneSegmentObj0x06].segment);
    gSPSegment(POLY_OPA_DISP++, 0x06, playState->objectCtx.status[sSceneSegmentObj0x06].segment);
    gSPSegment(POLY_XLU_DISP++, 0x06, playState->objectCtx.status[sSceneSegmentObj0x06].segment);
    gSPSegment(OVERLAY_DISP++, 0x06, playState->objectCtx.status[sSceneSegmentObj0x06].segment);
#endif
    
    gSPSegment(POLY_OPA_DISP++, 0x02, playState->sceneSegment);
    gSPSegment(POLY_XLU_DISP++, 0x02, playState->sceneSegment);
    gSPSegment(OVERLAY_DISP++, 0x02, playState->sceneSegment);
    
    Gfx_SetupFrame(gfxCtx, 0, 0, 0);
    
    if ((HREG(80) != 10) || (HREG(82) != 0)){
        z64rom_PrePlayDraw(playState);
        Play_Draw2(playState);
        z64rom_PostPlayDraw(playState);
    }
    
    if (playState->view.unk_124 != 0) {
        Camera_Update(GET_ACTIVE_CAM(playState));
        View_UpdateViewingMatrix(&playState->view);
        playState->view.unk_124 = 0;
        if (playState->skyboxId && (playState->skyboxId != SKYBOX_UNSET_1D) && !playState->envCtx.skyboxDisabled) {
            SkyboxDraw_UpdateMatrix(
                &playState->skyboxCtx,
                playState->view.eye.x,
                playState->view.eye.y,
                playState->view.eye.z
            );
        }
    }
    
    Camera_Finish(GET_ACTIVE_CAM(playState));
}

Asm_VanillaHook(Play_Update);
void Play_Update(PlayState* play) {
    s32 sp80 = 0;
    Input* input;
    
    input = play->state.input;
    
    gSegments[4] = VIRTUAL_TO_PHYSICAL(play->objectCtx.status[play->objectCtx.mainKeepIndex].segment);
    gSegments[5] = VIRTUAL_TO_PHYSICAL(play->objectCtx.status[play->objectCtx.subKeepIndex].segment);
    gSegments[2] = VIRTUAL_TO_PHYSICAL(play->sceneSegment);
    
    if (Play_FrameAdvance(play)) {
        if ((play->transitionMode == TRANS_MODE_OFF) && (play->transitionTrigger != TRANS_TRIGGER_OFF)) {
            play->transitionMode = TRANS_MODE_SETUP;
        }
        
        if (gTrnsnUnkState != 0) {
            switch (gTrnsnUnkState) {
                case 2:
                    if (TransitionUnk_Init(&sTrnsnUnk, 10, 7) == NULL) {
                        gTrnsnUnkState = 0;
                    } else {
                        sTrnsnUnk.zBuffer = (u16*)gZBuffer;
                        gTrnsnUnkState = 3;
                        R_UPDATE_RATE = 1;
                    }
                    break;
                case 3:
                    func_800B23E8(&sTrnsnUnk);
                    break;
            }
        }
        
        if (play->transitionMode) {
            switch (play->transitionMode) {
                case TRANS_MODE_SETUP:
                    if (play->transitionTrigger != TRANS_TRIGGER_END) {
                        s16 sceneSetupIndex = 0;
                        s16 fadeOut = false;
                        
                        Interface_ChangeAlpha(1);
                        
                        if (gSaveContext.cutsceneIndex >= 0xFFF0) {
                            sceneSetupIndex = (gSaveContext.cutsceneIndex & 0xF) + 4;
                        }
                        
                        // z64rom
                        
                        if (gExitParam.isExit == false) {
                            if (!(gEntranceTable[gExitParam.nextEntranceIndex + sceneSetupIndex].field & 0x8000))
                                fadeOut = true;
                        } else {
                            if (gExitParam.exit.musicOn == false)
                                fadeOut = true;
                        }
                        
                        // fade out bgm if "continue bgm" flag is not set
                        if (fadeOut) {
                            if (
                                (play->transitionType < TRANS_TYPE_MAX) &&
                                !Environment_IsForcedSequenceDisabled()
                            ) {
                                func_800F6964(0x14);
                                gSaveContext.seqId = (u8)NA_BGM_DISABLED;
                                gSaveContext.natureAmbienceId = NATURE_ID_DISABLED;
                            }
                        }
                    }
                    
                    if (!R_TRANS_DBG_ENABLED)
                        Play_SetupTransition(play, play->transitionType);
                    else
                        Play_SetupTransition(play, R_TRANS_DBG_TYPE);
                    
                    if (play->transitionMode >= TRANS_MODE_FILL_WHITE_INIT) {
                        // non-instance modes break out of this switch
                        break;
                    }
                // fallthrough
                case TRANS_MODE_INSTANCE_INIT:
                    play->transitionCtx.init(&play->transitionCtx.instanceData);
                    
                    // circle types
                    if ((play->transitionCtx.transitionType >> 5) == 1) {
                        play->transitionCtx.setType(
                            &play->transitionCtx.instanceData,
                            play->transitionCtx.transitionType | TC_SET_PARAMS
                        );
                    }
                    
                    gSaveContext.transWipeSpeed = 14;
                    
                    if (
                        (play->transitionCtx.transitionType == TRANS_TYPE_WIPE_FAST) ||
                        (play->transitionCtx.transitionType == TRANS_TYPE_FILL_WHITE2)
                    ) {
                        //! @bug TRANS_TYPE_FILL_WHITE2 will never reach this code.
                        //! It is a non-instance type transition which doesn't run this case.
                        gSaveContext.transWipeSpeed = 28;
                    }
                    
                    gSaveContext.transFadeDuration = 60;
                    
                    if (
                        (play->transitionCtx.transitionType == TRANS_TYPE_FADE_BLACK_FAST) ||
                        (play->transitionCtx.transitionType == TRANS_TYPE_FADE_WHITE_FAST)
                    ) {
                        gSaveContext.transFadeDuration = 20;
                    } else if (
                        (play->transitionCtx.transitionType == TRANS_TYPE_FADE_BLACK_SLOW) ||
                        (play->transitionCtx.transitionType == TRANS_TYPE_FADE_WHITE_SLOW)
                    ) {
                        gSaveContext.transFadeDuration = 150;
                    } else if (play->transitionCtx.transitionType == TRANS_TYPE_FADE_WHITE_INSTANT) {
                        gSaveContext.transFadeDuration = 2;
                    }
                    
                    if (
                        (play->transitionCtx.transitionType == TRANS_TYPE_FADE_WHITE) ||
                        (play->transitionCtx.transitionType == TRANS_TYPE_FADE_WHITE_FAST) ||
                        (play->transitionCtx.transitionType == TRANS_TYPE_FADE_WHITE_SLOW) ||
                        (play->transitionCtx.transitionType == TRANS_TYPE_FADE_WHITE_CS_DELAYED) ||
                        (play->transitionCtx.transitionType == TRANS_TYPE_FADE_WHITE_INSTANT)
                    ) {
                        play->transitionCtx.setColor(
                            &play->transitionCtx.instanceData,
                            RGBA8(160, 160, 160, 255)
                        );
                        if (play->transitionCtx.setUnkColor != NULL) {
                            play->transitionCtx.setUnkColor(
                                &play->transitionCtx.instanceData,
                                RGBA8(160, 160, 160, 255)
                            );
                        }
                    } else if (play->transitionCtx.transitionType == TRANS_TYPE_FADE_GREEN) {
                        play->transitionCtx.setColor(
                            &play->transitionCtx.instanceData,
                            RGBA8(140, 140, 100, 255)
                        );
                        
                        if (play->transitionCtx.setUnkColor != NULL) {
                            play->transitionCtx.setUnkColor(
                                &play->transitionCtx.instanceData,
                                RGBA8(140, 140, 100, 255)
                            );
                        }
                    } else if (play->transitionCtx.transitionType == TRANS_TYPE_FADE_BLUE) {
                        play->transitionCtx.setColor(
                            &play->transitionCtx.instanceData,
                            RGBA8(70, 100, 110, 255)
                        );
                        
                        if (play->transitionCtx.setUnkColor != NULL) {
                            play->transitionCtx.setUnkColor(
                                &play->transitionCtx.instanceData,
                                RGBA8(70, 100, 110, 255)
                            );
                        }
                    } else {
                        play->transitionCtx.setColor(&play->transitionCtx.instanceData, RGBA8(0, 0, 0, 0));
                        
                        if (play->transitionCtx.setUnkColor != NULL) {
                            play->transitionCtx.setUnkColor(
                                &play->transitionCtx.instanceData,
                                RGBA8(0, 0, 0, 0)
                            );
                        }
                    }
                    
                    if (play->transitionTrigger == TRANS_TRIGGER_END) {
                        play->transitionCtx.setType(&play->transitionCtx.instanceData, 1);
                    } else {
                        play->transitionCtx.setType(&play->transitionCtx.instanceData, 2);
                    }
                    
                    play->transitionCtx.start(&play->transitionCtx.instanceData);
                    
                    if (play->transitionCtx.transitionType == TRANS_TYPE_FADE_WHITE_CS_DELAYED) {
                        play->transitionMode = TRANS_MODE_INSTANCE_WAIT;
                    } else {
                        play->transitionMode = TRANS_MODE_INSTANCE_RUNNING;
                    }
                    break;
                    
                case TRANS_MODE_INSTANCE_RUNNING:
                    if (play->transitionCtx.isDone(&play->transitionCtx.instanceData)) {
                        if (play->transitionCtx.transitionType >= TRANS_TYPE_MAX) {
                            if (play->transitionTrigger == TRANS_TRIGGER_END) {
                                play->transitionCtx.destroy(&play->transitionCtx.instanceData);
                                func_800BC88C(play);
                                play->transitionMode = TRANS_MODE_OFF;
                            }
                        } else if (play->transitionTrigger != TRANS_TRIGGER_END) {
                            play->state.running = false;
                            
                            if (gSaveContext.gameMode != 2) {
                                SET_NEXT_GAMESTATE(&play->state, Play_Init, PlayState);
                                gSaveContext.entranceIndex = gExitParam.nextEntranceIndex;
                                
                                if (gSaveContext.minigameState == 1) {
                                    gSaveContext.minigameState = 3;
                                }
                            } else {
                                SET_NEXT_GAMESTATE(&play->state, FileSelect_Init, FileSelectState);
                            }
                        } else {
                            play->transitionCtx.destroy(&play->transitionCtx.instanceData);
                            func_800BC88C(play);
                            play->transitionMode = TRANS_MODE_OFF;
                            
                            if (gTrnsnUnkState == 3) {
                                TransitionUnk_Destroy(&sTrnsnUnk);
                                gTrnsnUnkState = 0;
                                R_UPDATE_RATE = 3;
                            }
                        }
                        
                        play->transitionTrigger = TRANS_TRIGGER_OFF;
                    } else {
                        play->transitionCtx.update(&play->transitionCtx.instanceData, R_UPDATE_RATE);
                    }
                    break;
            }
            
            // update non-instance transitions
            switch (play->transitionMode) {
                case TRANS_MODE_FILL_WHITE_INIT:
                    sTransitionFillTimer = 0;
                    play->envCtx.fillScreen = true;
                    play->envCtx.screenFillColor[0] = 160;
                    play->envCtx.screenFillColor[1] = 160;
                    play->envCtx.screenFillColor[2] = 160;
                    
                    if (play->transitionTrigger != TRANS_TRIGGER_END) {
                        play->envCtx.screenFillColor[3] = 0;
                        play->transitionMode = TRANS_MODE_FILL_IN;
                    } else {
                        play->envCtx.screenFillColor[3] = 255;
                        play->transitionMode = TRANS_MODE_FILL_OUT;
                    }
                    break;
                    
                case TRANS_MODE_FILL_IN:
                    play->envCtx.screenFillColor[3] = (sTransitionFillTimer / 20.0f) * 255.0f;
                    
                    if (sTransitionFillTimer >= 20) {
                        play->state.running = false;
                        SET_NEXT_GAMESTATE(&play->state, Play_Init, PlayState);
                        gSaveContext.entranceIndex = gExitParam.nextEntranceIndex;
                        play->transitionTrigger = TRANS_TRIGGER_OFF;
                        play->transitionMode = TRANS_MODE_OFF;
                    } else {
                        sTransitionFillTimer++;
                    }
                    break;
                    
                case TRANS_MODE_FILL_OUT:
                    play->envCtx.screenFillColor[3] = (1 - sTransitionFillTimer / 20.0f) * 255.0f;
                    
                    if (sTransitionFillTimer >= 20) {
                        gTrnsnUnkState = 0;
                        R_UPDATE_RATE = 3;
                        play->transitionTrigger = TRANS_TRIGGER_OFF;
                        play->transitionMode = TRANS_MODE_OFF;
                        play->envCtx.fillScreen = false;
                    } else {
                        sTransitionFillTimer++;
                    }
                    break;
                    
                case TRANS_MODE_FILL_BROWN_INIT:
                    sTransitionFillTimer = 0;
                    play->envCtx.fillScreen = true;
                    play->envCtx.screenFillColor[0] = 170;
                    play->envCtx.screenFillColor[1] = 160;
                    play->envCtx.screenFillColor[2] = 150;
                    
                    if (play->transitionTrigger != TRANS_TRIGGER_END) {
                        play->envCtx.screenFillColor[3] = 0;
                        play->transitionMode = TRANS_MODE_FILL_IN;
                    } else {
                        play->envCtx.screenFillColor[3] = 255;
                        play->transitionMode = TRANS_MODE_FILL_OUT;
                    }
                    break;
                    
                case TRANS_MODE_INSTANT:
                    if (play->transitionTrigger != TRANS_TRIGGER_END) {
                        play->state.running = false;
                        SET_NEXT_GAMESTATE(&play->state, Play_Init, PlayState);
                        gSaveContext.entranceIndex = gExitParam.nextEntranceIndex;
                        play->transitionTrigger = TRANS_TRIGGER_OFF;
                        play->transitionMode = TRANS_MODE_OFF;
                    } else {
                        gTrnsnUnkState = 0;
                        R_UPDATE_RATE = 3;
                        play->transitionTrigger = TRANS_TRIGGER_OFF;
                        play->transitionMode = TRANS_MODE_OFF;
                    }
                    break;
                    
                case TRANS_MODE_INSTANCE_WAIT:
                    if (gSaveContext.cutsceneTransitionControl != 0) {
                        play->transitionMode = TRANS_MODE_INSTANCE_RUNNING;
                    }
                    break;
                    
                case TRANS_MODE_SANDSTORM_INIT:
                    if (play->transitionTrigger != TRANS_TRIGGER_END) {
                        // trigger in, leaving area
                        play->envCtx.sandstormState = SANDSTORM_FILL;
                        play->transitionMode = TRANS_MODE_SANDSTORM;
                    } else {
                        play->envCtx.sandstormState = SANDSTORM_UNFILL;
                        play->envCtx.sandstormPrimA = 255;
                        play->envCtx.sandstormEnvA = 255;
                        play->transitionMode = TRANS_MODE_SANDSTORM;
                    }
                    break;
                    
                case TRANS_MODE_SANDSTORM:
                    Audio_PlaySfxGeneral(
                        NA_SE_EV_SAND_STORM - SFX_FLAG,
                        &gSfxDefaultPos,
                        4,
                        &gSfxDefaultFreqAndVolScale,
                        &gSfxDefaultFreqAndVolScale,
                        &gSfxDefaultReverb
                    );
                    
                    if (play->transitionTrigger == TRANS_TRIGGER_END) {
                        if (play->envCtx.sandstormPrimA < 110) {
                            gTrnsnUnkState = 0;
                            R_UPDATE_RATE = 3;
                            play->transitionTrigger = TRANS_TRIGGER_OFF;
                            play->transitionMode = TRANS_MODE_OFF;
                        }
                    } else {
                        if (play->envCtx.sandstormEnvA == 255) {
                            play->state.running = false;
                            SET_NEXT_GAMESTATE(&play->state, Play_Init, PlayState);
                            gSaveContext.entranceIndex = gExitParam.nextEntranceIndex;
                            play->transitionTrigger = TRANS_TRIGGER_OFF;
                            play->transitionMode = TRANS_MODE_OFF;
                        }
                    }
                    break;
                    
                case TRANS_MODE_SANDSTORM_END_INIT:
                    if (play->transitionTrigger == TRANS_TRIGGER_END) {
                        play->envCtx.sandstormState = SANDSTORM_DISSIPATE;
                        play->envCtx.sandstormPrimA = 255;
                        play->envCtx.sandstormEnvA = 255;
                        play->transitionMode = TRANS_MODE_SANDSTORM_END;
                    } else {
                        play->transitionMode = TRANS_MODE_SANDSTORM_INIT;
                    }
                    break;
                    
                case TRANS_MODE_SANDSTORM_END:
                    Audio_PlaySfxGeneral(
                        NA_SE_EV_SAND_STORM - SFX_FLAG,
                        &gSfxDefaultPos,
                        4,
                        &gSfxDefaultFreqAndVolScale,
                        &gSfxDefaultFreqAndVolScale,
                        &gSfxDefaultReverb
                    );
                    if (play->transitionTrigger == TRANS_TRIGGER_END) {
                        if (play->envCtx.sandstormPrimA <= 0) {
                            gTrnsnUnkState = 0;
                            R_UPDATE_RATE = 3;
                            play->transitionTrigger = TRANS_TRIGGER_OFF;
                            play->transitionMode = TRANS_MODE_OFF;
                        }
                    }
                    break;
                    
                case TRANS_MODE_CS_BLACK_FILL_INIT:
                    sTransitionFillTimer = 0;
                    play->envCtx.fillScreen = true;
                    play->envCtx.screenFillColor[0] = 0;
                    play->envCtx.screenFillColor[1] = 0;
                    play->envCtx.screenFillColor[2] = 0;
                    play->envCtx.screenFillColor[3] = 255;
                    play->transitionMode = TRANS_MODE_CS_BLACK_FILL;
                    break;
                    
                case TRANS_MODE_CS_BLACK_FILL:
                    if (gSaveContext.cutsceneTransitionControl != 0) {
                        play->envCtx.screenFillColor[3] = gSaveContext.cutsceneTransitionControl;
                        if (gSaveContext.cutsceneTransitionControl <= 100) {
                            gTrnsnUnkState = 0;
                            R_UPDATE_RATE = 3;
                            play->transitionTrigger = TRANS_TRIGGER_OFF;
                            play->transitionMode = TRANS_MODE_OFF;
                        }
                    }
                    break;
            }
        }
        
        if (1 && (gTrnsnUnkState != 3)) {
            z64rom_PrePlayUpdate(play);
            if (
                (gSaveContext.gameMode == 0) && (play->msgCtx.msgMode == MSGMODE_NONE) &&
                (play->gameOverCtx.state == GAMEOVER_INACTIVE)
            ) {
                KaleidoSetup_Update(play);
            }
            
            sp80 = (play->pauseCtx.state != 0) || (play->pauseCtx.debugState != 0);
            AnimationContext_Reset(&play->animationCtx);
            Object_UpdateBank(&play->objectCtx);
            
            if ((sp80 == 0) && (IREG(72) == 0)) {
                play->gameplayFrames++;
                
                Rumble_SetUpdateEnabled(true);
                
                if (play->actorCtx.freezeFlashTimer && (play->actorCtx.freezeFlashTimer-- < 5)) {
                    if (
                        (play->actorCtx.freezeFlashTimer > 0) &&
                        ((play->actorCtx.freezeFlashTimer % 2) != 0)
                    ) {
                        play->envCtx.fillScreen = true;
                        play->envCtx.screenFillColor[0] = play->envCtx.screenFillColor[1] =
                            play->envCtx.screenFillColor[2] = 150;
                        play->envCtx.screenFillColor[3] = 80;
                    } else {
                        play->envCtx.fillScreen = false;
                    }
                } else {
                    func_800973FC(play, &play->roomCtx);
                    
                    CollisionCheck_AT(play, &play->colChkCtx);
                    CollisionCheck_OC(play, &play->colChkCtx);
                    CollisionCheck_Damage(play, &play->colChkCtx);
                    CollisionCheck_ClearContext(play, &play->colChkCtx);
                    
                    if (!play->unk_11DE9) {
                        Profiler_Start(&gLibCtx.profiler.actorUpdate);
                        Actor_UpdateAll(play, &play->actorCtx);
                        Profiler_End(&gLibCtx.profiler.actorUpdate);
                    }
                    
                    func_80064558(play, &play->csCtx);
                    func_800645A0(play, &play->csCtx);
                    Effect_UpdateAll(play);
                    EffectSs_UpdateAll(play);
                }
            } else {
                Rumble_SetUpdateEnabled(false);
            }
            
            func_80095AA0(play, &play->roomCtx.curRoom, &input[1], 0);
            func_80095AA0(play, &play->roomCtx.prevRoom, &input[1], 1);
            
            if (play->viewpoint != VIEWPOINT_NONE) {
                if (CHECK_BTN_ALL(input[0].press.button, BTN_CUP)) {
                    if ((play->pauseCtx.state != 0) || (play->pauseCtx.debugState != 0)) {
                    } else if (Player_InCsMode(play)) {
                    } else if (YREG(15) == 0x10) {
                        Audio_PlaySfxGeneral(
                            NA_SE_SY_ERROR,
                            &gSfxDefaultPos,
                            4,
                            &gSfxDefaultFreqAndVolScale,
                            &gSfxDefaultFreqAndVolScale,
                            &gSfxDefaultReverb
                        );
                    } else {
                        Play_SetViewpoint(play, play->viewpoint ^ (VIEWPOINT_LOCKED ^ VIEWPOINT_PIVOT));
                    }
                }
                Play_ChangeViewpointBgCamIndex(play);
            }
            
            SkyboxDraw_Update(&play->skyboxCtx);
            
            if ((play->pauseCtx.state != 0) || (play->pauseCtx.debugState != 0)) {
                KaleidoScopeCall_Update(play);
            } else if (play->gameOverCtx.state != GAMEOVER_INACTIVE) {
                GameOver_Update(play);
            } else {
                Message_Update(play);
            }
            
            Interface_Update(play);
            AnimationContext_Update(play, &play->animationCtx);
            SfxSource_UpdateAll(play);
            Letterbox_Update(R_UPDATE_RATE);
            TransitionFade_Update(&play->transitionFade, R_UPDATE_RATE);
            z64rom_PostPlayUpdate(play);
        } else {
            goto skip;
        }
    }
skip:
    
    if ((sp80 == 0) || (gDbgCamEnabled)) {
        s32 i;
        
        play->nextCamId = play->activeCamId;
        
        Profiler_Start(&gLibCtx.profiler.cameraUpdate);
        for (i = 0; i < NUM_CAMS; i++) {
            if ((i != play->nextCamId) && (play->cameraPtrs[i] != NULL)) {
                Camera_Update(play->cameraPtrs[i]);
            }
        }
        
        Camera_Update(play->cameraPtrs[play->nextCamId]);
        Profiler_End(&gLibCtx.profiler.cameraUpdate);
    }
    
    Environment_Update(
        play,
        &play->envCtx,
        &play->lightCtx,
        &play->pauseCtx,
        &play->msgCtx,
        &play->gameOverCtx,
        play->state.gfxCtx
    );
}

Asm_VanillaHook(Play_Main);
void Play_Main(GameState* __play) {
    PlayState* play = (void*)__play;
    
    D_8012D1F8 = &play->state.input[0];
    
    if ((HREG(80) == 10) && (HREG(94) != 10)) {
        HREG(81) = 1;
        HREG(82) = 1;
        HREG(83) = 1;
        HREG(84) = 3;
        HREG(85) = 1;
        HREG(86) = 1;
        HREG(87) = 1;
        HREG(88) = 1;
        HREG(89) = 1;
        HREG(90) = 15;
        HREG(91) = 1;
        HREG(92) = 1;
        HREG(93) = 1;
        HREG(94) = 10;
    }
    
    if ((HREG(80) != 10) || (HREG(81) != 0))
        Play_Update(play);
    Play_Draw(play);
    
#ifdef DEV_BUILD
    DebugMenu_Update(play);
#endif
}

Asm_VanillaHook(Play_SetupRespawnPoint);
void Play_SetupRespawnPoint(PlayState* this, s32 respawnMode, s32 playerParams) {
    RespawnData* respawnData = &gSaveContext.respawn[respawnMode];
    Player* player = GET_PLAYER(this);
    s32 entranceIndex = gSaveContext.entranceIndex;
    s8 roomIndex = this->roomCtx.curRoom.num;
    
    MemCpy(&gExitParam.respawn[respawnMode], &gExitParam.exit, sizeof(NewExit));
    
    respawnData->entranceIndex = entranceIndex;
    respawnData->roomIndex = roomIndex;
    respawnData->pos = player->actor.world.pos;
    respawnData->yaw = player->actor.shape.rot.y;
    respawnData->playerParams = playerParams;
    respawnData->tempSwchFlags = this->actorCtx.flags.tempSwch;
    respawnData->tempCollectFlags = this->actorCtx.flags.tempCollect;
}

Asm_VanillaHook(Play_Destroy);
void Play_Destroy(GameState* thisx) {
    PlayState* this = (PlayState*)thisx;
    Player* player = GET_PLAYER(this);
    
    this->state.gfxCtx->callback = NULL;
    this->state.gfxCtx->callbackParam = 0;
    SREG(91) = 0;
    R_PAUSE_MENU_MODE = 0;
    
    PreRender_Destroy(&this->pauseBgPreRender);
    Effect_DeleteAll(this);
    EffectSs_ClearAll(this);
    CollisionCheck_DestroyContext(this, &this->colChkCtx);
    
    if (gTrnsnUnkState == 3) {
        TransitionUnk_Destroy(&sTrnsnUnk);
        gTrnsnUnkState = 0;
    }
    
    if (this->transitionMode == TRANS_MODE_INSTANCE_RUNNING) {
        this->transitionCtx.destroy(&this->transitionCtx.instanceData);
        func_800BC88C(this);
        this->transitionMode = TRANS_MODE_OFF;
    }
    
    Letterbox_Destroy();
    TransitionFade_Destroy(&this->transitionFade);
    VisMono_Destroy(&D_80161498);
    
    if (gSaveContext.linkAge != this->linkAgeOnLoad) {
        Inventory_SwapAgeEquipment();
        Player_SetEquipmentData(this, player);
    }
    
    func_80031C3C(&this->actorCtx, this);
    Interface_Destroy(this);
    KaleidoScopeCall_Destroy(this);
    KaleidoManager_Destroy();
    ZeldaArena_Cleanup();
    Fault_RemoveClient(&D_801614B8);
    
    gLibCtx.state.isPlayGameMode = false;
}
