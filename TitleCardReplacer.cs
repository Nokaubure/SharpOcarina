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
    public partial class TitleCardReplacer : Form
    {

        public ushort sceneid;
        public bool empty = true;
        public byte[] titlecard = new byte[]{};
        List<byte> ROM;
        string ROMfile;
        PrivateFontCollection pfc = new PrivateFontCollection();
        public TitleCardReplacer(byte[] _titlecard)
        {
            titlecard = _titlecard;

            InitializeComponent();

            if (titlecard.Length != 0)
            {
                MemoryStream ms = new MemoryStream(titlecard);
                TextureBox.Image = Image.FromStream(ms);

                empty = false;
            }
      

            //Create your private font collection object.

            int fontLength = Properties.Resources.FOT_ChiaroStd_B.Length;
            byte[] fontdata = Properties.Resources.FOT_ChiaroStd_B;
            try
            {
                 System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
                 Marshal.Copy(fontdata, 0, data, fontLength);
                 pfc.AddMemoryFont(data, fontLength);
                 GenerateTextbox.Font = new Font(pfc.Families[0], GenerateTextbox.Font.Size);

            }
            catch (FileNotFoundException e)
            {
                try
                {
                fontLength = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/FOT-ChiaroStd-BWin7.otf")).Length;
                fontdata = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/FOT-ChiaroStd-BWin7.otf"));
                System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
                Marshal.Copy(fontdata, 0, data, fontLength);
                pfc.AddMemoryFont(data, fontLength);
                GenerateTextbox.Font = new Font(pfc.Families[0], GenerateTextbox.Font.Size);
                }
                catch(FileNotFoundException ee)
                {
                    MessageBox.Show("Sorry, seems that zelda font cannot be loaded in your computer for unknown reasons... using Arial font instead", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            GenerateTextbox.Text = "";

            Init();
        }

        public void Init()
        {
            /*
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
            }*/

   

        }

        public void RefreshTexture()
        {
            ROM rom = MainForm.CheckVersion(ROM);
            int Offset = (int)rom.SceneTable + (sceneid * 0x14 + 0x08);

            int startoffset = (int)Helpers.Read32(ROM, Offset);
            int endoffset = (int)Helpers.Read32(ROM, Offset + 4);

            TitleLabel.Text = "Current Title Card (" + startoffset.ToString("X8") + " - " + endoffset.ToString("X8") + "):";

            InjectOffsetTextbox.Text = startoffset.ToString("X");

            if (startoffset == 0) return;

            int size = endoffset - startoffset;

            Bitmap texture = new Bitmap(144, 24);
            int x = 0;
            int y = 0;
            int gray = 0;
            int alpha = 0;
            Color col = new Color();

            for (int i = 0; i < size; i++)
            {
                //   DebugConsole.WriteLine(((0x4F & 0xF0) >> 4).ToString("X"));
                gray = ((ROM[startoffset + i] & 0xF0) >> 4) * 17;
                alpha = (ROM[startoffset + i] & 0x0F) * 17;
                col = Color.FromArgb(alpha, gray, gray, gray);
                texture.SetPixel(x, y, col);
                x++;
                if (x >= texture.Width) { x = 0; y++; }
                if (y >= texture.Height) break;
            }

            TextureBox.Image = texture;
            //  DebugConsole.WriteLine("tableoffset: " + (TableOffset + 16).ToString("X"));
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadFromFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image (PNG) (*.png)|*.png";

            //openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var data = new List<byte>(File.ReadAllBytes(openFileDialog1.FileName));

                GenerateTextbox.Text = "";
                EnableGenerator.Checked = false;

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
                    //   DebugConsole.WriteLine(((0x4F & 0xF0) >> 4).ToString("X"));
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (InjectOffsetTextbox.IntValue == 0) MessageBox.Show("You must enter an injection address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (TextureBox.Image == null) MessageBox.Show("There's no image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                ROM rom = MainForm.CheckVersion(ROM);

                if (rom.Game == "MM")
                {
                    MessageBox.Show("MM unsupported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                BinaryWriter BWS = new BinaryWriter(File.OpenWrite(ROMfile));

                BWS.Seek(InjectOffsetTextbox.IntValue, SeekOrigin.Begin);

                List<Byte> Output = new List<byte>();

                Bitmap texture = (Bitmap) TextureBox.Image;

                int x = 0;
                int y = 0;
                int gray = 0;
                int alpha = 0;

                for (int i = 0; y < 72; i++)
                {
                    //   DebugConsole.WriteLine(((0x4F & 0xF0) >> 4).ToString("X"));

                    if (y < texture.Height)
                    {
                        gray = (texture.GetPixel(x, y).R != 0) ? texture.GetPixel(x, y).R / 17 : 0;
                        alpha = (texture.GetPixel(x, y).A != 0) ? texture.GetPixel(x, y).A / 17 : 0;
                        Output.Add((byte)(0x00 | (gray << 4) | (alpha)));
                    }
                    else
                    {
                        Output.Add(0x00);
                    }
                    

                    x++;
                    if (x >= texture.Width) { x = 0; y++; }
                }
                BWS.Write(Output.ToArray());

                int Offset = (int)rom.SceneTable + (sceneid * 0x14 + 0x08);

                BWS.Seek(Offset, SeekOrigin.Begin);
                BWS.Write((BitConverter.GetBytes(InjectOffsetTextbox.IntValue).Reverse()).ToArray());
                BWS.Seek(Offset+4, SeekOrigin.Begin);
                BWS.Write((BitConverter.GetBytes(InjectOffsetTextbox.IntValue + 10368).Reverse()).ToArray());
                BWS.Close();

                ROM = new List<byte>(File.ReadAllBytes(ROMfile));

                RefreshTexture();

                MessageBox.Show("Done! End injection offset is " + (InjectOffsetTextbox.IntValue + 10368).ToString("X"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InjectOffsetTextbox_Leave(object sender, EventArgs e)
        {

        }

        private void InjectOffsetTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
            //    InjectOffset = InjectOffsetTextbox.IntValue;
            }
        }

        private void FontSize_ValueChanged(object sender, EventArgs e)
        {
            if (EnableGenerator.Checked)
            {
                Generate();
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {

            Generate();
        }

        private void Generate()
        {

            Bitmap texture = new Bitmap(144, 24);

            Graphics g = Graphics.FromImage(texture);

            g.Clear(Color.FromArgb(0, 0, 0, 0));

            if (GenerateTextbox.Text != "")
            {

                empty = false;


                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                Font font;

                if (pfc.Families.Length > 0)
                {
                    font = new Font(pfc.Families[0], (float)FontSize.Value);
                }
                else
                {
                    font = GenerateTextbox.Font;
                }

                int xpos = (int)XPos.Value;
                int ypos = (int)YPos.Value;


                //border

                /*
                int v = 2;
                Color c = Color.FromArgb(127, 0, 0, 0);
                for (int i=0;i<1;i++)
                {
                    for (int ii = 0; ii < 1; ii++)
                    {
                        g.DrawString(GenerateTextbox.Text, GenerateTextbox.Font, new SolidBrush(c), new PointF(72 - v, 12 - v), sf);
                        g.DrawString(GenerateTextbox.Text, GenerateTextbox.Font, new SolidBrush(c), new PointF(72 - v, 12), sf);
                        g.DrawString(GenerateTextbox.Text, GenerateTextbox.Font, new SolidBrush(c), new PointF(72, 12 - v), sf);
                        g.DrawString(GenerateTextbox.Text, GenerateTextbox.Font, new SolidBrush(c), new PointF(72, 12 - v), sf);

                        c = Color.FromArgb(255, 0, 0, 0);
                        v = -v;
                    }
                    v-=2;
                }*/

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.ScaleTransform((float)XScale.Value, (float)VScale.Value);

                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.Black), new PointF(71 + xpos, 11 + ypos), sf);
                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.Black), new PointF(73 + xpos, 13 + ypos), sf);
                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.Black), new PointF(72 + xpos, 11 + ypos), sf);
                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.Black), new PointF(71 + xpos, 12 + ypos), sf);
                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.Black), new PointF(72 + xpos, 13 + ypos), sf);
                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.Black), new PointF(73 + xpos, 12 + ypos), sf);
                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.Black), new PointF(71 + xpos, 13 + ypos), sf);
                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.Black), new PointF(73 + xpos, 11 + ypos), sf);


                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.Black), new PointF(72 + xpos, 12 + ypos), sf);

                texture = Blur(texture, 2);

                g = Graphics.FromImage(texture);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.ScaleTransform((float)XScale.Value, (float)VScale.Value);

                //real text
                g.DrawString(GenerateTextbox.Text, font, new SolidBrush(Color.White), new PointF(72 + xpos, 12 + ypos), sf);
            }
            else
            {
                empty = true;
            }

            TextureBox.Image = texture;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void VScale_ValueChanged(object sender, EventArgs e)
        {
            if (EnableGenerator.Checked)
            {
                Generate();
            }
        }

        private Bitmap Blur(Bitmap image, Int32 blurSize)
        {
            return Blur(image, new Rectangle(0, 0, image.Width, image.Height), blurSize);
        }

        private Bitmap Blur(Bitmap image, Rectangle rectangle, Int32 blurSize)
        {
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // look at every pixel in the blur rectangle
            for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    int avgR = 0, avgG = 0, avgB = 0, avgA = 0;
                    int blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (int x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (int y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;
                            avgA += pixel.A;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;
                    avgA = avgA / blurPixelCount;


                    // now that we know the average for the blur size, set each pixel to that color
                    for (int x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                        for (int y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgA, avgR, avgG, avgB));
                }
            }

            for (int x = 0; x < image.Width && x < rectangle.Width; x++)
                for (int y = 0; y < image.Height && y < rectangle.Height; y++)
                {
                    int alpha = (int)(blurred.GetPixel(x, y)).A;
                    if (alpha > 48) alpha = MainForm.Clamp(alpha + 64, 0, 255);
                        blurred.SetPixel(x, y, Color.FromArgb(alpha, (blurred.GetPixel(x, y)).R, (blurred.GetPixel(x, y)).G, (blurred.GetPixel(x, y)).B));
                  }

            return blurred;
        }

        private void TitleCardReplacer_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.titlecardeditor_visible = false;
        }

        private void EnableGenerator_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control Ctrl in GenerateGroupBox.Controls)
                if (Ctrl != EnableGenerator) Ctrl.Enabled = EnableGenerator.Checked;
        }

        private void GenerateTextbox_TextChanged(object sender, EventArgs e)
        {
            if (EnableGenerator.Checked)
            {
                Generate();
            }
        }

        private void XPos_ValueChanged(object sender, EventArgs e)
        {
            if (EnableGenerator.Checked)
            {
                Generate();
            }
        }

        private void YPos_ValueChanged(object sender, EventArgs e)
        {
            if (EnableGenerator.Checked)
            {
                Generate();
            }
        }

        private void XScale_ValueChanged(object sender, EventArgs e)
        {
            if (EnableGenerator.Checked)
            {
                Generate();
            }
        }

        private void NewSaveButton_Click(object sender, EventArgs e)
        {
            if (empty)
            {
                titlecard = new byte[] { };
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                TextureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                titlecard = ms.ToArray();
            }


            DialogResult = DialogResult.OK;
            Close();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            GenerateTextbox.Text = "";
            Generate();
            EnableGenerator.Checked = false;

        }
    }
}
