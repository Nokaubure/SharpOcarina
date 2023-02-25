/*
 * GUI code "disclaimer":
Enjoy the mess!
 */
//new OpenTK.Graphics.GraphicsMode(0, 24)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32.SafeHandles;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using SharpOcarina.SayakaGL;
using System.Drawing.Text;
using System.Globalization;
using RedCell.Diagnostics.Update;
using TexLib;
using TgaDecoderTest;
using Microsoft.VisualBasic;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using Tommy;

namespace SharpOcarina
{

    public partial class MainForm : Form
    {
        #region Variables/Structs/Constructor

        bool NowLoading = false;

        public static readonly Vector3 NullVector = new Vector3(-99999, -99999, -99999);

        public static readonly ushort[] transitionids = { 0x0009, 0x0023, 0x002E };

        public static List<UndoRedo> undo = new List<UndoRedo>();

        public static List<UndoRedo> redo = new List<UndoRedo>();

        public static ZScene CurrentScene = null;

        public static ZScene NormalHeader = null;

        public ZScene CacheScene = new ZScene();

        bool SceneLoaded = false;

        bool IsReady = false;

        bool[] KeysDown = new bool[256];

        TextRendering.TextRenderer renderer;

        string[] viewporttext = new string[6];

        private static readonly PrivateFontCollection Fonts = new PrivateFontCollection();
        public static Font zeldafont;

        public static Settings settings = new Settings();

        public bool SimulateN64Gfx = false;

        public static Dictionary<string, ulong> flaglist = new Dictionary<string, ulong>();

        public static List<String> InjectMessages = new List<string>();

        public ushort[] datatemplate;

        public bool previewcamerapoints = false;

        public bool previewscenecamera = false;

        public bool previewcutscene = false;

        public bool notresize = false;

        public int globalframe = 0;

        public DateTime globalframestart;

        public List<ZCutscenePosition> playcamerapointscache = new List<ZCutscenePosition>();


        public static bool updateavailable = false;

        public static string GlobalROM = "";

        public Dictionary<ushort, string> ExitCache = new Dictionary<ushort, string>();

        public static Dictionary<ushort, ObjectInfo> ObjectCache = new Dictionary<ushort, ObjectInfo>();

        public static Dictionary<ushort, ActorInfo> ActorCache = new Dictionary<ushort, ActorInfo>();

        public static List<UcodeSimulator.TextureCacheStruct> AdditionalTexturesGLID = new List<UcodeSimulator.TextureCacheStruct>();

        public static bool exportingZobj = false;

        ObjFile.Material DummyMaterial = new ObjFile.Material();

        public static bool flaglog_visible = false;

        public static CustomCombiner customcombiner;

        public static bool customcombiner_visible = false;

        public static bool entrancetable_visible = false;

        public static bool cutscenetable_visible = false;

        public static bool savefileeditor_visible = false;

        public static bool subscreenmapeditor_visible = false;

        public static bool titlecardeditor_visible = false;

        public static bool restrictionflag_visible = false;

        public static bool n64preview = false;

        public static Keys[] ActorControlKeys = new Keys[0];


        private int actorpick = -1;
        public byte extrapick = 0;
        public string LastScene = "";
        public DateTime LastAutoSave = DateTime.Now;
        public bool isundo = false;
        public bool storeundo = true;
        public string LastInject = "";
        public static int EasterEggPhase = 0;
        public static int EasterEggCounter = 0;
        public static bool n64refresh = false;
        public bool nocheckevent = false;
        public bool noupdatetextureanim = false;

        public const int _Actor_ = 1;
        public const int _Transition_ = 2;
        public const int _Spawn_ = 3;
        public const int _Pathway_ = 4;
        public const int _Waterbox_ = 5;
        public const int _AddLight_ = 6;
        public const int _CutsceneCamera_ = 7;
        public const int _CutsceneActor_ = 8;
        public const int _Camera_ = 9;

        public double skyboxangle = 0;

        public static bool Is1April = false;

        public static bool subscreenmode = false;
        public static int subscreenroom = 0;

        public int prevwidth = 720;
        public int prevheight = 720;

        public int selectedtimer = 0;

        public int prevsceneheader = 0;

        public Stopwatch stopwatch = new Stopwatch();

        int ActorCubeGLID = 0, ActorPyramidGLID = 0, AxisMarkerGLID = 0, DoorGLID = 0, EnemyGLID = 0, BossGLID = 0, ActorCameraGLID = 0;

        //test

        // public static List<SayakaGL.UcodeSimulator.DisplayListStruct> zobj_cache = new List<SayakaGL.UcodeSimulator.DisplayListStruct>();
        public static List<ZScene.ZObjRender> zobj_cache = new List<ZScene.ZObjRender>();
        public static List<List<SayakaGL.UcodeSimulator.DisplayListStruct>> skyboxdlists = new List<List<UcodeSimulator.DisplayListStruct>>();

        public struct MouseStruct
        {
            public Vector2d Center, Move;
            public bool LDown, RDown, MDown;
        }

        MouseStruct Mouse = new MouseStruct();

        public MainForm()
        {

            InitializeComponent();

            //   ChangeTheme(new ColorScheme(true),tabControl1.Controls);

            switch (Program.KeyboardLayout)
            {
                case "AZERTY":
                    ActorControlKeys = new Keys[] { Keys.W, Keys.X }; break;
                case "DVORAK":
                    ActorControlKeys = new Keys[] { Keys.OemSemicolon, Keys.Q }; break;
                default:
                    ActorControlKeys = new Keys[] { Keys.Z, Keys.X }; break;
            }

            globalframestart = DateTime.Now;

            DummyMaterial.Name = "(none)";

            skyboxdlists.Add(new List<UcodeSimulator.DisplayListStruct>());
            skyboxdlists.Add(new List<UcodeSimulator.DisplayListStruct>());
            skyboxdlists.Add(new List<UcodeSimulator.DisplayListStruct>());
            skyboxdlists.Add(new List<UcodeSimulator.DisplayListStruct>());
            // this.glControl1 = new OpenTK.GLControl(new OpenTK.Graphics.GraphicsMode(0, 24));
            // new OpenTK.GraphicsM

#if DEBUG
            dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem.Visible = true;
            dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem.Visible = true;
            dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem.Visible = true;
#else

#endif


            if ((int)System.DateTime.Now.Day == 1 && (int)System.DateTime.Now.Month == 4)
            {
                Is1April = true;
                CDILink.Visible = true;
                CDILink.Size = new Size(324, 238);
                Program.ApplicationTitle = "OoT, but its a tool to make maps ";
            }


            if (File.Exists("Settings.xml"))
            {
                settings = IO.Import<Settings>("Settings.xml");
            }

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);



            // textBox2.ContextMenu = new ContextMenu();

            SimulateN64CheckBox.LostFocus += new EventHandler(checkBox5_LostFocus);
            glControl1.LostFocus += new EventHandler(glControl1_LostFocus);

            CutsceneTabs.ItemSize = new Size(1, 1);
            RenderFunctionTabs.ItemSize = new Size(1, 1);


            int fontLength = Properties.Resources.FOT_ChiaroStd_B.Length;
            byte[] fontdata = Properties.Resources.FOT_ChiaroStd_B;

            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            Fonts.AddMemoryFont(data, fontLength);
            zeldafont = new Font(Fonts.Families[0], 12);



            /*
            label121.BackColor = Color.Transparent;
            label121.ForeColor = Color.White;
            label121.Location = glControl1.PointToClient(this.PointToScreen(label121.Location));
            label121.Parent = glControl1;*/




        }

        #endregion

        #region Main Functions, Rendering

        bool fovOverrideFlag;
        float fovOverride;

        public void ProgramMainLoop()
        {
            Camera.KeyUpdate(KeysDown);

            glControl1.Invalidate();

            if (selectedtimer > 0) selectedtimer -= 1;

            globalframe = (int)((DateTime.Now.Subtract(globalframestart).TotalSeconds) * 20); //FPS

            if (previewcutscene)
            {
                CutscenePreview_Update();
            }
            else
            {
                if ((glControl1.Size.Width != prevwidth || glControl1.Size.Height != prevheight) && !(previewscenecamera && CurrentScene.prerenderimages.Count > 0))
                {
                    glControl1.Size = new Size(prevwidth, prevheight);
                    FormBorderStyle = FormBorderStyle.Sizable;
                }
            }
        }

        public void SetViewport(int VPWidth, int VPHeight)
        {
            Matrix4 PerspMatrix;
            GL.Viewport(0, 0, VPWidth, VPHeight);
            GL.Viewport(0, 0, VPWidth, VPHeight);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            if (!fovOverrideFlag)
                PerspMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians((float)ViewportFOV.Value), (float)VPWidth / (float)VPHeight, 0.1f, 10000.0f);
            else
                PerspMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fovOverride), (float)VPWidth / (float)VPHeight, 0.1f, 10000.0f);
            GL.MultMatrix(ref PerspMatrix);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            AxisMarkerGLID = GL.GenLists(1);
            ActorCubeGLID = GL.GenLists(1);
            ActorPyramidGLID = GL.GenLists(1);
            DoorGLID = GL.GenLists(1);
            EnemyGLID = GL.GenLists(1);
            BossGLID = GL.GenLists(1);
            ActorCameraGLID = GL.GenLists(1);

            // Axis marker
            GL.NewList(AxisMarkerGLID, ListMode.Compile);

            GL.LineWidth(3);

            GL.Begin(BeginMode.Lines);
            GL.Color3(Color.Blue);
            GL.Vertex3(20.0f, 0.1f, 0.1f);
            GL.Vertex3(-20.0f, 0.1f, 0.1f);
            GL.End();
            GL.Begin(BeginMode.Lines);
            if (settings.colorblindaxis)
                GL.Color3(Color.DarkGoldenrod);
            else
                GL.Color3(Color.Red);
            GL.Vertex3(0.1f, 20.0f, 0.1f);
            GL.Vertex3(0.1f, -20.0f, 0.1f);
            GL.End();

            // GL.LineWidth(2);
            GL.Begin(BeginMode.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0.1f, 0.1f, 20.0f);
            GL.Vertex3(0.1f, 0.1f, 0.0f);
            GL.Vertex3(0.1f, 0.1f, 0.0f);
            if (settings.colorblindaxis)
                GL.Color3(Color.Cyan);
            else
                GL.Color3(Color.Lime);
            GL.Vertex3(0.1f, 0.1f, -20.0f);
            // GL.Vertex3(0.1f, 0.1f, 20.0f);
            GL.End();

            GL.LineWidth(1);

            GL.EndList();

            // Actor cube
            GL.NewList(ActorCubeGLID, ListMode.Compile);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            GL.Begin(BeginMode.Quads);
            // Back Face
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            // Top Face
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            // Bottom Face
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            // Right face
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            // Left Face
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            // Front Face
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.End();

            GL.Disable(EnableCap.CullFace);

            GL.EndList();

            // Actor pyramid
            GL.NewList(ActorPyramidGLID, ListMode.Compile);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            GL.Begin(BeginMode.Triangles);
            // Front face
            GL.Vertex3(0.0f, 2.0f, 0.0f);
            GL.Vertex3(-2.0f, -2.0f, 2.0f);
            GL.Vertex3(2.0f, -2.0f, 2.0f);
            // Right face
            GL.Vertex3(0.0f, 2.0f, 0.0f);
            GL.Vertex3(2.0f, -2.0f, 2.0f);
            GL.Vertex3(2.0f, -2.0f, -2.0f);
            // Back face
            GL.Vertex3(0.0f, 2.0f, 0.0f);
            GL.Vertex3(2.0f, -2.0f, -2.0f);
            GL.Vertex3(-2.0f, -2.0f, -2.0f);
            // Left face
            GL.Vertex3(0.0f, 2.0f, 0.0f);
            GL.Vertex3(-2.0f, -2.0f, -2.0f);
            GL.Vertex3(-2.0f, -2.0f, 2.0f);
            GL.End();

            GL.Begin(BeginMode.Quads);
            // Bottom face
            GL.Vertex3(-2.0f, -2.0f, -2.0f);
            GL.Vertex3(2.0f, -2.0f, -2.0f);
            GL.Vertex3(2.0f, -2.0f, 2.0f);
            GL.Vertex3(-2.0f, -2.0f, 2.0f);
            GL.End();

            GL.Disable(EnableCap.CullFace);

            GL.EndList();

            GL.NewList(DoorGLID, ListMode.Compile);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            GL.Begin(BeginMode.Quads);
            // Back Face
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-3.0f, 0.0f, -0.5f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-3.0f, 10.0f, -0.50f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(3.0f, 10.0f, -0.5f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3.0f, 0.0f, -0.5f);
            // Top Face
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-3.0f, 10.0f, -0.5f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-3.0f, 10.0f, 0.5f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(3.0f, 10.0f, 0.5f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(3.0f, 10.0f, -0.5f);
            // Bottom Face
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-3.0f, 0f, -0.5f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(3.0f, 0f, -0.5f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3.0f, 0f, 0.5f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-3.0f, 0f, 0.5f);
            // Right face
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(3.0f, 0f, -0.5f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(3.0f, 10.0f, -0.5f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(3.0f, 10.0f, 0.5f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3.0f, 0f, 0.5f);
            // Left Face
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-3.0f, 0f, -0.5f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-3.0f, 0f, 0.5f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-3.0f, 10.0f, 0.5f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-3.0f, 10.0f, -0.5f);
            // Front Face
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-3.0f, 0f, 0.5f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(3.0f, 0f, 0.5f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(3.0f, 10f, 0.5f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-3.0f, 10f, 0.5f);
            GL.End();

            GL.Disable(EnableCap.CullFace);

            GL.EndList();

            // continue...
            IsReady = true;

            GL.ClearColor(Color.Black);
            SetViewport(glControl1.Width, glControl1.Height);


            Camera.Initialize();

            datatemplate = new ushort[4];
            Array.Clear(datatemplate, 0, datatemplate.Length);

            SongItem[] objs = new[]
            {
                new SongItem {Text = "0002 - Overworld", Value = 0x0002},
                new SongItem {Text = "0003 - Dungeon", Value = 0x0003},
                new SongItem {Text = "0000 - None", Value = 0x0000},
            };
            SpecialObjectComboBox.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "00 - No data", Value = 0x00},
                new SongItem {Text = "01 - Overworld", Value = 0x01},
                new SongItem {Text = "02 - Dungeon", Value = 0x02},
            };
            ElfMessageComboBox.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "00 - Standard dialog", Value = 0x00},
                new SongItem {Text = "01 - Branching dialog", Value = 0x01},
                new SongItem {Text = "02 - Learn song", Value = 0x02},
            };
            CutsceneTextboxType.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "Texture Scrolling", Value = 0x01},
                new SongItem {Text = "Animated Color Blending", Value = 0x02},
                new SongItem {Text = "Single Texture Swap", Value = 0x03},
                new SongItem {Text = "Multiple Texture Swap With Frames", Value = 0x04},
                new SongItem {Text = "Camera effect", Value = 0x05},
                new SongItem {Text = "Conditional draw", Value = 0x06},
            };
            RenderFunctionType.Items.AddRange(objs);

            RenderFunctionType.SelectedIndex = 0;

            objs = new[]
            {
                new SongItem {Text = "Earthquake effect", Value = 0x00},
                new SongItem {Text = "Jabu-jabu effect", Value = 0x01},
            };
            FunctionCameraEffectDropdown.Items.AddRange(objs);

            FunctionCameraEffectDropdown.SelectedIndex = 0;


            ReloadXMLs();

            RefreshRecetMenuItems(ref openSceneToolStripMenuItem, "SceneFile");
            RefreshRecetMenuItems(ref OpenGlobalROM, "GlobalFile");

            RefreshRecentRoms();

            // SceneSettingsComboBox.SetHorizontalExtent();

            Log.Console = true;
            Log.Debug = true;
            Log.Prefix = "[Update] ";


            Updater updater = new Updater();
#if !DEBUG
            updater.StartMonitoring();
#endif
            // updater.StopMonitoring();

            renderer = new TextRendering.TextRenderer(glControl1.Width, glControl1.Height);

        }

        private void ReloadXMLs()
        {

            string gameprefix = (!settings.MajorasMask) ? "OOT/" : "MM/";

            SongComboBox.Items.Clear();
            SongComboBox.Items.AddRange(XMLreader.getXMLItems(gameprefix + "SongNames", "Song"));

            SoundSpec.Items.Clear();
            SoundSpec.Items.AddRange(XMLreader.getXMLItems(gameprefix + "SoundSpecs", "Spec"));

            NightSFXComboBox.Items.Clear();
            NightSFXComboBox.Items.AddRange(XMLreader.getXMLItems(gameprefix + "NightSFX", "SFX"));

            SkyboxComboBox.Items.Clear();
            SkyboxComboBox.Items.AddRange(XMLreader.getXMLItems(gameprefix + "Skybox", "Skybox"));

            WorldMapComboBox.Items.Clear();
            WorldMapComboBox.Items.AddRange(XMLreader.getXMLItems(gameprefix + "Worldmap", "Worldmap"));

            RestrictionComboBox.Items.Clear();
            RestrictionComboBox.Items.AddRange(XMLreader.getXMLItems(gameprefix + "Miscelaneous", "Restriction"));

            IdleAnimComboBox.Items.Clear();
            IdleAnimComboBox.Items.AddRange(XMLreader.getXMLItems(gameprefix + "Miscelaneous", "Idleanim"));

            CameraMovementComboBox.Items.Clear();
            CameraMovementComboBox.Items.AddRange(XMLreader.getXMLItems(gameprefix + "Miscelaneous", "Camera"));

            CameraType.Items.Clear();
            CameraType.Items.AddRange(XMLreader.getXMLItems(gameprefix + "CameraTypes", "Camera"));

            XmlNodeList animnodes = XMLreader.getXMLNodes("OOT/" + "SceneAnimations", "Function");

            int incr = 0;
            AnimationItem[] animoutput = new AnimationItem[animnodes.Count];
            if (animnodes != null)
                foreach (XmlNode node in animnodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    animoutput[incr] = new AnimationItem();
                    animoutput[incr].Text = nodeAtt["Key"].Value + " - " + nodeAtt["Name"].Value;
                    animoutput[incr].Value = Convert.ToInt64(nodeAtt["Key"].Value, 16);
                    // animoutput[incr].Bank = Convert.ToByte(nodeAtt["Bank"].Value, 16);
                    animoutput[incr].Transparent = nodeAtt["Transparent"] != null;
                    animoutput[incr].Light = nodeAtt["Light"] != null;
                    animoutput[incr].Multitexture = nodeAtt["Multitexture"] != null;
                    incr++;
                }
            SceneSettingsComboBox.Items.Clear();
            SceneSettingsComboBox.Items.AddRange(animoutput);


            XmlNodeList markernodes = XMLreader.getXMLNodes(gameprefix + "CutsceneMarkers", "Marker");
            MarkerItem[] markernodesoutput = new MarkerItem[markernodes.Count];
            incr = 0;
            if (markernodes != null)
                foreach (XmlNode node in markernodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    markernodesoutput[incr] = new MarkerItem();
                    markernodesoutput[incr].Text = nodeAtt["Key"].Value + " - " + node.InnerText;
                    markernodesoutput[incr].Value = Convert.ToInt64(nodeAtt["Key"].Value, 16);
                    markernodesoutput[incr].Tab = Convert.ToByte(nodeAtt["Tab"].Value);
                    if (markernodesoutput[incr].Tab == 6)
                    {
                        markernodesoutput[incr].Actor = nodeAtt["Actor"].Value;
                        markernodesoutput[incr].Type = nodeAtt["Type"].Value;
                    }
                    else
                    {
                        markernodesoutput[incr].Actor = "";
                        markernodesoutput[incr].Type = "";
                    }
                    incr++;
                }
            MarkerType.Items.Clear();

            if (settings.UndocumentedCutsceneVars)
            {
                List<MarkerItem> tmp = markernodesoutput.ToList();

                for (int i = 1; i < 0xFF; i++)
                {
                    if (i == 2 || i == 6) continue;
                    if (tmp.Find(x => Convert.ToInt32(x.Value) == i) == null)
                    {
                        MarkerItem mk = new MarkerItem();

                        mk.Text = i.ToString("X4") + " - " + "???";
                        mk.Value = i;
                        mk.Tab = 6;
                        mk.Actor = "Placeholder";
                        mk.Type = "Actor";
                        tmp.Add(mk);
                    }
                }

                //tmp = new List<MarkerItem>(tmp.OrderBy(x => Convert.ToInt32(x.Value)));

                markernodesoutput = tmp.ToArray();
            }

            MarkerType.Items.AddRange(markernodesoutput);

            MarkerType.SelectedIndex = 0;

            flaglist.Clear();
            XmlNodeList flagnodes = XMLreader.getXMLNodes(gameprefix + "ModelFlags", "Flag");

            if (flagnodes != null)
                foreach (XmlNode node in flagnodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    flaglist.Add(nodeAtt["Key"].Value, Convert.ToUInt64(nodeAtt["Value"].Value, 16));
                }

            CutsceneAsmComboBox.Items.Clear();
            CutsceneAsmComboBox.Items.AddRange(XMLreader.getXMLItems(gameprefix + "CutsceneAsm", "Command"));

            CutsceneTransitionComboBox.Items.Clear();
            CutsceneTransitionComboBox.Items.AddRange(XMLreader.getXMLItems(gameprefix + "CutsceneTransition", "Transition"));

            XmlNodeList renderflagnodes = XMLreader.getXMLNodes("OOT/" + "RenderFunctions", "Flag");

            incr = 0;
            FlagItem[] flagoutput = new FlagItem[renderflagnodes.Count];
            if (renderflagnodes != null)
                foreach (XmlNode node in renderflagnodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    flagoutput[incr] = new FlagItem();
                    flagoutput[incr].Text = (nodeAtt["Key"].Value) + " - " + node.InnerText;
                    flagoutput[incr].Value = Convert.ToInt64(nodeAtt["Key"].Value, 16);
                    flagoutput[incr].MinValue = (nodeAtt["MinValue"] != null) ? Convert.ToUInt32(nodeAtt["MinValue"].Value, 16) : 0;
                    flagoutput[incr].MaxValue = (nodeAtt["MaxValue"] != null) ? Convert.ToUInt32(nodeAtt["MaxValue"].Value, 16) : 0;
                    flagoutput[incr].Bitwise = nodeAtt["Bitwise"] != null;
                    incr++;
                }
            RenderFunctionFlagType.Items.Clear();
            RenderFunctionFlagType.Items.AddRange(flagoutput);

            RenderFunctionFlagType.SelectedIndex = 0;

            XmlNodeList flagpresetnodes = XMLreader.getXMLNodes("OOT/" + "RenderFunctions", "Preset");

            incr = 0;
            FlagPreset[] flagoutput2 = new FlagPreset[flagpresetnodes.Count];
            if (flagpresetnodes != null)
                foreach (XmlNode node in flagpresetnodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    flagoutput2[incr] = new FlagPreset();
                    flagoutput2[incr].Text = node.InnerText;
                    flagoutput2[incr].Name = node.InnerText;
                    flagoutput2[incr].Value = Convert.ToUInt32(nodeAtt["Value"].Value, 16);
                    flagoutput2[incr].Type = (byte)((nodeAtt["Type"] != null) ? Convert.ToByte(nodeAtt["Type"].Value, 16) : 0);
                    flagoutput2[incr].Bitwise = (nodeAtt["Bitwise"] != null) ? Convert.ToUInt32(nodeAtt["Bitwise"].Value, 16) : 0;
                    flagoutput2[incr].Reverse = nodeAtt["Reverse"] != null;
                    flagoutput2[incr].Click += new System.EventHandler(this.SetFlagPreset);
                    incr++;
                }
            RenderFunctionFlagPresetButton.DropDownItems.Clear();
            RenderFunctionFlagPresetButton.DropDownItems.AddRange(flagoutput2);

        }

        private void DrawSkybox()
        {
            if (!SimulateN64Gfx) return;

            GL.PushMatrix();
            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Enable(EnableCap.Light0);

            //ABCD
            Vector3d campos = GetTrueCameraPosition();

            GL.Translate(campos.X, campos.Y, campos.Z);


            skyboxangle -= 0.01;
            GL.Rotate(skyboxangle, 0.0f, 1.0f, 0.0f);
            //GL.Rotate(Actor.XRot / 182.04444444444444444444444444444f, 1.0f, 0.0f, 0.0f);
            // GL.Rotate(Actor.ZRot / 182.04444444444444444444444444444f, 0.0f, 0.0f, 1.0f);
            GL.Scale(10f, 10f, 10f);

            List<UcodeSimulator.DisplayListStruct> skyboxdls = new List<UcodeSimulator.DisplayListStruct>();

            if (!settings.MajorasMask)
            {
                if (CurrentScene.SkyboxType == 0x01 && !CurrentScene.Cloudy)
                    skyboxdls = skyboxdlists[0];
                else if (CurrentScene.SkyboxType == 0x01 && CurrentScene.Cloudy)
                    skyboxdls = skyboxdlists[1];
                else if (CurrentScene.SkyboxType == 0x05)
                    skyboxdls = skyboxdlists[2];
            }
            else
            {
                if (CurrentScene.SkyboxType == 0x06 || CurrentScene.SkyboxType == 0x0A)
                    skyboxdls = skyboxdlists[3];
            }


            foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in skyboxdls)
            {
                GL.CallList(DL.GLID);
            }

            GL.PopMatrix();
            GL.PopAttrib();




        }

        private void DrawActorModel(ZActor Actor, Color FillColor, int DrawModelGLID, bool DrawAxis, bool DrawBorder)
        {
            DrawModelGLID = transitionids.Contains(Actor.Number) ? DoorGLID : DrawModelGLID;

            GL.PushMatrix();

            GL.Translate(Actor.XPos, Actor.YPos, Actor.ZPos);

            if (settings.MajorasMask && settings.IgnoreMMDaySystem == false)
            {
                GL.Rotate(((ushort)Actor.YRot & 0xFF80) >> 7, 0.0f, 1.0f, 0.0f);
                GL.Rotate(((ushort)Actor.XRot & 0xFF80) >> 7, 1.0f, 0.0f, 0.0f);
                GL.Rotate(((ushort)Actor.ZRot & 0xFF80) >> 7, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                GL.Rotate(Actor.YRot / 182.04444444444444444444444444444f, 0.0f, 1.0f, 0.0f);
                GL.Rotate(Actor.XRot / 182.04444444444444444444444444444f, 1.0f, 0.0f, 0.0f);
                GL.Rotate(Actor.ZRot / 182.04444444444444444444444444444f, 0.0f, 0.0f, 1.0f);
            }

            GL.Scale(10.0f, 10.0f, 10.0f);

            GL.Color4(FillColor);

            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable((EnableCap)All.FragmentProgram);
            GL.Disable(EnableCap.Lighting);


            for (int j = 0; j < 2; j++)
            {
                // If we're doing the outline, set some stuff prior to rendering
                if (j == 1)
                {
                    if (DrawBorder)
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                        GL.Color4(Color.Black.R, Color.Black.G, Color.Black.B, 1.0f);

                        GL.Enable(EnableCap.PolygonOffsetLine);
                        GL.PolygonOffset(-3.0f, -3.0f);
                    }
                }

                if (SimulateN64Gfx && DrawBorder && FindActorRender(Actor.Number, Actor.Variable) != -1 && j != 1 && settings.RenderActors) { }
                else { GL.CallList(DrawModelGLID); }

                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Disable(EnableCap.PolygonOffsetLine);
            }





            if (DrawAxis && settings.DisplayAxis)
            {
                if (transitionids.Contains(Actor.Number))
                {
                    GL.Translate(0, 5, 0);
                }
                GL.CallList(AxisMarkerGLID);


            }

            GL.PopMatrix();
            GL.PopAttrib();


        }

        private void DrawActorModel2(ZActor Actor, bool DrawBorder, bool transparency)
        {
            if (!SimulateN64Gfx) return;

            int render = FindActorRender((ushort)((Actor.Number == 0 && settings.RenderChildLink) ? 0xFFFF : Actor.Number), Actor.Variable);

            if (render != -1 && DrawBorder)
            {
                if (zobj_cache[render].Limbs.Count == 0)
                {
                    GL.PushMatrix();
                    GL.PushAttrib(AttribMask.AllAttribBits);
                    GL.Enable(EnableCap.Light0);
                    //ABCD
                    GL.Translate(Actor.XPos, Actor.YPos + zobj_cache[render].Yoff, Actor.ZPos);


                    if (settings.MajorasMask && settings.IgnoreMMDaySystem == false)
                    {
                        GL.Rotate(((ushort)Actor.YRot & 0xFF80) >> 7, 0.0f, 1.0f, 0.0f);
                        GL.Rotate(((ushort)Actor.XRot & 0xFF80) >> 7, 1.0f, 0.0f, 0.0f);
                        GL.Rotate(((ushort)Actor.ZRot & 0xFF80) >> 7, 0.0f, 0.0f, 1.0f);
                    }
                    else
                    {
                        GL.Rotate(Actor.YRot / 182.04444444444444444444444444444f, 0.0f, 1.0f, 0.0f);
                        GL.Rotate(Actor.XRot / 182.04444444444444444444444444444f, 1.0f, 0.0f, 0.0f);
                        GL.Rotate(Actor.ZRot / 182.04444444444444444444444444444f, 0.0f, 0.0f, 1.0f);
                    }
                    GL.Scale(zobj_cache[render].scale, zobj_cache[render].scale, zobj_cache[render].scale);




                    foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in zobj_cache[render].DLists)
                    {




                        if (DL.IsTransparent == transparency)
                        {
                            /*
                            int DEcommand = DL.Commands.FindIndex(x => x.ID == 0xDE && x.w1 > 0x07000000);
                            if (DEcommand != -1)
                            {//quack

                                
                                Random r = new Random();
                                UcodeSimulator.DLCommandStruct command = new UcodeSimulator.DLCommandStruct();
                                command.ID = 0x00;
                                command.w0 = (uint) ((uint) r.Next(0,0xFFFFFF) | 0x00 << 24);
                                command.w1 = (uint)r.Next(0, 0xFFFFFF);
                                DL.Commands[DEcommand] = command;

                                //DL.GLID = GL.GenLists(1);

                               // SayakaGL.UcodeSimulator.ParseDL(DL);

                              //  UcodeSimulator.ParseAllDLs(ref zobj_cache[render].DLists);


                                GL.CallList(DL.GLID);

                                command.ID = 0xDE;
                                command.w0 = 0xDE000000;
                                command.w1 = 0x08000000;
                                DL.Commands[DEcommand] = command;
                            }
                            else*/
                            GL.CallList(DL.GLID);
                        }
                    }

                    GL.PopMatrix();
                    GL.PopAttrib();
                }
                else
                {
                    GL.PushMatrix();
                    GL.Translate(0, (float)zobj_cache[render].Yoff, 0);
                    GL.PushAttrib(AttribMask.AllAttribBits);
                    GL.Enable(EnableCap.Light0);
                    DrawBone(zobj_cache[render].Limbs, 0, Actor, zobj_cache[render].scale, transparency);
                    GL.PopAttrib();
                    GL.PopMatrix();
                }
            }


        }

        void DrawBone(List<ZScene.Limb> Limbs, int CurrentBone, ZActor Actor, float scale, bool transparency)
        {
            GL.PushMatrix();


            /* Apply rotation in the order z, y, x! */

            if (CurrentBone == 0)
            {

                GL.Translate(Actor.XPos, Actor.YPos, Actor.ZPos);
                if (settings.MajorasMask && settings.IgnoreMMDaySystem == false)
                {
                    GL.Rotate(((ushort)Actor.ZRot & 0xFF80) >> 7, 0.0f, 0.0f, 1.0f);
                    GL.Rotate(((ushort)Actor.YRot & 0xFF80) >> 7, 0.0f, 1.0f, 0.0f);
                    GL.Rotate(((ushort)Actor.XRot & 0xFF80) >> 7, 1.0f, 0.0f, 0.0f);
                }
                else
                {
                    GL.Rotate(Actor.ZRot / 182.04444444444444444444444444444f, 0.0f, 0.0f, 1.0f);
                    GL.Rotate(Actor.YRot / 182.04444444444444444444444444444f, 0.0f, 1.0f, 0.0f);
                    GL.Rotate(Actor.XRot / 182.04444444444444444444444444444f, 1.0f, 0.0f, 0.0f);
                }
                GL.Scale(scale, scale, scale);
            }

            GL.Translate(Limbs[CurrentBone].x, Limbs[CurrentBone].y, Limbs[CurrentBone].z);

            GL.Rotate(Limbs[CurrentBone].rotation.Z / 182.04444444444444444444444444444f, 0.0f, 0.0f, 1.0f);
            GL.Rotate(Limbs[CurrentBone].rotation.Y / 182.04444444444444444444444444444f, 0.0f, 1.0f, 0.0f);
            GL.Rotate(Limbs[CurrentBone].rotation.X / 182.04444444444444444444444444444f, 1.0f, 0.0f, 0.0f);

            //Draw display list
            foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in Limbs[CurrentBone].DList)
            {
                if (DL.IsTransparent == transparency)
                    GL.CallList(DL.GLID);
            }

            //Draw child
            if (Limbs[CurrentBone].firstchild > -1)
                DrawBone(Limbs, Limbs[CurrentBone].firstchild, Actor, scale, transparency);

            GL.PopMatrix(); // pop matrix here!

            //Draw next
            if (Limbs[CurrentBone].nextchild > -1)
                DrawBone(Limbs, Limbs[CurrentBone].nextchild, Actor, scale, transparency);
        }

        private void DrawPathway(Vector3 Pathway, Vector3 NextPathway, Color FillColor, int DrawModelGLID, bool DrawAxis, bool DrawBorder, bool SelectedPathway)
        {
            GL.PushMatrix();

            if (NextPathway != NullVector)
            {

                GL.LineWidth(3);

                //  GL.PolygonOffset(-1, -1);
                GL.Begin(BeginMode.Lines);
                if (SelectedPathway)
                    GL.Color4(Color.Magenta);
                else
                    GL.Color4(Color.FromArgb(255, 170, 255));
                GL.Vertex3(Pathway.X, Pathway.Y, Pathway.Z);
                GL.Vertex3(NextPathway.X, NextPathway.Y, NextPathway.Z);
                GL.End();

                // GL.Clear(ClearBufferMask.DepthBufferBit);
                //GL.Enable(EnableCap.DepthTest);

            }
            GL.LineWidth(1);
            // GL.PolygonOffset(0, 0);

            GL.Translate(Pathway.X, Pathway.Y, Pathway.Z);

            GL.Scale(10.0f, 10.0f, 10.0f);

            GL.Color4(FillColor);

            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable((EnableCap)All.FragmentProgram);
            GL.Disable(EnableCap.Lighting);


            for (int j = 0; j < 2; j++)
            {
                // If we're doing the outline, set some stuff prior to rendering
                if (j == 1)
                {
                    if (DrawBorder)
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                        GL.Color4(Color.Black.R, Color.Black.G, Color.Black.B, 1.0f);

                        GL.Enable(EnableCap.PolygonOffsetLine);
                        GL.PolygonOffset(-3.0f, -3.0f);
                    }
                }

                GL.CallList(DrawModelGLID);

                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Disable(EnableCap.PolygonOffsetLine);
            }




            if (DrawAxis && settings.DisplayAxis)
                GL.CallList(AxisMarkerGLID);

            GL.PopMatrix();
            GL.PopAttrib();
        }

        private void DrawCameraPoint(Vector3 CameraPoint, Vector3 NextCameraPoint, Vector3 CameraPoint2, int DrawModelGLID, byte SelectedCameraPoint)
        {
            GL.PushMatrix();

            if (NextCameraPoint != NullVector)
            {

                GL.LineWidth(3);

                //  GL.PolygonOffset(-1, -1);
                GL.Begin(BeginMode.Lines);
                if (SelectedCameraPoint != 0)
                    GL.Color4(Color.Yellow);
                else
                    GL.Color4(Color.LightYellow);
                GL.Vertex3(CameraPoint.X, CameraPoint.Y, CameraPoint.Z);
                GL.Vertex3(NextCameraPoint.X, NextCameraPoint.Y, NextCameraPoint.Z);
                GL.End();
            }

            GL.Begin(BeginMode.Lines);
            if (SelectedCameraPoint != 0)
                GL.Color4(Color.DarkOrange);
            else
                GL.Color4(Color.LightSalmon);
            GL.Vertex3(CameraPoint.X, CameraPoint.Y, CameraPoint.Z);
            GL.Vertex3(CameraPoint2.X, CameraPoint2.Y, CameraPoint2.Z);
            GL.End();

            // GL.Clear(ClearBufferMask.DepthBufferBit);
            //GL.Enable(EnableCap.DepthTest);


            GL.LineWidth(1);
            // GL.PolygonOffset(0, 0);

            GL.Translate(CameraPoint.X, CameraPoint.Y, CameraPoint.Z);

            GL.Scale(10.0f, 10.0f, 10.0f);

            if (SelectedCameraPoint != 0)
                GL.Color4(Color.DarkSlateGray);
            else
                GL.Color4(Color.LightSlateGray);

            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable((EnableCap)All.FragmentProgram);
            GL.Disable(EnableCap.Lighting);


            for (int j = 0; j < 2; j++)
            {
                // If we're doing the outline, set some stuff prior to rendering
                if (j == 1)
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    GL.Color4(Color.Black.R, Color.Black.G, Color.Black.B, 1.0f);

                    GL.Enable(EnableCap.PolygonOffsetLine);
                    GL.PolygonOffset(-3.0f, -3.0f);

                }

                GL.CallList(DrawModelGLID);

                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Disable(EnableCap.PolygonOffsetLine);
            }

            if (SelectedCameraPoint == 1 && settings.DisplayAxis)
                GL.CallList(AxisMarkerGLID);

            GL.PopMatrix();
            GL.PopAttrib();
            GL.PushMatrix();

            GL.Translate(CameraPoint2.X, CameraPoint2.Y, CameraPoint2.Z);

            GL.Scale(10.0f, 10.0f, 10.0f);

            if (SelectedCameraPoint != 0)
                GL.Color4(Color.SaddleBrown);
            else
                GL.Color4(Color.RosyBrown);

            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable((EnableCap)All.FragmentProgram);
            GL.Disable(EnableCap.Lighting);


            for (int j = 0; j < 2; j++)
            {
                // If we're doing the outline, set some stuff prior to rendering
                if (j == 1)
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    GL.Color4(Color.Black.R, Color.Black.G, Color.Black.B, 1.0f);

                    GL.Enable(EnableCap.PolygonOffsetLine);
                    GL.PolygonOffset(-3.0f, -3.0f);

                }

                GL.CallList(DrawModelGLID);

                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Disable(EnableCap.PolygonOffsetLine);
            }




            if (SelectedCameraPoint == 2 && settings.DisplayAxis)
                GL.CallList(AxisMarkerGLID);

            GL.PopMatrix();
            GL.PopAttrib();
        }

        private void DrawCutsceneActor(Vector3 StartPos, Vector3 EndPos, Vector3 Rotation, int DrawModelGLID, byte SelectedCutsceneActor)
        {
            GL.PushMatrix();



            GL.Begin(BeginMode.Lines);
            if (SelectedCutsceneActor != 0)
                GL.Color4(Color.DarkOrange);
            else
                GL.Color4(Color.LightSalmon);
            GL.Vertex3(StartPos.X, StartPos.Y, StartPos.Z);
            GL.Vertex3(EndPos.X, EndPos.Y, EndPos.Z);
            GL.End();

            // GL.Clear(ClearBufferMask.DepthBufferBit);
            //GL.Enable(EnableCap.DepthTest);


            GL.LineWidth(1);
            // GL.PolygonOffset(0, 0);

            GL.Translate(StartPos.X, StartPos.Y, StartPos.Z);
            GL.Rotate(Rotation.X / 182.04444444444444444444444444444f, 1.0f, 0.0f, 0.0f);
            GL.Rotate(Rotation.Y / 182.04444444444444444444444444444f, 0.0f, 1.0f, 0.0f);
            GL.Rotate(Rotation.Z / 182.04444444444444444444444444444f, 0.0f, 0.0f, 1.0f);

            GL.Scale(10.0f, 10.0f, 10.0f);

            if (SelectedCutsceneActor != 0)
                GL.Color4(Color.DarkSlateGray);
            else
                GL.Color4(Color.LightSlateGray);

            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable((EnableCap)All.FragmentProgram);
            GL.Disable(EnableCap.Lighting);


            for (int j = 0; j < 2; j++)
            {
                // If we're doing the outline, set some stuff prior to rendering
                if (j == 1)
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    GL.Color4(Color.Black.R, Color.Black.G, Color.Black.B, 1.0f);

                    GL.Enable(EnableCap.PolygonOffsetLine);
                    GL.PolygonOffset(-3.0f, -3.0f);

                }

                GL.CallList(DrawModelGLID);

                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Disable(EnableCap.PolygonOffsetLine);
            }

            if (SelectedCutsceneActor == 1 && settings.DisplayAxis)
                GL.CallList(AxisMarkerGLID);

            GL.PopMatrix();
            GL.PopAttrib();
            GL.PushMatrix();

            GL.Translate(EndPos.X, EndPos.Y, EndPos.Z);

            GL.Scale(10.0f, 10.0f, 10.0f);

            if (SelectedCutsceneActor != 0)
                GL.Color4(Color.SaddleBrown);
            else
                GL.Color4(Color.RosyBrown);

            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable((EnableCap)All.FragmentProgram);
            GL.Disable(EnableCap.Lighting);


            for (int j = 0; j < 2; j++)
            {
                // If we're doing the outline, set some stuff prior to rendering
                if (j == 1)
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    GL.Color4(Color.Black.R, Color.Black.G, Color.Black.B, 1.0f);

                    GL.Enable(EnableCap.PolygonOffsetLine);
                    GL.PolygonOffset(-3.0f, -3.0f);

                }

                GL.CallList(DrawModelGLID);

                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Disable(EnableCap.PolygonOffsetLine);
            }




            if (SelectedCutsceneActor == 2 && settings.DisplayAxis)
                GL.CallList(AxisMarkerGLID);

            GL.PopMatrix();
            GL.PopAttrib();
        }

        private void DrawPointLight(ZAdditionalLight Light, Color FillColor, int DrawModelGLID, bool DrawAxis, bool DrawBorder)
        {
            GL.PushMatrix();

            GL.Translate(Light.XPos, Light.YPos, Light.ZPos);

            GL.Scale(10.0f, 10.0f, 10.0f);

            GL.Color4(FillColor);

            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable((EnableCap)All.FragmentProgram);
            GL.Disable(EnableCap.Lighting);


            for (int j = 0; j < 2; j++)
            {
                // If we're doing the outline, set some stuff prior to rendering
                if (j == 1)
                {
                    if (DrawBorder)
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                        GL.Color4(Color.Black.R, Color.Black.G, Color.Black.B, 1.0f);

                        GL.Enable(EnableCap.PolygonOffsetLine);
                        GL.PolygonOffset(-3.0f, -3.0f);
                    }
                }

                GL.CallList(DrawModelGLID);

                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Disable(EnableCap.PolygonOffsetLine);
            }




            if (DrawAxis && settings.DisplayAxis)
                GL.CallList(AxisMarkerGLID);

            GL.PopMatrix();
            GL.PopAttrib();
        }

        private void DrawFixedCamera(ZCamera Camera, Color FillColor, int DrawModelGLID, bool DrawAxis, bool DrawBorder)
        {
            GL.PushMatrix();

            GL.Translate(Camera.XPos, Camera.YPos, Camera.ZPos);

            //  GL.Rotate(90, 1.0f, 0.0f, 0.0f);
            //  GL.Rotate(90, 0.0f, 0.0f, 1.0f);

            GL.Rotate(Camera.ZRot / 182.04444444444444444444444444444f, 0.0f, 0.0f, 1.0f);
            GL.Rotate(Camera.YRot / 182.04444444444444444444444444444f, 0.0f, 1.0f, 0.0f);
            GL.Rotate(Camera.XRot / 182.04444444444444444444444444444f, 1.0f, 0.0f, 0.0f);


            GL.Scale(10.0f, 10.0f, 10.0f);

            GL.Color4(FillColor);

            GL.PushAttrib(AttribMask.AllAttribBits);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable((EnableCap)All.FragmentProgram);
            GL.Disable(EnableCap.Lighting);


            for (int j = 0; j < 2; j++)
            {
                // If we're doing the outline, set some stuff prior to rendering
                if (j == 1)
                {
                    if (DrawBorder)
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                        GL.Color4(Color.Black.R, Color.Black.G, Color.Black.B, 1.0f);

                        GL.Enable(EnableCap.PolygonOffsetLine);
                        GL.PolygonOffset(-3.0f, -3.0f);
                    }
                }

                GL.CallList(DrawModelGLID);

                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Disable(EnableCap.PolygonOffsetLine);
            }




            if (DrawAxis && settings.DisplayAxis)
                GL.CallList(AxisMarkerGLID);

            GL.PopMatrix();
            GL.PopAttrib();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            PaintControl();
        }

        private DateTime fpsTimer = DateTime.Now;
        private double[] fpsRingTime = new double[10];
        private int fpsRingIndex;

        private int GetFPS()
        {
            double time = DateTime.Now.Subtract(fpsTimer).TotalSeconds;
            fpsTimer = DateTime.Now;

            fpsRingTime[fpsRingIndex] = time;
            fpsRingIndex = (fpsRingIndex + 1) % 10;

            double result = 0;

            for (int i = 0; i < 10; i++)
            {
                result += fpsRingTime[i];
            }
            result /= 10;

            return (int)(1.0 / result);
        }

        public void PaintControl()
        {
            if (IsReady == false)
                return;

            //SwapBuffers();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();



            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 2.5f, 2.5f, 2.5f, 2.5f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 0.0f, 25.0f, 0.0f, 0.0f });

            if (CurrentScene != null && CurrentScene.Environments.Count != 0 && EnvironmentSelect.Value > 0)
            {


            }

            Camera.Position();

            GL.Enable(EnableCap.DepthTest);

            GL.Scale(0.005f, 0.005f, 0.005f);

            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Normalize);
            GL.Disable(EnableCap.Lighting);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            if (subscreenmode == true)
            {

                GL.ClearColor(Color.Aqua);

                ZScene.ZRoom Room = CurrentScene.Rooms[subscreenroom];

                GL.PushAttrib(AttribMask.AllAttribBits);

                /* Prepare... */
                GL.PushMatrix();
                GL.Scale(CurrentScene.Scale, CurrentScene.Scale, CurrentScene.Scale);

                GL.Enable(EnableCap.CullFace);
                GL.CullFace(CullFaceMode.Back);


                /* Render groups... */
                for (int i = 0; i < Room.TrueGroups.Count; i++)
                {
                    GL.Enable(EnableCap.Texture2D);
                    GL.Color4(Color.FromArgb(0xFF, 0, 0, 0));
                    Room.ObjModel.Render(Room.TrueGroups[i]);
                }

                GL.PopMatrix();


                GL.PopAttrib();

                glControl1.SwapBuffers();
                return;
            }

            if (CurrentScene != null && CurrentScene.Prerendered && CurrentScene.Cameras.Count > 0 && previewscenecamera)
            {
                //TODO


                if (!notresize)
                {
                    notresize = true;

                    glControl1.Size = new Size(prevwidth, (int)(prevheight * 0.75));
                    FormBorderStyle = FormBorderStyle.FixedDialog;

                }

                GL.Color4(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0, 320, 240, 0, -1, 1);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                GL.LoadIdentity();
                GL.Disable(EnableCap.DepthTest);
                GL.Enable(EnableCap.Blend);
                GL.Enable(EnableCap.Texture2D);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                int textureID;

                Bitmap bitmap = new Bitmap(CurrentScene.prerenderimages[CurrentScene.Cameras[(int)CameraSelect.Value].Unk1 < CurrentScene.prerenderimages.Count ? CurrentScene.Cameras[(int)CameraSelect.Value].Unk1 : 0]);

                GL.GenTextures(1, out textureID);
                GL.BindTexture(TextureTarget.Texture2D, textureID);

                BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);


                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.BindTexture(TextureTarget.Texture2D, textureID);

                GL.Begin(BeginMode.Quads);
                GL.TexCoord2(0, 0);
                GL.Vertex2(0, 0);
                GL.TexCoord2(0, 1);
                GL.Vertex2(0, 240);
                GL.TexCoord2(1, 1);
                GL.Vertex2(320, 240);
                GL.TexCoord2(1, 0);
                GL.Vertex2(320, 0);
                GL.End();

                GL.DeleteTexture(textureID);

                GL.Disable(EnableCap.Texture2D);
                GL.Disable(EnableCap.Blend);
                GL.Enable(EnableCap.DepthTest);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                SetViewport(glControl1.Width, glControl1.Height);

                Camera.Position();
                GL.Scale(0.005f, 0.005f, 0.005f);

            }
            else if (!previewcamerapoints)
            {
                notresize = false;
            }

            labelcamerapos.Text = Camera.Pos.X.ToString("F") + " X " + Camera.Pos.Y.ToString("F") + " Y " + Camera.Pos.Z.ToString("F") + " Z ";

            if (CurrentScene != null && NowLoading == false)
            {
                /* Rendering base settings... */
                if (CurrentScene.Environments.Count > 0 && ((!settings.MajorasMask && CurrentScene.SkyboxType == 0x1D) || (settings.MajorasMask && CurrentScene.SkyboxType == 0x08)))
                {
                    EnvironmentSelect.Value = EnvironmentSelect.Value < 0 ? 0 : EnvironmentSelect.Value;
                    GL.ClearColor(CurrentScene.Environments[(int)EnvironmentSelect.Value].FogColorC);
                }
                else if (CurrentScene.OutdoorLight)
                    GL.ClearColor(Color.FromArgb(255, 51, 128, 179));
                else
                    GL.ClearColor(Color.Black);

                if (CurrentScene.Environments.Count != 0 && (int)EnvironmentSelect.Value >= 0)
                {
                    GL.Light(LightName.Light0, LightParameter.Diffuse, Color.FromArgb(
                        CurrentScene.Environments[(int)EnvironmentSelect.Value].C3C.A,
                        CurrentScene.Environments[(int)EnvironmentSelect.Value].C3C.R,
                        CurrentScene.Environments[(int)EnvironmentSelect.Value].C3C.G,
                        CurrentScene.Environments[(int)EnvironmentSelect.Value].C3C.B));


                    GL.Fog(FogParameter.FogMode, (int)FogMode.Linear);
                    GL.Hint(HintTarget.FogHint, HintMode.Nicest);

                    if (CurrentScene.Environments.Count > 0)
                        GL.Fog(FogParameter.FogColor, new float[] { CurrentScene.Environments[(int)EnvironmentSelect.Value].FogColorC.R / 255.0f, CurrentScene.Environments[(int)EnvironmentSelect.Value].FogColorC.G / 255.0f, CurrentScene.Environments[(int)EnvironmentSelect.Value].FogColorC.B / 255.0f });
                    else
                        GL.Fog(FogParameter.FogColor, new float[] { 0, 0, 0 });


                    float near = CurrentScene.Environments[(int)(EnvironmentSelect.Value)].FogDistance;
                    float far = 1000f;

                    float fogMultiply = (500.0f * 256.0f) / (far - near);
                    float fogOffset = (500.0f - near) * 256.0f / (far - near);

                    //  Console.WriteLine("Fog Multiply: " + fogMultiply);
                    //  Console.WriteLine("Fog Offset: " + fogOffset);

                    //   GL.Fog(FogParameter.)


                    GL.Fog(FogParameter.FogStart, (float)CurrentScene.Environments[(int)(EnvironmentSelect.Value)].FogDistance / 100f);
                    GL.Fog(FogParameter.FogEnd, 10f);
                }

                foreach (ZScene.ZRoom Room in CurrentScene.Rooms)
                {
                    /* Render room actors... */
                    if (Room == CurrentScene.Rooms[RoomList.SelectedIndex])
                    {
                        GL.Disable(EnableCap.Texture2D);
                        for (int i = 0; i < Room.ZActors.Count; i++)
                        {
                            DrawActorModel(Room.ZActors[i],
                                (i == actorEditControl1.ActorNumber ? Color.FromArgb(0, 255, 0) : Color.FromArgb(192, 255, 192)),
                                ActorCubeGLID,
                                Convert.ToBoolean(i == actorEditControl1.ActorNumber), true);
                        }
                        /* Render room additional lights... */
                        for (int i = 0; i < Room.AdditionalLights.Count; i++)
                        {
                            if (Room.AdditionalLights[i].PointLight)
                            {
                                DrawPointLight(Room.AdditionalLights[i],
                                    (i == AdditionalLightSelect.Value - 1 ? Color.FromArgb(255, 128, 0) : Color.FromArgb(255, 198, 70)),
                                    ActorCubeGLID,
                                    Convert.ToBoolean(i == AdditionalLightSelect.Value - 1), true);
                            }
                        }
                    }

                }


                if (CurrentScene.SkyboxType == 0x01 || CurrentScene.SkyboxType == 0x05 || CurrentScene.SkyboxType == 0x06 || CurrentScene.SkyboxType == 0x0A)
                    DrawSkybox();

                /* Render spawn points... */
                GL.Disable(EnableCap.Texture2D);
                for (int i = 0; i < CurrentScene.SpawnPoints.Count; i++)
                    DrawActorModel(CurrentScene.SpawnPoints[i],
                        (i == actorEditControl3.ActorNumber ? Color.FromArgb(0, 0, 255) : Color.FromArgb(192, 192, 255)),
                        ActorPyramidGLID,
                        Convert.ToBoolean(i == actorEditControl3.ActorNumber), true);

                /* Render transition actors... */
                for (int i = 0; i < CurrentScene.Transitions.Count; i++)
                    DrawActorModel(CurrentScene.Transitions[i],
                        (i == actorEditControl2.ActorNumber ? Color.FromArgb(255, 0, 0) : Color.FromArgb(255, 192, 192)),
                        ActorPyramidGLID,
                        Convert.ToBoolean(i == actorEditControl2.ActorNumber), true);

                /* Render pathways */
                for (int i = 0; i < CurrentScene.Pathways.Count; i++)
                {
                    for (int ii = 0; ii < CurrentScene.Pathways[i].Points.Count; ii++)
                    {
                        Vector3 NextVector;
                        if (ii < CurrentScene.Pathways[i].Points.Count - 1)
                            NextVector = CurrentScene.Pathways[i].Points[ii + 1];
                        else
                            NextVector = NullVector;
                        DrawPathway(CurrentScene.Pathways[i].Points[ii], NextVector,
                            (ii == PathwayListBox.SelectedIndex && i == PathwayNumber.Value ? Color.FromArgb(255, 255, 0) : Color.FromArgb(255, 255, 192)),
                            ActorPyramidGLID,
                            Convert.ToBoolean(i == PathwayNumber.Value && ii == PathwayListBox.SelectedIndex), true,
                            Convert.ToBoolean(i == PathwayNumber.Value));
                    }
                }

                /* Render fixed camera points... */
                for (int i = 0; i < CurrentScene.Cameras.Count; i++)
                    DrawFixedCamera(CurrentScene.Cameras[i],
                        (i == CameraSelect.Value ? Color.FromArgb(30, 30, 30) : Color.FromArgb(75, 75, 75)),
                        ActorPyramidGLID,
                        Convert.ToBoolean(i == CameraSelect.Value), true);

                /* Render camera points only when the tab is selected */
                if (tabControl1.SelectedIndex == 8)
                {
                    for (int i = 0; i < CurrentScene.Cutscene.Count; i++)
                    {
                        if ((settings.DrawSelectedCutsceneCommands && MarkerSelect.SelectedIndex != i) || previewcutscene) continue;

                        if (((!settings.MajorasMask) && CurrentScene.Cutscene[i].Marker == 0x01 || CurrentScene.Cutscene[i].Marker == 0x05) || (settings.MajorasMask && CurrentScene.Cutscene[i].Marker == 0x5A)) // if its camera position list
                        {
                            for (int ii = 0; ii < CurrentScene.Cutscene[i].Points.Count; ii++)
                            {
                                Vector3 NextVector;
                                byte selected = 0;

                                if (MarkerSelect.SelectedIndex == i && CutsceneAbsolutePositionListBox.SelectedIndex == ii) selected = (byte)(1 + extrapick);

                                if (ii < CurrentScene.Cutscene[i].Points.Count - 1)
                                    NextVector = CurrentScene.Cutscene[i].Points[ii + 1].Position;
                                else
                                    NextVector = NullVector;
                                DrawCameraPoint(CurrentScene.Cutscene[i].Points[ii].Position, NextVector, CurrentScene.Cutscene[i].Points[ii].Position2,
                                    ActorPyramidGLID,
                                    selected);

                            }
                        }
                    }

                    for (int i = 0; i < CurrentScene.Cutscene.Count; i++)
                    {
                        if (settings.DrawSelectedCutsceneCommands && MarkerSelect.SelectedIndex != i) continue;
                        if (CurrentScene.Cutscene[i].CutsceneActors.Count > 0 && CurrentScene.Cutscene[i].Data[0] != 0xFF)
                        {
                            for (int ii = 0; ii < CurrentScene.Cutscene[i].CutsceneActors.Count; ii++)
                            {
                                byte selected = 0;

                                if (MarkerSelect.SelectedIndex == i && CutsceneActorListBox.SelectedIndex == ii) selected = (byte)(1 + extrapick);

                                DrawCutsceneActor(CurrentScene.Cutscene[i].CutsceneActors[ii].Position, CurrentScene.Cutscene[i].CutsceneActors[ii].Position2, CurrentScene.Cutscene[i].CutsceneActors[ii].Rotation,
                                    ActorPyramidGLID,
                                    selected);

                            }
                        }
                    }
                }

                int incr3 = 0;
                foreach (ZScene.ZRoom Room in NormalHeader.Rooms)
                {
                    if (settings.ShowRoomModels == true)
                    {
                        GL.PushAttrib(AttribMask.AllAttribBits);

                        if (SimulateN64Gfx == false)
                        {
                            /* Prepare... */
                            GL.PushMatrix();
                            GL.Scale(CurrentScene.Scale, CurrentScene.Scale, CurrentScene.Scale);

                            GL.Enable(EnableCap.CullFace);
                            GL.CullFace(CullFaceMode.Back);

                            /* Faked environmental lighting... */
                            if (settings.ApplyEnvLighting == true)
                            {
                                GL.Enable(EnableCap.ColorMaterial);
                                GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.Diffuse);
                                GL.Enable(EnableCap.Normalize);
                                GL.Enable(EnableCap.Lighting);
                                GL.Enable(EnableCap.Light0);
                                GL.Enable(EnableCap.Fog);
                            }
                            else
                            {
                                GL.Disable(EnableCap.Fog);
                            }

                            /* Render groups... */
                            for (int i = 0; i < Room.TrueGroups.Count; i++)
                            {
                                GL.Enable(EnableCap.Texture2D);
                                GL.Color4(Color.FromArgb((int)Room.TrueGroups[i].TintAlpha));
                                Room.ObjModel.Render(Room.TrueGroups[i]);
                            }

                            GL.PopMatrix();
                        }
                        else if (Room.N64DLists != null)
                        {//aaa

                            GL.Enable(EnableCap.Light0);


                            // AdditionalTexturesGLID.Clear();

                            //  UpdateAdditionalTextureGLIDs();


                            foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in Room.N64DLists)
                            {
                                if (!DL.IsTransparent)
                                {
                                    if ((DL.Animation != 0 && DL.Animation - 8 < CurrentScene.SegmentFunctions.Count && CurrentScene.SegmentFunctions[DL.Animation - 8].HasScroll())
                                        || (DL.TextureAnimation != 0 && DL.TextureAnimation - 8 < CurrentScene.SegmentFunctions.Count && CurrentScene.SegmentFunctions[DL.TextureAnimation - 8].HasPointer())
                                        || (DL.ColorAnimation != 0 && DL.ColorAnimation - 8 < CurrentScene.SegmentFunctions.Count && CurrentScene.SegmentFunctions[DL.ColorAnimation - 8].HasBlending()))
                                    {
                                        AdvancedCallList(DL);
                                    }
                                    else
                                        GL.CallList(DL.GLID);
                                }

                            }



                            if (incr3 == RoomList.SelectedIndex && settings.RenderActors)
                            {

                                for (int i = 0; i < CurrentScene.Rooms[incr3].ZActors.Count; i++)
                                {
                                    DrawActorModel2(CurrentScene.Rooms[incr3].ZActors[i], true, false);
                                }
                            }




                            foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in Room.N64DLists)
                            {
                                if (DL.IsTransparent)
                                {
                                    if (DL.Animation != 0 && DL.Animation - 8 < CurrentScene.SegmentFunctions.Count && CurrentScene.SegmentFunctions[DL.Animation - 8].HasScroll())
                                    {
                                        AdvancedCallList(DL);
                                    }
                                    else
                                        GL.CallList(DL.GLID);
                                }

                            }
                        }
                        GL.PopAttrib();
                    }




                    incr3++;
                }


                if (settings.RenderActors)
                {
                    // GL.Disable(EnableCap.Texture2D);
                    for (int i = 0; i < CurrentScene.Transitions.Count; i++)
                    {
                        DrawActorModel2(CurrentScene.Transitions[i], true, false);
                        DrawActorModel2(CurrentScene.Transitions[i], true, true);
                    }

                    for (int i = 0; i < CurrentScene.SpawnPoints.Count; i++)
                    {
                        DrawActorModel2(CurrentScene.SpawnPoints[i], true, false);
                        DrawActorModel2(CurrentScene.SpawnPoints[i], true, true);
                    }
                }




                foreach (ZScene.ZRoom Room in CurrentScene.Rooms)
                {

                    if (Room == CurrentScene.Rooms[RoomList.SelectedIndex] && settings.RenderActors)
                    {
                        // GL.Disable(EnableCap.Texture2D);
                        /*
                        Vector3d camerapos = GetTrueCameraPosition();
                        var sortedactors = from element in Room.ZActors
                                          orderby Distance3D(new Vector3(element.XPos,element.YPos,element.ZPos),  (Vector3)camerapos) descending
                                           select element;*/

                        for (int i = 0; i < Room.ZActors.Count; i++)
                        {
                            DrawActorModel2(Room.ZActors[i], true, true);
                        }
                    }
                }


                /* Render waterboxes... */
                if ((settings.OnlyRenderWaterboxesGeneral && tabControl1.SelectedIndex == 0) || !settings.OnlyRenderWaterboxesGeneral)
                {
                    GL.Disable(EnableCap.CullFace);
                    int incr = 0;
                    foreach (ZWaterbox WBox in CurrentScene.Waterboxes)
                    {
                        GL.PushMatrix();

                        GL.Translate(WBox.XPos, WBox.YPos, WBox.ZPos);

                        for (int i = 0; i < 2; i++)
                        {
                            if (i == 0)
                            {
                                if (incr == (int)WaterboxSelect.Value)
                                    GL.Color4(0.0f, 0.0f, 1.0f, 0.6f);
                                else
                                    GL.Color4(0.4f, 0.4f, 1.0f, 0.5f);
                            }
                            else
                            {
                                GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);
                                GL.Color4(Color.Black);
                            }

                            GL.Begin(BeginMode.Quads);
                            GL.Vertex3(0.0f, 1.0f, 0.0f);
                            GL.Vertex3(0.0f, 1.0f, WBox.ZSize);
                            GL.Vertex3(WBox.XSize, 1.0f, WBox.ZSize);
                            GL.Vertex3(WBox.XSize, 1.0f, 0.0f);
                            GL.End();
                        }

                        GL.PopMatrix();

                        GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);

                        incr++;
                    }
                }




                /* Render collision model... */
                if (settings.ShowCollisionModel == true && CurrentScene.ColModel != null)
                {
                    GL.PushMatrix();
                    GL.Scale(CurrentScene.Scale, CurrentScene.Scale, CurrentScene.Scale);
                    CurrentScene.ColModel.Render();
                    GL.PopMatrix();
                }

                /* Render group highlight... */
                if (((ObjFile.Group)GroupList.SelectedItem) != null && (tabControl1.SelectedIndex == 1 || (tabControl1.SelectedIndex != 1 && selectedtimer > 0)) && customcombiner == null)
                {
                    GL.PushMatrix();
                    GL.PushAttrib(AttribMask.AllAttribBits);
                    GL.Scale(CurrentScene.Scale, CurrentScene.Scale, CurrentScene.Scale);

                    GL.Disable((EnableCap)All.FragmentProgram);
                    GL.Disable(EnableCap.Texture2D);
                    GL.Enable(EnableCap.Blend);
                    GL.Enable(EnableCap.PolygonOffsetFill);
                    GL.PolygonOffset(-5.0f, -5.0f);

                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                    if (tabControl1.SelectedIndex == 1) GL.Color4(1.0f, 0.5f, 0.0f, 0.5f);
                    else
                    {
                        GL.Color4(1.0f, 0.5f, 0.0f, selectedtimer > 60 ? 0.5f : 0.0083f * selectedtimer);
                    }
                    CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Render(((ObjFile.Group)GroupList.SelectedItem));

                    GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
                    GL.PolygonOffset(0.0f, 0.0f);

                    GL.PopMatrix();
                    GL.PopAttrib();
                }
            }

            if (previewcutscene)
            {
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0, 320, 240, 0, -1, 1);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Disable(EnableCap.DepthTest);
                GL.Enable(EnableCap.Blend);
                GL.Enable(EnableCap.Texture2D);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


                GL.Begin(BeginMode.Quads);   //We want to draw a quad, i.e. shape with four sides
                GL.Color3(0, 0, 0); //Set the colour to red
                GL.Vertex2(0, 0);            //Draw the four corners of the rectangle
                GL.Vertex2(0, 32);
                GL.Vertex2(320, 32);
                GL.Vertex2(320, 0);
                GL.End();

                GL.Begin(BeginMode.Quads);   //We want to draw a quad, i.e. shape with four sides
                GL.Color3(0, 0, 0); //Set the colour to red
                GL.Vertex2(0, 240 - 32);            //Draw the four corners of the rectangle
                GL.Vertex2(0, 240);
                GL.Vertex2(320, 240);
                GL.Vertex2(320, 240 - 32);
                GL.End();

                GL.Disable(EnableCap.Texture2D);
                //GL.Disable(EnableCap.Blend);
                GL.Enable(EnableCap.DepthTest);

                SetViewport(glControl1.Width, glControl1.Height);
            }

            bool betatextdraw = false;

            if (betatextdraw) //beta draw text
            {
                string text = "FPS " + GetFPS();


                if (text != viewporttext[0])
                {

                    renderer.Clear(Color.Transparent);

                    PointF position = PointF.Empty;
                    viewporttext[0] = text;

                    // GL.Color4(Color.FromArgb(0xFF, 0, 0, 0));
                    renderer.DrawString(viewporttext[0], zeldafont, Brushes.Black, position);
                    position.X += 2;
                    renderer.DrawString(viewporttext[0], zeldafont, Brushes.Black, position);
                    position.Y += 2;
                    renderer.DrawString(viewporttext[0], zeldafont, Brushes.Black, position);
                    position.X -= 2;
                    renderer.DrawString(viewporttext[0], zeldafont, Brushes.Black, position);

                    position.X += 1;
                    position.Y -= 1;

                    //  GL.Color4(Color.FromArgb(0xFF, 0xFF, 0xFF, 0));
                    renderer.DrawString(viewporttext[0], zeldafont, Brushes.White, position);

                }


                GL.Color4(Color.FromArgb(0xFF, 0xFF, 0xFF, 0));

                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0, 320, 240, 0, -1, 1);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Disable(EnableCap.DepthTest);
                GL.Enable(EnableCap.Blend);
                GL.Enable(EnableCap.Texture2D);

                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.BindTexture(TextureTarget.Texture2D, renderer.Texture);

                GL.Begin(BeginMode.Quads);

                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(0, 0);            //Draw the four corners of the rectangle
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(0, 240);
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(320, 240);
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(320, 0);

                GL.End();


                GL.Disable(EnableCap.Texture2D);
                GL.Disable(EnableCap.Blend);
                GL.Enable(EnableCap.DepthTest);

                SetViewport(glControl1.Width, glControl1.Height);

            }

            glControl1.SwapBuffers();
        }


        private Color Interpolate(Color color1, Color color2, double fraction)
        {
            double r = Interpolate(color1.R, color2.R, fraction);
            double g = Interpolate(color1.G, color2.G, fraction);
            double b = Interpolate(color1.B, color2.B, fraction);
            double a = Interpolate(color1.A, color2.A, fraction);
            return Color.FromArgb((int)Math.Round(a), (int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b));
        }

        private double Interpolate(double d1, double d2, double fraction)
        {
            return d1 + (d2 - d1) * fraction;
        }

        public void AdvancedCallList(UcodeSimulator.DisplayListStruct DL)
        {
            if (settings.MajorasMask)
            {
                GL.MatrixMode(MatrixMode.Texture);

                GL.PushMatrix();


                GL.ActiveTexture(TextureUnit.Texture0);
                GL.Translate(-(CurrentScene.TextureAnims[DL.Animation - 8].XVelocity1 / 80f * (20f / CurrentScene.TextureAnims[DL.Animation - 8].Width1)) * globalframe, -(CurrentScene.TextureAnims[DL.Animation - 8].YVelocity1 / 80f * (20f / CurrentScene.TextureAnims[DL.Animation - 8].Height1)) * globalframe, 0);

                GL.ActiveTexture(TextureUnit.Texture1);
                GL.PushMatrix();

                GL.Translate(-(CurrentScene.TextureAnims[DL.Animation - 8].XVelocity2 / 80f * (20f / CurrentScene.TextureAnims[DL.Animation - 8].Width2)) * globalframe, -(CurrentScene.TextureAnims[DL.Animation - 8].YVelocity2 / 80f * (20f / CurrentScene.TextureAnims[DL.Animation - 8].Height2)) * globalframe, 0);

                GL.MatrixMode(MatrixMode.Modelview);

                GL.ActiveTexture(TextureUnit.Texture0);

                GL.CallList(DL.GLID);

                GL.MatrixMode(MatrixMode.Texture);
                GL.ActiveTexture(TextureUnit.Texture1);
                GL.PopMatrix();
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.PopMatrix();


                GL.MatrixMode(MatrixMode.Modelview);
            }
            else
            {

                bool drawn = false;

                if (DL.ColorAnimation != 0)
                {


                    ZTextureAnim targetfunction = CurrentScene.SegmentFunctions[DL.ColorAnimation - 8].Functions.Find(x => x.Type == ZTextureAnim.blending && x.Preview);

                    if (targetfunction != null && targetfunction.ColorList.Count > 0)
                    {


                        int totalframes = 0;

                        int tempframe = 0;

                        //Vector3 currentcolor = new Vector3();

                        //Vector3 targetcolor = new Vector3();

                        Color currentcolorC = new Color();

                        Color targetcolorC = new Color();



                        foreach (ZTextureAnimColor col in targetfunction.ColorList)
                        {
                            totalframes += col.Duration;
                        }

                        int currentframe = globalframe % totalframes;
                        int relativeframe = currentframe;

                        for (int i = 0; i < targetfunction.ColorList.Count; i++)
                        {
                            tempframe += targetfunction.ColorList[i].Duration;
                            if (currentframe < tempframe)
                            {
                                //targetcolor = new Vector3(targetfunction.ColorList[i].C1C.R / 255f, targetfunction.ColorList[i].C1C.G / 255f, targetfunction.ColorList[i].C1C.B / 255f);
                                targetcolorC = targetfunction.ColorList[i].C1C;

                                if (targetfunction.ColorList.Count == 1)
                                {
                                    //currentcolor = targetcolor;
                                    currentcolorC = targetcolorC;
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        //  currentcolor = new Vector3(targetfunction.ColorList[targetfunction.ColorList.Count - 1].C1C.R / 255f, targetfunction.ColorList[targetfunction.ColorList.Count - 1].C1C.G / 255f, targetfunction.ColorList[targetfunction.ColorList.Count - 1].C1C.B / 255f);
                                        currentcolorC = targetfunction.ColorList[targetfunction.ColorList.Count - 1].C1C;
                                    }
                                    else
                                    {
                                        //     currentcolor = new Vector3(targetfunction.ColorList[i - 1].C1C.R / 255f, targetfunction.ColorList[i-1].C1C.G / 255f, targetfunction.ColorList[i - 1].C1C.B / 255f);
                                        currentcolorC = targetfunction.ColorList[i - 1].C1C;
                                    }
                                }
                                float lerpamount = 0f;

                                //Console.WriteLine((currentframe % targetfunction.ColorList[i].Duration));

                                if ((relativeframe) != 0) lerpamount = (1.0f / targetfunction.ColorList[i].Duration) * (relativeframe);

                                //old formula
                                //if ((currentframe % targetfunction.ColorList[i].Duration) != 0) lerpamount = (1.0f/ targetfunction.ColorList[i].Duration) * (currentframe % targetfunction.ColorList[i].Duration);


                                // Console.WriteLine("currentframe " + currentframe + " asd " + (currentframe % targetfunction.ColorList[i].Duration +" i " + i + " lerp: " + lerpamount + " target duration " + targetfunction.ColorList[i].Duration));


                                if (float.IsNaN(lerpamount))
                                {
                                    lerpamount = 1f;

                                }

                                //Vector3 result = new Vector3();



                                // result = Vector3.Lerp(currentcolor, targetcolor, lerpamount);


                                Color resultC = Interpolate(currentcolorC, targetcolorC, lerpamount);


                                //lerpamount = 0.5f;

                                // if (float.IsNaN(result.X)) result.X = 0;
                                //  if (float.IsNaN(result.Y)) result.Y = 0;
                                // if (float.IsNaN(result.Z)) result.Z = 0;



                                // Console.WriteLine("current " + currentcolor + "target " + targetcolor + "result " + result + "lerp amount " + lerpamount);

                                // GL.Arb.ProgramEnvParameter4(AssemblyProgramTargetArb.FragmentProgram, 0, result.X, result.Y, result.Z, 1f);

                                GL.Arb.ProgramEnvParameter4(AssemblyProgramTargetArb.FragmentProgram, 0, resultC.R / 255f, resultC.G / 255f, resultC.B / 255f, resultC.A / 255f);

                                break;
                            }
                            relativeframe -= targetfunction.ColorList[i].Duration;
                        }




                    }

                }

                if (DL.Animation != 0)
                {
                    ZTextureAnim TextureScroll = CurrentScene.SegmentFunctions[DL.Animation - 8].Functions.Find(x => x.Type == ZTextureAnim.scroll && x.Preview);





                    GL.MatrixMode(MatrixMode.Texture);

                    GL.PushMatrix();




                    GL.ActiveTexture(TextureUnit.Texture0);
                    if (TextureScroll != null) GL.Translate(-(TextureScroll.XVelocity1 / 80f * (20f / TextureScroll.Width1)) * globalframe, -(TextureScroll.YVelocity1 / 80f * (20f / TextureScroll.Height1)) * globalframe, 0);

                    GL.ActiveTexture(TextureUnit.Texture1);
                    GL.PushMatrix();

                    if (TextureScroll != null) GL.Translate(-(TextureScroll.XVelocity2 / 80f * (20f / TextureScroll.Width2)) * globalframe, -(TextureScroll.YVelocity2 / 80f * (20f / TextureScroll.Height2)) * globalframe, 0);

                    GL.MatrixMode(MatrixMode.Modelview);

                    GL.ActiveTexture(TextureUnit.Texture0);




                    GL.CallList(DL.GLID);

                    //  GL.Arb.ProgramEnvParameter4(AssemblyProgramTargetArb.FragmentProgram, 0, 0.9f, 0.9f, 0, 1f);

                    GL.MatrixMode(MatrixMode.Texture);
                    GL.ActiveTexture(TextureUnit.Texture1);
                    GL.PopMatrix();
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.PopMatrix();


                    GL.MatrixMode(MatrixMode.Modelview);

                    drawn = true;
                }
                else if (DL.TextureAnimation != 0)
                {

                    int totalframes = 0;

                    int tempframe = 0;


                    ZTextureAnim targetfunction = CurrentScene.SegmentFunctions[DL.TextureAnimation - 8].Functions.Find(x => x.Type == ZTextureAnim.texframe && x.Preview);

                    if (targetfunction == null)
                    {
                        targetfunction = CurrentScene.SegmentFunctions[DL.TextureAnimation - 8].Functions.Find(x => x.Type == ZTextureAnim.texswap && x.Preview);


                    }

                    if (targetfunction != null)
                    {

                        int textureglid = -1;

                        if (targetfunction.Type == ZTextureAnim.texframe)
                        {
                            foreach (ZTextureAnimImage img in targetfunction.TextureSwapList)
                            {
                                totalframes += img.Duration;
                            }

                            if (totalframes > 0)

                            {

                                int currentframe = globalframe % totalframes;


                                for (int i = 0; i < targetfunction.TextureSwapList.Count; i++)
                                {
                                    tempframe += targetfunction.TextureSwapList[i].Duration;
                                    if (currentframe <= tempframe)
                                    {
                                        //textureglid = AdditionalTexturesGLID[CurrentScene.AdditionalTextures.FindIndex(x => x.DisplayName == targetfunction.TextureSwapList[i].Texture)].GLID;

                                        //textureglid -= AdditionalTexturesGLID.Count;

                                        textureglid = CurrentScene.AdditionalTextures.Find(x => x.DisplayName == targetfunction.TextureSwapList[i].Texture).GLID;

                                        // Console.WriteLine("Texture GLID" + textureglid);
                                        // textureglid = i;
                                        break;
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (CurrentScene.AdditionalTextures.FindIndex(x => x.DisplayName == targetfunction.TextureSwap2) != -1)
                                textureglid = CurrentScene.AdditionalTextures.Find(x => x.DisplayName == targetfunction.TextureSwap2).GLID;

                        }

                        if (textureglid == -1) return;




                        // textureglid = new Random().Next(AdditionalTexturesGLID.Count+1);


                        GL.ActiveTexture(TextureUnit.Texture0);
                        GL.Disable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, 0);
                        //    GL.ActiveTexture(TextureUnit.Texture1);
                        //   GL.Disable(EnableCap.Texture2D);
                        //  GL.BindTexture(TextureTarget.Texture2D, 0);

                        // UcodeSimulator.CalculateTextureSize(textureglid);
                        GL.ActiveTexture(TextureUnit.Texture0);
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, textureglid);

                        /*

                        GL.ActiveTexture(TextureUnit.Texture1);
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, 1);*/

                        // GL.ActiveTexture(TextureUnit.Texture1);
                        //  GL.Disable(EnableCap.Texture2D);


                        GL.ActiveTexture(TextureUnit.Texture0);
                        GL.Disable(EnableCap.Texture2D);


                        GL.CallList(DL.GLID);

                        //GL.BindTexture(TextureTarget.Texture2D, UcodeSimulator.NGraphics.Textures[0].GLID);

                        drawn = true;


                    }


                }
                if (!drawn)
                {
                    GL.CallList(DL.GLID);
                }
            }
        }

        public void UpdateAdditionalTextureGLIDs()
        {
            return;
            foreach (ObjFile.Material mat in CurrentScene.AdditionalTextures)
            {
                if (AdditionalTexturesGLID.FindIndex(glid => glid.Filename == mat.DisplayName) == -1)
                {

                    UcodeSimulator.TextureCacheStruct NewCache = new UcodeSimulator.TextureCacheStruct();

                    int trueglid = AdditionalTexturesGLID.Count;

                    int GLID = GL.GenTexture();

                    Console.WriteLine("true glid: " + trueglid);
                    Console.WriteLine("glid " + GLID);

                    Bitmap bitmap = new Bitmap(mat.map_Kd);

                    // GL.GenTextures(1, out GLID);
                    GL.BindTexture(TextureTarget.Texture2D, GLID);

                    BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                        OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                    bitmap.UnlockBits(data);

                    if (mat.DisplayName.ToLower().Contains("#clampx"))
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
                    else if (mat.DisplayName.Contains("#mirrorx"))
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.MirroredRepeatArb);
                    else
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);

                    if (mat.DisplayName.Contains("#clampy"))
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);
                    else if (mat.DisplayName.Contains("#mirrory"))
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.MirroredRepeatArb);
                    else
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);

                    // GL.BindTexture(TextureTarget.Texture2D, GLID);

                    NewCache.GLID = GLID;
                    NewCache.Filename = mat.DisplayName;

                    GL.DeleteTexture(GLID);

                    AdditionalTexturesGLID.Add(NewCache);


                }
            }
        }

        public Bitmap TakeScreenshot()
        {
            if (GraphicsContext.CurrentContext == null)
                throw new GraphicsContextMissingException();
            int w = glControl1.ClientSize.Width;
            int h = glControl1.ClientSize.Height;
            Bitmap bmp = new Bitmap(w, h);
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(glControl1.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, w, h, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }

        public void GLrefresh()
        {
            glControl1.Invalidate();
            glControl1.Update();
            glControl1.Refresh();
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (IsReady == false)
                return;

            SetViewport(glControl1.Width, glControl1.Height);
            if (!notresize)
            {
                prevwidth = glControl1.Width;
                prevheight = glControl1.Height;
            }
            glControl1.Invalidate();
        }

        void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            KeysDown[(int)(e.KeyCode & Keys.KeyCode)] = true;

            if (e.KeyCode == Keys.F && CurrentScene != null && CurrentScene.Rooms.Count > 0)
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabActors"] && CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.Count > 0)
                {
                    /*
                    double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
                    double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

                    Vector3d truepos = GetTrueCameraPosition();

                    if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
                    {
                        truepos.Y += (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 6000f;
                    }
                    else
                    {
                        truepos.X -= (float)Math.Sin(RotYRad) * Camera.CameraCoeff * 6000f;
                        truepos.Z += (float)Math.Cos(RotYRad) * Camera.CameraCoeff * 6000f;
                        truepos.Y += (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 6000f;
                    }
                    */

                }
            }
            if (KeysDown[(int)Keys.Z] && Control.ModifierKeys == Keys.Control && CurrentScene != null && CurrentScene.Rooms.Count > 0)
            {
                Undo();
            }
            if (KeysDown[(int)Keys.Y] && Control.ModifierKeys == Keys.Control && CurrentScene != null && CurrentScene.Rooms.Count > 0)
            {
                Undo(true);
            }
        }

        void glControl1_KeyUp(object sender, KeyEventArgs e)
        {
            KeysDown[(int)(e.KeyCode & Keys.KeyCode)] = false;

        }

        void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Mouse.LDown = true;
            else if (e.Button == MouseButtons.Right)
                Mouse.RDown = true;
            else if (e.Button == MouseButtons.Middle)
                Mouse.MDown = true;

            Mouse.Center = new Vector2d(e.X, e.Y);

            if (Mouse.LDown && !Mouse.MDown)
            {
                if (Mouse.Center != Mouse.Move)
                    Camera.MouseMove(Mouse.Move);
                else
                    Camera.MouseCenter(Mouse.Move);


            }
            if (Mouse.RDown && CurrentScene != null && CurrentScene.Rooms.Count > 0)
            {


                byte[] pixel = new byte[4];
                int[] viewport = new int[4];

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.Disable(EnableCap.Texture2D);

                bool selected = false;

                if (Control.ModifierKeys != Keys.Control)
                {
                    //Dictionary<int, float> objects = new Dictionary<int, float>;

                    for (int i = 0; i < CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.Count; i++)
                    {
                        if (i != actorEditControl1.ActorNumber)
                        {
                            DrawActorModel(CurrentScene.Rooms[RoomList.SelectedIndex].ZActors[i],
                            Color.FromArgb(192, 255, 192),
                            ActorCubeGLID,
                            false, false);

                            GL.GetInteger(GetPName.Viewport, viewport);
                            GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                            //   Console.WriteLine(pixel[0] + " " + pixel[1] + " " + pixel[2] + " " + pixel[3]);

                            if (pixel[0] == 192)
                            {
                                selected = true;
                                actorEditControl1.ActorComboBox.SelectedIndex = i;
                                actorEditControl1.UpdateActorEdit();
                                actorEditControl1.UpdateForm();
                                break;
                            }
                        }


                    }

                    if (!selected)
                    {
                        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                        /* Render transition actors... */
                        for (int i = 0; i < CurrentScene.Transitions.Count; i++)
                        {
                            if (i != actorEditControl2.ActorNumber)
                            {
                                DrawActorModel(CurrentScene.Transitions[i],
                                Color.FromArgb(255, 192, 192),
                                ActorPyramidGLID,
                                false, false);

                                GL.GetInteger(GetPName.Viewport, viewport);
                                GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                                if (pixel[0] == 255)
                                {
                                    selected = true;
                                    actorEditControl2.ActorComboBox.SelectedIndex = i;
                                    actorEditControl2.UpdateActorEdit();
                                    actorEditControl2.UpdateForm();
                                    break;
                                }
                            }

                        }
                    }

                    if (!selected)
                    {
                        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                        /* Render spawn points... */
                        for (int i = 0; i < CurrentScene.SpawnPoints.Count; i++)
                        {
                            if (i != actorEditControl3.ActorNumber)
                            {
                                DrawActorModel(CurrentScene.SpawnPoints[i],
                                Color.FromArgb(192, 192, 255),
                                ActorPyramidGLID,
                                false, false);

                                GL.GetInteger(GetPName.Viewport, viewport);
                                GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                                if (pixel[0] == 192)
                                {
                                    selected = true;
                                    actorEditControl3.ActorComboBox.SelectedIndex = i;
                                    actorEditControl3.UpdateActorEdit();
                                    actorEditControl3.UpdateForm();
                                    break;
                                }
                            }

                        }
                    }

                    if (!selected)
                    {
                        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                        /* Render pathways */
                        for (int i = 0; i < CurrentScene.Pathways.Count && !selected; i++)
                        {
                            for (int ii = 0; ii < CurrentScene.Pathways[i].Points.Count; ii++)
                            {
                                if (i != PathwayNumber.Value || ii != PathwayListBox.SelectedIndex)
                                {
                                    DrawPathway(CurrentScene.Pathways[i].Points[ii], NullVector,
                                        Color.FromArgb(255, 255, 192),
                                        ActorPyramidGLID,
                                        false, false,
                                        false);

                                    GL.GetInteger(GetPName.Viewport, viewport);
                                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                                    if (pixel[0] == 255)
                                    {
                                        //Console.WriteLine(i + " i " + ii + " ii");
                                        selected = true;
                                        PathwayNumber.Value = i;
                                        PathwayListBox.SelectedIndex = ii;
                                        UpdateForm();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (!selected)
                    {
                        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                        /* Render lightpoints... */
                        for (int i = 0; i < CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights.Count; i++)
                        {
                            if (i != AdditionalLightSelect.Value - 1 && CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[i].PointLight)
                            {
                                DrawPointLight(CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[i],
                                Color.FromArgb(255, 0, 0),
                                ActorCubeGLID,
                                false, false);

                                GL.GetInteger(GetPName.Viewport, viewport);
                                GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                                if (pixel[0] == 255)
                                {
                                    selected = true;
                                    AdditionalLightSelect.Value = i + 1;
                                    UpdateForm();
                                    break;
                                }
                            }

                        }
                    }

                    if (!selected)
                    {
                        /* Render camera points only when the tab is selected */
                        if (tabControl1.SelectedTab == tabControl1.TabPages["tabCutscene"])
                        {
                            for (int i = 0; i < CurrentScene.Cutscene.Count; i++)
                            {
                                if (settings.DrawSelectedCutsceneCommands && MarkerSelect.SelectedIndex != i) continue;

                                if (previewcutscene) continue;

                                if ((!settings.MajorasMask && CurrentScene.Cutscene[i].Marker == 0x01) || (settings.MajorasMask && CurrentScene.Cutscene[i].Marker == 0x5A)) // if its camera position list
                                {

                                    for (int ii = 0; ii < CurrentScene.Cutscene[i].Points.Count; ii++)
                                    {
                                        // if (i != MarkerSelect.SelectedIndex || ii != CutsceneAbsolutePositionListBox.SelectedIndex)
                                        // {
                                        Vector3 NextVector;
                                        byte sel = 0;

                                        if (CutsceneAbsolutePositionListBox.SelectedIndex == ii) sel = 1;

                                        if (ii < CurrentScene.Cutscene[i].Points.Count - 1)
                                            NextVector = CurrentScene.Cutscene[i].Points[ii + 1].Position;
                                        else
                                            NextVector = NullVector;
                                        DrawCameraPoint(CurrentScene.Cutscene[i].Points[ii].Position, NextVector, CurrentScene.Cutscene[i].Points[ii].Position2,
                                            ActorPyramidGLID,
                                            sel);

                                        GL.GetInteger(GetPName.Viewport, viewport);
                                        GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                                        if (pixel[0] == Color.LightSlateGray.R || pixel[0] == Color.RosyBrown.R)
                                        {
                                            selected = true;
                                            MarkerSelect.SelectedIndex = i;
                                            UpdateCutsceneEdit();
                                            CutsceneAbsolutePositionListBox.SelectedIndex = ii;
                                            extrapick = (byte)((pixel[0] == Color.RosyBrown.R) ? 1 : 0);
                                            UpdateForm();
                                            break;
                                        }
                                        else if (pixel[0] == Color.DarkSlateGray.R || pixel[0] == Color.SaddleBrown.R)
                                        {
                                            selected = true;
                                            extrapick = (byte)((pixel[0] == Color.SaddleBrown.R) ? 1 : 0);
                                            UpdateForm();
                                            break;
                                        }

                                        //}


                                    }
                                }
                            }
                        }
                    }

                    if (!selected)
                    {
                        /* Render cutscene actors only when the tab is selected */
                        if (tabControl1.SelectedTab == tabControl1.TabPages["tabCutscene"])
                        {
                            for (int i = 0; i < CurrentScene.Cutscene.Count; i++)
                            {
                                if (settings.DrawSelectedCutsceneCommands && MarkerSelect.SelectedIndex != i) continue;

                                if (CurrentScene.Cutscene[i].CutsceneActors.Count > 0 && CurrentScene.Cutscene[i].Data[0] != 0xFF)
                                {

                                    for (int ii = 0; ii < CurrentScene.Cutscene[i].CutsceneActors.Count; ii++)
                                    {

                                        Vector3 NextVector;
                                        byte sel = 0;

                                        if (CutsceneActorListBox.SelectedIndex == ii) sel = 1;

                                        DrawCutsceneActor(CurrentScene.Cutscene[i].CutsceneActors[ii].Position, CurrentScene.Cutscene[i].CutsceneActors[ii].Position2, CurrentScene.Cutscene[i].CutsceneActors[ii].Rotation,
                                            ActorPyramidGLID,
                                            sel);

                                        GL.GetInteger(GetPName.Viewport, viewport);
                                        GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                                        if (pixel[0] == Color.LightSlateGray.R || pixel[0] == Color.RosyBrown.R)
                                        {
                                            selected = true;
                                            MarkerSelect.SelectedIndex = i;
                                            UpdateCutsceneEdit();
                                            CutsceneActorListBox.SelectedIndex = ii;
                                            extrapick = (byte)((pixel[0] == Color.RosyBrown.R) ? 1 : 0);
                                            UpdateForm();
                                            break;
                                        }
                                        else if (pixel[0] == Color.DarkSlateGray.R || pixel[0] == Color.SaddleBrown.R)
                                        {
                                            selected = true;
                                            extrapick = (byte)((pixel[0] == Color.SaddleBrown.R) ? 1 : 0);
                                            UpdateForm();
                                            break;
                                        }

                                        //}


                                    }
                                }
                            }
                        }
                    }

                    if (!selected)
                    {
                        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                        /* Render cameras... */
                        for (int i = 0; i < CurrentScene.Cameras.Count; i++)
                        {
                            if (i != CameraSelect.Value)
                            {
                                DrawFixedCamera(CurrentScene.Cameras[i],
                                    Color.FromArgb(255, 0, 0),
                                    ActorPyramidGLID,
                                    false, false);

                                GL.GetInteger(GetPName.Viewport, viewport);
                                GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                                if (pixel[0] == 255)
                                {
                                    selected = true;
                                    CameraSelect.Value = i;
                                    UpdateForm();
                                    break;
                                }
                            }

                        }
                    }

                    if (!selected && ((settings.OnlyRenderWaterboxesGeneral && tabControl1.SelectedIndex == 0) || !settings.OnlyRenderWaterboxesGeneral))
                    {
                        /* Render waterboxes... */
                        GL.Disable(EnableCap.CullFace);
                        int incr = 0;
                        foreach (ZWaterbox WBox in CurrentScene.Waterboxes)
                        {
                            if (incr != (int)WaterboxSelect.Value)
                            {
                                GL.PushMatrix();
                                GL.Translate(WBox.XPos, WBox.YPos, WBox.ZPos);
                                GL.Color4(0.4f, 0.4f, 1.0f, 1.0f);
                                GL.Begin(BeginMode.Quads);
                                GL.Vertex3(0.0f, 1.0f, 0.0f);
                                GL.Vertex3(0.0f, 1.0f, WBox.ZSize);
                                GL.Vertex3(WBox.XSize, 1.0f, WBox.ZSize);
                                GL.Vertex3(WBox.XSize, 1.0f, 0.0f);
                                GL.End();
                                GL.PopMatrix();
                                GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);

                                GL.GetInteger(GetPName.Viewport, viewport);
                                GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                                if (pixel[2] == 255)
                                {
                                    selected = true;
                                    WaterboxSelect.Value = incr;
                                    UpdateWaterboxData();
                                    UpdateForm();
                                    break;
                                }

                            }

                            incr++;
                        }
                    }

                }
                else
                {

                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    GL.Disable(EnableCap.Texture2D);

                    GL.PushAttrib(AttribMask.AllAttribBits);

                    /* Prepare... */
                    GL.PushMatrix();
                    GL.Scale(CurrentScene.Scale, CurrentScene.Scale, CurrentScene.Scale);

                    GL.Enable(EnableCap.CullFace);
                    GL.CullFace(CullFaceMode.Back);

                    int roomselect = -1;
                    int groupselect = -1;


                    /* Render groups... */
                    for (int i = 0; i < CurrentScene.Rooms.Count; i++)
                    {
                        for (int y = 0; y < CurrentScene.Rooms[i].TrueGroups.Count; y++)
                        {
                            int green = y;
                            int red = 0;
                            while (green > 254) { green -= 254; red += 1; }
                            GL.Color4(Color.FromArgb(0xFF, 0x01 + red, 0x01 + green, 0x01 + i));
                            CurrentScene.Rooms[i].ObjModel.Render(CurrentScene.Rooms[i].TrueGroups[y]);


                            GL.GetInteger(GetPName.Viewport, viewport);
                            GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                            if (pixel[0] == 0x1 + red && pixel[1] == 0x1 + green && pixel[2] == 0x01 + i)
                            {
                                selected = true;
                                roomselect = i;
                                groupselect = y;

                                //    break;
                            }
                        }
                        //if (selected) break;
                    }

                    if (selected)
                    {
                        RoomList.SelectedIndex = roomselect;
                        //SelectRoom();


                        GroupList.SelectedIndex = groupselect;
                        selectedtimer = 90;
                        UpdateForm();
                    }

                    GL.PopMatrix();


                    GL.PopAttrib();

                    glControl1.SwapBuffers();
                    return;
                }


            }


            if (Mouse.MDown && CurrentScene != null && CurrentScene.Rooms.Count > 0)
            {
                byte[] pixel = new byte[4];
                int[] viewport = new int[4];

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.Disable(EnableCap.Texture2D);

                actorpick = -1;

                if (CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.Count > 0)
                {
                    DrawActorModel(CurrentScene.Rooms[RoomList.SelectedIndex].ZActors[actorEditControl1.ActorNumber],
                        Color.FromArgb(255, 0, 0),
                        ActorCubeGLID,
                        false, false);

                    GL.GetInteger(GetPName.Viewport, viewport);
                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                    if (pixel[0] == 255)
                    {
                        actorpick = _Actor_;
                    }
                }

                if (actorpick == -1 && CurrentScene.Transitions.Count > 0)
                {
                    DrawActorModel(CurrentScene.Transitions[actorEditControl2.ActorNumber],
                        Color.FromArgb(255, 0, 0),
                        ActorPyramidGLID,
                        false, false);

                    GL.GetInteger(GetPName.Viewport, viewport);
                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                    if (pixel[0] == 255)
                    {
                        actorpick = _Transition_;
                    }


                }

                if (actorpick == -1 && CurrentScene.SpawnPoints.Count > 0)
                {
                    DrawActorModel(CurrentScene.SpawnPoints[actorEditControl3.ActorNumber],
                        Color.FromArgb(255, 0, 0),
                        ActorPyramidGLID,
                        false, false);

                    GL.GetInteger(GetPName.Viewport, viewport);
                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                    if (pixel[0] == 255)
                    {
                        actorpick = _Spawn_;
                    }
                }
                // pathways
                if (actorpick == -1 && CurrentScene.Pathways.Count > 0)
                {
                    DrawPathway(CurrentScene.Pathways[(int)PathwayNumber.Value].Points[PathwayListBox.SelectedIndex], NullVector,
                        Color.FromArgb(255, 0, 0),
                        ActorPyramidGLID,
                        false, false, false);

                    GL.GetInteger(GetPName.Viewport, viewport);
                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                    if (pixel[0] == 255)
                    {
                        actorpick = _Pathway_;
                    }


                }

                if (actorpick == -1 && CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights.Count > 0)
                {
                    DrawPointLight(CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)(AdditionalLightSelect.Value - 1)],
                        Color.FromArgb(255, 0, 0),
                        ActorCubeGLID,
                        false, false);

                    GL.GetInteger(GetPName.Viewport, viewport);
                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                    if (pixel[0] == 255)
                    {
                        actorpick = _AddLight_;
                    }
                }

                if (actorpick == -1 && CurrentScene.Cutscene.Count > 0 && tabControl1.SelectedTab == tabControl1.TabPages["tabCutscene"] && ((!settings.MajorasMask && CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Marker == 0x01) || (settings.MajorasMask && CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Marker == 0x5A)))
                {
                    DrawCameraPoint(CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Position,
                        NullVector,
                        CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Position2,
                        ActorPyramidGLID,
                        1);

                    GL.GetInteger(GetPName.Viewport, viewport);
                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);
                    if (pixel[0] == Color.DarkSlateGray.R || pixel[0] == Color.SaddleBrown.R)
                    {
                        extrapick = (byte)((pixel[0] == Color.SaddleBrown.R) ? 1 : 0);
                        actorpick = _CutsceneCamera_;
                        // Console.WriteLine("Im simply famy " + extrapick + " aaa" + new Random().Next(44444));
                    }
                }

                if (actorpick == -1 && CurrentScene.Cutscene.Count > 0 && tabControl1.SelectedTab == tabControl1.TabPages["tabCutscene"] && (CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Count > 0))
                {
                    DrawCameraPoint(CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Position,
                        NullVector,
                        CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Position2,
                        ActorPyramidGLID,
                        1);

                    GL.GetInteger(GetPName.Viewport, viewport);
                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);
                    if (pixel[0] == Color.DarkSlateGray.R || pixel[0] == Color.SaddleBrown.R)
                    {
                        extrapick = (byte)((pixel[0] == Color.SaddleBrown.R) ? 1 : 0);
                        actorpick = _CutsceneActor_;
                    }
                }

                //fixed cameras
                if (actorpick == -1 && CurrentScene.Cameras.Count > 0)
                {
                    DrawFixedCamera(CurrentScene.Cameras[(int)CameraSelect.Value],
                        Color.FromArgb(255, 0, 0),
                        ActorPyramidGLID,
                        false, false);

                    GL.GetInteger(GetPName.Viewport, viewport);
                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                    if (pixel[0] == 255)
                    {
                        actorpick = _Camera_;
                    }
                }

                // waterboxes
                if (actorpick == -1 && CurrentScene.Waterboxes.Count > 0 && ((settings.OnlyRenderWaterboxesGeneral && tabControl1.SelectedIndex == 0) || !settings.OnlyRenderWaterboxesGeneral))
                {
                    ZWaterbox WBox = CurrentScene.Waterboxes[(int)WaterboxSelect.Value];

                    GL.PushMatrix();
                    GL.Translate(WBox.XPos, WBox.YPos, WBox.ZPos);
                    Color.FromArgb(255, 0, 0);
                    GL.Begin(BeginMode.Quads);
                    GL.Vertex3(0.0f, 1.0f, 0.0f);
                    GL.Vertex3(0.0f, 1.0f, WBox.ZSize);
                    GL.Vertex3(WBox.XSize, 1.0f, WBox.ZSize);
                    GL.Vertex3(WBox.XSize, 1.0f, 0.0f);
                    GL.End();
                    GL.PopMatrix();
                    GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);

                    GL.GetInteger(GetPName.Viewport, viewport);
                    GL.ReadPixels(e.X, viewport[3] - e.Y - 6, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

                    if (pixel[0] == 255)
                    {
                        actorpick = _Waterbox_;
                    }


                }


                StoreUndo(actorpick);

            }


        }

        void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            Mouse.Move = new Vector2d(e.X, e.Y);

            if (Mouse.LDown == true)
            {
                if (Mouse.Center != Mouse.Move)
                    Camera.MouseMove(Mouse.Move);
                else
                    Camera.MouseCenter(Mouse.Move);

                if (e.Button != MouseButtons.Left)
                    Mouse.LDown = false;
            }
            if (Mouse.MDown && CurrentScene != null && CurrentScene.Rooms.Count > 0 && actorpick != -1)
            {

                //I adapted the function from scenenavi...
                /* Speed modifiers */
                double movemod = 3.0;
                // if (keysDown[(ushort)Keys.Space]) movemod = 8.0;
                // else if (keysDown[(ushort)Keys.ShiftKey]) movemod = 1.0;

                /* Determine mouse position and displacement */
                Vector2d pickObjPosition = new Vector2d(e.X, e.Y);
                Vector2d pickObjDisplacement = ((pickObjPosition - Mouse.Center) * movemod);

                /* No displacement? Exit */
                if (pickObjDisplacement == Vector2d.Zero) return;

                /* Calculate camera rotation */
                double CamXRotd = Camera.Rot.X * (double)(Math.PI / 180);
                double CamYRotd = Camera.Rot.Y * (double)(Math.PI / 180);

                Object target = null;
                if (actorpick == _Actor_)
                    target = CurrentScene.Rooms[RoomList.SelectedIndex].ZActors[actorEditControl1.ActorNumber];
                else if (actorpick == _Transition_)
                    target = CurrentScene.Transitions[actorEditControl2.ActorNumber];
                else if (actorpick == _Spawn_)
                    target = CurrentScene.SpawnPoints[actorEditControl3.ActorNumber];
                else if (actorpick == _Pathway_)
                    target = CurrentScene.Pathways[(int)PathwayNumber.Value].Points[PathwayListBox.SelectedIndex];
                else if (actorpick == _Waterbox_)
                    target = CurrentScene.Waterboxes[(int)WaterboxSelect.Value];
                else if (actorpick == _AddLight_)
                    target = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1];
                else if (actorpick == _CutsceneCamera_)
                    target = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex];
                else if (actorpick == _CutsceneActor_)
                    target = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex];
                else if (actorpick == _Camera_)
                    target = CurrentScene.Cameras[(int)CameraSelect.Value];

                Vector3d objpos = (Vector3d)NullVector;
                if (actorpick <= 3)
                {
                    objpos = new Vector3d(((ZActor)target).XPos,
                    ((ZActor)target).YPos,
                    ((ZActor)target).ZPos);
                }
                else if (actorpick == 4)
                {
                    objpos = (Vector3d)((Vector3)target);
                }
                else if (actorpick == 5)
                {
                    objpos = new Vector3d(((ZWaterbox)target).XPos,
                    ((ZWaterbox)target).YPos,
                    ((ZWaterbox)target).ZPos);
                }
                else if (actorpick == 6)
                {
                    objpos = new Vector3d(((ZAdditionalLight)target).XPos,
                    ((ZAdditionalLight)target).YPos,
                    ((ZAdditionalLight)target).ZPos);
                }
                else if (actorpick == 7)
                {
                    if (extrapick == 0)
                        objpos = (Vector3d)((ZCutscenePosition)target).Position;
                    else
                        objpos = (Vector3d)((ZCutscenePosition)target).Position2;
                }
                else if (actorpick == 8)
                {
                    if (extrapick == 0)
                        objpos = (Vector3d)((ZCutsceneActor)target).Position;
                    else
                        objpos = (Vector3d)((ZCutsceneActor)target).Position2;
                }
                else if (actorpick == 9)
                {
                    objpos = new Vector3d(((ZCamera)target).XPos,
                    ((ZCamera)target).YPos,
                    ((ZCamera)target).ZPos);
                }


                if (Control.ModifierKeys == Keys.Shift) //front/back
                {
                    objpos.X += ((Math.Sin(CamYRotd) * -pickObjDisplacement.Y));
                    objpos.Z -= ((Math.Cos(CamYRotd) * -pickObjDisplacement.Y));
                }
                else
                {
                    objpos.X += ((Math.Cos(CamYRotd) * pickObjDisplacement.X));
                    objpos.Y -= (pickObjDisplacement.Y);
                    objpos.Z += ((Math.Sin(CamYRotd) * pickObjDisplacement.X));

                }

                if (actorpick <= 4 || actorpick == _CutsceneActor_)
                {
                    //down
                    if (KeysDown[(int)ActorControlKeys[0]])
                    {
                        objpos.X += ((Math.Cos(CamYRotd) * pickObjDisplacement.X));
                        objpos.Z += ((Math.Sin(CamYRotd) * pickObjDisplacement.X));
                        objpos.Y = MoveToCollision(new Vector3((float)objpos.X, (float)objpos.Y + 50, (float)objpos.Z), new Vector3(0, -30000, 0)).Y;
                    }
                    //up
                    else if (KeysDown[(int)ActorControlKeys[1]])
                    {
                        objpos.X += ((Math.Cos(CamYRotd) * pickObjDisplacement.X));
                        objpos.Z += ((Math.Sin(CamYRotd) * pickObjDisplacement.X));
                        objpos.Y = MoveToCollision(new Vector3((float)objpos.X, (float)objpos.Y - 50, (float)objpos.Z), new Vector3(0, 30000, 0)).Y;
                    }
                }

                objpos.X = Clamp(Math.Round(objpos.X, 0), -32768, 32767);
                objpos.Y = Clamp(Math.Round(objpos.Y, 0), -32768, 32767);
                objpos.Z = Clamp(Math.Round(objpos.Z, 0), -32768, 32767);
                if (actorpick <= 3)
                {
                    ((ZActor)target).XPos = (float)objpos.X;
                    ((ZActor)target).YPos = (float)objpos.Y;
                    ((ZActor)target).ZPos = (float)objpos.Z;
                }
                else if (actorpick == 4)
                {
                    CurrentScene.Pathways[(int)PathwayNumber.Value].Points[PathwayListBox.SelectedIndex] = (Vector3)objpos;
                }
                else if (actorpick == 5 && !settings.Disablewaterboxmovement)
                {
                    ((ZWaterbox)target).XPos = (float)objpos.X;
                    ((ZWaterbox)target).YPos = (float)objpos.Y;
                    ((ZWaterbox)target).ZPos = (float)objpos.Z;
                }
                else if (actorpick == 6)
                {
                    ((ZAdditionalLight)target).XPos = (short)objpos.X;
                    ((ZAdditionalLight)target).YPos = (short)objpos.Y;
                    ((ZAdditionalLight)target).ZPos = (short)objpos.Z;
                }
                else if (actorpick == 7)
                {
                    if (extrapick == 0)
                        ((ZCutscenePosition)target).Position = (Vector3)objpos;
                    else
                        ((ZCutscenePosition)target).Position2 = (Vector3)objpos;
                }
                else if (actorpick == 8)
                {
                    if (extrapick == 0)
                        ((ZCutsceneActor)target).Position = (Vector3)objpos;
                    else
                        ((ZCutsceneActor)target).Position2 = (Vector3)objpos;
                }
                else if (actorpick == 9)
                {
                    ((ZCamera)target).XPos = (short)objpos.X;
                    ((ZCamera)target).YPos = (short)objpos.Y;
                    ((ZCamera)target).ZPos = (short)objpos.Z;
                }

                //PaintControl();
                if (Mouse.Center == pickObjDisplacement)
                {
                    UpdateForm();
                }
                else
                    Mouse.Center = new Vector2d(e.X, e.Y);

            }
        }

        void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Mouse.LDown = false;
            else if (e.Button == MouseButtons.Right)
                Mouse.RDown = false;
            else if (e.Button == MouseButtons.Middle)
                Mouse.MDown = false;
            if (e.Button == MouseButtons.Middle && CurrentScene != null && CurrentScene.Rooms.Count > 0)
            {
                Mouse.MDown = false;

                actorEditControl1.UpdateActorEdit();
                actorEditControl1.UpdateForm();
                actorEditControl2.UpdateActorEdit();
                actorEditControl3.UpdateActorEdit();
                UpdateForm();
            }
        }

        #endregion

        #region Form & Control Updates, Helpers

        private void ValidateGroupSettings(ref ZScene.ZRoom.ZGroupSettings GSet, int GroupCount)
        {
            if (GSet.TintAlpha.Length != GroupCount)
            {
                GSet.TintAlpha = new uint[GroupCount];
                GSet.TintAlpha.Fill(new uint[] { 0xFFFFFFFF });
            }

            if (GSet.MultiTexAlpha.Length != GroupCount)
            {
                GSet.MultiTexAlpha = new uint[GroupCount];
                GSet.MultiTexAlpha.Fill(new uint[] { 0xFFFFFFFF });
            }

            if (GSet.TileS.Length != GroupCount)
            {
                GSet.TileS = new int[GroupCount];
                GSet.TileS.Fill(new int[] { GBI.G_TX_WRAP });
            }

            if (GSet.TileT.Length != GroupCount)
            {
                GSet.TileT = new int[GroupCount];
                GSet.TileT.Fill(new int[] { GBI.G_TX_WRAP });
            }

            if (GSet.PolyType.Length != GroupCount)
            {
                GSet.PolyType = new int[GroupCount];
                GSet.PolyType.Fill(new int[] { 0x0000000000000000 });
            }

            if (GSet.BackfaceCulling.Length != GroupCount)
            {
                GSet.BackfaceCulling = new bool[GroupCount];
                GSet.BackfaceCulling.Fill(new bool[] { true });
            }

            if (GSet.Animated.Length != GroupCount)
            {
                GSet.Animated = new bool[GroupCount];
                GSet.Animated.Fill(new bool[] { false });
            }

            if (GSet.Metallic.Length != GroupCount)
            {
                GSet.Metallic = new bool[GroupCount];
                GSet.Metallic.Fill(new bool[] { false });
            }

            if (GSet.EnvColor.Length != GroupCount)
            {
                GSet.EnvColor = new bool[GroupCount];
                GSet.EnvColor.Fill(new bool[] { false });
            }

            if (GSet.Decal.Length != GroupCount)
            {
                GSet.Decal = new bool[GroupCount];
                GSet.Decal.Fill(new bool[] { false });
            }

            if (GSet.IgnoreFog.Length != GroupCount)
            {
                GSet.IgnoreFog = new bool[GroupCount];
                GSet.IgnoreFog.Fill(new bool[] { false });
            }

            if (GSet.SmoothRGBAEdges.Length != GroupCount)
            {
                GSet.SmoothRGBAEdges = new bool[GroupCount];
                GSet.SmoothRGBAEdges.Fill(new bool[] { false });
            }

            if (GSet.Pixelated.Length != GroupCount)
            {
                GSet.Pixelated = new bool[GroupCount];
                GSet.Pixelated.Fill(new bool[] { false });
            }

            if (GSet.Billboard.Length != GroupCount)
            {
                GSet.Billboard = new bool[GroupCount];
                GSet.Billboard.Fill(new bool[] { false });
            }

            if (GSet.TwoAxisBillboard.Length != GroupCount)
            {
                GSet.TwoAxisBillboard = new bool[GroupCount];
                GSet.TwoAxisBillboard.Fill(new bool[] { false });
            }

            if (GSet.ReverseLight.Length != GroupCount)
            {
                GSet.ReverseLight = new bool[GroupCount];
                GSet.ReverseLight.Fill(new bool[] { false });
            }

            if (GSet.MultiTexMaterial.Length != GroupCount)
            {
                GSet.MultiTexMaterial = new int[GroupCount];
                GSet.MultiTexMaterial.Fill(new int[] { -1 });
            }

            if (GSet.ShiftS.Length != GroupCount)
            {
                GSet.ShiftS = new int[GroupCount];
                GSet.ShiftS.Fill(new int[] { GBI.G_TX_NOLOD });
            }

            if (GSet.ShiftT.Length != GroupCount)
            {
                GSet.ShiftT = new int[GroupCount];
                GSet.ShiftT.Fill(new int[] { GBI.G_TX_NOLOD });
            }

            if (GSet.BaseShiftS.Length != GroupCount)
            {
                GSet.BaseShiftS = new int[GroupCount];
                GSet.BaseShiftS.Fill(new int[] { GBI.G_TX_NOLOD });
            }

            if (GSet.BaseShiftT.Length != GroupCount)
            {
                GSet.BaseShiftT = new int[GroupCount];
                GSet.BaseShiftT.Fill(new int[] { GBI.G_TX_NOLOD });
            }

            if (GSet.AnimationBank.Length != GroupCount)
            {
                GSet.AnimationBank = new int[GroupCount];
                GSet.AnimationBank.Fill(new int[] { 8 });
            }

            if (GSet.LodGroup.Length != GroupCount)
            {
                GSet.LodGroup = new int[GroupCount];
                GSet.LodGroup.Fill(new int[] { 0 });
            }

            if (GSet.LodDistance.Length != GroupCount)
            {
                GSet.LodDistance = new int[GroupCount];
                GSet.LodDistance.Fill(new int[] { 0 });
            }

            if (GSet.LOD.Length != GroupCount)
            {
                GSet.LOD = new bool[GroupCount];
                GSet.LOD.Fill(new bool[] { false });
            }

            if (GSet.RenderLast.Length != GroupCount)
            {
                GSet.RenderLast = new bool[GroupCount];
                GSet.RenderLast.Fill(new bool[] { false });
            }

            if (GSet.AlphaMask.Length != GroupCount)
            {
                GSet.AlphaMask = new bool[GroupCount];
                GSet.AlphaMask.Fill(new bool[] { false });
            }

            if (GSet.VertexNormals.Length != GroupCount)
            {
                GSet.VertexNormals = new bool[GroupCount];
                GSet.VertexNormals.Fill(new bool[] { false });
            }

            if (GSet.Custom.Length != GroupCount)
            {
                GSet.Custom = new bool[GroupCount];
                GSet.Custom.Fill(new bool[] { false });
            }

            if (GSet.CustomDL.GetLength(0) != GroupCount)
            {
                GSet.CustomDL = new ulong[GroupCount, 4];
                for (int m = 0; m < GroupCount; m++)
                {
                    for (int n = 0; n < 4; n++)
                    {
                        GSet.CustomDL[m, n] = 0;
                    }
                }
            }
        }

        private bool ColorPicker(ref PictureBox PB)
        {
            ColorDialog CD = new ColorDialog();
            CD.FullOpen = true;
            CD.Color = PB.BackColor;
            if (CD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PB.BackColor = CD.Color;
                return true;
            }
            return false;
        }

        private Vector3d GetCenterPoint(List<ObjFile.Vertex> Vertices)
        {
            if (CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices.Count < 1) return new Vector3d(0, 0, 0);
            ObjFile.Vertex v = CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices[0];
            Vector3d MinCoordinate = new Vector3d(v.X, v.Y, v.Z);
            Vector3d MaxCoordinate = new Vector3d(v.X, v.Y, v.Z);

            foreach (ObjFile.Vertex Vtx in Vertices)
            {
                /* Minimum... */
                MinCoordinate.X = Math.Min(MinCoordinate.X, Vtx.X * CurrentScene.Scale);
                MinCoordinate.Y = Math.Min(MinCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                MinCoordinate.Z = Math.Min(MinCoordinate.Z, Vtx.Z * CurrentScene.Scale);

                /* Maximum... */
                MaxCoordinate.X = Math.Max(MaxCoordinate.X, Vtx.X * CurrentScene.Scale);
                MaxCoordinate.Y = Math.Max(MaxCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                MaxCoordinate.Z = Math.Max(MaxCoordinate.Z, Vtx.Z * CurrentScene.Scale);
            }

            return Vector3d.Lerp(MinCoordinate, MaxCoordinate, 0.5f);
        }

        private void UpdateForm()
        {

            if (CurrentScene != null)
            {
                if (NowLoading == false)
                {

                    this.SuspendLayout();

                    if (!Is1April)
                        this.Text = Program.ApplicationTitle + " - " + CurrentScene.Name;
                    else
                        this.Text = "OoT, but its a tool to make maps" + " - " + CurrentScene.Name;



                    if (settings.AutoSave)
                    {
                        if ((DateTime.Now - LastAutoSave).TotalSeconds >= 60)
                        {
                            LastAutoSave = DateTime.Now;
                            SaveScene(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"autosave.xml"), true);
                        }
                    }



                    #region Comboboxforeachs

                    foreach (SongItem item in SongComboBox.Items)
                    {
                        if (item != null && Convert.ToInt32(item.Value) == CurrentScene.Music)
                        {
                            SongComboBox.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (SongItem item in NightSFXComboBox.Items)
                    {
                        if (item != null && Convert.ToInt32(item.Value) == CurrentScene.NightSFX)
                        {
                            NightSFXComboBox.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (SongItem item in WorldMapComboBox.Items)
                    {
                        if (item != null && Convert.ToInt32(item.Value) == CurrentScene.WorldMap)
                        {
                            WorldMapComboBox.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (SongItem item in CameraMovementComboBox.Items)
                    {
                        if (item != null && Convert.ToInt32(item.Value) == CurrentScene.CameraMovement)
                        {
                            CameraMovementComboBox.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (SongItem item in SpecialObjectComboBox.Items)
                    {
                        if (item != null && Convert.ToInt32(item.Value) == CurrentScene.SpecialObject)
                        {
                            SpecialObjectComboBox.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (SongItem item in ElfMessageComboBox.Items)
                    {
                        if (item != null && Convert.ToInt32(item.Value) == CurrentScene.ElfMessage)
                        {
                            ElfMessageComboBox.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (AnimationItem item in SceneSettingsComboBox.Items)
                    {
                        if (item != null && Convert.ToInt32(item.Value) == CurrentScene.SceneSettings)
                        {
                            SceneSettingsComboBox.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (SongItem item in SkyboxComboBox.Items)
                    {
                        if (item != null && Convert.ToInt32(item.Value) == CurrentScene.SkyboxType)
                        {
                            SkyboxComboBox.SelectedItem = item;
                            break;
                        }
                    }

                    #endregion

                    SoundSpec.SelectedIndex = CurrentScene.Reverb;
                    CloudyCheckBox.Checked = CurrentScene.Cloudy;

                    ContinualInject.Checked = CurrentScene.ContinualInject;
                    UnusedCommandCheckBox.Checked = CurrentScene.OutdoorLight;


                    saveBinaryToolStripMenuItem.Enabled = true;
                    saveSceneToolStripMenuItem.Enabled = true;
                    SaveScenetoolStripMenuItem3.Enabled = (LastScene != "");
                    injectToROMToolStripMenuItem.Enabled = true;
#if DEBUG
                    openZmapToolstrip.Visible = true;
                    openSceneToolStripMenuItem.Visible = true;
#endif

                    SetRestrictionFlags.Enabled = true;
                    SetTitlecard.Enabled = true;


                    GroupAnimatedBank.Enabled = GroupAnimated.Checked;
                    GroupLODDIstance.Enabled = GroupLod.Checked;
                    GroupLODGroup.Enabled = GroupLod.Checked;

                    UpdateLabel.Visible = updateavailable;

                    showCollisionModelToolStripMenuItem.Checked = settings.ShowCollisionModel;
                    showRoomModelsToolStripMenuItem.Checked = settings.ShowRoomModels;
                    applyEnvironmentLightingToolStripMenuItem.Checked = settings.ApplyEnvLighting;
                    consecutiveRoomInjectionToolStripMenuItem.Checked = settings.ConsecutiveRoomInject;
                    forceRGBATexturesToolStripMenuItem.Checked = settings.ForceRGBATextures;
                    DegreesMenuItem.Checked = settings.Degrees;
                    AutoaddGroupsMenuItem.Checked = settings.AutoaddObjects;
                    DisplayAxisMenuItem.Checked = settings.DisplayAxis;
                    SimulateN64CheckBox.Checked = SimulateN64Gfx;
                    disableWaterboxMouseMovementToolStripMenuItem.Checked = settings.Disablewaterboxmovement;
                    dListCullingMenuItem.Checked = settings.DListCulling;
                    updateCRCMenuItem.Checked = settings.UpdateCRC;
                    RenderActorstoolStrip.Checked = settings.RenderActors;
                    majorasMaskModeexperimentalToolStripMenuItem.Checked = settings.MajorasMask;
                    autosaveSceneXmlToolStripMenuItem.Checked = settings.AutoSave;
                    dIsplayUndocumentedCutsceneVarsToolStripMenuItem.Checked = settings.UndocumentedCutsceneVars;
                    noDummyPointsInCutsceneCamerasToolStripMenuItem.Checked = settings.NoDummyPoints;
                    printOffsetsOnInjectToolStripMenuItem.Checked = settings.printoffsets;
                    triplicateCollisionBoundsToolStripMenuItem.Checked = settings.TriplicateCollisionBounds;
                    showRotationValuesAsHexadecimalToolStripMenuItem.Checked = settings.HexRotations;
                    ZmapOffsetNames.Checked = settings.Zmapoffsetnames;
                    AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem.Checked = settings.GenerateCustomDMATable;
                    writeCollisionToolStripMenuItem.Checked = settings.WriteCollisionZobj;
                    dontConvertMultitextureToRGBAToolStripMenuItem.Checked = settings.DontConvertMultitexture;
                    CheckEmptyOffsetItem.Checked = settings.CheckEmptyOffset;
                    RenderChildLinkMenuItem.Checked = settings.RenderChildLink;
                    IgnoreMajorasMaskDaySystem.Checked = settings.IgnoreMMDaySystem;
                    addEmptySpaceInSceneHeaderToolStripMenuItem.Checked = (settings.EmptySpace > 0);
                    RenderSelectedCutsceneCommandsMenuItem.Checked = settings.DrawSelectedCutsceneCommands;
                    DisableRGBA32ToolStrip.Checked = settings.DisableRGBA32;
                    AutoFixErrorsStripMenuItem3.Checked = settings.AutoFixErrors;
                    AdvancedTextureAnimationsMenuItem.Checked = settings.command1AOoT;
                    AutoReload.Checked = settings.AutoReload;

                    RenderWaterboxesMenuItem.Checked = settings.OnlyRenderWaterboxesGeneral;
                    ColorBlindMenuItem.Checked = settings.colorblindaxis;
                    DisableTextureWarningsMenuItem.Checked = settings.DisableTextureWarnings;
                    EnableNexExitFormatMenuItem.Checked = settings.EnableNewExitFormat;

                    if (EasterEggPhase == 2 || Is1April)
                    {
                        this.Icon = Properties.Resources.Bombiwa;
                        Application.DoEvents();
                    }
                    else if (settings.MajorasMask && this.Icon != Properties.Resources.MM)
                    {
                        this.Icon = Properties.Resources.MM;
                        Application.DoEvents();
                    }
                    else if (this.Icon != Properties.Resources.OOT)
                    {
                        this.Icon = Properties.Resources.OOT;
                        Application.DoEvents();
                    }


                    if (Is1April)
                    {
                        if (CurrentScene != null) CDILink.Visible = false;

                        Random rand = new Random();
                        foreach (TabPage page in tabControl1.TabPages)
                        {
                            page.BackColor = Color.FromArgb(rand.Next(210, 255), rand.Next(210, 255), rand.Next(210, 255));
                            page.ForeColor = Color.FromArgb(255 - page.BackColor.R, 255 - page.BackColor.G, 255 - page.BackColor.B);

                            foreach (Control ctrl in page.Controls)
                            {
                                ctrl.BackColor = Color.FromArgb(rand.Next(210, 255), rand.Next(210, 255), rand.Next(210, 255));
                                ctrl.ForeColor = Color.FromArgb(255 - page.BackColor.R, 255 - page.BackColor.G, 255 - page.BackColor.B);

                                foreach (Control ctrl2 in page.Controls)
                                {
                                    ctrl2.BackColor = Color.FromArgb(rand.Next(210, 255), rand.Next(210, 255), rand.Next(210, 255));
                                    ctrl2.ForeColor = Color.FromArgb(255 - page.BackColor.R, 255 - page.BackColor.G, 255 - page.BackColor.B);
                                }
                            }
                        }
                    }

                    optionsToolStripMenuItem.Enabled = true;
                    extraToolStripMenuItem.Enabled = true;
                    tabControl1.Enabled = true;

                    NameTextbox.Text = CurrentScene.Name;
                    ScaleNumericbox.Value = (decimal)CurrentScene.Scale;
                    CollisionTextbox.Text = System.IO.Path.GetFileName(CurrentScene.CollisionFilename);
                    InjectoffsetTextbox.Text = CurrentScene.InjectOffset.ToString("X8");
                    ScenenumberTextbox.Value = CurrentScene.SceneNumber;
                    // checkBox1.Checked = CurrentScene.IsOutdoors;

                    ReverseLightCheckBox.Text = (CurrentScene.OutdoorLight) ? "Use vertex colors" : "Use normal light";

                    SceneSettingsComboBox.Visible = !settings.MajorasMask;
                    SceneFunctionLabel.Visible = !settings.MajorasMask;
                    CloudyCheckBox.Visible = !settings.MajorasMask;

                    SceneSettingsComboBox.Enabled = !settings.command1AOoT;
                    SceneFunctionLabel.Enabled = !settings.command1AOoT;

                    actorEditControl2.Enabled = true;
                    actorEditControl3.Enabled = true;

                    groupBox6.Enabled = true;
                    // UpdateExitEdit();

                    CameraXRot.Hexadecimal = settings.HexRotations;
                    CameraYRot.Hexadecimal = settings.HexRotations;
                    CameraZRot.Hexadecimal = settings.HexRotations;
                    CutsceneActorXRot.Hexadecimal = settings.HexRotations;
                    CutsceneActorYRot.Hexadecimal = settings.HexRotations;
                    CutsceneActorZRot.Hexadecimal = settings.HexRotations;

                    PrerenderedGroupBox.Visible = !settings.MajorasMask;

                    if (CurrentScene.Rooms.Count > 0) RoomSelector.Maximum = CurrentScene.Rooms.Count - 1;
                    else RoomSelector.Maximum = 0;
                    RoomSelector.Enabled = CurrentScene.Rooms.Count != 0;

                    if (RoomSelector.Value != RoomList.SelectedIndex && CurrentScene.Rooms.Count != 0) RoomSelector.Value = RoomList.SelectedIndex;

                    if (settings.MajorasMask)
                    {
                        TextureAnimsGroupBox.Visible = true;
                        WaterboxGroupBox.Location = new Point(6, 322);
                        CamerasGroupBox.Location = new Point(6, 497);
                    }
                    else
                    {
                        TextureAnimsGroupBox.Visible = false;
                        WaterboxGroupBox.Location = new Point(6, 222);
                        CamerasGroupBox.Location = new Point(6, 397);
                    }

                    RefreshExitLabels();

                    if (CurrentScene.Rooms.Count > 0)
                    {
                        #region foreachsrooms
                        foreach (SongItem item in RestrictionComboBox.Items)
                        {
                            if (item != null && Convert.ToInt32(item.Value) == CurrentScene.Rooms[RoomList.SelectedIndex].Restriction)
                            {
                                RestrictionComboBox.SelectedItem = item;
                                break;
                            }
                        }
                        foreach (SongItem item in IdleAnimComboBox.Items)
                        {
                            if (item != null && Convert.ToInt32(item.Value) == CurrentScene.Rooms[RoomList.SelectedIndex].IdleAnim)
                            {
                                IdleAnimComboBox.SelectedItem = item;
                                break;
                            }
                        }

                        #endregion

                        WindSouth.Text = CurrentScene.Rooms[RoomList.SelectedIndex].WindSouth.ToString("X2");
                        WindStrength.Text = CurrentScene.Rooms[RoomList.SelectedIndex].WindStrength.ToString("X2");
                        WindVertical.Text = CurrentScene.Rooms[RoomList.SelectedIndex].WindVertical.ToString("X2");
                        WindWest.Text = CurrentScene.Rooms[RoomList.SelectedIndex].WindWest.ToString("X2");
                        // TimeStart.Text = CurrentScene.Rooms[RoomList.SelectedIndex].StartTime.ToString("X4");
                        TimeSpeed.Text = CurrentScene.Rooms[RoomList.SelectedIndex].TimeSpeed.ToString("X2");

                        SoundEcho.Text = CurrentScene.Rooms[RoomList.SelectedIndex].Echo.ToString("X2");

                        DisableStartTime.Checked = CurrentScene.Rooms[RoomList.SelectedIndex].StartTime == 0xFFFF;



                        TimeHour.Value = (DisableStartTime.Checked) ? 0 : ((CurrentScene.Rooms[RoomList.SelectedIndex].StartTime & 0xFF00) >> 8);
                        TimeMinute.Value = (DisableStartTime.Checked) ? 0 : ((CurrentScene.Rooms[RoomList.SelectedIndex].StartTime & 0x00FF));

                        SkyboxCheckBox.Checked = CurrentScene.Rooms[RoomList.SelectedIndex].DisableSkybox;
                        SunmoonCheckBox.Checked = CurrentScene.Rooms[RoomList.SelectedIndex].DisableSunMoon;
                        InvisibleActorsCheckBox.Checked = CurrentScene.Rooms[RoomList.SelectedIndex].ShowInvisibleActors;
                        WarpsongsCheckBox.Checked = CurrentScene.Rooms[RoomList.SelectedIndex].DisableWarpSongs;
                        Roomaffectedpointlightscheckbox.Checked = CurrentScene.Rooms[RoomList.SelectedIndex].AffectedByPointLight;
                        UpdateAdditionalLightEdit();
                        RenderFunctionInherit.Checked = CurrentScene.inherittextureanims;
                        RenderFunctionInherit.Visible = (CurrentScene != NormalHeader);

                        GroupList.Enabled = true;

                        Roomaffectedpointlightscheckbox.Enabled = !CurrentScene.PregeneratedMesh;



                        foreach (Control Ctrl in tabRooms.Controls)
                            Ctrl.Enabled = true;


                        if (SceneHeaderSelector.Value == 0)
                        {
                            AlternateHeadersGroupBox.Enabled = true;


                            foreach (Control Ctrl in AlternateHeadersGroupBox.Controls)
                                Ctrl.Enabled = true;
                        }
                        else
                        {
                            AlternateHeadersGroupBox.Enabled = false;
                        }



                        //object size
                        if (CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects.Count > 0)
                        {
                            ObjectSpace.Visible = true;
                            int size = 0 + 0x567B0 + 0x37800;
                            if (CurrentScene.SpecialObject == 0x0002) size += 0xD330;
                            else size += 0x17AF0;
                            int maxsize = 0xFA000;

                            if (rom64.isSet())
                                maxsize = 2000000;

                            foreach (ZScene.ZUShort obj in CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects)
                            {
                                if (ObjectCache.ContainsKey(obj.Value))
                                    size += ObjectCache[obj.Value].size;
                                else
                                    size += Convert.ToInt32(XMLreader.getObjectSize(obj.ValueHex)[0], 16);
                            }
                            ObjectSpace.Text = "Object space: " + size.ToString("X") + " / " + maxsize.ToString("X");
                            if (size > maxsize) ObjectSpace.ForeColor = Color.Red;
                            else ObjectSpace.ForeColor = Color.Black;

                        }
                        else ObjectSpace.Visible = false;
                    }
                    else
                    {
                        foreach (Control Ctrl in AdditionalTexturesGroupBox.Controls)
                            Ctrl.Enabled = false;


                        foreach (Control Ctrl in AlternateHeadersGroupBox.Controls)
                            Ctrl.Enabled = false;
                    }



                    if (RoomList.SelectedItem != null)
                    {
                        groupBox2.Enabled = true;
                        actorEditControl1.Enabled = true;
                        refreshobjdescription();

                        if (settings.ConsecutiveRoomInject == true && (RoomList.SelectedIndex > 0 || CurrentScene.ContinualInject))
                        {
                            RoomInjectionOffset.Enabled = false;
                            //RoomInjectionOffset.Text = string.Empty;
                        }
                        else
                        {
                            RoomInjectionOffset.Enabled = true;
                            RoomInjectionOffset.Text = CurrentScene.Rooms[RoomList.SelectedIndex].InjectOffset.ToString("X8");
                        }

                        UpdateObjectEdit();

                        //if (GroupList.SelectedIndex != -1)
                        //((CurrencyManager)GroupList.BindingContext[CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Groups]).Refresh();
                    }
                    else
                    {
                        groupBox2.Enabled = false;
                        actorEditControl1.Enabled = false;
                    }

                    if (RoomList.Items.Count == 0 || RoomList.SelectedIndex == -1)
                    {
                        DeleteRoom.Enabled = false;
                        ReloadRoomButton.Enabled = false;
                    }
                    else
                    {
                        DeleteRoom.Enabled = true;
                        ReloadRoomButton.Enabled = true;
                    }

                    if (CurrentScene.NewRoomMode)
                        DeleteRoom.Text = "Delete All Rooms";
                    else
                        DeleteRoom.Text = "Delete Room";

                    if (CurrentScene.ColModel == null)
                    {
                        AddRoom.Enabled = false;
                        AddMultipleRooms.Enabled = false;
                    }
                    else
                    {
                        if (RoomList.Items.Count == 0)
                        {
                            AddRoom.Enabled = true;
                            AddMultipleRooms.Enabled = true;
                        }
                        else
                        {
                            AddRoom.Enabled = !CurrentScene.NewRoomMode;
                            AddMultipleRooms.Enabled = false;
                        }
                    }

                    if (listBox3.Items.Count == 0 || listBox3.SelectedIndex == -1)
                        button7.Enabled = false;
                    else
                        button7.Enabled = true;

                    button8.Enabled = (listBox3.Items.Count < 0x0F);

                    if (ExitList.Items.Count == 0 || ExitList.SelectedIndex == -1)
                        DeleteexitButton.Enabled = false;
                    else
                        DeleteexitButton.Enabled = true;


                    this.ResumeLayout();
                }

                if (!settings.command1AOoT && AddRenderFunction.Enabled == true) //disable room edit in alternate headers...
                {
                    foreach (Control Ctrl in tabAnimations.Controls)
                        Ctrl.Enabled = false;
                    RenderFunctionWarningLabel.Visible = true;
                    RenderFunctionWarningLabel.Enabled = true;
                }
                if (settings.command1AOoT)
                {
                    RenderFunctionWarningLabel.Visible = false;
                    RenderFunctionWarningLabel.Enabled = false;

                    if (CurrentScene != NormalHeader)
                    {
                        if (CurrentScene.inherittextureanims)
                        {
                            if (AddRenderFunction.Enabled == true)
                            {
                                foreach (Control Ctrl in tabAnimations.Controls)
                                    Ctrl.Enabled = false;
                            }
                            RenderFunctionInherit.Enabled = true;

                        }
                        else
                        {
                            if (AddRenderFunction.Enabled == false)
                            {
                                foreach (Control Ctrl in tabAnimations.Controls)
                                    Ctrl.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        if (AddRenderFunction.Enabled == false)
                        {
                            foreach (Control Ctrl in tabAnimations.Controls)
                                Ctrl.Enabled = true;
                        }
                    }


                }

                UpdateWaterboxEdit();
                UpdateEnvironmentEdit();
                UpdateGroupSelect();
                UpdatePolyTypeEdit();
                UpdatePathwayEdit();
                UpdateCutsceneEdit();
                UpdateCameraEdit();
                if (settings.command1AOoT) UpdateRenderFunctionEdit();
                if (settings.MajorasMask) UpdateTextureAnimEdit();
                UpdateAlternateSceneHeaders();
                UpdateAdditionalTextures();
                UpdatePrerenders();
                UpdateExits();

                if (CurrentScene.cloneid > 0) //disable room edit in alternate headers...
                {
                    foreach (Control Ctrl in tabRooms.Controls)
                        Ctrl.Enabled = false;
                    RoomList.Enabled = true;
                }

                if (CurrentScene.PregeneratedMesh)
                {
                    foreach (Control Ctrl in tabRooms.Controls)
                        if (Ctrl != AdditionalTextureList && Ctrl != AdditionalTexturesGroupBox && Ctrl != AddAdditionalTexture && Ctrl != DeleteAdditionalTexture) Ctrl.Enabled = false;
                    RoomList.Enabled = true;
                }

                if (CurrentScene.PregeneratedMesh)
                {
                    foreach (Control Ctrl in tabCollision.Controls)
                        if (Ctrl != ExitList && Ctrl != ExitNumber && Ctrl != ExitListLabel && Ctrl != AddexitButton && Ctrl != DeleteexitButton) Ctrl.Enabled = false;

                }



            }
        }

        public void RefreshExitLabels()
        {
            if (GlobalROM != "" && !settings.EnableNewExitFormat)
            {
                if (ExitList.Items.Count == 0)
                {
                    ExitListLabel.Text = "";
                }
                else
                {
                    if (ExitCache.ContainsKey(CurrentScene.ExitList[ExitList.SelectedIndex].Value) && CurrentScene.ExitList.Count != 0)
                        ExitListLabel.Text = ExitCache[CurrentScene.ExitList[ExitList.SelectedIndex].Value];
                    else
                        ExitListLabel.Text = "Invalid exit";
                }


            }
            if (!settings.EnableNewExitFormat)
            {
                if (ExitNumber.Value == 0)
                    PolytypeExitLabel.Text = "";
                else if (ExitNumber.Value <= ExitList.Items.Count)
                {
                    PolytypeExitLabel.Text = "(Exit " + CurrentScene.ExitList[ExitList.SelectedIndex].Value.ToString("X4") + ")";
                }
            }
        }

        public void StoreUndo(int datatype)
        {
            if (actorpick == -1) return;


            if (undo.Count == settings.MaxUndoRedo)
            {
                undo.RemoveAt(0);
            }

            if (datatype > 3) return; //TODO remove this

            redo.Clear();

            Object target = null;
            if (datatype == _Actor_)
                target = CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.ConvertAll(actor => (actor.Clone()));
            else if (datatype == _Transition_)
                target = CurrentScene.Transitions.ConvertAll(actor => (actor.Clone()));
            else if (datatype == _Spawn_)
                target = CurrentScene.SpawnPoints.ConvertAll(actor => (actor.Clone()));
            else if (datatype == _Pathway_)
                target = CurrentScene.Pathways;
            else if (datatype == _Waterbox_)
                target = CurrentScene.Waterboxes;
            else if (datatype == _AddLight_)
                target = CurrentScene.Rooms;
            else if (datatype == _CutsceneCamera_)
                target = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points;
            else if (datatype == _CutsceneActor_)
                target = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors;
            else if (datatype == _Camera_)
                target = CurrentScene.Cameras;

            undo.Add(new UndoRedo(datatype, target, RoomList.SelectedIndex));

            // Console.WriteLine("storing undo datatype: " + datatype + " / amount of undos: " + undo.Count);

        }
        private void Undo(bool Redo = false)
        {
            if ((!Redo && undo.Count > 0) || (Redo && redo.Count > 0))
            {

                UndoRedo undoredo = (!Redo) ? undo[undo.Count - 1] : redo[redo.Count - 1];
                Object target = null;
                List<UndoRedo> targetback = (!Redo) ? redo : undo;

                //  Console.WriteLine("performing " + ((!Redo) ? "undo" : "redo") + " datatype: " + undoredo.datatype);

                if (undoredo.datatype == _Actor_)
                {
                    SelectRoom(undoredo.room);

                    target = CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.ConvertAll(actor => (actor.Clone()));
                    targetback.Add(new UndoRedo(undoredo.datatype, target, RoomList.SelectedIndex));
                    CurrentScene.Rooms[RoomList.SelectedIndex].ZActors = ((List<ZActor>)undoredo.data).ConvertAll(actor => (actor.Clone()));

                    actorEditControl1.SetActors(ref CurrentScene.Rooms[RoomList.SelectedIndex].ZActors);

                    actorEditControl1.UpdateActorEdit();
                }
                else if (undoredo.datatype == _Transition_)
                {
                    target = CurrentScene.Transitions.ConvertAll(actor => (actor.Clone()));
                    targetback.Add(new UndoRedo(undoredo.datatype, target, RoomList.SelectedIndex));
                    CurrentScene.Transitions = ((List<ZActor>)undoredo.data).ConvertAll(actor => (actor.Clone()));

                    actorEditControl2.SetActors(ref CurrentScene.Transitions);

                    actorEditControl2.UpdateActorEdit();
                }
                else if (undoredo.datatype == _Spawn_)
                {
                    target = CurrentScene.SpawnPoints.ConvertAll(actor => (actor.Clone()));
                    targetback.Add(new UndoRedo(undoredo.datatype, target, RoomList.SelectedIndex));
                    CurrentScene.SpawnPoints = ((List<ZActor>)undoredo.data).ConvertAll(actor => (actor.Clone()));

                    actorEditControl3.SetActors(ref CurrentScene.SpawnPoints);

                    actorEditControl3.UpdateActorEdit();
                }


                if (!Redo && undo.Count > 0) undo.RemoveAt(undo.Count - 1);
                else if (redo.Count > 0) redo.RemoveAt(redo.Count - 1);

                // actorEditControl1.UpdateForm();

                UpdateForm();
            }
        }

        private void UpdatePathwayEdit()
        {
            if (CurrentScene.Pathways.Count != 0)
            {
                Helpers.SelectClamp(PathwayNumber, CurrentScene.Pathways);
                PathwayNumber.Enabled = true;
                AddPointButton.Enabled = true;
                int prevsel = PathwayListBox.SelectedIndex;
                PathwayListBox.Items.Clear();
                foreach (Vector3 point in CurrentScene.Pathways[(int)PathwayNumber.Value].Points)
                {
                    PathwayListBox.Items.Add("(X " + Math.Floor(point.X) + ",Y " + Math.Floor(point.Y) + ",Z " + Math.Floor(point.Z) + ")");
                }
                if (prevsel >= PathwayListBox.Items.Count && PathwayListBox.Items.Count > 0) PathwayListBox.SelectedIndex = prevsel - 1;
                else if (prevsel >= PathwayListBox.Items.Count) PathwayListBox.SelectedIndex = -1;
                else PathwayListBox.SelectedIndex = prevsel;

                if (PathwayListBox.Items.Count > 0)
                {
                    if (PathwayListBox.SelectedIndex < 0) PathwayListBox.SelectedIndex = 0;
                    PathwayXPos.Value = (decimal)CurrentScene.Pathways[(int)PathwayNumber.Value].Points[PathwayListBox.SelectedIndex].X;
                    PathwayYPos.Value = (decimal)CurrentScene.Pathways[(int)PathwayNumber.Value].Points[PathwayListBox.SelectedIndex].Y;
                    PathwayZPos.Value = (decimal)CurrentScene.Pathways[(int)PathwayNumber.Value].Points[PathwayListBox.SelectedIndex].Z;
                    PathwayXPos.Enabled = true;
                    PathwayYPos.Enabled = true;
                    PathwayZPos.Enabled = true;
                    PathwayLabel1.Enabled = true;
                    PathwayLabel2.Enabled = true;
                    PathwayLabel3.Enabled = true;
                    PathwayXPosStrip.Enabled = true;
                    PathwayYPosStrip.Enabled = true;
                    PathwayZPosStrip.Enabled = true;
                    DeletePointButton.Enabled = true;
                    PathwayDown.Enabled = (PathwayListBox.SelectedIndex < PathwayListBox.Items.Count - 1 && PathwayListBox.SelectedIndex != -1);
                    PathwayUp.Enabled = (PathwayListBox.SelectedIndex > 0);
                    PathwayAddButton.Enabled = (CurrentScene.Pathways.Count < 0x0F);

                }
                else
                {
                    PathwayXPos.Value = 0;
                    PathwayYPos.Value = 0;
                    PathwayZPos.Value = 0;
                    PathwayXPos.Enabled = false;
                    PathwayYPos.Enabled = false;
                    PathwayZPos.Enabled = false;
                    PathwayLabel1.Enabled = false;
                    PathwayLabel2.Enabled = false;
                    PathwayLabel3.Enabled = false;
                    PathwayXPosStrip.Enabled = false;
                    PathwayYPosStrip.Enabled = false;
                    PathwayZPosStrip.Enabled = false;
                    DeletePointButton.Enabled = false;
                    PathwayUp.Enabled = false;
                    PathwayDown.Enabled = false;
                }

                PathwayDeleteButton.Enabled = true;
            }
            else
            {
                PathwayNumber.Minimum = 0;
                PathwayNumber.Maximum = 0;
                PathwayNumber.Value = 0;
                PathwayNumber.Enabled = false;
                PathwayXPos.Enabled = false;
                PathwayYPos.Enabled = false;
                PathwayZPos.Enabled = false;
                PathwayLabel1.Enabled = false;
                PathwayLabel2.Enabled = false;
                PathwayLabel3.Enabled = false;
                DeletePointButton.Enabled = false;
                AddPointButton.Enabled = false;

                PathwayXPos.Value = 0;
                PathwayYPos.Value = 0;
                PathwayZPos.Value = 0;

                PathwayDeleteButton.Enabled = false;
            }
        }

        private void UpdateCutsceneEdit()
        {
            if (CurrentScene.Cutscene.Count != 0)
            {

                MarkerDown.Enabled = (MarkerSelect.SelectedIndex < CurrentScene.Cutscene.Count - 1 && MarkerSelect.SelectedIndex != -1);
                MarkerUp.Enabled = (MarkerSelect.SelectedIndex > 0);

                //Console.WriteLine("TAb: " + (MarkerType.SelectedItem as MarkerItem).Tab);


                int prevsel = MarkerSelect.SelectedIndex;
                int prevsel2 = CutsceneAbsolutePositionListBox.SelectedIndex;
                int prevsel3 = CutsceneTextboxList.SelectedIndex;
                int prevsel4 = CutsceneActorListBox.SelectedIndex;
                MarkerSelect.Items.Clear();
                foreach (ZCutscene cutscene in CurrentScene.Cutscene)
                {
                    MarkerSelect.Items.Add((MarkerType.Items[FindComboItemValue(MarkerType.Items, cutscene.Marker)] as MarkerItem).Text + " - Total Frames: " + cutscene.GetTotalFrames());
                }

                // if (MarkerSelect.SelectedIndex < 0 && prevsel == -1) MarkerSelect.SelectedIndex = 0;

                if (prevsel >= MarkerSelect.Items.Count && prevsel < MarkerSelect.Items.Count) MarkerSelect.SelectedIndex = prevsel - 1;
                else if (prevsel >= MarkerSelect.Items.Count) MarkerSelect.SelectedIndex = MarkerSelect.Items.Count - 1;
                else MarkerSelect.SelectedIndex = prevsel;

                if (MarkerSelect.SelectedIndex < 0) MarkerSelect.SelectedIndex = 0;

                MarkerType.SelectedIndex = FindComboItemValue(MarkerType.Items, CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Marker);

                CutsceneTabs.SelectedIndex = (MarkerType.SelectedItem as MarkerItem).Tab;

                MarkerStartFrame.Enabled = true;
                MarkerStartFrame.Text = "" + CurrentScene.Cutscene[MarkerSelect.SelectedIndex].StartFrame;

                DeleteMarker.Enabled = true;


                #region CutscenePosition

                CutsceneAddAbsolutePosition.Enabled = true;


                //   Console.WriteLine("Points count: " + CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Count + "\n Textbox count: " + CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes.Count);

                //  Console.WriteLine("path going");
                CutsceneAbsolutePositionListBox.Items.Clear();

                foreach (ZCutscenePosition point in CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points)
                {
                    CutsceneAbsolutePositionListBox.Items.Add("" + Math.Floor(point.Position.X) + "," + Math.Floor(point.Position.Y) + "," + Math.Floor(point.Position.Z) + ", Frames: " + point.Frames + "");
                }


                if (prevsel2 >= CutsceneAbsolutePositionListBox.Items.Count && CutsceneAbsolutePositionListBox.Items.Count > 0) CutsceneAbsolutePositionListBox.SelectedIndex = prevsel2 - 1;
                else if (prevsel2 >= CutsceneAbsolutePositionListBox.Items.Count) CutsceneAbsolutePositionListBox.SelectedIndex = -1;
                else CutsceneAbsolutePositionListBox.SelectedIndex = prevsel2;

                // Console.WriteLine(MarkerSelect.SelectedIndex + " Marker selected index");
                //  Console.WriteLine(FindComboItemValue(MarkerType.Items, CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Marker) + " comboitem value");
                //  Console.WriteLine(CutsceneAbsolutePositionListBox.Items.Count + " cutscene position counts");

                if (CutsceneTabs.SelectedIndex != 0)
                {
                    CameraPreview_Clear();
                    CutscenePreview_Clear();
                }

                if (CutsceneAbsolutePositionListBox.Items.Count > 0)
                {
                    if (CutsceneAbsolutePositionListBox.SelectedIndex < 0) CutsceneAbsolutePositionListBox.SelectedIndex = 0;

                    var selectedpos = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex];

                    CutsceneAbsolutePositionX.Value = (decimal)selectedpos.Position.X;
                    CutsceneAbsolutePositionY.Value = (decimal)selectedpos.Position.Y;
                    CutsceneAbsolutePositionZ.Value = (decimal)selectedpos.Position.Z;
                    CutscenePositionXFocus.Value = (decimal)selectedpos.Position2.X;
                    CutscenePositionYFocus.Value = (decimal)selectedpos.Position2.Y;
                    CutscenePositionZFocus.Value = (decimal)selectedpos.Position2.Z;
                    CutsceneAbsolutePositionAngleView.Value = (decimal)selectedpos.Angle;
                    CutsceneAbsolutePositionCameraRoll.Value = selectedpos.Cameraroll;
                    CutscenePositionFrameDuration.Value = selectedpos.Frames;
                    CutsceneAbsolutePositionX.Enabled = true;
                    CutsceneAbsolutePositionY.Enabled = true;
                    CutsceneAbsolutePositionZ.Enabled = true;
                    CutscenePositionXFocus.Enabled = true;
                    CutscenePositionYFocus.Enabled = true;
                    CutscenePositionZFocus.Enabled = true;
                    CutsceneAbsolutePositionAngleView.Enabled = true;
                    CutsceneAbsolutePositionCameraRoll.Enabled = true;
                    CutscenePositionFrameDuration.Enabled = true;
                    CutsceneDeleteAbsolutePosition.Enabled = !previewcutscene;
                    CutsceneAddAbsolutePosition.Enabled = !previewcutscene;
                    CutscenePositionCopyCamera.Enabled = true;
                    CutscenePositionViewMode.Enabled = true;
                    CutscenePositionPlayMode.Enabled = true;
                    CutsceneAbsolutePositionListBox.Enabled = !previewcutscene;

                    CutscenePositionDown.Enabled = (CutsceneAbsolutePositionListBox.SelectedIndex < CutsceneAbsolutePositionListBox.Items.Count - 1 && CutsceneAbsolutePositionListBox.SelectedIndex != -1);
                    CutscenePositionUp.Enabled = (CutsceneAbsolutePositionListBox.SelectedIndex > 0);

                }
                else
                {
                    CutsceneAbsolutePositionX.Value = 0;
                    CutsceneAbsolutePositionY.Value = 0;
                    CutsceneAbsolutePositionZ.Value = 0;
                    CutsceneAbsolutePositionX.Enabled = false;
                    CutsceneAbsolutePositionY.Enabled = false;
                    CutsceneAbsolutePositionZ.Enabled = false;
                    CutscenePositionXFocus.Enabled = false;
                    CutscenePositionYFocus.Enabled = false;
                    CutscenePositionZFocus.Enabled = false;
                    CutsceneAbsolutePositionAngleView.Enabled = false;
                    CutsceneAbsolutePositionCameraRoll.Enabled = false;
                    CutscenePositionFrameDuration.Enabled = false;
                    CutsceneDeleteAbsolutePosition.Enabled = false;
                    CutscenePositionCopyCamera.Enabled = false;
                    CutscenePositionViewMode.Enabled = false;
                    CutscenePositionPlayMode.Enabled = false;
                    CutscenePositionUp.Enabled = false;
                    CutscenePositionDown.Enabled = false;
                    CutsceneAddAbsolutePosition.Enabled = (CurrentScene.Cutscene.Count > 0);

                    CameraPreview_Clear();
                    CutscenePreview_Clear();
                }

                if (previewcutscene)
                {
                    CutscenePositionViewMode.Enabled = false;
                    CutscenePositionCopyCamera.Enabled = false;
                    CutsceneAbsolutePositionX.Enabled = false;
                    CutsceneAbsolutePositionY.Enabled = false;
                    CutsceneAbsolutePositionZ.Enabled = false;
                    CutscenePositionXFocus.Enabled = false;
                    CutscenePositionYFocus.Enabled = false;
                    CutscenePositionZFocus.Enabled = false;
                    CutsceneAbsolutePositionAngleView.Enabled = false;
                    CutsceneAbsolutePositionCameraRoll.Enabled = false;
                    CutscenePositionFrameDuration.Enabled = false;
                }

                CutscenePositionViewMode.BackColor = (previewcamerapoints) ? Color.LawnGreen : Color.LightGray;
                CutscenePositionPlayMode.BackColor = (previewcutscene) ? Color.LawnGreen : Color.LightGray;

                #endregion

                #region Textboxes

                CutsceneAddTextbox.Enabled = true;

                CutsceneTextboxList.Items.Clear();

                foreach (ZTextbox textbox in CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes)
                {
                    CutsceneTextboxList.Items.Add("Message: " + textbox.Message.ToString("X4") + ", Frames: " + textbox.Frames + ")");
                }

                if (prevsel3 >= CutsceneTextboxList.Items.Count && CutsceneTextboxList.Items.Count > 0) CutsceneTextboxList.SelectedIndex = prevsel3 - 1;
                else if (prevsel3 >= CutsceneTextboxList.Items.Count) CutsceneTextboxList.SelectedIndex = -1;
                else CutsceneTextboxList.SelectedIndex = prevsel3;

                if (CutsceneTextboxList.Items.Count > 0)
                {
                    if (CutsceneTextboxList.SelectedIndex < 0) CutsceneTextboxList.SelectedIndex = 0;
                    var selectedtextbox = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes[CutsceneTextboxList.SelectedIndex];

                    CutsceneTextboxType.SelectedIndex = FindSongComboItemValue(CutsceneTextboxType.Items, selectedtextbox.Type);
                    CutsceneTextboxMessageId.Text = "" + selectedtextbox.Message.ToString("X4");
                    CutsceneTextboxTopMessageID.Text = "" + selectedtextbox.TopMessage.ToString("X4");
                    CutsceneTextboxBottomMessageID.Text = "" + selectedtextbox.BottomMessage.ToString("X4");
                    CutsceneTextboxFrames.Value = selectedtextbox.Frames;

                    CutsceneTextboxType.Enabled = true;
                    CutsceneTextboxMessageId.Enabled = true;
                    CutsceneTextboxTopMessageID.Enabled = true;
                    CutsceneTextboxBottomMessageID.Enabled = true;
                    CutsceneTextboxFramesLabel.Enabled = true;
                    CutsceneTextboxFrames.Enabled = true;
                    CutsceneDeleteTextbox.Enabled = true;

                    CutsceneTextboxDown.Enabled = (CutsceneTextboxList.SelectedIndex < CutsceneTextboxList.Items.Count - 1 && CutsceneTextboxList.SelectedIndex != -1);
                    CutsceneTextboxUp.Enabled = (CutsceneTextboxList.SelectedIndex > 0);
                }
                else
                {
                    CutsceneTextboxType.Enabled = false;
                    CutsceneTextboxMessageId.Enabled = false;
                    CutsceneTextboxTopMessageID.Enabled = false;
                    CutsceneTextboxBottomMessageID.Enabled = false;
                    CutsceneTextboxFrames.Enabled = false;
                    CutsceneDeleteTextbox.Enabled = false;
                    CutsceneTextboxDown.Enabled = false;
                    CutsceneTextboxUp.Enabled = false;
                    CutsceneAddTextbox.Enabled = (CurrentScene.Cutscene.Count > 0);
                }
                #endregion

                #region CutsceneActor


                if (CutsceneTabs.SelectedIndex == 6)
                {
                    string gameprefix = (!settings.MajorasMask) ? "OOT/" : "MM/";

                    CutsceneActorAnimation.Items.Clear();
                    if (CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Marker == 0x56)
                    {
                        List<SongItem> items = XMLreader.getXMLItems(gameprefix + "SongNames", "Song").ToList();
                        items.RemoveAt(0);
                        foreach (SongItem item in items) item.Value = Convert.ToInt32(item.Value) + 1;
                        CutsceneActorAnimation.Items.AddRange(items.ToArray());
                    }
                    else
                    {

                        if (settings.UndocumentedCutsceneVars)
                        {
                            SongItem[] orgitems = XMLreader.getXMLItems(gameprefix + "CutsceneActions", (MarkerType.SelectedItem as MarkerItem).Actor);
                            List<SongItem> org = orgitems.ToList();
                            List<SongItem> tmp = new List<SongItem>();
                            for (int i = 1; i < 0xFF; i++)
                            {
                                if (org.Find(x => Convert.ToInt32(x.Value) == i) == null)
                                {
                                    SongItem mk = new SongItem();

                                    mk.Text = i.ToString("X4") + " - " + "???";
                                    mk.Value = i;
                                    tmp.Add(mk);
                                }
                            }
                            CutsceneActorAnimation.Items.AddRange(orgitems);
                            CutsceneActorAnimation.Items.AddRange(tmp.ToArray());
                        }
                        else CutsceneActorAnimation.Items.AddRange(XMLreader.getXMLItems(gameprefix + "CutsceneActions", (MarkerType.SelectedItem as MarkerItem).Actor));

                    }

                }

                CutsceneActorAddAction.Enabled = true;


                CutsceneActorListBox.Items.Clear();

                foreach (ZCutsceneActor actor in CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors)
                {
                    CutsceneActorListBox.Items.Add("" + actor.Position.X + "," + actor.Position.Y + "," + actor.Position.Z + ", Frames: " + actor.Frames + "");
                }


                if (prevsel4 >= CutsceneActorListBox.Items.Count && CutsceneActorListBox.Items.Count > 0) CutsceneActorListBox.SelectedIndex = CutsceneActorListBox.Items.Count - 1;
                else if (prevsel4 >= CutsceneActorListBox.Items.Count) CutsceneActorListBox.SelectedIndex = -1;
                else CutsceneActorListBox.SelectedIndex = prevsel4;


                if (CutsceneActorListBox.Items.Count > 0)
                {
                    if (CutsceneActorListBox.SelectedIndex < 0) CutsceneActorListBox.SelectedIndex = 0;

                    var selectedpos = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex];


                    CutsceneActorXStart.Value = (decimal)selectedpos.Position.X;
                    CutsceneActorYStart.Value = (decimal)selectedpos.Position.Y;
                    CutsceneActorZStart.Value = (decimal)selectedpos.Position.Z;
                    CutsceneActorXEnd.Value = (decimal)selectedpos.Position2.X;
                    CutsceneActorYEnd.Value = (decimal)selectedpos.Position2.Y;
                    CutsceneActorZEnd.Value = (decimal)selectedpos.Position2.Z;
                    CutsceneActorXRot.Value = (decimal)selectedpos.Rotation.X;
                    CutsceneActorYRot.Value = (decimal)selectedpos.Rotation.Y;
                    CutsceneActorZRot.Value = (decimal)selectedpos.Rotation.Z;
                    CutsceneActorFrameDuration.Value = selectedpos.Frames;
                    CutsceneActorAnimation.SelectedIndex = FindSongComboItemValue(CutsceneActorAnimation.Items, selectedpos.Animation);
                    if (selectedpos.Animation != Convert.ToUInt16(((SongItem)CutsceneActorAnimation.SelectedItem).Value))
                        selectedpos.Animation = Convert.ToUInt16(((SongItem)CutsceneActorAnimation.SelectedItem).Value);

                    if ((MarkerType.SelectedItem as MarkerItem).Type == "Actor")
                    {
                        CutsceneActorXStart.Enabled = true;
                        CutsceneActorYStart.Enabled = true;
                        CutsceneActorZStart.Enabled = true;
                        CutsceneActorXEnd.Enabled = true;
                        CutsceneActorYEnd.Enabled = true;
                        CutsceneActorZEnd.Enabled = true;
                        CutsceneActorXRot.Enabled = true;
                        CutsceneActorYRot.Enabled = true;
                        CutsceneActorZRot.Enabled = true;
                        CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[0] = 0;
                    }
                    else
                    {
                        CutsceneActorXStart.Enabled = false;
                        CutsceneActorYStart.Enabled = false;
                        CutsceneActorZStart.Enabled = false;
                        CutsceneActorXEnd.Enabled = false;
                        CutsceneActorYEnd.Enabled = false;
                        CutsceneActorZEnd.Enabled = false;
                        CutsceneActorXRot.Enabled = false;
                        CutsceneActorYRot.Enabled = false;
                        CutsceneActorZRot.Enabled = false;
                        CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[0] = 0xFF;
                    }
                    CutsceneActorAnimation.Enabled = true;
                    CutsceneActorAddAction.Enabled = true;
                    CutsceneActorDeleteAction.Enabled = true;
                    CutsceneActorFrameDuration.Enabled = true;


                    CutsceneActorDown.Enabled = (CutsceneActorListBox.SelectedIndex < CutsceneActorListBox.Items.Count - 1 && CutsceneActorListBox.SelectedIndex != -1);
                    CutsceneActorUp.Enabled = (CutsceneActorListBox.SelectedIndex > 0);

                }
                else
                {
                    CutsceneActorXStart.Value = 0;
                    CutsceneActorYStart.Value = 0;
                    CutsceneActorZStart.Value = 0;
                    CutsceneActorXStart.Enabled = false;
                    CutsceneActorYStart.Enabled = false;
                    CutsceneActorZStart.Enabled = false;
                    CutsceneActorXEnd.Enabled = false;
                    CutsceneActorYEnd.Enabled = false;
                    CutsceneActorZEnd.Enabled = false;
                    CutsceneActorXRot.Enabled = false;
                    CutsceneActorYRot.Enabled = false;
                    CutsceneActorZRot.Enabled = false;
                    CutsceneActorFrameDuration.Enabled = false;
                    CutsceneActorAnimation.Enabled = false;
                    CutsceneActorAddAction.Enabled = (CurrentScene.Cutscene.Count > 0);
                    CutsceneActorDeleteAction.Enabled = false;
                    CutsceneActorDown.Enabled = false;
                    CutsceneActorUp.Enabled = false;
                }
                #endregion

                #region transitions and asm

                if (CutsceneTransitionComboBox.SelectedIndex < 0) CutsceneTransitionComboBox.SelectedIndex = 0;
                if (CutsceneAsmComboBox.SelectedIndex < 0) CutsceneAsmComboBox.SelectedIndex = 0;



                CutsceneTransitionComboBox.SelectedIndex = FindSongComboItemValue(CutsceneTransitionComboBox.Items, CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[0]);
                CutsceneAsmComboBox.SelectedIndex = FindSongComboItemValue(CutsceneAsmComboBox.Items, CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[0]);



                #endregion

                #region set time

                if (CutsceneTabs.SelectedIndex == 1)
                {
                    CutsceneSetTimeHours.Value = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[0];
                    CutsceneSetTimeMinutes.Value = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[1];
                }

                #endregion

                //depending on the marker id, the endframe is stored automatically

                uint[] autoendframe;

                if (!settings.MajorasMask)
                {
                    autoendframe = new uint[] { 0x01, 0x05, 0x13, 0x0A, 0x3E };
                }
                else
                {
                    autoendframe = new uint[] { 0x5A, 0xA, 0xC8 };
                }

                if (autoendframe.Contains(CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Marker) || CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Count > 0)
                {
                    MarkerEndFrame.Enabled = false;
                    CurrentScene.Cutscene[MarkerSelect.SelectedIndex].EndFrame = (ushort)(CurrentScene.Cutscene[MarkerSelect.SelectedIndex].StartFrame + CurrentScene.Cutscene[MarkerSelect.SelectedIndex].GetTotalFrames());
                }
                else
                {
                    MarkerEndFrame.Enabled = true;
                }
                MarkerEndFrame.Text = "" + CurrentScene.Cutscene[MarkerSelect.SelectedIndex].EndFrame;



            }
            else
            {
                CutsceneAbsolutePositionX.Enabled = false;
                CutsceneAbsolutePositionY.Enabled = false;
                CutsceneAbsolutePositionZ.Enabled = false;
                CutsceneAbsolutePositionAngleView.Enabled = false;
                CutsceneAbsolutePositionCameraRoll.Enabled = false;
                CutscenePositionFrameDuration.Enabled = false;
                CutsceneDeleteAbsolutePosition.Enabled = false;
                CutscenePositionXFocus.Enabled = false;
                CutscenePositionYFocus.Enabled = false;
                CutscenePositionZFocus.Enabled = false;
                CutscenePositionUp.Enabled = false;
                CutscenePositionDown.Enabled = false;
                CutscenePositionCopyCamera.Enabled = false;
                CutscenePositionViewMode.Enabled = false;
                CutscenePositionPlayMode.Enabled = false;
                MarkerSelect.Items.Clear();
                CutsceneAbsolutePositionListBox.Items.Clear();
                CutsceneActorListBox.Items.Clear();
                CutsceneTextboxList.Items.Clear();
                MarkerUp.Enabled = false;
                MarkerDown.Enabled = false;
                MarkerStartFrame.Enabled = false;
                MarkerEndFrame.Enabled = false;
                CutsceneAddAbsolutePosition.Enabled = (CurrentScene.Cutscene.Count > 0);
                CutsceneAbsolutePositionX.Value = 0;
                CutsceneAbsolutePositionY.Value = 0;
                CutsceneAbsolutePositionZ.Value = 0;
                DeleteMarker.Enabled = false;

                CutsceneTextboxType.Enabled = false;
                CutsceneTextboxMessageId.Enabled = false;
                CutsceneTextboxTopMessageID.Enabled = false;
                CutsceneTextboxBottomMessageID.Enabled = false;
                CutsceneTextboxFrames.Enabled = false;
                CutsceneAddTextbox.Enabled = false;
                CutsceneDeleteTextbox.Enabled = false;

                CutsceneActorDown.Enabled = false;
                CutsceneActorUp.Enabled = false;

                CutsceneTextboxDown.Enabled = false;
                CutsceneTextboxUp.Enabled = false;
            }
            CutsceneEntrance.Enabled = (settings.MajorasMask || CurrentScene.CutsceneTableRow != -1);
            CutsceneEntranceLabel.Enabled = (settings.MajorasMask || CurrentScene.CutsceneTableRow != -1);
            CutsceneEntrance.Value = CurrentScene.CutsceneEntrance;

            CutsceneSpawn.Visible = (settings.MajorasMask);
            CutsceneSpawnLabel.Visible = (settings.MajorasMask);
            CutsceneSpawn.Value = CurrentScene.CutsceneEntranceNum;

            CutsceneFlag.Enabled = (settings.MajorasMask || CurrentScene.CutsceneTableRow != -1);
            CutsceneFlagLabel.Enabled = (settings.MajorasMask || CurrentScene.CutsceneTableRow != -1);
            CutsceneFlag.Value = CurrentScene.CutsceneFlag;

            CutsceneTableEntry.Visible = (!settings.MajorasMask);
            CutsceneTableEntryLabel.Visible = (!settings.MajorasMask);
            CutsceneTableEntry.Value = CurrentScene.CutsceneTableRow;
        }

        #region Camera Preview

        private decimal previewstoredfov = 60;

        private void CameraPreview_Set()
        {
            if (!previewcamerapoints)
            {
                previewcamerapoints = true;
                previewstoredfov = ViewportFOV.Value;

                Console.WriteLine("Stored FOV " + previewstoredfov);
            }
        }

        private void CameraPreview_Clear()
        {
            if (previewcamerapoints)
            {
                previewcamerapoints = false;
                Camera.Rot.Z = 0.0f;
                ViewportFOV.Value = previewstoredfov;
                SetViewport(glControl1.Width, glControl1.Height);
            }
        }

        private void CameraPreview_Toggle()
        {
            if (previewcamerapoints)
                CameraPreview_Clear();
            else
                CameraPreview_Set();
        }

        private void CameraPreview_UpdateTransforms()
        {
            if (!previewcamerapoints) return;
            Camera.Pos = ConvertToCameraPosition((Vector3d)CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Position);
            Vector3d position2 = ConvertToCameraPosition((Vector3d)CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Position2);
            Camera.Rot.Y = Math.Atan2(Camera.Pos.X - position2.X, position2.Z - Camera.Pos.Z) * 180f / Math.PI;
            float hipotenuse = Distance3D((Vector3)Camera.Pos, (Vector3)position2);
            float opposite = (float)(position2.Y - Camera.Pos.Y);
            Camera.Rot.X = Math.Asin(opposite / hipotenuse) * 180f / Math.PI;
        }

        private void CameraPreview_UpdateParams()
        {
            if (!previewcamerapoints) return;
            ViewportFOV.Value = CutsceneAbsolutePositionAngleView.Value;
            Camera.Rot.Z = (float)CutsceneAbsolutePositionCameraRoll.Value * 256.0f * (180.0f / 32767.0f);
            SetViewport(glControl1.Width, glControl1.Height);
        }

        #endregion

        #region Cutscene Play

        public DateTime cutsceneplaystarttime;
        public DateTime cutsceneplaydeltatime;
        public int cutsceneplaycamerakeyframe = 0;
        private float cutsceneplaymod;
        private int cutsceneplayframe;
        private decimal cutscenestoredfov = 60;

        private void CutscenePreview_Set()
        {
            cutscenestoredfov = ViewportFOV.Value;

            fovOverrideFlag = true;
            fovOverride = (float)ViewportFOV.Value;

            CutsceneAbsolutePositionListBox.Enabled = false;
            CutsceneAbsolutePositionListBox.SelectedIndex = 0;
            cutsceneplaystarttime = DateTime.Now;
            cutsceneplaydeltatime = DateTime.Now;
            cutsceneplayframe = 0;
            cutsceneplaycamerakeyframe = 0;
            cutsceneplaymod = 0;

            playcamerapointscache = new List<ZCutscenePosition>(CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points);

            if (!settings.NoDummyPoints)
            {
                int lastid = playcamerapointscache.Count - 1;
                playcamerapointscache.Add(new ZCutscenePosition(0, 20, 45, playcamerapointscache[lastid].Position, playcamerapointscache[lastid].Position2));
                playcamerapointscache.Insert(0, new ZCutscenePosition(0, 20, 45, playcamerapointscache[0].Position, playcamerapointscache[0].Position2));
            }
        }

        private void CutscenePreview_Clear()
        {
            fovOverrideFlag = false;
            previewcutscene = false;
            cutsceneplaycamerakeyframe = 0;
            cutsceneplaymod = 0;
            Camera.Rot.Z = 0.0f;
            ViewportFOV.Value = cutscenestoredfov;
            SetViewport(glControl1.Width, glControl1.Height);
        }

        private void CutscenePreview_Update()
        {
            DateTime time = DateTime.Now;
            float delta = (float)(20.0 / (1.0 / time.Subtract(cutsceneplaydeltatime).TotalSeconds));
            double cur = time.Subtract(cutsceneplaystarttime).TotalSeconds;

            cutsceneplaydeltatime = time;

            if (cur >= (1.0 / 20.0) * CurrentScene.Cutscene[MarkerSelect.SelectedIndex].EndFrame)
            {
                CutscenePreview_Clear();
                UpdateCutsceneEdit();

                return;
            }

#if DEBUG
#else
            if (!notresize)
            {
                notresize = true;
                glControl1.Size = new Size(prevwidth, (int)(prevheight * 0.75));
                FormBorderStyle = FormBorderStyle.FixedDialog;
            }
#endif

            if (cutsceneplaycamerakeyframe + 4 < playcamerapointscache.Count)
            {
                const float i16toDeg = (180.0f / 32768.0f);
                Vector3d[] eye = new Vector3d[4];
                Vector3d[] at = new Vector3d[4];
                float[] roll = new float[4];
                float[] fov = new float[4];

                for (int i = 0; i < 4; i++)
                {
                    eye[i] = (Vector3d)playcamerapointscache[cutsceneplaycamerakeyframe + i].Position;
                    at[i] = (Vector3d)playcamerapointscache[cutsceneplaycamerakeyframe + i].Position2;
                    roll[i] = ((float)playcamerapointscache[cutsceneplaycamerakeyframe + i].Cameraroll) * 256.0f * i16toDeg;
                    fov[i] = playcamerapointscache[cutsceneplaycamerakeyframe + i].Angle;
                }

                float u = cutsceneplaymod;
                float[] coeff = {
                    (1.0f - u) * (1.0f - u) * (1.0f - u) / 6.0f,
                    u * u * u / 2.0f - u * u + 2.0f / 3.0f,
                    -u * u * u / 2.0f + u * u / 2.0f + u / 2.0f + 1.0f / 6.0f,
                    u * u * u / 6.0f
                };

                float SplineValue(float[] value)
                {
                    return coeff[0] * value[0] +
                        coeff[1] * value[1] +
                        coeff[2] * value[2] +
                        coeff[3] * value[3];
                }

                Vector3d SplineVector(Vector3d[] point)
                {
                    Vector3d vec;

                    vec.X = coeff[0] * point[0].X + coeff[1] * point[1].X + coeff[2] * point[2].X + coeff[3] * point[3].X;
                    vec.Y = coeff[0] * point[0].Y + coeff[1] * point[1].Y + coeff[2] * point[2].Y + coeff[3] * point[3].Y;
                    vec.Z = coeff[0] * point[0].Z + coeff[1] * point[1].Z + coeff[2] * point[2].Z + coeff[3] * point[3].Z;

                    return vec;
                }

                Vector3d resultpos = SplineVector(eye);
                Vector3d resultpos2 = SplineVector(at);
                fovOverride = SplineValue(fov);

                Camera.Pos = ConvertToCameraPosition(resultpos);
                Vector3d position2 = ConvertToCameraPosition(resultpos2);
                Camera.Rot.Y = Math.Atan2(Camera.Pos.X - position2.X, position2.Z - Camera.Pos.Z) * 180f / Math.PI;
                float hipotenuse = Distance3D((Vector3)Camera.Pos, (Vector3)position2);
                float opposite = (float)(position2.Y - Camera.Pos.Y);
                Camera.Rot.X = Math.Asin(opposite / hipotenuse) * 180f / Math.PI;
                Camera.Rot.Z = -SplineValue(roll);

                float speed1 = 1.0f / playcamerapointscache[cutsceneplaycamerakeyframe + 1].Frames;
                float speed2 = 1.0f / playcamerapointscache[cutsceneplaycamerakeyframe + 2].Frames;
                float advance = (cutsceneplaymod * (speed2 - speed1)) + speed1;

                if (advance < 0.0f) advance = 0.0f;

                cutsceneplaymod += advance * delta;

                if (cutsceneplaymod >= 1.0f)
                {
                    cutsceneplaycamerakeyframe++;
                    CutsceneAbsolutePositionListBox.SelectedIndex++;

                    cutsceneplaymod -= 1.0f;
                }

                SetViewport(glControl1.Width, glControl1.Height);
            }
        }

        #endregion

        private void UpdateAdditionalLightEdit()
        {
            if (CurrentScene.Rooms.Count <= 0) return;
            if (CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights.Count != 0)
            {
                AdditionalLightSelect.Minimum = 1;
                AdditionalLightSelect.Maximum = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights.Count;
                AdditionalLightSelect.Enabled = true;
                AdditionalLightAdd.Enabled = true;
                if (AdditionalLightSelect.Value <= 0) AdditionalLightSelect.Value = 1;

                AdditionalLightXPos.Value = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].XPos;
                AdditionalLightYPos.Value = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].YPos;
                AdditionalLightZPos.Value = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].ZPos;
                AdditionalLightNS.Value = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].NSdirection;
                AdditionalLightEW.Value = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].EWdirection;
                AdditionalLightRadius.Value = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].Radius;
                AdditionalLightColor.BackColor = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].ColorC;
                PointLightCheckBox.Checked = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].PointLight;
                AdditionalLightColor.Enabled = true;
                PointLightCheckBox.Enabled = true;
                AdditionalLightDelete.Enabled = true;
                AdditionalLightLabel1.Enabled = true;

                if (PointLightCheckBox.Checked)
                {
                    AdditionalLightXPos.Enabled = true;
                    AdditionalLightYPos.Enabled = true;
                    AdditionalLightZPos.Enabled = true;
                    AdditionalLightPointLabel1.Enabled = true;
                    AdditionalLightPointLabel2.Enabled = true;
                    AdditionalLightPointLabel3.Enabled = true;
                    AdditionalLightEW.Enabled = false;
                    AdditionalLightNS.Enabled = false;
                    AdditionalLightRadius.Enabled = true;
                    AdditionalLightDirectionLabel1.Enabled = false;
                    AdditionalLightDirectionLabel2.Enabled = false;
                    AdditionalLightDirectionLabel3.Enabled = true;
                }
                else
                {
                    AdditionalLightXPos.Enabled = false;
                    AdditionalLightYPos.Enabled = false;
                    AdditionalLightZPos.Enabled = false;
                    AdditionalLightPointLabel1.Enabled = false;
                    AdditionalLightPointLabel2.Enabled = false;
                    AdditionalLightPointLabel3.Enabled = false;
                    AdditionalLightEW.Enabled = true;
                    AdditionalLightNS.Enabled = true;
                    AdditionalLightRadius.Enabled = false;
                    AdditionalLightDirectionLabel1.Enabled = true;
                    AdditionalLightDirectionLabel2.Enabled = true;
                    AdditionalLightDirectionLabel3.Enabled = false;
                }

            }
            else
            {
                AdditionalLightXPos.Value = 0;
                AdditionalLightYPos.Value = 0;
                AdditionalLightZPos.Value = 0;
                AdditionalLightNS.Value = 0;
                AdditionalLightEW.Value = 0;
                AdditionalLightRadius.Value = 0;
                AdditionalLightXPos.Enabled = false;
                AdditionalLightYPos.Enabled = false;
                AdditionalLightZPos.Enabled = false;
                AdditionalLightEW.Enabled = false;
                AdditionalLightNS.Enabled = false;
                AdditionalLightColor.Enabled = false;
                AdditionalLightRadius.Enabled = false;
                PointLightCheckBox.Enabled = false;
                AdditionalLightDelete.Enabled = false;
                AdditionalLightPointLabel1.Enabled = false;
                AdditionalLightPointLabel2.Enabled = false;
                AdditionalLightPointLabel3.Enabled = false;
                AdditionalLightDirectionLabel1.Enabled = false;
                AdditionalLightDirectionLabel2.Enabled = false;
                AdditionalLightDirectionLabel3.Enabled = false;
                AdditionalLightLabel1.Enabled = false;
            }
        }

        private void UpdateRenderFunctionEdit()
        {
            // try
            //   {
            if (CurrentScene.SegmentFunctions.Count != 0 && !noupdatetextureanim)
            {
                // UpdateAdditionalTextureGLIDs();

                noupdatetextureanim = true;
                if (CurrentScene.SegmentFunctions.Count <= RenderFunctionID.Value - 8)
                {
                    CurrentScene.SegmentFunctions.Add(new ZSegmentFunction());
                }

                RenderFunctionDown.Enabled = (RenderFunctionSelect.SelectedIndex < CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions.Count - 1 && RenderFunctionSelect.SelectedIndex != -1);
                RenderFunctionUp.Enabled = (RenderFunctionSelect.SelectedIndex > 0);

                //Console.WriteLine("TAb: " + (MarkerType.SelectedItem as MarkerItem).Tab);


                int prevsel = RenderFunctionSelect.SelectedIndex;
                int prevsel2 = FunctionTextureSwapAnimationList.SelectedIndex;
                int prevsel3 = FunctionColorBlendList.SelectedIndex;

                RenderFunctionSelect.Items.Clear();

                FunctionTextureSwapAnimationList.Items.Clear();

                FunctionColorBlendList.Items.Clear();


                foreach (ZTextureAnim textureanim in CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions)
                {
                    RenderFunctionSelect.Items.Add((RenderFunctionType.Items[FindSongComboItemValue(RenderFunctionType.Items, textureanim.Type)] as SongItem).Text + GetFlagInfo(textureanim) + ((textureanim.Preview) ? " [Preview]" : ""));
                }

                // if (MarkerSelect.SelectedIndex < 0 && prevsel == -1) MarkerSelect.SelectedIndex = 0;

                if (prevsel >= RenderFunctionSelect.Items.Count && prevsel < RenderFunctionSelect.Items.Count) RenderFunctionSelect.SelectedIndex = prevsel - 1;
                else if (prevsel >= RenderFunctionSelect.Items.Count) RenderFunctionSelect.SelectedIndex = RenderFunctionSelect.Items.Count - 1;
                else RenderFunctionSelect.SelectedIndex = prevsel;

                if (RenderFunctionSelect.SelectedIndex < 0 && RenderFunctionSelect.Items.Count > 0) RenderFunctionSelect.SelectedIndex = 0;

                if (RenderFunctionSelect.Items.Count > 0)

                {

                    RenderFunctionType.SelectedIndex = FindSongComboItemValue(RenderFunctionType.Items, CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Type);

                    RenderFunctionTabs.SelectedIndex = RenderFunctionType.SelectedIndex;
                }

                DeleteRenderFunction.Enabled = (RenderFunctionSelect.Items.Count > 0);

                FunctionTextureSwapTextureID.Items.Clear();

                FunctionTextureSwapTextureID2.Items.Clear();

                FunctionTextureSwapAnimationImage.Items.Clear();


                RenderFunctionID.Maximum = CurrentScene.SegmentFunctions.Count < 8 ? 8 + CurrentScene.SegmentFunctions.Count : 0xF;

                //Flag controls
                if (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions.Count != 0)
                {
                    foreach (Control Ctrl in RenderFunctionGroupBoxFlag.Controls)
                        Ctrl.Enabled = true;


                    if (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].FlagType == 0xFF)
                        RenderFunctionFlagType.SelectedIndex = RenderFunctionFlagType.Items.Count - 1;
                    else
                        RenderFunctionFlagType.SelectedIndex = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].FlagType;


                    RenderFunctionFlagBitwise.Enabled = RenderFunctionFlagBitwiseLabel.Enabled = (RenderFunctionFlagType.SelectedItem as FlagItem).Bitwise;
                    RenderFunctionFlagLabel.Enabled = RenderFunctionFlagID.Enabled = (RenderFunctionFlagType.SelectedItem as FlagItem).MaxValue != 0;
                    RenderFunctionFlagID.Maximum = (RenderFunctionFlagType.SelectedItem as FlagItem).MaxValue;
                    RenderFunctionFlagID.Minimum = (RenderFunctionFlagType.SelectedItem as FlagItem).MinValue;
                    RenderFunctionFlagLabel.Text = (RenderFunctionFlagBitwiseLabel.Enabled) ? "Offset:" : "Flag ID:";
                    RenderFunctionFlagID.Enabled = (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].FlagType != 0xFF);
                    RenderFunctionFlagReverseCheckbox.Enabled = (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].FlagType != 0xFF);
                    RenderFunctionFlagReverseCheckbox.Checked = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].FlagReverse;
                    RenderFunctionFlagID.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].FlagValue;
                    RenderFunctionFlagBitwise.Text = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].FlagBitwise.ToString("X8");
                    RenderFunctionFlagFreezeCheckBox.Enabled = (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].FlagType != 0xFF);
                    RenderFunctionFlagFreezeCheckBox.Checked = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Freeze;

                    if (!(new uint[] { ZTextureAnim.texframe, ZTextureAnim.texswap, ZTextureAnim.blending, ZTextureAnim.scroll }).Contains(CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Type))
                    {
                        RenderFunctionPreview.Enabled = false;
                        RenderFunctionPreview.BackColor = Color.LightGray;
                    }
                    else
                    {
                        RenderFunctionPreview.Enabled = true;
                        RenderFunctionPreview.BackColor = (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Preview) ? Color.LawnGreen : Color.LightGray;
                    }



                }
                else
                {
                    foreach (Control Ctrl in RenderFunctionGroupBoxFlag.Controls)
                        Ctrl.Enabled = false;

                    foreach (Control Ctrl in RenderFunctionTabs.SelectedTab.Controls)
                        Ctrl.Enabled = false;

                    RenderFunctionPreview.Enabled = false;
                    RenderFunctionPreview.BackColor = Color.LightGray;
                }

                #region texturescroll
                if (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions.Count != 0 && CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Type == ZTextureAnim.scroll)
                {
                    foreach (Control Ctrl in RenderFunctionTabs.SelectedTab.Controls)
                        Ctrl.Enabled = true;


                    FunctionTextureScrollXVelocity.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].XVelocity1;
                    FunctionTextureScrollYVelocity.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].YVelocity1;
                    FunctionTextureScrollWidth.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Width1;
                    FunctionTextureScrollHeight.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Height1;

                    FunctionTextureScrollXVelocity2.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].XVelocity2;
                    FunctionTextureScrollYVelocity2.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].YVelocity2;
                    FunctionTextureScrollWidth2.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Width2;
                    FunctionTextureScrollHeight2.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Height2;


                }
                #endregion

                #region textureswap
                // texture swap
                else if (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions.Count != 0 && CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Type == ZTextureAnim.texswap)
                {
                    foreach (Control Ctrl in RenderFunctionTabs.SelectedTab.Controls)
                        Ctrl.Enabled = true;


                    FunctionTextureSwapTextureID.DisplayMember = "DisplayName";
                    FunctionTextureSwapTextureID.SelectedIndex = -1;
                    FunctionTextureSwapTextureID2.DisplayMember = "DisplayName";
                    FunctionTextureSwapTextureID2.SelectedIndex = -1;

                    foreach (ObjFile.Material Mat in CurrentScene.AdditionalTextures)
                    {
                        FunctionTextureSwapTextureID.Items.Add(Mat);
                        FunctionTextureSwapTextureID2.Items.Add(Mat);

                        if (Mat.DisplayName == CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].TextureSwap)
                        {
                            FunctionTextureSwapTextureID.SelectedIndex = FunctionTextureSwapTextureID.Items.Count - 1;


                        }

                        if (Mat.DisplayName == CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].TextureSwap2)
                        {

                            FunctionTextureSwapTextureID2.SelectedIndex = FunctionTextureSwapTextureID2.Items.Count - 1;

                        }

                        if (FunctionTextureSwapTextureID.SelectedItem == null)
                            FunctionTextureSwapTextureID.SelectedItem = FunctionTextureSwapTextureID.Items[0];
                        if (FunctionTextureSwapTextureID2.SelectedItem == null)
                            FunctionTextureSwapTextureID2.SelectedItem = FunctionTextureSwapTextureID2.Items[0];


                    }

                    if (FunctionTextureSwapTextureID.Items.Count == 0)
                    {

                        FunctionTextureSwapTextureID.Items.Add(DummyMaterial);
                        FunctionTextureSwapTextureID2.Items.Add(DummyMaterial);
                        FunctionTextureSwapTextureID.SelectedIndex = 0;
                        FunctionTextureSwapTextureID2.SelectedIndex = 0;
                    }

                    if (FunctionTextureSwapTextureID.SelectedIndex == -1) { FunctionTextureSwapTextureID.SelectedIndex = 0; FunctionTextureSwapTextureID2.SelectedIndex = 0; }



                    if (FunctionTextureSwapTextureID.SelectedText != "none" && FunctionTextureSwapTextureID.SelectedText != "")
                    {
                        FunctionTextureSwapPictureBox.Image = Image.FromFile((FunctionTextureSwapTextureID.SelectedItem as ObjFile.Material).map_Kd);
                    }
                    if (FunctionTextureSwapTextureID2.SelectedText != "none" && FunctionTextureSwapTextureID2.SelectedText != "")
                    {
                        FunctionTextureSwapPictureBox2.Image = Image.FromFile((FunctionTextureSwapTextureID2.SelectedItem as ObjFile.Material).map_Kd);
                    }




                }

                #endregion

                #region textureswaplist

                // texture swap list
                else if (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions.Count != 0 && CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Type == ZTextureAnim.texframe)
                {
                    foreach (Control Ctrl in RenderFunctionTabs.SelectedTab.Controls)
                        Ctrl.Enabled = true;


                    foreach (ZTextureAnimImage image in CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList)
                    {

                        FunctionTextureSwapAnimationList.Items.Add("" + image.Texture + ", Frames: " + image.Duration + "");
                    }


                    if (prevsel2 >= FunctionTextureSwapAnimationList.Items.Count && FunctionTextureSwapAnimationList.Items.Count > 0) FunctionTextureSwapAnimationList.SelectedIndex = 0;
                    else if (prevsel2 >= FunctionTextureSwapAnimationList.Items.Count) FunctionTextureSwapAnimationList.SelectedIndex = -1;
                    else FunctionTextureSwapAnimationList.SelectedIndex = prevsel2;

                    if (FunctionTextureSwapAnimationList.SelectedIndex < 0 && FunctionTextureSwapAnimationList.Items.Count > 0) FunctionTextureSwapAnimationList.SelectedIndex = 0;



                    FunctionTextureSwapAnimationDown.Enabled = (FunctionTextureSwapAnimationList.SelectedIndex < FunctionTextureSwapAnimationList.Items.Count - 1 && FunctionTextureSwapAnimationList.SelectedIndex != -1);
                    FunctionTextureSwapAnimationUp.Enabled = (FunctionTextureSwapAnimationList.SelectedIndex > 0);

                    FunctionTextureSwapAnimationDuration.Enabled = (FunctionTextureSwapAnimationList.Items.Count > 0);
                    FunctionTextureSwapAnimationImage.Enabled = (FunctionTextureSwapAnimationList.Items.Count > 0);

                    FunctionTextureSwapAnimationDelete.Enabled = (FunctionTextureSwapAnimationList.Items.Count > 0);

                    if (FunctionTextureSwapAnimationList.Items.Count > 0)
                    {


                        FunctionTextureSwapAnimationImage.DisplayMember = "DisplayName";
                        FunctionTextureSwapAnimationImage.SelectedIndex = -1;

                        foreach (ObjFile.Material Mat in CurrentScene.AdditionalTextures)
                        {
                            FunctionTextureSwapAnimationImage.Items.Add(Mat);


                            if (Mat.DisplayName == CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList[FunctionTextureSwapAnimationList.SelectedIndex].Texture)
                            {
                                FunctionTextureSwapAnimationImage.SelectedIndex = FunctionTextureSwapAnimationImage.Items.Count - 1;


                            }


                        }

                        if (FunctionTextureSwapAnimationImage.Items.Count == 0)
                        {

                            FunctionTextureSwapAnimationImage.Items.Add(DummyMaterial);
                            FunctionTextureSwapAnimationImage.SelectedIndex = 0;
                            CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList[FunctionTextureSwapAnimationList.SelectedIndex].Texture = "none";
                        }

                        if (FunctionTextureSwapAnimationImage.SelectedIndex == -1) { FunctionTextureSwapAnimationImage.SelectedIndex = 0; }

                        if (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList[FunctionTextureSwapAnimationList.SelectedIndex].Texture != "none")
                        {
                            FunctionTextureSwapAnimationPictureBox.Image = Image.FromFile((FunctionTextureSwapAnimationImage.SelectedItem as ObjFile.Material).map_Kd);
                        }
                        else
                        {
                            FunctionTextureSwapAnimationPictureBox.Image = new Bitmap(32, 32);
                        }


                        FunctionTextureSwapAnimationDuration.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList[FunctionTextureSwapAnimationList.SelectedIndex].Duration;

                    }

                }

                #endregion

                #region colorlist
                // color list
                else if (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions.Count != 0 && CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Type == ZTextureAnim.blending)
                {
                    foreach (Control Ctrl in RenderFunctionTabs.SelectedTab.Controls)
                        Ctrl.Enabled = true;

                    FunctionColorBlendList.Items.Clear();
                    foreach (ZTextureAnimColor color in CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].ColorList)
                    {

                        FunctionColorBlendList.Items.Add("" + color.Color.ToString("X8") + ", Frames: " + color.Duration + "");

                    }


                    if (prevsel3 >= FunctionColorBlendList.Items.Count && FunctionColorBlendList.Items.Count > 0) FunctionColorBlendList.SelectedIndex = 0;
                    else if (prevsel3 >= FunctionColorBlendList.Items.Count) FunctionColorBlendList.SelectedIndex = -1;
                    else FunctionColorBlendList.SelectedIndex = prevsel3;

                    if (FunctionColorBlendList.SelectedIndex < 0 && FunctionColorBlendList.Items.Count > 0) FunctionColorBlendList.SelectedIndex = 0;



                    FunctionColorBlendDown.Enabled = (FunctionColorBlendList.SelectedIndex < FunctionColorBlendList.Items.Count - 1 && FunctionColorBlendList.SelectedIndex != -1);
                    FunctionColorBlendUp.Enabled = (FunctionColorBlendList.SelectedIndex > 0);

                    FunctionColorBlendFrames.Enabled = (FunctionColorBlendList.Items.Count > 0);

                    FunctionColorBlendAlpha.Enabled = (FunctionColorBlendList.Items.Count > 0);

                    FunctionColorBlendDelete.Enabled = (FunctionColorBlendList.Items.Count > 0);

                    if (FunctionColorBlendList.Items.Count > 0)
                    {
                        FunctionColorBlendColor.BackColor = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].ColorList[FunctionColorBlendList.SelectedIndex].C1C;
                        FunctionColorBlendColor.BackColor = (Color)new Color4(FunctionColorBlendColor.BackColor.R, FunctionColorBlendColor.BackColor.G, FunctionColorBlendColor.BackColor.B, 0xFF);
                        FunctionColorBlendFrames.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].ColorList[FunctionColorBlendList.SelectedIndex].Duration;
                        FunctionColorBlendAlpha.Value = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].ColorList[FunctionColorBlendList.SelectedIndex].C1C.A;

                    }

                }
                #endregion

                #region cameraeffect
                if (CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions.Count != 0 && CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].Type == ZTextureAnim.camera)
                {
                    foreach (Control Ctrl in RenderFunctionTabs.SelectedTab.Controls)
                        Ctrl.Enabled = true;


                    FunctionCameraEffectDropdown.SelectedIndex = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].CameraEffect;

                }
                #endregion


                noupdatetextureanim = false;

            }
            else if (!noupdatetextureanim)
            {
                RenderFunctionSelect.Items.Clear();

                RenderFunctionUp.Enabled = false;
                RenderFunctionDown.Enabled = false;
                DeleteRenderFunction.Enabled = false;

                foreach (Control Ctrl in RenderFunctionTabs.SelectedTab.Controls)
                    Ctrl.Enabled = false;

                foreach (Control Ctrl in RenderFunctionGroupBoxFlag.Controls)
                    Ctrl.Enabled = false;

                RenderFunctionPreview.Enabled = false;
                RenderFunctionPreview.BackColor = Color.LightGray;


            }
            //    }
            //     catch(Exception e)
            //     {
            //         noupdatetextureanim = false;
            //         throw;
            //   }



        }

        private string GetFlagInfo(ZTextureAnim anim)
        {
            string output = " (";
            foreach (FlagItem item in RenderFunctionFlagType.Items)
            {
                if (Convert.ToByte((item as FlagItem).Value) == anim.FlagType)
                {
                    String[] separator = { " - " };
                    output += (item as FlagItem).Text.Split(separator, StringSplitOptions.None)[1];
                    break;
                }
            }
            if (anim.FlagType != 0xFF)
            {
                output += " " + (!(new byte[] { 0x3, 0x8 }).Contains(anim.FlagType) ? anim.FlagValue.ToString("X") : "") + ((new byte[] { 0x9, 0xA, 0xB }).Contains(anim.FlagType) ? " & " + anim.FlagBitwise.ToString("X8") : "") + " is " + (anim.FlagReverse ? "false" : "true");
            }

            output += ")";

            return output;
        }

        private void UpdateGroupSelect(bool resetdisplay = false)
        {
            if (GroupList.SelectedItem != null)
            {
                /* ---- Multitex stuff START ---- */

                /* Multitex material selector */
                MultiTextureComboBox.BeginUpdate();
                MultiTextureComboBox.Items.Clear();
                MultiTextureComboBox.DisplayMember = "DisplayName";
                MultiTextureComboBox.Items.Add(DummyMaterial);
                foreach (ObjFile.Material Mat in CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Materials)
                    MultiTextureComboBox.Items.Add(Mat);

                foreach (ObjFile.Material Mat in CurrentScene.AdditionalTextures)
                    MultiTextureComboBox.Items.Add(Mat);


                // Console.WriteLine("cccc" + CurrentScene.AdditionalTextures.Count);

                MultiTextureComboBox.SelectedIndexChanged -= comboBox3_SelectedIndexChanged;
                if (((ObjFile.Group)GroupList.SelectedItem).MultiTexMaterial != -1)
                    MultiTextureComboBox.SelectedIndex = ((ObjFile.Group)GroupList.SelectedItem).MultiTexMaterial + 1;
                else
                    MultiTextureComboBox.SelectedIndex = 0;
                MultiTextureComboBox.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
                MultiTextureComboBox.EndUpdate();

                //optimization

                n64refresh = false;

                /* Multitex controls */
                numericUpDown5.Value = ((ObjFile.Group)GroupList.SelectedItem).ShiftS;
                numericUpDown6.Value = ((ObjFile.Group)GroupList.SelectedItem).ShiftT;

                ShiftSNumeric.Value = ((ObjFile.Group)GroupList.SelectedItem).BaseShiftS;
                ShiftTNumeric.Value = ((ObjFile.Group)GroupList.SelectedItem).BaseShiftT;

                /* ---- Multitex stuff END ---- */

                GroupPolygonType.Minimum = 1;
                GroupPolygonType.Maximum = CurrentScene.PolyTypes.Count;

                GroupAnimatedBank.Enabled = GroupAnimated.Checked;
                GroupLODDIstance.Enabled = GroupLod.Checked;
                GroupLODGroup.Enabled = GroupLod.Checked;

                GroupCustomizeButton.Enabled = GroupCustom.Checked;

                if (((ObjFile.Group)GroupList.SelectedItem).AnimationBank == 0) ((ObjFile.Group)GroupList.SelectedItem).AnimationBank = 8;

                groupBox1.Enabled = true;
                pictureBox7.BackColor = Color.FromArgb((int)(0xFF000000 | (((ObjFile.Group)GroupList.SelectedItem).TintAlpha & 0xFFFFFF)));
                numericUpDown2.Value = (((ObjFile.Group)GroupList.SelectedItem).TintAlpha >> 24);
                GroupMultitextureAlpha.Value = (((ObjFile.Group)GroupList.SelectedItem).MultiTexAlpha >> 24);
                comboBox1.SelectedIndex = ((ObjFile.Group)GroupList.SelectedItem).TileS;
                comboBox2.SelectedIndex = ((ObjFile.Group)GroupList.SelectedItem).TileT;
                GroupPolygonType.Value = ((ObjFile.Group)GroupList.SelectedItem).PolyType + 1;
                checkBox3.Checked = ((ObjFile.Group)GroupList.SelectedItem).BackfaceCulling;
                GroupAnimated.Checked = ((ObjFile.Group)GroupList.SelectedItem).Animated;
                GroupMetallic.Checked = ((ObjFile.Group)GroupList.SelectedItem).Metallic;
                GroupEnvColor.Checked = ((ObjFile.Group)GroupList.SelectedItem).EnvColor;
                GroupDecal.Checked = ((ObjFile.Group)GroupList.SelectedItem).Decal;
                GroupIgnoreFog.Checked = ((ObjFile.Group)GroupList.SelectedItem).IgnoreFog;
                GroupSmoothRgbaEdges.Checked = ((ObjFile.Group)GroupList.SelectedItem).SmoothRGBAEdges;
                GroupPixelated.Checked = ((ObjFile.Group)GroupList.SelectedItem).Pixelated;
                GroupBillboard.Checked = ((ObjFile.Group)GroupList.SelectedItem).Billboard;
                Group2AxisBillboard.Checked = ((ObjFile.Group)GroupList.SelectedItem).TwoAxisBillboard;
                ReverseLightCheckBox.Checked = ((ObjFile.Group)GroupList.SelectedItem).ReverseLight;
                GroupAnimatedBank.Value = ((ObjFile.Group)GroupList.SelectedItem).AnimationBank;
                GroupLODGroup.Value = ((ObjFile.Group)GroupList.SelectedItem).LodGroup;
                GroupLODDIstance.Value = ((ObjFile.Group)GroupList.SelectedItem).LodDistance;
                GroupLod.Checked = ((ObjFile.Group)GroupList.SelectedItem).LOD;
                GroupAlphaMask.Checked = ((ObjFile.Group)GroupList.SelectedItem).AlphaMask;
                GroupRenderLast.Checked = ((ObjFile.Group)GroupList.SelectedItem).RenderLast;
                GroupVertexNormals.Checked = ((ObjFile.Group)GroupList.SelectedItem).VertexNormals;
                GroupCustom.Checked = ((ObjFile.Group)GroupList.SelectedItem).Custom;

                //optimization

                n64refresh = true;


                CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Prepare(false, CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups);

                if (SimulateN64Gfx && resetdisplay)
                    CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
            }
            else
            {
                GroupPolygonType.Minimum = 0;
                GroupPolygonType.Maximum = 0;

                groupBox1.Enabled = false;
                numericUpDown2.Value = 0;
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                pictureBox7.BackColor = Control.DefaultBackColor;
                GroupPolygonType.Value = 0;
                checkBox3.Checked = false;
            }
        }

        private void SelectRoom()
        {
            SelectRoom(RoomList.Items.Count - 1);
        }

        private void SelectRoom(int Index)
        {

            GroupList.BeginUpdate();
            if (customcombiner != null) customcombiner.Close();
            if (Index >= 0 && !CurrentScene.PregeneratedMesh)
            {
                if (CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.Count > 0) GroupList.DataSource = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups;
                else
                {
                    if (!CurrentScene.NewRoomMode)
                    {
                        GroupList.DataSource = CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Groups;
                    }
                    else
                    {
                        List<ObjFile.Group> tmpgroups = new List<ObjFile.Group>();
                        foreach (ObjFile.Group group in ((ZScene.ZRoom)RoomList.Items[0]).ObjModel.Groups)
                        {
                            group.Name = group.Name.Replace("TAG_", "#");

                            if (group.Name.ToLower().Contains("#room" + Index))
                            {
                                tmpgroups.Add(group);
                            }
                        }

                        GroupList.DataSource = tmpgroups;
                    }
                }
            }
            else
                GroupList.DataSource = null;

            if (RoomList.SelectedItem != null)
            {
                actorEditControl1.SetActors(ref CurrentScene.Rooms[RoomList.SelectedIndex].ZActors);
                if (!CurrentScene.PregeneratedMesh) actorEditControl1.CenterPoint = GetCenterPoint(CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices);
            }
            else
                actorEditControl1.ClearActors();

            GroupList.DisplayMember = "DisplayName";
            GroupList.EndUpdate();
            listBox3.SelectedItem = null;


            if (NowLoading == false) UpdateForm();
        }

        private NumericTextBox ListEditBox;
        private TextBox ListStringEditBox;
        private int itemSelected;

        private void CreateEditBox(object sender, string type)
        {
            ListBox LB = (ListBox)sender;

            itemSelected = LB.SelectedIndex;
            if (itemSelected == -1) return;

            Rectangle r = LB.GetItemRectangle(itemSelected);
            string itemText = "";

            switch (type)
            {
                case "exit":
                    if (!settings.EnableNewExitFormat)
                        itemText = CurrentScene.ExitList[itemSelected].ValueHex;
                    else
                        itemText = CurrentScene.ExitListV2[itemSelected].Raw.ToString("X8");
                    break;

                case "object":
                    itemText = ((ZScene.ZUShort)LB.Items[itemSelected]).ValueHex;
                    break;

                case "room":
                    itemText = CurrentScene.Rooms[RoomList.SelectedIndex].ModelShortFilename;
                    break;
            }

            switch (type)
            {
                case "exit":
                case "object":
                    ListEditBox = new NumericTextBox();
                    ListEditBox.AllowHex = true;
                    ListEditBox.MaxLength = 4;
                    ListEditBox.CharacterCasing = CharacterCasing.Upper;

                    ListEditBox.BackColor = Color.Beige;
                    ListEditBox.Font = listBox3.Font;
                    ListEditBox.BorderStyle = BorderStyle.FixedSingle;

                    ListEditBox.Location = new Point(r.X, r.Y);
                    ListEditBox.Size = new Size(r.Width, r.Height);
                    ListEditBox.Show();
                    ListEditBox.Text = itemText;
                    ListEditBox.Focus();
                    ListEditBox.SelectAll();
                    break;

                case "room":
                    ListStringEditBox = new TextBox();

                    ListStringEditBox.MaxLength = 64;
                    ListStringEditBox.BackColor = Color.Beige;
                    ListStringEditBox.Font = listBox3.Font;
                    ListStringEditBox.BorderStyle = BorderStyle.FixedSingle;

                    ListStringEditBox.Location = new Point(r.X, r.Y);
                    ListStringEditBox.Size = new Size(r.Width, r.Height);
                    ListStringEditBox.Show();
                    ListStringEditBox.Text = itemText;
                    ListStringEditBox.Focus();
                    ListStringEditBox.SelectAll();
                    break;
            }
        }

        private void CloseEditBox()
        {
            if (ListEditBox != null)
                ListEditBox.Hide();
            if (ListStringEditBox != null)
                ListStringEditBox.Hide();
            UpdateForm();
        }

        private void checkBox5_Click(object sender, EventArgs e)
        {
            SimulateN64Gfx = SimulateN64CheckBox.Checked;

            if (SimulateN64Gfx == true && CurrentScene.ColModel != null)
                CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
        }

        private void checkBox5_LostFocus(object sender, EventArgs e)
        {
            //    if (glControl1.Focused == false)
            //         SimulateN64Gfx = checkBox5.Checked = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (SimulateN64CheckBox.Checked == true && CurrentScene.ColModel != null)
                SimulateN64CheckBox.BackColor = Color.LightGreen;
            else
                SimulateN64CheckBox.BackColor = SystemColors.Control;
        }

        private void glControl1_LostFocus(object sender, EventArgs e)
        {
            // if (checkBox5.Focused == false)
            //     SimulateN64Gfx = checkBox5.Checked = false;
        }

        #endregion

        #region Menu Functions

        private void newSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NowLoading = true;

            SimulateN64CheckBox.Checked = false;



            LastScene = "";

            /* Reset camera */
            Camera.Initialize();

            /* Clear form */
            RoomList.DataSource = null;
            GroupList.DataSource = null;
            listBox3.DataSource = null;

            /* Generate new scene */
            CurrentScene = new ZScene();
            CurrentScene.Name = "unnamed";
            CurrentScene.Scale = 1.0f;
            CurrentScene.Music = 0x02;
            CurrentScene.NightSFX = 0x00;
            CurrentScene.SkyboxType = 0x00;
            CurrentScene.version = Program.ApplicationVersion;
            CurrentScene.PregeneratedMesh = false;

            while (CurrentScene.SegmentFunctions.Count < 8)
            {
                CurrentScene.SegmentFunctions.Add(new ZSegmentFunction());
            }

            if (settings.MajorasMask)
            {
                CurrentScene.SceneNumber = 0x12;
                CurrentScene.InjectOffset = 0x02000000;
            }

            NormalHeader = CurrentScene;

            /* Add Link's default spawn point */
            CurrentScene.SpawnPoints.Add(new ZActor(0x0000, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x0FFF));

            /* Add default environment settings */
            SetDefaultEnvironments();

            /* Add default collision polygon type */
            CurrentScene.PolyTypes.Add(new ZColPolyType(0x0000000000000000));

            /* Setup interface */
            RoomList.DataSource = CurrentScene.Rooms;
            RoomList.DisplayMember = "ModelShortFilename";

            ActorEditControl.UpdateFormDelegate TempDelegate = new ActorEditControl.UpdateFormDelegate(UpdateForm);
            actorEditControl1.SetUpdateDelegate(TempDelegate);
            actorEditControl1.SetLabels("Actor", "Actors");
            actorEditControl1.mainform = this;

            actorEditControl2.SetUpdateDelegate(TempDelegate);
            actorEditControl2.SetLabels("Transition", "Transitions");
            actorEditControl2.IsTransitionActor = true;
            actorEditControl2.SetActors(ref CurrentScene.Transitions);
            actorEditControl2.mainform = this;

            actorEditControl3.SetUpdateDelegate(TempDelegate);
            actorEditControl3.SetLabels("Spawn", "Spawn Points");
            actorEditControl3.IsSpawnActor = true;
            actorEditControl3.SetActors(ref CurrentScene.SpawnPoints);
            actorEditControl3.mainform = this;

            PathwayListBox.Items.Clear();


            SetPolyTypesInCollision();

            NowLoading = false;

            UpdateForm();

            SceneLoaded = false;

            if (settings.firsttime)
            {
                settings.firsttime = false;
                IO.Export<Settings>(settings, "Settings.xml");
                showControlsToolStripMenuItem_Click(new object(), new EventArgs());
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showCollisionModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.ShowCollisionModel = showCollisionModelToolStripMenuItem.Checked;
        }

        private void showRoomModelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.ShowRoomModels = showRoomModelsToolStripMenuItem.Checked;
        }

        private void applyEnvironmentLightingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.ApplyEnvLighting = applyEnvironmentLightingToolStripMenuItem.Checked;
        }

        private void consecutiveRoomInjectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.ConsecutiveRoomInject = consecutiveRoomInjectionToolStripMenuItem.Checked;
            UpdateForm();
        }

        private void forceRGBATexturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.ForceRGBATextures = forceRGBATexturesToolStripMenuItem.Checked;
        }

        public static bool IsFileLocked(string file)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenWrite(file);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

        private void injectToROMToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string ROM = "";

            if (rom64.isSet())
            {
                AddMissingObjects();
                CurrentScene.ConvertSave(rom64.getPath() + Path.DirectorySeparatorChar, settings.ConsecutiveRoomInject, settings.ForceRGBATextures, 3);
                return;
            }

            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.FileName = "";
                saveFileDialog1.Filter = "Nintendo 64 ROMs (*.z64)|*.z64|All Files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                injectToROMToolStripMenuItem.Owner.Hide();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    FileInfo info = new FileInfo(saveFileDialog1.FileName);
                    if (info.Length < 33554432 + 50000)
                    {
                        MessageBox.Show("This ROM is not uncompressed! go to Tools > Decompress ROM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ROM = saveFileDialog1.FileName;
                }
            }

            if (ROM != "")
            {
                InjectToRom(ROM);
            }
        }

        private void InjectToRom(string rom)
        {
            int prev = CurrentScene.cloneid;
            if (prev > 0) SetSceneHeader(0);

            ROM rom2 = CheckVersion(new List<byte>(File.ReadAllBytes(rom)));
            var Game = rom2.Game;



            AddMissingObjects();


            if (CurrentScene.Cameras.Count == 0)
            {
                CurrentScene.Cameras.Add(new ZCamera(0, 0, 0, 0, 0, 0, 3, 45, 0xFFFF, 0xFFFF));
                UpdateForm();
            }
            AutoFixErrors(rom);


            if (IsFileLocked(rom))
                MessageBox.Show("Injection failed... the ROM is currently being used by another program. Close it and try again.", "Injection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                if (Is1April && Game == "OOT")
                {
                    bool addedbombiwa = false, addedfun = false;
                    Random rnd = new Random();
                    for (int i = 0; i < CurrentScene.Rooms.Count; i++)
                    {
                        if (CurrentScene.Rooms[i].ZActors.FindAll(x => x.Number == 0x0127).Count == 0)
                        {
                            ZActor[] actors = new[]
                            {
                                new ZActor() {Number = 0x0127, Variable = (ushort) rnd.Next(0x38, 0x3F)},
                                new ZActor() {Number = 0x0127, Variable = (ushort) rnd.Next(0x38, 0x3F)},
                                new ZActor() {Number = 0x0127, Variable = (ushort) rnd.Next(0x38, 0x3F)},
                                new ZActor() {Number = 0x0127, Variable = (ushort) rnd.Next(0x38, 0x3F)},
                                new ZActor() {Number = 0x0127, Variable = (ushort) rnd.Next(0x38, 0x3F)},

                            };
                            PlaceActors(i, actors);
                            if (CurrentScene.Rooms[i].ZObjects.FindAll(x => x.Value == 0x0163).Count == 0)
                            {
                                CurrentScene.Rooms[i].ZObjects.Add(new ZScene.ZUShort(0x0163));
                            }
                            addedbombiwa = true;
                        }
                        if (CurrentScene.Rooms[i].ZActors.FindAll(x => x.Number == 0x00A7 && x.Variable == 0x00F2).Count == 0)
                        {
                            ZActor[] actors = new[]
                          {
                                new ZActor() {Number = 0x00A7, Variable = 0x00F2},
                                new ZActor() {Number = 0x00A7, Variable = 0x00F2},

                        };
                            PlaceActors(i, actors);
                            if (CurrentScene.Rooms[i].ZObjects.FindAll(x => x.Value == 0x0017).Count == 0)
                            {
                                CurrentScene.Rooms[i].ZObjects.Add(new ZScene.ZUShort(0x0017));
                            }
                            addedfun = true;
                        }
                    }

                    if (addedbombiwa)
                        MessageBox.Show("I noticed that you didn't placed any bombiwa in some rooms, but don't worry! I placed them for you!", "Injection", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    if (addedfun)
                        MessageBox.Show("Added some fun! \n\nHave a happy 1-April day - Nokaubure", "Injection", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                CurrentScene.ConvertInject(rom, settings.ConsecutiveRoomInject, settings.ForceRGBATextures, Game);
                if (GlobalROM == "" && !rom64.isSet()) RefreshRecentRoms(rom);

                LastInject = rom;
                LaunchRomToolStripMenuItem.Enabled = true;

                if (settings.UpdateCRC && !settings.GenerateCustomDMATable)
                {
                    RecalculateCRC(File.Open(rom, FileMode.Open, FileAccess.ReadWrite));
                    // Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"rn64crc/rn64crc.exe"), "-u " + saveFileDialog1.FileName);

                }
                if (settings.GenerateCustomDMATable)
                {
                    RebuildDMATable(rom, true);
                }
                string outputmsg = "";
                while (InjectMessages.Count != 0)
                {
                    outputmsg += InjectMessages[0] + "\n";
                    InjectMessages.RemoveAt(0);
                }
                if (outputmsg != "") MessageBox.Show(outputmsg, "Injection", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            if (prev > 0) SetSceneHeader(prev);

            UpdateForm();
        }

        private bool CheckForAnythingAnimated()
        {
            bool ret = false;
            for (int i = 0; i < CurrentScene.Rooms.Count; i++)
            {

                for (int j = 0; j < CurrentScene.Rooms[i].ObjModel.Groups.Count; j++)
                {
                    if (CurrentScene.Rooms[i].ObjModel.Groups[j].Animated)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }

        private void saveSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.CheckFileExists = false;
            if (SceneLoaded == false && Helpers.MakeValidFileName(CurrentScene.Name) != string.Empty)
                saveFileDialog1.FileName = Helpers.MakeValidFileName(CurrentScene.Name) + ".xml";
            else
                saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "XML Scene File (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveScene(saveFileDialog1.FileName);
            }
        }

        private void SaveScene(string FileName, bool autosave = false)
        {
            int prev = CurrentScene.cloneid;
            if (prev > 0) SetSceneHeader(0);
            if (!autosave) AddMissingObjects();
            if (!autosave) LastScene = FileName;

            string GetRelativePath(string relativeTo, string path)
            {
                var uri = new Uri(relativeTo);
                var rel = Uri.UnescapeDataString(uri.MakeRelativeUri(new Uri(path)).ToString()).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                if (rel.Contains(Path.DirectorySeparatorChar.ToString()) == false)
                {
                    rel = $"{ rel }";
                }
                return rel;
            }

            foreach (var room in CurrentScene.Rooms)
                if (Path.IsPathRooted(room.ModelFilename))
                    room.ModelFilename = GetRelativePath(Path.GetDirectoryName(FileName) + "\\", room.ModelFilename);
            if (Path.IsPathRooted(CurrentScene.CollisionFilename))
                CurrentScene.CollisionFilename = GetRelativePath(Path.GetDirectoryName(FileName) + "\\", CurrentScene.CollisionFilename);

            IO.Export<ZScene>(CurrentScene, FileName);

            if (prev > 0) SetSceneHeader(prev);
        }

        private void AddMissingObjects()
        {
            if (settings.AutoaddObjects)
            {
                String messagetext = "";
                int roomind = 0;
                bool fieldobj = false, dngobj = false, invalidobj = false;

                List<ZScene> Headers = new List<ZScene>();
                Headers.Add(NormalHeader);
                Headers.AddRange(NormalHeader.SceneHeaders.ConvertAll(x => (x.Scene)));
                foreach (ZScene scene in Headers)
                {
                    roomind = 0;
                    foreach (ZScene.ZRoom room in scene.Rooms)
                    {
                        foreach (ZActor actor in room.ZActors)
                        {
                            // Console.WriteLine("before " + actor.Number.ToString("X4"));
                            int[] actorobjects = new int[0];
                            if (ActorCache.ContainsKey(actor.Number))
                            {
                                if (ActorCache[actor.Number].objects != "")
                                {
                                    string[] strobjects = ActorCache[actor.Number].objects.Split(',');
                                    actorobjects = new int[strobjects.Length];
                                    int incr = 0;
                                    foreach (String s in strobjects)
                                    {
                                        actorobjects[incr] = Convert.ToInt32(s, 16);
                                        incr++;

                                    }
                                }
                            }
                            else
                                actorobjects = XMLreader.getActorObject(actor.Number.ToString("X4"));
                            //Console.WriteLine("after " + actorgroups.ToString("X4"));
                            foreach (int actorobject in actorobjects)
                            {
                                if (actorobject > 0x0003 && !room.ZObjects.Exists(x => x.Value == actorobject))
                                {
                                    ZScene.ZUShort newgroup = new ZScene.ZUShort((ushort)actorobject);
                                    room.ZObjects.Add(newgroup);
                                    string extrainfo = "";
                                    if (ObjectCache.ContainsKey((ushort)actorobject))
                                        extrainfo = ObjectCache[(ushort)actorobject].usedby;
                                    else
                                        extrainfo = XMLreader.getActorNamesByObject(actorobject.ToString("X4"));
                                    messagetext += "Header " + scene.cloneid + " | Room" + roomind + ": " + actorobject.ToString("X4") + " (" + extrainfo + ")" + Environment.NewLine;
                                }
                                else if (actorobject == 0x0002) fieldobj = true;
                                else if (actorobject == 0x0003) dngobj = true;
                            }
                        }
                        roomind++;

                        if (room.ZObjects.Count > 0x0F)
                        {
                            MessageBox.Show("Too many objects on Header " + scene.cloneid + " | Room " + roomind + "! max is 0x0F", "Auto-add objects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (room.ZObjects.Exists(x => x.Value < 4))
                        {
                            invalidobj = true;
                            while (room.ZObjects.FindIndex(x => x.Value < 4) != -1)
                                room.ZObjects.RemoveAt(room.ZObjects.FindIndex(x => x.Value < 4));
                        }
                    }
                }
                if (messagetext != "")
                {
                    MessageBox.Show("The following objects are missing and have been added: " + Environment.NewLine + Environment.NewLine + messagetext, "Auto-add objects", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateForm();
                }
                if (fieldobj && CurrentScene.SpecialObject == 0x0003)
                {
                    MessageBox.Show("There are actors with object 0x0002 while the scene special object is set to 0x0003! Fix this before running the Rom!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dngobj && CurrentScene.SpecialObject == 0x0002)
                {
                    MessageBox.Show("There are actors with object 0x0003 while the scene special object is set to 0x0002! Fix this before running the Rom!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (invalidobj)
                {
                    MessageBox.Show("Never use objects 0000-0003 in any room! Deleting them.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void AutoFixErrors(string romfilename)
        {
            if (settings.AutoFixErrors && !settings.MajorasMask)
            {
                String messagetext = "";
                int roomind = 0;
                bool invalidtransitions = false, invalidcameras = false, invalidprerender = false, invalidcutscene = false, invalidspawns = false, pointlightpatchmissing = false, textureanimpatchmissing = false, invalidsegment = false, extendrampatchmissing = false, cleardmatable = false;

                foreach (ZActor transition in CurrentScene.Transitions)
                {
                    if (transition.BackSwitchTo >= CurrentScene.Rooms.Count && transition.BackSwitchTo != 0xFF)
                    {
                        transition.BackSwitchTo = 0;
                        invalidtransitions = true;
                    }
                    if (transition.FrontSwitchTo >= CurrentScene.Rooms.Count && transition.FrontSwitchTo != 0xFF)
                    {
                        transition.FrontSwitchTo = 0;
                        invalidtransitions = true;
                    }
                    if (transition.BackCamera > CurrentScene.Cameras.Count && transition.BackCamera != 0xFF)
                    {
                        transition.BackCamera = 0xFF;
                        invalidcameras = true;
                    }
                    if (transition.FrontCamera > CurrentScene.Cameras.Count && transition.FrontCamera != 0xFF)
                    {
                        transition.FrontCamera = 0xFF;
                        invalidcameras = true;
                    }
                }

                if (CurrentScene.prerenderimages.Count > 0 && CurrentScene.SkyboxType <= 1)
                {
                    CurrentScene.SkyboxType = 11;
                    invalidprerender = true;
                }



                if (CurrentScene.prerenderimages.Count > 0)
                {
                    foreach (ZActor spawn in CurrentScene.SpawnPoints)
                    {
                        if ((ushort)(spawn.Variable & 0x00FF) == 0x00FF)
                        {
                            spawn.Variable = (ushort)(spawn.Variable & 0xFF00);
                            invalidspawns = true;
                        }

                    }
                }

                if (settings.command1AOoT)
                {
                    foreach (ZSegmentFunction seg in CurrentScene.SegmentFunctions)
                    {
                        if ((seg.HasConditional() && seg.HasPointer()) || (seg.HasScroll() && seg.HasPointer()))
                        {
                            invalidsegment = true;
                        }
                    }
                }

                if (romfilename != "")
                {


                    var data = new List<byte>(File.ReadAllBytes(romfilename));
                    ROM rom = MainForm.CheckVersion(data);

                    if (CurrentScene.CutsceneTableRow == -1)
                    {



                        int offset = (int)(rom.CutsceneTableStart);

                        while (offset < rom.CutsceneTableEnd)
                        {
                            ushort entrance = Helpers.Read16(data, offset);

                            ushort scene = data[(int)(rom.EntranceTableStart + (entrance * 4))];

                            if (scene == CurrentScene.SceneNumber)
                            {

                                List<byte> Temp3 = new List<byte>();


                                Helpers.Append16(ref Temp3, 0xFFFF);

                                BinaryWriter BWS = new BinaryWriter(File.OpenWrite(romfilename));

                                BWS.Seek(offset, SeekOrigin.Begin);
                                BWS.Write(Temp3.ToArray());

                                BWS.Close();

                                invalidcutscene = true;
                            }

                            offset += 8;
                        }


                    }



                    bool haspointlight = false;

                    foreach (ZScene.ZRoom room in CurrentScene.Rooms)
                    {
                        foreach (ZAdditionalLight light in room.AdditionalLights)
                        {
                            if (light.PointLight)
                            {
                                haspointlight = true;
                                break;
                            }
                        }
                        // also check billboards
                        foreach (ObjFile.Group group in room.TrueGroups)
                        {
                            if (group.TwoAxisBillboard)
                            {
                                haspointlight = true;
                                break;
                            }
                        }
                    }

                    if (haspointlight && rom.Prefix == "DBGMQ")
                    {
                        if (Helpers.Read32(data, 0xB0E640) != 0x08028A6F)
                        {
                            pointlightpatchmissing = true;
                        }
                    }

                    if (settings.command1AOoT && rom.Prefix == "DBGMQ")
                    {
                        if (Helpers.Read32(data, 0xBA1554) != 0x8009e658)
                        {
                            textureanimpatchmissing = true;

                        }
                    }

                    if (rom.Prefix == "DBGMQ")
                    {
                        if (data[0x00B415B3] != 0x74)
                        {
                            extendrampatchmissing = true;

                        }
                    }

                    if (settings.GenerateCustomDMATable == false)
                    {
                        if (Helpers.Read32(data, (int)rom.SceneDmaTableStart) != 0)
                        {
                            cleardmatable = true;

                        }
                    }
                }
                if (invalidtransitions)
                {
                    MessageBox.Show("Some of your transition actors were pointing to nonexistent rooms! This would cause a crash ingame so setting them to 0, but you should change them accordingly!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (invalidcameras)
                {
                    MessageBox.Show("Some of your transition actors had their camera IDs set to nonexistent IDs! This can cause weird effects so setting them to FF", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (invalidcutscene)
                {
                    MessageBox.Show("There's a cutscene entrance entry of this scene in the cutscene table, but this scene has no cutscene so the entry will be removed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (invalidprerender)
                {
                    MessageBox.Show("Prerendered scenes crashes if the skybox is not set to a prerender scene, setting them to 11 - kokiri shop (no need to change if you don't use bird view C-up)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (invalidspawns)
                {
                    MessageBox.Show("Prerendered scenes crashes if the link spawner has no camera assigned, setting them to 0, but you should change them accordingly!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (extendrampatchmissing)
                {
                    MessageBox.Show("Your debug ROM lacks the 8mb ram hack. Its extremely recommended to use it, so the patch has been applied automatically.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ExtendRamPatch(romfilename);
                }
                if (cleardmatable)
                {
                    // MessageBox.Show("Your debug ROM lacks the 8mb ram hack. Its extremely recommended to use it, so the patch has been applied automatically.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearSceneDmaTable(romfilename);
                }
                if (pointlightpatchmissing)
                {
                    MessageBox.Show("You're using point lights without the point lights patch. The patch has been applied automatically.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ApplyAdditionalLightsPatch(romfilename);
                }
                if (textureanimpatchmissing)
                {
                    MessageBox.Show("You're using texture animations without the texture animations patch. The patch has been applied automatically.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ApplyTextureAnimPatch(romfilename);
                }
                if (invalidsegment)
                {
                    MessageBox.Show("You cannot use texture swap in combination of texture scroll or conditional draw in the same animation segment, fix it and inject again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


            actorEditControl1.UpdateForm();
            actorEditControl2.UpdateForm();
            actorEditControl3.UpdateForm();
            UpdateForm();
        }


        private void openSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "XML Scene File (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (customcombiner != null) customcombiner.Close();

            openSceneToolStripMenuItem.Owner.Hide();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenScene(openFileDialog1.FileName);

            }
        }

        private void OpenScene(string FileName)
        {
            LastScene = FileName;

            string temppath = "", fullpath;

            NowLoading = true;

            Camera.Initialize();

            RoomList.DataSource = null;
            GroupList.DataSource = null;
            listBox3.DataSource = null;

            CurrentScene = IO.Import<ZScene>(FileName);
            NormalHeader = CurrentScene;
            CurrentScene.BasePath = System.IO.Path.GetDirectoryName(FileName);
            fullpath = Path.GetDirectoryName(FileName) + Path.DirectorySeparatorChar;
            temppath = CurrentScene.CollisionFilename;

            while (CurrentScene.SegmentFunctions.Count < 8)
            {
                CurrentScene.SegmentFunctions.Add(new ZSegmentFunction());
            }



            if (!File.Exists(temppath))
                temppath = fullpath + Path.GetFileName(temppath);
            if (!File.Exists(temppath))
            {
                MessageBox.Show("File " + temppath + " doesn't exists, fix XML paths before attempting to open it again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CurrentScene.ColModel = new ObjFile(temppath, true);
            CurrentScene.CollisionFilename = temppath;

            for (int i = 0; i < CurrentScene.Rooms.Count; i++)
            {
                if (!CurrentScene.PregeneratedMesh)
                {
                    temppath = CurrentScene.Rooms[i].ModelFilename;
                    if (!File.Exists(temppath))
                        temppath = fullpath + Path.GetFileName(temppath);
                    if (!File.Exists(temppath))
                    {
                        MessageBox.Show("File " + temppath + " doesn't exists, fix XML paths before attempting to open it again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    CurrentScene.Rooms[i].ObjModel = new ObjFile(temppath);
                    CurrentScene.Rooms[i].ModelFilename = temppath;

                    //old XML compatibility
                    if (CurrentScene.Rooms[i].TrueGroups == null || CurrentScene.Rooms[i].TrueGroups.Count == 0)
                    {
                        CurrentScene.Rooms[i].TrueGroups = new List<ObjFile.Group>();

                        if (CurrentScene.NewRoomMode)
                        {

                            foreach (ObjFile.Group group in CurrentScene.Rooms[i].ObjModel.Groups)
                            {
                                group.Name = group.Name.Replace("TAG_", "#");

                                if (group.Name.ToLower().Contains("#room"))
                                {

                                    string s = group.Name.Substring(group.Name.ToLower().IndexOf("#room") + 5);
                                    if (s.Contains("#"))
                                        s = s.Substring(0, s.IndexOf("#"));

                                    int tmp = 9999;

                                    if (!Int32.TryParse(s, out tmp))
                                    {
                                        MessageBox.Show("Bad usage of #Room tag. The tag needs to be at the end of the group name or before another tag.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }


                                    if (tmp == i)
                                        CurrentScene.Rooms[i].TrueGroups.Add(group);


                                }
                            }

                        }
                        else { CurrentScene.Rooms[i].TrueGroups = CurrentScene.Rooms[i].ObjModel.Groups; }
                    }
                }
                else
                {
                    CurrentScene.Rooms[i].TrueGroups = new List<ObjFile.Group>();
                }

                foreach (ZEnvironment env in CurrentScene.Environments)
                {
                    if (env.FogDistance > 0x03FF)
                    {
                        ushort oldfogval = env.FogDistance;
                        env.FogDistance = (ushort)(oldfogval & 0x03FF);
                        env.FogUnknown = (ushort)((oldfogval & 0xFC00) >> 10);
                    }
                }

                foreach (var multitex in CurrentScene.AdditionalTextures)
                {
                    temppath = multitex.map_Kd;
                    if (!File.Exists(temppath))
                    {
                        temppath = fullpath + Path.GetFileName(temppath);
                        if (!File.Exists(temppath))
                        {
                            MessageBox.Show("Texture " + temppath + " doesn't exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            multitex.map_Kd = temppath;


                            multitex.TexImage = new Bitmap(Bitmap.FromFile(multitex.map_Kd));
                            multitex.GLID = TexUtil.CreateTextureFromBitmap(multitex.TexImage);
                            multitex.Width = multitex.TexImage.Width;
                            multitex.Height = multitex.TexImage.Height;
                        }


                    }
                    else
                    {

                        multitex.TexImage = new Bitmap(Bitmap.FromFile(multitex.map_Kd));
                        multitex.GLID = TexUtil.CreateTextureFromBitmap(multitex.TexImage);
                        multitex.Width = multitex.TexImage.Width;
                        multitex.Height = multitex.TexImage.Height;
                    }
                }

                //old jfif format

                if (CurrentScene.JFIFpath != "")
                {
                    CurrentScene.prerenderimages.Add(CurrentScene.JFIFpath);
                    CurrentScene.JFIFpath = "";
                }

                if (!CurrentScene.PregeneratedMesh)

                {

                    ValidateGroupSettings(ref CurrentScene.Rooms[i].GroupSettings, CurrentScene.Rooms[i].TrueGroups.Count);

                    // compatibility fix, old scenes didn't had decal checkbox
                    if (CurrentScene.Rooms[i].GroupSettings.Decal.Length < CurrentScene.Rooms[i].GroupSettings.TileS.Length)
                        CurrentScene.Rooms[i].GroupSettings.Decal = new bool[CurrentScene.Rooms[i].GroupSettings.TileS.Length];

                    if (CurrentScene.Rooms[i].GroupSettings.IgnoreFog.Length < CurrentScene.Rooms[i].GroupSettings.TileS.Length)
                        CurrentScene.Rooms[i].GroupSettings.IgnoreFog = new bool[CurrentScene.Rooms[i].GroupSettings.TileS.Length];

                    if (CurrentScene.Rooms[i].GroupSettings.SmoothRGBAEdges.Length < CurrentScene.Rooms[i].GroupSettings.TileS.Length)
                        CurrentScene.Rooms[i].GroupSettings.SmoothRGBAEdges = new bool[CurrentScene.Rooms[i].GroupSettings.TileS.Length];


                    if (CurrentScene.Rooms[i].GroupSettings.Pixelated.Length < CurrentScene.Rooms[i].GroupSettings.TileS.Length)
                        CurrentScene.Rooms[i].GroupSettings.Pixelated = new bool[CurrentScene.Rooms[i].GroupSettings.TileS.Length];

                    if (CurrentScene.Rooms[i].GroupSettings.Billboard.Length < CurrentScene.Rooms[i].GroupSettings.TileS.Length)
                        CurrentScene.Rooms[i].GroupSettings.Billboard = new bool[CurrentScene.Rooms[i].GroupSettings.TileS.Length];

                    if (CurrentScene.Rooms[i].GroupSettings.TwoAxisBillboard.Length < CurrentScene.Rooms[i].GroupSettings.TileS.Length)
                        CurrentScene.Rooms[i].GroupSettings.TwoAxisBillboard = new bool[CurrentScene.Rooms[i].GroupSettings.TileS.Length];

                    if (CurrentScene.Rooms[i].GroupSettings.AlphaMask.Length < CurrentScene.Rooms[i].GroupSettings.TileS.Length)
                        CurrentScene.Rooms[i].GroupSettings.AlphaMask = new bool[CurrentScene.Rooms[i].GroupSettings.TileS.Length];

                    if (CurrentScene.Rooms[i].GroupSettings.RenderLast.Length < CurrentScene.Rooms[i].GroupSettings.TileS.Length)
                        CurrentScene.Rooms[i].GroupSettings.RenderLast = new bool[CurrentScene.Rooms[i].GroupSettings.TileS.Length];

                    if (CurrentScene.Rooms[i].GroupSettings.VertexNormals.Length < CurrentScene.Rooms[i].GroupSettings.TileS.Length)
                        CurrentScene.Rooms[i].GroupSettings.VertexNormals = new bool[CurrentScene.Rooms[i].GroupSettings.TileS.Length];

                    SetTrueGroupsToGroupSettings(i);




                    CurrentScene.Rooms[i].ObjModel.Prepare(CurrentScene.Rooms[i].TrueGroups);

                }
            }

            foreach (ZSceneHeader sceneheader in CurrentScene.SceneHeaders)
            {
                sceneheader.Scene.ColModel = CurrentScene.ColModel;
                int cnt = 0;
                foreach (ZScene.ZRoom room in sceneheader.Scene.Rooms)
                {
                    room.ObjModel = CurrentScene.Rooms[cnt].ObjModel;
                    room.TrueGroups = CurrentScene.Rooms[cnt].TrueGroups;
                    cnt++;
                }
            }



            ActorEditControl.UpdateFormDelegate TempDelegate = new ActorEditControl.UpdateFormDelegate(UpdateForm);
            actorEditControl1.SetUpdateDelegate(TempDelegate);
            actorEditControl1.SetLabels("Actor", "Actors");
            actorEditControl1.mainform = this;


            actorEditControl2.SetUpdateDelegate(TempDelegate);
            actorEditControl2.SetLabels("Transition", "Transitions");
            actorEditControl2.IsTransitionActor = true;
            actorEditControl2.SetActors(ref CurrentScene.Transitions);
            actorEditControl2.mainform = this;

            actorEditControl3.SetUpdateDelegate(TempDelegate);
            actorEditControl3.SetLabels("Spawn", "Spawn Points");
            actorEditControl3.IsSpawnActor = true;
            actorEditControl3.SetActors(ref CurrentScene.SpawnPoints);
            actorEditControl3.mainform = this;

            if (CurrentScene.version == 0)
            {
                foreach (ZWaterbox waterbox in CurrentScene.Waterboxes)
                {
                    waterbox.Env = (byte)((waterbox.Properties & 0xFF00) >> 8);
                    waterbox.Camera = (byte)(waterbox.Properties & 0x00FF);
                }
            }

            int prevpoly = 0;

            if (!CurrentScene.PregeneratedMesh && CurrentScene.Rooms.Count != 0) prevpoly = CurrentScene.Rooms[0].ObjModel.Groups[0].PolyType; //weird fix with polytypes

            RoomList.DataSource = CurrentScene.Rooms;
            RoomList.DisplayMember = "ModelShortFilename";

            if (!CurrentScene.PregeneratedMesh && CurrentScene.Rooms.Count != 0) CurrentScene.Rooms[0].ObjModel.Groups[0].PolyType = prevpoly; //weird fix with polytypes

            if (RoomList.SelectedItem != null)
                if (CurrentScene.Rooms.Count != -1)
                    SelectRoom(0);
            SelectObject(-1);

            SetPolyTypesInCollision();

            NowLoading = false;

            UpdateForm();

            SceneLoaded = true;

            RefreshRecetMenuItems(ref openSceneToolStripMenuItem, "SceneFile", FileName);

            if (CurrentScene.PregeneratedMesh)
            {
                SimulateN64Gfx = true;

                SimulateN64CheckBox.Checked = true;

                CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);

            }
            else if (SimulateN64Gfx)
            {
                CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
            }
        }

        private void SetTrueGroupsToGroupSettings(int i)
        {

            for (int j = 0; j < CurrentScene.Rooms[i].TrueGroups.Count; j++)
            {
                CurrentScene.Rooms[i].TrueGroups[j].TintAlpha = CurrentScene.Rooms[i].GroupSettings.TintAlpha[j];
                CurrentScene.Rooms[i].TrueGroups[j].MultiTexAlpha = CurrentScene.Rooms[i].GroupSettings.MultiTexAlpha[j];
                CurrentScene.Rooms[i].TrueGroups[j].TileS = CurrentScene.Rooms[i].GroupSettings.TileS[j];
                CurrentScene.Rooms[i].TrueGroups[j].TileT = CurrentScene.Rooms[i].GroupSettings.TileT[j];
                CurrentScene.Rooms[i].TrueGroups[j].PolyType = CurrentScene.Rooms[i].GroupSettings.PolyType[j];
                CurrentScene.Rooms[i].TrueGroups[j].BackfaceCulling = CurrentScene.Rooms[i].GroupSettings.BackfaceCulling[j];
                CurrentScene.Rooms[i].TrueGroups[j].Animated = CurrentScene.Rooms[i].GroupSettings.Animated[j];
                CurrentScene.Rooms[i].TrueGroups[j].Metallic = CurrentScene.Rooms[i].GroupSettings.Metallic[j];
                CurrentScene.Rooms[i].TrueGroups[j].EnvColor = CurrentScene.Rooms[i].GroupSettings.EnvColor[j];
                CurrentScene.Rooms[i].TrueGroups[j].Decal = CurrentScene.Rooms[i].GroupSettings.Decal[j];
                CurrentScene.Rooms[i].TrueGroups[j].IgnoreFog = CurrentScene.Rooms[i].GroupSettings.IgnoreFog[j];
                CurrentScene.Rooms[i].TrueGroups[j].SmoothRGBAEdges = CurrentScene.Rooms[i].GroupSettings.SmoothRGBAEdges[j];
                CurrentScene.Rooms[i].TrueGroups[j].Pixelated = CurrentScene.Rooms[i].GroupSettings.Pixelated[j];
                CurrentScene.Rooms[i].TrueGroups[j].Billboard = CurrentScene.Rooms[i].GroupSettings.Billboard[j];
                CurrentScene.Rooms[i].TrueGroups[j].TwoAxisBillboard = CurrentScene.Rooms[i].GroupSettings.TwoAxisBillboard[j];
                CurrentScene.Rooms[i].TrueGroups[j].MultiTexMaterial = CurrentScene.Rooms[i].GroupSettings.MultiTexMaterial[j];
                CurrentScene.Rooms[i].TrueGroups[j].ShiftS = CurrentScene.Rooms[i].GroupSettings.ShiftS[j];
                CurrentScene.Rooms[i].TrueGroups[j].ShiftT = CurrentScene.Rooms[i].GroupSettings.ShiftT[j];
                CurrentScene.Rooms[i].TrueGroups[j].BaseShiftS = CurrentScene.Rooms[i].GroupSettings.BaseShiftS[j];
                CurrentScene.Rooms[i].TrueGroups[j].BaseShiftT = CurrentScene.Rooms[i].GroupSettings.BaseShiftT[j];
                CurrentScene.Rooms[i].TrueGroups[j].ReverseLight = CurrentScene.Rooms[i].GroupSettings.ReverseLight[j];
                CurrentScene.Rooms[i].TrueGroups[j].AnimationBank = CurrentScene.Rooms[i].GroupSettings.AnimationBank[j];
                CurrentScene.Rooms[i].TrueGroups[j].LodGroup = CurrentScene.Rooms[i].GroupSettings.LodGroup[j];
                CurrentScene.Rooms[i].TrueGroups[j].LodDistance = CurrentScene.Rooms[i].GroupSettings.LodDistance[j];
                CurrentScene.Rooms[i].TrueGroups[j].LOD = CurrentScene.Rooms[i].GroupSettings.LOD[j];
                CurrentScene.Rooms[i].TrueGroups[j].RenderLast = CurrentScene.Rooms[i].GroupSettings.RenderLast[j];
                CurrentScene.Rooms[i].TrueGroups[j].VertexNormals = CurrentScene.Rooms[i].GroupSettings.VertexNormals[j];
                CurrentScene.Rooms[i].TrueGroups[j].AlphaMask = CurrentScene.Rooms[i].GroupSettings.AlphaMask[j];
                CurrentScene.Rooms[i].TrueGroups[j].Custom = CurrentScene.Rooms[i].GroupSettings.Custom[j];

            }
        }

        private void saveBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog1.FileName = "Save Here (or target a zzrp or z64rom project)";
            saveFileDialog1.Filter = "All Files (*.*)|*.*";
            saveFileDialog1.InitialDirectory = CurrentScene.BasePath;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CreatePrompt = true;

            int prev = CurrentScene.cloneid;
            if (prev > 0) SetSceneHeader(0);

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AddMissingObjects();
                if (CurrentScene.Cameras.Count == 0)
                {
                    CurrentScene.Cameras.Add(new ZCamera(0, 0, 0, 0, 0, 0, 3, 45, 0xFFFF, 0xFFFF));
                    UpdateForm();
                }
                AutoFixErrors("");

                if (saveFileDialog1.FileName.Contains(".zzrp"))
                    CurrentScene.ConvertSave(Path.GetDirectoryName(saveFileDialog1.FileName) + Path.DirectorySeparatorChar, settings.ConsecutiveRoomInject, settings.ForceRGBATextures, saveFileDialog1.FileName.Contains(".zzrpl") ? 2 : 1);
                else if (saveFileDialog1.FileName.Contains("z64project.toml"))
                    CurrentScene.ConvertSave(Path.GetDirectoryName(saveFileDialog1.FileName) + Path.DirectorySeparatorChar, settings.ConsecutiveRoomInject, settings.ForceRGBATextures, 3);
                else
                    CurrentScene.ConvertSave(Path.GetDirectoryName(saveFileDialog1.FileName) + Path.DirectorySeparatorChar, settings.ConsecutiveRoomInject, settings.ForceRGBATextures, 0);

                string outputmsg = "";
                while (InjectMessages.Count != 0)
                {
                    outputmsg += InjectMessages[0] + "\n";
                    InjectMessages.RemoveAt(0);
                }
                if (outputmsg != "") MessageBox.Show(outputmsg, "Injection", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            if (prev > 0) SetSceneHeader(prev);

            UpdateForm();
        }

        private void showReadmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "ReadMe.txt");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                Program.ApplicationTitle + " - Zelda OoT Scene Development System" + Environment.NewLine + Environment.NewLine +
                "Revived in 2017-2019 by Nokaubure, started in 2011/2012 by xdaniel; see the Readme for more",
                "About" + Environment.NewLine, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Editor - Scene

        private void SceneName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentScene != null)
            {
                CurrentScene.Name = NameTextbox.Text;
                UpdateForm();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
                CurrentScene.Scale = (float)ScaleNumericbox.Value;
        }

        private void AddRoom_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Wavefront .obj / Collada .dae (*.obj;*.dae)|*.obj;*.dae|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (customcombiner != null) customcombiner.Close();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CurrentScene.AddRoom(openFileDialog1.FileName);
                ((CurrencyManager)RoomList.BindingContext[CurrentScene.Rooms]).Refresh();
                CurrentScene.NewRoomMode = false;

                if (settings.MajorasMask)
                {
                    CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].WindSouth = 0;
                    CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].WindStrength = 0;
                    CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].WindVertical = 0;
                    CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].WindWest = 0;
                }

                if (NormalHeader.SceneHeaders.Count > 0) ResetAlternateRooms();

                UpdateForm();
                SelectRoom();

                if (CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].TrueGroups.Count == 0)
                {
                    MessageBox.Show("Room " + (CurrentScene.Rooms.Count - 1) + " has no groups, probably due to bad export settings. If using blender, try installing the SharpOcarina export plugin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }



        private void LoadCollision_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Wavefront .obj / Collada .dae (*.obj;*.dae)|*.obj;*.dae|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                CurrentScene.ColModel = new ObjFile(openFileDialog1.FileName, true);
                CurrentScene.CollisionFilename = openFileDialog1.FileName;

                UpdateForm();

                if (CurrentScene.ColModel.Vertices.Count > 0x1FFF)
                {
                    MessageBox.Show("This collision mesh has more than 8191 vertex! this is going to crash the game, try reducing triangle count.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void numericTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentScene != null)
            {
                CurrentScene.InjectOffset = InjectoffsetTextbox.IntValue;


                UpdateForm();
            }
        }


        #endregion

        #region Editor - Rooms, Objects & Actors

        private void FocusOverObjEd(object sender, EventArgs e)
        {
            ApplyObjectEdit();
        }

        private void EditOverObjEd(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                ApplyObjectEdit();
        }

        private void UpdateObjectEdit()
        {
            if (RoomList.SelectedIndex == -1) { listBox3.DataSource = null; return; }

            listBox3.DataSource = CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects;
            listBox3.DisplayMember = "ValueHex";
            ((CurrencyManager)listBox3.BindingContext[CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects]).Refresh();
        }

        private void ApplyObjectEdit()
        {
            ((ZScene.ZUShort)listBox3.SelectedItem).Value = ushort.Parse(ListEditBox.Text.PadLeft(4, '0'), System.Globalization.NumberStyles.HexNumber);
            ListEditBox.Hide();
            UpdateForm();
            listBox3.Focus();
        }

        private void SelectObject()
        {
            SelectObject(listBox3.Items.Count - 1);
        }

        private void SelectObject(int Index)
        {
            if (Index >= 0)
            {
                listBox3.DataSource = CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects;
                listBox3.SelectedIndex = Index;
            }
            else
                listBox3.DataSource = null;

            listBox3.DisplayMember = "ValueHex";
        }

        private void DeleteRoom_Click(object sender, EventArgs e)
        {
            if (customcombiner != null) customcombiner.Close();

            if (RoomList.SelectedItem != null)
            {

                if (!CurrentScene.NewRoomMode)
                    CurrentScene.Rooms.Remove(CurrentScene.Rooms[RoomList.SelectedIndex]);
                else
                { CurrentScene.Rooms.Clear(); CurrentScene.NewRoomMode = false; }
                ((CurrencyManager)RoomList.BindingContext[CurrentScene.Rooms]).Refresh();

                if (NormalHeader.SceneHeaders.Count > 0) ResetAlternateRooms();
            }

            GroupList.DataSource = null;

            UpdateForm();
            SelectRoom();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectRoom(RoomList.SelectedIndex);
        }

        private void listBox1_ApplyEdit(object sender, EventArgs e)
        {
            CurrentScene.Rooms[RoomList.SelectedIndex].ModelShortFilename = ListStringEditBox.Text;
            ((CurrencyManager)RoomList.BindingContext[CurrentScene.Rooms]).Refresh();
            CloseEditBox();
        }

        private void listBox1_ExitEdit(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.listBox1_ApplyEdit(sender, e);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            CreateEditBox(sender, "room");

            ListStringEditBox.KeyPress += new KeyPressEventHandler(this.listBox1_ExitEdit);
            ListStringEditBox.LostFocus += new EventHandler(this.listBox1_ApplyEdit);
            RoomList.Controls.AddRange(new System.Windows.Forms.Control[] { this.ListStringEditBox });
            this.ListStringEditBox.Focus();
        }

        private void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > GroupList.ItemHeight * GroupList.Items.Count)
                GroupList.SelectedIndex = -1;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customcombiner != null) customcombiner.Close();
            UpdateGroupSelect();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).TintAlpha = (uint)(((byte)numericUpDown2.Value << 24) | pictureBox7.BackColor.ToArgb() & 0xFFFFFF);

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.TintAlpha[Index] = ((ObjFile.Group)GroupList.SelectedItem).TintAlpha;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).TileS = comboBox1.SelectedIndex;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.TileS[Index] = ((ObjFile.Group)GroupList.SelectedItem).TileS;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).TileT = comboBox2.SelectedIndex;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.TileT[Index] = ((ObjFile.Group)GroupList.SelectedItem).TileT;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void pictureBox7_DoubleClick(object sender, EventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            if (ColorPicker(ref PB) == true)
            {
                ((ObjFile.Group)GroupList.SelectedItem).TintAlpha = (uint)(((byte)numericUpDown2.Value << 24) | PB.BackColor.ToArgb() & 0xFFFFFF);

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.TintAlpha[Index] = ((ObjFile.Group)GroupList.SelectedItem).TintAlpha;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).BackfaceCulling = checkBox3.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.BackfaceCulling[Index] = ((ObjFile.Group)GroupList.SelectedItem).BackfaceCulling;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).ShiftS = (int)numericUpDown5.Value;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.ShiftS[Index] = ((ObjFile.Group)GroupList.SelectedItem).ShiftS;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).ShiftT = (int)numericUpDown6.Value;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.ShiftT[Index] = ((ObjFile.Group)GroupList.SelectedItem).ShiftT;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).MultiTexMaterial = MultiTextureComboBox.SelectedIndex - 1;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.MultiTexMaterial[Index] = ((ObjFile.Group)GroupList.SelectedItem).MultiTexMaterial;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void RoomInjectionOffset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentScene != null)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].InjectOffset = RoomInjectionOffset.IntValue;
                UpdateForm();
            }
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            CreateEditBox(sender, "object");

            ListEditBox.KeyPress += new KeyPressEventHandler(this.EditOverObjEd);
            ListEditBox.LostFocus += new EventHandler(this.FocusOverObjEd);
            listBox3.Controls.AddRange(new System.Windows.Forms.Control[] { this.ListEditBox });
            this.ListEditBox.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects.Add(new ZScene.ZUShort(0x0000));
            UpdateForm();
            SelectObject();
            refreshobjdescription();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
                CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects.Remove(((ZScene.ZUShort)listBox3.SelectedItem));

            SelectObject(-1);
            UpdateForm();
        }

        #endregion

        #region Editor - Environments

        private void SetDefaultEnvironments()
        {
            /* Clear existing environments */
            CurrentScene.Environments.Clear();

            /* Now add default environments ... normal environment */
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x46, 0x2D, 0x39), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0xB4, 0x9A, 0x8A), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x14, 0x14, 0x3C),
                Color.FromArgb(0x8C, 0x78, 0x6E), 0x03E1, 0x3200, 1));
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x69, 0x5A, 0x5A), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0xFF, 0xFF, 0xF0), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x32, 0x32, 0x5A),
                Color.FromArgb(0x64, 0x64, 0x78), 0x03E4, 0x3200, 1));
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x78, 0x5A, 0x00), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0xFA, 0x87, 0x32), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x1E, 0x1E, 0x3C),
                Color.FromArgb(0x78, 0x46, 0x32), 0x03E3, 0x3200, 1));
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x28, 0x46, 0x64), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0x14, 0x14, 0x23), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x32, 0x32, 0x64),
                Color.FromArgb(0x00, 0x00, 0x1E), 0x03E0, 0x3200, 1));

            /* ... underwater environment */
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x3C, 0x28, 0x46), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0x50, 0x1E, 0x3C), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x50, 0x32, 0x96),
                Color.FromArgb(0x46, 0x2B, 0x2D), 0x03D2, 0x3200, 0x3F));
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x4B, 0x5A, 0x64), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0x37, 0xFF, 0xF0), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x0A, 0x96, 0xBE),
                Color.FromArgb(0x14, 0x5A, 0x6E), 0x03D2, 0x3200, 0x3F));
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x3C, 0x28, 0x50), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0x3C, 0x4B, 0x96), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x3C, 0x37, 0x96),
                Color.FromArgb(0x32, 0x1E, 0x1E), 0x03D2, 0x3200, 0x3F));
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x00, 0x28, 0x50), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0x14, 0x32, 0x4B), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x32, 0x64, 0x96),
                Color.FromArgb(0x00, 0x0A, 0x14), 0x03D2, 0x3200, 0x3F));

            /* ... rainy environment
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x32, 0x19, 0x25), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0xA0, 0x86, 0x76), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x1E, 0x0A, 0x0A),
                Color.FromArgb(0x28, 0x0F, 0x0F), 0x03DA, 0x3200, 1));
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x5F, 0x50, 0x50), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0x91, 0x91, 0x82), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x28, 0x28, 0x50),
                Color.FromArgb(0x96, 0xA0, 0xAA), 0x03DA, 0x3200, 1));
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x64, 0x46, 0x00), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0x96, 0x46, 0x23), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x0A, 0x0A, 0x19),
                Color.FromArgb(0x14, 0x00, 0x00), 0x03DC, 0x3200, 1));
            CurrentScene.Environments.Add(new ZEnvironment(
                Color.FromArgb(0x14, 0x14, 0x32), Color.FromArgb(0x49, 0x49, 0x49), Color.FromArgb(0x00, 0x00, 0x0F), Color.FromArgb(0xB7, 0xB7, 0xB7), Color.FromArgb(0x1E, 0x1E, 0x50),
                Color.FromArgb(0x00, 0x00, 0x0A), 0x03DC, 0x3200, 1));*/
        }

        private void EnvironmentSelect_ValueChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void AddEnvironmentButton_Click(object sender, EventArgs e)
        {
            CurrentScene.Environments.Add(new ZEnvironment(Color.Red, Color.Green, Color.Blue, Color.Blue, Color.White, Color.Pink, 0x03F0, 0x3200, 0));
            UpdateForm();
            Helpers.SelectAdd(EnvironmentSelect, CurrentScene.Environments);
        }

        private void DeleteEnvironmentButton_Click(object sender, EventArgs e)
        {
            ZEnvironment DelEnv = CurrentScene.Environments[(int)EnvironmentSelect.Value];
            CurrentScene.Environments.Remove(DelEnv);

            EnvironmentSelect.Value = Helpers.Clamp(
                EnvironmentSelect.Value,
                0, CurrentScene.Environments.Count - 1);

            UpdateForm();
        }

        private void UpdateEnvironmentEdit()
        {
            if (CurrentScene.Environments.Count != 0)
            {
                Helpers.SelectClamp(EnvironmentSelect, CurrentScene.Environments);
                EnvironmentSelect.Enabled = true;

                LightingA.BackColor = CurrentScene.Environments[(int)EnvironmentSelect.Value].C1C;
                EnvironmentDirectionAX.Value = CurrentScene.Environments[(int)EnvironmentSelect.Value].C2X;
                EnvironmentDirectionAY.Value = CurrentScene.Environments[(int)EnvironmentSelect.Value].C2Y;
                EnvironmentDirectionAZ.Value = CurrentScene.Environments[(int)EnvironmentSelect.Value].C2Z;
                LightingC.BackColor = CurrentScene.Environments[(int)EnvironmentSelect.Value].C3C;
                EnvironmentDirectionBX.Value = CurrentScene.Environments[(int)EnvironmentSelect.Value].C4X;
                EnvironmentDirectionBY.Value = CurrentScene.Environments[(int)EnvironmentSelect.Value].C4Y;
                EnvironmentDirectionBZ.Value = CurrentScene.Environments[(int)EnvironmentSelect.Value].C4Z;
                LightingE.BackColor = CurrentScene.Environments[(int)EnvironmentSelect.Value].C5C;
                FogColor.BackColor = CurrentScene.Environments[(int)EnvironmentSelect.Value].FogColorC;
                FogDistance.Value = CurrentScene.Environments[(int)EnvironmentSelect.Value].FogDistance;
                FogUnknown.Value = CurrentScene.Environments[(int)EnvironmentSelect.Value].FogUnknown;
                DrawDistance.Value = CurrentScene.Environments[(int)EnvironmentSelect.Value].DrawDistance;

                foreach (Control Ctrl in panel3.Controls)
                    Ctrl.Enabled = true;

                button11.Enabled = true;
            }
            else
            {
                EnvironmentSelect.Minimum = 0;
                EnvironmentSelect.Maximum = 0;
                EnvironmentSelect.Value = EnvironmentSelect.Minimum;
                EnvironmentSelect.Enabled = false;

                LightingA.BackColor = Color.White;
                EnvironmentDirectionAX.Value = 0;
                EnvironmentDirectionAY.Value = 0;
                EnvironmentDirectionAZ.Value = 0;
                LightingC.BackColor = Color.White;
                EnvironmentDirectionBX.Value = 0;
                EnvironmentDirectionBY.Value = 0;
                EnvironmentDirectionBZ.Value = 0;
                LightingE.BackColor = Color.White;
                FogColor.BackColor = Color.White;
                FogDistance.Value = 0;
                FogUnknown.Value = 0;
                DrawDistance.Value = 0;

                foreach (Control Ctrl in panel3.Controls)
                    Ctrl.Enabled = false;

                button11.Enabled = false;
            }
        }

        private void UpdateEnvironmentData()
        {
            CurrentScene.Environments[(int)EnvironmentSelect.Value].C1C = LightingA.BackColor;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].C2X = (byte)EnvironmentDirectionAX.Value;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].C2Y = (byte)EnvironmentDirectionAY.Value;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].C2Z = (byte)EnvironmentDirectionAZ.Value;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].C3C = LightingC.BackColor;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].C4X = (byte)EnvironmentDirectionBX.Value;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].C4Y = (byte)EnvironmentDirectionBY.Value;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].C4Z = (byte)EnvironmentDirectionBZ.Value;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].C5C = LightingE.BackColor;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].FogColorC = FogColor.BackColor;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].FogDistance = (ushort)FogDistance.Value;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].FogUnknown = (ushort)FogUnknown.Value;
            CurrentScene.Environments[(int)EnvironmentSelect.Value].DrawDistance = (ushort)DrawDistance.Value;

            UpdateForm();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            if (ColorPicker(ref PB) == true)
                UpdateEnvironmentData();
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            if (ColorPicker(ref PB) == true)
                UpdateEnvironmentData();
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            if (ColorPicker(ref PB) == true)
                UpdateEnvironmentData();
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            if (ColorPicker(ref PB) == true)
                UpdateEnvironmentData();
        }

        private void pictureBox5_DoubleClick(object sender, EventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            if (ColorPicker(ref PB) == true)
                UpdateEnvironmentData();
        }

        private void pictureBox6_DoubleClick(object sender, EventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            if (ColorPicker(ref PB) == true)
                UpdateEnvironmentData();
        }

        private void numericTextBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateEnvironmentData();
        }

        private void numericTextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateEnvironmentData();
        }

        #endregion

        #region Editor - Waterboxes

        private void WaterboxSelect_ValueChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void AddWaterboxButton_Click(object sender, EventArgs e)
        {
            if (CurrentScene.ColModel == null) return;

            if (Control.ModifierKeys == Keys.Shift)
            {
                //create in front of the camera

                double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
                double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

                Vector3d truepos = GetTrueCameraPosition();

                if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
                {
                    truepos.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }
                else
                {
                    truepos.X += (float)Math.Sin(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Z -= (float)Math.Cos(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Y -= (float)Math.Tan(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }

                truepos.X = Clamp(truepos.X, -32767, 32767);
                truepos.Y = Clamp(truepos.Y, -32767, 32767);
                truepos.Z = Clamp(truepos.Z, -32767, 32767);

                CurrentScene.Waterboxes.Add(new ZWaterbox((short)truepos.X, (short)truepos.Y, (short)truepos.Z, 50f, 50f, 1, 0, 0x3F));

            }
            else
            {

                OpenTK.Vector3d MinCoordinate = new OpenTK.Vector3d(0, 0, 0);
                OpenTK.Vector3d MaxCoordinate = new OpenTK.Vector3d(0, 0, 0);

                foreach (ObjFile.Vertex Vtx in CurrentScene.ColModel.Vertices)
                {
                    /* Minimum... */
                    MinCoordinate.X = Math.Min(MinCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MinCoordinate.Y = Math.Min(MinCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MinCoordinate.Z = Math.Min(MinCoordinate.Z, Vtx.Z * CurrentScene.Scale);

                    /* Maximum... */
                    MaxCoordinate.X = Math.Max(MaxCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MaxCoordinate.Y = Math.Max(MaxCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MaxCoordinate.Z = Math.Max(MaxCoordinate.Z, Vtx.Z * CurrentScene.Scale);
                }

                CurrentScene.Waterboxes.Add(new ZWaterbox(0, -20.0f, 0, 100f, 100f, 1, 0, 0x3F));
                // CurrentScene.Waterboxes.Add(new ZWaterbox((float)MinCoordinate.X, (float)-20.0f, (float)MinCoordinate.Z, (float)(MaxCoordinate.X - MinCoordinate.X), (float)(MaxCoordinate.Z - MinCoordinate.Z), 1, 0, 0x3F));

            }

            UpdateForm();
            Helpers.SelectAdd(WaterboxSelect, CurrentScene.Waterboxes);
        }

        private void DeleteWaterboxButton_Click(object sender, EventArgs e)
        {
            ZWaterbox DelWBox = CurrentScene.Waterboxes[(int)WaterboxSelect.Value];
            CurrentScene.Waterboxes.Remove(DelWBox);
            UpdateForm();
        }

        private void UpdateExits()
        {
            if (!settings.EnableNewExitFormat)
            {
                int prevsel = ExitList.SelectedIndex;
                ExitList.Items.Clear();
                foreach (ZScene.ZUShort point in CurrentScene.ExitList)
                {
                    ExitList.Items.Add(point.ValueHex);
                }
                if (prevsel == -1 && ExitList.Items.Count > 0) prevsel = 0;
                if (prevsel >= ExitList.Items.Count && ExitList.Items.Count > 0) ExitList.SelectedIndex = prevsel - 1;
                else if (prevsel >= ExitList.Items.Count) ExitList.SelectedIndex = -1;
                else ExitList.SelectedIndex = prevsel;

                if (ExitList.Items.Count > 0) DeleteexitButton.Enabled = true;
                else DeleteexitButton.Enabled = false;
            }

            if (settings.EnableNewExitFormat && CurrentScene.ExitListV2.Count != 0)
            {
                int prevsel = ExitList.SelectedIndex;
                if (prevsel == -1) prevsel = 0;
                ExitList.Items.Clear();
                foreach (ZExit point in CurrentScene.ExitListV2)
                {
                    ExitList.Items.Add(point.Raw.ToString("X8"));
                }
                if (prevsel >= ExitList.Items.Count && ExitList.Items.Count > 0) ExitList.SelectedIndex = prevsel - 1;
                else if (prevsel >= ExitList.Items.Count) ExitList.SelectedIndex = -1;
                else ExitList.SelectedIndex = prevsel;

                ExitFadeIn.Value = CurrentScene.ExitListV2[ExitList.SelectedIndex].FadeIn;
                ExitFadeOut.Value = CurrentScene.ExitListV2[ExitList.SelectedIndex].FadeOut;
                ExitSceneIndex.Value = CurrentScene.ExitListV2[ExitList.SelectedIndex].SceneIndex;
                ExitSpawnIndex.Value = CurrentScene.ExitListV2[ExitList.SelectedIndex].SpawnIndex;
                ExitHeaderIndex.Value = CurrentScene.ExitListV2[ExitList.SelectedIndex].HeaderIndex;
                ExitMusicOn.Checked = CurrentScene.ExitListV2[ExitList.SelectedIndex].MusicOn;
                ExitShowTitlecard.Checked = CurrentScene.ExitListV2[ExitList.SelectedIndex].ShowTitleCard;
                DeleteexitButton.Enabled = true;


                foreach (Control Ctrl in ExitGroupBox.Controls)
                {
                    Ctrl.Enabled = true;
                    Ctrl.Visible = true;
                }

                //DeletewaterboxButton.Enabled = true;
            }
            else
            {

                ExitFadeIn.Value = 0;
                ExitFadeOut.Value = 0;
                ExitSceneIndex.Value = 0;
                ExitSpawnIndex.Value = 0;
                ExitHeaderIndex.Value = 0;
                ExitMusicOn.Checked = false;
                ExitShowTitlecard.Checked = false;
                DeleteexitButton.Enabled = false;
                if (settings.EnableNewExitFormat) ExitList.Items.Clear();

                foreach (Control Ctrl in ExitGroupBox.Controls)
                {
                    Ctrl.Enabled = false;
                    Ctrl.Visible = settings.EnableNewExitFormat;
                }

            }
        }

        private void UpdateWaterboxEdit()
        {
            if (CurrentScene.Waterboxes.Count != 0)
            {
                Helpers.SelectClamp(WaterboxSelect, CurrentScene.Waterboxes);
                WaterboxSelect.Enabled = true;

                WaterboxRoom.Text = CurrentScene.Waterboxes[(int)WaterboxSelect.Value].Room.ToString("X2");
                WaterboxEnv.Value = (decimal)CurrentScene.Waterboxes[(int)WaterboxSelect.Value].Env;
                WaterboxCam.Value = (decimal)CurrentScene.Waterboxes[(int)WaterboxSelect.Value].Camera;
                WaterboxXPos.Value = (decimal)CurrentScene.Waterboxes[(int)WaterboxSelect.Value].XPos;
                WaterboxYPos.Value = (decimal)CurrentScene.Waterboxes[(int)WaterboxSelect.Value].YPos;
                WaterboxZPos.Value = (decimal)CurrentScene.Waterboxes[(int)WaterboxSelect.Value].ZPos;
                WaterboxXSize.Value = (decimal)CurrentScene.Waterboxes[(int)WaterboxSelect.Value].XSize;
                WaterboxYSize.Value = (decimal)CurrentScene.Waterboxes[(int)WaterboxSelect.Value].ZSize;

                foreach (Control Ctrl in panel1.Controls)
                    Ctrl.Enabled = true;

                DeletewaterboxButton.Enabled = true;
            }
            else
            {
                WaterboxSelect.Minimum = 0;
                WaterboxSelect.Maximum = 0;
                WaterboxSelect.Value = 0;
                WaterboxSelect.Enabled = false;

                WaterboxRoom.Text = string.Empty;
                WaterboxXPos.Value = 0;
                WaterboxYPos.Value = 0;
                WaterboxZPos.Value = 0;
                WaterboxXSize.Value = 0;
                WaterboxYSize.Value = 0;
                WaterboxCam.Value = 0;
                WaterboxEnv.Value = 0;

                foreach (Control Ctrl in panel1.Controls)
                    Ctrl.Enabled = false;

                DeletewaterboxButton.Enabled = false;
            }
        }

        private void UpdateWaterboxData()
        {
            CurrentScene.Waterboxes[(int)WaterboxSelect.Value].Env = (byte)WaterboxEnv.Value;
            CurrentScene.Waterboxes[(int)WaterboxSelect.Value].Camera = (byte)WaterboxCam.Value;
            CurrentScene.Waterboxes[(int)WaterboxSelect.Value].Room = byte.Parse(WaterboxRoom.Text, System.Globalization.NumberStyles.HexNumber);
            CurrentScene.Waterboxes[(int)WaterboxSelect.Value].XPos = (float)WaterboxXPos.Value;
            CurrentScene.Waterboxes[(int)WaterboxSelect.Value].YPos = (float)WaterboxYPos.Value;
            CurrentScene.Waterboxes[(int)WaterboxSelect.Value].ZPos = (float)WaterboxZPos.Value;
            CurrentScene.Waterboxes[(int)WaterboxSelect.Value].XSize = (float)WaterboxXSize.Value;
            CurrentScene.Waterboxes[(int)WaterboxSelect.Value].ZSize = (float)WaterboxYSize.Value;
            UpdateForm();
        }



        private void WaterboxRoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (WaterboxRoom.IntValue > CurrentScene.Rooms.Count && WaterboxRoom.IntValue != 0x3F) WaterboxRoom.Text = (CurrentScene.Rooms.Count - 1).ToString("X2");
                UpdateWaterboxData();
            }
        }

        private void WaterboxTransform_ChangeValue(object sender, EventArgs e)
        {
            UpdateWaterboxData();
        }

        private void WaterboxSizeX_ChangeValue(object sender, EventArgs e)
        {
            int negative = (WaterboxXSize.Value < (decimal)CurrentScene.Waterboxes[(int)WaterboxSelect.Value].XSize) ? 1 : -1;
            WaterboxXPos.Value += ((Control.ModifierKeys == Keys.Shift) ? 10 : 1) * (negative);
            UpdateWaterboxData();
        }

        private void WaterboxSizeY_ChangeValue(object sender, EventArgs e)
        {
            int negative = (WaterboxYSize.Value < (decimal)CurrentScene.Waterboxes[(int)WaterboxSelect.Value].ZSize) ? 1 : -1;
            WaterboxZPos.Value += ((Control.ModifierKeys == Keys.Shift) ? 10 : 1) * (negative); ;
            UpdateWaterboxData();
        }

        #endregion

        #region Editor - Collision

        /* Special FX flags
         * 400 -> climbable ladder
         * 800 -> whole surface climbable
         * 008 -> quicksand (shallow)
         * 018 -> quicksand (deep, kills)
         * 004 -> lava damage
         */
        private void UpdatePolyTypeEdit()
        {
            if (CurrentScene.PolyTypes.Count != 0)
            {
                PolygonSelect.Minimum = 1;
                PolygonSelect.Maximum = CurrentScene.PolyTypes.Count;
                PolygonSelect.Enabled = true;

                if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ExitNumber >= CurrentScene.ExitList.Count)
                    CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ExitNumber = CurrentScene.ExitList.Count;
                ExitNumber.Value = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ExitNumber;
                //  ExitNumber.Maximum = 0xFF;

                if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].EnvNumber >= CurrentScene.Environments.Count)
                    CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].EnvNumber = CurrentScene.Environments.Count;
                EnvironmentType.Value = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].EnvNumber;
                // EnvironmentType.Maximum = 0xF;

                CameraAngleNumeric.Value = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].CameraAngle;

                EchoRange.Value = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].EchoRange;
                GroundType.Value = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].GroundType;
                TerrainType.Value = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].TerrainType;
                SteepterrainCheckbox.Checked = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].IsSteep;
                HookshotableCheckbox.Checked = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].IsHookshotable;
                PolytypeUnk1.Value = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].Unk1;
                PolytypeUnk2.Value = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].Unk2;
                WallDamageCheck.Checked = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].IsWallDamage;
                Lower1UnitChecbox.Checked = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].Lower1Unit;
                BlockEponaCheckBox.Checked = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].EponaBlock;

                //0000000008000000

                if ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags & 0x30) == 0x30)
                    VoidCheckBox.Checked = true;
                else if ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags & 0x24) == 0x24 && (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags & 0x2C) != 0x2C)
                    NoLedgeJumpRadio.Checked = true;
                else if ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags & 0x14) == 0x14)
                    SmallVoidRadioButton.Checked = true;
                else if ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags & 0x2C) == 0x2C)
                    DiveRadioButton.Checked = true;
                else if ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags & 0x18) == 0x18)
                    AutograbClimbRadioButton.Checked = true;
                else
                {
                    NoMiscRadioButton.Checked = true;

                }




                nocheckevent = true;
                GroupDetectionA2.Checked = ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagA & 0x2000) != 0);
                GroupDetectionA4.Checked = ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagA & 0x4000) != 0);
                GroupDetectionA8.Checked = ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagA & 0x8000) != 0);

                GroupDetectionB2.Checked = ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagB & 0x2000) != 0);
                GroupDetectionB4.Checked = ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagB & 0x4000) != 0);
                GroupDetectionB8.Checked = ((CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagB & 0x8000) != 0);
                nocheckevent = false;

                if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags == 0x0)
                    radioButton1.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags == 0x4)
                    radioButton2.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags == 0x8)
                    radioButton3.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags == 0x6)
                    LadderTopRadioButton.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags == 0xA)
                    CrawlSpaceRadio.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags == 0x2)
                    NoLedgeClimbRadio.Checked = true;
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    LadderTopRadioButton.Checked = false;
                    CrawlSpaceRadio.Checked = false;
                    NoLedgeClimbRadio.Checked = false;
                }

                if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags == 0x00)
                    radioButton7.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags == 0x08)
                    radioButton4.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags == 0x18)
                    radioButton5.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags == 0x04)
                    radioButton6.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags == 0x12)
                    KillingLavaRadioButton.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags == 0x0C)
                    NoFallDamageRadio.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags == 0x10)
                    JabuJabuRadio.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags == 0x0E)
                    KillingQuicksand2Radio.Checked = true;
                else if (CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags == 0x0A)
                    IceRadioButton.Checked = true;
                else
                {
                    radioButton7.Checked = false;
                    radioButton4.Checked = false;
                    radioButton5.Checked = false;
                    radioButton6.Checked = false;
                    KillingLavaRadioButton.Checked = false;
                    NoFallDamageRadio.Checked = false;
                    JabuJabuRadio.Checked = false;
                    KillingQuicksand2Radio.Checked = false;
                    IceRadioButton.Checked = false;
                }

                PolygonRawdata.Text = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].Raw.ToString("X16");

                foreach (Control Ctrl in panel2.Controls)
                    Ctrl.Enabled = true;

                DeletepolygonButton.Enabled = (CurrentScene.PolyTypes.Count > 1);

            }
            else
            {
                PolygonSelect.Minimum = 0;
                PolygonSelect.Maximum = 0;
                PolygonSelect.Value = 0;
                PolygonSelect.Enabled = false;

                ExitNumber.Value = 0;
                EnvironmentType.Value = 0;
                EchoRange.Value = 0;
                GroundType.Value = 0;
                TerrainType.Value = 0;
                CameraAngleNumeric.Value = 0;
                SteepterrainCheckbox.Checked = false;
                HookshotableCheckbox.Checked = false;

                NoMiscRadioButton.Checked = true;

                nocheckevent = true;
                GroupDetectionA2.Checked = false;
                GroupDetectionA4.Checked = false;
                GroupDetectionA8.Checked = false;

                GroupDetectionB2.Checked = false;
                GroupDetectionB4.Checked = false;
                GroupDetectionB8.Checked = false;
                nocheckevent = false;

                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                LadderTopRadioButton.Checked = false;
                CrawlSpaceRadio.Checked = false;
                NoLedgeClimbRadio.Checked = false;

                radioButton7.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                KillingLavaRadioButton.Checked = false;
                NoFallDamageRadio.Checked = false;
                JabuJabuRadio.Checked = false;
                KillingQuicksand2Radio.Checked = false;
                IceRadioButton.Checked = false;

                WallDamageCheck.Checked = false;

                PolygonRawdata.Text = string.Empty;

                foreach (Control Ctrl in panel2.Controls)
                    Ctrl.Enabled = false;

                DeletepolygonButton.Enabled = false;
            }
        }

        private void UpdateClimbableCrawlableFlags()
        {
            if (radioButton1.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags = 0x0;
            else if (radioButton2.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags = 0x4;
            else if (radioButton3.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags = 0x8;
            else if (LadderTopRadioButton.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags = 0x6;
            else if (CrawlSpaceRadio.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags = 0xA;
            else if (NoLedgeClimbRadio.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ClimbingCrawlingFlags = 0x2;
        }

        private void UpdateDamageSurfaceFlags()
        {
            if (radioButton7.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags = 0x00;
            else if (radioButton4.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags = 0x08;
            else if (radioButton5.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags = 0x18;
            else if (radioButton6.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags = 0x04;
            else if (KillingLavaRadioButton.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags = 0x12;
            else if (NoFallDamageRadio.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags = 0x0C;
            else if (JabuJabuRadio.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags = 0x10;
            else if (KillingQuicksand2Radio.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags = 0x0E;
            else if (IceRadioButton.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].DamageSurfaceFlags = 0x0A;
        }

        private void UpdateFirstByteFlags()
        {
            if (VoidCheckBox.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags = 0x30;
            else if (NoLedgeJumpRadio.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags = 0x24;
            else if (SmallVoidRadioButton.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags = 0x14;
            else if (DiveRadioButton.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags = 0x2C;
            else if (AutograbClimbRadioButton.Checked == true)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags = 0x18;
            else
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags = 0x00;

            if (BlockEponaCheckBox.Checked)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags += 0x80;
            if (Lower1UnitChecbox.Checked)
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].FirstByteFlags += 0x40;

        }

        private void PolygonRawData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].Raw = ulong.Parse(PolygonRawdata.Text, System.Globalization.NumberStyles.HexNumber);
                UpdateForm();
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes.Add(new ZColPolyType());
            UpdateForm();
            PolygonSelect.Value = PolygonSelect.Maximum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZColPolyType DelPT = CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1];
            CurrentScene.PolyTypes.Remove(DelPT);
            UpdateForm();
        }

        private void SetPolyTypesInCollision()
        {
            /* (Fuck Xdaniel haha) See this? Pure lazyness. Collision poly types are stored in the -room's- model data, while later on they're read from the -scene's- collision model.
             * So what do I do here? For each group in each room, I go through the collision model's groups and see if their names match up. If they do, I copy over
             * the poly type from the room to the collision model. I -could've- done this differently from the start, but a brainfart hindered me... *cough*
             */
            foreach (ZScene.ZRoom Room in CurrentScene.Rooms)
            {
                foreach (ObjFile.Group RoomGrp in Room.TrueGroups)
                {
                    foreach (ObjFile.Group SceneGrp in CurrentScene.ColModel.Groups)
                    {
                        if (RoomGrp.Name == SceneGrp.Name)
                            SceneGrp.PolyType = RoomGrp.PolyType;
                    }
                }
            }


        }

        private void GroupPolygonType_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null && CurrentScene.ColModel != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).PolyType = ((int)GroupPolygonType.Value - 1);

                foreach (ObjFile.Group Grp in CurrentScene.ColModel.Groups)
                {
                    if (Grp.Name == ((ObjFile.Group)GroupList.SelectedItem).Name)
                        Grp.PolyType = ((int)GroupPolygonType.Value - 1);
                }

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);

                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.PolyType[Index] = ((ObjFile.Group)GroupList.SelectedItem).PolyType;

                UpdateGroupSelect();
            }
        }

        private void numericUpDownEx10_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].ExitNumber = (int)ExitNumber.Value;
            UpdateForm();
        }

        private void numericUpDownEx3_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].EchoRange = (int)EchoRange.Value;
            UpdateForm();
        }

        private void numericUpDownEx7_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].EnvNumber = (int)EnvironmentType.Value;
            UpdateForm();
        }

        private void numericUpDownEx9_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].GroundType = (int)GroundType.Value;
            UpdateForm();
        }

        private void numericUpDownEx8_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].TerrainType = (int)TerrainType.Value;
            UpdateForm();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].IsSteep = SteepterrainCheckbox.Checked;
            UpdateForm();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].IsHookshotable = HookshotableCheckbox.Checked;
            UpdateForm();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateClimbableCrawlableFlags();
            UpdateForm();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateClimbableCrawlableFlags();
            //CameraAngleNumeric.Text = "0B";
            UpdateForm();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateClimbableCrawlableFlags();
            UpdateForm();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDamageSurfaceFlags();
            UpdateForm();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDamageSurfaceFlags();
            UpdateForm();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDamageSurfaceFlags();
            UpdateForm();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDamageSurfaceFlags();
            UpdateForm();
        }

        #endregion

        #region Editor - Exit List

        private void listBox4_DoubleClick(object sender, EventArgs e)
        {
            if (settings.EnableNewExitFormat)
                return;

            CreateEditBox(sender, "exit");

            ListEditBox.KeyPress += new KeyPressEventHandler(this.EditOverExitEd);
            ListEditBox.LostFocus += new EventHandler(this.FocusOverExitEd);
            ExitList.Controls.AddRange(new System.Windows.Forms.Control[] { this.ListEditBox });
            this.ListEditBox.Focus();
        }

        private void AddExit_Click(object sender, EventArgs e)
        {
            if (!settings.EnableNewExitFormat)
                CurrentScene.ExitList.Add(new ZScene.ZUShort(0x0000));
            else
                CurrentScene.ExitListV2.Add(new ZExit());
            UpdateForm();
            SelectExit();
            UpdateExitEdit();
        }

        private void DeleteExit_Click(object sender, EventArgs e)
        {
            if (ExitList.Items.Count != 0)
            {
                if (!settings.EnableNewExitFormat)
                    CurrentScene.ExitList.RemoveAt(ExitList.SelectedIndex);
                else
                    CurrentScene.ExitListV2.RemoveAt(ExitList.SelectedIndex);

            }

            SelectExit(-1);
            UpdateForm();
        }

        private void FocusOverExitEd(object sender, EventArgs e)
        {
            ApplyExitEdit();
        }

        private void EditOverExitEd(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                ApplyExitEdit();
        }

        private void UpdateExitEdit()
        {
            //  ExitList.DataSource = CurrentScene.ExitList;
            //   ExitList.DisplayMember = "ValueHex";
            //  ((CurrencyManager)ExitList.BindingContext[CurrentScene.ExitList]).Refresh();
        }

        private void ApplyExitEdit()
        {
            CurrentScene.ExitList[ExitList.SelectedIndex].Value = ushort.Parse(ListEditBox.Text.PadLeft(4, '0'), System.Globalization.NumberStyles.HexNumber);
            ListEditBox.Hide();
            UpdateForm();
            ExitList.Focus();

        }

        private void SelectExit()
        {
            SelectExit(ExitList.Items.Count - 1);
        }

        private void SelectExit(int Index)
        {

            ExitList.SelectedIndex = Index;

        }

        #endregion

        private void UpdateCameraEdit()
        {
            if (CurrentScene.Cameras.Count != 0)
            {
                Helpers.SelectClamp(CameraSelect, CurrentScene.Cameras);
                CameraSelect.Enabled = true;

                CameraXPos.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].XPos;
                CameraYPos.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].YPos;
                CameraZPos.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].ZPos;
                CameraXRot.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].XRot;
                CameraYRot.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].YRot;
                CameraZRot.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].ZRot;
                CameraFov.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Fov;
                CameraUnk1.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk1;
                CameraUnk2.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk2;
                CameraType.SelectedIndex = FindSongComboItemValue(CameraType.Items, CurrentScene.Cameras[(int)CameraSelect.Value].Type);

                foreach (Control Ctrl in CameraPanel.Controls)
                    Ctrl.Enabled = true;

                foreach (Control Ctrl in CameraPanel2.Controls)
                    Ctrl.Enabled = true;

                DeleteCameraButton.Enabled = true;

                AddCameraButton.Enabled = (CurrentScene.Cameras.Count < 0x0F);

                CameraCopyViewport.Enabled = true;
                CameraView.Enabled = true;

                if (CurrentScene.Cameras[(int)CameraSelect.Value].Type != 0x1E)
                {
                    CameraPanel.Visible = true;
                    CameraPanel2.Visible = false;
                    CameraPage2.Visible = false;
                }
                else
                {
                    CameraPage2.Visible = true;
                    CameraPage1.Visible = true;

                    CameraUnk12.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk12;
                    CameraUnk14.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk14;
                    CameraUnk16.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk16;
                    CameraUnk18.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk18;
                    CameraUnk1A.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk1A;
                    CameraUnk1C.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk1C;
                    CameraUnk1E.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk1E;
                    CameraUnk20.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk20;
                    CameraUnk22.Value = (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk22;
                }
            }
            else
            {
                CameraSelect.Minimum = 0;
                CameraSelect.Maximum = 0;
                CameraSelect.Value = 0;
                CameraSelect.Enabled = false;

                CameraXRot.Value = 0;
                CameraYRot.Value = 0;
                CameraZRot.Value = 0;
                CameraFov.Value = 0;
                CameraUnk1.Value = 0;
                CameraUnk2.Value = 0;
                CameraType.SelectedIndex = 0;

                CameraPanel.Visible = true;
                CameraPanel2.Visible = false;

                foreach (Control Ctrl in CameraPanel.Controls)
                    Ctrl.Enabled = false;

                DeleteCameraButton.Enabled = false;

                CameraCopyViewport.Enabled = false;
                CameraView.Enabled = false;

                previewscenecamera = false;
            }

            CameraView.BackColor = (previewscenecamera) ? Color.LawnGreen : Color.LightGray;
        }

        private void UpdateTextureAnimEdit()
        {
            if (CurrentScene.TextureAnims.Count != 0)
            {
                TextureAnimSelect.Minimum = 8;
                TextureAnimSelect.Maximum = 7 + CurrentScene.TextureAnims.Count;
                TextureAnimSelect.Enabled = true;

                TextureAnimXVelocity1.Value = (decimal)CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].XVelocity1;
                TextureAnimXVelocity2.Value = (decimal)CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].XVelocity2;
                TextureAnimYVelocity1.Value = (decimal)CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].YVelocity1;
                TextureAnimYVelocity2.Value = (decimal)CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].YVelocity2;

                TextureAnimWidth1.Value = (decimal)CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].Width1;
                TextureAnimWidth2.Value = (decimal)CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].Width2;
                TextureAnimHeight1.Value = (decimal)CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].Height1;
                TextureAnimHeight2.Value = (decimal)CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].Height2;


                foreach (Control Ctrl in TextureAnimsGroupBox.Controls)
                    Ctrl.Enabled = true;

                AddTextureAnim.Enabled = (CurrentScene.TextureAnims.Count < 8);
            }
            else
            {
                TextureAnimSelect.Minimum = 0;
                TextureAnimSelect.Maximum = 0;
                TextureAnimSelect.Value = 0;
                TextureAnimSelect.Enabled = false;

                TextureAnimWidth1.Value = 0;
                TextureAnimWidth2.Value = 0;
                TextureAnimHeight1.Value = 0;
                TextureAnimHeight2.Value = 0;

                TextureAnimXVelocity1.Value = 0;
                TextureAnimXVelocity2.Value = 0;
                TextureAnimYVelocity1.Value = 0;
                TextureAnimYVelocity2.Value = 0;

                foreach (Control Ctrl in TextureAnimsGroupBox.Controls)
                    Ctrl.Enabled = false;

                AddTextureAnim.Enabled = true;


            }
        }

        private void UpdateAlternateSceneHeaders()
        {
            string[] usedin = { "Child Link (night)", "Adult Link (day)", "Adult Link (night)", "Cutscenes" };

            prevsceneheader = (int)SceneHeaderSelector.Value;

            if (NormalHeader.SceneHeaders.Count != 0)
            {
                SceneHeaderList.Minimum = 1;
                SceneHeaderList.Maximum = NormalHeader.SceneHeaders.Count;
                SceneHeaderList.Enabled = true;

                if (SceneHeaderList.Value > 0)
                {
                    SceneHeaderCopyList.Minimum = 0;
                    SceneHeaderCopyList.Maximum = NormalHeader.SceneHeaders.Count - 1;
                    SceneHeaderCopyList.Enabled = true;

                    SceneHeaderSameCheckbox.Checked = NormalHeader.SceneHeaders[(int)SceneHeaderList.Value - 1].SameAsPrevious;
                    SceneHeaderSameCheckbox.Enabled = true;

                    if (SceneHeaderCopyList.Value > SceneHeaderList.Maximum)
                    {
                        SceneHeaderCopyList.Value = 1;
                    }
                    if (SceneHeaderCopyList.Value == SceneHeaderList.Maximum)
                    {
                        NormalHeader.SceneHeaders[(int)SceneHeaderList.Value - 1].CloneFromHeader = 0;
                        NormalHeader.SceneHeaders[(int)SceneHeaderList.Value - 1].SameAsPrevious = false;
                        SceneHeaderCopyList.Value = 0;
                        SceneHeaderSameCheckbox.Checked = false;
                    }

                    SceneHeaderCopyList.Value = NormalHeader.SceneHeaders[(int)SceneHeaderList.Value - 1].CloneFromHeader;
                    SceneHeaderCopyList.Enabled = SceneHeaderSameCheckbox.Checked;

                }
                else
                {
                    SceneHeaderSameCheckbox.Enabled = false;
                    SceneHeaderCopyList.Value = 0;

                    SceneHeaderSameCheckbox.Checked = false;
                    SceneHeaderCopyList.Enabled = false;

                }

                SceneHeaderSelector.Maximum = NormalHeader.SceneHeaders.Count;

                DeleteSceneHeaderButton.Enabled = true;

                SceneHeaderUsedLabel.Text = "Used in: " + usedin[Clamp((int)SceneHeaderList.Value - 1, 0, 3)];
            }
            else
            {
                SceneHeaderList.Minimum = 0;
                SceneHeaderList.Maximum = 0;
                SceneHeaderList.Value = 0;
                SceneHeaderList.Enabled = false;
                SceneHeaderSelector.Maximum = 0;
                if (SceneHeaderSelector.Value != 0)
                {
                    SceneHeaderSelector.Value = 0;
                    //update sceneheaderselect
                }

                SceneHeaderCopyList.Enabled = false;

                SceneHeaderSameCheckbox.Enabled = false;

                DeleteSceneHeaderButton.Enabled = false;

                SceneHeaderUsedLabel.Text = "";
            }
        }

        private void UpdatePrerenders()
        {


            if (CurrentScene.prerenderimages.Count != 0)
            {
                DeleteJFIF.Enabled = true;

                PrerenderedList.Minimum = 1;
                PrerenderedList.Maximum = CurrentScene.prerenderimages.Count;
                PrerenderedList.Enabled = true;


                JFIFLabel.Text = "Used in: " + CurrentScene.prerenderimages[(int)(PrerenderedList.Value - 1)];

                CurrentScene.Prerendered = true;
            }
            else
            {
                PrerenderedList.Minimum = 0;
                PrerenderedList.Maximum = 0;
                PrerenderedList.Value = 0;
                PrerenderedList.Enabled = false;
                PrerenderedList.Maximum = 0;
                if (PrerenderedList.Value != 0)
                {
                    PrerenderedList.Value = 0;

                }


                JFIFLabel.Text = "";

                CurrentScene.Prerendered = false;
            }
        }

        private void UpdateAdditionalTextures()
        {
            AddAdditionalTexture.Enabled = (CurrentScene.Rooms.Count > 0);
            if (CurrentScene.AdditionalTextures.Count != 0)
            {
                AdditionalTextureList.Minimum = 1;
                AdditionalTextureList.Maximum = CurrentScene.AdditionalTextures.Count;
                AdditionalTextureList.Enabled = true;


                DeleteAdditionalTexture.Enabled = true;

                AdditionalTextureLabel.Text = CurrentScene.AdditionalTextures[(int)AdditionalTextureList.Value - 1].Name;
            }
            else
            {
                AdditionalTextureList.Minimum = 0;
                AdditionalTextureList.Maximum = 0;
                AdditionalTextureList.Value = 0;
                AdditionalTextureList.Enabled = false;

                DeleteAdditionalTexture.Enabled = false;

                AdditionalTextureLabel.Text = "";
            }
        }

        private void SongOnChange(object sender, EventArgs e)
        {
            CurrentScene.Music = Convert.ToByte((SongComboBox.SelectedItem as SongItem).Value);
            UpdateForm();
        }

        private void DegreesMenuItemClick(object sender, EventArgs e)
        {
            settings.Degrees = DegreesMenuItem.Checked;
        }

        private void listBox3_Click(object sender, EventArgs e)
        {
            refreshobjdescription();
        }

        private void refreshobjdescription()
        {
            ObjectDescription.Text = "";
            if (listBox3.SelectedIndex > -1 && listBox3.SelectedIndex < CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects.Count)
            {
                if (ObjectCache.ContainsKey(CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects[listBox3.SelectedIndex].Value))
                {
                    ObjectDescription.Text += "Internal name: " + ObjectCache[CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects[listBox3.SelectedIndex].Value].name + Environment.NewLine;
                    ObjectDescription.Text += String.Format("Size: {0:X}", ObjectCache[CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects[listBox3.SelectedIndex].Value].size) + Environment.NewLine;
                    ObjectDescription.Text += "Actors that use this object: ";
                    ObjectDescription.Text += ObjectCache[CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects[listBox3.SelectedIndex].Value].usedby;
                }
                else
                {
                    String[] data = XMLreader.getObjectSize((CurrentScene.Rooms[RoomList.SelectedIndex]).ZObjects[listBox3.SelectedIndex].ValueHex);
                    ObjectDescription.Text += "Internal name: " + data[1] + Environment.NewLine;
                    ObjectDescription.Text += String.Format("Size: {0:X}", data[0]) + Environment.NewLine;
                    ObjectDescription.Text += "Actors that use this object: ";
                    ObjectDescription.Text += XMLreader.getActorNamesByObject(CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects[listBox3.SelectedIndex].ValueHex);
                }
            }
        }

        private void AutoaddGroupsClick(object sender, EventArgs e)
        {
            settings.AutoaddObjects = AutoaddGroupsMenuItem.Checked;
        }

        private void EnemyTest(object sender, EventArgs e)
        {
            if (CurrentScene == null || CurrentScene.Rooms.Count <= 0 || CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices.Count < 1) return;

            StoreUndo(_Actor_);

            ObjFile.Vertex v = CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices[0];
            Vector3d MinCoordinate = new Vector3d(v.X, v.Y, v.Z);
            Vector3d MaxCoordinate = new Vector3d(v.X, v.Y, v.Z);
            foreach (ObjFile.Vertex Vtx in CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices)
            {
                /* Minimum... */
                MinCoordinate.X = Math.Min(MinCoordinate.X, Vtx.X * CurrentScene.Scale);
                MinCoordinate.Y = Math.Min(MinCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                MinCoordinate.Z = Math.Min(MinCoordinate.Z, Vtx.Z * CurrentScene.Scale);

                /* Maximum... */
                MaxCoordinate.X = Math.Max(MaxCoordinate.X, Vtx.X * CurrentScene.Scale);
                MaxCoordinate.Y = Math.Max(MaxCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                MaxCoordinate.Z = Math.Max(MaxCoordinate.Z, Vtx.Z * CurrentScene.Scale);
            }
            Random r = new Random(DateTime.Now.Millisecond);
            ZActor[] actors;
            Console.WriteLine(((ToolStripMenuItem)sender).Name);
            if (((ToolStripMenuItem)sender).Name.Contains("Enemy"))
            {
                actors = new[]
                {
                    new ZActor() {Number = 0x0025, Variable = 0x00FF},
                    new ZActor() {Number = 0x001B, Variable = 0xFFFE},
                    new ZActor() {Number = 0x0013, Variable = 0x0004},
                    new ZActor() {Number = 0x0012, Variable = 0xFFFF},
                    new ZActor() {Number = 0x0002, Variable = 0x0002},
                    new ZActor() {Number = 0x0055, Variable = 0x0001},
                    new ZActor() {Number = 0x0060, Variable = 0x004A},
                    new ZActor() {Number = 0x0090, Variable = 0x0000},
                    new ZActor() {Number = 0x0113, Variable = 0xFF02},
                    new ZActor() {Number = 0x000A, Variable = 0x1AC0},

                };
            }
            else if (((ToolStripMenuItem)sender).Name.Contains("puzzle"))
            {
                actors = new[] {
                    new ZActor() {Number = 0x0117, Variable = 0x014C},
                    new ZActor() {Number = 0x0117, Variable = 0x1FCC},
                    new ZActor() {Number = 0x0117, Variable = 0x1FCC},
                    new ZActor() {Number = 0x0117, Variable = 0x1FCC},
                    new ZActor() {Number = 0x0117, Variable = 0x1FCC},
                    new ZActor() {Number = 0x0117, Variable = 0x1FCC},
                    new ZActor() {Number = 0x000A, Variable = 0xBAC1, ZRot = 0x0C},

                };
            }
            else
            {
                List<ZActor> actorlist = new List<ZActor>();
                for (int i = 0; i < 0x1F; i++)
                {
                    actorlist.Add(new ZActor() { Number = (ushort)((!settings.MajorasMask) ? 0x0127 : 0x0092), Variable = (ushort)i });
                }
                actors = actorlist.ToArray();
            }
            int newactors = 0, RNGtries = 0;
            Vector3 rand;
            while (newactors < actors.Length && RNGtries < 10000)
            {
                RNGtries++;
                float dist = 999999;
                Vector3 truepos = new Vector3(-1, -1, -1);
                rand.X = (float)(r.NextDouble() * (MaxCoordinate.X - MinCoordinate.X) + MinCoordinate.X);
                rand.Y = (float)(r.NextDouble() * (MaxCoordinate.Y - MinCoordinate.Y) + MinCoordinate.Y);
                rand.Z = (float)(r.NextDouble() * (MaxCoordinate.Z - MinCoordinate.Z) + MinCoordinate.Z);
                // if (RNGtries%100 == 0)Console.WriteLine(rand.ToString());
                foreach (ObjFile.Group Group in CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups)
                {
                    foreach (ObjFile.Triangle Tri in Group.Triangles)
                    {

                        Vector3 collision = ObjFile.RayCollision(
                            CurrentScene.ColModel.Vertices[Tri.VertIndex[0]],
                            CurrentScene.ColModel.Vertices[Tri.VertIndex[1]],
                            CurrentScene.ColModel.Vertices[Tri.VertIndex[2]],
                            rand,
                            new Vector3(rand.X, rand.Y - 30000, rand.Z),
                            CurrentScene.Scale);

                        if (collision != new Vector3(-1, -1, -1) && dist > Distance3D(rand, collision))
                        {
                            dist = Distance3D(rand, collision);
                            truepos = collision;
                        }

                    }
                }

                if (truepos != new Vector3(-1, -1, -1))
                {
                    //  CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.Add(new ZActor(0x00, 0x00, 0x00, 0x00, 0x0008, truepos.X, truepos.Y, truepos.Z, 0.0f, 0x0000));
                    actors[newactors].XPos = truepos.X;
                    actors[newactors].YPos = truepos.Y;
                    actors[newactors].ZPos = truepos.Z;
                    actors[newactors].YRot = (float)(r.NextDouble() * (32000 - -32000) + -32000);
                    CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.Add(actors[newactors]);
                    //   Console.WriteLine("Added actor!");
                    newactors++;
                }
            }
            //Console.WriteLine("Actors added " + newactors);
            //  Console.WriteLine("RNGtries " + RNGtries);

            if (!settings.DisableEasterEgg && EasterEggPhase == 1 && ((ToolStripMenuItem)sender).Text.Contains("Wtf"))
            {
                EasterEggPhase++;
                EasterEggToolStripMenuItem.Text = "Bombiwas";

                UpdateForm();
                actorEditControl1.UpdateActorEdit();
                actorEditControl1.UpdateForm();

                settings.RenderActors = true;
                if (!SimulateN64CheckBox.Checked)
                {
                    Console.WriteLine("checking");
                    SimulateN64CheckBox.Checked = true;
                    SimulateN64CheckBox.BackColor = Color.LightGreen;
                    SimulateN64Gfx = true;
                    Program.ApplicationTitle = "SharpBombiwa";
                    CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
                }

                MessageBox.Show("You found the bombiwa easter egg! let me tell you a story...", "Bombiwa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("Once upon a time... there was a guy in a discord group who wanted to fill all OoT vanilla scenes with bombable rocks, which are called Bombiwa by the game code. I'm too lazy to explain the rest of the story, instead I will copy paste it from an external source. ", "Bombiwa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                string text = "Number 15: Burger king foot lettuce. The last thing you\'d want in your Burger King burger is someone\'s foot fungus. But as it turns out, that might be what you get. A 4channer uploaded a photo anonymously to the site showcasing his feet in a plastic bin of lettuce. With the statement: \"This is the lettuce you eat at Burger King.\" Admittedly, he had shoes on. But that\'s even worse.\r\n\r\nThe post went live at 11:38 PM on July 16, and a mere 20 minutes later, the Burger King in question was alerted to the rogue employee. At least, I hope he\'s rogue. How did it happen? Well, the BK employee hadn\'t removed the Exif data from the uploaded photo, which suggested the culprit was somewhere in Mayfleld Heights, Ohio. This was at 11:47. Three minutes later at 11:50, the Burger King branch address was posted with wishes of happy unemployment. 5 minutes later, the news station was contacted by another 4channer. And three minutes later, at 11:58, a link was posted: BK\'s \"Tell us about us\" online forum. The foot photo, otherwise known as exhibit A, was attached. Cleveland Scene Magazine contacted the BK in question the next day. When questioned, the breakfast shift manager said \"Oh, I know who that is. He\'s getting fired.\" Mystery solved, by 4chan. Now we can all go back to eating our fast food in peace.";
                MessageBox.Show(text, "Bombiwa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


            }

            UpdateForm();
            actorEditControl1.UpdateActorEdit();
            actorEditControl1.UpdateForm();


        }

        private void PlaceActors(int room, ZActor[] actors)
        {
            if (CurrentScene == null || CurrentScene.Rooms.Count <= 0 || CurrentScene.Rooms[room].ObjModel.Vertices.Count < 1) return;

            StoreUndo(_Actor_);

            ObjFile.Vertex v = CurrentScene.Rooms[room].ObjModel.Vertices[0];
            Vector3d MinCoordinate = new Vector3d(v.X, v.Y, v.Z);
            Vector3d MaxCoordinate = new Vector3d(v.X, v.Y, v.Z);
            foreach (ObjFile.Vertex Vtx in CurrentScene.Rooms[room].ObjModel.Vertices)
            {
                /* Minimum... */
                MinCoordinate.X = Math.Min(MinCoordinate.X, Vtx.X * CurrentScene.Scale);
                MinCoordinate.Y = Math.Min(MinCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                MinCoordinate.Z = Math.Min(MinCoordinate.Z, Vtx.Z * CurrentScene.Scale);

                /* Maximum... */
                MaxCoordinate.X = Math.Max(MaxCoordinate.X, Vtx.X * CurrentScene.Scale);
                MaxCoordinate.Y = Math.Max(MaxCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                MaxCoordinate.Z = Math.Max(MaxCoordinate.Z, Vtx.Z * CurrentScene.Scale);
            }
            Random r = new Random(DateTime.Now.Millisecond);

            int newactors = 0, RNGtries = 0;
            Vector3 rand;
            while (newactors < actors.Length && RNGtries < 10000)
            {
                RNGtries++;
                float dist = 999999;
                Vector3 truepos = new Vector3(-1, -1, -1);
                rand.X = (float)(r.NextDouble() * (MaxCoordinate.X - MinCoordinate.X) + MinCoordinate.X);
                rand.Y = (float)(r.NextDouble() * (MaxCoordinate.Y - MinCoordinate.Y) + MinCoordinate.Y);
                rand.Z = (float)(r.NextDouble() * (MaxCoordinate.Z - MinCoordinate.Z) + MinCoordinate.Z);
                // if (RNGtries%100 == 0)Console.WriteLine(rand.ToString());
                foreach (ObjFile.Group Group in CurrentScene.Rooms[room].TrueGroups)
                {
                    foreach (ObjFile.Triangle Tri in Group.Triangles)
                    {
                        if (Group.Name.ToLower().Contains("#nocollision")) continue;

                        Vector3 collision = ObjFile.RayCollision(
                            CurrentScene.ColModel.Vertices[Tri.VertIndex[0]],
                            CurrentScene.ColModel.Vertices[Tri.VertIndex[1]],
                            CurrentScene.ColModel.Vertices[Tri.VertIndex[2]],
                            rand,
                            new Vector3(rand.X, rand.Y - 30000, rand.Z),
                            CurrentScene.Scale);

                        if (collision != new Vector3(-1, -1, -1) && dist > Distance3D(rand, collision))
                        {
                            dist = Distance3D(rand, collision);
                            truepos = collision;
                        }

                    }
                }

                if (truepos != new Vector3(-1, -1, -1))
                {
                    //  CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.Add(new ZActor(0x00, 0x00, 0x00, 0x00, 0x0008, truepos.X, truepos.Y, truepos.Z, 0.0f, 0x0000));
                    actors[newactors].XPos = truepos.X;
                    actors[newactors].YPos = truepos.Y;
                    actors[newactors].ZPos = truepos.Z;
                    actors[newactors].YRot = (float)(r.NextDouble() * (32000 - -32000) + -32000);
                    CurrentScene.Rooms[room].ZActors.Add(actors[newactors]);
                    //   Console.WriteLine("Added actor!");
                    newactors++;
                }
            }


            UpdateForm();
            actorEditControl1.UpdateActorEdit();
            actorEditControl1.UpdateForm();


        }

        public static float Distance3D(Vector3 v1, Vector3 v2)
        {

            float result = 0;
            double part1 = Math.Pow((v2.X - v1.X), 2);
            double part2 = Math.Pow((v2.Y - v1.Y), 2);
            double part3 = Math.Pow((v2.Z - v1.Z), 2);
            double underRadical = part1 + part2 + part3;
            result = (float)Math.Sqrt(underRadical);
            return result;
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void SpecialObjectChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
                CurrentScene.SpecialObject = Convert.ToUInt16((SpecialObjectComboBox.SelectedItem as SongItem).Value);
        }

        private void ElfMessageChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
                CurrentScene.ElfMessage = Convert.ToByte((ElfMessageComboBox.SelectedItem as SongItem).Value);
        }

        private void NightSFXChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
                CurrentScene.NightSFX = Convert.ToByte((NightSFXComboBox.SelectedItem as SongItem).Value);
        }

        private void EchoKeydown(object sender, KeyEventArgs e)
        {
            if (CurrentScene != null && CurrentScene.Rooms.Count > 0)
                CurrentScene.Rooms[RoomList.SelectedIndex].Echo = (byte)SoundEcho.IntValue;
        }

        private void CameraMovementChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.CameraMovement = Convert.ToByte((CameraMovementComboBox.SelectedItem as SongItem).Value);
                UpdateForm();
            }
        }

        private void WorldmapChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
                CurrentScene.WorldMap = Convert.ToByte((WorldMapComboBox.SelectedItem as SongItem).Value);
        }

        private void AddPathwayButton_Click(object sender, MouseEventArgs e)
        {

            CurrentScene.Pathways.Add(new ZPathway(new List<Vector3>()));
            UpdateForm();
            Helpers.SelectAdd(PathwayNumber, CurrentScene.Pathways);
        }

        private void DeletePathwayButton_Click(object sender, MouseEventArgs e)
        {
            ZPathway Del = CurrentScene.Pathways[(int)PathwayNumber.Value];
            CurrentScene.Pathways.Remove(Del);
            if (CurrentScene.Pathways.Count == 0) PathwayListBox.Items.Clear();
            UpdateForm();
        }

        private void PathwayList_ValueChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void PathwayTransform_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Pathways[(int)PathwayNumber.Value].Points[PathwayListBox.SelectedIndex] =
                new Vector3((float)PathwayXPos.Value, (float)PathwayYPos.Value, (float)PathwayZPos.Value);
            UpdateForm();
        }

        private void PathwayListBox_Click(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void AddPointButton_Click(object sender, EventArgs e)
        {
            if (CurrentScene.ColModel == null) return;

            if (Control.ModifierKeys == Keys.Shift)
            {
                //create in front of the camera

                double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
                double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

                Vector3d truepos = GetTrueCameraPosition();

                if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
                {
                    truepos.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }
                else
                {
                    truepos.X += (float)Math.Sin(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Z -= (float)Math.Cos(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Y -= (float)Math.Tan(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }

                truepos.X = Clamp(truepos.X, -32767, 32767);
                truepos.Y = Clamp(truepos.Y, -32767, 32767);
                truepos.Z = Clamp(truepos.Z, -32767, 32767);

                CurrentScene.Pathways[(int)PathwayNumber.Value].Points.Add((Vector3)truepos);
                UpdateForm();
            }

            else if (CurrentScene.Pathways[(int)PathwayNumber.Value].Points.Count == 0)
            {
                OpenTK.Vector3 MinCoordinate = new OpenTK.Vector3(0, 0, 0);
                OpenTK.Vector3 MaxCoordinate = new OpenTK.Vector3(0, 0, 0);

                foreach (ObjFile.Vertex Vtx in CurrentScene.ColModel.Vertices)
                {
                    /* Minimum... */
                    MinCoordinate.X = (float)Math.Min(MinCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MinCoordinate.Y = (float)Math.Min(MinCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MinCoordinate.Z = (float)Math.Min(MinCoordinate.Z, Vtx.Z * CurrentScene.Scale);

                    /* Maximum... */
                    MaxCoordinate.X = (float)Math.Max(MaxCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MaxCoordinate.Y = (float)Math.Max(MaxCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MaxCoordinate.Z = (float)Math.Max(MaxCoordinate.Z, Vtx.Z * CurrentScene.Scale);
                }

                CurrentScene.Pathways[(int)PathwayNumber.Value].Points.Add(Vector3.Lerp(MinCoordinate, MaxCoordinate, 0.5f));
                UpdateForm();
            }
            else
            {
                CurrentScene.Pathways[(int)PathwayNumber.Value].Points.Add(CurrentScene.Pathways[(int)PathwayNumber.Value].Points[CurrentScene.Pathways[(int)PathwayNumber.Value].Points.Count - 1]);
                UpdateForm();
            }
            if (PathwayListBox.Items.Count == 1) PathwayListBox.SelectedIndex = 0;
            else PathwayListBox.SelectedIndex++;
            UpdateForm();
        }

        private void DeletePointButton_Click(object sender, EventArgs e)
        {
            CurrentScene.Pathways[(int)PathwayNumber.Value].Points.RemoveAt(PathwayListBox.SelectedIndex);
            if (CurrentScene.Pathways[(int)PathwayNumber.Value].Points.Count == 0) PathwayListBox.Items.Clear();
            UpdateForm();
        }

        private void PathwayStickToWall(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;
            if (CurrentScene == null || CurrentScene.Pathways.Count <= 0) return;

            Vector3 from = CurrentScene.Pathways[(int)PathwayNumber.Value].Points[PathwayListBox.SelectedIndex];

            String operation = obj.Name.Replace("StickTo", "");
            float Xdif = 0, Ydif = 0, Zdif = 0;
            if (operation.Contains("X") && operation.Contains("plus")) Xdif = 30000;
            if (operation.Contains("X") && operation.Contains("minus")) Xdif = -30000;
            if (operation.Contains("Y") && operation.Contains("plus")) Ydif = 30000;
            if (operation.Contains("Y") && operation.Contains("minus")) Ydif = -30000;
            if (operation.Contains("Z") && operation.Contains("plus")) Zdif = 30000;
            if (operation.Contains("Z") && operation.Contains("minus")) Zdif = -30000;

            Vector3 newpos = MoveToCollision(from, new Vector3(from.X + Xdif, from.Y + Ydif, from.Z + Zdif));

            if (newpos != new Vector3(-1, -1, -1))
            {
                CurrentScene.Pathways[(int)PathwayNumber.Value].Points[PathwayListBox.SelectedIndex] = newpos;
                UpdateForm();
            }

        }

        private Vector3 MoveToCollision(Vector3 pos, Vector3 dir)
        {
            Vector3 output = new Vector3(-1, -1, -1);
            float dist = 999999;
            foreach (ObjFile.Group Group in CurrentScene.ColModel.Groups)
            {
                foreach (ObjFile.Triangle Tri in Group.Triangles)
                {
                    Vector3 collision = ObjFile.RayCollision(
                        CurrentScene.ColModel.Vertices[Tri.VertIndex[0]],
                        CurrentScene.ColModel.Vertices[Tri.VertIndex[1]],
                        CurrentScene.ColModel.Vertices[Tri.VertIndex[2]],
                        pos,
                        dir,
                        CurrentScene.Scale);
                    if (collision != new Vector3(-1, -1, -1) && dist > Distance3D(pos, collision))
                    {
                        dist = Distance3D(pos, collision);
                        output = collision;
                    }
                }

            }
            return output;
        }

        private void DefaultEnvironmentMenuItem_Click(object sender, EventArgs e)
        {
            SetDefaultEnvironments();
        }

        private void HWWindMenuItem_Click(object sender, EventArgs e)
        {
            CurrentScene.Rooms[RoomList.SelectedIndex].WindWest = 0x0F;
            CurrentScene.Rooms[RoomList.SelectedIndex].WindVertical = 0x28;
            CurrentScene.Rooms[RoomList.SelectedIndex].WindSouth = 0x6D;
            CurrentScene.Rooms[RoomList.SelectedIndex].WindStrength = 0xBE;
            UpdateForm();
        }

        private void CopyFirstRoomSettingsMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 1 && MessageBox.Show("This will copy first room environment settings to all rooms. Proceed?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                int incr = 0;
                foreach (ZScene.ZRoom room in CurrentScene.Rooms)
                {
                    if (incr > 0)
                    {
                        room.StartTime = CurrentScene.Rooms[0].StartTime;
                        room.TimeSpeed = CurrentScene.Rooms[0].TimeSpeed;
                        room.Echo = CurrentScene.Rooms[0].Echo;
                        room.DisableSkybox = CurrentScene.Rooms[0].DisableSkybox;
                        room.DisableSunMoon = CurrentScene.Rooms[0].DisableSunMoon;
                        room.WindWest = CurrentScene.Rooms[0].WindWest;
                        room.WindVertical = CurrentScene.Rooms[0].WindVertical;
                        room.WindSouth = CurrentScene.Rooms[0].WindSouth;
                        room.WindStrength = CurrentScene.Rooms[0].WindStrength;
                        room.Restriction = CurrentScene.Rooms[0].Restriction;
                        room.IdleAnim = CurrentScene.Rooms[0].IdleAnim;
                        room.ShowInvisibleActors = CurrentScene.Rooms[0].ShowInvisibleActors;
                        room.DisableWarpSongs = CurrentScene.Rooms[0].DisableWarpSongs;
                        room.AffectedByPointLight = CurrentScene.Rooms[0].AffectedByPointLight;
                    }
                    incr++;
                }
                UpdateForm();
            }
        }

        private void DisplayAxisMenuItem_Click(object sender, EventArgs e)
        {
            settings.DisplayAxis = DisplayAxisMenuItem.Checked;
        }

        private void UnusedCommandCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.OutdoorLight = UnusedCommandCheckBox.Checked;
            if (SimulateN64Gfx && SimulateN64CheckBox.Checked)
                CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);

            ReverseLightCheckBox.Text = (CurrentScene.OutdoorLight) ? "Use vertex colors" : "Use normal light";
        }

        private void SkyboxComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0 && SkyboxComboBox.SelectedItem != null)
            {
                CurrentScene.SkyboxType = Convert.ToByte((SkyboxComboBox.SelectedItem as SongItem).Value);
                //Console.WriteLine("cst :" + CurrentScene.SkyboxType);
            }
        }

        private void RestrictionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0 && RestrictionComboBox.SelectedItem != null)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].Restriction = Convert.ToByte((RestrictionComboBox.SelectedItem as SongItem).Value);
            }
        }

        private void IdleAnimComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0 && IdleAnimComboBox.SelectedItem != null)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].IdleAnim = Convert.ToByte((IdleAnimComboBox.SelectedItem as SongItem).Value);
            }
        }

        private void CloudyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Cloudy = CloudyCheckBox.Checked;
            }
        }

        private void SkyboxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].DisableSkybox = SkyboxCheckBox.Checked;
            }
        }

        private void SunmoonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].DisableSunMoon = SunmoonCheckBox.Checked;
            }
        }

        private void InvisibleActorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].ShowInvisibleActors = InvisibleActorsCheckBox.Checked;
            }
        }

        private void WarpsongsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].DisableWarpSongs = WarpsongsCheckBox.Checked;
            }
        }


        private void TimeSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].TimeSpeed = (byte)TimeSpeed.IntValue;
            }
        }

        private void WindWest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].WindWest = (byte)WindWest.IntValue;
            }
        }

        private void WindSouth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].WindSouth = (byte)WindSouth.IntValue;
            }
        }

        private void WindVertical_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].WindVertical = (byte)WindVertical.IntValue;
            }
        }

        private void WindStrength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].WindStrength = (byte)WindStrength.IntValue;
            }
        }

        private void AdditionalLightSelect_ValueChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void AdditionalLightAdd_Click(object sender, EventArgs e)
        {
            if (CurrentScene.ColModel == null) return;

            if (Control.ModifierKeys == Keys.Shift)
            {
                //create in front of the camera

                double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
                double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

                Vector3d truepos = GetTrueCameraPosition();

                if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
                {
                    truepos.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }
                else
                {
                    truepos.X += (float)Math.Sin(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Z -= (float)Math.Cos(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Y -= (float)Math.Tan(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }

                truepos.X = Clamp(truepos.X, -32767, 32767);
                truepos.Y = Clamp(truepos.Y, -32767, 32767);
                truepos.Z = Clamp(truepos.Z, -32767, 32767);

                CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights.Add(new ZAdditionalLight((short)truepos.X, (short)truepos.Y, (short)truepos.Z, false, 0, 0, 0, Color.Black));

            }
            else
            {

                Vector3d CenterPoint = GetCenterPoint((CurrentScene.Rooms[RoomList.SelectedIndex]).ObjModel.Vertices);

                CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights.Add(new ZAdditionalLight((short)CenterPoint.X, (short)CenterPoint.Y, (short)CenterPoint.Z, false, 0, 0, 0, Color.Black)
                );
            }

            UpdateForm();
            AdditionalLightSelect.Value = AdditionalLightSelect.Maximum;
        }

        private void AdditionalLightDelete_Click(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights.Count > 0)
            {
                ZAdditionalLight del = CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1];
                CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights.Remove(del);
                UpdateForm();
            }
        }

        private void AdditionalLightTransform_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].XPos = (short)AdditionalLightXPos.Value;
            CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].YPos = (short)AdditionalLightYPos.Value;
            CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].ZPos = (short)AdditionalLightZPos.Value;

            UpdateForm();
        }

        private void AdditionalLightNS_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].NSdirection = (byte)AdditionalLightNS.Value;

            UpdateForm();
        }

        private void AdditionalLightEW_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].EWdirection = (byte)AdditionalLightEW.Value;

            UpdateForm();
        }

        private void AdditionalLightRadius_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].Radius = (byte)AdditionalLightRadius.Value;

            UpdateForm();
        }

        private void PointLightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].PointLight = PointLightCheckBox.Checked;

            UpdateForm();
        }

        private void AdditionalLightColor_DoubleClick(object sender, EventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            if (ColorPicker(ref PB) == true)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].AdditionalLights[(int)AdditionalLightSelect.Value - 1].ColorC = AdditionalLightColor.BackColor;
                UpdateForm();
            }
        }

        private void FixedCameraToViewMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScene != null && short.MaxValue - Math.Abs(Camera.Pos.X) > 0 && short.MaxValue - Math.Abs(Camera.Pos.Y) > 0 && short.MaxValue - Math.Abs(Camera.Pos.Z) > 0)
            {

                Vector3d result = GetTrueCameraPosition();

                CurrentScene.FixedCameraXPos = (short)result.X;
                CurrentScene.FixedCameraYPos = (short)result.Y;
                CurrentScene.FixedCameraZPos = (short)result.Z;

                CurrentScene.FixedCameraXRot = (short)Camera.Rot.X;
                CurrentScene.FixedCameraYRot = (short)Camera.Rot.Y;
                CurrentScene.FixedCameraZRot = (short)Camera.Rot.Z;

                UpdateForm();
            }
        }

        private void ViewToFixedCameraMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                Camera.Pos.X = CurrentScene.FixedCameraXPos;
                Camera.Pos.Y = CurrentScene.FixedCameraYPos;
                Camera.Pos.Z = CurrentScene.FixedCameraZPos;
                Camera.Rot.X = CurrentScene.FixedCameraXRot;
                Camera.Rot.Y = CurrentScene.FixedCameraYRot;
                Camera.Rot.Z = CurrentScene.FixedCameraZRot;
            }
        }

        private void SceneSettingsChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
                CurrentScene.SceneSettings = Convert.ToByte((SceneSettingsComboBox.SelectedItem as AnimationItem).Value);
        }

        private void AnimatedCheckBox(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).Animated = GroupAnimated.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.Animated[Index] = ((ObjFile.Group)GroupList.SelectedItem).Animated;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void setRoomsToUseEnvironment1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.OutdoorLight = true;
                UnusedCommandCheckBox.Checked = true;
                if (SimulateN64Gfx)
                    CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
                for (int i = 0; i < CurrentScene.Rooms.Count; i++)
                {
                    CurrentScene.Rooms[i].StartTime = 0x0610;
                    CurrentScene.Rooms[i].TimeSpeed = 0x00;
                }
                UpdateForm();
            }
        }

        private void LadderTopRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateClimbableCrawlableFlags();
            //CameraAngleNumeric.Text = "0B";
            UpdateForm();
        }

        private void KillingLavaRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDamageSurfaceFlags();
            UpdateForm();
        }

        private void CrawlSpaceRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateClimbableCrawlableFlags();
            UpdateForm();
        }

        private void NoFallDamageRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDamageSurfaceFlags();
            UpdateForm();
        }

        private void CameraAngleNumeric_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].CameraAngle = (long)CameraAngleNumeric.Value;
            UpdateForm();
        }

        private void JabuJabuRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDamageSurfaceFlags();
            UpdateForm();
        }

        private void VoidCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFirstByteFlags();
            UpdateForm();
        }

        private void showControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            int layout = Program.KeyboardLayout == "AZERTY" ? 1 : (Program.KeyboardLayout == "DVORAK" ? 2 : 0); 
               MessageBox.Show(
            "Camera view: " + Environment.NewLine +
            "- Left click (hold): Rotate the camera " + Environment.NewLine +
            "- " + (new[]{ "WASD", "ZQSD", ",AOE" })[layout] + " keys: Move the camera to the sides and front" + Environment.NewLine +
            "- " + (new[]{ "QE", "AE", "'." })[layout] + " keys: Move the camera up and down" + Environment.NewLine +
            "-    +Shift (hold): Move slower" + Environment.NewLine +
            "-    +Space (hold): Move faster" + Environment.NewLine +
            "- Right click: Select instances" + Environment.NewLine +
            "- Middle click (hold) inside the instance: Move the instance in 2D axis" + Environment.NewLine +
            "-    +Shift (hold): Move the instance in a depth axis" + Environment.NewLine + Environment.NewLine +
            "Instances: " + Environment.NewLine +
            "- Shift (hold) while increasing/decreasing position or waterbox size: Increases it by 20 units" + Environment.NewLine +
            " - " + (new[] { "Z/X", "W/X", ";Q" })[layout] + " while moving an actor: Stick to ground/ceiling" + Environment.NewLine +
            " - Mouse wheel after selecting a waterbox: Increase/decrease waterbox size"
            , "Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void AddMarker_Click(object sender, EventArgs e)
        {
            CurrentScene.Cutscene.Add(new ZCutscene(Convert.ToUInt32((MarkerType.SelectedItem as MarkerItem).Value), (ushort[])datatemplate.Clone(), new List<ZCutscenePosition>(), new List<ZTextbox>(), new List<ZCutsceneActor>(), 0, 0));
            UpdateForm();
            if (MarkerSelect.SelectedIndex < MarkerSelect.Items.Count - 1) MarkerSelect.SelectedIndex = MarkerSelect.Items.Count - 1;
            UpdateCutsceneEdit();
        }

        private void DeleteMarker_Click(object sender, EventArgs e)
        {
            if (MarkerSelect.SelectedIndex != -1)
            {
                ZCutscene Del = CurrentScene.Cutscene[MarkerSelect.SelectedIndex];
                CurrentScene.Cutscene.Remove(Del);
                if (CurrentScene.Cutscene.Count == 0) MarkerSelect.Items.Clear();
                CutsceneTabs.SelectedIndex = 2;
                CutsceneAbsolutePositionListBox.SelectedIndex = -1;
                CutsceneActorListBox.SelectedIndex = -1;
                UpdateCutsceneEdit();
            }
        }

        private void MarkerDown_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Cutscene[MarkerSelect.SelectedIndex];
            CurrentScene.Cutscene.RemoveAt(MarkerSelect.SelectedIndex);
            CurrentScene.Cutscene.Insert(MarkerSelect.SelectedIndex + 1, item);
            MarkerSelect.SelectedIndex++;
            UpdateCutsceneEdit();
        }

        private void MarkerUp_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Cutscene[MarkerSelect.SelectedIndex];
            CurrentScene.Cutscene.RemoveAt(MarkerSelect.SelectedIndex);
            CurrentScene.Cutscene.Insert(MarkerSelect.SelectedIndex - 1, item);
            MarkerSelect.SelectedIndex--;
            UpdateCutsceneEdit();
        }

        private void CutsceneAddAbsolutePosition_Click(object sender, EventArgs e)
        {
            if (CurrentScene.ColModel == null) return;

            if (Control.ModifierKeys == Keys.Shift)
            {
                //create in front of the camera

                double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
                double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

                Vector3d truepos = GetTrueCameraPosition(), truepos2 = truepos;

                truepos.X = Clamp(truepos.X, -32767, 32767);
                truepos.Y = Clamp(truepos.Y, -32767, 32767);
                truepos.Z = Clamp(truepos.Z, -32767, 32767);

                if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
                {
                    truepos2.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 5000f;
                }
                else
                {
                    truepos2.X += (float)Math.Sin(RotYRad) * Camera.CameraCoeff * 2.0f * 5000f;
                    truepos2.Z -= (float)Math.Cos(RotYRad) * Camera.CameraCoeff * 2.0f * 5000f;
                    truepos2.Y -= (float)Math.Tan(RotXRad) * Camera.CameraCoeff * 2.0f * 5000f;
                }

                truepos2.X = Clamp(truepos2.X, -32767, 32767);
                truepos2.Y = Clamp(truepos2.Y, -32767, 32767);
                truepos2.Z = Clamp(truepos2.Z, -32767, 32767);

                CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Insert(CutsceneAbsolutePositionListBox.SelectedIndex + 1, new ZCutscenePosition(0, 1, (float)ViewportFOV.Value, (Vector3)truepos, (Vector3)truepos2));
                UpdateCutsceneEdit();
            }

            else if (CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Count == 0)
            {
                OpenTK.Vector3 MinCoordinate = new OpenTK.Vector3(0, 0, 0);
                OpenTK.Vector3 MaxCoordinate = new OpenTK.Vector3(0, 0, 0);

                foreach (ObjFile.Vertex Vtx in CurrentScene.ColModel.Vertices)
                {
                    /* Minimum... */
                    MinCoordinate.X = (float)Math.Min(MinCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MinCoordinate.Y = (float)Math.Min(MinCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MinCoordinate.Z = (float)Math.Min(MinCoordinate.Z, Vtx.Z * CurrentScene.Scale);

                    /* Maximum... */
                    MaxCoordinate.X = (float)Math.Max(MaxCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MaxCoordinate.Y = (float)Math.Max(MaxCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MaxCoordinate.Z = (float)Math.Max(MaxCoordinate.Z, Vtx.Z * CurrentScene.Scale);
                }

                Vector3 pos = Vector3.Lerp(MinCoordinate, MaxCoordinate, 0.5f), pos2;
                pos.X = (float)Math.Round(pos.X);
                pos.Y = (float)Math.Round(pos.Y);
                pos.Z = (float)Math.Round(pos.Z);

                pos2 = pos;
                pos2.Y += 20;

                CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Add(new ZCutscenePosition(0, 1, (float)ViewportFOV.Value, pos, pos2));
                UpdateCutsceneEdit();
            }
            else
            {
                Vector3 from = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Count - 1].Position, from2;
                from2 = from;
                from2.Y += 20;

                CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Insert(CutsceneAbsolutePositionListBox.SelectedIndex + 1, new ZCutscenePosition(0, 1, (float)ViewportFOV.Value, from, from2));
                UpdateCutsceneEdit();
            }
            if (CutsceneAbsolutePositionListBox.Items.Count > 1) CutsceneAbsolutePositionListBox.SelectedIndex++;
            UpdateCutsceneEdit();
        }

        private void MarkerType_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void CutsceneDeleteAbsolutePosition_Click(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.RemoveAt(CutsceneAbsolutePositionListBox.SelectedIndex);
            if (CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Count == 0) CutsceneAbsolutePositionListBox.Items.Clear();
            UpdateCutsceneEdit();
        }

        private void MarkerSelect_MouseClick(object sender, MouseEventArgs e)
        {
            CutsceneTextboxList.SelectedIndex = -1;
            CutsceneAbsolutePositionListBox.SelectedIndex = -1;
            UpdateCutsceneEdit();
        }

        private void MarkerSelect_KeyDown(object sender, KeyEventArgs e)
        {
            CutsceneTextboxList.SelectedIndex = -1;
            CutsceneAbsolutePositionListBox.SelectedIndex = -1;
            UpdateCutsceneEdit();
        }


        private void objectTableEditorToolStripMenuItem_Click(object sender, EventArgs e) //NO RELEASE!!!
        {
            ObjectTableEditor objecttableeditor = new ObjectTableEditor();
            objecttableeditor.ShowDialog();

        }

        private void CutsceneAbsolutePosition_ChangeValue(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Position = new Vector3((float)CutsceneAbsolutePositionX.Value, (float)CutsceneAbsolutePositionY.Value, (float)CutsceneAbsolutePositionZ.Value);
            CameraPreview_UpdateTransforms();
            UpdateCutsceneEdit();
        }

        private void CutsceneAbsolutePositionListBox_Click(object sender, EventArgs e)
        {
            UpdateCutsceneEdit();
            CameraPreview_UpdateTransforms();
            CameraPreview_UpdateParams();
        }

        private void CutsceneFoxusPosition_ChangeValue(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Position2 = new Vector3((float)CutscenePositionXFocus.Value, (float)CutscenePositionYFocus.Value, (float)CutscenePositionZFocus.Value);

            CameraPreview_UpdateTransforms();
            UpdateCutsceneEdit();
        }

        private void MarkerEndFrame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MarkerEndFrame.IntValue > 0xFFFF) MarkerEndFrame.Text = "" + 0xFFFF;
                CurrentScene.Cutscene[MarkerSelect.SelectedIndex].EndFrame = (ushort)MarkerEndFrame.IntValue;

                UpdateCutsceneEdit();
            }
        }

        private void MarkerStartFrame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MarkerStartFrame.IntValue > 0xFFFF) MarkerStartFrame.Text = "" + 0xFFFF;
                CurrentScene.Cutscene[MarkerSelect.SelectedIndex].StartFrame = (ushort)MarkerStartFrame.IntValue;

                UpdateCutsceneEdit();
            }
        }

        private void CutsceneTextboxType_SelectedValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes[CutsceneTextboxList.SelectedIndex].Type = Convert.ToByte((CutsceneTextboxType.SelectedItem as SongItem).Value);
            UpdateCutsceneEdit();
        }

        private void CutsceneTextboxFrames_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes[CutsceneTextboxList.SelectedIndex].Frames = (ushort)CutsceneTextboxFrames.Value;
            UpdateCutsceneEdit();
        }

        private void CutsceneTextboxMessageId_Leave(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes[CutsceneTextboxList.SelectedIndex].Message = (ushort)CutsceneTextboxMessageId.IntValue;
            UpdateCutsceneEdit();
        }

        private void CutsceneAddTextbox_Click(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes.Add(new ZTextbox(0, 1, 0xFFFF, 0, 0));
            UpdateCutsceneEdit();
            if (CutsceneTextboxList.Items.Count > 1) CutsceneTextboxList.SelectedIndex++;
            UpdateCutsceneEdit();
        }

        private void CutsceneDeleteTextbox_Click(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes.RemoveAt(CutsceneTextboxList.SelectedIndex);
            if (CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes.Count == 0) CutsceneTextboxList.Items.Clear();
            UpdateCutsceneEdit();
        }

        private void Injectoffset_leave(object sender, CancelEventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.InjectOffset = InjectoffsetTextbox.IntValue;
                UpdateForm();
            }
        }


        private void rOMEntranceTableEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!entrancetable_visible)
            {

                EntranceTableEditor entrancetableeditor = new EntranceTableEditor();
                entrancetableeditor.mainform = this;
                entrancetableeditor.Show();
                entrancetable_visible = true;


            }


        }

        private void InjectoffsetTextbox_Leave(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.InjectOffset = InjectoffsetTextbox.IntValue;

                UpdateForm();
            }
        }

        private void NameTextbox_Leave(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Name = NameTextbox.Text;
                UpdateForm();
            }
        }


        private void WaterboxRoom_Leave(object sender, EventArgs e)
        {
            if (WaterboxRoom.IntValue > CurrentScene.Rooms.Count && WaterboxRoom.IntValue != 0x3F) WaterboxRoom.Text = (CurrentScene.Rooms.Count - 1).ToString("X2");
            UpdateWaterboxData();
        }

        private void RoomInjectionOffset_Leave(object sender, EventArgs e)
        {
            if (CurrentScene != null && CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].InjectOffset = RoomInjectionOffset.IntValue;
                UpdateForm();
            }
        }

        private void TimeSpeed_Leave(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].TimeSpeed = (byte)TimeSpeed.IntValue;
            }
        }

        private void SoundEcho_Leave(object sender, EventArgs e)
        {
            if (CurrentScene != null)
                CurrentScene.Rooms[RoomList.SelectedIndex].Echo = (byte)SoundEcho.IntValue;
        }

        private void WindWest_Leave(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].WindWest = (byte)WindWest.IntValue;
            }
        }

        private void WindSouth_Leave(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].WindSouth = (byte)WindSouth.IntValue;
            }
        }

        private void WindVertical_Leave(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].WindVertical = (byte)WindVertical.IntValue;
            }
        }

        private void WindStrength_Leave(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].WindStrength = (byte)WindStrength.IntValue;
            }
        }

        private void PolygonRawdata_Leave(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].Raw = ulong.Parse(PolygonRawdata.Text, System.Globalization.NumberStyles.HexNumber);
            UpdateForm();
        }

        public void SetActorFromDatabase(ushort number, ushort variable, byte target)
        {
            if (target == 0)
            {
                StoreUndo(_Actor_);
                CurrentScene.Rooms[RoomList.SelectedIndex].ZActors[actorEditControl1.ActorComboBox.SelectedIndex].Number = number;
                CurrentScene.Rooms[RoomList.SelectedIndex].ZActors[actorEditControl1.ActorComboBox.SelectedIndex].Variable = variable;
                actorEditControl1.UpdateActorEdit();
                actorEditControl1.UpdateForm();

            }
            else if (target == 1)
            {
                StoreUndo(_Transition_);
                CurrentScene.Transitions[actorEditControl2.ActorComboBox.SelectedIndex].Number = number;
                CurrentScene.Transitions[actorEditControl2.ActorComboBox.SelectedIndex].Variable = variable;
                actorEditControl2.UpdateActorEdit();
                actorEditControl2.UpdateForm();
            }
            else if (target == 2)
            {
                StoreUndo(_Spawn_);
                CurrentScene.SpawnPoints[actorEditControl3.ActorComboBox.SelectedIndex].Number = number;
                CurrentScene.SpawnPoints[actorEditControl3.ActorComboBox.SelectedIndex].Variable = variable;
                actorEditControl3.UpdateActorEdit();
                actorEditControl3.UpdateForm();
            }
        }


        private int FindComboItemValue(ComboBox.ObjectCollection items, uint marker)
        {
            foreach (MarkerItem item in items)
            {
                if (Convert.ToUInt32(item.Value) == marker) return items.IndexOf(item);
            }
            return 0;
        }

        private int FindSongComboItemValue(ComboBox.ObjectCollection items, uint marker)
        {
            foreach (SongItem item in items)
            {
                if (Convert.ToUInt32(item.Value.ToString()) == marker) return items.IndexOf(item);
            }
            return 0;
        }

        private void clearSceneDmatableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ROM = "";
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = saveFileDialog1.FileName;
                }
            }

            if (ROM != "")
            {
                ClearSceneDmaTable(ROM);

            }
        }

        private void ClearSceneDmaTable(string ROM)
        {
            if (IsFileLocked(ROM))
                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                ROM rom = CheckVersion(new List<byte>(File.ReadAllBytes(ROM)));
                /*
                if (rom.Prefix != "DBGMQ")
                {
                    MessageBox.Show("This is not an OoT debug rom. Better enable 'custom dma table on inject' in options", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }*/

                BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));
                int StartOffset = (int)rom.SceneDmaTableStart;
                int EndOffset = (int)rom.SceneDmaTableEnd;
                BWS.Seek(StartOffset, SeekOrigin.Begin);

                byte[] Output = new byte[EndOffset - StartOffset];

                BWS.Write(Output.ToArray());

                BWS.Close();


                RecalculateCRC(File.Open(ROM, FileMode.Open, FileAccess.ReadWrite));
                //Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"rn64crc/rn64crc.exe"), "-u " + saveFileDialog1.FileName);

                MessageBox.Show("DMA table cleared! crc updated! (remember to set the rom to use 8mb again in emulator settings!)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void disableWaterboxMouseMovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.Disablewaterboxmovement = disableWaterboxMouseMovementToolStripMenuItem.Checked;
        }

        private void saveOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IO.Export<Settings>(settings, "Settings.xml");
        }

        private void cutsceneTableEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!cutscenetable_visible)
            {

                CutsceneTableEditor cutscenetableeditor = new CutsceneTableEditor();

                cutscenetableeditor.Show();
                cutscenetable_visible = true;


            }


        }

        private void CutsceneTextboxTopMessageID_Leave(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes[CutsceneTextboxList.SelectedIndex].TopMessage = (ushort)CutsceneTextboxTopMessageID.IntValue;
            UpdateCutsceneEdit();
        }

        private void CutsceneTextboxBottomMessageID_Leave(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes[CutsceneTextboxList.SelectedIndex].BottomMessage = (ushort)CutsceneTextboxBottomMessageID.IntValue;
            UpdateCutsceneEdit();
        }

        private void CutsceneTextboxList_Click(object sender, EventArgs e)
        {
            UpdateCutsceneEdit();
        }

        private void MarkerType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CurrentScene != null && CurrentScene.Cutscene.Count > 0)
            {
                CurrentScene.Cutscene[MarkerSelect.SelectedIndex] = new ZCutscene(Convert.ToUInt32((MarkerType.SelectedItem as MarkerItem).Value), (ushort[])datatemplate.Clone(), new List<ZCutscenePosition>(), new List<ZTextbox>(), new List<ZCutsceneActor>(), 0, 0);
                UpdateCutsceneEdit();
            }
        }

        private void CutscenePositionFrameDuration_Leave(object sender, EventArgs e)
        {

            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Frames = (ushort)CutscenePositionFrameDuration.Value;

            UpdateCutsceneEdit();

        }

        private void CutsceneAbsolutePositionCameraRoll_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Cameraroll = (sbyte)CutsceneAbsolutePositionCameraRoll.Value;

            CameraPreview_UpdateParams();
            UpdateCutsceneEdit();
        }

        private void CutsceneAbsolutePositionAngleView_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Angle = (float)CutsceneAbsolutePositionAngleView.Value;

            CameraPreview_UpdateParams();
            UpdateCutsceneEdit();
        }

        private void MarkerStartFrame_Leave(object sender, EventArgs e)
        {
            if (MarkerStartFrame.IntValue > 0xFFFF) MarkerStartFrame.Text = "" + 0xFFFF;
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].StartFrame = (ushort)MarkerStartFrame.IntValue;

            UpdateCutsceneEdit();
        }

        private void GroupMetallic_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).Metallic = GroupMetallic.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.Metallic[Index] = ((ObjFile.Group)GroupList.SelectedItem).Metallic;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void createPathwaysForEachBoundingBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vector3 MinCoordinate = new Vector3(32766, 32766, 32766);
            Vector3 MaxCoordinate = new Vector3(-32766, -32766, -32766);

            foreach (ObjFile.Triangle Tri2 in CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups[GroupList.SelectedIndex].Triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    // if ((float)Obj.VertexColors[Tri.VertColor[i] - 1].A < 1) Console.WriteLine("Vertex with alpha!");
                    /* Minimum... */
                    MinCoordinate.X = (float)Math.Min(MinCoordinate.X, CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices[Tri2.VertIndex[i]].X * (float)ScaleNumericbox.Value);
                    MinCoordinate.Y = (float)Math.Min(MinCoordinate.Y, CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices[Tri2.VertIndex[i]].Y * (float)ScaleNumericbox.Value);
                    MinCoordinate.Z = (float)Math.Min(MinCoordinate.Z, CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices[Tri2.VertIndex[i]].Z * (float)ScaleNumericbox.Value);

                    /* Maximum... */
                    MaxCoordinate.X = (float)Math.Max(MaxCoordinate.X, CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices[Tri2.VertIndex[i]].X * (float)ScaleNumericbox.Value);
                    MaxCoordinate.Y = (float)Math.Max(MaxCoordinate.Y, CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices[Tri2.VertIndex[i]].Y * (float)ScaleNumericbox.Value);
                    MaxCoordinate.Z = (float)Math.Max(MaxCoordinate.Z, CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Vertices[Tri2.VertIndex[i]].Z * (float)ScaleNumericbox.Value);
                }

            }

            ZPathway path = new ZPathway();
            path.Points = new List<Vector3>();

            path.Points.Add(new Vector3(MinCoordinate.X, MinCoordinate.Y, MinCoordinate.Z));
            path.Points.Add(new Vector3(MaxCoordinate.X, MinCoordinate.Y, MinCoordinate.Z));
            path.Points.Add(new Vector3(MaxCoordinate.X, MinCoordinate.Y, MaxCoordinate.Z));
            path.Points.Add(new Vector3(MinCoordinate.X, MinCoordinate.Y, MaxCoordinate.Z));
            path.Points.Add(new Vector3(MinCoordinate.X, MinCoordinate.Y, MinCoordinate.Z)); //repeat
            path.Points.Add(new Vector3(MinCoordinate.X, MaxCoordinate.Y, MinCoordinate.Z));
            path.Points.Add(new Vector3(MaxCoordinate.X, MaxCoordinate.Y, MinCoordinate.Z));
            path.Points.Add(new Vector3(MaxCoordinate.X, MaxCoordinate.Y, MaxCoordinate.Z));
            path.Points.Add(new Vector3(MinCoordinate.X, MaxCoordinate.Y, MaxCoordinate.Z));
            path.Points.Add(new Vector3(MinCoordinate.X, MaxCoordinate.Y, MinCoordinate.Z)); //repeat

            CurrentScene.Pathways.Add(path);

            UpdateForm();
        }

        private void dListCullingMenuItem_Click(object sender, EventArgs e)
        {
            settings.DListCulling = dListCullingMenuItem.Checked;
        }

        private void updateCRCMenuItem_Click(object sender, EventArgs e)
        {
            settings.UpdateCRC = updateCRCMenuItem.Checked;
        }

        private void FogDistance_Leave(object sender, EventArgs e)
        {
            UpdateEnvironmentData();
        }

        private void DrawDistance_Leave(object sender, EventArgs e)
        {
            UpdateEnvironmentData();
        }

        private void renderActorslMenuItem_Click(object sender, EventArgs e)
        {
            settings.RenderActors = RenderActorstoolStrip.Checked;
        }

        private void ContinualInject_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.ContinualInject = ContinualInject.Checked;
                if (RoomList.SelectedIndex == 0)
                    RoomInjectionOffset.Enabled = !ContinualInject.Checked;
            }
        }

        private void AddMultipleRooms_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Wavefront .obj / Collada .dae (*.obj;*.dae)|*.obj;*.dae|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            CurrentScene.NewRoomMode = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CurrentScene.AddRoom(openFileDialog1.FileName, " (Room 0)");
                int maxroom = 0;
                int tmp = 0;
                foreach (ObjFile.Group group in CurrentScene.Rooms[0].ObjModel.Groups)
                {
                    // Console.WriteLine(group.Name.ToLower());
                    group.Name = group.Name.Replace("TAG_", "#");

                    if (group.Name.ToLower().Contains("#room"))
                    {

                        string s = group.Name.ToLower().Substring(group.Name.ToLower().IndexOf("#room") + 5);

                        if (s.Contains("#"))
                            s = s.Substring(0, s.IndexOf("#"));

                        if (!Int32.TryParse(s, out tmp))
                        {
                            MessageBox.Show("Bad usage of #Room tag. The tag needs to be at the end of the group name or before another tag.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            CurrentScene.Rooms.Clear(); CurrentScene.NewRoomMode = false;
                            ((CurrencyManager)RoomList.BindingContext[CurrentScene.Rooms]).Refresh();
                            if (NormalHeader.SceneHeaders.Count > 0) ResetAlternateRooms();
                            GroupList.DataSource = null;
                            UpdateForm();
                            SelectRoom();
                            return;
                        }
                        //tmp = Convert.ToInt32(s);
                        if (tmp > maxroom) maxroom = tmp;
                    }
                }

                if (maxroom > 0)
                {
                    for (int i = 0; i < maxroom; i++)
                    {
                        CurrentScene.AddRoom(openFileDialog1.FileName, " (Room " + (i + 1) + ")");

                        if (settings.MajorasMask)
                        {
                            CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].WindSouth = 0;
                            CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].WindStrength = 0;
                            CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].WindVertical = 0;
                            CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].WindWest = 0;
                        }

                        if (CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].TrueGroups.Count == 0)
                        {
                            MessageBox.Show("Room " + (CurrentScene.Rooms.Count - 1) + " has no groups, probably due to bad export settings, or maybe because you didn't assigned any group to this room. If using blender, try installing the SharpOcarina export plugin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }


                ((CurrencyManager)RoomList.BindingContext[CurrentScene.Rooms]).Refresh();

                if (NormalHeader.SceneHeaders.Count > 0) ResetAlternateRooms();

                UpdateForm();
                SelectRoom(0);

            }
        }

        private void CutsceneActorAddAction_Click(object sender, EventArgs e)
        {
            if (CurrentScene.ColModel == null) return;

            if (Control.ModifierKeys == Keys.Shift)
            {
                //create in front of the camera

                double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
                double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

                Vector3d truepos = GetTrueCameraPosition(), truepos2;

                if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
                {
                    truepos.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }
                else
                {
                    truepos.X += (float)Math.Sin(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Z -= (float)Math.Cos(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Y -= (float)Math.Tan(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }

                truepos.X = Clamp(truepos.X, -32767, 32767);
                truepos.Y = Clamp(truepos.Y, -32767, 32767);
                truepos.Z = Clamp(truepos.Z, -32767, 32767);

                truepos2 = truepos;
                truepos2.Y += 20;

                CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Add(new ZCutsceneActor(0, 1, (Vector3)truepos, (Vector3)truepos2, new Vector3(0, 0, 0)));
                UpdateCutsceneEdit();
            }

            else if (CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Count == 0)
            {
                OpenTK.Vector3 MinCoordinate = new OpenTK.Vector3(0, 0, 0);
                OpenTK.Vector3 MaxCoordinate = new OpenTK.Vector3(0, 0, 0);

                foreach (ObjFile.Vertex Vtx in CurrentScene.ColModel.Vertices)
                {
                    /* Minimum... */
                    MinCoordinate.X = (float)Math.Min(MinCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MinCoordinate.Y = (float)Math.Min(MinCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MinCoordinate.Z = (float)Math.Min(MinCoordinate.Z, Vtx.Z * CurrentScene.Scale);

                    /* Maximum... */
                    MaxCoordinate.X = (float)Math.Max(MaxCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MaxCoordinate.Y = (float)Math.Max(MaxCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MaxCoordinate.Z = (float)Math.Max(MaxCoordinate.Z, Vtx.Z * CurrentScene.Scale);
                }

                Vector3 pos = Vector3.Lerp(MinCoordinate, MaxCoordinate, 0.5f), pos2;
                pos.X = (float)Math.Round(pos.X);
                pos.Y = (float)Math.Round(pos.Y);
                pos.Z = (float)Math.Round(pos.Z);

                pos2 = pos;
                pos2.Y += 20;

                CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Add(new ZCutsceneActor(0, 0, pos, pos2, new Vector3(0, 0, 0)));
                UpdateCutsceneEdit();
            }
            else
            {
                Vector3 from = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Count - 1].Position2, from2;
                from2 = from;
                from2.Y += 20;
                ushort animtest = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Count - 1].Animation;

                CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Add(new ZCutsceneActor(0, 0, from, from2, new Vector3(0, 0, 0)));
                UpdateCutsceneEdit();
            }
            if (CutsceneActorListBox.Items.Count > 1) CutsceneActorListBox.SelectedIndex++;
            UpdateCutsceneEdit();
        }

        private void CutsceneActorDeleteAction_Click(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.RemoveAt(CutsceneActorListBox.SelectedIndex);
            if (CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Count == 0) CutsceneActorListBox.Items.Clear();
            UpdateCutsceneEdit();
        }

        private void CutsceneActorStart_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Position = new Vector3((float)CutsceneActorXStart.Value, (float)CutsceneActorYStart.Value, (float)CutsceneActorZStart.Value);

            UpdateCutsceneEdit();
        }

        private void CutsceneActorEnd_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Position2 = new Vector3((float)CutsceneActorXEnd.Value, (float)CutsceneActorYEnd.Value, (float)CutsceneActorZEnd.Value);

            UpdateCutsceneEdit();
        }

        private void CutsceneActorFrameDuration_Leave(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Frames = (ushort)CutsceneActorFrameDuration.Value;

            UpdateCutsceneEdit();
        }

        private void CutsceneActorListBox_Click(object sender, EventArgs e)
        {
            UpdateCutsceneEdit();
        }

        private void CutsceneActorAnimation_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Animation = Convert.ToUInt16((CutsceneActorAnimation.SelectedItem as SongItem).Value.ToString());
            UpdateCutsceneEdit();
        }

        private void MarkerEndFrame_Leave(object sender, EventArgs e)
        {

            if (MarkerEndFrame.IntValue > 0xFFFF) MarkerEndFrame.Text = "" + 0xFFFF;
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].EndFrame = (ushort)MarkerEndFrame.IntValue;

            UpdateCutsceneEdit();

        }

        private void CutsceneTransitionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[0] = Convert.ToUInt16((CutsceneTransitionComboBox.SelectedItem as SongItem).Value);
            UpdateCutsceneEdit();
        }

        private void CutsceneAsmComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[0] = Convert.ToUInt16((CutsceneAsmComboBox.SelectedItem as SongItem).Value);
            UpdateCutsceneEdit();
        }

        private void CutsceneActorListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void label67_Click(object sender, EventArgs e)
        {

        }

        private void label69_Click(object sender, EventArgs e)
        {

        }

        private void label68_Click(object sender, EventArgs e)
        {

        }

        private void label64_Click(object sender, EventArgs e)
        {

        }

        private void label66_Click(object sender, EventArgs e)
        {

        }

        private void label65_Click(object sender, EventArgs e)
        {

        }

        private void label62_Click(object sender, EventArgs e)
        {

        }

        private void importCutscene(List<byte> CutsceneBinaryData, int start)
        {


            List<ZCutscene> CameraCommands = new List<ZCutscene>();

            ushort marker;
            string scenename = "";


            int offset = start + 8; //skip cutscene header

            while (offset < CutsceneBinaryData.Count - 1)
            {

                marker = Helpers.Read16(CutsceneBinaryData, offset + 2);


                offset += 4;

                // Console.WriteLine("marker: " + marker.ToString("X") + " at offset: " + offset.ToString("X"));

                if (marker == 0x0001 || marker == 0x0005)
                {
                    ZCutscene cutscene = new ZCutscene(marker, (ushort[])datatemplate.Clone(), new List<ZCutscenePosition>(), new List<ZTextbox>(), new List<ZCutsceneActor>(), 0, 0);

                    cutscene.StartFrame = Helpers.Read16(CutsceneBinaryData, offset + 2);

                    offset += 8;

                    while (1 == 1)
                    {
                        Vector3 pos = new Vector3();
                        pos.X = Helpers.Read16S(CutsceneBinaryData, offset + 8);
                        pos.Y = Helpers.Read16S(CutsceneBinaryData, offset + 10);
                        pos.Z = Helpers.Read16S(CutsceneBinaryData, offset + 12);

                        ZCutscenePosition cutpos = new ZCutscenePosition(0, 0, 45, pos, new Vector3());

                        cutscene.Points.Add(cutpos);


                        //  Console.WriteLine("offset:  " + offset);

                        offset += 16;

                        if (CutsceneBinaryData[offset - 16] == 0xFF) break;
                    }

                    CameraCommands.Add(cutscene);

                }
                else if (marker == 0x0002 || marker == 0x0006)
                {

                    ZCutscene cutscene = CameraCommands[0];


                    offset += 8;

                    int counter = 0;

                    while (1 == 1)
                    {
                        Vector3 pos = new Vector3();
                        pos.X = Helpers.Read16S(CutsceneBinaryData, offset + 8);
                        pos.Y = Helpers.Read16S(CutsceneBinaryData, offset + 10);
                        pos.Z = Helpers.Read16S(CutsceneBinaryData, offset + 12);

                        cutscene.Points[counter].Position2 = pos;

                        cutscene.Points[counter].Cameraroll = (sbyte)CutsceneBinaryData[offset + 1];
                        cutscene.Points[counter].Frames = Helpers.Read16(CutsceneBinaryData, offset + 2);
                        cutscene.Points[counter].Angle = BitConverter.ToSingle(BitConverter.GetBytes(Helpers.Read32(CutsceneBinaryData, offset + 4)), 0);

                        //    Console.WriteLine("offset:  " + offset);

                        offset += 16;

                        counter++;

                        if (CutsceneBinaryData[offset - 16] == 0xFF) break;
                    }

                    // CameraCommands.Add(cutscene);

                    CurrentScene.Cutscene.Add(cutscene);
                    CameraCommands.RemoveAt(0);

                }
                else if (marker == 0x0009) //unknown, skip
                {
                    int skip = Helpers.Read16(CutsceneBinaryData, offset + 2);
                    offset += 4;
                    offset += skip * 12;
                }
                else if (marker == 0x008C) //set time
                {
                    ZCutscene cutscene = new ZCutscene(marker, (ushort[])datatemplate.Clone(), new List<ZCutscenePosition>(), new List<ZTextbox>(), new List<ZCutsceneActor>(), 0, 0);

                    cutscene.StartFrame = Helpers.Read16(CutsceneBinaryData, offset + 6);
                    cutscene.EndFrame = (ushort)(cutscene.StartFrame + 1);
                    cutscene.Data[0] = CutsceneBinaryData[offset + 10];
                    cutscene.Data[1] = CutsceneBinaryData[offset + 11];
                    CurrentScene.Cutscene.Add(cutscene);

                    offset += 4 + (12 * Helpers.Read16(CutsceneBinaryData, offset + 2));

                }
                else if (marker == 0x002D || marker == 0x03E8) //transition and asm
                {
                    ZCutscene cutscene = new ZCutscene(marker, (ushort[])datatemplate.Clone(), new List<ZCutscenePosition>(), new List<ZTextbox>(), new List<ZCutsceneActor>(), 0, 0);

                    cutscene.Data[0] = Helpers.Read16(CutsceneBinaryData, offset + 4);
                    cutscene.StartFrame = Helpers.Read16(CutsceneBinaryData, offset + 6);
                    cutscene.EndFrame = Helpers.Read16(CutsceneBinaryData, offset + 8);

                    CurrentScene.Cutscene.Add(cutscene);

                    offset += 12;
                }
                else if (marker == 0xFFFF) //finish
                {
                    break;
                }
                else if (marker == 0x0013)
                {
                    ZCutscene cutscene = new ZCutscene(marker, (ushort[])datatemplate.Clone(), new List<ZCutscenePosition>(), new List<ZTextbox>(), new List<ZCutsceneActor>(), 0, 0);

                    int entries = Helpers.Read16(CutsceneBinaryData, offset + 2);

                    offset += 4;

                    cutscene.StartFrame = Helpers.Read16(CutsceneBinaryData, offset + 2);

                    while (entries != 0)
                    {
                        entries--;

                        // if (Helpers.Read16(CutsceneBinaryData, offset) != 0xFFFF)
                        // {
                        ZTextbox textbox = new ZTextbox();

                        textbox.Message = Helpers.Read16(CutsceneBinaryData, offset);
                        textbox.Frames = (ushort)(Helpers.Read16(CutsceneBinaryData, offset + 4) - Helpers.Read16(CutsceneBinaryData, offset + 2));

                        cutscene.Textboxes.Add(textbox);
                        //  }

                        offset += 12;

                    }

                    CurrentScene.Cutscene.Add(cutscene);

                }
                else
                {
                    /*
                    int skip = Helpers.Read16(CutsceneBinaryData, offset + 2);
                    Console.WriteLine("Skipping: " + skip);
                    offset += 4;
                    offset += skip * 48;*/
                    ZCutscene cutscene = new ZCutscene(marker, (ushort[])datatemplate.Clone(), new List<ZCutscenePosition>(), new List<ZTextbox>(), new List<ZCutsceneActor>(), 0, 0);

                    int entries = Helpers.Read16(CutsceneBinaryData, offset + 2);

                    offset += 4;

                    cutscene.StartFrame = Helpers.Read16(CutsceneBinaryData, offset + 2);

                    while (entries != 0)
                    {
                        entries--;

                        ZCutsceneActor action = new ZCutsceneActor();

                        Vector3 pos = new Vector3();
                        pos.X = Helpers.Read32S(CutsceneBinaryData, offset + 12);
                        pos.Y = Helpers.Read32S(CutsceneBinaryData, offset + 16);
                        pos.Z = Helpers.Read32S(CutsceneBinaryData, offset + 20);

                        Vector3 pos2 = new Vector3();
                        pos2.X = Helpers.Read32S(CutsceneBinaryData, offset + 24);
                        pos2.Y = Helpers.Read32S(CutsceneBinaryData, offset + 28);
                        pos2.Z = Helpers.Read32S(CutsceneBinaryData, offset + 32);

                        Vector3 rot = new Vector3();
                        rot.X = Helpers.Read16S(CutsceneBinaryData, offset + 6);
                        rot.Y = Helpers.Read16S(CutsceneBinaryData, offset + 8);
                        rot.Z = Helpers.Read16S(CutsceneBinaryData, offset + 10);

                        action.Animation = Helpers.Read16(CutsceneBinaryData, offset);
                        action.Frames = (ushort)(Helpers.Read16(CutsceneBinaryData, offset + 4) - Helpers.Read16(CutsceneBinaryData, offset + 2));
                        action.Position = pos;
                        action.Position2 = pos2;
                        action.Rotation = rot;

                        // Console.WriteLine(rot);

                        cutscene.CutsceneActors.Add(action);


                        offset += 48;


                    }

                    CurrentScene.Cutscene.Add(cutscene);
                }

            }
        }

        private void openCutsceneRawDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files (*.*)|*.*";

            //openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> CutsceneBinaryData = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                CurrentScene.Cutscene.Clear();

                importCutscene(CutsceneBinaryData, 0);

                UpdateForm();

            }
        }

        private void autoplaceDoorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int doorcount = 0;
            StoreUndo(_Transition_);
            foreach (ObjFile.Group group in CurrentScene.ColModel.Groups)
            {
                string name = group.Name.ToLower().Replace("TAG_", "#");
                if (name.Contains("#door") || name.Contains("#blackplane"))
                {
                    //Console.WriteLine(group.Name);
                    foreach (ObjFile.Triangle tri in group.Triangles)
                    {
                        List<Vector3d> face = new List<Vector3d>() { CurrentScene.ColModel.Vertices[tri.VertIndex[0]].ToVector3d(), CurrentScene.ColModel.Vertices[tri.VertIndex[1]].ToVector3d(), CurrentScene.ColModel.Vertices[tri.VertIndex[2]].ToVector3d() };

                        int score = 0;
                        foreach (ObjFile.Triangle tri2 in group.Triangles)
                        {
                            score = 0;
                            if (face.Contains(CurrentScene.ColModel.Vertices[tri2.VertIndex[0]].ToVector3d())) score++;
                            if (face.Contains(CurrentScene.ColModel.Vertices[tri2.VertIndex[1]].ToVector3d())) score++;
                            if (face.Contains(CurrentScene.ColModel.Vertices[tri2.VertIndex[2]].ToVector3d())) score++;

                            if (score == 2)
                            {
                                face.AddRange(new List<Vector3d>() { CurrentScene.ColModel.Vertices[tri2.VertIndex[0]].ToVector3d(), CurrentScene.ColModel.Vertices[tri2.VertIndex[1]].ToVector3d(), CurrentScene.ColModel.Vertices[tri2.VertIndex[2]].ToVector3d() });
                                break;
                            }
                        }

                        if (score != 2) continue;

                        Vector3d newpos = ObjFile.CalculateCentroid(face);
                        double min = Math.Min(CurrentScene.ColModel.Vertices[tri.VertIndex[0]].Y, CurrentScene.ColModel.Vertices[tri.VertIndex[1]].Y);
                        min = Math.Min(min, CurrentScene.ColModel.Vertices[tri.VertIndex[2]].Y);
                        newpos = new Vector3d(newpos.X, min, newpos.Z);
                        bool hasactor = false;
                        foreach (ZActor actor in CurrentScene.Transitions)
                        {
                            if (Distance3D(new Vector3(actor.XPos, actor.YPos, actor.ZPos), (Vector3)newpos) < 20) { hasactor = true; break; }
                        }
                        if (!hasactor)
                        {


                            ObjFile.Normal normal = ObjFile.GenerateNormal(CurrentScene.ColModel.Vertices[tri.VertIndex[0]], CurrentScene.ColModel.Vertices[tri.VertIndex[1]], CurrentScene.ColModel.Vertices[tri.VertIndex[2]]);

                            double RotY = (Math.Atan2(normal.X, normal.Z));

                            //Console.WriteLine("Rot Y rad: (" + doorcount + ")" + RotYRad);
                            Vector3d truepos;

                            float dist;
                            byte[] roomid = new byte[] { 0, 0 };
                            int roomcount;

                            for (int i = 0; i < 2; i++)
                            {
                                truepos = newpos;

                                truepos.X -= (((float)Math.Sin(RotY) * 100f)) * (1 - i * 2);
                                truepos.Z -= (((float)Math.Cos(RotY) * 100f)) * (1 - i * 2);
                                truepos.Y += 1;

                                dist = 99999;
                                roomcount = 0;
                                foreach (ZScene.ZRoom Room in MainForm.CurrentScene.Rooms)
                                {
                                    foreach (ObjFile.Group Group in Room.TrueGroups)
                                    {
                                        foreach (ObjFile.Triangle Tri in Group.Triangles)
                                        {
                                            Vector3 collision = ObjFile.RayCollision(
                                                Room.ObjModel.Vertices[Tri.VertIndex[0]],
                                                Room.ObjModel.Vertices[Tri.VertIndex[1]],
                                                Room.ObjModel.Vertices[Tri.VertIndex[2]],
                                                (Vector3)truepos,
                                                new Vector3((float)truepos.X, (float)truepos.Y - 30000, (float)truepos.Z),
                                                MainForm.CurrentScene.Scale);
                                            if (collision != new Vector3(-1, -1, -1) && dist > MainForm.Distance3D((Vector3)truepos, collision))
                                            {
                                                dist = MainForm.Distance3D((Vector3)truepos, collision);
                                                roomid[i] = (byte)roomcount;
                                            }
                                        }
                                    }
                                    roomcount++;
                                }
                            }

                            if (name.Contains("#door")) CurrentScene.Transitions.Add(new ZActor(roomid[0], 0xFF, roomid[1], 0xFF, 0x002E, (short)newpos.X, (short)newpos.Y, (short)newpos.Z, (short)((Math.Atan2(normal.X, normal.Z) * 180 / Math.PI) * 182.04444444444444444444444444444f), 0));
                            else if (name.Contains("#blackplane")) CurrentScene.Transitions.Add(new ZActor(roomid[0], 0xFF, roomid[1], 0xFF, 0x0023, (short)newpos.X, (short)newpos.Y, (short)newpos.Z, (short)((Math.Atan2(normal.X, normal.Z) * 180 / Math.PI) * 182.04444444444444444444444444444f), 0));

                            doorcount++;

                        }
                    }
                }


            }

            if (doorcount == 0) MessageBox.Show("No doors has been added, are you using the #Door tag in your collision file? (or you pressed this option 2 times)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else MessageBox.Show("Doors added: " + doorcount + "!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            actorEditControl2.UpdateActorEdit();
            actorEditControl2.UpdateForm();
            UpdateForm();
        }

        private void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            Object target = null;
            if (actorpick == 1)
                target = CurrentScene.Rooms[RoomList.SelectedIndex].ZActors[actorEditControl1.ActorNumber];
            else if (actorpick == 2)
                target = CurrentScene.Transitions[actorEditControl2.ActorNumber];
            else if (actorpick == 3)
                target = CurrentScene.SpawnPoints[actorEditControl3.ActorNumber];
            else if (actorpick == 5)
                target = CurrentScene.Waterboxes[(int)WaterboxSelect.Value];

            if (actorpick <= 3 && target != null)
            {
                float newrot = ((ZActor)target).YRot + (e.Delta * 4);
                if (newrot < -32767) newrot = 32767;
                else if (newrot > 32767) newrot = -32767;
                ((ZActor)target).YRot = newrot;

                actorEditControl1.UpdateActorEdit();
                actorEditControl1.UpdateForm();
                actorEditControl2.UpdateActorEdit();
                actorEditControl2.UpdateForm();
                actorEditControl3.UpdateActorEdit();
                actorEditControl3.UpdateForm();
            }
            else if (actorpick == 5)
            {
                int incr = (e.Delta > 0) ? 20 : -20;
                float newXsize = ((ZWaterbox)target).XSize + incr, newZsize = ((ZWaterbox)target).ZSize + incr;
                if (newXsize < 32767 && newXsize > -32767 && newZsize < 32767 && newZsize > -32767)
                {
                    ((ZWaterbox)target).XSize += incr;
                    ((ZWaterbox)target).ZSize += incr;

                    ((ZWaterbox)target).XPos -= incr / 2;
                    ((ZWaterbox)target).ZPos -= incr / 2;

                    UpdateForm();
                }
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void majorasMaskModeexperimentalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.MajorasMask = majorasMaskModeexperimentalToolStripMenuItem.Checked;

            ReloadXMLs();

            RefreshActorCache();

            RefreshObjectCache();

            UpdateForm();
        }


        private void replaceSceneTitleCardTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScene != null && !titlecardeditor_visible)
            {


                // TitleCardReplacer titlecard = new TitleCardReplacer((ushort)CurrentScene.SceneNumber);

                //titlecard.Show();
                // titlecardeditor_visible = true;
            }


        }

        private void AddCameraButton_Click(object sender, EventArgs e)
        {
            if (CurrentScene.ColModel == null) return;

            if (Control.ModifierKeys == Keys.Shift)
            {
                //create in front of the camera

                double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
                double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

                Vector3d truepos = GetTrueCameraPosition();

                if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
                {
                    truepos.Y -= (float)Math.Sin(RotXRad);
                }
                else
                {
                    truepos.X += (float)Math.Sin(RotYRad);
                    truepos.Z -= (float)Math.Cos(RotYRad);
                    truepos.Y -= (float)Math.Sin(RotXRad);
                }

                truepos.X = Clamp(truepos.X, -32767, 32767);
                truepos.Y = Clamp(truepos.Y, -32767, 32767);
                truepos.Z = Clamp(truepos.Z, -32767, 32767);


                CurrentScene.Cameras.Add(new ZCamera((short)truepos.X, (short)truepos.Y, (short)truepos.Z, (short)(Camera.Rot.X * 182.04444444444444444444444444444f), (short)(-(Camera.Rot.Y - 180) * 182.04444444444444444444444444444f), (short)(Camera.Rot.Z * 182.04444444444444444444444444444f), 0, 0x2D, (ushort)0xFFFF, (ushort)0xFFFF));
                UpdateForm();
            }
            else
            {

                OpenTK.Vector3d MinCoordinate = new OpenTK.Vector3d(0, 0, 0);
                OpenTK.Vector3d MaxCoordinate = new OpenTK.Vector3d(0, 0, 0);

                foreach (ObjFile.Vertex Vtx in CurrentScene.ColModel.Vertices)
                {
                    /* Minimum... */
                    MinCoordinate.X = Math.Min(MinCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MinCoordinate.Y = Math.Min(MinCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MinCoordinate.Z = Math.Min(MinCoordinate.Z, Vtx.Z * CurrentScene.Scale);

                    /* Maximum... */
                    MaxCoordinate.X = Math.Max(MaxCoordinate.X, Vtx.X * CurrentScene.Scale);
                    MaxCoordinate.Y = Math.Max(MaxCoordinate.Y, Vtx.Y * CurrentScene.Scale);
                    MaxCoordinate.Z = Math.Max(MaxCoordinate.Z, Vtx.Z * CurrentScene.Scale);
                }

                CurrentScene.Cameras.Add(new ZCamera((short)(MaxCoordinate.X - MinCoordinate.X), (short)(MaxCoordinate.Y - MinCoordinate.Y), (short)(MaxCoordinate.Z - MinCoordinate.Z), 0, 0, 0, 0, 0x2D, 0xFFFF, 0xFFFF));
            }

            UpdateForm();
            Helpers.SelectAdd(CameraSelect, CurrentScene.Cameras);
        }

        private void DeleteCameraButton_Click(object sender, EventArgs e)
        {
            CurrentScene.Cameras.RemoveAt((int)CameraSelect.Value);
            UpdateForm();
        }

        private void CameraSelect_ValueChanged(object sender, EventArgs e)
        {
            UpdateForm();

            if (previewscenecamera)
            {
                Camera.Pos = ConvertToCameraPosition((Vector3d)CurrentScene.Cameras[(int)CameraSelect.Value].Position);
                Camera.Rot.X = CurrentScene.Cameras[(int)CameraSelect.Value].XRot / 182.04444444444444444444444444444f;
                Camera.Rot.Y = -CurrentScene.Cameras[(int)CameraSelect.Value].YRot / 182.04444444444444444444444444444f + 180;
                Camera.Rot.Z = CurrentScene.Cameras[(int)CameraSelect.Value].ZRot / 182.04444444444444444444444444444f;
            }
        }

        private void CameraPos_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cameras[(int)CameraSelect.Value].XPos = (short)CameraXPos.Value;
            CurrentScene.Cameras[(int)CameraSelect.Value].ZPos = (short)CameraZPos.Value;
            CurrentScene.Cameras[(int)CameraSelect.Value].YPos = (short)CameraYPos.Value;

            UpdateCameraEdit();
        }

        private void CameraRot_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cameras[(int)CameraSelect.Value].XRot = (short)CameraXRot.Value;
            CurrentScene.Cameras[(int)CameraSelect.Value].YRot = (short)CameraYRot.Value;
            CurrentScene.Cameras[(int)CameraSelect.Value].ZRot = (short)CameraZRot.Value;

            UpdateCameraEdit();
        }

        private void CameraFov_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && short.MaxValue - Math.Abs(CameraFov.Value) > 20)
                CameraFov.Value += (CameraFov.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Fov) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Fov = (short)CameraFov.Value;

            UpdateCameraEdit();
        }

        private void PrerenderedCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LoadJFIF_Click(object sender, EventArgs e)
        {
            if (CurrentScene.ColModel == null) return;
            openFileDialog1.FileName = "";

            openFileDialog1.Filter = "JPEG image / JFIF image (*.jpeg;*.jpg;*.jfif)|*.jpeg;*.jpg;*.jfif|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            string temppath = "", fullpath, temppath2 = "";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CurrentScene.prerenderimages.Add(openFileDialog1.FileName);
                UpdatePrerenders();
            }
        }

        private void GroupMultitextureAlpha_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).MultiTexAlpha = (uint)(0 | ((byte)GroupMultitextureAlpha.Value << 24));

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.MultiTexAlpha[Index] = ((ObjFile.Group)GroupList.SelectedItem).MultiTexAlpha;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void CameraType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentScene.Cameras[(int)CameraSelect.Value].Type = Convert.ToByte((CameraType.SelectedItem as SongItem).Value.ToString());
            UpdateCameraEdit();
        }

        public Vector3d GetTrueCameraPosition()
        {

            /*
            Matrix4d modelViewMatrix;

            GL.GetDouble(GetPName.ModelviewMatrix, out modelViewMatrix);

            modelViewMatrix = Matrix4d.Transpose(modelViewMatrix);
            
            Vector3d n1 = new Vector3d(modelViewMatrix.Row0.X, modelViewMatrix.Row0.Y, modelViewMatrix.Row0.Z);
            Vector3d n2 = new Vector3d(modelViewMatrix.Row1.X, modelViewMatrix.Row1.Y, modelViewMatrix.Row1.Z);
            Vector3d n3 = new Vector3d(modelViewMatrix.Row2.X, modelViewMatrix.Row2.Y, modelViewMatrix.Row2.Z);

            // Get plane distances
            double d1 = (modelViewMatrix.Row0.W);
            double d2 = (modelViewMatrix.Row1.W);
            double d3 = (modelViewMatrix.Row2.W);

            // Get the intersection of these 3 planes
            Vector3d n2n3 = Vector3d.Cross(n2, n3);
            Vector3d n3n1 = Vector3d.Cross(n3, n1);
            Vector3d n1n2 = Vector3d.Cross(n1, n2);

            Vector3d top = (n2n3 * d1) + (n3n1 * d2) + (n1n2 * d3);
            double denom = Vector3d.Dot(n1, n2n3);

            Vector3d result = top / -denom;*/

            Vector3d result = new Vector3d(-(Camera.Pos.X * 200), -(Camera.Pos.Y * 200), -(Camera.Pos.Z * 200));

            return result;
        }

        public static Vector3d ConvertToCameraPosition(Vector3d vector)
        {
            return new Vector3d(-(vector.X / 200), -(vector.Y / 200), -(vector.Z / 200));
        }



        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        private void AdjustWidthComboBox_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (SongItem item in ((ComboBox)sender).Items)
            {
                string s = Convert.ToString(item.Text);

                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void AdjustWidthMaterials_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (ObjFile.Material item in ((ComboBox)sender).Items)
            {
                string s = Convert.ToString(item.DisplayName);

                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void SaveScenetoolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Console.WriteLine(LastScene);
            SaveScene(LastScene);
        }

        private void autosaveSceneXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.AutoSave = autosaveSceneXmlToolStripMenuItem.Checked;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void SetSceneHeader(int id)
        {
            if (NormalHeader == null) NormalHeader = CurrentScene;
            // Console.WriteLine("cloneid: " + CurrentScene.cloneid + " Normal header id: " + NormalHeader.cloneid);



            if (id > 0)
            {
                SetSceneHeader(0);
                NormalHeader = CurrentScene;
                CurrentScene = NormalHeader.SceneHeaders[id - 1].Scene;
                TransferSceneValues(NormalHeader, CurrentScene);
                undo.Clear();
                redo.Clear();

            }
            else
            {
                int tmpid = CurrentScene.cloneid;
                if (tmpid == 0) return;
                TransferSceneValues(CurrentScene, NormalHeader);
                // NormalHeader.SceneHeaders[tmpid - 1].Scene = CurrentScene;
                CurrentScene = NormalHeader;
                undo.Clear();
                redo.Clear();
            }
        }

        public void TransferSceneValues(ZScene source, ZScene target)
        {
            target.Name = source.Name;
            target.Scale = source.Scale;
            target.InjectOffset = source.InjectOffset;
            target.SceneNumber = source.SceneNumber;
            target.ElfMessage = source.ElfMessage;
            target.SpecialObject = source.SpecialObject;
            target.SceneSettings = source.SceneSettings;
            target.NewRoomMode = source.NewRoomMode;
            target.Prerendered = source.Prerendered;
            //target.JFIFpath = source.JFIFpath;
        }

        public void ResetAlternateRooms()
        {
            foreach (ZSceneHeader header in NormalHeader.SceneHeaders)
            {
                header.Scene.Rooms = NormalHeader.Rooms.ConvertAll(x => (x.Clone()));
            }
        }

        private void AddSceneHeaderButton_Click(object sender, EventArgs e)
        {
            SceneHeaderSelector.Value = 0;
            SetSceneHeader(0);

            ZScene clone = CurrentScene.Clone();
            clone.cloneid = CurrentScene.SceneHeaders.Count + 1;
            CurrentScene.SceneHeaders.Add(new ZSceneHeader(false, clone));

            UpdateAlternateSceneHeaders();

            SceneHeaderList.Value = CurrentScene.SceneHeaders.Count;
            actorEditControl1.SetActors(ref NormalHeader.Rooms[RoomList.SelectedIndex].ZActors);
            actorEditControl1.UpdateActorEdit();
            actorEditControl2.SetActors(ref NormalHeader.Transitions);
            actorEditControl2.UpdateActorEdit();
            actorEditControl3.SetActors(ref NormalHeader.SpawnPoints);
            actorEditControl3.UpdateActorEdit();
            UpdateEnvironmentData();
            UpdateForm();
        }

        private void DeleteSceneHeaderButton_Click(object sender, EventArgs e)
        {
            int id = (int)SceneHeaderSelector.Value;



            SceneHeaderSelector.Value = 0;
            SetSceneHeader(0);

            for (int i = 0; i < CurrentScene.SceneHeaders.Count; i++)
            {
                if (CurrentScene.SceneHeaders[i]._CloneFromHeader == id)
                {
                    CurrentScene.SceneHeaders[i]._CloneFromHeader = 0;
                }
                if (CurrentScene.SceneHeaders[i]._CloneFromHeader > id)
                {
                    CurrentScene.SceneHeaders[i]._CloneFromHeader -= 1;
                }
            }

            CurrentScene.SceneHeaders.RemoveAt((int)SceneHeaderList.Value - 1);
            UpdateAlternateSceneHeaders();
        }

        private void SceneHeaderSameCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            NormalHeader.SceneHeaders[(int)SceneHeaderList.Value - 1].SameAsPrevious = SceneHeaderSameCheckbox.Checked;
            UpdateAlternateSceneHeaders();
        }

        private void SceneHeaderList_ValueChanged(object sender, EventArgs e)
        {
            UpdateAlternateSceneHeaders();
        }

        private void LaunchRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(LastInject))
            {
                try
                {
                    System.Diagnostics.Process.Start(LastInject);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Seems that there's no emulator associated with this extension. Assign one and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("There's no ROM to launch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ReloadRoomButton_Click(object sender, EventArgs e)
        {
            bool rereload = false;

            if (customcombiner != null) customcombiner.Close();

            if (!File.Exists(CurrentScene.Rooms[RoomList.SelectedIndex].ModelFilename) && Control.ModifierKeys != Keys.Shift)
            {
                MessageBox.Show("Room file no longer exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(CurrentScene.CollisionFilename))
            {
                MessageBox.Show("Collision file no longer exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (Control.ModifierKeys == Keys.Shift)
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Wavefront .obj / Collada .dae (*.obj;*.dae)|*.obj;*.dae|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;


                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (CurrentScene.NewRoomMode == false)
                    {
                        CurrentScene.Rooms[RoomList.SelectedIndex].ModelFilename = openFileDialog1.FileName;
                    }
                    else
                    {
                        foreach (ZScene.ZRoom room in CurrentScene.Rooms)
                        {
                            room.ModelFilename = openFileDialog1.FileName;
                        }
                    }
                }
                else
                {
                    return;
                }

            }

            CurrentScene.ColModel = new ObjFile(CurrentScene.CollisionFilename, true);

            if (CurrentScene.NewRoomMode == false)
            {
                CurrentScene.AddRoom(CurrentScene.Rooms[RoomList.SelectedIndex].ModelFilename);
                // ZScene.ZRoom.ZGroupSettings PreviousSettings = new Dictionary<string, ZScene.ZRoom.ZGroupSettings>();
                ObjFile obj = CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].ObjModel.Clone();
                ZScene.ZRoom room = CurrentScene.Rooms[CurrentScene.Rooms.Count - 1].Clone();

                cleargroupsettings(room);

                List<int> restored = new List<int>();

                for (int i = 0; i < room.TrueGroups.Count; i++)
                {
                    for (int y = 0; y < CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.Count; y++)
                    {
                        if (room.TrueGroups[i].Name == CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups[y].Name && !restored.Contains(i))
                        {
                            room.GroupSettings.CopyVals(i, CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings, y);
                            restored.Add(i);
                        }
                    }
                    if (!restored.Contains(i))
                    {
                        string name1 = room.TrueGroups[i].Name;
                        if (name1.Contains("#")) name1 = name1.Split('#')[0];

                        if (name1.Length > 0)
                            for (int y = 0; y < CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.Count; y++)
                            {
                                string name2 = room.TrueGroups[y].Name;
                                if (name2.Contains("#")) name2 = name2.Split('#')[0];

                                if ((name2.Length > 0 && name1 == name2 && !restored.Contains(i)))
                                {
                                    room.GroupSettings.CopyVals(i, CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings, y);
                                    restored.Add(i);
                                }
                            }
                    }

                    CurrentScene.ApplyMeshTags(room.TrueGroups[i], room, i);
                }
                CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel = obj;
                CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups = room.TrueGroups;
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings = room.GroupSettings;
                CurrentScene.Rooms.RemoveAt(CurrentScene.Rooms.Count - 1);

                SetTrueGroupsToGroupSettings(RoomList.SelectedIndex);

                if (CurrentScene.Rooms.Count > 0)
                {
                    RoomList.SelectedIndex = 0;

                    SelectRoom(0);
                    UpdateGroupSelect();
                    GroupList.SelectedItem = GroupList.Items[0];
                }
                UpdateForm();

            }
            else
            {
                int counter = CurrentScene.Rooms.Count;
                int testcounter = CurrentScene.Rooms.Count + 1;
                CurrentScene.AddRoom(CurrentScene.Rooms[RoomList.SelectedIndex].ModelFilename, " (Room 0)", counter);
                int maxroom = 1;
                int tmp = 0;
                int loops = 0;
                foreach (ObjFile.Group group in CurrentScene.Rooms[counter].ObjModel.Groups)
                {
                    group.Name = group.Name.Replace("TAG_", "#");

                    if (group.Name.ToLower().Contains("#room"))
                    {

                        string s = group.Name.Substring(group.Name.ToLower().IndexOf("#room") + 5);



                        if (s.Contains("#"))
                            s = s.Substring(0, s.IndexOf("#"));


                        // tmp = Convert.ToInt32(s);

                        if (!Int32.TryParse(s, out tmp))
                        {
                            MessageBox.Show("Bad usage of #Room tag. The tag needs to be at the end of the group name or before another tag.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (tmp + 1 > maxroom) maxroom = tmp + 1;

                    }
                }
                int addedrooms = 0;

                if (maxroom > 0)
                {
                    for (int i = 0; i < maxroom; i++)
                    {
                        //    Console.WriteLine("fffffffffff");
                        CurrentScene.AddRoom(CurrentScene.Rooms[0].ModelFilename, " (Room " + (i + 1) + ")", counter);
                        addedrooms++;
                        ZScene.ZRoom room = CurrentScene.Rooms[counter + i].Clone();
                        ObjFile obj = CurrentScene.Rooms[counter + i].ObjModel.Clone();

                        cleargroupsettings(room);

                        /*


                        for (int w = 0; w < room.TrueGroups.Count; w++)
                        {
                            for (int y = 0; y < CurrentScene.Rooms[i].TrueGroups.Count; y++)
                            {
                                if (room.TrueGroups[w].Name == CurrentScene.Rooms[i].TrueGroups[y].Name)
                                {
                      
                                    room.GroupSettings.CopyVals(w, CurrentScene.Rooms[i].GroupSettings, y);
                                }
                            }

                            CurrentScene.ApplyMeshTags(room.TrueGroups[w], room, w);
                        }
                        */

                        List<int> restored = new List<int>();

                        for (int w = 0; w < room.TrueGroups.Count; w++)
                        {
                            for (int y = 0; y < CurrentScene.Rooms[i].TrueGroups.Count; y++)
                            {
                                if (room.TrueGroups[w].Name == CurrentScene.Rooms[i].TrueGroups[y].Name && !restored.Contains(w))
                                {
                                    room.GroupSettings.CopyVals(w, CurrentScene.Rooms[i].GroupSettings, y);
                                    restored.Add(w);
                                }
                            }
                            if (!restored.Contains(w))
                            {
                                string name1 = room.TrueGroups[w].Name;
                                if (name1.Contains("#")) name1 = name1.Split('#')[0];

                                if (name1.Length > 0)
                                    for (int y = 0; y < CurrentScene.Rooms[i].TrueGroups.Count; y++)
                                    {
                                        string name2 = CurrentScene.Rooms[i].TrueGroups[y].Name;
                                        if (name2.Contains("#")) name2 = name2.Split('#')[0];

                                        if ((name2.Length > 0 && name1 == name2 && !restored.Contains(w)))
                                        {
                                            room.GroupSettings.CopyVals(w, CurrentScene.Rooms[i].GroupSettings, y);
                                            restored.Add(w);
                                        }
                                    }
                            }

                            CurrentScene.ApplyMeshTags(room.TrueGroups[w], room, w);
                        }



                        CurrentScene.Rooms[i].ObjModel = obj;
                        CurrentScene.Rooms[i].TrueGroups = room.TrueGroups;
                        CurrentScene.Rooms[i].GroupSettings = room.GroupSettings;

                        SetTrueGroupsToGroupSettings(i);
                    }
                    addedrooms = addedrooms - counter;
                    counter += addedrooms;
                    if (addedrooms < 0) addedrooms = 0;
                    if (addedrooms > 0) rereload = true;
                    while (CurrentScene.Rooms.Count > counter)
                        CurrentScene.Rooms.RemoveAt(CurrentScene.Rooms.Count - 1 - addedrooms);
                }
                ((CurrencyManager)RoomList.BindingContext[CurrentScene.Rooms]).Refresh();

                if (CurrentScene.Rooms.Count > 0)
                {
                    RoomList.SelectedIndex = 0;
                    GroupList.SelectedIndex = 0;
                    SelectRoom(0);
                    UpdateGroupSelect();
                }
                if (CurrentScene.Rooms.Count > counter && NormalHeader.SceneHeaders.Count > 0) ResetAlternateRooms();
                if (!rereload) UpdateForm();
            }

            //((CurrencyManager)RoomList.BindingContext[CurrentScene.Rooms]).Refresh();

            if (CurrentScene.ColModel.Vertices.Count > 0x1FFF)
            {
                MessageBox.Show("This collision mesh has more than 8191 vertex! this is going to crash the game, try reducing triangle count.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            if (rereload) ReloadRoomButton_Click(sender, e);

            else
            {
                if (SimulateN64Gfx == true && CurrentScene.ColModel != null)
                    CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
            }

        }

        private void AdjustWidthAnimationBox_DropDown(object sender, System.EventArgs e)
        {

            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;

            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (AnimationItem item in ((ComboBox)sender).Items)
            {
                string s = Convert.ToString(item.Text);

                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void dIsplayUndocumentedCutsceneVarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.UndocumentedCutsceneVars = dIsplayUndocumentedCutsceneVarsToolStripMenuItem.Checked;
        }

        private void exportAszobjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count <= 0) return;
            saveFileDialog1.CheckFileExists = false;
            //    saveFileDialog1.FileName = Path.GetFileName(OldFileName) + "_new";
            saveFileDialog1.Filter = "Zobj file (*.zobj)|*.zobj|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    bool previoussetting = settings.DListCulling;
                    int bank = Convert.ToInt32(((ToolStripMenuItem)sender).Text.Substring(7, 2));
                    settings.DListCulling = false;
                    List<Byte> Data = new List<byte>();
                    List<Byte> TextureData = new List<byte>();
                    List<NTexture> Textures = new List<NTexture>();

                    ZScene.ZRoom Room = CurrentScene.Rooms[RoomList.SelectedIndex];

                    exportingZobj = true;

                    if (settings.EmptySpace > 0)
                    {
                        InjectMessages.Add("Adding empty space at the begining: " + settings.EmptySpace.ToString("X"));
                        Data.AddRange(new byte[settings.EmptySpace]);
                    }
                    CurrentScene.textureoffsets.Clear();
                    CurrentScene.paletteoffsets.Clear();
                    CurrentScene.GenerateTextures(Room, Data, Textures);

                    if (Data.Count <= settings.EmptySpace)
                    {
                        InjectMessages.Add("Textures were not written, this is a random bug, try to save and reopen the scene xml.");
                    }

                    string Game = (!settings.MajorasMask) ? "OOT" : "MM";

                    if (settings.WriteCollisionZobj)
                    {
                        InjectMessages.Add("Collision Offset " + Data.Count.ToString("X8"));
                        CurrentScene.WriteSceneCollision(Data, (byte)bank, true, Game);
                    }

                    for (int j = 0; j < Room.TrueGroups.Count; j++)
                    {
                        if (Room.TrueGroups[j].Name.ToLower().Contains("#nomesh")) continue;
                        NDisplayList DList = new NDisplayList(CurrentScene.Scale, Room.TrueGroups[j].TintAlpha, Room.TrueGroups[j].MultiTexAlpha, 1.0f, UnusedCommandCheckBox.Checked, Room.TrueGroups[j].BackfaceCulling, Room.TrueGroups[j].Animated, Room.TrueGroups[j].Metallic, Room.TrueGroups[j].Decal, Room.TrueGroups[j].Pixelated, Room.TrueGroups[j].Billboard, Room.TrueGroups[j].TwoAxisBillboard, Room.TrueGroups[j].IgnoreFog, Room.TrueGroups[j].SmoothRGBAEdges, Room.TrueGroups[j].EnvColor, Room.TrueGroups[j].AlphaMask, Room.TrueGroups[j].RenderLast, Room.TrueGroups[j].VertexNormals, Room.AffectedByPointLight, Room.TrueGroups[j].AnimationBank, bank);
                        DList.Convert(Room.ObjModel, Room.TrueGroups[j], Textures, (uint)Data.Count, CurrentScene.SceneSettings, CurrentScene.AdditionalTextures);
                        InjectMessages.Add("Group " + Room.TrueGroups[j].Name + " Offset " + (Data.Count + DList.Vertoffset).ToString("X8"));
                        Data.AddRange(DList.Data);

                    }
                    File.WriteAllBytes(saveFileDialog1.FileName, Data.ToArray());

                    settings.DListCulling = previoussetting;

                    string outputmsg = "";
                    while (InjectMessages.Count != 0)
                    {
                        outputmsg += InjectMessages[0] + "\n";
                        InjectMessages.RemoveAt(0);
                    }

                    MessageBox.Show("Done! \n" + outputmsg + "\nFile Size: " + Data.Count.ToString("X") + " bytes\nClose this window to copy this to your clipboard", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clipboard.SetText(outputmsg);

                    exportingZobj = false;
                }
            }
        }

        private void exportCutsceneRawDataToolStripMenuItem_DisplayStyleChanged(object sender, EventArgs e)
        {

            if (CurrentScene.Cutscene.Count <= 0) return;
            saveFileDialog1.CheckFileExists = false;
            //    saveFileDialog1.FileName = Path.GetFileName(OldFileName) + "_new";
            saveFileDialog1.Filter = "Cutscene file (*.bin)|*.bin|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    List<Byte> CutsceneData = new List<byte>();
                    CurrentScene.GenerateCutsceneData(CutsceneData, settings.MajorasMask ? "MM" : "OOT");
                    File.WriteAllBytes(saveFileDialog1.FileName, CutsceneData.ToArray());
                    MessageBox.Show("Done! File Size: " + CutsceneData.Count.ToString("X") + " bytes", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }


        private void CutsceneActorXRot_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && short.MaxValue - Math.Abs(CutsceneActorXRot.Value) > 1820)
                CutsceneActorXRot.Value += (CutsceneActorXRot.Value - (decimal)CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Rotation.X) * 9;
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Rotation = new Vector3((float)CutsceneActorXRot.Value, (float)CutsceneActorYRot.Value, (float)CutsceneActorZRot.Value);

            UpdateCutsceneEdit();
        }

        private void CutsceneActorYRot_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && short.MaxValue - Math.Abs(CutsceneActorYRot.Value) > 1820)
                CutsceneActorYRot.Value += (CutsceneActorYRot.Value - (decimal)CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Rotation.Y) * 9;
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Rotation = new Vector3((float)CutsceneActorXRot.Value, (float)CutsceneActorYRot.Value, (float)CutsceneActorZRot.Value);

            UpdateCutsceneEdit();
        }

        private void CutsceneActorZRot_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && short.MaxValue - Math.Abs(CutsceneActorZRot.Value) > 1820)
                CutsceneActorZRot.Value += (CutsceneActorZRot.Value - (decimal)CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Rotation.Z) * 9;
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex].Rotation = new Vector3((float)CutsceneActorXRot.Value, (float)CutsceneActorYRot.Value, (float)CutsceneActorZRot.Value);

            UpdateCutsceneEdit();
        }

        private void CutsceneSetTimeHours_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[0] = (ushort)CutsceneSetTimeHours.Value;
            UpdateCutsceneEdit();
        }

        private void CutsceneSetTimeMinutes_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Data[1] = (ushort)CutsceneSetTimeMinutes.Value;
            UpdateCutsceneEdit();
        }

        private void restrictionFlagsTableEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!restrictionflag_visible)
            {
                if (!settings.MajorasMask && CurrentScene != null)
                    MessageBox.Show("To edit the restriction flags of the current scene, use the button in General tab!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RestrictionFlagEditor restrictionflagseditor = new RestrictionFlagEditor();

                restrictionflagseditor.Show();
                restrictionflag_visible = true;


            }
        }

        private void AddAdditionalTexture_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openSceneToolStripMenuItem.Owner.Hide();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AdditionalTexture_Add(openFileDialog1.FileName);
            }

        }

        private void AdditionalTexture_Add(string filename)
        {
            ObjFile.Material mat = new ObjFile.Material();

            try
            {
                if (ObjFile.ValidImageTypes.IndexOf(Path.GetExtension(filename).ToLowerInvariant()) == -1) throw new Exception();

                if (Path.GetExtension(filename).ToLowerInvariant() == ".tga")
                {
                    if (!File.Exists(Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + ".png"))
                    {
                        String pdetail = @"/c ndec\tga2png.exe -i " + "\"" + filename + "\"" + " -o " + "\"" + Path.GetDirectoryName(filename) + "\\\"";
                        // Console.WriteLine(Path.GetDirectoryName(LoadPath) + Path.GetFileNameWithoutExtension(LoadPath) + ".png");
                        ProcessStartInfo pcmd = new ProcessStartInfo("cmd.exe");
                        pcmd.Arguments = pdetail;

                        Process cmd = Process.Start(pcmd);
                        cmd.WaitForExit();


                    }
                    //   Console.WriteLine(Path.GetDirectoryName(LoadPath) + "\\" + Path.GetFileNameWithoutExtension(LoadPath) + ".png");
                    mat.TexImage = ObjFile.BitmapFromFile(Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(filename) + ".png");
                    mat.map_Kd = Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(filename) + ".png";
                }
                else if (Path.GetExtension(filename).ToLowerInvariant() == ".gif")
                {
                    Image IMG = Image.FromFile(filename);


                    int Length = IMG.GetFrameCount(FrameDimension.Time);

                    if (Length == 0)
                    {
                        MessageBox.Show("Gif corrupted!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Image[] frames = new Image[Length];

                    for (int i = 0; i < Length; i++)
                    {
                        mat = new ObjFile.Material();

                        IMG.SelectActiveFrame(FrameDimension.Time, i);

                        string framefilename = "Import\\" + Path.GetFileNameWithoutExtension(filename) + "#Frame" + i + ".png";

                        if (File.Exists(framefilename))
                            File.Delete(framefilename);

                        frames[i] = ((Image)IMG.Clone());

                        frames[i].Save(framefilename);

                        mat.TexImage = new Bitmap(Bitmap.FromFile(framefilename));
                        mat.map_Kd = framefilename;
                        mat.GLID = TexUtil.CreateTextureFromBitmap(mat.TexImage);
                        mat.Width = mat.TexImage.Width;
                        mat.Height = mat.TexImage.Height;
                        mat.Name = Path.GetFileName(mat.map_Kd);

                        if (CurrentScene.AdditionalTextures.Find(x => x.map_Kd == framefilename) != null)
                        {
                            CurrentScene.AdditionalTextures[CurrentScene.AdditionalTextures.FindIndex(x => x.map_Kd == framefilename)] = mat;
                        }
                        else
                            CurrentScene.AdditionalTextures.Add(mat);

                    }
                    UpdateAdditionalTextures();
                    AdditionalTextureList.Value = AdditionalTextureList.Maximum;
                    UpdateGroupSelect();

                    return;
                }

                else
                {
                    mat.TexImage = new Bitmap(Bitmap.FromFile(filename));
                    mat.map_Kd = filename;
                }
                mat.GLID = TexUtil.CreateTextureFromBitmap(mat.TexImage);
                mat.Width = mat.TexImage.Width;
                mat.Height = mat.TexImage.Height;
                mat.Name = Path.GetFileName(mat.map_Kd);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Texture image " + filename + " not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Texture image " + filename + " has incorrect format and cannot be loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CurrentScene.AdditionalTextures.Add(mat);
            UpdateAdditionalTextures();
            AdditionalTextureList.Value = AdditionalTextureList.Maximum;
            UpdateGroupSelect();
        }

        private void DeleteAdditionalTexture_Click(object sender, EventArgs e)
        {

            // if (((ObjFile.Group)GroupList.SelectedItem).MultiTexMaterial = MultiTextureComboBox.SelectedIndex - 1; ;

            foreach (ZScene.ZRoom room in CurrentScene.Rooms)
            {
                foreach (ObjFile.Group grp in room.TrueGroups)
                {
                    if (grp.MultiTexMaterial >= CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Materials.Count + AdditionalTextureList.Value - 1)
                        grp.MultiTexMaterial -= 1;

                }
            }

            UpdateGroupSelect();
            CurrentScene.AdditionalTextures.RemoveAt((int)AdditionalTextureList.Value - 1);
            UpdateAdditionalTextures();
            UpdateGroupSelect(n64refresh);
        }

        private void AdditionalTextureList_ValueChanged(object sender, EventArgs e)
        {
            UpdateAdditionalTextures();
        }

        private void ScenenumberTextbox_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.SceneNumber = (int)ScenenumberTextbox.Value;

                if (AutoInjectOffsetCheckBox.Checked)
                {
                    CurrentScene.InjectOffset = (int)(0x02000000 + (ScenenumberTextbox.Value * 0x4A000));
                    InjectoffsetTextbox.Text = (CurrentScene.InjectOffset).ToString("X8");
                }

                UpdateForm();
            }
        }

        private void noDummyPointsInCutsceneCamerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.NoDummyPoints = noDummyPointsInCutsceneCamerasToolStripMenuItem.Checked;
        }

        private bool importEnvironments(List<byte> data, int i)
        {
            uint test = Helpers.Read32(data, i);
            if ((test & 0xFF00FFFF) == 0x0F000000)
            {
                int lightcount = data[i + 1];
                int offset = (int)Helpers.Read24(data, i + 5);
                CurrentScene.Environments.Clear();

                for (i = offset; i < offset + (22 * lightcount); i += 22)
                {
                    Color lightA, lightB, lightC, lightD, lightE, fogcol;
                    ushort fogunknown, fogdist, drawdist;

                    lightA = Color.FromArgb(data[i], data[i + 1], data[i + 2]);
                    lightB = Color.FromArgb(data[i + 3], data[i + 4], data[i + 5]);
                    lightC = Color.FromArgb(data[i + 6], data[i + 7], data[i + 8]);
                    lightD = Color.FromArgb(data[i + 9], data[i + 10], data[i + 11]);
                    lightE = Color.FromArgb(data[i + 12], data[i + 13], data[i + 14]);
                    fogcol = Color.FromArgb(data[i + 15], data[i + 16], data[i + 17]);
                    fogunknown = (ushort)((Helpers.Read16(data, i + 18) & 0xFC00) >> 10);
                    fogdist = (ushort)(Helpers.Read16(data, i + 18) & 0x03FF);
                    drawdist = Helpers.Read16(data, i + 20);


                    CurrentScene.Environments.Add(new ZEnvironment(lightA, lightB, lightC, lightD, lightE, fogcol, fogdist, drawdist, fogunknown));
                }

                if (lightcount > 0) return true;

            }
            return false;
        }
        private void importActorsObjects(List<byte> data, int i)
        {
            uint test = Helpers.Read32(data, i);
            if ((test & 0xFF00FFFF) == 0x01000000 && data[i + 4] == 0x03)
            {
                int actorcount = data[i + 1];
                int offset = (int)Helpers.Read24(data, i + 5);


                for (int ii = offset; ii < offset + (16 * actorcount); ii += 16)
                {
                    ushort num, var;

                    short xpos, ypos, zpos, xrot, yrot, zrot;

                    num = Helpers.Read16(data, ii);
                    xpos = Helpers.Read16S(data, ii + 2);
                    ypos = Helpers.Read16S(data, ii + 4);
                    zpos = Helpers.Read16S(data, ii + 6);


                    if (MainForm.settings.MajorasMask && MainForm.settings.IgnoreMMDaySystem)
                    {
                        xrot = (short)Math.Round((double)(short)(((int)Helpers.Read16S(data, ii + 8) & 0xFF80) >> 7) * 182.044403076172);
                        yrot = (short)Math.Round((double)(short)(((int)Helpers.Read16S(data, ii + 10) & 0xFF80) >> 7) * 182.044403076172);
                        zrot = (short)Math.Round((double)(short)(((int)Helpers.Read16S(data, ii + 12) & 0xFF80) >> 7) * 182.044403076172);
                    }
                    else
                    {
                        xrot = Helpers.Read16S(data, ii + 8);
                        yrot = Helpers.Read16S(data, ii + 10);
                        zrot = Helpers.Read16S(data, ii + 12);
                    }


                    var = Helpers.Read16(data, ii + 14);

                    CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.Add(new ZActor(num, xpos, ypos, zpos, xrot, yrot, zrot, var));
                }
            }
            else if ((test & 0xFF00FFFF) == 0x0B000000 && data[i + 4] == 0x03)
            {
                int objectcount = data[i + 1];
                int offset = (int)Helpers.Read24(data, i + 5);



                for (int ii = offset; ii < offset + (2 * objectcount); ii += 2)
                {
                    ushort obj;

                    obj = Helpers.Read16(data, ii);

                    CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects.Add(new ZScene.ZUShort(obj));
                }
            }

        }

        private void importEnvironmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZScene File (*.zscene)|*.zscene";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> data = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));


                for (int i = 0; i < data.Count; i += 4)
                {
                    if (i == 0 & Helpers.Read32(data, i) == 0x18000000)
                    {
                        using (PickSceneSetting pickscenesetting = new PickSceneSetting(data, 0x02))
                        {
                            if (pickscenesetting.ShowDialog() == DialogResult.OK)
                            {
                                i = pickscenesetting.resultoffset;
                                //  Console.WriteLine("result " + i.ToString("X8"));
                            }
                        }
                    }

                    if (importEnvironments(data, i))
                    {
                        MessageBox.Show("Imported: " + CurrentScene.Environments.Count, "Import", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        UpdateForm();
                        UpdateEnvironmentEdit();
                        break;
                    }


                }

            }
        }

        private void importActorsAndObjectsOfZmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count <= 0)
            {
                MessageBox.Show("You need to add rooms to your scene", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZScene File (*.zmap)|*.zmap";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> data = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));
                CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.Clear();
                CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects.Clear();

                for (int i = 0; i < data.Count; i += 4)
                {
                    if (i == 0 & Helpers.Read32(data, i) == 0x18000000)
                    {
                        using (PickSceneSetting pickscenesetting = new PickSceneSetting(data, 0x03))
                        {
                            if (pickscenesetting.ShowDialog() == DialogResult.OK)
                            {
                                i = pickscenesetting.resultoffset;
                                //  Console.WriteLine("result " + i.ToString("X8"));
                            }
                        }
                    }
                    importActorsObjects(data, i);

                    if (Helpers.Read32(data, i) == 0x14000000)
                    {
                        MessageBox.Show("Imported actors: " + CurrentScene.Rooms[RoomList.SelectedIndex].ZActors.Count + "\n" +
                            "Imported objects: " + CurrentScene.Rooms[RoomList.SelectedIndex].ZObjects.Count, "Import", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        actorEditControl1.UpdateForm();
                        actorEditControl1.UpdateActorEdit();
                        actorEditControl2.UpdateForm();
                        actorEditControl2.UpdateActorEdit();
                        actorEditControl3.UpdateForm();
                        actorEditControl3.UpdateActorEdit();
                        UpdateForm();
                        break;
                    }
                }

            }
        }

        private void SceneHeaderSelector_ValueChanged(object sender, EventArgs e)
        {
            //  Console.WriteLine("Triggered " + SceneHeaderSelector.Maximum);

            bool setvalue = false;

            int increment = (SceneHeaderSelector.Value > prevsceneheader) ? +1 : -1;

            int nextval = (int)SceneHeaderSelector.Value;

            if (nextval == 0) setvalue = true;

            while (!setvalue)
            {
                if (NormalHeader.SceneHeaders[nextval - 1].SameAsPrevious)
                {
                    nextval += increment;
                    if (nextval == 0 || nextval > NormalHeader.SceneHeaders.Count)
                    {
                        nextval = 0;
                        SceneHeaderSelector.Value = 0;
                        setvalue = true;
                    }
                }
                else
                {
                    SceneHeaderSelector.Value = nextval;
                    setvalue = true;
                }
            }

            SetSceneHeader((int)SceneHeaderSelector.Value);
            if (RoomList.SelectedIndex != -1) actorEditControl1.SetActors(ref CurrentScene.Rooms[RoomList.SelectedIndex].ZActors);
            actorEditControl1.UpdateActorEdit();
            actorEditControl2.SetActors(ref CurrentScene.Transitions);
            actorEditControl2.UpdateActorEdit();
            actorEditControl3.SetActors(ref CurrentScene.SpawnPoints);
            actorEditControl3.UpdateActorEdit();
            UpdateObjectEdit();
            UpdateEnvironmentEdit();
            UpdateForm();
        }

        private void printOffsetsOnInjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.printoffsets = printOffsetsOnInjectToolStripMenuItem.Checked;
        }

        private void tabCollision_Click(object sender, EventArgs e)
        {

        }

        private void GroupDetectionA2_CheckedChanged(object sender, EventArgs e)
        {
            if (!nocheckevent)
            {
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagA = (ushort)((0x0000 | ((GroupDetectionA2.Checked ? 1 : 0) << 13) | (GroupDetectionA4.Checked ? 1 : 0) << 14) | ((GroupDetectionA8.Checked ? 1 : 0) << 15));
                UpdateForm();
            }
        }

        private void GroupDetectionA4_CheckedChanged(object sender, EventArgs e)
        {
            if (!nocheckevent)
            {
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagA = (ushort)((0x0000 | ((GroupDetectionA2.Checked ? 1 : 0) << 13) | (GroupDetectionA4.Checked ? 1 : 0) << 14) | ((GroupDetectionA8.Checked ? 1 : 0) << 15));
                UpdateForm();
            }
        }

        private void GroupDetectionA8_CheckedChanged(object sender, EventArgs e)
        {
            if (!nocheckevent)
            {
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagA = (ushort)((0x0000 | ((GroupDetectionA2.Checked ? 1 : 0) << 13) | (GroupDetectionA4.Checked ? 1 : 0) << 14) | ((GroupDetectionA8.Checked ? 1 : 0) << 15));
                UpdateForm();
            }
        }

        private void sharpPixelatedTexturesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownEx1_ValueChanged_1(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].Unk1 = (int)PolytypeUnk1.Value;
            UpdateForm();
        }

        private void numericUpDownEx2_ValueChanged_1(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].Unk2 = (int)PolytypeUnk2.Value;
            UpdateForm();
        }

        private void GroupDetectionB2_CheckedChanged(object sender, EventArgs e)
        {
            if (!nocheckevent)
            {
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagB = (ushort)((0x0000 | ((GroupDetectionB2.Checked ? 1 : 0) << 13) | (GroupDetectionB4.Checked ? 1 : 0) << 14) | ((GroupDetectionB8.Checked ? 1 : 0) << 15));
                UpdateForm();
            }
        }

        private void GroupDetectionB4_CheckedChanged(object sender, EventArgs e)
        {
            if (!nocheckevent)
            {
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagB = (ushort)((0x0000 | ((GroupDetectionB2.Checked ? 1 : 0) << 13) | (GroupDetectionB4.Checked ? 1 : 0) << 14) | ((GroupDetectionB8.Checked ? 1 : 0) << 15));
                UpdateForm();
            }
        }

        private void GroupDetectionB8_CheckedChanged(object sender, EventArgs e)
        {
            if (!nocheckevent)
            {
                CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].PolyFlagB = (ushort)((0x0000 | ((GroupDetectionB2.Checked ? 1 : 0) << 13) | (GroupDetectionB4.Checked ? 1 : 0) << 14) | ((GroupDetectionB8.Checked ? 1 : 0) << 15));
                UpdateForm();
            }
        }

        private void KillingQuicksand2Radio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDamageSurfaceFlags();
            UpdateForm();
        }

        private void IceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDamageSurfaceFlags();
            UpdateForm();
        }

        private void triplicateCollisionBoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.TriplicateCollisionBounds = triplicateCollisionBoundsToolStripMenuItem.Checked;
        }

        private void addEmptySpaceInSceneHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (addEmptySpaceInSceneHeaderToolStripMenuItem.Checked)
            {
                string val = Interaction.InputBox("Enter amount of empty space in hex", "Empty space", "100");
                if (val != "")
                    settings.EmptySpace = Convert.ToInt32(val, 16);
                else
                {
                    addEmptySpaceInSceneHeaderToolStripMenuItem.Checked = false;
                    settings.EmptySpace = 0x00;
                }

            }
            else
                settings.EmptySpace = 0x00;
        }

        private void NoLedgeJumpRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFirstByteFlags();
            UpdateForm();
        }

        private void NoLedgeClimbRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateClimbableCrawlableFlags();
            UpdateForm();
        }

        private void WallDamageCheck_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].IsWallDamage = WallDamageCheck.Checked;
            UpdateForm();
        }

        private void fileCreationEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!savefileeditor_visible)
            {

                FileCreationSaveEditor filecreationsaveeditor = new FileCreationSaveEditor();
                filecreationsaveeditor.Show();
                savefileeditor_visible = true;


            }



        }

        private void ReverseLightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).ReverseLight = ReverseLightCheckBox.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.ReverseLight[Index] = ((ObjFile.Group)GroupList.SelectedItem).ReverseLight;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void ShiftSNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).BaseShiftS = (int)ShiftSNumeric.Value;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.BaseShiftS[Index] = ((ObjFile.Group)GroupList.SelectedItem).BaseShiftS;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void ShiftTNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).BaseShiftT = (int)ShiftTNumeric.Value;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.BaseShiftT[Index] = ((ObjFile.Group)GroupList.SelectedItem).BaseShiftT;

                UpdateGroupSelect(n64refresh);
            }
        }

        private ObjFile importCollision(List<byte> data, int y, bool zobj = false)
        {
            uint test = Helpers.Read32(data, y);
            if ((test & 0xFF00FFFF) == 0x03000000 || zobj)
            {
                // int lightcount = data[i + 1];
                int collisionoffset = (int)Helpers.Read24(data, y + 5);

                if (zobj) collisionoffset = y;

                int vertexnum = Helpers.Read16(data, collisionoffset + 0x0C);
                int vertexoffset = (int)Helpers.Read24(data, collisionoffset + 0x10 + 1);
                int polynum = Helpers.Read16(data, collisionoffset + 0x14);
                int polygonoffset = (int)Helpers.Read24(data, collisionoffset + 0x18 + 1);
                int polytypeoffset = (int)Helpers.Read24(data, collisionoffset + 0x1C + 1);

                ObjFile colfile = new ObjFile();
                colfile.Groups = new List<ObjFile.Group>();
                ObjFile.Group group = new ObjFile.Group();
                List<ObjFile.Group> groups = new List<ObjFile.Group>();
                colfile.Normals.Add(new ObjFile.Normal(1, 1, 1));
                colfile.TextureCoordinates.Add(new ObjFile.TextureCoord(0, 0));
                colfile.VertexColors.Add(new ObjFile.VertexColor(0, 0, 0, 1));




                for (int i = vertexoffset; i < vertexoffset + (0x6 * vertexnum); i += 0x6)
                {
                    double X = Helpers.Read16S(data, i);
                    double Y = Helpers.Read16S(data, i + 0x2);
                    double Z = Helpers.Read16S(data, i + 0x4);
                    ObjFile.Vertex vertex = new ObjFile.Vertex(X, Y, Z);
                    colfile.Vertices.Add(vertex);
                }

                for (int i = polygonoffset; i < polygonoffset + (0x10 * polynum); i += 0x10)
                {
                    ObjFile.Triangle tri = new ObjFile.Triangle();
                    int polytypeid = Helpers.Read16(data, i);
                    uint polytype = Helpers.Read32(data, polytypeoffset + (0x8 * polytypeid));
                    uint polytype2 = Helpers.Read32(data, polytypeoffset + (0x8 * polytypeid) + 4);

                    int A = Helpers.Read16(data, i + 0x2) & 0x1FFF;
                    int B = Helpers.Read16(data, i + 0x4) & 0x1FFF;
                    int C = Helpers.Read16(data, i + 0x6) & 0x1FFF;
                    int polyflagA = Helpers.Read16(data, i + 0x2) & 0xE000;
                    int polyflagB = Helpers.Read16(data, i + 0x4) & 0xE000;

                    tri.VertIndex = new[] { A, B, C };
                    tri.NormalIndex = new[] { 0, 0, 0 };
                    tri.TexCoordIndex = new[] { 0, 0, 0 };
                    tri.VertColor = new[] { 0, 0, 0 };

                    string polyname = "#Raw" + polytype.ToString("X8") + polytype2.ToString("X8")
                        + (((polyflagA & 0x2000) != 0) ? "#IgnoreCamera" : "")
                        + (((polyflagA & 0x4000) != 0) ? "#IgnoreActors" : "")
                        + (((polyflagA & 0x8000) != 0) ? "#IgnoreProjectiles" : "");


                    group = groups.Find(x => x.Name.Contains(polyname));
                    if (group == null)
                    {
                        group = new ObjFile.Group();
                        group.Name = "Group" + groups.Count + "_" + polyname;
                        group.Triangles.Add(tri);
                        groups.Add(group);
                        //      Console.WriteLine(group.Name);
                    }
                    else
                    {
                        group.Triangles.Add(tri);
                    }
                }
                colfile.Groups.AddRange(groups);

                return colfile;



            }
            else return null;
        }

        private void importCollisionFromzsceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZScene File (*.zscene)|*.zscene|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> data = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                for (int y = 0; y < data.Count; y += 4)
                {
                    ObjFile objfile;
                    if (openFileDialog1.FileName.Contains(".zobj")) //DunGen test, can be removed
                        objfile = importCollision(data, 0x52458, true);
                    else
                        objfile = importCollision(data, y);

                    if (objfile != null)
                    {
                        saveFileDialog1.CheckFileExists = false;
                        saveFileDialog1.Filter = "wavefront obj file (*.obj)|*.obj|All Files (*.*)|*.*";
                        saveFileDialog1.CreatePrompt = true;

                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if (IsFileLocked(saveFileDialog1.FileName))
                                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                string colfilename = objfile.ConvertToObject(saveFileDialog1.FileName);
                                CurrentScene.ColModel = new ObjFile(colfilename, true);
                                // CurrentScene.ColModel = colfile;
                                //CurrentScene.ColModel.Prepare(CurrentScene.ColModel.Groups);
                            }
                        }


                        // MessageBox.Show("Imported: ", "Import", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        CollisionTextbox.Text = saveFileDialog1.FileName;
                        UpdateForm();
                        break;
                    }
                }

            }
        }

        public int FindActorRender(ushort actor, ushort variable)
        {
            List<ZScene.ZObjRender> tmplist = zobj_cache.FindAll(x => x.actor == actor);
            if (tmplist != null)
            {
                foreach (ZScene.ZObjRender zobj in tmplist)
                {
                    Regex regex = new Regex(@"" + zobj.variableregex);
                    Match match = regex.Match(variable.ToString("X4"));

                    if (match.Success)
                    {
                        //            Console.WriteLine(actor.ToString("X4") + " " + variable.ToString("X4") + " matched with" + zobj.variableregex);
                        return zobj_cache.IndexOf(zobj);

                    }
                }
            }

            return -1;
        }

        private void rebuildDmaTableallToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string ROM = "";
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = saveFileDialog1.FileName;
                }
            }


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (IsFileLocked(ROM))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {

                    RebuildDMATable(ROM, true);
                }
            }
        }

        public void RebuildDMATable(string ROMname, bool forcecrc = false)
        {
            List<byte> data = new List<byte>(File.ReadAllBytes(ROMname));

            ROM rom = CheckVersion(data);

            int increment = rom.Game == "OOT" ? 0x14 : 0x10;


            BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROMname));
            int StartDMAOffset = (int)rom.SceneDmaTableStart;
            int EndDMAOffset = (int)rom.SceneDmaTableEnd;
            int StartSceneOffset = (int)rom.SceneTable;
            int EndSceneOffset = (int)rom.SceneTableEnd;


            List<byte> DMAtable = new List<byte>();

            int scenes = 0;
            int rooms = 0;
            int blank = 0;


            //Get Scene Offsets

            for (int i = StartSceneOffset; i < EndSceneOffset - 1; i += increment)
            {

                uint startoffset = Helpers.Read32(data, i);
                uint endoffset = Helpers.Read32(data, i + 4);

                if (startoffset != 0 && endoffset > startoffset && endoffset - startoffset < 0x500000)
                {
                    DMAtable.AddRange(BitConverter.GetBytes(startoffset).Reverse());
                    DMAtable.AddRange(BitConverter.GetBytes(endoffset).Reverse());
                    DMAtable.AddRange(BitConverter.GetBytes(startoffset).Reverse());
                    DMAtable.AddRange(BitConverter.GetBytes(blank).Reverse());

                    scenes++;

                    for (int y = (int)startoffset; y < endoffset - 1; y += 4)
                    {
                        uint test = Helpers.Read32(data, y);

                        //check room command and segment offset 02
                        if ((test & 0xFF00FFFF) == 0x04000000 && data[y + 4] == 0x02)
                        {
                            uint roomlist = Helpers.Read24(data, y + 5) + startoffset;
                            int roomcount = data[y + 1];
                            for (int r = 0; r < roomcount; r += 1)
                            {
                                uint rstartoffset = Helpers.Read32(data, (int)(roomlist + (r * 0x8)));
                                uint rendoffset = Helpers.Read32(data, (int)(roomlist + (r * 0x8) + 4));
                                DMAtable.AddRange(BitConverter.GetBytes(rstartoffset).Reverse());
                                DMAtable.AddRange(BitConverter.GetBytes(rendoffset).Reverse());
                                DMAtable.AddRange(BitConverter.GetBytes(rstartoffset).Reverse());
                                DMAtable.AddRange(BitConverter.GetBytes(blank).Reverse());

                                rooms++;
                            }

                            break;
                        }
                    }
                }
            }


            if (DMAtable.Count > EndDMAOffset - StartDMAOffset)
            {
                MessageBox.Show("Not enought space in the DMA table. Try deleting all scenes first. \nAvailable space: " + (EndDMAOffset - StartDMAOffset).ToString("X8") + " \nCurrent DMA table space: " + (DMAtable.Count).ToString("X8"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BWS.Close();
                return;
            }



            BWS.Seek(StartDMAOffset, SeekOrigin.Begin);

            BWS.Write(DMAtable.ToArray());

            BWS.Close();


            if (forcecrc)
            {
                RecalculateCRC(File.Open(ROMname, FileMode.Open, FileAccess.ReadWrite));

                MessageBox.Show("DMA table rebuild! crc updated!\nScenes: " + scenes + "\nRooms: " + rooms, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("DMA table rebuild! Scenes: " + scenes + "\nRooms: " + rooms, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"rn64crc/rn64crc.exe"), "-u " + saveFileDialog1.FileName);



        }

        private void RecalculateCRC(string rom)
        {
            String pdetail = @"/c rn64crc\rn64crc.exe -u " + "\"" + rom + "\"";
            ProcessStartInfo pcmd = new ProcessStartInfo("cmd.exe");
            pcmd.Arguments = pdetail;
            pcmd.UseShellExecute = false;
            pcmd.RedirectStandardOutput = true;
            pcmd.RedirectStandardError = true;
            Process cmd = Process.Start(pcmd);
            string output = cmd.StandardError.ReadToEnd();
            string output2 = cmd.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            Console.WriteLine(output2);
            output = output + output2;

            File.Delete(Path.GetTempPath() + "rn64crc.exe");

            if (output.Contains("[OK]") || output.Contains("[Updated]")) ;
            // Console.WriteLine("CRC recalculated");
            else
            {
                MessageBox.Show("CRC recalculation failed, this normally happens when the program doesn't have admin rights", "Injection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void removeAllRomScenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ROM = "";
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else if (rom64.isSet())
            {

                Helpers.ReplaceLine("gExitParam.nextEntranceIndex", "    gExitParam.nextEntranceIndex = 0x0000", @"\project\src\system\state\0x04-Opening\Opening.c");

                return;
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = saveFileDialog1.FileName;
                }
            }



            if (ROM != "")
            {
                if (IsFileLocked(ROM))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {

                    ROM rom = CheckVersion(new List<byte>(File.ReadAllBytes(ROM)));

                    List<byte> data = new List<byte>(File.ReadAllBytes(ROM));

                    BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));
                    int StartOffset = (int)rom.SceneTable;
                    int EndOffset = (int)rom.SceneTableEnd;
                    int scenes = 0;
                    int rooms = 0;


                    int increment = rom.Game == "OOT" ? 0x14 : 0x10;

                    //Get Scene Offsets

                    for (int i = StartOffset; i < EndOffset - 1; i += increment)
                    {
                        uint StartScene = Helpers.Read32(data, i);
                        uint EndScene = Helpers.Read32(data, i + 4);

                        if (StartScene != 0 && StartScene < EndScene && EndScene - StartScene < 0x500000)
                        {

                            for (int y = (int)StartScene; y < EndScene - 1; y += 4)
                            {
                                uint test = Helpers.Read32(data, y);

                                //check room command and segment offset 02
                                if ((test & 0xFF00FFFF) == 0x04000000 && data[y + 4] == 0x02)
                                {
                                    uint roomlist = Helpers.Read24(data, y + 5) + StartScene;
                                    int roomcount = data[y + 1];
                                    for (int r = 0; r < roomcount; r += 1)
                                    {
                                        uint rstartoffset = Helpers.Read32(data, (int)(roomlist + (r * 0x8)));
                                        uint rendoffset = Helpers.Read32(data, (int)(roomlist + (r * 0x8) + 4));

                                        BWS.Seek((int)rstartoffset, SeekOrigin.Begin);

                                        byte[] Output3 = new byte[rendoffset - rstartoffset];

                                        BWS.Write(Output3.ToArray());

                                        //  Console.WriteLine("S " + rstartoffset.ToString("X") +  "  E " + rendoffset.ToString("X"));

                                        rooms++;

                                    }

                                    break;
                                }
                            }

                            BWS.Seek((int)StartScene, SeekOrigin.Begin);

                            byte[] Output2 = new byte[EndScene - StartScene];

                            BWS.Write(Output2.ToArray());

                            scenes++;
                        }
                    }


                    BWS.Seek(StartOffset, SeekOrigin.Begin);

                    byte[] Output = new byte[EndOffset - StartOffset];

                    BWS.Write(Output.ToArray());

                    data.Clear();



                    MessageBox.Show("Scenes deleted: " + scenes + "\nRooms deleted: " + rooms, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    DialogResult answer = MessageBox.Show("Inject basic title screen on scene " + (rom.Game == "OOT" ? "0x27" : "0x08") + ", offset 0x" + rom.FirstScene.ToString("X8") + "?", "Basic Title Screen", MessageBoxButtons.YesNo);
                    if (answer == DialogResult.Yes)
                    {

                        List<Patch> Patches = new List<Patch>();
                        if (rom.Game == "OOT")
                        {
                            uint EntranceId = (((rom.EntranteTableEnd - 0x14) - rom.EntranceTableStart) / 4);
                            Patches.Add(new Patch((int)(rom.FirstScene), 99, Properties.Resources.titlescreenOOT.ToList(), "TitleScreen scene"));
                            Patches.Add(new Patch((int)(rom.FirstScene + 0x60), 8, rom.FirstScene + 0x1290, "Fixing room command"));
                            Patches.Add(new Patch((int)(rom.FirstScene + 0x64), 8, rom.FirstScene + 0x1290 + 0x200));
                            Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0x14), 8, 0x27004102, "Entrance table"));
                            Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0x10), 8, 0x27004102, "Entrance table"));
                            Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0xC), 8, 0x27004102, "Entrance table"));
                            Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0x8), 8, 0x27004102, "Entrance table"));
                            Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0x4), 8, 0x27004102, "Entrance table"));
                            Patches.Add(new Patch((int)(rom.CutsceneTableEnd - 0x8), 8, (uint)(0x000002A0 | EntranceId << 16), "Cutscene table")); //CHECK
                            Patches.Add(new Patch((int)(rom.CutsceneTableEnd - 0x4), 8, 0x02000188, "Cutscene table"));
                            Patches.Add(new Patch((int)(rom.EntranceTitle), 4, EntranceId, "Starting entrance"));
                            Patches.Add(new Patch((int)(rom.HeaderTitle), 4, 0x0000, "Starting header"));
                            Patches.Add(new Patch((int)(rom.SceneTable) + (0x14 * 0x27), 8, rom.FirstScene, "Scene table data"));
                            Patches.Add(new Patch((int)(rom.SceneTable) + (0x14 * 0x27) + 0x4, 8, rom.FirstScene + 0x1290));
                            Patches.Add(new Patch((int)(rom.SceneTable) + (0x14 * 0x27) + 0x8, 8, 0));
                            Patches.Add(new Patch((int)(rom.SceneTable) + (0x14 * 0x27) + 0xC, 8, 0));
                            Patches.Add(new Patch((int)(rom.SceneTable) + (0x14 * 0x27) + 0x10, 8, 0));
                        }
                        else
                        {
                            //     uint EntranceId = (((rom.EntranteTableEnd - 0x14) - rom.EntranceTableStart) / 4);
                            Patches.Add(new Patch((int)(rom.FirstScene), 99, Properties.Resources.titlescreenMM.ToList(), "TitleScreen scene"));
                            Patches.Add(new Patch((int)(rom.FirstScene + 0xA0), 8, rom.FirstScene + 0x12F0, "Fixing room command"));
                            Patches.Add(new Patch((int)(rom.FirstScene + 0xA4), 8, rom.FirstScene + 0x12F0 + 0x200));
                            Patches.Add(new Patch((int)(rom.FirstScene + 0x84), 4, 0x1C00, "Entrance"));
                            //    Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0x14), 8, 0x01004102, "Entrance table"));
                            //    Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0x10), 8, 0x01004102, "Entrance table"));
                            //     Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0xC), 8, 0x01004102, "Entrance table"));
                            //    Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0x8), 8, 0x01004102, "Entrance table"));
                            //   Patches.Add(new Patch((int)(rom.EntranteTableEnd - 0x4), 8, 0x01004102, "Entrance table"));
                            //  Patches.Add(new Patch((int)(rom.EntranceTitle), 4, EntranceId, "Starting entrance"));
                            Patches.Add(new Patch((int)(rom.HeaderTitle), 4, 0x0000, "Starting header"));
                            Patches.Add(new Patch((int)(rom.SceneTable) + (0x10 * 0x08), 8, rom.FirstScene, "Scene table data"));
                            Patches.Add(new Patch((int)(rom.SceneTable) + (0x10 * 0x08) + 0x4, 8, rom.FirstScene + 0x12F0));
                            Patches.Add(new Patch((int)(rom.SceneTable) + (0x10 * 0x08) + 0x8, 8, 0));
                            Patches.Add(new Patch((int)(rom.SceneTable) + (0x10 * 0x08) + 0xC, 8, 0));

                            DialogResult answer2 = MessageBox.Show("Start file on scene 0x12? (entrance 0000)", "Starting Scene", MessageBoxButtons.YesNo);
                            if (answer2 == DialogResult.Yes)
                            {
                                Patches.Add(new Patch((int)(rom.EntranceNewFile), 4, 0x0000, "Starting Entrance"));
                            }

                        }

                        foreach (Patch patch in Patches)
                        {
                            if (patch.byteamount == 2) data.Add((byte)patch.data);
                            else if (patch.byteamount == 4) Helpers.Append16(ref data, (ushort)patch.data);
                            else if (patch.byteamount == 8) Helpers.Append32(ref data, patch.data);
                            else if (patch.byteamount > 8) data.AddRange(patch.bytedata);

                            BWS.Seek(patch.offset, SeekOrigin.Begin);
                            BWS.Write(data.ToArray());
                            data.Clear();

                            if (patch.name != "") Console.WriteLine(patch.name);
                        }

                    }


                    MessageBox.Show("Basic title screen added! you should rebuild dma table now", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    BWS.Close();


                }
            }
        }

        private void AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.GenerateCustomDMATable = AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem.Checked;
        }

        private void writeCollisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.WriteCollisionZobj = writeCollisionToolStripMenuItem.Checked;
        }

        private void WaterboxEnv_ValueChanged(object sender, EventArgs e)
        {

            UpdateWaterboxData();
        }

        private void WaterboxCam_ValueChanged(object sender, EventArgs e)
        {
            UpdateWaterboxData();
        }

        private void EnvironmentDirectionA_ValueChanged(object sender, EventArgs e)
        {
            UpdateEnvironmentData();
        }

        private void EnvironmentDirectionB_ValueChanged(object sender, EventArgs e)
        {
            UpdateEnvironmentData();
        }

        private void CutsceneActorXRot_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && MainForm.settings.Degrees)
            {
                CutsceneActorXRot.Value = Clamp(CutsceneActorXRot.Value * (decimal)182.044444444, CutsceneActorXRot.Minimum, CutsceneActorXRot.Maximum);
                UpdateCutsceneEdit();
            }


        }

        private void CutsceneActorYRot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && MainForm.settings.Degrees)
            {
                CutsceneActorYRot.Value = Clamp(CutsceneActorYRot.Value * (decimal)182.044444444, CutsceneActorYRot.Minimum, CutsceneActorYRot.Maximum);
                UpdateCutsceneEdit();
            }
        }

        private void CutsceneActorZRot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && MainForm.settings.Degrees)
            {
                CutsceneActorZRot.Value = Clamp(CutsceneActorZRot.Value * (decimal)182.044444444, CutsceneActorZRot.Minimum, CutsceneActorZRot.Maximum);
                UpdateCutsceneEdit();
            }
        }

        private void GroupDecal_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).Decal = GroupDecal.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.Decal[Index] = ((ObjFile.Group)GroupList.SelectedItem).Decal;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void decompressROMToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            //  openSceneToolStripMenuItem.Owner.Hide();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                saveFileDialog1.CheckFileExists = false;
                saveFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (IsFileLocked(saveFileDialog1.FileName))
                        MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        String pdetail = @"/k ndec\ndec.exe " + "\"" + openFileDialog1.FileName + "\" " + "\"" + saveFileDialog1.FileName + "\"";

                        ProcessStartInfo pcmd = new ProcessStartInfo("cmd.exe");
                        pcmd.Arguments = pdetail;
                        Process cmd = Process.Start(pcmd);
                        cmd.WaitForExit();

                    }
                }

            }
        }

        private void CutsceneEntrance_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.CutsceneEntrance = (int)CutsceneEntrance.Value;
                UpdateForm();
            }
        }

        private void CutsceneSpawn_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.CutsceneEntranceNum = (int)CutsceneSpawn.Value;
                UpdateForm();
            }
        }

        private void CutsceneFlag_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.CutsceneFlag = (int)CutsceneFlag.Value;
                UpdateForm();
            }
        }

        private void changeBinaryZmapNamesToTheirOffsetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.Zmapoffsetnames = ZmapOffsetNames.Checked;
        }

        private void importCamerasWaterboxes(List<byte> data, int y)
        {

            uint test = Helpers.Read32(data, y);
            if ((test & 0xFF00FFFF) == 0x03000000)
            {
                // int lightcount = data[i + 1];
                int collisionoffset = (int)Helpers.Read24(data, y + 5);

                int cameraoffset = (int)Helpers.Read24(data, collisionoffset + 0x20 + 1);
                int waternum = Helpers.Read16(data, collisionoffset + 0x24);
                int waterboxoffset = (int)Helpers.Read24(data, collisionoffset + 0x28 + 1);
                int polygonarrayoffset = (int)Helpers.Read24(data, collisionoffset + 0x18 + 1);
                int polynum = (int)Helpers.Read16(data, collisionoffset + 0x14);
                int polytypeoffset = (int)Helpers.Read24(data, collisionoffset + 0x1C + 1);

                int waterboxes = 0;
                int cameras = 0;
                int cameranum = 0;

                if (waterboxoffset != 0)
                {
                    CurrentScene.Waterboxes.Clear();
                    for (int i = waterboxoffset; i < waterboxoffset + (0x10 * waternum); i += 0x10)
                    {
                        ZWaterbox waterbox = new ZWaterbox();

                        waterbox.XPos = Helpers.Read16S(data, i);
                        waterbox.YPos = Helpers.Read16S(data, i + 2);
                        waterbox.ZPos = Helpers.Read16S(data, i + 4);
                        waterbox.XSize = Helpers.Read16S(data, i + 6);
                        waterbox.ZSize = Helpers.Read16S(data, i + 8);
                        int properties = Helpers.Read16(data, i + 0xC);
                        waterbox.Room = (byte)(properties & 0xE000);
                        waterbox.Env = (byte)(properties & 0x1F00);
                        waterbox.Camera = (byte)(properties & 0x00FF);
                        CurrentScene.Waterboxes.Add(waterbox);
                        waterboxes++;
                    }
                }

                if (cameraoffset != 0)
                {
                    CurrentScene.Cameras.Clear();
                    //  int cameranum = Helpers.Read16S(data, cameraoffset + 2);
                    int numpolys = 0;
                    //get cameranum
                    for (int i = polygonarrayoffset; i < polygonarrayoffset + (0x10 * polynum); i += 0x10)
                    {
                        int tmp = Helpers.Read16(data, i + 0x0);
                        if (tmp > numpolys) numpolys = tmp;
                    }
                    for (int i = polytypeoffset; i < polytypeoffset + (0x8 * numpolys); i += 0x8)
                    {
                        uint tmp = Helpers.Read32(data, i);
                        if ((tmp & 0x000000FF) > cameranum) cameranum = (int)(tmp & 0x000000FF);
                    }

                    cameranum++;


                    for (int i = cameraoffset; i < cameraoffset + (0x8 * cameranum); i += 0x8)
                    {
                        ZCamera camera = new ZCamera();

                        camera.Type = (byte)Helpers.Read16(data, i);

                        int offset = (int)Helpers.Read24(data, i + 5);

                        camera.XPos = Helpers.Read16S(data, offset);
                        camera.YPos = Helpers.Read16S(data, offset + 2);
                        camera.ZPos = Helpers.Read16S(data, offset + 4);
                        camera.XRot = Helpers.Read16S(data, offset + 6);
                        camera.YRot = Helpers.Read16S(data, offset + 8);
                        camera.ZRot = Helpers.Read16S(data, offset + 0xA);
                        camera.Fov = Helpers.Read16S(data, offset + 0xC);
                        camera.Unk1 = Helpers.Read16(data, offset + 0xE);
                        camera.Unk2 = Helpers.Read16(data, offset + 0x10);

                        if (camera.Type == 0x1E)
                        {
                            camera.Unk12 = Helpers.Read16(data, offset + 0x12);
                            camera.Unk14 = Helpers.Read16(data, offset + 0x14);
                            camera.Unk16 = Helpers.Read16(data, offset + 0x16);
                            camera.Unk18 = Helpers.Read16(data, offset + 0x18);
                            camera.Unk1A = Helpers.Read16(data, offset + 0x1A);
                            camera.Unk1C = Helpers.Read16(data, offset + 0x1C);
                            camera.Unk1E = Helpers.Read16(data, offset + 0x1E);
                            camera.Unk20 = Helpers.Read16(data, offset + 0x20);
                            camera.Unk22 = Helpers.Read16(data, offset + 0x22);
                        }

                        //jfif id

                        CurrentScene.Cameras.Add(camera);
                        cameras++;
                    }
                }

            }
        }

        private void importCamerasAndWaterboxFromzsceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZScene File (*.zscene)|*.zscene";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> data = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                for (int y = 0; y < data.Count; y += 4)
                {
                    importCamerasWaterboxes(data, y);

                    if (CurrentScene.Cameras.Count > 0 || CurrentScene.Waterboxes.Count > 0)
                    {

                        MessageBox.Show("Cameras added: " + CurrentScene.Cameras.Count + "\nWaterboxes added: " + CurrentScene.Waterboxes.Count, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        UpdateForm();

                        return;
                    }

                }
            }
        }

        private void GroupPixelated_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).Pixelated = GroupPixelated.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.Pixelated[Index] = ((ObjFile.Group)GroupList.SelectedItem).Pixelated;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void showRotationValuesAsHexadecimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.HexRotations = showRotationValuesAsHexadecimalToolStripMenuItem.Checked;
            CameraXRot.Hexadecimal = settings.HexRotations;
            CameraYRot.Hexadecimal = settings.HexRotations;
            CameraZRot.Hexadecimal = settings.HexRotations;
            CutsceneActorXRot.Hexadecimal = settings.HexRotations;
            CutsceneActorYRot.Hexadecimal = settings.HexRotations;
            CutsceneActorZRot.Hexadecimal = settings.HexRotations;
            actorEditControl1.UpdateActorEdit();
            actorEditControl2.UpdateActorEdit();
            actorEditControl3.UpdateActorEdit();
        }

        private void NoMiscRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFirstByteFlags();
            UpdateForm();
        }

        private void SmallVoidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFirstByteFlags();
            UpdateForm();
        }

        private void DiveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFirstByteFlags();
            UpdateForm();
        }

        private void AutograbClimbRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFirstByteFlags();
            UpdateForm();
        }

        private void Lower1UnitChecbox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].Lower1Unit = Lower1UnitChecbox.Checked;
            UpdateForm();
        }

        private void BlockEponaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.PolyTypes[(int)PolygonSelect.Value - 1].EponaBlock = BlockEponaCheckBox.Checked;
            UpdateForm();
        }

        private void CutscenePositionCopyCamera_Click(object sender, EventArgs e)
        {
            double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
            double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

            Vector3d truepos = GetTrueCameraPosition(), truepos2 = truepos;

            truepos.X = Clamp(truepos.X, -32767, 32767);
            truepos.Y = Clamp(truepos.Y, -32767, 32767);
            truepos.Z = Clamp(truepos.Z, -32767, 32767);

            if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
            {
                truepos2.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 5000f;
            }
            else
            {
                truepos2.X += (float)Math.Sin(RotYRad) * Camera.CameraCoeff * 2.0f * 5000f;
                truepos2.Z -= (float)Math.Cos(RotYRad) * Camera.CameraCoeff * 2.0f * 5000f;
                truepos2.Y -= (float)Math.Tan(RotXRad) * Camera.CameraCoeff * 2.0f * 5000f;
            }

            truepos2.X = Clamp(truepos2.X, -32767, 32767);
            truepos2.Y = Clamp(truepos2.Y, -32767, 32767);
            truepos2.Z = Clamp(truepos2.Z, -32767, 32767);

            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Position = (Vector3)truepos;
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex].Position2 = (Vector3)truepos2;
        }

        private void CutscenePositionUp_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex];
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.RemoveAt(CutsceneAbsolutePositionListBox.SelectedIndex);
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Insert(CutsceneAbsolutePositionListBox.SelectedIndex - 1, item);
            CutsceneAbsolutePositionListBox.SelectedIndex--;
            UpdateCutsceneEdit();
        }

        private void CutscenePositionDown_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points[CutsceneAbsolutePositionListBox.SelectedIndex];
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.RemoveAt(CutsceneAbsolutePositionListBox.SelectedIndex);
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Insert(CutsceneAbsolutePositionListBox.SelectedIndex + 1, item);
            CutsceneAbsolutePositionListBox.SelectedIndex++;
            UpdateCutsceneEdit();
        }

        private void CutscenePositionViewMode_Click(object sender, EventArgs e)
        {
            CameraPreview_Toggle();

            if (previewcamerapoints)
                CutscenePreview_Clear();

            CameraPreview_UpdateTransforms();
            CameraPreview_UpdateParams();
            UpdateCutsceneEdit();
        }

        private void CutsceneAbsolutePositionListBox_KeyDown(object sender, KeyEventArgs e)
        {
            CameraPreview_UpdateTransforms();
        }

        private void CutsceneGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void CutsceneActorUp_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex];
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.RemoveAt(CutsceneActorListBox.SelectedIndex);
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Insert(CutsceneActorListBox.SelectedIndex - 1, item);
            CutsceneActorListBox.SelectedIndex--;
            UpdateCutsceneEdit();
        }

        private void CutsceneActorDown_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors[CutsceneActorListBox.SelectedIndex];
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.RemoveAt(CutsceneActorListBox.SelectedIndex);
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].CutsceneActors.Insert(CutsceneActorListBox.SelectedIndex + 1, item);
            CutsceneActorListBox.SelectedIndex++;
            UpdateCutsceneEdit();
        }

        private void CutsceneTextboxUp_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes[CutsceneTextboxList.SelectedIndex];
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes.RemoveAt(CutsceneTextboxList.SelectedIndex);
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes.Insert(CutsceneTextboxList.SelectedIndex - 1, item);
            CutsceneTextboxList.SelectedIndex--;
            UpdateCutsceneEdit();
        }

        private void CutsceneTextboxDown_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes[CutsceneTextboxList.SelectedIndex];
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes.RemoveAt(CutsceneTextboxList.SelectedIndex);
            CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Textboxes.Insert(CutsceneTextboxList.SelectedIndex + 1, item);
            CutsceneTextboxList.SelectedIndex++;
            UpdateCutsceneEdit();
        }

        private void PathwayUp_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Pathways[(int)(PathwayNumber.Value)].Points[PathwayListBox.SelectedIndex];
            CurrentScene.Pathways[(int)(PathwayNumber.Value)].Points.RemoveAt(PathwayListBox.SelectedIndex);
            CurrentScene.Pathways[(int)(PathwayNumber.Value)].Points.Insert(PathwayListBox.SelectedIndex - 1, item);
            PathwayListBox.SelectedIndex--;
            UpdateForm();
        }

        private void PathwayDown_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.Pathways[(int)(PathwayNumber.Value)].Points[PathwayListBox.SelectedIndex];
            CurrentScene.Pathways[(int)(PathwayNumber.Value)].Points.RemoveAt(PathwayListBox.SelectedIndex);
            CurrentScene.Pathways[(int)(PathwayNumber.Value)].Points.Insert(PathwayListBox.SelectedIndex + 1, item);
            PathwayListBox.SelectedIndex++;
            UpdateForm();
        }

        private void patchROMToUse2axisBillboardOoTDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string ROM = "";
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = saveFileDialog1.FileName;
                }
            }

            if (ROM != "")
            {
                if (IsFileLocked(ROM))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {

                    ROM rom = CheckVersion(new List<byte>(File.ReadAllBytes(ROM)));

                    if (rom.Prefix != "DBGMQ")
                    {
                        MessageBox.Show("This is not an OoT debug rom. The patch only works with that version atm. If using MM, you don't have to use this.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //  var assembly = Assembly.GetExecutingAssembly();

                    if (MessageBox.Show("WARNING! This replaces the code file, meaning that all your rom modifications as well as maps and models pointers will be erased! Continue? (Ignore this if you're using a clean debug rom)", "WARNING",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {

                        List<Byte> data = new List<byte>();

                        data.AddRange(ExtractResource("SharpOcarina.Files.2AxisBillboardPatch").ToList());

                        BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));

                        BWS.Seek((0xA94000), SeekOrigin.Begin); // + 0xA2234
                        BWS.Write(data.ToArray());
                        data.Clear();

                        BWS.Close();


                        RecalculateCRC(File.Open(ROM, FileMode.Open, FileAccess.ReadWrite));
                        //Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"rn64crc/rn64crc.exe"), "-u " + saveFileDialog1.FileName);

                        MessageBox.Show("Patch applied! crc updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void GroupBillboard_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).Billboard = GroupBillboard.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.Billboard[Index] = ((ObjFile.Group)GroupList.SelectedItem).Billboard;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void Group2AxisBillboard_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).TwoAxisBillboard = Group2AxisBillboard.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.TwoAxisBillboard[Index] = ((ObjFile.Group)GroupList.SelectedItem).TwoAxisBillboard;

                UpdateGroupSelect(n64refresh);
            }
        }

        public void RefreshRecetMenuItems(ref ToolStripMenuItem menu, string table, string newpath = "")
        {
            XmlDocument doc = new XmlDocument();
            File.Delete(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFilestmp.xml"));

            System.IO.File.Copy(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFiles.xml"), Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFilestmp.xml"));
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFilestmp.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/" + table);


            if (newpath != "")
            {
                XmlNode deletenode = doc.SelectSingleNode("//" + table + "[text()='" + newpath + "']");
                if (deletenode != null)
                {
                    deletenode.ParentNode.RemoveChild(deletenode);
                }

                XmlNode newnode = doc.CreateElement(table);
                newnode.InnerText = newpath;
                XmlNode Table = doc.SelectSingleNode("//Table");
                Table.AppendChild(newnode);
                doc.Save(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFiles.xml"));
            }

            if (nodes.Count > settings.MaxLastFile)
            {
                while (nodes.Count > settings.MaxLastFile)
                {
                    nodes[0].ParentNode.RemoveChild(nodes[0]);
                    nodes = doc.SelectNodes("Table/" + table);
                }
            }

            if (nodes != null)
            {
                menu.DropDownItems.Clear();
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    XmlNode node = nodes[i];

                    ToolStripMenuItem MenuItem = new System.Windows.Forms.ToolStripMenuItem() { Name = node.InnerText, Text = node.InnerText };

                    if (table == "SceneFile")
                        MenuItem.Click += new System.EventHandler(this.OpenRecentScene);
                    else if (table == "GlobalFile")
                        MenuItem.Click += new System.EventHandler(this.OpenRecentGlobal);

                    menu.DropDownItems.Add(MenuItem);
                };
            }

            fs.Close();
            File.Delete(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFilestmp.xml"));
        }

        public void OpenRecentScene(object sender, System.EventArgs e)
        {
            OpenScene(((ToolStripMenuItem)sender).Text);
        }

        public void OpenRecentGlobal(object sender, System.EventArgs e)
        {
            OpenGlobalFile(((ToolStripMenuItem)sender).Text);
        }

        private void CutsceneTableEntry_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {
                CurrentScene.CutsceneTableRow = (int)CutsceneTableEntry.Value;
                UpdateForm();
            }
        }

        private void iGotACrashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("https://docs.google.com/document/d/1JD2Up1sy7VnTAAphzRvs3divol60Lwp2YrIe1doXCo0 \nPress Ok to copy the URL to your clipboard", "URL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clipboard.SetText("https://docs.google.com/document/d/1JD2Up1sy7VnTAAphzRvs3divol60Lwp2YrIe1doXCo0");
        }

        private void clearAllGroupSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will reset all group settings of all rooms, continue?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                foreach (ZScene.ZRoom room in CurrentScene.Rooms)
                {
                    cleargroupsettings(room);


                }
                ((CurrencyManager)GroupList.BindingContext[CurrentScene.Rooms[RoomList.SelectedIndex].ObjModel.Groups]).Refresh();
                UpdateForm();
            }
        }

        private void cleargroupsettings(ZScene.ZRoom room)
        {

            for (int i = 0; i < room.TrueGroups.Count; i++)
            {
                room.GroupSettings.TintAlpha[i] = 0xFFFFFFFF;
                room.GroupSettings.MultiTexAlpha[i] = 0xFFFFFFFF;
                room.GroupSettings.TileS[i] = GBI.G_TX_WRAP;
                room.GroupSettings.TileT[i] = GBI.G_TX_WRAP;
                room.GroupSettings.BackfaceCulling[i] = true;
                room.GroupSettings.Animated[i] = false;
                room.GroupSettings.Metallic[i] = false;
                room.GroupSettings.Decal[i] = false;
                room.GroupSettings.IgnoreFog[i] = false;
                room.GroupSettings.SmoothRGBAEdges[i] = false;
                room.GroupSettings.Pixelated[i] = false;
                room.GroupSettings.Billboard[i] = false;
                room.GroupSettings.TwoAxisBillboard[i] = false;
                room.GroupSettings.ReverseLight[i] = false;
                room.GroupSettings.MultiTexMaterial[i] = -1;
                room.GroupSettings.ShiftS[i] = GBI.G_TX_NOLOD;
                room.GroupSettings.ShiftT[i] = GBI.G_TX_NOLOD;
                room.GroupSettings.BaseShiftS[i] = GBI.G_TX_NOLOD;
                room.GroupSettings.BaseShiftT[i] = GBI.G_TX_NOLOD;
                room.GroupSettings.AnimationBank[i] = 8;
                room.GroupSettings.LodGroup[i] = 0;
                room.GroupSettings.LodDistance[i] = 0;
                room.GroupSettings.RenderLast[i] = false;
                room.GroupSettings.VertexNormals[i] = false;
                room.GroupSettings.AlphaMask[i] = false;
                room.GroupSettings.Custom[i] = false;

                room.TrueGroups[i].TintAlpha = 0xFFFFFFFF;
                room.TrueGroups[i].MultiTexAlpha = 0xFFFFFFFF;
                room.TrueGroups[i].TileS = GBI.G_TX_WRAP;
                room.TrueGroups[i].TileT = GBI.G_TX_WRAP;
                room.TrueGroups[i].BackfaceCulling = true;
                room.TrueGroups[i].Animated = false;
                room.TrueGroups[i].Metallic = false;
                room.TrueGroups[i].EnvColor = false;
                room.TrueGroups[i].Decal = false;
                room.TrueGroups[i].IgnoreFog = false;
                room.TrueGroups[i].SmoothRGBAEdges = false;
                room.TrueGroups[i].Pixelated = false;
                room.TrueGroups[i].Billboard = false;
                room.TrueGroups[i].TwoAxisBillboard = false;
                room.TrueGroups[i].ReverseLight = false;
                room.TrueGroups[i].MultiTexMaterial = -1;
                room.TrueGroups[i].ShiftS = GBI.G_TX_NOLOD;
                room.TrueGroups[i].ShiftT = GBI.G_TX_NOLOD;
                room.TrueGroups[i].BaseShiftS = GBI.G_TX_NOLOD;
                room.TrueGroups[i].BaseShiftT = GBI.G_TX_NOLOD;
                room.TrueGroups[i].AnimationBank = 8;
                room.TrueGroups[i].LodGroup = 0;
                room.TrueGroups[i].LodDistance = 0;
                room.GroupSettings.LOD[i] = false;
                room.GroupSettings.RenderLast[i] = false;
                room.GroupSettings.VertexNormals[i] = false;
                room.GroupSettings.AlphaMask[i] = false;
                room.GroupSettings.Custom[i] = false;
            }


        }

        private void importTransitionsSpawns(List<byte> data, int i)
        {
            uint test = Helpers.Read32(data, i);
            if ((test & 0xFF00FFFF) == 0x0E000000 && data[i + 4] == 0x02)
            {
                int actorcount = data[i + 1];
                int offset = (int)Helpers.Read24(data, i + 5);
                CurrentScene.Transitions.Clear();

                for (int ii = offset; ii < offset + (16 * actorcount); ii += 16)
                {

                    ushort num, var;

                    byte frontroom, backroom, frontcamera, backcamera;

                    short xpos, ypos, zpos, yrot;

                    frontroom = data[ii];
                    frontcamera = data[ii + 2];
                    backroom = data[ii + 3];
                    backcamera = data[ii + 4];

                    num = Helpers.Read16(data, ii + 4);
                    xpos = Helpers.Read16S(data, ii + 6);
                    ypos = Helpers.Read16S(data, ii + 8);
                    zpos = Helpers.Read16S(data, ii + 10);
                    yrot = Helpers.Read16S(data, ii + 12);
                    var = Helpers.Read16(data, ii + 14);

                    CurrentScene.Transitions.Add(new ZActor(frontroom, frontcamera, backroom, backcamera, num, xpos, ypos, zpos, yrot, var));
                }
            }
            else if ((test & 0xFF00FFFF) == 0x00000000 && data[i + 4] == 0x02)
            {
                int actorcount = data[i + 1];
                int offset = (int)Helpers.Read24(data, i + 5);
                CurrentScene.SpawnPoints.Clear();

                for (int ii = offset; ii < offset + (16 * actorcount); ii += 16)
                {
                    ushort num, var;

                    short xpos, ypos, zpos, xrot, yrot, zrot;

                    num = Helpers.Read16(data, ii);
                    xpos = Helpers.Read16S(data, ii + 2);
                    ypos = Helpers.Read16S(data, ii + 4);
                    zpos = Helpers.Read16S(data, ii + 6);
                    xrot = Helpers.Read16S(data, ii + 8);
                    yrot = Helpers.Read16S(data, ii + 10);
                    zrot = Helpers.Read16S(data, ii + 12);
                    var = Helpers.Read16(data, ii + 14);

                    CurrentScene.SpawnPoints.Add(new ZActor(num, xpos, ypos, zpos, xrot, yrot, zrot, var));
                }
            }
        }

        private void importTransitionsAndSpawnsFromzsceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZScene File (*.zscene)|*.zscene";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> data = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));
                bool transitionfound = false, spawnfound = false;
                for (int i = 0; i < data.Count; i += 4)
                {
                    importTransitionsSpawns(data, i);

                    uint test = Helpers.Read32(data, i);

                    if ((CurrentScene.Transitions.Count > 0 && CurrentScene.SpawnPoints.Count > 0) || ((test & 0xFF00FFFF) == 0x14000000 && data[i + 4] == 0x00))
                    {
                        MessageBox.Show("Imported transitions: " + CurrentScene.Transitions.Count + "\n" +
                            "Imported spawns: " + CurrentScene.SpawnPoints.Count, "Import", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        actorEditControl1.UpdateForm();
                        actorEditControl1.UpdateActorEdit();
                        actorEditControl2.UpdateForm();
                        actorEditControl2.UpdateActorEdit();
                        actorEditControl3.UpdateForm();
                        actorEditControl3.UpdateActorEdit();
                        UpdateForm();
                        break;
                    }

                }

            }
        }

        private void importPathways(List<byte> data, int y)
        {
            uint test = Helpers.Read32(data, y);
            if ((test & 0xFF00FFFF) == 0x0D000000)
            {
                int pathways = 0;
                int pathwayoffset = 0;
                int pathpointoffset = 0;
                int points = 0;

                pathwayoffset = Helpers.Read24S(data, y + 5);

                test = Helpers.Read32(data, pathwayoffset + 4);

                while (((test & 0xFF000000) == 0x02000000))
                {
                    ZPathway pathway = new ZPathway();
                    pathway.Points = new List<Vector3>();
                    points = data[pathwayoffset];
                    pathpointoffset = Helpers.Read24S(data, pathwayoffset + 5);
                    for (int w = 0; w < points; w++)
                    {
                        pathway.Points.Add(new Vector3(
                            Helpers.Read16S(data, pathpointoffset),
                            Helpers.Read16S(data, pathpointoffset + 2),
                            Helpers.Read16S(data, pathpointoffset + 4)

                            ));

                        pathpointoffset += 6;
                    }
                    pathways++;
                    CurrentScene.Pathways.Add(pathway);
                    pathwayoffset += 8;
                    test = Helpers.Read32(data, pathwayoffset + 4);
                }
            }

        }

        private void importPathwaysFromzsceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZScene File (*.zscene)|*.zscene";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> data = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));
                CurrentScene.Pathways.Clear();

                for (int y = 0; y < data.Count; y += 4)
                {
                    importPathways(data, y);
                    uint test = Helpers.Read32(data, y);

                    if (CurrentScene.Pathways.Count > 0)
                    {
                        MessageBox.Show("Pathways added: " + CurrentScene.Pathways.Count, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        UpdateForm();

                        return;
                    }

                    if (((test & 0xFF00FFFF) == 0x14000000 && data[y + 4] == 0x00))
                    {
                        MessageBox.Show("No pathways in this scene ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
            }
        }

        private void displaySwitchFlagsUsedByAllRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScene != null && CurrentScene.Rooms.Count > 0 && !flaglog_visible)
            {

                FlagLog flaglog = new FlagLog(GenerateFlagLog());
                flaglog.Show();
                flaglog_visible = true;


            }
        }

        private class FlagEntryInfo
        {
            public string room;
            public int ID;
            public string name;
            public int entryNum;

            public FlagEntryInfo(int _ID, string _room, string _name, int _entryNum)
            {
                ID = _ID;
                room = _room;
                name = _name;
                entryNum = _entryNum;
            }
        }

        private class FlagLogInfo
        {
            bool has_printed = false;
            string message;
            int i;

            public FlagLogInfo(int _i)
            {
                i = _i;
            }

            public void processEntry(FlagEntryInfo match)
            {
                if (!has_printed)
                {
                    message += @"\par \b" + " " + i.ToString("X2") + ": " + @"\b0";
                    has_printed = true;
                }

                if (match.room != "")
                    message += @"\par " + "    " + match.entryNum.ToString("d") + ". " + match.name + " (" + match.room + ")";
                else
                    message += @"\par " + "    " + match.entryNum.ToString("d") + ". " + match.name + " (Transition)";
            }

            public string getMsg()
            {
                return message;
            }
        }

        public static string GenerateFlagLog()
        {
            List<FlagEntryInfo> switchflags = new List<FlagEntryInfo>();
            List<FlagEntryInfo> chestflags = new List<FlagEntryInfo>();
            List<FlagEntryInfo> collectibleflags = new List<FlagEntryInfo>();
            List<FlagEntryInfo> pathways = new List<FlagEntryInfo>();


            int entryIndex = 0;
            int roomIndex = 0;
            foreach (ZScene.ZRoom room in CurrentScene.Rooms)
            {
                entryIndex = 0;
                foreach (ZActor actor in room.ZActors)
                {
                    List<ActorProperty> properties = XMLreader.getActorProperties(actor.Number.ToString("X4"));

                    foreach (ActorProperty property in properties)
                    {
                        if (property.Name.ToLower().Contains("switch flag") || property.Name.ToLower().Contains("chest flag") || property.Name.ToLower().Contains("collectible flag") || property.Name.ToLower().Contains("path id"))
                        {
                            int flag = 0;

                            if (property.Target == "Var")
                            {
                                flag = ((actor.Variable & property.Mask) >> property.Position);
                            }
                            else if (property.Target == "XRot")
                            {
                                flag = (((ushort)actor.XRot & property.Mask) >> property.Position);
                            }
                            else if (property.Target == "YRot")
                            {
                                flag = (((ushort)actor.YRot & property.Mask) >> property.Position);
                            }
                            else if (property.Target == "ZRot")
                            {
                                flag = (((ushort)actor.ZRot & property.Mask) >> property.Position);
                            }
                            string name = XMLreader.getActorName(actor.Number.ToString("X4"));
                            string roomName = roomIndex.ToString("d") + ". " + room.ModelShortFilename;

                            if (property.Name.ToLower().Contains("switch flag"))
                                switchflags.Add(new FlagEntryInfo(flag, roomName, name, entryIndex));
                            else if (property.Name.ToLower().Contains("chest flag"))
                                chestflags.Add(new FlagEntryInfo(flag, roomName, name, entryIndex));
                            else if (property.Name.ToLower().Contains("collectible flag"))
                                collectibleflags.Add(new FlagEntryInfo(flag, roomName, name, entryIndex));
                            else if (property.Name.ToLower().Contains("path id"))
                                pathways.Add(new FlagEntryInfo(flag, roomName, name, entryIndex));

                        }
                    }
                    entryIndex++;
                }
                roomIndex++;
            }

            entryIndex = 0;
            foreach (ZActor actor in CurrentScene.Transitions)
            {
                List<ActorProperty> properties = XMLreader.getActorProperties(actor.Number.ToString("X4"));

                foreach (ActorProperty property in properties)
                {
                    if (property.Name.ToLower().Contains("switch flag"))
                    {
                        int flag = 0;

                        if (property.Target == "Var")
                        {
                            flag = ((actor.Variable & property.Mask) >> property.Position);
                        }
                        else if (property.Target == "XRot")
                        {
                            flag = (((ushort)actor.XRot & property.Mask) >> property.Position);
                        }
                        else if (property.Target == "YRot")
                        {
                            flag = (((ushort)actor.YRot & property.Mask) >> property.Position);
                        }
                        else if (property.Target == "ZRot")
                        {
                            flag = (((ushort)actor.ZRot & property.Mask) >> property.Position);
                        }
                        string name = XMLreader.getActorName(actor.Number.ToString("X4"));
                        switchflags.Add(new FlagEntryInfo(flag, "", name, entryIndex));

                    }
                }
                entryIndex++;
            }

            switchflags = switchflags.OrderBy(x => x.room).ToList();
            collectibleflags = collectibleflags.OrderBy(x => x.room).ToList();
            chestflags = chestflags.OrderBy(x => x.room).ToList();
            pathways = pathways.OrderBy(x => x.room).ToList();

            string message = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red0\green0\blue255;} \cf2\b " + "Switch Flags" + @"\line" + @"\b0\cf1  ";

            for (int i = 0; i <= 0x3F; i++)
            {
                FlagLogInfo flag = new FlagLogInfo(i);

                foreach (FlagEntryInfo match in switchflags.FindAll(x => x.ID == i))
                    flag.processEntry(match);

                message += flag.getMsg();
            }

            message += @"\par\par  \cf2\b Collectible Flags" + @"\line" + @"\b0\cf1  ";

            for (int i = 0; i <= 0x3F; i++)
            {
                FlagLogInfo flag = new FlagLogInfo(i);

                foreach (FlagEntryInfo match in collectibleflags.FindAll(x => x.ID == i))
                    flag.processEntry(match);

                message += flag.getMsg();
            }

            message += @"\par\par  \cf2\b Chest Flags" + @"\line" + @"\b0\cf1  ";

            for (int i = 0; i <= 0x1F; i++)
            {
                FlagLogInfo flag = new FlagLogInfo(i);

                foreach (FlagEntryInfo match in chestflags.FindAll(x => x.ID == i))
                    flag.processEntry(match);

                message += flag.getMsg();
            }

            message += @"\par\par  \cf2\b Pathways" + @"\line" + @"\b0\cf1  ";

            for (int i = 0; i <= CurrentScene.Pathways.Count - 1; i++)
            {
                FlagLogInfo flag = new FlagLogInfo(i);

                foreach (FlagEntryInfo match in pathways.FindAll(x => x.ID == i))
                    flag.processEntry(match);

                message += flag.getMsg();
            }

            // MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            message += "}";
            return message;
        }

        private void pauseScreenMapEditorOoTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!subscreenmapeditor_visible)
            {
                //Enabled = false;

                SubscreenMapEditor mapeditor = new SubscreenMapEditor(CurrentScene != null ? (ushort)0 : (ushort)CurrentScene.SceneNumber, this);
                mapeditor.Show();
                subscreenmapeditor_visible = true;

            }
        }

        private void GroupAnimatedBank_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).AnimationBank = (int)GroupAnimatedBank.Value;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.AnimationBank[Index] = ((ObjFile.Group)GroupList.SelectedItem).AnimationBank;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void GroupIgnoreFog_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).IgnoreFog = GroupIgnoreFog.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.IgnoreFog[Index] = ((ObjFile.Group)GroupList.SelectedItem).IgnoreFog;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void AddTextureAnim_Click(object sender, EventArgs e)
        {
            if (CurrentScene.ColModel == null) return;

            CurrentScene.TextureAnims.Add(new ZTextureAnim(ZTextureAnim.scroll));

            UpdateForm();
            TextureAnimSelect.Value = TextureAnimSelect.Maximum;
        }

        private void DeleteTextureAnim_Click(object sender, EventArgs e)
        {
            CurrentScene.TextureAnims.RemoveAt((int)TextureAnimSelect.Value - 8);
            UpdateForm();
        }

        private void TextureAnimUseSecondLayer_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TextureAnimXVelocity1_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].XVelocity1 = (sbyte)TextureAnimXVelocity1.Value;

            UpdateTextureAnimEdit();
        }

        private void TextureAnimYVelocity1_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].YVelocity1 = (sbyte)TextureAnimYVelocity1.Value;

            UpdateTextureAnimEdit();
        }

        private void TextureAnimWidth1_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].Width1 = (byte)TextureAnimWidth1.Value;

            UpdateTextureAnimEdit();
        }

        private void TextureAnimHeight1_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].Height1 = (byte)TextureAnimHeight1.Value;

            UpdateTextureAnimEdit();
        }

        private void TextureAnimXVelocity2_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].XVelocity2 = (sbyte)TextureAnimXVelocity2.Value;

            UpdateTextureAnimEdit();
        }

        private void TextureAnimYVelocity2_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].YVelocity2 = (sbyte)TextureAnimYVelocity2.Value;

            UpdateTextureAnimEdit();
        }

        private void TextureAnimWidth2_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].Width2 = (byte)TextureAnimWidth2.Value;

            UpdateTextureAnimEdit();
        }

        private void TextureAnimHeight2_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.TextureAnims[(int)TextureAnimSelect.Value - 8].Height2 = (byte)TextureAnimHeight2.Value;

            UpdateTextureAnimEdit();
        }

        private void TextureAnimSelect_ValueChanged(object sender, EventArgs e)
        {
            UpdateTextureAnimEdit();
        }

        private void patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            string ROM = "";
            bool z64rom = false;
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "Rom / z64rom project (*.z64;*.rom,*.toml)|*.z64;*.rom;*.toml|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = saveFileDialog1.FileName;

                    if (ROM.Contains("z64project.toml"))
                        z64rom = true;
                    else if (ROM.Contains(".toml"))
                    {
                        MessageBox.Show("invalid config file, you need to import z64project.toml", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (ROM != "")
            {

                ExtendRamPatch(ROM, z64rom);

            }
        }

        private void ExtendRamPatch(string ROM, bool z64rom = false)
        {

            if (!z64rom && IsFileLocked(ROM))
                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string game = "";
                string prefix = "";
                string filepath = "";

                game = GetVersion(z64rom, ROM, out prefix);
                if (z64rom) filepath = Path.GetDirectoryName(ROM);

                if (game != "OOT" || prefix != "DBGMQ")
                {
                    MessageBox.Show("Only debug rom supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<Patch> Patches = new List<Patch>();



                Patches.Add(new Patch(0x00B74FE0, 8, 0, "SpeedUP: Stop Writing AB To Empty RAM Slots"));
                Patches.Add(new Patch(0x00B75094, 8, 0));
                Patches.Add(new Patch(0x00ACEE2C, 8, 0x0C041A18));


                Patches.Add(new Patch(0x00002CF0, 8, 0x03E00008, "SpeedUP: Remove printf Debug Function"));
                Patches.Add(new Patch(0x00002CF8, 8, 0));
                Patches.Add(new Patch(0x00C09D78, 4, 0x1000, "SpeedUP: Remove the Exception Handler On osStartThread"));

                Patches.Add(new Patch(0x00B415B3, 2, 0x74, "RAM Expansion Hack"));
                Patches.Add(new Patch(0x00B33C5F, 2, 0x4D));

                Patches.Add(new Patch(0x00B0EF9F, 2, 0x20, "More RAM Space For Loaded Files"));
                Patches.Add(new Patch(0x00B0EFBF, 2, 0x20));
                Patches.Add(new Patch(0x00B0EFE7, 2, 0x20));
                Patches.Add(new Patch(0x00B0F00F, 2, 0x20));
                Patches.Add(new Patch(0x00B0F013, 2, 0x20));

                Patches.Add(new Patch(0x00005DFC, 8, 0xAFA10008, "Faster Boot Loading(For RAM) & Extended RAM"));
                Patches.Add(new Patch(0x00005E00, 8, 0));

                Patches.Add(new Patch(0x00B36B6B, 2, 0x03, "Subscreen Delay Fix"));
                Patches.Add(new Patch(0x00B3A994, 8, 0));

                Patches.Add(new Patch(0x00AB33C8, 8, 0, "Collision bounds fix"));
                Patches.Add(new Patch(0x00AB33D0, 8, 0));

                if (!z64rom)
                {

                    List<byte> data = new List<byte>();

                    BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));

                    foreach (Patch patch in Patches)
                    {
                        if (patch.byteamount == 2) data.Add((byte)patch.data);
                        else if (patch.byteamount == 4) Helpers.Append16(ref data, (ushort)patch.data);
                        else if (patch.byteamount == 8) Helpers.Append32(ref data, patch.data);
                        else if (patch.byteamount > 8) data.AddRange(patch.bytedata);

                        BWS.Seek(patch.offset, SeekOrigin.Begin);
                        BWS.Write(data.ToArray());
                        data.Clear();

                        if (patch.name != "") Console.WriteLine(patch.name);
                    }

                    BWS.Close();

                    RecalculateCRC(File.Open(ROM, FileMode.Open, FileAccess.ReadWrite));

                    MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (SavePatches(filepath + @"\patch\SO_ExtendRam+FixCrashingBugs.cfg", Patches))
                        MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string ROM = "";
            bool z64rom = false;
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "Rom / z64rom project (*.z64;*.rom,*.toml)|*.z64;*.rom;*.toml|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = saveFileDialog1.FileName;

                    if (ROM.Contains("z64project.toml"))
                        z64rom = true;
                    else if (ROM.Contains(".toml"))
                    {
                        MessageBox.Show("invalid config file, you need to import z64project.toml", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (ROM != "")
            {
                if (!z64rom && IsFileLocked(ROM))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string game = "";
                    string prefix = "";
                    string filepath = "";

                    game = GetVersion(z64rom, ROM, out prefix);
                    if (z64rom) filepath = Path.GetDirectoryName(ROM);

                    if (game != "OOT" || prefix != "DBGMQ")
                    {
                        MessageBox.Show("Only debug rom supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    List<Patch> Patches = new List<Patch>();


                    Patches.Add(new Patch(0x00B20434, 8, 0, "Make File 1 Normal"));
                    Patches.Add(new Patch(0x00BDF648, 4, 0x1000));
                    Patches.Add(new Patch(0x00B20488, 4, 0x1000));
                    Patches.Add(new Patch(0x00B1FE50, 4, 0x1000));
                    Patches.Add(new Patch(0x00B3D954, 4, 0x1000, "Remove Zelda Map Select"));
                    Patches.Add(new Patch(0x00BEEC23, 2, 0, "Remove Inventory Debug"));
                    Patches.Add(new Patch(0x00C1EC04, 4, 0x1000, "Remove Free Movement"));
                    Patches.Add(new Patch(0x00C1EBB4, 4, 0x1000));
                    Patches.Add(new Patch(0x00B3F0F0, 8, 0, "Disable controllers 2-4"));



                    Patches.Add(new Patch(0x00ADC49C, 4, 0x1000, "Remove Cutscene Control"));
                    Patches.Add(new Patch(0x00ADF480, 4, 0x1000));
                    Patches.Add(new Patch(0x00B3B930, 4, 0x1000, "Remove Input Display"));
                    Patches.Add(new Patch(0x00AD0AA0, 8, 0x00007021, "Remove Debug Camera"));
                    Patches.Add(new Patch(0x00AD0AEC, 4, 0x1000));

                    if (!z64rom)
                    {
                        List<byte> data = new List<byte>();

                        BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));

                        foreach (Patch patch in Patches)
                        {
                            if (patch.byteamount == 2) data.Add((byte)patch.data);
                            else if (patch.byteamount == 4) Helpers.Append16(ref data, (ushort)patch.data);
                            else if (patch.byteamount == 8) Helpers.Append32(ref data, patch.data);
                            else if (patch.byteamount > 8) data.AddRange(patch.bytedata);

                            BWS.Seek(patch.offset, SeekOrigin.Begin);
                            BWS.Write(data.ToArray());
                            data.Clear();

                            if (patch.name != "") Console.WriteLine(patch.name);
                        }
                        BWS.Close();

                        RecalculateCRC(File.Open(ROM, FileMode.Open, FileAccess.ReadWrite));
                        MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (SavePatches(filepath + @"\patch\SO_RemoveDebugFeatures.cfg", Patches))
                            MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }



                }
            }
        }

        private string GetVersion(bool z64rom, string ROM, out string prefix)
        {
            string game = "";
            prefix = "";
            string filepath = "";
            ROM rom = new ROM();

            if (!z64rom)
            {
                rom = CheckVersion(new List<byte>(File.ReadAllBytes(ROM)));
                game = rom.Game;
                prefix = rom.Prefix;
                return game;
            }
            else
            {


                string[] lines = File.ReadAllLines(ROM);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("z_rom_type"))
                    {
                        string tmp = lines[i].Split('=')[1];
                        if (tmp.Contains('#'))
                            tmp = tmp.Split('#')[0];

                        if (tmp.Contains("oot"))
                        {
                            game = "OOT";
                            if (tmp.Contains("debug"))
                                prefix = "DBGMQ";
                            else if (tmp.Contains("u10"))
                                prefix = "N0";
                        }
                        else
                            game = "MM";
                    }
                }
                return game;
            }
        }

        private bool SavePatches(string filename, List<Patch> Patches)
        {
            if (File.Exists(filename) && IsFileLocked(filename))
            {
                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if (File.Exists(filename))
                    File.Delete(filename);

                StreamWriter BWS = new StreamWriter(File.OpenWrite(filename));

                bool firstline = true;

                foreach (Patch patch in Patches)
                {
                    if (patch.name != "")
                    {

                        BWS.Write(((firstline) ? "" : Environment.NewLine) + "# " + patch.name + Environment.NewLine);
                        firstline = false;
                    }
                    if (patch.byteamount <= 8)
                    {
                        BWS.Write("0x" + patch.offset.ToString("X8") + " = 0x" + patch.data.ToString("X" + patch.byteamount) + Environment.NewLine);
                    }
                    else
                    {
                        BWS.Write("0x" + patch.offset.ToString("X8") + " = 0x");
                        foreach (byte b in patch.bytedata)
                        {
                            BWS.Write(b.ToString("X2"));
                        }
                        BWS.Write(Environment.NewLine);
                    }




                    if (patch.name != "") Console.WriteLine(patch.name);
                }
                BWS.Close();
                return true;
            }
        }

        private void OpenGlobalROM_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "N64 Rom / z64rom project (*.z64;*.toml)|*.z64;*.toml|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            injectToROMToolStripMenuItem.Owner.Hide();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenGlobalFile(openFileDialog1.FileName);
            }
        }

        public void OpenGlobalFile(string filename)
        {
            FileInfo info = new FileInfo(filename);

            injectToROMToolStripMenuItem.Text = "&Inject to ROM";

            if (info.Extension != ".toml")
            {
                if (info.Length < 33554432 + 50000)
                {
                    MessageBox.Show("This ROM is not uncompressed! go to Tools > Decompress ROM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                rom64.set("");
                GlobalROM = filename;
                RomModeLabel.Text = "Global ROM Mode: ON";
                RomModeLabel.ForeColor = Color.Green;
                injectToROMToolStripMenuItem.DropDownItems.Clear();
                RefreshExitCache();
                RefreshActorCache();
                RefreshObjectCache();
                RefreshRecetMenuItems(ref OpenGlobalROM, "GlobalFile", GlobalROM);
                GlobalRomRefresh.Visible = true;
                LaunchRomToolStripMenuItem.Visible = true;


                ROM rom = CheckVersion(new List<byte>(File.ReadAllBytes(GlobalROM)));
                if (rom.Game == "MM")
                {
                    settings.MajorasMask = true;
                    majorasMaskModeexperimentalToolStripMenuItem.Checked = true;
                }
                else
                {
                    settings.MajorasMask = false;
                    majorasMaskModeexperimentalToolStripMenuItem.Checked = false;
                }

                UpdateForm();
            }
            else
            {
                injectToROMToolStripMenuItem.Text = "Send to &z64rom";
                rom64.set(filename);
                GlobalROM = "";
                RomModeLabel.Text = "Global z64rom Mode: ON";
                RomModeLabel.ForeColor = Color.Green;
                injectToROMToolStripMenuItem.DropDownItems.Clear();
                RefreshExitCache();
                RefreshActorCache();
                RefreshObjectCache();
                RefreshRecetMenuItems(ref OpenGlobalROM, "GlobalFile", filename);
                GlobalRomRefresh.Visible = true;
                LaunchRomToolStripMenuItem.Visible = true;
            }
        }

        public void RefreshExitCache()
        {
            if (GlobalROM != "")
            {
                List<byte> EntranceTable = new List<byte>(File.ReadAllBytes(GlobalROM));

                ROM rom = MainForm.CheckVersion(EntranceTable);


                ushort variable;
                string scenename = "";

                int offset = (int)rom.EntranceTableStart;
                ushort count = 0;

                ExitCache.Clear();

                while (offset < ((int)rom.EntranteTableEnd) - 1)
                {

                    if (!MainForm.settings.MajorasMask || (MainForm.settings.MajorasMask && EntranceTable[offset] != 0x80))
                        ExitCache.Add(count, "(Scene " + EntranceTable[offset].ToString("X2") + " Spawn " + EntranceTable[offset + 1] + ")");
                    offset += 4;
                    count++;
                }
            }
            else if (rom64.isSet())
            {
                ExitCache.Clear(); //nothing to do for now
            }
        }

        public void RefreshActorCache()
        {
            ActorCache.Clear();
            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + gameprefix + "ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Actor");

            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    List<ActorProperty> properties = new List<ActorProperty>();
                    string name = "";
                    string objects = "";
                    ushort id = 0;

                    XmlAttributeCollection nodeAtt = node.Attributes;



                    /*

                    if (nodeAtt["Properties"] != null)
                    {
                        string[] names = nodeAtt["PropertiesNames"].Value.Split(',');
                        string[] values = nodeAtt["Properties"].Value.Split(',');
                        string[] targets = null;
                        if (nodeAtt["PropertiesTarget"] != null) targets = nodeAtt["PropertiesTarget"].Value.Split(',');
                    

                    for (int i = 0; i < names.Length; i++)
                    {
                        ActorProperty property = new ActorProperty();
                        property.Name = names[i];
                        property.Mask = Convert.ToUInt16(values[i], 16);
                        if (targets != null) property.Target = targets[i];
                        else property.Target = "Var";

                        property.Max = property.Mask;
                        while ((property.Max & 1) == 0)
                        {
                            property.Max = (ushort)(property.Max >> 1);
                            property.Position += 1;
                        }

                        properties.Add(property);

                    }

                    }
                    */

                    if (nodeAtt["Name"] != null) name = nodeAtt["Name"].Value;
                    if (nodeAtt["Object"] != null) objects = nodeAtt["Object"].Value;
                    id = Convert.ToUInt16(nodeAtt["Key"].Value, 16);

                    properties = XMLreader.getActorProperties(nodeAtt["Key"].Value);

                    ActorCache.Add(id, new ActorInfo(name, properties, objects));

                }
            fs.Close();

            if (GlobalROM != "")
            {
                List<byte> ActorTable = new List<byte>(File.ReadAllBytes(GlobalROM));

                ROM rom = MainForm.CheckVersion(ActorTable);

                int offset = (int)rom.ActorTable;
                ushort count = 0;

                while (offset < ((int)rom.ActorTableEnd) - 1)
                {
                    if (!ActorCache.ContainsKey(count))
                    {
                        uint initvars = Helpers.Read32(ActorTable, offset + 0x14);
                        uint ramstart = Helpers.Read32(ActorTable, offset + 0x8);
                        uint romstart = Helpers.Read32(ActorTable, offset);
                        initvars = initvars - ramstart + romstart;
                        if (romstart != 0 && initvars != 0)
                        {
                            ushort obj = Helpers.Read16(ActorTable, (int)(initvars + 8));
                            byte type = ActorTable[(int)(initvars + 2)];
                            string name = "";

                            if (type == 4)
                            {
                                name = "Custom NPC " + count.ToString("X4");
                            }
                            else if (type == 5)
                            {
                                name = "Custom Enemy " + count.ToString("X4");
                            }
                            else if (type == 9)
                            {
                                name = "Custom Boss " + count.ToString("X4");
                            }
                            else
                            {
                                name = "Custom Actor " + count.ToString("X4");
                            }

                            ActorCache.Add(count, new ActorInfo(name, new List<ActorProperty>(), obj.ToString("X4")));
                        }
                        else
                        {
                            ActorCache.Add(count, new ActorInfo("Invalid", new List<ActorProperty>(), ""));
                        }
                    }

                    offset += 0x20;
                    count++;
                }

            }

            if (rom64.isSet())
            {
                List<String> actors = rom64.getList("src\\actor");
                List<String> warning = new List<String>();

                foreach (var str in actors)
                {
                    string basename = "";
                    ushort index = 0;

                    if (!rom64.getNameAndIndex(str, ref basename, ref index))
                        continue;

                    TomlTable toml = rom64.parseToml(str + "\\actor.toml");
                    List<ActorProperty> prop = null;

                    if (toml != null)
                        if (toml.HasKey("Name"))
                            basename = toml["Name"].AsString;

                    if (toml != null && toml.HasKey("Property"))
                    {
                        prop = new List<ActorProperty>();
                        TomlArray proparr = toml["Property"].AsArray;

                        foreach (TomlTable tbl in proparr)
                        {
                            if (!tbl.HasKey("Mask") || !tbl.HasKey("Name") || !tbl.HasKey("Target"))
                            {

                                /*
                                 * If only some of the requirement parameters are set,
                                 * consider this an error from the user and warn
                                 * accordingly.
                                 */
                                if (tbl.HasKey("Mask") || tbl.HasKey("Name") || tbl.HasKey("Target"))
                                {
                                    string msg = str + "\\actor.toml\n";

                                    msg = msg.Remove(0, rom64.getPath().Length + 1);

                                    if (!tbl.HasKey("Mask"))
                                        msg += "Mask = ??\n";
                                    if (!tbl.HasKey("Name"))
                                        msg += "Name = ??\n";
                                    if (!tbl.HasKey("Target"))
                                        msg += "Target = ??\n";

                                    warning.Add(msg);
                                }

                                continue;
                            }

                            ActorProperty p = new ActorProperty(
                                (ushort)tbl["Mask"].AsInteger.Value,
                                tbl["Name"].AsString,
                                tbl["Target"].AsString
                            );

                            Console.WriteLine("Name: " + p.Name);
                            Console.WriteLine("Mask: " + tbl["Mask"].AsInteger.Value.ToString("X4"));
                            Console.WriteLine("Max:  " + p.Max.ToString("X4"));

                            if (tbl.HasKey("Dropdown"))
                            {
                                TomlArray droparr = tbl["Dropdown"].AsArray;
                                int max_str_length = p.Max.ToString("X").Length;

                                SongItem dropdown = new SongItem();
                                dropdown.Value = (ushort)0;
                                dropdown.Text = "Unknown";
                                p.DropdItems.Add(dropdown);

                                foreach (TomlArray arr in droparr)
                                {
                                    dropdown = new SongItem();

                                    dropdown.Value = (ushort)arr[0].AsInteger.Value;
                                    dropdown.Text = ((ushort)dropdown.Value).ToString("X" + max_str_length) + " - " + arr[1].AsString;

                                    p.DropdItems.Add(dropdown);
                                }
                            }

                            prop.Add(p);
                        }
                    }

                    if (ActorCache.ContainsKey(index))
                    {
                        if (prop == null)
                            prop = XMLreader.getActorProperties(index.ToString("X4"));
                        ActorCache.Remove(index);
                    }

                    if (prop == null)
                        prop = new List<ActorProperty>();

                    string rompath = str.Replace("src\\", "rom\\");
                    uint objectid = rom64.getActorObjID(rompath);

                    ActorCache.Add(index, new ActorInfo(basename, prop, objectid.ToString("X4")));
                }

                if (warning.Count > 0)
                {
                    string msg = String.Join("\n", warning);

                    MessageBox.Show("Warning! Following toml files do not have some of the required variables defined!\n\n" + msg, "actor.toml warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void RefreshObjectCache()
        {
            ObjectCache.Clear();

            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + gameprefix + "ObjectData.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Object");

            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    if (nodeAtt["Key"] != null)
                    {
                        string name = "";
                        int size = 0;


                        if (nodeAtt["Size"] != null) size = Convert.ToInt32(nodeAtt["Size"].Value, 16);
                        name = node.InnerText;

                        ObjectCache.Add(Convert.ToUInt16(nodeAtt["Key"].Value, 16), new ObjectInfo(size, name, ""));
                    }
                }


            if (GlobalROM != "")
            {


                List<byte> ObjectTable = new List<byte>(File.ReadAllBytes(GlobalROM));

                ROM rom = MainForm.CheckVersion(ObjectTable);

                int offset = (int)rom.ObjectTable;
                ushort count = 0;

                uint ObjectTableEnd = rom.ObjectTable + 8 + (Helpers.Read32(ObjectTable, (int)(rom.ObjectTable + 4)) * 8);

                offset += 8;

                while (offset < (ObjectTableEnd) - 1)
                {
                    if (!ObjectCache.ContainsKey(count))
                    {

                        uint start = Helpers.Read32(ObjectTable, offset);
                        uint end = Helpers.Read32(ObjectTable, offset + 4);

                        if (start != 0 && end != 0)
                        {

                            string name = "Custom Object";


                            ObjectCache.Add(count, new ObjectInfo((int)(end - start), name, ""));
                        }
                        else
                        {
                            ObjectCache.Add(count, new ObjectInfo(0, "Invalid", ""));
                        }
                    }

                    offset += 0x8;
                    count++;
                }

            }

            if (rom64.isSet())
            {
                List<String> objects = rom64.getList("src\\object");

                foreach (var str in objects)
                {
                    var basename = Path.GetFileNameWithoutExtension(str + ".exe");

                    if (!basename.StartsWith("0x"))
                        continue;

                    var indexname = basename.Substring(2, basename.IndexOf("-") - 2);

                    if (!ushort.TryParse(indexname, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort index))
                        continue;

                    basename = basename.Substring(basename.IndexOf("-") + 1);

                    var rompath = str.Replace("src\\", "rom\\");
                    long size = new FileInfo(rompath + "\\object.zobj").Length;

                    if (ObjectCache.ContainsKey(index))
                        ObjectCache.Remove(index);

                    ObjectCache.Add(index, new ObjectInfo((int)size, basename, ""));
                }
            }

            fs.Close();



            foreach (KeyValuePair<ushort, ObjectInfo> obj in ObjectCache)
            {
                foreach (KeyValuePair<ushort, ActorInfo> actor in ActorCache)
                {

                    if (actor.Value.objects.Contains(obj.Key.ToString("X4")))
                    {
                        if (obj.Value.usedby.Length > 0) obj.Value.usedby += ",";
                        obj.Value.usedby += actor.Value.name;

                    }
                }
            }


        }

        private void ExitList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DatabaseButton_Click(object sender, EventArgs e)
        {
            if (rom64.isSet())
            {
                MainForm.zobj_cache.Clear();
                CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
            }
            RefreshExitCache();
            RefreshExitLabels();
            RefreshActorCache();
            RefreshObjectCache();
            UpdateForm();
        }

        private void dontConvertMultitextureToRGBAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.DontConvertMultitexture = dontConvertMultitextureToRGBAToolStripMenuItem.Checked;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            settings.CheckEmptyOffset = CheckEmptyOffsetItem.Checked;
        }

        private void numericUpDownEx1_ValueChanged_2(object sender, EventArgs e)
        {
            SetViewport(glControl1.Width, glControl1.Height);
        }

        private void RenderChildLinkMenuItem_Click(object sender, EventArgs e)
        {
            settings.RenderChildLink = RenderChildLinkMenuItem.Checked;
        }

        private void GroupSmoothRgbaEdges_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).SmoothRGBAEdges = GroupSmoothRgbaEdges.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.SmoothRGBAEdges[Index] = ((ObjFile.Group)GroupList.SelectedItem).SmoothRGBAEdges;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void IgnoreMajorasMaskDaySystem_Click(object sender, EventArgs e)
        {
            settings.IgnoreMMDaySystem = IgnoreMajorasMaskDaySystem.Checked;
            if (settings.MajorasMask)
            {
                ActorCache.Clear();
                actorEditControl1.UpdateForm();
                actorEditControl2.UpdateForm();
                actorEditControl3.UpdateForm();
                UpdateForm();
            }
        }

        private void EnvColor_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).EnvColor = GroupEnvColor.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.EnvColor[Index] = ((ObjFile.Group)GroupList.SelectedItem).EnvColor;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void CameraUnk1_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk1.Value) > 1820)
                CameraUnk1.Value += (CameraUnk1.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk1) * 9;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk1 = (ushort)CameraUnk1.Value;

            UpdateCameraEdit();
        }

        private void CameraUnk2_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk2.Value) > 1820)
                CameraUnk2.Value += (CameraUnk2.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk2) * 9;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk2 = (ushort)CameraUnk2.Value;

            UpdateCameraEdit();
        }

        private void GroupLod_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).LOD = GroupLod.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.LOD[Index] = ((ObjFile.Group)GroupList.SelectedItem).LOD;

                UpdateGroupSelect();
            }
        }

        private void GroupLODGroup_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).LodGroup = (int)GroupLODGroup.Value;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.LodGroup[Index] = ((ObjFile.Group)GroupList.SelectedItem).LodGroup;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void GroupLODDIstance_ValueChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).LodDistance = (int)GroupLODDIstance.Value;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.LodDistance[Index] = ((ObjFile.Group)GroupList.SelectedItem).LodDistance;

                UpdateGroupSelect(n64refresh);
            }
        }

        private void RenderSelectedCutsceneCommandsMenuItem_Click(object sender, EventArgs e)
        {
            settings.DrawSelectedCutsceneCommands = RenderSelectedCutsceneCommandsMenuItem.Checked;
        }

        private void dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clipboard = "";

            foreach (ZActor actor in CurrentScene.Rooms[RoomList.SelectedIndex].ZActors)
            {
                clipboard += "<Actor id=\"" + actor.Number.ToString("X4") + "\" var=\"" + actor.Variable.ToString("X4") +
                    "\" Position=\"Center\" X=\"" + Math.Round(actor.XPos % 1500) + "\" Y=\"" + Math.Round(actor.YPos % 1500) + "\" Z=\"" + Math.Round(actor.ZPos % 1500) + "\"";
                if (actor.XRot != 0) clipboard += " XRot=\"" + Math.Round(actor.XRot) + "\"";
                if (actor.YRot != 0) clipboard += " YRot=\"" + Math.Round(actor.YRot) + "\"";
                if (actor.ZRot != 0) clipboard += " ZRot=\"" + Math.Round(actor.ZRot) + "\"";
                clipboard += " Amount=\"1\"></Actor>";
                clipboard += " <!-- " + XMLreader.getActorName(actor.Number.ToString("X4")) + " -->\n";
            }

            Clipboard.SetText(clipboard);
        }

        private void dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clipboard = "";
            foreach (ZEnvironment env in CurrentScene.Environments)
            {
                clipboard += "CurrentScene.Environments.Add(new ZEnvironment(\n" +
                "Color.FromArgb(" + env.C1 + "), Color.FromArgb(" + env.C2 + "), Color.FromArgb(" + env.C3 + "), Color.FromArgb(" + env.C4 + "), Color.FromArgb(" + env.C5 + "),\n" +
                "Color.FromArgb(" + env.FogColor + "), 0x" + env.FogDistance.ToString("X4") + ", 0x" + env.DrawDistance.ToString("X4") + ", 0x" + env.FogUnknown.ToString("X2") + ")); \n";
            }

            Clipboard.SetText(clipboard);
        }

        private void dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clipboard = "";

            foreach (ZActor actor in CurrentScene.Rooms[RoomList.SelectedIndex].ZActors)
            {
                int[] objid = XMLreader.getActorObject("" + actor.Number.ToString("X4"));
                string obj = "";
                if (objid.Length > 0 && ObjectCache.ContainsKey((ushort)objid[0])) obj = ObjectCache[(ushort)objid[0]].name;

                clipboard += "<Actor Key=\"" + actor.Number.ToString("X4") + "\" var=\"" + actor.Variable.ToString("X4") +
                    "\" Scale=\"0.1\" Offset=\"00000000\">";
                clipboard += obj;
                clipboard += "</Actor>";
                clipboard += " <!-- " + XMLreader.getActorName(actor.Number.ToString("X4")) + " -->\n";
            }

            Clipboard.SetText(clipboard);


        }

        private void GroupAlphaMask_CheckedChanged(object sender, EventArgs e)
        {
            ((ObjFile.Group)GroupList.SelectedItem).AlphaMask = GroupAlphaMask.Checked;

            int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
            CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.AlphaMask[Index] = ((ObjFile.Group)GroupList.SelectedItem).AlphaMask;

            UpdateGroupSelect(n64refresh);
        }

        private void GroupRenderLast_CheckedChanged(object sender, EventArgs e)
        {
            ((ObjFile.Group)GroupList.SelectedItem).RenderLast = GroupRenderLast.Checked;

            int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
            CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.RenderLast[Index] = ((ObjFile.Group)GroupList.SelectedItem).RenderLast;

            UpdateGroupSelect(n64refresh);
        }

        private void GroupVertexNormals_CheckedChanged(object sender, EventArgs e)
        {
            ((ObjFile.Group)GroupList.SelectedItem).VertexNormals = GroupVertexNormals.Checked;

            int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
            CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.VertexNormals[Index] = ((ObjFile.Group)GroupList.SelectedItem).VertexNormals;

            UpdateGroupSelect(n64refresh);
        }

        private void DisableRGBA32ToolStrip_Click(object sender, EventArgs e)
        {
            settings.DisableRGBA32 = DisableRGBA32ToolStrip.Checked;
        }

        private void CutscenePositionPlayMode_Click(object sender, EventArgs e)
        {
            previewcutscene = !previewcutscene;

            if (previewcutscene)
            {
                if (CurrentScene.Cutscene[MarkerSelect.SelectedIndex].Points.Count < 4)
                {
                    MessageBox.Show("You need atleast 4 camera points to play the command!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CutscenePreview_Clear();
                }
                else
                {
                    CutscenePreview_Set();
                }
            }
            else
                CutscenePreview_Clear();

            UpdateCutsceneEdit();
        }

        private void openZmapToolstrip_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZScene File (*.zscene)|*.zscene|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            injectToROMToolStripMenuItem.Owner.Hide();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                List<byte> data = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                LoadZscene(data);

            }
        }

        private void LoadZscene(List<byte> data, int sceneid = -1)
        {
            //TODO

            newSceneToolStripMenuItem_Click(new object(), new EventArgs());
            SimulateN64Gfx = false;

            List<byte> originaldata = new List<byte>();
            List<List<byte>> roomlist = new List<List<byte>>();
            ROM rom;
            uint zsceneoffset = 0, zsceneendoffset = 0;
            if (sceneid != -1)
            {
                originaldata = new List<byte>(data);
                rom = MainForm.CheckVersion(data);


                zsceneoffset = Helpers.Read32(data, (int)(rom.SceneTable + sceneid * 0x14));
                zsceneendoffset = Helpers.Read32(data, (int)(rom.SceneTable + sceneid * 0x14 + 4));


                data = new List<byte>(originaldata.GetRange((int)zsceneoffset, (int)(zsceneendoffset - zsceneoffset)));
            }

            // Console.WriteLine(zsceneoffset.ToString("X8"));
            //  Console.WriteLine(zsceneendoffset.ToString("X8"));

            newSceneToolStripMenuItem_Click(new object(), new EventArgs());

            CurrentScene.Environments.Clear();
            CurrentScene.SpawnPoints.Clear();
            CurrentScene.Transitions.Clear();
            CurrentScene.Pathways.Clear();
            CurrentScene.Waterboxes.Clear();

            CurrentScene.PregeneratedMesh = true;



            bool collision = false;

            //zscene stuff

            List<uint> headeroffsets = new List<uint>();
            headeroffsets.Add(0);


            CurrentScene.OriginalSceneData = new List<byte>(data);

            if (Helpers.Read32(data, 0) == 0x18000000)
            {
                int offset = Helpers.Read24S(data, 5);
                int count = 1;
                while (offset < data.Count)
                {
                    uint test = Helpers.Read24(data, offset + 1);
                    if ((data[offset] == 0x02 || Helpers.Read32(data, offset) == 0) && test < data.Count)
                    {

                        ZScene clone = CurrentScene.Clone();
                        clone.cloneid = CurrentScene.SceneHeaders.Count + 1;
                        CurrentScene.SceneHeaders.Add(new ZSceneHeader(data[offset] != 0x02, clone));

                        headeroffsets.Add(test);

                    }
                    else if (Helpers.Read32(data, offset) != 0)
                    {
                        break;
                    }

                    count++;
                    offset += 4;

                }

            }

            int cnt = 0;
            bool command19 = false;
            bool command15 = false;
            bool command13 = false;
            bool command11 = false;
            bool command7 = false;
            bool command4;
            int exitcommandoffset = 0;
            int roomcount = 0;

            foreach (uint headeroffset in headeroffsets)
            {
                command19 = false;
                command15 = false;
                command13 = false;
                command11 = false;
                command7 = false;
                command4 = false;

                if (cnt == 0 || headeroffset != 0)
                {
                    SetSceneHeader(cnt);

                    for (int y = (int)headeroffset; y < data.Count; y += 4)
                    {
                        uint test = Helpers.Read32(data, y);

                        if (((test & 0xFF00FFFF) == 0x14000000 && data[y + 4] == 0x00))
                        {
                            break;
                        }


                        if (!collision)
                        {
                            ObjFile objfile = importCollision(data, y);
                            if (objfile != null)
                            {

                                string colfilename = objfile.ConvertToObject("Import/Collision_" + DateTime.Now.Ticks.ToString());
                                CurrentScene.ColModel = new ObjFile(colfilename, true);

                                CollisionTextbox.Text = saveFileDialog1.FileName;

                                collision = true;

                                CurrentScene.CollisionFilename = colfilename;

                                importCamerasWaterboxes(data, y);
                            }

                        }

                        if (CurrentScene.Transitions.Count == 0 || CurrentScene.SpawnPoints.Count == 0) importTransitionsSpawns(data, y);
                        if (CurrentScene.Waterboxes.Count == 0 || CurrentScene.Cameras.Count == 0) importCamerasWaterboxes(data, y);
                        if (CurrentScene.Environments.Count == 0) importEnvironments(data, y);
                        if (CurrentScene.Pathways.Count == 0) importPathways(data, y);

                        //cutscene
                        if (CurrentScene.Cutscene.Count == 0 && ((test & 0xFFFFFFFF) == 0x17000000 && data[y + 4] == 0x02))
                        {
                            importCutscene(data, (int)Helpers.Read24S(data, y + 5));
                        }

                        //music settings
                        if (!command15 && ((test & 0xFF00FFFF) == 0x15000000 && Helpers.Read16(data, y + 4) == 0))
                        {
                            command15 = true;
                            //unknown = data[y + 1];
                            CurrentScene.NightSFX = data[y + 6];
                            CurrentScene.Music = data[y + 7];


                        }

                        //exits
                        if ((!command13 && ((test & 0xFFFFFFFF) == 0x13000000 && data[y + 4] == 0x02)) || (collision && exitcommandoffset > 0 && !command13))
                        {
                            if (exitcommandoffset == 0) exitcommandoffset = y;

                            if (CurrentScene.ColModel == null)
                            {

                                continue;
                            }


                            command13 = true;

                            foreach (ObjFile.Group Group in CurrentScene.ColModel.Groups)
                            {
                                ulong polytype = 0;
                                ushort polyflags = (ushort)((Group.Name.ToLower().Contains("#ignorecamera") ? 0x2000 : 0) + (Group.Name.ToLower().Contains("#ignoreactors") ? 0x4000 : 0) + (Group.Name.ToLower().Contains("#ignoreprojectiles") ? 0x8000 : 0));
                                ushort polyflagsB = (ushort)(((Group.Name.ToLower().Contains("#speed") || Group.Name.ToLower().Contains("#direction")) && !Group.Name.ToLower().Contains("#waterstream") ? 0x2000 : 0));

                                if (!UInt64.TryParse(Group.Name.Substring(Group.Name.IndexOf("#Raw") + 4, 16), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out polytype))
                                {
                                    MessageBox.Show("Bad usage of Raw tag. It should be #RawXXXXXXXXXXXXXXXX (XXXXXXXXXXXXXXX = polytype raw data in Hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                                int polyid = CurrentScene.PolyTypes.FindIndex(x => x.Raw == polytype && x.PolyFlagA == polyflags && x.PolyFlagB == polyflagsB);
                                if (polyid == -1)
                                {
                                    CurrentScene.PolyTypes.Add(new ZColPolyType(polytype, polyflags, polyflagsB));

                                }
                            }

                            int entries = (int)(CurrentScene.PolyTypes.MaxBy(x => x.ExitNumber))[0].ExitNumber;

                            int exitlistoffset = (int)Helpers.Read24(data, exitcommandoffset + 5);

                            for (int i = exitlistoffset; i < exitlistoffset + entries * 2; i += 2)
                            {
                                CurrentScene.ExitList.Add(new ZScene.ZUShort(Helpers.Read16(data, i)));
                            }

                        }

                        //skybox settings
                        if (!command11 && ((test & 0xFFFFFFFF) == 0x11000000 && data[y + 7] == 0))
                        {
                            command11 = true;

                            CurrentScene.SkyboxType = data[y + 4];
                            CurrentScene.Cloudy = data[y + 5] == 1;
                            CurrentScene.OutdoorLight = data[y + 6] == 0;

                        }

                        //special object
                        if (!command7 && ((test & 0xFF00FFFF) == 0x07000000 && Helpers.Read16(data, y + 4) == 0))
                        {
                            command7 = true;

                            CurrentScene.ElfMessage = data[y + 1];
                            CurrentScene.SpecialObject = Helpers.Read16(data, y + 6);


                        }

                        //worldmap
                        if (!command19 && ((test & 0xFF00FFFF) == 0x19000000 && Helpers.Read16(data, y + 4) == 0))
                        {
                            command19 = true;

                            CurrentScene.CameraMovement = data[y + 1];
                            CurrentScene.WorldMap = data[y + 7];


                        }

                        //rooms
                        if (!command4 && ((test & 0xFF00FFFF) == 0x04000000 && data[y + 4] == 0x02))
                        {
                            command4 = true;

                            roomcount = data[y + 1];

                            if (sceneid != -1)
                            {
                                uint roomlistoffset = Helpers.Read32(data, y + 4) & 0x00FFFFFF;

                                for (int i = 0; i < roomcount; i++)
                                {
                                    uint roomstartoffset = Helpers.Read32(data, (int)(roomlistoffset + (i * 8)));
                                    uint roomendoffset = Helpers.Read32(data, (int)(roomlistoffset + (i * 8) + 4));
                                    roomlist.Add(new List<byte>(originaldata.GetRange((int)roomstartoffset, (int)(roomendoffset - roomstartoffset))));

                                    //     Console.WriteLine("roomstartoffset " + roomstartoffset.ToString("X8"));
                                    //       Console.WriteLine("roomendoffset" + roomendoffset.ToString("X8"));
                                }
                            }



                        }


                    }

                    if (!collision)
                    {
                        MessageBox.Show("Collision not found, aborting import", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



                }
                cnt++;
            }

            if (roomcount == 0)
            {
                MessageBox.Show("The scene has no rooms, aborting import", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool commandA = false;
            bool command5 = false;
            bool command8 = false;
            bool commandC = false;
            bool command10 = false;
            bool command12 = false;
            bool command16 = false;
            bool isz64rom = false;

            string basestr = "";
            string currentroom = "";
            if (sceneid == -1)
            {
                if (openFileDialog1.FileName.Contains("_scene"))
                    basestr = (openFileDialog1.FileName).Substring(0, (openFileDialog1.FileName).IndexOf("_scene")) + "_";
                else
                {
                    isz64rom = true;
                    basestr = (openFileDialog1.FileName).Substring(0, (openFileDialog1.FileName).IndexOf("scene.zscene"));
                }
            }
            SetSceneHeader(0);

            for (int r = 0; r < roomcount; r++)
            {
                if (sceneid == -1)
                {

                    currentroom = basestr + "room_" + r + (isz64rom ? ".zroom" : ".zmap");
                    if (!File.Exists(currentroom))
                    {
                        MessageBox.Show("Room file " + currentroom + " not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    data = new List<byte>(File.ReadAllBytes(currentroom));
                }
                else
                {
                    currentroom = "Room" + r + (isz64rom ? ".zroom" : ".zmap");

                    data = roomlist[r];
                }


                CurrentScene.AddRoom(currentroom);

                foreach (ZSceneHeader header in CurrentScene.SceneHeaders)
                {
                    header.Scene.AddRoom(currentroom);

                }



                // RoomList.DataSource = CurrentScene.Rooms;
                ((CurrencyManager)RoomList.BindingContext[CurrentScene.Rooms]).Refresh();


                if (CurrentScene.Rooms.Count != RoomList.Items.Count)
                {
                    // RoomList.DataSource = null;
                    // RoomList.Items.Add(currentroom);

                    MessageBox.Show("Header missmatch", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Console.WriteLine(CurrentScene.Rooms.Count);
                    Console.WriteLine(RoomList.Items.Count);
                    return;
                }

                RoomList.SelectedIndex = r;


                headeroffsets = new List<uint>();
                headeroffsets.Add(0);

                // TODO get textures

                if (Helpers.Read32(data, 0) == 0x18000000)
                {
                    int offset = Helpers.Read24S(data, 5);
                    int count = 1;
                    while (offset < data.Count)
                    {
                        uint test = Helpers.Read24(data, offset + 1);
                        if ((data[offset] == 0x03 || Helpers.Read32(data, offset) == 0) && test < data.Count)
                        {

                            ZScene clone = CurrentScene.Clone();
                            clone.cloneid = CurrentScene.SceneHeaders.Count + 1;
                            CurrentScene.SceneHeaders.Add(new ZSceneHeader(data[offset] != 0x03, clone));

                            headeroffsets.Add(test);

                        }
                        else if (Helpers.Read32(data, offset) != 0)
                        {
                            break;
                        }

                        count++;
                        offset += 4;

                    }

                }

                commandA = false;

                CurrentScene.Rooms[r].OriginalRoomData = new List<byte>(data);

                cnt = 0;

                foreach (uint headeroffset in headeroffsets)
                {
                    command5 = false;
                    command8 = false;
                    commandC = false;
                    command10 = false;
                    command12 = false;
                    command16 = false;

                    if (cnt == 0 || headeroffset != 0)
                    {
                        SetSceneHeader(cnt);

                        for (int y = (int)headeroffset; y < data.Count; y += 4)
                        {
                            uint test = Helpers.Read32(data, y);

                            if (((test & 0xFF00FFFF) == 0x14000000 && data[y + 4] == 0x00))
                            {
                                break;
                            }

                            if (CurrentScene.Rooms[r].ZActors.Count == 0 || CurrentScene.Rooms[r].ZObjects.Count == 0) importActorsObjects(data, y);


                            //wind
                            if (!command5 && ((test & 0xFFFFFFFF) == 0x05000000))
                            {
                                command5 = true;

                                CurrentScene.Rooms[r].WindWest = data[y + 4];
                                CurrentScene.Rooms[r].WindVertical = data[y + 5];
                                CurrentScene.Rooms[r].WindSouth = data[y + 6];
                                CurrentScene.Rooms[r].WindStrength = data[y + 7];

                            }

                            //room behaviour
                            if (!command8 && ((test & 0xFFFFFFFF) == 0x08000000 && data[y + 4] == 0x00))
                            {
                                command8 = true;

                                CurrentScene.Rooms[r].Restriction = data[y + 1];
                                CurrentScene.Rooms[r].DisableWarpSongs = ((data[y + 6] & 0xF0) >> 4 == 1);
                                CurrentScene.Rooms[r].ShowInvisibleActors = ((data[y + 6] & 0x0F) == 1);
                                CurrentScene.Rooms[r].IdleAnim = data[y + 7];

                            }

                            //unused lights
                            if (!commandC && ((test & 0xFF00FFFFF) == 0x0C000000 && data[y + 4] == 0x03))
                            {
                                commandC = true;

                                int unusedlightsoffset = (int)Helpers.Read24(data, y + 5);
                                int lightcount = data[y + 1];


                                for (int i = unusedlightsoffset; i < unusedlightsoffset + lightcount * 14; i += 14)
                                {
                                    ZAdditionalLight light = new ZAdditionalLight();
                                    light.PointLight = data[y + 1] == 2 || data[y + 1] == 0;
                                    if (!light.PointLight)
                                    {
                                        light.NSdirection = data[y + 2];
                                        light.EWdirection = data[y + 4];
                                        light.Color = (int)Helpers.Read32(data, y + 5);
                                    }
                                    else
                                    {
                                        light.XPos = Helpers.Read16S(data, y + 2);
                                        light.YPos = Helpers.Read16S(data, y + 4);
                                        light.ZPos = Helpers.Read16S(data, y + 6);
                                        light.Color = (int)Helpers.Read32(data, y + 8);
                                        light.Radius = data[y + 12];
                                    }
                                    CurrentScene.Rooms[r].AdditionalLights.Add(light);
                                }

                            }

                            //time settings
                            if (!command10 && ((test & 0xFFFFFFFF) == 0x10000000))
                            {
                                command10 = true;

                                CurrentScene.Rooms[r].StartTime = Helpers.Read16(data, y + 4);
                                CurrentScene.Rooms[r].TimeSpeed = data[y + 6];


                            }

                            //skybox modifier
                            if (!command12 && ((test & 0xFFFFFFFF) == 0x12000000))
                            {
                                command12 = true;

                                CurrentScene.Rooms[r].DisableSkybox = data[y + 4] == 1;
                                CurrentScene.Rooms[r].DisableSunMoon = data[y + 5] == 1;


                            }

                            //echo
                            if (!command16 && ((test & 0xFFFFFFFF) == 0x16000000))
                            {
                                command16 = true;

                                CurrentScene.Rooms[r].Echo = data[y + 7];


                            }

                            //mesh


                            if (!commandA && ((test & 0xFFFFFFFF) == 0x0A000000 && data[y + 4] == 0x03))
                            {
                                commandA = true;

                                CurrentScene.Rooms[r].OriginalMeshHeaderOffset = (int)Helpers.Read24(data, y + 5);

                                int meshoffset = Helpers.Read24S(data, y + 5);



                                //type1
                                if (data[meshoffset] == 1 && r == 0) //room 0 only
                                {

                                    if (data[meshoffset + 1] == 0x1) // single background
                                    {
                                        int imageoffset = Helpers.Read24S(data, meshoffset + 9);
                                        int endofimage = 0;
                                        //   Console.WriteLine(imageoffset.ToString("X"));
                                        bool continuetillend = false;
                                        for (int i = imageoffset + 2; i < data.Count; i++)
                                        {
                                            ushort marker = Helpers.Read16(data, i);
                                            //   Console.WriteLine(marker.ToString("X"));

                                            if ((marker & 0xFF00) != 0xFF00 && !continuetillend)
                                            {
                                                MessageBox.Show("Prerendered image conversion error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                break;
                                            }

                                            if (marker == 0xFFD9)
                                            {
                                                endofimage = i + 2;
                                                break;
                                            }
                                            if (continuetillend || marker == 0xFFDA)
                                            {
                                                continuetillend = true;

                                            }
                                            else
                                            {
                                                i += Helpers.Read16(data, i + 2) + 2 - 1;
                                            }
                                        }
                                        if (endofimage != 0)
                                        {
                                            List<byte> imagedata = data.GetRange(imageoffset, endofimage - imageoffset);
                                            string filename = "Import/Background0_" + DateTime.Now.Ticks.ToString() + ".jpeg";
                                            File.Create(filename).Close();
                                            File.WriteAllBytes(filename, imagedata.ToArray());


                                            CurrentScene.prerenderimages.Add(filename);



                                        }
                                    }
                                    else // multibackground
                                    {
                                        uint backgroundcount = data[meshoffset + 0x8];
                                        int backgroundsoffset = Helpers.Read24S(data, meshoffset + 0xD);


                                        for (int b = 0; b < backgroundcount; b++)
                                        {

                                            int imageoffset = Helpers.Read24S(data, backgroundsoffset + 0x1C * b + 5);
                                            int endofimage = 0;
                                            Console.WriteLine(imageoffset.ToString("X"));
                                            bool continuetillend = false;
                                            for (int i = imageoffset + 2; i < data.Count; i++)
                                            {
                                                ushort marker = Helpers.Read16(data, i);
                                                if (!continuetillend)
                                                    Console.WriteLine(marker.ToString("X"));

                                                if ((marker & 0xFF00) != 0xFF00 && !continuetillend)
                                                {
                                                    MessageBox.Show("Prerendered image conversion error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    break;
                                                }

                                                if (marker == 0xFFD9)
                                                {
                                                    endofimage = i + 2;
                                                    break;
                                                }
                                                if (continuetillend || marker == 0xFFDA)
                                                {
                                                    continuetillend = true;

                                                }
                                                else
                                                {
                                                    i += Helpers.Read16(data, i + 2) + 2 - 1;
                                                }
                                            }
                                            if (endofimage != 0)
                                            {
                                                List<byte> imagedata = data.GetRange(imageoffset, endofimage - imageoffset);
                                                string filename = "Import/Background" + b + "_" + DateTime.Now.Ticks.ToString() + ".jpeg";
                                                File.Create(filename).Close();
                                                File.WriteAllBytes(filename, imagedata.ToArray());


                                                CurrentScene.prerenderimages.Add(filename);



                                            }
                                        }
                                    }


                                }


                            }




                        }
                    }

                    cnt++;
                }
            }



            SetSceneHeader(0);
            UpdateAlternateSceneHeaders();
            actorEditControl1.UpdateActorEdit();
            actorEditControl1.UpdateForm();
            actorEditControl2.UpdateActorEdit();
            actorEditControl2.UpdateForm();
            actorEditControl3.UpdateActorEdit();
            actorEditControl3.UpdateForm();
            UpdateForm();

            if (CurrentScene.prerenderimages.Count > 0)
            {
                ViewportFOV.Value = 45;
                previewscenecamera = true;
            }

            SimulateN64Gfx = true;

            SimulateN64CheckBox.Checked = true;

            CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
        }

        private void CameraCopyViewport_Click(object sender, EventArgs e)
        {
            double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
            double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

            Vector3d truepos = GetTrueCameraPosition();

            if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
            {
                truepos.Y -= (float)Math.Sin(RotXRad);
            }
            else
            {
                truepos.X += (float)Math.Sin(RotYRad);
                truepos.Z -= (float)Math.Cos(RotYRad);
                truepos.Y -= (float)Math.Sin(RotXRad);
            }

            truepos.X = Clamp(truepos.X, -32767, 32767);
            truepos.Y = Clamp(truepos.Y, -32767, 32767);
            truepos.Z = Clamp(truepos.Z, -32767, 32767);


            CurrentScene.Cameras[(int)CameraSelect.Value].XPos = (short)truepos.X;
            CurrentScene.Cameras[(int)CameraSelect.Value].YPos = (short)truepos.Y;
            CurrentScene.Cameras[(int)CameraSelect.Value].ZPos = (short)truepos.Z;

            CurrentScene.Cameras[(int)CameraSelect.Value].XRot = (short)(Camera.Rot.X * 182.04444444444444444444444444444f);
            CurrentScene.Cameras[(int)CameraSelect.Value].YRot = (short)(-(Camera.Rot.Y - 180) * 182.04444444444444444444444444444f);
            CurrentScene.Cameras[(int)CameraSelect.Value].ZRot = (short)(Camera.Rot.Z * 182.04444444444444444444444444444f);

            UpdateCameraEdit();
        }

        private void CameraView_Click(object sender, EventArgs e)
        {
            previewscenecamera = !previewscenecamera;

            if (previewscenecamera)
            {

                Camera.Pos = ConvertToCameraPosition((Vector3d)CurrentScene.Cameras[(int)CameraSelect.Value].Position);
                Camera.Rot.X = CurrentScene.Cameras[(int)CameraSelect.Value].XRot / 182.04444444444444444444444444444f;
                Camera.Rot.Y = -CurrentScene.Cameras[(int)CameraSelect.Value].YRot / 182.04444444444444444444444444444f + 180;
                Camera.Rot.Z = CurrentScene.Cameras[(int)CameraSelect.Value].ZRot / 182.04444444444444444444444444444f;

            }

            UpdateCameraEdit();
        }

        private void PrerenderedList_ValueChanged(object sender, EventArgs e)
        {
            UpdatePrerenders();
        }

        private void AddObjectToAllRoomsMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScene != null && CurrentScene.Rooms.Count > 0)
            {

                using (AddObjectAllRooms addobject = new AddObjectAllRooms())
                {
                    if (addobject.ShowDialog() == DialogResult.OK)
                    {
                        ushort objectid = addobject.objectid;
                        int position = addobject.position;
                        int cnt = 0;
                        foreach (ZScene.ZRoom room in CurrentScene.Rooms)
                        {
                            if (room.ZObjects.FindIndex(x => x.Value == objectid) == -1)
                            {
                                if (room.ZObjects.Count == 15)
                                {
                                    MessageBox.Show("Room " + cnt + " already has 15 objects!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                                else
                                {
                                    room.ZObjects.Add(new ZScene.ZUShort(objectid));
                                }

                                if (room.ZObjects.Count < position)
                                {
                                    room.ZObjects.RemoveAt(room.ZObjects.FindIndex(x => x.Value == objectid));
                                    room.ZObjects.Insert(position, new ZScene.ZUShort(objectid));
                                }
                            }

                            cnt++;
                        }


                    }
                    UpdateForm();
                }

            }
        }

        private void DeleteJFIF_Click(object sender, EventArgs e)
        {
            if (CurrentScene.prerenderimages.Count == 0) return;

            CurrentScene.prerenderimages.RemoveAt((int)PrerenderedList.Value - 1);
            UpdatePrerenders();
        }

        private void additionalLightsFixOoTDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ROM = "";
            bool z64rom = false;
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else if (rom64.isSet())
            {
                ROM = rom64.getRomCfg();
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "Rom / z64rom project (*.z64;*.rom,*.toml)|*.z64;*.rom;*.toml|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = saveFileDialog1.FileName;

                    if (ROM.Contains("z64project.toml"))
                        z64rom = true;
                    else if (ROM.Contains(".toml"))
                    {
                        MessageBox.Show("invalid config file, you need to import z64project.toml", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (ROM != "")
            {

                ApplyAdditionalLightsPatch(ROM, z64rom);

            }
        }

        public void ApplyAdditionalLightsPatch(string ROM, bool z64rom = false)
        {

            if (!z64rom && IsFileLocked(ROM))
                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string game = "";
                string prefix = "";
                string filepath = "";

                game = GetVersion(z64rom, ROM, out prefix);
                if (z64rom) filepath = Path.GetDirectoryName(ROM);

                if (game != "OOT" || prefix != "DBGMQ")
                {
                    MessageBox.Show("Only debug rom supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<Patch> Patches = new List<Patch>();

                Patches.Add(new Patch(0xAD4EFC, 99, ExtractResource("SharpOcarina.Files.func_80098B74").ToList(), "func_80098B74"));
                Patches.Add(new Patch(0xB19B5C, 99, ExtractResource("SharpOcarina.Files.Room_Draw").ToList(), "Room_Draw"));
                Patches.Add(new Patch(0xAF19C4, 99, ExtractResource("SharpOcarina.Files.room_uses_pointlights").ToList(), "room_uses_pointlights"));
                Patches.Add(new Patch(0xAD4A4C, 16, (ulong)0, "ensures our custom structs are zero-initialized"));
                Patches.Add(new Patch(0xAD4CA4, 16, (ulong)0));
                Patches.Add(new Patch(0xB0E640, 16, 0x08028A6F00000000, "redirects old Room_Draw to new Room_Draw"));
                Patches.Add(new Patch(0xB9E670, 8, 0x8005DD5C, "redirects old func_80098B74 to new func_80098B74"));
                Patches.Add(new Patch(0xB362DE, 4, 0x0080, "redirects old func_80098B74 to new func_80098B74"));


                if (!z64rom)
                {
                    List<byte> data = new List<byte>();

                    BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));

                    foreach (Patch patch in Patches)
                    {
                        if (patch.byteamount == 2) data.Add((byte)patch.data);
                        else if (patch.byteamount == 4) Helpers.Append16(ref data, (ushort)patch.data);
                        else if (patch.byteamount == 8) Helpers.Append32(ref data, patch.data);
                        else if (patch.byteamount > 8) data.AddRange(patch.bytedata);

                        BWS.Seek(patch.offset, SeekOrigin.Begin);
                        BWS.Write(data.ToArray());
                        data.Clear();

                        if (patch.name != "") Console.WriteLine(patch.name);
                    }
                    BWS.Close();

                    RecalculateCRC(File.Open(ROM, FileMode.Open, FileAccess.ReadWrite));
                    MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (SavePatches(filepath + @"\patch\SO_AdditionalLightsFix.cfg", Patches))
                        MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void roomaffectedbypointlights_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].AffectedByPointLight = Roomaffectedpointlightscheckbox.Checked;
            }
        }

        private void TimeHour_ValueChanged(object sender, EventArgs e)
        {
            if (!DisableStartTime.Checked && CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].StartTime = ((int)TimeHour.Value << 8) | (CurrentScene.Rooms[RoomList.SelectedIndex].StartTime & 0x00FF);
            }
        }

        private void TimeMinute_ValueChanged(object sender, EventArgs e)
        {
            if (!DisableStartTime.Checked && CurrentScene.Rooms.Count > 0)
            {
                CurrentScene.Rooms[RoomList.SelectedIndex].StartTime = ((int)TimeMinute.Value) | (CurrentScene.Rooms[RoomList.SelectedIndex].StartTime & 0xFF00);
            }
        }

        private void DisableStartTime_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentScene.Rooms.Count > 0)
            {
                if (DisableStartTime.Checked)
                {
                    CurrentScene.Rooms[RoomList.SelectedIndex].StartTime = 0xFFFF;
                    TimeHour.Enabled = false;
                    TimeHour.Value = 0;
                    TimeMinute.Enabled = false;
                    TimeMinute.Value = 0;
                }
                else
                {
                    CurrentScene.Rooms[RoomList.SelectedIndex].StartTime = 0;
                    TimeHour.Enabled = true;
                    TimeMinute.Enabled = true;
                }



            }
        }

        private void OpenSceneFromRoomToolStrip_Click(object sender, EventArgs e)
        {
            //TODO

            string ROM = "";
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else
            {
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Nintendo 64 ROMs (*.z64)|*.z64|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = openFileDialog1.FileName;
                }
                else return;
            }

            int sceneid = 0;

            List<byte> data = new List<byte>(File.ReadAllBytes(ROM));

            using (OpenRomScene openromscene = new OpenRomScene(data))
            {
                if (openromscene.ShowDialog() == DialogResult.OK)
                {
                    sceneid = openromscene.sceneid;

                    LoadZscene(data, sceneid);

                    //  Console.WriteLine("result " + i.ToString("X8"));
                }
            }
        }

        private void AutoFixErrorsStripMenuItem3_Click(object sender, EventArgs e)
        {
            settings.AutoFixErrors = AutoFixErrorsStripMenuItem3.Checked;
        }

        private void AutoInjectOffsetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InjectoffsetTextbox.Enabled = !AutoInjectOffsetCheckBox.Checked;
            if (AutoInjectOffsetCheckBox.Checked)
            {
                CurrentScene.InjectOffset = (int)(0x02000000 + (ScenenumberTextbox.Value * 0x4A000));
                InjectoffsetTextbox.Text = (CurrentScene.InjectOffset).ToString("X8");

            }
        }

        private void AdvancedTextureAnimationsMenuItem_Click(object sender, EventArgs e)
        {
            settings.command1AOoT = AdvancedTextureAnimationsMenuItem.Checked;
            UpdateForm();
        }

        private void advancedTextureAnimationsOoTDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ROM = "";
            bool z64rom = false;
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else if (rom64.isSet())
            {
                ROM = rom64.getRomCfg();
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "Rom / z64rom project (*.z64;*.rom,*.toml)|*.z64;*.rom;*.toml|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = saveFileDialog1.FileName;

                    if (ROM.Contains("z64project.toml"))
                        z64rom = true;
                    else if (ROM.Contains(".toml"))
                    {
                        MessageBox.Show("invalid config file, you need to import z64project.toml", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (ROM != "")
            {
                ApplyTextureAnimPatch(ROM, z64rom);


            }
        }

        public void ApplyTextureAnimPatch(string ROM, bool z64rom = false)
        {
            if (!z64rom && IsFileLocked(ROM))
                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string game = "";
                string prefix = "";
                string filepath = "";

                game = GetVersion(z64rom, ROM, out prefix);
                if (z64rom) filepath = Path.GetDirectoryName(ROM);

                if (game != "OOT" || prefix != "DBGMQ")
                {
                    MessageBox.Show("Only debug rom supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                List<Patch> Patches = new List<Patch>();


                //Patches.Add(new Patch(0xB15258, 99, ExtractResource("SharpOcarina.Files.Advanced_Texture_Animations").ToList(), "Advanced_Texture_Animations"));

                //List<byte> textureanimpatch = File.ReadAllBytes("Z:\\MEGA\\projects\\custom actors\\toolkit\\WindowsFormsApp4\\bin\\Debug\\Advanced_Texture_Animations").ToList();
                List<byte> textureanimpatch = ExtractResource("SharpOcarina.Files.Advanced_Texture_Animations").ToList();

                while (textureanimpatch.Count < 0x109B) textureanimpatch.Add(00);

                Patches.Add(new Patch(0xB15258, 99, textureanimpatch, "Advanced_Texture_Animations"));

                //Patches.Add(new Patch(0xBA1554, 8, 0x8009E0B8 + 0x518, "3C02800A"));//27BDFF68 518
                Patches.Add(new Patch(0xBA1554, 8, 0x8009e658, "entry point"));


                if (!z64rom)
                {
                    List<byte> data = new List<byte>();

                    BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));

                    foreach (Patch patch in Patches)
                    {
                        if (patch.byteamount == 2) data.Add((byte)patch.data);
                        else if (patch.byteamount == 4) Helpers.Append16(ref data, (ushort)patch.data);
                        else if (patch.byteamount == 8) Helpers.Append32(ref data, patch.data);
                        else if (patch.byteamount > 8) data.AddRange(patch.bytedata);

                        BWS.Seek(patch.offset, SeekOrigin.Begin);
                        BWS.Write(data.ToArray());
                        data.Clear();

                        if (patch.name != "") Console.WriteLine(patch.name);
                    }
                    BWS.Close();

                    RecalculateCRC(File.Open(ROM, FileMode.Open, FileAccess.ReadWrite));
                    MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (SavePatches(filepath + @"\patch\SO_AdvancedTextureAnimations.cfg", Patches))
                        MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
        }

        public void ApplyDynapolyPatch(string ROM, bool z64rom = false)
        {
            if (!z64rom && IsFileLocked(ROM))
                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string game = "";
                string prefix = "";
                string filepath = "";

                game = GetVersion(z64rom, ROM, out prefix);
                if (z64rom) filepath = Path.GetDirectoryName(ROM);

                if (game != "OOT" || prefix != "DBGMQ")
                {
                    MessageBox.Show("Only debug rom supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!z64rom)
                {
                    var romdata = new List<byte>(File.ReadAllBytes(ROM));

                    if (romdata[0x00B415B3] != 0x74)
                    {
                        MessageBox.Show("Apply the expanded ram patch first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                List<Patch> Patches = new List<Patch>();

                Patches.Add(new Patch(0xAB3218, 99, ExtractResource("SharpOcarina.Files.BgCheck_Allocate").ToList(), "BgCheck_Allocate"));

                if (!z64rom)
                {
                    List<byte> data = new List<byte>();

                    BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));

                    foreach (Patch patch in Patches)
                    {
                        if (patch.byteamount == 2) data.Add((byte)patch.data);
                        else if (patch.byteamount == 4) Helpers.Append16(ref data, (ushort)patch.data);
                        else if (patch.byteamount == 8) Helpers.Append32(ref data, patch.data);
                        else if (patch.byteamount > 8) data.AddRange(patch.bytedata);

                        BWS.Seek(patch.offset, SeekOrigin.Begin);
                        BWS.Write(data.ToArray());
                        data.Clear();

                        if (patch.name != "") Console.WriteLine(patch.name);
                    }
                    BWS.Close();

                    RecalculateCRC(File.Open(ROM, FileMode.Open, FileAccess.ReadWrite));
                    MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (SavePatches(filepath + @"\patch\SO_ExtendedDynapoly.cfg", Patches))
                        MessageBox.Show("Patch applied successfully!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
        }

        public void RefreshRecentRoms(string newpath = "")
        {
            XmlDocument doc = new XmlDocument();
            File.Delete(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFilestmp.xml"));

            System.IO.File.Copy(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFiles.xml"), Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFilestmp.xml"));
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFilestmp.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Rom");


            if (newpath != "")
            {
                XmlNode deletenode = doc.SelectSingleNode("//Rom[text()='" + newpath + "']");
                if (deletenode != null)
                {
                    deletenode.ParentNode.RemoveChild(deletenode);
                }

                XmlNode newnode = doc.CreateElement("Rom");
                newnode.InnerText = newpath;
                XmlNode Table = doc.SelectSingleNode("//Table");
                Table.AppendChild(newnode);
                doc.Save(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFiles.xml"));

            }
            if (nodes.Count > settings.MaxLastFile)
            {
                while (nodes.Count > settings.MaxLastFile)
                {
                    nodes[0].ParentNode.RemoveChild(nodes[0]);
                    nodes = doc.SelectNodes("Table/Rom");
                }
            }

            if (nodes != null)
            {
                injectToROMToolStripMenuItem.DropDownItems.Clear();
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    XmlNode node = nodes[i];

                    ToolStripMenuItem MenuItem = new System.Windows.Forms.ToolStripMenuItem() { Name = node.InnerText, Text = node.InnerText };
                    MenuItem.Click += new System.EventHandler(this.OpenRecentRom);

                    injectToROMToolStripMenuItem.DropDownItems.Add(MenuItem);
                };
            }

            fs.Close();
            File.Delete(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/RecentFilestmp.xml"));
            // openSceneToolStripMenuItem.Enabled = openSceneToolStripMenuItem.DropDownItems.Count > 0;
        }

        private void AddRenderFunction_Click(object sender, EventArgs e)
        {
            ZTextureAnim textureanim = new ZTextureAnim(Convert.ToUInt32((RenderFunctionType.SelectedItem as SongItem).Value));

            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.Add(textureanim);


            UpdateForm();

            if (RenderFunctionSelect.SelectedIndex < CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.Count - 1) RenderFunctionSelect.SelectedIndex = RenderFunctionSelect.Items.Count - 1;

            ZTextureAnim addedfunction = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex];

            if ((new uint[] { ZTextureAnim.texframe, ZTextureAnim.texswap, ZTextureAnim.blending, ZTextureAnim.scroll }).Contains(addedfunction.Type))
            {
                if (CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.FindIndex(x => x.Preview && x != textureanim && (x.Type == addedfunction.Type || (x.Type == ZTextureAnim.texswap && addedfunction.Type == ZTextureAnim.texframe) || (x.Type == ZTextureAnim.texframe && addedfunction.Type == ZTextureAnim.texswap))) != -1)
                {
                    addedfunction.Preview = false;
                }
            }
            else
            {
                addedfunction.Preview = false;
            }

            UpdateRenderFunctionEdit();

            if (SimulateN64Gfx)
                CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
        }

        private void DeleteRenderFunction_Click(object sender, EventArgs e)
        {
            if (RenderFunctionSelect.SelectedIndex != -1)
            {
                ZTextureAnim Del = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex];
                CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.Remove(Del);
                if (CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.Count == 0) RenderFunctionSelect.Items.Clear();
                RenderFunctionTabs.SelectedIndex = 0;
                UpdateRenderFunctionEdit();

                if (SimulateN64Gfx)
                    CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
            }
        }

        private void RenderFunctionID_ValueChanged(object sender, EventArgs e)
        {
            UpdateRenderFunctionEdit();
        }

        private void RenderFunctionType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CurrentScene != null && CurrentScene.SegmentFunctions.Count != 0 && CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.Count > 0)
            {
                ZTextureAnim textureanim = new ZTextureAnim(Convert.ToUInt32((RenderFunctionType.SelectedItem as SongItem).Value));
                CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex] = textureanim;


                ZTextureAnim addedfunction = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex];

                if ((new uint[] { ZTextureAnim.texframe, ZTextureAnim.texswap, ZTextureAnim.blending, ZTextureAnim.scroll }).Contains(addedfunction.Type))
                {
                    if (CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.FindIndex(x => x.Preview && x != textureanim && (x.Type == addedfunction.Type || (x.Type == ZTextureAnim.texswap && addedfunction.Type == ZTextureAnim.texframe) || (x.Type == ZTextureAnim.texframe && addedfunction.Type == ZTextureAnim.texswap))) != -1)
                    {
                        addedfunction.Preview = false;
                    }
                }
                else
                {
                    addedfunction.Preview = false;
                }


                UpdateRenderFunctionEdit();

                if (SimulateN64Gfx)
                    CurrentScene.ConvertPreview(settings.ConsecutiveRoomInject, settings.ForceRGBATextures);
            }
        }

        private void RenderFunctionUp_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex];
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.RemoveAt(RenderFunctionSelect.SelectedIndex);
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.Insert(RenderFunctionSelect.SelectedIndex - 1, item);
            RenderFunctionSelect.SelectedIndex--;
            UpdateRenderFunctionEdit();


        }

        private void RenderFunctionDown_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex];
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.RemoveAt(RenderFunctionSelect.SelectedIndex);
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions.Insert(RenderFunctionSelect.SelectedIndex + 1, item);
            RenderFunctionSelect.SelectedIndex++;
            UpdateRenderFunctionEdit();
        }

        private void RenderFunctionSelect_MouseClick(object sender, MouseEventArgs e)
        {
            FunctionTextureSwapAnimationList.SelectedIndex = -1;
            FunctionColorBlendList.SelectedIndex = -1;
            UpdateRenderFunctionEdit();


        }

        private void RenderFunctionSelect_KeyDown(object sender, KeyEventArgs e)
        {
            FunctionTextureSwapAnimationList.SelectedIndex = -1;
            FunctionColorBlendList.SelectedIndex = -1;
            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureScrollXVelocity_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].XVelocity1 = (sbyte)FunctionTextureScrollXVelocity.Value;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureScrollYVelocity_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].YVelocity1 = (sbyte)FunctionTextureScrollYVelocity.Value;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureScrollWidth_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].Width1 = (byte)FunctionTextureScrollWidth.Value;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureScrollHeight_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].Height1 = (byte)FunctionTextureScrollHeight.Value;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureScrollXVelocity2_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].XVelocity2 = (sbyte)FunctionTextureScrollXVelocity2.Value;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureScrollYVelocity2_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].YVelocity2 = (sbyte)FunctionTextureScrollYVelocity2.Value;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureScrollWidth2_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].Width2 = (byte)FunctionTextureScrollWidth2.Value;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureScrollHeight2_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].Height2 = (byte)FunctionTextureScrollHeight2.Value;

            UpdateRenderFunctionEdit();
        }

        private void RenderFunctionFlagType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].FlagType = (Convert.ToByte((RenderFunctionFlagType.SelectedItem as FlagItem).Value));

            UpdateRenderFunctionEdit();
        }

        private void RenderFunctionFlagID_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].FlagValue = (uint)RenderFunctionFlagID.Value;

            UpdateRenderFunctionEdit();
        }

        private void RenderFunctionFlagReverseCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].FlagReverse = RenderFunctionFlagReverseCheckbox.Checked;

            UpdateRenderFunctionEdit();
        }

        private void RenderFunctionFlagBitwise_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].FlagBitwise = (uint)RenderFunctionFlagBitwise.IntValue;

                UpdateRenderFunctionEdit();
            }


        }

        private void RenderFunctionFlagBitwise_Leave(object sender, EventArgs e)
        {

            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].FlagBitwise = (uint)RenderFunctionFlagBitwise.IntValue;

            UpdateRenderFunctionEdit();
        }

        public void SetFlagPreset(object sender, System.EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].FlagValue = Convert.ToUInt32(((FlagPreset)sender).Value);
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].FlagBitwise = Convert.ToUInt32(((FlagPreset)sender).Bitwise);
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].FlagType = Convert.ToByte(((FlagPreset)sender).Type);
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].FlagReverse = (((FlagPreset)sender).Reverse);
            RenderFunctionFlagType.SelectedIndex = CurrentScene.SegmentFunctions[(int)RenderFunctionID.Value - 8].Functions[RenderFunctionSelect.SelectedIndex].FlagType;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureSwapTextureID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwap = (FunctionTextureSwapTextureID.SelectedItem as ObjFile.Material).DisplayName;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureSwapTextureID2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwap2 = (FunctionTextureSwapTextureID2.SelectedItem as ObjFile.Material).DisplayName;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureSwapAnimationAdd_Click(object sender, EventArgs e)
        {
            ZTextureAnimImage item = new ZTextureAnimImage();
            if (CurrentScene.AdditionalTextures.Count > 0)
                item.Texture = CurrentScene.AdditionalTextures[0].DisplayName;
            else
                item.Texture = "none";
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList.Add(item);

            UpdateRenderFunctionEdit();
            if (FunctionTextureSwapAnimationList.Items.Count > 1)
            {
                FunctionTextureSwapAnimationList.SelectedIndex++;
                UpdateRenderFunctionEdit();
            }

        }

        private void FunctionTextureSwapAnimationDelete_Click(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList.RemoveAt(FunctionTextureSwapAnimationList.SelectedIndex);
            if (CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList.Count == 0) FunctionTextureSwapAnimationList.Items.Clear();
            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureSwapAnimationUp_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList[FunctionTextureSwapAnimationList.SelectedIndex];
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList.RemoveAt(FunctionTextureSwapAnimationList.SelectedIndex);
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList.Insert(FunctionTextureSwapAnimationList.SelectedIndex - 1, item);
            FunctionTextureSwapAnimationList.SelectedIndex--;
            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureSwapAnimationDown_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList[FunctionTextureSwapAnimationList.SelectedIndex];
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList.RemoveAt(FunctionTextureSwapAnimationList.SelectedIndex);
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList.Insert(FunctionTextureSwapAnimationList.SelectedIndex + 1, item);
            FunctionTextureSwapAnimationList.SelectedIndex++;
            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureSwapAnimationImage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList[FunctionTextureSwapAnimationList.SelectedIndex].Texture = (FunctionTextureSwapAnimationImage.SelectedItem as ObjFile.Material).DisplayName;

            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureSwapAnimationDuration_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].TextureSwapList[FunctionTextureSwapAnimationList.SelectedIndex].Duration = (ushort)FunctionTextureSwapAnimationDuration.Value;
            UpdateRenderFunctionEdit();
        }

        private void FunctionTextureSwapAnimationList_Click(object sender, EventArgs e)
        {
            UpdateRenderFunctionEdit();
        }

        private void ExtendDynapolyCountStripMenuItem_Click(object sender, EventArgs e)
        {
            string ROM = "";
            bool z64rom = false;
            if (GlobalROM != "")
            {
                ROM = GlobalROM;
            }
            else if (rom64.isSet())
            {
                ROM = rom64.getRomCfg();
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "Rom / z64rom project (*.z64;*.rom,*.toml)|*.z64;*.rom;*.toml|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ROM = saveFileDialog1.FileName;

                    if (ROM.Contains("z64project.toml"))
                        z64rom = true;
                    else if (ROM.Contains(".toml"))
                    {
                        MessageBox.Show("invalid config file, you need to import z64project.toml", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (ROM != "")
            {
                ApplyDynapolyPatch(ROM, z64rom);


            }
        }

        private void FunctionColorBlendAdd_Click(object sender, EventArgs e)
        {
            ZTextureAnimColor item = new ZTextureAnimColor();

            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList.Add(item);

            UpdateRenderFunctionEdit();
            if (FunctionColorBlendList.Items.Count > 1)
            {
                FunctionColorBlendList.SelectedIndex++;
                UpdateRenderFunctionEdit();
            }
        }

        private void FunctionColorBlendDelete_Click(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList.RemoveAt(FunctionColorBlendList.SelectedIndex);
            if (CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList.Count == 0) FunctionColorBlendList.Items.Clear();
            UpdateRenderFunctionEdit();
        }

        private void FunctionColorBlendUp_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList[FunctionColorBlendList.SelectedIndex];
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList.RemoveAt(FunctionColorBlendList.SelectedIndex);
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList.Insert(FunctionColorBlendList.SelectedIndex - 1, item);
            FunctionColorBlendList.SelectedIndex--;
            UpdateRenderFunctionEdit();
        }

        private void FunctionColorBlendDown_Click(object sender, EventArgs e)
        {
            var item = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList[FunctionColorBlendList.SelectedIndex];
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList.RemoveAt(FunctionColorBlendList.SelectedIndex);
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList.Insert(FunctionColorBlendList.SelectedIndex + 1, item);
            FunctionColorBlendList.SelectedIndex++;
            UpdateRenderFunctionEdit();
        }

        private void FunctionColorBlendList_Click(object sender, EventArgs e)
        {
            UpdateRenderFunctionEdit();
        }

        private void FunctionColorBlendFrames_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList[FunctionColorBlendList.SelectedIndex].Duration = (ushort)FunctionColorBlendFrames.Value;
            UpdateRenderFunctionEdit();
        }

        private void FunctionColorBlendColor_DoubleClick(object sender, EventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            if (ColorPicker(ref PB) == true)
            {
                CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList[FunctionColorBlendList.SelectedIndex].C1C = FunctionColorBlendColor.BackColor;
                UpdateRenderFunctionEdit();
            }
        }

        private void FunctionCameraEffectDropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].CameraEffect = Convert.ToByte((FunctionCameraEffectDropdown.SelectedItem as SongItem).Value);
            UpdateRenderFunctionEdit();
        }

        private void RenderFunctionFlagFreezeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].Freeze = RenderFunctionFlagFreezeCheckBox.Checked;

            UpdateRenderFunctionEdit();
        }

        private void RenderFunctionPreview_Click(object sender, EventArgs e)
        {
            ZTextureAnim targetfunction = CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex];

            if (!targetfunction.Preview)
            {
                foreach (ZTextureAnim anim in CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions)
                {
                    if (anim.Type == targetfunction.Type || (anim.Type == ZTextureAnim.texswap && targetfunction.Type == ZTextureAnim.texframe) || (anim.Type == ZTextureAnim.texframe && targetfunction.Type == ZTextureAnim.texswap)) anim.Preview = false;
                }
            }

            targetfunction.Preview = !targetfunction.Preview;

            UpdateRenderFunctionEdit();
        }

        private void ReloadXMLMenuItem_Click(object sender, EventArgs e)
        {
            ReloadXMLs();

            UpdateForm();

            actorEditControl1.UpdateForm();
            actorEditControl2.UpdateForm();
            actorEditControl3.UpdateForm();
        }

        private void CameraUnk12_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk12.Value) > 20)
                CameraUnk12.Value += (CameraUnk12.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk12) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk12 = (ushort)CameraUnk12.Value;

            UpdateCameraEdit();
        }

        private void CameraUnk14_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk14.Value) > 20)
                CameraUnk14.Value += (CameraUnk14.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk14) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk14 = (ushort)CameraUnk14.Value;

            UpdateCameraEdit();
        }

        private void CameraUnk16_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk16.Value) > 20)
                CameraUnk16.Value += (CameraUnk16.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk16) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk16 = (ushort)CameraUnk16.Value;

            UpdateCameraEdit();
        }

        private void CameraUnk18_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk18.Value) > 20)
                CameraUnk18.Value += (CameraUnk18.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk18) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk18 = (ushort)CameraUnk18.Value;

            UpdateCameraEdit();
        }

        private void CameraUnk1A_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk1A.Value) > 20)
                CameraUnk1A.Value += (CameraUnk1A.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk1A) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk1A = (ushort)CameraUnk1A.Value;

            UpdateCameraEdit();
        }

        private void CameraUnk1C_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk1C.Value) > 20)
                CameraUnk1C.Value += (CameraUnk1C.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk1C) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk1C = (ushort)CameraUnk1C.Value;

            UpdateCameraEdit();
        }

        private void CameraUnk1E_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk1E.Value) > 20)
                CameraUnk1E.Value += (CameraUnk1E.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk1E) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk1E = (ushort)CameraUnk1E.Value;

            UpdateCameraEdit();
        }

        private void CameraUnk20_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk20.Value) > 20)
                CameraUnk20.Value += (CameraUnk20.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk20) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk20 = (ushort)CameraUnk20.Value;

            UpdateCameraEdit();
        }

        private void CameraUnk22_ValueChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && ushort.MaxValue - Math.Abs(CameraUnk22.Value) > 20)
                CameraUnk22.Value += (CameraUnk22.Value - (decimal)CurrentScene.Cameras[(int)CameraSelect.Value].Unk22) * 19;

            CurrentScene.Cameras[(int)CameraSelect.Value].Unk22 = (ushort)CameraUnk22.Value;

            UpdateCameraEdit();
        }

        private void CameraPage1_Click(object sender, EventArgs e)
        {
            CameraPanel.Visible = true;
            CameraPanel2.Visible = false;
        }

        private void CameraPage2_Click(object sender, EventArgs e)
        {
            CameraPanel2.Visible = true;
            CameraPanel.Visible = false;
        }

        private void SceneHeaderCopyList_ValueChanged(object sender, EventArgs e)
        {
            NormalHeader.SceneHeaders[(int)SceneHeaderList.Value - 1].CloneFromHeader = (int)SceneHeaderCopyList.Value;

            UpdateAlternateSceneHeaders();
        }

        private void RenderFunctionInherit_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScene.inherittextureanims = RenderFunctionInherit.Checked;

            UpdateForm();
        }

        private void GroupCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedItem != null)
            {
                ((ObjFile.Group)GroupList.SelectedItem).Custom = GroupCustom.Checked;

                int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == ((ObjFile.Group)GroupList.SelectedItem).Name);
                CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.Custom[Index] = ((ObjFile.Group)GroupList.SelectedItem).Custom;

                UpdateGroupSelect();
            }
        }

        private void GroupCustomizeButton_Click(object sender, EventArgs e)
        {
            if (CurrentScene != null && CurrentScene.Rooms.Count > 0)
            {
                if (customcombiner != null) customcombiner.Close();
                customcombiner = new CustomCombiner(this, ((ObjFile.Group)GroupList.SelectedItem), CurrentScene.OutdoorLight, CurrentScene.Rooms[RoomList.SelectedIndex].AffectedByPointLight);
                customcombiner.Show();
                //customcombiner_visible = true;


            }
        }

        public void SetCustomCombinerData(ObjFile.Group group)
        {
            int Index = CurrentScene.Rooms[RoomList.SelectedIndex].TrueGroups.FindIndex(x => x.Name == (group.Name));
            CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.CustomDL[Index, 0] = group.CustomDL[0];
            CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.CustomDL[Index, 1] = group.CustomDL[1];
            CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.CustomDL[Index, 2] = group.CustomDL[2];
            CurrentScene.Rooms[RoomList.SelectedIndex].GroupSettings.CustomDL[Index, 3] = group.CustomDL[3];

            UpdateGroupSelect(n64refresh);

        }

        private void FunctionColorBlendAlpha_ValueChanged(object sender, EventArgs e)
        {
            CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList[FunctionColorBlendList.SelectedIndex].Color = (CurrentScene.SegmentFunctions[(int)(RenderFunctionID.Value - 8)].Functions[RenderFunctionSelect.SelectedIndex].ColorList[FunctionColorBlendList.SelectedIndex].Color & 0x00FFFFFF) | ((ushort)FunctionColorBlendAlpha.Value << 24);
            UpdateRenderFunctionEdit();
        }

        private void RenderWaterboxesMenuItem_Click(object sender, EventArgs e)
        {
            settings.OnlyRenderWaterboxesGeneral = RenderWaterboxesMenuItem.Checked;
        }

        private void ColorBlindMenuItem_Click(object sender, EventArgs e)
        {
            settings.colorblindaxis = ColorBlindMenuItem.Checked;

            // Axis marker
            GL.NewList(AxisMarkerGLID, ListMode.Compile);

            GL.LineWidth(3);

            GL.Begin(BeginMode.Lines);
            GL.Color3(Color.Blue);
            GL.Vertex3(20.0f, 0.1f, 0.1f);
            GL.Vertex3(-20.0f, 0.1f, 0.1f);
            GL.End();
            GL.Begin(BeginMode.Lines);
            if (settings.colorblindaxis)
                GL.Color3(Color.DarkGoldenrod);
            else
                GL.Color3(Color.Red);
            GL.Vertex3(0.1f, 20.0f, 0.1f);
            GL.Vertex3(0.1f, -20.0f, 0.1f);
            GL.End();

            // GL.LineWidth(2);
            GL.Begin(BeginMode.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0.1f, 0.1f, 20.0f);
            GL.Vertex3(0.1f, 0.1f, 0.0f);
            GL.Vertex3(0.1f, 0.1f, 0.0f);
            if (settings.colorblindaxis)
                GL.Color3(Color.Cyan);
            else
                GL.Color3(Color.Lime);
            GL.Vertex3(0.1f, 0.1f, -20.0f);
            // GL.Vertex3(0.1f, 0.1f, 20.0f);
            GL.End();

            GL.LineWidth(1);

            GL.EndList();
        }

        private void DisableTextureWarningsMenuItem_Click(object sender, EventArgs e)
        {
            settings.DisableTextureWarnings = DisableTextureWarningsMenuItem.Checked;
        }

        private void EnableNexExitFormatMenuItem_Click(object sender, EventArgs e)
        {
            settings.EnableNewExitFormat = EnableNexExitFormatMenuItem.Checked;

            ExitList.SelectedIndex = -1;
            UpdateExits();
        }

        private void ExitSceneIndex_ValueChanged(object sender, EventArgs e)
        {
            if (settings.EnableNewExitFormat)
            {
                CurrentScene.ExitListV2[ExitList.SelectedIndex].SceneIndex = (uint)ExitSceneIndex.Value;
                UpdateExits();
            }
        }

        private void ExitSpawnIndex_ValueChanged(object sender, EventArgs e)
        {
            if (settings.EnableNewExitFormat)
            {
                CurrentScene.ExitListV2[ExitList.SelectedIndex].SpawnIndex = (uint)ExitSpawnIndex.Value;
                UpdateExits();
            }
        }

        private void ExitFadeIn_ValueChanged(object sender, EventArgs e)
        {
            if (settings.EnableNewExitFormat)
            {
                CurrentScene.ExitListV2[ExitList.SelectedIndex].FadeIn = (uint)ExitFadeIn.Value;
                UpdateExits();
            }
        }

        private void ExitFadeOut_ValueChanged(object sender, EventArgs e)
        {
            if (settings.EnableNewExitFormat)
            {
                CurrentScene.ExitListV2[ExitList.SelectedIndex].FadeOut = (uint)ExitFadeOut.Value;
                UpdateExits();
            }
        }

        private void ExitHeaderIndex_ValueChanged(object sender, EventArgs e)
        {
            if (settings.EnableNewExitFormat)
            {
                CurrentScene.ExitListV2[ExitList.SelectedIndex].HeaderIndex = (uint)ExitHeaderIndex.Value;
                UpdateExits();
            }
        }

        private void ExitMusicOn_CheckedChanged(object sender, EventArgs e)
        {
            if (settings.EnableNewExitFormat && CurrentScene.ExitListV2.Count > 0)
            {
                CurrentScene.ExitListV2[ExitList.SelectedIndex].MusicOn = ExitMusicOn.Checked;
                UpdateExits();
            }
        }

        private void ExitShowTitlecard_CheckedChanged(object sender, EventArgs e)
        {
            if (settings.EnableNewExitFormat && CurrentScene.ExitListV2.Count > 0)
            {
                CurrentScene.ExitListV2[ExitList.SelectedIndex].ShowTitleCard = ExitShowTitlecard.Checked;
                UpdateExits();
            }
        }

        private void ExitList_Click(object sender, EventArgs e)
        {
            RefreshExitLabels();
            UpdateExits();
        }

        private void EnvironmentDirectionAY_ValueChanged(object sender, EventArgs e)
        {
            UpdateEnvironmentData();
        }

        private void EnvironmentDirectionAZ_ValueChanged(object sender, EventArgs e)
        {
            UpdateEnvironmentData();
        }

        private void EnvironmentDirectionBY_ValueChanged(object sender, EventArgs e)
        {
            UpdateEnvironmentData();
        }

        private void EnvironmentDirectionBZ_ValueChanged(object sender, EventArgs e)
        {
            UpdateEnvironmentData();
        }

        private void SetRestrictionFlags_Click(object sender, EventArgs e)
        {
            if (CurrentScene != null)
            {


                using (RestrictionFlagPicker restrictionFlagPicker = new RestrictionFlagPicker(CurrentScene.RestrictionFlags))
                {
                    if (restrictionFlagPicker.ShowDialog() == DialogResult.OK)
                    {
                        CurrentScene.RestrictionFlags = restrictionFlagPicker.raw;
                    }
                }


            }
        }

        private void SetTitlecard_Click(object sender, EventArgs e)
        {

            if (CurrentScene != null)
            {


                using (TitleCardReplacer titlecard = new TitleCardReplacer(CurrentScene.TitleCard))
                {
                    if (titlecard.ShowDialog() == DialogResult.OK)
                    {
                        CurrentScene.TitleCard = titlecard.titlecard;
                    }
                }


            }
        }

        private void RoomSelector_ValueChanged(object sender, EventArgs e)
        {
            if (RoomSelector.Value <= CurrentScene.Rooms.Count - 1 && RoomList.SelectedIndex != (int)RoomSelector.Value)
            {
                RoomList.SelectedIndex = (int)RoomSelector.Value;
                SelectRoom(RoomList.SelectedIndex);
            }
        }

        private void actorEditControl1_Load(object sender, EventArgs e)
        {

        }

        private double DegToRad(double val)
        {
            return (val / 180.0) * Math.PI;
        }

        private Vector3d RotToNormal(double X, double Y)
        {
            double[,] mtx = new double[4, 4];
            double x = DegToRad(X);
            double y = DegToRad(-Y);
            double sin = 0.0;
            double cos = 1.0;

            if (y != 0.0)
            {
                sin = Math.Sin(y);
                cos = Math.Cos(y);
            }

            mtx[0, 0] = cos;
            mtx[0, 1] = 0.0;
            mtx[0, 2] = -sin;
            mtx[0, 3] = 0.0;

            mtx[1, 0] = 0.0;
            mtx[1, 1] = 1.0;
            mtx[1, 2] = 0.0;
            mtx[1, 3] = 0.0;

            mtx[2, 0] = sin;
            mtx[2, 1] = 0.0;
            mtx[2, 2] = cos;
            mtx[2, 3] = 0.0;

            mtx[3, 0] = 0.0;
            mtx[3, 1] = 0.0;
            mtx[3, 2] = 0.0;
            mtx[3, 3] = 1.0;

            sin = Math.Sin(x);
            cos = Math.Cos(x);

            double tcy;
            double tcz;

            for (int i = 0; i < 4; i++)
            {
                tcy = mtx[1, i];
                tcz = mtx[2, i];
                mtx[1, i] = tcy * cos - tcz * sin;
                mtx[2, i] = tcy * sin + tcz * cos;
            }

            Vector3d vec = new Vector3d(
                mtx[3, 0] + mtx[2, 0],
                mtx[3, 1] + mtx[2, 1],
                mtx[3, 2] + mtx[2, 2]
            );

            vec = Vector3d.Normalize(vec);

            return vec;
        }

        private decimal ViewNormalCopy_NormalToU8(double angle)
        {
            int ang;

            int positive_modulo(int i, int n)
            {
                return (i % n + n) % n;
            }

            if (angle > 0)
                ang = (int)(angle * 0x7F);
            else
                ang = (int)(angle * 0x7F);

            ang = positive_modulo(ang, 255);

            return ang;
        }

        private void ViewNormalCopyEnvA_Click(object sender, EventArgs e)
        {
            Vector3d n = RotToNormal(Camera.Rot.X, Camera.Rot.Y);

            EnvironmentDirectionAX.Value = ViewNormalCopy_NormalToU8(n.X);
            EnvironmentDirectionAY.Value = ViewNormalCopy_NormalToU8(n.Y);
            EnvironmentDirectionAZ.Value = ViewNormalCopy_NormalToU8(n.Z);
            UpdateEnvironmentData();
        }

        private void ViewNormalCopyEnvB_Click(object sender, EventArgs e)
        {
            Vector3d n = RotToNormal(Camera.Rot.X, Camera.Rot.Y);

            EnvironmentDirectionBX.Value = ViewNormalCopy_NormalToU8(n.X);
            EnvironmentDirectionBY.Value = ViewNormalCopy_NormalToU8(n.Y);
            EnvironmentDirectionBZ.Value = ViewNormalCopy_NormalToU8(n.Z);
            UpdateEnvironmentData();
        }

        private void SoundSpec_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentScene.Reverb = (byte)SoundSpec.SelectedIndex;
        }

        private static DateTime lastTime;
        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!AutoReload.Checked || !ReloadRoomButton.Enabled) return;

            foreach (ZScene.ZRoom Room in CurrentScene.Rooms)
            {
                DateTime now = File.GetLastWriteTime(Room.ModelFilename);

                if (now > lastTime)
                {
                    lastTime = now;
                    Console.WriteLine("Auto Reload Rooms");
                    ReloadRoomButton_Click(sender, e);
                    break;
                }
            }
        }

        private void AutoReload_Click(object sender, EventArgs e)
        {
            settings.AutoReload = AutoReload.Checked;
        }

        public void OpenRecentRom(object sender, System.EventArgs e)
        {
            InjectToRom(((ToolStripMenuItem)sender).Text);
        }

        public void EasterEggPhaseOne()
        {
            EasterEggToolStripMenuItem.Enabled = true;
            EasterEggToolStripMenuItem.Text = "Wtf is this?";
        }

        public static ROM CheckVersion(List<byte> ROM)
        {
            ROM result = new ROM();
            bool found = false;

            var bytes = new List<Byte>();

            for (int i = 0; i < 0x50000; i++)
            {
                bytes.Add(ROM[i]);
                bytes.Add(ROM[i + 1]);
                bytes.Add(ROM[i + 2]);
                bytes.Add(ROM[i + 3]);
                bytes.Add(ROM[i + 4]);
                bytes.Add(ROM[i + 5]);
                if (!bytes.Contains(0x00) && System.Text.Encoding.ASCII.GetString(bytes.ToArray()) == "zelda@")
                {
                    bytes.Clear();
                    for (int ii = 0; ii < 50; ii++)
                    {
                        for (int i3 = 0; i3 < 17; i3++)
                        {
                            bytes.Add(ROM[i + ii + i3]);
                        }

                        if (!bytes.Contains(0x00) && Regex.Replace(System.Text.Encoding.ASCII.GetString(bytes.ToArray()), @"\d", "X") == "XX-XX-XX XX:XX:XX")
                        {
                            string builddate = System.Text.Encoding.ASCII.GetString(bytes.ToArray());
                            // Console.WriteLine(builddate);
                            XmlDocument doc = new XmlDocument();
                            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/Roms.xml");
                            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                            doc.Load(fs);
                            XmlNodeList nodes = doc.SelectNodes("Table/ROM");
                            String[] output = { "0", "" };
                            if (nodes != null)
                                foreach (XmlNode node in nodes)
                                {
                                    XmlAttributeCollection nodeAtt = node.Attributes;
                                    if (nodeAtt["Build"] != null && nodeAtt["Build"].Value == builddate)
                                    {
                                        result.Game = nodeAtt["Game"].Value;
                                        result.Prefix = nodeAtt["Prefix"].Value;
                                        result.SceneTable = Convert.ToUInt32(nodeAtt["SceneTable"].Value, 16);
                                        result.SceneTableEnd = Convert.ToUInt32(nodeAtt["SceneTableEnd"].Value, 16);
                                        result.EntranceTableStart = Convert.ToUInt32(nodeAtt["EntranceTableStart"].Value, 16);
                                        result.EntranteTableEnd = Convert.ToUInt32(nodeAtt["EntranteTableEnd"].Value, 16);
                                        result.CutsceneTableStart = Convert.ToUInt32(nodeAtt["CutsceneTableStart"].Value, 16);
                                        result.CutsceneTableEnd = Convert.ToUInt32(nodeAtt["CutsceneTableEnd"].Value, 16);
                                        result.DmaTableStart = Convert.ToUInt32(nodeAtt["DmaTableStart"].Value, 16);
                                        result.DmaTableEnd = Convert.ToUInt32(nodeAtt["DmaTableEnd"].Value, 16);
                                        result.SceneDmaTableStart = Convert.ToUInt32(nodeAtt["SceneDmaTableStart"].Value, 16);
                                        result.SceneDmaTableEnd = Convert.ToUInt32(nodeAtt["SceneDmaTableEnd"].Value, 16);
                                        result.RestrictionFlagStart = Convert.ToUInt32(nodeAtt["RestrictionFlagStart"].Value, 16);
                                        result.RestrictionFlagEnd = Convert.ToUInt32(nodeAtt["RestrictionFlagEnd"].Value, 16);
                                        result.DefaultSaveFile = Convert.ToUInt32(nodeAtt["DefaultSaveFile"].Value, 16);
                                        result.HeaderTitle = Convert.ToUInt32(nodeAtt["HeaderTitle"].Value, 16);
                                        result.EntranceTitle = Convert.ToUInt32(nodeAtt["EntranceTitle"].Value, 16);
                                        result.HeaderNewFile = Convert.ToUInt32(nodeAtt["HeaderNewFile"].Value, 16);
                                        result.EntranceNewFile = Convert.ToUInt32(nodeAtt["EntranceNewFile"].Value, 16);
                                        result.AgeNewFile = Convert.ToUInt32(nodeAtt["AgeNewFile"].Value, 16);
                                        result.AgeTitle = Convert.ToUInt32(nodeAtt["AgeTitle"].Value, 16);
                                        result.RespawnChild = Convert.ToUInt32(nodeAtt["RespawnChild"].Value, 16);
                                        result.RespawnAdult = Convert.ToUInt32(nodeAtt["RespawnAdult"].Value, 16);
                                        result.FirstScene = Convert.ToUInt32(nodeAtt["FirstScene"].Value, 16);
                                        result.SubscreenMapInfo = Convert.ToUInt32(nodeAtt["SubscreenMapInfo"].Value, 16);
                                        result.SubscreenMapInfo2 = Convert.ToUInt32(nodeAtt["SubscreenMapInfo2"].Value, 16);
                                        result.SubscreenMapCompassIcons = Convert.ToUInt32(nodeAtt["SubscreenMapCompassIcons"].Value, 16);
                                        result.SubscreenMapFloorTextures = Convert.ToUInt32(nodeAtt["SubscreenMapFloorTextures"].Value, 16);
                                        result.SubscreenMapTitleCards = Convert.ToUInt32(nodeAtt["SubscreenMapTitleCards"].Value, 16);
                                        result.SubscreenMapFloorAmount = Convert.ToUInt32(nodeAtt["SubscreenMapFloorAmount"].Value, 16);
                                        result.SubscreenMapChestVertexData = Convert.ToUInt32(nodeAtt["SubscreenMapChestVertexData"].Value, 16);
                                        result.SubscreenPatch = nodeAtt["SubscreenPatch"].Value;
                                        result.SpecialTextureTable = Convert.ToUInt32(nodeAtt["SpecialTextureTable"].Value, 16);
                                        result.ActorTable = Convert.ToUInt32(nodeAtt["ActorTable"].Value, 16);
                                        result.ActorTableEnd = Convert.ToUInt32(nodeAtt["ActorTableEnd"].Value, 16);
                                        result.ObjectTable = Convert.ToUInt32(nodeAtt["ObjectTable"].Value, 16);
                                        result.ParticleTable = Convert.ToUInt32(nodeAtt["ParticleTable"].Value, 16);
                                        result.ParticleTableEnd = Convert.ToUInt32(nodeAtt["ParticleTableEnd"].Value, 16);


                                        found = true;


                                        break;
                                    }
                                }
                            if (!found) break;

                        }
                        bytes.Clear();
                    }
                    if (!found)
                    {
                        MessageBox.Show("Only OoT MQ debug, OoT 1.0 U, MM J 1.0 and MM U 1.0 roms are supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return result;
                    }
                }
                bytes.Clear();
            }


            return result;
        }

        public void RecalculateCRC(Stream sw)
        {
            uint[] crc = new uint[2];
            byte[] data = new byte[0x00101000];

            uint d, r, t1, t2, t3, t4, t5, t6 = 0xDF26F436;

            t1 = t2 = t3 = t4 = t5 = t6;

            sw.Position = 0;
            sw.Read(data, 0, 0x00101000);

            for (int i = 0x00001000; i < 0x00101000; i += 4)
            {
                d = (uint)((data[i] << 24) | (data[i + 1] << 16) | (data[i + 2] << 8) | data[i + 3]);
                if ((t6 + d) < t6) t4++;
                t6 += d;
                t3 ^= d;
                r = (d << (int)(d & 0x1F)) | (d >> (32 - (int)(d & 0x1F)));
                t5 += r;
                if (t2 > d) t2 ^= r;
                else t2 ^= t6 ^ d;
                t1 += (uint)((data[0x00000750 + (i & 0xFF)] << 24) | (data[0x00000751 + (i & 0xFF)] << 16) |
                      (data[0x00000752 + (i & 0xFF)] << 8) | data[0x00000753 + (i & 0xFF)]) ^ d;
            }
            crc[0] = t6 ^ t4 ^ t3;
            crc[1] = t5 ^ t2 ^ t1;

            if (BitConverter.IsLittleEndian)
            {
                crc[0] = (crc[0] >> 24) | ((crc[0] >> 8) & 0xFF00) | ((crc[0] << 8) & 0xFF0000) | ((crc[0] << 24) & 0xFF000000);
                crc[1] = (crc[1] >> 24) | ((crc[1] >> 8) & 0xFF00) | ((crc[1] << 8) & 0xFF0000) | ((crc[1] << 24) & 0xFF000000);
            }

            //Seek to 0x10 from rom start
            sw.Position = 0x10;
            BinaryWriter br = new BinaryWriter(sw);
            br.Write(crc[0]);
            br.Write(crc[1]);

            br.Close();
            sw.Close();

            Console.WriteLine("CRC \n" + crc[0].ToString("X") + "\n" + crc[1].ToString("X"));
        }

        public static byte[] ExtractResource(String filename)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            using (Stream resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream == null) return null;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }



        public void ChangeTheme(ColorScheme scheme, Control.ControlCollection container)
        {
            foreach (Control component in container)
            {
                if (component is Panel)
                {
                    ChangeTheme(scheme, component.Controls);
                    component.BackColor = scheme.PanelBG;
                    component.ForeColor = scheme.PanelFG;
                }
                else if (component is Button)
                {
                    component.BackColor = scheme.ButtonBG;
                    component.ForeColor = scheme.ButtonFG;

                }
                else if (component is TextBox)
                {
                    component.BackColor = scheme.TextBoxBG;
                    component.ForeColor = scheme.TextBoxFG;
                }
                else if (component is ComboBox)
                {
                    component.BackColor = scheme.ComboBoxBG;
                    component.ForeColor = scheme.ComboBoxFG;
                }
                else if (component is NumericUpDown || component is NumericUpDownEx)
                {
                    component.BackColor = scheme.NumericUpDownBG;
                    component.ForeColor = scheme.NumericUpDownFG;
                }
                else if (component is Label)
                {
                    component.ForeColor = scheme.LabelFG;
                }
                else if (component is CheckBox)
                {
                    component.BackColor = scheme.CheckBoxBG;
                    component.ForeColor = scheme.CheckBoxFG;
                }
                else if (component is GroupBox)
                {
                    component.BackColor = scheme.GroupBoxBG;
                    component.ForeColor = scheme.GroupBoxFG;
                }
                else if (component is TabControl)
                {
                    component.BackColor = scheme.TabControlBG;
                    component.ForeColor = scheme.TabControlFG;
                }

            }

            this.ForeColor = scheme.FormFG;
            this.BackColor = scheme.FormBG;
            //  MainForm.ActiveForm.ForeColor = scheme.FormFG;
            //  MainForm.ActiveForm.BackColor = scheme.FormBG;
        }


    }

    #region Extensions

    public static class ArrayExtensions
    {
        public static void Init<T>(this T[] array, T defaultValue)
        {
            if (array == null)
                return;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = defaultValue;
            }
        }

        public static void Fill<T>(this T[] array, T[] data)
        {
            if (array == null)
                return;

            for (int i = 0; i < array.Length; i += data.Length)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    try
                    {
                        array[i + j] = data[j];
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }
    }

    public class SongItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class MarkerItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public byte Tab { get; set; }
        public string Actor { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class AnimationItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public byte Bank { get; set; }
        public bool Transparent { get; set; }
        public bool Light { get; set; }
        public bool Multitexture { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }



    public class FlagItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public uint MinValue { get; set; }
        public uint MaxValue { get; set; }
        public bool Bitwise { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class FlagPreset : ToolStripMenuItem
    {
        public object Value { get; set; }
        public uint Bitwise { get; set; }
        public byte Type { get; set; }
        public bool Reverse { get; set; }



    }



    #endregion

    public class Settings
    {
        public bool ShowCollisionModel = false;
        public bool ShowRoomModels = true;
        public bool ApplyEnvLighting = false;
        public bool ConsecutiveRoomInject = true;
        public bool ForceRGBATextures = false;
        public bool Disablewaterboxmovement = false;
        public bool Degrees = false;
        public bool AutoaddObjects = true;
        public bool DisplayAxis = true;
        public bool DListCulling = true;
        public bool UpdateCRC = false;
        public bool RenderActors = true;
        public bool MajorasMask = false;
        public bool AutoSave = true;
        public bool UndocumentedCutsceneVars = true;
        public int MaxUndoRedo = 10;
        public int MaxLastFile = 10;
        public bool DisableEasterEgg = false;
        public bool NoDummyPoints = false;
        public bool printoffsets = false;
        public bool sharptextures = false;
        public bool TriplicateCollisionBounds = false;
        public bool HexRotations = false;
        public bool GenerateCustomDMATable = false;
        public int EmptySpace = 0;
        public int Yoffsetfix = 0;
        public bool WriteCollisionZobj = true;
        public bool Zmapoffsetnames = false;
        public bool OldRenderFormula = true;
        public bool DontConvertMultitexture = false;
        public bool CheckEmptyOffset = true;
        public bool RenderChildLink = false;
        public bool IgnoreMMDaySystem = true;
        public bool DrawSelectedCutsceneCommands = false;
        public bool colorblindaxis = false;
        public bool DisableRGBA32 = false;
        public bool firsttime = true;
        public bool command1AOoT = false;
        public bool AutoFixErrors = true;
        public bool OnlyRenderWaterboxesGeneral = true;
        public bool DisableTextureWarnings = false;
        public bool EnableNewExitFormat = false;
        public bool AutoReload = false;
    }

    public class UndoRedo
    {
        public object data = null;
        public int datatype = 0;
        public int room = 0;

        public UndoRedo(int _datatype, object _data, int _room)
        {
            data = _data;
            datatype = _datatype;
            room = _room;
        }
    }

    public class ROM
    {
        public uint SceneTable;
        public uint SceneTableEnd;
        public uint EntranceTableStart;
        public uint EntranteTableEnd;
        public uint CutsceneTableStart;
        public uint CutsceneTableEnd;
        public uint DmaTableStart;
        public uint DmaTableEnd;
        public uint SceneDmaTableStart;
        public uint SceneDmaTableEnd;
        public uint RestrictionFlagStart;
        public uint RestrictionFlagEnd;
        public uint DefaultSaveFile;
        public uint HeaderTitle;
        public uint EntranceTitle;
        public uint HeaderNewFile;
        public uint EntranceNewFile;
        public uint AgeNewFile;
        public uint AgeTitle;
        public uint RespawnChild;
        public uint RespawnAdult;
        public uint FirstScene;
        public uint SubscreenMapInfo;
        public uint SubscreenMapInfo2;
        public uint SubscreenMapCompassIcons;
        public uint SubscreenMapFloorTextures;
        public uint SubscreenMapTitleCards;
        public uint SubscreenMapFloorAmount;
        public string SubscreenPatch;
        public string Game;
        public string Prefix;
        public uint SpecialTextureTable;
        public uint SubscreenMapChestVertexData;
        public uint ObjectTable;
        public uint ActorTable;
        public uint ActorTableEnd;
        public uint ParticleTable;
        public uint ParticleTableEnd;
    }

    public class Patch
    {
        public int byteamount = 2; //can be 2, 4, and 8
        public int offset = 0;
        public uint data = 0;
        public string name = "";
        public List<byte> bytedata = new List<byte>();
        public string GetName()
        {
            if (name == "") return "Offset " + offset.ToString("X8") + " Byte Amount: " + byteamount;
            else return "";
        }

        public Patch(int _offset, int _byteamount, uint _data, string _name = "")
        {
            byteamount = _byteamount;
            offset = _offset;
            data = _data;
            name = _name;
        }

        public Patch(int _offset, int _byteamount, ulong _data, string _name = "")
        {
            byteamount = _byteamount;
            offset = _offset;
            name = _name;
            Helpers.Append64(ref bytedata, _data);
        }

        public Patch(int _offset, int _byteamount, List<byte> _bytedata, string _name = "")
        {
            byteamount = _byteamount;
            offset = _offset;
            bytedata = _bytedata;
            name = _name;
        }

    }

    public class PropertyMatch
    {
        public int room;
        public int ID;
        public string name;

        public PropertyMatch(int _ID, int _room, string _name)
        {
            room = _room;
            name = _name;
            ID = _ID;
        }
    }

    public class ObjectInfo
    {
        public int size = 0;
        public string name = "";
        public string usedby = "";
        public ObjectInfo(int _size, string _name, string _usedby)

        {
            size = _size;
            name = _name;
            usedby = _usedby;
        }
    }

    public class ActorInfo
    {
        public string name = "";
        public List<ActorProperty> actorproperties = new List<ActorProperty>();
        public string objects = "";
        public ActorInfo(string _name, List<ActorProperty> _actorproperties, string _objects)
        {
            name = _name;
            actorproperties = _actorproperties;
            objects = _objects;
        }
    }

    public class ColorScheme
    {
        public Color PanelBG;
        public Color PanelFG;
        public Color TextBoxBG;
        public Color TextBoxFG;
        public Color ButtonBG;
        public Color ButtonFG;
        public Color LabelFG;
        public Color FormFG;
        public Color FormBG;
        public Color ComboBoxFG;
        public Color ComboBoxBG;
        public Color NumericUpDownFG;
        public Color NumericUpDownBG;
        public Color GroupBoxFG;
        public Color GroupBoxBG;
        public Color CheckBoxFG;
        public Color CheckBoxBG;
        public Color TabControlFG;
        public Color TabControlBG;

        public ColorScheme(bool dark)
        {
            if (dark)
            {
                this.PanelBG = Color.DimGray;
                this.PanelFG = Color.White;
                this.TextBoxBG = Color.DimGray;
                this.TextBoxFG = Color.White;
                this.ButtonBG = Color.DimGray;
                this.ButtonFG = Color.White;
                this.FormFG = Color.White;
                this.FormBG = Color.DimGray;
                this.LabelFG = Color.White;
                this.ComboBoxFG = Color.White;
                this.ComboBoxBG = Color.DimGray;
                this.NumericUpDownFG = Color.White;
                this.NumericUpDownBG = Color.DimGray;

                this.GroupBoxFG = Color.White;
                this.GroupBoxBG = Color.DimGray;
                this.CheckBoxFG = Color.White;
                this.CheckBoxBG = Color.DimGray;
                this.TabControlFG = Color.White;
                this.TabControlBG = Color.DimGray;
            }
        }


    }



}
