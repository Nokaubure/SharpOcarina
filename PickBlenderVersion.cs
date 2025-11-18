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
    public partial class PickBlenderVersion : Form
    {
        public List<Byte> Data;
        public string path = "";

        public PickBlenderVersion()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string blenderBasePath = System.IO.Path.Combine(appData, "Blender Foundation", "Blender");

            List<string> versions = new List<string>();

            if (Directory.Exists(blenderBasePath))
            {
                foreach (string dir in Directory.GetDirectories(blenderBasePath))
                {
                    versions.Add(Path.GetFileName(dir));
                }
            }
            if (versions.Count == 0)
            {
                MessageBox.Show("No 3.0+ blender installations found in your appdata folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SongItem item = new SongItem();
            item.Text = "Select a version";
            item.Value = "";
            BlenderVersionComboBox.Items.Add(item);
            BlenderVersionComboBox.SelectedIndex = 0;

            foreach (string v in versions)
            {
                item = new SongItem();
                item.Text = v;
                item.Value = blenderBasePath + "/" + v;
                BlenderVersionComboBox.Items.Add(item);
            }




        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SceneSettingComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
           path = Convert.ToString(((SongItem)BlenderVersionComboBox.SelectedItem).Value);
        }

        private void PickBlenderVersion_FormClosed(object sender, FormClosedEventArgs e)
        {
            //path = "";
        }
    }
}
