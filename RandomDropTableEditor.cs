using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SharpOcarina
{
    public partial class RandomDropTableEditor : Form
    {

        String OldFileName = "";
        Boolean isrom = false, isztable = false;
        Dictionary<byte,string> SceneNames;
        public int RandomDropTableOffset = 0;
        public DropTable[] DropTables = new DropTable[16];
        ComboBox[] items;
        NumericUpDown[] amounts;
        public string ROMpath = "";
        public int previd = 0;

        public RandomDropTableEditor()
        {

            InitializeComponent();

            if (MainForm.GlobalROM != "")
            {
                OpenROM(MainForm.GlobalROM);
            }
            else if (rom64.isSet())
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

                        List<byte>  ROM = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                        ROM rom = MainForm.CheckVersion(ROM);

                        if (rom.Game == "MM")
                        {
                            MessageBox.Show("MM unsupported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        this.Close();
                    }

                }
                else
                {
                    OpenROM(binarydata);
                }
            }
            else
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";

                //openFileDialog1.FilterIndex = 1;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OpenROM(openFileDialog1.FileName);
                }
                else
                {
                    this.Close();
                }
            }


            items = new []{ Item0, Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14, Item15 };
            amounts = new []{ Amount0, Amount1, Amount2, Amount3, Amount4, Amount5, Amount6, Amount7, Amount8, Amount9, Amount10, Amount11, Amount12, Amount13, Amount14, Amount15 };


            InitializeForm();


        }

        private void Close_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void InitializeForm()
        {

            for(int i = 0; i < 16; i++)
            {
                items[i].Items.AddRange(XMLreader.getXMLItems("OOT\\ItemDrops", "Item"));
            }

            List<byte> ROM = new List<byte>(File.ReadAllBytes(ROMpath));

            int offset = (int)RandomDropTableOffset;
            int cnt = 0;
            while (cnt != 15)
            {
                DropTables[cnt] = new DropTable();
                for (int i = 0; i < 16; i++)
                {
                    DropTables[cnt].ItemVal[i] = ROM[offset + i];
                    DropTables[cnt].ItemAmount[i] = ROM[offset + i + 0xF0];
                }
                offset += 0x10;
                cnt++; 
            }

            ChangeTable(false);
        }

        public void OpenROM(string romfile)
        {

            List<byte> ROM = new List<byte>(File.ReadAllBytes(romfile));

            ROM rom = MainForm.CheckVersion(ROM);

            if (rom.Game == "MM")
            {
                MessageBox.Show("MM unsupported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ROMpath = romfile;
            RandomDropTableOffset = (int)rom.RandomDropTable;
        }


        private void saveROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (IsFileLocked(ROMpath))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {

                    SaveToRom();
                    MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
        }

        private void SaveToRom()
        {
            BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROMpath));
            int offset = (int)RandomDropTableOffset;
            BWS.Seek(offset, SeekOrigin.Begin);

            ChangeTable(true);

            List<Byte> Output = new List<byte>();
            List<Byte> Output2 = new List<byte>();

            for (int i = 0; i < 15; i++)
            {
                for (int y = 0; y < 16; y++)
                {
                    Output.Add(DropTables[i].ItemVal[y]);
                    Output2.Add(DropTables[i].ItemAmount[y]);
                }
            }
            Output.AddRange(Output2);
            BWS.Write(Output.ToArray());

            BWS.Close();
        }

        private void exportBinaryToolStripMenuItem_Click(object sender, EventArgs e)
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
                    ChangeTable(true);

                    List<Byte> Output = ConvertBinary();
                    File.WriteAllBytes(saveFileDialog1.FileName, Output.ToArray());
                    MessageBox.Show("Done! File Size: " + Output.Count.ToString("X") + " bytes", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private List<Byte> ConvertBinary()
        {
            List<Byte> Output = new List<byte>();
            List<Byte> Output2 = new List<byte>();

            for (int i = 0; i < 15; i++)
            {
                for (int y = 0; y < 16; y++)
                {
                    Output.Add(DropTables[i].ItemVal[y]);
                    Output2.Add(DropTables[i].ItemAmount[y]);
                }
            }
            Output.AddRange(Output2);

            return Output;
        }

        private void DropTableID_ValueChanged(object sender, EventArgs e)
        {
            ChangeTable(true);
        }

        private void ChangeTable(bool change = false)
        {
            if (change)
            {

                for (int y = 0; y < 16; y++)
                {
                    DropTables[previd].ItemVal[y] = (byte)Convert.ToByte((items[y].SelectedItem as SongItem).Value);
                    DropTables[previd].ItemAmount[y] = (byte) amounts[y].Value;
                }
                
            }
            for(int i=0; i < 16; i++)
            {
                items[i].SelectedIndex = FindSongComboItemValue(items[i].Items, DropTables[(int)DropTableID.Value].ItemVal[i]);
                amounts[i].Value = DropTables[(int) DropTableID.Value].ItemAmount[i];
            }

            previd = (int)DropTableID.Value;
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

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private int FindSongComboItemValue(ComboBox.ObjectCollection items, uint marker)
        {
            foreach (SongItem item in items)
            {
                if (Convert.ToUInt32(item.Value.ToString()) == marker) return items.IndexOf(item);
            }
            return 0;
        }

        private void exportAsZ64romPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "";

            if (rom64.isSet())
            {
                path = (rom64.getPath() + "\\patch\\");
            }
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "z64rom project (z64project.toml)|z64project.toml|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName;

                    if (path.Contains("z64project.toml"))
                        path = Path.GetDirectoryName(path) + "\\patch\\";
                    else
                    {
                        MessageBox.Show("invalid config file, you need to import z64project.toml", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (IsFileLocked(ROMpath))
                MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                SaveToRom();

                string droptablepath = path + "droptable";
                if (File.Exists(droptablepath)) File.Delete(droptablepath);
                List<Byte> droptableData = ConvertBinary();
                File.WriteAllBytes(droptablepath, droptableData.ToArray());

                //patch file
                string droptable_SO = path + "droptable_SO.cfg";

                if (File.Exists(droptable_SO)) File.Delete(droptable_SO);

                File.Copy(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Files/droptable_SO.cfg"), droptable_SO);


                MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }

        private void exportAsCHeaderArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string output = "static u8 sItemDropIds[] = {\n";

            string output2 = "static u8 sDropQuantities[] = {\n";

            //string[] names = "";


            for (int i = 0; i < 15; i++)
            {
                for (int y = 0; y < 16; y++)
                {
                    output += DropTables[i].ItemVal[y] + ",";
                    output2 += DropTables[i].ItemAmount[y] + ",";
                }
                output += "\n";
                output2 += "\n";
            }

            output += "};\n";
            output2 += "};\n";

            output += output2;

            MessageBox.Show("Done! \nClose this window to copy this to your clipboard", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clipboard.SetText(output);
        }

        private void RandomDropTableEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.droptableeditor_visible = false;
        }

        public class DropTable
        {
            public byte[] ItemVal = new byte[16];
            public byte[] ItemAmount = new byte[16];
        }
     


    }
}
