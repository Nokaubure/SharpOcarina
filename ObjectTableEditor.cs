using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpOcarina
{
    public partial class ObjectTableEditor : Form
    {

        String OldFileName = "";

        public ObjectTableEditor()
        {
            InitializeComponent();
        }

        private void Close_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            
            //openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<byte> ObjectTable = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                if (Helpers.Read64(ObjectTable, 0x00) != 0x00000000) //if first bytes are not 00 it's not an object table
                {
                    MessageBox.Show("This doesn't look like an object table...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OldFileName = openFileDialog1.FileName;
                    int offset = 0;
                    ObjectGrid.Enabled = true;
                    while (offset < ObjectTable.Count)
                    {
                        ObjectGrid.Rows.Add(offset/8, Helpers.Read32(ObjectTable, offset), Helpers.Read32(ObjectTable, offset+4));
                        offset += 8;
                    }
                    RefreshRowCount();

                }


            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.FileName = Path.GetFileName(OldFileName) + "_new";
            saveFileDialog1.Filter = "All Files (*.*)|*.*";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CreatePrompt = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    List<Byte> Output = new List<byte>();
                    for (int i = 0; i < ObjectGrid.RowCount - 1; i++)
                    {
                        Output.AddRange(BitConverter.GetBytes(Convert.ToUInt32(ObjectGrid.Rows[i].Cells[1].Value.ToString())).Reverse());
                        Output.AddRange(BitConverter.GetBytes(Convert.ToUInt32(ObjectGrid.Rows[i].Cells[2].Value.ToString())).Reverse());
                        if (i==2)
                        {
                            Console.WriteLine(Convert.ToInt32("" + ObjectGrid.Rows[i].Cells[1].Value));
                            Console.WriteLine(Convert.ToUInt32("" + ObjectGrid.Rows[i].Cells[1].Value));
                            Console.WriteLine(Convert.ToInt32(ObjectGrid.Rows[i].Cells[1].Value.ToString()));
                            Console.WriteLine(ObjectGrid.Rows[i].Cells[1].Value.ToString(), 16);
                        }
                    }
                    File.WriteAllBytes(saveFileDialog1.FileName, Output.ToArray());
                    MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
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
            NumObjectsLabel.Text = "Number of objects: " + (ObjectGrid.RowCount - 1);
            for (int i = 0; i < ObjectGrid.RowCount - 1; i++)
            {
                ObjectGrid.Rows[i].Cells[0].Value = i;
            }
            ObjectGrid.Rows[ObjectGrid.RowCount - 1].Cells[0].Value = "";
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

        private void extractFromRomToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void ObjectGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
               // if (ObjectGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "") ObjectGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "00000000";
            }
        }
    }
}
