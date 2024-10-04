#region --- About ---
/*
 * Project SayakaGL
 */
#endregion

#region --- Using Directives ---
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

using TexLib;
using NImage;
#endregion

namespace SharpOcarina.SayakaGL
{
    #region Definitions

    public enum Ucodes
    {
        Fast3D = 0,
        F3DEX = 1,
        F3DEX2 = 2
    }

    enum UcodeG
    {
        SETCIMG = 0xFF,
        SETZIMG = 0xFE,
        SETTIMG = 0xFD,
        SETCOMBINE = 0xFC,
        SETENVCOLOR = 0xFB,
        SETPRIMCOLOR = 0xFA,
        SETBLENDCOLOR = 0xF9,
        SETFOGCOLOR = 0xF8,
        SETFILLCOLOR = 0xF7,
        FILLRECT = 0xF6,
        SETTILE = 0xF5,
        LOADTILE = 0xF4,
        LOADBLOCK = 0xF3,
        SETTILESIZE = 0xF2,
        LOADTLUT = 0xF0,
        RDPSETOTHERMODE = 0xEF,
        SETPRIMDEPTH = 0xEE,
        SETSCISSOR = 0xED,
        SETCONVERT = 0xEC,
        SETKEYR = 0xEB,
        SETKEYGB = 0xEA,
        RDPFULLSYNC = 0xE9,
        RDPTILESYNC = 0xE8,
        RDPPIPESYNC = 0xE7,
        RDPLOADSYNC = 0xE6,
        TEXRECTFLIP = 0xE5,
        TEXRECT = 0xE4
    }

    enum UcodeF3DEX2
    {
        VTX = 0x01,
        MODIFYVTX = 0x02,
        CULLDL = 0x03,
        BRANCH_Z = 0x04,
        TRI1 = 0x05,
        TRI2 = 0x06,
        QUAD = 0x07,
        SPECIAL_3 = 0xD3,
        SPECIAL_2 = 0xD4,
        SPECIAL_1 = 0xD5,
        DMA_IO = 0xD6,
        TEXTURE = 0xD7,
        POPMTX = 0xD8,
        GEOMETRYMODE = 0xD9,
        MTX = 0xDA,
        MOVEWORD = 0xDB,
        MOVEMEM = 0xDC,
        LOAD_UCODE = 0xDD,
        DL = 0xDE,
        ENDDL = 0xDF,
        SPNOOP = 0xE0,
        RDPHALF_1 = 0xE1,
        SETOTHERMODE_L = 0xE2,
        SETOTHERMODE_H = 0xE3,
        RDPHALF_2 = 0xF1
    }

    enum Combiner
    {
        CCMUX_COMBINED = 0,
        CCMUX_TEXEL0 = 1,
        CCMUX_TEXEL1 = 2,
        CCMUX_PRIMITIVE = 3,
        CCMUX_SHADE = 4,
        CCMUX_ENVIRONMENT = 5,
        CCMUX_CENTER = 6,
        CCMUX_SCALE = 6,
        CCMUX_COMBINED_ALPHA = 7,
        CCMUX_TEXEL0_ALPHA = 8,
        CCMUX_TEXEL1_ALPHA = 9,
        CCMUX_PRIMITIVE_ALPHA = 10,
        CCMUX_SHADE_ALPHA = 11,
        CCMUX_ENV_ALPHA = 12,
        CCMUX_LOD_FRACTION = 13,
        CCMUX_PRIM_LOD_FRAC = 14,
        CCMUX_NOISE = 7,
        CCMUX_K4 = 7,
        CCMUX_K5 = 15,
        CCMUX_1 = 6,
        CCMUX_0 = 31,
        ACMUX_COMBINED = 0,
        ACMUX_TEXEL0 = 1,
        ACMUX_TEXEL1 = 2,
        ACMUX_PRIMITIVE = 3,
        ACMUX_SHADE = 4,
        ACMUX_ENVIRONMENT = 5,
        ACMUX_LOD_FRACTION = 0,
        ACMUX_PRIM_LOD_FRAC = 6,
        ACMUX_1 = 6,
        ACMUX_0 = 7
    }

    enum GeometryMode
    {
        ZBUFFER = 0x01,
        SHADE = 0x04,
        CULL_FRONT = 0x0200,
        CULL_BACK = 0x0400,
        CULL_BOTH = 0x0600,
        FOG = 0x010000,
        LIGHTING = 0x020000,
        TEXTURE_GEN = 0x040000,
        TEXTURE_GEN_LINEAR = 0x080000,
        LOD = 0x0100000,
        SHADING_SMOOTH = 0x0200000,
        CLIPPING = 0x0800000
    }

    enum OtherModeL
    {
        AA_EN = 0x08,
        Z_CMP = 0x10,
        Z_UPD = 0x20,
        IM_RD = 0x40,
        CLR_ON_CVG = 0x80,
        CVG_DST_WRAP = 0x100,
        CVG_DST_FULL = 0x200,
        CVG_DST_SAVE = 0x300,
        ZMODE_INTER = 0x400,
        ZMODE_XLU = 0x800,
        ZMODE_DEC = 0xC00,
        CVG_X_ALPHA = 0x1000,
        ALPHA_CVG_SEL = 0x2000,
        FORCE_BL = 0x4000
    }

    #endregion

    public static class UcodeSimulator
    {
        #region Variables, Structs

        public delegate void UcodeCommandDelegate(UInt32 w0, UInt32 w1);
        static UcodeCommandDelegate[] UcodeCommands = new UcodeCommandDelegate[256];
        public static byte DLCmd, EndDLCmd, RDPHalf1Cmd;
        public static int limbID = -1;
        public static List<short[]> limbtransformations = new List<short[]>();
        public static string currentfilename = "";
        public static UInt32[] textureoffsets = new uint[15];

        public struct VertexStruct
        {
            public Vector3d Position;
            public Vector2d TexCoord;
            public Color4 Colors;
            public Vector3d Normals;

            public UInt32 Address;
            public int InFileNumber;
        }

        public struct NTextureStruct
        {
            public UInt32 Address;
            public uint Format;
            public uint CMS, CMT;
            public uint LineSize;
            public uint Palette;
            public uint ShiftS, ShiftT;
            public uint MaskS, MaskT;
            public uint Tile;
            public uint ULS, ULT;
            public uint LRS, LRT;

            public uint Width, Height;
            public uint RealWidth, RealHeight;

            public float ScaleT, ScaleS;
            public float ShiftScaleT, ShiftScaleS;

            public int GLID;
        }

        public struct NPrimColor
        {
            public Color4 Color;
            public float L;
            public UInt32 M;

            public NPrimColor(Color4 color, float l, UInt16 m)
            {
                this.Color = color;
                this.L = l;
                this.M = m;
            }
        }

        public struct NGraphicsStruct
        {
            public int ActiveTexture;
            public bool IsMultiTexture;
            public Color4 FillColor, FogColor, BlendColor, EnvColor;
            public NPrimColor PrimColor;
            public UInt32 CombinerMux0, CombinerMux1;
            public UInt32 GeometryMode;
            public UInt32 RDPHalf1, RDPHalf2;

            public NTextureStruct[] Textures;
            public Color4[] Palette;
        }

        public struct DLCommandStruct
        {
            public byte ID;
            public UInt32 w0, w1;
            public string Name;
            public UInt32 Address;
            public int InFileNumber;
        }

        public struct DisplayListStruct
        {
            public bool Highlight;
            public int InFileNumber;
            public UInt32 StartAddress, EndAddress;
            public List<DLCommandStruct> Commands;
            public int GLID;
            public bool IsTransparent;
            public int Animation;
            public int TextureAnimation;
            public int ColorAnimation;
            public int Billboard;
            public short midX, midY, midZ;


            public Color4 PickColor;


        }

        public struct UnpackedCombinerStruct
        {
            public byte[] cA;
            public byte[] cB;
            public byte[] cC;
            public byte[] cD;
            public byte[] aA;
            public byte[] aB;
            public byte[] aC;
            public byte[] aD;
        }

        public struct FragmentCacheStruct
        {
            public UnpackedCombinerStruct UnpackedMux;
            public UInt32 Mux0, Mux1;
            public int GLID;
        }

        public struct TextureCacheStruct
        {
            public uint Format;
            public UInt32 Address;
            public uint RealWidth, RealHeight;
            public int GLID;
            public string Filename;
        }

        public delegate void MacroDelegate(UInt32[] w0, UInt32[] w1);
        public struct MacroStruct
        {
            public MacroDelegate Function;
            public byte[] Commands;
        }

        public struct MacroCacheStruct
        {
            public MacroDelegate Function;
            public UInt32[] NW0;
            public UInt32[] NW1;
            public UInt32 Address;
            public int InFileNumber;
        }

        public static NGraphicsStruct NGraphics;

        static VertexStruct[] CurrentVertices;
        public static List<FragmentCacheStruct> FragmentCache;
        public static List<TextureCacheStruct> TextureCache;

        public static List<DisplayListStruct> CurrentDLists;
        static DisplayListStruct CurrentDL;

        static List<MacroStruct> Macros;

        public static int FPHighlight;
        public static bool IsMacro = false;
        public static UInt32 TexAddr = 0;

        public static int ParseMode = 0;
        public static bool Wireframe = false;

        static bool EnableCombiner = true;

        static string[] NamesG = Enum.GetNames(typeof(UcodeG));
        static int[] ValuesG = (int[])Enum.GetValues(typeof(UcodeG));
        static string[] NamesF3DEX2 = Enum.GetNames(typeof(UcodeF3DEX2));
        static int[] ValuesF3DEX2 = (int[])Enum.GetValues(typeof(UcodeF3DEX2));

        static Random Rand = new Random((int)DateTime.Now.Ticks);

        public static void Initialize(Ucodes UcodeID)
        {
            TexUtil.InitTexturing();

            NGraphics = new NGraphicsStruct();
            NGraphics.Textures = new NTextureStruct[2];
            NGraphics.Palette = new Color4[256];

            CurrentDLists = new List<DisplayListStruct>();

            FragmentCache = new List<FragmentCacheStruct>();
            TextureCache = new List<TextureCacheStruct>();

            InitParser(UcodeID);
            /*
            byte[] HighlightBytes = Encoding.ASCII.GetBytes(
                "!!ARBfp1.0\n" +
                "OUTPUT FinalColor = result.color;\n" +
                "MOV FinalColor, {0.0, 1.0, 0.0, 0.3};\n" +
                "END");

            GL.Arb.GenProgram(1, out FPHighlight);
            GL.Arb.BindProgram(AssemblyProgramTargetArb.FragmentProgram, FPHighlight);
            GL.Arb.ProgramString(AssemblyProgramTargetArb.FragmentProgram, ArbVertexProgram.ProgramFormatAsciiArb,
                HighlightBytes.Length, HighlightBytes);
            */
            AddMacros();
        }

        #endregion

        #region Helpers

        public static void SetCombiner(bool OnOff)
        {
            EnableCombiner = OnOff;
        }

        public static Color4 RandomColor()
        {
            byte[] RandomBytes = new byte[4];
            Rand.NextBytes(RandomBytes);
            Color4 ReturnVal = new Color4(RandomBytes[0], RandomBytes[1], RandomBytes[2], 255);
            return ReturnVal;
        }

        public static uint ShiftL(UInt32 v, int s, int w)
        {
            return (uint)(((uint)v & (((uint)0x01 << w) - 1)) << s);
        }

        public static uint ShiftR(UInt32 v, int s, int w)
        {
            return (uint)(((uint)v >> s) & (((int)0x01 << w) - 1));
        }

        #endregion

        #region Display List Reader & Parser

        public static void InitParser(Ucodes UcodeID)
        {
            UcodeCommands.Init(new UcodeCommandDelegate(Ucode_NOOP));

            switch (UcodeID)
            {
                case Ucodes.F3DEX2:
                    UcodeCommands[(byte)UcodeF3DEX2.VTX] = new UcodeCommandDelegate(Ucode_F3DEX2_VTX);
                    UcodeCommands[(byte)UcodeF3DEX2.TRI1] = new UcodeCommandDelegate(Ucode_F3DEX2_TRI1);
                    UcodeCommands[(byte)UcodeF3DEX2.TRI2] = new UcodeCommandDelegate(Ucode_F3DEX2_TRI2);
                    UcodeCommands[(byte)UcodeF3DEX2.DL] = new UcodeCommandDelegate(Ucode_F3DEX2_DL);
                    UcodeCommands[(byte)UcodeF3DEX2.ENDDL] = new UcodeCommandDelegate(Ucode_F3DEX2_ENDDL);
                    UcodeCommands[(byte)UcodeF3DEX2.TEXTURE] = new UcodeCommandDelegate(Ucode_F3DEX2_TEXTURE);
                    UcodeCommands[(byte)UcodeG.SETTIMG] = new UcodeCommandDelegate(Ucode_G_SETTIMG);
                    UcodeCommands[(byte)UcodeG.SETTILE] = new UcodeCommandDelegate(Ucode_G_SETTILE);
                    UcodeCommands[(byte)UcodeG.SETTILESIZE] = new UcodeCommandDelegate(Ucode_G_SETTILESIZE);
                    UcodeCommands[(byte)UcodeG.LOADBLOCK] = new UcodeCommandDelegate(Ucode_G_LOADBLOCK);
                    UcodeCommands[(byte)UcodeG.LOADTLUT] = new UcodeCommandDelegate(Ucode_G_LOADTLUT);
                    UcodeCommands[(byte)UcodeG.SETCOMBINE] = new UcodeCommandDelegate(Ucode_G_SETCOMBINE);
                    UcodeCommands[(byte)UcodeG.SETFILLCOLOR] = new UcodeCommandDelegate(Ucode_G_SETFILLCOLOR);
                    UcodeCommands[(byte)UcodeG.SETFOGCOLOR] = new UcodeCommandDelegate(Ucode_G_SETFOGCOLOR);
                    UcodeCommands[(byte)UcodeG.SETBLENDCOLOR] = new UcodeCommandDelegate(Ucode_G_SETBLENDCOLOR);
                    UcodeCommands[(byte)UcodeG.SETPRIMCOLOR] = new UcodeCommandDelegate(Ucode_G_SETPRIMCOLOR);
                    UcodeCommands[(byte)UcodeG.SETENVCOLOR] = new UcodeCommandDelegate(Ucode_G_SETENVCOLOR);
                    UcodeCommands[(byte)UcodeF3DEX2.GEOMETRYMODE] = new UcodeCommandDelegate(Ucode_F3DEX2_GEOMETRYMODE);
                    UcodeCommands[(byte)UcodeF3DEX2.SETOTHERMODE_L] = new UcodeCommandDelegate(Ucode_F3DEX2_SETOTHERMODE_L);
                    UcodeCommands[(byte)UcodeF3DEX2.SETOTHERMODE_H] = new UcodeCommandDelegate(Ucode_F3DEX2_SETOTHERMODE_H);
                    UcodeCommands[(byte)UcodeF3DEX2.RDPHALF_1] = new UcodeCommandDelegate(Ucode_F3DEX2_RDPHALF_1);
                    UcodeCommands[(byte)UcodeF3DEX2.RDPHALF_2] = new UcodeCommandDelegate(Ucode_F3DEX2_RDPHALF_2);
                    UcodeCommands[(byte)UcodeF3DEX2.BRANCH_Z] = new UcodeCommandDelegate(Ucode_F3DEX2_BRANCH_Z);
                    UcodeCommands[(byte)UcodeF3DEX2.MTX] = new UcodeCommandDelegate(Ucode_F3DEX2_MTX);
                    UcodeCommands[(byte)UcodeF3DEX2.POPMTX] = new UcodeCommandDelegate(Ucode_F3DEX2_POPMTX);

                    DLCmd = (byte)UcodeF3DEX2.DL;
                    EndDLCmd = (byte)UcodeF3DEX2.ENDDL;
                    RDPHalf1Cmd = (byte)UcodeF3DEX2.RDPHALF_1;
                    break;
            }
        }

        public static void IdentifyCommand(byte Command, ref string NameTarget)
        {
            for (int i = 0; i < NamesG.Length; i++)
            {
                if (ValuesG[i] == Command)
                {
                    NameTarget = "G_" + NamesG[i];
                    return;
                }
            }

            for (int i = 0; i < NamesF3DEX2.Length; i++)
            {
                if (ValuesF3DEX2[i] == Command)
                {
                    NameTarget = "F3DEX2_" + NamesF3DEX2[i];
                    return;
                }
            }

            NameTarget = "Unidentified 0x" + Command.ToString("X2");
        }

        public static uint ReadDLCommands(int FileNumber, UInt32 Address, ref DisplayListStruct ThisDL)
        {
            DLCommandStruct ThisCommand = new DLCommandStruct();

            int Segment = (int)(Address >> 24);
            UInt32 Offset = (Address & 0x00FFFFFF);

            if (GameHandler.IsAddressValid(Address) == false) return Offset + 8;

            // Loop through the Display List in memory until we reach an EndDL command
            while (ThisCommand.ID != EndDLCmd)
            {
                // Take note of the command's data, type, name, memory location and file number
                ThisCommand.w0 = GameHandler.Read32(GameHandler.RAM[Segment].Data, Offset);
                ThisCommand.w1 = GameHandler.Read32(GameHandler.RAM[Segment].Data, Offset + 4);
                ThisCommand.ID = (byte)(ThisCommand.w0 >> 24);
                IdentifyCommand(ThisCommand.ID, ref ThisCommand.Name);
                ThisCommand.Address = (((UInt32)Segment << 24) | Offset);
                ThisCommand.InFileNumber = FileNumber;

                // Add the command to our Display List's command structure
                ThisDL.Commands.Add(ThisCommand);

                // If the command is a DL call or a RDP_HALF1 storage, go and process that Display List next
                if (ThisCommand.ID == DLCmd || ThisCommand.ID == RDPHalf1Cmd)
                {
                    ReadDLCommands(FileNumber, ThisCommand.w1, ref ThisDL);
                }

                Offset += 8;
            }

            return Offset;
        }

        public static uint ReadDL(int FileNumber, UInt32 Address, ref List<DisplayListStruct> DLists)
        {

            // Some sanity checking and data extraction from the RAM address
            if (GameHandler.IsAddressValid(Address) == false) return 0;

            

            // Prepare some temporary structures
            DisplayListStruct ThisDL = new DisplayListStruct();
            ThisDL.Commands = new List<DLCommandStruct>();

            // Add identification information to the Display List
            ThisDL.InFileNumber = FileNumber;
            ThisDL.StartAddress = Address;
            ThisDL.EndAddress = ((Address & 0xFF000000) | ReadDLCommands(FileNumber, Address, ref ThisDL));
            ThisDL.GLID = GL.GenLists(1);

            // Generate random color for picking
            ThisDL.PickColor = RandomColor();

            ThisDL.Animation = 0;

            ThisDL.TextureAnimation = 0;

            ThisDL.ColorAnimation = 0;

            ThisDL.Billboard = 0;

            ThisDL.midX = ThisDL.midY = ThisDL.midZ = 0;

            foreach (DLCommandStruct command in ThisDL.Commands)
            {
                // the dlist is transparent if prim color transparency is below 255
                if (ThisDL.IsTransparent == false && (command.ID == 0xFA) && (command.w1 & 0x000000FF) != 0xFF)
                {
                    ThisDL.IsTransparent = true;

                }
                /*
                if (ThisDL.IsTransparent == false && command.ID == 0xF5 && ((command.w0 & 0x0E0000 >> 21) == 0x03 || ((command.w0 & 0x0E0000 >> 21) == 0x0 && (command.w0 & 0x010000 >> 20) == 0x3)))
                {
                    ThisDL.IsTransparent = true;

                }*/

                if (ThisDL.Animation == 0 && command.ID == 0xDE && ((command.w1 & 0xFF000000) >> 24) >= 8 && ((command.w1 & 0xFF000000) >> 24) <= 0xE)
                {
                    ThisDL.Animation = (int) ((command.w1 & 0xFF000000) >> 24);

                    
                }

                if (ThisDL.TextureAnimation == 0 && command.ID == 0xFD && ((command.w1 & 0xFF000000) >> 24) >= 8 && ((command.w1 & 0xFF000000) >> 24) <= 0xE)
                {
                    ThisDL.TextureAnimation = (int)((command.w1 & 0xFF000000) >> 24);


                }
                if (ThisDL.ColorAnimation == 0 && command.ID == 0x00 && (command.w1 & 0xFFFFFF00) == 0x12345600 )
                {
                    ThisDL.ColorAnimation = (int)((command.w1 & 0x000000FF));
                }
                if (ThisDL.Billboard == 0 && command.ID == 0xDA && command.w1 == 0x01000000)
                {
                    ThisDL.Billboard = 1;
                }
                if (ThisDL.Billboard == 0 && command.ID == 0xDA && command.w1 == 0x01000040)
                {
                    ThisDL.Billboard = 2;
                }
                if (command.ID == 0 && ThisDL.Billboard != 0)
                {
                    ThisDL.midX = (short) (command.w0 & 0x0000FFFF);
                    ThisDL.midY = (short)((command.w1 & 0xFFFF0000) >> 16);
                    ThisDL.midZ = (short)((command.w1 & 0x0000FFFF));
                }
            }




            // Add the Display List to our DL list
            DLists.Add(ThisDL);



            return ThisDL.EndAddress;
        }

        public static void ParseAllDLs(ref List<DisplayListStruct> DLists)
        {

    
            // Clear primitive and environment colors
            NGraphics.PrimColor = new NPrimColor(new Color4(0.5f, 0.5f, 0.5f, 0.5f), 0.0f, 0);
            NGraphics.EnvColor = new Color4(0.5f, 0.5f, 0.5f, 0.5f);
            GL.Arb.ProgramEnvParameter4(AssemblyProgramTargetArb.FragmentProgram, 0, NGraphics.PrimColor.Color.R, NGraphics.PrimColor.Color.G, NGraphics.PrimColor.Color.B, NGraphics.PrimColor.Color.A);
            GL.Arb.ProgramEnvParameter4(AssemblyProgramTargetArb.FragmentProgram, 1, NGraphics.EnvColor.R, NGraphics.EnvColor.G, NGraphics.EnvColor.B, NGraphics.EnvColor.A);

            // For temporary access by other functions
            CurrentDLists = DLists;

            // Loop through each Display List for parsing
            foreach (DisplayListStruct ThisDL in CurrentDLists)
            {

                

                // Again, for temporary access
                CurrentDL = ThisDL;

                // Begin new OpenGL Display List
                GL.NewList(ThisDL.GLID, ListMode.CompileAndExecute);

                // Parse all commands of the Display List
              
                ParseDL(CurrentDL);

                // If the current Display List is supposed to be highlighted, parse its essential commands once more
                if (CurrentDL.Highlight == true && ParseMode != 2)
                {
                    ParseMode = 1;
                    ParseDL(CurrentDL);
                    ParseMode = 0;
                }

                // End the GL Display List
                GL.EndList();
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            }
        }

        // Used for macro detection
        private static bool CompareBytes(byte[] b1, byte[] b2)
        {
            return CompareBytes(b1, b2, 0);
        }

        private static bool CompareBytes(byte[] b1, byte[] b2, int ForceLength)
        {
            if (ForceLength == 0)
            {
                if (b1.Length != b2.Length)
                    return false;
                for (int i = 0; i < b1.Length; ++i)
                    if (b1[i] != b2[i])
                        return false;
            }
            else
            {
                if (b1.Length < ForceLength || b2.Length < ForceLength)
                    return false;
                for (int i = 0; i < ForceLength; ++i)
                    if (b1[i] != b2[i])
                        return false;
            }

            return true;
        }

        public static void ParseDL(DisplayListStruct ThisDL)
        {
            // See if we're told to parse everything, or just selectively
            if (ParseMode > 0)
            {
                if (ParseMode == 1)
                {
                    // Prepare highlight rendering
                    GL.Enable((EnableCap)All.FragmentProgram);
                    GL.Arb.BindProgram(AssemblyProgramTargetArb.FragmentProgram, FPHighlight);
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                }
                else if (ParseMode == 2)
                {
                    GL.Disable(EnableCap.Texture2D);
                    GL.Color4(ThisDL.PickColor);
                }

                GL.Disable(EnableCap.AlphaTest);
                GL.Disable(EnableCap.Lighting);

                // Go through every command in the current DList
                for (int i = 0; i < ThisDL.Commands.Count; i++)
                {
                    // If it's either DL/EndDL, GeometryMode or otherwise geometry-related, execute it
                    if (ThisDL.Commands[i].ID == (byte)UcodeF3DEX2.VTX ||
                        ThisDL.Commands[i].ID == (byte)UcodeF3DEX2.TRI1 ||
                        ThisDL.Commands[i].ID == (byte)UcodeF3DEX2.TRI2 ||
                        ThisDL.Commands[i].ID == (byte)UcodeF3DEX2.DL ||
                        ThisDL.Commands[i].ID == (byte)UcodeF3DEX2.ENDDL ||
                        ThisDL.Commands[i].ID == (byte)UcodeF3DEX2.GEOMETRYMODE ||
                        ThisDL.Commands[i].ID == (byte)UcodeF3DEX2.MODIFYVTX ||
                        ThisDL.Commands[i].ID == (byte)UcodeF3DEX2.MTX ||
                        ThisDL.Commands[i].ID == (byte)UcodeF3DEX2.POPMTX)
                    {
                       
                        UcodeCommands[ThisDL.Commands[i].ID](ThisDL.Commands[i].w0, ThisDL.Commands[i].w1);
                    }
                    //   else if (ThisDL.InFileNumber == 0x06) DebugConsole.WriteLine(ThisDL.Commands[i].w0.ToString("X") + " " + ThisDL.Commands[i].w1.ToString("X"));
                  // DebugConsole.WriteLine(ThisDL.Commands[i].w0.ToString("X8") + " " + ThisDL.Commands[i].w1.ToString("X8"));
                }

               // if (ThisDL.InFileNumber == 0x06) DebugConsole.WriteLine("amount of commands: " + ThisDL.Commands.Count);

                // Reset some changed states
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Lighting);
                GL.Enable(EnableCap.AlphaTest);
                GL.Disable((EnableCap)All.FragmentProgram);
            }
            else
            {
                // Go through every command in the current DList
                for (int i = 0; i < ThisDL.Commands.Count; i++)
                {
                    if (i < ThisDL.Commands.Count - Macros.Max(a => a.Commands.Length))
                    {
                        foreach (MacroStruct ThisMacro in Macros)
                        {
                            // Get the next commands from the current position in the DList
                            byte[] NextCmds = new byte[ThisMacro.Commands.Length];
                            UInt32[] NextW0 = new UInt32[ThisMacro.Commands.Length + 1];
                            UInt32[] NextW1 = new UInt32[ThisMacro.Commands.Length + 1];

                            for (int j = 0; j <= ThisMacro.Commands.Length; j++)
                            {
                                if (j != ThisMacro.Commands.Length) NextCmds[j] = ThisDL.Commands[i + j].ID;
                                NextW0[j] = ThisDL.Commands[i + j].w0;
                                NextW1[j] = ThisDL.Commands[i + j].w1;

                            }




                            // Compare the bytes to the macro and execute it if we have a match
                            if (CompareBytes(NextCmds, ThisMacro.Commands))
                            {
                                IsMacro = true;
                                ThisMacro.Function(NextW0, NextW1);
                                i += ThisMacro.Commands.Length - 1;
                                break;
                            }
                        }
                        IsMacro = false;
                    }

                    // Execute the next command
                    UcodeCommands[ThisDL.Commands[i].ID](ThisDL.Commands[i].w0, ThisDL.Commands[i].w1);
                   // DebugConsole.WriteLine(ThisDL.Commands[i].w0.ToString("X8") + " " + ThisDL.Commands[i].w1.ToString("X8"));
                    // if (ThisDL.InFileNumber == 0x03) DebugConsole.WriteLine(ThisDL.Commands[i].w0.ToString("X8") + " " + ThisDL.Commands[i].w1.ToString("X8"));
                }
            }



            return;
        }

        #endregion

        #region Macro Functions

        private static void AddMacros()
        {
            Macros = new List<MacroStruct>();

            Macros.Add(new MacroStruct()
            {
                Function = MacroLoadTextureBlock,
                Commands = new byte[] {
                    (byte)UcodeG.SETTIMG,
                    (byte)UcodeG.SETTILE,
                    (byte)UcodeG.RDPLOADSYNC,
                    (byte)UcodeG.LOADBLOCK,
                    (byte)UcodeG.RDPPIPESYNC,
                    (byte)UcodeG.SETTILE,
                    (byte)UcodeG.SETTILESIZE
                }
            });

            Macros.Add(new MacroStruct()
            {
                Function = MacroLoadTLUT,
                Commands = new byte[] {
                    (byte)UcodeG.SETTIMG,
                    (byte)UcodeG.RDPTILESYNC,
                    (byte)UcodeG.SETTILE,
                    (byte)UcodeG.RDPLOADSYNC,
                    (byte)UcodeG.LOADTLUT,
                    (byte)UcodeG.RDPPIPESYNC
                }
            });

            // this seems to be zobjs exported with fast64
            Macros.Add(new MacroStruct()
            {
                Function = MacroLoadTLUT,
                Commands = new byte[] {
                    (byte)UcodeG.SETTIMG,
                    (byte)UcodeG.SETTILE,
                    (byte)UcodeG.LOADTLUT,
                    (byte)UcodeG.SETTIMG,
                    (byte)UcodeG.SETTILE,
                    (byte)UcodeG.LOADBLOCK,
                    (byte)UcodeG.SETTILE,
                    (byte)UcodeG.SETTILESIZE
                }
            });
        }

        private static void MacroLoadTextureBlock(UInt32[] w0, UInt32[] w1)
        {
            if ((w1[5] & 0x0F000000) == 0)
            {
                NGraphics.ActiveTexture = 0;
                NGraphics.IsMultiTexture = false;
            }
            else
            {
                NGraphics.ActiveTexture = 1;
                NGraphics.IsMultiTexture = true;
            }

            Ucode_G_SETTIMG(w0[0], w1[0]);
            Ucode_G_SETTILE(w0[5], w1[5]);
            Ucode_G_SETTILESIZE(w0[6], w1[6]);

            // Texture is CI-type texture; palette will be loaded next
            if (NGraphics.Textures[NGraphics.ActiveTexture].Format == 0x40 ||
                NGraphics.Textures[NGraphics.ActiveTexture].Format == 0x48 ||
                NGraphics.Textures[NGraphics.ActiveTexture].Format == 0x50)
            {
                if ((w0[7] >> 24) == (byte)UcodeG.SETTIMG) return;
            }

            InitLoadTexture();
        }

        private static void MacroLoadTLUT(UInt32[] w0, UInt32[] w1)
        {
            Ucode_G_SETTIMG(w0[0], w1[0]);
            Ucode_G_LOADTLUT(w0[4], w1[4]);

            InitLoadTexture();
        }

        #endregion

        #region Primitive Drawing Functions

        private static void TexGen(ref Vector2d TexCoord, Vector3d Normals)
        {
            if (Convert.ToBoolean(NGraphics.GeometryMode & (UInt32)GeometryMode.TEXTURE_GEN_LINEAR) == true)
            {
                TexCoord.X = Math.Asin(Normals.X);
                TexCoord.Y = Math.Asin(Normals.Y);
            }
            else
            {
                TexCoord.X = 0.5f / (1.0f + Normals.X);
                TexCoord.Y = 0.5f / (1.0f - Normals.Y);
            }
        }

        public static void RenderTriangles(int[] Indices, ref VertexStruct[] Vertices)
        {
            GL.Begin(BeginMode.Triangles);

            foreach (int ThisIndex in Indices)
            {
                if (Convert.ToBoolean(NGraphics.GeometryMode & (UInt32)GeometryMode.TEXTURE_GEN) == true)
                {
                    Vertices[ThisIndex].Normals.Normalize();

                    TexGen(ref Vertices[ThisIndex].TexCoord, Vertices[ThisIndex].Normals);

                    if (EnableCombiner == true)
                    {
                        GL.Arb.MultiTexCoord2(TextureUnit.Texture0, Vertices[ThisIndex].TexCoord.X, Vertices[ThisIndex].TexCoord.Y);
                        GL.Arb.MultiTexCoord2(TextureUnit.Texture1, Vertices[ThisIndex].TexCoord.X, Vertices[ThisIndex].TexCoord.Y);
                    }
                    else
                    {
                        GL.TexCoord2(Vertices[ThisIndex].TexCoord.X, Vertices[ThisIndex].TexCoord.Y);
                    }
                }
                else
                {

                    if (Vertices == null) continue; //failsafe

                    double S0 = Vertices[ThisIndex].TexCoord.X * (NGraphics.Textures[0].ScaleS * NGraphics.Textures[0].ShiftScaleS) / 32.0f / NGraphics.Textures[0].RealWidth;
                    double T0 = Vertices[ThisIndex].TexCoord.Y * (NGraphics.Textures[0].ScaleT * NGraphics.Textures[0].ShiftScaleT) / 32.0f / NGraphics.Textures[0].RealHeight;
                    double S1 = Vertices[ThisIndex].TexCoord.X * (NGraphics.Textures[1].ScaleS * NGraphics.Textures[1].ShiftScaleS) / 32.0f / NGraphics.Textures[1].RealWidth;
                    double T1 = Vertices[ThisIndex].TexCoord.Y * (NGraphics.Textures[1].ScaleT * NGraphics.Textures[1].ShiftScaleT) / 32.0f / NGraphics.Textures[1].RealHeight;

                    if (EnableCombiner == true)
                    {
                        GL.Arb.MultiTexCoord2(TextureUnit.Texture0, S0, T0);
                        GL.Arb.MultiTexCoord2(TextureUnit.Texture1, S1, T1);
                    }
                    else
                    {
                        GL.TexCoord2(S0, T0);
                    }
                }

                if (ParseMode != 2)
                {
                    if (Convert.ToBoolean(NGraphics.GeometryMode & (UInt32)GeometryMode.LIGHTING) == false)
                    {
                        GL.Color4(Vertices[ThisIndex].Colors.R, Vertices[ThisIndex].Colors.G, Vertices[ThisIndex].Colors.B, Vertices[ThisIndex].Colors.A);
                    }
                    else
                    {
                        GL.Color4(1, 1, 1, Vertices[ThisIndex].Colors.A);
                    }

                    GL.Normal3(Vertices[ThisIndex].Normals.X, Vertices[ThisIndex].Normals.Y, Vertices[ThisIndex].Normals.Z);
                }

                GL.Vertex3(Vertices[ThisIndex].Position.X, Vertices[ThisIndex].Position.Y, Vertices[ThisIndex].Position.Z);
            }

            GL.End();

        }

        #endregion

        #region Ucode Commands

        private static void Ucode_NOOP(UInt32 w0, UInt32 w1)
        {
            //
        }

        private static void Ucode_F3DEX2_VTX(UInt32 w0, UInt32 w1)
        {
            byte N = (byte)((w0 >> 12) & 0xFF);
            byte V0 = (byte)(((w0 >> 1) & 0x7F) - N);

            if (N > 32 || V0 > 32 || GameHandler.IsAddressValid(w1) == false) return;

            if (V0 == 0) CurrentVertices = new VertexStruct[32];

            int VSegment = (int)(w1 >> 24);
            UInt32 VOffset = (w1 & 0x00FFFFFF);

            double[] modifier = new double[] { 0, 0, 0 };
            
            
            if (limbID != -1 && limbtransformations.Count > 0) modifier = new double[]
            {
                -limbtransformations[limbtransformations.Count - 1][0] ,
                -limbtransformations[limbtransformations.Count - 1][1] ,
                -limbtransformations[limbtransformations.Count - 1][2]
            };



            for (int i = 0; i < N; i++)
            {
                CurrentVertices[V0 + i] = (new VertexStruct()
                {
                    Position = new Vector3d((double)GameHandler.Read16S(GameHandler.RAM[VSegment].Data, VOffset) + modifier[0], (double)GameHandler.Read16S(GameHandler.RAM[VSegment].Data, VOffset + 2) + modifier[1], (double)GameHandler.Read16S(GameHandler.RAM[VSegment].Data, VOffset + 4) + modifier[2]),
                    TexCoord = new Vector2d(GameHandler.Read16S(GameHandler.RAM[VSegment].Data, VOffset + 8), GameHandler.Read16S(GameHandler.RAM[VSegment].Data, VOffset + 10)),
                    Colors = new Color4(GameHandler.Read8(GameHandler.RAM[VSegment].Data, VOffset + 12), GameHandler.Read8(GameHandler.RAM[VSegment].Data, VOffset + 13), GameHandler.Read8(GameHandler.RAM[VSegment].Data, VOffset + 14), GameHandler.Read8(GameHandler.RAM[VSegment].Data, VOffset + 15)),
                    Normals = new Vector3d((double)GameHandler.Read8(GameHandler.RAM[VSegment].Data, VOffset + 12), (double)GameHandler.Read8(GameHandler.RAM[VSegment].Data, VOffset + 13), (double)GameHandler.Read8(GameHandler.RAM[VSegment].Data, VOffset + 14)),
                    Address = ((uint)VSegment << 24 | VOffset),
                    InFileNumber = CurrentDL.InFileNumber
                });

                VOffset += 16;
            }
        }

        private static void Ucode_F3DEX2_TRI1(UInt32 w0, UInt32 w1)
        {
            int[] TriangleVertices = new int[]
            {
                (int)((w0 & 0x00FF0000) >> 16) >> 1,
                (int)((w0 & 0x0000FF00) >> 8) >> 1,
                (int)(w0 & 0x000000FF) >> 1
            };

            RenderTriangles(TriangleVertices, ref CurrentVertices);
        }

        private static void Ucode_F3DEX2_TRI2(UInt32 w0, UInt32 w1)
        {
            int[] TriangleVertices = new int[]
            {
                (int)((w0 & 0x00FF0000) >> 16) >> 1,
                (int)((w0 & 0x0000FF00) >> 8) >> 1,
                (int)(w0 & 0x000000FF) >> 1,
                (int)((w1 & 0x00FF0000) >> 16) >> 1,
                (int)((w1 & 0x0000FF00) >> 8) >> 1,
                (int)(w1 & 0x000000FF) >> 1
            };

            RenderTriangles(TriangleVertices, ref CurrentVertices);
        }

        private static void Ucode_F3DEX2_DL(UInt32 w0, UInt32 w1)
        {
            // Interpreting this would mess up the Display List selector, so don't
        }

        private static void Ucode_F3DEX2_ENDDL(UInt32 w0, UInt32 w1)
        {
            //
        }

        private static void Ucode_F3DEX2_TEXTURE(UInt32 w0, UInt32 w1)
        {
            NGraphics.ActiveTexture = 0;
            NGraphics.IsMultiTexture = false;

            if (ShiftR(w1, 16, 16) < 0xFFFF)
            {
                NGraphics.Textures[0].ScaleS = (float)(ShiftR(w1, 16, 16));
            }
            else
            {
                NGraphics.Textures[0].ScaleS = 1.0f;
            }

            if (ShiftR(w1, 0, 16) < 0xFFFF)
            {
                NGraphics.Textures[0].ScaleT = (float)(ShiftR(w1, 0, 16));
            }
            else
            {
                NGraphics.Textures[0].ScaleT = 1.0f;
            }

            NGraphics.Textures[1].ScaleS = NGraphics.Textures[0].ScaleS;
            NGraphics.Textures[1].ScaleT = NGraphics.Textures[0].ScaleT;
        }

        private static void Ucode_G_SETTIMG(UInt32 w0, UInt32 w1)
        {

            if (IsMacro)
                TexAddr = w1;
            else
                NGraphics.Textures[NGraphics.ActiveTexture].Address = w1;


        }

        private static void Ucode_G_SETTILE(UInt32 w0, UInt32 w1)
        {
            if (IsMacro)
                NGraphics.Textures[NGraphics.ActiveTexture].Address = TexAddr;

            NGraphics.Textures[NGraphics.ActiveTexture].Format = ((w0 & 0xFF0000) >> 16);
            NGraphics.Textures[NGraphics.ActiveTexture].CMS = ShiftR(w1, 8, 2);
            NGraphics.Textures[NGraphics.ActiveTexture].CMT = ShiftR(w1, 18, 2);
            NGraphics.Textures[NGraphics.ActiveTexture].LineSize = ShiftR(w0, 9, 9);
            NGraphics.Textures[NGraphics.ActiveTexture].Palette = ShiftR(w1, 20, 4);
            NGraphics.Textures[NGraphics.ActiveTexture].ShiftS = ShiftR(w1, 0, 4);
            NGraphics.Textures[NGraphics.ActiveTexture].ShiftT = ShiftR(w1, 10, 4);
            NGraphics.Textures[NGraphics.ActiveTexture].MaskS = ShiftR(w1, 4, 4);
            NGraphics.Textures[NGraphics.ActiveTexture].MaskT = ShiftR(w1, 14, 4);
        }

        private static void Ucode_G_SETTILESIZE(UInt32 w0, UInt32 w1)
        {
            uint ULS = ShiftR(w0, 12, 12);
            uint ULT = ShiftR(w0, 0, 12);
            uint LRS = ShiftR(w1, 12, 12);
            uint LRT = ShiftR(w1, 0, 12);

            NGraphics.Textures[NGraphics.ActiveTexture].Tile = ShiftR(w1, 24, 3);
            NGraphics.Textures[NGraphics.ActiveTexture].ULS = ShiftR(ULS, 2, 10);
            NGraphics.Textures[NGraphics.ActiveTexture].ULT = ShiftR(ULT, 2, 10);
            NGraphics.Textures[NGraphics.ActiveTexture].LRS = ShiftR(LRS, 2, 10);
            NGraphics.Textures[NGraphics.ActiveTexture].LRT = ShiftR(LRT, 2, 10);
        }

        private static void Ucode_G_LOADBLOCK(UInt32 w0, UInt32 w1)
        {
            Ucode_G_SETTILESIZE(w0, w1);
        }

        private static void Ucode_G_LOADTLUT(UInt32 w0, UInt32 w1)
        {
            if (GameHandler.IsAddressValid(TexAddr) == false) return;

            UInt32 PaletteSegment = ((TexAddr & 0xFF000000) >> 24);
            UInt32 PaletteOffset = (TexAddr & 0x00FFFFFF);

            uint PalSize = ((w1 & 0x00FFF000) >> 14) + 1;

            for (int i = 0; i < PalSize; i++)
            {
                UInt16 Raw = (UInt16)((GameHandler.RAM[PaletteSegment].Data[PaletteOffset] << 8) | GameHandler.RAM[PaletteSegment].Data[PaletteOffset + 1]);

                NGraphics.Palette[i].R = (byte)((Raw & 0xF800) >> 8);
                NGraphics.Palette[i].G = (byte)(((Raw & 0x07C0) << 5) >> 8);
                NGraphics.Palette[i].B = (byte)(((Raw & 0x003E) << 18) >> 16);
                NGraphics.Palette[i].A = 0;
                if ((Raw & 0x0001) == 1) NGraphics.Palette[i].A = 0xFF;

                PaletteOffset += 2;
            }
        }

        private static void Ucode_G_SETCOMBINE(UInt32 w0, UInt32 w1)
        {
            NGraphics.CombinerMux0 = (w0 & 0x00FFFFFF);
            NGraphics.CombinerMux1 = w1;

            CheckFragmentCache(NGraphics.CombinerMux0, NGraphics.CombinerMux1);
        }

        private static void Ucode_G_SETFILLCOLOR(UInt32 w0, UInt32 w1)
        {
            NGraphics.FillColor.R = ShiftR(w1, 11, 5) * 0.032258064f;
            NGraphics.FillColor.G = ShiftR(w1, 6, 5) * 0.032258064f;
            NGraphics.FillColor.B = ShiftR(w1, 1, 5) * 0.032258064f;
            NGraphics.FillColor.A = ShiftR(w1, 0, 1);
        }

        private static void Ucode_G_SETFOGCOLOR(UInt32 w0, UInt32 w1)
        {
            NGraphics.FogColor.R = ShiftR(w1, 24, 8) * 0.0039215689f;
            NGraphics.FogColor.G = ShiftR(w1, 16, 8) * 0.0039215689f;
            NGraphics.FogColor.B = ShiftR(w1, 8, 8) * 0.0039215689f;
            NGraphics.FogColor.A = ShiftR(w1, 0, 8) * 0.0039215689f;
        }

        private static void Ucode_G_SETBLENDCOLOR(UInt32 w0, UInt32 w1)
        {
            NGraphics.BlendColor.R = ShiftR(w1, 24, 8) * 0.0039215689f;
            NGraphics.BlendColor.G = ShiftR(w1, 16, 8) * 0.0039215689f;
            NGraphics.BlendColor.B = ShiftR(w1, 8, 8) * 0.0039215689f;
            NGraphics.BlendColor.A = ShiftR(w1, 0, 8) * 0.0039215689f;
        }

        private static void Ucode_G_SETPRIMCOLOR(UInt32 w0, UInt32 w1)
        {
            NGraphics.PrimColor.Color.R = ShiftR(w1, 24, 8) * 0.0039215689f;
            NGraphics.PrimColor.Color.G = ShiftR(w1, 16, 8) * 0.0039215689f;
            NGraphics.PrimColor.Color.B = ShiftR(w1, 8, 8) * 0.0039215689f;
            NGraphics.PrimColor.Color.A = ShiftR(w1, 0, 8) * 0.0039215689f;

            NGraphics.PrimColor.M = ShiftL(w0, 8, 8);
            NGraphics.PrimColor.L = ShiftL(w0, 0, 8) * 0.0039215689f;

            GL.Arb.ProgramEnvParameter4(AssemblyProgramTargetArb.FragmentProgram, 0, NGraphics.PrimColor.Color.R, NGraphics.PrimColor.Color.G, NGraphics.PrimColor.Color.B, NGraphics.PrimColor.Color.A);
            GL.Arb.ProgramEnvParameter4(AssemblyProgramTargetArb.FragmentProgram, 2, NGraphics.PrimColor.L, NGraphics.PrimColor.L, NGraphics.PrimColor.L, NGraphics.PrimColor.L);
        }

        private static void Ucode_G_SETENVCOLOR(UInt32 w0, UInt32 w1)
        {
            NGraphics.EnvColor.R = ShiftR(w1, 24, 8) * 0.0039215689f;
            NGraphics.EnvColor.G = ShiftR(w1, 16, 8) * 0.0039215689f;
            NGraphics.EnvColor.B = ShiftR(w1, 8, 8) * 0.0039215689f;
            NGraphics.EnvColor.A = ShiftR(w1, 0, 8) * 0.0039215689f;

            GL.Arb.ProgramEnvParameter4(AssemblyProgramTargetArb.FragmentProgram, 1, NGraphics.EnvColor.R, NGraphics.EnvColor.G, NGraphics.EnvColor.B, NGraphics.EnvColor.A);
        }

        private static void Ucode_F3DEX2_GEOMETRYMODE(UInt32 w0, UInt32 w1)
        {
            UInt32 Clear = ~(w0 & 0x00FFFFFF);
            UInt32 Set = (w1 & 0x00FFFFFF);

            NGraphics.GeometryMode = (NGraphics.GeometryMode & ~Clear) | Set;

            if (Convert.ToBoolean(NGraphics.GeometryMode & (UInt32)GeometryMode.CULL_BOTH) == true)
            {
                // Enable face culling
                GL.Enable(EnableCap.CullFace);

                if (Convert.ToBoolean(NGraphics.GeometryMode & (UInt32)GeometryMode.CULL_BACK) == true)
                    // Set backface culling
                    GL.CullFace(CullFaceMode.Back);
                else
                    // Set frontface culling
                    GL.CullFace(CullFaceMode.Front);
            }
            else
            {
                // Disable face culling
                GL.Disable(EnableCap.CullFace);
            }

            // If we're parsing everything, execute the following block, too...
            if (ParseMode == 0)
            {
                if (Convert.ToBoolean(NGraphics.GeometryMode & (UInt32)GeometryMode.LIGHTING) == true)
                {
                    // Enable lighting and normalization
                    GL.Enable(EnableCap.Normalize);

                    // if (!MainForm.settings.OldRenderFormula)
                    //     GL.Disable(EnableCap.Lighting); 
                    //else
                    GL.Enable(EnableCap.Lighting);
  

                }
                else
                {
                    // Disable lighting and normalization
                    GL.Disable(EnableCap.Normalize);
                    GL.Disable(EnableCap.Lighting);
                }
            }
        }

        private static void Ucode_F3DEX2_SETOTHERMODE_L(UInt32 w0, UInt32 w1)
        {
            byte MDSFT = (byte)(32 - ((w0 & 0x00FFFFFF) << 4 >> 4) - 1);

            switch (MDSFT)
            {
                case 3:
                    // Render mode
                    {
                        if (Convert.ToBoolean(w1 & (uint)OtherModeL.ZMODE_DEC) == true)
                        {
                            // Enable decal mode (ex. pathways in Hyrule Field, Kokiri Forest, etc.)
                            GL.Enable(EnableCap.PolygonOffsetFill);
                            GL.PolygonOffset(-0.5f, -0.5f);
                        }
                        else
                        {
                            // Disable decal mode
                            GL.Disable(EnableCap.PolygonOffsetFill);
                        }

                        if ((Convert.ToBoolean(w1 & (uint)OtherModeL.CVG_X_ALPHA) == true) ||
                            (Convert.ToBoolean(w1 & (uint)OtherModeL.ALPHA_CVG_SEL) == true))
                        {
                            // Enable alpha testing, disable blending
                            GL.AlphaFunc(AlphaFunction.Gequal, 0.4f);
                            GL.Enable(EnableCap.AlphaTest);
                            GL.Disable(EnableCap.Blend);
                        }
                        else if (Convert.ToBoolean(w1 & (uint)OtherModeL.FORCE_BL) == true)
                        {
                            // Force blending
                            ForceBlending(w0, w1);
                        }
                        else
                        {
                            // Disable blending
                            GL.Disable(EnableCap.Blend);
                        }

                        if (Convert.ToBoolean(w1 & (uint)OtherModeL.Z_CMP) == true)
                            // Enable depth testing
                            //GL.Enable(EnableCap.DepthTest);
                            GL.DepthFunc(DepthFunction.Lequal);
                        else
                            // Disable depth testing
                            //GL.Disable(EnableCap.DepthTest);
                            GL.DepthFunc(DepthFunction.Always);

                        if (Convert.ToBoolean(w1 & (uint)OtherModeL.Z_UPD) == true)
                            GL.DepthMask(true);
                        else
                            GL.DepthMask(false);

                        break;
                    }
            }
        }

        private static void Ucode_F3DEX2_SETOTHERMODE_H(UInt32 w0, UInt32 w1)
        {

           // byte MDSFT = (byte)(32 - ((w0 & 0x00FFFFFF) << 4 >> 4) - 1);

            byte MDSFT = (byte)((w0 & 0x0000FF00)>> 8);

            switch (MDSFT)
            {
                case 0x12:
                {

                        if ((w1 & 0x3000) == 0)
                        {
                            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
                        }
                        else
                        {
                            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
                        }

                        break;
                }
            }
         //   if (MDSFT != 0) DebugConsole.WriteLine(MDSFT);

                 
        }

        private static void Ucode_F3DEX2_RDPHALF_1(UInt32 w0, UInt32 w1)
        {
            NGraphics.RDPHalf1 = w1;
        }

        private static void Ucode_F3DEX2_RDPHALF_2(UInt32 w0, UInt32 w1)
        {
            NGraphics.RDPHalf2 = w1;
        }

        private static void Ucode_F3DEX2_BRANCH_Z(UInt32 w0, UInt32 w1)
        {
            if (GameHandler.IsAddressValid(NGraphics.RDPHalf1) == false) return;

            uint Vtx = ShiftR(w0, 1, 11);
            short ZVal = (short)w1;

            if (CurrentVertices[Vtx].Position.Z < ZVal)
            {
                DisplayListStruct BranchDL = new DisplayListStruct();
                BranchDL.Commands = new List<DLCommandStruct>();
                ReadDLCommands(CurrentDL.InFileNumber, NGraphics.RDPHalf1, ref BranchDL);
                ParseDL(BranchDL);
            }
        }

        private static void Ucode_F3DEX2_MTX(UInt32 w0, UInt32 w1)
        {
           
            if (limbtransformations.Count == 0) return;
            if (limbID != -1)
            {
                limbID = -1;
                //DebugConsole.WriteLine("Rendering the rest of the vertex");
                return;
            };

            

            // Get address elements
            int Segment = (int)(w1 >> 24);
            UInt32 Offset = (w1 & 0x00FFFFFF);

            if (Offset != 0) limbID = (int)(Offset / 0x40);
            else limbID = 0;

            

           // DebugConsole.WriteLine("Rendering matrix vertex of limb " + limbtransformations.Count + "joining to " + limbID);

            if (limbID > limbtransformations.Count - 1) {
                limbID = 0; 
                //DebugConsole.WriteLine("wtf? joining to 0 instead"); 
            }

           

            // If matrix is supposed to be read from RDRAM, pop current matrix stack and return
            if (Segment == 0x80) // || Segment == 0xD
            {
                GL.PopMatrix();
                return;
            }

          //  DebugConsole.WriteLine("rendering matrix");

            // If matrix address is invalid, return
        //    if (GameHandler.IsAddressValid(w1) == false) return;



            // Prepare variables for reading
            int MtxTemp1 = 0, MtxTemp2 = 0;
            double[] TempMatrix = new double[16];

            // Conversion loop
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
               //     MtxTemp1 = GameHandler.Read16(GameHandler.RAM[Segment].Data, Offset);
               //     MtxTemp2 = GameHandler.Read16(GameHandler.RAM[Segment].Data, Offset + 32);
                     TempMatrix[(x * 4) + y] = ((MtxTemp1 << 16) | MtxTemp2) * (1.0f / 65536.0f);
                    Offset += 2;
                }
            }


            // Push current matrix to stack, then multiply with the matrix just read
          //  GL.PushMatrix();
           // GL.MultMatrix(new double[] { 1,2, 3, 4, 5, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1 });
           // GL.Translate(111,0,0);
          // GL.MultMatrix(TempMatrix);
            

        }

        private static void Ucode_F3DEX2_POPMTX(UInt32 w0, UInt32 w1)
        {
            // Because MTX is simulated via OZMAV-style matrix faking, just pop the OpenGL matrix stack
            GL.PopMatrix();
        }

        #endregion

        #region Texture Loading

        private static void InitLoadTexture()
        {



            UInt32 TextureSegment = ((NGraphics.Textures[0].Address & 0xFF000000) >> 24);


            if (CurrentDL.TextureAnimation >= 8 && !currentfilename.Contains(".zobj"))
            {
                DebugConsole.WriteLine($"Special texture " + TextureSegment);

                NGraphics.Textures[0].GLID = GameHandler.InvalidTextureID;
                NGraphics.Textures[1].GLID = GameHandler.InvalidTextureID;

                GL.ActiveTexture(TextureUnit.Texture1);
                GL.Disable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, 0);


                if (NGraphics.IsMultiTexture == true)
                {
                    CalculateTextureSize(1);
                    GL.ActiveTexture(TextureUnit.Texture1);
                    GL.Enable(EnableCap.Texture2D);
                    GL.BindTexture(TextureTarget.Texture2D, CheckTextureCache(1));

                    GL.Disable(EnableCap.Texture2D);
                }
                else
                {
                    GL.ActiveTexture(TextureUnit.Texture1);
                    GL.Disable(EnableCap.Texture2D);
                }

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.Disable(EnableCap.Texture2D);

                return;
            }

            NGraphics.Textures[0].GLID = GameHandler.InvalidTextureID;
            NGraphics.Textures[1].GLID = GameHandler.InvalidTextureID;

            

            if (EnableCombiner)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.Disable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.ActiveTexture(TextureUnit.Texture1);
                GL.Disable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, 0);

                CalculateTextureSize(0);
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, CheckTextureCache(0));


        //        GL.BindTexture(TextureTarget.Texture2D, UcodeSimulator.NGraphics.Textures[0].GLID);


                if (NGraphics.IsMultiTexture == true)
                {
                    CalculateTextureSize(1);
                    GL.ActiveTexture(TextureUnit.Texture1);
                    GL.Enable(EnableCap.Texture2D);
                    GL.BindTexture(TextureTarget.Texture2D, CheckTextureCache(1));

                    GL.Disable(EnableCap.Texture2D);
                }
                else
                {
                    GL.ActiveTexture(TextureUnit.Texture1);
                    GL.Disable(EnableCap.Texture2D);
                }

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.Disable(EnableCap.Texture2D);
            }
            else
            {


                CalculateTextureSize(0);
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, CheckTextureCache(0));
            }
        }

        public static void CalculateTextureSize(int ActiveTexture)
        {
            int MaxTexel = 0, LineShift = 0;

            switch (NGraphics.Textures[ActiveTexture].Format)
            {
                /* 4-bit */
                case 0x00:
                    // RGBA
                    MaxTexel = 4096; LineShift = 4;
                    break;
                case 0x40:
                    // CI
                    MaxTexel = 4096; LineShift = 4;
                    break;
                case 0x60:
                    // IA
                    MaxTexel = 8192; LineShift = 4;
                    break;
                case 0x80:
                    // I
                    MaxTexel = 8192; LineShift = 4;
                    break;

                /* 8-bit */
                case 0x08:
                    // RGBA
                    MaxTexel = 2048; LineShift = 3;
                    break;
                case 0x48:
                    // CI
                    MaxTexel = 2048; LineShift = 3;
                    break;
                case 0x68:
                    // IA
                    MaxTexel = 4096; LineShift = 3;
                    break;
                case 0x88:
                    // I
                    MaxTexel = 4096; LineShift = 3;
                    break;

                /* 16-bit */
                case 0x10:
                    // RGBA
                    MaxTexel = 2048; LineShift = 2;
                    break;
                case 0x50:
                    // CI
                    MaxTexel = 2048; LineShift = 0;
                    break;
                case 0x70:
                    // IA
                    MaxTexel = 2048; LineShift = 2;
                    break;
                case 0x90:
                    // I
                    MaxTexel = 2048; LineShift = 0;
                    break;

                /* 32-bit */
                case 0x18:
                    // RGBA
                    MaxTexel = 1024; LineShift = 2;
                    break;

                default:
                    return;
            }

            int Line_Width = ((int)NGraphics.Textures[ActiveTexture].LineSize << LineShift);

            int Tile_Width = ((int)NGraphics.Textures[ActiveTexture].LRS - (int)NGraphics.Textures[ActiveTexture].ULS) + 1;
            int Tile_Height = ((int)NGraphics.Textures[ActiveTexture].LRT - (int)NGraphics.Textures[ActiveTexture].ULT) + 1;

            int Mask_Width = 1 << (int)NGraphics.Textures[ActiveTexture].MaskS;
            int Mask_Height = 1 << (int)NGraphics.Textures[ActiveTexture].MaskT;

            int Line_Height = 0;
            if (Line_Width > 0)
                Line_Height = Math.Min(MaxTexel / Line_Width, Tile_Height);

            if ((NGraphics.Textures[ActiveTexture].MaskS > 0) && ((Mask_Width * Mask_Height) <= MaxTexel))
                NGraphics.Textures[ActiveTexture].Width = (uint)Mask_Width;
            else if ((Tile_Width * Tile_Height) <= MaxTexel)
                NGraphics.Textures[ActiveTexture].Width = (uint)Tile_Width;
            else
                NGraphics.Textures[ActiveTexture].Width = (uint)Line_Width;

            if ((NGraphics.Textures[ActiveTexture].MaskT > 0) && ((Mask_Width * Mask_Height) <= MaxTexel))
                NGraphics.Textures[ActiveTexture].Height = (uint)Mask_Height;
            else if ((Tile_Width * Tile_Height) <= MaxTexel)
                NGraphics.Textures[ActiveTexture].Height = (uint)Tile_Height;
            else
                NGraphics.Textures[ActiveTexture].Height = (uint)Line_Height;

            int Clamp_Width = 0;
            int Clamp_Height = 0;

            if (NGraphics.Textures[ActiveTexture].CMS == 1)
                Clamp_Width = Tile_Width;
            else
                Clamp_Width = (int)NGraphics.Textures[ActiveTexture].Width;

            if (NGraphics.Textures[ActiveTexture].CMT == 1)
                Clamp_Height = Tile_Height;
            else
                Clamp_Height = (int)NGraphics.Textures[ActiveTexture].Height;

            if (Clamp_Width > 256)
                NGraphics.Textures[ActiveTexture].CMS = 0;
            if (Clamp_Height > 256)
                NGraphics.Textures[ActiveTexture].CMT = 0;

            if (Mask_Width > NGraphics.Textures[ActiveTexture].Width)
            {
                NGraphics.Textures[ActiveTexture].MaskS = (uint)PowOf((int)NGraphics.Textures[ActiveTexture].Width);
                Mask_Width = 1 << (int)NGraphics.Textures[ActiveTexture].MaskS;
            }
            if (Mask_Height > NGraphics.Textures[ActiveTexture].Height)
            {
                NGraphics.Textures[ActiveTexture].MaskT = (uint)PowOf((int)NGraphics.Textures[ActiveTexture].Height);
                Mask_Height = 1 << (int)NGraphics.Textures[ActiveTexture].MaskT;
            }

            if (NGraphics.Textures[ActiveTexture].CMS == 2 || NGraphics.Textures[ActiveTexture].CMS == 3)
                NGraphics.Textures[ActiveTexture].RealWidth = (uint)Pow2(Clamp_Width);
            else if (NGraphics.Textures[ActiveTexture].CMS == 1)
                NGraphics.Textures[ActiveTexture].RealWidth = (uint)Pow2(Mask_Width);
            else
                NGraphics.Textures[ActiveTexture].RealWidth = (uint)Pow2((int)NGraphics.Textures[ActiveTexture].Width);

            if (NGraphics.Textures[ActiveTexture].CMT == 2 || NGraphics.Textures[ActiveTexture].CMT == 3)
                NGraphics.Textures[ActiveTexture].RealHeight = (uint)Pow2(Clamp_Height);
            else if (NGraphics.Textures[ActiveTexture].CMT == 1)
                NGraphics.Textures[ActiveTexture].RealHeight = (uint)Pow2(Mask_Height);
            else
                NGraphics.Textures[ActiveTexture].RealHeight = (uint)Pow2((int)NGraphics.Textures[ActiveTexture].Height);

            NGraphics.Textures[ActiveTexture].ShiftScaleS = 1.0f;
            NGraphics.Textures[ActiveTexture].ShiftScaleT = 1.0f;

            if (NGraphics.Textures[ActiveTexture].ShiftS > 10)
                NGraphics.Textures[ActiveTexture].ShiftScaleS = (float)(1 << (int)(16 - NGraphics.Textures[ActiveTexture].ShiftS));
            else if (NGraphics.Textures[ActiveTexture].ShiftS > 0)
                NGraphics.Textures[ActiveTexture].ShiftScaleS /= (float)(1 << (int)NGraphics.Textures[ActiveTexture].ShiftS);

            if (NGraphics.Textures[ActiveTexture].ShiftT > 10)
                NGraphics.Textures[ActiveTexture].ShiftScaleT = (float)(1 << (16 - (int)NGraphics.Textures[ActiveTexture].ShiftT));
            else if (NGraphics.Textures[ActiveTexture].ShiftT > 0)
                NGraphics.Textures[ActiveTexture].ShiftScaleT /= (float)(1 << (int)NGraphics.Textures[ActiveTexture].ShiftT);
        }

        static int Pow2(int dim)
        {
            int i = 1;

            while (i < dim) i <<= 1;

            return i;
        }

        static int PowOf(int dim)
        {
            int num = 1;
            int i = 0;

            while (num < dim)
            {
                num <<= 1;
                i++;
            }

            return i;
        }

        public static int CheckTextureCache(int ActiveTexture)
        {



            foreach (TextureCacheStruct ThisCache in TextureCache)
            {
                if (ThisCache.Format == NGraphics.Textures[ActiveTexture].Format &&
                    ThisCache.Address == NGraphics.Textures[ActiveTexture].Address &&
                    ThisCache.RealHeight == NGraphics.Textures[ActiveTexture].RealHeight &&
                    ThisCache.RealWidth == NGraphics.Textures[ActiveTexture].RealWidth &&
                    ThisCache.Filename == currentfilename)
                {
                    return ThisCache.GLID;
                }
            }

            TextureCacheStruct NewCache = new TextureCacheStruct();

            NewCache.Format = NGraphics.Textures[ActiveTexture].Format;
            NewCache.Address = NGraphics.Textures[ActiveTexture].Address;
            NewCache.RealHeight = NGraphics.Textures[ActiveTexture].RealHeight;
            NewCache.RealWidth = NGraphics.Textures[ActiveTexture].RealWidth;
            NewCache.GLID = LoadTexture(ActiveTexture);
            NewCache.Filename = currentfilename;

            TextureCache.Add(NewCache);

            return NewCache.GLID;
        }

        private static int LoadTexture(int ActiveTexture)
        {
            UInt32 TextureSegment = ((NGraphics.Textures[ActiveTexture].Address & 0xFF000000) >> 24);
            UInt32 TextureOffset = (NGraphics.Textures[ActiveTexture].Address & 0x00FFFFFF);



            uint BufferSize = NGraphics.Textures[ActiveTexture].RealWidth * NGraphics.Textures[ActiveTexture].RealHeight * 4;

            byte[] TextureBuffer = new byte[BufferSize];


            if (GameHandler.IsAddressValid(NGraphics.Textures[ActiveTexture].Address) == true)
            {
                NImageUtil.ConvertTexture((byte)NGraphics.Textures[ActiveTexture].Format,
                    GameHandler.RAM[TextureSegment].Data,
                    TextureOffset,
                    ref TextureBuffer,
                    NGraphics.Textures[ActiveTexture].Width,
                    NGraphics.Textures[ActiveTexture].Height,
                    NGraphics.Textures[ActiveTexture].LineSize,
                    NGraphics.Palette);
            }
            else if (TextureSegment >= 8 && textureoffsets[TextureSegment] != 0)
            {
                if (currentfilename.Contains(".zobj"))
                {
                    NImageUtil.ConvertTexture((byte)NGraphics.Textures[ActiveTexture].Format,
                    GameHandler.RAM[(currentfilename.Contains("gameplay_dangeon_keep") || currentfilename.Contains("GamePlay_Dungeon") || currentfilename.Contains("GamePlay_Field") || currentfilename.Contains("gameplay_field_keep")) ? 0x5 : (currentfilename.Contains("gameplay_keep") || currentfilename.Contains("GamePlay_Global")) ? 0x4 : 0x6].Data,
                    textureoffsets[TextureSegment] & 0x00FFFFFF,
                    ref TextureBuffer,
                    NGraphics.Textures[ActiveTexture].Width,
                    NGraphics.Textures[ActiveTexture].Height,
                    NGraphics.Textures[ActiveTexture].LineSize,
                    NGraphics.Palette);
                }
                else
                {
                    //TODO
                    //  if (List.count == 0)
                    //  {
                    // ADD cache info
                    //   }

                    TextureBuffer.Fill(new byte[] { 0xFF, 0x00, 0xFF, 0xFF });

                }
            }

            else
            {
                // Invalid address -> yellow texture
                TextureBuffer.Fill(new byte[] { 0xFF, 0xFF, 0x00, 0xFF });
                //DebugConsole.WriteLine("SCENE " + Program.MF.CurrentScene.ToString() + ", " + GameHandler.SceneInfos[Program.MF.CurrentScene].Name + " -> invalid texture address! " + NGraphics.Textures[ActiveTexture].Address.ToString("X8"));
            }

            int GLID = TexUtil.CreateRGBATexture(
                (int)NGraphics.Textures[ActiveTexture].RealWidth,
                (int)NGraphics.Textures[ActiveTexture].RealHeight,
                TextureBuffer);

            if (NGraphics.Textures[ActiveTexture].CMS == 2 || NGraphics.Textures[ActiveTexture].CMS == 3)
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            else if (NGraphics.Textures[ActiveTexture].CMS == 1)
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.MirroredRepeatArb);
            else
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);

            if (NGraphics.Textures[ActiveTexture].CMT == 2 || NGraphics.Textures[ActiveTexture].CMT == 3)
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);
            else if (NGraphics.Textures[ActiveTexture].CMT == 1)
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.MirroredRepeatArb);
            else
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);

            return GLID;
        }

        #endregion

        #region Combiner Emulation

        public static void CheckFragmentCache(UInt32 Mux0, UInt32 Mux1)
        {
            foreach (FragmentCacheStruct ThisCache in FragmentCache)
            {
                if (ThisCache.Mux0 == Mux0 && ThisCache.Mux1 == Mux1)
                {
                    GL.Arb.BindProgram(AssemblyProgramTargetArb.FragmentProgram, ThisCache.GLID);
                    if (EnableCombiner == true)
                        GL.Enable((EnableCap)All.FragmentProgram);
                    return;
                }
            }

            FragmentCacheStruct NewCache = new FragmentCacheStruct();

            NewCache.UnpackedMux = UnpackCombinerMux(Mux0, Mux1);
            NewCache.Mux0 = Mux0;
            NewCache.Mux1 = Mux1;

            CreateFragmentProgram(ref NewCache);

            GL.Arb.BindProgram(AssemblyProgramTargetArb.FragmentProgram, NewCache.GLID);
            if (EnableCombiner == true)
                GL.Enable((EnableCap)All.FragmentProgram);

            FragmentCache.Add(NewCache);
        }

        public static UnpackedCombinerStruct UnpackCombinerMux(UInt32 Mux0, UInt32 Mux1)
        {
            UnpackedCombinerStruct ThisMux = new UnpackedCombinerStruct();

            ThisMux.cA = new byte[2];
            ThisMux.cB = new byte[2];
            ThisMux.cC = new byte[2];
            ThisMux.cD = new byte[2];
            ThisMux.aA = new byte[2];
            ThisMux.aB = new byte[2];
            ThisMux.aC = new byte[2];
            ThisMux.aD = new byte[2];

            ThisMux.cA[0] = (byte)((Mux0 >> 20) & 0x0F);
            ThisMux.cB[0] = (byte)((Mux1 >> 28) & 0x0F);
            ThisMux.cC[0] = (byte)((Mux0 >> 15) & 0x1F);
            ThisMux.cD[0] = (byte)((Mux1 >> 15) & 0x07);

            ThisMux.aA[0] = (byte)((Mux0 >> 12) & 0x07);
            ThisMux.aB[0] = (byte)((Mux1 >> 12) & 0x07);
            ThisMux.aC[0] = (byte)((Mux0 >> 9) & 0x07);
            ThisMux.aD[0] = (byte)((Mux1 >> 9) & 0x07);

            ThisMux.cA[1] = (byte)((Mux0 >> 5) & 0x0F);
            ThisMux.cB[1] = (byte)((Mux1 >> 24) & 0x0F);
            ThisMux.cC[1] = (byte)((Mux0 >> 0) & 0x1F);
            ThisMux.cD[1] = (byte)((Mux1 >> 6) & 0x07);

            ThisMux.aA[1] = (byte)((Mux1 >> 21) & 0x07);
            ThisMux.aB[1] = (byte)((Mux1 >> 3) & 0x07);
            ThisMux.aC[1] = (byte)((Mux1 >> 18) & 0x07);
            ThisMux.aD[1] = (byte)((Mux1 >> 0) & 0x07);

            return ThisMux;
        }

        private static void CreateFragmentProgram(ref FragmentCacheStruct ThisFP)
        {
            ThisFP.GLID = CreateFragmentProgram(ThisFP.Mux0, ThisFP.Mux1);
        }

        private static int CreateFragmentProgram(UInt32 Mux0, UInt32 Mux1)
        {
            int GLID = 0;

            UnpackedCombinerStruct ThisMux = UnpackCombinerMux(Mux0, Mux1);

            string ProgramString =
                "!!ARBfp1.0\n" +
                "\n" +
                "TEMP Tex0; TEMP Tex1;\n" +
                "TEMP R0; TEMP R1;\n" +
                "TEMP aR0; TEMP aR1;\n" +
                "TEMP Comb; TEMP aComb;\n" +
                "\n" +
                "PARAM PrimColor = program.env[0];\n" +
                "PARAM EnvColor = program.env[1];\n" +
                "PARAM PrimColorLOD = program.env[2];\n" +
                "ATTRIB Shade = fragment.color;\n" +
                "\n" +
                "OUTPUT Out = result.color;\n" +
                "\n" +
                "TEX Tex0, fragment.texcoord[0], texture[0], 2D;\n" +
                "TEX Tex1, fragment.texcoord[1], texture[1], 2D;\n" +
                "\n";

            for (int i = 0; i < 2; i++)
            {
                switch (ThisMux.cA[i])
                {
                    case (byte)Combiner.CCMUX_COMBINED:
                        ProgramString += "MOV R0.rgb, Comb;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL0:
                        ProgramString += "MOV R0.rgb, Tex0;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL1:
                        ProgramString += "MOV R0.rgb, Tex1;\n";
                        break;
                    case (byte)Combiner.CCMUX_PRIMITIVE:
                        ProgramString += "MOV R0.rgb, PrimColor;\n";
                        break;
                    case (byte)Combiner.CCMUX_SHADE:
                        ProgramString += "MOV R0.rgb, Shade;\n";
                        break;
                    case (byte)Combiner.CCMUX_ENVIRONMENT:
                        ProgramString += "MOV R0.rgb, EnvColor;\n";
                        break;
                    case (byte)Combiner.CCMUX_1:
                        ProgramString += "MOV R0.rgb, {1.0, 1.0, 1.0, 1.0};\n";
                        break;
                    case (byte)Combiner.CCMUX_COMBINED_ALPHA:
                        ProgramString += "MOV R0.rgb, Comb.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL0_ALPHA:
                        ProgramString += "MOV R0.rgb, Tex0.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL1_ALPHA:
                        ProgramString += "MOV R0.rgb, Tex1.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_PRIMITIVE_ALPHA:
                        ProgramString += "MOV R0.rgb, PrimColor.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_SHADE_ALPHA:
                        ProgramString += "MOV R0.rgb, Shade.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_ENV_ALPHA:
                        ProgramString += "MOV R0.rgb, EnvColor.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_LOD_FRACTION:
                        ProgramString += "MOV R0.rgb, {0.0, 0.0, 0.0, 0.0};\n";	// unemulated
                        break;
                    case (byte)Combiner.CCMUX_PRIM_LOD_FRAC:
                        ProgramString += "MOV R0.rgb, PrimColorLOD;\n";
                        break;
                    case 15:	// 0
                        ProgramString += "MOV R0.rgb, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                    default:
                        ProgramString += "MOV R0.rgb, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                }

                switch (ThisMux.cB[i])
                {
                    case (byte)Combiner.CCMUX_COMBINED:
                        ProgramString += "MOV R1.rgb, Comb;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL0:
                        ProgramString += "MOV R1.rgb, Tex0;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL1:
                        ProgramString += "MOV R1.rgb, Tex1;\n";
                        break;
                    case (byte)Combiner.CCMUX_PRIMITIVE:
                        ProgramString += "MOV R1.rgb, PrimColor;\n";
                        break;
                    case (byte)Combiner.CCMUX_SHADE:
                        ProgramString += "MOV R1.rgb, Shade;\n";
                        break;
                    case (byte)Combiner.CCMUX_ENVIRONMENT:
                        ProgramString += "MOV R1.rgb, EnvColor;\n";
                        break;
                    case (byte)Combiner.CCMUX_1:
                        ProgramString += "MOV R1.rgb, {1.0, 1.0, 1.0, 1.0};\n";
                        break;
                    case (byte)Combiner.CCMUX_COMBINED_ALPHA:
                        ProgramString += "MOV R1.rgb, Comb.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL0_ALPHA:
                        ProgramString += "MOV R1.rgb, Tex0.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL1_ALPHA:
                        ProgramString += "MOV R1.rgb, Tex1.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_PRIMITIVE_ALPHA:
                        ProgramString += "MOV R1.rgb, PrimColor.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_SHADE_ALPHA:
                        ProgramString += "MOV R1.rgb, Shade.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_ENV_ALPHA:
                        ProgramString += "MOV R1.rgb, EnvColor.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_LOD_FRACTION:
                        ProgramString += "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n";	// unemulated
                        break;
                    case (byte)Combiner.CCMUX_PRIM_LOD_FRAC:
                        ProgramString += "MOV R1.rgb, PrimColorLOD;\n";
                        break;
                    case 15:	// 0
                        ProgramString += "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                    default:
                        ProgramString += "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                }
                ProgramString += "SUB R0, R0, R1;\n\n";

                switch (ThisMux.cC[i])
                {
                    case (byte)Combiner.CCMUX_COMBINED:
                        ProgramString += "MOV R1.rgb, Comb;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL0:
                        ProgramString += "MOV R1.rgb, Tex0;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL1:
                        ProgramString += "MOV R1.rgb, Tex1;\n";
                        break;
                    case (byte)Combiner.CCMUX_PRIMITIVE:
                        ProgramString += "MOV R1.rgb, PrimColor;\n";
                        break;
                    case (byte)Combiner.CCMUX_SHADE:
                        ProgramString += "MOV R1.rgb, Shade;\n";
                        break;
                    case (byte)Combiner.CCMUX_ENVIRONMENT:
                        ProgramString += "MOV R1.rgb, EnvColor;\n";
                        break;
                    case (byte)Combiner.CCMUX_1:
                        ProgramString += "MOV R1.rgb, {1.0, 1.0, 1.0, 1.0};\n";
                        break;
                    case (byte)Combiner.CCMUX_COMBINED_ALPHA:
                        ProgramString += "MOV R1.rgb, Comb.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL0_ALPHA:
                        ProgramString += "MOV R1.rgb, Tex0.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL1_ALPHA:
                        ProgramString += "MOV R1.rgb, Tex1.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_PRIMITIVE_ALPHA:
                        ProgramString += "MOV R1.rgb, PrimColor.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_SHADE_ALPHA:
                        ProgramString += "MOV R1.rgb, Shade.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_ENV_ALPHA:
                        ProgramString += "MOV R1.rgb, EnvColor.a;\n";
                        break;
                    case (byte)Combiner.CCMUX_LOD_FRACTION:
                        ProgramString += "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n";	// unemulated
                        break;
                    case (byte)Combiner.CCMUX_PRIM_LOD_FRAC:
                        ProgramString += "MOV R1.rgb, PrimColorLOD;\n";
                        break;
                    case (byte)Combiner.CCMUX_K5:
                        ProgramString += "MOV R1.rgb, {1.0, 1.0, 1.0, 1.0};\n";	// unemulated
                        break;
                    case (byte)Combiner.CCMUX_0:
                        ProgramString += "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                    default:
                        ProgramString += "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                }
                ProgramString += "MUL R0, R0, R1;\n\n";

                switch (ThisMux.cD[i])
                {
                    case (byte)Combiner.CCMUX_COMBINED:
                        ProgramString += "MOV R1.rgb, Comb;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL0:
                        ProgramString += "MOV R1.rgb, Tex0;\n";
                        break;
                    case (byte)Combiner.CCMUX_TEXEL1:
                        ProgramString += "MOV R1.rgb, Tex1;\n";
                        break;
                    case (byte)Combiner.CCMUX_PRIMITIVE:
                        ProgramString += "MOV R1.rgb, PrimColor;\n";
                        break;
                    case (byte)Combiner.CCMUX_SHADE:
                        ProgramString += "MOV R1.rgb, Shade;\n";
                        break;
                    case (byte)Combiner.CCMUX_ENVIRONMENT:
                        ProgramString += "MOV R1.rgb, EnvColor;\n";
                        break;
                    case (byte)Combiner.CCMUX_1:
                        ProgramString += "MOV R1.rgb, {1.0, 1.0, 1.0, 1.0};\n";
                        break;
                    case 7:		// 0
                        ProgramString += "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                    default:
                        ProgramString += "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                }
                ProgramString += "ADD R0, R0, R1;\n\n";

                switch (ThisMux.aA[i])
                {
                    case (byte)Combiner.ACMUX_COMBINED:
                        ProgramString += "MOV aR0.a, aComb;\n";
                        break;
                    case (byte)Combiner.ACMUX_TEXEL0:
                        ProgramString += "MOV aR0.a, Tex0.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_TEXEL1:
                        ProgramString += "MOV aR0.a, Tex1.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_PRIMITIVE:
                        ProgramString += "MOV aR0.a, PrimColor.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_SHADE:
                        ProgramString += "MOV aR0.a, Shade.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_ENVIRONMENT:
                        ProgramString += "MOV aR0.a, EnvColor.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_1:
                        ProgramString += "MOV aR0.a, {1.0, 1.0, 1.0, 1.0};\n";
                        break;
                    case (byte)Combiner.ACMUX_0:
                        ProgramString += "MOV aR0.a, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                    default:
                        ProgramString += "MOV aR0.a, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                }

                switch (ThisMux.aB[i])
                {
                    case (byte)Combiner.ACMUX_COMBINED:
                        ProgramString += "MOV aR1.a, aComb;\n";
                        break;
                    case (byte)Combiner.ACMUX_TEXEL0:
                        ProgramString += "MOV aR1.a, Tex0.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_TEXEL1:
                        ProgramString += "MOV aR1.a, Tex1.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_PRIMITIVE:
                        ProgramString += "MOV aR1.a, PrimColor.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_SHADE:
                        ProgramString += "MOV aR1.a, Shade.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_ENVIRONMENT:
                        ProgramString += "MOV aR1.a, EnvColor.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_1:
                        ProgramString += "MOV aR1.a, {1.0, 1.0, 1.0, 1.0};\n";
                        break;
                    case (byte)Combiner.ACMUX_0:
                        ProgramString += "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                    default:
                        ProgramString += "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                }
                ProgramString += "SUB aR0.a, aR0.a, aR1.a;\n\n";

                switch (ThisMux.aC[i])
                {
                    case (byte)Combiner.ACMUX_LOD_FRACTION:
                        ProgramString += "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n";	// unemulated
                        break;
                    case (byte)Combiner.ACMUX_TEXEL0:
                        ProgramString += "MOV aR1.a, Tex0.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_TEXEL1:
                        ProgramString += "MOV aR1.a, Tex1.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_PRIMITIVE:
                        ProgramString += "MOV aR1.a, PrimColor.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_SHADE:
                        ProgramString += "MOV aR1.a, Shade.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_ENVIRONMENT:
                        ProgramString += "MOV aR1.a, EnvColor.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_PRIM_LOD_FRAC:
                        ProgramString += "MOV aR1.a, PrimColorLOD.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_0:
                        ProgramString += "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                    default:
                        ProgramString += "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                }
                ProgramString += "MUL aR0.a, aR0.a, aR1.a;\n\n";

                switch (ThisMux.aD[i])
                {
                    case (byte)Combiner.ACMUX_COMBINED:
                        ProgramString += "MOV aR1.a, aComb.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_TEXEL0:
                        ProgramString += "MOV aR1.a, Tex0.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_TEXEL1:
                        ProgramString += "MOV aR1.a, Tex1.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_PRIMITIVE:
                        ProgramString += "MOV aR1.a, PrimColor.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_SHADE:
                        ProgramString += "MOV aR1.a, Shade.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_ENVIRONMENT:
                        ProgramString += "MOV aR1.a, EnvColor.a;\n";
                        break;
                    case (byte)Combiner.ACMUX_1:
                        ProgramString += "MOV aR1.a, {1.0, 1.0, 1.0, 1.0};\n";
                        break;
                    case (byte)Combiner.ACMUX_0:
                        ProgramString += "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                    default:
                        ProgramString += "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n";
                        break;
                }

                ProgramString += "ADD aR0.a, aR0.a, aR1.a;\n\n";

                ProgramString += "MOV Comb.rgb, R0;\n";
                ProgramString += "MOV aComb.a, aR0;\n\n";
            }

            ProgramString += "MOV Comb.a, aComb.a;\n" +
                "MOV Out, Comb;\n" +
                "END\n";

            byte[] ProgramBytes = Encoding.ASCII.GetBytes(ProgramString);

            GL.Arb.GenProgram(1, out GLID);
            GL.Arb.BindProgram(AssemblyProgramTargetArb.FragmentProgram, GLID);
            GL.Arb.ProgramString(AssemblyProgramTargetArb.FragmentProgram, ArbVertexProgram.ProgramFormatAsciiArb,
                ProgramBytes.Length, ProgramBytes);

            return GLID;
        }

        #endregion

        #region Blender Definitions & Functions

        static string[] sc_szBlClr = { "In", "Mem", "Bl", "Fog" };
        static string[] sc_szBlA1 = { "AIn", "AFog", "AShade", "0" };
        static string[] sc_szBlA2 = { "1-A", "AMem", "1", "?" };

        const uint BLEND_NOOP1 = 0x00000000;
        const uint BLEND_NOOP2 = 0x00000000;

        const uint BLEND_FOG_ASHADE1 = 0xC8000000;
        const uint BLEND_FOG_ASHADE2 = 0xC8000000;
        const uint BLEND_FOG_APRIM1 = 0xC4000000;

        const uint BLEND_PASS1 = 0x0C080000;
        const uint BLEND_PASS2 = 0x03020000;

        const uint BLEND_OPA1 = 0x00440000;
        const uint BLEND_OPA2 = 0x00110000;

        const uint BLEND_XLU1 = 0x00400000;
        const uint BLEND_XLU2 = 0x00100000;

        const uint BLEND_ADD1 = 0x04400000;
        const uint BLEND_ADD2 = 0x01100000;

        const uint BLEND_BI_AFOG = 0x84000000;

        const uint BLEND_MEM1 = 0x4C400000;
        const uint BLEND_MEM2 = 0x13100000;

        const uint BLEND_NOOP3 = 0x0C480000;
        const uint BLEND_NOOP4 = 0xCC080000;

        private static void ForceBlending(UInt32 w0, UInt32 w1)
        {
            GL.Disable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.Blend);

            uint blendmode = (w1 & 0xFFFF0000);
            uint blendmode_1 = ((w1 >> 16) & 0xCCCC);
            uint blendmode_2 = ((w1 >> 16) & 0x3333);

            switch (blendmode)
            {
                default:
                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                    GL.AlphaFunc(AlphaFunction.Greater, 0.5f);
                    break;
            }
            /*
            byte m1A_1 = (byte)((w1 >> 30) & 0x3);
            byte m1B_1 = (byte)((w1 >> 26) & 0x3);
            byte m2A_1 = (byte)((w1 >> 22) & 0x3);
            byte m2B_1 = (byte)((w1 >> 18) & 0x3);
            byte m1A_2 = (byte)((w1 >> 28) & 0x3);
            byte m1B_2 = (byte)((w1 >> 24) & 0x3);
            byte m2A_2 = (byte)((w1 >> 20) & 0x3);
            byte m2B_2 = (byte)((w1 >> 16) & 0x3);

            string Message =
                "Blender:\n" +
                blendmode_1.ToString("X4") + " -> " +
                "Cycle1:" + sc_szBlClr[m1A_1] + " * " + sc_szBlA1[m1B_1] + " + " + sc_szBlClr[m2A_1] + " * " + sc_szBlA2[m2B_1] + "\n" +
                blendmode_2.ToString("X4") + " -> " +
                "Cycle2:" + sc_szBlClr[m1A_2] + " * " + sc_szBlA1[m1B_2] + " + " + sc_szBlClr[m2A_2] + " * " + sc_szBlA2[m2B_2];
            
            MessageBox.Show(Message);*/
        }

        #endregion
    }
}
