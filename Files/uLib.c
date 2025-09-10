#include <uLib.h>
#include "vt.h"

#include <uLib_vector.h>

//Version: 1.1
/*Used by SharpOcarina to determine if the project is safe to be built with --release. If --release bug ever gets fixed the correct way this will
be removed in future updates as well as the ulib.c modifications
*/

void SoundEffect_Update(PlayState* play);

f32 EPSILON = 0.0000001f;

asm ("gSfxDefaultPos = 0x801333D4");
asm ("gSfxDefaultFreqAndVolScale = 0x801333E0");
asm ("gSfxDefaultReverb = 0x801333E8");
asm ("MapSelect_Init = 0x80801E44");

void uLib_Update(GameState* gameState) {
    PlayState* play = (void*)gameState;
    
    gSaveContext.language = 0;
    
#ifdef DEV_BUILD
    static s32 firstMessage;
    const char* state[] = {
        "" PRNT_REDD "false",
        "" PRNT_BLUE "true",
    };
    
    if (firstMessage == 0) {
        osLibPrintf("" PRNT_BLUE "--- [z64rom] ---\n");
        osLibPrintf("Vanilla Printf [L + D-UP]");
        osLibPrintf("Dma Info       [L + D-DOWN]");
        osLibPrintf("Player Print   [L + D-LEFT]");
        firstMessage++;
    }
    
    if (CHK_ALL(press, BTN_L | BTN_DUP)) {
        gLibCtx.state.vanillaOsPrintf ^= 1;
        osLibPrintf("Vanilla Messages: [%s" PRNT_RSET "]", state[gLibCtx.state.vanillaOsPrintf]);
    }
    
    if (CHK_ALL(press, BTN_L | BTN_DDOWN)) {
        gLibCtx.state.dmaLog ^= 1;
        osLibPrintf("Dma Info: [%s" PRNT_RSET "]", state[gLibCtx.state.dmaLog]);
    }
    
    if (CHK_ALL(press, BTN_L | BTN_DLEFT)) {
        gLibCtx.state.playerPrint ^= 1;
        osLibPrintf("Player Print: [%s" PRNT_RSET "]", state[gLibCtx.state.playerPrint]);
    }
    
    if (CHK_ALL(press, BTN_Z) && CHK_ALL(cur, BTN_L | BTN_R)) {
        gSaveContext.gameMode = 0;
        SET_NEXT_GAMESTATE(gameState, MapSelect_Init, MapSelectState);
        gameState->running = false;
    }
    
#endif
    
    if (gLibCtx.state.isPlayGameMode) {
        
#if Patch_QuickText == true
        // Skip current textbox when pressing B
        static s32 soundFlag;
        MessageContext* msgCtx = &play->msgCtx;
        
        msgCtx->textUnskippable = 1;
        
        if (soundFlag == 1 && (msgCtx->msgMode == 52 || msgCtx->textboxEndType == 0x30)) {
            Audio_PlaySys(NA_SE_SY_MESSAGE_END);
            soundFlag = 0;
        }
        
        #define msgCtx_DecodeCur *AVAL(msgCtx, u16, 0xE3D2)
        #define msgCtx_DecodeEnd *AVAL(msgCtx, u16, 0xE3D4)
        
        if (CHK_ALL(press, BTN_B) && play->msgCtx.msgMode == 6 && msgCtx_DecodeCur >= 1 && msgCtx_DecodeCur < msgCtx_DecodeEnd) {
            msgCtx_DecodeCur = msgCtx_DecodeEnd;
            soundFlag = 1;
        }
#endif
        
    }
    
    SoundEffect_Update(play);
}

void* memset(void* m, int v, unsigned int s) {
    for (s32 i = 0; i < s; i++)
        ((u8*)m)[i] = v;
    
    return NULL;
}

void* memmove(void* dest, const void* src, size_t len) {
	char* d = dest;
	const char* s = src;
	
	if (d < s) {
		while (len--)
			*d++ = *s++;
		
	} else {
		char* lasts = (char*)s + (len - 1);
		char* lastd = d + (len - 1);
		while (len--)
			*lastd-- = *lasts--;
	}
	
	return dest;
}

#if DEV_BUILD

void Profiler_Start(DebugProfiler* profiler) {
    if (gLibCtx.profiler.enabled)
        profiler->start = osGetTime();
}

void Profiler_End(DebugProfiler* profiler) {
    if (gLibCtx.profiler.enabled)
        profiler->buffer[profiler->ringId] += osGetTime() - profiler->start;
}

static void __p_osLibPrintf(const char* fmt, ...) {
    va_list args;
    
    va_start(args, fmt);
    
    _Printf(is_proutSyncPrintf, NULL, fmt, args);
    
    va_end(args);
}

void osLibPrintf(const char* fmt, ...) {
    va_list args;
    
    va_start(args, fmt);
    
    __p_osLibPrintf("" PRNT_GRAY "[" PRNT_BLUE ">" PRNT_GRAY "]: " PRNT_RSET);
    _Printf(is_proutSyncPrintf, NULL, fmt, args);
    __p_osLibPrintf("" PRNT_RSET "\n");
    
    va_end(args);
}

void osLibHex(const char* txt, const void* data, u32 size, u32 dispOffset) {
    const u8* d = data;
    u32 num = 8;
    char digit[64];
    s32 i = 0;
    
    for (;; num--)
        if ((size + dispOffset) >> (num * 4))
            break;
    
    sprintf(digit, "" PRNT_GRAY "%c0%dX: " PRNT_RSET, '%', num + 1);
    
    if (txt)
        osLibPrintf("%s", txt);
    for (; i < size; i++) {
        if (i % 16 == 0)
            __p_osLibPrintf(digit, i + dispOffset);
        
        __p_osLibPrintf("%02X", d[i]);
        if ((i + 1) % 4 == 0)
            __p_osLibPrintf(" ");
        if ((i + 1) % 16 == 0)
            __p_osLibPrintf("\n");
    }
    
    if (i % 16 != 0)
        __p_osLibPrintf("\n");
}
#else

void Profiler_Start(DebugProfiler* profiler) {};
void Profiler_End(DebugProfiler* profiler) {};
static void __p_osLibPrintf(const char* fmt, ...) {};
void osLibPrintf(const char* fmt, ...) {};
void osLibHex(const char* txt, const void* data, u32 size, u32 dispOffset) {};

#endif