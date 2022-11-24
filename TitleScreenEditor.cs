using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenTK.Graphics;
using OpenTK.Input;

namespace SharpOcarina
{
    public partial class TitleScreenEditor : Form
    {
        //Title screen logo:

        public ushort sceneid;
        List<byte> ROM;
        string ROMfile;
        PrivateFontCollection pfc = new PrivateFontCollection();
        public TitleScreenEditor(ushort _sceneid)
        {
            sceneid = _sceneid;
            ROM = new List<byte>();
            ROMfile = "";



            InitializeComponent();

            Init();
        }

        public void Init()
        {

            if (MainForm.GlobalROM != "")
            {
                ROM = new List<byte>(File.ReadAllBytes(MainForm.GlobalROM));
                ROMfile = MainForm.GlobalROM;
                RefreshTexture();
            }

            else
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Rom files (*.z64;*.rom)|*.z64;*.rom|All Files (*.*)|*.*";

                //openFileDialog1.FilterIndex = 1;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    ROM = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));
                    ROMfile = openFileDialog1.FileName;
                    RefreshTexture();

                }
                else this.Close();
            }

   

        }

        public void RefreshTexture()
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image (PNG) (*.png)|*.png";

            //openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var data = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                Bitmap texture = new Bitmap(openFileDialog1.FileName);
                if (texture.Width != 144 || texture.Height != 24)
                {
                    MessageBox.Show("Your image must be 144x24!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int x = 0;
                int y = 0;
                int gray = 0;
                int alpha = 0;
                Color col = new Color();

                for (int i = 0; y < texture.Height; i++)
                {
                    //   Console.WriteLine(((0x4F & 0xF0) >> 4).ToString("X"));
                    col = Color.FromArgb(texture.GetPixel(x, y).A, texture.GetPixel(x,y).R, texture.GetPixel(x, y).R, texture.GetPixel(x, y).R);
                    texture.SetPixel(x, y, col);
                    x++;
                    if (x >= texture.Width) { x = 0; y++; }
                    if (y >= texture.Height) break;
                }

                TextureBox.Image = texture;

            }
        }

        private void ExtractButton_Click(object sender, EventArgs e)
        {
            if (TextureBox.Image == null) return;

            saveFileDialog1.CheckFileExists = false;
            //    saveFileDialog1.FileName = Path.GetFileName(OldFileName) + "_new";
            saveFileDialog1.Filter = "Image (*.png)|*.png";
            saveFileDialog1.CreatePrompt = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                TextureBox.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);

                MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void VScale_ValueChanged(object sender, EventArgs e)
        {

        }
        
    }
}
