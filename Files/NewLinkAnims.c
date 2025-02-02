#include "uLib_macros.h"
#include <uLib.h>

#define NEWLINKANIMS_DMAID 0x20

AnimationEntry* AnimationContext_AddEntry(AnimationContext* animationCtx, AnimationType type);
asm ("AnimationContext_AddEntry = 0x800A3334");

#define NEW_LINK_ANIMATION_OFFSET(addr, offset) \
    (((((u32)(addr) >> 24) == 0x17) ? \
      ((gDmaDataTable[NEWLINKANIMS_DMAID].vromStart) + ((u32)(addr)) - (0x17000000) + ((u32)(offset))) : \
      ((gDmaDataTable[6].vromStart) + ((u32)(addr)) - (0x07000000) + ((u32)(offset)))))
    
Asm_VanillaHook(AnimationContext_SetLoadFrame);
void AnimationContext_SetLoadFrame(PlayState* playState, LinkAnimationHeader* animation, s32 frame, s32 limbCount, Vec3s* frameTable) {
    AnimationEntry* entry = AnimationContext_AddEntry(&playState->animationCtx, ANIMENTRY_LOADFRAME);
    
    if (entry != NULL) {
        LinkAnimationHeader* linkAnimHeader = SEGMENTED_TO_VIRTUAL(animation);
        
        osCreateMesgQueue(&entry->data.load.msgQueue, &entry->data.load.msg, 1);
        DmaMgr_SendRequest2(
            &entry->data.load.req,
            (u32)frameTable,
            NEW_LINK_ANIMATION_OFFSET(linkAnimHeader->segment, ((sizeof(Vec3s) * limbCount + 2) * frame)),
            sizeof(Vec3s) * limbCount + 2,
            0,
            &entry->data.load.msgQueue,
            NULL,
            0,
            0
        );
    }
}
