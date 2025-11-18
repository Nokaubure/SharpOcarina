using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

namespace SharpOcarina
{
    public class NDisplayList
    {
        #region Variables/Constructor/etc.

        public byte[] Data;
        public uint Offset = 0;

        public float Scale;
        public uint TintAlpha;
        public uint MultitextureAlpha;
        public float TexScale;
        public bool OutdoorLight;
        public bool Culling;
        public bool Animated; 
        public bool Metallic;
        public bool EnvColor;
        public bool Decal;
        public bool IgnoreFog;
        public bool SmoothRGBAEdges;
        public int Bank;
        public int Vertoffset;
        public bool Pixelated;
        public bool Billboard;
        public bool TwoAxisBillboard;
        public int AnimationBank;
        public bool AlphaMask;
        public bool VertexNormals;
        public bool RenderLast;
        public bool PointLight;
        public bool ScaledNormals;
        public bool TexPointerPlus1;

        public short blackvertexypos;

        public Vector3d MinCoordinate = new Vector3d(32766, 32766, 32766);
        public Vector3d MaxCoordinate = new Vector3d(-32766, -32766, -32766);


        public const int C_COMBINED = 0;
        public const int C_TEXEL0 = 1;
        public const int C_TEXEL1 = 2;
        public const int C_PRIMITIVE = 3;
        public const int C_SHADE = 4;
        public const int C_ENVIRONMENT = 5;
        public const int C_CENTER = 6;
        public const int C_SCALE = 6;
        public const int C_COMBINED_ALPHA = 7;
        public const int C_TEXEL0_ALPHA = 8;
        public const int C_TEXEL1_ALPHA = 9;
        public const int C_PRIMITIVE_ALPHA = 10;
        public const int C_SHADE_ALPHA = 11;
        public const int C_ENV_ALPHA = 12;
        public const int C_LOD_FRACTION = 13;
        public const int C_PRIM_LOD_FRAC = 14;
        public const int C_NOISE = 7;
        public const int C_K4 = 7;
        public const int C_K5 = 15;
        public const int C_1 = 6;
        public const int C_0 = 31;
        public const int A_COMBINED = 0;
        public const int A_TEXEL0 = 1;
        public const int A_TEXEL1 = 2;
        public const int A_PRIMITIVE = 3;
        public const int A_SHADE = 4;
        public const int A_ENVIRONMENT = 5;
        public const int A_LOD_FRACTION = 0;
        public const int A_PRIM_LOD_FRAC = 6;
        public const int A_1 = 6;
        public const int A_0 = 7;
        


        public class NVertex
        {
            public Vector3d Position;
            public Vector2d TexCoord;
            public Color4 Colors;
            public Vector3d Normals;

            public NVertex(Vector3d _Position, Vector2d _TexCoord, Color4 _Colors, Vector3d _Normals)
            {
                Position = _Position;
                TexCoord = _TexCoord;
                Colors = _Colors;
                Normals = _Normals;
            }
        }

        public struct SurfaceBundle
        {
            public ObjFile.Material Material;
            public List<ObjFile.Triangle> Triangles;
            public bool textureless;
        }

        public NDisplayList()
        {
            Scale = 1.0f;
            TintAlpha = 0xFFFFFFFF;
            TexScale = 1.0f;
            OutdoorLight = false;
            Culling = true;
            Animated = false;
        }

        public NDisplayList(float _Scale, uint _TintAlpha, uint _MultitextureAlpha, float _TexScale, bool outdoorLight, bool _Culling, bool _Animated, bool _Metallic, bool _Decal, bool _Pixelated, bool _Billboard, bool _TwoAxisBillboard, bool _IgnoreFog, bool _SmoothRGBAEdges, bool _EnvColor, bool _AlphaMask, bool _renderLast, bool _vertexNormals, bool _PointLight, bool _scaledNormals, bool _texPointerPlus1, int _AnimationBank,  int _bank = 0x03)
        {
            Scale = _Scale;
            TintAlpha = _TintAlpha;
            MultitextureAlpha = _MultitextureAlpha;
            TexScale = _TexScale;
            OutdoorLight = outdoorLight;
            Culling = _Culling;
            Animated = _Animated;
            Metallic = _Metallic;
            EnvColor = _EnvColor;
            Bank = _bank;
            Decal = _Decal;
            Pixelated = _Pixelated;
            Billboard = _Billboard;
            TwoAxisBillboard = _TwoAxisBillboard;
            AnimationBank = _AnimationBank;
            IgnoreFog = _IgnoreFog;
            SmoothRGBAEdges = _SmoothRGBAEdges;
            AlphaMask = _AlphaMask;
            RenderLast = _renderLast;
            VertexNormals = _vertexNormals;
            PointLight = _PointLight;
            ScaledNormals = _scaledNormals;
            TexPointerPlus1 = _texPointerPlus1;
        }

        #endregion

        #region General Macros

        private ulong NoParam(byte Cmd)
        {
            return ((ulong)Helpers.ShiftL(Cmd, 24, 8) << 32) | 0;
        }

        private ulong Param(byte Cmd, uint Param)
        {
            return ((ulong)Helpers.ShiftL(Cmd, 24, 8) << 32) | Param;
        }

        private ulong FullSync()
        {
            return NoParam(GBI.G_RDPFULLSYNC);
        }

        private ulong TileSync()
        {
            return NoParam(GBI.G_RDPTILESYNC);
        }

        private ulong PipeSync()
        {
            return NoParam(GBI.G_RDPPIPESYNC);
        }

        private ulong LoadSync()
        {
            return NoParam(GBI.G_RDPLOADSYNC);
        }

        private ulong NoOp()
        {
            return NoParam(GBI.G_NOOP);
        }

        #endregion

        #region Mode Macros

        private ulong SetRenderMode(uint C0, uint C1)
        {
            return SetOtherMode(GBI.G_SETOTHERMODE_L, GBI.G_MDSFT_RENDERMODE, 29, (C0) | (C1));
        }

        private ulong SetOtherMode(byte Cmd, uint Sft, uint Len_, ulong Data)
        {
            return ((ulong)(Helpers.ShiftL(Cmd, 24, 8) | Helpers.ShiftL((uint)(32 - (Sft) - (Len_)), 8, 8) | Helpers.ShiftL((uint)((Len_) - 1), 0, 8)) << 32) | (Data & 0xFFFFFFFF);
        }

        private ulong GeometryMode(int Clear, int Set)
        {
            return ((ulong)(Helpers.ShiftL(GBI.G_GEOMETRYMODE, 24, 8) | Helpers.ShiftL((~(uint)Clear) & 0xFFFFFFFF, 0, 24)) << 32) | ((uint)Set & 0xFFFFFFFF);
        }

        private ulong SetGeometryMode(int Mode)
        {
            return GeometryMode(0, Mode);
        }

        private ulong ClearGeometryMode(int Mode)
        {
            return GeometryMode(Mode, 0);
        }

        private ulong LoadGeometryMode(int Mode)
        {
            return GeometryMode(-1, Mode);
        }

        #endregion

        #region Texture & Palette Macros

        private bool IsI4orI8(NTexture texture)
        {
            return (texture.Format == GBI.G_IM_FMT_I);
        }

        private ulong SetTextureLUT(uint Type_)
        {
            return SetOtherMode(GBI.G_SETOTHERMODE_H, GBI.G_MDSFT_TEXTLUT, 2, Type_);
        }

        private ulong SetTextureLOD(uint Type_)
        {
            return SetOtherMode(GBI.G_SETOTHERMODE_H, GBI.G_MDSFT_TEXTLOD, 1, Type_);
        }

        private ulong SetTextureFilt(uint Type_)
        {
            return SetOtherMode(GBI.G_SETOTHERMODE_H, GBI.G_MDSFT_TEXTFILT, 2, Type_);
        }

        private ulong Texture(int S, int T, uint Level, uint Tile, uint On)
        {
            return ((ulong)(Helpers.ShiftL(GBI.G_TEXTURE, 24, 8) | Helpers.ShiftL(GBI.BOWTIE_VAL, 16, 8) | Helpers.ShiftL((Level), 11, 3) | Helpers.ShiftL((Tile), 8, 3) | Helpers.ShiftL((On), 1, 7)) << 32) | (Helpers.ShiftL((uint)(S), 16, 16) | Helpers.ShiftL((uint)(T), 0, 16));
        }

        private ulong SetImage(byte Cmd, int Fmt, int Siz, uint Width, uint I)
        {
            return ((ulong)(Helpers.ShiftL(Cmd, 24, 8) | Helpers.ShiftL((uint)Fmt, 21, 3) | Helpers.ShiftL((uint)Siz, 19, 2) | Helpers.ShiftL((Width) - 1, 0, 12)) << 32) | (I);
        }

        private ulong SetTextureImage(int F, int S, uint W, uint I)
        {
            return SetImage(GBI.G_SETTIMG, F, S, W, I);
        }

        private ulong SetTile(int Fmt, int Siz, int Line, int TMEM, int Tile, int Palette, uint CMT, uint MaskT, uint ShiftT, uint CMS, uint MaskS, uint ShiftS)
        {
            return (
                ((ulong)(Helpers.ShiftL(GBI.G_SETTILE, 24, 8) | Helpers.ShiftL((uint)Fmt, 21, 3) | Helpers.ShiftL((uint)Siz, 19, 2) |
                Helpers.ShiftL((uint)Line, 9, 9) | Helpers.ShiftL((uint)TMEM, 0, 9)) << 32) |
                (Helpers.ShiftL((uint)Tile, 24, 3) | Helpers.ShiftL((uint)Palette, 20, 4) | Helpers.ShiftL(CMT, 18, 2) |
                Helpers.ShiftL(MaskT, 14, 4) | Helpers.ShiftL(ShiftT, 10, 4) | Helpers.ShiftL(CMS, 8, 2) |
                Helpers.ShiftL(MaskS, 4, 4) | Helpers.ShiftL(ShiftS, 0, 4)));
        }

        private ulong LoadBlock(int Tile, int ULS, int ULT, int LRS, int DXT)
        {
            return (
                ((ulong)(Helpers.ShiftL(GBI.G_LOADBLOCK, 24, 8) | Helpers.ShiftL((uint)ULS, 12, 12) | Helpers.ShiftL((uint)ULT, 0, 12)) << 32) |
                (Helpers.ShiftL((uint)Tile, 24, 3) | Helpers.ShiftL(((uint)Math.Min(LRS, GBI.G_TX_LDBLK_MAX_TXL)), 12, 12) |
                Helpers.ShiftL((uint)DXT, 0, 12)));
        }

        private ulong LoadTileGeneric(int C, int Tile, int ULS, int ULT, int LRS, int LRT)
        {
            return (
                ((ulong)(Helpers.ShiftL((uint)C, 24, 8) | Helpers.ShiftL((uint)ULS, 12, 12) | Helpers.ShiftL((uint)ULT, 0, 12)) << 32) |
                Helpers.ShiftL((uint)Tile, 24, 3) | Helpers.ShiftL((uint)LRS, 12, 12) | Helpers.ShiftL((uint)LRT, 0, 12));
        }

        private ulong SetTileSize(int Tile, int ULS, int ULT, int LRS, int LRT)
        {
            return LoadTileGeneric(GBI.G_SETTILESIZE, Tile, ULS, ULT + MainForm.settings.Yoffsetfix, LRS, LRT - MainForm.settings.Yoffsetfix);
        }

        private ulong LoadTile(int Tile, int ULS, int ULT, int LRS, int LRT)
        {
            return LoadTileGeneric(GBI.G_LOADTILE, Tile, ULS, ULT, LRS, LRT);
        }

        private void LoadTextureBlock(ref List<byte> DList, uint TImg, int Fmt, int Siz, uint Width, uint Height, uint Pal, uint CMS, uint CMT, uint MaskS, uint MaskT, uint ShiftS, uint ShiftT)
        {
            LoadMultiBlock(ref DList, TImg, 0, GBI.G_TX_RENDERTILE, Fmt, Siz, Width, Height, Pal, CMS, CMT, MaskS, MaskT, ShiftS, ShiftT);
        }

        private void LoadMultiBlock(ref List<byte> DList, uint TImg, int TMem, int RTile, int Fmt, int Siz, uint Width, uint Height, uint Pal, uint CMS, uint CMT, uint MaskS, uint MaskT, uint ShiftS, uint ShiftT)
        {
            Helpers.Append64(ref DList, SetTextureImage(Fmt, GBI.G_IM_LOAD_BLOCK.Get(Siz), 1, TImg));
            Helpers.Append64(ref DList, SetTile(Fmt, GBI.G_IM_LOAD_BLOCK.Get(Siz), 0, TMem, GBI.G_TX_LOADTILE,
                0, CMT, MaskT, ShiftT, CMS, MaskS, ShiftS));
            Helpers.Append64(ref DList, LoadSync());
            Helpers.Append64(ref DList, LoadBlock(GBI.G_TX_LOADTILE, 0, 0,
                (int)((((Width * Height) + GBI.G_IM_INCR.Get(Siz)) >> GBI.G_IM_SHIFT.Get(Siz)) - 1),
                GBI.CALC_DXT((int)Width, GBI.G_IM_BYTES.Get(Siz))));
            Helpers.Append64(ref DList, PipeSync());
            Helpers.Append64(ref DList, SetTile(Fmt, Siz,
                (int)((((Width) * GBI.G_IM_LINE_BYTES.Get(Siz)) + 7) >> 3), TMem,
                RTile, (int)Pal, CMT, MaskT, ShiftT, CMS, MaskS, ShiftS));
            Helpers.Append64(ref DList, SetTileSize(RTile, 0, 0,
                (int)((Width - 1) << GBI.G_TEXTURE_IMAGE_FRAC),
                (int)((Height - 1) << GBI.G_TEXTURE_IMAGE_FRAC)));
           // Console.WriteLine((SetTileSize(RTile, 0, 0,(int)((Width - 1) << GBI.G_TEXTURE_IMAGE_FRAC),(int)((Height - 1) << GBI.G_TEXTURE_IMAGE_FRAC))).ToString("X16"));
        }

        private void LoadTextureBlock_4b(ref List<byte> DList, uint TImg, int Fmt, uint Width, uint Height, uint Pal, uint CMS, uint CMT, uint MaskS, uint MaskT, uint ShiftS, uint ShiftT)
        {
            LoadMultiBlock_4b(ref DList, TImg, 0, GBI.G_TX_RENDERTILE, Fmt, Width, Height, Pal, CMS, CMT, MaskS, MaskT, ShiftS, ShiftT);
        }

        private void LoadMultiBlock_4b(ref List<byte> DList, uint TImg, int TMem, int RTile, int Fmt, uint Width, uint Height, uint Pal, uint CMS, uint CMT, uint MaskS, uint MaskT, uint ShiftS, uint ShiftT)
        {
            Helpers.Append64(ref DList, SetTextureImage(Fmt, GBI.G_IM_SIZ_16b, 1, TImg));
            Helpers.Append64(ref DList, SetTile(Fmt, GBI.G_IM_SIZ_16b, 0, TMem, GBI.G_TX_LOADTILE,
                0, CMT, MaskT, ShiftT, CMS, MaskS, ShiftS));
            Helpers.Append64(ref DList, LoadSync());
            Helpers.Append64(ref DList, LoadBlock(GBI.G_TX_LOADTILE, 0, 0,
                (int)((((Width * Height) + 3) >> 2) - 1),
                GBI.CALC_DXT_4b((int)Width)));
            Helpers.Append64(ref DList, PipeSync());
            Helpers.Append64(ref DList, SetTile(Fmt, GBI.G_IM_SIZ_4b,
                (int)((((Width) >> 1) + 7) >> 3), TMem,
                RTile, (int)Pal, CMT, MaskT, ShiftT, CMS, MaskS, ShiftS));
            Helpers.Append64(ref DList, SetTileSize(RTile, 0, 0,
                (int)((Width - 1) << GBI.G_TEXTURE_IMAGE_FRAC),
                (int)((Height - 1) << GBI.G_TEXTURE_IMAGE_FRAC)));
        }

        private ulong LoadTLUTCmd(int Tile, int Count)
        {
            return ((ulong)Helpers.ShiftL(GBI.G_LOADTLUT, 24, 8) << 32) | (Helpers.ShiftL((uint)Tile, 24, 3) | Helpers.ShiftL((uint)Count, 14, 10));
        }

        private void LoadTLUT16(ref List<byte> DList, int Pal, uint DRAM)
        {
#if DEBUG
          //  DebugConsole.WriteLine("LoadTLUT16 -> pal: " + Pal.ToString() + ", address: " + DRAM.ToString("X8"));
#endif
            Helpers.Append64(ref DList, SetTextureImage(GBI.G_IM_FMT_RGBA, GBI.G_IM_SIZ_16b, 1, (DRAM & 0xFFFFFFFF)));
            Helpers.Append64(ref DList, TileSync());
            Helpers.Append64(ref DList, SetTile(0, 0, 0, (256 + (((Pal) & 0xF) * 16)), GBI.G_TX_LOADTILE, 0, 0, 0, 0, 0, 0, 0));
            Helpers.Append64(ref DList, LoadSync());
            Helpers.Append64(ref DList, LoadTLUTCmd(GBI.G_TX_LOADTILE, 15));
            Helpers.Append64(ref DList, PipeSync());
        }

        private void LoadTLUT256(ref List<byte> DList, uint DRAM)
        {
#if DEBUG
         //   DebugConsole.WriteLine("LoadTLUT256 -> offset: " + DRAM.ToString("X8"));
#endif
            Helpers.Append64(ref DList, SetTextureImage(GBI.G_IM_FMT_RGBA, GBI.G_IM_SIZ_16b, 1, (DRAM & 0xFFFFFFFF)));
            Helpers.Append64(ref DList, TileSync());
            Helpers.Append64(ref DList, SetTile(0, 0, 0, 256, GBI.G_TX_LOADTILE, 0, 0, 0, 0, 0, 0, 0));
            Helpers.Append64(ref DList, LoadSync());
            Helpers.Append64(ref DList, LoadTLUTCmd(GBI.G_TX_LOADTILE, 255));
            Helpers.Append64(ref DList, PipeSync());
        }

        #endregion

        #region Combiner Macros

        private ulong SetPrimColor(uint ARGB)
        {
            return Param(GBI.G_SETPRIMCOLOR, (uint)(((ARGB >> 16) & 0xFF) << 24 | ((ARGB >> 8) & 0xFF) << 16 | (ARGB & 0xFF) << 8 | (ARGB >> 24)));
        }

        private ulong SetPrimColorNew(uint ARGB, uint primlodfrac)
        {
            //Console.WriteLine((0xFA00000000000000 | ((ulong)primlodfrac << 32) | (uint)(((ARGB >> 16) & 0xFF) << 24 | ((ARGB >> 8) & 0xFF) << 16 | (ARGB & 0xFF) << 8 | (ARGB >> 24))).ToString("X16"));
            return 0xFA00000000000000 | ((ulong)primlodfrac << 32) | (uint)(((ARGB >> 16) & 0xFF) << 24 | ((ARGB >> 8) & 0xFF) << 16 | (ARGB & 0xFF) << 8 | (ARGB >> 24));
           // return Param(GBI.G_SETPRIMCOLOR, (uint)(((ARGB >> 16) & 0xFF) << 24 | ((ARGB >> 8) & 0xFF) << 16 | (ARGB & 0xFF) << 8 | (ARGB >> 24)));
        }

        private ulong SetEnvColor(uint ARGB)
        {
            return Param(GBI.G_SETENVCOLOR, (uint)(((ARGB >> 16) & 0xFF) << 24 | ((ARGB >> 8) & 0xFF) << 16 | (ARGB & 0xFF) << 8 | (ARGB >> 24)));
        }

        private ulong SetPrimColor(byte R, byte G, byte B, byte A)
        {
            return Param(GBI.G_SETPRIMCOLOR, (uint)((R << 24) | (G << 16) | (B << 8) | A));
        }

        private ulong SetCombine(uint MuxS0, uint MuxS1)
        {
            return ((ulong)(Helpers.ShiftL(GBI.G_SETCOMBINE, 24, 8) | Helpers.ShiftL(MuxS0, 0, 24)) << 32) | (MuxS1 & 0xFFFFFFFF);
        }

        private ulong SetCombineNew(ulong C1A, ulong C1B, ulong C1C, ulong C1D, ulong C2A, ulong C2B, ulong C2C, ulong C2D,
                                    ulong A1A, ulong A1B, ulong A1C, ulong A1D, ulong A2A, ulong A2B, ulong A2C, ulong A2D)
        {
            C1A &= 0xF;
            C2A &= 0xF;
            C1B &= 0xF;
            C2B &= 0xF;
            C1D &= 0x7;
            C2D &= 0x7;
            ulong Combiner = (0 | ((C1A << (20 + 32)))
                            | ((C1B << 28))
                            | ((C1C << (15 + 32)))
                            | ((C1D << 15))

                            | ((C2A << (5 + 32)))
                            | ((C2B << 24))
                            | ((C2C << (0 + 32)))
                            | ((C2D << 6))

                            | ((A1A << (12 + 32)))
                            | ((A1B << 12))
                            | ((A1C << (9 + 32)))
                            | ((A1D << 9))

                            | ((A2A << 21))
                            | ((A2B << 3))
                            | ((A2C << 18))
                            | ((A2D << 0)));

            //DebugConsole.WriteLine((0xFC00000000000000 | Combiner).ToString("X16"));

            return 0xFC00000000000000 | Combiner;
        }



        #endregion

        #region Conversion

        public void InsertTextureLoad(ref List<byte> DList, int Width, int Height, NTexture ThisTexture, int TexPal, int RenderTile, int CMS, int CMT, int MultiShiftS, int MultiShiftT, int ShiftS = 0, int ShiftT = 0)
        {
            /* If texture is in CI format, add correct SetTextureLUT and LoadTLUT macro */

            uint tmpbank = (Bank == 0x03) ?  0x02 : (uint)Bank;



            if (ThisTexture.Format == GBI.G_IM_FMT_CI)
            {
                //Helpers.Append64(ref DList, SetTextureLUT(GBI.G_TT_RGBA16)); //TODO roll back?

                

                if (ThisTexture.Size == GBI.G_IM_SIZ_4b)
                    LoadTLUT16(ref DList, TexPal, 0x00000000 | (tmpbank << 24) | ThisTexture.PalOffset);
                else if (ThisTexture.Size == GBI.G_IM_SIZ_8b)
                    LoadTLUT256(ref DList, 0x00000000 | (tmpbank << 24) | ThisTexture.PalOffset);
            }
            else
            {
              //  Helpers.Append64(ref DList, SetTextureLUT(GBI.G_TT_NONE));
            }

            /* Select appropriate load block macro to use (LoadTextureBlock or LoadMultiBlock) */
            if (RenderTile == GBI.G_TX_RENDERTILE)
            {
                /* Select appropriate LoadTextureBlock macro to use (4-bit or standard) */
                if (ThisTexture.Size == GBI.G_IM_SIZ_4b)
                {
                    LoadTextureBlock_4b(ref DList, 0x00000000 | (tmpbank << 24) | ThisTexture.TexOffset,
                        ThisTexture.Format, (uint)Width, (uint)Height, (uint)TexPal,
                        (uint)CMS, (uint)CMT,
                        (uint)Helpers.Log2(Width), (uint)Helpers.Log2(Height),
                        (uint)ShiftS, (uint)ShiftT);
                }
                else
                {
                    LoadTextureBlock(ref DList, 0x00000000 | (tmpbank << 24) | ThisTexture.TexOffset,
                        ThisTexture.Format, ThisTexture.Size, (uint)Width, (uint)Height, (uint)TexPal,
                        (uint)CMS, (uint)CMT,
                        (uint)Helpers.Log2(Width), (uint)Helpers.Log2(Height),
                        (uint)ShiftS, (uint)ShiftT);
                }
            }
            else
            {
                if (ThisTexture.Size == GBI.G_IM_SIZ_4b)
                {
                    LoadMultiBlock_4b(ref DList, 0x00000000 | (tmpbank << 24) | ThisTexture.TexOffset, RenderTile * 256, RenderTile,
                        ThisTexture.Format, (uint)Width, (uint)Height, (uint)TexPal,
                        (uint)CMS, (uint)CMT,
                        (uint)Helpers.Log2(Width), (uint)Helpers.Log2(Height),
                        (uint)MultiShiftS, (uint)MultiShiftT);
                }
                else
                {
                    LoadMultiBlock(ref DList, 0x00000000 | (tmpbank << 24) | ThisTexture.TexOffset, RenderTile * 256, RenderTile,
                        ThisTexture.Format, ThisTexture.Size, (uint)Width, (uint)Height, (uint)TexPal,
                        (uint)CMS, (uint)CMT,
                        (uint)Helpers.Log2(Width), (uint)Helpers.Log2(Height),
                        (uint)MultiShiftS, (uint)MultiShiftT);          // multitex scale HERE!!!
                }
            }
        }

        

        public void Convert(ObjFile Obj, ObjFile.Group Group, List<NTexture> Textures, uint BaseOffset, byte SceneSettings, List<ObjFile.Material> AdditionalTextures)
        {

            /* Create lists, etc. */
            List<byte> DList = new List<byte>();
            List<SurfaceBundle> SurfBundles = new List<SurfaceBundle>();

            List<NVertex> VertList = new List<NVertex>();
            List<byte> VertData = new List<byte>();
            List<byte> VertCull = new List<byte>();
            List<byte> VertMtx = new List<byte>();
            List<byte> CondMtx = new List<byte>();

            bool firsttime = false;
            bool hasalphavertex = false;

            List<ObjFile.Material> MatList = new List<ObjFile.Material>();

            MatList.AddRange(Obj.Materials);

            MatList.AddRange(AdditionalTextures);

            int TexturePointerBank = (TexPointerPlus1 && AnimationBank < 0xF) ? AnimationBank + 1 : AnimationBank;


            foreach (ObjFile.Triangle Tri2 in Group.Triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    // if ((float)Obj.VertexColors[Tri.VertColor[i] - 1].A < 1) DebugConsole.WriteLine("Vertex with alpha!");
                    /* Minimum... */
                    MinCoordinate.X = Math.Min(MinCoordinate.X, Obj.Vertices[Tri2.VertIndex[i]].X * Scale);
                    MinCoordinate.Y = Math.Min(MinCoordinate.Y, Obj.Vertices[Tri2.VertIndex[i]].Y * Scale);
                    MinCoordinate.Z = Math.Min(MinCoordinate.Z, Obj.Vertices[Tri2.VertIndex[i]].Z * Scale);

                    /* Maximum... */
                    MaxCoordinate.X = Math.Max(MaxCoordinate.X, Obj.Vertices[Tri2.VertIndex[i]].X * Scale);
                    MaxCoordinate.Y = Math.Max(MaxCoordinate.Y, Obj.Vertices[Tri2.VertIndex[i]].Y * Scale);
                    MaxCoordinate.Z = Math.Max(MaxCoordinate.Z, Obj.Vertices[Tri2.VertIndex[i]].Z * Scale);

                    if (Obj.VertexColors[Tri2.VertColor[i] - 1].A < 1.0 && (TintAlpha >> 24) != 255) hasalphavertex = true;
                }

                MinCoordinate = new Vector3d(MainForm.Clamp(MinCoordinate.X - 5f,-32767,32767), MainForm.Clamp(MinCoordinate.Y - 5f, -32767, 32767), MainForm.Clamp(MinCoordinate.Z - 5f,-32767,32767));
                MaxCoordinate = new Vector3d(MainForm.Clamp(MaxCoordinate.X + 5f, -32767, 32767), MainForm.Clamp(MaxCoordinate.Y + 5f, -32767, 32767), MainForm.Clamp(MaxCoordinate.Z + 5f, -32767, 32767));

            }
            ushort midX = 0, midY = 0, midZ = 0;
            if ((Billboard || TwoAxisBillboard ))
            {
                if (Group.PivotPoint.X != 32767 && Group.PivotPoint.Y != 32767 && Group.PivotPoint.Z != 32767)
                {
                    midX = (ushort)Group.PivotPoint.X;
                    midY = (ushort)Group.PivotPoint.Y;
                    midZ = (ushort)Group.PivotPoint.Z;
                }
                else
                {
                    midX = (ushort)(MinCoordinate.X + ((MaxCoordinate.X - MinCoordinate.X) / 2));
                    midY = (ushort)((!TwoAxisBillboard) ? (MinCoordinate.Y + ((MaxCoordinate.Y - MinCoordinate.Y) / 2)) : MinCoordinate.Y + 5f);
                    midZ = (ushort)(MinCoordinate.Z + ((MaxCoordinate.Z - MinCoordinate.Z) / 2));
                }
            }

            //textureless material

            if (!MatList.Exists(x => x.Name == "Dummy_textureless"))
            {
                ObjFile.Material nomat = new ObjFile.Material();
                nomat.Name = "Dummy_textureless";
            }

 

            /* Parse all known materials */
            foreach (ObjFile.Material Mat in Obj.Materials)
            {


              // DebugConsole.WriteLine(Mat.map_Kd);

               //DebugConsole.WriteLine("textureless: " + textureless);

                /* Create new surface bundle */
                SurfaceBundle Surf = new SurfaceBundle();

                /* Assign material and create triangle list */
                Surf.Material = Mat;
                Surf.Triangles = new List<ObjFile.Triangle>();

                if (Mat.TexImage == null && Mat.map_Kd == null)
                    Surf.textureless = true;
                else
                    Surf.textureless = false;

                /* Parse triangles and group appropriate tris to bundle */
                foreach (ObjFile.Triangle Tri in Group.Triangles)
                {
                    /* If tri's material name matches current material, add it to bundle */
                    if (Tri.MaterialName == Mat.Name)
                    {
                        Surf.Triangles.Add(Tri);
                        continue;
                    }

                    if (Animated && MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask)
                    {
                        //DebugConsole.WriteLine("animated: " + Mat.Name);
                        try {
                            if (MainForm.CurrentScene.SegmentFunctions[TexturePointerBank - 8].HasPointer())
                            {
                                int search = MainForm.CurrentScene.AdditionalTextures.FindIndex(y => y.DisplayName == MainForm.CurrentScene.SegmentFunctions[TexturePointerBank - 8].Functions.Find(x => x.Type == 0x03).TextureSwap);
                                if (search != -1)
                                {
                                    if (MainForm.CurrentScene.AdditionalTextures.Find(y => y.DisplayName == MainForm.CurrentScene.SegmentFunctions[TexturePointerBank - 8].Functions.Find(x => x.Type == 0x03).TextureSwap).Name == Mat.Name)
                                    {
                                        Surf.Triangles.Add(Tri);
                                    }
                                }
                                else
                                {
                                    search = MainForm.CurrentScene.AdditionalTextures.FindIndex(y => y.DisplayName == MainForm.CurrentScene.SegmentFunctions[TexturePointerBank - 8].Functions.Find(x => x.Type == ZTextureAnim.texframe).TextureSwapList[0].Texture);
                                    if (search != -1)
                                    {
                                        if (MainForm.CurrentScene.AdditionalTextures.Find(y => y.DisplayName == MainForm.CurrentScene.SegmentFunctions[TexturePointerBank - 8].Functions.Find(x => x.Type == ZTextureAnim.texframe).TextureSwapList[0].Texture).Name == Mat.Name)
                                        {
                                            Surf.Triangles.Add(Tri);
                                        }
                                    }
          
                                }

                            }
                        }
                        catch (Exception e)
                        {
                            // skip
                        }

                    }
                }

                /* Add new surface bundle to list */
                if (Surf.Triangles.Count != 0)
                    SurfBundles.Add(Surf);
            }
            int culloffset = 0;
            int mtxoffset = 0;
            int conditionoffset = 0;

            /* Parse surface bundles to create the actual display list */
            foreach (SurfaceBundle Surf in SurfBundles)
            {
                /* General variables, etc. */
                List<byte> AsmTris = new List<byte>();
                bool CommToggle = true;
                int hasalphavertexoffset = 0;

                /* Generate initial commands */
                Helpers.Append64(ref DList, NoParam(GBI.G_RDPPIPESYNC));





                //  for(int dd = 0; dd < 10; dd++)
                //    {
                //          Helpers.Append64(ref DList, 0x0000000000000000);
                //    }

                Helpers.Append64(ref DList, 0xD9FCFFFF00000000);


                if (!firsttime && MainForm.settings.DListCulling && !Surf.textureless && !Billboard && !TwoAxisBillboard && !(MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask && AnimationBank >= 8 && MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasConditional()))
                {
                    culloffset = DList.Count;
                    Helpers.Append64(ref DList, 0x0000000000000000); //vtx command for culling
                    Helpers.Append64(ref DList, 0x030000000000000E); //cull command
                   // Helpers.Append64(ref DList, 0x0300000E0000000E); //cull command
                }

                if (Billboard || TwoAxisBillboard)
                {
                    mtxoffset = DList.Count;

                    //if (MainForm.n64preview == false)
                    //{
                        Helpers.Append64(ref DList, 0xDA38000000000000);
                        if (Billboard)
                            Helpers.Append64(ref DList, 0xDA38000101000000);
                        else
                            Helpers.Append64(ref DList, 0xDA38000101000040);
                  //  }


          
                }

                Helpers.Append64(ref DList, 0xE700000000000000); //pipe sinc



                if (!hasalphavertex) Helpers.Append64(ref DList, 0xD9FFFFFF00030000);

                NTexture ThisTexture;
                /* Get texture information */
                ThisTexture = Textures[Obj.Materials.IndexOf(Surf.Material)];

                if (!Surf.textureless)
                {
                    /*
                    // Helpers.Append64(ref DList, SetTextureLOD(GBI.G_TL_TILE)); //G_SETOTHERMODE_H
                    if (Pixelated) Helpers.Append64(ref DList, SetTextureFilt(GBI.G_TF_POINT)); //G_SETOTHERMODE_H pixel effect
                    else Helpers.Append64(ref DList, SetTextureFilt(GBI.G_TF_BILERP));
                    //  if (!Pixelated) DebugConsole.WriteLine(SetTextureFilt(GBI.G_TF_AVERAGE).ToString("X16"));
                    // SetOtherMode(GBI.G_SETOTHERMODE_H, GBI.G_MDSFT_TEXTDETAIL, 2, GBI.G_TD_CLAMP);
                    if (ThisTexture.Format == GBI.G_IM_FMT_CI)
                    {
                        Helpers.Append64(ref DList, SetTextureLUT(GBI.G_TT_RGBA16)); //TODO rollback?
                    }
                    else
                    {
                        Helpers.Append64(ref DList, SetTextureLUT(GBI.G_TT_NONE));
                    }*/

                    /*
                    Helpers.Append64(ref DList, SetOtherMode(GBI.G_SETOTHERMODE_H, 4 , 20, 
                        (ulong)(Pixelated ? GBI.G_TF_POINT : GBI.G_TF_BILERP) 
                        | (ulong)(ThisTexture.Format == GBI.G_IM_FMT_CI ? GBI.G_TT_RGBA16 : GBI.G_TT_NONE)));
                        */

                    Helpers.Append64(ref DList, SetOtherMode(GBI.G_SETOTHERMODE_H, 4, 20, (ulong) (GBI.G_AD_NOISE | GBI.G_CD_MAGICSQ | GBI.G_CK_NONE | GBI.G_TC_FILT | (Pixelated ? GBI.G_TF_POINT : GBI.G_TF_BILERP) | GBI.G_TL_TILE | GBI.G_TD_CLAMP | GBI.G_TP_PERSP | GBI.G_CYC_2CYCLE | GBI.G_PM_NPRIMITIVE | (ThisTexture.Format == GBI.G_IM_FMT_CI ? GBI.G_TT_RGBA16 : GBI.G_TT_NONE))));
                    
                    Helpers.Append64(ref DList, Texture(-1, -1, 0, GBI.G_TX_RENDERTILE, GBI.G_ON)); //G_TEXTURE

                }

                if (hasalphavertex)
                {
                   // Helpers.Append64(ref DList, 0xD9F1FFFF00000000);
                    hasalphavertexoffset = DList.Count;
                    Helpers.Append64(ref DList, 0xD9FFFFFF00010400);

                    
                    
                }

                /* Is surface translucent? (needed later) */
                bool IsTranslucent = ((TintAlpha >> 24) != 255);

                if (hasalphavertex) IsTranslucent = true;

                //byte animationid = 0x00;
                string AnimationType = "";

                if (Animated && !MainForm.exportingZobj)
                {
                    string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";


                    if(!MainForm.settings.MajorasMask && !MainForm.settings.command1AOoT)
                    { 
                    XmlNodeList animnodes = XMLreader.getXMLNodes("SceneAnimations", "Function");
                    AnimationItem AnimNode = new AnimationItem();
                        if (animnodes != null)
                            foreach (XmlNode node in animnodes)
                            {
                                XmlAttributeCollection nodeAtt = node.Attributes;
                                if (System.Convert.ToByte(nodeAtt["Key"].Value, 16) == SceneSettings)
                                {

                                foreach (XmlNode node2 in node.ChildNodes)
                                {
                                        if (node2.NodeType == XmlNodeType.Comment) continue;

                                        nodeAtt = node2.Attributes;
                                        if (System.Convert.ToByte(nodeAtt["Key"].Value, 16) == AnimationBank)
                                        {
                                           if (nodeAtt["Type"].Value == "Animation")
                                           {
                                                AnimationType = "Animation";
                                               // AnimNode.Bank = System.Convert.ToByte(nodeAtt["Bank"].Value, 16);
                                                AnimNode.Transparent = nodeAtt["Transparent"] != null;
                                                AnimNode.Light = nodeAtt["Light"] != null;
                                               // AnimNode.Multitexture = nodeAtt["Multitexture"] != null;
                                            }
                                           else if (nodeAtt["Type"].Value == "Texture")
                                           {
                                                AnimationType = "Texture";
                                            }
                                        }
                                }

                            }
                        }

                        if (AnimationType != "Animation") { Animated = false; }
                        else
                        {
                         //   animationid = (byte) AnimationBank;
                        //    if (animationid == 0xFF) Animated = false;
                            if (AnimationType == "Animation")
                            {
                                IsTranslucent = System.Convert.ToBoolean(AnimNode.Transparent);
                                OutdoorLight = System.Convert.ToBoolean(AnimNode.Light);
                                if ((TintAlpha >> 24) < 255 && !IsTranslucent) TintAlpha = 0xFFFFFFFF;
                                if ((TintAlpha >> 24) == 255 && IsTranslucent) TintAlpha = 0xFEFFFFFE;

                            }

                        }


                    }
                    else
                    {
                        //animationid = (byte) AnimationBank;
                    }

                }


                float TexXR, TexYR;

                if (!Surf.textureless & Obj.Materials.IndexOf(Surf.Material) < Textures.Count)
                {
                    ObjFile.Material targetmat = (Surf.Material);

                    /*

                    if (Animated && MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask)
                    {
                        try
                        {


                        if (MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasPointer())
                        {
                            int search = MainForm.CurrentScene.AdditionalTextures.FindIndex(y => y.DisplayName == MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].Functions.Find(x => x.Type == 0x03).TextureSwap);
                            if (search != -1)
                                targetmat = MainForm.CurrentScene.AdditionalTextures.Find(y => y.DisplayName == MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].Functions.Find(x => x.Type == 0x03).TextureSwap);
                            
                            else
                            {
                                search = MainForm.CurrentScene.AdditionalTextures.FindIndex(y => y.DisplayName == MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].Functions.Find(x => x.Type == ZTextureAnim.texframe).TextureSwapList[0].Texture);
                                if (search != -1)
                                    targetmat = MainForm.CurrentScene.AdditionalTextures.Find(y => y.DisplayName == MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].Functions.Find(x => x.Type == ZTextureAnim.texframe).TextureSwapList[0].Texture);

                            }

                        }
                        }
                        catch (Exception e)
                        {
                            targetmat = (Surf.Material);
                        }
                    }
                    */

                    /* Texture variables */
                    TexXR = targetmat.Width / (32.0f * TexScale);
                    TexYR = targetmat.Height / (32.0f * TexScale);
                    int TexPal = 0;

                    int setimgoffset = DList.Count + 8;

                    /* Insert texture loading commands */

                    int tileS = Group.TileS;
                    int tileT = Group.TileT;

                    if ((targetmat.map_Kd != null && targetmat.map_Kd.ToLower().Contains("#clampx")) || (targetmat.tags != null && targetmat.tags.ToLower().Contains("#clampx"))) tileS = 2;
                    else if ((targetmat.map_Kd != null && targetmat.map_Kd.ToLower().Contains("#mirrorx")) || (targetmat.tags != null && targetmat.tags.ToLower().Contains("#mirrorx"))) tileS = 1;

                    if ((targetmat.map_Kd != null && targetmat.map_Kd.ToLower().Contains("#clampy")) || (targetmat.tags != null && targetmat.tags.ToLower().Contains("#clampy"))) tileT = 2;
                    else if ((targetmat.map_Kd != null && targetmat.map_Kd.ToLower().Contains("#mirrory")) || (targetmat.tags != null && targetmat.tags.ToLower().Contains("#mirrory"))) tileT = 1;

                    int prevbank = Bank;
                    uint prevoffset = ThisTexture.TexOffset;

                    if (AnimationType == "Texture" && targetmat.map_Kd != null && targetmat.map_Kd.ToLower().Contains("#special"))
                    {
                        Bank = TexturePointerBank;
                        ThisTexture.TexOffset = 0;
                    }
                    else if (Animated && MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask)
                    {
                        if (MainForm.CurrentScene.SegmentFunctions[TexturePointerBank - 8].HasPointer())
                        {
                            Bank = TexturePointerBank;
                            ThisTexture.TexOffset = 0;
                        }
                    }

                    InsertTextureLoad(ref DList, targetmat.Width, targetmat.Height, ThisTexture, TexPal,
                        GBI.G_TX_RENDERTILE, tileS, tileT, 0, 0, Group.BaseShiftS, Group.BaseShiftT);

                    Bank = prevbank;
                    ThisTexture.TexOffset = prevoffset;

                    if (Group.MultiTexMaterialName != "")
                    {
                        tileS = Group.TileS;
                        tileT = Group.TileT;

                        ObjFile.Material mat = MatList.Find(x => x.Name == Group.MultiTexMaterialName);
                        int matindex = MatList.FindIndex(x => x.Name == Group.MultiTexMaterialName);

                        if (mat != null)
                        {
                            if ((mat.map_Kd != null && mat.map_Kd.ToLower().Contains("#clampx")) || (mat.tags != null && mat.tags.ToLower().Contains("#clampx"))) tileS = 2;
                            else if ((mat.map_Kd != null && mat.map_Kd.ToLower().Contains("#mirrorx")) || (mat.tags != null && mat.tags.ToLower().Contains("#mirrorx"))) tileS = 1;

                            if ((mat.map_Kd != null && mat.map_Kd.ToLower().Contains("#clampy")) || (mat.tags != null && mat.tags.ToLower().Contains("#clampy"))) tileT = 2;
                            else if ((mat.map_Kd != null && mat.map_Kd.ToLower().Contains("#mirrory")) || (mat.tags != null && mat.tags.ToLower().Contains("#mirrory"))) tileT = 1;

                            //TODO is using matindex correct?
                            InsertTextureLoad(ref DList, mat.Width,
                                mat.Height,
                                Textures[matindex], TexPal, GBI.G_TX_RENDERTILE + 1,
                                tileS, tileT,
                               Group.ShiftS, Group.ShiftT, Group.BaseShiftS, Group.BaseShiftT);
                        }
                    }

                    if (Animated && MainForm.settings.MajorasMask) Helpers.Append64(ref DList, 0xDE00000000000000 | (ulong)(AnimationBank << 24));




                    /*
                    if (SceneSettings == 0x14 && Animated) //dodongo's cavern special animation, doesn't work anyways
                    {

                        Helpers.Overwrite64(ref DList, setimgoffset, 0xFD50000009000000);
                        for (int i = 0; i < 20; i++)
                            Helpers.Append64(ref DList, 0x0000000000000000);

                        //  DebugConsole.WriteLine("Special animation: dodongo cavern");
                    }
                    */
                }
                else
                {
                    /* Get texture information */
                    ThisTexture = new NTexture();

                    /* Texture variables */
                    TexXR = 1;
                    TexYR = 1;
                }

                //

                bool HasBlendingAnimation = (Animated && MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask
                    && MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasBlending());

                bool WritePrim = (TintAlpha != 0xFFFFFFFF || IsTranslucent || hasalphavertex || HasBlendingAnimation || Group.MultiTexMaterialName != "" || AlphaMask);

                ulong LerpValue = (ulong) (!HasBlendingAnimation ? C_PRIM_LOD_FRAC : C_ENV_ALPHA);



                if (!Group.Custom)
                {
                    if (Surf.textureless)
                    {
                        if (!hasalphavertex)
                        {
                            if (IsTranslucent == true)
                                //Helpers.Append64(ref DList, 0xFCBD7E038FFE7DFB);
                                //textureless transparent
                                Helpers.Append64(ref DList, SetCombineNew(
                                    C_0,        C_0, C_0,         C_SHADE,
                                    C_COMBINED, C_0, C_PRIMITIVE, C_0,
                                    A_0,        A_0, A_0,         A_0,
                                    A_0,        A_0, A_0,         A_PRIMITIVE));
                            else
                                //Helpers.Append64(ref DList, 0xFCBD7E038FFE7DF8);
                                //textureless opaque
                                if (WritePrim)
                                    Helpers.Append64(ref DList, SetCombineNew(
                                        C_0,        C_0, C_0,         C_SHADE,
                                        C_COMBINED, C_0, C_PRIMITIVE, C_0,
                                        A_0,        A_0, A_0,         A_0,
                                        A_0,        A_0, A_0,         A_1));
                                else
                                    Helpers.Append64(ref DList, SetCombineNew(
                                        C_0,        C_0, C_0,         C_SHADE,
                                        C_0,        C_0, C_0,         C_COMBINED,
                                        A_0,        A_0, A_0,         A_0,
                                        A_0,        A_0, A_0,         A_1));
                        }
                        else
                        {

                            //Helpers.Append64(ref DList, 0xFCDC7E03FF1E7DFC);
                            //textureless vertex alpha
                            Helpers.Append64(ref DList, SetCombineNew(
                                C_0,        C_0, C_0,         C_SHADE,
                                C_COMBINED, C_0, C_PRIMITIVE, C_0,
                                A_0,        A_0, A_0,         A_0,
                                A_0,        A_0, A_0,         A_SHADE));
                        }

                        if (hasalphavertex)
                        {
                            Helpers.Append64(ref DList, 0xE200001C0C1849D8 | (ulong)((Decal) ? 0xC00 : 0x000));
                            Helpers.Append64(ref DList, 0xD9FEFFFF00000000);
                        }
                        //  DebugConsole.WriteLine("textureless");
                    }
                    else
                    {
                        int matindex = MatList.FindIndex(x => x.Name == Group.MultiTexMaterialName);
                        if (IsTranslucent == true)
                        {
                            // XLU
                            if (hasalphavertex)
                            {
                                if (Group.MultiTexMaterialName == "")
                                {
                                    if (!ThisTexture.HasAlpha && !IsI4orI8(ThisTexture))
                                        //Helpers.Append64(ref DList, 0xFC127E03FF1FFDFC);
                                        // Vertex alpha, single opaque texture
                                        Helpers.Append64(ref DList, SetCombineNew(
                                            C_TEXEL0,   C_0, C_SHADE,     C_0,
                                            C_COMBINED, C_0, C_PRIMITIVE, C_0,
                                            A_0,        A_0, A_0,         A_PRIMITIVE,
                                            A_COMBINED, A_0, A_SHADE,     A_0));
                                    else
                                        //Helpers.Append64(ref DList, 0xFC272C041F1093FF);
                                        // Vertex alpha, single transparent texture
                                        Helpers.Append64(ref DList, SetCombineNew(
                                            C_TEXEL0,   C_0, C_SHADE,     C_0,
                                            C_COMBINED, C_0, C_PRIMITIVE, C_0,
                                            A_TEXEL0,   A_0, A_PRIMITIVE, A_0,
                                            A_COMBINED, A_0, A_SHADE,     A_0));
                                }
                                else
                                {
                                    if (!ThisTexture.HasAlpha && !IsI4orI8(ThisTexture))
                                        // Vertex alpha, multi opaque texture
                                        Helpers.Append64(ref DList, SetCombineNew(
                                            C_TEXEL1,   C_TEXEL0, LerpValue,   C_TEXEL0,
                                            C_COMBINED, C_0,      C_SHADE,     C_0,
                                            A_0,        A_0,      A_0,         A_PRIMITIVE,
                                            A_COMBINED, A_0,      A_SHADE,     A_0));
                                    else
                                        // Vertex alpha, multi transparent texture
                                        Helpers.Append64(ref DList, SetCombineNew(
                                            C_TEXEL1,   C_TEXEL0, LerpValue,       C_TEXEL0,
                                            C_COMBINED, C_0,      C_SHADE,         C_0,
                                            A_TEXEL1,   A_TEXEL0, A_0,             A_TEXEL0,
                                            A_COMBINED, A_0,      A_SHADE,         A_0));
                                }
                            }
                            else if (Group.MultiTexMaterialName != "")
                            {
                                if ((Textures[matindex].HasAlpha || IsI4orI8(Textures[matindex])))
                                {
                                    if (AlphaMask)
                                        //Helpers.Append64(ref DList, SetCombine(0x127E03FFFFF5F8));
                                        //Alpha mask, transparent multitexture
                                        Helpers.Append64(ref DList, SetCombineNew(
                                            C_TEXEL0,   C_0, C_SHADE,     C_0,
                                            C_COMBINED, C_0, C_PRIMITIVE, C_0,
                                            A_TEXEL1,   A_0, A_PRIMITIVE, A_0,
                                            A_0,        A_0, A_0,         A_COMBINED));
                                    else
                                        //Helpers.Append64(ref DList, SetCombine(0x262A04, 0x1FFC93F8));
                                        //transparent multitexture
                                        Helpers.Append64(ref DList, SetCombineNew(
                                            C_TEXEL1,   C_TEXEL0, LerpValue,     C_TEXEL1,
                                            C_COMBINED, C_0,      C_SHADE,       C_0,
                                            A_TEXEL1,   A_TEXEL0, A_0,           A_TEXEL0,
                                            A_COMBINED, A_0,      A_PRIMITIVE,   A_0));
                                    //else Helpers.Append64(ref DList, SetCombine(0x262A051FFC93F8));
                                }
                                else
                                    //Helpers.Append64(ref DList, SetCombine(0x267E04, 0x1F0CFDFF));
                                    //opaque multitexture
                                    Helpers.Append64(ref DList, SetCombineNew(
                                        C_TEXEL1,   C_TEXEL0, LerpValue,   C_TEXEL0,
                                        C_COMBINED, C_0,      C_SHADE,     C_0,
                                        A_0,        A_0,      A_0,         A_1,
                                        A_COMBINED, A_0,      A_PRIMITIVE, A_0));

                            }
                            else if (ThisTexture.HasAlpha || IsI4orI8(ThisTexture))
                                //Helpers.Append64(ref DList, SetCombine(0x127E03, 0xFFFFF3F8));
                                //xlu single transparent texture
                                Helpers.Append64(ref DList, SetCombineNew(
                                    C_TEXEL0,   C_0, C_SHADE,     C_0,
                                    C_COMBINED, C_0, C_PRIMITIVE, C_0,
                                    A_0,        A_0, A_0,         A_TEXEL0,
                                    A_COMBINED, A_0, A_PRIMITIVE, A_0));

                            else
                                //Helpers.Append64(ref DList, SetCombine(0x167E03, 0xFF0FFDFF));
                                //xlu single opaque texture
                                Helpers.Append64(ref DList, SetCombineNew(
                                    C_TEXEL0,   C_0, C_SHADE,     C_0,
                                    C_COMBINED, C_0, C_PRIMITIVE, C_0,
                                    A_0,        A_0, A_0,         A_0,
                                    A_0,        A_0, A_0,         A_PRIMITIVE));



                           
                            if (hasalphavertex)
                            {
                                Helpers.Append64(ref DList, 0xE200001C0C1849D8 | (ulong)((Decal) ? 0xC00 : 0x000));
                                Helpers.Append64(ref DList, 0xD9FEFFFF00000000);
                            }
                            else if (ThisTexture.HasAlpha || (Group.MultiTexMaterialName != "" && Textures[matindex].HasAlpha) || IsI4orI8(ThisTexture))
                                Helpers.Append64(ref DList, SetRenderMode(0x18, (uint)(0x081049D0 | ((IgnoreFog) ? 0x00000000 : 0xC0000000) | ((Decal) ? 0xC00 : 0x000))));
                            else
                                Helpers.Append64(ref DList, SetRenderMode(0x18, (uint)(0x081049D8 | ((IgnoreFog) ? 0x00000000 : 0xC0000000) | ((Decal) ? 0xC00 : 0x000))));
                            //Helpers.Append64(ref DList, SetRenderMode(0x18, 0xC8113078));   

                        } //Non-translucent
                        else if (ThisTexture.HasAlpha || (Group.MultiTexMaterialName != "" && Textures[matindex].HasAlpha))
                        {
                            /* Texture with alpha channel */
                            if (Group.MultiTexMaterialName != "")
                            {

                                //Helpers.Append64(ref DList, SetCombine(0x262A04, 0x1FFC93F8));
                                //opa multitexture, one of the textures is transparent
                                Helpers.Append64(ref DList, SetCombineNew(
                                    C_TEXEL1,   C_TEXEL0, LerpValue,     C_TEXEL0,
                                    C_COMBINED, C_0,      C_SHADE,       C_0,
                                    A_TEXEL1,   A_TEXEL0, A_ENVIRONMENT, A_TEXEL0,
                                    A_0,        A_0,      A_0,           A_COMBINED));
                            }
                            else //Helpers.Append64(ref DList, SetCombine(0x127E03, 0xFFFFF3F8));
                                //opa single transparent texture
                                if (WritePrim)
                                        Helpers.Append64(ref DList, SetCombineNew(
                                        C_TEXEL0,   C_0,   C_SHADE,     C_0,
                                        C_COMBINED, C_0,   C_PRIMITIVE, C_0,
                                        A_0,        A_0,   A_0,         A_TEXEL0,
                                        A_0,        A_0,   A_0,         A_COMBINED));
                                else
                                        Helpers.Append64(ref DList, SetCombineNew(
                                        C_TEXEL0,   C_0,   C_SHADE,     C_0,
                                        C_0,        C_0,   C_0,         C_COMBINED,
                                        A_0,        A_0,   A_0,         A_TEXEL0,
                                        A_0,        A_0,   A_0,         A_COMBINED));

                            if ((Group.MultiTexMaterialName != "" && Textures[matindex].HasAlpha && Textures[matindex].Format != GBI.G_IM_FMT_RGBA)) Helpers.Append64(ref DList, SetRenderMode(0x18, (uint)(0x081049D0 | ((IgnoreFog) ? 0x00000000 : 0xC0000000) | ((Decal) ? 0xC00 : 0x000))));
                            else Helpers.Append64(ref DList, SetRenderMode(0x18, (uint)(0x08100078 | ((SmoothRGBAEdges) ? 0x4000 : 0x3000) | ((IgnoreFog) ? 0x00000000 : 0xC0000000) | ((Decal) ? 0xC00 : 0x000)))); //crap solution
                                                                                                                                                                                                                     // 0xC8104B50

                        }
                        else
                        {
                            if (Group.MultiTexMaterialName != "")
                                //Helpers.Append64(ref DList, SetCombine(0x267E04, 0x1FFCFDF8));
                                //opa multitexture
                                if (!hasalphavertex)
                                    Helpers.Append64(ref DList, SetCombineNew(
                                        C_TEXEL1,   C_TEXEL0, LerpValue,   C_TEXEL0,
                                        C_COMBINED, C_0,      C_SHADE,     C_0,
                                        A_0,        A_0,      A_0,         A_0,
                                        A_0,        A_0,      A_0,         A_1));
                                else 
                                    //Helpers.Append64(ref DList, SetCombine(0x121603, 0xFF5BFFF8));
                                    //opa multitexture alpha vertex (makes second texture appear when alpha = 0)
                                    Helpers.Append64(ref DList, SetCombineNew(
                                        C_TEXEL0,   C_TEXEL1, A_SHADE, C_TEXEL1,
                                        C_COMBINED, C_0,      C_SHADE, C_0,
                                        A_0,        A_0,      A_0,     A_0,
                                        A_0,        A_0,      A_0,     A_1));
                            else
                                //Helpers.Append64(ref DList, SetCombine(0x127E03, 0xFFFFFDF8));
                                //opa single texture
                                if (WritePrim)
                                    Helpers.Append64(ref DList, SetCombineNew(
                                        C_TEXEL0,   C_0, C_SHADE,     C_0,
                                        C_COMBINED, C_0, C_PRIMITIVE, C_0,
                                        A_0,        A_0, A_0,         A_0,
                                        A_0,        A_0, A_0,         A_1));
                                else
                                    Helpers.Append64(ref DList, SetCombineNew(
                                        C_TEXEL0,   C_0, C_SHADE,     C_0,
                                        C_0,        C_0, C_0,         C_COMBINED,
                                        A_0,        A_0, A_0,         A_0,
                                        A_0,        A_0, A_0,         A_1));

                            if (VertexNormals) Helpers.Append64(ref DList, SetRenderMode(0x18, (uint)(0xC8112078 | ((Decal) ? 0xC00 : 0x000))));
                            else if (hasalphavertex) Helpers.Append64(ref DList, SetRenderMode(0x18, (uint)(0xC81049D8 | ((Decal) ? 0xC00 : 0x000))));
                            else Helpers.Append64(ref DList, SetRenderMode(0x18, (uint)(0x08110078 | ((SmoothRGBAEdges) ? 0x4000 : 0x3000) | ((IgnoreFog) ? 0x00000000 : 0xC0000000) | ((Decal) ? 0xC00 : 0x000))));

                            // DebugConsole.WriteLine("alpha vertex!");

                        }
                    }
                    if (OutdoorLight)
                    {
                        if (IsTranslucent == true || !Culling)
                        {
                            //Helpers.Append64(ref DList, 0xD9F3FBFF00000000);
                            if (!Metallic && Culling) Helpers.Append64(ref DList, 0xD9F3FBFF00030400 | (ulong)(PointLight ? 0x400000 : 0x000000));
                            else if (!Metallic && !Culling) Helpers.Append64(ref DList, 0xD9F3FBFF00030000 | (ulong)(PointLight ? 0x400000 : 0x000000));
                            else Helpers.Append64(ref DList, 0xD9F3FBFF000E0400);
                        }
                        else
                        {
                            //Helpers.Append64(ref DList, 0xD9F3FFFF00000000);
                            if (!Metallic) Helpers.Append64(ref DList, 0xD9F3FFFF00030400 | (ulong)(PointLight ? 0x400000 : 0x000000));
                            else Helpers.Append64(ref DList, 0xD9F3FFFF000E0400);

                        }
                    }
                    else
                    {
                        if (IsTranslucent == true || !Culling)
                        {
                            //if (!Metallic) Helpers.Append64(ref DList, 0xD9F1FBFF00000000); //why this is needed?
                            //else Helpers.Append64(ref DList, 0xD9F3FBFF00000000);
                            if (!Metallic && Culling) Helpers.Append64(ref DList, 0xD9F1FBFF00010400);
                            else if (!Metallic && !Culling) Helpers.Append64(ref DList, 0xD9F1FBFF00010000);
                            else Helpers.Append64(ref DList, 0xD9F3FBFF000C0400);
                        }
                        else
                        {
                            //if (!Metallic) Helpers.Append64(ref DList, 0xD9F1FFFF00000000); //why this is needed?
                            //else Helpers.Append64(ref DList, 0xD9F3FBFF00000000);
                            if (!Metallic) Helpers.Append64(ref DList, 0xD9F1FFFF00000400); //| (ulong) ((IgnoreFog) ? 0x00000 : 0x10000)
                            else Helpers.Append64(ref DList, 0xD9F3FBFF000C0400);
                        }
                    }



                }
                else
                {
                    Helpers.Append64(ref DList, 0xFC00000000000000 | Group.CustomDL[0]);
                    Helpers.Append64(ref DList, 0xE200000000000000 | Group.CustomDL[2]);
                    Helpers.Append64(ref DList, 0xD900000000000000 | Group.CustomDL[1]);
                }

                if (hasalphavertex)
                {
                    //   DebugConsole.WriteLine("dl c " + DList.Count);
                    //    DebugConsole.WriteLine("" + Helpers.Read32(DList, hasalphavertexoffset+4).ToString("X8"));
                    Helpers.Overwrite32(ref DList, hasalphavertexoffset, Helpers.Read32(DList, DList.Count - 8));
                    Helpers.Overwrite32(ref DList, hasalphavertexoffset + 4, Helpers.Read32(DList, DList.Count - 8 + 4));
                    //Helpers.Overwrite32(ref DList, hasalphavertexoffset - 8, Helpers.Read32(DList, DList.Count - 16));
                    //Helpers.Overwrite32(ref DList, hasalphavertexoffset - 4, Helpers.Read32(DList, DList.Count - 16 + 4));

                    DList.RemoveRange(DList.Count - 1 - 8, 8);

                  //  DList.RemoveRange(DList.Count - 1 - 16, 16);

                    //  DebugConsole.WriteLine("dl c " + DList.Count);
                    // DebugConsole.WriteLine("" + Helpers.Read32(DList, hasalphavertexoffset+4).ToString("X8"));

                }

                //     DebugConsole.WriteLine("translucent: " + string.Format("0x{0:X}", SetCombine(0x167E03, 0xFF0FFDFF)) );

                //  DebugConsole.WriteLine("solid: " + string.Format("0x{0:X}", SetCombine(0x127E03, 0xFFFFFDF8))) ;

                /* Generate SetCombine/RenderMode commands */




                bool DEplaced = false;



                if (Animated && !MainForm.settings.MajorasMask && !MainForm.settings.command1AOoT)
                    Helpers.Append64(ref DList, 0xDE00000000000000 | (ulong) (AnimationBank << 24));
                else if (Animated && MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask)
                {
                    if (MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasScroll() || MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasConditional())
                    {
                        Helpers.Append64(ref DList, 0xDE00000000000000 | (ulong)(AnimationBank << 24));
                        DEplaced = true;
                    }
                }

                uint primlodfrac = MultitextureAlpha >> 24;

                if (HasBlendingAnimation)
                {
                    if (!DEplaced) Helpers.Append64(ref DList, 0xDE00000000000000 | (ulong)(AnimationBank << 24));
                    if (!MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].BlendingHasMultitexAlpha())
                        Helpers.Append64(ref DList, SetEnvColor(MultitextureAlpha));
                    if (MainForm.n64preview)
                    {
                        if (IsTranslucent)
                            Helpers.Append64(ref DList, 0x0000000012345100 | (ulong)(AnimationBank));
                        else
                            Helpers.Append64(ref DList, 0x0000000012345000 | (ulong)(AnimationBank));
                    }
                    

                }
                else
                {
                    if (WritePrim) Helpers.Append64(ref DList, SetPrimColorNew(TintAlpha, primlodfrac));
                }
                
                    
                /*
                if (Group.MultiTexMaterialName != "")
                    Helpers.Append64(ref DList, SetEnvColor(MultitextureAlpha));
                else if (EnvColor)
                    Helpers.Append64(ref DList, SetEnvColor(TintAlpha));
                else
                    Helpers.Append64(ref DList, SetEnvColor(0xFFFFFFFF));
                */

                if (Animated && MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask && MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasConditional())
                {

                    if (!MainForm.n64preview)
                    {


                        //Helpers.Append64(ref DList, 0xDA38000300000000);
                        Helpers.Append64(ref DList, 0xDA38000000000000 | (ulong)(AnimationBank << 24));
                    }
                    
                }


                /* Parse triangles, generate VTX and TRI commands */
                /* Very heavily based on code from spinout's .obj importer r13 */
                foreach (ObjFile.Triangle Tri in Surf.Triangles)
                {
                    int TriIndex = Surf.Triangles.IndexOf(Tri);

                  //  DebugConsole.WriteLine("vertex color count: " + Obj.VertexColors.Count);
                 //   DebugConsole.WriteLine("vertex color count: " + (Tri.VertColor[0] - 1));

                    int[] TriPoints = new int[3];
                    for (int i = 0; i < 3; i++)
                    {
                        
                       // if ((float)Obj.VertexColors[Tri.VertColor[i] - 1].A < 1) DebugConsole.WriteLine("Vertex with alpha!");

                        NVertex NewVert = new NVertex(
                            new Vector3d(Obj.Vertices[Tri.VertIndex[i]].X, Obj.Vertices[Tri.VertIndex[i]].Y, Obj.Vertices[Tri.VertIndex[i]].Z),
                            new Vector2d(Obj.TextureCoordinates[Tri.TexCoordIndex[i]].U * TexXR, Obj.TextureCoordinates[Tri.TexCoordIndex[i]].V * TexYR),
                            //new Color4(Obj.Materials[Obj.Materials.IndexOf(Surf.Material)].Kd[0] * 255, Obj.Materials[Obj.Materials.IndexOf(Surf.Material)].Kd[1] * 255, Obj.Materials[Obj.Materials.IndexOf(Surf.Material)].Kd[2] * 255, 0xFF),
                            new Color4((float)Obj.VertexColors[Tri.VertColor[i] -1 ].R, (float)Obj.VertexColors[Tri.VertColor[i] - 1].G, (float)Obj.VertexColors[Tri.VertColor[i] - 1].B, (float)Obj.VertexColors[Tri.VertColor[i] - 1].A),
                            Obj.Vertices[Tri.VertIndex[i]].VN);

                        int VtxNo = VertList.FindIndex(FindObj =>
                            FindObj.Position == NewVert.Position &&
                            FindObj.TexCoord == NewVert.TexCoord &&
                            FindObj.Colors == NewVert.Colors &&
                            FindObj.Normals == NewVert.Normals);

                        if (VtxNo == -1)
                        {
                            if (VertList.Count <= 29 + i)
                                VertList.Add(NewVert);
                            else
                                throw new Exception("Vertex buffer overflow; this should never happen!");

                            VtxNo = VertList.Count - 1;
                        }

                        TriPoints[i] = (VtxNo << 1);
                    }

                    Helpers.Append32(ref AsmTris, (uint)(((CommToggle ? 0x06 : 0x00) << 24) | (TriPoints[0] << 16) | (TriPoints[1] << 8) | TriPoints[2]));
                    //Helpers.Append32(ref AsmTris, (uint)(((CommToggle ? 0x06 : 0x00) << 24) | (999 << 16) | (999 << 8) | 999));

                    CommToggle = !CommToggle;

                    if (VertList.Count > 29 || TriIndex == Surf.Triangles.Count - 1)
                    {
                        uint VertOffset = BaseOffset + (uint)VertData.Count;

                        for (int j = 0; j < VertList.Count; j++)
                        {
                            try
                            {

                                Helpers.Append16(ref VertData, (ushort)(System.Convert.ToInt16(MainForm.Clamp(VertList[j].Position.X * Scale, -32768, 32767)) - midX));
                                Helpers.Append16(ref VertData, (ushort)(System.Convert.ToInt16(MainForm.Clamp(VertList[j].Position.Y * Scale, -32768, 32767)) - midY));
                                Helpers.Append16(ref VertData, (ushort)(System.Convert.ToInt16(MainForm.Clamp(VertList[j].Position.Z * Scale, -32768, 32767)) - midZ));
                                Helpers.Append16(ref VertData, 0);
                                Helpers.Append16S(ref VertData, (short)(VertList[j].TexCoord.X * 1024.0f));
                                Helpers.Append16S(ref VertData, (short)(VertList[j].TexCoord.Y * 1024.0f));
                            }
                            catch(System.OverflowException e)
                            {
                                MessageBox.Show("Vertex overflow! is the scale of the map too big?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            
                            //(???)
                            if (OutdoorLight)
                            {
                                Vector3d tmpNormals = VertList[j].Normals;
                                if (ScaledNormals)
                                {
                                    float mul = ((VertList[j].Colors.R + VertList[j].Colors.G + VertList[j].Colors.B) / 3.0f) * 2.0f;
                                    tmpNormals = Vector3d.Multiply(tmpNormals, mul);
                                }

                                VertData.Add((byte)System.Convert.ToByte(((int)(tmpNormals.X * 127.0f)) & 0xFF));
                                VertData.Add((byte)System.Convert.ToByte(((int)(tmpNormals.Y * 127.0f)) & 0xFF));
                                VertData.Add((byte)System.Convert.ToByte(((int)(tmpNormals.Z * 127.0f)) & 0xFF));
                               // DebugConsole.WriteLine("Normals: " + VertList[j].Normals.X + "    " +  VertList[j].Normals.Y + "    " + VertList[j].Normals.Z);
                               // DebugConsole.WriteLine("Normals2: " + (byte)System.Convert.ToByte(((int)(VertList[j].Normals.X * 255.0f)) & 0xFF) + "    " + (byte)System.Convert.ToByte(((int)(VertList[j].Normals.Y * 255.0f)) & 0xFF) + "    " + (byte)System.Convert.ToByte(((int)(VertList[j].Normals.Z * 255.0f)) & 0xFF));
                            }
                            else
                            {
                                uint Color = (uint)VertList[j].Colors.ToArgb();
                                VertData.Add((byte)((Color >> 16) & 0xFF));
                                VertData.Add((byte)((Color >> 8) & 0xFF));
                                VertData.Add((byte)(Color & 0xFF));
                            }

                            //  DebugConsole.WriteLine("vcolor "+ VertList[j].Colors.ToString());
                         //      DebugConsole.WriteLine(Color.ToString("X"));
                            uint Color2 = (uint)VertList[j].Colors.ToArgb();
                            VertData.Add((byte)((Color2 >> 24) & 0xFF));
                          //  VertData.Add((byte)((Color2 >> 24) & 0xFF));

                           // if ((byte)((Color2 >> 24) & 0xFF) < 0xFF) DebugConsole.WriteLine("Vertex alpha!: " + ((byte)((Color2 >> 24) & 0xFF)).ToString("X"));
                        }

                        if ((AsmTris.Count & 4) != 0)
                        {
                            AsmTris[AsmTris.Count - 4] = 0x05;
                            Helpers.Append32(ref AsmTris, 0);
                        }

                        Helpers.Append64(ref DList, ((ulong)(Helpers.ShiftL(GBI.G_VTX, 24, 8) | (uint)(VertList.Count << 12) | (uint)(VertList.Count * 2)) << 32) | (ulong) (Bank << 24 | VertOffset));


                        

                        DList.AddRange(AsmTris);

                        /* Determine minimum/maximum coordinate changes... */


                        //  DebugConsole.WriteLine(MinCoordinate);
                        // DebugConsole.WriteLine(MaxCoordinate);

                        if (!firsttime && MainForm.settings.DListCulling && !Billboard && !TwoAxisBillboard && !(MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask && AnimationBank >= 8 && MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasConditional()))
                        {


                            //culling bounding box


                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.X)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.Y)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.Z)));
                            Helpers.Append16(ref VertCull, 0x0000);
                            Helpers.Append64(ref VertCull, 0x0000000000000000);
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.X)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.Y)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.Z)));
                            Helpers.Append16(ref VertCull, 0x0000);
                            Helpers.Append64(ref VertCull, 0x0000000000000000);
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.X)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.Y)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.Z)));
                            Helpers.Append16(ref VertCull, 0x0000);
                            Helpers.Append64(ref VertCull, 0x0000000000000000);
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.X)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.Y)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.Z)));
                            Helpers.Append16(ref VertCull, 0x0000);
                            Helpers.Append64(ref VertCull, 0x0000000000000000);
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.X)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.Y)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.Z)));
                            Helpers.Append16(ref VertCull, 0x0000);
                            Helpers.Append64(ref VertCull, 0x0000000000000000);
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.X)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.Y)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.Z)));
                            Helpers.Append16(ref VertCull, 0x0000);
                            Helpers.Append64(ref VertCull, 0x0000000000000000);
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MinCoordinate.X)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.Y)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.Z)));
                            Helpers.Append16(ref VertCull, 0x0000);
                            Helpers.Append64(ref VertCull, 0x0000000000000000);
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.X)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.Y)));
                            Helpers.Append16(ref VertCull, (ushort)(System.Convert.ToInt16(MaxCoordinate.Z)));
                            Helpers.Append16(ref VertCull, 0x0000);
                            Helpers.Append64(ref VertCull, 0x0000000000000000);
                            /*
                        Helpers.Append64(ref VertCull, (ulong)(0x0000000000000000 | (ulong)(System.Convert.ToInt16(MaxCoordinate.X)) << 48 | (ulong)(System.Convert.ToInt16(MinCoordinate.Y)) << 32 | (ulong)(System.Convert.ToInt16(MinCoordinate.Z)) << 16));
                        Helpers.Append64(ref VertCull, 0x0000000000000000);;*/

                        }
                        if (!firsttime && (Billboard || TwoAxisBillboard))
                        {

                            Helpers.Append64(ref VertMtx, 0x0001000000000000);
                            Helpers.Append64(ref VertMtx, 0x0000000100000000);
                            Helpers.Append64(ref VertMtx, 0x0000000000010000);
                            Helpers.Append16(ref VertMtx, midX);
                            Helpers.Append16(ref VertMtx, midY);
                            Helpers.Append16(ref VertMtx, midZ);
                            Helpers.Append16(ref VertMtx, 0x0001);
                            Helpers.Append64(ref VertMtx, 0x0000000000000000);
                            Helpers.Append64(ref VertMtx, 0x0000000000000000);
                            Helpers.Append64(ref VertMtx, 0x0000000000000000);
                            Helpers.Append64(ref VertMtx, 0x0000000000000000);






                        }




                        if (MainForm.settings.DListCulling && !Billboard && !TwoAxisBillboard && !(MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask && AnimationBank >= 8 && MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasConditional())) Helpers.Overwrite64(ref DList, culloffset, ((ulong)0x0100801000000000 | (ulong) (Bank << 24) | BaseOffset + (uint)VertData.Count));

                        if (Billboard || TwoAxisBillboard) Helpers.Overwrite32(ref DList, mtxoffset+4, ((uint)0x00000000 | (uint)(Bank << 24) | BaseOffset + (uint)VertData.Count + (uint)VertCull.Count));

                      //  if (Animated && MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask && MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasConditional() && !MainForm.n64preview) Helpers.Overwrite32(ref DList, conditionoffset + 4, ((uint)0x00000000 | (uint)(Bank << 24) | BaseOffset + (uint)VertData.Count + (uint)VertCull.Count + (uint)VertMtx.Count));

                        firsttime = true;

                        VertList.Clear();
                        AsmTris.Clear();
                        CommToggle = true;
                    }
                }
            }


            /* End of display list */
       //     if (!MainForm.CurrentScene.Prerendered || MainForm.CurrentScene.Prerendered && )

            if ((Billboard || TwoAxisBillboard))
            {
                Helpers.Append64(ref DList, 0xD838000200000040);
            }
            if ((MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask && AnimationBank >= 8 && MainForm.CurrentScene.SegmentFunctions[AnimationBank - 8].HasConditional()) && !MainForm.n64preview)
            {
                Helpers.Append64(ref DList, 0xD838000200000040);

            }

            if ((Billboard || TwoAxisBillboard) && MainForm.n64preview) //TODO check render only
            {
                Helpers.Append32(ref DList, midX);
                Helpers.Append32(ref DList, (uint) (midZ | (midY << 16)));
            }

            Helpers.Append64(ref DList, NoParam(GBI.G_ENDDL)); 

            /* Finish conversion */
            List<byte> FinalData = new List<byte>();
            FinalData.AddRange(VertData);
            FinalData.AddRange(VertCull);
            FinalData.AddRange(VertMtx);
            Vertoffset = FinalData.Count; //for debugging
            FinalData.AddRange(DList);
            Data = FinalData.ToArray();



            Offset = (uint)(BaseOffset + VertData.Count + VertCull.Count + VertMtx.Count + CondMtx.Count);
        }

        #endregion

    }
}
