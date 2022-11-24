using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SharpOcarina.Properties;

namespace SharpOcarina
{
    public partial class FileCreationSaveEditor : Form
    {

        String OldFileName = "";
        Boolean isrom = false;
        SaveFile save = new SaveFile();
        List<List<byte>> defaultitems = new List<List<byte>>();
        public bool imageloaded = false;
        public bool updating = false;

        public FileCreationSaveEditor()
        {
            InitializeComponent();

            defaultitems.AddRange(new List<List<byte>>()
            {
                new List<byte>(){0x00,0xFF},
                new List<byte>(){0x01,0xFF},
                new List<byte>(){0x02,0xFF},
                new List<byte>(){0x03,0xFF},
                new List<byte>(){0x04,0xFF},
                new List<byte>(){0x05,0xFF},
                new List<byte>(){0x06,0xFF},
                new List<byte>(){0x07,0x08,0xFF },
                new List<byte>(){0x09,0xFF },
                new List<byte>(){0x0A,0x0B,0xFF },
                new List<byte>(){0x0C,0xFF },
                new List<byte>(){0x0D,0xFF },
                new List<byte>(){0x0E,0xFF },
                new List<byte>(){0x0F,0xFF },
                new List<byte>(){0x10,0xFF },
                new List<byte>(){0x11,0xFF },
                new List<byte>(){0x12,0xFF },
                new List<byte>(){0x13,0xFF },
                new List<byte>(){0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0xFF },
                new List<byte>(){0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0xFF },
                new List<byte>(){0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0xFF },
                new List<byte>(){0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0xFF },
                new List<byte>(){0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0xFF },
                new List<byte>(){0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0xFF },


            });

            SongItem[] objs;
            objs = new[]
            {
                new SongItem {Text = "0 - Nothing", Value = 0x00},
                new SongItem {Text = "1 - Kokiri Sword", Value = 0x01},
                new SongItem {Text = "2 - Master Sword", Value = 0x02},
                new SongItem {Text = "3 - Giant Knife / Biggoron Sword", Value = 0x04},
            };

            EquipedSword.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Nothing", Value = 0x00},
                new SongItem {Text = "1 - Kokiri Shield", Value = 0x10},
                new SongItem {Text = "2 - Hylian Shield", Value = 0x20},
                new SongItem {Text = "3 - Mirror Shield", Value = 0x40},
            };

            EquipedShield.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Nothing", Value = 0x00},
                new SongItem {Text = "1 - Kokiri Tunic", Value = 0x01},
                new SongItem {Text = "2 - Goron Tunic", Value = 0x02},
                new SongItem {Text = "3 - Zora Tunic", Value = 0x04},
            };

            EquipedTunic.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Nothing", Value = 0x00},
                new SongItem {Text = "1 - Kokiri Boots", Value = 0x10},
                new SongItem {Text = "2 - Iron Boots", Value = 0x20},
                new SongItem {Text = "3 - Hover Boots", Value = 0x40},
            };

            EquipedBoots.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Nothing", Value = 0x00},
                new SongItem {Text = "1 - Quiver (30)", Value = 0x01},
                new SongItem {Text = "2 - Big Quiver (40)", Value = 0x02},
                new SongItem {Text = "3 - Biggest Quiver (50)", Value = 0x03},
            };

            ArrowsUpgrade.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Nothing", Value = 0x00},
                new SongItem {Text = "1 - Bomb Bag (20)", Value = 0x08},
                new SongItem {Text = "2 - Big Bomb Bag (30)", Value = 0x04},
                new SongItem {Text = "3 - Biggest Bomb Bag (40)", Value = 0x02},
               // new SongItem {Text = "3 - Biggest Bomb Bag (40)", Value = 0x0A}, fuck
            };

            BombsUpgrade.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Nothing", Value = 0x00},
                new SongItem {Text = "1 - Bullet Bag (30)", Value = 0x40},
                new SongItem {Text = "2 - Big Bullet Bag (40)", Value = 0x80},
                new SongItem {Text = "3 - Biggest Bullet Bag (50)", Value = 0xC0},
            };

            SeedsUpgrade.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Nothing", Value = 0x00},
                new SongItem {Text = "1 - Silver Scale", Value = 0x02},
                new SongItem {Text = "2 - Golden Scale", Value = 0x04},
            };

            ScaleUpgrade.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Nothing", Value = 0x00},
                new SongItem {Text = "1 - Goron Bracelet", Value = 0x40},
                new SongItem {Text = "2 - Silver Gauntlet", Value = 0x80},
                new SongItem {Text = "3 - Golden Gauntlet", Value = 0xC0},
            };

            PowerUpgrade.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - No Wallet (99)", Value = 0x00},
                new SongItem {Text = "1 - Adult's Wallet (200)", Value = 0x10},
                new SongItem {Text = "2 - Giant's Wallet (500)", Value = 0x20},
            };

            RupeeUpgrade.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Not obtained", Value = 0x00},
                new SongItem {Text = "1 - Deku Nuts (20)", Value = 0x10},
                new SongItem {Text = "2 - Deku Nuts (30)", Value = 0x20},
                new SongItem {Text = "3 - Deku Nuts (40)", Value = 0x30},
            };

            NutsUpgrade.Items.AddRange(objs);

            objs = new[]
            {
                new SongItem {Text = "0 - Not obtained", Value = 0x00},
                new SongItem {Text = "1 - Deku Sticks (10)", Value = 0x02},
                new SongItem {Text = "2 - Deku Sticks (20)", Value = 0x04},
                new SongItem {Text = "3 - Deku Sticks (30)", Value = 0x06},
                new SongItem {Text = "4 - Deku Sticks (40) unused", Value = 0x0E},
            };

            SticksUpgrade.Items.AddRange(objs);

            EquipedSword.SelectedIndex = 0;
            EquipedShield.SelectedIndex = 0;
            EquipedTunic.SelectedIndex = 0;
            EquipedBoots.SelectedIndex = 0;
            ArrowsUpgrade.SelectedIndex = 0;
            BombsUpgrade.SelectedIndex = 0;
            SeedsUpgrade.SelectedIndex = 0;
            ScaleUpgrade.SelectedIndex = 0;
            PowerUpgrade.SelectedIndex = 0;
            RupeeUpgrade.SelectedIndex = 0;
            NutsUpgrade.SelectedIndex = 0;
            SticksUpgrade.SelectedIndex = 0;

            if (MainForm.GlobalROM != "")
            {
                OpenRomToolStripMenuItem.Visible = false;
                OpenROM(MainForm.GlobalROM);
            }

            //for(int i = 0; i < 18; i++)


        }

        private void Close_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void OpenRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 00B9F360 - 00BA0BB0
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";
           

            //openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenROM(openFileDialog1.FileName);
            }
        }

        public void OpenROM(string ROM)
        {
            save = new SaveFile();
            List<byte> Data = new List<byte>(File.ReadAllBytes(ROM));
            ROM rom = MainForm.CheckVersion(new List<byte>(File.ReadAllBytes(ROM)));
            if (rom.Game == "MM")
            {
                MessageBox.Show("MM unsupported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int offset = (int)rom.DefaultSaveFile;
            save.maxhealth = (ushort)(Helpers.Read16(Data, offset + 0x2E) / 0x10);
            save.magicmeter = Data[offset + 0x32];
            save.rupees = (ushort)Helpers.Read16(Data, offset + 0x34);
            save.knifeuses = (ushort)Helpers.Read16(Data, offset + 0x36);
            save.isbiggoron = (Data[offset + 0x3E] > 0);
            for (int i = 0; i < 0xA; i++)
            {
                save.items[i] = Data[offset + 0x68 + i];
            }
            for (int i = 0; i < 0x18; i++)
            {
                save.items[i] = Data[offset + 0x74 + i];
            }
            for (int i = 0; i < 0xF; i++)
            {
                save.quantity[i] = Data[offset + 0x8C + i];
            }
            for (int i = 0; i < 0x8; i++)
            {
                save.equipment[i] = Data[offset + 0x9C + i];
            }
            for (int i = 0; i < 0x4; i++)
            {
                save.quest[i] = Data[offset + 0xA4 + i];
            }
            for (int i = 0; i < 0xA; i++)
            {
                save.currentequipment[i] = Data[offset + 0x68 + i];
            }
            //dungeon items 0xA8 0x14 times
            //small key amount 0xBC 0x13 times
            save.doubledefense = (Data[offset + 0xCF] > 0);

            save.skulltulatokens = Helpers.Read16(Data, offset + 0xD0);

            save.headertitle = Helpers.Read16(Data, (int)rom.HeaderTitle);
            save.entrancetitle = Helpers.Read16(Data, (int)rom.EntranceTitle);
            save.headernewfile = Helpers.Read16(Data, (int)rom.HeaderNewFile);
            //  save.entrancenewfile = Helpers.Read16(Data, (int)rom.EntranceNewFile);
            save.respawnchild = Helpers.Read16(Data, (int)rom.RespawnChild);
            save.respawnadult = Helpers.Read16(Data, (int)rom.RespawnAdult);
            save.scenenewfile = Data[offset + 0x67];
            save.adultlinknewfile = (Data[(int)rom.AgeNewFile] == 0);
            save.adultlinktitle = Helpers.Read32(Data, (int)rom.AgeTitle) != 0xAC4E0004; 
            //base index + scene setup = actual entrance index, or 00CD + 07 = 00D4 

            save.bombupgradeindex = 0;
            if ((save.equipment[7] & 0x08) != 0 && (save.equipment[7] & 0x10) == 0)
                save.bombupgradeindex = 1;
            else if ((save.equipment[7] & 0x08) == 0 && (save.equipment[7] & 0x10) != 0)
                save.bombupgradeindex = 2;
            else if ((save.equipment[7] & 0x08) != 0 && (save.equipment[7] & 0x10) != 0)
                save.bombupgradeindex = 3;


            tabControl1_TabIndexChanged(null, null);
        }

        private void saveROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.GlobalROM != "")
            {
                SaveROM(MainForm.GlobalROM);
                return;
            }
            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                SaveROM(saveFileDialog1.FileName);

            }
        }

        public void SaveROM(string ROM)
        {

            if (IsFileLocked(ROM))
                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                ROM rom = MainForm.CheckVersion(new List<byte>(File.ReadAllBytes(ROM)));

                if (rom.Game == "MM")
                {
                    MessageBox.Show("MM unsupported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));

                BWS.Seek((int)(rom.DefaultSaveFile + 0x2E), SeekOrigin.Begin);

                List<Byte> Output = GenerateSaveData();



                BWS.Write(Output.ToArray());

                Output.Clear();
                BWS.Seek((int)rom.HeaderTitle, SeekOrigin.Begin);
                Helpers.Append16(ref Output, save.headertitle);
                BWS.Write(Output.ToArray());
                Output.Clear();
                BWS.Seek((int)rom.EntranceTitle, SeekOrigin.Begin);
                Helpers.Append16(ref Output, save.entrancetitle);
                BWS.Write(Output.ToArray());
                Output.Clear();
                BWS.Seek((int)rom.HeaderNewFile, SeekOrigin.Begin);
                Helpers.Append16(ref Output, save.headernewfile);
                BWS.Write(Output.ToArray());
                Output.Clear();
                /*
                BWS.Seek((int)rom.EntranceNewFile, SeekOrigin.Begin);
                Helpers.Append16(ref Output, save.entrancenewfile);
                BWS.Write(Output.ToArray());
                Output.Clear();*/
                BWS.Seek((int)rom.AgeNewFile, SeekOrigin.Begin);
                Output.Add((byte)(save.adultlinknewfile ? 0x00 : 0x01));
                BWS.Write(Output.ToArray());
                Output.Clear();
                BWS.Seek((int)rom.RespawnChild, SeekOrigin.Begin);
                Helpers.Append16(ref Output, save.respawnchild);
                BWS.Write(Output.ToArray());
                Output.Clear();
                BWS.Seek((int)rom.RespawnAdult, SeekOrigin.Begin);
                Helpers.Append16(ref Output, save.respawnadult);
                BWS.Write(Output.ToArray());
                Output.Clear();
                BWS.Seek((int)rom.AgeTitle, SeekOrigin.Begin);
                Helpers.Append32(ref Output, save.adultlinktitle ? 0xAC400004 : 0xAC4E0004);
                BWS.Write(Output.ToArray());
                Output.Clear();



                BWS.Close();

                MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private List<byte> GenerateSaveData()
        {
            List<Byte> Output = new List<byte>();
            Helpers.Append16(ref Output, (ushort)(save.maxhealth * 0x10));
            Helpers.Append16(ref Output, (ushort)(save.maxhealth * 0x10));
            Output.Add(save.magicmeter);
            Output.Add((byte)(save.magicmeter * 0x30));
            Helpers.Append16(ref Output, save.rupees);
            Helpers.Append16(ref Output, save.knifeuses); // 0x36
            AddBytes(ref Output, 2); // 0x38-0x39
            Output.Add((byte)((save.magicmeter > 0) ? 0x01 : 0x00)); // 0x3A
            Output.Add(0); // 0x3B
            Output.Add((byte)((save.magicmeter == 2) ? 0x01 : 0x00)); // 0x3C
            Output.Add((byte)(save.doubledefense ? 0x01 : 0x00)); // 0x3D
            Output.Add((byte)(save.isbiggoron ? 0x01 : 0x00)); // 0x3E
            Output.Add(0);
            AddBytes(ref Output, 0xA, 0xFF);
            AddBytes(ref Output, 0x1C);
            Helpers.Append16(ref Output, save.scenenewfile);
            Output.AddRange(save.currentequipment);
            AddBytes(ref Output, 2);
            Output.AddRange(save.items);
            Output.AddRange(save.quantity);
            Output.Add(0x0); //magic beans sold
            Output.AddRange(save.equipment);
            Output.AddRange(save.quest);
            AddBytes(ref Output, 0x14);//dungeon items
            AddBytes(ref Output, 0x13, 0xFF);//keys
            Output.Add((byte)(save.doubledefense ? save.maxhealth : 0x00));
            Helpers.Append16(ref Output, save.skulltulatokens);//skulltula tokens

            return Output;
        }

        private void AddBytes(ref List<Byte> data, int amount, byte write = 0)
        {
            for (int i = 0; i < amount; i++)
                data.Add(write);
        }

        private void ItemsImage_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            Point coords = me.Location;
        //    int trueX = (int)(Math.Floor((double)(coords.X / 64)));
           // int trueY = (int)(Math.Floor((double)(coords.Y / 64)));
            int item = (int)(Math.Floor((double)(coords.X / 64)) + (Math.Floor((double)(coords.Y / 64)) * 6));

            if (me.Button == MouseButtons.Left)
            { 


            save.items[item] = defaultitems[item][0];
            defaultitems[item].RemoveAt(0);
            defaultitems[item].Add(save.items[item]);

            }
            else if (me.Button == MouseButtons.Right)
            {
                using (PickItem pickitem = new PickItem())
                {
                    if (pickitem.ShowDialog() == DialogResult.OK)
                    {
                        save.items[item] = (byte) pickitem.result;
                    }
                }
            }


            //MessageBox.Show("Item: " + item + "\nX: " + coords.X + "\nY: " + coords.Y, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DrawInventory();


        }

        private void DrawInventory()
        {
            Graphics g = ItemsImage.CreateGraphics();
         //   g.Clear(Color.Black);
            g.DrawImage(new Bitmap(Resources.UIitems),0,0,ItemsImage.Width,ItemsImage.Height);


            Bitmap itemicons = new Bitmap(Resources.itemicons);
            for (int i= 0; i < save.items.Length; i++)
            {
                if (save.items[i] == 0xFF || save.items[i] > 0x59) continue;

                Rectangle cloneRect = new Rectangle((save.items[i] % 6) * 32, (save.items[i]/6) * 32, 32, 32);
                System.Drawing.Imaging.PixelFormat format = itemicons.PixelFormat;
                Bitmap cloneBitmap = itemicons.Clone(cloneRect, format);
                g.DrawImage(cloneBitmap, (i % 6) * 64 + 12, (i / 6) * 64 + 16);
                g = ItemsImage.CreateGraphics();

            }

            DekuSticksAmount.Value = save.quantity[0];
            DekuNutsAmount.Value = save.quantity[1];
            BombsAmount.Value = save.quantity[2];
            ArrowsAmount.Value = save.quantity[3];
            DekuSeedsAmount.Value = save.quantity[6];
            BombchusAmount.Value = save.quantity[8];
            MagicBeansAmount.Value = save.quantity[0xE];

          
        }

        private void DrawEquipment()
        {
            updating = true;
            Boots1.Checked = ((save.equipment[0] & 0x10) != 0);
            Boots2.Checked = ((save.equipment[0] & 0x20) != 0);
            Boots3.Checked = ((save.equipment[0] & 0x40) != 0);
            Tunic1.Checked = ((save.equipment[0] & 0x01) != 0);
            Tunic2.Checked = ((save.equipment[0] & 0x02) != 0);
            Tunic3.Checked = ((save.equipment[0] & 0x04) != 0);

            Shield1.Checked = ((save.equipment[1] & 0x10) != 0);
            Shield2.Checked = ((save.equipment[1] & 0x20) != 0);
            Shield3.Checked = ((save.equipment[1] & 0x40) != 0);
            Sword1.Checked = ((save.equipment[1] & 0x01) != 0);
            Sword2.Checked = ((save.equipment[1] & 0x02) != 0);
            Sword3.Checked = ((save.equipment[1] & 0x04) != 0);

            EquipedBoots.SelectedIndex = (save.currentequipment[8] & 0x30)>>4;
            EquipedTunic.SelectedIndex = save.currentequipment[8] & 0x03;
            EquipedShield.SelectedIndex = (save.currentequipment[9] & 0x30)>> 4;
            EquipedSword.SelectedIndex = save.currentequipment[9] & 0x03;

    
            ArrowsUpgrade.SelectedIndex = (ArrowsUpgrade.Items.Cast<SongItem>().ToList().FindIndex(x => Convert.ToByte(x.Value) == (save.equipment[7] & 0x03)));
            SeedsUpgrade.SelectedIndex = (SeedsUpgrade.Items.Cast<SongItem>().ToList().FindIndex(x => Convert.ToByte(x.Value) == (save.equipment[6] & 0xC0)));
            ScaleUpgrade.SelectedIndex = (ScaleUpgrade.Items.Cast<SongItem>().ToList().FindIndex(x => Convert.ToByte(x.Value) == (save.equipment[6] & 0x04)));
            PowerUpgrade.SelectedIndex = (PowerUpgrade.Items.Cast<SongItem>().ToList().FindIndex(x => Convert.ToByte(x.Value) == (save.equipment[7] & 0xC0)));
            RupeeUpgrade.SelectedIndex = (RupeeUpgrade.Items.Cast<SongItem>().ToList().FindIndex(x => Convert.ToByte(x.Value) == (save.equipment[6] & 0x20)));
            NutsUpgrade.SelectedIndex = (NutsUpgrade.Items.Cast<SongItem>().ToList().FindIndex(x => Convert.ToByte(x.Value) == (save.equipment[5] & 0x30)));
            SticksUpgrade.SelectedIndex = (SticksUpgrade.Items.Cast<SongItem>().ToList().FindIndex(x => Convert.ToByte(x.Value) == (save.equipment[5] & 0x0F)));

           // if (ArrowsUpgrade.SelectedIndex == -1) ArrowsUpgrade.SelectedIndex = 0;
        //    if (SeedsUpgrade.SelectedIndex == -1) SeedsUpgrade.SelectedIndex = 0;
        //    if (ScaleUpgrade.SelectedIndex == -1) ScaleUpgrade.SelectedIndex = 0;
         //   if (PowerUpgrade.SelectedIndex == -1) PowerUpgrade.SelectedIndex = 0;
        //    if (RupeeUpgrade.SelectedIndex == -1) RupeeUpgrade.SelectedIndex = 0;
         //   if (NutsUpgrade.SelectedIndex == -1) NutsUpgrade.SelectedIndex = 0;
       //     if (SticksUpgrade.SelectedIndex == -1) SticksUpgrade.SelectedIndex = 0;

            if (EquipedBoots.SelectedIndex == -1) EquipedBoots.SelectedIndex = 0;
            if (EquipedTunic.SelectedIndex == -1) EquipedTunic.SelectedIndex = 0;
            if (EquipedShield.SelectedIndex == -1) EquipedShield.SelectedIndex = 0;
            if (EquipedSword.SelectedIndex == -1) EquipedSword.SelectedIndex = 0;

            BombsUpgrade.SelectedIndex = save.bombupgradeindex;

            GiantKnifeUses.Value = save.knifeuses;
            BiggoronFlag.Checked = save.isbiggoron;
            updating = false;
        }

        private void DrawQuest()
        {
            updating = true;
            WSong1.Checked = ((save.quest[3] & 0x40) != 0);
            WSong2.Checked = ((save.quest[3] & 0x80) != 0);
            WSong3.Checked = ((save.quest[2] & 0x01) != 0);
            WSong4.Checked = ((save.quest[2] & 0x02) != 0);
            WSong5.Checked = ((save.quest[2] & 0x04) != 0);
            WSong6.Checked = ((save.quest[2] & 0x08) != 0);

            Song1.Checked = ((save.quest[2] & 0x10) != 0);
            Song2.Checked = ((save.quest[2] & 0x20) != 0);
            Song3.Checked = ((save.quest[2] & 0x40) != 0);
            Song4.Checked = ((save.quest[2] & 0x80) != 0);
            Song5.Checked = ((save.quest[1] & 0x01) != 0);
            Song6.Checked = ((save.quest[1] & 0x02) != 0);
            SkulltulaTokens.Value = save.skulltulatokens;

            updating = false;
        }

        private void DrawOther()
        {
            updating = true;
            MaxHearts.Value = save.maxhealth;
            MagicAmount.Value = save.magicmeter;
            DoubleDefenseCheckbox.Checked = save.doubledefense;
            RupeesAmount.Value = save.rupees;
            TitleScreenEntrance.Value = save.entrancetitle;
            TitleScreenHeader.Value = save.headertitle;
            NewFileScene.Value = save.scenenewfile;
            NewFileHeader.Value = save.headernewfile;
            RespawnEntranceAdult.Value = save.respawnadult;
            RespawnEntranceChild.Value = save.respawnchild;
            AdultLinkNewFile.Checked = save.adultlinknewfile;
            AdultLinkTitle.Checked = save.adultlinktitle;
            updating = false;
        }

        private void UpdateItemAmounts()
        {
            save.quantity[0] = (byte)DekuSticksAmount.Value;
            save.quantity[1] = (byte)DekuNutsAmount.Value;
            save.quantity[2] = (byte)BombsAmount.Value;
            save.quantity[3] = (byte)ArrowsAmount.Value;
            save.quantity[6] = (byte)DekuSeedsAmount.Value;
            save.quantity[8] = (byte)BombchusAmount.Value;
            save.quantity[0xE] = (byte)MagicBeansAmount.Value;
        }

        private void UpdateCurrentEquipment()
        {
            save.currentequipment[8] = (byte) ((EquipedBoots.SelectedIndex * 0x10) + EquipedTunic.SelectedIndex);
            save.currentequipment[9] = (byte)((EquipedShield.SelectedIndex * 0x10) + EquipedSword.SelectedIndex);
            save.knifeuses = (ushort) GiantKnifeUses.Value;
            save.isbiggoron = BiggoronFlag.Checked;
            save.currentequipment[0] = (byte) ((EquipedSword.SelectedIndex > 0) ? 0x3A + EquipedSword.SelectedIndex : 0xFF);
        }

        private void UpdateEquipment()
        {
            save.equipment[0] = (byte) (((Boots1.Checked) ? 0x10 : 0) + ((Boots2.Checked) ? 0x20 : 0) + ((Boots3.Checked) ? 0x40 : 0)
                + ((Tunic1.Checked) ? 0x01 : 0) + ((Tunic2.Checked) ? 0x02 : 0) + ((Tunic3.Checked) ? 0x04 : 0));
            save.equipment[1] = (byte) (((Shield1.Checked) ? 0x10 : 0) + ((Shield2.Checked) ? 0x20 : 0) + ((Shield3.Checked) ? 0x40 : 0)
                + ((Sword1.Checked) ? 0x01 : 0) + ((Sword2.Checked) ? 0x02 : 0) + ((Sword3.Checked) ? 0x04 : 0));
            save.equipment[5] = (byte)(Convert.ToByte(((SongItem)SticksUpgrade.SelectedItem).Value) + Convert.ToByte(((SongItem)NutsUpgrade.SelectedItem).Value));
            save.equipment[6] = (byte)(Convert.ToByte(((SongItem)ScaleUpgrade.SelectedItem).Value) + Convert.ToByte(((SongItem)RupeeUpgrade.SelectedItem).Value) + Convert.ToByte(((SongItem)SeedsUpgrade.SelectedItem).Value));
            save.equipment[7] = (byte)(Convert.ToByte(((SongItem)ArrowsUpgrade.SelectedItem).Value) + Convert.ToByte(((SongItem)PowerUpgrade.SelectedItem).Value));

            if (BombsUpgrade.SelectedIndex == 1 || BombsUpgrade.SelectedIndex == 3)
               save.equipment[7] += 0x08;
            if (BombsUpgrade.SelectedIndex >= 2)
                save.equipment[7] += 0x10;

            save.bombupgradeindex = BombsUpgrade.SelectedIndex;
        }

        private void UpdateQuest()
        {
            save.quest[3] = (byte)(((WSong1.Checked) ? 0x40 : 0) + ((WSong1.Checked) ? 0x80 : 0));
            save.quest[2] = (byte)(((WSong3.Checked) ? 0x01 : 0) + ((WSong4.Checked) ? 0x02 : 0) + ((WSong5.Checked) ? 0x04 : 0) + ((WSong6.Checked) ? 0x08 : 0) + ((Song1.Checked) ? 0x10 : 0) + ((Song2.Checked) ? 0x20 : 0) + ((Song3.Checked) ? 0x40 : 0) + ((Song4.Checked) ? 0x80 : 0));
            save.quest[1] = (byte)(((Song5.Checked) ? 0x01 : 0) + ((Song6.Checked) ? 0x02 : 0) + ((SkulltulaTokens.Value > 0) ? 0x80 : 0));
            save.skulltulatokens = (ushort)SkulltulaTokens.Value;
        }

        private void UpdateOther()
        {
            save.maxhealth = (byte) MaxHearts.Value;
            save.magicmeter = (byte) MagicAmount.Value;
            save.doubledefense = DoubleDefenseCheckbox.Checked;
            save.rupees = (byte)RupeesAmount.Value;
            save.entrancetitle = (ushort) TitleScreenEntrance.Value;
            save.headertitle = (ushort)TitleScreenHeader.Value;
            save.scenenewfile = (ushort)NewFileScene.Value;
            save.headernewfile = (ushort)NewFileHeader.Value;
            save.adultlinknewfile = AdultLinkNewFile.Checked;
            save.adultlinktitle = AdultLinkTitle.Checked;
            save.respawnadult = (ushort)RespawnEntranceAdult.Value;
            save.respawnchild = (ushort)RespawnEntranceChild.Value;
        }

        private bool IsFileLocked(string file)
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

        private class SaveFile
        {
            public byte[] items = new byte[0x18];
            public byte[] quantity = new byte[0xF];
            public byte[] equipment = new byte[0x8];
            public byte[] quest = new byte[0x4];
            public byte[] currentequipment = new byte[0xA];
            public ushort maxhealth = 3;
            public ushort health = 3;
            public byte magicmeter = 0x00;
            public ushort rupees = 0;
            public ushort knifeuses = 0;
            public bool isbiggoron = false;
            public byte availablebeans = 0;
            public bool doubledefense = false;
            public bool adultlinktitle = true;
            public bool adultlinknewfile = false;
            public ushort entrancetitle = 0x00CD;
            public ushort entrancenewfile = 0x00BB;
            public ushort headertitle = 0xFFF3;
            public ushort headernewfile = 0xFFF1;
            public ushort scenenewfile = 0x34;
            public int bombupgradeindex = 0;
            public byte magicbeanssale = 0x0A;
            public ushort respawnadult = 0x05F4;
            public ushort respawnchild = 0x00BB;

            public ushort skulltulatokens = 0;
            public SaveFile()
            {

                for(int i = 0; i < items.Length; i++)
                {
                    items[i] = 0xFF;
                }
                for (int i = 0; i < quantity.Length; i++)
                {
                    quantity[i] = 0;
                }
                for (int i = 0; i < equipment.Length; i++)
                {
                    equipment[i] = 0;
                }
                for (int i = 0; i < quest.Length; i++)
                {
                    quest[i] = 0;
                }
                for (int i = 0; i < currentequipment.Length-2; i++)
                {
                    currentequipment[i] = 0xFF;
                }
                equipment[0] = 0x11;
                currentequipment[8] = 0x11;

            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                DrawInventory();
            else if (tabControl1.SelectedIndex == 1)
                DrawEquipment();
            else if (tabControl1.SelectedIndex == 2)
                DrawQuest();
            else if (tabControl1.SelectedIndex == 3)
                DrawOther();
            imageloaded = false;
        }


        private void EquipmentCheckboxes_CheckedChanged(object sender, EventArgs e)
        {
            if (!updating) UpdateEquipment();
        }

        private void ItemQuantities_ValueChanged(object sender, EventArgs e)
        {
            if (!updating) UpdateItemAmounts();
        }

        private void CurrentEquipment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!updating) UpdateCurrentEquipment();
        }

        private void Song1_CheckedChanged(object sender, EventArgs e)
        {
            if (!updating) UpdateQuest();
        }

        private void GiantKnifeUses_ValueChanged(object sender, EventArgs e)
        {
            if (!updating) UpdateCurrentEquipment();
        }

        private void BiggoronFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (!updating) UpdateCurrentEquipment();
        }

        private void OtherNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (!updating) UpdateOther();
        }



        private void ItemsImage_MouseEnter(object sender, EventArgs e)
        {
            if (!imageloaded) { DrawInventory(); imageloaded = true; }
        }

        private void Upgrade_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateEquipment();
        }

        private void FileCreationSaveEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!imageloaded) { DrawInventory(); imageloaded = true; }
        }

        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
        {
          
        }

        private void saveBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.CheckFileExists = false;
            //    saveFileDialog1.FileName = Path.GetFileName(OldFileName) + "_new";
            saveFileDialog1.Filter = "Save file binary (*.bin)|*.bin|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    List<Byte> SaveData = GenerateSaveData();
                    File.WriteAllBytes(saveFileDialog1.FileName, SaveData.ToArray());
                    MessageBox.Show("Done! File Size: " + SaveData.Count.ToString("X") + " bytes", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveROMToolStripMenuItem_Click(new object(), new EventArgs());
        }

        private void SkulltulaTokens_ValueChanged(object sender, EventArgs e)
        {
            if (!updating) UpdateQuest();
        }

        private void AllMaps_CheckedChanged(object sender, EventArgs e)
        {
            if (!updating) UpdateQuest();
        }

        private void FileCreationSaveEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.savefileeditor_visible = false;
        }
    }
}
