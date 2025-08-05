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
        public Z64romInstallPostOperations(string path, bool defaults)
        {
            InitializeComponent();
            RemoveAllScenesCheckBox.Checked = defaults;
            RemoveLogoCheckBox.Checked = defaults;
            ClearCutsceneTableCheckBox.Checked = defaults;
            RemovePrerenderedsCheckBox.Checked = defaults;
            removeallscenes = defaults;
            removen64logo = defaults;
            clearcutscenetable = defaults;
            removeprerendereds = defaults;
            WarningLabel.Visible = !defaults;
            ABButtonColorsDropdown.SelectedIndex = 0;

            if (!defaults)
            {
                SaveAnywhereCheckbox.Checked = Helpers.GetDefineBool("SAVE_ANYWHERE", path + @"\src\lib_user\uLib.h");
                MMBunnyHoodCheckBox.Checked = Helpers.GetDefineBool("MM_BUNNYHOOD", path + @"\src\lib_user\uLib.h");
                MMButtonShadowsCheckBox.Checked = Helpers.GetDefineBool("Patch_MM_INTERFACE_BUTTONS_CORDS", path + @"\src\lib_user\uLib.h");
                //MMButtonShadowsCheckBox.Checked = Helpers.GetDefineBool("Patch_MM_INTERFACE_SHADOWS", path + @"\src\lib_user\uLib.h");
                //MMButtonShadowsCheckBox.Checked = Helpers.GetDefineBool("Patch_INTERFACE_C_UP_TATL", path + @"\src\lib_user\uLib.h");
                //.Checked = Helpers.GetDefineBool("Patch_MM_INTERFACE_RUPEE_UPGRADES", path + @"\src\lib_user\uLib.h");
                MMCbuttonColorsCheckBox.Checked = Helpers.GetDefineBool("Patch_INTERFACE_C_BUTTON_COLORS_MM", path + @"\src\lib_user\uLib.h");
                MMEntranceTitleCardsCheckBox.Checked = Helpers.GetDefineBool("MM_TITLECARD", path + @"\src\lib_user\uLib.h");
                
                string AB = Helpers.GetDefineString("Patch_INTERFACE_BUTTON_COLORS", path + @"\src\lib_user\uLib.h");

                ABButtonColorsDropdown.SelectedIndex = (AB == "OOT") ? 0 : (AB == "MM" ? 1 : 2);
            }

            Init();

        }

        private void Init()
        {
        }


        private void Ok_Click(object sender, EventArgs e)
        {
            ABButtonColors = ABButtonColorsDropdown.SelectedItem.ToString();
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
            MMBunnyHood = MMBunnyHoodCheckBox.Checked;
        }

        private void ABButtonColorsDropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ABButtonColors = ABButtonColorsDropdown.SelectedItem.ToString();
        }

        private void WarningLabel_Click(object sender, EventArgs e)
        {

        }

    }
}
