#include "uLib.h"
#include "SceneRender.h"
//Version: 1.3
/*This version value is used by SharpOcarina to determine if it needs to update the SceneRender.c of an old project
to use newly added features. Put a high value like 99 to stop SharpOcarina from ever asking to update it again.
*/

void func_8009BEEC(PlayState* play);
asm ("func_8009BEEC = 0x8009BEEC");

/*add -DMATCH_MM_UV_SCROLL to your build flags (or add #define MATCH_MM_UV_SCROLL to the top of this file)
to use MM's UV scrolling direction (this exists because my original implementation hadit backwards; 
it is left off by default so older projects and tools will not be broken by it)
*/
#ifdef MATCH_MM_TEXSCROLL
#    define VSCROLL -1
#else
#    define VSCROLL 1
#endif

static SceneAnimContext gSceneAnimCtx = {
    .magic = 0xDEADBEEF,
};

static void SegmentData(s32 seg, void* data) {
    gSPSegment(POLY_OPA_DISP++, seg, data);
    gSPSegment(POLY_XLU_DISP++, seg, data);
}

static s32 SceneAnim_Flag(PlayState* play, Flag* f) {
    s32 r = 0;
    
    switch (f->type) {
        case FLAG_TYPE_ROOMCLEAR:
            r = Flags_GetClear(play, f->flag);
            break;
            
        case FLAG_TYPE_TREASURE:
            r = Flags_GetTreasure(play, f->flag);
            break;
            
        case FLAG_TYPE_USCENE:
            r = Flags_GetUnknown(play, f->flag);
            break;
            
        case FLAG_TYPE_TEMP:
            r = Flags_GetTempClear(play, f->flag);
            break;
            
        case FLAG_TYPE_SCENECOLLECT:
            r = Flags_GetCollectible(play, f->flag);
            break;
            
        case FLAG_TYPE_SWITCH:
            r = Flags_GetSwitch(play, f->flag);
            break;
            
        case FLAG_TYPE_EVENTCHKINF:
            r = Flags_GetEventChkInf(f->flag);
            break;
            
        case FLAG_TYPE_INFTABLE:
            r = Flags_GetInfTable(f->flag);
            break;
            
        case FLAG_TYPE_IS_NIGHT:
            r = (gSaveContext.nightFlag == 1);
            break;
            
        case FLAG_TYPE_SAVE:
            r = !!((*(u32*)((u8*)(&gSaveContext) + f->flag)) & f->and);
            break;
            
        case FLAG_TYPE_GLOBAL:
            r = !!(
                (*(u32*)((u8*)(play) + f->flag)) & f->and
            );
            break;
            
        case FLAG_TYPE_RAM:
            r = !!((*(u32*)(f->flag)) & f->and);
            break;
    }
    
    return f->eq == r;
}

static ColorKey* SceneAnim_GetColorKey(ColorList* list, u32* frame) {
    ColorKey* key;
    u32 along = 0;
    
    if (!list->dur)
        return list->key;
    
    /* loop */
    *frame %= list->dur;
    
    /* locate appropriate keyframe */
    for (key = list->key; key->next; ++key) {
        u32 next = along + key->next;
        
        /* keyframe range encompasses frame */
        if (*frame >= along && *frame <= next)
            return key;
        
        /* advance to next frame range */
        along = next;
    }
    
    /* found none; return first keyframe */
    return list->key;
}

/* get next key in list */
static ColorKey* SceneAnim_GetNextColorKey(ColorList* list, ColorKey* key) {
    /* next == 0 indicates end of list */
    if (!key->next)
        return key;
    
    /* advance to next key */
    key += 1;
    
    /* key->next == 0 indicates end of list, so loop to first key */
    if (!key->next)
        return list->key;
    
    /* return next key */
    return key;
}

static s32 SceneAnim_Ease_s32(s32 from, s32 to, float factor) {
    return LERP(from, to, factor);
}

static s32 SceneAnim_Ease_s8(s32 from, s32 to, float factor) {
    return SceneAnim_Ease_s32(from, to, factor) & 0xFF;
}

static u32 SceneAnim_Ease_RGBA(u32 from, u32 to, float factor) {
    Color_RGBA8 rgba;
    Color_RGBA8* f = (void*)&from;
    Color_RGBA8* t = (void*)&to;
    
    rgba.r = LERP(f->r, t->r, factor);
    rgba.g = LERP(f->g, t->g, factor);
    rgba.b = LERP(f->b, t->b, factor);
    rgba.a = LERP(f->a, t->a, factor);
    
    return *(u32*)&rgba;
}

static void SceneAnim_PutColorKey(u8 which, Gfx** disp, ColorKey* key) {
    if (which & COLORKEY_PRIM) {
        u8* rgba = (u8*)&key->prim;
        
        gDPSetPrimColor((*disp)++, key->mlevel, key->lfrac, rgba[0], rgba[1], rgba[2], rgba[3]);
    }
    
    if (which & COLORKEY_ENV) {
        u8* rgba = (u8*)&key->env;
        
        gDPSetEnvColor((*disp)++, rgba[0], rgba[1], rgba[2], rgba[3]);
    }
}

static void SceneAnim_BlendColorKey(enum8(ColorKeyTypes) which, float factor, ColorKey* from, ColorKey* to, ColorKey* result) {
    if (from == to) {
        *result = *from;
        
        return;
    }
    
    /* primcolor */
    if (which & COLORKEY_PRIM) {
        result->prim = SceneAnim_Ease_RGBA(from->prim, to->prim, factor);
        
        if (which & COLORKEY_LODFRAC)
            result->lfrac = SceneAnim_Ease_s8(from->lfrac, to->lfrac, factor);
        
        if (which & COLORKEY_MINLEVEL)
            result->mlevel = SceneAnim_Ease_s8(from->mlevel, to->mlevel, factor);
    }
    
    /* envcolor */
    if (which & COLORKEY_ENV) {
        result->env = SceneAnim_Ease_RGBA(from->env, to->env, factor);
    }
}

static float SceneAnim_Interpolate(u32 frame, u32 next, enum8(ease) ease) {
    float factor;
    
    if (!next)
        return 0;
    
    factor = (f32)frame / next;
    
    // TODO transform factor with easing transformations
    if (ease != EASE_LINEAR) {
        switch (ease) {
            /* sinusoidal (in) */
            case EASE_SIN_IN:
                break;
                
            /* sinusoidal (out) */
            case EASE_SIN_OUT:
                break;
                
            /* unsupported: use linear */
            default:
                break;
        }
    }
    
    return factor;
}

static void SceneAnim_Color_Loop(PlayState* play, Gfx** disp, ColorList* list) {
    ColorKey* from;
    ColorKey* to;
    ColorKey* lastKey = NULL;
    
    u32 totalFrames = !list->dur ? 1 : list->dur;
    u32 gameFrames = play->gameplayFrames;
    
    for (ColorKey* key = list->key; key->next; key++)
        lastKey = key;
    
    u32 curFrame = gameFrames % totalFrames;
    u32 relFrame = curFrame;
    
    from = list->key;
    to = list->key;
    
    u32 temp = 0;
    for (ColorKey* key = list->key, * prevKey = NULL; key->next; prevKey = key++) {
        temp += key->next;
        
        if (curFrame < temp) {
            to = key;
            
            if (!prevKey && !key->next)
                from = key;
            else
                from = prevKey ? prevKey : lastKey;
            
            f32 lerp = 0;
            
            if (relFrame)
                lerp = (1.0f / to->next) * relFrame;
            
            SceneAnim_BlendColorKey(list->which, lerp, from, to, &gSceneAnimCtx.Pcolorkey);
            SceneAnim_PutColorKey(list->which, disp, &gSceneAnimCtx.Pcolorkey);
            
            break;
        }
        
        relFrame -= key->next;
    }
}

static void SceneAnim_Color_LoopFlag(PlayState* play, Gfx** disp, ColorListFlag* c) {
    Flag* f = &c->flag;
    ColorList* list = &c->list;
    
    ColorKey* key;
    s32 active = SceneAnim_Flag(play, f);
    
    enum8(ColorKeyTypes) which = list->which;
    
    /* not across-fading, and flag is not active */
    if (!active && (!(f->freeze & FreezeFlag_Freeze)))
        return;
    
    key = &gSceneAnimCtx.Pcolorkey;
    
    if (active && (!(f->freeze & FreezeFlag_StopAtEnd) || 
        ((f->freeze & FreezeFlag_StopAtEnd) && c->flag.frames < list->dur-SceneAnim_GetNextColorKey(list, &list->key[0])->next)))
        c->flag.frames++;
    if (!active && (f->freeze & FreezeFlag_StopAtEnd) && c->flag.frames > 0)
        c->flag.frames--;
    u32 frame = c->flag.frames;
    
    /* if cross fading or flag is active, compute colors */

    ColorKey* from;
    ColorKey* to;

    float factor;
    
    from = SceneAnim_GetColorKey(list, &frame);
    to = SceneAnim_GetNextColorKey(list, from);

    
    
    factor = SceneAnim_Interpolate(frame, from->next, list->ease);
    
    /* blend color keys (result goes into key) */
    SceneAnim_BlendColorKey(which, factor, from, to, key);

    
    SceneAnim_PutColorKey(which, disp, key);
}

static void SceneAnim_Pointer_Flag(PlayState* play, Gfx** disp, PointerFlag* ptr) {
    // TODO don't forget to uncomment this
    *disp = (void*)SEGMENTED_TO_VIRTUAL(ptr->ptr[SceneAnim_Flag(play, &ptr->flag)]);
    // testing:
    // *disp = (void*)SEGMENTED_TO_VIRTUAL(ptr->ptr[0]);
}

static void SceneAnim_Pointer_Timeloop(PlayState* play, Gfx** disp, PointerTimeloop* ptr) {
    s32 item;
    s32 num = ptr->num;
    u32* list = (void*)(ptr->each + num + !(num & 1));
    
    /* walk list */
    for (item = ptr->prev; item < num; ++item)
        if (ptr->time >= ptr->each[item])
            break;
    
    /* reached end of animation; roll back to beginning */
    if (item >= num - 1)
        item = ptr->prev = ptr->time = 0;
    
    *disp = (void*)SEGMENTED_TO_VIRTUAL(list[item]);
    
    /* increment time elapsed */
    ptr->time += 1;
    if (ptr->time == ptr->each[item + 1])
        ptr->prev += 1;
}

static s32 SceneAnim_Pointer_TimeloopFlag(PlayState* play, Gfx** disp, PointerTimeloopFlag* _ptr) {
    PointerTimeloop* ptr = &_ptr->list;
    
    if (!SceneAnim_Flag(play, &_ptr->flag))
        return 0;
    
    SceneAnim_Pointer_Timeloop(play, disp, ptr);
    
    return 1;
}

static void SceneAnim_Pointer_Loop(PlayState* play, Gfx** disp, PointerLoop* ptr) {
    s32 item;
    
    /* overflow test */
    if (ptr->time >= ptr->dur)
        ptr->time = 0;
    
    item = ptr->time / ptr->each;
    *disp = (void*)SEGMENTED_TO_VIRTUAL(ptr->ptr[item]);
    
    /* increment time elapsed */
    ptr->time += 1;
}

static s32 SceneAnim_Pointer_LoopFlag(PlayState* play, Gfx** disp, PointerLoopFlag* _ptr) {
    PointerLoop* ptr = &_ptr->list;
    u8 flagstate = SceneAnim_Flag(play, &_ptr->flag);
    Flag* f = &_ptr->flag;
    
    if (!flagstate && f->freeze == 0)
        return 0;
    
    SceneAnim_Pointer_Loop(play, disp, ptr);
    
    // if freeze mode is set, time doesnt advance when flag is not set
    if (!flagstate && f->freeze & FreezeFlag_Freeze)
        ptr->time -= 1;
    
    return 1;
}

static void SceneAnim_TexScroll_One(PlayState* play, Gfx** disp, TexScroll* sc) {
    u32 frame = play->gameplayFrames;
    Gfx* dl = Gfx_TexScroll(play->state.gfxCtx, sc->u * frame, sc->v * frame * VSCROLL, sc->w, sc->h);
    
    gSPDisplayList((*disp)++, dl);
}

static void SceneAnim_TexScroll_Two(PlayState* play, Gfx** disp, TexScroll* sc) {
    u32 frame = play->gameplayFrames;
    Gfx* dl = Gfx_TwoTexScroll(
        play->state.gfxCtx,
        0, sc[0].u * frame, (sc[0].v * frame) * VSCROLL, sc[0].w, sc[0].h,
        1, sc[1].u * frame, (sc[1].v * frame) * VSCROLL, sc[1].w, sc[1].h);
    
    gSPDisplayList((*disp)++, dl);
}

static void SceneAnim_TexScroll_Flag(PlayState* play, Gfx** disp, TexScrollFlag* scroll) {
    TexScroll* sc = scroll->sc;
    TexScroll* sc1 = sc + 1;
    u16 frame = scroll->flag.frames;
    
    if (SceneAnim_Flag(play, &scroll->flag))
        scroll->flag.frames++;
    
    gDPSetTileSize(
        (*disp)++
        ,
        0,
        sc->u * frame,
        sc->v * frame * VSCROLL,
        sc->w,
        sc->h
    );
    
    gDPSetTileSize(
        (*disp)++
        ,
        1,
        sc1->u * frame,
        sc1->v * frame * VSCROLL,
        sc1->w,
        sc1->h
    );
}

/* change pointer as time progresses (each pointer has its own time) */
/* skipped if flag is undesirable */
static s32 SceneAnim_CameraEffect(PlayState* play, Gfx** disp, CameraEffect* cam) { // TODO
    u8 cameratype = cam->cameratype;
    
    if (!SceneAnim_Flag(play, &cam->flag))
        return 0;
    
    if (cameratype == 0) {
        func_8009BEEC(play); // camera shake
    } else { // jabu jabu
        static s16 D_UNK_JAB1 = 538;
        static s16 D_UNK_JAB2 = 4272;
        
        f32 temp;
        
        if (FrameAdvance_IsEnabled(play) != true) {
            D_UNK_JAB1 += 1820;
            D_UNK_JAB2 += 1820;
            
            temp = 0.020000001f;
            View_SetDistortionOrientation(
                &play->view,
                ((360.00018f / 65535.0f) * (M_PI / 180.0f)) * temp * Math_CosS(D_UNK_JAB1),
                ((360.00018f / 65535.0f) * (M_PI / 180.0f)) * temp * Math_SinS(D_UNK_JAB1),
                ((360.00018f / 65535.0f) * (M_PI / 180.0f)) * temp * Math_SinS(D_UNK_JAB2)
            );
            View_SetDistortionScale(
                &play->view,
                1.f + (0.79999995f * temp * Math_SinS(D_UNK_JAB2)),
                1.f + (0.39999998f * temp * Math_CosS(D_UNK_JAB2)),
                1.f + (1 * temp * Math_CosS(D_UNK_JAB1))
            );
            View_SetDistortionSpeed(&play->view, 0.95f);
        }
    }
    
    return 1;
}

/* Draws or hides a display list depending on flag by scaling it with a matrix */
static s32 SceneAnim_DrawCondition(PlayState* play, Gfx** disp, DrawCondition* _ptr, u8 seg) {
    Matrix_Push();
    if (!SceneAnim_Flag(play, &_ptr->flag))
    {
        Matrix_Scale(0.0f, 0.0f, 0.0f, MTXMODE_NEW);
    }
    else
    {
        Matrix_Scale(1.0f, 1.0f, 1.0f, MTXMODE_NEW);
    }


    gSPSegment((*disp)++, seg, Matrix_NewMtx(play->state.gfxCtx, __FILE__, __LINE__));

    Matrix_Pop();

    return 1;
}

const u8 Poly[2][2] = {{0,1},{1,0}};

static void SceneAnim_PolytypeSwapSetData(PlayState* play, PolytypeSwap* data, u8 active) {

    play->colCtx.colHeader->surfaceTypeList[data->polytypeID[Poly[active][0]]].data[Poly[active][0]] = data->polytype1Data[Poly[active][0]];
    play->colCtx.colHeader->surfaceTypeList[data->polytypeID[Poly[active][0]]].data[Poly[active][1]] = data->polytype1Data[Poly[active][1]];
    play->colCtx.colHeader->surfaceTypeList[data->polytypeID[Poly[active][1]]].data[Poly[active][0]] = data->polytype2Data[Poly[active][0]];
    play->colCtx.colHeader->surfaceTypeList[data->polytypeID[Poly[active][1]]].data[Poly[active][1]] = data->polytype2Data[Poly[active][1]];

    u16 triID = data->triangleIDs[0];
    u16 c = 0;

    for(int i = 0; i<2; i++)
    {
        u16 flagsA = ((data->polytypeVFlags[Poly[active][i]] & 0x0F) << 12);
        u16 flagsB = ((data->polytypeVFlags[Poly[active][i]] & 0xF0) << 8);
        while(triID != 0xFFFF)
        {
            play->colCtx.colHeader->polyList[triID].flags_vIA = (play->colCtx.colHeader->polyList[triID].flags_vIA & 0x1FFF) + flagsA;
            play->colCtx.colHeader->polyList[triID].flags_vIB = (play->colCtx.colHeader->polyList[triID].flags_vIB & 0x1FFF) + flagsB;
            c++;
            triID = data->triangleIDs[c];
        }
    }
}

/* changes a polytype when flag is set */
static s32 SceneAnim_PolytypeSwap(PlayState* play, Gfx** disp, PolytypeSwap* data) {

    if (!SceneAnim_Flag(play, &data->flag))
    {
        if (data->curTimer > 1)
        {
            data->curTimer--;
        }
        else if (data->curTimer == 1)
        {
            data->curTimer = 0;

            SceneAnim_PolytypeSwapSetData(play,data,0);
        }
    }
    else
    {
        if (data->curTimer < data->maxTimer)
        {
            data->curTimer++;
        }
        else if (data->curTimer == data->maxTimer)
        {
            data->curTimer = data->maxTimer+1;

            SceneAnim_PolytypeSwapSetData(play,data,1);
        }
    }
    
    return 1;
}


static AnimInfo* SceneAnim_GetSceneAnimCommand(void* _scene) {
    SceneCmd* cmd = _scene;
    
    for (; cmd->base.code != SCENE_CMD_ID_END; cmd++) {
        if (cmd->base.code == 0x1A) {
            if (cmd->base.code == 0) return NULL;
            
            return SEGMENTED_TO_VIRTUAL(cmd->base.data2);
        }
    }
    
    return NULL;
}

static inline void SceneAnim_Overlay(PlayState *play, struct SceneOvl *sovl)
{
    // not yet relocated (not a ram address)
    if (!(sovl->u.main & 0x80000000))
    {
        u8 *scene  = (u8*)play->sceneSegment;
        u8 *start  = scene + sovl->start;
        u8 *end    = scene + sovl->end;
        u8 *header = end - ((u32*)(end))[-1];
        u32 main   = sovl->u.main;
        u32 size   = end - start;
        
        // relocate overlay from virtual ram to physical ram
        Overlay_Relocate(start, (void*)header, (void*)0x80800000);
        
        // clear instruction cache for memory region occupied by overlay
        osWritebackDCache(start, size);
        osInvalICache(start, size);
        
        // update exec to point to main routine
        sovl->u.exec = (void (*)(void*))(scene + main);
    }
    
    // run main routine
    sovl->u.exec(play);
}

static void SceneAnim_UnusedDL(Gfx** disp) {
    gDPNoOp((*disp)++);
    gSPEndDisplayList((*disp)++);
    return;
}

#include <code/z_scene_table.h>

static void SceneAnim_WriteDisp(Gfx* head, Gfx* disp, int prevSeg) {
    if (disp) {
        if (disp > head)
            gSPEndDisplayList(disp++);
        
        SegmentData(prevSeg, head);
    }
}

void SceneAnim_Update(PlayState* play) {
    void* hdr = Segment_Scene_GetHeader(play->sceneSegment, gSaveContext.sceneLayer);
    
    gSceneAnimCtx.animInfoList = SceneAnim_GetSceneAnimCommand(hdr);
    
    if (!gSceneAnimCtx.animInfoList) {
        for (int i = 0x8; i <= 0xF; i++) {
            Gfx* disp;
            Gfx* head;
            
            disp = head = Graph_Alloc(__gfxCtx, sizeof(Gfx[2]));
            gDPNoOp(disp++);
            gSPEndDisplayList(disp++);
            SegmentData(i, head);
        }
        
        return;
    }
    // scene overlay format: first (and only) item in list should be type 0xffff
    else if (gSceneAnimCtx.animInfoList->type == 0xffff) {
        SceneAnim_Overlay(play, SEGMENTED_TO_VIRTUAL(gSceneAnimCtx.animInfoList->data));
        return;
    }
    
    if (!gSceneAnimCtx.animInfoList->processFlag) {
        gSceneAnimCtx.animInfoList->processFlag = 1;
        
        for (AnimInfo* item = gSceneAnimCtx.animInfoList;; ++item) {
            item->nseg = ABS(item->seg) + 7;
            
            if (item->seg <= 0)
                break;
        }
    }
    
    gDPPipeSync(POLY_OPA_DISP++);
    gDPSetEnvColor(POLY_OPA_DISP++, 128, 128, 128, 128);
    
    gDPPipeSync(POLY_XLU_DISP++);
    gDPSetEnvColor(POLY_XLU_DISP++, 128, 128, 128, 128);
    
    Gfx* disp = NULL;
    Gfx* head = NULL;
    int prevSeg = 0;
    int hasWrittenPointer = 0;
    
    for (AnimInfo* item = gSceneAnimCtx.animInfoList;; ++item) {
        int seg = item->nseg;
        void* data = SEGMENTED_TO_VIRTUAL(item->data);
        
        if (seg != prevSeg || !disp) {
            SceneAnim_WriteDisp(head, disp, prevSeg);
            head = disp = Graph_Alloc(play->state.gfxCtx, sizeof(Gfx[8]));
            prevSeg = seg;
            hasWrittenPointer = 0;
        }
        
        switch (item->type) {
            case 0x0000:
                SceneAnim_TexScroll_One(play, &disp, data);
                break;
                
            case 0x0001:
                SceneAnim_TexScroll_Two(play, &disp, data);
                break;
                
            /* cycle through color list (advance one per frame) */
            case 0x0002:
                SceneAnim_UnusedDL(&disp);
                break;
                
            /* unused in MM */
            case 0x0003:
                SceneAnim_UnusedDL(&disp);
                break;
                
            /* color easing with keyframe support */
            case 0x0004:
                SceneAnim_UnusedDL(&disp);
                break;
                
            /* flag-based texture scrolling */
            /* used only in Sakon's Hideout in MM */
            case 0x0005:
                SceneAnim_UnusedDL(&disp);
                break;
                
            /* nothing; data and seg are always 0 when this is used */
            case 0x0006:
                SceneAnim_UnusedDL(&disp);
                break;
                
            /* extended functionality */
                
            case 0x0007:
                if (hasWrittenPointer)
                    break;
                hasWrittenPointer = 1;
                SceneAnim_Pointer_Flag(play, &disp, data);
                head = disp;
                break;
                
            case 0x0008:
                SceneAnim_TexScroll_Flag(play, &disp, data);
                break;
                
            case 0x0009:
                SceneAnim_Color_Loop(play, &disp, data);
                break;
                
            case 0x000A:
                SceneAnim_Color_LoopFlag(play, &disp, data);
                break;
                
            case 0x000B:
                if (hasWrittenPointer)
                    break;
                hasWrittenPointer = 1;
                SceneAnim_Pointer_Loop(play, &disp, data);
                head = disp;
                break;
                
            case 0x000C:
                if (hasWrittenPointer)
                    break;
                if (SceneAnim_Pointer_LoopFlag(play, &disp, data))
                    hasWrittenPointer = 1;
                head = disp;
                break;
                
            case 0x000D:
                if (hasWrittenPointer)
                    break;
                hasWrittenPointer = 1;
                SceneAnim_Pointer_Timeloop(play, &disp, data);
                head = disp;
                break;
                
            case 0x000E:
                if (hasWrittenPointer)
                    break;
                if (SceneAnim_Pointer_TimeloopFlag(play, &disp, data))
                    hasWrittenPointer = 1;
                head = disp;
                break;
                
            case 0x000F:
                SceneAnim_CameraEffect(play, &disp, data);
                break;
                
            case 0x0010:
                SceneAnim_DrawCondition(play, &disp, data, seg);
                break;

            case 0x0011:
                SceneAnim_PolytypeSwap(play, &disp, data);
                break;
                
            default:
                SceneAnim_UnusedDL(&disp);
                break;
        }
        
        if (item->seg <= 0) {
            SceneAnim_WriteDisp(head, disp, prevSeg);
            
            break;
        }
    }
}
