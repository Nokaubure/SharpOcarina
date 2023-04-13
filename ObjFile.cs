/*
 * ObjFile.cs / Simple(?) Wavefront .obj loader and renderer
 * Class for loading and rendering Wavefront .obj files via OpenTK
 * Written in 2011 by xdaniel
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

using TexLib;
using TgaDecoderTest;

namespace SharpOcarina
{
    public class ObjFile
    {
        #region Constructors

        public ObjFile() { }

        public ObjFile(string Filename)
            : this(Filename, false) { }

        public ObjFile(string Filename, bool IgnoreMats)
        {
            _IgnoreMaterials = IgnoreMats;
            TexUtil.InitTexturing();
            if (Filename != string.Empty)
            {
                if (Filename.Contains(".dae"))
                    ParseDae(Filename);
                else if (Filename.Contains(".zmap"))
                {}
                else
                    ParseObj(Filename);
            }
        }

        #endregion

        #region Element Classes

        public class Triangle
        {
            public string MaterialName;
            public int[] VertIndex;
            public int[] VertColor;
            public int[] TexCoordIndex;
            public int[] NormalIndex;

            public Triangle()
            {
                VertIndex = new int[3];
                VertColor = new int[] { 1, 1, 1 };
                TexCoordIndex = new int[3];
                NormalIndex = new int[3];
            }

            public Triangle(int[] _VertIndex, int[] _TexCoordIndex, int[] _NormalIndex)
            {
                MaterialName = string.Empty;
                VertIndex = _VertIndex; TexCoordIndex = _TexCoordIndex; NormalIndex = _NormalIndex;
                VertColor = new int[] { 1, 1, 1};
            }

            public Triangle(int[] _VertIndex, int[] _TexCoordIndex, int[] _NormalIndex, int[] _ColorIndex)
            {
                MaterialName = string.Empty;
                VertIndex = _VertIndex; TexCoordIndex = _TexCoordIndex; NormalIndex = _NormalIndex; VertColor = _ColorIndex;
               
            }

            public Triangle(string _MaterialName, int[] _VertIndex, int[] _TexCoordIndex, int[] _NormalIndex)
            {
                MaterialName = _MaterialName;
                VertIndex = _VertIndex;
                TexCoordIndex = _TexCoordIndex;
                if (TexCoordIndex[0] == -1) TexCoordIndex[0]++;
                if (TexCoordIndex[1] == -1) TexCoordIndex[1]++;
                if (TexCoordIndex[2] == -1) TexCoordIndex[2]++;
                NormalIndex = _NormalIndex;
                VertColor = new int[] { 1, 1, 1 };
            }


        }

        public class Vertex
        {
            public double X = 0.0f, Y = 0.0f, Z = 0.0f, W = 0.0f;
            public Vector3d VN = new Vector3d();

            public Vertex() { }

            public Vertex(double _X, double _Y, double _Z)
            {
                X = _X; Y = _Y; Z = _Z; W = 0.0f;
            }

            public Vertex(double _X, double _Y, double _Z, double _W)
            {
                X = _X; Y = _Y; Z = _Z; W = _W;
            }

            public Vector3d ToVector3d()
            {
                return new Vector3d(X,Y,Z);
            }
        }

        public class VertexColor
        {
            public double R = 1.0f, G = 1.0f, B = 1.0f, A = 1.0f;

            public VertexColor() { }

            public VertexColor(double _R, double _G, double _B, double _A)
            {
                R = _R; G = _G; B = _B; A = _A;
            }
        }


        public class TextureCoord
        {
            public double U = 0.0f, V = 0.0f, W = 0.0f;

            public TextureCoord() { }

            public TextureCoord(double _U, double _V)
            {
                U = _U; V = _V; W = 0.0f;
            }
            public TextureCoord(double _U, double _V, double _W)
            {
                U = _U; V = _V; W = _W;
            }
        }

        public class Normal
        {
            public double X = 0.0f, Y = 0.0f, Z = 0.0f;

            public Normal() { }

            public Normal(double _X, double _Y, double _Z)
            {
                X = _X; Y = _Y; Z = _Z;
            }
        }


        public class Material
        {
            public string Name;
            public float[] Ka, Kd, Ks;
            public float Tr;
            public int illum;
            public string map_Ka, map_Kd, map_Ks, map_d, map_bump;

            [XmlIgnore]
            public Bitmap TexImage;
            [XmlIgnore]
            public int Width, Height;
            [XmlIgnore]
            public int GLID;

            [XmlIgnore]
            public bool ForceRGBA;

            [XmlIgnore]
            public string ForcedFormat = "";

            public Material()
            {
                Ka = new float[] { 0.2f, 0.2f, 0.2f };
                Kd = new float[] { 0.8f, 0.8f, 0.8f };
                Ks = new float[] { 1.0f, 1.0f, 1.0f };
                Tr = 1.0f;
                illum = 0;

                ForceRGBA = false;
            }

            public string DisplayName
            {
                get { return (map_Kd == null ? "None" : map_Kd.Contains(Path.DirectorySeparatorChar) ? map_Kd.Substring(map_Kd.LastIndexOf(Path.DirectorySeparatorChar) +1) : map_Kd); }
            }

            public Material Clone()
            {
                Material clone = (Material)this.MemberwiseClone();

                return clone;

            }

            // public bool ShouldSerializeTexImage()
        }

        public class Group
        {
            public string Name;

            [XmlIgnore]
            public int GLID;

            [XmlIgnore]
            public uint TintAlpha = 0xFFFFFFFF;
            [XmlIgnore]
            public int TileS = 0, TileT = 0, PolyType = 0;
            [XmlIgnore]
            public bool BackfaceCulling = true;
            [XmlIgnore]
            public bool Animated = false;
            [XmlIgnore]
            public bool Metallic = false;
            [XmlIgnore]
            public bool EnvColor = false;
            [XmlIgnore]
            public bool Decal = false;
            [XmlIgnore]
            public bool IgnoreFog = false;
            [XmlIgnore]
            public bool SmoothRGBAEdges = false;
            [XmlIgnore]
            public bool Pixelated = false;
            [XmlIgnore]
            public bool Billboard = false;
            [XmlIgnore]
            public bool TwoAxisBillboard = false;
            [XmlIgnore]
            public bool ReverseLight = false;
            [XmlIgnore]
            public int MultiTexMaterial = -1, ShiftS = 0, ShiftT = 0;
            [XmlIgnore]
            public int BaseShiftS = 0, BaseShiftT = 0, AnimationBank = 8;
            [XmlIgnore]
            public int LodGroup = 0, LodDistance = 0;
            [XmlIgnore]
            public uint MultiTexAlpha = 0xFFFFFFFF;
            [XmlIgnore]
            public bool LOD = false;
            [XmlIgnore]
            public bool AlphaMask = false;
            [XmlIgnore]
            public bool RenderLast = false;
            [XmlIgnore]
            public bool VertexNormals = false;
            [XmlIgnore]
            public bool Custom = false;
            [XmlIgnore]
            public ulong[] CustomDL = new ulong[4];

            private List<Triangle> _Tris = new List<Triangle>();
            
            public List<Triangle> Triangles
            {
                get { return _Tris; }
                set { _Tris = value; }
            }

            public string DisplayName
            {
                get { return Name; }
            }

            public Group Clone()
            {
                return (Group)this.MemberwiseClone();
            }
        }

        #endregion

        #region Element Lists

        private List<Group> _Groups = new List<Group>();
        private List<Vertex> _Verts = new List<Vertex>();
        private List<VertexColor> _VertColors = new List<VertexColor>();
        private List<TextureCoord> _TexCoords = new List<TextureCoord>();
        private List<Normal> _Norms = new List<Normal>();
        private List<Material> _Mats = new List<Material>();
        private List<String> _AdditionalTextures = new List<String>();
        private List<List<int>> _Islands = new List<List<int>>();

        public List<Group> Groups
        {
            get { return _Groups; }
            set { _Groups = value; }
        }

        public List<Vertex> Vertices
        {
            get { return _Verts; }
        }

        public List<TextureCoord> TextureCoordinates
        {
            get { return _TexCoords; }
        }

        public List<Normal> Normals
        {
            get { return _Norms; }
        }

        public List<Material> Materials
        {
            get { return _Mats; }
        }

        public List<VertexColor> VertexColors
        {
            get { return _VertColors; }
        }

        public List<string> AdditionalTextures
        {
            get { return _AdditionalTextures; }
        }

        #endregion

        #region Other Variables

        private string _BasePath = string.Empty;

        [XmlIgnore]
        public string BasePath
        {
            get { return _BasePath; }
            set { _BasePath = value; }
        }

        private string Line = string.Empty;

        private char[] TokenSeperator = { ' ', '\t' };
        private char[] TokenValSeperator = { '/' };
        public static List<string> ValidImageTypes = new List<string>(new string[] { ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".tiff", ".tif",".tga"});

        private string MtlFilename = string.Empty;
        private string CurrentMtlName = string.Empty;

        private double X, Y, Z, U, V, W, R, G, B, A;

        private bool GroupIsOpen;
        private bool MaterialIsOpen;

        private bool _MaterialLighting = false;

        [XmlIgnore]
        public bool MaterialLighting
        {
            get { return _MaterialLighting; }
            set { _MaterialLighting = value; }
        }

        private bool _IgnoreMaterials = false;

        [XmlIgnore]
        public bool IgnoreMaterials
        {
            get { return _IgnoreMaterials; }
            set { _IgnoreMaterials = value; }
        }

        #endregion

        #region Loading & Setup Functions

        public void Load(string Filename)
        {
            if (Filename.Contains(".dae"))
                ParseDae(Filename);
            else
                ParseObj(Filename);
        }

        #endregion

        #region Model Parser

        private void ParseObj(string Filename)
        {
            _VertColors.Add(new VertexColor(1, 1, 1, 1));
            StreamReader SR = File.OpenText(Filename);

            Group NewGroup = new Group();
            GroupIsOpen = false;


            List<int> nocollisionvertexfix = new List<int>();
            List<int> nocollisionnormalfix = new List<int>();

            while ((Line = SR.ReadLine()) != null)
            {
                Line = Line.TrimStart(TokenSeperator);
                if (Line == string.Empty) continue;

                string[] Tokenized = Line.Split(TokenSeperator, StringSplitOptions.RemoveEmptyEntries);

                switch (Tokenized[0])
                {
                    case "#":
                        /* Comment */
                        break;

                    case "g":
                        /* Group */
                        if (GroupIsOpen == true && NewGroup.Triangles.Count > 0)
                            AddGroup(NewGroup);

                        GroupIsOpen = true;
                        NewGroup = FindGroup(Line.Substring(Line.IndexOf(' ') + 1));
                        if (NewGroup == null) NewGroup = new Group();
                        NewGroup.Name = Line.Substring(Line.IndexOf(' ') + 1);
                        break;

                    case "mtllib":
                        /* Material lib reference */
                        MtlFilename = Line.Substring(Line.IndexOf(' ') + 1);
                        //ParseMtl(Filename.Substring(0, Filename.LastIndexOf('\\')) + "\\" + MtlFilename);
                        ParseMtl(Path.IsPathRooted(MtlFilename) == true ? Path.GetFileName(MtlFilename) : Path.GetDirectoryName(Path.GetFullPath(Filename)) + Path.DirectorySeparatorChar + Path.GetFileName(MtlFilename));
                        break;

                    case "v":
                        /* Vertex */

                        if (IgnoreMaterials && NewGroup.Name != null && NewGroup.Name.ToLower().Contains("#nocollision"))
                        {
                            nocollisionvertexfix.Add(_Verts.Count);
                       
                          continue;
                        }

                        X = Y = Z = W = 0;
                        double.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out X);
                        double.TryParse(Tokenized[2], NumberStyles.Float, CultureInfo.InvariantCulture, out Y);
                        double.TryParse(Tokenized[3], NumberStyles.Float, CultureInfo.InvariantCulture, out Z);



                        if (Tokenized.Length == 5)
                            double.TryParse(Tokenized[4], NumberStyles.Float, CultureInfo.InvariantCulture, out W);

                        _Verts.Add(new Vertex(X, Y, Z, W));
                        if (Math.Abs(X) > 32767 || Math.Abs(Y) > 32767 || Math.Abs(Z) > 32767)
                        {
                            MessageBox.Show("Vertex can't be further than 32767 units on both sides, try making a smaller map! cancelling import",
   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    case "vc":
                        /* Vertex Color */

                        if (IgnoreMaterials && NewGroup.Name != null && NewGroup.Name.ToLower().Contains("#nocollision"))
                            continue;

                        R = G = B = A = 0;
                        double.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out R);
                        double.TryParse(Tokenized[2], NumberStyles.Float, CultureInfo.InvariantCulture, out G);
                        double.TryParse(Tokenized[3], NumberStyles.Float, CultureInfo.InvariantCulture, out B);
                        double.TryParse(Tokenized[4], NumberStyles.Float, CultureInfo.InvariantCulture, out A);

                        _VertColors.Add(new VertexColor(R, G, B, A));
                        break;

                    case "vt":
                        /* Texture coordinates */

                        if (IgnoreMaterials && NewGroup.Name != null && NewGroup.Name.ToLower().Contains("#nocollision"))
                            continue;

                        U = V = W = 0;
                        double.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out U);
                        double.TryParse(Tokenized[2], NumberStyles.Float, CultureInfo.InvariantCulture, out V);
                        if (Tokenized.Length == 4)
                            double.TryParse(Tokenized[3], NumberStyles.Float, CultureInfo.InvariantCulture, out W);

                        V -= 1; // clamp fix

                       // if (_Islands.Count == 0) _Islands.Add(new List<int>());
                        _TexCoords.Add(new TextureCoord(U, -V, W));
                     //   _Islands[_Islands.Count-1].Add(_TexCoords.Count-1);
                        break;

                    case "vn":
                        /* Normals */

                        if (IgnoreMaterials && NewGroup.Name != null && NewGroup.Name.ToLower().Contains("#nocollision"))
                        {
                            nocollisionnormalfix.Add(_Norms.Count);

                            continue;
                        }

                        X = Y = Z = 0;
                        double.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out X);
                        double.TryParse(Tokenized[2], NumberStyles.Float, CultureInfo.InvariantCulture, out Y);
                        double.TryParse(Tokenized[3], NumberStyles.Float, CultureInfo.InvariantCulture, out Z);

                        _Norms.Add(new Normal(X, Y, Z));

                        break;

                    case "usemtl":
                        /* Material to use */
                       // _Islands.Add(new List<int>());
                        CurrentMtlName = Tokenized[1].Replace("DISPLAY","");
                        break;

                    case "fc":
                        // link colors to triangles

                        if (IgnoreMaterials && NewGroup.Name != null && NewGroup.Name.ToLower().Contains("#nocollision"))
                            continue;

                        X = Y = Z = 0;
                        double.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out X);
                        double.TryParse(Tokenized[2], NumberStyles.Float, CultureInfo.InvariantCulture, out Y);
                        double.TryParse(Tokenized[3], NumberStyles.Float, CultureInfo.InvariantCulture, out Z);

                        X += 1;
                        Y += 1;
                        Z += 1;

                        NewGroup.Triangles[NewGroup.Triangles.Count - 1].VertColor = new int[] { (int)X, (int)Y, (int)Z, 0 };

                        break;

                    case "f":
                        /* Face/triangle */

                        if (IgnoreMaterials && NewGroup.Name != null && NewGroup.Name.ToLower().Contains("#nocollision"))
                            continue;

                        int[] VIndex = new int[16];
                        int[] TIndex = new int[16];
                        int[] NIndex = new int[16];

                        // Triangulate face
                        for (int i = 0; i < Tokenized.Length-1; i++)
                        {
                            string[] TokenizedVals = Tokenized[i + 1].Split(TokenValSeperator);
                            int[] VLocal = new int[3];
                            int[] TLocal = new int[3];
                            int[] NLocal = new int[3];

                            int.TryParse(TokenizedVals[0], out VIndex[i]);
                            int.TryParse(TokenizedVals[1], out TIndex[i]);
                            if (TokenizedVals.Length == 3)
                                int.TryParse(TokenizedVals[2], out NIndex[i]);

                            VIndex[i] -= 1;
                            TIndex[i] -= 1;
                            NIndex[i] -= 1;

                            // Last vertex of triangle, or index to next point in the face (e.g. quad)
                            if(i >= 2){
                                if (VIndex[0 + (i - 2)] != -1 && VIndex[1 + (i - 2)] != -1 && VIndex[2 + (i - 2)] != -1)
                                {
                                    VLocal[0] = VIndex[0];     TLocal[0] = TIndex[0];     NLocal[0] = NIndex[0];
                                    VLocal[1] = VIndex[i - 1]; TLocal[1] = TIndex[i - 1]; NLocal[1] = NIndex[i - 1];
                                    VLocal[2] = VIndex[i];     TLocal[2] = TIndex[i];     NLocal[2] = NIndex[i];


                                    if (IgnoreMaterials)
                                    {
                                        foreach(int val in nocollisionvertexfix)
                                        {
                                            for(int y = 0; y < 3; y++)
                                            {
                                                if (VLocal[y] >= val) VLocal[y]--;
                                            }

                                        }

                                        foreach (int val in nocollisionnormalfix)
                                        {
                                            for (int y = 0; y < 3; y++)
                                            {
                                                if (NLocal[y] >= val) NLocal[y]--;
                                            }
                                        }
                                    }
                                      

                                    NewGroup.Triangles.Add(new Triangle(CurrentMtlName, VLocal, TLocal, NLocal));
                                }
                            }
                        }
                        
                        break;                }
            }
            if (GroupIsOpen == true)
                AddGroup(NewGroup);

            SR.Close();

            if(_VertColors.Count == 0)
            {
                _VertColors.Add(new VertexColor());
            }

            if (_TexCoords.Count == 0)
            {
                _TexCoords.Add(new TextureCoord());
            }

            //    FixUv();

            if (IgnoreMaterials)
            {
                AddDoorMeshes();
            }

            Prepare(_Groups);
        }

        private void ParseDae(string filename)
        {
            _VertColors.Add(new VertexColor(1, 1, 1, 1));
            _VertColors.Add(new VertexColor(1, 1, 1, 1));

            XmlDocument doc = new XmlDocument();

         //   FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            using (XmlTextReader tr = new XmlTextReader(filename))
            {
                tr.Namespaces = false;
                doc.Load(tr);
            }


         
         


            XmlNodeList nodes = doc.SelectNodes("COLLADA/library_images/image");


            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    string id = nodeAtt["id"].Value;
                    foreach (XmlNode node2 in node)
                    {
                        if (node2.Name == "init_from")
                        {
                            Material mat = new Material();
                            string path = node2.InnerText;
                            path = path.Replace("%20", " ");
                            if (!File.Exists(path))
                            {
                                path = Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar + path;
                                if (!File.Exists(path) && !MainForm.settings.DisableTextureWarnings)
                                {
                                    MessageBox.Show("Texture " + node2.InnerText + " not found",
                                                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            mat.map_Kd = path;
                            mat.map_Ka = path;
                            mat.Name = id;
                            Materials.Add(mat);
                        }
                    }
                }


            int vertexstack = 0;
            int normalstack = 0;
            int texcoordstack = 0;
            int colorstack = 2;

            nodes = doc.SelectNodes("COLLADA/library_geometries/geometry");

            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    string vertexarray = "", normalarray = "", texcoordarray = "", colorarray = "";
                    List<string> triangleids = new List<string>();
                    List<string> materialids = new List<string>();
                    if (node.Name == "geometry")
                    {
                        Group NewGroup = new Group();
                        XmlAttributeCollection nodeAtt = node.Attributes;
                        NewGroup.Name = nodeAtt["name"].Value;
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (node2.Name == "mesh")
                            {
                                foreach (XmlNode node3 in node2.ChildNodes)
                                {
                                    if (node3.Name == "vertices")
                                    {
                                        foreach (XmlNode node4 in node3.ChildNodes)
                                        {
                                            if (node4.Name == "input")
                                            {
                                                nodeAtt = node4.Attributes;
                                                if (nodeAtt["semantic"].Value == "POSITION")
                                                {
                                                    vertexarray = nodeAtt["source"].Value.Replace("#", "");
                                                }
                                            }
                                        }
                                    }
                                    else if (node3.Name == "triangles")
                                    {
                                        nodeAtt = node3.Attributes;
                                        if (nodeAtt["material"] != null)
                                        {
                                            bool found = false;
                                            foreach (Material mat in Materials)
                                            {
                                                if (mat.Name == nodeAtt["material"].Value.Replace("-material", ""))
                                                {
                                                    materialids.Add(mat.Name);
                                                    found = true;
                                                    break;
                                                }

                                            }
                                            if (!found)
                                            {
                                                Console.WriteLine("mat  " + nodeAtt["material"].Value.Replace("-material", "") + " NOT FOUND");
                                                materialids.Add("");
                                            }

                                        }
                                        else
                                        {
                                            materialids.Add("");
                                        }
                                        foreach (XmlNode node4 in node3.ChildNodes)
                                        {
                                            if (node4.Name == "input")
                                            {
                                                nodeAtt = node4.Attributes;
                                                if (nodeAtt["semantic"].Value == "NORMAL")
                                                {
                                                    normalarray = nodeAtt["source"].Value.Replace("#", "");
                                                }
                                                else if (nodeAtt["semantic"].Value == "TEXCOORD")
                                                {
                                                    texcoordarray = nodeAtt["source"].Value.Replace("#", "");
                                                }
                                                else if (nodeAtt["semantic"].Value == "COLOR")
                                                {
                                                    colorarray = nodeAtt["source"].Value.Replace("#", "");
                                                }
                                            }
                                            else if (node4.Name == "p")
                                            {
                                                triangleids.Add(node4.InnerText);
                                            }

                                        }
                                    }
                                }


                                foreach (XmlNode node3 in node2.ChildNodes)
                                {
                                    if (node3.Name == "source")
                                    {
                                        nodeAtt = node3.Attributes;
                                        string arraytype = nodeAtt["id"].Value;
                                        foreach (XmlNode node4 in node3.ChildNodes)
                                        {
                                            if (node4.Name == "float_array")
                                            {
                                                if (arraytype == vertexarray)
                                                {
                                                    string[] values = node4.InnerText.Split(' ');
                                                    if (node4.InnerText == "") continue;
                                                    for(int i = 0; i < values.Length; i+=3)
                                                    {
                                                        _Verts.Add(new Vertex(Convert.ToDouble(values[i], CultureInfo.InvariantCulture), Convert.ToDouble(values[i+2], CultureInfo.InvariantCulture), -Convert.ToDouble(values[i+1], CultureInfo.InvariantCulture)));
                                                        if (Math.Abs(_Verts[_Verts.Count-1].X) > 32767 || Math.Abs(_Verts[_Verts.Count - 1].Y) > 32767 || Math.Abs(_Verts[_Verts.Count - 1].Z) > 32767)
                                                        {
                                                            MessageBox.Show("Vertex can't be further than 32767 units on both sides, try making a smaller map! cancelling import",
                                                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            return;
                                                        }
                                                    }
                                                }
                                                else if (arraytype == normalarray)
                                                {
                                                    string[] values = node4.InnerText.Split(' ');
                                                    if (node4.InnerText == "") continue;
                                                    for (int i = 0; i < values.Length; i += 3)
                                                    {
                                                        _Norms.Add(new Normal(Convert.ToDouble(values[i], CultureInfo.InvariantCulture), Convert.ToDouble(values[i + 1], CultureInfo.InvariantCulture), Convert.ToDouble(values[i + 2], CultureInfo.InvariantCulture)));
                                                    }
                                                }
                                                else if (arraytype == texcoordarray)
                                                {
                                                    string[] values = node4.InnerText.Split(' ');
                                                    if (node4.InnerText == "") continue;
                                                    for (int i = 0; i < values.Length; i += 2)
                                                    {
                                                        _TexCoords.Add(new TextureCoord(Convert.ToDouble(values[i], CultureInfo.InvariantCulture), -Convert.ToDouble(values[i + 1], CultureInfo.InvariantCulture)));
                                                    }
                                                }
                                                else if (arraytype == colorarray)
                                                {
                                                    string[] values = node4.InnerText.Split(' ');
                                                    if (node4.InnerText == "") continue;
                                                    for (int i = 0; i < values.Length; i += 4)
                                                    {
                                                        _VertColors.Add(new VertexColor(Convert.ToDouble(values[i], CultureInfo.InvariantCulture), Convert.ToDouble(values[i + 1], CultureInfo.InvariantCulture), Convert.ToDouble(values[i + 2], CultureInfo.InvariantCulture), Convert.ToDouble(values[i + 3], CultureInfo.InvariantCulture)));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                for (int y = 0; y < triangleids.Count; y++)
                                {
                                    string[] index = triangleids[y].Split(' ');
                                    int incr = (colorarray == "") ? 3 : 4;
                                    //   int count = (colorarray == "") ? index.Length / 3 : index.Length / 4;
                                    Triangle tri = new Triangle();
                                    tri.MaterialName = materialids[y];
                                    int t = 0;

                                    for (int i = 0; i < index.Length; i += incr)
                                    {
                                        if (t == 3 && i != 0)
                                        {
                                          //  if (incr == 3) tri.VertColor = new int[] { 1, 1, 1 };
                                            NewGroup.Triangles.Add(tri);
                                            tri = new Triangle();
                                            tri.MaterialName = materialids[y];
                                            t = 0;
                                        }
                                        tri.VertIndex[i % 3] = Convert.ToInt32(index[i]) + vertexstack;
                                        tri.NormalIndex[i % 3] = Convert.ToInt32(index[i + 1]) + normalstack;
                                        tri.TexCoordIndex[i % 3] = Convert.ToInt32(index[i + 2]) + texcoordstack;
                                        if (incr == 4) tri.VertColor[i % 3] = Convert.ToInt32(index[i + 3]) + colorstack +1;
                                        t++;

                                    }
                                    NewGroup.Triangles.Add(tri);
                                }

                            }
                        }
                        CalculateVertexNormals(ref NewGroup);
                        _Groups.Add(NewGroup);
                        vertexstack = _Verts.Count;
                        colorstack = _VertColors.Count;
                        normalstack = _Norms.Count;
                        texcoordstack = _TexCoords.Count;
                    }



                   // XmlAttributeCollection nodeAtt = node.Attributes;
                    
                }





            if (_VertColors.Count == 0)
            {
                _VertColors.Add(new VertexColor());
            }

            if (_TexCoords.Count == 0)
            {
                _TexCoords.Add(new TextureCoord());
            }

            //    FixUv();

            if (IgnoreMaterials)
            {
                AddDoorMeshes();
            }

            Prepare(_Groups);
        }

        private void AddDoorMeshes()
        {
            List<Group> NewGroups = new List<Group>();
            foreach (Group group in _Groups)
            {
                if (group.Name.ToLower().Contains("#door") || group.Name.ToLower().Contains("tag_door"))
                {
                    Group groupF = group.Clone();
                    Group groupB = group.Clone();
                    groupF.Name = groupF.Name.Replace("#door", "Fdoor").Replace("#Door", "Fdoor").Replace("TAG_door", "Fdoor");
                    groupB.Name = groupB.Name.Replace("#door", "Bdoor").Replace("#Door", "Bdoor").Replace("TAG_door", "Bdoor");

                    Console.WriteLine(groupF.Name);
                    Console.WriteLine(groupB.Name);

                    groupF.Triangles = new List<Triangle>();
                    groupB.Triangles = new List<Triangle>();

                    Dictionary<int, int> vertexcopyF = new Dictionary<int, int>();
                    Dictionary<int, int> vertexcopyB = new Dictionary<int, int>();

                    foreach (Triangle tri in group.Triangles)
                    {
                        Triangle triF = new Triangle(new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 });
                        Triangle triB = new Triangle(new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 });

                        for (int i = 0; i < 3; i++)
                        {
                            if (!vertexcopyF.ContainsKey(tri.VertIndex[i]))
                            {
                                vertexcopyF.Add(tri.VertIndex[i], _Verts.Count);

                                ObjFile.Normal normal = ObjFile.GenerateNormal(Vertices[tri.VertIndex[0]], Vertices[tri.VertIndex[1]], Vertices[tri.VertIndex[2]]);
                                double RotY = (Math.Atan2(normal.X, normal.Z));

                                Vertex v = new Vertex(

                                    Vertices[tri.VertIndex[i]].X + (float)Math.Sin(RotY) * 10f,
                                    Vertices[tri.VertIndex[i]].Y,
                                    Vertices[tri.VertIndex[i]].Z + (float)Math.Cos(RotY) * 10f

                                    );
                                triF.VertIndex[i] = _Verts.Count;

                                _Verts.Add(v);
                            }
                            else
                                triF.VertIndex[i] = vertexcopyF[tri.VertIndex[i]];


                            if (!vertexcopyB.ContainsKey(tri.VertIndex[i]))
                            {
                                vertexcopyB.Add(tri.VertIndex[i], _Verts.Count);

                                ObjFile.Normal normal = ObjFile.GenerateNormal(Vertices[tri.VertIndex[0]], Vertices[tri.VertIndex[1]], Vertices[tri.VertIndex[2]]);
                                double RotY = (Math.Atan2(normal.X, normal.Z));

                                if (double.IsNaN(RotY)) continue;

                                Vertex v = new Vertex(

                                    Vertices[tri.VertIndex[i]].X - (float)Math.Sin(RotY) * 10f,
                                    Vertices[tri.VertIndex[i]].Y,
                                    Vertices[tri.VertIndex[i]].Z - (float)Math.Cos(RotY) * 10f

                                    );
                                triB.VertIndex[i] = _Verts.Count;

                                _Verts.Add(v);

                            }
                            else
                                triB.VertIndex[i] = vertexcopyB[tri.VertIndex[i]];
                        }
                        int tmp = triB.VertIndex[2];
                        triB.VertIndex[2] = triB.VertIndex[0];
                        triB.VertIndex[0] = tmp;

                        groupF.Triangles.Add(triF);
                        groupB.Triangles.Add(triB);
                    }

                    NewGroups.Add(groupF);
                    NewGroups.Add(groupB);
                }
            }

            _Groups.AddRange(NewGroups);
        }

        private void CalculateVertexNormalsA(ref Group Grp)
        {
            if (_Norms == null || _Norms.Count == 0) return;

            for (int i = 0; i < _Verts.Count; i++)
            {
                foreach (Triangle Tri in Grp.Triangles)
                {
                    int x = -1;
                    for(int y = 0; y <= 2; y++)
                    {
                        if (Tri.VertIndex[y] == i)
                        { x = y; break; }
                    }
                    if (x == -1) continue;
                    _Verts[i].VN.X += (_Norms[Tri.NormalIndex[x]].X );
                    _Verts[i].VN.Y += (_Norms[Tri.NormalIndex[x]].Y );
                    _Verts[i].VN.Z += (_Norms[Tri.NormalIndex[x]].Z );
                    _Verts[i].VN.Normalize();

                }
            }
        }

        private void CalculateVertexNormals(ref Group Grp)
        {
            if (_Norms == null || _Norms.Count == 0) return;

            for (int i = 0; i < _Verts.Count; i++)
            {
                foreach (Triangle Tri in Grp.Triangles)
                {
                    if (Tri.VertIndex[0] == i || Tri.VertIndex[1] == i || Tri.VertIndex[2] == i)
                    {
                        _Verts[i].VN.X += (_Norms[Tri.NormalIndex[0]].X + _Norms[Tri.NormalIndex[1]].X + _Norms[Tri.NormalIndex[2]].X);
                        _Verts[i].VN.Y += (_Norms[Tri.NormalIndex[0]].Y + _Norms[Tri.NormalIndex[1]].Y + _Norms[Tri.NormalIndex[2]].Y);
                        _Verts[i].VN.Z += (_Norms[Tri.NormalIndex[0]].Z + _Norms[Tri.NormalIndex[1]].Z + _Norms[Tri.NormalIndex[2]].Z);
                        _Verts[i].VN.Normalize();
                    }
                }
            }
        }

        private void AddGroup(Group GroupToAdd)
        {
            if (IgnoreMaterials && GroupToAdd.Name.ToLower().Contains("#nocollision"))
            {
                Console.WriteLine("Skipping #NoCollision!");
                return;
            }
            CalculateVertexNormals(ref GroupToAdd);
            GroupIsOpen = false;
            if (FindGroup(GroupToAdd.Name) == null)
            {
                _Groups.Add(GroupToAdd);
            }
        }

        private Group FindGroup(string name)
        {
            foreach(Group group in _Groups)
            {
                if (group.Name == name) return group;
            }
            return null;
        }

        #endregion

        #region Material Parser

        private void ParseMtl(string Filename)
        {
            if (_IgnoreMaterials == true) return;

            //_BasePath = Filename.Substring(0, Filename.LastIndexOf('\\')) + "\\";
            _BasePath = Path.GetDirectoryName(Filename) + Path.DirectorySeparatorChar;

            StreamReader SR = File.OpenText(Filename);

            Material NewMaterial = null;
            MaterialIsOpen = false;

            while ((Line = SR.ReadLine()) != null)
            {
                Line = Line.TrimStart(TokenSeperator);
                if (Line == string.Empty) continue;

                string[] Tokenized = Line.Split(TokenSeperator, StringSplitOptions.RemoveEmptyEntries);

                switch (Tokenized[0])
                {
                    case "#":
                        /* Comment */
                        break;

                    case "newmtl":
                        /* New material */
                        AddMaterial(NewMaterial);

                        MaterialIsOpen = true;
                        NewMaterial = new Material();
                        NewMaterial.Name = Tokenized[1];
                        break;

                    case "Ka":
                        NewMaterial.Ka = new float[3];

                        float.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Ka[0]);
                        float.TryParse(Tokenized[2], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Ka[1]);
                        float.TryParse(Tokenized[3], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Ka[2]);
                        break;

                    case "Kd":
                        NewMaterial.Kd = new float[3];

                        float.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Kd[0]);
                        float.TryParse(Tokenized[2], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Kd[1]);
                        float.TryParse(Tokenized[3], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Kd[2]);
                        break;

                    case "Ks":
                        NewMaterial.Ks = new float[3];

                        float.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Ks[0]);
                        float.TryParse(Tokenized[2], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Ks[1]);
                        float.TryParse(Tokenized[3], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Ks[2]);
                        break;

                    case "Tr":
                    case "d":
                        float.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.Tr);
                        break;

                    case "illum":
                        int.TryParse(Tokenized[1], NumberStyles.Float, CultureInfo.InvariantCulture, out NewMaterial.illum);
                        break;

                    case "map_Ka":
                    case "mapKa":
                        NewMaterial.map_Ka = Line.Substring(Line.IndexOf(' ') + 1);
                        break;

                    case "map_Kd":
                    case "mapKd":
                        NewMaterial.map_Kd = Line.Substring(Line.IndexOf(' ') + 1);
                        break;

                    case "map_Ks":
                    case "mapKs":
                        NewMaterial.map_Ks = Line.Substring(Line.IndexOf(' ') + 1);
                        break;

                    case "map_d":
                        NewMaterial.map_d = Line.Substring(Line.IndexOf(' ') + 1);
                        break;

                    case "bump":
                    case "map_bump":
                        NewMaterial.map_bump = Line.Substring(Line.IndexOf(' ') + 1);
                        break;
                }
            }

            AddMaterial(NewMaterial);
        }

        private void AddMaterial(Material MatToAdd)
        {
            if (MaterialIsOpen == true)
            {
                /* If map_Ka is empty, set it to map_Kd */
                if (MatToAdd.map_Ka == null && MatToAdd.map_Kd != null)
                    MatToAdd.map_Ka = MatToAdd.map_Kd;

                /* Else if map_Kd is empty, set it to map_Ka */
                else if (MatToAdd.map_Kd == null && MatToAdd.map_Ka != null)
                    MatToAdd.map_Kd = MatToAdd.map_Ka;
                
                /* Only add the material if both, map_Ka and map_Kd, aren't empty */
                //if (MatToAdd.map_Ka != null && MatToAdd.map_Kd != null)
                Materials.Add(MatToAdd);

                if (MatToAdd.map_Ks != null && AdditionalTextures.Find(x => x == MatToAdd.map_Ks) == null)
                {
                    string LoadPath = Path.IsPathRooted(MatToAdd.map_Ks) == true ? MatToAdd.map_Ks :  _BasePath + MatToAdd.map_Ks;
                    MatToAdd.map_Ks = Path.GetDirectoryName(LoadPath) + Path.DirectorySeparatorChar + Path.GetFileName(LoadPath);
                    AdditionalTextures.Add(MatToAdd.map_Ks);
                }

                MaterialIsOpen = false;
            }
        }

        #endregion

        #region Model Rendering

        public Material GetMaterial(string Name)
        {
            if (_IgnoreMaterials == true) return null;

            foreach (Material Mat in Materials)
            {
                if (string.Compare(Mat.Name, Name) == 0)
                    return Mat;
            }

            return null;
        }

        public void Prepare(List<Group> TrueGroups)
        {
            Prepare(true, TrueGroups);
        }

        public void Prepare(bool All, List<Group> TrueGroups)
        {
            if (All == true) LoadTextures();
            PrepareDisplayLists(TrueGroups);
        }

        private void LoadTextures()
        {
            string LoadPath = string.Empty;

            for (int i = 0; i < _Mats.Count; i++)
            {
                if (_Mats[i].map_Ka != null)
                {
                    try
                    {
                  

                        if (GL.IsTexture(_Mats[i].GLID) == true) GL.DeleteTexture(_Mats[i].GLID);

                        if (_Mats[i].TexImage != null) _Mats[i].TexImage.Dispose();

                        LoadPath = Path.IsPathRooted(_Mats[i].map_Ka) == true ? _Mats[i].map_Ka : _BasePath + _Mats[i].map_Ka;

                        if (!File.Exists(LoadPath)) throw new FileNotFoundException();

                        if (ValidImageTypes.IndexOf(Path.GetExtension(LoadPath).ToLowerInvariant()) == -1) throw new Exception();

                        if (Path.GetExtension(LoadPath).ToLowerInvariant() == ".tga")
                        {
                            if (!File.Exists(Path.GetDirectoryName(LoadPath) + "\\" + Path.GetFileNameWithoutExtension(LoadPath) + ".png"))
                            {
                                String pdetail = @"/c ndec\tga2png.exe -i " + "\"" + LoadPath + "\"" + " -o " + "\"" + Path.GetDirectoryName(LoadPath) + "\\\"";
                               // Console.WriteLine(Path.GetDirectoryName(LoadPath) + Path.GetFileNameWithoutExtension(LoadPath) + ".png");
                                ProcessStartInfo pcmd = new ProcessStartInfo("cmd.exe");
                                pcmd.Arguments = pdetail;
                                
                                Process cmd = Process.Start(pcmd);
                                cmd.WaitForExit();


                            }
                         //   Console.WriteLine(Path.GetDirectoryName(LoadPath) + "\\" + Path.GetFileNameWithoutExtension(LoadPath) + ".png");
                            _Mats[i].TexImage = BitmapFromFile(Path.GetDirectoryName(LoadPath) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(LoadPath) + ".png");
                        }
                        else
                            _Mats[i].TexImage = BitmapFromFile(LoadPath);



                        _Mats[i].GLID = TexUtil.CreateTextureFromBitmap(_Mats[i].TexImage);
                        _Mats[i].Width = _Mats[i].TexImage.Width;
                        _Mats[i].Height = _Mats[i].TexImage.Height;
                    }
                    catch (FileNotFoundException)
                    {
                        if (!MainForm.settings.DisableTextureWarnings) MessageBox.Show("Texture image " + LoadPath + " not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    catch (Exception)
                    {
                        if (!MainForm.settings.DisableTextureWarnings) MessageBox.Show("Texture image " + LoadPath + " is in " + Path.GetExtension(LoadPath).ToUpperInvariant() + " format and cannot be loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                }
            }
        }

        private void PrepareDisplayLists(List<Group> TrueGroups)
        {

            for (int i = 0; i < TrueGroups.Count; i++)
            {
                if (GL.IsList(TrueGroups[i].GLID) == true) GL.DeleteLists(TrueGroups[i].GLID, 1);

                TrueGroups[i].GLID = GL.GenLists(1);
                GL.NewList(TrueGroups[i].GLID, ListMode.Compile);

                GL.ActiveTexture(TextureUnit.Texture0);
                
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);

                if (TrueGroups[i].BackfaceCulling == true)
                    GL.Enable(EnableCap.CullFace);
                else
                    GL.Disable(EnableCap.CullFace);


                foreach (Triangle Tri in TrueGroups[i].Triangles)
                {
                    Material Mat = GetMaterial(Tri.MaterialName);
                    if (Mat != null)
                    {
                        GL.BindTexture(TextureTarget.Texture2D, Mat.GLID);

                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);

                        if (TrueGroups[i].TileS != 0 || TrueGroups[i].TileT != 0)
                        {
                            if (TrueGroups[i].TileS == 1)
                                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.MirroredRepeatArb);
                            else if (TrueGroups[i].TileS == 2)
                                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);

                            if (TrueGroups[i].TileT == 1)
                                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.MirroredRepeatArb);
                            else if (TrueGroups[i].TileT == 2)
                                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);
                        }

                    }

                    DrawTriangle(Tri, Mat);
                }

                GL.EndList();
            }
        }

        private void FixUv()
        {
            
            //Console.WriteLine("Islands count: " + _Islands.Count);
            Console.WriteLine("Texcoord count " + _TexCoords.Count);

            List<TextureCoord> tx2 = new List<TextureCoord>();
            int tmp = 0;
            int incr = 0;
            foreach (Group g in _Groups)
            {

                int k = g.Triangles.Count;

                for (int i = 0; i < k; i++)
                {
                    incr = 0;
                        tmp = g.Triangles[i].TexCoordIndex[0] - 1;
                    if (tmp < 0) {tmp = 0;}
                        tx2.Add(_TexCoords[tmp]);
                       g.Triangles[i].TexCoordIndex[0] = 3 * i + 1;

                        tmp = g.Triangles[i].TexCoordIndex[1] - 1;
                      if (tmp < 0) tmp = 0;
                        tx2.Add(_TexCoords[tmp]);
                       g.Triangles[i].TexCoordIndex[1] = 3 * i + 2;

                        tmp = g.Triangles[i].TexCoordIndex[2] - 1;
                      if (tmp < 0) tmp = 0;
                        tx2.Add(_TexCoords[tmp]);
                       g.Triangles[i].TexCoordIndex[2] = 3 * i + 3;

                }

                double maxu, maxv, minu, minv, mu, mv;

                for (int i = 0; i < k; i++)
                {
                    maxu = Math.Max(tx2[(i*3)].U, tx2[(i*3) + 1].U);
                    maxu = Math.Max(maxu, tx2[(i*3) + 2].U);

                    maxv = Math.Max(tx2[(i*3)].V, tx2[(i*3) + 1].V);
                    maxv = Math.Max(maxv, tx2[(i*3) + 2].V);

                    minu = Math.Min(tx2[(i*3)].U, tx2[(i*3) + 1].U);
                    minu = Math.Min(minu, tx2[(i*3) + 2].U);

                    minv = Math.Min(tx2[(i*3)].V, tx2[(i*3) + 1].V);
                    minv = Math.Min(minv, tx2[(i*3) + 2].V);

                    mu = (maxu + minu) / 2;
                    mv = (maxv + minv) / 2;

                    mu = Math.Floor(mu);
                    mv = Math.Floor(mv);

                    tx2[(i * 3)].U -= mu;
                    tx2[(i * 3) + 1].U -= mu;
                    tx2[(i * 3) + 2].U -= mu;

                    tx2[(i * 3)].V -= mv;
                    tx2[(i * 3) + 1].V -= mv;
                    tx2[(i * 3) + 2].V -= mv;
                }

            }


            
        }

        private void DrawTriangle(Triangle Tri, Material Mat)
        {
            if (Mat != null)
            {
                /* Normal, textured model rendering */
                if (_MaterialLighting == true)
                {
                    GL.Enable(EnableCap.ColorMaterial);
                    GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, Mat.Ka);
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, Mat.Kd);
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, Mat.Ks);
                }

                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Nearest);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Nearest);

                DrawNormalTriangle(Tri, Mat);
            }
            else
            {
                DrawHighlightedTriangle(Tri, new Color4(1.0f, 0.0f, 0.0f, 0.25f), true);
            }
        }

        private void DrawHighlightedTriangle(Triangle Tri, Color4 Color, bool Outlined)
        {
            /* Setup */
            GL.Disable(EnableCap.Texture2D);
            GL.Enable(EnableCap.PolygonOffsetFill);
            GL.PolygonOffset(-1.0f, -1.0f);

            /* Polygons */
            GL.Color4(Color);
            DrawNormalTriangle(Tri, null);

            /* Outlines */
            if (Outlined == true)
            {
                GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
                GL.LineWidth(3.0f);
                GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);
                DrawNormalTriangle(Tri, null);
            }

            /* Reset */
            GL.LineWidth(1.0f);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.PolygonOffset(0.0f, 0.0f);
        }

        private void DrawNormalTriangle(Triangle Tri, Material Mat)
        {
            GL.Begin(BeginMode.Triangles);

            for (int j = 0; j < 3; j++)
            {
                if (Mat != null)
                {
                    if (TextureCoordinates.Count != 0 && Tri.TexCoordIndex[j] != -1)
                        GL.TexCoord3(
                            TextureCoordinates[Tri.TexCoordIndex[j]].U,
                            TextureCoordinates[Tri.TexCoordIndex[j]].V,
                            TextureCoordinates[Tri.TexCoordIndex[j]].W);
                }
                
                if (Normals.Count != 0 && Tri.NormalIndex[j] != -1)
                {
                    if (Tri.NormalIndex[j] > Normals.Count-1)
                        Console.WriteLine("Normal index out of bounds! " + Tri.NormalIndex[j]);
                    else
                    GL.Normal3(Normals[Tri.NormalIndex[j]].X, Normals[Tri.NormalIndex[j]].Y, Normals[Tri.NormalIndex[j]].Z);
                }

                /* Should never trip!! */
                if (Tri.VertIndex[j] != -1)
                {
                    if (Tri.VertIndex[j] > Vertices.Count - 1)
                        Console.WriteLine("Vertex index out of bounds! " + Tri.VertIndex[j]);
                    else
                        GL.Vertex3(Vertices[Tri.VertIndex[j]].X, Vertices[Tri.VertIndex[j]].Y, Vertices[Tri.VertIndex[j]].Z);
                }
                else
                    throw new Exception("Invalid vertex index detected; should've been filtered out beforehand!");
            }

            GL.End();
        }

        public void Render(Group Grp)
        {
            GL.CallList(Grp.GLID);
        }

        public void Render(int Grp)
        {
            GL.CallList(_Groups[Grp].GLID);
        }

        public void Render()
        {

           
            foreach (Group G in _Groups)
                GL.CallList(G.GLID);
        }

        #endregion



        public static Vector3 RayCollision(Vertex V1, Vertex V2, Vertex V3, Vector3 R1, Vector3 R2, float scale)
        {

            Vector3 P1 = new Vector3((float)V1.X * scale, (float)V1.Y * scale, (float)V1.Z * scale);
            Vector3 P2 = new Vector3((float)V2.X * scale, (float)V2.Y * scale, (float)V2.Z * scale);
            Vector3 P3 = new Vector3((float)V3.X * scale, (float)V3.Y * scale, (float)V3.Z * scale);

            Vector3 normal, IntersectPos;

          //  Vector3 normal = new Vector3((float)norm.X, (float)norm.Y, (float)norm.Z);

            

            // Find Triangle Normal
            normal = Vector3.Cross( P2 - P1, P3 - P1 );
            normal.Normalize(); // not really needed?  Vector3f does this with cross.

            // Find distance from LP1 and LP2 to the plane defined by the triangle
            float Dist1 = Vector3.Dot((R1 - P1),normal);
            float Dist2 = Vector3.Dot((R2 - P1), normal);

            if ( (Dist1 * Dist2) >= 0.0f) { 
                //SFLog(@"no cross"); 
                return new Vector3(-1,-1,-1); 
            } // line doesn't cross the triangle.

            if ( Dist1 == Dist2) { 
                //SFLog(@"parallel"); 
                return new Vector3(-1,-1,-1);  
            } // line and plane are parallel

            // Find point on the line that intersects with the plane
            IntersectPos = R1 + (R2-R1) * ( -Dist1/(Dist2-Dist1) );

            // Find if the interesection point lies inside the triangle by testing it against all edges
            Vector3 vTest;

            vTest = Vector3.Cross(normal, P2 - P1);
            if ( Vector3.Dot(vTest,( IntersectPos-P1)) < 0.0f)  { 
                //SFLog(@"no intersect P2-P1"); 
                return new Vector3(-1,-1,-1); 
            }

            vTest = Vector3.Cross(normal, P3 - P2);
            if (Vector3.Dot(vTest, (IntersectPos - P2)) < 0.0f)
            { 
                //SFLog(@"no intersect P3-P2"); 
                return new Vector3(-1, -1, -1); 
            }

            vTest = Vector3.Cross(normal, P1 - P3);
            if (Vector3.Dot(vTest, (IntersectPos - P1)) < 0.0f)
            { 
                //SFLog(@"no intersect P1-P3"); 
                return new Vector3(-1, -1, -1); 
            }


            return IntersectPos;
        }

        public static Vector3d CalculateCentroid(List<Vector3d> verticies)
        {
            var s = new Vector3d();
            var areaTotal = 0.0;

            var p1 = verticies[0];
            var p2 = verticies[1];

            for (var i = 2; i < verticies.Count; i++)
            {
                var p3 = verticies[i];
                var edge1 = p3 - p1;
                var edge2 = p3 - p2;

                var crossProduct = Vector3d.Cross(edge1, edge2);
                var area = crossProduct.Length / 2;

                s.X += area * (p1.X + p2.X + p3.X) / 3;
                s.Y += area * (p1.Y + p2.Y + p3.Y) / 3;
                s.Z += area * (p1.Z + p2.Z + p3.Z) / 3;

                areaTotal += area;
                p2 = p3;
            }

            var point = new Vector3d
            {
                X = s.X / areaTotal,
                Y = s.Y / areaTotal,
                Z = s.Z / areaTotal
            };


            return point;
        }

        public static Vector3d GenerateNormal(List<Vector3d> points)
        {
            Vector3 P1 = new Vector3((float)points[0].X, (float)points[0].Y , (float)points[0].Z);
            Vector3 P2 = new Vector3((float)points[1].X, (float)points[1].Y, (float)points[1].Z);
            Vector3 P3 = new Vector3((float)points[2].X, (float)points[2].Y, (float)points[2].Z);

            Vector3 normal;
            // Find Triangle Normal
            normal = Vector3.Cross(P2 - P1, P3 - P1);
            normal.Normalize();

            return (Vector3d) normal;
        }

        public static ObjFile.Normal GenerateNormal(ObjFile.Vertex v1, ObjFile.Vertex v2, ObjFile.Vertex v3)
        {
            Vector3 P1 = new Vector3((float)v1.X, (float)v1.Y, (float)v1.Z);
            Vector3 P2 = new Vector3((float)v2.X, (float)v2.Y, (float)v2.Z);
            Vector3 P3 = new Vector3((float)v3.X, (float)v3.Y, (float)v3.Z);

            Vector3 normal;
            // Find Triangle Normal
            normal = Vector3.Cross(P2 - P1, P3 - P1);
            normal.Normalize();

            ObjFile.Normal output = new Normal();
            output.X = normal.X;
            output.Y = normal.Y;
            output.Z = normal.Z;

            return output;
        }


        public void FixNormals()
        {
            foreach (Group g in Groups)
            {
                foreach (Triangle t in g.Triangles)
                {
                    int[] n = t.NormalIndex;
                 //   for loop_i in poly.loop_indices:
             //       lerp_to_vertex_loop_normal(loop_i, n, self.factor)
                }
            }

        }

        public string ConvertToObject(string filename)
        {
            if (!filename.Contains(".obj")) filename += ".obj";
            string mtlfilename = filename.Replace(".obj", ".mtl");

            if (!File.Exists(filename))
                File.Create(filename).Close();

            if (!File.Exists(mtlfilename))
                File.Create(mtlfilename).Close();


            StreamWriter sw = new StreamWriter(filename);

            string data = "# Generated with SharpOcarina\n"
                + "mtlib " + mtlfilename + "\n";

            foreach(Vertex v in Vertices)
            {
                data += "v " + v.X.ToString("0.000000", new CultureInfo("en-US")) + " " + v.Y.ToString("0.000000", new CultureInfo("en-US")) + " " + v.Z.ToString("0.000000", new CultureInfo("en-US")) + "\n";
            }
            foreach(TextureCoord vt in TextureCoordinates)
            {
                data += "vt " + vt.U.ToString("0.000000", new CultureInfo("en-US")) + " " + vt.V.ToString("0.000000", new CultureInfo("en-US")) + " " + vt.W.ToString("0.000000", new CultureInfo("en-US")) + "\n";
            }
            foreach (Normal vn in Normals)
            {
                data += "vn " + vn.X.ToString("0.000000", new CultureInfo("en-US")) + " " + vn.Y.ToString("0.000000", new CultureInfo("en-US")) + " " + vn.Z.ToString("0.000000", new CultureInfo("en-US")) + "\n";
            }


            foreach (Group g in Groups)
            {
                data += "g " + g.Name + "\n"
                    + "usemtl None\n";

                foreach (Triangle f in g.Triangles)
                {
                    data += "f " + (f.VertIndex[0]+1) + "/" + (f.TexCoordIndex[0]+1) + "/" + (f.NormalIndex[0]+1) + " "
                        + (f.VertIndex[1]+1) + "/" + (f.TexCoordIndex[1]+1) + "/" + (f.NormalIndex[1]+1) + " "
                        + (f.VertIndex[2]+1) + "/" + (f.TexCoordIndex[2]+1) + "/" + (f.NormalIndex[2]+1) + "\n";
                }
            }


            sw.Write(data);
            sw.Flush();
            sw.Close();



                //nothing

            StreamWriter sw2 = new StreamWriter(mtlfilename);

            data = "newmtl None\r\nNs 0\r\nKa 0.000000 0.000000 0.000000\r\nKd 0.8 0.8 0.8\r\nKs 0.8 0.8 0.8\r\nd 1\r\nillum 2";

            sw2.Write(data);
            sw2.Flush();
            sw2.Close();

            return filename;
        }

        public static Bitmap BitmapFromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(bytes);
            var img = (Bitmap)Image.FromStream(ms);
            return img;
        }

        public ObjFile Clone()
        {
            return (ObjFile)this.MemberwiseClone();
        }

        //    def lerp_to_vertex_loop_normal(index, value, t):
        //      n = b4w_loops_normals[index]
        //      n = value.lerp(n, 1 - t).normalized()
        //      b4w_loops_normals[index] = (n.x, n.y, n.z)


    }




}
