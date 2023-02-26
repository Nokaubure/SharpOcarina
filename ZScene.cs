using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SharpOcarina.SayakaGL;
using Tommy;

namespace SharpOcarina
{
    public class ZScene
    {
        #region Classes, Variables, etc.

        public class ZRoom
        {
            public string ModelFilename = string.Empty;
            public int StartTime = 0xFFFF;
            public byte TimeSpeed = 0x0A;
            public bool DisableSkybox = false;
            public bool DisableSunMoon = false;
            public byte WindWest = 0x0F;
            public byte WindVertical = 0x28;
            public byte WindSouth = 0x6D;
            public byte WindStrength = 0xBE;
            public byte Restriction = 0x00;
            public byte IdleAnim = 0x00;
            public bool ShowInvisibleActors = false;
            public bool DisableWarpSongs = false;
            public byte Echo = 0x00;
            public bool AffectedByPointLight = false;
            public List<ZAdditionalLight> AdditionalLights = new List<ZAdditionalLight>();
       
            [XmlIgnore]
            public List<LODgroup> LODgroups = new List<LODgroup>();
            [XmlIgnore]
            public List<NDisplayList> LODdlists = new List<NDisplayList>();

            [XmlIgnore]
            private string _ModelShortFilename = string.Empty;

            [XmlIgnore] public uint newoffset = 0;

            [XmlIgnore] public uint oldoffset = 0;

            public int InjectOffset;
            [XmlIgnore]
            public int FullDataLength;

            public List<ZUShort> ZObjects = null;
            public List<ZActor> ZActors = null;
            [XmlIgnore]
            public List<ObjFile.Group> TrueGroups = null;
            [XmlIgnore]
            public ObjFile ObjModel = null;
            [XmlIgnore]
            public List<NDisplayList> DLists = null;

            public string ModelShortFilename
            {
                get { return _ModelShortFilename; }
                set { _ModelShortFilename = value; }
            }

            public class ZGroupSettings
            {
                public uint[] TintAlpha = new uint[1];
                public int[] TileS = new int[1];
                public int[] TileT = new int[1];
                public int[] PolyType = new int[1];
                public bool[] BackfaceCulling = new bool[1];
                public bool[] Animated = new bool[1];
                public bool[] Metallic = new bool[1];
                public bool[] EnvColor = new bool[1];
                public bool[] Decal = new bool[1];
                public bool[] IgnoreFog = new bool[1];
                public bool[] SmoothRGBAEdges = new bool[1];
                public bool[] Pixelated = new bool[1];
                public bool[] Billboard = new bool[1];
                public bool[] TwoAxisBillboard = new bool[1];
                public int[] MultiTexMaterial = new int[1];
                public int[] ShiftS = new int[1];
                public int[] ShiftT = new int[1];
                public int[] BaseShiftS = new int[1];
                public int[] BaseShiftT = new int[1];
                public uint[] MultiTexAlpha = new uint[2];
                public bool[] ReverseLight = new bool[1];
                public int[] AnimationBank = new int[1];
                public int[] LodGroup = new int[1];
                public int[] LodDistance = new int[1];
                public bool[] LOD = new bool[1];
                public bool[] AlphaMask = new bool[1];
                public bool[] RenderLast = new bool[1];
                public bool[] VertexNormals = new bool[1];
                public bool[] Custom = new bool[1];
                public ulong[,] CustomDL = new ulong[1,4];

                [XmlIgnore]
                public string[] groupname = new string[1];

                public ZGroupSettings Clone()
                {
                    ZGroupSettings clone = (ZGroupSettings)this.MemberwiseClone();

                    return clone;
                }

                public void CopyVals(int indexA, ZGroupSettings B, int index)
                {
                TintAlpha[indexA] = B.TintAlpha[index];
                TileS[indexA] = B.TileS[index];
                TileT[indexA] = B.TileT[index];
                PolyType[indexA] = B.PolyType[index];
                BackfaceCulling[indexA] = B.BackfaceCulling[index];
                Animated[indexA] = B.Animated[index];
                Metallic[indexA] = B.Metallic[index];
                EnvColor[indexA] = B.EnvColor[index];
                Decal[indexA] = B.Decal[index];
                IgnoreFog[indexA] = B.IgnoreFog[index];
                SmoothRGBAEdges[indexA] = B.SmoothRGBAEdges[index];
                MultiTexMaterial[indexA] = B.MultiTexMaterial[index];
                ShiftS[indexA] = B.ShiftS[index];
                ShiftT[indexA] = B.ShiftT[index];
                BaseShiftS[indexA] = B.BaseShiftS[index];
                BaseShiftT[indexA] = B.BaseShiftT[index];
                MultiTexAlpha[indexA] = B.MultiTexAlpha[index];
                ReverseLight[indexA] = B.ReverseLight[index];
                Pixelated[indexA] = B.Pixelated[index];
                Billboard[indexA] = B.Billboard[index];
                TwoAxisBillboard[indexA] = B.TwoAxisBillboard[index];
                AnimationBank[indexA] = B.AnimationBank[index];
                LodGroup[indexA] = B.LodGroup[index];
                LodDistance[indexA] = B.LodDistance[index];
                LOD[indexA] = B.LOD[index];
                AlphaMask[indexA] = B.AlphaMask[index];
                RenderLast[indexA] = B.RenderLast[index];
                VertexNormals[indexA] = B.VertexNormals[index];
                Custom[indexA] = B.Custom[index];
                CustomDL[indexA,0] = B.CustomDL[index,0];
                CustomDL[indexA, 1] = B.CustomDL[index, 1];
                CustomDL[indexA, 2] = B.CustomDL[index, 2];
                CustomDL[indexA, 3] = B.CustomDL[index, 3];
                }
            }

            public ZGroupSettings GroupSettings = new ZGroupSettings();

            [XmlIgnore]
            public List<byte> RoomData;

            public List<byte> OriginalRoomData;

            [XmlIgnore]
            public List<SayakaGL.UcodeSimulator.DisplayListStruct> N64DLists;
            [XmlIgnore]
            public int MeshHeaderOffset = 0;

            public int OriginalMeshHeaderOffset = 0;

            public ZRoom() { }

            public ZRoom Clone()
            {
                ZRoom clone = (ZRoom)this.MemberwiseClone();
                clone.ZActors = ZActors.ConvertAll(x => (x.Clone()));
                clone.ZObjects = ZObjects.ConvertAll(x => (x.Clone()));
                clone.AdditionalLights = AdditionalLights.ConvertAll(x => (x.Clone()));

                return clone;
            }


        }

        public class ZUShort
        {
            [XmlIgnore]
            private ushort _Value;

            [XmlIgnore]
            public string ValueHex
            {
                get { return _Value.ToString("X4"); }
            }

            public ushort Value
            {
                get { return _Value; }
                set { _Value = value; }
            }

            public ZUShort() { }

            public ZUShort(ushort value)
            {
                _Value = value;
            }

            public ZUShort Clone()
            {
                return (ZUShort)this.MemberwiseClone();
            }
        }

        public string Name;
        public float Scale;
        public byte Music, NightSFX, Skybox, Weather;
        public ushort SpecialObject = 0x0003;
        public byte ElfMessage = 0x00;
        public int InjectOffset = 0x02D00000;
        public int SceneNumber = 108;
        public byte Reverb = 0x00;
        public byte CameraMovement = 0x00;
        public byte WorldMap = 0x00;
        public byte SceneSettings = 0x00;
        public bool OutdoorLight = true;
        public short FixedCameraXPos = 0;
        public short FixedCameraYPos = 0;
        public short FixedCameraZPos = 0;
        public short FixedCameraXRot = 0;
        public short FixedCameraYRot = 0;
        public short FixedCameraZRot = 0;
        public byte TimeCtrl = 0x00;
        public byte SkyboxType = 0x00;
        public bool Cloudy = false;
        public bool ContinualInject = true;
        public bool NewRoomMode = false;
        public bool Prerendered = false;
        public bool PregeneratedMesh = false;
        public string JFIFpath = "";
        public int cloneid = 0;
        public int CutsceneEntrance = 0x0000;
        public int CutsceneEntranceNum = 0x00;
        public int CutsceneFlag = 0x00;
        public int CutsceneTableRow = -1;
        public int version = 0;
        public List<ZSegmentFunction> SegmentFunctions = new List<ZSegmentFunction>();
        public List<string> prerenderimages = new List<string>();
        public bool inherittextureanims = true;
        [XmlIgnore]
        public uint MainHeaderTextureAnimOffset = 0;
        public uint RestrictionFlags = 0;
        public byte[] TitleCard = new byte[]{};

        [XmlIgnore]
        private uint cachecutsceneoffset = 0;

        public List<ZRoom> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; }
        }

        /* Scene data */
        public string CollisionFilename = string.Empty;
        [XmlIgnore]
        public ObjFile ColModel = null;
        public List<ZActor> Transitions = new List<ZActor>();
        public List<ZActor> SpawnPoints = new List<ZActor>();
        public List<ZEnvironment> Environments = new List<ZEnvironment>();
        public List<ZWaterbox> Waterboxes = new List<ZWaterbox>();
        public List<ZColPolyType> PolyTypes = new List<ZColPolyType>();
        public List<ZUShort> ExitList = new List<ZUShort>();
        public List<ZExit> ExitListV2 = new List<ZExit>();
        public List<ZPathway> Pathways = new List<ZPathway>();
        public List<ZCutscene> Cutscene = new List<ZCutscene>();
        public List<ZCamera> Cameras = new List<ZCamera>();
        public List<ZSceneHeader> SceneHeaders = new List<ZSceneHeader>();
        public List<ObjFile.Material> AdditionalTextures = new List<ObjFile.Material>();
        public List<ZTextureAnim> TextureAnims = new List<ZTextureAnim>();

        [XmlIgnore]
        public Dictionary<string, int> textureoffsets = new Dictionary<string, int>();
        [XmlIgnore]
        public Dictionary<byte[], int> paletteoffsets = new Dictionary<byte[], int>();

        [XmlIgnore]
        private List<byte> SceneData;

        public List<byte> OriginalSceneData;

        [XmlIgnore]
        private int CmdCollisionOffset = -1, CmdMapListOffset = -1, CmdTransitionsOffset = -1, CmdExitListOffset = -1, CmdSpawnPointOffset = -1, CmdEnvironmentsOffset = -1, CmdEntranceListOffset = -1, CmdPathwayOffset = -1, CmdCutsceneOffset = -1, MMCmdCameraOffset = -1, MMCmdMinimapOffset = -1, MMCmdTextureAnimOffset = -1, CmdAlternateSceneHeader = -1, CmdRoomAlternateSceneHeader = -1;

        /* Room data */
        private List<ZRoom> _Rooms = new List<ZRoom>();

        private string _BasePath = string.Empty;

        [XmlIgnore]
        const uint Dummy = 0xDEADBEEF;

        [XmlIgnore]
        int CollisionOffset = 0, EntranceOffset = 0, RoomOffsets = 0;

        [XmlIgnore]
        int RoomIndex;

        [XmlIgnore]
        private int CmdMeshHeaderOffset = -1, CmdObjectOffset = -1, CmdActorOffset = -1, CmdExtraLightOffset = -1, CmdAlternateRoomHeader = -1, PrerenderedFixOffset = -1;
        [XmlIgnore]
        private int MeshHeaderOffset, ObjectOffset, ActorOffset, ExtraLightOffset;
        [XmlIgnore]
        private List<NTexture> Textures;

        [XmlIgnore]
        public string BasePath
        {
            get { return _BasePath; }
            set { _BasePath = value; }
        }

        #endregion

        #region Constructors, Basic Functions

        public ZScene() { }


        public void Prepare()
        {
            foreach (ZRoom Room in _Rooms)
            {
                Room.ObjModel.BasePath = BasePath;
                Room.ObjModel.Prepare(Room.TrueGroups);
            }
        }

        #endregion

        #region Helper Functions

        private void AddPadding(ref List<byte> Data, int Length)
        {
            int ToAdd = Length - (Data.Count % Length);
            if (ToAdd != Length) for (int i = 0; i < ToAdd; i++) Data.Add(0);
        }

        public void AddRoom(string Filename, string extrastr = "", int subs = 0)
        {
            ZRoom NewRoom = new ZRoom();

            NewRoom.ModelFilename = Filename;
            NewRoom.ModelShortFilename = Path.GetFileNameWithoutExtension(Filename) + extrastr;
            NewRoom.ObjModel = new ObjFile(Filename);
            NewRoom.ZObjects = new List<ZUShort>();
            NewRoom.ZActors = new List<ZActor>();
            NewRoom.InjectOffset = 0x02D00000;
            NewRoom.TrueGroups = new List<ObjFile.Group>();



            int groupcount = NewRoom.ObjModel.Groups.Count;

            if (NewRoomMode)
            {
                groupcount = 0;
                foreach (ObjFile.Group group in NewRoom.ObjModel.Groups)
                {
                    group.Name = group.Name.Replace("TAG_", "#");

                    if (group.Name.ToLower().Contains("#room" + (_Rooms.Count - subs)))
                    {
                        if ((_Rooms.Count - subs) < 10)
                        {
                            Regex regex = new Regex(@"#room" + (_Rooms.Count - subs) + "([^0-9]|$)");
                            Match match = regex.Match(group.Name.ToLower());
                            if (!match.Success) continue;
                        }
                        groupcount++;



                        NewRoom.TrueGroups.Add(group);
                    }
                }

            }
            else { NewRoom.TrueGroups = NewRoom.ObjModel.Groups; }

            ulong polytype = 0;
            NewRoom.GroupSettings.PolyType = new int[groupcount];



            NewRoom.GroupSettings.TintAlpha = new uint[groupcount];
            NewRoom.GroupSettings.TileS = new int[groupcount];
            NewRoom.GroupSettings.TileT = new int[groupcount];
            NewRoom.GroupSettings.BackfaceCulling = new bool[groupcount];
            NewRoom.GroupSettings.Animated = new bool[groupcount];
            NewRoom.GroupSettings.Metallic = new bool[groupcount];
            NewRoom.GroupSettings.EnvColor = new bool[groupcount];
            NewRoom.GroupSettings.Decal = new bool[groupcount];
            NewRoom.GroupSettings.IgnoreFog = new bool[groupcount];
            NewRoom.GroupSettings.SmoothRGBAEdges = new bool[groupcount];
            NewRoom.GroupSettings.Pixelated = new bool[groupcount];
            NewRoom.GroupSettings.Billboard = new bool[groupcount];
            NewRoom.GroupSettings.TwoAxisBillboard = new bool[groupcount];
            NewRoom.GroupSettings.ReverseLight = new bool[groupcount];
            NewRoom.GroupSettings.MultiTexMaterial = new int[groupcount];
            NewRoom.GroupSettings.ShiftS = new int[groupcount];
            NewRoom.GroupSettings.ShiftT = new int[groupcount];
            NewRoom.GroupSettings.BaseShiftS = new int[groupcount];
            NewRoom.GroupSettings.BaseShiftT = new int[groupcount];
            NewRoom.GroupSettings.MultiTexAlpha = new uint[groupcount];
            NewRoom.GroupSettings.AnimationBank = new int[groupcount];
            NewRoom.GroupSettings.LodGroup = new int[groupcount];
            NewRoom.GroupSettings.LodDistance = new int[groupcount];
            NewRoom.GroupSettings.LOD = new bool[groupcount];
            NewRoom.GroupSettings.AlphaMask = new bool[groupcount];
            NewRoom.GroupSettings.RenderLast = new bool[groupcount];
            NewRoom.GroupSettings.VertexNormals = new bool[groupcount];
            NewRoom.GroupSettings.Custom = new bool[groupcount];
            NewRoom.GroupSettings.CustomDL = new ulong[groupcount,4];

            for (int i = 0; i < groupcount; i++)
            {
                NewRoom.GroupSettings.TintAlpha[i] = 0xFFFFFFFF;
                NewRoom.GroupSettings.MultiTexAlpha[i] = 0xFFFFFFFF;
                NewRoom.GroupSettings.TileS[i] = GBI.G_TX_WRAP;
                NewRoom.GroupSettings.TileT[i] = GBI.G_TX_WRAP;
                //  NewRoom.GroupSettings.PolyType[i] = 0x0000000000000000;
                NewRoom.GroupSettings.BackfaceCulling[i] = true;
                NewRoom.GroupSettings.Animated[i] = false;
                NewRoom.GroupSettings.Metallic[i] = false;
                NewRoom.GroupSettings.EnvColor[i] = false;
                NewRoom.GroupSettings.Decal[i] = false;
                NewRoom.GroupSettings.IgnoreFog[i] = false;
                NewRoom.GroupSettings.SmoothRGBAEdges[i] = false;
                NewRoom.GroupSettings.Pixelated[i] = false;
                NewRoom.GroupSettings.Billboard[i] = false;
                NewRoom.GroupSettings.TwoAxisBillboard[i] = false;
                NewRoom.GroupSettings.ReverseLight[i] = false;
                NewRoom.GroupSettings.MultiTexMaterial[i] = -1;
                NewRoom.GroupSettings.ShiftS[i] = GBI.G_TX_NOLOD;
                NewRoom.GroupSettings.ShiftT[i] = GBI.G_TX_NOLOD;
                NewRoom.GroupSettings.BaseShiftS[i] = GBI.G_TX_NOLOD;
                NewRoom.GroupSettings.BaseShiftT[i] = GBI.G_TX_NOLOD;
                NewRoom.GroupSettings.AnimationBank[i] = 8;
                NewRoom.GroupSettings.LodGroup[i] = 0;
                NewRoom.GroupSettings.LodDistance[i] = 0;
                NewRoom.GroupSettings.LOD[i] = false;
                NewRoom.GroupSettings.AlphaMask[i] = false;
                NewRoom.GroupSettings.RenderLast[i] = false;
                NewRoom.GroupSettings.VertexNormals[i] = false;
                NewRoom.GroupSettings.Custom[i] = false;
                NewRoom.GroupSettings.CustomDL[i, 0] = 0;
                NewRoom.GroupSettings.CustomDL[i, 1] = 0;
                NewRoom.GroupSettings.CustomDL[i, 2] = 0;
                NewRoom.GroupSettings.CustomDL[i, 3] = 0;
                //dungen

                ObjFile.Group group = NewRoom.TrueGroups[i];

                ApplyMeshTags(group, NewRoom, i);

                
            }



            _Rooms.Add(NewRoom);
        }

        public void ApplyMeshTags(ObjFile.Group group, ZRoom NewRoom, int i)
        {

            group.Name = group.Name.Replace("TAG_", "#");

            if (group.Name.ToLower().Contains("#alpha"))
            {
                int alphanum = 0;
                if (!Int32.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#alpha") + 6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out alphanum))
                {
                    MessageBox.Show("Bad usage of alpha tag. It should be #AlphaXX (XX = amount in hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //int alphanum = Convert.ToInt32(group.Name.ToLower().Substring(group.Name.IndexOf("#alpha") + 6, 2), 16);
                NewRoom.GroupSettings.TintAlpha[i] = (uint)(0x00FFFFFF | (alphanum << 24));
                group.TintAlpha = (uint)(0x00FFFFFF | (alphanum << 24));
            }
            if (group.Name.ToLower().Contains("#animated"))
            {
                int animatednum = 0;
                if (!Int32.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#animated") + 9, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out animatednum))
                {
                    MessageBox.Show("Bad usage of animated tag. It should be #AnimatedX (X = bank in hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                animatednum = MainForm.Clamp(animatednum, 8, 0xF);
                //int animatednum = Convert.ToInt32(, 16);
                NewRoom.GroupSettings.AnimationBank[i] = animatednum;
                group.AnimationBank = animatednum;
                NewRoom.GroupSettings.Animated[i] = true;
                group.Animated = true;

            }
            if (group.Name.ToLower().Contains("#multialpha"))
            {
                int alphanum = 0;
                if (!Int32.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#multialpha") + 11, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out alphanum))
                {
                    MessageBox.Show("Bad usage of multialpha tag. It should be #MultiAlphaXX (XX = amount in hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                NewRoom.GroupSettings.MultiTexAlpha[i] = (uint)(0x00FFFFFF | (alphanum << 24));
                group.MultiTexAlpha = (uint)(0x00FFFFFF | (alphanum << 24));
            }
            if (group.Name.ToLower().Contains("#shifts"))
            {
                int shift = 0;

                if (!Int32.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#shifts") + 7, 2), out shift))
                {
                    MessageBox.Show("Bad usage of shiftS tag. It should be #ShiftSXX (XX = amount in decimal) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shift = MainForm.Clamp(shift, -9, 9);
                NewRoom.GroupSettings.BaseShiftS[i] = shift;
                group.BaseShiftS = shift;
            }
            if (group.Name.ToLower().Contains("#shiftt"))
            {
                int shift = 0;
                if (!Int32.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#shiftt") + 7, 2), out shift))
                {
                    MessageBox.Show("Bad usage of shiftT tag. It should be #ShiftTXX (XX = amount in decimal) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shift = MainForm.Clamp(shift, -9, 9);
                NewRoom.GroupSettings.BaseShiftT[i] = shift;
                group.BaseShiftT = shift;
            }
            if (group.Name.ToLower().Contains("#multishifts"))
            {
                int shift = 0;
                if (!Int32.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#multishifts") + 12, 2), out shift))
                {
                    MessageBox.Show("Bad usage of multishiftS tag. It should be #MultiShiftSXX (XX = amount in decimal) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shift = MainForm.Clamp(shift, -9, 9);
                NewRoom.GroupSettings.ShiftS[i] = shift;
                group.ShiftS = shift;
            }
            if (group.Name.ToLower().Contains("#multishiftt"))
            {
                int shift = 0;
                if (!Int32.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#multishiftt") + 12, 2), out shift))
                {
                    MessageBox.Show("Bad usage of multishiftT tag. It should be #MultiShiftTXX (XX = amount in decimal) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shift = MainForm.Clamp(shift, -9, 9);
                NewRoom.GroupSettings.ShiftT[i] = shift;
                group.ShiftT = shift;
            }
            if (group.Name.ToLower().Contains("#backfaceculling"))
            {
                NewRoom.GroupSettings.BackfaceCulling[i] = false;
                group.BackfaceCulling = false;
            }
            if (group.Name.ToLower().Contains("#metallic"))
            {
                NewRoom.GroupSettings.Metallic[i] = true;
                group.Metallic = true;
            }
            if (group.Name.ToLower().Contains("#decal"))
            {
                NewRoom.GroupSettings.Decal[i] = true;
                group.Decal = true;
            }
            if (group.Name.ToLower().Contains("#billboard"))
            {
                NewRoom.GroupSettings.Billboard[i] = true;
                group.Billboard = true;
            }
            if (group.Name.ToLower().Contains("#2dbillboard"))
            {
                NewRoom.GroupSettings.TwoAxisBillboard[i] = true;
                group.TwoAxisBillboard = true;
            }
            if (group.Name.ToLower().Contains("#mirrorx"))
            {
                NewRoom.GroupSettings.TileS[i] = GBI.G_TX_MIRROR;
                group.TileS = GBI.G_TX_MIRROR;
            }
            if (group.Name.ToLower().Contains("#clampx"))
            {
                NewRoom.GroupSettings.TileS[i] = GBI.G_TX_CLAMP;
                group.TileS = GBI.G_TX_CLAMP;
            }
            if (group.Name.ToLower().Contains("#mirrory"))
            {
                NewRoom.GroupSettings.TileT[i] = GBI.G_TX_MIRROR;
                group.TileT = GBI.G_TX_MIRROR;
            }
            if (group.Name.ToLower().Contains("#clampy"))
            {
                NewRoom.GroupSettings.TileT[i] = GBI.G_TX_CLAMP;
                group.TileT = GBI.G_TX_CLAMP;
            }
            if (group.Name.ToLower().Contains("#ignorefog"))
            {
                NewRoom.GroupSettings.IgnoreFog[i] = true;
                group.IgnoreFog = true;
            }
            if (group.Name.ToLower().Contains("#smoothrgbaledges"))
            {
                NewRoom.GroupSettings.SmoothRGBAEdges[i] = true;
                group.SmoothRGBAEdges = true;
            }
            if (group.Name.ToLower().Contains("#pixelated"))
            {
                NewRoom.GroupSettings.Pixelated[i] = true;
                group.Pixelated = true;
            }
            if (group.Name.ToLower().Contains("#envcolor"))
            {
                NewRoom.GroupSettings.EnvColor[i] = true;
                group.EnvColor = true;
            }
            if (group.Name.ToLower().Contains("#reverselight"))
            {
                NewRoom.GroupSettings.ReverseLight[i] = true;
                group.ReverseLight = true;
            }
            if (group.Name.ToLower().Contains("#alphamask"))
            {
                NewRoom.GroupSettings.AlphaMask[i] = true;
                group.AlphaMask = true;
            }
            if (group.Name.ToLower().Contains("#renderlast"))
            {
                NewRoom.GroupSettings.RenderLast[i] = true;
                group.RenderLast = true;
            }
            if (group.Name.ToLower().Contains("#vertexnormals"))
            {
                NewRoom.GroupSettings.VertexNormals[i] = true;
                group.VertexNormals = true;
            }
            if (group.Name.ToLower().Contains("#lodgroup"))
            {
                int animatednum = 0;
                if (!Int32.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#lodgroup") + 9, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out animatednum))
                {
                    MessageBox.Show("Bad usage of lodgroup tag. It should be #LODGroupX (X = group in hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                NewRoom.GroupSettings.LOD[i] = true;
                group.LOD = true;
                NewRoom.GroupSettings.LodGroup[i] = animatednum;
                group.LodGroup = animatednum;
            }
            if (group.Name.ToLower().Contains("#loddistance"))
            {
                int animatednum = 0;
                if (!Int32.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#loddistance") + 12, 4), out animatednum))
                {
                    MessageBox.Show("Bad usage of loddistance tag. It should be #LODDistanceXXXX (XXXX = distance in dec,) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                NewRoom.GroupSettings.LOD[i] = true;
                group.LOD = true;
                NewRoom.GroupSettings.LodDistance[i] = animatednum;
                group.LodDistance = animatednum;
            }
            if (group.Name.ToLower().Contains("#fc"))
            {
                ulong command = 0;
                if (!UInt64.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#FC") + 3, 14), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out command))
                {
                    MessageBox.Show("Bad usage of FC tag. It should be #FCXXXXXXXXXXXXXX", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                NewRoom.GroupSettings.CustomDL[i,0] = command;
                group.CustomDL[0] = command;
                NewRoom.GroupSettings.Custom[i] = true;
                group.Custom = true;
            }
            if (group.Name.ToLower().Contains("#d9"))
            {
                ulong command = 0;
                if (!UInt64.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#D9") + 3, 14), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out command))
                {
                    MessageBox.Show("Bad usage of D9 tag. It should be #D9XXXXXXXXXXXXXX", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                NewRoom.GroupSettings.CustomDL[i, 1] = command;
                group.CustomDL[1] = command;
                NewRoom.GroupSettings.Custom[i] = true;
                group.Custom = true;
            }
            if (group.Name.ToLower().Contains("#e2"))
            {
                ulong command = 0;
                if (!UInt64.TryParse(group.Name.Substring(group.Name.ToLower().IndexOf("#E2") + 3, 14), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out command))
                {
                    MessageBox.Show("Bad usage of E2 tag. It should be #E2XXXXXXXXXXXXXX", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                NewRoom.GroupSettings.CustomDL[i, 2] = command;
                group.CustomDL[2] = command;
                NewRoom.GroupSettings.Custom[i] = true;
                group.Custom = true;
            }


        }


        #endregion

        #region Saving/Injection...

        public void ConvertInject(string Filename, bool ConsecutiveRoomInject, bool ForceRGBATextures, string Game)
        {
            var data = new List<byte>(File.ReadAllBytes(Filename));
            ROM rom = MainForm.CheckVersion(data);

            MainForm.n64preview = false;

            ConvertScene(ConsecutiveRoomInject, ForceRGBATextures, this, rom.Game);

            if (ColModel == null) return;

            foreach(ZSceneHeader sceneheader in SceneHeaders)
            {
                if (!sceneheader.SameAsPrevious) sceneheader.Scene.ConvertScene(ConsecutiveRoomInject, ForceRGBATextures, this, rom.Game);
            }


            // fix scene header offsets
            foreach (ZSceneHeader sceneheader in SceneHeaders)
            {
                if (sceneheader.SameAsPrevious)
                {
                    Helpers.Overwrite32(ref SceneData, sceneheader._InjectOffset, sceneheader.CloneFromHeader == 0 ? 0 : SceneHeaders[sceneheader.CloneFromHeader - 1]._InjectOffsetValue);
                    for (int i = 0; i < Rooms.Count; i++)
                    {
                        Helpers.Overwrite32(ref _Rooms[i].RoomData, sceneheader._RoomInjectOffset[i], sceneheader.CloneFromHeader == 0 ? 0 : SceneHeaders[sceneheader.CloneFromHeader - 1]._RoomInjectOffsetValues[i]);
                    }
                }
            }


            List<String> TmpMsg = new List<string>();

            // Crude inject method here
                int RoomInjectOffset = _Rooms[0].InjectOffset;
                RoomInjectOffset = InjectOffset + SceneData.Count;
                for (int i = 0; i < _Rooms.Count; i++)
                {
#if DEBUG
                    Console.WriteLine("INJECTING TO " + Filename + ", OFFSET " + RoomInjectOffset.ToString("X"));
#endif
                if (MainForm.settings.CheckEmptyOffset == true)
                {
                    string overridefile = CheckEmptyOffset(RoomInjectOffset, _Rooms[i].RoomData.ToArray().Length, rom, data);
                    if (overridefile != "")
                    {
                        if (MessageBox.Show("There's a file '" + overridefile + "'in this offset, inject anyways?", "Warning",MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            Helpers.GenericInject(Filename, RoomInjectOffset, _Rooms[i].RoomData.ToArray(), _Rooms[i].RoomData.Count);
                        }
                    }
                    else
                        Helpers.GenericInject(Filename, RoomInjectOffset, _Rooms[i].RoomData.ToArray(), _Rooms[i].RoomData.Count);
                }
                else
                {
                    Helpers.GenericInject(Filename, RoomInjectOffset, _Rooms[i].RoomData.ToArray(), _Rooms[i].RoomData.Count);
                }

                    

                    TmpMsg.Add("Room " + i + " offset: " + RoomInjectOffset.ToString("X8") + " - " + (RoomInjectOffset + _Rooms[i].FullDataLength).ToString("X8"));

                    RoomInjectOffset += _Rooms[i].FullDataLength;
                }

            //titlecard edit

            BinaryWriter BWS;

            if (TitleCard.Length > 0)
            {
                List<Byte> Output = new List<byte>();

                MemoryStream ms = new MemoryStream(TitleCard);

                BWS = new BinaryWriter(File.OpenWrite(Filename));

                BWS.Seek(RoomInjectOffset, SeekOrigin.Begin);

                Bitmap texture = (Bitmap)(Image.FromStream(ms));

                int x = 0;
                int y = 0;
                int gray = 0;
                int alpha = 0;

                for (int i = 0; y < 72; i++)
                {
                    //   Console.WriteLine(((0x4F & 0xF0) >> 4).ToString("X"));

                    if (y < texture.Height)
                    {
                        gray = (texture.GetPixel(x, y).R != 0) ? texture.GetPixel(x, y).R / 17 : 0;
                        alpha = (texture.GetPixel(x, y).A != 0) ? texture.GetPixel(x, y).A / 17 : 0;
                        Output.Add((byte)(0x00 | (gray << 4) | (alpha)));
                    }
                    else
                    {
                        Output.Add(0x00);
                    }


                    x++;
                    if (x >= texture.Width) { x = 0; y++; }
                }
                BWS.Write(Output.ToArray());

                int Offset = (int)rom.SceneTable + (SceneNumber * 0x14 + 0x08);

                BWS.Seek(Offset, SeekOrigin.Begin);
                BWS.Write((BitConverter.GetBytes(RoomInjectOffset).Reverse()).ToArray());
                BWS.Seek(Offset + 4, SeekOrigin.Begin);
                BWS.Write((BitConverter.GetBytes(RoomInjectOffset + 10368).Reverse()).ToArray());
                BWS.Close();
            }


             if (MainForm.settings.CheckEmptyOffset == true)
            {
                string overridefile = CheckEmptyOffset(InjectOffset, SceneData.ToArray().Length, rom, data);
                if (overridefile != "")
                {
                    if (MessageBox.Show("There's a file '" + overridefile + "'in this offset, inject anyways?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        Helpers.GenericInject(Filename, InjectOffset, SceneData.ToArray(), SceneData.Count);
                    }
                }
                else
                    Helpers.GenericInject(Filename, InjectOffset, SceneData.ToArray(), SceneData.Count);
            }
            else
               Helpers.GenericInject(Filename, InjectOffset, SceneData.ToArray(), SceneData.Count);


            List<byte> Temp = new List<byte>();
            Helpers.Append32(ref Temp, (uint)InjectOffset);
            Helpers.Append32(ref Temp, (uint)(InjectOffset + SceneData.Count));

            if (MainForm.settings.printoffsets)
            {
                MainForm.InjectMessages.Add("Scene offset: " + InjectOffset.ToString("X8") + " - " + (InjectOffset + SceneData.Count).ToString("X8"));
                MainForm.InjectMessages.AddRange(TmpMsg);
            }


            List<byte> Temp2 = new List<byte>();
            Helpers.Append32(ref Temp2, (uint)0x00000000 | (uint)(MainForm.settings.command1AOoT ? 0x4 : SceneSettings) << 16);




            BWS = new BinaryWriter(File.OpenWrite(Filename));
            int TableOffset;
            if (rom.Game == "OOT")//oot debug rom / mm gcn usa rom
                TableOffset = (int) (rom.SceneTable + (SceneNumber * 0x14));
            else
                TableOffset = (int) (rom.SceneTable + (SceneNumber * 0x10));


            BWS.Seek(TableOffset, SeekOrigin.Begin);
            BWS.Write(Temp.ToArray());

          

            if (rom.Game == "MM")
            {
                bool hasanim = false;
                foreach (ZRoom room in Rooms)
                {
                    if (hasanim) break;
                    foreach(ObjFile.Group group in room.TrueGroups)
                    {
                        if (group.Animated)
                        {
                            hasanim = true;
                            break;
                        }
                    }
                }

                BWS.Seek(TableOffset+0xB, SeekOrigin.Begin);
                BWS.Write((hasanim) ? 0x01 : 0x00);
                
            }

            //  Console.WriteLine("tableoffset: " + (TableOffset + 16).ToString("X"));


            if (rom.Game == "OOT")
            {
                BWS.Seek(TableOffset + 16, SeekOrigin.Begin);
                BWS.Write(Temp2.ToArray()); // Render function

                // Cutscene table edit

                if (CutsceneTableRow != -1 && Cutscene.Count > 0 && cachecutsceneoffset != 0)
                {
                    int ctableoffset = (int) (rom.CutsceneTableStart + (CutsceneTableRow * 8));
                    if (ctableoffset > rom.CutsceneTableEnd -1)
                    {
                        MessageBox.Show("Cutscene Table row is outside range! aborting cutscene table editing", "Injection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        List<byte> Temp3 = new List<byte>();


                        Helpers.Append16(ref Temp3, (ushort) CutsceneEntrance);
                        Temp3.Add(2);
                        Temp3.Add((byte) CutsceneFlag);
                        Helpers.Append32(ref Temp3, cachecutsceneoffset);


                        BWS.Seek(ctableoffset, SeekOrigin.Begin);
                        BWS.Write(Temp3.ToArray());

                        MainForm.InjectMessages.Add("Updated row " + CutsceneTableRow + " of cutscene table");
                    }

                }

                // restriction flag edit 

                //List<byte> byteList = new List<byte>((IEnumerable<byte>)File.ReadAllBytes(MainForm.GlobalROM));
           
                for (uint restrictionFlagStart = rom.RestrictionFlagStart; (long)restrictionFlagStart < (long)((int)rom.RestrictionFlagEnd - 1); restrictionFlagStart += 4)
                {
                    if ((int)data[(int)restrictionFlagStart] == this.SceneNumber)
                    {
                    

                        List<byte> Temp4 = new List<byte>();
                        Helpers.Append32(ref Temp4,(RestrictionFlags | ((uint)SceneNumber << 24)));

                        BWS.Seek((int)restrictionFlagStart, SeekOrigin.Begin);
                        BWS.Write(Temp4.ToArray());
                        break;
                    }
                }

    
                

                if (SceneSettings != 0 && !MainForm.settings.command1AOoT)
                {

                    int slot = -1;

                    XmlNodeList animnodes = XMLreader.getXMLNodes("OOT/" + "SceneAnimations", "Function");
                    AnimationItem AnimNode = new AnimationItem();
                    if (animnodes != null)
                        foreach (XmlNode node in animnodes)
                        {
                            XmlAttributeCollection nodeAtt = node.Attributes;
                            if (System.Convert.ToByte(nodeAtt["Key"].Value, 16) == SceneSettings)
                            {
                                if (nodeAtt["Slot"] != null)
                                {
                                    slot = Convert.ToInt32(nodeAtt["Slot"].Value);
                                }
                                break;
                            }
                        }

                    if (slot != -1)
                    {
                        Dictionary<int,uint> specialtextures = new Dictionary<int, uint>();

                        foreach (NTexture texture in Textures)
                        {
                            if (texture.Name != null && texture.Name.ToLower().Contains("#special"))
                            {
                                int key = 0;
                                string s = texture.Name.Substring(texture.Name.ToLower().IndexOf("#special") + 8, 1);
                                if (!Int32.TryParse(s, out key))
                                {
                                    MessageBox.Show("Bad usage of texture tag. It should be #SpecialX where X is the number following an order. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                                if (!specialtextures.ContainsKey(key))
                                {
                                    specialtextures.Add(key, texture.TexOffset | 02 << 24);
                                }
                            }
                        }

                        Temp2.Clear();

                        for(int i = 0; i < specialtextures.Count; i++)
                        {
                            if (!specialtextures.ContainsKey(i))
                            {
                                MessageBox.Show("special texture ID " + i + " is missing, you need to have special textures in order, starting from 0. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            else
                            {
                                Helpers.Append32(ref Temp2, specialtextures[i]);
                                MainForm.InjectMessages.Add("Special texture inject " + specialtextures[i].ToString("X"));
                            }
                        }

                        BWS.Seek((int) (rom.SpecialTextureTable + (8 * slot) + (slot > 1 ? 32 : 0) + (slot > 7 ? 8 : 0)), SeekOrigin.Begin);
                        BWS.Write(Temp2.ToArray()); // Render function
                    }
                }

            }


            BWS.Close();
        }

        public string CheckEmptyOffset(int offset, int size, ROM rom, List<byte> data)
        {

            return "";

            int StartDMAOffset = (int)rom.DmaTableStart;
            int EndDMAOffset = (int)rom.DmaTableEnd;
            int StartSceneOffset = (int)rom.SceneTable;
            int EndSceneOffset = (int)rom.SceneTableEnd;

            List<DMAFile> files = new List<DMAFile>();


            string file = "";
            bool found = false;
            int incr = 0;
            uint foundstartoffset = 0;
            uint foundendoffset = 0;

            
            for (int i = StartDMAOffset; i < EndDMAOffset - 1; i += 0x10)
            {
                uint startoffset = Helpers.Read32(data, i);
                uint endoffset = Helpers.Read32(data, i + 4);
                if ((offset >= startoffset && offset <= startoffset) || (offset+size > startoffset && offset+size < endoffset))
                {
                    found = true;
                    file = "File #" + incr;
                    foundstartoffset = startoffset;
                    foundendoffset = endoffset;
                    break;
                }
                incr++;
            }

            if (found)
            {
                int increment = rom.Game == "OOT" ? 0x14 : 0x10;

                    uint startoffset = Helpers.Read32(data, StartSceneOffset + (increment * SceneNumber));
                    uint endoffset = Helpers.Read32(data, StartSceneOffset + (increment * SceneNumber) + 4);


                    for (int y = (int)startoffset; y < endoffset - 1; y += 4)
                    {
                        uint test = Helpers.Read32(data, y);

                        //check room command and segment offset 02
                        if ((test & 0xFF00FFFF) == 0x04000000 && data[y + 4] == 0x02)
                        {
                            uint roomlist = Helpers.Read24(data, y + 5) + startoffset;
                            int roomcount = data[y + 1];

                            int r = roomcount - 1;
                            endoffset = Helpers.Read32(data, (int)(roomlist + (r * 0x8) + 4));

                           if ((foundstartoffset >= startoffset && foundstartoffset <= endoffset) || (foundendoffset >= startoffset && foundendoffset <= endoffset))
                           {
                                 return "";
                           }

                            break;
                        }
                    }
                    
                

            }
            return file;
        }

        public void ConvertSave(string Filepath, bool ConsecutiveRoomInject, bool ForceRGBATextures, int zzrp)
        {
            string game = (!MainForm.settings.MajorasMask) ? "OOT" : "MM";

            if (zzrp > 0)
            {
                if (zzrp != 3)
                {
                    if (!Directory.Exists(Filepath + "Scene"))
                    {
                        Directory.CreateDirectory(Filepath + "Scene");
                        if (zzrp == 1)
                            Directory.CreateDirectory(Filepath + @"Scene/" + SceneNumber + " - " + Helpers.MakeValidFileName(Name));
                        else if (zzrp == 2)
                            Directory.CreateDirectory(Filepath + @"Scene/0x" + SceneNumber.ToString("X2") + " - " + Helpers.MakeValidFileName(Name));

                    }
                }
                else
                {
                    Directory.CreateDirectory(Filepath + @"rom/scene/0x" + SceneNumber.ToString("X2") + "-" + Helpers.MakeValidFileName(Name));
                }
                string[] subdirectoryEntries = (zzrp != 3) ? Directory.GetDirectories(Filepath + "Scene") : Directory.GetDirectories(Filepath + "rom/scene");
                string target = "";
                foreach (string subdirectory in subdirectoryEntries)
                {
                    string number = subdirectory;
                    if (number.Contains(Path.DirectorySeparatorChar)) number = number.Substring(number.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    if (number.Contains(" "))
                        number = number.Split(' ')[0];
                    else
                        number = number.Split('-')[0];
                    if (number == SceneNumber + "" || number == "0x" + this.SceneNumber.ToString("X2")) 
                    {
                        target = subdirectory;
                        break;
                    }
                }
                if (target == "")
                {
                    if (zzrp == 1)
                    {
                        Directory.CreateDirectory(Filepath + @"Scene/" + SceneNumber + " - " + Helpers.MakeValidFileName(Name));
                        target = Filepath + @"Scene/" + SceneNumber + " - " + Helpers.MakeValidFileName(Name);
                    }
                    else if (zzrp == 2)
                    {
                        Directory.CreateDirectory(Filepath + @"Scene/0x" + SceneNumber.ToString("X2") + " - " + Helpers.MakeValidFileName(Name));
                        target = Filepath + @"Scene/0x" + SceneNumber.ToString("X2") + " - " + Helpers.MakeValidFileName(Name);
                    }
                    else
                    {
                        Directory.CreateDirectory(Filepath + @"rom/scene/0x" + SceneNumber.ToString("X2") + "-" + Helpers.MakeValidFileName(Name));
                        target = Filepath + @"rom/scene/0x" + SceneNumber.ToString("X2") + "-" + Helpers.MakeValidFileName(Name);
                    }
                }
                else
                {
                    try
                    {
                        if (zzrp == 1)
                            Directory.Move(target, Filepath + "Scene/" + SceneNumber + " - " + Helpers.MakeValidFileName(Name));
                        else if (zzrp == 2)
                            Directory.Move(target, Filepath + "Scene/0x" + SceneNumber.ToString("X2") + " - " + Helpers.MakeValidFileName(Name));
                        else
                            Directory.Move(target, Filepath + "rom/scene/0x" + SceneNumber.ToString("X2") + "-" + Helpers.MakeValidFileName(Name));
                    }
                    catch (Exception ex)
                    {
                    }
                    if (zzrp == 1)
                        target = Filepath + @"Scene/" + SceneNumber + " - " + Helpers.MakeValidFileName(Name);
                    else if (zzrp == 2)
                        target = Filepath + @"Scene/0x" + SceneNumber.ToString("X2") + " - " + Helpers.MakeValidFileName(Name);
                    else
                        target = Filepath + @"rom/scene/0x" + SceneNumber.ToString("X2") + "-" + Helpers.MakeValidFileName(Name);
                }
                Filepath = target + Path.DirectorySeparatorChar;

            }

            ConvertScene(ConsecutiveRoomInject, ForceRGBATextures,this, game);

#if DEBUG
            Console.WriteLine("scene count" + SceneData.Count.ToString("X"));
#endif
            foreach (ZSceneHeader sceneheader in SceneHeaders)
            {
                if (!sceneheader.SameAsPrevious) sceneheader.Scene.ConvertScene(ConsecutiveRoomInject, ForceRGBATextures, this, game);

            }

            // fix scene header offsets
            foreach (ZSceneHeader sceneheader in SceneHeaders)
            {
                if (sceneheader.SameAsPrevious)
                {
                    Helpers.Overwrite32(ref SceneData, sceneheader._InjectOffset, sceneheader.CloneFromHeader == 0 ? 0x02000008 : SceneHeaders[sceneheader.CloneFromHeader-1]._InjectOffsetValue);
                    for (int i = 0; i < Rooms.Count; i++)
                    {
                        Helpers.Overwrite32(ref _Rooms[i].RoomData, sceneheader._RoomInjectOffset[i], sceneheader.CloneFromHeader == 0 ? 0x03000008 : SceneHeaders[sceneheader.CloneFromHeader - 1]._RoomInjectOffsetValues[i]);
                    }
                }
            }

            for (int i = 0; i < _Rooms.Count; i++)
            {
                string SaveRoomTo = "";
                string extension = zzrp == 3 ? ".zroom" : ".zmap";

                if (zzrp > 0)
                    SaveRoomTo = Filepath + "room_" + i + extension;
                else if (MainForm.settings.Zmapoffsetnames)
                    SaveRoomTo = Filepath + (_Rooms[i].InjectOffset+_Rooms[i].FullDataLength).ToString("X8") + extension;
                else
                    SaveRoomTo = Filepath + Helpers.MakeValidFileName(Name) + "_room_" + i + extension;
#if DEBUG
                Console.WriteLine("SAVING DATA TO " + SaveRoomTo);
#endif 
                if (MainForm.IsFileLocked(SaveRoomTo))
                { 
                    MessageBox.Show("File " + SaveRoomTo + " is in use, try again later.", "Saving failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                File.Delete(SaveRoomTo);

                BinaryWriter BWR = new BinaryWriter(File.OpenWrite(SaveRoomTo));
                BWR.Write(_Rooms[i].RoomData.ToArray());
                BWR.Close();
            }
            string SaveSceneTo = "";
            if (zzrp > 0)
                SaveSceneTo = Filepath + "scene.zscene";
            else
                SaveSceneTo = Filepath + Helpers.MakeValidFileName(Name) + "_scene.zscene";
#if DEBUG
            Console.WriteLine("SAVING DATA TO " + SaveSceneTo);
#endif

            if (MainForm.IsFileLocked(SaveSceneTo))
            {
                MessageBox.Show("File " + SaveSceneTo + " is in use, try again later.", "Saving failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            File.Delete(SaveSceneTo);

            BinaryWriter BWS = new BinaryWriter(File.OpenWrite(SaveSceneTo));
            BWS.Write(SceneData.ToArray());
            BWS.Close();

            if (zzrp > 0)
            {
                uint restriction = RestrictionFlags;

                /*
                if (MainForm.GlobalROM != "")
                {
                    List<byte> byteList = new List<byte>((IEnumerable<byte>)File.ReadAllBytes(MainForm.GlobalROM));
                    ROM rom = MainForm.CheckVersion(byteList);
                    for (uint restrictionFlagStart = rom.RestrictionFlagStart; (long)restrictionFlagStart < (long)((int)rom.RestrictionFlagEnd - 1); restrictionFlagStart += 4U)
                    {
                        if ((int)byteList[(int)restrictionFlagStart] == this.SceneNumber)
                        {
                            restriction = Helpers.Read24(byteList, (int)restrictionFlagStart + 1);
                            break;
                        }
                    }
                }*/

                string filenameconf = (zzrp != 3) ? "conf.txt" : "config.toml";

                if (File.Exists(Filepath + filenameconf)) File.Delete(Filepath + filenameconf);

                StreamWriter sw = File.CreateText(Filepath + filenameconf);
                if (zzrp == 1)
                    sw.Write("unk-a: 0\r\nunk-b: 0\r\nshader: " + (MainForm.settings.command1AOoT ? 0x4 : SceneSettings) + "\r\nsave: " + SceneNumber + "\r\nrestrict: " + restriction);
                else if (zzrp == 2)
                    sw.Write("unk-a 0\r\nunk-b 0\r\nshader " + (MainForm.settings.command1AOoT ? 0x4 : SceneSettings) + "\r\nsave " + SceneNumber + "\r\nrestrict " + restriction);
                else
                {
                    string restrictiontext = "";
                    var dict = new Dictionary<string, uint>(){
                        { "\tbottles     = ",  ((((RestrictionFlags & 0x00FF0000) >> 16) & 0x03)) },
                        { "\ta_button    = ",  ((((RestrictionFlags & 0x00FF0000) >> 16) & 0x0C)) },
                        { "\tb_button    = ",  ((((RestrictionFlags & 0x00FF0000) >> 16) & 0x30)) },
                        { "\tunused      = ",  ((((RestrictionFlags & 0x00FF0000) >> 16) & 0xC0)) },
                        { "\twarp_song   = ",  ((((RestrictionFlags & 0x0000FF00) >> 8) & 0x03))  },
                        { "\tocarina     = ",  ((((RestrictionFlags & 0x0000FF00) >> 8) & 0x0C))  },
                        { "\thookshot    = ",  ((((RestrictionFlags & 0x0000FF00) >> 8) & 0x30))  },
                        { "\ttrade_item  = ",  ((((RestrictionFlags & 0x0000FF00) >> 8) & 0xC0))  },
                        { "\tother       = ",  ((((RestrictionFlags & 0x000000FF)) & 0x03))       },
                        { "\tdin_nayru   = ",  ((((RestrictionFlags & 0x000000FF)) & 0x0C))       },
                        { "\tfarores     = ",  ((((RestrictionFlags & 0x000000FF)) & 0x30))       },
                        { "\tsun_song    = ",  ((((RestrictionFlags & 0x000000FF)) & 0xC0))       },
                    };

                    foreach(var i in dict) {
                        restrictiontext += i.Key;
                        if (i.Value != 0) restrictiontext += "false\r\n";
                        else restrictiontext += "true\r\n";
                    }

                    sw.Write("# " + Name + "\r\n" + 
                        "draw_func_index = " + (MainForm.settings.command1AOoT ? 0x4 : SceneSettings) + "\r\n" + 
                        "[enables]\r\n" + restrictiontext);
                }
                sw.Close();
                //File.WriteAllText(Filepath + "conf.txt", "unk-a: 0\r\n   unk-b: 0\r\n  shader: " + SceneSettings + "\r\n    save: 1\r\nrestrict: 0");
                

            //title card

            if (TitleCard.Length > 0)
            {
                    MemoryStream ms = new MemoryStream(TitleCard);
                    Image returnImage = Image.FromStream(ms);
                    returnImage.Save(Filepath + "title.png", ImageFormat.Png);
            }


            }
            
        }

        #endregion

        #region ... Preview

        public void RegisterActorPreview(
            UInt16 key,
            UInt32 offset,
            string[] offsetsstr,
            string[] textureoffsetsstr,
            float scale,
            UInt32 dlistcount,
            bool animated,
            int hirearchy,
            string var,
            UInt16 animation,
            UInt16 Yoff,
            uint bank,
            string[] colors,
            string file) {
            
            UInt32[] offsets = new uint[offsetsstr.Length];
            UInt32[] textureoffsets = new uint[15];
            offset = offset | (bank << 24);


            for(int i = 0; i < offsetsstr.Length; i++)
                offsets[i] = Convert.ToUInt16(offsetsstr[i], 16 ) | (bank << 24);

            for (int i = 8; i < textureoffsetsstr.Length+8; i++)
                textureoffsets[i] = Convert.ToUInt32(textureoffsetsstr[i-8], 16) | (bank << 24);
            
            ZObjRender objrender = new ZObjRender(key,var,scale);

            objrender.Yoff = Yoff;

            List<byte> ClearBlock = new List<byte>(File.ReadAllBytes(file));

            UcodeSimulator.currentfilename = file;
            UcodeSimulator.TextureCache.Clear();
            Array.Copy(textureoffsets, UcodeSimulator.textureoffsets, textureoffsets.Length);
            SayakaGL.GameHandler.LoadToRAM(ClearBlock.ToArray(), (int) bank);

            if (animated)
            {
                byte[] zobj = ClearBlock.ToArray();
                uint limb_count = 0;
                int hirearchyfound = 0;
                int animationfound = 0;

                for (int i = 0; i < zobj.Length; i += 0x4)
                {
                    if (zobj[i] == bank && zobj[i + 5] == 0x00 && zobj[i + 6] == 0x00 && zobj[i + 7] == 0x00)
                    {
                        int check = Convert.ToInt32(string.Format("{0:X2}{1:X2}{2:X2}", zobj[i + 1], zobj[i + 2], zobj[i + 3]), 16);
                        int check_prev = Convert.ToInt32(string.Format("{0:X2}{1:X2}{2:X2}", zobj[i - 3], zobj[i - 2], zobj[i - 1]), 16);
                        if (check - check_prev != 0x0C && check - check_prev != 0x10)
                            continue;
                        if (++hirearchyfound != hirearchy)
                            continue;
                            
                        List<byte> zobjlist = new List<byte>(zobj);
                        limb_count = zobjlist[i+4];
                        offset = (uint) Helpers.Read24S(zobjlist, i + 1);
                        int animrotvaloffset = 0x00;
                        int animrotindexoffset = 0x00;

                        for (int ii = 0; ii < zobj.Length; ii += 4)
                        {
                            if (!(ii + 4 > zobj.GetUpperBound(0)))
                            {
                                if (zobj[ii + 2] == 0x00 && zobj[ii + 3] == 0x00 && zobj[ii + 4] == bank && zobj[ii + 8] == bank && zobj[ii + 14] == 0x00 && zobj[ii + 15] == 0x00)
                                {
                                    animationfound++;

                                    if (animationfound == animation || animationfound == 1)
                                    {
                                        animrotvaloffset = Helpers.Read24S(zobjlist, ii + 5);
                                        animrotindexoffset = Helpers.Read24S(zobjlist, ii + 9);

                                        if (animationfound == animation)
                                                break;
                                    }
                                    
                                }
                            }
                        }
                        
                        int counter = 0;
                        short[] prevlimb = new short[] { 0, 0, 0 };
                        while (counter != limb_count)
                        {
                            Limb limb = new Limb();
                            limb.DList = new List<UcodeSimulator.DisplayListStruct>();
                            int limboffset = Helpers.Read24S(new List<byte>(zobj), (int)(offset + 1));
                            limb.x = Helpers.Read16S(zobjlist, limboffset);
                            limb.y = Helpers.Read16S(zobjlist, limboffset + 2);
                            limb.z = Helpers.Read16S(zobjlist, limboffset + 4);
                            limb.firstchild = (sbyte)zobjlist[limboffset + 6];
                            limb.nextchild = (sbyte)zobjlist[limboffset + 7];
                            limb.rotation = new Vector3(0,0,0);
                            limb.rotation.X = Helpers.Read16(zobjlist, MainForm.Clamp(animrotvaloffset + 2 * (Helpers.Read16(zobjlist, (int)(animrotindexoffset + 6 + 6 * counter))), 0, zobjlist.Count - 2));
                            limb.rotation.Y = Helpers.Read16(zobjlist, MainForm.Clamp(animrotvaloffset + 2 * (Helpers.Read16(zobjlist, (int)(animrotindexoffset + 8 + 6 * counter))),0,zobjlist.Count-2));
                            limb.rotation.Z = Helpers.Read16(zobjlist, MainForm.Clamp(animrotvaloffset + 2 * (Helpers.Read16(zobjlist, (int)(animrotindexoffset + 10 + 6 * counter))), 0, zobjlist.Count - 2));

                            if ((uint)Helpers.Read24S(zobjlist, limboffset + 9) != 0x000000) //if limb has dlist
                            {
                                int target = counter;
                                while (true)
                                {
                                    target = objrender.Limbs.FindIndex(x => ((Limb)x).firstchild == target);
                                    if (target == -1) break;
                                    prevlimb[0] += objrender.Limbs[target].x;
                                    prevlimb[1] += objrender.Limbs[target].y;
                                    prevlimb[2] += objrender.Limbs[target].z;
                                    break;
                                }

                                UcodeSimulator.limbtransformations.Add(new short[] { (short) (limb.x + prevlimb[0]), (short)(limb.y + prevlimb[1]), (short)(limb.z + prevlimb[2]) });
                                prevlimb = new short[] { 0, 0, 0 };

                                SayakaGL.UcodeSimulator.ReadDL((int) bank, Helpers.Read32(zobjlist, limboffset + 8), ref limb.DList);
                                foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in limb.DList)
                                    SayakaGL.UcodeSimulator.ParseDL(DL);

                                SayakaGL.UcodeSimulator.ParseAllDLs(ref limb.DList);
                            }
                            else
                            {
                            //    prevlimb[0] += limb.x;
                            //     prevlimb[1] += limb.y;
                            //    prevlimb[2] += limb.z;
                            }

                            objrender.Limbs.Add(limb);

                            offset += 4;

                            counter++;

                        }

                        MainForm.zobj_cache.Add(objrender);

                        UcodeSimulator.limbtransformations.Clear();
                        UcodeSimulator.limbID = -1;
                    }
                }
            }


            if (!animated)
            {
                if (offsets.Length == 0)
                    while (dlistcount > 0)
                    {

                        // Console.WriteLine("offset: " + offset.ToString("X"));
                        offset = SayakaGL.UcodeSimulator.ReadDL((int) bank, offset, ref objrender.DLists);

                        foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in objrender.DLists)
                        {
                            SayakaGL.UcodeSimulator.ParseDL(DL);
            
                        }
                        dlistcount -= 1;
                    
                    }
                else
                    for (int i = 0; i < offsets.Length; i++)
                    {

                        // Console.WriteLine("offset: " + offset.ToString("X"));
                        SayakaGL.UcodeSimulator.ReadDL((int)bank, offsets[i], ref objrender.DLists);

                        foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in objrender.DLists)
                        {
                            SayakaGL.UcodeSimulator.ParseDL(DL);

                        }

                    }
                SayakaGL.UcodeSimulator.ParseAllDLs(ref objrender.DLists);

                MainForm.zobj_cache.Add(objrender);
            }
        }

        public void ConvertPreview(bool ConsecutiveRoomInject, bool ForceRGBATextures)
        {

            // Get the GameHandler stub and UcodeSimulator all ready
            SayakaGL.GameHandler.Initialize();
            SayakaGL.UcodeSimulator.Initialize(SayakaGL.Ucodes.F3DEX2);

            string game = (!MainForm.settings.MajorasMask) ? "OOT" : "MM";

            if (OriginalSceneData == null || OriginalSceneData.Count == 0)
            {
                MainForm.n64preview = true;

                if (cloneid > 0) { MainForm.NormalHeader.ConvertPreview(ConsecutiveRoomInject, ForceRGBATextures); return; }
                else ConvertScene(ConsecutiveRoomInject, ForceRGBATextures, this, game);
            }

            // Alright, convert the scene
            GL.PushAttrib(AttribMask.AllAttribBits);

            // Load the converted scene data into dummy memory
            if (OriginalSceneData != null && OriginalSceneData.Count != 0)
            {
                SayakaGL.GameHandler.LoadToRAM(MainForm.NormalHeader.OriginalSceneData.ToArray(), 0x02); 
            }
            else
            {
                SayakaGL.GameHandler.LoadToRAM(MainForm.NormalHeader.SceneData.ToArray(), 0x02);
            }

            // Go through the rooms...
            for (int i = 0; i < _Rooms.Count; i++)
            {
                // Make a DList list for the UcodeSimulator for each
                _Rooms[i].N64DLists = new List<SayakaGL.UcodeSimulator.DisplayListStruct>();

                // Load the room data into dummy memory

                List<uint> DLOffsets;

                if (_Rooms[i].OriginalRoomData != null && _Rooms[i].OriginalRoomData.Count != 0)
                {
                    SayakaGL.GameHandler.LoadToRAM(_Rooms[i].OriginalRoomData.ToArray(), 0x03);
                    DLOffsets = SayakaGL.GameHandler.GetDisplayLists((uint)(_Rooms[i].OriginalMeshHeaderOffset | (0x03 << 24)));

                }
                else
                {
                    SayakaGL.GameHandler.LoadToRAM(_Rooms[i].RoomData.ToArray(), 0x03);
                    DLOffsets = SayakaGL.GameHandler.GetDisplayLists((uint)(_Rooms[i].MeshHeaderOffset | (0x03 << 24)));
                }
                

                // Get the Display Lists offsets back from the mesh header and read each DList
                
                foreach (UInt32 DL in DLOffsets)
                {
                    SayakaGL.UcodeSimulator.ReadDL(0, DL, ref _Rooms[i].N64DLists);
                }
                // Finally parse all the DLists
                SayakaGL.UcodeSimulator.ParseAllDLs(ref _Rooms[i].N64DLists);

               
            }

            if (MainForm.skyboxdlists[0].Count == 0)
            {
                List<byte> skybox = new List<byte>(File.ReadAllBytes("F3DEX2" + Path.DirectorySeparatorChar + "skybox01.zobj"));

                UcodeSimulator.currentfilename = "skybox01.zobj";

                SayakaGL.GameHandler.LoadToRAM(skybox.ToArray(), 0x06);

                List<UcodeSimulator.DisplayListStruct> templist = new List<UcodeSimulator.DisplayListStruct>();

                uint asd = SayakaGL.UcodeSimulator.ReadDL((int)0x06, 0x06017400, ref templist);

                foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in templist)
                {
                    SayakaGL.UcodeSimulator.ParseDL(DL);
                }

                SayakaGL.UcodeSimulator.ParseAllDLs(ref templist);

                MainForm.skyboxdlists[0] = templist;

                UcodeSimulator.currentfilename = "";
            }
            if (MainForm.skyboxdlists[1].Count == 0)
            {
                List<byte> skybox = new List<byte>(File.ReadAllBytes("F3DEX2" + Path.DirectorySeparatorChar + "skybox01cloudy.zobj"));

                UcodeSimulator.currentfilename = "skybox01cloudy.zobj";

                SayakaGL.GameHandler.LoadToRAM(skybox.ToArray(), 0x06);

                List<UcodeSimulator.DisplayListStruct> templist = new List<UcodeSimulator.DisplayListStruct>();

                uint asd = SayakaGL.UcodeSimulator.ReadDL((int)0x06, 0x06017020, ref templist);

                foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in templist)
                {
                    SayakaGL.UcodeSimulator.ParseDL(DL);
                }

                SayakaGL.UcodeSimulator.ParseAllDLs(ref templist);
                MainForm.skyboxdlists[1] = templist;

                UcodeSimulator.currentfilename = "";

            }
            if (MainForm.skyboxdlists[2].Count == 0)
            {
                List<byte> skybox = new List<byte>(File.ReadAllBytes("F3DEX2" + Path.DirectorySeparatorChar + "skybox05.zobj"));

                UcodeSimulator.currentfilename = "skybox05.zobj";

                SayakaGL.GameHandler.LoadToRAM(skybox.ToArray(), 0x06);

                List<UcodeSimulator.DisplayListStruct> templist = new List<UcodeSimulator.DisplayListStruct>();

                uint asd = SayakaGL.UcodeSimulator.ReadDL((int)0x06, 0x0601B380, ref templist);

                foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in templist)
                {
                    SayakaGL.UcodeSimulator.ParseDL(DL);
                }

                SayakaGL.UcodeSimulator.ParseAllDLs(ref templist);
                MainForm.skyboxdlists[2] = templist;

                UcodeSimulator.currentfilename = "";
            }
            if (MainForm.skyboxdlists[3].Count == 0)
            {
                List<byte> skybox = new List<byte>(File.ReadAllBytes("F3DEX2" + Path.DirectorySeparatorChar + "skybox01MM.zobj"));

                UcodeSimulator.currentfilename = "skybox01MM.zobj";

                SayakaGL.GameHandler.LoadToRAM(skybox.ToArray(), 0x06);

                List<UcodeSimulator.DisplayListStruct> templist = new List<UcodeSimulator.DisplayListStruct>();

                uint asd = SayakaGL.UcodeSimulator.ReadDL((int)0x06, 0x06010B40, ref templist);

                foreach (SayakaGL.UcodeSimulator.DisplayListStruct DL in templist)
                {
                    SayakaGL.UcodeSimulator.ParseDL(DL);
                }

                SayakaGL.UcodeSimulator.ParseAllDLs(ref templist);
                MainForm.skyboxdlists[3] = templist;

                UcodeSimulator.currentfilename = "";
            }


            if (MainForm.zobj_cache.Count == 0 && MainForm.settings.RenderActors)
            {
                string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

                XmlNodeList nodes = XMLreader.getXMLNodes(gameprefix + "ActorRendering", "Actor");
                List<uint> z64romActors = new List<uint>();

                if (rom64.isSet())
                {
                    List<String> actors = rom64.getList("src\\actor");

                    foreach(var str in actors)
                    {
                        string basename = "";
                        ushort index = 0;

                        if (!rom64.getNameAndIndex(str, ref basename, ref index))
                            continue;
                        
                        var rompath = str.Replace("src\\", "rom\\");
                        uint objectid = rom64.getActorObjID(rompath);

                        if (File.Exists(str + "\\actor.toml")) {
                            TomlTable toml = rom64.parseToml(str + "\\actor.toml");

                            if (toml.HasKey("Render")){
                                TomlArray arr = toml["Render"].AsArray;
                                foreach (TomlNode node in arr.RawArray) {
                                    UInt16 key = index;
                                    float scale = node.HasKey("Scale") ? node["Scale"].AsFloat : 0.01f;
                                    bool animated = node.HasKey("Animation") ? true : false;
                                    UInt16 animation = (ushort)( node.HasKey("Animation") ? node["Animation"].AsInteger.Value : 0 );
                                    UInt16 yoff = (ushort)( node.HasKey("YOffset") ? node["YOffset"].AsInteger.Value : 0 );
                                    string var = node.HasKey("Regex") ? node["Regex"].AsString : "....";
                                    ushort bank = (ushort)( node.HasKey("Segment") ? node["Segment"].AsInteger.Value : 6 );
                                    string file = rom64.getItem("rom\\object", (int)objectid);
                                    string dl = node.HasKey("DisplayList") ? node["DisplayList"].AsString.ToString() : "";
                                    uint offset = 0;

                                    z64romActors.Add((uint)index);

                                    if (dl != "") {
                                        string object_ld = rom64.openFile("include\\z_object_user.ld");

                                        Match match = Regex.Match(object_ld, dl + "[^=]*=[^0]*(0x[0-9a-fA-F]*)");
                                        if (match.Value != "")
                                            offset = Convert.ToUInt32(match.Groups[1].Value.ToString(), 16);
                                    }

                                    if (file != "" && (offset != 0 || animated)) {
                                        Console.WriteLine("Register Actor Preview: " + file);
                                        Console.WriteLine("key:    " + index.ToString("X04"));
                                        Console.WriteLine("scale:  " + scale);
                                        Console.WriteLine("yoff:   " + yoff);
                                        Console.WriteLine("var:    " + var);
                                        Console.WriteLine("offset: " + offset.ToString("X08"));
                                        Console.WriteLine("bank:   " + bank.ToString("X02"));


                                        RegisterActorPreview(
                                            key, offset, new string[0], new string[0],
                                            scale, 1, animated, 
                                            1, var, animation, 
                                            yoff, bank, new string[0], file);
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;

                    UInt16 key = (ushort) ((nodeAtt["Key"] != null) ? Convert.ToUInt16(nodeAtt["Key"].Value,16) : 0x00);
                    bool skip = false;

                    foreach (uint index in z64romActors) {
                        if (key == index) {
                            skip = true;
                            break;
                        }
                    }

                    if (skip) continue;

                    UInt32 offset = (nodeAtt["Offset"] != null) ? Convert.ToUInt32(nodeAtt["Offset"].Value, 16) : 0x00;
                    string[] offsetsstr = (nodeAtt["Offsets"] != null) ? (nodeAtt["Offsets"].Value.Split(',')) : new string[0];
                    string[] textureoffsetsstr = (nodeAtt["TextureOffsets"] != null) ? (nodeAtt["TextureOffsets"].Value.Split(',')) : new string[0];
                    float scale = (nodeAtt["Scale"] != null) ? XmlConvert.ToSingle(nodeAtt["Scale"].Value) : 1.0f;
                    UInt32 dlistcount = (nodeAtt["DListCount"] != null) ? Convert.ToUInt32(nodeAtt["DListCount"].Value) : 1;
                    bool animated = (nodeAtt["Animated"] != null);
                    int hirearchy = (nodeAtt["Hirearchy"] != null) ? Convert.ToInt32(nodeAtt["Hirearchy"].Value) : 1;
                    string var = (nodeAtt["Var"] != null) ? Convert.ToString(nodeAtt["Var"].Value) : "....";
                    UInt16 animation = (ushort) ((nodeAtt["Animation"] != null) ? Convert.ToUInt16(nodeAtt["Animation"].Value) : 0);
                    UInt16 Yoff = (ushort)((nodeAtt["Yoff"] != null) ? Convert.ToUInt16(nodeAtt["Yoff"].Value) : 0);
                    string[] colors = (nodeAtt["Colors"] != null) ? (nodeAtt["Colors"].Value.Split(',')) : new string[0]; 
                    uint bank;
                    string file = "F3DEX2/" + node.InnerText + ".zobj";

                    if (node.InnerText == "gameplay_keep") bank = 0x04;
                    else if (node.InnerText == "gameplay_field_keep" || node.InnerText == "gameplay_dangeon_keep") bank = 0x05;
                    else bank = 0x06;
                    
                    RegisterActorPreview(
                        key, offset, offsetsstr, textureoffsetsstr,
                        scale, dlistcount, animated,
                        hirearchy, var, animation,
                        Yoff, bank, colors, file);
                }
            }

            UcodeSimulator.currentfilename = "";

            GL.PopAttrib();
        }

        #endregion

        #region ... Conversion

        public void ConvertScene(bool ConsecutiveRoomInject, bool ForceRGBATextures, ZScene MainHeader, string Game)
        {
            /* Check if collision model is valid */
            if (ColModel == null)
            {
                MessageBox.Show("There's no collision file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
#if DEBUG
            Console.WriteLine("converting header with cloneid : " + cloneid);
#endif
            textureoffsets.Clear();
            paletteoffsets.Clear();


            /* Create new scene file if its the main header */
            if (cloneid == 0) SceneData = new List<byte>();
            else SceneData = MainHeader.SceneData;
#if DEBUG
            Console.WriteLine("scene count" + SceneData.Count.ToString("X"));
#endif

            /* Write scene header */

            if (SceneHeaders.Count > 0)
            {
                CmdAlternateSceneHeader = SceneData.Count;
                Helpers.Append64(ref SceneData, 0x1800000000000000);                        /* Alternate Scene Headers */
            }
            else if (cloneid > 0)
            {
                MainHeader.SceneHeaders[MainHeader.SceneHeaders.FindIndex(x => x.Scene == this)]._InjectOffsetValue = (uint)(0x02000000 | SceneData.Count);
                Helpers.Overwrite32(ref SceneData, MainHeader.SceneHeaders[MainHeader.SceneHeaders.FindIndex(x => x.Scene == this)]._InjectOffset, (uint)(0x02000000 | SceneData.Count));
            }

            Helpers.Append64(ref SceneData, (ulong)(0x1500000000000000 | ((ulong)Reverb << 48) | ((ulong)NightSFX << 8) | (ulong)Music));       /* Sound settings */
            if (cloneid == 0)
            {
                CmdMapListOffset = SceneData.Count;
                Helpers.Append64(ref SceneData, 0x0400000000000000);                        /* Map list */
            }
            else
            {
                Helpers.Append64(ref SceneData, (ulong)(0x0400000002000000 | ((ulong)_Rooms.Count << 48) | (ulong) (MainHeader.RoomOffsets)));
            }
            if (Transitions.Count > 0)
            {
                CmdTransitionsOffset = SceneData.Count;
                Helpers.Append64(ref SceneData, 0x0E00000000000000);                        /* Transition list */
            }

            Helpers.Append64(ref SceneData, (ulong)(0x1900000000000000 | (ulong)CameraMovement << 48 | (ulong)WorldMap));  /* Camera movement, world map */
            if (cloneid == 0)
            { 
            CmdCollisionOffset = SceneData.Count;
            Helpers.Append64(ref SceneData, 0x0300000000000000);                        /* Collision header */

            CmdEntranceListOffset = SceneData.Count;
            Helpers.Append64(ref SceneData, 0x0600000000000000);                        /* Entrance index */
            }
            else //if its an alternate header, we reuse the collision and entrance list...
            {
                Helpers.Append64(ref SceneData, (ulong) (0x0300000002000000 | (MainHeader.CollisionOffset)));
                Helpers.Append64(ref SceneData, (ulong) (0x0600000002000000 | (MainHeader.EntranceOffset)));
            }


                                                                                        /* Special objects */
            Helpers.Append64(ref SceneData, (ulong)(0x0700000000000000 | ((ulong)ElfMessage << 48) | SpecialObject));

            if (Pathways.Count > 0)
            {

                CmdPathwayOffset = SceneData.Count;
                Helpers.Append64(ref SceneData, 0x0D00000000000000);                        /* Pathways */

            }

            CmdSpawnPointOffset = SceneData.Count;
            Helpers.Append64(ref SceneData, 0x0000000000000000);                        /* Spawn point list */

            if (Game == "OOT")
                Helpers.Append64(ref SceneData, (ulong)(0x1100000000000000 | ((ulong)SkyboxType << 24) | ((ulong)(Cloudy ? 1 : 0) << 16) | ((ulong)(OutdoorLight ? 0 : 1) << 8)));                         /* Skybox / lighting settings */
            else
                Helpers.Append64(ref SceneData, (ulong)(0x1100000000000000 | ((ulong)((SkyboxType > 0) ? 1 : 0) << 24) | (ulong)SkyboxType << 16) |  ((ulong)(OutdoorLight ? 0 : 1) << 8));                         /* Skybox / lighting settings */
            if (ExitList.Count > 0)
            {
                CmdExitListOffset = SceneData.Count;
                Helpers.Append64(ref SceneData, 0x1300000000000000);                        /* Exit list */
            }
            CmdEnvironmentsOffset = SceneData.Count;
            Helpers.Append64(ref SceneData, 0x0F00000000000000);                        /* Environments */
            

            if (Cutscene.Count > 0)
            {
                CmdCutsceneOffset = SceneData.Count;
                if (Game != "MM")
                    Helpers.Append64(ref SceneData, 0x1700000000000000);                        /* Cutscene */
                else
                {
                    Helpers.Append64(ref SceneData, (ulong) (0x1701000002000000 | (SceneData.Count + 8)));                       

                }
            }

            if (Game == "MM")
            {
                MMCmdCameraOffset = SceneData.Count;
                Helpers.Append64(ref SceneData, 0x1B0E000000000000);                        /* MM Cameras */

                Helpers.Append64(ref SceneData, 0x0200000000000000 | ((ulong)Cameras.Count << 48));

                MMCmdMinimapOffset = SceneData.Count;
               Helpers.Append64(ref SceneData, 0x1C00000000000000);                        /* MM Minimaps */


            }

            if (Game == "MM" || (MainForm.settings.command1AOoT && SegmentFunctions.FindIndex(x => x.Functions.Count > 0) != -1) || (MainForm.settings.command1AOoT && cloneid > 0 && inherittextureanims))
            {
                MMCmdTextureAnimOffset = SceneData.Count;
                if ((cloneid > 0 && inherittextureanims))
                    Helpers.Append64(ref SceneData, (ulong) (0x1A00000000000000 | MainHeader.MainHeaderTextureAnimOffset));
                else
                    Helpers.Append64(ref SceneData, 0x1A00000000000000);                        /* MM Texture anims */
            }

           
            Helpers.Append64(ref SceneData, 0x1400000000000000);                        /* End marker */

            //empty space
            for(int i = 0; i < MainForm.settings.EmptySpace; i++)
            {
                SceneData.Add(0);
            }

            AddPadding(ref SceneData, 0x10);

            int endofsceneheader = SceneData.Count;

            if (cloneid == 0)
            {
                for (int i = 0; i < Rooms.Count; i++) Helpers.Append64(ref SceneData, 0x0000000000000000);
            }

            //alternate scene header list
            if (SceneHeaders.Count > 0)
            {
                Helpers.Overwrite32(ref SceneData, CmdAlternateSceneHeader + 4, (uint)(0x02000000 | SceneData.Count));
                foreach (ZSceneHeader SceneHeader in SceneHeaders)
                {
                    SceneHeader._InjectOffset = SceneData.Count;
                    Helpers.Append32(ref SceneData, 0x00000000); //placeholder
                }
            }
            AddPadding(ref SceneData, 8);

            if (Transitions.Count > 0)
            {
                /* ... transition list ... */
                Helpers.Overwrite32(ref SceneData, CmdTransitionsOffset, (uint)(0x0E000000 | (Transitions.Count << 16)));
                Helpers.Overwrite32(ref SceneData, CmdTransitionsOffset + 4, (uint)(0x02000000 | SceneData.Count));
                foreach (ZActor Trans in Transitions)
                {

                    SceneData.Add(Trans.FrontSwitchTo);
                    SceneData.Add(Trans.FrontCamera);
                    SceneData.Add(Trans.BackSwitchTo);
                    SceneData.Add(Trans.BackCamera);
                    Helpers.Append16(ref SceneData, Trans.Number);
                    Helpers.Append16(ref SceneData, (ushort)Trans.XPos);
                    Helpers.Append16(ref SceneData, (ushort)Trans.YPos);
                    Helpers.Append16(ref SceneData, (ushort)Trans.ZPos);
                    if (Game == "OOT")
                        Helpers.Append16(ref SceneData, (ushort)Trans.YRot);
                    else
                    {
                        short Yrot = (short)Math.Round(Trans.YRot / 182.0444f);
                        if (Yrot < 0) Yrot += 360;
                        Helpers.Append16(ref SceneData, (ushort)(0 | (Yrot << 7))); //0x1B command here in some future
                    }
                    Helpers.Append16(ref SceneData, Trans.Variable);
                }
                AddPadding(ref SceneData, 8);
            }

            /* ... exit list ... */
            if (!MainForm.settings.EnableNewExitFormat && ExitList.Count > 0)
            {
                Helpers.Overwrite32(ref SceneData, CmdExitListOffset + 4, (uint)(0x02000000 | SceneData.Count));
                foreach (ZUShort Exit in ExitList)
                {
                    Helpers.Append16(ref SceneData, Exit.Value);
                }
                AddPadding(ref SceneData, 8);
            }
            else if (MainForm.settings.EnableNewExitFormat && ExitListV2.Count > 0)
            {
                Helpers.Overwrite32(ref SceneData, CmdExitListOffset + 4, (uint)(0x02000000 | SceneData.Count));
                foreach (ZExit Exit in ExitListV2)
                {
                    Helpers.Append32(ref SceneData, Exit.Raw + (uint)0x80000000);
                }
                AddPadding(ref SceneData, 8);
            }

            /* ... spawn point list ... */
            Helpers.Overwrite32(ref SceneData, CmdSpawnPointOffset, (uint)(0x00000000 | (SpawnPoints.Count << 16)));
            Helpers.Overwrite32(ref SceneData, CmdSpawnPointOffset + 4, (uint)(0x02000000 | SceneData.Count));
            foreach (ZActor Spawn in SpawnPoints)
            {
                Helpers.Append16(ref SceneData, Spawn.Number);
                Helpers.Append16(ref SceneData, (ushort)Spawn.XPos);
                Helpers.Append16(ref SceneData, (ushort)Spawn.YPos);
                Helpers.Append16(ref SceneData, (ushort)Spawn.ZPos);
                Helpers.Append16(ref SceneData, (ushort)Spawn.XRot);
                if (Game == "OOT")
                    Helpers.Append16(ref SceneData, (ushort)Spawn.YRot);
                else
                {
                    short Yrot = (short)Math.Round(Spawn.YRot / 182.0444f);
                    if (Yrot < 0) Yrot += 360;
                    Helpers.Append16(ref SceneData, (ushort)(0 | (Yrot << 7))); //0x1B command here in some future
                }
                Helpers.Append16(ref SceneData, (ushort)Spawn.ZRot);
                Helpers.Append16(ref SceneData, Spawn.Variable);
            }
            AddPadding(ref SceneData, 8);

            /* ... environments ... */
            Helpers.Overwrite32(ref SceneData, CmdEnvironmentsOffset, (uint)(0x0F000000 | (Environments.Count << 16)));
            Helpers.Overwrite32(ref SceneData, CmdEnvironmentsOffset + 4, (uint)(0x02000000 | SceneData.Count));
            foreach (ZEnvironment Env in Environments)
            {
                Helpers.Append48(ref SceneData, (ulong)(Env.C1C.ToArgb() & 0xFFFFFF));
                Helpers.Append48(ref SceneData, (ulong)(Env.C2C.ToArgb() & 0xFFFFFF));
                Helpers.Append48(ref SceneData, (ulong)(Env.C3C.ToArgb() & 0xFFFFFF));
                Helpers.Append48(ref SceneData, (ulong)(Env.C4C.ToArgb() & 0xFFFFFF));
                Helpers.Append48(ref SceneData, (ulong)(Env.C5C.ToArgb() & 0xFFFFFF));
                Helpers.Append48(ref SceneData, (ulong)(Env.FogColorC.ToArgb() & 0xFFFFFF));
                Helpers.Append16(ref SceneData, (ushort) (Env.FogDistance | (Env.FogUnknown << 10)));
                Helpers.Append16(ref SceneData, Env.DrawDistance);

               // Console.WriteLine((Env.FogDistance | (Env.FogUnknown << 10)).ToString("X4"));
            }
            AddPadding(ref SceneData, 8);


            /* ... Pathways ... */
            if (Pathways.Count > 0)
            {

                //Helpers.Overwrite32(ref SceneData, CmdPathwayOffset, (uint)(0x0D000000 | (Pathways.Count << 16)));
                Helpers.Overwrite32(ref SceneData, CmdPathwayOffset + 4, (uint)(0x02000000 | SceneData.Count));
                int pointoffset = -1;
                int incr = 0, incr2 = 0;

                foreach (ZPathway Path in Pathways)
                {

                    Helpers.Append32(ref SceneData, (uint)(0x00000000 | (uint)Path.Points.Count << 24));
                    Helpers.Append32(ref SceneData, (uint)(0x02000000 | (uint)SceneData.Count + 4 + ((Pathways.Count - 1 - incr2) * 8) + (incr * 6)));
                    incr += Path.Points.Count;
                    incr2++;
                }
                foreach (ZPathway Path in Pathways)
                {
                    foreach (Vector3 point in Path.Points)
                    {
                        Helpers.Append16(ref SceneData, (ushort)point.X);
                        Helpers.Append16(ref SceneData, (ushort)point.Y);
                        Helpers.Append16(ref SceneData, (ushort)point.Z);
                    }
                }
                AddPadding(ref SceneData, 8);
            }

            /* ... entrance list ... */
            if (cloneid == 0)
            {
                Helpers.Overwrite32(ref SceneData, CmdEntranceListOffset + 4, (uint)(0x02000000 | SceneData.Count));
                EntranceOffset = SceneData.Count;
                ushort entrancenum = 0;
                foreach (ZActor Spawn in SpawnPoints)
                {
                    Vector3 pos = new Vector3(Spawn.XPos, Spawn.YPos + 1, Spawn.ZPos);
                    float dist = 999999;
                    ushort roomid = 0;

                    if (Spawn.SpawnRoom != 0xFF)
                    {
                        roomid = Spawn.SpawnRoom;
                    }
                    else
                    {

                        ushort roomcount = 0;
                        foreach (ZRoom Room in MainForm.CurrentScene.Rooms)
                        {
                            foreach (ObjFile.Group Group in Room.TrueGroups)
                            {
                                if (Group.Name.ToLower().Contains("#nocollision")) continue;

                                foreach (ObjFile.Triangle Tri in Group.Triangles)
                                {
                                    Vector3 collision = ObjFile.RayCollision(
                                        Room.ObjModel.Vertices[Tri.VertIndex[0]],
                                        Room.ObjModel.Vertices[Tri.VertIndex[1]],
                                        Room.ObjModel.Vertices[Tri.VertIndex[2]],
                                        pos,
                                        new Vector3(pos.X, pos.Y - 30000, pos.Z),
                                        MainForm.CurrentScene.Scale);
                                    if (collision != new Vector3(-1, -1, -1) && dist > MainForm.Distance3D(pos, collision))
                                    {
                                        dist = MainForm.Distance3D(pos, collision);
                                        roomid = roomcount;
                                    }
                                }
                            }
                            roomcount++;
                        }
                    }
                    Helpers.Append16(ref SceneData, (ushort)(0x0000 | (entrancenum << 8) | roomid));
                    entrancenum++;
                }
                AddPadding(ref SceneData, 8);
            }

            /* ... Cutscene ... */
            
            if (Cutscene.Count > 0)
            {
                Helpers.Overwrite32(ref SceneData, CmdCutsceneOffset + 4, (uint)(0x02000000 | SceneData.Count));

                if (Game == "OOT")
                {
                    if (MainForm.settings.printoffsets) MainForm.InjectMessages.Add("Cutscene offset: " + ((uint)(0x02000000 | SceneData.Count)).ToString("X8"));
                    cachecutsceneoffset = (uint)(0x02000000 | SceneData.Count);
                }

                else if (Game == "MM")
                {
                    Helpers.Append32(ref SceneData, (uint)(0x02000000 | (SceneData.Count + 8)));
                    Helpers.Append32(ref SceneData, (uint)(0x00000000 | (CutsceneEntrance << 16) | (CutsceneEntranceNum << 8) | (CutsceneFlag)));
                    if (MainForm.settings.printoffsets) MainForm.InjectMessages.Add("Cutscene offset: " + ((uint)(0x02000000 | SceneData.Count)).ToString("X8"));
                    cachecutsceneoffset = (uint)(0x02000000 | SceneData.Count);

                    //Helpers.Overwrite32(ref SceneData, CmdCutsceneOffset - 8 + 4, (uint)(0x02000000 | SceneData.Count));
                }

                GenerateCutsceneData(SceneData, Game);

               
            }

            if (Game == "MM")
            {
                Helpers.Overwrite32(ref SceneData, MMCmdCameraOffset + 4, (uint)(0x02000000 | SceneData.Count));
                /*
                Helpers.Append64(ref SceneData, 0x02BCFFFFFFFDFFFF);
                Helpers.Append64(ref SceneData, 0x000100FF0000001B);
                Helpers.Append64(ref SceneData, 0x0258FFFFFFFEFFFF);
                Helpers.Append64(ref SceneData, 0x000200FF0000001B);
                Helpers.Append64(ref SceneData, 0x02BCFFFFFFFCFFFF);
                Helpers.Append64(ref SceneData, 0x000300FF0000011B);
                Helpers.Append64(ref SceneData, 0x02BCFFFFFFFBFFFF);
                Helpers.Append64(ref SceneData, 0x000400FF0000001B);
                Helpers.Append64(ref SceneData, 0x01F4FFFFFFF9FFFF);
                Helpers.Append64(ref SceneData, 0x000500FF00000020);
                Helpers.Append64(ref SceneData, 0x0190FFFFFFF5FFFF);
                Helpers.Append64(ref SceneData, 0x000600FF00000120);
                Helpers.Append64(ref SceneData, 0x0064FFFFFFF8FFFF);
                Helpers.Append64(ref SceneData, 0x000700FF00000020);
                Helpers.Append64(ref SceneData, 0x00C8FFFFFFF7FFFF);
                Helpers.Append64(ref SceneData, 0x000800FF00000020);
                Helpers.Append64(ref SceneData, 0x0320FFFFFFFAFFFF);
                Helpers.Append64(ref SceneData, 0x000900FF00000020);
                Helpers.Append64(ref SceneData, 0x0384FFFFFFF0FFFF);
                Helpers.Append64(ref SceneData, 0xFFFF00FF00000120);
                */
                Helpers.Append64(ref SceneData, 0x02BCFFFFFFFDFFFF);
                Helpers.Append64(ref SceneData, 0x000100FF0000001B);
                Helpers.Append64(ref SceneData, 0x0258FFFFFFFEFFFF);
                Helpers.Append64(ref SceneData, 0x000200FF0000001B);
                Helpers.Append64(ref SceneData, 0x02BCFFFFFFFCFFFF);
                Helpers.Append64(ref SceneData, 0x000300FF0000001B);
                Helpers.Append64(ref SceneData, 0x02BCFFFFFFFBFFFF);
                Helpers.Append64(ref SceneData, 0x000400FF0000001B);
                Helpers.Append64(ref SceneData, 0x01F4FFFFFFF9FFFF);
                Helpers.Append64(ref SceneData, 0x000500FF00000020);
                Helpers.Append64(ref SceneData, 0x0190FFFFFFF5FFFF);
                Helpers.Append64(ref SceneData, 0x000600FF00000120);
                Helpers.Append64(ref SceneData, 0x0064FFFFFFF8FFFF);
                Helpers.Append64(ref SceneData, 0x000700FF00000020);
                Helpers.Append64(ref SceneData, 0x00C8FFFFFFF7FFFF);
                Helpers.Append64(ref SceneData, 0x000800FF00000020);
                Helpers.Append64(ref SceneData, 0x0320FFFFFFFAFFFF);
                Helpers.Append64(ref SceneData, 0xFFFF00FF00000020);
                Helpers.Append64(ref SceneData, 0x0352FFFF0000FFFF);
                Helpers.Append64(ref SceneData, 0x000A00FF00000120);
                Helpers.Append64(ref SceneData, 0x035CFFFF0001FFFF);
                Helpers.Append64(ref SceneData, 0xFFFF00FF00000120);
                Helpers.Append64(ref SceneData, 0x000B0087FFF6FFFF);
                Helpers.Append64(ref SceneData, 0xFFFF000100000120);
                Helpers.Append64(ref SceneData, 0x0384003C0002FFFF);
                Helpers.Append64(ref SceneData, 0xFFFF00FF00000020);
                Helpers.Append64(ref SceneData, 0xFFFFFFFF0003FFFF);
                Helpers.Append64(ref SceneData, 0xFFFF00FF00000020);
                AddPadding(ref SceneData, 8);
                

                Helpers.Overwrite32(ref SceneData, MMCmdMinimapOffset + 4, (uint)(0x02000000 | SceneData.Count));
                Helpers.Append32(ref SceneData, (uint)(0x02000000 | SceneData.Count + 8));
                Helpers.Append32(ref SceneData, 0xFFFF0000);
                Helpers.Append32(ref SceneData, 0x00000000);
                Helpers.Append16(ref SceneData, 0x0000);
                AddPadding(ref SceneData, 8);


            }

            if (cloneid == 0)
            {
                /* ... collision */
                WriteSceneCollision(SceneData, 0x02, false, Game);
            }


            AddPadding(ref SceneData, 0x10);



            /* Process rooms... */
            for (int i = 0; i < _Rooms.Count; i++)
            {

                /* Get current room from list */
                ZRoom Room = _Rooms[i];

                RoomIndex = i;

                /* Create new room file, DList offset list and texture list */
                if (cloneid == 0)
                    Room.RoomData = new List<byte>();
                else
                {
                    Room.RoomData = MainHeader.Rooms[i].RoomData;
                    CmdMeshHeaderOffset = MainHeader.Rooms[i].MeshHeaderOffset;
                }
                Room.DLists = new List<NDisplayList>();
                Textures = new List<NTexture>();

                /* Create room header */
                WriteRoomHeader(Room, MainHeader);

                /* Write objects */
                if (Room.ZObjects.Count != 0)
                {
                    ObjectOffset = Room.RoomData.Count;
                    foreach (ZUShort Obj in Room.ZObjects)
                        Helpers.Append16(ref Room.RoomData, Obj.Value);
                    AddPadding(ref Room.RoomData, 8);
                }

                /* Write actors */
                if (Room.ZActors.Count != 0)
                {
                    ActorOffset = Room.RoomData.Count;
                    foreach (ZActor Actor in Room.ZActors)
                    {
                        if (Game == "OOT" || MainForm.settings.IgnoreMMDaySystem == false)
                        {
                            Helpers.Append16(ref Room.RoomData, Actor.Number);
                            Helpers.Append16(ref Room.RoomData, (ushort)Actor.XPos);
                            Helpers.Append16(ref Room.RoomData, (ushort)Actor.YPos);
                            Helpers.Append16(ref Room.RoomData, (ushort)Actor.ZPos);
                            Helpers.Append16(ref Room.RoomData, (ushort)Actor.XRot);
                            Helpers.Append16(ref Room.RoomData, (ushort)Actor.YRot);
                            Helpers.Append16(ref Room.RoomData, (ushort)Actor.ZRot);
                            Helpers.Append16(ref Room.RoomData, Actor.Variable);
                        }
                        else
                        {
                            short Xrot = (short) Math.Round(Actor.XRot / 182.0444f);
                            if (Xrot < 0) Xrot += 360;
                            short Yrot = (short)Math.Round(Actor.YRot / 182.0444f);
                            if (Yrot < 0) Yrot += 360;
                            short Zrot = (short)Math.Round(Actor.ZRot / 182.0444f);
                            if (Zrot < 0) Zrot += 360;
                            Helpers.Append16(ref Room.RoomData, Actor.Number);
                            Helpers.Append16(ref Room.RoomData, (ushort)Actor.XPos);
                            Helpers.Append16(ref Room.RoomData, (ushort)Actor.YPos);
                            Helpers.Append16(ref Room.RoomData, (ushort)Actor.ZPos);
                            Helpers.Append16(ref Room.RoomData, (ushort) (0x7 | (Xrot << 7))); // upper bits of time flags
                            Helpers.Append16(ref Room.RoomData, (ushort)(0 | (Yrot << 7))); //0x1B command here in some future
                            Helpers.Append16(ref Room.RoomData, (ushort)(0x7F | (Zrot << 7))); // lower bits of time flags
                            Helpers.Append16(ref Room.RoomData, Actor.Variable);
                        }
                    }
                    AddPadding(ref Room.RoomData, 8);
                }


                /* Write additional lights */

                List<ZAdditionalLight> tempAdditionalLights = new List<ZAdditionalLight>();
                tempAdditionalLights.AddRange(Room.AdditionalLights.FindAll(x => x.ColorC != Color.Black || x.Radius != 0 || !x.PointLight));

                if (tempAdditionalLights.Count != 0)
                {
                    ExtraLightOffset = Room.RoomData.Count;
                    foreach (ZAdditionalLight light in tempAdditionalLights)
                    {

                        if (!light.PointLight)
                        {
                            Helpers.Append16(ref Room.RoomData, 0x0100);
                            Helpers.Append16(ref Room.RoomData, (ushort)(0x0000 | (ushort)light.NSdirection << 8));
                            Helpers.Append16(ref Room.RoomData, (ushort)(light.ColorC.R | (ushort)light.EWdirection << 8));
                            Helpers.Append16(ref Room.RoomData, (ushort)(light.ColorC.B | (ushort)light.ColorC.G << 8));
                            Helpers.Append16(ref Room.RoomData, 0x0000);
                            Helpers.Append16(ref Room.RoomData, 0x0000);
                            Helpers.Append16(ref Room.RoomData, 0x0000);
                        }
                        else
                        {
                            Helpers.Append16(ref Room.RoomData, 0x0000);
                            Helpers.Append16(ref Room.RoomData, (ushort)light.XPos);
                            Helpers.Append16(ref Room.RoomData, (ushort)light.YPos);
                            Helpers.Append16(ref Room.RoomData, (ushort)light.ZPos);
                            Helpers.Append16(ref Room.RoomData, (ushort)(light.ColorC.G | (ushort)light.ColorC.R << 8));
                            Helpers.Append16(ref Room.RoomData, (ushort)(0x0000 | (ushort)light.ColorC.B << 8));
                            Helpers.Append16(ref Room.RoomData, (ushort)(0x0000 | (ushort)light.Radius << 8));
                        }
                    }
                    AddPadding(ref Room.RoomData, 8);
                }

                if (cloneid == 0)
                    GenerateTextures(Room, SceneData, Textures);

                // OOT texture anims

                if (Game == "OOT" && MainForm.settings.command1AOoT && SegmentFunctions.FindIndex(x => x.Functions.Count > 0) != -1 && !(cloneid > 0 && inherittextureanims))
                {
                    if (cloneid == 0) MainHeaderTextureAnimOffset = (uint)(0x02000000 | SceneData.Count);

                    Helpers.Overwrite32(ref SceneData, MMCmdTextureAnimOffset + 4, (uint)(0x02000000 | SceneData.Count));

                    List<uint> updateoffsets = new List<uint>();

                    int incr = 0, incr2 = 0, segmentid = 0;

                    int lastbyteoffset = 0;

                    foreach (ZSegmentFunction Segment in SegmentFunctions)
                    {
                        incr2 = 0;

                        if (Segment.Functions.Count == 0) { segmentid++; continue; }

                        foreach (ZTextureAnim TextureAnim in Segment.Functions)
                        {

                            if (TextureAnim.Type == ZTextureAnim.scroll)
                            {
                                if (TextureAnim.FlagType == 0xFF && TextureAnim.XVelocity2 == 0 && TextureAnim.YVelocity2 == 0)
                                {
                                    TextureAnim.TempType = 0x00; // single layer no flag
                                }
                                else if (TextureAnim.FlagType == 0xFF)
                                {
                                    TextureAnim.TempType = 0x01; // double layer no flag
                                }
                                else
                                {
                                    TextureAnim.TempType = 0x08; // double layer with flag
                                }
                            }
                            else if (TextureAnim.Type == ZTextureAnim.texswap)
                            {
                                TextureAnim.TempType = 0x07; // pointer changes based on flag
                            }
                            else if (TextureAnim.Type == ZTextureAnim.texframe)
                            {
                                bool sameframeduration = false;
                                int firstduration = 0;

                                if (TextureAnim.TextureSwapList.Count == 0)
                                {
                                    //TextureAnim.Type = 0xFF; // invalid
                                    continue;
                                }
                                else
                                {
                                    firstduration = TextureAnim.TextureSwapList[0].Duration;
                                    sameframeduration = TextureAnim.TextureSwapList.FindIndex(x => x.Duration != firstduration) == -1;
                                }


                                if (sameframeduration && TextureAnim.FlagType == 0xFF)
                                {
                                    TextureAnim.TempType = 0x0B; // frame 1 pointer list no flag
                                }
                                else if (sameframeduration && TextureAnim.FlagType != 0xFF)
                                {
                                    TextureAnim.TempType = 0x0C; // frame 1 pointer list with flag 
                                }
                                else if (!sameframeduration && TextureAnim.FlagType == 0xFF)
                                {
                                    TextureAnim.TempType = 0x0D; // frame variable pointer list no flag 
                                }
                                else if (!sameframeduration && TextureAnim.FlagType != 0xFF)
                                {
                                    TextureAnim.TempType = 0x0E; // frame variable pointer list with flag 
                                }
                            }
                            else if (TextureAnim.Type == ZTextureAnim.blending)
                            {
                                if (TextureAnim.FlagType == 0xFF)
                                {
                                    TextureAnim.TempType = 0x09; // color blend no flag
                                }
                                else
                                {
                                    TextureAnim.TempType = 0x0A; // color blend flag
                                }
                            }
                            else if (TextureAnim.Type == ZTextureAnim.camera)
                            {
                                TextureAnim.TempType = 0x0F;
                            }
                            else if (TextureAnim.Type == ZTextureAnim.condition)
                            {
                                TextureAnim.TempType = 0x10;
                            }

                            lastbyteoffset = SceneData.Count;
                            Helpers.Append32(ref SceneData, (uint)((uint)(0x00000000 | (uint)(segmentid + 1) << 24) | TextureAnim.TempType << 0));
                            updateoffsets.Add((uint)SceneData.Count);
                            Helpers.Append32(ref SceneData, (uint)(0x02000000));
                            // incr += 8;
                            incr2++;
                        }



                        segmentid++;
                    }

                    SceneData[lastbyteoffset] = (byte)-SceneData[lastbyteoffset];
                    incr2 = 0;

                    foreach (ZSegmentFunction Segment in SegmentFunctions)
                    {
                        if (Segment.Functions.Count == 0) { segmentid++; continue; }

                        foreach (ZTextureAnim TextureAnim in Segment.Functions)
                        {
                            if (incr2 >= updateoffsets.Count) continue;

                            Helpers.Overwrite32(ref SceneData, (int)updateoffsets[incr2], (uint)(0x02000000 | SceneData.Count));

                            if (TextureAnim.TempType == 0x00 || TextureAnim.TempType == 0x01 || TextureAnim.TempType == 0x08)
                            {
                                Helpers.Append32(ref SceneData, (uint)((uint)TextureAnim.Height1 | (byte)TextureAnim.XVelocity1 << 24 | (byte)TextureAnim.YVelocity1 << 16 | (uint)TextureAnim.Width1 << 8));

                                if (TextureAnim.TempType > 0) Helpers.Append32(ref SceneData, (uint)((uint)TextureAnim.Height2 | (byte)TextureAnim.XVelocity2 << 24 | (byte)TextureAnim.YVelocity2 << 16 | (uint)TextureAnim.Width2 << 8));


                            }

                            if (TextureAnim.TempType == 0x07)
                            {
                                if (TextureAnim.TextureSwap != null && TextureAnim.TextureSwap2 != null)
                                {
                                    Helpers.Append32(ref SceneData, (uint)(0x02000000 | Textures.Find(x => x.Name == TextureAnim.TextureSwap).TexOffset)); // TODO flag not set
                                    Helpers.Append32(ref SceneData, (uint)(0x02000000 | Textures.Find(x => x.Name == TextureAnim.TextureSwap2).TexOffset)); // TODO flag set
                                }
                                else
                                {
                                    Helpers.Append32(ref SceneData, (uint)(0x02000000));
                                    Helpers.Append32(ref SceneData, (uint)(0x02000000));
                                }
                            }


                            if (TextureAnim.TempType == 0x08 || TextureAnim.TempType == 0x07 || TextureAnim.TempType == 0x0C || TextureAnim.TempType == 0x0E || TextureAnim.TempType == 0x0A || TextureAnim.TempType == 0x0F || TextureAnim.TempType == 0x10) // flag stuff
                            {
                                Helpers.Append32(ref SceneData, TextureAnim.FlagValue);
                                Helpers.Append32(ref SceneData, TextureAnim.FlagBitwise);
                                SceneData.Add(TextureAnim.FlagType);
                                if (TextureAnim.FlagReverse)
                                    SceneData.Add(0);
                                else
                                {
                                    if (TextureAnim.FlagType == 0x00 || TextureAnim.FlagType == 0x01 || TextureAnim.FlagType == 0x04 || TextureAnim.FlagType == 0x05)
                                        SceneData.Add((byte)(TextureAnim.FlagValue < 0x20 ? (1 << (byte)TextureAnim.FlagValue) : (1 << (byte)TextureAnim.FlagValue - 0x20)));
                                    else
                                    {
                                        SceneData.Add(1); // could give issues with other types
                                    }
                                }
                                Helpers.Append16(ref SceneData, 0);
                                Helpers.Append16(ref SceneData, (ushort)(TextureAnim.Freeze ? 1 : 0));
                                Helpers.Append16(ref SceneData, 0);
                            }



                            if (TextureAnim.TempType == 0x0B || TextureAnim.TempType == 0x0C)
                            {
                                int totalframes = TextureAnim.TextureSwapList[0].Duration * TextureAnim.TextureSwapList.Count;

                                Helpers.Append16(ref SceneData, (ushort)totalframes);
                                Helpers.Append16(ref SceneData, 0);
                                Helpers.Append16(ref SceneData, TextureAnim.TextureSwapList[0].Duration);
                                Helpers.Append16(ref SceneData, 0);

                                foreach (ZTextureAnimImage tex in TextureAnim.TextureSwapList)
                                {
                                    Helpers.Append32(ref SceneData, (uint)(0x02000000 | Textures.Find(x => x.Name == tex.Texture).TexOffset));
                                }

                            }

                            if (TextureAnim.TempType == 0x0D || TextureAnim.TempType == 0x0E)
                            {

                                Helpers.Append16(ref SceneData, 0);
                                Helpers.Append16(ref SceneData, 0);
                                Helpers.Append16(ref SceneData, (ushort)TextureAnim.TextureSwapList.Count);
                                foreach (ZTextureAnimImage tex in TextureAnim.TextureSwapList)
                                {
                                    Helpers.Append16(ref SceneData, tex.Duration);
                                }
                                if (TextureAnim.TextureSwapList.Count % 2 != 0)
                                    Helpers.Append16(ref SceneData, 0);

                                foreach (ZTextureAnimImage tex in TextureAnim.TextureSwapList)
                                {
                                    Helpers.Append32(ref SceneData, (uint)(0x02000000 | Textures.Find(x => x.Name == tex.Texture).TexOffset));
                                }

                            }

                            if (TextureAnim.TempType == 0x09 || TextureAnim.TempType == 0x0A)
                            {
                                SceneData.Add(1); // interpolate prim color
                                SceneData.Add(0);

                                ushort duration = 0;

                                foreach (ZTextureAnimColor color in TextureAnim.ColorList)
                                {
                                    duration += color.Duration;
                                }


                                Helpers.Append16(ref SceneData, duration);

                                int cnt = 0;
                                foreach (ZTextureAnimColor color in TextureAnim.ColorList)
                                {
                                    cnt++;
                                    // Console.WriteLine(" a " + (color.C1C.ToArgb() << 8).ToString("X8"));

                                    Helpers.Append32(ref SceneData, (uint)(color.C1C.ToArgb() << 8) + color.C1C.A);
                                    Helpers.Append32(ref SceneData, (uint)(color.C1C.ToArgb() << 8) + color.C1C.A);
                                    Helpers.Append16(ref SceneData, 0);
                                    Helpers.Append16(ref SceneData, color.Duration);
                                }
                                Helpers.Append32(ref SceneData, 0);
                                Helpers.Append32(ref SceneData, 0);
                                Helpers.Append32(ref SceneData, 0);

                            }

                            if (TextureAnim.TempType == 0x0F)
                            {
                                SceneData.Add(TextureAnim.CameraEffect);
                                SceneData.Add(0);
                                Helpers.Append16(ref SceneData, 0); //padding
                            }

                            incr2++;
                        }


                    }





                    AddPadding(ref SceneData, 8);

                }


                /* Prepare dummy mesh header */
                if (cloneid == 0)
                {
                    if (!Prerendered)
                    {
                        MeshHeaderOffset = Room.MeshHeaderOffset = Room.RoomData.Count;
                        Helpers.Append32(ref Room.RoomData, 0);  /* Mesh type X, Y meshes */
                        Helpers.Append32(ref Room.RoomData, 0);  /* Start address */
                        Helpers.Append32(ref Room.RoomData, 0);  /* End address */
                    }
                    else 
                    {
                        MeshHeaderOffset = Room.MeshHeaderOffset = Room.RoomData.Count;
                       

                        if (prerenderimages.Count == 1) //PRERENDERED Single Background
                        {
                            PrerenderedFixOffset = MeshHeaderOffset + 32;
                            Helpers.Append32(ref Room.RoomData, 0x01010000);  /* Mesh type 01, single background */
                            Helpers.Append32(ref Room.RoomData, (uint)(0x03000000 | (MeshHeaderOffset + 32))); //dlist to draw
                            Helpers.Append32(ref Room.RoomData, 0x03000000); // background location
                            Helpers.Append32(ref Room.RoomData, 0x00000000); //unknown
                            Helpers.Append32(ref Room.RoomData, 0x00000000); //unknown
                            Helpers.Append16(ref Room.RoomData, 0x0140); //width (320)
                            Helpers.Append16(ref Room.RoomData, 0x00F0); //height (240)
                            Helpers.Append16(ref Room.RoomData, 0x0002); //image fmt? image size?
                            Helpers.Append16(ref Room.RoomData, 0x0000); //image pal?
                            Helpers.Append16(ref Room.RoomData, 0x0000); //image flip?
                            Helpers.Append16(ref Room.RoomData, 0x0000); //padding
                            Helpers.Append32(ref Room.RoomData, 0x00000000);  /* Start address */
                            Helpers.Append32(ref Room.RoomData, 0x00000000);  /* End address */
                        }
                        else //PRERENDERED MULTI
                        {
                            Helpers.Append32(ref Room.RoomData, 0x01020000);  /* Mesh type 01, single background */
                            //Console.WriteLine("Dlist to draw location: " + Room.RoomData.Count.ToString("X8"));
                            Helpers.Append32(ref Room.RoomData, (uint)(0x03000000 | (MeshHeaderOffset + 16 + (28*prerenderimages.Count)))); //dlist to draw
                            Helpers.Append32(ref Room.RoomData, (uint)(0 | (prerenderimages.Count << 24)));
                            Helpers.Append32(ref Room.RoomData, (uint) (0x03000000 | (Room.RoomData.Count + 4)));  /* Start of background array */

                            PrerenderedFixOffset = MeshHeaderOffset + 32;

                            for (int p = 0; p < prerenderimages.Count; p++)
                            {
                                Helpers.Append32(ref Room.RoomData, (uint) (0x00820000 | (p << 8)));  /* 0x0082, background id */
                               // Console.WriteLine("Background location: " + Room.RoomData.Count.ToString("X8"));
                                Helpers.Append32(ref Room.RoomData, 0x03000000); // background location
                                Helpers.Append32(ref Room.RoomData, 0x00000000); //unknown
                                Helpers.Append32(ref Room.RoomData, 0x00000000); //unknown
                                Helpers.Append16(ref Room.RoomData, 0x0140); //width (320)
                                Helpers.Append16(ref Room.RoomData, 0x00F0); //height (240)
                                Helpers.Append16(ref Room.RoomData, 0x0002); //image fmt? image size?
                                Helpers.Append16(ref Room.RoomData, 0x0000); //image pal?
                                Helpers.Append16(ref Room.RoomData, 0x0000); //image flip?
                                Helpers.Append16(ref Room.RoomData, 0x0000); //padding
                            }

                            Helpers.Append32(ref Room.RoomData, 0x00000000); //overwrite dlist to draw

                        }
                    }

                    //prerendering variables
                    List<byte> DListData = new List<byte>();
                    List<int> DListOffset = new List<int>();
                    List<int> DListSize = new List<int>();


                    if (!PregeneratedMesh) // normal mode
                    {

                    for (int j = 0; j < Room.TrueGroups.Count; j++)
                    {
                        Helpers.Append64(ref Room.RoomData, 0);
                        Helpers.Append32(ref Room.RoomData, 0);  /* Display List offset 1 */
                        Helpers.Append32(ref Room.RoomData, 0);  /* Display List offset 2 */
                    }
                    AddPadding(ref Room.RoomData, 8);



                        if (Game == "MM")
                        {
                            if (TextureAnims.Count > 0)
                            {
                                Helpers.Overwrite32(ref SceneData, MMCmdTextureAnimOffset + 4, (uint)(0x02000000 | SceneData.Count));

                                int incr = 0, incr2 = 0;

                                foreach (ZTextureAnim TextureAnim in TextureAnims)
                                {

                                    Helpers.Append32(ref SceneData, (uint)(0x00000000 | (uint)(incr2 == TextureAnims.Count - 1 ? -(incr2 + 1) : incr2 + 1) << 24) | (uint)(TextureAnim.UseSecondLayer ? 1 : 0) << 0);
                                    Helpers.Append32(ref SceneData, (uint)(0x02000000 | (uint)SceneData.Count + 4 + ((TextureAnims.Count - 1 - incr2) * 8) + (incr)));
                                    incr += 8;
                                    incr2++;
                                }
                                // Helpers.Append32(ref SceneData, (uint)(0x00000000 | (uint)-TextureAnims.Count << 24));
                                // Helpers.Append32(ref SceneData, (uint)(0x02000000 | (uint)SceneData.Count + 4 + ((TextureAnims.Count - 1 - incr2) * 8) + (incr) + 8));
                                foreach (ZTextureAnim TextureAnim in TextureAnims)
                                {
                                    Helpers.Append32(ref SceneData, (uint)((uint)TextureAnim.Height1 | (byte)TextureAnim.XVelocity1 << 24 | (byte)TextureAnim.YVelocity1 << 16 | (uint)TextureAnim.Width1 << 8));
                                    Helpers.Append32(ref SceneData, (uint)((uint)TextureAnim.Height2 | (byte)TextureAnim.XVelocity2 << 24 | (byte)TextureAnim.YVelocity2 << 16 | (uint)TextureAnim.Width2 << 8));
                                }
                                // Helpers.Append32(ref SceneData, 0xFF002020);
                                //Helpers.Append32(ref SceneData, 0x00002020);
                                AddPadding(ref SceneData, 8);
                            }
                        }


                    Room.LODgroups.Clear();
                    Room.LODdlists.Clear();

                    /* Create Display Lists */
                    for (int j = 0; j < Room.TrueGroups.Count; j++)
                    {

                        if (Room.TrueGroups[j].Name.ToLower().Contains("#nomesh") || Room.TrueGroups[j].Name.Contains("TAG_NoMesh")) continue;

                        if (Room.TrueGroups[j].Animated && MainForm.settings.command1AOoT && !MainForm.settings.MajorasMask && (MainForm.CurrentScene.SegmentFunctions[Room.TrueGroups[j].AnimationBank - 8].Functions.Count == 0) && !MainForm.n64preview)
                        {
                                MessageBox.Show("Animation segment " + Room.TrueGroups[j].AnimationBank + " is empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        NDisplayList DList = new NDisplayList(Scale, Room.TrueGroups[j].TintAlpha, Room.TrueGroups[j].MultiTexAlpha, 1.0f, (Room.TrueGroups[j].ReverseLight) ? !OutdoorLight : OutdoorLight, Room.TrueGroups[j].BackfaceCulling, Room.TrueGroups[j].Animated, Room.TrueGroups[j].Metallic, Room.TrueGroups[j].Decal, Room.TrueGroups[j].Pixelated, Room.TrueGroups[j].Billboard, Room.TrueGroups[j].TwoAxisBillboard, Room.TrueGroups[j].IgnoreFog, Room.TrueGroups[j].SmoothRGBAEdges, Room.TrueGroups[j].EnvColor, Room.TrueGroups[j].AlphaMask, Room.TrueGroups[j].RenderLast, Room.TrueGroups[j].VertexNormals, Room.AffectedByPointLight, Room.TrueGroups[j].AnimationBank, 3);
                        DList.Convert(Room.ObjModel, Room.TrueGroups[j], Textures, (!Prerendered) ? (uint)Room.RoomData.Count : (uint)(Room.RoomData.Count + DListData.Count), SceneSettings, AdditionalTextures);
                        if (DList.Data != null)
                        {
                            //if (Prerendered && j != Room.TrueGroups.Count -1) Array.Resize(ref DList.Data,DList.Data.Length-8);
                            if (!Prerendered)
                                Room.RoomData.AddRange(DList.Data);
                            else
                            {
                                DListOffset.Add((int)(DList.Offset));
                                DListSize.Add((int)(DList.Data.Length));
                                DListData.AddRange(DList.Data);
                            }
                        }

                        if (Room.TrueGroups[j].LOD)
                        {
                            if (Room.LODgroups.FindIndex(x => x.id == Room.TrueGroups[j].LodGroup) == -1)
                            {
                                Room.LODgroups.Add(new LODgroup(Room.TrueGroups[j].LodGroup));
                            }
                            Room.LODgroups.Find(x => x.id == Room.TrueGroups[j].LodGroup).dlists.Add(new LODgroupDlist(DList, Room.TrueGroups[j].LodDistance));
                            Room.LODdlists.Add(DList);

                        }

                        Room.DLists.Add(DList);
                    }
                   
                    foreach(LODgroup group in Room.LODgroups)
                    {
                     //   Console.WriteLine("lod group offset: " + Room.RoomData.Count.ToString("X8"));
                        group.lodgroupoffset = (uint) (Room.RoomData.Count + 16);
                        List<byte> lodgroupdata = new List<byte>();
                        ushort midX = (ushort)(group.dlists[0].dlist.MinCoordinate.X + ((group.dlists[0].dlist.MaxCoordinate.X - group.dlists[0].dlist.MinCoordinate.X) / 2));
                        ushort midY = (ushort)((group.dlists[0].dlist.MinCoordinate.Y + ((group.dlists[0].dlist.MaxCoordinate.Y - group.dlists[0].dlist.MinCoordinate.Y) / 2)));
                        ushort midZ = (ushort)(group.dlists[0].dlist.MinCoordinate.Z + ((group.dlists[0].dlist.MaxCoordinate.Z - group.dlists[0].dlist.MinCoordinate.Z) / 2));
                        Helpers.Append16(ref lodgroupdata, midX);
                        Helpers.Append16(ref lodgroupdata, midY);
                        Helpers.Append16(ref lodgroupdata, midZ);
                        Helpers.Append16(ref lodgroupdata, 0);
                        Helpers.Append64(ref lodgroupdata, 0); // padding
                        Helpers.Append64(ref lodgroupdata,  (0x0100100203000000 | (ulong)(Room.RoomData.Count)));

                        group.dlists = new List<LODgroupDlist>(@group.dlists.OrderBy(x => x.distance));

                        foreach (LODgroupDlist dlist in group.dlists)
                        {
                            Helpers.Append64(ref lodgroupdata, (0xE100000003000000 | (ulong)(dlist.dlist.Offset)));
                            Helpers.Append64(ref lodgroupdata, (0x0400000000000000 | (ulong)(dlist.distance)));

                        }

                        Helpers.Append64(ref lodgroupdata, (0xDF00000000000000 ));

                        Room.RoomData.AddRange(lodgroupdata);

                    }


                    }

                    else //pregenerated mode
                    {

                        List<uint> DLOffsets;

                        _Rooms[i].N64DLists = new List<SayakaGL.UcodeSimulator.DisplayListStruct>();

                        SayakaGL.GameHandler.LoadToRAM(_Rooms[i].OriginalRoomData.ToArray(), 0x03);
                        DLOffsets = SayakaGL.GameHandler.GetDisplayLists((uint)(_Rooms[i].OriginalMeshHeaderOffset | (0x03 << 24)));

                        foreach (UInt32 DL in DLOffsets)
                        {
                            SayakaGL.UcodeSimulator.ReadDL(0, DL, ref _Rooms[i].N64DLists);
                            
                        }

                        //we make room for the display lists
                        for (int j = 0; j < _Rooms[i].N64DLists.Count; j++)
                        {
                            Helpers.Append64(ref Room.RoomData, 0);
                            Helpers.Append32(ref Room.RoomData, 0);  /* Display List offset 1 */
                            Helpers.Append32(ref Room.RoomData, 0);  /* Display List offset 2 */
                        }
                        AddPadding(ref Room.RoomData, 8);

                      //  uint oldoffset = _Rooms[i].N64DLists[0].Commands[0].Address & 0x00FFFFFF; //first command of first dlist to determine start

                        Room.oldoffset = 0xFFFFFFFF;

                        for (int ii = 0; ii < Room.N64DLists.Count; ii++)
                            for (int y = 0; y < Room.N64DLists[ii].Commands.Count; y++)
                            {
                                if (new byte[] { 01, 04, 0xDA, 0xDC, 0xDE, 0xFE, 0xFF }.Contains(Room.N64DLists[ii].Commands[y].ID))
                                {
                                    uint tmp = Room.N64DLists[ii].Commands[y].w1 & 0x00FFFFFF;
                                    if (tmp != 0 && tmp < Room.oldoffset)
                                    {
                                        Room.oldoffset = tmp;
                                    }
                                }
                            }


                        Room.newoffset = (uint) Room.RoomData.Count;

                     //  uint lastoffset = _Rooms[i].N64DLists[0].Commands[0].Address+8 & 0x00FFFFFF; //first command of first dlist to determine start

                        uint lastoffset = 0;

                        for (int ii = 0; ii < Room.N64DLists.Count; ii++)
                            for (int y = 0; y < Room.N64DLists[ii].Commands.Count; y++)
                            {
                         
                                    uint tmp = Room.N64DLists[ii].Commands[y].Address & 0x00FFFFFF;
                                    if (tmp > lastoffset)
                                    {
                                        lastoffset = tmp;
                                    }
                                
                            }

                        lastoffset += 8;

                  //      Console.WriteLine("oldoffset " + Room.oldoffset.ToString("X8"));
                   //     Console.WriteLine("lastoffset " + lastoffset.ToString("X8"));
                   //     Console.WriteLine("newoffset " + Room.newoffset.ToString("X8"));


                        Room.RoomData.AddRange(_Rooms[i].OriginalRoomData.GetRange((int)Room.oldoffset, (int)(lastoffset-Room.oldoffset)));


                        foreach (UcodeSimulator.DisplayListStruct DL in _Rooms[i].N64DLists)
                        {
                            /*
                             * 01: w1
                             * 04: w1
                             * DA: w1
                 
                             * DC: w1
                             * DE: w1
                             
                             * FD: w1 (texture offset)
                             * FE FF: w1 (is this event used?)
                             *
                             *
                             */

                            DListOffset.Add((int)(Room.newoffset+DListData.Count));
                            DListSize.Add((int)(DL.Commands.Count*8)); //each command is worth 8 bytes?

                            int currentcommand = 0;


                            foreach (UcodeSimulator.DLCommandStruct command in DL.Commands)
                            {
                                if (new byte[]{01,04,0xDA,0xDC,0xDE,0xFE,0xFF}.Contains(command.ID))
                                {
                                    int VSegment = (int)(command.w1 >> 24);
                                    uint VOffset = (command.w1 & 0x00FFFFFF);

                                    VOffset = VOffset - Room.oldoffset + Room.newoffset;

                                    int oldcommandoffset = (int)((DL.StartAddress + currentcommand * 8 - Room.oldoffset + Room.newoffset) & 0x00FFFFFF);


                                    if (VSegment == 0x02 || VSegment == 0x03)
                                    {

                                      //  Helpers.Append32(ref DListData, command.w0);
                                       // Helpers.Append32(ref DListData, (uint)(VOffset | (VSegment << 24)));

                                        Helpers.Overwrite32(ref Room.RoomData, oldcommandoffset, command.w0);
                                        Helpers.Overwrite32(ref Room.RoomData, oldcommandoffset + 4, (uint)(VOffset | (VSegment << 24)));

                            //            Console.WriteLine("Overwritten command " + command.ID.ToString("X2") + " at " + (oldcommandoffset + 4).ToString("X8") + " DLstartaddress " + (DL.StartAddress + currentcommand * 8).ToString("X8") + " Voffset " + ((VOffset | (VSegment << 24)).ToString("X8")));
                                    }
                                    else
                                    {
                                        //  Helpers.Append32(ref DListData, command.w0);
                                        //  Helpers.Append32(ref DListData, command.w1);
                                    //    Helpers.Overwrite32(ref Room.RoomData, (int)(DL.StartAddress - Room.oldoffset + Room.newoffset), command.w0);
                                      //  Helpers.Overwrite32(ref Room.RoomData, (int)(DL.StartAddress - Room.oldoffset + Room.newoffset) +4, command.w1);
                                    }


                                }
                                else if (command.ID == 0xFD)
                                {
                                    int VSegment = (int)(command.w1 >> 24);
                                    uint VOffset = (command.w1 & 0x00FFFFFF);


                                    int oldcommandoffset = (int)((DL.StartAddress + currentcommand * 8 - Room.oldoffset + Room.newoffset) & 0x00FFFFFF);

                                    if (VSegment == 0x02 || VSegment == 0x03) // non dynamic texture
                                    {
                                       // Console.WriteLine(UcodeSimulator.NGraphics.Textures.Length);
                                        //TODO do something with textures

                        
                                        Helpers.Overwrite32(ref Room.RoomData, oldcommandoffset + 4, (uint)(0 | (VSegment << 24)));
                                    }

                               //    Helpers.Append32(ref DListData, command.w0);
                            //       Helpers.Append32(ref DListData, command.w1);
                                }
                                else
                                {
                             //       Helpers.Append32(ref DListData, command.w0);
                          //          Helpers.Append32(ref DListData, command.w1);
                                }

                                currentcommand++;
                           
                            }
                        }

                        // Room.DLists.Add();
                        //Dlist.Data


                        // we add the dlist data to the room
                        //Room.RoomData.AddRange(DListData);
                      

                    }


                /* Fix room header and add missing data */
                FixRoomHeader(Room);

                if (Prerendered) //dlist jump table
                {
                 //   int fix = Helpers.Read24S(Room.RoomData, PrerenderedFixOffset + 1);
                    uint fix2 = (uint) Room.RoomData.Count;


                    Room.RoomData.AddRange(DListData);

                    for (int z = 0; z < DListOffset.Count; z++)
                    {
                        //Console.WriteLine(" Pointers[z]: " + Pointers[z].ToString("X"));
                        Helpers.Append64(ref Room.RoomData, 0xDE00000003000000 | (ulong)(DListOffset[z]));
                        if (z == DListOffset.Count-1) Helpers.Append64(ref Room.RoomData, 0xDF00000000000000);
                        
                    }



                    if (prerenderimages.Count == 1)
                    {
                          //  Console.WriteLine("Overwrite Dlist to draw: " + (PrerenderedFixOffset).ToString("X8"));
                            Helpers.Overwrite32(ref Room.RoomData, PrerenderedFixOffset, (uint)(0x03000000 | fix2 + DListData.Count));

                            Helpers.Overwrite32(ref Room.RoomData, PrerenderedFixOffset-24, (uint)(0x03000000 | Room.RoomData.Count));
                            Room.RoomData.AddRange(File.ReadAllBytes(prerenderimages[0]));
                            AddPadding(ref Room.RoomData, 10);
                    }
                    else if (prerenderimages.Count > 1)
                    {
                           // Console.WriteLine("Overwrite Dlist to draw: " + (PrerenderedFixOffset - 16 + (28*prerenderimages.Count)).ToString("X8"));
                            Helpers.Overwrite32(ref Room.RoomData, (PrerenderedFixOffset - 16 + (28 * prerenderimages.Count)), (uint)(0x03000000 | fix2 + DListData.Count));


                            for (int p = 0; p < prerenderimages.Count; p++)
                            {
                              //  Console.WriteLine("Overwrite offset: " + (PrerenderedFixOffset - 20 + (28 * p) + 8).ToString("X8"));
                                Helpers.Overwrite32(ref Room.RoomData, PrerenderedFixOffset - 20 + (28*p) + 8, (uint)(0x03000000 | Room.RoomData.Count));
                                int jpgsize = Room.RoomData.Count;
                                Room.RoomData.AddRange(File.ReadAllBytes(prerenderimages[p]));
                                AddPadding(ref Room.RoomData, 0x10);
                                jpgsize = Room.RoomData.Count - jpgsize;
                                Room.RoomData.AddRange(new byte[0x25800 - jpgsize]);
                            }
                    }
                }

                }
                else
                {
                    FixRoomHeader(Room);
                }

                // Console.WriteLine("offset" + Helpers.Read24S(Room.RoomData,PrerenderedFixOffset+1).ToString("X"));

                /* Add some padding for good measure */
                AddPadding(ref Room.RoomData, 0x10);

                /* Store room data length */
                Room.FullDataLength = Room.RoomData.ToArray().Length;

                /* Put modified room info back into list */
                _Rooms[i] = Room;
            }

            /* Fix scene header; map list ... */
            if (cloneid == 0)
            {
                Helpers.Overwrite32(ref SceneData, CmdMapListOffset, (uint)(0x04000000 | (_Rooms.Count << 16)));
                Helpers.Overwrite32(ref SceneData, CmdMapListOffset + 4, (uint)(0x02000000 | endofsceneheader));
                RoomOffsets = endofsceneheader;
            }

            if (_Rooms.Count < 1) return;

            AddPadding(ref SceneData, 0x10);
            AddPadding(ref SceneData, 8);

            int RoomInjectOffset = MainHeader._Rooms[0].InjectOffset;
            RoomInjectOffset = InjectOffset + SceneData.Count;
            int counter = 0;
            foreach (ZRoom Room in MainHeader._Rooms)
            {
                Room.FullDataLength = Room.RoomData.ToArray().Length;
#if DEBUG
                Console.WriteLine("inject offset: " + RoomInjectOffset.ToString("X"));
                Console.WriteLine("full data length: " + Room.FullDataLength.ToString("X"));
#endif
                Helpers.Overwrite32(ref SceneData, MainHeader.RoomOffsets + counter, (uint)RoomInjectOffset);
                Helpers.Overwrite32(ref SceneData, MainHeader.RoomOffsets + 4 + counter, (uint)(RoomInjectOffset + Room.FullDataLength));
                RoomInjectOffset += Room.FullDataLength;
                counter += 8;
            }
            

            
            // AddPadding(ref SceneData, 8);

        }

        public void GenerateTextures(ZRoom Room, List<Byte> Data, List<NTexture> Textures)
        {
            /* Create textures */

            List<ObjFile.Material> MaterialsList = new List<ObjFile.Material>();
            MaterialsList.AddRange(Room.ObjModel.Materials);
            MaterialsList.AddRange(AdditionalTextures);


            for (int M = 0; M < MaterialsList.Count; M++)
            {

                /* Get material & force RGBA default */
                ObjFile.Material Mat = MaterialsList[M];
                Mat.ForceRGBA = MainForm.settings.ForceRGBATextures;

                string name = Mat.DisplayName.ToLower();

                if (name.Contains("#forcergba") || name.Contains("#rgba16"))
                    Mat.ForceRGBA = true;

                else if (name.Contains("#rgba32"))
                    Mat.ForcedFormat = "RGBA32";

                else if (name.Contains("#ci4"))
                    Mat.ForcedFormat = "CI4";

                else if (name.Contains("#ci8"))
                    Mat.ForcedFormat = "CI8";

                else if (name.Contains("#i4"))
                    Mat.ForcedFormat = "I4";

                else if (name.Contains("#i8"))
                    Mat.ForcedFormat = "I8";

                else if (name.Contains("#ia8"))
                    Mat.ForcedFormat = "IA8";

                else if (name.Contains("#ia8"))
                    Mat.ForcedFormat = "IA16";

                bool appears = false;
                //check if material is used first....

                if (name.Contains("#special"))
                    appears = true;
                else
                {
                    for (int x = 0; x < Room.TrueGroups.Count; x++)
                    {
                        if (Room.TrueGroups[x].MultiTexMaterial == M) { appears = true; }
                        for (int y = 0; y < Room.TrueGroups[x].Triangles.Count; y++)
                        {
                            if (Room.TrueGroups[x].Triangles[y].MaterialName == Mat.Name)
                            {
                                appears = true;
                            }
                        }
                    }
                    foreach (ZSegmentFunction Segment in SegmentFunctions)
                    {
                        foreach (ZTextureAnim TextureAnim in Segment.Functions)
                        {
                            if (TextureAnim.TextureSwap == Mat.Name)
                                appears = true;

                            if (TextureAnim.TextureSwap2 == Mat.Name)
                                appears = true;

                            foreach(ZTextureAnimImage TextureAnimImg in TextureAnim.TextureSwapList)
                            {
                                if (TextureAnimImg.Texture == Mat.Name)
                                    appears = true;
                            }
                        }
                    }
                }

                // Console.WriteLine("mat: " + Mat.Name);

                if (!appears)
                {
                    Textures.Add(new NTexture());
                    continue;
                }

                if (!MainForm.settings.DontConvertMultitexture)
                {
                    // ---VERY kludgy RGBA forcing code--- 
                    for (int x = 0; x < Room.TrueGroups.Count; x++)
                    {

                        // If group has multitex material number... 
                        if (Room.TrueGroups[x].MultiTexMaterial != -1)
                        {
                            // Turn force RGBA ON for multitex material 

                            MaterialsList[Room.TrueGroups[x].MultiTexMaterial].ForceRGBA = true;


                            // Scan group's triangles for current material name... 

                            for (int y = 0; y < Room.TrueGroups[x].Triangles.Count; y++)
                            {
                                if (Room.TrueGroups[x].Triangles[y].MaterialName == Mat.Name)
                                {
                                    //Change material name to RGBA

                                    Mat.ForceRGBA = true;
                                    goto Cont;
                                }
                            }
                        }
                    }
                }

                /* Continue here... */
                Cont:
                if (Mat.TexImage != null)
                {
                    /* Create new texture, convert current material */
                    NTexture Texture = new NTexture();
                    Texture.Convert(Mat);

                    //string filename = Mat.map_Kd.Split(Path.AltDirectorySeparatorChar).Last();
                   // filename = filename.Split(Path.DirectorySeparatorChar).Last(); //TODO

                    string filename = Mat.map_Kd;


                    Texture.Name = filename;

                    if (textureoffsets.ContainsKey(filename))
                    {
                        Texture.TexOffset = (uint)textureoffsets[filename];
#if DEBUG
                        Console.WriteLine("Matching texture: " + filename + " " + String.Format("" + (uint)textureoffsets[filename], 'X'));
#endif
                    }
                    else
                    {
                        /* Add current offset to texture offset list */
                        Texture.TexOffset = ((uint)Data.Count);

                        textureoffsets.Add(filename, Data.Count);
#if DEBUG
                        Console.WriteLine("Writting texture offset: " + filename + " " + String.Format("" + Data.Count, 'X'));
#endif
                        /* Write converted data to room file */
                        Data.AddRange(Texture.Data);


                    }


                    /* See if we've got a CI-format texture... */
                    int Format = ((Texture.Type & 0xE0) >> 5);
#if DEBUG
                    Console.WriteLine("Texture format N64: " + Format.ToString("X2"));
#endif
                    if (Format == GBI.G_IM_FMT_CI)
                    {
                        Texture.PalOffset = 0;
                        /* OLD palette sharing
                        foreach (KeyValuePair<byte[], int> palette in paletteoffsets)
                        {
                            if (Texture.Palette.OrderBy(a => a).SequenceEqual(palette.Key.OrderBy(a => a)))
                            {
                                Texture.PalOffset = (uint)palette.Value;
                                Console.WriteLine("Matching palette: " + String.Format("" + (uint)palette.Value, 'X'));
                                break;
                            }
                        }*/
                        if (Texture.PalOffset == 0)
                        {
                            /* If it's CI, add current offset to palette offset list */

                            Texture.PalOffset = ((uint)Data.Count);

                            paletteoffsets.Add(Texture.Palette, Data.Count);
#if DEBUG
                            Console.WriteLine("Writting palette offset: " + String.Format("" + Data.Count, 'X'));
#endif
                            /* Write palette data to room file */
                            Data.AddRange(Texture.Palette);
                        }


                    }
                    else
                    {
                        /* Add dummy entry to palette offset list */
                        Texture.PalOffset = Dummy;
                    }

                    Textures.Add(Texture);
                }
                else
                {
                    
                    Textures.Add(new NTexture());
                }
            }
        }

        public void GenerateCutsceneData(List<Byte> data, string Game)
        {
            int totalframes = 0;
            Helpers.Append32(ref data, (uint)Cutscene.Count + (uint)Cutscene.FindAll(x => x.Marker == 0x01).Count);
            int totalframesoffset = data.Count;
            Helpers.Append32(ref data, 0xFEFEFEFE);
            List<ZCutscene> orderedcutscene = new List<ZCutscene>();
            List<ZCutscene> backupcutscene = new List<ZCutscene>();

            int cameradataheader = 0; // MM stuff

            if (Game == "MM")
            {
                //reordering camera commands
                orderedcutscene = new List<ZCutscene>();
                Cutscene.OrderBy(x => x.StartFrame);
                foreach (ZCutscene cutscene in Cutscene)
                {
                    if (cutscene.Marker == 0x5A)
                    {
                        orderedcutscene.Add(cutscene);
                    }
                }
                foreach (ZCutscene cutscene in Cutscene)
                {
                    if (cutscene.Marker != 0x5A)
                    {
                        orderedcutscene.Add(cutscene);
                    }
                }
                backupcutscene = Cutscene;
                Cutscene = orderedcutscene;
            }

            foreach (ZCutscene cutscene in Cutscene)
            {
                // if (cutscene.Marker != 0x13) totalframes += cutscene.GetTotalFrames();
                if (cutscene.StartFrame + cutscene.GetTotalFrames() > totalframes) totalframes = cutscene.StartFrame + cutscene.GetTotalFrames();
                if (Game == "OOT" && cutscene.Marker == 0x01 || Game == "OOT" && cutscene.Marker == 0x05) //position
                {
                    Helpers.Append32(ref data, (uint)cutscene.Marker);

                    Helpers.Append16(ref data, 0x0000); //unknown w
                    Helpers.Append16(ref data, cutscene.StartFrame);
                    Helpers.Append16(ref data, (ushort)(cutscene.StartFrame + cutscene.GetTotalFrames()));
                    Helpers.Append16(ref data, 0x0000); //unknown z

                    int pcounter = 1;


                    int lastid = cutscene.Points.Count - 1;

                    if (!MainForm.settings.NoDummyPoints)
                    {

                        //mandatory to have atleast 4 points
                        while (cutscene.Points.Count < 4)
                        {
                            cutscene.Points.Add(new ZCutscenePosition(cutscene.Points[lastid].Cameraroll, 0, 45, cutscene.Points[lastid].Position, cutscene.Points[lastid].Position2));
                        }
                        //also we need 2 dummy points
                        cutscene.Points.Add(new ZCutscenePosition(0, 0, 45, cutscene.Points[lastid].Position, cutscene.Points[lastid].Position2));
                        cutscene.Points.Add(new ZCutscenePosition(0, 0, 45, cutscene.Points[lastid].Position, cutscene.Points[lastid].Position2));
                        cutscene.Points.Insert(0, new ZCutscenePosition(0, 1, 45, cutscene.Points[0].Position, cutscene.Points[0].Position2));
                        cutscene.Points.Insert(0, new ZCutscenePosition(0, 1, 45, cutscene.Points[0].Position, cutscene.Points[0].Position2));
                    }

                    foreach (ZCutscenePosition point in cutscene.Points)
                    {
                        Helpers.Append16(ref data, (ushort)(0x0000 | ((pcounter == cutscene.Points.Count ? 0xFF : 0x00) << 8)));
                        Helpers.Append16(ref data, 0x0000); //padding
                        Helpers.Append32(ref data, 0x00000000); //angle, but its unused here
                        Helpers.Append16(ref data, (ushort)point.Position.X);
                        Helpers.Append16(ref data, (ushort)point.Position.Y);
                        Helpers.Append16(ref data, (ushort)point.Position.Z);
                        Helpers.Append16(ref data, 0x0000); //padding
                        pcounter++;
                    }

                    //focus point

                    Helpers.Append32(ref data, (uint)cutscene.Marker + 1);

                    Helpers.Append16(ref data, 0x0000); //unknown w
                    Helpers.Append16(ref data, cutscene.StartFrame);
                    Helpers.Append16(ref data, (ushort)(cutscene.StartFrame + cutscene.GetTotalFrames()));
                    Helpers.Append16(ref data, 0x0000); //unknown z

                    pcounter = 1;

                    foreach (ZCutscenePosition point in cutscene.Points)
                    {
                        Helpers.Append16(ref data, (ushort)(0x0000 | ((pcounter == cutscene.Points.Count ? 0xFF : 0x00) << 8) | (point.Cameraroll & 0x00FF)));
                        Helpers.Append16(ref data, point.Frames);
                        data.AddRange(BitConverter.GetBytes(point.Angle).Reverse());
                        Helpers.Append16(ref data, (ushort)point.Position2.X);
                        Helpers.Append16(ref data, (ushort)point.Position2.Y);
                        Helpers.Append16(ref data, (ushort)point.Position2.Z);
                        Helpers.Append16(ref data, 0x0000); //padding
                        pcounter++;
                    }

                    if (!MainForm.settings.NoDummyPoints)
                    {
                        //delete dummy points
                        cutscene.Points.RemoveAt(cutscene.Points.Count - 1);
                        cutscene.Points.RemoveAt(cutscene.Points.Count - 1);
                        cutscene.Points.RemoveAt(0);
                        cutscene.Points.RemoveAt(0);
                    }

                }
                else if (Game == "MM" && cutscene.Marker == 0x5A) //position
                {
                    if (cameradataheader == 0)
                    {
                        Helpers.Append32(ref data, (uint)cutscene.Marker);
                        cameradataheader = data.Count;
                        Helpers.Append32(ref data, 0); // size of the block
                    }

                    int lastid = cutscene.Points.Count - 1;

                    if (!MainForm.settings.NoDummyPoints)
                    {

                        //mandatory to have atleast 4 points
                        while (cutscene.Points.Count < 4)
                        {
                            cutscene.Points.Add(new ZCutscenePosition(cutscene.Points[lastid].Cameraroll, 0, 45, cutscene.Points[lastid].Position, cutscene.Points[lastid].Position2));
                        }
                        //also we need 2 dummy points
                        cutscene.Points.Add(new ZCutscenePosition(0, 0, 45, cutscene.Points[lastid].Position, cutscene.Points[lastid].Position2));
                        cutscene.Points.Add(new ZCutscenePosition(0, 0, 45, cutscene.Points[lastid].Position, cutscene.Points[lastid].Position2));
                        cutscene.Points.Insert(0, new ZCutscenePosition(0, 1, 45, cutscene.Points[0].Position, cutscene.Points[0].Position2));
                        cutscene.Points.Insert(0, new ZCutscenePosition(0, 1, 45, cutscene.Points[0].Position, cutscene.Points[0].Position2));
                    }

                    //int numentries = data.Count;
                    Helpers.Append16(ref data, (ushort)cutscene.Points.Count); //num entries
                    Helpers.Append16(ref data, (ushort)((cutscene.Points.Count * 32) + 8)); //size (???)
                    Helpers.Append32(ref data, (uint)(cutscene.GetTotalFrames() - cutscene.StartFrame));

                    Helpers.Overwrite32(ref data, cameradataheader, (uint)(Helpers.Read32(data, cameradataheader) + ((cutscene.Points.Count * 32) + 8)));

                    int pcounter = 1;
                    int endframe = cutscene.StartFrame;




                    foreach (ZCutscenePosition point in cutscene.Points)
                    {

                        endframe += point.Frames;
                        Helpers.Append16(ref data, 0564); //camera behaviour unknown, use 05,  factor 00?
                        Helpers.Append16(ref data, point.Frames); //end frame
                        Helpers.Append16(ref data, (ushort)point.Position2.X);
                        Helpers.Append16(ref data, (ushort)point.Position2.Y);
                        Helpers.Append16(ref data, (ushort)point.Position2.Z);
                        Helpers.Append16(ref data, 0x0000); //actor focus
                        pcounter++;

                    }

                    foreach (ZCutscenePosition point in cutscene.Points)
                    {
                        endframe += point.Frames;
                        Helpers.Append16(ref data, 0564); //camera behaviour unknown, use 05,  factor 100?
                        Helpers.Append16(ref data, point.Frames); //end frame
                        Helpers.Append16(ref data, (ushort)point.Position.X);
                        Helpers.Append16(ref data, (ushort)point.Position.Y);
                        Helpers.Append16(ref data, (ushort)point.Position.Z);
                        Helpers.Append16(ref data, 0x0000); //actor focus
                        pcounter++;

                    }

                    foreach (ZCutscenePosition point in cutscene.Points)
                    {
                        Helpers.Append16(ref data, point.Frames); //length (frames)
                        Helpers.Append16(ref data, (ushort)point.Cameraroll);
                        Helpers.Append16(ref data, (ushort)point.Angle);
                        Helpers.Append16(ref data, 0x0000); //padding
                        pcounter++;
                    }

                    if (!MainForm.settings.NoDummyPoints)
                    {
                        //delete dummy points
                        cutscene.Points.RemoveAt(cutscene.Points.Count - 1);
                        cutscene.Points.RemoveAt(cutscene.Points.Count - 1);
                        cutscene.Points.RemoveAt(0);
                        cutscene.Points.RemoveAt(0);
                    }

                }
                else if ((Game == "OOT" && cutscene.Marker == 0x13) || (Game == "MM" && cutscene.Marker == 0xA))
                {
                    int textboxframes = 0;
                    Helpers.Append32(ref data, (uint)cutscene.Marker);
                    Helpers.Append16(ref data, 0x0000);
                    Helpers.Append16(ref data, (ushort)cutscene.Textboxes.Count);
                    foreach (ZTextbox textbox in cutscene.Textboxes)
                    {
                        Helpers.Append16(ref data, textbox.Message);
                        Helpers.Append16(ref data, (ushort)(cutscene.StartFrame + textboxframes));
                        Helpers.Append16(ref data, (ushort)(cutscene.StartFrame + textboxframes + textbox.Frames));
                        Helpers.Append16(ref data, (ushort)(textbox.Type));
                        Helpers.Append16(ref data, (ushort)(textbox.Type < 2 ? textbox.TopMessage : 0x088B));
                        Helpers.Append16(ref data, (ushort)(textbox.Type < 2 ? textbox.BottomMessage : 0xFFFF));
                        textboxframes += textbox.Frames;
                    }
                }
                else if ((Game == "OOT" && cutscene.Marker == 0x2D) || (Game == "MM" && cutscene.Marker == 0x98)) //transitions
                {
                    Helpers.Append32(ref data, (uint)cutscene.Marker);
                    Helpers.Append32(ref data, 0x00000001);
                    Helpers.Append16(ref data, cutscene.Data[0]);
                    Helpers.Append16(ref data, cutscene.StartFrame);
                    Helpers.Append16(ref data, cutscene.EndFrame);
                    Helpers.Append16(ref data, cutscene.EndFrame);
                }
                else if ((Game == "OOT" && cutscene.Marker == 0x3E8) || (Game == "MM" && cutscene.Marker == 0x15E)) //asm
                {
                    Helpers.Append32(ref data, (uint)cutscene.Marker);
                    Helpers.Append32(ref data, 0x00000001);
                    Helpers.Append16(ref data, cutscene.Data[0]);
                    Helpers.Append16(ref data, cutscene.StartFrame);
                    Helpers.Append16(ref data, cutscene.EndFrame);
                    Helpers.Append16(ref data, cutscene.EndFrame);
                }
                else if (Game == "OOT" && cutscene.Marker == 0x8C) //set time
                {
                    Helpers.Append32(ref data, (uint)cutscene.Marker);
                    Helpers.Append32(ref data, 0x00000001);
                    Helpers.Append16(ref data, 0x0000);
                    Helpers.Append16(ref data, cutscene.StartFrame);
                    Helpers.Append16(ref data, 0x0000);
                    Helpers.Append16(ref data, (ushort)(cutscene.Data[0] << 8 | cutscene.Data[1]));
                    Helpers.Append32(ref data, 0x00000000);
                }
                else if (cutscene.CutsceneActors.Count > 0)
                {
                    int actorframes = 0;
                    Helpers.Append32(ref data, (uint)cutscene.Marker);
                    Helpers.Append16(ref data, 0x0000);
                    Helpers.Append16(ref data, (ushort)cutscene.CutsceneActors.Count);
                    foreach (ZCutsceneActor actoraction in cutscene.CutsceneActors)
                    {
                        Helpers.Append16(ref data, actoraction.Animation);
                        Helpers.Append16(ref data, (ushort)(cutscene.StartFrame + actorframes));
                        Helpers.Append16(ref data, (ushort)(cutscene.StartFrame + actorframes + actoraction.Frames));
                        Helpers.Append16(ref data, (ushort)(actoraction.Rotation.X));
                        Helpers.Append16(ref data, (ushort)(actoraction.Rotation.Y));
                        Helpers.Append16(ref data, (ushort)(actoraction.Rotation.Z));
                        Helpers.Append32(ref data, (uint)(actoraction.Position.X));
                        Helpers.Append32(ref data, (uint)(actoraction.Position.Y));
                        Helpers.Append32(ref data, (uint)(actoraction.Position.Z));
                        Helpers.Append32(ref data, (uint)(actoraction.Position2.X));
                        Helpers.Append32(ref data, (uint)(actoraction.Position2.Y));
                        Helpers.Append32(ref data, (uint)(actoraction.Position2.Z));
                        //  Helpers.Append32(ref data, 0x3F800000); //vertex normal?
                        //  Helpers.Append32(ref data, 0x3F800000); //vertex normal?
                        // Helpers.Append32(ref data, 0x3F800000); //vertex normal?
                        Helpers.Append32(ref data, 0x00000000); //vertex normal?
                        Helpers.Append32(ref data, 0x00000000); //vertex normal?
                        Helpers.Append32(ref data, 0x00000001); //vertex normal?
                        actorframes += actoraction.Frames;
                    }
                }

            }

            Helpers.Append64(ref data, 0xFFFFFFFF00000000); // cutscene terminator

            AddPadding(ref data, 8);
            Helpers.Overwrite32(ref data, totalframesoffset, (uint)totalframes);

            if (Game == "MM")
            {
                Cutscene = backupcutscene;
            }
        }

        #region Collision

        public void WriteSceneCollision(List<byte> Data, byte bank, bool zobj, string Game)
        {
            /* Fix scene header */
            if (!zobj)
            {
                Helpers.Overwrite32(ref Data, CmdCollisionOffset + 4, (uint)(0x00000000 | bank << 24 | Data.Count));
                CollisionOffset = Data.Count;
            }

            /* Determine collision's minimum/maximum coordinates... */
            OpenTK.Vector3d MinCoordinate = new OpenTK.Vector3d(0, 0, 0);
            OpenTK.Vector3d MaxCoordinate = new OpenTK.Vector3d(0, 0, 0);
    
            foreach (ObjFile.Vertex Vtx in ColModel.Vertices)
            {
                /* Minimum... */
                MinCoordinate.X = Math.Min(MinCoordinate.X, Vtx.X * Scale);
                MinCoordinate.Y = Math.Min(MinCoordinate.Y, Vtx.Y * Scale);
                MinCoordinate.Z = Math.Min(MinCoordinate.Z, Vtx.Z * Scale);

                /* Maximum... */
                MaxCoordinate.X = Math.Max(MaxCoordinate.X, Vtx.X * Scale);
                MaxCoordinate.Y = Math.Max(MaxCoordinate.Y, Vtx.Y * Scale);
                MaxCoordinate.Z = Math.Max(MaxCoordinate.Z, Vtx.Z * Scale);

            }

            if (MainForm.settings.TriplicateCollisionBounds)
            {
                MinCoordinate.X = MainForm.Clamp(MinCoordinate.X * 3, -32767, 32767);
                MinCoordinate.Y = MainForm.Clamp(MinCoordinate.Y * 3, -32767, 32767);
                MinCoordinate.Z = MainForm.Clamp(MinCoordinate.Z * 3, -32767, 32767);
                MaxCoordinate.X = MainForm.Clamp(MaxCoordinate.X * 3, -32767, 32767);
                MaxCoordinate.Y = MainForm.Clamp(MaxCoordinate.Y * 3, -32767, 32767);
                MaxCoordinate.Z = MainForm.Clamp(MaxCoordinate.Z * 3, -32767, 32767);
            }

            /* Prepare variables */
            int CmdVertexArray = -1, CmdPolygonArray = -1, CmdPolygonTypes = -1, CmdWaterBoxes = -1;
            int VertexArrayOffset = -1, PolygonArrayOffset = -1, PolygonTypesOffset = -1, WaterBoxesOffset = -1, CameraOffset = -1;

            /* Write collision header */
            Helpers.Append16(ref Data, (ushort)Convert.ToInt16(MinCoordinate.X));  /* Absolute minimum X/Y/Z */
            Helpers.Append16(ref Data, (ushort)Convert.ToInt16(MinCoordinate.Y));
            Helpers.Append16(ref Data, (ushort)Convert.ToInt16(MinCoordinate.Z));
            Helpers.Append16(ref Data, (ushort)Convert.ToInt16(MaxCoordinate.X));  /* Absolute maximum X/Y/Z */
            Helpers.Append16(ref Data, (ushort)Convert.ToInt16(MaxCoordinate.Y));
            Helpers.Append16(ref Data, (ushort)Convert.ToInt16(MaxCoordinate.Z));
            CmdVertexArray = Data.Count;
            Helpers.Append32(ref Data, 0x00000000);                                /* Vertex count */
            Helpers.Append32(ref Data, 0x00000000);                                /* Vertex array offset */
            CmdPolygonArray = Data.Count;
            Helpers.Append32(ref Data, 0x00000000);                                /* Polygon count */
            Helpers.Append32(ref Data, 0x00000000);                                /* Polygon array offset */
            CmdPolygonTypes = Data.Count;
            Helpers.Append32(ref Data, 0x00000000);                                /* Polygon type offset */
            CameraOffset = Data.Count;
            Helpers.Append32(ref Data, 0x00000000);                                /* Camera data offset */
            CmdWaterBoxes = Data.Count;
            Helpers.Append32(ref Data, 0x00000000);                                /* Waterbox count */
            Helpers.Append32(ref Data, 0x00000000);                                /* Waterbox offset */

            AddPadding(ref Data, 8);

            /* Write vertex array & fix command */
            VertexArrayOffset = Data.Count;
            foreach (ObjFile.Vertex Vtx in ColModel.Vertices)
            {
                Helpers.Append16(ref Data, (ushort)Convert.ToInt16(Vtx.X * Scale));
                Helpers.Append16(ref Data, (ushort)Convert.ToInt16(Vtx.Y * Scale));
                Helpers.Append16(ref Data, (ushort)Convert.ToInt16(Vtx.Z * Scale));
            }
            Helpers.Overwrite32(ref Data, CmdVertexArray, (uint)(ColModel.Vertices.Count << 16));
            Helpers.Overwrite32(ref Data, CmdVertexArray + 4, (uint)(0x00000000 | bank << 24 | VertexArrayOffset));

            AddPadding(ref Data, 8);

            /* Write polygon array & fix command */
            PolygonArrayOffset = Data.Count;
            int TriangleTotal = 0;
            ulong polytype;
            ushort polyflags, polyflagsB;
            ushort polytypeID = 0;
 
            foreach (ObjFile.Group Group in ColModel.Groups)
            {
                Group.Name = Group.Name.Replace("TAG_", "#");
                polytype = 0;
                polyflags = (ushort) ((Group.Name.ToLower().Contains("#ignorecamera") ? 0x2000 : 0) + (Group.Name.ToLower().Contains("#ignoreactors") ? 0x4000 : 0) + (Group.Name.ToLower().Contains("#ignoreprojectiles") ? 0x8000 : 0));
                polyflagsB = (ushort)(((Group.Name.ToLower().Contains("#speed") || Group.Name.ToLower().Contains("#direction")) && !Group.Name.ToLower().Contains("#waterstream") ? 0x2000 : 0));
                if (Group.Name.ToLower().Contains("#polytype"))
                {
                    int polytypenumber = 0;
                    try
                    {
                        if (!Int32.TryParse(Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#polytype") + 9, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out polytypenumber))
                        {
                            MessageBox.Show("Bad usage of #Polytype tag, expected #PolytypeXX where XX is the ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Bad usage of #Polytype tag, expected #PolytypeXX where XX is the ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
             
                    polytypeID = (ushort)polytypenumber;
                    while (polytypeID + 1 > MainForm.CurrentScene.PolyTypes.Count)
                        MainForm.CurrentScene.PolyTypes.Add(new ZColPolyType(0));
                }
                else if (Group.Name.ToLower().Replace("#room", "").Contains("#"))
                {
                    if (Group.Name.ToLower().Contains("#raw"))
                    {
                        polytype = 0;
                        if (!UInt64.TryParse(Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#raw") + 4, 16), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out polytype))
                        {
                            MessageBox.Show("Bad usage of Raw tag. It should be #RawXXXXXXXXXXXXXXXX (XXXXXXXXXXXXXXX = polytype raw data in Hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                   //     polytype = Convert.ToUInt64(Group.Name.Substring(Group.Name.IndexOf("#Raw") + 4, 16), 16);
                    }
                    else
                    foreach (KeyValuePair<string, ulong> kp in MainForm.flaglist)
                    {
                        if (Group.Name.ToLower().Contains(kp.Key.ToLower()))
                        {
                            if (kp.Key == "#Exit")
                            {
                                int exitnum = 0;

                                if (Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#exit")).Length < 7 || !Int32.TryParse(Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#exit") + 5, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out exitnum))
                                {
                                    if (!Int32.TryParse(Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#exit") + 5, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out exitnum))
                                    {
                                        MessageBox.Show("Bad usage of Exit tag. It should be #ExitXX (XX = exit ID in hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }
                                }
                                


                                exitnum++;
                                polytype = (ulong)(polytype | ((ulong)exitnum << 40));
                                while (exitnum > MainForm.CurrentScene.ExitList.Count)
                                    MainForm.CurrentScene.ExitList.Add(new ZUShort(0));
                            }
                            else if (kp.Key == "#Camera")
                            {
                                int cameranum = 0;
                                if (!Int32.TryParse(Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#camera") + 7, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out cameranum))
                                {
                                    MessageBox.Show("Bad usage of Camera tag. It should be #CameraX (X = camera ID in hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                                polytype = (ulong)(polytype | ((ulong)cameranum << 32));
                                while (cameranum+1 > MainForm.CurrentScene.Cameras.Count)
                                    MainForm.CurrentScene.Cameras.Add(new ZCamera(0, 0, 0, 0, 0, 0, 1, 45,0xFFFF,0xFFFF));
                            }
                            else if (kp.Key == "#Environment")
                            {
                                int envnum = 0;
                                if (!Int32.TryParse(Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#environment") + 12, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out envnum))
                                {
                                    MessageBox.Show("Bad usage of Environment tag. It should be #EnvironmentX (X = environment ID in hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                             
                                polytype = (ulong)(polytype | ((ulong)envnum << 8));
                            }
                            else if (kp.Key == "#IndoorEnv")
                            {
                                int envnum = 0;
                                if (!Int32.TryParse(Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#indoorenv") + 10, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out envnum))
                                {
                                    MessageBox.Show("Bad usage of Environment tag. It should be #IndoorEnvXX (XX = environment ID in hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                             
                                polytype = (ulong)(polytype | ((ulong)envnum << 6));
                            }
                            else if (kp.Key == "#Direction")
                            {
                                int dirnum = 0;
                                if (!Int32.TryParse(Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#direction") + 10, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dirnum))
                                {
                                    MessageBox.Show("Bad usage of Direction tag. It should be #DirectionXX (XX = direction in hex) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                    
                                if (dirnum < 0x3F) polytype = (ulong)(polytype | ((ulong)dirnum << 21));
                            }
                            else if (kp.Key == "#Speed")
                            {
                                int spdnum = 0;
                                if (!Int32.TryParse(Group.Name.ToLower().Substring(Group.Name.ToLower().IndexOf("#speed") + 6, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out spdnum))
                                {
                                    MessageBox.Show("Bad usage of Speed tag. It should be #SpeedX (X = 1 slow, 2 mid, 3 fast, 4 preserves previous speed) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                if (spdnum < 8) polytype = (ulong)(polytype | ((ulong)spdnum << 18));
                            }
                            else
                                polytype = polytype | kp.Value;
                        }
                    }

                    if (polytype != 0 || polyflags != 0)
                    {

                        int polyid = PolyTypes.FindIndex(x => x.Raw == polytype && x.PolyFlagA == polyflags && x.PolyFlagB == polyflagsB);
                        if (polyid != -1)
                            polytypeID = (ushort) polyid;
                        else
                        {
                            PolyTypes.Add(new ZColPolyType(polytype, polyflags, polyflagsB));
                            polytypeID = (ushort) (PolyTypes.Count - 1);
                        }
                    }
                    else
                        polytypeID = (ushort)Group.PolyType;

                }
                else polytypeID = (ushort)Group.PolyType;

               // Console.WriteLine("Polytype assigned: " + polytypeID);

                if (MainForm.Is1April)
                {
                    foreach(ZColPolyType poly in PolyTypes)
                    {
                        poly.Raw = poly.Raw | 0x0000800000000000;
                    }
                }

                foreach (ObjFile.Triangle Tri in Group.Triangles)
                {
                    int[] ni = new int[3];
                    Vector3[] vtx = new Vector3[3];

                    for (int i = 0; i < 3; i++)
                        vtx[i] = new Vector3(
                            (int)ColModel.Vertices[Tri.VertIndex[i]].X, 
                            (int)ColModel.Vertices[Tri.VertIndex[i]].Y, 
                            (int)ColModel.Vertices[Tri.VertIndex[i]].Z);
                   

                    Vector3 u = vtx[1] - vtx[0];
                    Vector3 v = vtx[2] - vtx[0];
                    Vector3 triWind;
                    triWind.X = (u.Y * v.Z) - (u.Z * v.Y);
                    triWind.Y = (u.Z * v.X) - (u.X * v.Z);
                    triWind.Z = (u.X * v.Y) - (u.Y * v.X);

                    Vector3 triNorm = Vector3.Normalize(triWind);
                    ni[0] = (int)Math.Round(triNorm.X * 0x7FFF);
                    ni[1] = (int)Math.Round(triNorm.Y * 0x7FFF);
                    ni[2] = (int)Math.Round(triNorm.Z * 0x7FFF);

                    // distance from origin
                    int dn = (int)Math.Round(((triNorm.X * vtx[0].X) + (triNorm.Y * vtx[1].X) + (triNorm.Z * vtx[2].X)) * -1);
                    if (dn < 0) dn += 0x10000;

                    for (int i = 0; i < 3; i++)
                        if (ni[i] < 0)
                            ni[i] += 0x10000;

                    bool skip = false;

                    if (ni[0] == 0 && ni[1] == 0 && ni[2] == 0) {
                        skip = true;
                        Console.WriteLine("Skip Collision Triangle:");
                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine( "Tri Vtx[" + i.ToString() + "] Pos: " + ColModel.Vertices[Tri.VertIndex[i]].X.ToString() + ", " +
                                ColModel.Vertices[Tri.VertIndex[i]].Y.ToString() + ", " +
                                ColModel.Vertices[Tri.VertIndex[i]].Z.ToString());
                        }
                        Console.WriteLine( "Tri Nrm: " + triNorm.X.ToString() + ", " +
                            triNorm.Y.ToString() + ", " +
                            triNorm.Z.ToString());
                    }

                    if (!Group.Name.ToLower().Contains("#blackplane") && !Group.Name.ToLower().Contains("#nocollision") && !Group.Name.ToLower().Contains("#door") && !skip)
                    {
                        Helpers.Append16(ref Data, polytypeID);    /* Polygon type */
                        Helpers.Append16(ref Data, (ushort) ((ushort)Tri.VertIndex[0] | MainForm.CurrentScene.PolyTypes[polytypeID].PolyFlagA));  /* Index of vertex 1 */
                        Helpers.Append16(ref Data, (ushort) ((ushort)Tri.VertIndex[1] | MainForm.CurrentScene.PolyTypes[polytypeID].PolyFlagB));  /* Index of vertex 2 */
                        Helpers.Append16(ref Data, (ushort)Tri.VertIndex[2]);  /* Index of vertex 3 */
                        Helpers.Append16(ref Data, (ushort)(ni[0] & 0xFFFF));                    /* Collision normals X/Y/Z */
                        Helpers.Append16(ref Data, (ushort)(ni[1] & 0xFFFF));
                        Helpers.Append16(ref Data, (ushort)(ni[2] & 0xFFFF));
                        Helpers.Append16(ref Data, (ushort)(dn & 0xFFFF));                    /* Distance from origin */
                    }
                    else TriangleTotal--;
                }

                TriangleTotal += Group.Triangles.Count;
            }
            Helpers.Overwrite32(ref Data, CmdPolygonArray, (uint)(TriangleTotal << 16));
            Helpers.Overwrite32(ref Data, CmdPolygonArray + 4, (uint)(0x00000000 | bank << 24 | PolygonArrayOffset));

            FixCollision(ref Data, VertexArrayOffset, PolygonArrayOffset, TriangleTotal);

            AddPadding(ref Data, 8);

            /* Write polygon types & fix command */
                PolygonTypesOffset = Data.Count;
            foreach (ZColPolyType PT in PolyTypes)
                Helpers.Append64(ref Data, PT.Raw);
            Helpers.Overwrite32(ref Data, CmdPolygonTypes, (uint)(0x00000000 | bank << 24 | PolygonTypesOffset));

            AddPadding(ref Data, 8);

            /* Write waterboxes & fix command */
            WaterBoxesOffset = Data.Count;
            foreach (ZWaterbox WBox in Waterboxes)
            {
                Helpers.Append16(ref Data, (ushort)Convert.ToInt16(WBox.XPos));
                Helpers.Append16(ref Data, (ushort)Convert.ToInt16(WBox.YPos));
                Helpers.Append16(ref Data, (ushort)Convert.ToInt16(WBox.ZPos));
                Helpers.Append16(ref Data, (ushort)Convert.ToInt16(WBox.XSize));
                Helpers.Append16(ref Data, (ushort)Convert.ToInt16(WBox.ZSize));
                Helpers.Append16(ref Data, 0x0000);
                Helpers.Append32(ref Data, (uint)(WBox.Camera | (WBox.Env << 8) | (WBox.Room << 13)));
            }
            Helpers.Overwrite32(ref Data, CmdWaterBoxes, (uint)(Waterboxes.Count << 16));
            Helpers.Overwrite32(ref Data, CmdWaterBoxes + 4, (uint)(0x00000000 | bank << 24 | WaterBoxesOffset));

            AddPadding(ref Data, 8);


            //camera

           
            Helpers.Overwrite32(ref Data, CameraOffset, (uint)(0x00000000 | bank << 24 | Data.Count));

            if (Game == "MM")
            {
                Helpers.Overwrite32(ref Data, MMCmdCameraOffset + 8, (uint)(0x00000000 | bank << 24 | Data.Count));
            }
            
            int incr2 = 0;

            if (1 == 1) //Game == "OOT")
            {

                foreach (ZCamera camera in Cameras)
                {
                    Helpers.Append32(ref Data, (uint)(0x00000000 | (camera.Type << 16) | ((camera.Type == 0x1E ? 6 : 3)))); //camera type, camera count
                    Helpers.Append32(ref Data, (uint)(0x00000000 | bank << 24 | (uint)Data.Count + 4 + ((Cameras.Count - 1 - incr2) * 8) + (18 * incr2)));
                   // Console.WriteLine("" + (0x00000000 | bank << 24 | (uint)Data.Count + 4 + ((Cameras.Count - 1 - incr2) * 8) + (18 * incr2)).ToString("X8"));
                    incr2++;
                    if (camera.Type == 0x1E) incr2++;
                }

                foreach (ZCamera camera in Cameras)
                {

                    Helpers.Append16(ref Data, (ushort)camera.XPos);
                    Helpers.Append16(ref Data, (ushort)camera.YPos);
                    Helpers.Append16(ref Data, (ushort)camera.ZPos);
                    Helpers.Append16(ref Data, (ushort)camera.XRot);
                    Helpers.Append16(ref Data, (ushort)camera.YRot);
                    Helpers.Append16(ref Data, (ushort)camera.ZRot);
                    Helpers.Append16(ref Data, (ushort)camera.Fov); //fov

                    //type 0x1E

                    Helpers.Append16(ref Data, (ushort)camera.Unk1);
                    Helpers.Append16(ref Data, (ushort)camera.Unk2);

                    if (camera.Type == 0x1E)
                    {
                        Helpers.Append16(ref Data, (ushort)camera.Unk12);
                        Helpers.Append16(ref Data, (ushort)camera.Unk14);
                        Helpers.Append16(ref Data, (ushort)camera.Unk16);
                        Helpers.Append16(ref Data, (ushort)camera.Unk18);
                        Helpers.Append16(ref Data, (ushort)camera.Unk1A);
                        Helpers.Append16(ref Data, (ushort)camera.Unk1C);
                        Helpers.Append16(ref Data, (ushort)camera.Unk1E);
                        Helpers.Append16(ref Data, (ushort)camera.Unk20);
                        Helpers.Append16(ref Data, (ushort)camera.Unk22);
                    }

                    
                }
            }
            else
            {
                    Helpers.Append32(ref Data, (uint)(0x003F0008)); 
                    Helpers.Append32(ref Data, (uint)(0x08000000));
                    Helpers.Append64(ref Data, 0x00);
                    Helpers.Append64(ref Data, 0x00);
            }

           // Console.WriteLine("cam offset "   + CameraOffset);



            /* Padding for good measure , before it was 0x800*/
            AddPadding(ref Data, 0x10);
        }

        /* Algorithm by MN, implementation by JSA, C version by spinout: http://wiki.spinout182.com/w/Zelda_64:_Collision_Normals
         * Fixed for good by DeathBasket: http://core.the-gcn.com/index.php?/topic/675-sharpocarina-zelda-oot-scene-development-system/page__view__findpost__p__11060
         */
        private void FixCollision(ref List<byte> Data, int VertOff, int TriOff, int TriCount)
        {
            int i, pos, end = TriOff + (TriCount << 4);
            int v1, v2, v3, dn;
            int[] p1 = new int[3], p2 = new int[3], p3 = new int[3], dx = new int[2], dy = new int[2], dz = new int[2], ni = new int[3];
            float nd;
            float[] nf = new float[3], uv = new float[3];

            for (pos = TriOff; pos < end; pos += 0x10)
            {
                v1 = Helpers.Read16(Data, pos + 2) & 0x1FFF;
                v2 = Helpers.Read16(Data, pos + 4) & 0x1FFF;
                v3 = Helpers.Read16(Data, pos + 6) & 0x1FFF;

                for (i = 0; i < 3; i++)
                {
                    p1[i] = Helpers.Read16S(Data, VertOff + (v1 * 0x6) + (i << 1));
                    p2[i] = Helpers.Read16S(Data, VertOff + (v2 * 0x6) + (i << 1));
                    p3[i] = Helpers.Read16S(Data, VertOff + (v3 * 0x6) + (i << 1));
                }

                dx[0] = p1[0] - p2[0]; dx[1] = p2[0] - p3[0];
                dy[0] = p1[1] - p2[1]; dy[1] = p2[1] - p3[1];
                dz[0] = p1[2] - p2[2]; dz[1] = p2[2] - p3[2];

                nf[0] = (float)(dy[0] * dz[1]) - (dz[0] * dy[1]);
                nf[1] = (float)(dz[0] * dx[1]) - (dx[0] * dz[1]);
                nf[2] = (float)(dx[0] * dy[1]) - (dy[0] * dx[1]);

                /* calculate length of normal vector */
                nd = (float)Math.Sqrt((nf[0] * nf[0]) + (nf[1] * nf[1]) + (nf[2] * nf[2]));

                for (i = 0; i < 3; i++)
                {
                    if (nd != 0)
                        uv[i] = nf[i] / nd; /* uv being the unit normal vector */
                    nf[i] = uv[i] * 0x7FFF;   /* nf being the way OoT uses it */
                }

                /* distance from origin... */
                dn = (int)Math.Round(((uv[0] * p1[0]) + (uv[1] * p1[1]) + (uv[2] * p1[2])) * -1);

                if (dn < 0)
                    dn += 0x10000;
                Helpers.Overwrite16(ref Data, pos + 0xE, (ushort)(dn & 0xFFFF));
                for (i = 0; i < 3; i++)
                {
                    ni[i] = (int)Math.Round(nf[i]);
                    if (ni[i] < 0)
                        ni[i] += 0x10000;
                    Helpers.Overwrite16(ref Data, (pos + 8 + (i << 1)), (ushort)(ni[i] & 0xFFFF));
                }
            }
        }

        #endregion

        #region Header Writing/Fixing

        private void WriteRoomHeader(ZRoom Room, ZScene MainHeader)
        {
            /* Write room header */
            if (SceneHeaders.Count > 0)
            {
                CmdRoomAlternateSceneHeader = Room.RoomData.Count;
                Helpers.Append64(ref Room.RoomData, 0x1800000000000000);                        /* Alternate Scene Headers */
            }
            else if (cloneid > 0)
            {
                MainHeader.SceneHeaders[MainHeader.SceneHeaders.FindIndex(x => x.Scene == this)]._RoomInjectOffsetValues[RoomIndex] = (uint)(0x03000000 | Room.RoomData.Count);
                Helpers.Overwrite32(ref Room.RoomData, MainHeader.SceneHeaders[MainHeader.SceneHeaders.FindIndex(x => x.Scene == this)]._RoomInjectOffset[RoomIndex], (uint)(0x03000000 | Room.RoomData.Count));
            }

            Helpers.Append64(ref Room.RoomData, (ulong)(0x1600000000000000 | Room.Echo ));        /* Sound settings */
            Helpers.Append64(ref Room.RoomData, (ulong)(0x0800000000000000 | ((ulong)Room.Restriction << 48)
                | ((ulong)(Room.ShowInvisibleActors ? 1 : 0) << 8) | ((ulong)(Room.DisableWarpSongs ? 4 : 0) << 8) | (Room.IdleAnim)));        /* idle anims settings etc */
            Helpers.Append64(ref Room.RoomData, (ulong)(0x1200000000000000 | ((ulong)(Room.DisableSkybox ? 1 : 0) << 24) | ((ulong)(Room.DisableSunMoon ? 1 : 0) << 16)));        /* Skybox modifier */

            Helpers.Append64(ref Room.RoomData, (ulong)(0x1000000000000000 | ((ulong)(Room.StartTime != 0xFFFF ? Room.StartTime + 1 : Room.StartTime) << 16) | ((ulong)Room.TimeSpeed << 8)));  /* Time settings */

            if (cloneid == 0)
            {
                CmdMeshHeaderOffset = Room.RoomData.Count;
                Helpers.Append64(ref Room.RoomData, 0x0A00000000000000);        /* Mesh header */
            }
            else
            {
                Helpers.Append64(ref Room.RoomData, (ulong) (0x0A00000003000000 | CmdMeshHeaderOffset));        /* Mesh header */
            }

            /* Objects */
            if (Room.ZObjects.Count != 0)
            {
                CmdObjectOffset = Room.RoomData.Count;
                Helpers.Append64(ref Room.RoomData, 0x0B00000000000000);
            }

            /* Actors */
            if (Room.ZActors.Count != 0)
            {
                CmdActorOffset = Room.RoomData.Count;
                Helpers.Append64(ref Room.RoomData, 0x0100000000000000);
            }

            if (Room.WindWest == 0 && Room.WindSouth == 0 && Room.WindStrength == 0 && Room.WindVertical == 0) { }
            else
            {
            Helpers.Append64(ref Room.RoomData, (ulong)(0x0500000000000000 | (ulong)Room.WindWest << 24 | (ulong)Room.WindVertical << 16 
                | (ulong)Room.WindSouth << 8 | (ulong)Room.WindStrength));        /* Wind settings */
            }

            /* Unused environments */
            List<ZAdditionalLight> tempAdditionalLights = new List<ZAdditionalLight>();
            tempAdditionalLights.AddRange(Room.AdditionalLights.FindAll(x => x.ColorC != Color.Black || x.Radius != 0 || !x.PointLight));
            if (tempAdditionalLights.Count != 0)
            {
                CmdExtraLightOffset = Room.RoomData.Count;
                Helpers.Append64(ref Room.RoomData, 0x0C00000000000000);
            }
            else if (Rooms.FindIndex(x => x.AdditionalLights.Count > 0) != -1) //if atleast one room has additional lights, write dummy header
            {
                Helpers.Append64(ref Room.RoomData, 0x0C00000000000000);
            }

        
            Helpers.Append64(ref Room.RoomData, 0x1400000000000000 | (ulong)(Room.AffectedByPointLight ? 01 : 00) << 48);        /* End marker */

            if (SceneHeaders.Count > 0)
            {
                Helpers.Overwrite32(ref Room.RoomData, CmdRoomAlternateSceneHeader + 4, (uint)(0x03000000 | Room.RoomData.Count));
                foreach (ZSceneHeader SceneHeader in SceneHeaders)
                {
                    if (SceneHeader._RoomInjectOffset.Length < MainHeader.Rooms.Count)
                    {
                        SceneHeader._RoomInjectOffset = new int[MainHeader.Rooms.Count];
                        SceneHeader._RoomInjectOffsetValues = new uint[MainHeader.Rooms.Count];
                    }
                    SceneHeader._RoomInjectOffset[RoomIndex] = Room.RoomData.Count;
                    Helpers.Append32(ref Room.RoomData, 0x00000000); //placeholder
                }
                AddPadding(ref Room.RoomData, 8);
            }
        }

        private void FixRoomHeader(ZRoom Room)
        {
            /* Fix room header commands; mesh header... */
            if (CmdMeshHeaderOffset != -1 && cloneid == 0)
                Helpers.Overwrite32(ref Room.RoomData, CmdMeshHeaderOffset + 4, (uint)(0x03000000 | MeshHeaderOffset)); /* Mesh header */

            /* ...object list... */
            if (Room.ZObjects.Count != 0 && CmdObjectOffset != -1)
            {
                Helpers.Overwrite32(ref Room.RoomData, CmdObjectOffset, (uint)(0x0B000000 | (Room.ZObjects.Count << 16))); /* Objects */
                Helpers.Overwrite32(ref Room.RoomData, CmdObjectOffset + 4, (uint)(0x03000000 | ObjectOffset));
            }

            /* ...actor list */
            if (Room.ZActors.Count != 0 && CmdActorOffset != -1)
            {
                Helpers.Overwrite32(ref Room.RoomData, CmdActorOffset, (uint)(0x01000000 | (Room.ZActors.Count << 16))); /* Actors */
                Helpers.Overwrite32(ref Room.RoomData, CmdActorOffset + 4, (uint)(0x03000000 | ActorOffset));
            }

            /* ...extralights list */
            List<ZAdditionalLight> tempAdditionalLights = new List<ZAdditionalLight>();
            tempAdditionalLights.AddRange(Room.AdditionalLights.FindAll(x => x.ColorC != Color.Black || x.Radius != 0 || !x.PointLight));
            if (tempAdditionalLights.Count != 0 && CmdExtraLightOffset != -1)
            {
                Helpers.Overwrite32(ref Room.RoomData, CmdExtraLightOffset, (uint)(0x0C000000 | (tempAdditionalLights.Count << 16))); /* additional lights */
                Helpers.Overwrite32(ref Room.RoomData, CmdExtraLightOffset + 4, (uint)(0x03000000 | ExtraLightOffset));
            }


            if (MeshHeaderOffset != -1 && cloneid == 0)
            {
                if (!PregeneratedMesh)
                {

                    List<NDisplayList> opaqueLists = Room.DLists.FindAll(DL => (DL.TintAlpha >> 24) == 255 && !Room.LODdlists.Contains(DL));
                    List<NDisplayList> translucentLists = Room.DLists.FindAll(DL => (DL.TintAlpha >> 24) != 255 && !Room.LODdlists.Contains(DL));

                    opaqueLists = new List<NDisplayList>(opaqueLists.OrderBy(x => x.RenderLast == false));
                    translucentLists = new List<NDisplayList>(translucentLists.OrderBy(x => x.RenderLast == false));

                    foreach (LODgroup group in Room.LODgroups)
                    {
                        NDisplayList dummy = new NDisplayList();
                        dummy.Offset = group.lodgroupoffset;
                        translucentLists.Add(dummy);
                    }


                    int numEntries = Math.Max(opaqueLists.Count, translucentLists.Count);

                    if (!Prerendered)

                    {
                        Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, (uint)(0x00000000 | (numEntries << 16)));
                        Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset + 4, (uint)(0x03000000 | MeshHeaderOffset + 12));
                        Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset + 8, (uint)(0x03000000 | (MeshHeaderOffset + 12) + (numEntries * 8)));
                        MeshHeaderOffset += 12;
                    }
                    else
                    {
                        //Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, (uint)(0x00opaqueLists000000 | (numEntries << 16)));
                        // Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset + 4, (uint)(0x03000000 | MeshHeaderOffset + 32));
                        // Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset + 8, (uint)(0x03000000 | (MeshHeaderOffset + 32) + (numEntries * 8)));
                        MeshHeaderOffset += 32;
                    }


                    if (!Prerendered)
                    {
                        for (int i = 0; i < numEntries; i++)
                        {
                            if (i < opaqueLists.Count)
                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, (uint)(0x03000000 | opaqueLists[i].Offset));
                            else
                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, 0);

                            MeshHeaderOffset += 4;

                            if (i < translucentLists.Count)
                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, (uint)(0x03000000 | translucentLists[i].Offset));
                            else
                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, 0);

                            MeshHeaderOffset += 4;
                        }
                    }

                }
                else
                {
                    // TODO pregenerated mesh

                  

                    byte meshtype = Room.OriginalRoomData[Room.OriginalMeshHeaderOffset];

                    if (meshtype != 1)
                    {

                        int numEntries = Helpers.Read16(Room.OriginalRoomData, Room.OriginalMeshHeaderOffset) & 0x00FF;
                        uint originalmeshoffset = Helpers.Read32(Room.OriginalRoomData, Room.OriginalMeshHeaderOffset + 4) & 0x00FFFFFF;


                        Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, (uint)(0x00000000 | (numEntries << 16) | (meshtype << 24)));
                        Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset + 4, (uint)(0x03000000 | MeshHeaderOffset + 12));
                        Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset + 8, (uint)(0x03000000 | (MeshHeaderOffset + 12) + (numEntries * (new int[] { 8, 8, 16 }[meshtype]))));
                        MeshHeaderOffset += 12;

                        for (int i = 0; i < numEntries; i++)
                        {
                            // we copy the old mesh offset

                            if (meshtype == 0)
                            {
                                uint dlistoffset1 = Helpers.Read32(Room.OriginalRoomData, (int)originalmeshoffset + (8 * i)) & 0x00FFFFFF;
                                uint dlistoffset2 = Helpers.Read32(Room.OriginalRoomData, (int)originalmeshoffset + 4 + (8 * i)) & 0x00FFFFFF;
                                dlistoffset1 = dlistoffset1 - Room.oldoffset + Room.newoffset;
                                dlistoffset2 = dlistoffset2 - Room.oldoffset + Room.newoffset;

                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, (uint)(0x03000000 | dlistoffset1));
                                MeshHeaderOffset += 4;
                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, (uint)(0x03000000 | dlistoffset2));
                                MeshHeaderOffset += 4;
                            }
                            else if (meshtype == 2)
                            {
                                uint data1 = Helpers.Read32(Room.OriginalRoomData, (int)originalmeshoffset + 0 + (16 * i)); // X Y 
                                uint data2 = Helpers.Read32(Room.OriginalRoomData, (int)originalmeshoffset + 4 + (16 * i)); // Z dist

                                uint dlistoffset1 = Helpers.Read32(Room.OriginalRoomData, (int)originalmeshoffset + 8 + (16 * i));
                                uint dlistoffset2 = Helpers.Read32(Room.OriginalRoomData, (int)originalmeshoffset + 12 + (16 * i));

                                if ((dlistoffset1 & 0xFF000000) >> 24 != 0)
                                    dlistoffset1 = (dlistoffset1 & 0x00FFFFFF) - Room.oldoffset + Room.newoffset;
                                if ((dlistoffset2 & 0xFF000000) >> 24 != 0)
                                    dlistoffset2 = (dlistoffset2 & 0x00FFFFFF) - Room.oldoffset + Room.newoffset;


                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, data1);
                                MeshHeaderOffset += 4;
                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, data2);
                                MeshHeaderOffset += 4;
                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, dlistoffset1 == 0 ? 0 : (uint)(0x03000000 | dlistoffset1));
                                MeshHeaderOffset += 4;
                                Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, dlistoffset2 == 0 ? 0 : (uint)(0x03000000 | dlistoffset2));
                                MeshHeaderOffset += 4;
                            }
                        }
                    }
                    else
                    {


                        uint originalmeshoffset = Helpers.Read32(Room.OriginalRoomData, Room.OriginalMeshHeaderOffset + 4) & 0x00FFFFFF;


                        Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, (uint)(0x00000000 | ((prerenderimages.Count == 1 ? 0x01 : 0x02) << 16) | (meshtype << 24)));
                        Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset + 4, (uint)(0x03000000 | MeshHeaderOffset + 12));
                        MeshHeaderOffset += 12;
                        /*
                        uint dlistoffset1 = Helpers.Read32(Room.OriginalRoomData, (int)originalmeshoffset + (8 * i)) & 0x00FFFFFF;
                        dlistoffset1 = dlistoffset1 - Room.oldoffset + Room.newoffset;

                        Helpers.Overwrite32(ref Room.RoomData, MeshHeaderOffset, (uint)(0x03000000 | dlistoffset1));
                        MeshHeaderOffset += 4;*/
                    }
                        
                    }
                }

            

        }

        #endregion

        #endregion
        
        public Limb[] GetHierarchies(List<byte> Data, byte Bank)
        {
            try
            {
                int tOffset = 0;
                byte tBank = 0;
                int tCnt = 0;
                int j = 0;
                for (int i = 0; (i
                            <= (Data.Count - 8)); i = (i + 4))
                {
                    if (((Data[i] == Bank)
                                && (Data[i + 4] > 0)))
                    {
                        tBank = Data[i];
                        tCnt = Data[i + 4];
                        tOffset = Helpers.Read24S(Data, (i + 1));
                        if ((tOffset
                                    < (Data.Count - 16)))
                        {
                            for (j = tOffset; (j
                                        <= (tOffset
                                        + ((tCnt * 4)
                                        - 1))); j = (j + 4))
                            {
                                if ((Data[j] != tBank))
                                {
                                    break;
                                }

                            }

                            if ((i == j))
                            {
                                Limb[] tmpHierarchy = new Limb[tCnt];
                                int tmpLimbOff = 0;
                                for (int k = 0; (k
                                            <= (tCnt - 1)); k++)
                                {
                                    tmpHierarchy[k] = new Limb();
                                    tmpLimbOff =  Helpers.Read24S(Data, (tOffset + 1));
                                    int DisplayList = 0;
                                    // With...
                                    if ((Data[(int) (tmpLimbOff + 8)] == Bank))
                                    {
                                        DisplayList = Helpers.Read24S(Data, (tmpLimbOff + 9));
                                        tmpHierarchy[k].firstchild = (sbyte) Data[tmpLimbOff + 7];
                                        tmpHierarchy[k].z = (sbyte)Data[tmpLimbOff + 7];
                                        tmpHierarchy[k].y = (sbyte)Data[tmpLimbOff + 7];
                                        tmpHierarchy[k].x = (sbyte)Data[tmpLimbOff + 7];
                                        //  N64DList(N64DList.Length);
                                        // ReadInDL(Data, N64DList, ., DisplayList, (N64DList.Length - 1));
                                    }
                                    else
                                    {
                                        DisplayList = 0;
                                    }

                                    // If Data(tmpLimbOff + 12) = Bank Then
                                    //     .DisplayListLow = ReadUInt24(Data, tmpLimbOff + 13)
                                    //     ReDim Preserve N64DList(N64DList.Length)
                                    //     ReadInDL(Data, N64DList, .DisplayListLow, N64DList.Length - 1)
                                    // Else

                                    tOffset += 4;
                                }

                                return tmpHierarchy;
                            }

                        }

                    }

                }

                return null;
            }
            catch (Exception err)
            {
                return null;
            }

        }

        public class ZObjRender
        {
            public UInt16 actor = 0x00;
            public string variableregex = "....";
            public float scale = 1.0f;
            public UInt16 Yoff = 0;
            public List<SayakaGL.UcodeSimulator.DisplayListStruct> DLists = new List<UcodeSimulator.DisplayListStruct>();
            public List<Limb> Limbs = new List<Limb>();

            public ZObjRender(UInt16 actor, string var, float scale)
            {
                this.actor = actor;
                this.variableregex = var;
                this.scale = scale;
            }

            public ZObjRender(UInt16 actor, string var, float scale, List<SayakaGL.UcodeSimulator.DisplayListStruct> DLists, List<Limb> Limbs)
            {
                this.actor = actor;
                this.variableregex = var;
                this.scale = scale;
                this.DLists = DLists;
                this.Limbs = Limbs;
            }
        }

        public struct Limb
        {

            public short x;
            public short y;
            public short z;
            public SByte firstchild;
            public SByte nextchild;
            public Vector3 rotation;
            public List<SayakaGL.UcodeSimulator.DisplayListStruct> DList;


        }

        public ZScene Clone()
        {
            ZScene clone = (ZScene)this.MemberwiseClone();
            clone.Rooms = Rooms.ConvertAll(x => (x.Clone()));
            clone.Pathways = Pathways.ConvertAll(x => (x.Clone()));
            clone.Environments = Environments.ConvertAll(x => (x.Clone()));
            clone.SpawnPoints = SpawnPoints.ConvertAll(x => (x.Clone()));
            clone.Transitions = Transitions.ConvertAll(x => (x.Clone()));
            clone.Cutscene = new List<ZCutscene>();
            clone.SceneHeaders = new List<ZSceneHeader>();
            clone.SegmentFunctions = new List<ZSegmentFunction>();
            clone.SegmentFunctions.Add(new ZSegmentFunction());
            


            return clone;

        }

        public class DMAFile
        {
            public uint startoffset = 0;
            public uint endoffset = 0;
            public string name = "";
        }

        public class LODgroup
        {
            public int id = 0;
            public uint lodgroupoffset = 0;
            public List<LODgroupDlist> dlists = new List<LODgroupDlist>();

            public LODgroup(int id)
            {
                this.id = id;
             //   this.lodgroupoffset = lodgroupoffset;
            }
            



        }

        public class LODgroupDlist
        {
            public NDisplayList dlist;
            public int distance;
            public int X;
            public int Y;
            public int Z;

            public LODgroupDlist(NDisplayList dlist, int distance)
            {
                this.dlist = dlist;
                this.distance = distance;
            }

        }
    }
}
