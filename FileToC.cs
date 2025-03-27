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

            if (Helpers.ValidImageTypes.IndexOf(Path.GetExtension(SourceFilename.Text).ToLowerInvariant()) != -1)
            {
                ImageFormatComboBox.Enabled = ImageFormatLabel.Enabled = true;
                if (ImageFormatComboBox.SelectedIndex == -1) ImageFormatComboBox.SelectedIndex = 0;
                GenerateTexture();
                SplitCheckBox.Enabled = true;

            }
            else
            {
                ImageFormatComboBox.Enabled = ImageFormatLabel.Enabled = false;
                TextureBox.Image = null;
                ConvertedData = new List<byte>();
                SplitCheckBox.Enabled = false;
                SplitCheckBox.Checked = false;
            }

            SaveButton.Enabled = true;
            SizeLabel.Visible = true;
            SizeLabel.Text = "Size: " + Data.Count + " bytes";
        }



        private void GenerateTexture()
        {
            
            ObjFile.Material Mat = new ObjFile.Material();
            Mat.ForcedFormat = ImageFormatComboBox.Text;
            Mat.TexImage = Helpers.NewBitmap(SourceFilename.Text);
            Mat.Name = Path.GetFileNameWithoutExtension(SourceFilename.Text);
            Mat.Width = Mat.TexImage.Width;
            Mat.Height = Mat.TexImage.Height;
            NTexture Texture = new NTexture();
            Texture.Convert(Mat);
            ConvertedData = Texture.Data.ToList();

            ConvertedData = Helpers.ConvertImageToData(SourceFilename.Text, ImageFormatComboBox.Text);
            
            uint BufferSize = (uint) (Mat.Width * Mat.Height * 4);
            byte[] Result = new byte[BufferSize];
            Result.Fill(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
            switch (ImageFormatComboBox.Text)
            {
                default:
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
            string dir = Path.GetDirectoryName(SourceFilename.Text) + "\\";
            string filename = Path.GetFileNameWithoutExtension(SourceFilename.Text);
            string extensions = Path.GetExtension(SourceFilename.Text);
            if (filename.IndexOf('.') != -1)
            {
                filename = filename.SubstringTill(0, ".");
                string tmp = Path.GetFileName(SourceFilename.Text);
                extensions = tmp.Substring(tmp.IndexOf('.'));
            }
            string output = "";
            List<List<byte>> Files = new List<List<byte>>();
            if (SplitCheckBox.Checked && Helpers.ValidImageTypes.IndexOf(Path.GetExtension(SourceFilename.Text).ToLowerInvariant()) != -1)
            {
                Bitmap baseimage = Helpers.NewBitmap(SourceFilename.Text);
                if (baseimage.Width % SplitWidth.Value != 0 || baseimage.Height % SplitHeight.Value != 0)
                {
                    MessageBox.Show("The image has to be divisable by the split amount!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    List<Bitmap> splits = Helpers.SplitImage(baseimage, (int)SplitWidth.Value, (int)SplitHeight.Value);
                    foreach (Bitmap split in splits)
                    {
                        Files.Add(Helpers.ConvertImageToData(split, ImageFormatComboBox.Text));

                    }
                }
            }
            else
            {
                Files.Add(TargetData);

            }

            if (!ConvertToInclude.Checked)
            {
                int c = 0;
                foreach(List<byte> file in Files)
                {
                    string suffix = Files.Count > 1 ? "_" + c : "";
                    output += Helpers.DataToC64(file,Path.GetFileNameWithoutExtension(SourceFilename.Text) + suffix);
                    c++;
                }

                if (output == "")
                {
                    MessageBox.Show("File too small", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Clipboard.SetText(output);

                    MessageBox.Show("C u64 array copied to the clipboard! You can paste it in your file. Size:" + TargetData.Count, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                int c = 0;
                foreach (List<byte> file in Files)
                {
                    List<byte> data = file;
                    Helpers.AddPadding(ref data, 8);
                    string suffix = Files.Count > 1 ? "_" + c : "";
                    string newfile = SourceFilename.Text;
                    output = FaroresPlugin.ConvertFileToInc(data);
                    string binfile = dir + filename + suffix + extensions.Replace(Path.GetExtension(SourceFilename.Text), ".inc"); 
                    //string binfile = newfile.Replace(Path.GetExtension(SourceFilename.Text), suffix+".inc");
                    File.WriteAllText(binfile, output);
                    DebugConsole.WriteLine(binfile);
                    c++;
                }
                MessageBox.Show("Saved as " + SourceFilename.Text.Replace(Path.GetExtension(SourceFilename.Text), ".inc"), "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void SplitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SplitGroupBox.Enabled = SplitCheckBox.Checked;
        }

        private void SplitWidth_ValueChanged(object sender, EventArgs e)
        {
            if (SplitWidth.Value % 2 != 0)
            {
                SplitWidth.Value++;
            }
        }

        private void SplitHeight_ValueChanged(object sender, EventArgs e)
        {
            if (SplitHeight.Value % 2 != 0)
            {
                SplitHeight.Value++;
            }
        }
    }
}
