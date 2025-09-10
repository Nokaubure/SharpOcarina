using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tommy;

namespace SharpOcarina
{
    public static class FaroresPlugin
    {
        public static string[] imageFormats = { "RGBA16", "RGBA32", "I4", "I8", "IA4", "IA8", "IA16", "CI4", "CI8" };

        public static string AddLinkAnimations(string basedir,bool silent)
        {
            string path = basedir + Path.DirectorySeparatorChar + @"src\system\link_animation\";
            string headerfile = "#include <uLib.h>\n\n";
            //string dmatoml = "[0x20]\r\n\tfile = \"src/NewLinkAnims.bin\"\r\n\tcompress = false";
            int filesizestack = 0;
            List<byte> FullData = new List<byte>();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                if (!silent) MessageBox.Show("Usage: With objex2 plugin, export your animation BINs inside " + path + " with the following name: XX-Name.bin where XX is the number of frames the animation has.", "Usage", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "Tutorial";
            }
            DirectoryInfo sourcedir = new DirectoryInfo(path);
            FileInfo[] files = sourcedir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (!file.Name.Contains("-"))
                {
                    MessageBox.Show("Error in file " + file + "\nUsage: With objex2 plugin, export your animation BINs inside " + path + " with the following name: XX-Name.bin where XX is the number of frames the animation has.", "Usage", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "Error!";
                }
                string[] split = file.Name.Split('-');
                int frames = 0;
                if (!Int32.TryParse(split[0], out frames))
                {
                    MessageBox.Show("Error in file " + file + "\nUsage: With objex2 plugin, export your animation BINs inside " + path + " with the following name: XX-Name.bin where XX is the number of frames the animation has.", "Usage", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "Error!";
                }
                int size = (int)file.Length;
                headerfile += "LinkAnimationHeader gPlayerAnim_" + file.Name.Substring(file.Name.IndexOf('-') + 1).Replace(".bin", "") +
                              " = { { " + frames + " }, (void*)0x17" + filesizestack.ToString("X6") + " }; \n";

                FullData.AddRange(File.ReadAllBytes(file.FullName));
                Helpers.AddPadding(ref FullData, 0x10);
                filesizestack = FullData.Count;


            }
            string newanimheaderpath = basedir + @"\src\system\kaleido\0x01-Player\NewAnimHeader.h";
            string newlinkanimspath = basedir + @"\rom\NewLinkAnims.bin";
            File.WriteAllText(newanimheaderpath, headerfile);
            File.WriteAllBytes(newlinkanimspath, FullData.ToArray());

            string libcodepatch = basedir + @"\src\lib_code\!std\dma\AnimationContext_SetLoadFrame.c";
            File.Delete(libcodepatch);


            string playerpath = basedir + @"\src\system\kaleido\0x01-Player\Player.c";
            string[] lines = File.ReadAllLines(playerpath);
            bool includeexists = false;
            for (int i = 0; i < 30 && i < lines.Length; i++)
            {

                if (lines[i].Contains("#include \"NewAnimHeader.h\""))
                {
                    includeexists = true;
                    break;
                }
            }

            if (!includeexists)
            {
                Helpers.ReplaceLine("#include \"Player.h\"", "#include \"Player.h\"\n#include \"NewAnimHeader.h\"", playerpath);
            }

            string newfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\NewLinkAnims.c");
            string oldfile = basedir + @"\src\lib_user\library\NewLinkAnims.c";

            for (int i = 0; i < files.Length; i++)
            {
                if (File.Exists(oldfile))
                    File.Delete(oldfile);
                File.Copy(newfile, oldfile);
            }

            //DMA entry
            int maxKey = AddDMAEntry(basedir, 0x1F, "rom/NewLinkAnims.bin", false);

            if (maxKey != 0x20)
            {
                Helpers.ReplaceLine("#define NEWLINKANIMS_DMAID", "#define NEWLINKANIMS_DMAID 0x" + maxKey.ToString("X2"), oldfile);
            }
            return "";
        }

        public static string ConvertAllIncPngFiles(string basedir)
        {
            List<string> pngFiles = Directory.GetFiles(basedir + "\\src\\", "*.*", SearchOption.AllDirectories).Where(file => Helpers.ValidImageTypes.Contains(Path.GetExtension(file).ToLower())).ToList();
            pngFiles.AddRange(Directory.GetFiles(basedir + "\\include\\", "*.*", SearchOption.AllDirectories).Where(file => Helpers.ValidImageTypes.Contains(Path.GetExtension(file).ToLower())).ToList());
            //pngFiles.AddRange(Directory.GetFiles(basedir + "\\patch\\", "*.*", SearchOption.AllDirectories).Where(file => Helpers.ValidImageTypes.Contains(Path.GetExtension(file).ToLower())).ToList());
            //            pngFiles.AddRange(Directory.GetFiles(basedir + "\\patch\\", "*.*.png", SearchOption.AllDirectories).ToList());
            int imagesConverted = 0;

            foreach (string file in pngFiles)
            {
                if (file.Contains("\\src\\system\\DMA\\")) continue;
                if (file.Contains("\\src\\object\\")) continue;
                string[] tmpsplits = file.Split('.');
                string format = tmpsplits[tmpsplits.Length - 2].ToUpper(); 
                string dir = Path.GetDirectoryName(file) + "\\";
                string filename = Path.GetFileNameWithoutExtension(file);
                string extensions = Path.GetExtension(file);
                string replace = "";
                if (filename.IndexOf('.') != -1)
                {
                    filename = filename.SubstringTill(0, ".");
                    string tmp = Path.GetFileName(file);
                    extensions = tmp.Substring(tmp.IndexOf('.'));
                }
                if (imageFormats.Contains(format))
                {
                    List<List<byte>> Files = new List<List<byte>>();
                    bool IsSplit = false;
                    string extra = file.ToLower().Contains("#split") ? "_0" : "";
                    string binfile = dir+filename+extra+extensions.Replace(format, ".inc");
                    FileInfo pnginfo = new FileInfo(file);
                    FileInfo bininfo = File.Exists(binfile) ? new FileInfo(binfile) : null;
                    if (bininfo == null || (pnginfo.LastWriteTime >= bininfo.LastWriteTime) || (pnginfo.CreationTime > bininfo.CreationTime))
                    {
                        Files = ConvertFileToByteList(file,format,out replace);

                        int c = 0;
                        foreach (List<byte> filee in Files)
                        {
                            List<byte> imagedata = filee;
                            Helpers.AddPadding(ref imagedata, 8);
                            string output = ConvertFileToInc(imagedata);
                            string suffix = Files.Count > 1 ? "_" + c : "";
                            binfile = dir + filename + suffix + extensions.Replace(Path.GetExtension(file), ".inc");
                            binfile = Regex.Replace(binfile,replace, "",RegexOptions.IgnoreCase);
                            File.WriteAllText(binfile, output);
                            File.SetCreationTime(binfile, DateTime.Now);
                            DebugConsole.WriteLine(binfile);
                            c++;
                        }

                        imagesConverted++;
                    }
                }
            }

            return "Done! images converted: " + imagesConverted;
        }
        public static List<List<byte>> ConvertFileToByteList(string file, string format, out string replace)
        {
            List<List<byte>> Files = new List<List<byte>>();
            bool IsSplit = false;
            int width = 0;
            int height = 0;
            replace = "";
            if (file.ToLower().Contains("#split"))
            {

                string tmp = file.Substring(file.ToLower().IndexOf("#split") + 6);
                if (tmp.Contains(".")) tmp = tmp.SubstringTill(0, '.');
                if (tmp.ToLower().Contains("x"))
                {
                    string[] tmpnum = tmp.ToLower().Split('x');
                    if (tmpnum.Length == 2)
                    {
                        if (!Int32.TryParse(tmpnum[0], out width) || !Int32.TryParse(tmpnum[1], out height))
                        {
                            MessageBox.Show("Bad usage of Split tag. It should be #SplitWWxHH (WW = width, HH = height) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return Files; 
                        }
                        else
                        {
                            IsSplit = true;
                            replace = "#split" + width + "x" + height;
                        }
                    }
                }
            }
            if (IsSplit && width != 0 && height != 0)
            {
                Bitmap baseimage = Helpers.NewBitmap(file);
                if (baseimage.Width % width != 0 || baseimage.Height % height != 0)
                {
                    MessageBox.Show("The image has to be divisable by the split amount!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return Files;
                }
                else
                {
                    List<Bitmap> splits = Helpers.SplitImage(baseimage, width, height);
                    foreach (Bitmap split in splits)
                    {
                        Files.Add(Helpers.ConvertImageToData(split, format));
                        split.Dispose();

                    }
                }
                baseimage.Dispose();
            }
            else
            {
                Files.Add(Helpers.ConvertImageToData(file, format));
            }


            return Files;
        }
        public static string ConvertFileToInc(List<byte> data)
        {
            string output = "";
            try
            {
                int column = 0;

                for (int i = 0; i < data.Count - 1; i += 8)
                {

                    output += "0x" + Helpers.Read32(data, i).ToString("X8") + Helpers.Read32(data, i + 4).ToString("X8") + ", ";
                    column++;
                    if (column == 4)
                    {
                        output += "\n";
                        column = 0;
                    }
                }
            }
            catch (Exception e)
            {
                output = "0x0000000000000000";
                DebugConsole.WriteLine("Error when converting image ");
            }
            return output;
        }
        public static string CustomDMAEntries(string basedir, bool silent)
        {
            string path = basedir + "\\src\\system\\DMA\\";
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                if (!silent) MessageBox.Show("Usage: Create folders inside \\src\\system\\DMA\\ with .format.png files inside, such as .i8.png (or other image formats)", "Usage", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "Tutorial";
            }
            else
            {
                List<string> directories = Directory.GetDirectories(path).ToList();
                int DMAfiles = 0;
                
                foreach(string dir in directories)
                {

                    List<byte> DMAfile = new List<byte>();
                    Dictionary<string,int> Addresses = new Dictionary<string, int>();
                    
                    List<string> pngFiles = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories).Where(file => Helpers.ValidImageTypes.Contains(Path.GetExtension(file).ToLower())).ToList();
                    foreach (string file in pngFiles)
                    {
                        string[] splits = file.Split('.');
                        string format = splits[splits.Length - 2].ToUpper();
                        string filename = Path.GetFileNameWithoutExtension(file);
                        string extensions = Path.GetExtension(file);
                        string replace = "";
                        if (filename.IndexOf('.') != -1)
                        {
                            filename = filename.SubstringTill(0, ".");
                            string tmp = Path.GetFileName(file);
                            extensions = tmp.Substring(tmp.IndexOf('.'));
                        }
                        if (imageFormats.Contains(format))
                        {
                            string extra = file.ToLower().Contains("#split") ? "_0" : "";
                            string binfile = dir + filename + extra + extensions.Replace(format, ".inc");
                            FileInfo pnginfo = new FileInfo(file);
                            FileInfo bininfo = File.Exists(binfile) ? new FileInfo(binfile) : null;
                            if (bininfo == null || (pnginfo.LastWriteTime >= bininfo.LastWriteTime) || (pnginfo.CreationTime > bininfo.CreationTime))
                            {
                                List<List<byte>> Files = ConvertFileToByteList(file, format, out replace);

                                int c = 0;
                                foreach (List<byte> filee in Files)
                                {
                                    List<byte> imagedata = filee;
                                    Helpers.AddPadding(ref imagedata, 8);
                                    string suffix = Files.Count > 1 ? "_" + c : "";
                                    binfile = dir + "\\" + filename + suffix + extensions.Replace(Path.GetExtension(file), ".bin");
                                    binfile = Regex.Replace(binfile, replace, "", RegexOptions.IgnoreCase);
                                    File.WriteAllBytes(binfile, imagedata.ToArray());
                                    File.SetCreationTime(binfile, DateTime.Now);
                                    DebugConsole.WriteLine(binfile);
                                    c++;
                                }


                            }
                            
                        }
                    }
                    List<string> binFiles = Directory.GetFiles(dir, "*.bin", SearchOption.AllDirectories).ToList();
                    foreach (string bin in binFiles)
                    {
                        Addresses.Add(Path.GetFileNameWithoutExtension(bin), DMAfile.Count);
                        DMAfile.AddRange(File.ReadAllBytes(bin));
                    }
                    if (Addresses.Count > 0)
                    {
                        string romDMA = basedir + "\\rom\\system\\DMA\\";
                        string includeDMA = basedir + "\\include\\DMA\\";
                        string DMAname = dir.Substring(dir.LastIndexOf('\\')+1);
                        if (!Directory.Exists(romDMA))
                            Directory.CreateDirectory(romDMA);
                        if (!Directory.Exists(includeDMA))
                            Directory.CreateDirectory(includeDMA);
                        File.WriteAllBytes(romDMA + DMAname + ".bin", DMAfile.ToArray());
                        string header = "#include <ulib.h>\n";
                        foreach (KeyValuePair<string,int> pair in Addresses)
                        {
                            header += "#define " + ToValidCVariableName(DMAname + "_" + pair.Key) + " 0x" + pair.Value.ToString("X8") + "\n";
                        }
                        File.WriteAllText(includeDMA + DMAname + ".h",header);

                        AddDMAEntry(basedir, 0x1F, "rom/system/DMA/" + DMAname + ".bin", false);

                        DebugConsole.WriteLine("Added DMA file " + romDMA + DMAname + ".bin");
                        DMAfiles++;
                    }
                }
                if (DMAfiles == 0)
                return "No DMA files have been added. \n Usage: Create folders inside \\src\\system\\DMA\\ with .format.png files inside, such as .i8.png";
                else
                return "Done! DMA files added: " + DMAfiles;
            }
        }

        public static int AddDMAEntry(string basedir, int maxKey, string file, bool compress)
        {
            string dmapath = basedir + @"\dma.toml";

            if (!File.Exists(dmapath))
            {
                File.Create(dmapath).Close();
            }

            TomlTable toml = rom64.parseToml(dmapath);
            bool animExists = false;

            foreach (var key in toml.Keys)
            {
                if (toml[key] is TomlTable section && section["file"]?.ToString() == file)
                {
                    animExists = true;
                }
                if (int.TryParse(key.Replace("0x", ""), NumberStyles.HexNumber, null, out int numericKey))
                {
                    maxKey = Math.Max(maxKey, numericKey);
                    if (animExists) return maxKey;
                    
                }
            }
            if (animExists)
                return maxKey;
            else
            {
                TomlTable newSection = new TomlTable
                {
                    ["file"] = file,
                    ["compress"] = compress
                };
                maxKey++;
                string newKey = $"0x{maxKey:X}";
                toml[newKey] = newSection;

                using (StreamWriter writer = new StreamWriter(dmapath))
                {
                    toml.WriteTo(writer);
                }

                return maxKey;

            }

            
        }

        static string ToValidCVariableName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "_"; // Default to underscore if input is empty

            // Replace invalid characters with underscores
            input = Regex.Replace(input, @"\W", "_");

            // Ensure it starts with a letter or underscore
            if (!Regex.IsMatch(input, @"^[a-zA-Z_]"))
                input = "_" + input;

            return input;
        }

    }

}
