/* 
 * NTexture.cs / Semi-intelligent N64 texture converter
 * Analyzes image to determine best possible N64 texture format (well, sort of)
 * Written in 2011 by xdaniel
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

/*
 * - Supported N64 texture formats:
 * -> RGBA 16-bit, max. 2048 byte (ex. 64x32)
 * -> CI 4-bit, max. 4096 byte (ex. 64x64)
 * -> CI 8-bit, max. 2048 byte (ex. 64x32)
 * -> IA 8-bit, max. 4096 byte (ex. 64x64)
 * -> IA 16-bit, max. 2048 byte (ex. 64x32)
 * -> I 4-bit, max. 4096 byte (ex. 128x64)
 * -> I 8-bit, max. 4096 byte (ex. 64x64)
 * -> RGBA 32-bit, max. 1024 byte (ex. 32x32)
 */

namespace SharpOcarina
{
    public class NTexture
    {
        /// <summary>
        /// Texture width in pixels
        /// </summary>
        public int Width;
        /// <summary>
        /// Texture height in pixels
        /// </summary>
        public int Height;
        /// <summary>
        /// N64-side texture type
        /// </summary>
        public byte Type;
        /// <summary>
        /// N64-side texture format
        /// </summary>
        public byte Format;
        /// <summary>
        /// N64-side texture size
        /// </summary>
        public byte Size;
        /// <summary>
        /// Converted N64 texture data
        /// </summary>
        public byte[] Data;
        /// <summary>
        /// Converted N64 palette data, if applicable
        /// </summary>
        public byte[] Palette;
        /// <summary>
        /// Address of texture in N64 memory map
        /// </summary>
        public uint TexOffset;
        /// <summary>
        /// Address of palette in N64 memory map
        /// </summary>
        public uint PalOffset;
        /// <summary>
        /// Is texture image grayscale? (set by CheckImageProperties)
        /// </summary>
        public bool IsGrayscale;
        /// <summary>
        /// Does texture image have alpha channel? (set by CheckImageProperties)
        /// </summary>
        public bool HasAlpha;

        public string Name;

        /// <summary>
        /// Find and return all unique colors of a bitmap
        /// </summary>
        /// <param name="Image">Bitmap to get colors from</param>
        /// <returns>List of unique colors</returns>
        private List<Color> GetUniqueColors(Bitmap Image)
        {
            List<Color> Colors = new List<Color>();

            for (int X = 0; X < Image.Width; ++X)
                for (int Y = 0; Y < Image.Height; ++Y)
                {
                
                    Colors.Add(Image.GetPixel(X, Y));
                }

            Colors = Colors.Distinct().ToList();
            return Colors;
        }

        private int GetUniqueColorsCount(Bitmap Image)
        {
            
            List<Color> Colors = new List<Color>();

            for (int X = 0; X < Image.Width; ++X)
                for (int Y = 0; Y < Image.Height; ++Y)
                {
                    Color c = Image.GetPixel(X, Y);
                    
                    if (c.A != 0) Colors.Add(Color.FromArgb(1, c.R, c.G, c.B));
                }

            Colors = Colors.Distinct().ToList();
            return Colors.Count;

        }

        private int GetUniqueColorsCountIA(Bitmap Image)
        {

            List<Color> Colors = new List<Color>();

            for (int X = 0; X < Image.Width; ++X)
                for (int Y = 0; Y < Image.Height; ++Y)
                {
                    Color c = Image.GetPixel(X, Y);

                    c = Color.FromArgb(c.A, (int) (Math.Round((double)(c.R / 17)) * 17), (int) (Math.Round((double)(c.G / 17)) * 17), (int) (Math.Round((double)(c.B / 17)) * 17));

                    if (c.A != 0) Colors.Add(Color.FromArgb(1, c.R, c.G, c.B));
                }

            Colors = Colors.Distinct().ToList();
            return Colors.Count;

        }

        private int GetUniqueAlphaCount(Bitmap Image)
        {
            List<Color> Colors = new List<Color>();

            for (int X = 0; X < Image.Width; ++X)
                for (int Y = 0; Y < Image.Height; ++Y)
                {
                    Color c = Image.GetPixel(X, Y);
                    Colors.Add(Color.FromArgb(c.A, 0,0,0));
                }

            Colors = Colors.Distinct().ToList();
            return Colors.Count;
        }

        /// <summary>
        /// Checks given bitmap for alpha channel and if color or grayscale
        /// </summary>
        /// <param name="Image">Bitmap to check</param>
        private void CheckImageProperties(Bitmap Image)
        {
            IsGrayscale = true;
            HasAlpha = false;

            Color Pixel;

            for (int X = 0; X < Image.Width; ++X)
                for (int Y = 0; Y < Image.Height; ++Y)
                {
                    Pixel = Image.GetPixel(X, Y);
                    if (Pixel.R != Pixel.G || Pixel.R != Pixel.B || Pixel.G != Pixel.B)
                        IsGrayscale = false;
                    if (Pixel.A != 0xFF)
                        HasAlpha = true;
                }

            Width = Image.Width;
            Height = Image.Height;
        }

        /// <summary>
        /// Checks if the texture's size is valid
        /// </summary>
        /// <returns>True or False, depending on size validity</returns>
        private bool IsSizeValid()
        {
            int[] ValidValues = new int[] { 8, 16, 32, 64, 128, 256, 512 };

            if (Array.Find(ValidValues, element => element == Width) == 0) return false;
            if (Array.Find(ValidValues, element => element == Height) == 0) return false;

            return true;
        }

        /// <summary>
        /// Converts 8-bit RGBA values into 16-bit RGBA5551 value
        /// </summary>
        /// <param name="R">8-bit Red channel</param>
        /// <param name="G">8-bit Green channel</param>
        /// <param name="B">8-bit Blue channel</param>
        /// <param name="A">8-bit Alpha channel</param>
        /// <returns>16-bit RGBA5551 value</returns>
        private ushort ToRGBA5551(byte R, byte G, byte B, byte A)
        {
            return (ushort)((((R) << 8) & 0xF800) | (((G) << 3) & 0x7C0) | (((B) >> 2) & 0x3E) | (((A) >> 7) & 0x1));
        }

        /// <summary>
        /// Generates N64 color palette from a list of colors
        /// </summary>
        /// <param name="Colors">List of color to convert</param>
        /// <param name="ColorCount">Amount of colors</param>
        /// <returns>Byte array containing N64 palette</returns>
        private byte[] GeneratePalette(List<Color> Colors, int ColorCount)
        {
            byte[] Palette = new byte[ColorCount * 2];

            List<ushort> PalEntries = new List<ushort>();
            foreach (Color Col in Colors)
                PalEntries.Add(ToRGBA5551(Col.R, Col.G, Col.B, Col.A));

            PalEntries = PalEntries.Distinct().ToList();
            for (int i = 0, j = 0; i < PalEntries.Count; i++, j += 2)
            {
                Palette[j] = (byte)(PalEntries[i] >> 8);
                Palette[j + 1] = (byte)(PalEntries[i] & 0xFF);
            }

            return Palette;
        }

        /// <summary>
        /// Find 16-bit RGBA5551 value in N64 color palette
        /// </summary>
        /// <param name="Palette">Byte array with N64 palette</param>
        /// <param name="RGBA5551">16-bit RGBA5551 value to search</param>
        /// <returns>Offset of color in N64 palette</returns>
        private int GetPaletteIndex(byte[] Palette, ushort RGBA5551)
        {
            for (int i = 0; i < Palette.Length; i += 2)
            {
                if (RGBA5551 == (ushort)((Palette[i] << 8) | (Palette[i + 1]))) return (i / 2);
            }

            return -1;
        }

        /// <summary>
        /// Converts given ObjFile.Material into N64 texture
        /// </summary>
        /// <param name="Material">Material to convert</param>
        public void Convert(ObjFile.Material Material)
        {
            try
            {
                BitmapData RawBmp = null;
                byte[] Raw = null;

                IsGrayscale = false;
                HasAlpha = false;

                CheckImageProperties(Material.TexImage);
                //TODO
                if (IsSizeValid() == false)
                {
                    MessageBox.Show(string.Format("Invalid texture size {0}x{1}, converting to RGB format meanwhile", Width, Height), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Material.ForceRGBA = true;
                }
                try
                {
                    RawBmp = Material.TexImage.LockBits(
                        new Rectangle(0, 0, (int)Material.Width, (int)Material.Height),
                        ImageLockMode.ReadOnly,
                        PixelFormat.Format32bppArgb
                    );

                    int TotalSize = RawBmp.Height * RawBmp.Stride;
                    Raw = new byte[TotalSize];

                    System.Runtime.InteropServices.Marshal.Copy(RawBmp.Scan0, Raw, 0, TotalSize);
                }
                finally
                {
                    if (RawBmp != null)
                        Material.TexImage.UnlockBits(RawBmp);
                }

                //throw new Exception("Too many grayshades in texture OR invalid size");
                List<Color> UniqueColors = GetUniqueColors(Material.TexImage);

                int ColorCount = GetUniqueColorsCount(Material.TexImage);
                int AlphaCount = GetUniqueAlphaCount(Material.TexImage);
               // int ColorCountIA = GetUniqueColorsCountIA(Material.TexImage);

               if (Material.ForcedFormat != "")
               {
                    ConvertTexture(Material.ForcedFormat, Material, Raw, UniqueColors);
               }
               else if (IsGrayscale == true && Material.ForceRGBA == false && HasAlpha == true)
                {
                
                        /* Convert to IA */
                        if (ColorCount <= 64 && AlphaCount <= 16 && (Material.Width*Material.Height) <= 4096) 
                        {
#if DEBUG
                            Console.WriteLine("IA 8-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString() + ", " + UniqueColors.Count.ToString() + " grayshades");
#endif

                            ConvertTexture("IA8", Material, Raw);


                            
                        }
                        else if (ColorCount <= 256 && Material.Width * Material.Height <= 2048)
                        {
#if DEBUG
                            Console.WriteLine("IA 16-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString() + ", " + UniqueColors.Count.ToString() + " grayshades");
#endif

                            ConvertTexture("IA16", Material, Raw);

                           
                        }
                        else if (UniqueColors.Count <= 256 && Material.Width * Material.Height <= 4096)
                        {
#if DEBUG
                            Console.WriteLine("I 8-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString() + ", " + UniqueColors.Count.ToString() + " grayshades");
#endif

                            ConvertTexture("I8", Material, Raw);


                            
                        }
                        /* Convert to I */
                        else if (UniqueColors.Count <= 16)
                        {
    #if DEBUG
                            Console.WriteLine("I 4-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString() + ", " + UniqueColors.Count.ToString() + " grayshades");
    #endif

                            ConvertTexture("I4", Material, Raw);

                        }
                        else
                            {
                                /* Uh-oh, too many grayshades OR invalid size! */
                                throw new Exception("Too many grayshades in texture OR invalid size");
                            }
                    

                }
                else
                {
                    /* Convert to CI */
                    if (UniqueColors.Count <= 16 && Material.Width * Material.Height <= 4096 && Material.ForceRGBA == false)
                    {
#if DEBUG
                        Console.WriteLine("CI 4-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString() + ", " + UniqueColors.Count.ToString() + " unique colors");
#endif

                        ConvertTexture("CI4", Material, Raw, UniqueColors);

 
                    //    Console.WriteLine("Size: " + Data.Length);
                     //   Console.WriteLine(Data[Data.Length-1]);
                    }
                    else if (UniqueColors.Count <= 256 && Material.Width * Material.Height <= 2048 && Material.ForceRGBA == false)
                    {
#if DEBUG
                        Console.WriteLine("CI 8-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString() + ", " + UniqueColors.Count.ToString() + " unique colors");
#endif
                        ConvertTexture("CI8", Material, Raw, UniqueColors);


                    }
                    else if (AlphaCount > 2 && Material.Width * Material.Height <= 1024 && Material.ForceRGBA == false && !MainForm.settings.DisableRGBA32)
                    {
                        /* Convert to RGBA */
#if DEBUG
                        Console.WriteLine("RGBA 32-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString());
#endif
                        ConvertTexture("RGBA32", Material, Raw);


                    }
                    else if (UniqueColors.Count <= 16 && IsGrayscale == true && Material.ForceRGBA == false)
                    {
#if DEBUG
                        Console.WriteLine("I 4-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString() + ", " + UniqueColors.Count.ToString() + " grayshades");
#endif
                        ConvertTexture("I4",Material, Raw);
                    }
                    else if (UniqueColors.Count <= 256 && IsGrayscale == true && Material.Width * Material.Height <= 4096 && Material.ForceRGBA == false)
                    {
#if DEBUG
                        Console.WriteLine("I 8-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString() + ", " + UniqueColors.Count.ToString() + " grayshades");
#endif
                        ConvertTexture("I8", Material, Raw);
                    }
                    else
                    {
                        /* Convert to RGBA */
#if DEBUG
                        Console.WriteLine("RGBA 16-bit <- " + Material.Name + ", " + Material.Width.ToString() + "*" + Material.Height.ToString());
#endif
                       ConvertTexture("RGBA16", Material, Raw);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Material '" + Material.DisplayName + "': " + ex.Message, "Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                SetInvalidTexture(Material);
            }

            /* Pack texture type */
            Type = (byte)((Format << 5) | (Size << 3));
        }

        private void ConvertTexture(string type, ObjFile.Material Material, byte[] Raw, List<Color> UniqueColors = null)
        {
            if (type == "RGBA16" || type == "")
            {
                /* Set type, RGBA 16-bit */
                Format = GBI.G_IM_FMT_RGBA;
                Size = GBI.G_IM_SIZ_16b;

                /* Generate texture buffer */
                Data = new byte[Material.Width * Material.Height * 2];
                Palette = null;

                /* Loop through pixels, convert to RGBA5551, write to texture buffer */
                for (int i = 0, j = 0; i < Raw.Length; i += 4, j += 2)
                {
                    ushort RGBA5551 = ToRGBA5551(Raw[i + 2], Raw[i + 1], Raw[i], Raw[i + 3]);
                    Data[j] = (byte)(RGBA5551 >> 8);
                    Data[j + 1] = (byte)(RGBA5551 & 0xFF);
                }
            }
            else if (type == "RGBA32")
            {
                /* Set type, RGBA 32-bit */
                Format = GBI.G_IM_FMT_RGBA;
                Size = GBI.G_IM_SIZ_32b;

                /* Generate texture buffer */
                Data = new byte[Material.Width * Material.Height * 4];
                Palette = null;

                /* Loop through pixels, convert to RGBA32, write to texture buffer */
                for (int i = 0, j = 0; i < Raw.Length; i += 4, j += 4)
                {
                    Data[j] = Raw[i + 2];
                    Data[j + 1] = Raw[i + 1];
                    Data[j + 2] = Raw[i];
                    Data[j + 3] = Raw[i + 3];
                }
            }
            else if (type == "IA8")
            {
                /* Set type, IA 8-bit */
                Format = GBI.G_IM_FMT_IA;
                Size = GBI.G_IM_SIZ_8b;

                /* Generate texture buffer */
                Data = new byte[Material.Width * Material.Height];
                Palette = null;

                /* Loop through pixels, convert to IA 8-bit, write to texture buffer */
                for (int i = 0, j = 0; i < Raw.Length; i += 4, j++)
                {
                    Data[j] = (byte)(((Raw[i] / 16) << 4) | ((Raw[i + 3] / 16) & 0xF));
                }
            }
            else if (type == "IA16")
            {
                /* Set type, IA 16-bit */
                Format = GBI.G_IM_FMT_IA;
                Size = GBI.G_IM_SIZ_16b;

                /* Generate texture buffer */
                Data = new byte[Material.Width * Material.Height * 2];
                Palette = null;

                /* Loop through pixels, convert to IA 16-bit, write to texture buffer */
                for (int i = 0, j = 0; i < Raw.Length; i += 4, j += 2)
                {
                    Data[j] = Raw[i + 2];
                    Data[j + 1] = Raw[i + 3];
                }
            }
            else if (type == "I4")
            {
                /* Set type, I 4-bit */
                Format = GBI.G_IM_FMT_I;
                Size = GBI.G_IM_SIZ_4b;

                /* Generate texture buffer */
                Data = new byte[(Material.Width * Material.Height) / 2];
                Palette = null;

                /* Loop through pixels, convert to I 4-bit, write to texture buffer */
                for (int i = 0, j = 0; i < Raw.Length; i += 8, j++)
                {
                    Data[j] = (byte)(((Raw[i] / 16) << 4) | ((Raw[i + 4] / 16) & 0xF));
                }
            }
            else if (type == "I8")
            {
                /* Set type, I 8-bit */
                Format = GBI.G_IM_FMT_I;
                Size = GBI.G_IM_SIZ_8b;

                /* Generate texture buffer */
                Data = new byte[Material.Width * Material.Height];
                Palette = null;

                /* Loop through pixels, convert to I 8-bit, write to texture buffer */
                for (int i = 0, j = 0; i < Raw.Length; i += 4, j++)
                {
                    Data[j] = Raw[i];
                }
            }
            else if (type == "CI4")
            {
                /* Set type, CI 4-bit */
                Format = GBI.G_IM_FMT_CI;
                Size = GBI.G_IM_SIZ_4b;

                /* Generate texture buffer */
                Data = new byte[(Material.Width * Material.Height) / 2];

                /* Generate 16-color RGBA5551 palette */
                Palette = GeneratePalette(UniqueColors, 16);

                /* Loop through pixels, get palette indexes, write to texture buffer */
                for (int i = 0, j = 0; i < Raw.Length; i += 8, j++)
                {

                    ushort RGBA5551_1 = ToRGBA5551(Raw[i + 2], Raw[i + 1], Raw[i], Raw[i + 3]);
                    ushort RGBA5551_2 = ToRGBA5551(Raw[i + 6], Raw[i + 5], Raw[i + 4], Raw[i + 7]);
                    byte Value = (byte)(
                        ((GetPaletteIndex(Palette, RGBA5551_1)) << 4) |
                        ((GetPaletteIndex(Palette, RGBA5551_2) & 0xF)));

                    Data[j] = Value;
                }
            }
            else if (type == "CI8")
            {
                /* Set type, CI 8-bit */
                Format = GBI.G_IM_FMT_CI;
                Size = GBI.G_IM_SIZ_8b;

                /* Generate texture buffer */
                Data = new byte[Material.Width * Material.Height];

                /* Generate 256-color RGBA5551 palette */
                Palette = GeneratePalette(UniqueColors, 256);

                /* Loop through pixels, get palette indexes, write to texture buffer */
                for (int i = 0, j = 0; i < Raw.Length; i += 4, j++)
                {
                    ushort RGBA5551 = ToRGBA5551(Raw[i + 2], Raw[i + 1], Raw[i], Raw[i + 3]);
                    Data[j] = (byte)GetPaletteIndex(Palette, RGBA5551);
                }
            }
        }

        /// <summary>
        /// Creates dummy texture
        /// </summary>
        /// <param name="Material">Material to use for texture parameters</param>
        private void SetInvalidTexture(ObjFile.Material Material)
        {
            Format = GBI.G_IM_FMT_RGBA;
            Size = GBI.G_IM_SIZ_16b;

            Data = new byte[Material.Width * Material.Height * 2];
            Palette = null;
        }
    }
}
