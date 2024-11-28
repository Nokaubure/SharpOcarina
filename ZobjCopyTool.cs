using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using OpenTK.Graphics.ES20;

namespace SharpOcarina
{


    public partial class ZobjCopyToolForm : Form
    {
        List<byte> SourceZobj = new List<byte>();
        List<byte> TargetZobj = new List<byte>();
        string TargetZobjFilename = "";
        List<ZobjElement> SourceElements = new List<ZobjElement>();
        List<ZobjElement> TargetElements = new List<ZobjElement>();
        int SourceBank = 6;
        int TargetBank = 6;
        string LastTargetFilename = "";
        
        string[] typenames = { "Unk", "Anim", "DList", "Col", "Tex", "Skel", "PlAnim", "Mat", "Limb" };

        string[] texturenames = { "Unknown", "RGBA16", "RGBA32", "I4", "I8", "IA4", "IA8", "IA16", "CI4", "CI8", "TLUT" };

        string[] headernames = { "u16", "AnimationHeader", "Gfx", "CollisionHeader", "u16", "FlexSkeletonHeader", "LinkAnimationHeader", "Gfx", "Gfx" };

        float[] texturebyteweight = { 0f, 2f, 4f, 0.5f, 1f, 0.5f, 1f, 2f, 0.5f, 1f, 1f };

        const int RGBA16 = 1;
        const int RGBA32 = 2;
        const int I4 = 3;
        const int I8 = 4;
        const int IA4 = 5;
        const int IA8 = 6;
        const int IA16 = 7;
        const int CI4 = 8;
        const int CI8 = 9;
        const int TLUT = 10;

        public ZobjCopyToolForm()
        {
            InitializeComponent();

            SongItem[] objs = new[]
{
                new SongItem {Text = "Unknown", Value = 0},
                new SongItem {Text = "RGBA16", Value = 1},
                new SongItem {Text = "RGBA32", Value = 2},
                new SongItem {Text = "I4", Value = 3},
                new SongItem {Text = "I8", Value = 4},
                new SongItem {Text = "IA4", Value = 5},
                new SongItem {Text = "IA8", Value = 6},
                new SongItem {Text = "IA16", Value = 7},
                new SongItem {Text = "CI4", Value = 8},
                new SongItem {Text = "CI8", Value = 9},
                new SongItem {Text = "TLUT", Value = 10},
            };
            SourceTextureFormatComboBox.Items.AddRange(objs);

            SourceListBox.DrawItem += ListBox_DrawItem;
            SourceListBox.DrawMode = DrawMode.OwnerDrawFixed;
            SourceListBox.SelectionMode = SelectionMode.One;
            SourceListBox.ForeColor = Color.LightBlue;
            TargetListBox.DrawItem += ListBox_DrawItem;
            TargetListBox.DrawMode = DrawMode.OwnerDrawFixed;
            TargetListBox.SelectionMode = SelectionMode.One;

            UpdateForm();
        }

        private void UpdateForm()
        {
            if (SourceZobj.Count > 0) SearchButton.Enabled = true;
            SaveAsButton.Enabled = GenerateLdButton.Enabled =  (SourceElements.Count > 0);
            SaveButton.Enabled = (TargetFilename.Text != "" && SourceElements.Count > 0);

            MoveButton.Enabled = (SourceElements.Count > 0 && !SourceElements[SourceListBox.SelectedIndex].moved && TargetElements.FindIndex(x => x.name == SourceElements[SourceListBox.SelectedIndex].name) == -1);

            SourceOffset.Enabled = (SourceZobj.Count > 0);

            TargetOffset.Enabled = (TargetZobj.Count > 0);

            /*
            if (SourceListBox.Items.Count > 0)
                toolTip1.SetToolTip(SourceListBox, SourceListBox.Items[SourceListBox.SelectedIndex].ToString());
            if (TargetListBox.Items.Count > 0)
                toolTip1.SetToolTip(TargetListBox, TargetListBox.Items[TargetListBox.SelectedIndex].ToString());*/

            if (SourceZobj.Count == 0)
            {
                SearchLabel.Text = "Select a source .zobj file";
            }
            else
            {
                string text = "";
                if (SourceFilename.Text != "")
                {
                    if (SourceOffsetFilename.Text == "") text += "Source elements will be guessed (only animations!)";
                    else text += "Source elements will be read from " + Path.GetExtension(SourceOffsetFilename.Text) + " file";
                }
                if (TargetFilename.Text != "")
                {
                    if (TargetOffsetFilename.Text == "") text += ", Target elements will be guessed (only animations!)";
                    else text += ", Target elements will be read from " + Path.GetExtension(TargetOffsetFilename.Text) + " file";
                }
                SearchLabel.Text = text;
            }

            if (SourceListBox.Items.Count > 0 && SourceElements[SourceListBox.SelectedIndex].type == ZobjType.TEXTURE)
            {
                SourceTextureFormatComboBox.Enabled = true;
                SourceTextureWidth.Enabled = true;
                SourceTextureHeight.Enabled = true;
                foreach (SongItem item in SourceTextureFormatComboBox.Items)
                {
                    if (item != null && Convert.ToByte(item.Value) == (byte)SourceElements[SourceListBox.SelectedIndex].textureFormat)
                    {
                        SourceTextureFormatComboBox.SelectedItem = item;
                        break;
                    }
                }
                SourceTextureWidth.Value = SourceElements[SourceListBox.SelectedIndex].textureWidth;
                SourceTextureHeight.Value = SourceElements[SourceListBox.SelectedIndex].textureHeight;
            }
            else
            {
                SourceTextureFormatComboBox.Enabled = false;
                SourceTextureWidth.Enabled = false;
                SourceTextureHeight.Enabled = false;
                //SourceTextureWidth.Value = 0;
                //SourceTextureHeight.Value = 0;
            }


        }

        private void SourceButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZObj Files (*.zobj*)|*.zobj*|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                SourceZobj = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                SourceFilename.Text = openFileDialog1.FileName;

                if (openFileDialog1.FileName.Contains("\\rom\\object\\"))
                {
                    string[] splits = openFileDialog1.FileName.Split(new string[] { "rom\\object\\" }, StringSplitOptions.None);
                    string basefolder = splits[0];
                    if (File.Exists(basefolder+ "z64project.toml"))
                    {
                        string rest = splits[1].Split(new string[] { "\\" }, StringSplitOptions.None)[0];
                        string ldfile = basefolder + "include\\object\\" + rest + ".ld";
                        if (File.Exists(ldfile))
                        {
                            SourceOffsetFilename.Text = ldfile;
                        }
                    }
                }

                UpdateForm();
            }
        }

        private void TargetButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZObj Files (*.zobj*)|*.zobj*|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                TargetZobj = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                TargetFilename.Text = openFileDialog1.FileName;

                UpdateForm();

            }
        }

        private void UpdateListbox(List<byte> zobjlist, ref List<ZobjElement> elements, ref ListBox listbox, string offsetfile)
        {

            if (offsetfile != "")
            {
                if (Path.GetExtension(offsetfile).ToLower() == ".xml")
                {
                    //using .xml
                    XmlDocument doc = new XmlDocument();
                    FileStream fs = new FileStream(offsetfile, FileMode.Open, FileAccess.Read);
                    doc.Load(fs);
                    XmlNodeList filenodes = doc.SelectNodes("Root/File");
                    if (filenodes.Count == 0)
                    {
                        MessageBox.Show("Invalid xml! must be an xml from decomp's assets/xml/objects", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        elements.Clear();
                        listbox.Items.Clear();

                        foreach (XmlNode filenode in filenodes)
                        {
                            if (filenode.NodeType == XmlNodeType.Comment) continue;
                            foreach (XmlNode node in filenode.ChildNodes)
                            {
                                if (node.NodeType == XmlNodeType.Comment) continue;

                                ZobjType type = ZobjType.BINARY;
                                switch (node.Name)
                                {
                                    case "Texture":
                                    {
                                        type = ZobjType.TEXTURE;
                                        break;
                                    }
                                    case "DList":
                                    {
                                        type = ZobjType.MODEL;
                                        break;
                                    }
                                    case "Collision":
                                    {
                                        type = ZobjType.COLLISION;
                                        break;
                                    }
                                    case "Skeleton":
                                    {
                                        type = ZobjType.SKELETON;
                                        break;
                                    }
                                    case "Animation":
                                    {
                                        type = ZobjType.ANIMATION;
                                        break;
                                    }
                                    case "PlayerAnimation":
                                    {
                                        type = ZobjType.LINKANIMATION;
                                        break;
                                    }
                                    case "Limb":
                                    {
                                        type = ZobjType.LIMB;
                                        break;
                                    }
                                }
                                XmlAttributeCollection nodeAtt = node.Attributes;
                                XmlAttributeCollection filenodeAtt = filenode.Attributes;
                                string name = (nodeAtt["Name"] != null) ? nodeAtt["Name"].Value : "Unk";
                                uint offset = (nodeAtt["Offset"] != null) ? Convert.ToUInt32(nodeAtt["Offset"].Value, 16) : 9;
                                if (offset == 9) continue;
                                byte bank = (byte) ((filenodeAtt["Segment"] != null) ? Convert.ToByte(filenodeAtt["Segment"].Value, 16) : 6);
                                

                                if (listbox == SourceListBox) SourceBankNumeric.Value = bank;
                                else if (listbox == TargetListBox) TargetBankNumeric.Value = bank;
                                byte[] data = { };
                                ZobjElement element = new ZobjElement(name, (uint) (offset | (bank << 24)), data, type);
                                

                                if (type == ZobjType.TEXTURE)
                                {
                                    element.textureWidth = (nodeAtt["Width"] != null) ? Convert.ToUInt16(nodeAtt["Width"].Value) : (byte)0;
                                    element.textureHeight = (nodeAtt["Height"] != null) ? Convert.ToUInt16(nodeAtt["Height"].Value) : (byte)0;
                                    byte targetformat = 0;
                                    if ((nodeAtt["Format"] != null))
                                    {
                                        string format = nodeAtt["Format"].Value.ToUpper();
                                        for (byte i = 0; i < texturenames.Length; i++)
                                        {
                                            if (texturenames[i] == format) targetformat = i;
                                        }

                                    }
                                    //TlutOffset?

                                    element.textureFormat = (nodeAtt["Format"] != null) ? targetformat : (byte)0;
                                }

                                elements.Add(element);

                            }
                        }
                        //
                    }
                }
                else if (Path.GetExtension(offsetfile).ToLower() == ".ld")
                {
                    //using .ld
                    string[] lines = File.ReadAllLines(offsetfile);

                    elements.Clear();
                    listbox.Items.Clear();

                    foreach (string line in lines)
                    {
                        if (!line.Contains("= 0x")) continue;
                        ZobjType type = ZobjType.BINARY;

                        if (line.Contains("_Tex")) type = ZobjType.TEXTURE;
                        else if (line.Contains("_Dl")) type = ZobjType.MODEL;
                        else if (line.Contains("_Mtl")) type = ZobjType.MATERIAL;
                        else if (line.Contains("_Anim")) type = ZobjType.ANIMATION;
                        else if (line.Contains("_Skel")) type = ZobjType.SKELETON;
                        else if (line.Contains("_Col")) type = ZobjType.COLLISION;


                        string name = line.Split('=')[0].Replace(" ", "");
                        uint offset =  Convert.ToUInt32(line.Split('=')[1].Replace(" ", "").Replace(";",""), 16);
                        byte bank = (byte)((offset & 0x0F000000) >> 24);
                        if (listbox == SourceListBox) SourceBankNumeric.Value = bank;
                        else if (listbox == TargetListBox) TargetBankNumeric.Value = bank;
                        byte[] data = { };
                        elements.Add(new ZobjElement(name, offset, data, type));


                    }
                }
                else
                {
                    MessageBox.Show("Offset file should be of extensions .xml or .ld!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



            }
            else if (zobjlist.Count > 0)
            {
                //time to guess
            
                byte[] zobj = zobjlist.ToArray();
                elements.Clear();
                listbox.Items.Clear();

                byte[] banks = { 4, 5, 6 , 7 };

                for (int ii = 0; ii < zobj.Length; ii += 4)
                {
                    if (!(ii + 4 > zobj.GetUpperBound(0)))
                    {
                        if (zobj[ii + 2] == 0x00 && zobj[ii + 3] == 0x00 && banks.Contains(zobj[ii + 4]) && banks.Contains(zobj[ii + 8]) && zobj[ii + 14] == 0x00 && zobj[ii + 15] == 0x00 && Helpers.Read24S(zobjlist, ii + 5) < zobj.Length)
                        {
                            /*
                            int animrotvaloffset = Helpers.Read24S(zobjlist, ii + 5);
                            int animrotindexoffset = Helpers.Read24S(zobjlist, ii + 9);
                            ushort frames = Helpers.Read16(zobjlist, ii);*/

                            elements.Add(new ZobjElement("Anim_" + ii.ToString("X8"), (uint)ii, new byte[0],ZobjType.ANIMATION));

                            //elements.Add(new ZobjElement((uint) animrotvaloffset, (uint) animrotindexoffset, (uint) ii, frames));


                        }
                    }
                }

                if (elements.Count == 0)
                {
                    MessageBox.Show("No animations found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            RefreshListbox(listbox, elements);
            if (listbox.SelectionMode != SelectionMode.None && listbox.Items.Count > 0) listbox.SelectedIndex = 0;
            TargetBank = (int)TargetBankNumeric.Value;
            SourceBank = (int)SourceBankNumeric.Value;
        }

        private void RefreshListbox(ListBox listbox, List<ZobjElement> elements)
        {
            int previndex = listbox.SelectedIndex;
            if (previndex == -1) previndex = 0; 
            listbox.Items.Clear();
            foreach (ZobjElement element in elements)
            {
                listbox.Items.Add("[" + typenames[(byte)element.type] + "]" + (element.type == ZobjType.TEXTURE ? ("(" + texturenames[(byte)element.textureFormat] + " " + element.textureWidth + "x" + element.textureHeight + ")") : "") + " " + element.name + " (0x" + (element.newoffset != 9 ? element.newoffset.ToString("X8") : element.offset.ToString("X8")) + ")");
            }
            
            if (previndex < listbox.Items.Count) listbox.SelectedIndex = previndex;
            else if (listbox.Items.Count > 0) listbox.SelectedIndex = 0;
            else
            {
                //MessageBox.Show("Empty list?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }

            UpdateHorizontalScrollbarWidth(listbox);
        }

        private void MoveChildElement(uint fulloffset, int i, ref Dictionary<uint,uint> movedOffsets, ref uint startoffset, ref uint endoffset)
        {
            int index = TargetElements.FindIndex(x => x.moved && x.offset == fulloffset);
            uint offset = fulloffset & 0x00FFFFFF;
            if (index != -1)
            {
                DebugConsole.WriteLine("Offset " + fulloffset.ToString("X8") + " exists in a target element ");

                if (!movedOffsets.ContainsKey((uint)i)) movedOffsets.Add((uint)i, (uint)(TargetElements[index].newoffset));
            }
            else
            {
                index = SourceElements.FindIndex(x => x.offset == fulloffset);
                if (index != -1)
                {
                    DebugConsole.WriteLine("Offset " + fulloffset.ToString("X8") + " exists in a source element ");

                    uint newoffset_ = MoveElement(SourceElements[index]);

                    if (!movedOffsets.ContainsKey((uint)i)) movedOffsets.Add((uint)i, (uint)(newoffset_ | (TargetBank << 24)));
                }
                else
                {
                    DebugConsole.WriteLine("Offset " + fulloffset.ToString("X8") + " is within the element ");

                    if (!movedOffsets.ContainsKey((uint)i)) movedOffsets.Add((uint)i, 9);
                    if (offset < startoffset) startoffset = offset;
                    if (offset > endoffset) endoffset = offset;
                }
            }
            //offsetsToChange.Add((uint)i);
        }

        private uint MoveElement(ZobjElement sourceElement)
        {
            List<byte> elementdata = new List<byte>();

            switch (sourceElement.type)
            {
                case ZobjType.TEXTURE:
                    {
                        float bytesperpx = texturebyteweight[sourceElement.textureFormat];
                        uint startoffset = sourceElement.offset & 0x00FFFFFF;
                        uint endoffset = (startoffset + (uint)((sourceElement.textureWidth * sourceElement.textureHeight) * bytesperpx));

                        for (uint i = startoffset; i < endoffset; i++)
                        {
                            elementdata.Add(SourceZobj[(int)i]);
                        }

                        sourceElement.newoffset = (uint) (TargetZobj.Count | (TargetBank << 24));

                        TargetZobj.AddRange(elementdata);



                        break;

                    }
                case ZobjType.COLLISION:
                    {


                        int collisionoffset = (int)sourceElement.offset & 0x00FFFFFF;

                        int vertexnum = Helpers.Read16(SourceZobj, collisionoffset + 0x0C);
                        int vertexoffset = Helpers.Read24S(SourceZobj, collisionoffset + 0x10 + 1);
                        int polynum = Helpers.Read16(SourceZobj, collisionoffset + 0x14);
                        int polygonoffset = Helpers.Read24S(SourceZobj, collisionoffset + 0x18 + 1);
                        int polytypeoffset = Helpers.Read24S(SourceZobj, collisionoffset + 0x1C + 1);

                        int maxpolytype = 0;

                        //we search the highest polytype
                        for (int i = polygonoffset; i < polygonoffset + (0x10 * polynum); i += 0x10)
                        {
                            int polytypeid = Helpers.Read16(SourceZobj, i);

                            if (polytypeid > maxpolytype) maxpolytype = polytypeid;
                        }

                        uint startoffset = (uint)Maths.Min(collisionoffset, vertexoffset, polygonoffset, polytypeoffset);

                        uint endoffset = (uint)Maths.Max(collisionoffset + 0x2C, vertexoffset + (0x06 * vertexnum), polygonoffset + (0x10 * polynum), polytypeoffset + (0x8 * (maxpolytype + 1)));

                        for (uint i = startoffset; i < endoffset; i++)
                        {
                            elementdata.Add(SourceZobj[(int)i]);
                        }

                        uint newoffset = (uint)TargetZobj.Count;

                        uint newcollisionoffset = (uint)(collisionoffset - startoffset + newoffset);
                        uint newvertexoffset = (uint)(vertexoffset - startoffset + newoffset);
                        uint newpolygonoffset = (uint)(polygonoffset - startoffset + newoffset);
                        uint newpolytypeoffset = (uint)(polytypeoffset - startoffset + newoffset);

                        uint originalstartoffset = sourceElement.offset & 0x00FFFFFF;
                        sourceElement.newoffset = (uint)((originalstartoffset - startoffset + newoffset) | (TargetBank << 24));

                        TargetZobj.AddRange(elementdata);

                        Helpers.Overwrite32(ref TargetZobj, (int)newcollisionoffset + 0x10, (uint)((0x00000000 | (TargetBank << 24)) + newvertexoffset));
                        Helpers.Overwrite32(ref TargetZobj, (int)newcollisionoffset + 0x18, (uint)((0x00000000 | (TargetBank << 24)) + newpolygonoffset));
                        Helpers.Overwrite32(ref TargetZobj, (int)newcollisionoffset + 0x1C, (uint)((0x00000000 | (TargetBank << 24)) + newpolytypeoffset));

                        DebugConsole.WriteLine("Source bank " + SourceBank + " Target bank " + TargetBank);

                        break;
                    }

                case ZobjType.LIMB:
                case ZobjType.MODEL:
                case ZobjType.MATERIAL:
                    {
                        uint startoffset = sourceElement.offset & 0x00FFFFFF;
                        uint endoffset = 0;
                        int i = (int)startoffset & 0x00FFFFFF;
                        bool DEreturn = false;
                        int DEoffset = 0;
                        List<bool> trackendoffset = new List<bool>();
                        trackendoffset.Add(true);
                        Dictionary<uint, uint> movedOffsets = new Dictionary<uint, uint>();
                        while (1 == 1)
                        {
                            byte opcode = SourceZobj[i];
                            if (new byte[] { 0x01, 0x04, 0xDA, 0xDC, 0xFD, 0xFE, 0xFF }.Contains(opcode))
                            {
                                byte bank = SourceZobj[i + 4];
                                uint fulloffset = Helpers.Read32(SourceZobj, i + 4);
                                if (bank == SourceBank)
                                {
                                   // DebugConsole.WriteLine("Jumping to " + fulloffset.ToString("X8"));
                                    MoveChildElement(fulloffset, i, ref movedOffsets, ref startoffset, ref endoffset);

                                }
                            }

                            if (opcode == 0xDE)
                            {
                                byte bank = SourceZobj[i + 4];
                                uint fulloffset = Helpers.Read32(SourceZobj, i + 4);
                                uint offset = Helpers.Read24(SourceZobj, i + 5);
                                if (bank == SourceBank)
                                {
                                    //offsetsToChange.Add((uint)i);
                                    if (SourceZobj[i + 1] == 0)
                                    {
                                        DEreturn = true;
                                        DEoffset = i;
                                    }
                                    MoveChildElement(fulloffset, i, ref movedOffsets, ref startoffset, ref endoffset);
                                    i = (int)offset;
                                    //jump is within the object
                                    if (SourceElements.FindIndex(x => x.offset == fulloffset) == -1)
                                    {
                                        if (i < startoffset) startoffset = (uint)i;
                                        if (i > endoffset) endoffset = (uint)i;
                                        trackendoffset.Add(true);
                                    }
                                    else
                                    {
                                        trackendoffset.Add(false);
                                    }

                                    continue;
                                }
                            }
                            if (opcode == 0xDF)
                            {
                                //TODO this needs to be reworked...
                                // if (i < startoffset) startoffset = (uint)i;
                                
                                if (trackendoffset[trackendoffset.Count-1])
                                {
                                    if (i > endoffset) endoffset = (uint)i;
                                }
                                if (DEreturn)
                                {
                                    i = DEoffset + 8;
                                    DEreturn = false;
                                    trackendoffset.RemoveAt(trackendoffset.Count-1);

                                    continue;
                                }
                                else
                                {
                                    i += 8;
                                    if (trackendoffset[trackendoffset.Count - 1])
                                    {
                                        if (i > endoffset) endoffset = (uint)i;
                                    }
                                    break;
                                }
                            }
                            i += 8;
                        }

                       // if (i > endoffset) endoffset = (uint) i;


                        for (int y = (int) startoffset; y < endoffset; y++)
                        {
                            elementdata.Add(SourceZobj[y]);
                        }

                        uint newoffset = (uint)TargetZobj.Count;

                        //TODO relocate

                        uint originalstartoffset = sourceElement.offset & 0x00FFFFFF;
                        sourceElement.newoffset = (uint)((originalstartoffset - startoffset + newoffset) | (TargetBank << 24));

                        TargetZobj.AddRange(elementdata);

                        foreach (KeyValuePair<uint,uint> e in movedOffsets)
                        {
                            //uint fulloffset = Helpers.Read32(TargetZobj, i + 4);
                            //uint offset = Helpers.Read24(TargetZobj, i + 5);

                            uint offsetInTarget = (uint)(e.Key - startoffset + newoffset) & 0x00FFFFFF;
                            if (e.Value == 9)
                            {
                                uint newoffsetInTarget = Helpers.Read24(TargetZobj, (int)(offsetInTarget + 5)) - startoffset + newoffset;
                                DebugConsole.WriteLine(Helpers.Read32(TargetZobj, (int)offsetInTarget + 0x4).ToString("X8") + " -> " + ((uint)((0x00000000 | (TargetBank << 24)) + newoffsetInTarget)).ToString("X8"));
                                Helpers.Overwrite32(ref TargetZobj, (int)offsetInTarget + 0x4, (uint)((0x00000000 | (TargetBank << 24)) + newoffsetInTarget));
                                
                            }
                            else
                            {
                                DebugConsole.WriteLine((Helpers.Read32(TargetZobj, (int)offsetInTarget + 0x4)).ToString("X8") + " -> " + e.Value.ToString("X8"));
                                Helpers.Overwrite32(ref TargetZobj, (int)offsetInTarget + 0x4, e.Value);
                            }
                        }




                        break;
                    }

                case ZobjType.SKELETON:
                    {
                        int skeletonoffset = (int) (sourceElement.offset & 0x00FFFFFF);
                        byte bank = SourceZobj[skeletonoffset];
                        byte limbnumber = SourceZobj[skeletonoffset + 8];
                        Dictionary<uint, uint> movedOffsets = new Dictionary<uint, uint>();
                        if (bank != SourceBank)
                        {
                            MessageBox.Show("Bank missmatch", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        uint limbindexoffset = Helpers.Read24(SourceZobj, (int)skeletonoffset + 1);

                        for (int i = 0; i < limbnumber; i++)
                        {
                            //offsetsToChange.Add((uint) (limbindexoffset + (i*4)));
                            movedOffsets.Add((uint) (limbindexoffset + (i * 4)),9);
                            uint limboffset = Helpers.Read24(SourceZobj, (int)(limbindexoffset + (i * 4) + 1));
                            
                            byte limbbank = SourceZobj[(int)limboffset+8];
                            uint fulloffset = Helpers.Read32(SourceZobj, (int) (limboffset + 8));
                            if (limbbank == SourceBank)
                            {
                                //offsetsToChange.Add((uint)(limboffset + 8));
                                uint temp = 0;
                                MoveChildElement(fulloffset, i, ref movedOffsets, ref temp, ref temp);
                            }
                            else
                            {
                                DebugConsole.WriteLine("Bank missmatch on skeleton limbs");
                            }

                            
                        }

                        uint startoffset = Maths.Min((uint)skeletonoffset, limbindexoffset, movedOffsets.MinBy(x => x.Key)[0].Key);
                        uint endoffset = Maths.Max((uint)skeletonoffset + 0xC, limbindexoffset + 0x4, movedOffsets.MaxBy(x => x.Key)[0].Key + 0xC);

                        
                        uint newoffset = (uint)TargetZobj.Count;

                        uint newheader = (uint)(skeletonoffset - startoffset + newoffset);

                        sourceElement.newoffset = (uint)(newheader | (TargetBank << 24));

                        

                        for (int y = (int)startoffset; y < endoffset; y++)
                        {
                            elementdata.Add(SourceZobj[y]);
                        }

                        TargetZobj.AddRange(elementdata);

                        foreach (KeyValuePair<uint, uint> e in movedOffsets)
                        {

                            uint offsetInTarget = (uint)(e.Key - startoffset + newoffset);
                            if (e.Value == 9)
                            {
                                uint newoffsetInTarget = Helpers.Read24(TargetZobj, (int)(offsetInTarget + 5)) - startoffset + newoffset;
                                DebugConsole.WriteLine(Helpers.Read32(TargetZobj, (int)offsetInTarget + 0x4).ToString("X8") + " -> " + ((uint)((0x00000000 | (TargetBank << 24)) + newoffsetInTarget)).ToString("X8"));
                                Helpers.Overwrite32(ref TargetZobj, (int)offsetInTarget + 0x4, (uint)((0x00000000 | (TargetBank << 24)) + newoffsetInTarget));

                            }
                            else
                            {
                                DebugConsole.WriteLine((Helpers.Read32(TargetZobj, (int)offsetInTarget + 0x4)).ToString("X8") + " -> " + e.Value.ToString("X8"));
                                Helpers.Overwrite32(ref TargetZobj, (int)offsetInTarget + 0x4, e.Value);
                            }
                        }
                        /*
                        foreach (uint offsetToChange in offsetsToChange)
                        {
                            //uint fulloffset = Helpers.Read32(TargetZobj, i + 4);
                            //uint offset = Helpers.Read24(TargetZobj, i + 5);

                            uint offsetInTarget = (uint)(offsetToChange - startoffset + newoffset);

                            uint newoffsetInTarget = Helpers.Read24(TargetZobj, (int)(offsetInTarget + 1)) - startoffset + newoffset; 
                            DebugConsole.WriteLine(Helpers.Read32(TargetZobj, (int)offsetInTarget).ToString("X8") + " -> " + ((uint)((0x00000000 | (TargetBank << 24)) + newoffsetInTarget)).ToString("X8"));
                            Helpers.Overwrite32(ref TargetZobj, (int)offsetInTarget, (uint)((0x00000000 | (TargetBank << 24)) + newoffsetInTarget));

                        }*/

                        break;
                    }

                case ZobjType.LINKANIMATION:
                    {
                        int animoffset = (int)(sourceElement.offset & 0x00FFFFFF);
                        int animrotvaloffset = Helpers.Read24S(SourceZobj, (int)(animoffset + 5));
                        byte bank = SourceZobj[(int)animoffset + 4];
                        ushort frames = Helpers.Read16(SourceZobj, (int)(animoffset));
                        uint startoffset = 0;
                        uint endoffset = 0;

                        if (bank != SourceBank)
                        {
                            startoffset = sourceElement.offset;
                            endoffset = sourceElement.offset + 8;
                        }
                        else
                        {
                            startoffset = Maths.Min((uint)animoffset, (uint)animrotvaloffset);
                            endoffset = Maths.Max((uint)animoffset + 0x8, (uint)animrotvaloffset + 0x7E + 0x7E + 4);
                        }
                        for (uint i = startoffset; i < endoffset; i++)
                        {
                            elementdata.Add(SourceZobj[(int)i]);
                        }

                        uint newoffset = (uint)TargetZobj.Count;

                        uint newheader = (uint) (animoffset - startoffset + newoffset);

                        TargetZobj.AddRange(elementdata);

                        if (bank == SourceBank)
                        {

                            
                            uint newrotvaloffset = (uint)(animrotvaloffset - startoffset + newoffset);

                            Helpers.Overwrite32(ref TargetZobj, (int)newheader + 0x4, (uint)((0x00000000 | (TargetBank << 24)) + newrotvaloffset));
                            
                        }
                    
                        sourceElement.newoffset = (uint)(newheader | (TargetBank << 24));
                        

                        

                        
                        break;
                    }


                case ZobjType.ANIMATION:
                    {
                        int animoffset = (int)(sourceElement.offset & 0x00FFFFFF);
                        int animrotvaloffset = Helpers.Read24S(SourceZobj, (int) (animoffset + 5));
                        int animrotindexoffset = Helpers.Read24S(SourceZobj, (int)(animoffset + 9));
                        ushort frames = Helpers.Read16(SourceZobj, (int)(animoffset)); 


                        uint startoffset = Maths.Min((uint)animoffset, (uint)animrotindexoffset, (uint)animrotvaloffset);
                        uint endoffset = Maths.Max((uint)animoffset + 0x10, (uint)animrotindexoffset, (uint)(animrotvaloffset)); //TODO not accurate

                        for (uint i = startoffset; i < endoffset; i++)
                        {
                            elementdata.Add(SourceZobj[(int)i]);
                        }

                        uint newoffset = (uint)TargetZobj.Count;

                        uint newheader = (uint) (animoffset - startoffset + newoffset);
                        uint newrotvaloffset = (uint) (animrotvaloffset - startoffset + newoffset);
                        uint newrotindexoffset = (uint) (animrotindexoffset - startoffset + newoffset);

                        TargetZobj.AddRange(elementdata);

                        Helpers.Overwrite32(ref TargetZobj, (int)newheader + 0x4, (uint)((0x00000000 | (TargetBank << 24)) + newrotvaloffset));
                        Helpers.Overwrite32(ref TargetZobj, (int)newheader + 0x8, (uint)((0x00000000 | (TargetBank << 24)) + newrotindexoffset));

                        sourceElement.newoffset = (uint) (newheader | (TargetBank << 24));

                        break;
                    }
            }

            sourceElement.moved = true;
            TargetElements.Add(sourceElement);

            return sourceElement.newoffset;

        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            if (SourceElements[SourceListBox.SelectedIndex].moved) //unused
            {
                MessageBox.Show("You already copied this element!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            MoveElement(SourceElements[SourceListBox.SelectedIndex]);



            RefreshListbox(TargetListBox,TargetElements);

            UpdateForm();

            //TargetListBox.Items.Add(newheader.ToString("X8") + " (frames: " + SourceElements[SourceListBox.SelectedIndex].frames + ")");

            //SourceElements[SourceListBox.SelectedIndex].moved = true;

            //SaveButton.Enabled = true;
            //SaveAsButton.Enabled = true;
        }

        private void Save(string path)
        {
            if (MainForm.IsFileLocked(path))
                MessageBox.Show("Zobj is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                BinaryWriter BWS = new BinaryWriter(File.OpenWrite(path));
                BWS.Seek(0, SeekOrigin.Begin);

                while (TargetZobj.Count % 0x10 != 0)
                    TargetZobj.Add(0);


                BWS.Write(TargetZobj.ToArray());

                BWS.Close();

                if (AutomaticallyUpdateLDCheckbox.Checked && path.Contains("\\rom\\object\\"))
                {
                    string[] splits = path.Split(new string[] { "rom\\object\\" }, StringSplitOptions.None);
                    string basefolder = splits[0];
                    if (File.Exists(basefolder + "z64project.toml"))
                    {
                        string rest = splits[1].Split(new string[] { "\\" }, StringSplitOptions.None)[0];
                        string ldfile = basefolder + "include\\object\\" + rest + ".ld";

                        GenerateLDFile(ldfile, false);


                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MainForm.IsFileLocked(TargetFilename.Text))
                MessageBox.Show("Zobj is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                Save(TargetFilename.Text);

                MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {

            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.Filter = "Zobj binary model file (*.zobj)|*.zobj|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (MainForm.IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Save(saveFileDialog1.FileName);

                    MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

           
        }

        private void CopyAnimationsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.zobjcopy_visible = false;
        }


        private void SourceOffset_Click(object sender, EventArgs e)
        {

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Linker Files / XML files (*.ld;*.xml)|*.ld;*.xml|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SourceOffsetFilename.Text = openFileDialog1.FileName;

                UpdateForm();
            }

        }

        private void TargetOffset_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Linker Files / XML files (*.ld;*.xml)|*.ld;*.xml|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TargetOffsetFilename.Text = openFileDialog1.FileName;

                UpdateForm();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (SourceZobj.Count > 0)
            {
                UpdateListbox(SourceZobj, ref SourceElements, ref SourceListBox, SourceOffsetFilename.Text);
                //if (TargetFilename.Text == "") TargetZobj = new List<byte>();
            }
            if (TargetZobj.Count > 0)
            {
                if (DontReloadTargetCheckbox.Checked && TargetFilename.Text == LastTargetFilename)
                {

                }
                else
                {
                    LastTargetFilename = TargetFilename.Text;
                    TargetZobj = new List<byte>();
                    TargetElements.Clear();
                    UpdateListbox(TargetZobj, ref TargetElements, ref TargetListBox, TargetOffsetFilename.Text);
                }
            }
            else
            {
                
                if (DontReloadTargetCheckbox.Checked && TargetFilename.Text == LastTargetFilename)
                {

                }
                else
                {
                    LastTargetFilename = TargetFilename.Text;
                    TargetElements.Clear();
                    UpdateListbox(TargetZobj, ref TargetElements, ref TargetListBox, TargetOffsetFilename.Text);
                }

            }

            if (Path.GetExtension(SourceOffsetFilename.Text) == ".ld" && 
                SourceElements.Find(x => x.type == ZobjType.TEXTURE && x.textureFormat == 0) != null)
            {
                // solve the unknown texture formats
                List<ZobjElement> newelements = new List<ZobjElement>();
                foreach (ZobjElement element in SourceElements)
                {
                    if (element.type != ZobjType.MODEL && element.type != ZobjType.LIMB && element.type != ZobjType.MATERIAL) continue;
                    int i = (int)element.offset & 0x00FFFFFF;
                    ZobjElement targetelement = null;
                    bool possiblepalette = false;
                    while (1 == 1)
                    {
                        byte opcode = SourceZobj[i];
                        
                        if (new byte[] { 0xFD}.Contains(opcode))
                        {
                            uint fulloffset = Helpers.Read32(SourceZobj, i + 4);
                            targetelement = SourceElements.Find(x => x.offset == fulloffset);
                            if (targetelement == null)
                            {
                                targetelement = new ZobjElement("UnkTexture" + fulloffset.ToString("X8"), fulloffset, new byte[0], ZobjType.TEXTURE);
                                newelements.Add(targetelement);
                                possiblepalette = true;
                            }
                        }
                        if (new byte[] { 0xF5 }.Contains(opcode))
                        {
                            if (targetelement != null)
                            {

                                byte fmt = (byte)(((SourceZobj[i + 1]) & 0b11100000) >> 5);
                                byte siz = (byte)(((SourceZobj[i + 1]) & 0b00011000) >> 3);
                                
                                    if (fmt == 0 && siz == 2)
                                        targetelement.textureFormat = RGBA16;
                                    else if (fmt == 0 && siz == 3)
                                        targetelement.textureFormat = RGBA32;
                                    else if (fmt == 2 && siz == 0)
                                        targetelement.textureFormat = CI4;
                                    else if (fmt == 2 && siz == 1)
                                        targetelement.textureFormat = CI8;
                                    else if (fmt == 3 && siz == 0)
                                        targetelement.textureFormat = IA4;
                                    else if (fmt == 3 && siz == 1)
                                        targetelement.textureFormat = IA8;
                                    else if (fmt == 3 && siz == 2)
                                        targetelement.textureFormat = IA16;
                                    else if (fmt == 4 && siz == 0)
                                        targetelement.textureFormat = I4;
                                    else if (fmt == 4 && siz == 1)
                                        targetelement.textureFormat = I8;
                                    else
                                        DebugConsole.WriteLine("fmt " + fmt + " siz" + siz);

                                

                                if (targetelement.textureWidth != 0) targetelement = null;
                            }
                        }
                        if (new byte[] { 0xF2 }.Contains(opcode))
                        {
                            if (targetelement != null)
                            {

                                ushort width = (ushort)((Helpers.Read32(SourceZobj, i + 4) & 0x00FFF000) >> 12);
                                ushort height = (ushort)((Helpers.Read32(SourceZobj, i + 4) & 0x00000FFF));
                                targetelement.textureWidth = (ushort)((width + 4) / 4);
                                targetelement.textureHeight = (ushort)((height + 4) / 4);

                                if (targetelement.textureFormat != 0) targetelement = null;
                            }
                        }
                        if (new byte[] { 0xF0 }.Contains(opcode) && possiblepalette)
                        {
                            if (targetelement != null)
                            {
                                ushort size = (ushort) ((Helpers.Read32(SourceZobj, i + 4) & 0x00FFF000) >> 12);
                                targetelement.textureFormat = TLUT;
                                ushort newwidth = (ushort)(size / 4);
                                if (newwidth > 255) newwidth = 255;
                                while (newwidth % 4 != 0) newwidth++;
                                targetelement.textureWidth = newwidth;
                                targetelement.textureHeight = 1;
                                targetelement = null;
                            }
                        }

                        if (opcode == 0xDE)
                        {
                            if (SourceZobj[i + 1] == 1)
                            {
                                break;
                            }
                        }
                        if (opcode == 0xDF)
                        {
                            break;
                        }
                        i += 8;
                    }


                }
                if (newelements.Count > 0) SourceElements.AddRange(newelements);
            }

            UpdateForm();
            RefreshListbox(SourceListBox, SourceElements);
            RefreshListbox(TargetListBox, TargetElements);
        }

        private void SourceTextureFormatComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SourceListBox.Items.Count > 0 && SourceElements[SourceListBox.SelectedIndex].type == ZobjType.TEXTURE)
            {
                SourceElements[SourceListBox.SelectedIndex].textureFormat = Convert.ToByte(((SongItem)SourceTextureFormatComboBox.SelectedItem).Value);
            }
            RefreshListbox(SourceListBox, SourceElements);
            UpdateForm();
        }

        private void SourceTextureWidth_ValueChanged(object sender, EventArgs e)
        {
            if (SourceListBox.Items.Count > 0 && SourceElements[SourceListBox.SelectedIndex].type == ZobjType.TEXTURE)
            {
                SourceElements[SourceListBox.SelectedIndex].textureWidth = (byte)SourceTextureWidth.Value;
            }
            RefreshListbox(SourceListBox, SourceElements);
            UpdateForm();
        }

        private void SourceTextureHeight_ValueChanged(object sender, EventArgs e)
        {
            if (SourceListBox.Items.Count > 0 && SourceElements[SourceListBox.SelectedIndex].type == ZobjType.TEXTURE)
            {
                SourceElements[SourceListBox.SelectedIndex].textureHeight = (byte)SourceTextureHeight.Value;
            }
            RefreshListbox(SourceListBox,SourceElements);
            UpdateForm();
        }

        private void SourceListBox_Click(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void SourceListBox_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateForm();
        }


        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var listBox = sender as ListBox;

            ZobjElement element;
            if (((ListBox)sender).Name == "SourceListBox")
            {
                element = SourceElements[e.Index];
            }
            else
            {
                element = TargetElements[e.Index];
            }
            bool isRedItem = element.type == ZobjType.TEXTURE && (element.textureFormat == 0 || element.textureWidth == 0 || element.textureHeight == 0);
            
            //e.DrawBackground();
            var textBrush = isRedItem ? Brushes.Red : Brushes.Black;
            
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            
            Color backgroundColor = isSelected ? Color.LightGreen : listBox.BackColor;

            using (Brush backgroundBrush = new SolidBrush(backgroundColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            e.Graphics.DrawString(
                listBox.Items[e.Index].ToString(),
                e.Font,
                textBrush,
                e.Bounds
            );

            e.DrawFocusRectangle();
        }

        private void GenerateHFile(string path)
        {
            path = path.Replace(".ld", ".h");
            List<string> lines = new List<string>();
            
            if (File.Exists(path) && !MainForm.IsFileLocked(path))
            {

                List<string> linestoadd = new List<string>();
                lines = new List<string>(File.ReadAllLines(path));

                int targetline = -1;

                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("#endif"))
                    {
                        targetline = i - 1;
                    }
                }

                foreach (ZobjElement element in TargetElements)
                {
                    bool appears = false;
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].Contains(element.name + "[]"))
                        {
                            appears = true;
                        }
                    }
                    if (!appears) linestoadd.Add("extern " + headernames[(int) element.type] + " " + element.name + ((UnderscoreCheckBox.Checked) ? "_" : "") + "[];");
                }

                if (targetline == -1) targetline = lines.Count - 1;

                if (linestoadd.Count > 0)
                {
                    lines.InsertRange(targetline,linestoadd);
                }


            }
            else if (!File.Exists(path))
            {
                lines.Add("#ifndef __" + Path.GetFileNameWithoutExtension(path).Replace("-","") + "_H__");
                lines.Add("#define __" + Path.GetFileNameWithoutExtension(path).Replace("-", "") + "_H__");
                foreach (ZobjElement element in TargetElements)
                {
                    lines.Add("extern " + headernames[(int)element.type] + " " + element.name + ((UnderscoreCheckBox.Checked) ? "_" : "") +  "[];");
                }
                lines.Add("");
                lines.Add("#endif /* __" + Path.GetFileNameWithoutExtension(path).Replace("-", "") + "_H__ */");
            }

            File.WriteAllLines(path, lines);
        }

        private void GenerateLDFile(string path, bool showsuccess = true)
        {
            if (MainForm.IsFileLocked(path))
                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                StreamWriter SW = File.CreateText(path);

                foreach (ZobjElement element in TargetElements)
                {
                    SW.WriteLine(element.name + "_ = 0x" + (element.newoffset != 9 ? element.newoffset.ToString("X8") : element.offset.ToString("X8")) + ";");
                }


                SW.Close();

                if (AlsoUpdateHfileCheckbox.Checked) GenerateHFile(path);

                if (showsuccess) MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void GenerateLdButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.Filter = "Linker Files (*.ld;)|*.ld|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = false;
            

            if (TargetFilename.Text.Contains("\\rom\\object\\"))
            {
                string[] splits = TargetFilename.Text.Split(new string[] { "rom\\object\\" }, StringSplitOptions.None);
                string basefolder = splits[0];
                if (File.Exists(basefolder + "z64project.toml"))
                {
                    string rest = splits[1].Split(new string[] { "\\" }, StringSplitOptions.None)[0];
                    string ldfile = basefolder + "include\\object\\" + rest + ".ld";

                    saveFileDialog1.InitialDirectory = basefolder + "include\\object\\";
                    saveFileDialog1.FileName = rest + ".ld";


                }
            }


            GenerateLDFile(saveFileDialog1.FileName);
        }

        private void TargetBankNumeric_ValueChanged(object sender, EventArgs e)
        {
            TargetBank = (int) TargetBankNumeric.Value;
        }

        private void SourceBankNumeric_ValueChanged(object sender, EventArgs e)
        {
            SourceBank = (int)SourceBankNumeric.Value;
        }

        private void UpdateHorizontalScrollbarWidth(ListBox listBox)
        {
            using (Graphics g = listBox.CreateGraphics())
            {
                int maxWidth = 0;

                foreach (var item in listBox.Items)
                {
                    // Measure the width of the text for each item
                    int itemWidth = (int)g.MeasureString(item.ToString(), listBox.Font).Width;
                    if (itemWidth > maxWidth)
                        maxWidth = itemWidth;
                }

                // Set the horizontal extent to the maximum width
                listBox.HorizontalExtent = maxWidth;
            }
        }
    }

    public class ZobjElement
    {
        public string name;
        public uint offset;
        public byte[] data;
        public ZobjType type;
        public uint newoffset = 9;
        public byte bank = 6;


        //texture
        public byte textureFormat;
        public ushort textureWidth;
        public ushort textureHeight;

        public bool moved = false;

        public ZobjElement()
        {

        }

        public ZobjElement(string name, uint offset, byte[] data, ZobjType type)
        {
            this.name = name;
            this.offset = offset;
            this.data = data;
            this.type = type;
        }

    }

    public enum ZobjType : byte
    {
        BINARY,
        ANIMATION,
        MODEL,
        COLLISION,
        TEXTURE,
        SKELETON,
        LINKANIMATION,
        MATERIAL,
        LIMB

    }




    public static class Maths
    {

        public static T Min<T>(params T[] vals)
        {
            return vals.Min();
        }
        public static T Max<T>(params T[] vals)
        {
            return vals.Max();
        }

    }


}
