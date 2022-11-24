using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpOcarina
{
    public partial class RestrictionFlagPicker : Form
    {

        public uint raw;

        public RestrictionFlagPicker(uint _restrictionflags)
        {
            raw = _restrictionflags;

            InitializeComponent();

            Init();
        }
        public void Init()
        {
            Restriction0.Checked = ((((raw & 0x00FF0000) >> 16) & 0x03)) != 0;
            Restriction1.Checked = ((((raw & 0x00FF0000) >> 16) & 0x0C)) != 0;
            Restriction2.Checked = ((((raw & 0x00FF0000) >> 16) & 0x30)) != 0;
            Restriction3.Checked = ((((raw & 0x00FF0000) >> 16) & 0xC0)) != 0;

            Restriction4.Checked = ((((raw & 0x0000FF00) >> 8) & 0x03)) != 0;
            Restriction5.Checked = ((((raw & 0x0000FF00) >> 8) & 0x0C)) != 0;
            Restriction6.Checked = ((((raw & 0x0000FF00) >> 8) & 0x30)) != 0;
            Restriction7.Checked = ((((raw & 0x0000FF00) >> 8) & 0xC0)) != 0;

            Restriction8.Checked = ((((raw & 0x000000FF)) & 0x03)) != 0;
            Restriction9.Checked = ((((raw & 0x000000FF)) & 0x0C)) != 0;
            Restriction10.Checked = ((((raw & 0x000000FF)) & 0x30)) != 0;
            Restriction11.Checked = ((((raw & 0x000000FF)) & 0xC0)) != 0;


            //if (!MainForm.settings.MajorasMask || (MainForm.settings.MajorasMask && RestrictionFlagTable[offset] != 0x80))
            //  RestrictionFlagGrid.Rows.Add((offset - (int)rom.RestrictionFlagStart) / 4, RestrictionFlagTable[offset], ((RestrictionFlagTable[offset + 1] & 0x03) > 0), (RestrictionFlagTable[offset + 1] & 0x0C) > 0, (RestrictionFlagTable[offset + 1] & 0x30) > 0, (RestrictionFlagTable[offset + 1] & 0xC0) > 0, ((RestrictionFlagTable[offset + 2] & 0x03) > 0), (RestrictionFlagTable[offset + 2] & 0x0C) > 0, (RestrictionFlagTable[offset + 2] & 0x30) > 0, (RestrictionFlagTable[offset + 2] & 0xC0) > 0, ((RestrictionFlagTable[offset + 3] & 0x03) > 0), (RestrictionFlagTable[offset + 3] & 0x0C) > 0, (RestrictionFlagTable[offset + 3] & 0x30) > 0, (RestrictionFlagTable[offset + 3] & 0xC0) > 0, scenename);

            // int XFlags = ((bool)RestrictionFlagGrid.Rows[i].Cells[2].Value ? 0x01 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[3].Value ? 0x04 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[4].Value ? 0x10 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[5].Value ? 0x40 : 0);
            // int YFlags = ((bool)RestrictionFlagGrid.Rows[i].Cells[6].Value ? 0x01 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[7].Value ? 0x04 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[8].Value ? 0x10 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[9].Value ? 0x40 : 0);
            //  int ZFlags = ((bool)RestrictionFlagGrid.Rows[i].Cells[10].Value ? 0x01 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[11].Value ? 0x04 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[12].Value ? 0x10 : 0) + ((bool)RestrictionFlagGrid.Rows[i].Cells[13].Value ? 0x40 : 0);
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            raw = (uint) (Restriction8.Checked ? 0x01 : 0);
            raw += (uint)(Restriction9.Checked ? 0x04 : 0);
            raw += (uint)(Restriction10.Checked ? 0x10 : 0);
            raw += (uint)(Restriction11.Checked ? 0x40 : 0);

            raw += (uint)(Restriction4.Checked ? 0x0100 : 0);
            raw += (uint)(Restriction5.Checked ? 0x0400 : 0);
            raw += (uint)(Restriction6.Checked ? 0x1000 : 0);
            raw += (uint)(Restriction7.Checked ? 0x4000 : 0);

            raw += (uint)(Restriction0.Checked ? 0x010000 : 0);
            raw += (uint)(Restriction1.Checked ? 0x040000 : 0);
            raw += (uint)(Restriction2.Checked ? 0x100000 : 0);
            raw += (uint)(Restriction3.Checked ? 0x400000 : 0);


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
