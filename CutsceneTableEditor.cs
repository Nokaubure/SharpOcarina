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
    public partial class CutsceneTableEditor : Form
    {

        String OldFileName = "";
        Boolean isrom = false, isztable = false;
        Dictionary<byte, string> SceneNames;
        Dictionary<ushort, byte> EntranceScenes;

        public CutsceneTableEditor()
        {
            InitializeComponent();


            XmlNodeList nodes = XMLreader.getXMLNodes("OOT/SceneNames", "Scene");
            SceneNames = new Dictionary<byte, string>();
            EntranceScenes = new Dictionary<ushort, byte>();
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
                    string s = scene2.Substring(0, scene2.IndexOf("-"));
                    string name = scene2.Substring(scene2.IndexOf("-") + 1);
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
            }/*
            else if (rom64.isSet())
            {
                OpenRomToolStripMenuItem.Visible = false;
                OpenZ64rom();

            }*/
        }

        private void Close_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void OpenRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

            if (rom.Game == "MM")
            {
                MessageBox.Show("MM unsupported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            saveFileToolStripMenuItem.Visible = false;
            saveROMToolStripMenuItem.Visible = true;
            CutsceneGrid.AllowUserToAddRows = false;
            CutsceneGrid.AllowUserToDeleteRows = false;
            string scenename = "";
            byte scenenum = 0;
            CutsceneGrid.Rows.Clear();

            OldFileName = openFileDialog1.FileName;
            CutsceneGrid.Enabled = true;

            //building a way to link entrances to scene names
            int offset = (int)rom.EntranceTableStart;
            while (EntranceScenes.Count == 0 && offset < (int)rom.EntranteTableEnd - 1)
            {
                EntranceScenes.Add((ushort)((offset - (int)rom.EntranceTableStart) / 4), EntranceTable[offset]);
                offset += 4;
            }

            //creating the table
            offset = (int)rom.CutsceneTableStart;
            while (offset < (int)rom.CutsceneTableEnd - 1) //real end offset is 0x00B95534?
            {

                if (!EntranceScenes.TryGetValue(Convert.ToUInt16((Helpers.Read16(EntranceTable, offset)).ToString()), out scenenum)) scenename = "Unknown";
                else if (!SceneNames.TryGetValue(Convert.ToByte((scenenum).ToString()), out scenename)) scenename = "Unknown";

                CutsceneGrid.Rows.Add((offset - (int)rom.CutsceneTableStart) / 8, Helpers.Read16(EntranceTable, offset), EntranceTable[offset + 2], EntranceTable[offset + 3], Helpers.Read32(EntranceTable, offset + 4), scenename);
                offset += 8;
            }
            RefreshRowCount();
        }

        public void OpenZ64rom()
        {
            //not implemented

            TomlTable toml = rom64.parseToml(rom64.getPath() + "\\src\\spawn_cutscene_table.toml");
            TomlNode[] nodes = toml.Children.ToArray();
            string[] keys = toml.Keys.ToArray();


            saveFileToolStripMenuItem.Visible = false;
            saveROMToolStripMenuItem.Visible = true;
            CutsceneGrid.AllowUserToAddRows = false;
            CutsceneGrid.AllowUserToDeleteRows = false;
            string scenename = "";
            byte scenenum = 0;
            CutsceneGrid.Rows.Clear();

            OldFileName = openFileDialog1.FileName;
            CutsceneGrid.Enabled = true;

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
                int TableOffset = (int)rom.CutsceneTableStart;
                BWS.Seek(TableOffset, SeekOrigin.Begin);

                List<Byte> Output = GenerateCutsceneData();
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
            NumCutscenesLabel.Text = "Number of cutscenes: " + (CutsceneGrid.RowCount);
            for (int i = 0; i < CutsceneGrid.RowCount - 1; i++)
            {
                CutsceneGrid.Rows[i].Cells[0].Value = i;
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

        private void CutsceneGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (new int[] {1, 2, 3, 4}.Contains(e.ColumnIndex))
            {
                if (e != null && e.Value != null && e.DesiredType.Equals(typeof(string)))
                {
                    try
                    {
                        if (e.ColumnIndex == 1)
                            e.Value = string.Format("{0:X4}", e.Value);
                        else if (e.ColumnIndex == 4)
                            e.Value = string.Format("{0:X8}", e.Value);
                        else
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

        private void CutsceneGrid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (new int[] { 1, 2, 3, 4}.Contains(e.ColumnIndex))
            {
                //   Console.WriteLine("Desired type: "  + e.Value.GetType());
                if (e != null && e.Value != null && e.Value.GetType().Equals(typeof(System.String)))
                {
                    string str = (e.Value as string);
                    try
                    {
                        if (e.ColumnIndex == 1)
                            e.Value = Convert.ToUInt16(str, 16);
                        else if (e.ColumnIndex == 4)
                            e.Value = Convert.ToUInt32(str, 16);
                        else
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
            saveROMToolStripMenuItem_Click(new object(), new EventArgs());
        }

        private void CutsceneTableEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.cutscenetable_visible = false;

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
                    List<Byte> CutsceneData = GenerateCutsceneData();
                    File.WriteAllBytes(saveFileDialog1.FileName, CutsceneData.ToArray());
                    MessageBox.Show("Done! File Size: " + CutsceneData.Count.ToString("X") + " bytes", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private List<byte> GenerateCutsceneData()
        {

            List<Byte> Output = new List<byte>();
            for (int i = 0; i < CutsceneGrid.RowCount - 1; i++)
            {
                Output.AddRange(BitConverter.GetBytes(Convert.ToUInt16(CutsceneGrid.Rows[i].Cells[1].Value)).Reverse());
                Output.Add(Convert.ToByte(CutsceneGrid.Rows[i].Cells[2].Value));
                Output.Add(Convert.ToByte(CutsceneGrid.Rows[i].Cells[3].Value));
                Console.WriteLine(CutsceneGrid.Rows[i].Cells[4].Value + " p ");
                Console.WriteLine(Convert.ToUInt32(CutsceneGrid.Rows[i].Cells[4].Value).ToString("X"));
                Output.AddRange(BitConverter.GetBytes(Convert.ToUInt32(CutsceneGrid.Rows[i].Cells[4].Value)).Reverse());
            }

            return Output;
        }

        private void refresh(int column, int row)
        {
            try
            {
            if (column == 1)
            {
                string scenename;
                byte scenenum;
                try
                {

                        if (!EntranceScenes.TryGetValue(Convert.ToUInt16((CutsceneGrid.Rows[row].Cells[1].Value).ToString()), out scenenum)) scenename = "Unknown";
                        else if (!SceneNames.TryGetValue(Convert.ToByte((scenenum).ToString()), out scenename)) scenename = "Unknown";

                        CutsceneGrid.Rows[row].Cells[5].Value = scenename;
                }
                catch (System.FormatException)
                {
                    //nothing
                }
            }
            if (column == 2)
            {
                CutsceneGrid.Rows[row].Cells[column].Value = MainForm.Clamp(Convert.ToByte(CutsceneGrid.Rows[row].Cells[column].Value), 0, 2);
            }
            }
            catch (System.FormatException)
            {
                //nothing
                Console.WriteLine("Incorrect format? " + CutsceneGrid.Rows[row].Cells[1].Value);
            }
        }
    }
}
