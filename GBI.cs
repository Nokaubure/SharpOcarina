using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpOcarina
{
    public static class GBI
    {
        public const int BOWTIE_VAL = 0;
        
        /* Commands */
        public const int G_NOOP = 0x00;
        public const int G_RDPHALF_2 = 0xf1;
        public const int G_SETOTHERMODE_H = 0xe3;
        public const int G_SETOTHERMODE_L = 0xe2;
        public const int G_RDPHALF_1 = 0xe1;
        public const int G_SPNOOP = 0xe0;
        public const int G_ENDDL = 0xdf;
        public const int G_DL = 0xde;
        public const int G_LOAD_UCODE = 0xdd;
        public const int G_MOVEMEM = 0xdc;
        public const int G_MOVEWORD = 0xdb;
        public const int G_MTX = 0xda;
        public const int G_GEOMETRYMODE = 0xd9;
        public const int G_POPMTX = 0xd8;
        public const int G_TEXTURE = 0xd7;
        public const int G_DMA_IO = 0xd6;
        public const int G_SPECIAL_1 = 0xd5;
        public const int G_SPECIAL_2 = 0xd4;
        public const int G_SPECIAL_3 = 0xd3;

        public const int G_VTX = 0x01;
        public const int G_MODIFYVTX = 0x02;
        public const int G_CULLDL = 0x03;
        public const int G_BRANCH_Z = 0x04;
        public const int G_TRI1 = 0x05;
        public const int G_TRI2 = 0x06;
        public const int G_QUAD = 0x07;
        public const int G_LINE3D = 0x08;

        public const int G_SETCIMG = 0xff;
        public const int G_SETZIMG = 0xfe;
        public const int G_SETTIMG = 0xfd;
        public const int G_SETCOMBINE = 0xfc;
        public const int G_SETENVCOLOR = 0xfb;
        public const int G_SETPRIMCOLOR = 0xfa;
        public const int G_SETBLENDCOLOR = 0xf9;
        public const int G_SETFOGCOLOR = 0xf8;
        public const int G_SETFILLCOLOR = 0xf7;
        public const int G_FILLRECT = 0xf6;
        public const int G_SETTILE = 0xf5;
        public const int G_LOADTILE = 0xf4;
        public const int G_LOADBLOCK = 0xf3;
        public const int G_SETTILESIZE = 0xf2;
        public const int G_LOADTLUT = 0xf0;
        public const int G_RDPSETOTHERMODE = 0xef;
        public const int G_SETPRIMDEPTH = 0xee;
        public const int G_SETSCISSOR = 0xed;
        public const int G_SETCONVERT = 0xec;
        public const int G_SETKEYR = 0xeb;
        public const int G_SETKEYGB = 0xea;
        public const int G_RDPFULLSYNC = 0xe9;
        public const int G_RDPTILESYNC = 0xe8;
        public const int G_RDPPIPESYNC = 0xe7;
        public const int G_RDPLOADSYNC = 0xe6;
        public const int G_TEXRECTFLIP = 0xe5;
        public const int G_TEXRECT = 0xe4;

        /* General */
        public const int G_ON = 1;
        public const int G_OFF = 0;

        /* GeometryMode flags */
        public const int G_ZBUFFER = 0x00000001;
        public const int G_SHADE = 0x00000004;
        public const int G_TEXTURE_ENABLE = 0x00000000;
        public const int G_SHADING_SMOOTH = 0x00200000;
        public const int G_CULL_FRONT = 0x00000200;
        public const int G_CULL_BACK = 0x00000400;
        public const int G_CULL_BOTH = 0x00000600;
        public const int G_FOG = 0x00010000;
        public const int G_LIGHTING = 0x00020000;
        public const int G_TEXTURE_GEN = 0x00040000;
        public const int G_TEXTURE_GEN_LINEAR = 0x00080000;
        public const int G_LOD = 0x00100000;

        /* Texture flags */
        public const int G_TX_LOADTILE = 7;
        public const int G_TX_RENDERTILE = 0;

        public const int G_TX_NOMIRROR = 0;
        public const int G_TX_WRAP = 0;
        public const int G_TX_MIRROR = 0x1;
        public const int G_TX_CLAMP = 0x2;
        public const int G_TX_NOMASK = 0;
        public const int G_TX_NOLOD = 0;

        /* Coordinate shift values, number of bits of fraction */
        public const int G_TEXTURE_IMAGE_FRAC = 2;
        public const int G_TEXTURE_SCALE_FRAC = 16;
        public const int G_SCALE_FRAC = 8;
        public const int G_ROTATE_FRAC = 16;

        /* G_SETIMG fmt: set image format */
        public const int G_IM_FMT_RGBA = 0;
        public const int G_IM_FMT_YUV = 1;
        public const int G_IM_FMT_CI = 2;
        public const int G_IM_FMT_IA = 3;
        public const int G_IM_FMT_I = 4;

        /* G_SETIMG siz: set image pixel size */
        public const int G_IM_SIZ_4b = 0;
        public const int G_IM_SIZ_8b = 1;
        public const int G_IM_SIZ_16b = 2;
        public const int G_IM_SIZ_32b = 3;


        //color
        public const int G_CCMUX_COMBINED = 0;
        public const int G_CCMUX_TEXEL0 = 1;
        public const int G_CCMUX_TEXEL1 = 2;
        public const int G_CCMUX_PRIMITIVE = 3;
        public const int G_CCMUX_SHADE = 4;
        public const int G_CCMUX_ENVIRONMENT = 5;
        public const int G_CCMUX_1 = 6;
        public const int G_CCMUX_NOISE = 7;
        public const int G_CCMUX_0 = 7;
        public const int G_CCMUX_CENTER = 6;
        public const int G_CCMUX_K4 = 7;
        public const int G_CCMUX_SCALE = 6;
        public const int G_CCMUX_COMBINED_ALPHA = 7;
        public const int G_CCMUX_TEXEL0_ALPHA = 1;
        public const int G_CCMUX_TEXEL1_ALPHA = 1;
        public const int G_CCMUX_PRIMITIVE_ALPHA = 1;
        public const int G_CCMUX_SHADE_ALPHA = 1;
        public const int G_CCMUX_ENV_ALPHA = 1;
        public const int G_CCMUX_LOD_FRACTION = 1;
        public const int G_CCMUX_PRIM_LOD_FRAC = 1;
        public const int G_CCMUX_K5 = 1;

        //alpha
        public const int G_ACMUX_COMBINED = 1;
        public const int G_ACMUX_TEXEL0 = 1;
        public const int G_ACMUX_TEXEL1 = 1;
        public const int G_ACMUX_PRIMITIVE = 1;
        public const int G_ACMUX_SHADE = 1;
        public const int G_ACMUX_ENVIRONMENT = 1;
        public const int G_ACMUX_1 = 1;
        public const int G_ACMUX_0 = 1;
        public const int G_ACMUX_LOD_FRACTION = 1;
        public const int G_ACMUX_PRIM_LOD_FRAC = 1;

        public static class G_IM_BYTES
        {
            static Dictionary<int, int> Data = new Dictionary<int, int>()
            {
                { G_IM_SIZ_4b, 0 },
                { G_IM_SIZ_8b, 1 },
                { G_IM_SIZ_16b, 2 },
                { G_IM_SIZ_32b, 4 },
            };

            public static int Get(int Key)
            {
                return Data[Key];
            }
        }

        public static class G_IM_LINE_BYTES
        {
            static Dictionary<int, int> Data = new Dictionary<int, int>()
            {
                { G_IM_SIZ_4b, 0 },
                { G_IM_SIZ_8b, 1 },
                { G_IM_SIZ_16b, 2 },
                { G_IM_SIZ_32b, 2 }
            };

            public static int Get(int Key)
            {
                return Data[Key];
            }
        }

        public static class G_IM_TILE_BYTES
        {
            static Dictionary<int, int> Data = new Dictionary<int, int>()
            {
                { G_IM_SIZ_4b, 0 },
                { G_IM_SIZ_8b, 1 },
                { G_IM_SIZ_16b, 2 },
                { G_IM_SIZ_32b, 2 }
            };

            public static int Get(int Key)
            {
                return Data[Key];
            }
        }

        public static class G_IM_LOAD_BLOCK
        {
            static Dictionary<int, int> Data = new Dictionary<int, int>()
            {
                { G_IM_SIZ_4b, G_IM_SIZ_16b },
                { G_IM_SIZ_8b, G_IM_SIZ_16b },
                { G_IM_SIZ_16b, G_IM_SIZ_16b },
                { G_IM_SIZ_32b, G_IM_SIZ_32b }
            };

            public static int Get(int Key)
            {
                return Data[Key];
            }
        }

        public static class G_IM_SHIFT
        {
            static Dictionary<int, int> Data = new Dictionary<int, int>()
            {
                { G_IM_SIZ_4b, 2 },
                { G_IM_SIZ_8b, 1 },
                { G_IM_SIZ_16b, 0 },
                { G_IM_SIZ_32b, 0 }
            };

            public static int Get(int Key)
            {
                return Data[Key];
            }
        };

        public static class G_IM_INCR
        {
            static Dictionary<int, int> Data = new Dictionary<int, int>()
            {
                { G_IM_SIZ_4b, 3 },
                { G_IM_SIZ_8b, 1 },
                { G_IM_SIZ_16b, 0 },
                { G_IM_SIZ_32b, 0 }
            };

            public static int Get(int Key)
            {
                return Data[Key];
            }
        };

        /* G_SETOTHERMODE_L sft: shift count */
        public const int G_MDSFT_ALPHACOMPARE = 0;
        public const int G_MDSFT_ZSRCSEL = 2;
        public const int G_MDSFT_RENDERMODE = 3;
        public const int G_MDSFT_BLENDER = 16;

        /* G_SETOTHERMODE_H sft: shift count */
        public const int G_MDSFT_BLENDMASK = 0;	/* unsupported */
        public const int G_MDSFT_ALPHADITHER = 4;
        public const int G_MDSFT_RGBDITHER = 6;

        public const int G_MDSFT_COMBKEY = 8;
        public const int G_MDSFT_TEXTCONV = 9;
        public const int G_MDSFT_TEXTFILT = 12;
        public const int G_MDSFT_TEXTLUT = 14;
        public const int G_MDSFT_TEXTLOD = 16;
        public const int G_MDSFT_TEXTDETAIL = 17;
        public const int G_MDSFT_TEXTPERSP = 19;
        public const int G_MDSFT_CYCLETYPE = 20;
        public const int G_MDSFT_COLORDITHER = 22;	/* unsupported in HW 2.0 */
        public const int G_MDSFT_PIPELINE = 23;

        /* G_SETOTHERMODE_H gPipelineMode */
        public const int G_PM_1PRIMITIVE = (1 << G_MDSFT_PIPELINE);
        public const int G_PM_NPRIMITIVE = (0 << G_MDSFT_PIPELINE);

        /* G_SETOTHERMODE_H gSetCycleType */
        public const int G_CYC_1CYCLE = (0 << G_MDSFT_CYCLETYPE);
        public const int G_CYC_2CYCLE = (1 << G_MDSFT_CYCLETYPE);
        public const int G_CYC_COPY = (2 << G_MDSFT_CYCLETYPE);
        public const int G_CYC_FILL = (3 << G_MDSFT_CYCLETYPE);

        /* G_SETOTHERMODE_H gSetTexturePersp */
        public const int G_TP_NONE = (0 << G_MDSFT_TEXTPERSP);
        public const int G_TP_PERSP = (1 << G_MDSFT_TEXTPERSP);

        /* G_SETOTHERMODE_H gSetTextureDetail */
        public const int G_TD_CLAMP = (0 << G_MDSFT_TEXTDETAIL);
        public const int G_TD_SHARPEN = (1 << G_MDSFT_TEXTDETAIL);
        public const int G_TD_DETAIL = (2 << G_MDSFT_TEXTDETAIL);

        /* G_SETOTHERMODE_H gSetTextureLOD */
        public const int G_TL_TILE = (0 << G_MDSFT_TEXTLOD);
        public const int G_TL_LOD = (1 << G_MDSFT_TEXTLOD);

        /* G_SETOTHERMODE_H gSetTextureLUT */
        public const int G_TT_NONE = (0 << G_MDSFT_TEXTLUT);
        public const int G_TT_RGBA16 = (2 << G_MDSFT_TEXTLUT);
        public const int G_TT_IA16 = (3 << G_MDSFT_TEXTLUT);

        /* G_SETOTHERMODE_H gSetTextureFilter */
        public const int G_TF_POINT = (0 << G_MDSFT_TEXTFILT);
        public const int G_TF_AVERAGE = (3 << G_MDSFT_TEXTFILT);
        public const int G_TF_BILERP = (2 << G_MDSFT_TEXTFILT);

        /* G_SETOTHERMODE_H gSetTextureConvert */
        public const int G_TC_CONV = (0 << G_MDSFT_TEXTCONV);
        public const int G_TC_FILTCONV = (5 << G_MDSFT_TEXTCONV);
        public const int G_TC_FILT = (6 << G_MDSFT_TEXTCONV);

        /* G_SETOTHERMODE_H gSetCombineKey */
        public const int G_CK_NONE = (0 << G_MDSFT_COMBKEY);
        public const int G_CK_KEY = (1 << G_MDSFT_COMBKEY);

        /* G_SETOTHERMODE_H gSetColorDither */
        public const int G_CD_MAGICSQ = (0 << G_MDSFT_RGBDITHER);
        public const int G_CD_BAYER = (1 << G_MDSFT_RGBDITHER);
        public const int G_CD_NOISE = (2 << G_MDSFT_RGBDITHER);

        public const int G_CD_ENABLE = (1 << G_MDSFT_COLORDITHER);
        public const int G_CD_DISABLE = (0 << G_MDSFT_COLORDITHER);

        /* G_SETOTHERMODE_H gSetAlphaDither */
        public const int G_AD_PATTERN = (0 << G_MDSFT_ALPHADITHER);
        public const int G_AD_NOTPATTERN = (1 << G_MDSFT_ALPHADITHER);
        public const int G_AD_NOISE = (2 << G_MDSFT_ALPHADITHER);
        public const int G_AD_DISABLE = (3 << G_MDSFT_ALPHADITHER);

        /* G_SETOTHERMODE_L gSetAlphaCompare */
        public const int G_AC_NONE = (0 << G_MDSFT_ALPHACOMPARE);
        public const int G_AC_THRESHOLD = (1 << G_MDSFT_ALPHACOMPARE);
        public const int G_AC_DITHER = (3 << G_MDSFT_ALPHACOMPARE);

        /* G_SETOTHERMODE_L gSetDepthSource */
        public const int G_ZS_PIXEL = (0 << G_MDSFT_ZSRCSEL);
        public const int G_ZS_PRIM = (1 << G_MDSFT_ZSRCSEL);

        /* G_SETOTHERMODE_L gSetRenderMode */
        public const int AA_EN = 0x8;
        public const int Z_CMP = 0x10;
        public const int Z_UPD = 0x20;
        public const int IM_RD = 0x40;
        public const int CLR_ON_CVG = 0x80;
        public const int CVG_DST_CLAMP = 0;
        public const int CVG_DST_WRAP = 0x100;
        public const int CVG_DST_FULL = 0x200;
        public const int CVG_DST_SAVE = 0x300;
        public const int ZMODE_OPA = 0;
        public const int ZMODE_INTER = 0x400;
        public const int ZMODE_XLU = 0x800;
        public const int ZMODE_DEC = 0xc00;
        public const int CVG_X_ALPHA = 0x1000;
        public const int ALPHA_CVG_SEL = 0x2000;
        public const int FORCE_BL = 0x4000;
        public const int TEX_EDGE = 0x0000; /* used to be 0x8000 */

        public const int G_TX_DXT_FRAC = 11;
        public const int G_TX_LDBLK_MAX_TXL = 2047;

        public static int TXL2WORDS(int Txls, int B_Txl)
        {
            return Math.Max(1, ((Txls) * (B_Txl) / 8));
        }

        public static int CALC_DXT(int Width, int B_Txl)
        {
            return (((1 << G_TX_DXT_FRAC) + TXL2WORDS(Width, B_Txl) - 1) / TXL2WORDS(Width, B_Txl));
        }
        
        public static int TXL2WORDS_4b(int Txls)
        {
            return Math.Max(1, ((Txls) / 16));
        }

        public static int CALC_DXT_4b(int Width)
        {
            return (((1 << G_TX_DXT_FRAC) + TXL2WORDS_4b(Width) - 1) / TXL2WORDS_4b(Width));
        }
    }
}
