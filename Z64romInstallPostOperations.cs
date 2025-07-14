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
    public partial class Z64romInstallPostOperations : Form
    {
        public List<Byte> Data;
        public bool removeallscenes;
        public bool removen64logo;
        public bool clearcutscenetable;
        public bool removeprerendereds;
        public bool MMHUD = false;
        public bool MMTitleCard = false;
        public bool MMCButtonColors = false;
        public bool MMBunnyHood = false;
        public string ABButtonColors = "OOT";
        public bool SaveAnywhere = false;

        public Z64romInstallPostOperations(bool defaults)
        {
            InitializeComponent();
            RemoveAllScenesCheckBox.Checked = defaults;
            RemoveLogoCheckBox.Checked = defaults;
            ClearCutsceneTableCheckBox.Checked = defaults;
            RemovePrerenderedsCheckBox.Checked = defaults;
            WarningLabel.Visible = !defaults;
            ABButtonColorsDropdown.SelectedIndex = 0;

            Init();

        }

        private void Init()
        {
            
        }


        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            removeallscenes = RemoveAllScenesCheckBox.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            removen64logo = RemoveLogoCheckBox.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            clearcutscenetable = ClearCutsceneTableCheckBox.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            removeprerendereds = RemovePrerenderedsCheckBox.Checked;
        }

        private void SaveAnywhereCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SaveAnywhere = SaveAnywhereCheckbox.Checked;
        }

        private void MMButtonShadowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MMHUD = MMButtonShadowsCheckBox.Checked;
        }

        private void MMEntranceTitleCards_CheckedChanged(object sender, EventArgs e)
        {
            MMTitleCard = MMEntranceTitleCardsCheckBox.Checked;
        }

        private void MMCbuttonColorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MMCButtonColors = MMCbuttonColorsCheckBox.Checked;
        }

        private void MMBunnyHoodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MMBunnyHood = MMButtonShadowsCheckBox.Checked;
        }

        private void ABButtonColorsDropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ABButtonColors = ABButtonColorsDropdown.SelectedText;
        }
    }
}
