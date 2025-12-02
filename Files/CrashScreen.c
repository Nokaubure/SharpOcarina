#include <uLib.h>
#include "global.h"
#include "macros.h"
#include "variables.h"
#include "vt.h"
#include "alloca.h"
#include "z64.h"
#include "z64hdr/include/code/fault.h"
#include "z64hdr/include/code/fault_drawer.h"

#define FUNCTIONNAMES_DMAID 0x22
#define FUNCTIONNAMES_MAXACTORS 0x0205

#define FAULT_MSG_CPU_BREAK ((OSMesg)1)
#define FAULT_MSG_FAULT ((OSMesg)2)
#define FAULT_MSG_UNK ((OSMesg)3)

void FaultDrawer_Init(void);
void FaultDrawer_SetOsSyncPrintfEnabled(u32 enabled);
void FaultDrawer_DrawRecImpl(s32 xStart, s32 yStart, s32 xEnd, s32 yEnd, u16 color);
void FaultDrawer_FillScreen(void);
void FaultDrawer_SetInputCallback(void (*callback)(void));
void FaultDrawer_SetDrawerFB(void* fb, u16 w, u16 h);
extern STACK(sFaultStack, 0x600);
void PadMgr_RequestPadData(Input* inputs, s32 gameRequest);
void Fault_ClientProcessThread(void* arg);
void Fault_ClientRunTask(FaultClientTask* task);
s32 Fault_ProcessClient(void* callback, void* arg0, void* arg1);
void Fault_RemoveAddrConvClient(FaultAddrConvClient* client);
void Fault_SleepImpl(u32 msec);
void Fault_Sleep(u32 msec);
void Fault_UpdatePadImpl(void);
u32 Fault_WaitForInputImpl(void);
void Fault_DrawRec(s32 x, s32 y, s32 w, s32 h, u16 color);
void Fault_PrintFPCSR(u32 value);
void Fault_PrintFReg(s32 idx, f32* value);
void Fault_LogFPCSR(u32 value);
void Fault_LogFReg(s32 idx, f32* value);
void Fault_FillScreenBlack(void);
void Fault_DrawMemDumpContents(const char* title, uintptr_t addr, u32 arg2);
uintptr_t Fault_ConvertAddress(uintptr_t addr);
void Fault_WalkStack(uintptr_t* spPtr, uintptr_t* pcPtr, uintptr_t* raPtr);
void Fault_WaitForInput(void);
void Fault_DisplayFrameBuffer(void);
void Fault_UpdatePad(void);
OSThread* Fault_FindFaultedThread(void);
void Fault_Wait5Seconds(void);
void Fault_DrawCornerRec(u16 color);
void Fault_WaitForButtonCombo(void);
void Fault_PrintThreadContext(OSThread* thread);
void Fault_LogThreadContext(OSThread* thread);
void Fault_DrawStackTrace(OSThread* thread, s32 x, s32 y, s32 height);
void Fault_LogStackTrace(OSThread* thread, s32 height);
void Fault_ProcessClients(void);
void Fault_DrawMemDump(uintptr_t pc, uintptr_t sp, uintptr_t cLeftJump, uintptr_t cRightJump);
void Fault_FillScreenRed(void);
void Fault_ResumeThread(OSThread* thread);
void Fault_AddClient(FaultClient* client, void* callback, void* arg0, void* arg1);
void Fault_AddHungupAndCrashImpl(const char* exp1, const char* exp2);

#define IO_READ(addr)       (*(vu32*)PHYS_TO_K1(addr))
#define IO_WRITE(addr,data) (*(vu32*)PHYS_TO_K1(addr)=(u32)(data))
static u32 sPrevPICfg[4];


typedef struct {
u32 StartAddress;
u32 EndAddress;
char Name[32];
}FunctionName;

const char* sExceptionNames[] = {
    "Interrupt",
    "TLB modification",
    "TLB exception on load",
    "TLB exception on store",
    "Address error on load",
    "Address error on store",
    "Bus error on inst.",
    "Bus error on data",
    "System call exception",
    "Breakpoint exception",
    "Reserved instruction",
    "Coprocessor unusable",
    "Arithmetic overflow",
    "Trap exception",
    "Virtual coherency on inst.",
    "Floating point exception",
    "Watchpoint exception",
    "Virtual coherency on data",
};
const char* sFpExceptionNames[] = {
    "Unimplemented operation", "Invalid operation", "Division by zero", "Overflow", "Underflow", "Inexact operation",
};

static void Fault_PIGetAccess(void) {
    // Get exclusive access to the PI
    __osPiGetAccess();

    // Await any current DMA/IO completion
    u32 piStatus = IO_READ(PI_STATUS_REG);
    while (piStatus & (PI_STATUS_BUSY | PI_STATUS_IOBUSY)) {
        piStatus = IO_READ(PI_STATUS_REG);
    }

    // Save previous Domain 1 configuration
    sPrevPICfg[0] = IO_READ(PI_BSD_DOM1_LAT_REG);
    sPrevPICfg[1] = IO_READ(PI_BSD_DOM1_PWD_REG);
    sPrevPICfg[2] = IO_READ(PI_BSD_DOM1_PGS_REG);
    sPrevPICfg[3] = IO_READ(PI_BSD_DOM1_RLS_REG);

    // Set cartridge access timings
    IO_WRITE(PI_BSD_DOM1_LAT_REG, gCartHandle->latency);
    IO_WRITE(PI_BSD_DOM1_PWD_REG, gCartHandle->pulse);
    IO_WRITE(PI_BSD_DOM1_PGS_REG, gCartHandle->pageSize);
    IO_WRITE(PI_BSD_DOM1_RLS_REG, gCartHandle->relDuration);
}

static void Fault_PIRelAccess(void) {
    // Restore previous Domain 1 configuration
    IO_WRITE(PI_BSD_DOM1_LAT_REG, sPrevPICfg[0]);
    IO_WRITE(PI_BSD_DOM1_PWD_REG, sPrevPICfg[1]);
    IO_WRITE(PI_BSD_DOM1_PGS_REG, sPrevPICfg[2]);
    IO_WRITE(PI_BSD_DOM1_RLS_REG, sPrevPICfg[3]);

    // Release PI
    __osPiRelAccess();
}

void DmaString(u32 offset, char* nameOut)
{
    Fault_PIGetAccess();
    uintptr_t start = offset+0xB0000000;
    char* nameCur = nameOut;
    for(u8 i = 0; i < 32; i++) {
        u32 dat = *(u32*)(start); 
        *nameCur++ = (dat >> 24) & 0xFF;
        *nameCur++ = (dat >> 16) & 0xFF;
        *nameCur++ = (dat >>  8) & 0xFF;
        *nameCur++ = (dat >>  0) & 0xFF;
        start +=4;
        if ((dat & 0x000000FF) == 0) {
            break;
        }
    }
    Fault_PIRelAccess();
}
u32 DmaU32(u32 offset)
{
    Fault_PIGetAccess();
    uintptr_t start = offset + 0xB0000000; 
    u32 ret = *(u32*)(start); 
    Fault_PIRelAccess();
    return ret;
}

static bool isAssert = false;
//We fix a bug where the original crash debugger is unable to continue assert threads
Asm_VanillaHook(Fault_AddHungupAndCrashImpl);
void Fault_AddHungupAndCrashImpl(const char* exp1, const char* exp2) {
    FaultClient client;

    isAssert = true;
    
    Fault_AddClient(&client, Fault_HungupFaultClient, (void*)exp1, (void*)exp2);
    
    OSThread* thd = __osRunningThread;
    
    __osFaultedThread = thd;
    osSendMesg(&sFaultInstance->queue, FAULT_MSG_CPU_BREAK, OS_MESG_BLOCK);
    osStopThread(thd);
    
    Fault_RemoveClient(&client);
}



Asm_VanillaHook(Fault_ThreadEntry);
void Fault_ThreadEntry(void* arg) {
    OSMesg msg;
    OSThread* faultedThread;
    s32 pad;

    osSetEventMesg(OS_EVENT_CPU_BREAK, &sFaultInstance->queue, FAULT_MSG_CPU_BREAK);
    osSetEventMesg(OS_EVENT_FAULT, &sFaultInstance->queue, FAULT_MSG_FAULT);

    while (true) {
        do {
            osRecvMesg(&sFaultInstance->queue, &msg, OS_MESG_BLOCK);

            if (msg == FAULT_MSG_CPU_BREAK) {
                sFaultInstance->msgId = (u32)FAULT_MSG_CPU_BREAK;
            } else if (msg == FAULT_MSG_FAULT) {
                sFaultInstance->msgId = (u32)FAULT_MSG_FAULT;
            } else if (msg == FAULT_MSG_UNK) {
                Fault_UpdatePad();
                faultedThread = NULL;
                continue;
            } else {
                sFaultInstance->msgId = (u32)FAULT_MSG_UNK;
            }

            faultedThread = __osGetCurrFaultedThread();

            if (faultedThread == NULL) {
                faultedThread = Fault_FindFaultedThread();
            }
        } while (faultedThread == NULL);

        __osSetFpcCsr(__osGetFpcCsr() & ~(FPCSR_EV | FPCSR_EZ | FPCSR_EO | FPCSR_EU | FPCSR_EI));
        sFaultInstance->faultedThread = faultedThread;

        while (!sFaultInstance->faultHandlerEnabled) {
            Fault_Sleep(10);
        }
        Fault_Sleep(1000 / 2);

        Fault_DisplayFrameBuffer();

        if (sFaultInstance->autoScroll) {
            Fault_Wait5Seconds();
        } else {
            Fault_DrawCornerRec(GPACK_RGBA5551(255, 0, 0, 1));
            Fault_WaitForButtonCombo();
        }

        sFaultInstance->autoScroll = false;
        FaultDrawer_SetForeColor(GPACK_RGBA5551(255, 255, 255, 1));
        FaultDrawer_SetBackColor(GPACK_RGBA5551(0, 0, 0, 0));

        do {
            //Fault_PrintThreadContext(faultedThread);
            //Fault_LogThreadContext(faultedThread);
            //Fault_WaitForInput();
            //Fault_FillScreenBlack();
            //FaultDrawer_DrawText(120, 16, "STACK TRACE");
            Fault_DrawStackTrace(faultedThread, 28, 24, 22);
            Fault_LogStackTrace(faultedThread, 50);
            Fault_WaitForInput();
            //Fault_PrintThreadContext(faultedThread);
            //Fault_LogThreadContext(faultedThread);
            FaultDrawer_SetCursor(22, 20);
            Fault_ProcessClients();
            Fault_WaitForInput();
            if (isAssert)
            {
                isAssert = false;
                break;
            }
            //Fault_ProcessClients();
            //Fault_DrawMemDump(faultedThread->context.pc - 0x100, (uintptr_t)faultedThread->context.sp, 0, 0);
            Fault_FillScreenRed();
            FaultDrawer_DrawText(50, 90,  "        You can do it!");
            FaultDrawer_DrawText(50, 100, "Stay strong and NEVER give up!");
            FaultDrawer_DrawText(50, 118, "            <(-_-<)  ");
            Fault_WaitForInput();
            break;
        } while (!sFaultInstance->exit);

        sFaultInstance->exit = 1;
        __osFaultedThread = NULL;
        osStartThread(faultedThread);
    }
}

//changed to only show the first client
Asm_VanillaHook(Fault_ProcessClients);
void Fault_ProcessClients(void) {
    FaultClient* client = sFaultInstance->clients;
    s32 idx = 0;

    //while (client != NULL) {
        if (client->callback != NULL) {
            FaultDrawer_DrawRecImpl(2, 16, SCREEN_WIDTH-2, SCREEN_HEIGHT-16,GPACK_RGBA5551(0, 0, 0, 0) | 1);
            FaultDrawer_SetCharPad(-2, 0);
            FaultDrawer_Printf(FAULT_COLOR(DARK_GRAY) "CallBack (%d) %08x %08x %08x\n" FAULT_COLOR(WHITE), idx++,
                               client, client->arg0, client->arg1);
            FaultDrawer_SetCharPad(0, 0);
            Fault_ProcessClient(client->callback, client->arg0, client->arg1);
            Fault_WaitForInput();
            Fault_DisplayFrameBuffer();
        }
    //    client = client->next;
    //}
}


Asm_VanillaHook(Fault_PrintThreadContext);
void Fault_PrintThreadContext(OSThread* thread) {
    __OSThreadContext* ctx;
    s16 causeStrIdx = _SHIFTR((u32)thread->context.cause, 2, 5);

    if (causeStrIdx == 23) { // Watchpoint
        causeStrIdx = 16;
    }
    if (causeStrIdx == 31) { // Virtual coherency on data
        causeStrIdx = 17;
    }

    FaultDrawer_FillScreen();
    FaultDrawer_SetCharPad(-2, 4);
    FaultDrawer_SetCursor(22, 20);

    ctx = &thread->context;
    FaultDrawer_Printf("THREAD:%d (%d:%s)\n", thread->id, causeStrIdx, sExceptionNames[causeStrIdx]);
    FaultDrawer_SetCharPad(-1, 0);

    FaultDrawer_Printf("PC:%08xH SR:%08xH VA:%08xH\n", (u32)ctx->pc, (u32)ctx->sr, (u32)ctx->badvaddr);
    FaultDrawer_Printf("AT:%08xH V0:%08xH V1:%08xH\n", (u32)ctx->at, (u32)ctx->v0, (u32)ctx->v1);
    FaultDrawer_Printf("A0:%08xH A1:%08xH A2:%08xH\n", (u32)ctx->a0, (u32)ctx->a1, (u32)ctx->a2);
    FaultDrawer_Printf("A3:%08xH T0:%08xH T1:%08xH\n", (u32)ctx->a3, (u32)ctx->t0, (u32)ctx->t1);
    FaultDrawer_Printf("T2:%08xH T3:%08xH T4:%08xH\n", (u32)ctx->t2, (u32)ctx->t3, (u32)ctx->t4);
    FaultDrawer_Printf("T5:%08xH T6:%08xH T7:%08xH\n", (u32)ctx->t5, (u32)ctx->t6, (u32)ctx->t7);
    FaultDrawer_Printf("S0:%08xH S1:%08xH S2:%08xH\n", (u32)ctx->s0, (u32)ctx->s1, (u32)ctx->s2);
    FaultDrawer_Printf("S3:%08xH S4:%08xH S5:%08xH\n", (u32)ctx->s3, (u32)ctx->s4, (u32)ctx->s5);
    FaultDrawer_Printf("S6:%08xH S7:%08xH T8:%08xH\n", (u32)ctx->s6, (u32)ctx->s7, (u32)ctx->t8);
    FaultDrawer_Printf("T9:%08xH GP:%08xH SP:%08xH\n", (u32)ctx->t9, (u32)ctx->gp, (u32)ctx->sp);
    FaultDrawer_Printf("S8:%08xH RA:%08xH LO:%08xH\n\n", (u32)ctx->s8, (u32)ctx->ra, (u32)ctx->lo);

    Fault_PrintFPCSR(ctx->fpcsr);
    FaultDrawer_Printf("\n");

    Fault_PrintFReg(0, &ctx->fp0.f.f_even);
    Fault_PrintFReg(2, &ctx->fp2.f.f_even);
    FaultDrawer_Printf("\n");
    Fault_PrintFReg(4, &ctx->fp4.f.f_even);
    Fault_PrintFReg(6, &ctx->fp6.f.f_even);
    FaultDrawer_Printf("\n");
    Fault_PrintFReg(8, &ctx->fp8.f.f_even);
    Fault_PrintFReg(10, &ctx->fp10.f.f_even);
    FaultDrawer_Printf("\n");
    Fault_PrintFReg(12, &ctx->fp12.f.f_even);
    Fault_PrintFReg(14, &ctx->fp14.f.f_even);
    FaultDrawer_Printf("\n");
    Fault_PrintFReg(16, &ctx->fp16.f.f_even);
    Fault_PrintFReg(18, &ctx->fp18.f.f_even);
    FaultDrawer_Printf("\n");
    Fault_PrintFReg(20, &ctx->fp20.f.f_even);
    Fault_PrintFReg(22, &ctx->fp22.f.f_even);
    FaultDrawer_Printf("\n");
    Fault_PrintFReg(24, &ctx->fp24.f.f_even);
    Fault_PrintFReg(26, &ctx->fp26.f.f_even);
    FaultDrawer_Printf("\n");
    Fault_PrintFReg(28, &ctx->fp28.f.f_even);
    Fault_PrintFReg(30, &ctx->fp30.f.f_even);
    FaultDrawer_Printf("\n");

    FaultDrawer_SetCharPad(0, 0);
}

bool SearchForName(uintptr_t pc, int index, char* tmp)
{
    u32 tableoffset = DmaU32(gDmaDataTable[FUNCTIONNAMES_DMAID].romStart + (index * 4));
    u32 StartAddress = 0;
    u32 EndAddress;
    u32 NameOffset;
    for(u32 i = 0; StartAddress != 0xFFFFFFFF; i++)
    {
        StartAddress = DmaU32(gDmaDataTable[FUNCTIONNAMES_DMAID].romStart + tableoffset + i*40);
        EndAddress = DmaU32(gDmaDataTable[FUNCTIONNAMES_DMAID].romStart + tableoffset + i*40 + 4);
        if (pc >= StartAddress && pc <= EndAddress)
        {
            NameOffset = gDmaDataTable[FUNCTIONNAMES_DMAID].romStart + tableoffset + i*40 + 8;
            DmaString(NameOffset, tmp);
            return true;
            break;
        }
    }
    return false;
}


const char NotFound[32] = {"-"};
const char FoundActor[32] = {"Actor"};
const char UnknownFunction[32] = {"Unknown Function"};
static char Test[32];
const char Types[][8] = 
{
    {"Code"},
    {"Player"},
    {"Pause"},
    {"Hook"},
};

Asm_VanillaHook(Fault_DrawStackTrace);
void Fault_DrawStackTrace(OSThread* thread, s32 x, s32 y, s32 height) {

    s16 causeStrIdx = _SHIFTR((u32)thread->context.cause, 2, 5);

    if (causeStrIdx == 23) { // Watchpoint
        causeStrIdx = 16;
    }
    if (causeStrIdx == 31) { // Virtual coherency on data
        causeStrIdx = 17;
    }

    //FaultDrawer_FillScreen();

    FaultDrawer_DrawRecImpl(2, 16, SCREEN_WIDTH-2, SCREEN_HEIGHT-16,GPACK_RGBA5551(0, 0, 0, 0) | 1);
    //FaultDrawer_SetCursor(sFaultDrawer.xStart, sFaultDrawer.yStart);

    
    FaultDrawer_SetCharPad(-2, 4);
    FaultDrawer_SetCursor(22, 20);
    sFaultDrawer.xStart = 6;
    //sFaultDrawer.yStart = FAULT_DRAWER_CURSOR_Y;
    sFaultDrawer.xEnd = SCREEN_WIDTH - 6 - 1;
    //sFaultDrawer.yEnd = SCREEN_HEIGHT - FAULT_DRAWER_CURSOR_Y - 1;
    FaultDrawer_SetForeColor(sFaultDrawer.printColors[1]);
    FaultDrawer_Printf("THREAD:%d (%d:%s)\n", thread->id, causeStrIdx, sExceptionNames[causeStrIdx]);
    //FaultDrawer_SetCharPad(-1, 0);
    x -=20;

    s32 line;
    uintptr_t sp = thread->context.sp;
    uintptr_t ra = thread->context.ra;
    uintptr_t pc = thread->context.pc;
    uintptr_t addr;
    const char *functionName = NotFound;
    u8 type;
    u16 actID;
    FaultDrawer_SetForeColor(sFaultDrawer.printColors[3]);
    FaultDrawer_DrawText(x, y + 8, "PC       Function                         File");
    FaultDrawer_SetForeColor(sFaultDrawer.printColors[7]);/*
    u32 tableoffset = DmaU32(gDmaDataTable[FUNCTIONNAMES_DMAID].romStart);
    u32 test1 = DmaU32(gDmaDataTable[FUNCTIONNAMES_DMAID].romStart + tableoffset);
    u32 test2 = DmaU32(gDmaDataTable[FUNCTIONNAMES_DMAID].romStart + tableoffset + 4);
    sprintf(Test, "%08x %08x %08x",tableoffset, test1,test2);*/

    for (line = 2; line < height && (ra != 0 || sp != 0) && pc != (uintptr_t)__osCleanupThread; line++) {
        functionName = NotFound;
        char tmp[32];
        char tmp2[16];
        for(u32 i = 0; i < 2; i++)
        {
            if (pc >= gKaleidoMgrOverlayTable[i].loadedRamAddr && pc <= gKaleidoMgrOverlayTable[i].loadedRamAddr + (gKaleidoMgrOverlayTable[i].vramEnd - gKaleidoMgrOverlayTable[i].vramStart))
            {
                u32 add = (u32)pc - (u32)gKaleidoMgrOverlayTable[i].loadedRamAddr;
                type = i == 1 ? 1 : 2;

                if (SearchForName(add,type,tmp))
                {
                    functionName = tmp;
                }
                else
                {
                    sprintf(tmp, "+0x%05x",add);
                }
                break;
            }
        }
        if (functionName == NotFound)
            for(u32 i = 0; i < FUNCTIONNAMES_MAXACTORS; i++)
            {
                //if (((u32)gActorOverlayTable[i].loadedRamAddr & 0xF0000000) != 0x80000000) break;
                if (pc >= gActorOverlayTable[i].loadedRamAddr && pc <= gActorOverlayTable[i].loadedRamAddr + (gActorOverlayTable[i].vramEnd - gActorOverlayTable[i].vramStart))
                {
                    u32 add = (u32)pc - (u32)gActorOverlayTable[i].loadedRamAddr;
                    type = i + 3;
                    u32 tableoffset = DmaU32(gDmaDataTable[FUNCTIONNAMES_DMAID].romStart + (type * 4));
                    if (tableoffset != 0 && tableoffset != 0xFFFFFFFF && SearchForName(add,type,tmp))
                    {
                        functionName = tmp;
                    }
                    else
                    {
                        if (gActorOverlayTable[i].name == NULL)
                            sprintf(tmp, "%s+0x%05x", gActorOverlayTable[i].name, add);
                        else
                            sprintf(tmp, "+0x%05x", add);
                        functionName = tmp;
                    }
                    actID = i;
                    break;
                }
            }

        if (functionName == NotFound)
        {
            if (SearchForName(pc,0,tmp))
            {
                functionName = tmp;
            }
            else
                functionName = UnknownFunction;
            type = pc > 0x80700000 ? 3 : 0;
        }

       // FaultDrawer_DrawText(x, y + line * 8, "%08x %08x %s", sp, pc, functionName);
        //FaultDrawer_SetForeColor( sFaultDrawer.printColors[(line % 2) ? 7 : 9]);
        FaultDrawer_DrawText(x, y + line * 8, "%08x %s", pc, functionName);
        if (type < 4)
        {
            FaultDrawer_DrawText(x, y + line * 8, "                                          %s", Types[type]);
        }
        else
        {
            sprintf(tmp2, "Act%04x", actID);
            FaultDrawer_DrawText(x, y + line * 8, "                                          %s", tmp2);
        }
        addr = Fault_ConvertAddress(pc);
        if (addr != 0) {
            FaultDrawer_Printf(" -> %08x", addr);
        }
        Fault_WalkStack(&sp, &pc, &ra);
    }

    sFaultDrawer.xStart = FAULT_DRAWER_CURSOR_X;
    //sFaultDrawer.yStart = FAULT_DRAWER_CURSOR_Y;
    sFaultDrawer.xEnd = SCREEN_WIDTH - FAULT_DRAWER_CURSOR_X - 1;
    //sFaultDrawer.yEnd = SCREEN_HEIGHT - FAULT_DRAWER_CURSOR_Y - 1;
}
