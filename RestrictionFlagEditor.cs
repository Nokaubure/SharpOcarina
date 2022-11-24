using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SharpOcarina
{
    public partial class RestrictionFlagEditor : Form
    {

        String OldFileName = "";
        Boolean isrom = false, isztable = false;
        Dictionary<byte,string> SceneNames;

        public RestrictionFlagEditor()
        {
            InitializeComponent();


            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlNodeList nodes = XMLreader.getXMLNodes(gameprefix + "SceneNames", "Scene");
            SceneNames = new Dictionary<byte, string>();
            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection nodeAtt = node.Attributes;
                if (nodeAtt["Key"] != null)
                {
                    SceneNames.Add(Convert.ToByte(nodeAtt["Key"].Value, 16), node.InnerText);
                  //  Console.WriteLine(Convert.ToByte(nodeAtt["Key"].Value, 16) + " " + node.InnerText);
                }
            }


            if (MainForm.GlobalROM != "")
            {
                OpenRomToolStripMenuItem.Visible = false;
                OpenROM(MainForm.GlobalROM);
            }
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

            List<byte> RestrictionFlagTable = new List<byte>(File.ReadAllBytes(ROM));

            ROM rom = MainForm.CheckVersion(RestrictionFlagTable);

            saveROMToolStripMenuItem.Visible = true;
            RestrictionFlagGrid.AllowUserToAddRows = false;
            RestrictionFlagGrid.AllowUserToDeleteRows = false;
            ushort variable;
            string scenename = "";
            RestrictionFlagGrid.Rows.Clear();

            OldFileName = openFileDialog1.FileName;
            int offset = (int)rom.RestrictionFlagStart;
            RestrictionFlagGrid.Enabled = true;
            while (offset < ((int)rom.RestrictionFlagEnd) - 1)
            {
                // variable = Helpers.Read16(RestrictionFlagTable, offset + 2);

                if (!SceneNames.TryGetValue(RestrictionFlagTable[offset], out scenename)) scenename = "Unknown";


                if (!MainForm.settings.MajorasMask || (MainForm.settings.MajorasMask && RestrictionFlagTable[offset] != 0x80))
                    RestrictionFlagGrid.Rows.Add((offset - (int)rom.RestrictionFlagStart) / 4, RestrictionFlagTable[offset], ((RestrictionFlagTable[offset + 1] & 0x03) > 0), (RestrictionFlagTable[offset + 1] & 0x0C) > 0, (RestrictionFlagTable[offset + 1] & 0x30) > 0, (RestrictionFlagTable[offset + 1] & 0xC0) > 0, ((RestrictionFlagTable[offset + 2] & 0x03) > 0), (RestrictionFlagTable[offset + 2] & 0x0C) > 0, (RestrictionFlagTable[offset + 2] & 0x30) > 0, (RestrictionFlagTable[offset + 2] & 0xC0) > 0, ((RestrictionFlagTable[offset + 3] & 0x03) > 0), (RestrictionFlagTable[offset + 3] & 0x0C) > 0, (RestrictionFlagTable[offset + 3] & 0x30) > 0, (RestrictionFlagTable[offset + 3] & 0xC0) > 0, scenename);
                offset += 4;
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
                int TableOffset = (int)rom.RestrictionFlagStart;
                BWS.Seek(TableOffset, SeekOrigin.Begin);

                RestrictionFlagGrid.Sort(RestrictionFlagGrid.Columns[0], ListSortDirection.Ascending);

                List<Byte> Output = new List<byte>();
                for (int i = 0; i < RestrictionFlagGrid.RowCount; i++)
                {
                    Output.Add(Convert.ToByte(RestrictionFlagGrid.Rows[i].Cells[1].Value));
                    int XFlags = ((bool)RestrictionFlagGrid.Rows[i].Cells[2].Value ? 0x01 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[3].Value ? 0x04 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[4].Value ? 0x10 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[5].Value ? 0x40 : 0);
                    int YFlags = ((bool)RestrictionFlagGrid.Rows[i].Cells[6].Value ? 0x01 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[7].Value ? 0x04 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[8].Value ? 0x10 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[9].Value ? 0x40 : 0);
                    int ZFlags = ((bool)RestrictionFlagGrid.Rows[i].Cells[10].Value ? 0x01 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[11].Value ? 0x04 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[12].Value ? 0x10 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[13].Value ? 0x40 : 0);
                    Output.Add((byte)XFlags);
                    Output.Add((byte)YFlags);
                    Output.Add((byte)ZFlags);
                }
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
            NumEntriesLabel.Text = "Number of entries: " + (RestrictionFlagGrid.RowCount);
            for (int i = 0; i < RestrictionFlagGrid.RowCount - 1; i++)
            {
                RestrictionFlagGrid.Rows[i].Cells[0].Value = i;
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

            if (new int[]{1}.Contains(e.ColumnIndex))
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
            if (new int[] { 1}.Contains(e.ColumnIndex))
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
            saveROMToolStripMenuItem_Click(new object(), new EventArgs());
        }

        private void RestrictionFlagEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.restrictionflag_visible = false;
        }

        private void refresh(int column, int row)
        {
            try
            {
                if (column == 1)
                {
                    string scenename;
                   //     Console.WriteLine(Convert.ToByte(EntranceGrid.Rows[row].Cells[1].Value)); 
                        //Console.WriteLine(Convert.ToByte(EntranceGrid.Rows[row].Cells[1].Value));
                     if (!SceneNames.TryGetValue(Convert.ToByte((RestrictionFlagGrid.Rows[row].Cells[1].Value)), out scenename)) scenename = "Unknown";

                      RestrictionFlagGrid.Rows[row].Cells[14].Value = scenename;

                }
            }
            catch (System.FormatException)
            {
                //nothing
                Console.WriteLine("Incorrect format? " + RestrictionFlagGrid.Rows[row].Cells[1].Value);
            }
        }
    }
}
