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
    public partial class ImportDataFromFile : Form
    {

        public int room;
        public int header;
        public bool collision = false;
        public bool cameras = false;
        public bool waterboxes = false;
        public bool pathways = false;
        public bool transitions = false;
        public bool spawns = false;
        public bool environments = false;
        public bool actors = false;
        public bool objects = false;
        public bool cutscene = false;
        public bool textureanimations = false;
        public bool actorcutscenes = false;
        public bool exits = false;

        public ImportDataFromFile(int rooms, int headers, string filename)
        {
            InitializeComponent();
            this.Text = "Import data from " + Path.GetFileName(filename);
            string extension = Path.GetExtension(filename).ToLower();
            RoomID.Maximum = rooms - 1;
            HeaderID.Maximum = headers - 1;
            if (!MainForm.settings.MajorasMask) ActorCutsceneCheckbox.Enabled = false;
            if (extension != ".xml" && !MainForm.settings.MajorasMask) TextureAnimCheckbox.Enabled = false;
            if (rooms == 0)
            {
                ActorCheckbox.Enabled = false;
                ObjectCheckbox.Enabled = false;
            }

        }


        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            room = (int) RoomID.Value;
            header = (int)HeaderID.Value;
            this.Close();
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CollisionCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            collision = CollisionCheckbox.Checked;
        }

        private void CameraCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            cameras = CameraCheckbox.Checked;
        }

        private void WaterboxCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            waterboxes = WaterboxCheckbox.Checked;
        }

        private void PathwayCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            pathways = PathwayCheckbox.Checked;
        }

        private void TransitionCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            transitions = TransitionCheckbox.Checked;
        }

        private void SpawnCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            spawns = SpawnCheckbox.Checked;
        }

        private void EnvironmentCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            environments = EnvironmentCheckbox.Checked;
        }

        private void ActorCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            actors = ActorCheckbox.Checked;
        }

        private void ObjectCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            objects = ObjectCheckbox.Checked;
        }

        private void CutsceneCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            cutscene = CutsceneCheckbox.Checked;
        }

        private void TextureAnimCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            textureanimations = TextureAnimCheckbox.Checked;
        }

        private void ActorCutsceneCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            actorcutscenes = ActorCutsceneCheckbox.Checked;
        }

        private void ExitCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            exits = ExitCheckbox.Checked;
        }
    }
}
