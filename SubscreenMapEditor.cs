﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SharpOcarina.Properties;

namespace SharpOcarina
{
    public partial class SubscreenMapEditor : Form
    {

        public ushort sceneid;
        List<byte> ROM;
        string ROMfile;
        public Image[] floorimages = { null, Resources._8F, Resources._7F, Resources._6F, Resources._5F, Resources._4F, Resources._3F, Resources._2F, Resources._1F, Resources.B1, Resources.B2, Resources.B3, Resources.B4, Resources.B5, Resources.B6, Resources.B7, Resources.B8 };
        public Button[] floorbuttons = new Button[8];
        public Map[] maplist = new Map[10];
        int selectedfloor = 0;
        public bool noupdate = false;
        int startfloortexture = 0;
        byte[] paletteindexes = new byte[32];
        MainForm mainform;
        List<MapFloor> floors = new List<MapFloor>();
        List<List<CompassIcon>> compassicons = new List<List<CompassIcon>>();
        bool changed = false;
        bool titlecardchanged = false;
        int previd = 0;

        public SubscreenMapEditor(ushort _sceneid, MainForm form)
        {
            sceneid = _sceneid;
            ROM = new List<byte>();
            ROMfile = "";
            mainform = form;


            InitializeComponent();

            floorbuttons = new Button[] { Floor1, Floor2, Floor3, Floor4, Floor5, Floor6, Floor7, Floor8 };

            Init();
        }

        public void Init()
        {
            if (MainForm.GlobalROM != "")
            {
                OpenROM(MainForm.GlobalROM);
                return;
            }
            if (rom64.isSet())
            {
                string binarydata = rom64.getPath() + "\\tools\\sharpocarina\\binarydata";

                if (!File.Exists(binarydata))
                {
                    MessageBox.Show("Select an uncompressed ROM, this will only be asked the first time.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    openFileDialog1.FileName = "";
                    openFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";

                    //openFileDialog1.FilterIndex = 1;
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        ROM = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                        ROM rom = MainForm.CheckVersion(ROM);

                        if (rom.Game == "MM")
                        {
                            MessageBox.Show("MM unsupported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            mainform.Enabled = true;
                            this.Close();
                            return;
                        }

                        FileInfo info = new FileInfo(openFileDialog1.FileName);
                        if (info.Length < 0x3F00000)
                        {
                            MessageBox.Show("This ROM is not uncompressed! make sure you open a clean uncompressed debug ROM!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            return;
                        }

                        System.IO.FileInfo file = new System.IO.FileInfo(binarydata);
                        file.Directory.Create(); //if directory exists, does nothing
                        File.Copy(openFileDialog1.FileName, binarydata);

                        OpenROM(binarydata);

                    }
                    else
                    {
                        mainform.Enabled = true;
                        this.Close();
                    }

                }
                else
                {
                    OpenROM(binarydata);
                }





                return;
            }

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";

            //openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenROM(openFileDialog1.FileName);
            }
            else
            {
                mainform.Enabled = true;
                this.Close();
            }

        }

        public void OpenROM(string romfile)
        {

            ROM = new List<byte>(File.ReadAllBytes(romfile));

            ROM rom = MainForm.CheckVersion(ROM);

            if (rom.Game == "MM")
            {
                MessageBox.Show("MM unsupported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mainform.Enabled = true;
                this.Close();
                return;
            }


            ROMfile = romfile;
            if (sceneid <= 0x9)
            {
                noupdate = true;
                MapID.Value = sceneid;
                noupdate = false;
            }

            for (int i = 0; i < 32; i++)
            {
                PaletteListBox.Items.Add("Room " + i);
            }



            // Offset = ((int)rom.SubscreenMapCompassIcons + ((int)startfloortexture/2 * 0x1EC));
            LoadMaps();
            LoadFloors();
            RefreshTitleCardTexture();
            RefreshMapTexture();
        }

        public void LoadMaps()
        {
            ROM rom = MainForm.CheckVersion(ROM);

            int Offset;
            int maxfloorstack = 0;
            for (int i = 0; i < 10; i++)
            {
                maplist[i] = new Map();
                Offset = ((int)rom.SubscreenMapFloorAmount + ((int)i * 0x2));
                maplist[i].maxfloors = Helpers.Read16S(ROM, Offset) / 2;
                maplist[i].maxfloors -= maxfloorstack;

                if (i == 8)
                    maxfloorstack = 0;
                else
                    maxfloorstack = Helpers.Read16S(ROM, Offset) / 2;
            }
            Offset = (int)rom.SubscreenMapFloorTextures;
            int Offset2 = (int)rom.SubscreenMapCompassIcons;
            int c = 0;
            foreach (Map map in maplist)
            {
                //    DebugConsole.WriteLine("map [" + c + "] maxfloors " + map.maxfloors+ " start icon offset: " + Offset2.ToString("X8"));
                List<byte> mapdata = new List<byte>();
                for (int i = 0; i < 8; i++)
                {
                    if (i < map.maxfloors)
                    {
                        for (int y = 0; y < 0xFF0; y += 4)
                        {
                            Helpers.Append32(ref mapdata, Helpers.Read32(ROM, Offset + y));
                        }
                        Offset += 0xFF0;
                    }
                    else mapdata.AddRange(new byte[0xFF0]);
                }
                map.imagedata = mapdata;

                List<byte> icondata = new List<byte>();
                for (int i = 0; i < map.maxfloors; i++)
                {
                    for (int y = 0; y < 0x1EC; y += 4)
                    {
                        Helpers.Append32(ref icondata, Helpers.Read32(ROM, Offset2 + y));
                    }
                    Offset2 += 0x1EC;
                }
                map.icondata = icondata;
                // DebugConsole.WriteLine("end icon offset: " + Offset2.ToString("X8") + "\n");
                c++;
            }
        }

        public void LoadFloors()
        {
            changed = false;
            titlecardchanged = false;
            previd = (int)MapID.Value;
            noupdate = true;
            ROM rom = MainForm.CheckVersion(ROM);
            int Offset = ((int)rom.SubscreenMapInfo + ((int)MapID.Value * 0x10));
            floors.Clear();

            for (int i = 0; i < 8; i++)
            {
                MapFloor floor = new MapFloor();
                floor.textureID = ROM[Offset + 1];
                floors.Add(floor);
                Offset += 2;
            }

            Offset = ((int)rom.SubscreenMapInfo + 0xA0 + ((int)MapID.Value * 0x2));
            if (ROM[Offset + 1] <= floors.Count - 1) floors[ROM[Offset + 1]].bosslocated = true;

            Offset = ((int)rom.SubscreenMapInfo2 + ((int)MapID.Value * 0x8));

            for (int i = 0; i < 8; i++)
            {
                floors[i].floorlabel = ROM[Offset];
                Offset += 1;
            }

            selectedfloor = floors.FindIndex(x => x.floorlabel > 0);

            Offset = ((int)rom.SubscreenMapFloorAmount + ((int)MapID.Value * 0x2 - 0x2));

            startfloortexture = Helpers.Read16(ROM, Offset);

            Offset = ((int)rom.SubscreenMapFloorAmount + 0x14A + ((int)MapID.Value * 0x20));

            for (int i = 0; i < 8; i++)
            {
                floors[i].floorheight = (int)BitConverter.ToSingle(BitConverter.GetBytes(Helpers.Read32(ROM, Offset)), 0);
                Offset += 4;
            }

            Offset = ((int)rom.SubscreenMapInfo + 0xB0 + ((int)MapID.Value * 0x40));

            for (int i = 0; i < 32; i++)
            {
                paletteindexes[i] = ROM[Offset + 1 + (i * 2)];
            }

            //Offset = ((int)rom.SubscreenMapCompassIcons + ((int)MapID.Value * 0xF60));
            Offset = ((int)rom.SubscreenMapCompassIcons + ((int)startfloortexture / 2 * 0x1EC));

            compassicons.Clear();

            for (int i = 0; i < 8; i++)
            {
                compassicons.Add(new List<CompassIcon>());

                if (i >= maplist[(int)MapID.Value].maxfloors) continue;

                for (int y = 0; y < 3; y++)
                {

                    bool bossicon = ROM[Offset + (0xA4 * y) + 1] == 1;

                    byte chestnumber = ROM[Offset + (0xA4 * y) + 0x10 + 3];

                    if (chestnumber == 0) continue;

                    for (int w = 0; w < chestnumber; w++)
                    {
                        CompassIcon icon = new CompassIcon();
                        icon.bossicon = bossicon;
                        icon.ChestFlag = Helpers.Read16S(ROM, Offset + 0x14 + (0xA4 * y) + (0xC * w));
                        icon.X = BitConverter.ToSingle(BitConverter.GetBytes(Helpers.Read32(ROM, Offset + 0x14 + (0xA4 * y) + (0xC * w) + 4)), 0);
                        icon.Y = BitConverter.ToSingle(BitConverter.GetBytes(Helpers.Read32(ROM, Offset + 0x14 + (0xA4 * y) + (0xC * w) + 8)), 0);

                        compassicons[i].Add(icon);
                    }
                }

                Offset += 0x1EC;
            }


            UpdateForm();
            noupdate = false;
        }


        public List<MapFloor> GetMapFloors(int ID)
        {
            List<MapFloor> ret = new List<MapFloor>();
            previd = ID;
            noupdate = true;
            ROM rom = MainForm.CheckVersion(ROM);
            int Offset = ((int)rom.SubscreenMapInfo + (ID * 0x10));

            for (int i = 0; i < 8; i++)
            {
                MapFloor floor = new MapFloor();
                floor.textureID = ROM[Offset + 1];
                ret.Add(floor);
                Offset += 2;
            }

            Offset = ((int)rom.SubscreenMapInfo + 0xA0 + (ID * 0x2));
            if (ROM[Offset + 1] <= ret.Count - 1) ret[ROM[Offset + 1]].bosslocated = true;

            Offset = ((int)rom.SubscreenMapInfo2 + (ID * 0x8));

            for (int i = 0; i < 8; i++)
            {
                ret[i].floorlabel = ROM[Offset];
                Offset += 1;
            }

           // selectedfloor = ret.FindIndex(x => x.floorlabel > 0);

            Offset = ((int)rom.SubscreenMapFloorAmount + (ID * 0x2 - 0x2));

           // startfloortexture = Helpers.Read16(ROM, Offset);

            Offset = ((int)rom.SubscreenMapFloorAmount + 0x14A + (ID * 0x20));

            for (int i = 0; i < 8; i++)
            {
                ret[i].floorheight = (int)BitConverter.ToSingle(BitConverter.GetBytes(Helpers.Read32(ROM, Offset)), 0);
                Offset += 4;
            }


            noupdate = false;

            return ret;
        }

        public byte[] GetMapPaletteIndexes(int ID)
        {
            byte[] ret = new byte[32];
            previd = ID;
            noupdate = true;
            ROM rom = MainForm.CheckVersion(ROM);
            int Offset = ((int)rom.SubscreenMapInfo + 0xB0 + (ID * 0x40));

            for (int i = 0; i < 32; i++)
            {
                ret[i] = ROM[Offset + 1 + (i * 2)];
            }

            noupdate = false;

            return ret;
        }


        public List<List<CompassIcon>> GetCompassIcons(int mapID)
        {
            List<List<CompassIcon>> compassicons = new List<List<CompassIcon>>();

            int Offset = 0;

            for (int i = 0; i < 8; i++)
            {
                compassicons.Add(new List<CompassIcon>());

                if (i >= maplist[mapID].maxfloors) continue;

                for (int y = 0; y < 3; y++)
                {

                    bool bossicon = maplist[mapID].icondata[Offset + (0xA4 * y) + 1] == 1;

                    byte chestnumber = maplist[mapID].icondata[Offset + (0xA4 * y) + 0x10 + 3];

                    if (chestnumber == 0) continue;

                    for (int w = 0; w < chestnumber; w++)
                    {
                        CompassIcon icon = new CompassIcon();
                        icon.bossicon = bossicon;
                        icon.ChestFlag = Helpers.Read16S(maplist[mapID].icondata, Offset + 0x14 + (0xA4 * y) + (0xC * w));
                        icon.X = BitConverter.ToSingle(BitConverter.GetBytes(Helpers.Read32(maplist[mapID].icondata, Offset + 0x14 + (0xA4 * y) + (0xC * w) + 4)), 0);
                        icon.Y = BitConverter.ToSingle(BitConverter.GetBytes(Helpers.Read32(maplist[mapID].icondata, Offset + 0x14 + (0xA4 * y) + (0xC * w) + 8)), 0);

                        compassicons[i].Add(icon);
                    }
                }

                Offset += 0x1EC;
            }

            return compassicons;
        }

        public void UpdateForm()
        {
            // public Button[] floorbuttons = { Floor1 };

            SaveToROM.Text = (!rom64.isSet()) ? "Save to ROM" : "Send to z64rom";

            sendToZ64romToolStripMenuItem.Visible = (!rom64.isSet());

            FloorBossSkullIcon.Visible = false;

#if DEBUG
            GenerateMapButton.Visible = true;
#endif

            for (int i = 0; i < 8; i++)
            {
                floorbuttons[i].BackgroundImage = floorimages[floors[i].floorlabel];
                floorbuttons[i].Text = "";
                floorbuttons[i].BackColor = (i == selectedfloor) ? Color.Green : Color.DarkGray;

                if (floors[i].bosslocated)
                {
                    FloorBossSkullIcon.Visible = true;
                    FloorBossSkullIcon.Location = new Point(85, 80 + (29 * i));
                }
            }

            if (selectedfloor == -1) selectedfloor = 0;


            MapBossCheckbox.Checked = floors[selectedfloor].bosslocated;
            MapFloorHeight.Value = floors[selectedfloor].floorheight;
            MapFloorTextureID.Value = floors[selectedfloor].textureID;
            MapFloorLabel.Value = floors[selectedfloor].floorlabel;

            noupdate = true;

            if (PaletteListBox.SelectedIndex == -1) PaletteListBox.SelectedIndex = 0;

            PaletteIndex.Value = paletteindexes[PaletteListBox.SelectedIndex];

            

            RefreshIconList();



            noupdate = false;

        }

        public void RefreshIconList()
        {
            int prevsel = IconListBox.SelectedIndex;

            IconListBox.Items.Clear();

            if (floors[selectedfloor].floorlabel != 0)
            {
                for (int i = 0; i < compassicons[(int)MapFloorTextureID.Value / 2].Count; i++)
                {
                    IconListBox.Items.Add("(X " + compassicons[(int)MapFloorTextureID.Value / 2][i].X + ", Y " + compassicons[(int)MapFloorTextureID.Value / 2][i].Y + ", Flag " + compassicons[(int)MapFloorTextureID.Value / 2][i].ChestFlag.ToString("X1") + ")");
                }
            }

            if (prevsel <= compassicons[(int)MapFloorTextureID.Value / 2].Count - 1 && prevsel < IconListBox.Items.Count) IconListBox.SelectedIndex = prevsel;

            if (IconListBox.SelectedIndex > compassicons[(int)MapFloorTextureID.Value / 2].Count - 1) IconListBox.SelectedIndex = 0;



            if (compassicons[(int)MapFloorTextureID.Value / 2].Count == 0 || floors[selectedfloor].floorlabel == 0)
            {
                IconX.Enabled = false;
                IconY.Enabled = false;
                AddIcon.Enabled = (floors[selectedfloor].floorlabel != 0);
                DeleteIcon.Enabled = false;
                IconBossCheckbox.Enabled = false;
                IconChestFlag.Enabled = false;
                IconListBox.SelectedIndex = -1;
            }
            else
            {
                IconX.Enabled = true;
                IconY.Enabled = true;
                AddIcon.Enabled = (compassicons[(int)MapFloorTextureID.Value / 2].Count < 12);
                DeleteIcon.Enabled = true;
                IconBossCheckbox.Enabled = true;
                IconChestFlag.Enabled = true;
                if (IconListBox.SelectedIndex == -1) IconListBox.SelectedIndex = 0;
            }

            if (IconListBox.SelectedIndex > -1)
            {
                IconX.Value = (decimal)compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].X;
                IconY.Value = (decimal)compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].Y;
                IconBossCheckbox.Checked = compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].bossicon;
                IconChestFlag.Value = (decimal)compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].ChestFlag;

            }
            else
            {
                IconX.Value = 0;
                IconY.Value = 0;
                IconBossCheckbox.Checked = false;
                IconChestFlag.Value = 0;
            }
        }

        public void RefreshTitleCardTexture()
        {
            ROM rom = MainForm.CheckVersion(ROM);
            int Offset = (int)rom.SubscreenMapTitleCards + ((int)MapID.Value * 0x600);


            int size = 0x600;

            Bitmap texture = new Bitmap(96, 16);
            int x = 0;
            int y = 0;
            int gray = 0;
            int alpha = 0;
            Color col = new Color();

            for (int i = 0; i < size; i++)
            {
                //   DebugConsole.WriteLine(((0x4F & 0xF0) >> 4).ToString("X"));
                gray = ((ROM[Offset + i] & 0xF0) >> 4) * 17;
                alpha = (ROM[Offset + i] & 0x0F) * 17;
                col = Color.FromArgb(alpha, gray, gray, gray);
                texture.SetPixel(x, y, col);
                x++;
                if (x >= texture.Width) { x = 0; y++; }
                if (y >= texture.Height) break;
            }

            TitleTextureBox.Image = texture;
            //  DebugConsole.WriteLine("tableoffset: " + (TableOffset + 16).ToString("X"));
        }

        public void RefreshMapTexture(bool export = false)
        {
            //ROM rom = MainForm.CheckVersion(ROM);
            //int Offset = (int)rom.SubscreenMapFloorTextures + ((int)((startfloortexture+MapFloorTextureID.Value)/2) * 0xFF0);
            int Offset = ((int)((MapFloorTextureID.Value)) / 2) * 0xFF0;

            int size = 0xFF0;


            Bitmap texture = new Bitmap(96, 85);
            if (export) MapTextureBox.Image = texture;
            int x = 0;
            int y = 0;
            int gray = 0;
            int alpha = 0;
            Color col = new Color();

            bool secondhalf = false;

            for (int i = 0; i < size; i++)
            {
                alpha = ((maplist[(int)MapID.Value].imagedata[Offset + i] & 0xF0) >> 4) * 17;
                col = Color.FromArgb(alpha, gray, gray, gray);
                if (alpha / 17 == PaletteIndex.Value && !export) col = Color.FromArgb(0xFF, 0xFF, 0x66, 0);
                texture.SetPixel(x, y, col);
                x++;
                if (x >= 48 && !secondhalf) { x = 0; y++; }
                if (x >= texture.Width && secondhalf) { x = 48; y++; }

                if (y >= texture.Height)
                {
                    if (!secondhalf) { y = 0; x = 48; secondhalf = true; }
                    else break;
                }

                alpha = (maplist[(int)MapID.Value].imagedata[Offset + i] & 0x0F) * 17;
                col = Color.FromArgb(alpha, gray, gray, gray);
                if (alpha / 17 == PaletteIndex.Value && !export) col = Color.FromArgb(0xFF, 0xFF, 0x66, 0);
                texture.SetPixel(x, y, col);
                x++;
                if (x >= 48 && !secondhalf) { x = 0; y++; }
                if (x >= texture.Width && secondhalf) { x = 48; y++; }

                if (y >= texture.Height)
                {
                    if (!secondhalf) { y = 0; x = 48; secondhalf = true; }
                    else break;
                }
            }


            Graphics g = MapTextureBox.CreateGraphics();
            g.Clear(Color.Cyan);

            if (floors[selectedfloor].floorlabel != 0)
            {

                g.DrawImage(texture, 0, 0, MapTextureBox.Width, MapTextureBox.Height);


                Bitmap chesticon = new Bitmap(Resources.MinimapChest);
                Bitmap bossicon = new Bitmap(Resources.MinimapBoss);
                Bitmap selecticon = new Bitmap(Resources.MinimapSelection);
                for (int i = 0; i < compassicons[(int)MapFloorTextureID.Value / 2].Count; i++)
                {

                    if (!compassicons[(int)MapFloorTextureID.Value / 2][i].bossicon)
                    {
                        float X = compassicons[(int)MapFloorTextureID.Value / 2][i].X * 4 - 16;
                        float Y = -compassicons[(int)MapFloorTextureID.Value / 2][i].Y * 4 + 12;
                        g.DrawImage(chesticon, X, Y, 32f, 32f);
                        if (IconListBox.SelectedIndex == i)
                        {
                            g.DrawImage(selecticon, X -1, Y -1, 32f, 32f);
                        }
                    }
                    else
                    {
                        float X = compassicons[(int)MapFloorTextureID.Value / 2][i].X * 4 - 16;
                        float Y = -compassicons[(int)MapFloorTextureID.Value / 2][i].Y * 4 + 12;
                        g.DrawImage(bossicon, X, Y, 32f, 32f);
                        if (IconListBox.SelectedIndex == i)
                        {
                            g.DrawImage(selecticon, X -1, Y -1, 32f, 32f);
                        }
                    }



                    g = MapTextureBox.CreateGraphics();

                }

            }
            //MapTextureBox.Image = texture;
        }


        private void TitleCardLoad(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image (PNG) (*.png)|*.png";

            //openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                Bitmap texture = new Bitmap(openFileDialog1.FileName);
                if (texture.Width != 96 || texture.Height != 16)
                {
                    MessageBox.Show("Your image must be 96x16!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int x = 0;
                int y = 0;
                int gray = 0;
                int alpha = 0;
                Color col = new Color();

                for (int i = 0; y < texture.Height; i++)
                {
                    //   DebugConsole.WriteLine(((0x4F & 0xF0) >> 4).ToString("X"));
                    col = Color.FromArgb(texture.GetPixel(x, y).A, texture.GetPixel(x, y).R, texture.GetPixel(x, y).R, texture.GetPixel(x, y).R);
                    texture.SetPixel(x, y, col);
                    x++;
                    if (x >= texture.Width) { x = 0; y++; }
                    if (y >= texture.Height) break;
                }

                TitleTextureBox.Image = texture;
                titlecardchanged = true;

            }
        }

        private void ExtractButton_Click(object sender, EventArgs e)
        {
            if (TitleTextureBox.Image == null) return;

            saveFileDialog1.CheckFileExists = false;
            //    saveFileDialog1.FileName = Path.GetFileName(OldFileName) + "_new";
            saveFileDialog1.Filter = "Image (*.png)|*.png";
            saveFileDialog1.CreatePrompt = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                TitleTextureBox.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);

                MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }






        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void MapID_ValueChanged(object sender, EventArgs e)
        {
            if (!noupdate)
            {
                if ((changed || titlecardchanged))
                {
                    if (MessageBox.Show("You have unsaved changes, want to save first?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        noupdate = true;
                        int temp = (int)MapID.Value;
                        MapID.Value = previd;

                        if (titlecardchanged) savetitlecard();
                        savemap();

                        MapID.Value = temp;
                        noupdate = false;
                    }
                }

                LoadFloors();
                RefreshMapTexture();
                RefreshTitleCardTexture();

            }




        }

        public void savetitlecard()
        {
            BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROMfile));

            ROM rom = MainForm.CheckVersion(ROM);
            BWS.Seek((int)rom.SubscreenMapTitleCards + ((int)MapID.Value * 0x600), SeekOrigin.Begin);

            List<Byte> Output = GenerateTitlecardTextureData();

            

            BWS.Write(Output.ToArray());

            BWS.Close();

            ROM = new List<byte>(File.ReadAllBytes(ROMfile));

            RefreshTitleCardTexture();

            titlecardchanged = false;
        }

        public void savemap(bool showdone = true)
        {
            maplist[(int)MapID.Value].maxfloors = 0;
            for (int i = 0; i < floors.Count; i++)
            {
                if (floors[i].floorlabel != 0) maplist[(int)MapID.Value].maxfloors++;
            }
            int romfloors = 0;
            for (int i = 0; i < maplist.Length; i++)
            {
                romfloors += maplist[i].maxfloors;
            }
            if (romfloors > 34)
            {
                MessageBox.Show("The sum of all floors of all dungeon maps in the ROM can't exceed 34! you may want to remove all original maps first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // =============================================================
            // Sort texture IDs so they're consecutive for least space usage
            List<MapFloor> sortedfloors = floors.FindAll(x => x.floorlabel > 0).OrderBy(x => x.textureID).ToList();
            
            byte[] imagedata = maplist[(int)MapID.Value].imagedata.ToArray();
            byte[] newimagedata = new byte[maplist[(int)MapID.Value].imagedata.Count];
            int textureId = 0;

            foreach (MapFloor floor in sortedfloors)
            {
                Array.Copy(imagedata, 0xFF0 * (floor.textureID / 2), newimagedata, (textureId / 2) * 0xFF0, 0xFF0);
                floor.textureID = textureId;
                textureId += 2;
            }

            maplist[(int)MapID.Value].imagedata = newimagedata.ToList();

            // =============================================================


            if (MainForm.IsFileLocked(ROMfile))
            {
                MessageBox.Show("ROM is in use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROMfile));

            ROM rom = MainForm.CheckVersion(ROM);

            List<Byte> Output = new List<byte>();

            BWS.Seek(((int)rom.SubscreenMapInfo + ((int)MapID.Value * 0x10)), SeekOrigin.Begin);

            for (int i = 0; i < 8; i++)
            {
                Helpers.Append16(ref Output, (ushort)floors[i].textureID);

            }
            BWS.Write(Output.ToArray());
            Output.Clear();

            BWS.Seek(((int)rom.SubscreenMapInfo + 0xA0 + ((int)MapID.Value * 0x2)), SeekOrigin.Begin);
            int bossroom = (ushort)(floors.FindIndex(x => x.bosslocated) != -1 ? floors.FindIndex(x => x.bosslocated) : 9);
            Helpers.Append16(ref Output, (ushort)bossroom);
            BWS.Write(Output.ToArray());
            Output.Clear();

            BWS.Seek(((int)rom.SubscreenMapInfo2 + ((int)MapID.Value * 0x8)), SeekOrigin.Begin);

            for (int i = 0; i < 8; i++)
            {
                Output.Add((byte)floors[i].floorlabel);
            }

            BWS.Write(Output.ToArray());
            Output.Clear();

            BWS.Seek(((int)rom.SubscreenMapInfo2 + 0x50 + ((int)MapID.Value * 0x2)), SeekOrigin.Begin);

            Helpers.Append16(ref Output, (ushort)((bossroom != 9) ? (51 - (14 * bossroom)) : -99));

            BWS.Write(Output.ToArray());
            Output.Clear();


            BWS.Seek((int)rom.SubscreenMapFloorAmount - 2, SeekOrigin.Begin);
            ushort roomstack = 0;

            for (int i = 0; i < 10; i++)
            {
                Helpers.Append16(ref Output, roomstack);

                roomstack += (ushort)(maplist[i].maxfloors * 2);

                if (i == 9)
                    Helpers.Append16(ref Output, (ushort)(maplist[i].maxfloors * 2));
            }

            BWS.Write(Output.ToArray());
            Output.Clear();


            BWS.Seek(((int)rom.SubscreenMapFloorAmount + 0x14A + ((int)MapID.Value * 0x20)), SeekOrigin.Begin);

            for (int i = 0; i < 8; i++)
            {
                Output.AddRange(BitConverter.GetBytes((float)floors[i].floorheight).Reverse());

            }
            BWS.Write(Output.ToArray());
            Output.Clear();

            BWS.Seek(((int)rom.SubscreenMapInfo + 0xB0 + ((int)MapID.Value * 0x40)), SeekOrigin.Begin);


            for (int i = 0; i < 32; i++)
            {
                Helpers.Append16(ref Output, paletteindexes[i]);
            }
            BWS.Write(Output.ToArray());
            Output.Clear();


            BWS.Seek(((int)rom.SubscreenMapCompassIcons), SeekOrigin.Begin);

            for (int i = 0; i < (int)MapID.Value; i++)
            {
                Output.AddRange(maplist[i].icondata);
            }

            int c = 0;
            foreach (List<CompassIcon> compassiconlist in compassicons)
            {
                if (c >= maplist[(int)MapID.Value].maxfloors) break;
                List<CompassIcon> usableicons = compassiconlist.FindAll(x => x.bossicon == false);
                int padding = 0xA2;




                if (usableicons.Count > 0)
                {
                    Helpers.Append16(ref Output, (ushort)((usableicons.Count > 0) ? 0 : 0xFFFF)); //
                    Helpers.Append16(ref Output, 0); //icon type
                    Helpers.Append32(ref Output, 0x17); //????
                    Helpers.Append32(ref Output, rom.SubscreenMapChestVertexData); //vertex data
                    Helpers.Append32(ref Output, 4); //num vertices
                    Helpers.Append32(ref Output, (uint)usableicons.Count); //icon amount
                    foreach (CompassIcon icon in usableicons)
                    {
                        Helpers.Append16(ref Output, (ushort)icon.ChestFlag);
                        Helpers.Append16(ref Output, 0); // padding
                        Output.AddRange(BitConverter.GetBytes(icon.X).Reverse());
                        Output.AddRange(BitConverter.GetBytes(icon.Y).Reverse());
                    }

                    Output.AddRange(new byte[0xC * (12 - usableicons.Count)]);
                }
                else padding += 0xA4;

                usableicons = compassiconlist.FindAll(x => x.bossicon == true);
                if (usableicons.Count > 0)
                {
                    Helpers.Append16(ref Output, (ushort)((usableicons.Count > 0) ? 1 : 0xFFFF)); //icon type
                    Helpers.Append16(ref Output, 0);
                    Helpers.Append32(ref Output, 0x17); //????
                    Helpers.Append32(ref Output, rom.SubscreenMapChestVertexData - 0x40); //vertex data
                    Helpers.Append32(ref Output, 4); //num vertices
                    Helpers.Append32(ref Output, (uint)usableicons.Count); //icon amount
                    foreach (CompassIcon icon in usableicons)
                    {
                        Helpers.Append16(ref Output, 0xFFFF);
                        Helpers.Append16(ref Output, 0); // padding
                        Output.AddRange(BitConverter.GetBytes(icon.X).Reverse());
                        Output.AddRange(BitConverter.GetBytes(icon.Y).Reverse());
                    }
                    Output.AddRange(new byte[0xC * (12 - usableicons.Count)]);
                }
                else padding += 0xA4;


                Helpers.Append16(ref Output, 0xFFFF); //icon type
                Output.AddRange(new byte[padding]); // unused icon
                c++;
            }



            for (int i = (int)MapID.Value + 1; i < 10; i++)
            {
                Output.AddRange(maplist[i].icondata);
            }

            BWS.Write(Output.ToArray());
            Output.Clear();

            List<uint> offsets = new List<uint>();
            foreach (string s in rom.SubscreenPatch.Split(','))
            {
                offsets.Add(Convert.ToUInt32(s, 16));
            }
            if (offsets.Count == 3)
            {
                BWS.Seek((int)offsets[0], SeekOrigin.Begin);
                if (Helpers.Read32(ROM, (int)offsets[0]) == offsets[1])
                {
                    Helpers.Append32(ref Output, offsets[2]);
                    BWS.Write(Output.ToArray());
                    Output.Clear();
                }

            }


            BWS.Seek(((int)rom.SubscreenMapFloorTextures), SeekOrigin.Begin);

            Output.AddRange(GenerateMapTextureData()); 


            BWS.Write(Output.ToArray());
            Output.Clear();

            //discovery flag bug fix
            BWS.Seek(((int)rom.SubscreenMapInfo + 0x344 + ((int)MapID.Value * 28)), SeekOrigin.Begin);

            for (uint i = 0; i < 0xE; i++)
            {
                Helpers.Append32(ref Output, i);
            }

            BWS.Write(Output.ToArray());
            Output.Clear();


            BWS.Close();



            ROM = new List<byte>(File.ReadAllBytes(ROMfile));

            changed = false;




            if (showdone) MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // Reload the floors, since they may have changed due to the texture ID re-sort
            LoadMaps();
            LoadFloors();
            RefreshMapTexture();
        }

        private void MapFloorTextureID_ValueChanged(object sender, EventArgs e)
        {
            if (!noupdate)
            {
                floors[selectedfloor].textureID = (int)MapFloorTextureID.Value;
                changed = true;
                RefreshMapTexture();
            }
        }

        private void Floor1_Click(object sender, EventArgs e)
        {
            int buttonid = Convert.ToInt32(((Button)sender).Name.Substring(5));
            selectedfloor = buttonid - 1;
            noupdate = true;
            UpdateForm();
            RefreshMapTexture();
            noupdate = false;
        }

        private void MapBossCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
                floors[i].bosslocated = false;
            floors[selectedfloor].bosslocated = MapBossCheckbox.Checked;
            UpdateForm();
        }

        private void MapFloorLabel_ValueChanged(object sender, EventArgs e)
        {
            if (!noupdate)
            {
                floors[selectedfloor].floorlabel = (int)MapFloorLabel.Value;
                changed = true;
                UpdateForm();
            }
        }

        private void MapFloorHeight_ValueChanged(object sender, EventArgs e)
        {
            if (!noupdate)
            {
                floors[selectedfloor].floorheight = (int)MapFloorHeight.Value;
                changed = true;
                UpdateForm();
            }
        }

        private void IconListBox_Click(object sender, EventArgs e)
        {
            UpdateForm();
            RefreshMapTexture();
        }

        private void AddIcon_Click(object sender, EventArgs e)
        {
            compassicons[(int)MapFloorTextureID.Value / 2].Add(new CompassIcon());
            IconListBox.Items.Add(" ");
            IconListBox.SelectedIndex = IconListBox.Items.Count - 1;

            UpdateForm();
            RefreshMapTexture();
        }

        private void DeleteIcon_Click(object sender, EventArgs e)
        {
            compassicons[(int)MapFloorTextureID.Value / 2].RemoveAt(IconListBox.SelectedIndex);
            if (IconListBox.SelectedIndex > 0) IconListBox.SelectedIndex--;

            UpdateForm();
            RefreshMapTexture();
        }

        private void IconX_ValueChanged(object sender, EventArgs e)
        {
            if (!noupdate)
            {
                noupdate = true;
                if (Control.ModifierKeys == Keys.Shift && short.MaxValue - Math.Abs(IconX.Value) > 10)
                    IconX.Value += (IconX.Value - (decimal)compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].X) * 9;

                noupdate = false;
                compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].X = (short)IconX.Value;
                changed = true;
                RefreshMapTexture();
                RefreshIconList();
            }
        }

        private void IconY_ValueChanged(object sender, EventArgs e)
        {
            if (!noupdate)
            {
                noupdate = true;
                if (Control.ModifierKeys == Keys.Shift && short.MaxValue - Math.Abs(IconY.Value) > 10)
                    IconY.Value += (IconY.Value - (decimal)compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].Y) * 9;

                noupdate = false;




                compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].Y = (short)IconY.Value;
                changed = true;
                RefreshMapTexture();
                RefreshIconList();
            }
        }

        private void IconBossCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!noupdate)
            {
                compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].bossicon = IconBossCheckbox.Checked;
                changed = true;
                RefreshMapTexture();
            }
        }

        private void IconChestFlag_ValueChanged(object sender, EventArgs e)
        {
            if (!noupdate)
            {
                compassicons[(int)MapFloorTextureID.Value / 2][IconListBox.SelectedIndex].ChestFlag = (short)IconChestFlag.Value;
                changed = true;
                RefreshIconList();
            }
        }

        private void PaletteListBox_Click(object sender, EventArgs e)
        {
            UpdateForm();
            RefreshMapTexture();
        }

        private void PaletteIndex_ValueChanged(object sender, EventArgs e)
        {
            if (!noupdate)
            {
                paletteindexes[PaletteListBox.SelectedIndex] = (byte)PaletteIndex.Value;
                changed = true;
                RefreshMapTexture();
            }
        }

        private void RemoveFloorButton_Click(object sender, EventArgs e)
        {
            floors[selectedfloor].floorheight = 9999;
            floors[selectedfloor].floorlabel = 0;
            noupdate = true;
            UpdateForm();
            RefreshMapTexture();
            noupdate = false;
        }

        private void SubscreenMapEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            //mainform.Enabled = true;
            MainForm.subscreenmapeditor_visible = false;
        }

        private void GenerateMap_Click(object sender, EventArgs e)
        {
            if (MainForm.CurrentScene.Rooms.Count == 0) return;



            mainform.Enabled = true;

            MainForm.subscreenmode = true;


            OpenTK.Vector3d MinCoordinate = new Vector3d(32766, 32766, 32766);
            OpenTK.Vector3d MaxCoordinate = new Vector3d(-32766, -32766, -32766);


            foreach (ObjFile.Vertex Vtx in MainForm.CurrentScene.ColModel.Vertices)
            {
                /* Minimum... */
                MinCoordinate.X = Math.Min(MinCoordinate.X, Vtx.X * MainForm.CurrentScene.Scale);
                MinCoordinate.Y = Math.Min(MinCoordinate.Y, Vtx.Y * MainForm.CurrentScene.Scale);
                MinCoordinate.Z = Math.Min(MinCoordinate.Z, Vtx.Z * MainForm.CurrentScene.Scale);

                /* Maximum... */
                MaxCoordinate.X = Math.Max(MaxCoordinate.X, Vtx.X * MainForm.CurrentScene.Scale);
                MaxCoordinate.Y = Math.Max(MaxCoordinate.Y, Vtx.Y * MainForm.CurrentScene.Scale);
                MaxCoordinate.Z = Math.Max(MaxCoordinate.Z, Vtx.Z * MainForm.CurrentScene.Scale);

            }

            MaxCoordinate.Y = Math.Max((MaxCoordinate.X - MinCoordinate.X), (MaxCoordinate.Z - MinCoordinate.Z)) * 2;

            Camera.Pos = MainForm.ConvertToCameraPosition(new Vector3d((MaxCoordinate.X + MinCoordinate.X) / 2, MaxCoordinate.Y, (MaxCoordinate.Z + MinCoordinate.Z) / 2));

            Camera.Rot.Y = 0;
            Camera.Rot.X = 90;


            Bitmap texture = new Bitmap(96, 85);

            //Graphics g = MapTextureBox.CreateGraphics();
            Graphics g = Graphics.FromImage(texture);
            g = MapTextureBox.CreateGraphics();

            g.Clear(Color.Cyan);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //MapTextureBox.Image = texture;
            // MapTextureBox.Scale(new SizeF(0.25f,0.25f));




            for (int i = 0; i < MainForm.CurrentScene.Rooms.Count; i++)
            {



                MainForm.subscreenroom = i;

                mainform.PaintControl();

                mainform.GLrefresh();

                Bitmap bitmap = mainform.TakeScreenshot();

                //bitmap.SetResolution(96f,85f);

                ColorMap[] colorMap = new ColorMap[2];
                colorMap[0] = new ColorMap();
                colorMap[0].OldColor = Color.Aqua;
                colorMap[0].NewColor = Color.Transparent;
                colorMap[1] = new ColorMap();
                colorMap[1].OldColor = Color.Black;
                colorMap[1].NewColor = Color.FromArgb(0x17 * (i + 1), 0, 0, 0);
                ImageAttributes attr = new ImageAttributes();
                attr.SetRemapTable(colorMap);

                Rectangle rect = new Rectangle(0, 0, MapTextureBox.Width, MapTextureBox.Height);
                g.DrawImage(bitmap, rect, (bitmap.Width - MapTextureBox.Width) / 2, (bitmap.Height - MapTextureBox.Height) / 2, MapTextureBox.Width, MapTextureBox.Height, GraphicsUnit.Pixel, attr);

                g = MapTextureBox.CreateGraphics();
            }

            g.DrawImage(texture, new Rectangle(0, 0, MapTextureBox.Width, MapTextureBox.Height));

            //g = Graphics.FromImage(texture);

            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //MapTextureBox.Image = texture;



            // MapTextureBox.Scale(new SizeF(4f, 4f));
            //  Bitmap toscale = new Bitmap(MapTextureBox.Image);
            //   g.Clear(Color.Cyan);
            //    Rectangle rect2 = new Rectangle(0, 0, MapTextureBox.Width * 2, MapTextureBox.Height*2);
            //   g.DrawImage(toscale, rect2, 0, 0, MapTextureBox.Width, MapTextureBox.Height, GraphicsUnit.Pixel);

            //  g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //  g.ScaleTransform((float)4, (float)4);




            mainform.Enabled = false;


            MainForm.subscreenmode = false;

        }

        private void ClearAllMapsButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will remove all dungeon maps from the ROM in order to obtain free space. Continue?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (MainForm.IsFileLocked(ROMfile))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    ROM = new List<byte>(File.ReadAllBytes(ROMfile));

                    ROM rom = MainForm.CheckVersion(ROM);

                    BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROMfile));
                    int offset = (int)rom.SubscreenMapInfo;
                    BWS.Seek(offset, SeekOrigin.Begin);

                    byte[] Output = new byte[0x330];

                    BWS.Write(Output.ToArray());


                    offset = (int)rom.SubscreenMapInfo2;
                    BWS.Seek(offset, SeekOrigin.Begin);
                    Output = new byte[0x50];

                    BWS.Write(Output.ToArray());

                    offset = (int)rom.SubscreenMapFloorAmount;
                    BWS.Seek(offset, SeekOrigin.Begin);
                    Output = new byte[0x14];

                    BWS.Write(Output.ToArray());


                    offset = ((int)rom.SubscreenMapCompassIcons);
                    BWS.Seek(offset, SeekOrigin.Begin);
                    Output = new byte[34 * 0x1EC];

                    compassicons.Clear();

                    BWS.Write(Output.ToArray());

                    foreach (Map map in maplist)
                    {
                        map.imagedata = new List<byte>(new byte[map.imagedata.Count]);
                        map.maxfloors = 0;
                    }

                    BWS.Close();


                    MessageBox.Show("All dungeon maps has been removed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ROM = new List<byte>(File.ReadAllBytes(ROMfile));

                    LoadFloors();
                    RefreshMapTexture();
                }
            }
        }

        private void MapLoadFromFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image (PNG) (*.png)|*.png";

            //openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                Bitmap texture = new Bitmap(openFileDialog1.FileName);
                if (texture.Width != 96 || texture.Height != 85)
                {
                    MessageBox.Show("Your image must be 96x85!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int x = 0;
                int y = 0;
                int gray = 0;
                int alpha = 0;
                Color col = new Color();

                for (int i = 0; y < texture.Height; i++)
                {
                    //   DebugConsole.WriteLine(((0x4F & 0xF0) >> 4).ToString("X"));
                    col = Color.FromArgb(texture.GetPixel(x, y).A, texture.GetPixel(x, y).R, texture.GetPixel(x, y).R, texture.GetPixel(x, y).R);
                    texture.SetPixel(x, y, col);
                    x++;
                    if (x >= texture.Width) { x = 0; y++; }
                    if (y >= texture.Height) break;
                }

                MapTextureBox.Image = texture;
                changed = true;


                x = 0;
                y = 0;
                byte pixel;
                int size = 0xFF0;

                bool secondhalf = false;

                for (int i = 0; i < size; i++)
                {
                    pixel = (byte)(((texture.GetPixel(x, y).A / 17) << 4) + texture.GetPixel(x + 1, y).A / 17);
                    maplist[(int)MapID.Value].imagedata[(((int)MapFloorTextureID.Value / 2) * 0xFF0) + i] = pixel;

                    x += 2;
                    if (x >= 48 && !secondhalf) { x = 0; y++; }
                    if (x >= texture.Width && secondhalf) { x = 48; y++; }

                    if (y >= texture.Height)
                    {
                        if (!secondhalf) { y = 0; x = 48; secondhalf = true; }
                        else break;
                    }

                }
                MapTextureBox.Refresh();
                RefreshMapTexture();
                MapTextureBox.Refresh();


            }
        }

        private void MapExtract_Click(object sender, EventArgs e)
        {
            RefreshMapTexture(true);

            saveFileDialog1.CheckFileExists = false;
            //    saveFileDialog1.FileName = Path.GetFileName(OldFileName) + "_new";
            saveFileDialog1.Filter = "Image (*.png)|*.png";
            saveFileDialog1.CreatePrompt = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                MapTextureBox.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);

                MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            RefreshMapTexture(false);
        }

        private void SaveToROM_Click(object sender, EventArgs e)
        {
            if (!rom64.isSet())
            {
                if (titlecardchanged) savetitlecard();

                savemap();
            }
            else
            {
                sendToZ64romProjectToolStripMenuItem_Click(new object(), new EventArgs());

            }

        }

        private void TitleCardCopyFromScene_Click(object sender, EventArgs e)
        {

        }

        private void AutoPlaceIcons_Click(object sender, EventArgs e)
        {
            if (MainForm.CurrentScene == null) return;

            int icons = 0;

            for (int i = 0; i < 8; i++)
            {
                compassicons[i].Clear();
            }
            int stack = 4;

            for (int i = 0; i < MainForm.CurrentScene.Rooms.Count; i++)
            {
                foreach (ZActor actor in MainForm.CurrentScene.Rooms[i].ZActors)
                {
                    if (actor.Number == 0x000A)
                    {
                        CompassIcon icon = new CompassIcon();
                        icon.X = stack;
                        icon.ChestFlag = (short)(actor.Variable & 0x1F);
                        stack += 4;

                        for (int y = 0; y < floors.Count - 1; y++)
                        {
                            if (y == floors.Count - 1 || (actor.YPos >= floors[y].floorheight))
                            {
                                compassicons[floors[y].textureID / 2].Add(icon);
                                icons++;
                                break;
                            }
                        }
                    }
                }

            }

            foreach (ZActor actor in MainForm.CurrentScene.Transitions)
            {
                if (actor.Number == 0x002E && (actor.Variable & 0x0140) == 0x0140)
                {
                    CompassIcon icon = new CompassIcon();
                    icon.X = stack;
                    icon.ChestFlag = 0x1F;
                    icon.bossicon = true;
                    stack += 4;

                    for (int y = 0; y < floors.Count - 1; y++)
                    {
                        if (y == floors.Count - 1 || (actor.YPos >= floors[y].floorheight))
                        {
                            compassicons[floors[y].textureID / 2].Add(icon);
                            icons++;
                            break;
                        }
                    }
                }
            }

            MessageBox.Show("Added " + icons + " icons", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            RefreshMapTexture();
            changed = true;
            UpdateForm();
        }





        private void MapID_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.CheckFileExists = false;
            //    saveFileDialog1.FileName = Path.GetFileName(OldFileName) + "_new";
            saveFileDialog1.Filter = "Save file binary (*.bin)|*.bin|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                List<Byte> SaveData = maplist[(int)MapID.Value].icondata;
                File.WriteAllBytes(saveFileDialog1.FileName, SaveData.ToArray());
                MessageBox.Show("Done! File Size: " + SaveData.Count.ToString("X") + " bytes", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        public List<Byte> GenerateMapTextureData()
        {
            List<Byte> Output = new List<byte>();


            for (int i = 0; i < 10; i++)
            {
                List<byte> data = new List<byte>(maplist[i].imagedata);
                data.RemoveRange(maplist[i].maxfloors * 0xFF0, maplist[i].imagedata.Count - (maplist[i].maxfloors * 0xFF0));
                Output.AddRange(data);
                DebugConsole.WriteLine("data amount " + data.Count.ToString("X8"));
            }

            return Output;
        }

        public List<Byte> GenerateTitlecardTextureData()
        {
            List<Byte> Output = new List<byte>();

            Bitmap texture = (Bitmap)TitleTextureBox.Image;

            int x = 0;
            int y = 0;
            int gray = 0;
            int alpha = 0;

            for (int i = 0; y < 16; i++)
            {

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

            return Output;
        }

        private void map48x85staticbinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.Filter = "Save file binary (*.bin)|*.bin|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.FileName = "map_48x85_static.bin";


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Helpers.IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    List<Byte> TextureData = GenerateMapTextureData();
                    File.WriteAllBytes(saveFileDialog1.FileName, TextureData.ToArray());
                    MessageBox.Show("Done! File Size: " + TextureData.Count.ToString("X") + " bytes", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }


        private void sendToZ64romProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {


            string path = "",basepath = "";

            if (rom64.isSet())
            { 
                path = (rom64.getPath() + "\\src\\system\\kaleido\\0x00-StartMenu\\KaleidoMapMarkData.c");
                basepath = rom64.getPath() + "\\";
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "z64rom project (z64project.toml)|z64project.toml|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName;

                    basepath = path;

                    if (path.Contains("z64project.toml"))
                        path = Path.GetDirectoryName(path) + "\\src\\system\\kaleido\\0x00-StartMenu\\KaleidoMapMarkData.c";
                    else
                    {
                        MessageBox.Show("invalid config file, you need to import z64project.toml", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else return;
            }

            if (titlecardchanged) savetitlecard();

            savemap(false);

            // map static
            string mapstaticpath = Path.GetDirectoryName(basepath) + "\\rom\\system\\static\\map_48x85_static.bin";
            if (File.Exists(mapstaticpath)) File.Delete(mapstaticpath);
            List<Byte> TextureData = GenerateMapTextureData();
            File.WriteAllBytes(mapstaticpath, TextureData.ToArray());

            // icon_item_nes_static
            string nesstaticpath = Path.GetDirectoryName(basepath) + "\\rom\\system\\static\\icon_item_NES_static.bin";
            ExportTitlecardTextures(nesstaticpath);


            //chest locations
            if (File.Exists(path)) File.Delete(path);

            StreamWriter sw = File.CreateText(path);

            string output = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Files/KaleidoMapMarkData.c"));

            for (int i = 0; i < maplist.Length; i++)
            {
                List<List<CompassIcon>> tmpcompassicons = i == MapID.Value ? compassicons : GetCompassIcons(i);

                int c = 0;
                foreach (List<CompassIcon> compassiconlist in tmpcompassicons)
                {
                    if (c >= maplist[i].maxfloors) break;
                    List<CompassIcon> usableicons = compassiconlist.FindAll(x => x.bossicon == false);

                    output += "// Scene 0x" + i.ToString("X2") + " map " + c + "\n{\n";

                    if (usableicons.Count > 0)
                    {
                        output += "{\n PAUSE_MAP_MARK_CHEST,\n" +
                            "23,\n" +
                            "sMarkChestVtx,\n" +
                            "4,\n" +
                            (uint)usableicons.Count + ",\n" +
                            "{\n";
                        foreach (CompassIcon icon in usableicons)
                        {
                            output += "{ " + icon.ChestFlag + ",     " + icon.X.ToString() + ", " + icon.Y.ToString() + "},\n";
                        }
                        output += "} },\n";
                    }

                    usableicons = compassiconlist.FindAll(x => x.bossicon == true);
                    if (usableicons.Count > 0)
                    {
                        output += "{\n PAUSE_MAP_MARK_BOSS,\n" +
                            "23,\n" +
                            "sMarkChestVtx,\n" +
                            "4,\n" +
                            (uint)usableicons.Count + ",\n" +
                            "{\n";
                        foreach (CompassIcon icon in usableicons)
                        {
                            output += "{ " + icon.ChestFlag + ", " + icon.X.ToString() + ", " + icon.Y.ToString() + "},\n";
                        }
                        output += "} },\n";
                    }

                    output += "{ PAUSE_MAP_MARK_NONE, 0,      NULL, 0, 0, { 0} },\n" +
                        "},\n";

                    c++;
                }
            }

            output += "\n};";
            sw.Write(output);
            sw.Close();

            //copy static minimap and source files
            string z_map_data_minimap = Path.GetDirectoryName(basepath) + "\\src\\lib_user\\library\\z_map_data_minimap.h";
            string z_map_exp_SO = Path.GetDirectoryName(basepath) + "\\src\\lib_user\\library\\z_map_exp_SO.c";
            string subpath = Path.GetDirectoryName(basepath) + "\\src\\lib_user\\library\\z_map_data_subscreenmap.h";
            if (File.Exists(z_map_data_minimap)) File.Delete(z_map_data_minimap);
            if (File.Exists(z_map_exp_SO)) File.Delete(z_map_exp_SO);
            File.Copy(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Files/z_map_data_minimap.h"), z_map_data_minimap);
            File.Copy(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Files/z_map_exp_SO.c"), z_map_exp_SO);

            //subscreen
            if (File.Exists(subpath)) File.Delete(subpath);

            sw = File.CreateText(subpath);

            output = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Files/z_map_data_subscreenmap.h"));

            

            List<List<MapFloor>> MapFloors = new List<List<MapFloor>>();
            List<byte[]> PaletteIndexes = new List<byte[]>();

            for (int m = 0; m < maplist.Length; m++)
            {
                if (m != MapID.Value)
                    MapFloors.Add(GetMapFloors(m));
                else
                    MapFloors.Add(floors);
                PaletteIndexes.Add(GetMapPaletteIndexes(m));
            }

            output += "static u8 sFloorID[10][8] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {

                output += "{";


                for (int i = 0; i < 8; i++)
                {
                    output += MapFloors[m][i].floorlabel + ",";
                }

                output += "},\n";

            }

            output += "};\n";



            output += "static s16 sFloorTexIndexOffset[10][8] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {
                output += "{";


                for (int i = 0; i < 8; i++)
                {
                    output += MapFloors[m][i].textureID + ",";
                }

                output += "},\n";

            }

            output += "};\n";




            /*
            output += "static u8 test[10][8] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {
                ushort roomstack = 0;
                output += "{";


                for (int i = 0; i < 10; i++)
                {
                    output += roomstack + ",";

                    roomstack += (ushort)(maplist[i].maxfloors * 2);
                    if (i == 9)
                        output += maplist[i].maxfloors * 2 + ",";
                }

                output += "},\n";

            }

            output += "},\n";*/


            output += "static f32 sFloorCoordY[10][8] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {

                output += "{";


                for (int i = 0; i < 8; i++)
                {
                    output += MapFloors[m][i].floorheight + ",";
                }

                output += "},\n";

            }

            output += "};\n";


            output += "static s16 sRoomPalette[10][32] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {

                output += "{";


                for (int i = 0; i < 32; i++)
                {
                    output += PaletteIndexes[m][i] + ",";
                }

                output += "},\n";

            }

            output += "};\n";


            output += "static s16 sBossFloor[10] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {
                int bossroom = (ushort)(MapFloors[m].FindIndex(x => x.bosslocated) != -1 ? MapFloors[m].FindIndex(x => x.bosslocated) : 9);

                output += bossroom + ",";


            }

            output += "};\n";


            output += "static s16 sSkullFloorIconY[10] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {
                int bossroom = (ushort)(MapFloors[m].FindIndex(x => x.bosslocated) != -1 ? MapFloors[m].FindIndex(x => x.bosslocated) : 9);

                output += ((bossroom != 9) ? (51 - (14 * bossroom)) : -99) + ",";


            }

            output += "};\n";


            output += "static s16 sMaxPaletteCount[10] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {

                output += 14 + ",";


            }

            output += "};\n";

            output += "static s16 sPaletteRoom[10][8][14] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {

                output += "{";


                for (int i = 0; i < 8; i++)
                {
                    output += "{0,1,2,3,4,5,6,7,8,9,10,11,12,13},\n";
                }

                output += "},\n";

            }

            output += "};\n";
            

            output += "static u16 sSwitchEntryCount[10] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {

                output += 1 + ",";


            }

            output += "};\n";

            output += "static u8 sSwitchFromRoom[10][51] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {
                output += "{255},\n";
            }

            output += "};\n";

            output += "static u8 sSwitchFromFloor[10][51] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {
                output += "{255},\n";


            }

            output += "};\n";

            output += "static u8 sSwitchToRoom[10][51] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {
                output += "{255},\n";

            }

            output += "};\n";

            ushort roomstack = 0;

            output += "static u16 sDgnTexIndexBase[10] = {\n";

            for (int m = 0; m < maplist.Length; m++)
            {
                output += roomstack + ",";
                roomstack += (ushort)(maplist[m].maxfloors * 2);
            }

            output += "\n};\n";



            //output += "static s16 sMaxPaletteCount[10] = {\n";

            //output += "static s16 sPaletteRoom[10][8][14] = {\n";


            output += "\n#endif";
            sw.Write(output);
            sw.Close();

            MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void icon_item_nes_staticMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.Filter = "Save file binary (*.bin)|*.bin|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.FileName = "icon_item_NES_static.bin";


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Helpers.IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    int size = ExportTitlecardTextures(saveFileDialog1.FileName);

                    MessageBox.Show("Done! File Size: " + size.ToString("X") + " bytes", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        public int ExportTitlecardTextures(string FileName)
        {
            List<Byte> TextureData = GenerateTitlecardTextureData();

            List<byte> titlecarddata = new List<byte>();

            ROM rom = MainForm.CheckVersion(ROM);
            int Offset = (int)rom.SubscreenMapTitleCards;


            for (int y = 0; y < 0xDC80; y += 4)
            {
                Helpers.Append32(ref titlecarddata, Helpers.Read32(ROM, Offset + y));
            }



            File.WriteAllBytes(FileName, titlecarddata.ToArray());


            BinaryWriter BWS = new BinaryWriter(File.OpenWrite(FileName));

            BWS.Seek(((int)MapID.Value * 0x600), SeekOrigin.Begin);


            BWS.Write(TextureData.ToArray());

            BWS.Close();

            return titlecarddata.Count;
        }
    }

    public class Map
    {
        public List<byte> imagedata = new List<byte>();
        public List<byte> icondata = new List<byte>();
        public int maxfloors = 0;
    }

    public class MapFloor
    {
        public int textureID = 0;
        public bool bosslocated = false;
        public int floorheight = 0;
        public int floorlabel = 0;
    }

    public class CompassIcon
    {
        public float X = 0, Y = 0;
        public short ChestFlag = 0;
        public bool bossicon = false;
    }

}
