#include <z64hdr.h>

#define enum8(x)  u8
#define enum16(x) u16

typedef enum {
    FLAG_TYPE_ROOMCLEAR = 0,
    FLAG_TYPE_TREASURE,
    FLAG_TYPE_USCENE,
    FLAG_TYPE_TEMP,
    FLAG_TYPE_SCENECOLLECT,
    FLAG_TYPE_SWITCH,
    FLAG_TYPE_EVENTCHKINF,
    FLAG_TYPE_INFTABLE,
    FLAG_TYPE_IS_NIGHT
    
    /* NOTE: for the following types, flag must be 4-byte-aligned */
    
    , FLAG_TYPE_SAVE,          /* flag = word(  save_ctx[flag]) & and */
    FLAG_TYPE_GLOBAL,          /* flag = word(global_ctx[flag]) & and */
    FLAG_TYPE_RAM              /* flag = word(flag) & and */
} FlagType;

typedef struct {
    u32 flag;                  /* flag or offset    */
    u32 and;                   /* bit selection     */
    enum8(FlagType)  type; /* flag type         */
    u8  eq;                    /* if (flag() == eq) */
    u16 xfade;                 /* crossfade (color) */
    u16 freeze;                /* tells if command should be freeze or not written*/
    u16 frames;                /* frames flag is on */
} Flag;

#define FreezeFlag_Freeze 1
#define FreezeFlag_StopAtEnd 2

/* data processed by SceneAnim_Pointer_Flag functions */
typedef struct {
    u32  ptr[2];               /* pointer pointers  */
    Flag flag;                 /* flag structure    */
} PointerFlag;

/* data processed by SceneAnim_TexScroll_One functions */
typedef struct {
    s8 u;                      /* u speed           */
    s8 v;                      /* v speed           */
    u8 w;                      /* texture w         */
    u8 h;                      /* texture h         */
}TexScroll;

/* data processed by SceneAnim_TexScroll_Flag functions */
typedef struct  {
    TexScroll sc[2];           /* SceneAnim_TexScroll_One contents   */
    Flag      flag;
} TexScrollFlag;

typedef struct  {
    Flag flag;
    u8 polytypeID[2];
    u8 polytypeVFlags[2];
    u16 maxTimer;
    u16 curTimer;
    u32 polytype1Data[2];   
    u32 polytype2Data[2];         
    u16 triangleIDs[];

} PolytypeSwap;

typedef enum  {
    COLORKEY_PRIM     = 1 << 0,
    COLORKEY_ENV      = 1 << 1,
    COLORKEY_LODFRAC  = 1 << 2,
    COLORKEY_MINLEVEL = 1 << 3
} ColorKeyTypes;

typedef enum  {
    EASE_LINEAR = 0,
    EASE_SIN_IN,
    EASE_SIN_OUT
} EaseMethod;

/* substructure used to describe color keyframe */
typedef struct {
    u32 prim;                  /* primcolor  (rgba) */
    u32 env;                   /* envcolor   (rgba) */
    u8  lfrac;                 /* lodfrac    (prim) */
    u8  mlevel;                /* minlevel   (prim) */
    u16 next;                  /* frames til next; 0 = last frame */
} ColorKey;

/* data processed by color functions */
typedef struct {
    enum8(colorkey) which; /* units to compute  */
    enum8(EaseMethod) ease; /* ease function     */
    u16      dur;              /* duration          */
    ColorKey key[1];           /* keyframe storage  */
} ColorList;

typedef struct {
    Flag      flag;            /* flag structure    */
    ColorList list;            /* color structure   */
} ColorListFlag;

typedef struct {
    u16 dur;                   /* duration of full cycle (frames) */
    u16 time;                  /* frames elapsed (internal use)   */
    u16 each;                  /* frames to display each item     */
    u16 pad;                   /* unused; padding    */
    u32 ptr[1];                /* list: dur/each elements long    */
} PointerLoop;

typedef struct {
    Flag flag;                 /* flag structure */
    PointerLoop list;          /* list structure */
} PointerLoopFlag;

/* each frame can have its own time */
typedef struct {
    u16 prev;                  /* item selected (previous frame)  */
    u16 time;                  /* frames elapsed (internal use)   */
    u16 num;                   /* number of pointers in list      */
    u16 each[1];               /* first frame of each pointer     */
//	u32          ptr[1]; /* intentionally disabled, see note*/
    
    /* NOTE: each[1] can be any length; now imagine immediately  *
    *       after it, there is a ptr[1], containing one pointer *
    *       for each item; ptr[1] must be aligned by 4 bytes;   *
    *       so to get a pointer to it, do the following:        *
    *       u32 *ptr = (void*)(each + num + !(num & 1));   *
    *       see the functions that process this structure if    *
    *       you're having trouble                               */
    
    /* NOTE: each[] contains num items (the last indicating the  *
    *       end frame), and ptr[] contains num-1 items          */
} PointerTimeloop;

typedef struct {
    Flag flag;                 /* flag structure */
    PointerTimeloop list;      /* list structure */
} PointerTimeloopFlag;

/* animation settings; pointed to by 0x1A scene header command */
typedef struct {
    s8 seg;                    /* ram segment       */
    struct {
        u8 processFlag : 1;
        u8 nseg        : 7;
    };
    u16   type;                /* function          */
    void* data;                /* data              */
} AnimInfo;

// scene overlay, for zovl's inside scenes
struct SceneOvl
{
    // offsets are relative to the start of scene file in ram
    u32       start;           // start of overlay
    u32       end;             // end of overlay
    union
    {
        u32   main;            // entry point within overlay
        void  (*exec)(void*);  // main routine after relocating
    } u;
};

typedef struct {
    Flag flag;                 /* flag structure    */
    u8   cameratype;           /* camera type  */
    u8   set;                  /* used ingame to tell that the camera is set  */
} CameraEffect;

typedef struct {
    Flag flag;                 /* flag structure    */
} DrawCondition;

typedef struct {
    AnimInfo* animInfoList;
    /* last generated color key */
    ColorKey Pcolorkey;
    int      magic;
} SceneAnimContext;