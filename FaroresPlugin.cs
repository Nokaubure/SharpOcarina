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
            string newanimheaderpath_temp = basedir + @"\src\system\kaleido\0x01-Player\NewAnimHeader_2.h";
            string newlinkanimspath_temp = basedir + @"\rom\NewLinkAnims_2.bin";

            if (File.Exists(newanimheaderpath_temp))
                File.Delete(newanimheaderpath_temp);
            if (File.Exists(newlinkanimspath_temp))
                File.Delete(newlinkanimspath_temp);

            File.WriteAllText(newanimheaderpath_temp, headerfile);
            File.WriteAllBytes(newlinkanimspath_temp, FullData.ToArray());

            if (!Helpers.SameFileHash(newanimheaderpath, newanimheaderpath_temp))
            {
                if (File.Exists(newanimheaderpath))
                    File.Delete(newanimheaderpath);

                File.Move(newanimheaderpath_temp, newanimheaderpath);
            }
            else
                File.Delete(newanimheaderpath_temp);


            if (!Helpers.SameFileHash(newlinkanimspath, newlinkanimspath_temp))
            {
                if (File.Exists(newlinkanimspath))
                    File.Delete(newlinkanimspath);

                File.Move(newlinkanimspath_temp, newlinkanimspath);
            }
            else
                File.Delete(newlinkanimspath_temp);


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

           
            if (!File.Exists(oldfile))
                File.Copy(newfile, oldfile);
            

            //DMA entry
            int maxKey = AddDMAEntry(basedir, 0x1F, "rom/NewLinkAnims.bin", false, false);

            if (maxKey != -1)
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

                        AddDMAEntry(basedir, 0x1F, "rom/system/DMA/" + DMAname + ".bin", false, false);

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
        public static string BuildFunctionNamesArray(string basedir)
        {
            //Stopwatch stopwatch = new Stopwatch();
            long curtime = 0;
            bool updated = false;
            //stopwatch.Start();
            
            List<FunctionName> GlobalNames = new List<FunctionName>();
            List<FunctionName> LibUserNames = new List<FunctionName>();
            List<FunctionName> PlayerNames = new List<FunctionName>();
            List<FunctionName> PauseNames = new List<FunctionName>();
            //Dictionary<ushort, List<FunctionName>> ActorNames = new Dictionary<ushort, List<FunctionName>>();
            Dictionary<ushort, List<byte>> ActorData = new Dictionary<ushort, List<byte>>();
            int maxactorID = 0;

            if (!File.Exists(basedir + @"\rom\lib_user\z_lib_user.elf"))
                return "Error! z_lib_user.elf not found";
            LibUserNames = ElfSymbols.Start(basedir + @"\rom\lib_user\z_lib_user.elf", 0x80700000);
            //LibUserNames.RemoveAll(x => x.StartAddress < 0x80700000);
            //string[] files = Directory.GetFiles(basedir + @"\rom\lib_user\", "*.o", SearchOption.AllDirectories);
            //files.ForEach(x => LibUserNames.AddRange(ElfSymbols.Start(x)));
            //LibUserNames.ForEach(x => { x.StartAddress += 0x80700000; x.EndAddress += 0x80700000; });

            //DebugConsole.WriteLine("z_lib_user.elf: " + (stopwatch.ElapsedMilliseconds - curtime) + " ms");
            //curtime = stopwatch.ElapsedMilliseconds;

            List<byte> GlobalNamesBin = File.ReadAllBytes( Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files/DBGMQ_CrashDebugger.bin")).ToList();
            for(int i = 0; i < GlobalNamesBin.Count-40; i+= 40)
            {
                
                string Name = Helpers.ReadString(GlobalNamesBin, i + 8, 32).Replace("\0","");
                if (LibUserNames.FindIndex(x => x.Name == Name) == -1)
                {
                    uint StartAddress = Helpers.Read32(GlobalNamesBin, i);
                    uint EndAddress = Helpers.Read32(GlobalNamesBin, i + 4);
                    GlobalNames.Add(new FunctionName(StartAddress, EndAddress, Name));
                }
                

            }
            //DebugConsole.WriteLine("GlobalNamesBin files: " + (stopwatch.ElapsedMilliseconds - curtime) + " ms");
            //curtime = stopwatch.ElapsedMilliseconds;
            GlobalNames.AddRange(LibUserNames);
            //GlobalNames.Sort((x, y) => x.StartAddress.CompareTo(y.StartAddress));

            /*
            files = Directory.GetFiles(basedir + @"\rom\system\kaleido\0x01-Player\","*.o",SearchOption.AllDirectories);
            files.ForEach(x => PlayerNames.AddRange(ElfSymbols.Start(x)));

            files = Directory.GetFiles(basedir + @"\rom\system\kaleido\0x00-StartMenu\", "*.o", SearchOption.AllDirectories);
            files.ForEach(x => PauseNames.AddRange(ElfSymbols.Start(x)));*/

            PlayerNames = ElfSymbols.Start(basedir + @"\rom\system\kaleido\0x01-Player\file.elf", 0x80800000);
            PauseNames = ElfSymbols.Start(basedir + @"\rom\system\kaleido\0x00-StartMenu\file.elf", 0x80800000);
            PlayerNames.ForEach(x => { x.StartAddress -= 0x80800000; x.EndAddress -= 0x80800000; });
            PauseNames.ForEach(x => { x.StartAddress -= 0x80800000; x.EndAddress -= 0x80800000; });
            //DebugConsole.WriteLine("Elf symbols: " + (stopwatch.ElapsedMilliseconds - curtime) + " ms");
            //curtime = stopwatch.ElapsedMilliseconds;


            List<byte> ActorNamesBin = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files/DBGMQ_CrashDebuggerActors.bin")).ToList();
            for (int i = 0; ; i += 4)
            {
                uint Offset = Helpers.Read32(ActorNamesBin, i);
                ushort actorID = (ushort)(i / 4);
                if (Offset == 0xFFFFFFFF) break;
                if (Offset == 0) continue;
                maxactorID++;
                List<byte> actorfuncs = new List<byte>();
                for (int y = (int)Offset; y < ActorNamesBin.Count; y += 4)
                {
                    uint tmp = Helpers.Read32(ActorNamesBin, y);
                    if (tmp == 0xFFFFFFFF) break;
                    Helpers.Append32(ref actorfuncs, tmp);
                }
                ActorData.Add(actorID, actorfuncs);
            }

            List<String> actors = rom64.getList("rom\\actor\\");

            foreach (String str in actors)
            {
                string basename = "";
                ushort index = 0;

                if (!rom64.getNameAndIndex(str, ref basename, ref index))
                    continue;

                if (index+1 > maxactorID) maxactorID = index+1;

                string[] files = Directory.GetFiles(str, "*.o", SearchOption.AllDirectories);
                List<FunctionName> actorfuncs = new List<FunctionName>();
                files.ForEach(x => actorfuncs.AddRange(ElfSymbols.Start(x)));
                List<byte> actordata = new List<byte>();
                actorfuncs.ForEach(x => x.Write(ref actordata));
                ActorData[index] = actordata;

            }



            List<byte> data = new List<byte>();
            data.AddRange(new byte[4 * 3]);
            data.AddRange(new byte[4 * maxactorID]);
            Helpers.Append32(ref data, 0xFFFFFFFF);
            Helpers.Overwrite32(ref data, 0, (uint)data.Count);/*
            for (int i = 1; i < GlobalNames.Count; i++)
            {
                GlobalNames[i - 1].EndAddress = GlobalNames[i].StartAddress - 1;
            }*/
            /* struct
            0 - Functions
            1 - Player
            2 - Pause
            3 - Actors
            */
            GlobalNames.ForEach(x => x.Write(ref data));
            Helpers.Append32(ref data, 0xFFFFFFFF);
            Helpers.Overwrite32(ref data, 4 * 1, (uint)data.Count);
            PlayerNames.ForEach(x => x.Write(ref data));
            Helpers.Append32(ref data, 0xFFFFFFFF);
            Helpers.Overwrite32(ref data, 4 * 2, (uint)data.Count);
            PauseNames.ForEach(x => x.Write(ref data));
            Helpers.Append32(ref data, 0xFFFFFFFF);
            foreach (KeyValuePair<ushort,List<byte>> kp in ActorData)
            {
                Helpers.Overwrite32(ref data, (kp.Key + 3) * 4, (uint)data.Count);
                data.AddRange(kp.Value);
                Helpers.Append32(ref data, 0xFFFFFFFF);

            }
            

            string DMAfilepath = basedir + "/rom/FunctionNames.bin";
            string DMAfilepath2 = basedir + "/rom/FunctionNames_temp.bin";
            File.WriteAllBytes(DMAfilepath2, data.ToArray());
            if (!Helpers.SameFileHash(DMAfilepath, DMAfilepath2))
            {
                if (File.Exists(DMAfilepath))
                    File.Delete(DMAfilepath);

                File.Move(DMAfilepath2, DMAfilepath);
                updated = true;
            }
            else
                File.Delete(DMAfilepath2);
            /*
            for(int i = 1; i < FunctionNames.Count; i++)
            {
            FunctionNames[i - 1].EndAddress = FunctionNames[i].StartAddress - 1;
            }
            string output = "#include <uLib.h>\n" +
            "typedef struct {\n"+
            "u32 StartAddress;\n"+
            "u32 EndAddress;\n"+
            "char Name[32];\n"+
            "}FunctionName;\n"+ 
        "const FunctionName FunctionNames[] = {";
            foreach(FunctionName function in FunctionNames)
            {
                
                output += function.ToString() + "\n";
            }
            output += "};";
            File.WriteAllText(basedir+"/src/lib_user/library/CrashScreen.h", output);
            */
            /*
                       string[] GlobalLinkerFiles = { basedir + "/include/z64hdr/oot_mq_debug/sym_src.ld"};
                       foreach (string file in GlobalLinkerFiles)
                       {
                           string[] lines = File.ReadAllLines(file);
                           foreach(string line in lines)
                           {
                               string trimmedline = line.Trim();
                               if (trimmedline.Contains("="))
                               {
                                   string[] split = trimmedline.Split('=');
                                   if (split.Length != 2) continue;
                                   string Name = split[0].Replace(" ", "");
                                   if (Name.Contains("__vanilla_hook_"))
                                   {
                                       Name = (Name.Replace("__vanilla_hook_", "").Substring2(0, 25)) + "(Hook)";
                                   }
                                   else Name = Name.Substring2(0, 32);
                                   uint StartAddress = Convert.ToUInt32(split[1].SubstringTill(0,';').Replace("0x","").Replace(";","").Trim(),16);
                                   if (StartAddress < 0x80000000) continue;
                                   if (Name.StartsWith("s") || Name.StartsWith("g") || Name.StartsWith("_") || Name.StartsWith("DL_") || Name.StartsWith("D_")) continue;
                                   GlobalNames.Add(new FunctionName(StartAddress, 0, Name));
                               }
                           }
                       }*/


            string newfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\CrashScreen.c");
            string oldfile = basedir + @"\src\lib_user\library\CrashScreen.c";
            string tmpfile = basedir + @"\src\lib_user\library\CrashScreen_2.c";
            if (File.Exists(tmpfile))
                File.Delete(tmpfile);
            if (File.Exists(oldfile))
                File.Copy(oldfile, tmpfile);
            else
                File.Copy(newfile, tmpfile);

            int maxKey = AddDMAEntry(basedir, 0x1F, "rom/FunctionNames.bin", false, true);

            Helpers.ReplaceLine("#define FUNCTIONNAMES_DMAID", "#define FUNCTIONNAMES_DMAID 0x" + maxKey.ToString("X2"), tmpfile);
            Helpers.ReplaceLine("#define FUNCTIONNAMES_MAXACTORS", "#define FUNCTIONNAMES_MAXACTORS 0x" + maxactorID.ToString("X4"), tmpfile);


            if (!Helpers.SameFileHash(oldfile, tmpfile))
            {
                if (File.Exists(oldfile))
                    File.Delete(oldfile);

                File.Move(tmpfile, oldfile);
                updated = true;
            }
            else
                File.Delete(tmpfile);
            /*
            DebugConsole.WriteLine("Time elapsed: " + (stopwatch.ElapsedMilliseconds - curtime) + " ms");

            stopwatch.Stop();*/

            if (!updated)
                return "Unchanged";
            else
                return "Done!";
        }

        //used by dev only
        public static void BuildCrashDebuggerActors(string basedir, List<DatabaseActor> Database)
        {
            Stopwatch stopwatch = new Stopwatch();
            long curtime = 0;
            stopwatch.Start();
            
            Dictionary<ushort, List<FunctionName>> ActorNames = new Dictionary<ushort, List<FunctionName>>();
            int maxactorID = 0;
            string[] files = Directory.GetFiles(@"Z:\cygwin64\home\Noka\OoTdecomp\build\src\overlays\actors\", "*.o", SearchOption.AllDirectories); 
            foreach(string file in files)
            {
                if (file.Contains("_reloc.o")) continue;
                string debugname = Path.GetFileNameWithoutExtension(file).ToLower();
                if (debugname == "z_player") continue;
                debugname = debugname.Substring(2);
                DatabaseActor actor = Database.Find(x => x.DebugName.ToLower() == debugname);
                ushort actorID = 0xFFFF;
                if (actor != null)
                {
                    actorID = Database.Find(x => x.DebugName.ToLower() == debugname).Value;
                }
                else
                {
                    DebugConsole.WriteLine($"Actor {debugname} not found!");
                    continue;
                }
                if (ActorNames.ContainsKey(actorID))
                {
                    DebugConsole.WriteLine($"Actor {debugname}-{actorID.ToString("X4")} already exists!");
                    continue;
                }
                else
                {
                    ActorNames.Add(actorID, ElfSymbols.Start(file));
                    if (actorID > maxactorID) maxactorID = actorID;
                }
                

            }
            List<byte> temp = new List<byte>();
            temp.AddRange(new byte[4 * maxactorID]);
            Helpers.Append32(ref temp, 0xFFFFFFFF);
            for (ushort i = 0; i < maxactorID; i++)
            {
                if (ActorNames.ContainsKey(i))
                {
                    Helpers.Overwrite32(ref temp, 4 * i, (uint)temp.Count);
                    ActorNames[i].ForEach(x => x.Write(ref temp));
                }
                Helpers.Append32(ref temp, 0xFFFFFFFF);

            }

            File.WriteAllBytes(rom64.getPath() + "/DBGMQ_CrashDebuggerActors", temp.ToArray());

            return;
        }

        public static int AddDMAEntry(string basedir, int maxKey, string file, bool compress, bool returnKey)
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
                    if (animExists) return returnKey ? maxKey : -1;

                }
            }
            if (animExists)
                return returnKey ? maxKey : -1;
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

        public static string ToValidCVariableName(string input)
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

        public class FunctionName
        {
            public uint StartAddress;
            public uint EndAddress;
            public string Name;
            public int type;
            public const int GLOBAL = 0;
            public const int PLAYER = 1;
            public const int PAUSE = 2;
            public const int ACTOR = 3;
            public FunctionName(uint StartAddress, uint EndAddress, string Name)
            {
                this.StartAddress = StartAddress;
                this.EndAddress = EndAddress;
                this.Name = Name.Replace("__vanilla_hook_","");
            }
            public override string ToString()
            {
                return $"{{0x{StartAddress.ToString("X8")},0x{EndAddress.ToString("X8")},\"{Name}\"}},";
            }
            public void Write(ref List<byte> data)
            {
                Helpers.Append32(ref data, StartAddress);
                Helpers.Append32(ref data, EndAddress);
                Helpers.AppendString(ref data, Name, 32);
            }
        }

    }

}
