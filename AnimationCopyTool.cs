using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SharpOcarina
{
    public partial class CopyAnimationsForm : Form
    {
        List<byte> SourceZobj = new List<byte>();
        List<byte> TargetZobj = new List<byte>();
        string TargetZobjFilename = "";
        List<Animation> SourceAnimations = new List<Animation>();
        List<Animation> TargetAnimations = new List<Animation>();

        public CopyAnimationsForm()
        {
            InitializeComponent();
        }

        private void SourceButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZObj Files (*.zobj*)|*.zobj*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                SourceZobj = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                SourceFilename.Text = openFileDialog1.FileName;

                UpdateListbox(SourceZobj, ref SourceAnimations, ref SourceListBox);

                if (TargetZobj.Count > 0) MoveButton.Enabled = true;
            }
        }





        private void TargetButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ZObj Files (*.zobj*)|*.zobj*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                TargetZobj = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                TargetFilename.Text = openFileDialog1.FileName;

                UpdateListbox(TargetZobj, ref TargetAnimations, ref TargetListBox);

                if (SourceZobj.Count > 0) MoveButton.Enabled = true;

            }
        }

        private void UpdateListbox(List<byte> zobjlist, ref List<Animation> animations, ref ListBox listbox)
        {
            // ADD BANK SUPPORT
            byte[] zobj = zobjlist.ToArray();
            animations.Clear();
            listbox.Items.Clear();

            for (int ii = 0; ii < zobj.Length; ii += 4)
            {
                if (!(ii + 4 > zobj.GetUpperBound(0)))
                {
                    if (zobj[ii + 2] == 0x00 && zobj[ii + 3] == 0x00 && zobj[ii + 4] == 0x06 && zobj[ii + 8] == 0x06 && zobj[ii + 14] == 0x00 && zobj[ii + 15] == 0x00)
                    {

                        //  Console.WriteLine(string.Format("{0:D4} Frames, 06{1:X6}", frames, ii));

                        int animrotvaloffset = Helpers.Read24S(zobjlist, ii + 5);
                        int animrotindexoffset = Helpers.Read24S(zobjlist, ii + 9);
                        ushort frames = Helpers.Read16(zobjlist, ii);

                        // if (animrotindexoffset < animrotvaloffset) animrotvaloffset = animrotindexoffset;


                        animations.Add(new Animation((uint) animrotvaloffset, (uint) animrotindexoffset, (uint) ii, frames));


                    }
                }
            }

            if (animations.Count == 0)
            {
                MessageBox.Show("No animations found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (Animation anim in animations)
                {
                    listbox.Items.Add(anim.headeroffset.ToString("X8") + " (frames: " + anim.frames + ")");
                }
                if (listbox.SelectionMode != SelectionMode.None) listbox.SelectedIndex = 0;
            }
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
           if (SourceAnimations[SourceListBox.SelectedIndex].moved)
           {
                MessageBox.Show("You already copied this animation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<byte> animdata = new List<byte>();

            uint startoffset = Math.Min(SourceAnimations[SourceListBox.SelectedIndex].headeroffset, SourceAnimations[SourceListBox.SelectedIndex].rotindexoffset);
            startoffset = Math.Min(startoffset, SourceAnimations[SourceListBox.SelectedIndex].rotvaloffset);
            uint endoffset = SourceAnimations[SourceListBox.SelectedIndex].headeroffset + 0x10;

            for (uint i = startoffset; i < endoffset; i++)
            {
                animdata.Add(SourceZobj[(int) i]);
            }

            uint newoffset = (uint) TargetZobj.Count;

            uint newheader = SourceAnimations[SourceListBox.SelectedIndex].headeroffset - startoffset + newoffset;
            uint newrotvaloffset = SourceAnimations[SourceListBox.SelectedIndex].rotvaloffset - startoffset + newoffset;
            uint newrotindexoffset = SourceAnimations[SourceListBox.SelectedIndex].rotindexoffset - startoffset + newoffset;

            TargetZobj.AddRange(animdata);

            Helpers.Overwrite32(ref TargetZobj, (int) newheader + 0x4, 0x06000000 + newrotvaloffset);

            Helpers.Overwrite32(ref TargetZobj, (int)newheader + 0x8, 0x06000000 + newrotindexoffset);

            TargetListBox.Items.Add(newheader.ToString("X8") + " (frames: " + SourceAnimations[SourceListBox.SelectedIndex].frames + ")");

            SourceAnimations[SourceListBox.SelectedIndex].moved = true;

            SaveButton.Enabled = true;
            SaveAsButton.Enabled = true;
        }   

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MainForm.IsFileLocked(TargetFilename.Text))
                MessageBox.Show("Zobj is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                BinaryWriter BWS = new BinaryWriter(File.OpenWrite(TargetFilename.Text));
                BWS.Seek(0, SeekOrigin.Begin);

                while(TargetZobj.Count % 0x10 != 0)
                    TargetZobj.Add(0);


                BWS.Write(TargetZobj.ToArray());

                BWS.Close();

                MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {

            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.Filter = "wavefront obj file (*.obj)|*.obj|All Files (*.*)|*.*";
            saveFileDialog1.CreatePrompt = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (MainForm.IsFileLocked(saveFileDialog1.FileName))
                    MessageBox.Show("File is in use... try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    BinaryWriter BWS = new BinaryWriter(File.OpenWrite(saveFileDialog1.FileName));
                    BWS.Seek(0, SeekOrigin.Begin);

                    while (TargetZobj.Count % 0x10 != 0)
                        TargetZobj.Add(0);

                    BWS.Write(TargetZobj.ToArray());

                    BWS.Close();

                    MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

           
        }

        private void CopyAnimationsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void SourceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }

    public class Animation
    {
        public ushort frames;
        public uint rotvaloffset;
        public uint rotindexoffset;
        public uint headeroffset;
        public bool moved = false;

        public Animation(uint rotvaloffset, uint rotindexoffset, uint headeroffset, ushort frames)
        {
            this.rotvaloffset = rotvaloffset;
            this.rotindexoffset = rotindexoffset;
            this.headeroffset = headeroffset;
            this.frames = frames;
        }
    }
}
