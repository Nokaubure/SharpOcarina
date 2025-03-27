#region --- About ---
/*
 * Project SayakaGL
 */
#endregion

#region --- Using Directives ---
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics;

using SharpOcarina.SayakaGL;
#endregion

namespace NImage
{
    public static class NImageUtil
    {
        #region RGBA

        public static void RGBA16(uint Width, uint Height, uint LineSize, byte[] Source, uint SourceOffset, ref byte[] Target)
        {
            int TargetOffset = 0;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    if (SourceOffset + 1 >= Source.Length) break;

                    UInt16 Raw = (UInt16)((Source[SourceOffset] << 8) | Source[SourceOffset + 1]);
                    Target[TargetOffset] = (byte)((Raw & 0xF800) >> 8);
                    Target[TargetOffset + 1] = (byte)(((Raw & 0x07C0) << 5) >> 8);
                    Target[TargetOffset + 2] = (byte)(((Raw & 0x003E) << 18) >> 16);
                    Target[TargetOffset + 3] = 0;
                    if ((Raw & 0x0001) == 1) Target[TargetOffset + 3] = 0xFF;

                    SourceOffset += 2;
                    TargetOffset += 4;

                    
                }
                SourceOffset += LineSize * 4 - Width;
            }
        }

        public static void RGBA32(byte[] Source, uint SourceOffset, ref byte[] Target)
        {
            Buffer.BlockCopy(Source, (int)SourceOffset, Target, 0, (int)Target.Length);
        }

        #endregion

        #region CI

        public static void CI4(uint Width, uint Height, uint LineSize, byte[] Source, uint SourceOffset, ref byte[] Target, Color4[] Palette)
        {
            int TargetOffset = 0;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    byte CIIndex = (byte)(Source[SourceOffset]);

                    Target[TargetOffset] = (byte)Palette[CIIndex].R;
                    Target[TargetOffset + 1] = (byte)Palette[CIIndex].G;
                    Target[TargetOffset + 2] = (byte)Palette[CIIndex].B;
                    Target[TargetOffset + 3] = (byte)Palette[CIIndex].A;

                    SourceOffset++;
                    TargetOffset += 4;

                    if (SourceOffset >= Source.Length) break;
                }
                SourceOffset += LineSize * 8 - Width;
            }
        }

        public static void CI8(uint Width, uint Height, uint LineSize, byte[] Source, uint SourceOffset, ref byte[] Target, Color4[] Palette)
        {
            int TargetOffset = 0;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width / 2; i++)
                {
                    byte CIIndex1 = (byte)((Source[SourceOffset] & 0xF0) >> 4);
                    byte CIIndex2 = (byte)(Source[SourceOffset] & 0x0F);

                    Target[TargetOffset] = (byte)Palette[CIIndex1].R;
                    Target[TargetOffset + 1] = (byte)Palette[CIIndex1].G;
                    Target[TargetOffset + 2] = (byte)Palette[CIIndex1].B;
                    Target[TargetOffset + 3] = (byte)Palette[CIIndex1].A;

                    Target[TargetOffset + 4] = (byte)Palette[CIIndex2].R;
                    Target[TargetOffset + 5] = (byte)Palette[CIIndex2].G;
                    Target[TargetOffset + 6] = (byte)Palette[CIIndex2].B;
                    Target[TargetOffset + 7] = (byte)Palette[CIIndex2].A;

                    SourceOffset++;
                    TargetOffset += 8;

                    if (SourceOffset >= Source.Length) break;
                }
                SourceOffset += LineSize * 8 - (Width / 2);
            }
        }

        #endregion

        #region IA

        public static void IA4(uint Width, uint Height, float LineSize, byte[] Source, uint SourceOffset, ref byte[] Target)
        {
            int TargetOffset = 0;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width / 2; i++)
                {
                    byte Raw = (byte)((Source[SourceOffset] & 0xF0) >> 4);
                    Target[TargetOffset] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 1] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 2] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 3] = 0;
                    if ((Raw & 0x0001) == 1) Target[TargetOffset + 3] = 0xFF;

                    Raw = (byte)(Source[SourceOffset] & 0x0F);
                    Target[TargetOffset + 4] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 5] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 6] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 7] = 0;
                    if ((Raw & 0x0001) == 1) Target[TargetOffset + 7] = 0xFF;

                    SourceOffset++;
                    TargetOffset += 8;

                    if (SourceOffset >= Source.Length) break;
                }
                SourceOffset += (uint)(LineSize * 8) - (Width / 2);
            }
        }

        public static void IA8(uint Width, uint Height, float LineSize, byte[] Source, uint SourceOffset, ref byte[] Target)
        {
            int TargetOffset = 0;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    byte Raw = (byte)(Source[SourceOffset]);
                    Target[TargetOffset] = (byte)((Raw & 0xF0) + 0x0F);
                    Target[TargetOffset + 1] = (byte)((Raw & 0xF0) + 0x0F);
                    Target[TargetOffset + 2] = (byte)((Raw & 0xF0) + 0x0F);
                    Target[TargetOffset + 3] = (byte)((Raw & 0x0F) << 4);

                    SourceOffset++;
                    TargetOffset += 4;

                    if (SourceOffset >= Source.Length) break;
                }
                SourceOffset += (uint)(LineSize * 8) - Width;
            }
        }

        public static void IA16(uint Width, uint Height, float LineSize, byte[] Source, uint SourceOffset, ref byte[] Target)
        {
            int TargetOffset = 0;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    Target[TargetOffset] = Source[SourceOffset];
                    Target[TargetOffset + 1] = Source[SourceOffset];
                    Target[TargetOffset + 2] = Source[SourceOffset];
                    Target[TargetOffset + 3] = Source[SourceOffset + 1];

                    SourceOffset += 2;
                    TargetOffset += 4;

                    if (SourceOffset >= Source.Length) break;
                }
                SourceOffset += (uint)(LineSize * 4) - Width;
            }
        }

        #endregion

        #region I

        public static void I4(uint Width, uint Height, float LineSize, byte[] Source, uint SourceOffset, ref byte[] Target)
        {
            int TargetOffset = 0;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width / 2; i++)
                {
                    byte Raw = (byte)((Source[SourceOffset] & 0xF0) >> 4);
                    Target[TargetOffset] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 1] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 2] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 3] = 0xFF;

                    Raw = (byte)(Source[SourceOffset] & 0x0F);
                    Target[TargetOffset + 4] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 5] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 6] = (byte)((Raw & 0x0E) << 4);
                    Target[TargetOffset + 7] = 0xFF;

                    SourceOffset++;
                    TargetOffset += 8;

                    if (SourceOffset >= Source.Length) break;
                }
                SourceOffset += (uint)(LineSize * 8) - (Width / 2);
            }
        }

        public static void I8(uint Width, uint Height, float LineSize, byte[] Source, uint SourceOffset, ref byte[] Target)
        {
            int TargetOffset = 0;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    Target[TargetOffset] = Source[SourceOffset];
                    Target[TargetOffset + 1] = Source[SourceOffset];
                    Target[TargetOffset + 2] = Source[SourceOffset];
                    Target[TargetOffset + 3] = 0xFF;

                    SourceOffset++;
                    TargetOffset += 4;

                    if (SourceOffset >= Source.Length) break;
                }
                SourceOffset += (uint)(LineSize * 8) - Width;
            }
        }

        #endregion

        #region Main Function

        public static void ConvertTexture(uint Format, byte[] Source, uint SourceOffset, ref byte[] Target, uint Width, uint Height, uint LineSize, Color4[] Palette)
        {
           // DebugConsole.WriteLine("Texture converted");
            try
            {
                // Default -> white texture
                Target.Fill(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

                switch (Format)
                {
                    case 0x00:
                    case 0x08:
                    case 0x10:
                        RGBA16(Width, Height, LineSize, Source, SourceOffset, ref Target);
                        break;
                    case 0x18:
                        RGBA32(Source, SourceOffset, ref Target);
                        break;
                    case 0x40:
                    case 0x50:
                        CI8(Width, Height, LineSize, Source, SourceOffset, ref Target, Palette);
                        break;
                    case 0x48:
                        CI4(Width, Height, LineSize, Source, SourceOffset, ref Target, Palette);
                        break;
                    case 0x60:
                        IA4(Width, Height, LineSize, Source, SourceOffset, ref Target);
                        break;
                    case 0x68:
                        IA8(Width, Height, LineSize, Source, SourceOffset, ref Target);
                        break;
                    case 0x70:
                        IA16(Width, Height, LineSize, Source, SourceOffset, ref Target);
                        break;
                    case 0x80:
                    case 0x90:
                        I4(Width, Height, LineSize, Source, SourceOffset, ref Target);
                        break;
                    case 0x88:
                        I8(Width, Height, LineSize, Source, SourceOffset, ref Target);
                        break;
                    default:
                        // Unknown format -> blue texture
                        Target.Fill(new byte[] { 0x00, 0x00, 0xFF, 0xFF });
                        break;
                }
            }
            catch (Exception ea)
            {
                System.Windows.Forms.MessageBox.Show("Could not convert texture image!\n\nDebug information:\nFunction " + ea.TargetSite + "\nMessage: " + ea.Message, "Texture Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                // Conversion error -> red texture
                Target.Fill(new byte[] { 0xFF, 0x00, 0x00, 0xFF });
            }
        }

        #endregion
    }

    #region Extensions

    public static class ArrayExtensions
    {
        public static void Init<T>(this T[] array, T defaultValue)
        {
            if (array == null)
                return;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = defaultValue;
            }
        }

        public static void Fill<T>(this T[] array, T[] data)
        {
            if (array == null)
                return;

            for (int i = 0; i < array.Length; i += data.Length)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    try
                    {
                        array[i + j] = data[j];
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }
    }

    #endregion
}
