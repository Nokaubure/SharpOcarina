
#include <uLib.h>
#include <code/z_demo.h>
//Version: 1.1
/*This version value is used by SharpOcarina to determine if it needs to update the Cutscene.c of an old project
to use newly added features. Put a high value like 99 to stop SharpOcarina from ever asking to update it again.
*/



typedef enum {
    TYPE_EVENT_CHK_INF,
    TYPE_ITEM_GET_INF,
    TYPE_EVENT_INF,
    TYPE_ITEM,
    TYPE_QUEST_ITEM,
    TYPE_SWITCH_FLAG,
} CmdType;

typedef struct {
    struct {
        u8 lastCmd : 1; // O***-****
        u8 set     : 1; // *O**-****
        u8 type    : 6; // **OO-OOOO
    };
    u8 flag;
} CmdFlag;

typedef struct {
    /* 0x00 */ u16     cmdId;
    /* 0x02 */ u16     __padding;
    /* 0x04 */ NewExit exitParam;
    /* 0x08 */ CmdFlag list[];
} CmdHeader;

#define CLEAR_ITEMGETINF(flag) (gSaveContext.itemGetInf[(flag) >> 4] &= ~(1 << ((flag) & 0xF)))

#if MOTION_BLUR

s16 MotionBlurCacheDif = 0;

static void CutsceneCmd_MotionBlur(PlayState* play, CutsceneContext* csCtx, CsCmdBase* cmd) {

        if ((csCtx->frames >= cmd->startFrame) && (csCtx->frames <= cmd->endFrame)) {

            s16 tmp = (u8)play->motionBlurAlpha;
            u8 speed = CLAMP_MIN(cmd->endFrame-cmd->startFrame,1);
            if (MotionBlurCacheDif == 0) MotionBlurCacheDif = cmd->base - (u8)play->motionBlurAlpha;
            s16 add = (s16)(MotionBlurCacheDif * (1.0f / speed) + 0.5f);
            tmp += add;
            if (tmp < 0) tmp = 0;
            if (tmp > 255) tmp = 255;
            
            if (csCtx->frames == cmd->endFrame) 
            {
                    MotionBlurCacheDif = 0;
                    tmp = cmd->base;
            }
            play->motionBlurAlpha = tmp;
        }
    
}

#endif

static void* CutsceneCmd_ExitParam(PlayState* play, void* ptr) {
    CmdHeader* cmd = ptr;
    
    play->transitionTrigger = TRANS_TRIGGER_START;
    gExitParam.nextEntranceIndex = cmd->exitParam.upper;
    gExitParam.exit = cmd->exitParam;
    
    for (s32 i = 0;; i++) {
        CmdType type = cmd->list[i].type;
        u16 flag = cmd->list[i].flag;
        s8 set = cmd->list[i].set;
        
        switch (type) {
            case TYPE_EVENT_CHK_INF:
                if (set)
                    SET_EVENTCHKINF(flag);
                else
                    CLEAR_EVENTCHKINF(flag);
                break;
                
            case TYPE_ITEM_GET_INF:
                if (set)
                    SET_ITEMGETINF(flag);
                else
                    CLEAR_ITEMGETINF(flag);
                break;
                
            case TYPE_EVENT_INF:
                if (set)
                    SET_EVENTINF(flag);
                else
                    CLEAR_EVENTINF(flag);
                break;
                
            case TYPE_ITEM:
                if (set)
                    Item_Give(play, flag);
                else
                    Inventory_DeleteItem(flag, gItemSlots[flag]);
                break;
                
            case TYPE_QUEST_ITEM:
                if (set)
                    gSaveContext.inventory.questItems |= (1 << flag);
                else
                    gSaveContext.inventory.questItems &= ~(1 << flag);
                break;
                
            case TYPE_SWITCH_FLAG:
                if (set)
                    Flags_SetSwitch(play, flag);
                else
                    Flags_UnsetSwitch(play, flag);
                break;
        }
        
        if (cmd->list[i].lastCmd == false)
            break;
    }
    
    return cmd + 1;
}

Asm_VanillaHook(Cutscene_ProcessCommands);
void Cutscene_ProcessCommands(PlayState* play, CutsceneContext* csCtx, u8* cutscenePtr) {
    s16 i;
    s32 totalEntries;
    s32 cmdType;
    s32 cmdEntries;
    CsCmdBase* cmd;
    s32 cutsceneEndFrame;
    s16 j;
    
    if (cutscenePtr == NULL) {
        osLibPrintf("MemCpy(%08X, %08X, 4)", &totalEntries, cutscenePtr, 4);
        csCtx->state = CS_STATE_UNSKIPPABLE_INIT;
        
        return;
    }
    MemCpy(&totalEntries, cutscenePtr, 4);
    cutscenePtr += 4;
    MemCpy(&cutsceneEndFrame, cutscenePtr, 4);
    cutscenePtr += 4;
    
    if ((cutsceneEndFrame < csCtx->frames) && (csCtx->state != CS_STATE_UNSKIPPABLE_EXEC)) {
        csCtx->state = CS_STATE_UNSKIPPABLE_INIT;
        
        return;
    }
    
#ifdef DEV_BUILD
    if (CHECK_BTN_ALL(play->state.input[0].press.button, BTN_DRIGHT)) {
        csCtx->state = CS_STATE_UNSKIPPABLE_INIT;
        
        return;
    }
#endif
    
    for (i = 0; i < totalEntries; i++) {
        MemCpy(&cmdType, cutscenePtr, 4);
        cutscenePtr += 4;
        
        if (cmdType == -1) {
            return;
        }
        
        switch (cmdType) {
            case CS_CMD_MISC:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    func_80064824(play, csCtx, (void*)cutscenePtr);
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_LIGHTING:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    Cutscene_Command_SetLighting(play, csCtx, (void*)cutscenePtr);
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_PLAYBGM:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    Cutscene_Command_PlayBGM(play, csCtx, (void*)cutscenePtr);
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_STOPBGM:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    Cutscene_Command_StopBGM(play, csCtx, (void*)cutscenePtr);
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_FADEBGM:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    Cutscene_Command_FadeBGM(play, csCtx, (void*)cutscenePtr);
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_09:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    Cutscene_Command_09(play, csCtx, (void*)cutscenePtr);
                    cutscenePtr += 0xC;
                }
                break;
            case CS_CMD_SETTIME:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    func_80065134(play, csCtx, (void*)cutscenePtr);
                    cutscenePtr += 0xC;
                }
                break;
            case CS_CMD_SET_PLAYER_ACTION:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->linkAction = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_1:
            case 17:
            case 18:
            case 23:
            case 34:
            case 39:
            case 46:
            case 76:
            case 85:
            case 93:
            case 105:
            case 107:
            case 110:
            case 119:
            case 123:
            case 138:
            case 139:
            case 144:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[0] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_2:
            case 16:
            case 24:
            case 35:
            case 40:
            case 48:
            case 64:
            case 68:
            case 70:
            case 78:
            case 80:
            case 94:
            case 116:
            case 118:
            case 120:
            case 125:
            case 131:
            case 141:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[1] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_3:
            case 36:
            case 41:
            case 50:
            case 67:
            case 69:
            case 72:
            case 74:
            case 81:
            case 106:
            case 117:
            case 121:
            case 126:
            case 132:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[2] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_4:
            case 37:
            case 42:
            case 51:
            case 53:
            case 63:
            case 65:
            case 66:
            case 75:
            case 82:
            case 108:
            case 127:
            case 133:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[3] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_5:
            case 38:
            case 43:
            case 47:
            case 54:
            case 79:
            case 83:
            case 128:
            case 135:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[4] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_6:
            case 55:
            case 77:
            case 84:
            case 90:
            case 129:
            case 136:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[5] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_7:
            case 52:
            case 57:
            case 58:
            case 88:
            case 115:
            case 130:
            case 137:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[6] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_8:
            case 60:
            case 89:
            case 111:
            case 114:
            case 134:
            case 142:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[7] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_9:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[8] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_SET_ACTOR_ACTION_10:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if ((cmd->startFrame < csCtx->frames) && (csCtx->frames <= cmd->endFrame)) {
                        csCtx->npcActions[9] = (void*)cutscenePtr;
                    }
                    cutscenePtr += 0x30;
                }
                break;
            case CS_CMD_CAM_EYE:
                cutscenePtr += Cutscene_Command_CameraEyePoints(play, csCtx, (void*)cutscenePtr, 0);
                break;
            case CS_CMD_CAM_EYE_REL_TO_PLAYER:
                cutscenePtr += Cutscene_Command_CameraEyePoints(play, csCtx, (void*)cutscenePtr, 1);
                break;
            case CS_CMD_CAM_AT:
                cutscenePtr += Cutscene_Command_CameraLookAtPoints(play, csCtx, (void*)cutscenePtr, 0);
                break;
            case CS_CMD_CAM_AT_REL_TO_PLAYER:
                cutscenePtr += Cutscene_Command_CameraLookAtPoints(play, csCtx, (void*)cutscenePtr, 1);
                break;
            case CS_CMD_07:
                cutscenePtr += Cutscene_Command_07(play, csCtx, (void*)cutscenePtr, 0);
                break;
            case CS_CMD_08:
                cutscenePtr += Cutscene_Command_08(play, csCtx, (void*)cutscenePtr, 0);
                break;
            case CS_CMD_TERMINATOR:
                cutscenePtr += 4;
                Cutscene_Command_Terminator(play, csCtx, (void*)cutscenePtr);
                cutscenePtr += 8;
                break;
            case CS_CMD_TEXTBOX:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cmd = (CsCmdBase*)cutscenePtr;
                    if (cmd->base != 0xFFFF) {
                        Cutscene_Command_Textbox(play, csCtx, (void*)cutscenePtr);
                    }
                    cutscenePtr += 0xC;
                }
                break;
            case CS_CMD_SCENE_TRANS_FX:
                cutscenePtr += 4;
                Cutscene_Command_TransitionFX(play, csCtx, (void*)cutscenePtr);
                cutscenePtr += 8;
                break;
                
            // z64rom trigger exit
            case CS_CMD_EXITPARAM:
                cutscenePtr = CutsceneCmd_ExitParam(play, cutscenePtr);
                break;

            #if MOTION_BLUR
            // z64rom motion blur
            case 0xDE01:
                cutscenePtr += 4;
                CutsceneCmd_MotionBlur(play, csCtx, (void*)cutscenePtr);
                cutscenePtr += 8;
                break;
            #endif
                
            default:
                MemCpy(&cmdEntries, cutscenePtr, 4);
                cutscenePtr += 4;
                for (j = 0; j < cmdEntries; j++) {
                    cutscenePtr += 0x30;
                }
                break;
        }
    }
}

void Cutscene_PlaySegment(PlayState* play, void* segment) {
    Cutscene_SetSegment(play, segment);
    gSaveContext.cutsceneTrigger = 1;
}

#include "SpawnCutsceneTable"

Asm_VanillaHook(Cutscene_HandleEntranceTriggers);
void Cutscene_HandleEntranceTriggers(PlayState* play) {
    u8 spawnID;
    u8 sceneID;
    s32 playSegment = false;
    s32 isSegment = false;
    void* segment = NULL;
    
    if (gExitParam.isExit) {
        sceneID = gExitParam.exit.sceneIndex;
        spawnID = gExitParam.exit.spawnIndex;
    } else {
        u16 entrance;
        
        if (!IS_DAY) {
            if (!LINK_IS_ADULT) entrance = play->nextEntranceIndex + 1;
            else entrance = play->nextEntranceIndex + 3;
        } else {
            if (!LINK_IS_ADULT) entrance = play->nextEntranceIndex;
            else entrance = play->nextEntranceIndex + 2;
        }
        
        sceneID = gEntranceTable[entrance].sceneId;
        spawnID = gEntranceTable[entrance].spawn;
    }
    
    for (s32 i = 0; i <= ARRAY_COUNT(sSpawnCutsceneTable); i++) {
        if (playSegment) {
            if (isSegment)
                segment = sSpawnCutsceneTable[i].segment;
            
            Assert(segment != NULL);
            Cutscene_SetSegment(play, segment);
            gSaveContext.cutsceneTrigger = 2;
            gSaveContext.showTitleCard = false;
            break;
        }
        
        if (i == ARRAY_COUNT(sSpawnCutsceneTable))
            break;
        
        // Enable hardcoded segments
        if (isSegment) {
            isSegment = false;
            continue;
        }
        isSegment = sSpawnCutsceneTable[i].nextIsSegment;
        
        if (sceneID != sSpawnCutsceneTable[i].scene)
            continue;
        if (spawnID != sSpawnCutsceneTable[i].spawn)
            continue;
        if (sSpawnCutsceneTable[i].age != 2 && sSpawnCutsceneTable[i].age != gSaveContext.linkAge)
            continue;
        
        switch (sSpawnCutsceneTable[i].type) {
            case FLAG_SWITCH:
                if (Flags_GetSwitch(play, sSpawnCutsceneTable[i].flag))
                    return;
                Flags_SetSwitch(play, sSpawnCutsceneTable[i].flag);
                
                break;
            case FLAG_CHEST:
                if (Flags_GetTreasure(play, sSpawnCutsceneTable[i].flag))
                    return;
                Flags_SetTreasure(play, sSpawnCutsceneTable[i].flag);
                
                break;
            case FLAG_EVENTCHKINF:
                if (GET_EVENTCHKINF(sSpawnCutsceneTable[i].flag))
                    return;
                SET_EVENTCHKINF(sSpawnCutsceneTable[i].flag);
                
                break;
            case FLAG_COLLECTIBLE:
                if (Flags_GetCollectible(play, sSpawnCutsceneTable[i].flag))
                    return;
                Flags_SetCollectible(play, sSpawnCutsceneTable[i].flag);
                
                break;
        }
        
        playSegment = true;
        if (!isSegment)
            segment = Segment_Scene_GetCutscene(play->sceneSegment, sSpawnCutsceneTable[i].header);
    }
}

Asm_VanillaHook(Cutscene_Command_Terminator);
void Cutscene_Command_Terminator(PlayState* play, CutsceneContext* csCtx, CsCmdBase* cmd) {
    Player* player = GET_PLAYER(play);
    s32 temp = 0;

    if ((gSaveContext.gameMode != GAMEMODE_NORMAL) && (gSaveContext.gameMode != GAMEMODE_END_CREDITS) &&
        (play->sceneId != SCENE_SPOT00) && (csCtx->frames > 20) &&
        (CHECK_BTN_ALL(play->state.input[0].press.button, BTN_A) ||
         CHECK_BTN_ALL(play->state.input[0].press.button, BTN_B) ||
         CHECK_BTN_ALL(play->state.input[0].press.button, BTN_START)) &&
        (gSaveContext.fileNum != 0xFEDC) && (play->transitionTrigger == TRANS_TRIGGER_OFF)) {
        Audio_PlaySfxGeneral(NA_SE_SY_PIECE_OF_HEART, &gSfxDefaultPos, 4, &gSfxDefaultFreqAndVolScale,
                             &gSfxDefaultFreqAndVolScale, &gSfxDefaultReverb);
        temp = 1;
    }

#ifdef DEV_BUILD
    if ((csCtx->frames == cmd->startFrame) || (temp != 0) ||
        ((csCtx->frames > 20) &&
        CHECK_BTN_ALL(play->state.input[0].press.button, BTN_START) &&
         (gSaveContext.fileNum != 0xFEDC))) {
#else
    if (temp) {
#endif

        csCtx->state = CS_STATE_UNSKIPPABLE_EXEC;
        Audio_SetCutsceneFlag(0);
        gSaveContext.cutsceneTransitionControl = 1;

        osSyncPrintf("\n分岐先指定！！=[%d]番", cmd->base); // "Future fork designation=No. [%d]"

        if ((gSaveContext.gameMode != GAMEMODE_NORMAL) && (csCtx->frames != cmd->startFrame)) {
            gSaveContext.unk_13E7 = 1;
        }

        gSaveContext.cutsceneIndex = 0;

        switch (cmd->base) {
            case 1:
                play->nextEntranceIndex = ENTR_HIRAL_DEMO_0;
                gSaveContext.cutsceneIndex = 0xFFF1;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 2:
                play->nextEntranceIndex = ENTR_HIRAL_DEMO_0;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FILL_WHITE;
                break;
            case 3:
                play->nextEntranceIndex = ENTR_SPOT09_0;
                gSaveContext.cutsceneIndex = 0xFFF1;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FILL_WHITE;
                break;
            case 4:
                play->nextEntranceIndex = ENTR_SPOT16_0;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FILL_WHITE;
                break;
            case 5:
                play->nextEntranceIndex = ENTR_SPOT04_0;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FILL_WHITE;
                break;
            case 6:
                play->nextEntranceIndex = ENTR_HIRAL_DEMO_0;
                gSaveContext.cutsceneIndex = 0xFFF2;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FILL_WHITE;
                break;
            case 7:
                play->nextEntranceIndex = ENTR_SPOT04_0;
                gSaveContext.cutsceneIndex = 0xFFF2;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_INSTANT;
                break;
            case 8:
                gSaveContext.fw.set = 0;
                gSaveContext.respawn[RESPAWN_MODE_TOP].data = 0;
                if (!GET_EVENTCHKINF(EVENTCHKINF_45)) {
                    SET_EVENTCHKINF(EVENTCHKINF_45);
                    play->nextEntranceIndex = ENTR_HIRAL_DEMO_0;
                    play->transitionTrigger = TRANS_TRIGGER_START;
                    gSaveContext.cutsceneIndex = 0xFFF3;
                    play->transitionType = TRANS_TYPE_INSTANT;
                } else {
                    if (!IS_CUTSCENE_LAYER) {
                        if (!LINK_IS_ADULT) {
                            play->linkAgeOnLoad = LINK_AGE_ADULT;
                        } else {
                            play->linkAgeOnLoad = LINK_AGE_CHILD;
                        }
                    }
                    play->nextEntranceIndex = ENTR_TOKINOMA_2;
                    play->transitionTrigger = TRANS_TRIGGER_START;
                    play->transitionType = TRANS_TYPE_FADE_WHITE;
                    gSaveContext.nextTransitionType = TRANS_TYPE_FADE_WHITE;
                }
                break;
            case 9:
                play->nextEntranceIndex = ENTR_SPOT09_0;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FILL_BROWN;
                break;
            case 10:
                play->nextEntranceIndex = ENTR_LINK_HOME_0;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 11:
                play->nextEntranceIndex = ENTR_SPOT04_0;
                gSaveContext.cutsceneIndex = 0xFFF3;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 12:
                play->nextEntranceIndex = ENTR_SPOT16_5;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 13:
                play->nextEntranceIndex = ENTR_SPOT08_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 14:
                play->nextEntranceIndex = ENTR_SPOT04_11;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 15:
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF4;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 16:
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF5;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 17:
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF6;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 18:
                SET_EVENTCHKINF(EVENTCHKINF_4F);
                play->nextEntranceIndex = ENTR_TOKINOMA_4;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 19:
                play->nextEntranceIndex = ENTR_SPOT16_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                gSaveContext.cutsceneIndex = 0x8000;
                break;
            case 21:
                play->nextEntranceIndex = ENTR_SPOT06_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 22:
                Item_Give(play, ITEM_SONG_REQUIEM);
                play->nextEntranceIndex = ENTR_SPOT11_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 23:
                play->nextEntranceIndex = ENTR_HIRAL_DEMO_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF8;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 24:
                play->nextEntranceIndex = ENTR_BDAN_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 25:
                play->linkAgeOnLoad = LINK_AGE_ADULT;
                play->nextEntranceIndex = ENTR_KENJYANOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 26:
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF4;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 27:
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF5;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 28:
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF6;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 29:
                play->nextEntranceIndex = ENTR_KENJYANOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.chamberCutsceneNum = 0;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 30:
                play->nextEntranceIndex = ENTR_KENJYANOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                Item_Give(play, ITEM_MEDALLION_FIRE);
                gSaveContext.chamberCutsceneNum = 1;
                break;
            case 31:
                play->nextEntranceIndex = ENTR_KENJYANOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                gSaveContext.chamberCutsceneNum = 2;
                break;
            case 32:
                play->linkAgeOnLoad = LINK_AGE_CHILD;
                play->nextEntranceIndex = ENTR_SPOT00_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF2;
                play->transitionType = TRANS_TYPE_INSTANT;
                break;
            case 33:
                play->nextEntranceIndex = ENTR_SPOT00_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 34:
                play->nextEntranceIndex = ENTR_HIRAL_DEMO_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF3;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 35:
                play->nextEntranceIndex = ENTR_SPOT00_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                break;
            case 38:
                play->nextEntranceIndex = ENTR_HIRAL_DEMO_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF4;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                break;
            case 39:
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF9;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                break;
            case 40:
                play->linkAgeOnLoad = LINK_AGE_ADULT;
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFFA;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                break;
            case 41:
                play->nextEntranceIndex = ENTR_SPOT06_5;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 42:
                play->nextEntranceIndex = ENTR_SPOT01_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF2;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                break;
            case 43:
                play->nextEntranceIndex = ENTR_HAKASITARELAY_2;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                break;
            case 44:
                play->nextEntranceIndex = ENTR_TOKINOMA_3;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE_INSTANT;
                break;
            case 46:
                SET_EVENTCHKINF(EVENTCHKINF_4F);
                play->nextEntranceIndex = ENTR_TOKINOMA_4;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                break;
            case 47:
                Item_Give(play, ITEM_SONG_NOCTURNE);
                SET_EVENTCHKINF(EVENTCHKINF_54);
                play->nextEntranceIndex = ENTR_SPOT01_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF1;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                break;
            case 48:
                play->nextEntranceIndex = ENTR_SPOT11_4;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_SANDSTORM_END;
                gSaveContext.nextTransitionType = TRANS_TYPE_SANDSTORM_END;
                break;
            case 49:
                play->nextEntranceIndex = ENTR_TOKINOMA_5;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK_FAST;
                break;
            case 50:
                play->nextEntranceIndex = ENTR_SPOT01_13;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE_INSTANT;
                break;
            case 51:
                play->nextEntranceIndex = ENTR_SPOT00_0;
                gSaveContext.cutsceneIndex = 0xFFF8;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_CIRCLE(TCA_NORMAL, TCC_WHITE, TCS_SLOW);
                break;
            case 52:
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                gSaveContext.cutsceneIndex = 0xFFF7;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_INSTANT;
                break;
            case 53:
                play->nextEntranceIndex = ENTR_SPOT00_16;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 54:
                gSaveContext.gameMode = GAMEMODE_END_CREDITS;
                Audio_SetSfxBanksMute(0x6F);
                play->linkAgeOnLoad = LINK_AGE_CHILD;
                play->nextEntranceIndex = ENTR_SPOT09_0;
                gSaveContext.cutsceneIndex = 0xFFF2;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 55:
                play->nextEntranceIndex = ENTR_SPOT12_0;
                gSaveContext.cutsceneIndex = 0xFFF1;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 56:
                play->nextEntranceIndex = ENTR_SPOT01_0;
                gSaveContext.cutsceneIndex = 0xFFF4;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 57:
                play->nextEntranceIndex = ENTR_SPOT16_0;
                gSaveContext.cutsceneIndex = 0xFFF3;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 58:
                play->nextEntranceIndex = ENTR_SPOT18_0;
                gSaveContext.cutsceneIndex = 0xFFF1;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 59:
                play->nextEntranceIndex = ENTR_SPOT06_0;
                gSaveContext.cutsceneIndex = 0xFFF1;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 60:
                play->nextEntranceIndex = ENTR_SPOT08_0;
                gSaveContext.cutsceneIndex = 0xFFF2;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 61:
                play->nextEntranceIndex = ENTR_SPOT07_0;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 62:
                play->linkAgeOnLoad = LINK_AGE_ADULT;
                play->nextEntranceIndex = ENTR_SPOT04_0;
                gSaveContext.cutsceneIndex = 0xFFF6;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 63:
                play->nextEntranceIndex = ENTR_SPOT04_0;
                gSaveContext.cutsceneIndex = 0xFFF7;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 64:
                play->nextEntranceIndex = ENTR_SPOT00_0;
                gSaveContext.cutsceneIndex = 0xFFF5;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 65:
                play->linkAgeOnLoad = LINK_AGE_CHILD;
                play->nextEntranceIndex = ENTR_SPOT20_0;
                gSaveContext.cutsceneIndex = 0xFFF2;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 66:
                play->nextEntranceIndex = ENTR_SPOT01_14;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 67:
                play->nextEntranceIndex = ENTR_SPOT00_9;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 68:
                play->nextEntranceIndex = ENTR_HIRAL_DEMO_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF5;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 69:
                play->nextEntranceIndex = ENTR_SPOT04_12;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 70:
                play->nextEntranceIndex = ENTR_SPOT16_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF4;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 71:
                gSaveContext.equips.equipment |= EQUIP_VALUE_TUNIC_KOKIRI << (EQUIP_TYPE_TUNIC * 4);
                Player_SetEquipmentData(play, player);
                gSaveContext.equips.equipment |= EQUIP_VALUE_BOOTS_KOKIRI << (EQUIP_TYPE_BOOTS * 4);
                Player_SetEquipmentData(play, player);
                play->linkAgeOnLoad = LINK_AGE_CHILD;
                play->nextEntranceIndex = ENTR_TOKINOMA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF1;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 72:
                play->nextEntranceIndex = ENTR_NAKANIWA_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF0;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 73:
                play->linkAgeOnLoad = LINK_AGE_CHILD;
                play->nextEntranceIndex = ENTR_SPOT20_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF2;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 74:
                play->nextEntranceIndex = ENTR_SPOT20_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF3;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 75:
                play->linkAgeOnLoad = LINK_AGE_CHILD;
                play->nextEntranceIndex = ENTR_SPOT20_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF4;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 76:
                play->linkAgeOnLoad = LINK_AGE_ADULT;
                play->nextEntranceIndex = ENTR_SPOT20_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF5;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 77:
                play->linkAgeOnLoad = LINK_AGE_CHILD;
                play->nextEntranceIndex = ENTR_SPOT20_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF6;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 78:
                play->nextEntranceIndex = ENTR_SPOT20_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF7;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 79:
            case 80:
            case 81:
            case 82:
            case 83:
            case 84:
            case 85:
            case 86:
            case 87:
            case 88:
            case 89:
            case 90:
            case 91:
            case 92:
            case 93:
                play->nextEntranceIndex = ENTR_SPOT20_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 94:
                play->nextEntranceIndex = ENTR_SPOT20_1;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 95:
                if (GET_EVENTCHKINF(EVENTCHKINF_48) && GET_EVENTCHKINF(EVENTCHKINF_49) &&
                    GET_EVENTCHKINF(EVENTCHKINF_4A)) {
                    play->nextEntranceIndex = ENTR_TOKINOMA_0;
                    play->transitionTrigger = TRANS_TRIGGER_START;
                    gSaveContext.cutsceneIndex = 0xFFF3;
                    play->transitionType = TRANS_TYPE_FADE_BLACK;
                } else {
                    switch (gSaveContext.sceneLayer) {
                        case 8:
                            play->nextEntranceIndex = ENTR_SPOT05_0;
                            play->transitionTrigger = TRANS_TRIGGER_START;
                            play->transitionType = TRANS_TYPE_FADE_BLACK;
                            break;
                        case 9:
                            play->nextEntranceIndex = ENTR_SPOT17_0;
                            play->transitionTrigger = TRANS_TRIGGER_START;
                            play->transitionType = TRANS_TYPE_FADE_BLACK;
                            break;
                        case 10:
                            play->nextEntranceIndex = ENTR_SPOT06_0;
                            play->transitionTrigger = TRANS_TRIGGER_START;
                            gSaveContext.cutsceneIndex = 0xFFF0;
                            play->transitionType = TRANS_TYPE_FADE_WHITE;
                            break;
                    }
                }
                break;
            case 96:
                if (CHECK_QUEST_ITEM(QUEST_MEDALLION_SHADOW)) {
                    play->nextEntranceIndex = ENTR_KENJYANOMA_0;
                    play->transitionTrigger = TRANS_TRIGGER_START;
                    gSaveContext.cutsceneIndex = 0xFFF1;
                    play->transitionType = TRANS_TYPE_FADE_WHITE_FAST;
                } else {
                    SET_EVENTCHKINF(EVENTCHKINF_C8);
                    play->nextEntranceIndex = ENTR_SPOT11_8;
                    play->transitionTrigger = TRANS_TRIGGER_START;
                    play->transitionType = TRANS_TYPE_FADE_WHITE;
                    gSaveContext.nextTransitionType = TRANS_TYPE_FADE_WHITE;
                }
                break;
            case 97:
                if (CHECK_QUEST_ITEM(QUEST_MEDALLION_SPIRIT)) {
                    play->nextEntranceIndex = ENTR_KENJYANOMA_0;
                    play->transitionTrigger = TRANS_TRIGGER_START;
                    gSaveContext.cutsceneIndex = 0xFFF1;
                    play->transitionType = TRANS_TYPE_FADE_WHITE_FAST;
                } else {
                    play->nextEntranceIndex = ENTR_SPOT02_8;
                    play->transitionTrigger = TRANS_TRIGGER_START;
                    play->transitionType = TRANS_TYPE_FADE_WHITE;
                    gSaveContext.nextTransitionType = TRANS_TYPE_FADE_WHITE;
                }
                break;
            case 98:
                play->nextEntranceIndex = ENTR_SPOT17_5;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 99:
                play->nextEntranceIndex = ENTR_SPOT05_3;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 100:
                play->nextEntranceIndex = ENTR_SPOT04_0;
                gSaveContext.cutsceneIndex = 0xFFF8;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 101:
                play->nextEntranceIndex = ENTR_SPOT11_6;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_SANDSTORM_END;
                break;
            case 102:
                play->nextEntranceIndex = ENTR_TOKINOMA_6;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 103:
                play->nextEntranceIndex = ENTR_SPOT00_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF3;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 104:
                switch (sTitleCsState) {
                    case 0:
                        play->nextEntranceIndex = ENTR_JYASINBOSS_0;
                        play->transitionTrigger = TRANS_TRIGGER_START;
                        gSaveContext.cutsceneIndex = 0xFFF2;
                        play->transitionType = TRANS_TYPE_FADE_BLACK;
                        sTitleCsState++;
                        break;
                    case 1:
                        play->nextEntranceIndex = ENTR_SPOT17_0;
                        play->transitionTrigger = TRANS_TRIGGER_START;
                        gSaveContext.cutsceneIndex = 0xFFF1;
                        play->transitionType = TRANS_TYPE_FADE_BLACK;
                        sTitleCsState++;
                        break;
                    case 2:
                        play->nextEntranceIndex = ENTR_HIRAL_DEMO_0;
                        play->transitionTrigger = TRANS_TRIGGER_START;
                        gSaveContext.cutsceneIndex = 0xFFF6;
                        play->transitionType = TRANS_TYPE_FADE_BLACK;
                        sTitleCsState = 0;
                        break;
                }
                break;
            case 105:
                play->nextEntranceIndex = ENTR_SPOT02_0;
                play->transitionTrigger = TRANS_TRIGGER_START;
                gSaveContext.cutsceneIndex = 0xFFF1;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 106:
                play->nextEntranceIndex = ENTR_HAKAANA_OUKE_1;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 107:
                play->nextEntranceIndex = ENTR_GANONTIKA_2;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 108:
                play->nextEntranceIndex = ENTR_GANONTIKA_3;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 109:
                play->nextEntranceIndex = ENTR_GANONTIKA_4;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 110:
                play->nextEntranceIndex = ENTR_GANONTIKA_5;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 111:
                play->nextEntranceIndex = ENTR_GANONTIKA_6;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 112:
                play->nextEntranceIndex = ENTR_GANONTIKA_7;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 113:
                if (Flags_GetEventChkInf(EVENTCHKINF_BB) && Flags_GetEventChkInf(EVENTCHKINF_BC) &&
                    Flags_GetEventChkInf(EVENTCHKINF_BD) && Flags_GetEventChkInf(EVENTCHKINF_BE) &&
                    Flags_GetEventChkInf(EVENTCHKINF_BF) && Flags_GetEventChkInf(EVENTCHKINF_AD)) {
                    play->csCtx.segment = SEGMENTED_TO_VIRTUAL(gTowerBarrierCs);
                    play->csCtx.frames = 0;
                    gSaveContext.cutsceneTrigger = 1;
                    gSaveContext.cutsceneIndex = 0xFFFF;
                    csCtx->state = CS_STATE_UNSKIPPABLE_INIT;
                } else {
                    gSaveContext.cutsceneIndex = 0xFFFF;
                    csCtx->state = CS_STATE_UNSKIPPABLE_INIT;
                }
                break;
            case 114:
                play->nextEntranceIndex = ENTR_SPOT00_3;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 115:
                play->nextEntranceIndex = ENTR_SPOT00_17;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_BLACK;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 116:
                if (GET_EVENTCHKINF(EVENTCHKINF_C8)) {
                    play->nextEntranceIndex = ENTR_SPOT02_8;
                    play->transitionTrigger = TRANS_TRIGGER_START;
                    play->transitionType = TRANS_TYPE_FADE_WHITE;
                } else {
                    play->nextEntranceIndex = ENTR_SPOT11_8;
                    play->transitionTrigger = TRANS_TRIGGER_START;
                    play->transitionType = TRANS_TYPE_FADE_WHITE;
                }
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 117:
                gSaveContext.gameMode = GAMEMODE_END_CREDITS;
                Audio_SetSfxBanksMute(0x6F);
                play->linkAgeOnLoad = LINK_AGE_ADULT;
                play->nextEntranceIndex = ENTR_SPOT00_0;
                gSaveContext.cutsceneIndex = 0xFFF7;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
            case 118:
                gSaveContext.respawn[RESPAWN_MODE_DOWN].entranceIndex = ENTR_GANON_DEMO_0;
                Play_TriggerVoidOut(play);
                gSaveContext.respawnFlag = -2;
                gSaveContext.nextTransitionType = TRANS_TYPE_FADE_BLACK;
                break;
            case 119:
                gSaveContext.dayTime = CLOCK_TIME(12, 0);
                gSaveContext.skyboxTime = CLOCK_TIME(12, 0);
                play->nextEntranceIndex = ENTR_NAKANIWA_1;
                play->transitionTrigger = TRANS_TRIGGER_START;
                play->transitionType = TRANS_TYPE_FADE_WHITE;
                break;
        }
    }
}
