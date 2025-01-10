using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NImage;

namespace SharpOcarina
{
    public partial class FileToC : Form
    {
        List<byte> Data;
        List<byte> ConvertedData;


        public FileToC()
        {
            InitializeComponent();
            SongItem[] objs = new[]
{
                new SongItem {Text = "RGBA16", Value = 0},
                new SongItem {Text = "RGBA32", Value = 1},
                new SongItem {Text = "I4", Value = 2},
                new SongItem {Text = "I8", Value = 3},
                new SongItem {Text = "IA4", Value = 4},
                new SongItem {Text = "IA8", Value = 5},
                new SongItem {Text = "IA16", Value = 6},
            };
            ImageFormatComboBox.Items.AddRange(objs);
        }

        private void SourceButton_Click(object sender, EventArgs e)
        {


            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                LoadFile(openFileDialog1.FileName);

            }


        }

        private void LoadFile(string FileName)
        {

            Data = new List<byte>(File.ReadAllBytes(FileName));

            SourceFilename.Text = FileName;

            if (ObjFile.ValidImageTypes.IndexOf(Path.GetExtension(SourceFilename.Text).ToLowerInvariant()) != -1)
            {
                ImageFormatComboBox.Enabled = ImageFormatLabel.Enabled = true;
                ImageFormatComboBox.SelectedIndex = 0;
                GenerateTexture();

            }
            else
            {
                ImageFormatComboBox.Enabled = ImageFormatLabel.Enabled = false;
                TextureBox.Image = null;
                ConvertedData = new List<byte>();
            }

            SaveButton.Enabled = true;
            SizeLabel.Visible = true;
            SizeLabel.Text = "Size: " + Data.Count + " bytes";
        }

        private void GenerateTexture()
        {
            ObjFile.Material Mat = new ObjFile.Material();
            Mat.ForcedFormat = ImageFormatComboBox.Text;
            Mat.TexImage = new Bitmap(SourceFilename.Text);
            Mat.Name = Path.GetFileNameWithoutExtension(SourceFilename.Text);
            Mat.Width = Mat.TexImage.Width;
            Mat.Height = Mat.TexImage.Height;
            NTexture Texture = new NTexture();
            Texture.Convert(Mat);
            ConvertedData = Texture.Data.ToList();
            uint BufferSize = (uint) (Mat.Width * Mat.Height * 4);
            byte[] Result = new byte[BufferSize];
            Result.Fill(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
            switch (ImageFormatComboBox.Text)
            {
                case "RGBA16":
                {
                    uint linesize = (uint) (Texture.Width / 4);
                    NImageUtil.RGBA16((uint)Texture.Width, (uint)Texture.Height, linesize, Texture.Data, 0, ref Result);
                    break;
                }
                case "RGBA32":
                {
                    NImageUtil.RGBA32(Texture.Data, 0, ref Result);
                    break;
                }
                case "I4":
                {
                    float linesize = Texture.Width / 16.0f;
                    NImageUtil.I4((uint)Texture.Width, (uint)Texture.Height, linesize, Texture.Data, 0, ref Result);
                    break;
                }
                case "I8":
                {
                    float linesize = Texture.Width / 8.0f;
                    NImageUtil.I8((uint)Texture.Width, (uint)Texture.Height, linesize, Texture.Data, 0, ref Result);
                    break;
                }
                case "IA4":
                {
                    float linesize = Texture.Width / 16.0f;
                    NImageUtil.IA4((uint)Texture.Width, (uint)Texture.Height, linesize, Texture.Data, 0, ref Result);
                    break;
                }
                case "IA8":
                {
                    float linesize = Texture.Width / 8.0f;
                    NImageUtil.IA8((uint)Texture.Width, (uint)Texture.Height, linesize, Texture.Data, 0, ref Result);
                    break;
                }
                case "IA16":
                {
                    float linesize = Texture.Width / 4.0f;
                    NImageUtil.IA16((uint)Texture.Width, (uint)Texture.Height, linesize, Texture.Data, 0, ref Result);
                    break;
                }

            }
            
            TextureBox.Image = ResizeImage(ArrayToBitmap(Result, Texture.Width, Texture.Height), Texture.Width*2, Texture.Height*2);
            
            /*
            
            using (var ms = new MemoryStream(Result))
            {
                TextureBox.Image = Image.FromStream(ms);
            }*/
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }

        private Bitmap ArrayToBitmap(byte[] data, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            
                int offset = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color color = Color.FromArgb(
                            data[offset + 3], // Alpha
                            data[offset],     // Red
                            data[offset + 1], // Green
                            data[offset + 2]  // Blue
                        );
                        bitmap.SetPixel(x, y, color);
                        offset += 4;
                    }
                }
            return bitmap;
            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            List<byte> TargetData;
            if (ConvertedData.Count != 0) TargetData = ConvertedData;
            else TargetData = Data;

            Helpers.AddPadding(ref TargetData, 4);

            string output = "u32 " + Path.GetFileNameWithoutExtension(SourceFilename.Text) + "[] = {\n";

            int column = 0;

            for (int i = 0; i < TargetData.Count - 1; i += 4)
            {

                output += "0x" + Helpers.Read32(TargetData, i).ToString("X8") + ", ";
                column++;
                if (column == 4)
                {
                    output += "\n";
                    column = 0;
                }
            }
            

            if (!output.Contains(","))
            {
                MessageBox.Show("File too small", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                output = output.Substring(0, output.LastIndexOf(","));
                output += "\n};";

                Clipboard.SetText(output);

                MessageBox.Show("C u32 array copied to the clipboard! You can paste it in your file. Size:" + TargetData.Count, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void FileToC_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.filetoC_visible = false;
        }

        private void ImageFormatComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GenerateTexture();
        }

        private void SizeLabel_Click(object sender, EventArgs e)
        {

        }

        private void FileToC_DragDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    LoadFile(files[0]);
                }

            }
        }

        private void FileToC_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
