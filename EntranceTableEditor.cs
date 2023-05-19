using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Tommy;

namespace SharpOcarina
{
    public partial class EntranceTableEditor : Form
    {

        String OldFileName = "";
        Boolean isrom = false, isztable = false;
        Dictionary<byte,string> SceneNames;
        static List<string> TransitionTypes = new List<string> { "Wipe", "Triforce", "FadeBlack", "FadeWhite", "FadeBlackFast", "FadeWhiteFast", "FadeBlackSlow", "FadeWhiteSlow", "WipeFast", "FillWhite2", "FillWhite", "Instant", "FillBrown", "FadeWhiteCsDelayed", "Sandstorm", "SandstormEnd", "FillBlack", "FadeWhiteInstant", "FadeGreen", "FadeBlue", "Circle", "Warp" };
        public MainForm mainform;

        public EntranceTableEditor()
        {
            InitializeComponent();

            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlNodeList nodes = XMLreader.getXMLNodes(gameprefix + "SceneNames", "Scene");
            SceneNames = new Dictionary<byte, string>();
            if (!rom64.isSet())
            {
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    if (nodeAtt["Key"] != null)
                    {
                        SceneNames.Add(Convert.ToByte(nodeAtt["Key"].Value, 16), node.InnerText);
                        //  Console.WriteLine(Convert.ToByte(nodeAtt["Key"].Value, 16) + " " + node.InnerText);
                    }
                }
            }
            else
            {
                List<string> scenes = rom64.getList("rom\\scene");

                foreach (string scene in scenes)
                {
                    string scene2 = Path.GetFileName(scene);
                    if (!scene2.Contains("-")) continue;
                    if (!scene2.Contains("x")) continue;
                    string s = scene2.Substring(0,scene2.IndexOf("-"));
                    string name = scene2.Substring(scene2.IndexOf("-")+1);
                    s = s.Replace("0x", "");
                    //byte sceneid;
                    if (!byte.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte sceneid)) continue;
                    SceneNames.Add(sceneid, name);
                }
                scenes = rom64.getList("rom\\scene\\.vanilla");

                foreach (string scene in scenes)
                {
                
                    string scene2 = Path.GetFileName(scene);
                    if (!scene2.Contains("-")) continue;
                    if (!scene2.Contains("x")) continue;
                    string s = scene2.Substring(0, scene2.IndexOf("-"));
                    string name = scene2.Substring(scene2.IndexOf("-") + 1);
                    s = s.Replace("0x", "");
                    //byte sceneid;
                    if (!byte.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte sceneid)) continue;
                    if (SceneNames.ContainsKey(sceneid)) continue;
                    SceneNames.Add(sceneid, name);
                }

            }

            if (MainForm.GlobalROM != "")
            {
                OpenRomToolStripMenuItem.Visible = false;
                OpenROM(MainForm.GlobalROM);
            }
            else if (rom64.isSet())
            {
                OpenRomToolStripMenuItem.Visible = false;
                OpenZ64rom();
               
            }
        }

        private void Close_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Ztables binary file (*.bin)|*.bin|All Files (*.*)|*.*";
            
            //openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> EntranceTable = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                saveFileToolStripMenuItem.Visible = true;
                saveROMToolStripMenuItem.Visible = false;
                EntranceGrid.AllowUserToAddRows = true;
                EntranceGrid.AllowUserToDeleteRows = true;
                ushort variable;
                string scenename = "";
                EntranceGrid.Rows.Clear();

                OldFileName = openFileDialog1.FileName;
                int offset = 0;
                EntranceGrid.Enabled = true;
                while (offset < EntranceTable.Count - 1)
                {
                    variable = Helpers.Read16(EntranceTable, offset + 2);

                    if (!SceneNames.TryGetValue(EntranceTable[offset], out scenename)) scenename = "Unknown";
                    
                    EntranceGrid.Rows.Add(offset / 4, EntranceTable[offset], EntranceTable[offset + 1], (variable & 0x8000) == 0x08000 ? 1 : 0, (variable & 0x3F80) >> 7, variable & 0x007F, (variable & 0x4000) == 0x4000 ? 1 : 0, scenename);
                    offset += 4;
                }
                RefreshRowCount();

            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog1.CheckFileExists = true;
        //    saveFileDialog1.FileName = Path.GetFileName(OldFileName) + "_new";
            saveFileDialog1.Filter = "Ztables binary file (*.bin)|*.bin|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;

            

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    List<Byte> EntranceData = GenerateEntranceData();
                    File.WriteAllBytes(saveFileDialog1.FileName, EntranceData.ToArray());
                    MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
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

            List<byte> EntranceTable = new List<byte>(File.ReadAllBytes(ROM));

            ROM rom = MainForm.CheckVersion(EntranceTable);

            saveFileToolStripMenuItem.Visible = false;
            saveROMToolStripMenuItem.Visible = true;
            EntranceGrid.AllowUserToAddRows = false;
            EntranceGrid.AllowUserToDeleteRows = false;
            ushort variable;
            string scenename = "";
            EntranceGrid.Rows.Clear();

            OldFileName = openFileDialog1.FileName;
            int offset = (int)rom.EntranceTableStart;
            EntranceGrid.Enabled = true;
            while (offset < ((int)rom.EntranteTableEnd) - 1)
            {
                variable = Helpers.Read16(EntranceTable, offset + 2);

                if (!SceneNames.TryGetValue(EntranceTable[offset], out scenename)) scenename = "Unknown";


                if (!MainForm.settings.MajorasMask || (MainForm.settings.MajorasMask && EntranceTable[offset] != 0x80))
                    EntranceGrid.Rows.Add((offset - (int)rom.EntranceTableStart) / 4, EntranceTable[offset], EntranceTable[offset + 1], (variable & 0x8000) == 0x08000 ? 1 : 0, (variable & 0x3F80) >> 7, variable & 0x007F, (variable & 0x4000) == 0x4000 ? 1 : 0, scenename);
                offset += 4;
            }
            RefreshRowCount();
        }

        public void OpenZ64rom()
        {
            TomlTable toml = rom64.parseToml(rom64.getPath() + "\\src\\entrance_table.toml");
            TomlNode[] nodes = toml.Children.ToArray();
            string[] keys = toml.Keys.ToArray();


            saveFileToolStripMenuItem.Visible = false;
            saveROMToolStripMenuItem.Visible = true;
            EntranceGrid.AllowUserToAddRows = false;
            EntranceGrid.AllowUserToDeleteRows = false;
            bool continuebgm, titlecard;
            byte fadein, fadeout;
            byte scene, entrance;
            string scenename = "";
            EntranceGrid.Rows.Clear();

            OldFileName = openFileDialog1.FileName;

            EntranceGrid.Enabled = true;
            int i = 0;
            foreach(TomlNode node in nodes)
            {
                scene = (byte)node.Children.ToArray()[0].AsInteger.Value;
                entrance = (byte)node.Children.ToArray()[1].AsInteger.Value;
                continuebgm = node.Children.ToArray()[2].AsBoolean;
                titlecard = node.Children.ToArray()[3].AsBoolean;
                fadein = (byte) TransitionTypes.FindIndex(x => x == node.Children.ToArray()[4].AsString);
                fadeout = (byte)TransitionTypes.FindIndex(x => x == node.Children.ToArray()[5].AsString);


                if (!SceneNames.TryGetValue(scene, out scenename)) scenename = "Unknown";

                EntranceGrid.Rows.Add(i, scene, entrance, continuebgm, fadein, fadeout, titlecard, scenename);
                i++;
            }
            RefreshRowCount();

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

                BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROM));
                int TableOffset = (int)rom.EntranceTableStart;
                BWS.Seek(TableOffset, SeekOrigin.Begin);

                List<Byte> Output = GenerateEntranceData();

                BWS.Write(Output.ToArray());

                BWS.Close();

                MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }


        private void ObjectGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            RefreshRowCount();
        }

        private void ObjectGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            RefreshRowCount();
        }

        private void RefreshRowCount()
        {
            NumEntrancesLabel.Text = "Number of entrances: " + (EntranceGrid.RowCount);
            for (int i = 0; i < EntranceGrid.RowCount - 1; i++)
            {
                EntranceGrid.Rows[i].Cells[0].Value = i;
            }
            // EntranceGrid.Rows[EntranceGrid.RowCount - 1].Cells[0].Value = "";
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
        
        private void EntranceGrid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            refresh(e.ColumnIndex, e.RowIndex);
        }

        private void ObjectGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            refresh(e.ColumnIndex,e.RowIndex);
        }

        private void EntranceGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (new int[]{1,2,4,5}.Contains(e.ColumnIndex))
            {
                if (e != null && e.Value != null && e.DesiredType.Equals(typeof(string)))
                {
                    try
                    {
                        e.Value = string.Format("{0:X2}", e.Value);
                        e.FormattingApplied = true;
                    }
                    catch
                    {
                        /* Not hexadecimal */
                    }
                }
            }
        }

        private void EntranceGrid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (new int[] { 1, 2, 3, 4, 5 ,6 }.Contains(e.ColumnIndex))
            {
               //   Console.WriteLine("Desired type: "  + e.Value.GetType());
                if (e != null && e.Value != null  && e.Value.GetType().Equals(typeof(System.String)))
                {
                    string str = (e.Value as string);
                    try
                    {
                        e.Value = Convert.ToByte(str, 16);
                        e.ParsingApplied = true;
                    }
                    catch (Exception)
                    {
                        e.Value = 0;
                        e.ParsingApplied = true;
                    }

                }
                else
                {
                    e.Value = 0;
                    e.ParsingApplied = true;
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (!rom64.isSet())
            {
                saveROMToolStripMenuItem_Click(new object(), new EventArgs());
            }
            else
            {
                sendToZ64romProjectToolStripMenuItem_Click(new object(), new EventArgs());
            }
            
        }

        private void refresh(int column, int row) //TODO 0.92
        {
            try
            {
                if (column == 1)
                {
                    string scenename;
                   //     Console.WriteLine(Convert.ToByte(EntranceGrid.Rows[row].Cells[1].Value)); 
                        //Console.WriteLine(Convert.ToByte(EntranceGrid.Rows[row].Cells[1].Value));
                     if (!SceneNames.TryGetValue(Convert.ToByte((EntranceGrid.Rows[row].Cells[1].Value)), out scenename)) scenename = "Unknown";

                      EntranceGrid.Rows[row].Cells[7].Value = scenename;

                }
                if (column == 3 | column == 6)
                {
                    EntranceGrid.Rows[row].Cells[column].Value = MainForm.Clamp(Convert.ToByte(EntranceGrid.Rows[row].Cells[column].Value), 0, 1);
                }/*
                if (column == 1 | column == 2 | column == 4 | column == 5)
                {
                    EntranceGrid.Rows[row].Cells[column].Value = Convert.ToUInt16(EntranceGrid.Rows[row].Cells[column].Value.ToString(), 16);
                }*/
            }
            catch (System.FormatException)
            {
                //nothing
                Console.WriteLine("Incorrect format? " + EntranceGrid.Rows[row].Cells[1].Value);
            }
        }

        private void saveBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.Filter = "Save file binary (*.bin)|*.bin|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    List<Byte> EntranceData = GenerateEntranceData();
                    File.WriteAllBytes(saveFileDialog1.FileName, EntranceData.ToArray());
                    MessageBox.Show("Done! File Size: " + EntranceData.Count.ToString("X") + " bytes", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private List<byte> GenerateEntranceData()
        {
            EntranceGrid.Sort(EntranceGrid.Columns[0], ListSortDirection.Ascending);

            List<Byte> Output = new List<byte>();
            for (int i = 0; i < EntranceGrid.RowCount; i++)
            {
                Output.Add(Convert.ToByte(EntranceGrid.Rows[i].Cells[1].Value));
                Output.Add(Convert.ToByte(EntranceGrid.Rows[i].Cells[2].Value));
                ushort variable = (ushort)(0x0000 | (Convert.ToByte(EntranceGrid.Rows[i].Cells[3].Value) << 15) | (Convert.ToByte(EntranceGrid.Rows[i].Cells[6].Value) << 14)
                     | (Convert.ToUInt16(EntranceGrid.Rows[i].Cells[4].Value) << 7) | (Convert.ToUInt16(EntranceGrid.Rows[i].Cells[5].Value)));
                Output.AddRange(BitConverter.GetBytes(variable).Reverse());
            }

            return Output;
        }

        private void sendToZ64romProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EntranceGrid.Sort(EntranceGrid.Columns[0], ListSortDirection.Ascending);

            string path = "";

            if (rom64.isSet())
                path = (rom64.getPath() + "\\src\\entrance_table.toml");
            else
            {
                saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.Filter = "z64rom project (z64project.toml)|z64project.toml|All Files (*.*)|*.*";
                saveFileDialog1.CreatePrompt = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName;

                    if (path.Contains("z64project.toml"))
                        path = Path.GetDirectoryName(path) + "\\src\\entrance_table.toml";
                    else
                    {
                        MessageBox.Show("invalid config file, you need to import z64project.toml", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            if (File.Exists(path)) File.Delete(path);

            StreamWriter sw = File.CreateText(path);

            sw.Write("# Transition Types:\r\n\t# Wipe\r\n\t# Triforce\r\n\t# FadeBlack\r\n\t# FadeWhite\r\n\t# FadeBlackFast\r\n\t# FadeWhiteFast\r\n\t# FadeBlackSlow\r\n\t# FadeWhiteSlow\r\n\t# WipeFast\r\n\t# FillWhite2\r\n\t# FillWhite\r\n\t# Instant\r\n\t# FillBrown\r\n\t# FadeWhiteCsDelayed\r\n\t# Sandstorm\r\n\t# SandstormEnd\r\n\t# FillBlack\r\n\t# FadeWhiteInstant\r\n\t# FadeGreen\r\n\t# FadeBlue\r\n\t# Circle\r\n\t# Warp\r\n\r\n# Array Items: [ scene_id, spawn_id, continue_bgm, title_card, fade_in, fade_out ]\r\n");

            for (int i = 0; i < EntranceGrid.Rows.Count; i++)
            {
                sw.Write("0x" + i.ToString("X4") + "          = [ 0x" + Convert.ToByte(EntranceGrid.Rows[i].Cells[1].Value).ToString("X2") +
                         ", 0x" + Convert.ToByte(EntranceGrid.Rows[i].Cells[2].Value).ToString("X2") + 
                         ", " + (Convert.ToByte(EntranceGrid.Rows[i].Cells[3].Value) == 1 ? "true" : "false") +
                         ", " + (Convert.ToByte(EntranceGrid.Rows[i].Cells[6].Value) == 1 ? "true" : "false") +
                         ", \"" + TransitionTypes[Convert.ToByte(EntranceGrid.Rows[i].Cells[4].Value)] +
                         "\", \"" + TransitionTypes[Convert.ToByte(EntranceGrid.Rows[i].Cells[5].Value)] + "\" ]\r\n");
            }

            sw.Close();

        }
        private void EntranceTableEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.entrancetable_visible = false;


            if (MainForm.GlobalROM != "" || rom64.isSet())
            {
                mainform.RefreshExitCache();
            }
        }


    }
}
