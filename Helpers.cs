using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography;
using System.Collections;

namespace SharpOcarina
{
    public static class Helpers
    {
        public static List<string> ValidImageTypes = new List<string>(new string[] { ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".tiff", ".tif"});
        public static uint ShiftL(uint v, int s, int w)
        {
            return (uint)(((uint)v & (((uint)0x01 << w) - 1)) << s);
        }

        public static uint ShiftR(uint v, int s, int w)
        {
            return (uint)(((uint)v >> s) & (((int)0x01 << w) - 1));
        }

        public static ushort Read16(List<byte> Data, int Offset)
        {
            return (ushort)(Data[Offset] << 8 | Data[Offset + 1]);
        }

        public static short Read16S(List<byte> Data, int Offset)
        {
            return (short)(Data[Offset] << 8 | Data[Offset + 1]);
        }

        public static uint Read24(List<byte> Data, int Offset)
        {
            return (uint)(Data[Offset] << 16 | Data[Offset + 1] << 8 | Data[Offset + 2]);
        }

        public static int Read24S(List<byte> Data, int Offset)
        {
            return (int)(Data[Offset] << 16 | Data[Offset + 1] << 8 | Data[Offset + 2]);
        }

        public static int Read32S(List<byte> Data, int Offset)
        {
            return (int)(Data[Offset] << 24 | Data[Offset + 1] << 16 | Data[Offset + 2] << 8 | Data[Offset + 3]);
        }

        public static string ReadString(List<byte> data, int offset, int length)
        {
            if (offset + length > data.Count)
                throw new ArgumentException("Offset + length exceed the data size.");

            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = data[offset + i];
            }

            return Encoding.ASCII.GetString(buffer);
        }

        public static void Append16(ref List<byte> Data, ushort Value)
        {
            AppendXX(ref Data, Value, 1);
        }

        public static void Append16S(ref List<byte> Data, short Value)
        {
            AppendXX(ref Data, (ushort)Value, 1);
        }

        public static void Append32(ref List<byte> Data, uint Value)
        {


            AppendXX(ref Data, Value, 3);
        }

        public static void Append48(ref List<byte> Data, ulong Value)
        {
            Data.Add((byte)(Value >> 16));
            Append16(ref Data, (ushort)(Value & 0xFFFF));
        }

        public static void Append64(ref List<byte> Data, ulong Value)
        {
            AppendXX(ref Data, Value, 7);
        }

        private static void AppendXX(ref List<byte> Data, ulong Value, int Shifts)
        {
            for (int i = Shifts; i >= 0; --i)
            {
                byte DataByte = (byte)((Value >> (i * 8)) & 0xFF);
                Data.Add(DataByte);
            }
        }

        public static void Insert16(ref List<byte> Data, int Offset, ushort Value)
        {
            InsertXX(ref Data, Offset, Value, 1);
        }

        public static void Insert32(ref List<byte> Data, int Offset, uint Value)
        {
            InsertXX(ref Data, Offset, Value, 3);
        }

        public static void Insert64(ref List<byte> Data, int Offset, ulong Value)
        {
            InsertXX(ref Data, Offset, Value, 7);
        }

        private static void InsertXX(ref List<byte> Data, int Offset, ulong Value, int Shifts)
        {
            for (int i = Shifts; i >= 0; --i)
            {
                byte DataByte = (byte)((Value >> (i * 8)) & 0xFF);
                Data.Insert(Offset++, DataByte);
            }
        }

        public static void Overwrite16(ref List<byte> Data, int Offset, ushort Value)
        {
            OverwriteXX(ref Data, Offset, Value, 1);
        }

        public static void Overwrite32(ref List<byte> Data, int Offset, uint Value)
        {
            OverwriteXX(ref Data, Offset, Value, 3);
        }

        public static void Overwrite64(ref List<byte> Data, int Offset, ulong Value)
        {
            OverwriteXX(ref Data, Offset, Value, 7);
        }

        private static void OverwriteXX(ref List<byte> Data, int Offset, ulong Value, int Shifts)
        {
            if (Offset >= Data.Count)
            {
                AppendXX(ref Data, Value, Shifts);
            }
            else
            {
                for (int i = Shifts; i >= 0; --i)
                {
                    byte DataByte = (byte)((Value >> (i * 8)) & 0xFF);
                    Data.RemoveAt(Offset);
                    Data.Insert(Offset++, DataByte);
                }
            }
        }

        public static void GenericInject(string Filename, int Offset, byte[] Data)
        {
            GenericInject(Filename, Offset, Data, Data.Length);
        }

        public static void GenericInject(string Filename, int Offset, byte[] Data, int Length)
        {
            BinaryWriter BW = new BinaryWriter(File.OpenWrite(Filename));
            BW.Seek(Offset, SeekOrigin.Begin);
            BW.Write(new byte[Length], 0, Length);
            BW.Seek(Offset, SeekOrigin.Begin);
            BW.Write(Data);
            BW.Close();
        }

        public static double Log2(double Number)
        {
            return Math.Log(Number) / Math.Log(2);
        }

        public static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            return Regex.Replace(name, invalidReStr, "_");
        }

        public static uint Read32(List<byte> Data, int Offset)
        {
            return (uint)(Data[Offset] << 24 | Data[Offset + 1] << 16 | Data[Offset + 2] << 8 | Data[Offset + 3]);
        }

        public static ulong Read64(List<byte> Data, int Offset)
        {
            //
           // return ((ulong)(Read32(Data, Offset) << 32) | (Read32(Data, Offset + 4)));
            return (ulong)(Data[Offset] << 56 | Data[Offset + 1] << 48 | Data[Offset + 2] << 40 | Data[Offset + 3] << 32 | Data[Offset +4] << 24 | Data[Offset + 5] << 16 | Data[Offset + 6] << 8 | Data[Offset + 7]);
        }

        public static bool ReplaceLine(string search, string replace, string path, int max = 999999)
        {
            if (!File.Exists(path)) return false;

            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length && i < 999999; i++)
            {
                if (lines[i].Contains(search))
                {
                    lines[i] = replace;
                    File.WriteAllLines(path,lines);
                    return true;
                }
            }
            return false;
        }

        public static bool GetDefineBool(string search, string path, int max = 999999)
        {
            if (!File.Exists(path)) return false;

            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length && i < 999999; i++)
            {
                if (lines[i].Contains(search) && lines[i].Contains("define"))
                {
                    string trimmedline = lines[i].Trim().Replace("  ", "").Replace("# define", "#define");
                    string[] splits = trimmedline.Split(' ');
                    if (splits.Length >= 3 && splits[2].ToLower() == "true") return true;
                    else return false;
                }
            }
            return false;
        }

        public static int GetDefineInt(string search, string path, int max = 999999)
        {
            if (!File.Exists(path)) return -1;

            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length && i < 999999; i++)
            {
                if (lines[i].Contains(search) && lines[i].Contains("define"))
                {
                    string trimmedline = lines[i].Trim().Replace("  ", "").Replace("# define", "#define");
                    string[] splits = trimmedline.Split(' ');
                    if (splits.Length >= 3) return Convert.ToInt32(splits[2]);
                    else return -1;
                }
            }
            return -1;
        }

        public static bool GetDefineBoolAddIfNotExists(string search, string path, string bonus, int max = 999999)
        {
            if (!File.Exists(path)) return false;

            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length && i < 999999; i++)
            {
                if (lines[i].Contains(search) && lines[i].Contains("define"))
                {
                    string trimmedline = lines[i].Trim().Replace("  ", "").Replace("# define", "#define");
                    string[] splits = trimmedline.Split(' ');
                    if (splits.Length >= 3 && splits[2].ToLower() == "true") return true;
                    else return false;
                }
            }
            ReplaceLine("#define __ULIB_H__", "#define __ULIB_H__\n\n" + "#define " + search + " true" + "\n" + bonus,path,50);
            return true;
        }


        public static string GetDefineString(string search, string path, int max = 999999)
        {
            if (!File.Exists(path)) return "";

            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length && i < 999999; i++)
            {
                if (lines[i].Contains(search) && lines[i].Contains("define"))
                {
                    string trimmedline = lines[i].Trim().Replace("  ", "").Replace("# define", "#define");
                    string[] splits = trimmedline.Split(' ');
                    if (splits.Length >= 3) return splits[2];
                    else return "";
                }
            }
            return "";
        }

        public static List<byte> ConvertImageToData(string image, string format)
        {
            ObjFile.Material Mat = new ObjFile.Material();
            Mat.ForcedFormat = format;
            Mat.TexImage = new Bitmap(image);
            Mat.Name = Path.GetFileNameWithoutExtension(image);
            Mat.Width = Mat.TexImage.Width;
            Mat.Height = Mat.TexImage.Height;
            NTexture Texture = new NTexture();
            Texture.Convert(Mat);
            Mat.TexImage.Dispose();
            return Texture.Data.ToList();
        }

        public static List<byte> ConvertImageToData(Bitmap image, string format)
        {
            ObjFile.Material Mat = new ObjFile.Material();
            Mat.ForcedFormat = format;
            Mat.TexImage = image;
            Mat.Name = Path.GetFileNameWithoutExtension("split" + format);
            Mat.Width = Mat.TexImage.Width;
            Mat.Height = Mat.TexImage.Height;
            NTexture Texture = new NTexture();
            Texture.Convert(Mat);
            Mat.TexImage.Dispose();
            return Texture.Data.ToList();
        }

        public static string DataToC(List<byte> TargetData, string ArrayName)
        {
            
            Helpers.AddPadding(ref TargetData, 4);

            string output =  "u32 " + Path.GetFileNameWithoutExtension(ArrayName) + "[] = {\n";

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
                return "";
            }
            else
            {
                output = output.Substring(0, output.LastIndexOf(","));
                output += "\n};";

                return output;

            }

        }

        public static string DataToC64(List<byte> TargetData, string ArrayName)
        {

            Helpers.AddPadding(ref TargetData, 8);

            string output = "u64 " + (ArrayName) + "[] = {\n";

            int column = 0;

            for (int i = 0; i < TargetData.Count - 1; i += 8)
            {

                output += "0x" + Helpers.Read32(TargetData, i).ToString("X8") + Helpers.Read32(TargetData, i + 4).ToString("X8") + ", ";
                column++;
                if (column == 4)
                {
                    output += "\n";
                    column = 0;
                }
            }


            if (!output.Contains(","))
            {
                return "";
            }
            else
            {
                output = output.Substring(0, output.LastIndexOf(","));
                output += "\n};\n";

                return output;

            }

        }

        public static List<Bitmap> SplitImage(Bitmap source, int splitWidth, int splitHeight)
        {
            List<Bitmap> imageParts = new List<Bitmap>();

            int cols = source.Width / splitWidth;
            int rows = source.Height / splitHeight;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    // Create a new bitmap with the same pixel format
                    Bitmap part = new Bitmap(splitWidth, splitHeight, PixelFormat.Format32bppArgb);

                    // Lock bits for direct access
                    Rectangle rect = new Rectangle(x * splitWidth, y * splitHeight, splitWidth, splitHeight);

                    // Lock the source bitmap for reading
                    BitmapData sourceData = source.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    BitmapData targetData = part.LockBits(new Rectangle(0, 0, rect.Width, rect.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                    // Calculate the number of bytes per row
                    int bytesPerPixel = Image.GetPixelFormatSize(PixelFormat.Format32bppArgb) / 8;
                    int sourceStride = sourceData.Stride;
                    int targetStride = targetData.Stride;

                    // Allocate memory for a single row
                    byte[] rowBuffer = new byte[rect.Width * bytesPerPixel];

                    // Copy each row separately
                    for (int row = 0; row < rect.Height; row++)
                    {
                        // Read from source
                        IntPtr sourcePtr = sourceData.Scan0 + (row * sourceStride);
                        Marshal.Copy(sourcePtr, rowBuffer, 0, rowBuffer.Length);

                        // Write to target
                        IntPtr targetPtr = targetData.Scan0 + (row * targetStride);
                        Marshal.Copy(rowBuffer, 0, targetPtr, rowBuffer.Length);
                    }

                    // Unlock the bitmaps
                    source.UnlockBits(sourceData);
                    part.UnlockBits(targetData);

                    imageParts.Add(part);
                }
            }

            return imageParts;
        }

        public static void AppendString(ref List<byte> buffer, string value, int maxSize)
        {
            if (buffer == null)
                buffer = new List<byte>();

            byte[] stringBytes = Encoding.UTF8.GetBytes(value ?? string.Empty);

            int bytesToWrite = Math.Min(stringBytes.Length, maxSize);

            for (int i = 0; i < bytesToWrite; i++)
                buffer.Add(stringBytes[i]);

            for (int i = bytesToWrite; i < maxSize; i++)
                buffer.Add(0);
        }

        public static Bitmap NewBitmap(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return new Bitmap(fs);
            }
        }

        public static void SelectAdd<T>(System.Windows.Forms.NumericUpDown cur, List<T> list)
        {
            cur.Maximum = list.Count - 1;
            cur.Value = cur.Maximum;
        }

        public static void SelectClamp<T>(System.Windows.Forms.NumericUpDown cur, List<T> list)
        {
            cur.Minimum = 0;
            cur.Maximum = list.Count - 1;
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if(val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static bool IsFileLocked(string file)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenWrite(file);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

        public static void AddPadding(ref List<byte> Data, int Length)
        {
            int ToAdd = Length - (Data.Count % Length);
            if (ToAdd != Length) for (int i = 0; i < ToAdd; i++) Data.Add(0);
        }
        public static uint FloatToHex(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static void DeleteDirectory(string target_dir)
        {
            if (!Directory.Exists(target_dir)) return;
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public static WebClient DownloadTemporalFile(string website)
        {
            string tempw = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tempw\\");
            if (!Directory.Exists(tempw)) Directory.CreateDirectory(tempw);

            WebClient client = new WebClient();

            try
            {
                using (client)
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    client.DownloadFile(website, tempw + Path.GetFileName(website));

                }
            }
            catch
            {
                MessageBox.Show("Couldn't download file " + website, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return client;
        }

        public static string GetHttpJson(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "request"; // Required by GitHub
                request.Accept = "application/json";

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                DebugConsole.WriteLine("HTTP error: " + ex.Message);
                return null;
            }
        }

        public static string ExtractJsonValue(string json, string key)
        {
            int i = json.IndexOf(key);
            if (i < 0) return null;
            i = json.IndexOf(':', i);
            if (i < 0) return null;
            int start = json.IndexOf('"', i + 1);
            if (start < 0) return null;
            int end = json.IndexOf('"', start + 1);
            if (end < 0) return null;
            return json.Substring(start + 1, end - start - 1);
        }

        public static void DeleteZ64romFile(string file)
        {
            string trashdir = rom64.getPath() + "\\Trashbin";
            if (!Directory.Exists(trashdir))
                Directory.CreateDirectory(trashdir);
            string newfile = trashdir + "\\" + Path.GetFileName(file);
            if (File.Exists(newfile))
                newfile = trashdir + "\\" + Path.GetFileNameWithoutExtension(file) + "_" + DateTime.Now.Ticks.ToString() + Path.GetExtension(file);
            File.Move(file, newfile);
        }

        public static bool SameFileHash(string file1, string file2)
        {
            if (!File.Exists(file1) || !File.Exists(file2)) return false;
            var sha = SHA256.Create();

            var fs1 = File.OpenRead(file1);
            var fs2 = File.OpenRead(file2);

            var hash1 = sha.ComputeHash(fs1);
            var hash2 = sha.ComputeHash(fs2);

            fs1.Close();
            fs2.Close();

            return StructuralComparisons.StructuralEqualityComparer.Equals(hash1, hash2);
        }
    }

    public static class StringExtensionsClass
    {
        public static string Substring2(this string s, int startIndex, int length)
        {

            try
            {
                return s.Substring(startIndex, length);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return s.Substring(startIndex);
            } 
           
        }
        public static int IndexOrMax(this string s, char value)
        {

            try
            {
                return s.IndexOf(value);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return s.Length;
            }

        }
        public static int IndexOrMax(this string s, string value)
        {

            try
            {
                return s.IndexOf(value);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return s.Length;
            }

        }
        public static int LastIndexOrMax(this string s, char value)
        {

            try
            {
                return s.LastIndexOf(value);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return s.Length;
            }

        }
        public static string SubstringTill(this string s, int startIndex, char value)
        {

            try
            {
                return s.Substring(startIndex, s.IndexOrMax(value)-startIndex);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return s;
            }

        }
        public static string SubstringTill(this string s, int startIndex, string value)
        {

            try
            {
                return s.Substring(startIndex, s.IndexOrMax(value) - startIndex);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return s;
            }

        }

        

    }

    public static class DebugConsole
    {
        public static void WriteLine(string text)
        {
#if DEBUG
            Console.WriteLine(text);
#endif
        }
        public static void WriteLine(int text)
        {
#if DEBUG
            Console.WriteLine(text);
#endif
        }
    }

}
