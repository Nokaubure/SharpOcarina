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
    public partial class PickItem : Form
    {
        public int result = 0;

        public PickItem()
        {
            InitializeComponent();
            PickItemComboBox.Items.AddRange(XMLreader.getXMLItems("OOT/" + "ItemList", "Item"));
            PickItemComboBox.SelectedIndex = 0;
            Init();
        }
        public void Init()
        {

        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SceneSettingComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            result = Convert.ToInt32(((SongItem)PickItemComboBox.SelectedItem).Value);
        }
    }
}
