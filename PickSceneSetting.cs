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
    public partial class PickSceneSetting : Form
    {
        public List<Byte> Data;
        public int resultoffset;
        public int bank;

        public PickSceneSetting(List<byte> _Data, int _bank)
        {
            InitializeComponent();
            Data = _Data;
            bank = _bank;
            Init();
        }
        public void Init()
        {
            int offset = Helpers.Read24S(Data, 5);
            SceneSettingComboBox.Items.Clear();
            SongItem item = new SongItem();
            item.Text = "00 - Normal Header";
            item.Value = 4;
            SceneSettingComboBox.Items.Add(item);
            int count = 1;
            while(offset < Data.Count)
            {
                uint test = Helpers.Read24(Data, offset+1);
                if (Data[offset] == bank && test < Data.Count)
                {
                    item = new SongItem();
                    item.Text = count.ToString("X2") + " - Alternate header " + count;
                    item.Value = test;
                    SceneSettingComboBox.Items.Add(item);
                }
                else if(Helpers.Read32(Data, offset) != 0)
                {
                    break;
                }

                count++;
                offset += 4;

            }
            SceneSettingComboBox.SelectedIndex = 0;
            resultoffset = 4;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SceneSettingComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            resultoffset = Convert.ToInt32(((SongItem)SceneSettingComboBox.SelectedItem).Value);
        }
    }
}
