#include "global.h"
#include "vt.h"
#include "assets/objects/gameplay_keep/gameplay_keep.h"
#include "assets/textures/parameter_static/parameter_static.h"
#include <uLib.h>
#include "z_map_data_minimap.h"

MapData* gMapData;

s16 sPlayerInitialPosX = 0;
s16 sPlayerInitialPosZ = 0;
s16 sPlayerInitialDirection = 0;
s16 sEntranceIconMapIndex = 0;


Asm_VanillaHook(Map_Init);
void Map_Init(PlayState* play) {
    s32 mapIndex = gSaveContext.mapIndex;
    InterfaceContext* interfaceCtx = &play->interfaceCtx;

    gMapData = &gMapDataTableSO;

    interfaceCtx->unk_258 = -1;
    interfaceCtx->unk_25A = -1;

    interfaceCtx->mapSegment = GameState_Alloc(&play->state, 0x1000, "../z_map_exp.c", 457);
    // "ＭＡＰ texture initialization scene_data_ID=%d mapSegment=%x"
    osSyncPrintf("\n\n\nＭＡＰ テクスチャ初期化   scene_data_ID=%d\nmapSegment=%x\n\n", play->sceneId,
                 interfaceCtx->mapSegment, play);
    ASSERT(interfaceCtx->mapSegment != NULL, "parameter->mapSegment != NULL", "../z_map_exp.c", 459);

    switch (play->sceneId) {
        case SCENE_SPOT00:
        case SCENE_SPOT01:
        case SCENE_SPOT02:
        case SCENE_SPOT03:
        case SCENE_SPOT04:
        case SCENE_SPOT05:
        case SCENE_SPOT06:
        case SCENE_SPOT07:
        case SCENE_SPOT08:
        case SCENE_SPOT09:
        case SCENE_SPOT10:
        case SCENE_SPOT11:
        case SCENE_SPOT12:
        case SCENE_SPOT13:
        case SCENE_SPOT15:
        case SCENE_SPOT16:
        case SCENE_SPOT17:
        case SCENE_SPOT18:
        case SCENE_SPOT20:
        case SCENE_GANON_TOU:
            mapIndex = play->sceneId - SCENE_SPOT00;
            R_MAP_INDEX = gSaveContext.mapIndex = mapIndex;
            R_COMPASS_SCALE_X = gMapData->owCompassInfo[mapIndex][0];
            R_COMPASS_SCALE_Y = gMapData->owCompassInfo[mapIndex][1];
            R_COMPASS_OFFSET_X = gMapData->owCompassInfo[mapIndex][2];
            R_COMPASS_OFFSET_Y = gMapData->owCompassInfo[mapIndex][3];
            Map_InitData(play, mapIndex);
            R_OW_MINIMAP_X = gMapData->owMinimapPosX[mapIndex];
            R_OW_MINIMAP_Y = gMapData->owMinimapPosY[mapIndex];
            break;
        case SCENE_YDAN:
        case SCENE_DDAN:
        case SCENE_BDAN:
        case SCENE_BMORI1:
        case SCENE_HIDAN:
        case SCENE_MIZUSIN:
        case SCENE_JYASINZOU:
        case SCENE_HAKADAN:
        case SCENE_HAKADANCH:
        case SCENE_ICE_DOUKUTO:
        case SCENE_GANON:
        case SCENE_MEN:
        case SCENE_GERUDOWAY:
        case SCENE_GANONTIKA:
        case SCENE_GANON_SONOGO:
        case SCENE_GANONTIKA_SONOGO:
        case SCENE_TAKARAYA:
        case SCENE_YDAN_BOSS:
        case SCENE_DDAN_BOSS:
        case SCENE_BDAN_BOSS:
        case SCENE_MORIBOSSROOM:
        case SCENE_FIRE_BS:
        case SCENE_MIZUSIN_BS:
        case SCENE_JYASINBOSS:
        case SCENE_HAKADAN_BS:
            mapIndex = (play->sceneId >= SCENE_YDAN_BOSS) ? play->sceneId - SCENE_YDAN_BOSS : play->sceneId;
            R_MAP_INDEX = gSaveContext.mapIndex = mapIndex;
            if ((play->sceneId <= SCENE_ICE_DOUKUTO) || (play->sceneId >= SCENE_YDAN_BOSS)) {
                R_COMPASS_SCALE_X = gMapData->dgnCompassInfo[mapIndex][0];
                R_COMPASS_SCALE_Y = gMapData->dgnCompassInfo[mapIndex][1];
                R_COMPASS_OFFSET_X = gMapData->dgnCompassInfo[mapIndex][2];
                R_COMPASS_OFFSET_Y = gMapData->dgnCompassInfo[mapIndex][3];
                R_MAP_TEX_INDEX = R_MAP_TEX_INDEX_BASE = gMapData->dgnTexIndexBase[mapIndex];
                Map_InitRoomData(play, play->roomCtx.curRoom.num);
                MapMark_Init(play);
            }
            break;
    }
}

Asm_VanillaHook(Map_SetPaletteData);
void Map_SetPaletteData(PlayState* play, s16 room) {
    s32 mapIndex = gSaveContext.mapIndex;
    InterfaceContext* interfaceCtx = &play->interfaceCtx;
    s16 paletteIndex = gMapData->roomPalette[mapIndex][room];

    if (interfaceCtx->mapRoomNum == room) {
        interfaceCtx->mapPaletteIndex = paletteIndex;
    }

    osSyncPrintf(VT_FGCOL(YELLOW));
    // "PALETE Set"
    osSyncPrintf("ＰＡＬＥＴＥセット 【 i=%x : room=%x 】Room_Inf[%d][4]=%x  ( map_palete_no = %d )\n", paletteIndex,
                 room, mapIndex, gSaveContext.sceneFlags[mapIndex].rooms, interfaceCtx->mapPaletteIndex);
    osSyncPrintf(VT_RST);

    interfaceCtx->mapPalette[paletteIndex * 2] = 2;
    interfaceCtx->mapPalette[paletteIndex * 2 + 1] = 0xBF;
}

Asm_VanillaHook(Map_SetFloorPalettesData);
void Map_SetFloorPalettesData(PlayState* play, s16 floor) {
    s32 mapIndex = gSaveContext.mapIndex;
    InterfaceContext* interfaceCtx = &play->interfaceCtx;
    s16 room;
    s16 i;

    for (i = 0; i < 16; i++) {
        interfaceCtx->mapPalette[i] = 0;
        interfaceCtx->mapPalette[i + 16] = 0;
    }

    if (CHECK_DUNGEON_ITEM(DUNGEON_MAP, mapIndex)) {
        interfaceCtx->mapPalette[30] = 0;
        interfaceCtx->mapPalette[31] = 1;
    }

    switch (play->sceneId) {
        case SCENE_YDAN:
        case SCENE_DDAN:
        case SCENE_BDAN:
        case SCENE_BMORI1:
        case SCENE_HIDAN:
        case SCENE_MIZUSIN:
        case SCENE_JYASINZOU:
        case SCENE_HAKADAN:
        case SCENE_HAKADANCH:
        case SCENE_ICE_DOUKUTO:
        case SCENE_YDAN_BOSS:
        case SCENE_DDAN_BOSS:
        case SCENE_BDAN_BOSS:
        case SCENE_MORIBOSSROOM:
        case SCENE_FIRE_BS:
        case SCENE_MIZUSIN_BS:
        case SCENE_JYASINBOSS:
        case SCENE_HAKADAN_BS:
            for (i = 0; i < gMapData->maxPaletteCount[mapIndex]; i++) {
                room = gMapData->paletteRoom[mapIndex][floor][i];
                if ((room != 0xFF) && (gSaveContext.sceneFlags[mapIndex].rooms & gBitFlags[room])) {
                    Map_SetPaletteData(play, room);
                }
            }
            break;
    }
}


Asm_VanillaHook(Map_Destroy);
void Map_Destroy(PlayState* play) {
    MapMark_ClearPointers(play);
    gMapData = NULL;
}

Asm_VanillaHook(Map_GetFloorTextIndexOffset);
s16 Map_GetFloorTextIndexOffset(s32 mapIndex, s32 floor) {
    return gMapData->floorTexIndexOffset[mapIndex][floor];
}

Asm_VanillaHook(Map_Update);
void Map_Update(PlayState* play) {
    static s16 sLastRoomNum = 99;
    Player* player = GET_PLAYER(play);
    s32 mapIndex = gSaveContext.mapIndex;
    InterfaceContext* interfaceCtx = &play->interfaceCtx;
    s16 floor;
    s16 i;

    if ((play->pauseCtx.state == 0) && (play->pauseCtx.debugState == 0)) {
        switch (play->sceneId) {
            case SCENE_YDAN:
            case SCENE_DDAN:
            case SCENE_BDAN:
            case SCENE_BMORI1:
            case SCENE_HIDAN:
            case SCENE_MIZUSIN:
            case SCENE_JYASINZOU:
            case SCENE_HAKADAN:
            case SCENE_HAKADANCH:
            case SCENE_ICE_DOUKUTO:
                interfaceCtx->mapPalette[30] = 0;
                if (CHECK_DUNGEON_ITEM(DUNGEON_MAP, mapIndex)) {
                    interfaceCtx->mapPalette[31] = 1;
                } else {
                    interfaceCtx->mapPalette[31] = 0;
                }

                for (floor = 0; floor < 8; floor++) {
                    if (player->actor.world.pos.y > gMapData->floorCoordY[mapIndex][floor]) {
                        break;
                    }
                }

                gSaveContext.sceneFlags[mapIndex].floors |= gBitFlags[floor];
                VREG(30) = floor;
                if (R_MAP_TEX_INDEX != (R_MAP_TEX_INDEX_BASE + Map_GetFloorTextIndexOffset(mapIndex, floor))) {
                    R_MAP_TEX_INDEX = R_MAP_TEX_INDEX_BASE + Map_GetFloorTextIndexOffset(mapIndex, floor);
                }
                if (1) {} // Appears to be necessary to match

                if (interfaceCtx->mapRoomNum != sLastRoomNum) {
                    // "Current floor = %d Current room = %x Number of rooms = %d"
                    osSyncPrintf("現在階＝%d  現在部屋＝%x  部屋数＝%d\n", floor, interfaceCtx->mapRoomNum,
                                 gMapData->switchEntryCount[mapIndex]);
                    sLastRoomNum = interfaceCtx->mapRoomNum;
                }

                for (i = 0; i < gMapData->switchEntryCount[mapIndex]; i++) {
                    if ((interfaceCtx->mapRoomNum == gMapData->switchFromRoom[mapIndex][i]) &&
                        (floor == gMapData->switchFromFloor[mapIndex][i])) {
                        interfaceCtx->mapRoomNum = gMapData->switchToRoom[mapIndex][i];
                        osSyncPrintf(VT_FGCOL(YELLOW));
                        // "Layer switching = %x"
                        osSyncPrintf("階層切替＝%x\n", interfaceCtx->mapRoomNum);
                        osSyncPrintf(VT_RST);
                        Map_InitData(play, interfaceCtx->mapRoomNum);
                        gSaveContext.sunsSongState = SUNSSONG_INACTIVE;
                        Map_SavePlayerInitialInfo(play);
                    }
                }

                VREG(10) = interfaceCtx->mapRoomNum;
                break;
            case SCENE_YDAN_BOSS:
            case SCENE_DDAN_BOSS:
            case SCENE_BDAN_BOSS:
            case SCENE_MORIBOSSROOM:
            case SCENE_FIRE_BS:
            case SCENE_MIZUSIN_BS:
            case SCENE_JYASINBOSS:
            case SCENE_HAKADAN_BS:
                VREG(30) = gMapData->bossFloor[play->sceneId - SCENE_YDAN_BOSS];
                R_MAP_TEX_INDEX =
                    R_MAP_TEX_INDEX_BASE + gMapData->floorTexIndexOffset[play->sceneId - SCENE_YDAN_BOSS][VREG(30)];
                break;
        }
    }
}
