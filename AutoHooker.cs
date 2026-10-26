using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Ionic.Zip;

namespace SharpOcarina
{


    public partial class AutoHookerForm : Form
    {

        public List<FunctionHook> FunctionHooks = new List<FunctionHook>();
        public List<string> LibUserFileList = new List<string>();
        public List<string> LibCodeFileList = new List<string>();
        public List<FunctionHook> VanillaFunctions = new List<FunctionHook>();
        public List<HookActor> VanillaActors = new List<HookActor>();
        public List<CustomActorz64rom> z64romactors = new List<CustomActorz64rom>();
        public List<CustomObjectz64rom> z64romobjects = new List<CustomObjectz64rom>();
        public BindingSource bindingSource = new BindingSource();
        
        string website = "https://raw.githubusercontent.com/Nokaubure/SharpOcarina/master/Extra/";
        public WebClient client;
        public bool descending;
        public MainForm mainform;
        public AutoHookerForm(MainForm mainform)
        {
            this.mainform = mainform;
            InitializeComponent();
            HookGrid.AutoGenerateColumns = false;
#if DEBUG
            button2.Visible = true;
#endif

            string linker = rom64.getPath() + "\\include\\z64hdr\\oot_mq_debug\\sym_src.ld";
            string functionsh = rom64.getPath() + "\\include\\z64hdr\\include\\functions.h";

            client = new WebClient();

            if (!File.Exists(linker))
            {
                MessageBox.Show(linker + "Doesn't exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!Directory.Exists(rom64.getPath() + "\\z64oot\\src\\"))
            {
               
                string z64ootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\z64oot_optimized.zip");
                if (!File.Exists(z64ootPath))
                {
                    if (MessageBox.Show("z64oot decomp folder not present in the project, download it? (this is only required once)", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        PleaseWait pleasewait = new PleaseWait(website + "z64oot_optimized.zip", z64ootPath, rom64.getPath(), false);
                        pleasewait.ShowDialog();

                    }
                    else
                    {
                        this.Close();
                        return;
                    }
                }
                else
                {
                    PleaseWait pleasewait = new PleaseWait("", z64ootPath, rom64.getPath(), false);
                    pleasewait.ShowDialog();
                }
            }

            string[] lines = File.ReadAllLines(linker);

            string fileName = "";
            string decompPath = rom64.getPath() + "\\z64oot\\src\\";

            foreach (string rawline in lines)
            {
                string line = rawline.Trim();
                if (line.StartsWith("/*"))
                {
                    string str = line.Replace(" ", "").Replace("/*", "").Replace("*/", "");
                    fileName = decompPath + str;
                }
                else
                {
                    if (File.Exists(fileName) && line.Contains('=') && line.Contains("0x"))
                    {
                        string str = line.SubstringTill(0, '=').Trim(); //TODO check
                        //if (functionstext.Contains(str))
                            VanillaFunctions.Add(new FunctionHook(str.Trim(),fileName,false));
                    }
                }


            }

            FunctionNameTextbox.SetAutoCompleteItems(VanillaFunctions.Select(h => h.Name));

            RefreshLibUser();

            RefreshVanillaActorList();

            List<String> actors = rom64.getList("src\\actor\\");

            foreach (String str in actors)
            {
                string basename = "";
                ushort index = 0;
                int version = -1;

                if (!rom64.getNameAndIndex(str, ref basename, ref index))
                    continue;
                z64romactors.Add(new CustomActorz64rom(index, basename, -1));

            }

            List<String> objects = rom64.getList("src\\object\\");

            foreach (String str in objects)
            {
                string basename = "";
                ushort index = 0;

                if (!rom64.getNameAndIndex(str, ref basename, ref index))
                    continue;

                z64romobjects.Add(new CustomObjectz64rom(index, basename));

            }

            HookGrid.DataSource = FunctionHooks;

            HookGrid.CellFormatting += (sender, e) =>
            {
                if (HookGrid.Columns[e.ColumnIndex].DataPropertyName == "isLibCode" &&
                    e.Value is bool boolValue)
                {
                    e.Value = boolValue ? "lib_code" : "lib_user";
                    e.FormattingApplied = true;
                }
                if (HookGrid.Columns[e.ColumnIndex].DataPropertyName == "FileName")
                {
                    e.Value = Path.GetFileName((string)e.Value);
                    e.FormattingApplied = true;
                }
            };
 

            UpdateForm();
        }

        private void RefreshLibUser()
        {

            string basepath = rom64.getPath() + Path.DirectorySeparatorChar;
            string lib_userpath = "src\\lib_user";
            string lib_codepath = "rom\\lib_code\\!std";

            FunctionHooks.Clear();
            LibUserFileList.Clear();
            LibCodeFileList.Clear();

            List<string> lib_userdirs = rom64.getList(lib_userpath, false);
            List<string> lib_codedirs = rom64.getList(lib_codepath, false);
            List<string>[] dirs = { lib_userdirs , lib_codedirs };
            List<string>[] target = { LibUserFileList, LibCodeFileList };
            for (int i = 0; i < 2; i++)
            {
                foreach (string dir in dirs[i])
                {
                    List<string> files = Directory.GetFiles(dir).ToList();
                    foreach (string file in files)
                        if (i == 0 && (Path.GetExtension(file) == ".c" || Path.GetExtension(file) == ".h"))
                        {
                            target[i].Add(file);
                            string code = AutoHookerParser.Mask(File.ReadAllText(file));
                            foreach (Match hook in Regex.Matches(code, @"\bAsm_VanillaHook\s*\(\s*([A-Za-z_]\w*)\s*\)"))
                                FunctionHooks.Add(new FunctionHook(hook.Groups[1].Value, file, false));
                        }
                        else if (i == 1 && Path.GetExtension(file) == ".o")
                        {
                            FunctionHooks.Add(new FunctionHook(Path.GetFileNameWithoutExtension(file), file.Replace("\\rom\\","\\src\\").Replace(".o",".c"), true));
                        }
                }
                
            }
            HookGrid.Refresh();
        }

        private void RefreshVanillaActorList()
        {

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML/OOT/ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Actor");

            foreach (XmlNode node in nodes)
            {

                if (node is XmlElement element && element.HasAttribute("DebugName"))
                {
                    XmlAttributeCollection attr = element.Attributes;
                    XmlAttribute nameAttr = attr["Name"];

                    VanillaActors.Add(new HookActor(Convert.ToUInt16(attr["Key"].Value,16), attr["Name"].Value, attr["DebugName"].Value));
                }

            }
            bindingSource.DataSource = new BindingList<HookActor>(VanillaActors);
            VanillaActorList.DataSource = bindingSource;
            fs.Close();
        }

        private void UpdateForm()
        {

        }

        private void AutoHookerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.autohooker_visible = false;
        }


        private void RefreshListButton_Click(object sender, EventArgs e)
        {
            RefreshLibUser();
        }

        private void HookButton_Click(object sender, EventArgs e)
        {
            if (FunctionNameTextbox.Text == "") return;
            string file = HookFunction(FunctionNameTextbox.Text,true);
            if (file != "")
                if (MessageBox.Show("Done! open file containing the hook?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    OpenFile(file);
                }

            RefreshLibUser();
        }

        private string HookFunction(string functionname, bool warning = false)
        {

            
            int index = FunctionHooks.FindIndex(x => x.Name == functionname);
            if (index != -1)
            {
                if (warning) MessageBox.Show("This function is already hooked in file: " + FunctionHooks[index].FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DebugConsole.WriteLine("Function " + functionname + " is already hooked!");
                return "";
            }
            else
            {
                string basepath = rom64.getPath() + Path.DirectorySeparatorChar;

                int index2 = VanillaFunctions.FindIndex(x => x.Name == functionname);
                if (index2 == -1)
                {
                    if (warning) MessageBox.Show("This function doesn't exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DebugConsole.WriteLine("Function " + functionname + " doesn't exists!");
                    return "";
                }
                string sourceFile = VanillaFunctions[index2].FileName;
                string targetFile;
                if (FileNameOriginalRadioButton.Checked)
                    targetFile = basepath + "src\\lib_user\\library\\" + Path.GetFileName(sourceFile);
                else if (FileNameFunctionRadioButton.Checked)
                    targetFile = basepath + "src\\lib_user\\library\\" + functionname + ".c";
                else
                    targetFile = basepath + "src\\lib_user\\library\\" + CustomFileName.Text + (CustomFileName.Text.EndsWith(".c", StringComparison.OrdinalIgnoreCase) ? "" : ".c");

                try
                {
                    string output = AutoHookerParser.Copy(File.ReadAllText(sourceFile),
                        File.Exists(targetFile) ? File.ReadAllText(targetFile) : "", functionname,
                        VanillaFunctions.Select(x => x.Name));
                    Directory.CreateDirectory(Path.GetDirectoryName(targetFile));
                    File.WriteAllText(targetFile, output);
                    DebugConsole.WriteLine(functionname + " +");
                    return targetFile;
                }
                catch (FormatException ex)
                {
                    DebugConsole.WriteLine(ex.Message);
                    if (warning) MessageBox.Show(ex.Message, "Hook could not be created", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }

                


            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile(string FileName = "")
        {
           
            if (FileName == "" && FunctionHooks.Count != 0)
            {
                int index = HookGrid.CurrentRow.Index;
                if (index > -1 && index < FunctionHooks.Count)
                    System.Diagnostics.Process.Start(FunctionHooks[index].FileName);
            }
            else if(FileName != "")
            {
                System.Diagnostics.Process.Start(FileName);
            }
            
        }

        private void HookGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void FileNameCustomRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CustomFileName.Enabled = FileNameCustomRadioButton.Checked;
        }

        private void ExtraGoButton_Click(object sender, EventArgs e)
        {

        }

        private void HookGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int columnIndex = e.ColumnIndex;
            if (!descending)
                FunctionHooks.Sort((a, b) => string.Compare(columnIndex == 0 ? a.Name : (columnIndex == 1 ? a.FileName : a.isLibCode.ToString()), columnIndex == 0 ? b.Name : (columnIndex == 1 ? b.FileName : b.isLibCode.ToString()), StringComparison.OrdinalIgnoreCase));
            else
                FunctionHooks.Sort((a, b) => string.Compare(columnIndex == 0 ? b.Name : (columnIndex == 1 ? b.FileName : b.isLibCode.ToString()), columnIndex == 0 ? a.Name : (columnIndex == 1 ? a.FileName : a.isLibCode.ToString()), StringComparison.OrdinalIgnoreCase));
            HookGrid.Refresh();
            descending = !descending;
        }

        private void ExtraVanillaActorGoButton_Click(object sender, EventArgs e)
        {
            

            if (VanillaActorList.SelectedIndex != -1)
            {
                HookActor target = (VanillaActorList.SelectedItem as HookActor);
                if (z64romactors.FindIndex(x => x.ID == target.Value) == -1)
                {
                    string z64ootpath = rom64.getPath() + "\\z64oot\\src\\overlays\\actors\\ovl_" + target.DebugName;
                    string foldername = Regex.Replace(target.Name, "[^a-zA-Z0-9]", "");
                    string newpath = rom64.getPath() + "\\src\\actor\\" + ConvertToValidDirectoryName("0x" + target.Value.ToString("X4") + "-" + foldername);

                    if (!Directory.Exists(z64ootpath))
                    {
                        MessageBox.Show(z64ootpath + " doesn't exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        Directory.CreateDirectory(newpath);
                        DirectoryInfo dirinfo = new DirectoryInfo(z64ootpath);

                        FileInfo[] files = dirinfo.GetFiles();
                        foreach (FileInfo file in files)
                        {
                            string tempPath = Path.Combine(newpath, file.Name);
                            file.CopyTo(tempPath, false);
                        }

                        string[] newfiles = Directory.GetFiles(newpath);

                        foreach (string file in newfiles)
                        {
                            if (Path.GetExtension(file) != ".c" && Path.GetExtension(file) != ".h")
                                continue;
                            List<CData> includes = new List<CData>();

                            string text = File.ReadAllText(file);
                            string code = AutoHookerParser.Mask(text, false);
                            foreach (Match include in Regex.Matches(code, @"(?m)^[ \t]*#\s*include[ \t]*[<""](?<path>assets/overlays/[^>""\r\n]+)[>""]"))
                                includes.Add(new CData(include.Groups["path"].Value, text.Substring(include.Index, include.Length)));
                            if (includes.Count > 0)
                            {
                                bool exists = false;
                                string assets_overlaysPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\z64oot_assets_overlays.zip");
                                if (!Directory.Exists(rom64.getPath() + "\\z64oot\\assets\\overlays\\"))
                                {
                                    if (!File.Exists(assets_overlaysPath))
                                    {
                                        if (MessageBox.Show("This actor contains C assets, SharpOcarina needs to download them (this is only required once), continue?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                        {
                                            PleaseWait pleasewait = new PleaseWait(website + "z64oot_assets_overlays.zip", assets_overlaysPath, rom64.getPath() + "\\z64oot\\", false);
                                            pleasewait.ShowDialog();
                                            exists = true;
                                        }
                                    }
                                    else
                                    {
                                        PleaseWait pleasewait = new PleaseWait("", assets_overlaysPath, rom64.getPath() + "\\z64oot\\", false);
                                        pleasewait.ShowDialog();
                                        exists = true;
                                    }

                                }
                                else
                                {
                                    exists = true;
                                }
                                if (exists)
                                {
                                    foreach (CData include in includes)
                                    {
                                        string includepath = include.name;
                                        string assetpath = Path.GetDirectoryName(rom64.getPath() + "\\z64oot\\" + includepath);
                                        DirectoryInfo assetdirinfo = new DirectoryInfo(assetpath);

                                        FileInfo[] assetfiles = assetdirinfo.GetFiles();
                                        foreach (FileInfo assetfile in assetfiles)
                                        {
                                            if (!File.Exists(Path.Combine(newpath, assetfile.Name)))
                                            {
                                                string tempPath = Path.Combine(newpath, assetfile.Name);
                                                assetfile.CopyTo(tempPath, false);

                                                if (Path.GetExtension(tempPath) == ".c" || Path.GetExtension(tempPath) == ".h")
                                                {
                                                    string assettext = File.ReadAllText(tempPath);
                                                    assettext = assettext.Replace("assets/overlays/ovl_" + target.DebugName + "/", "")
                                                        .Replace("#include \"ultra64.h\"", "#include <uLib.h>")
                                                        .Replace("#include \"z64.h\"", "")
                                                        .Replace(".inc.c", ".inc");
                                                    File.WriteAllText(tempPath,assettext);
                                                }
                                                if (Path.GetExtension(tempPath) == ".png" && !File.Exists(tempPath.Replace(".png",".inc")))
                                                {
                                                    string[] splits = tempPath.Split('.');
                                                    List<byte> imagedata = Helpers.ConvertImageToData(tempPath, splits[splits.Length - 2].ToUpper());
                                                    Helpers.AddPadding(ref imagedata, 8);

                                                    string output = "";

                                                    int column = 0;

                                                    for (int i = 0; i < imagedata.Count - 1; i += 8)
                                                    {

                                                        output += "0x" + Helpers.Read32(imagedata, i).ToString("X8") + Helpers.Read32(imagedata, i+4).ToString("X8") + ", ";
                                                        column++;
                                                        if (column == 4)
                                                        {
                                                            output += "\n";
                                                            column = 0;
                                                        }
                                                    }
                                                    File.WriteAllText(tempPath.Replace(".png", ".inc"), output);
                                                }
                                                

                                            }
                                        }

                                        text = text.Replace(include.line, "#include \"" + Path.GetFileName(includepath) + "\"").Replace(".inc.c", ".inc");
                                    }
                                }

                                File.WriteAllText(file,text);
                                    
                            }
                        }
                        z64romactors.Add(new CustomActorz64rom(target.Value, target.Name, -1));

                        if (MessageBox.Show("Done! created directory in " + newpath + "\nOpen directory in explorer?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Process.Start(newpath);
                        }


                    }

                }
                else
                {
                    MessageBox.Show("An actor with the same ID already exists in this project!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        private string ConvertToValidDirectoryName(string input)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            return new string(input.Where(c => !invalidChars.Contains(c)).ToArray());
        }

        private void VanillaActorSort_CheckedChanged(object sender, EventArgs e)
        {
            if (VanillaActorSort.Checked)
                bindingSource.DataSource = new BindingList<HookActor>(VanillaActors.OrderBy(x => x.Name).ToList());
            else
                bindingSource.DataSource = new BindingList<HookActor>(VanillaActors.OrderBy(x => x.Value).ToList());

            // Refresh the ListBox
            VanillaActorList.Refresh();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string[] files =
            {
                "do_action_static", "icon_item_24_static", "icon_item_dungeon_static",
                "icon_item_field_static","icon_item_gameover_static","icon_item_NES_static",
                "icon_item_static","item_name_static","map_grand_static","message_static",
                "message_texture_static","map_name_static",
                "nintendo_rogo_static","parameter_static","title_static","map_i_static","object_mag"
            };
            //"nes_font_static",
            Array.Sort(files);

            string texelcfg = "# # Patch with textures # # # # # # # # # # # # # # # # # # # # # # # #\r\n#                                                                     #\r\n#    TEXTURE(\"file\", format)                                          #\r\n#                                                                     #\r\n#    Supported formats:                                               #\r\n#        I4     I8     I16                                            #\r\n#        IA4    IA8    IA16                                           #\r\n#        RGBA16 RGBA32                                                #\r\n#                                                                     #\r\n# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #\r\n\r\ninclude <macros.cfg>\r\n\r\n";

            string z64ootxml = rom64.getPath() + "\\z64oot\\assets\\xml\\textures\\";
            string z64oottextures = rom64.getPath() + "\\z64oot\\assets\\textures\\";
            string z64ootxml2 = rom64.getPath() + "\\z64oot\\assets\\xml\\objects\\";
            string z64oottextures2 = rom64.getPath() + "\\z64oot\\assets\\objects\\";
            string patchpath = rom64.getPath() + "\\patch\\";

            foreach (string file in files)
            {

                bool isobject = false;
                XmlDocument doc = new XmlDocument();
                var XML = z64ootxml + file + ".xml";
                if (!File.Exists(XML))
                {
                    XML = z64ootxml2 + file + ".xml";
                    isobject = true;
                }
                FileStream fs = new FileStream(XML, FileMode.Open, FileAccess.ReadWrite);
                doc.Load(fs);
                XmlNodeList nodes = doc.SelectNodes("Root/File/Texture");

                texelcfg += "\r\n[rom/system/static/" + file + ".bin]\r\n";

                 Directory.CreateDirectory(patchpath + "images\\" + file);
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection attributes = node.Attributes;
                    int offsetval = (Convert.ToInt32(attributes["Offset"].Value, 16));
                    string offset = "0x" + (offsetval).ToString("X8");
                    string format = attributes["Format"].Value.ToUpper();
                    if (format.Contains("CI")) continue;
                    FileInfo sourcefile = new FileInfo(rom64.getPath() + "\\rom\\system\\static\\.vanilla\\" + file + ".bin");
                    if (!isobject && offsetval >= sourcefile.Length) continue;
                    if (file == "parameter_static" && offsetval == 0x3AC0) continue;
                    string shortfilename = attributes["OutName"].Value + "." + format.ToLower() + ".png";
                    string filename = file + "/" + shortfilename;
                    texelcfg += $"##\t{offset} = TEXTURE(\"images/{filename}\", {format})\r\n";
                    string newfile = patchpath + "images\\" + file + "\\" + shortfilename;
                    if (!File.Exists(newfile))
                        File.Copy((!isobject ? z64oottextures : z64oottextures2) + file + "\\" + shortfilename, patchpath + "images\\" + file + "\\" + shortfilename);

                }

                fs.Close();
                
                
            }

            File.WriteAllText(patchpath + "texel.cfg",texelcfg);

        }

        private void VanillaActorListFilter_TextChanged(object sender, EventArgs e)
        {
            string filterText = VanillaActorListFilter.Text.ToLower();
            if (string.IsNullOrWhiteSpace(filterText))
            {
                bindingSource.DataSource = new BindingList<HookActor>(VanillaActors);
            }
            else
            {
            bindingSource.DataSource = new BindingList<HookActor>(
                VanillaActors.Where(item =>
                    item.Value.ToString("X4").Contains(filterText) ||
                    item.Name.ToLower().Contains(filterText) ||
                    item.DebugName.ToLower().Contains(filterText)
                ).ToList()
            );
            }
        }

        private void ViewFileButton_Click(object sender, EventArgs e)
        {

            if (FunctionNameTextbox.Text == "") return;
            string functionname = FunctionNameTextbox.Text;
            int index = FunctionHooks.FindIndex(x => x.Name == functionname);
            if (index != -1)
            {
                OpenFile(FunctionHooks[index].FileName);
            }
            else
            {
               
                int index2 = VanillaFunctions.FindIndex(x => x.Name == functionname);
                if (index2 == -1)
                {
                    MessageBox.Show("This function doesn't exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                OpenFile(VanillaFunctions[index2].FileName);
            }

        }

        private void CreateActorTextBox_TextChanged(object sender, EventArgs e)
        {
            CreateActorTextBox.Text = Regex.Replace(CreateActorTextBox.Text, "[^a-zA-Z0-9_]", "");//ConvertToValidDirectoryName(CreateActorTextBox.Text);
        }

        private void CreateActorButton_Click(object sender, EventArgs e)
        {
            if (CreateActorTextBox.Text == "")
            {
                MessageBox.Show("Please, choose a name for the new empty actor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string template = rom64.getPath() + "\\tools\\ActorTemplate";
            string destination;
            ushort ActorID = 0;
            ushort ObjectID = 0;
            bool proceed = false;
            using (PickCustomActorID pickID = new PickCustomActorID(0, 0, true, true, true, "Empty", z64romactors, z64romobjects))
            {
                if (pickID.ShowDialog() == DialogResult.OK)
                {
                    ActorID = pickID.ActorID;
                    ObjectID = pickID.ObjectID;
                    if (z64romactors.FindIndex(x => x.ID == ActorID) != -1)
                    {
                        MessageBox.Show("ActorID already in use by a custom actor!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    proceed = true;
                }
            }
            if (proceed)
            {
                destination = rom64.getPath() + "\\src\\actor\\0x" + ActorID.ToString("X4") + "-" + CreateActorTextBox.Text + "\\";
                string newfileC = destination + CreateActorTextBox.Text + ".c";
                string newfileH = destination + CreateActorTextBox.Text + ".h";
                Directory.CreateDirectory(destination);
                File.Copy(template + ".c", newfileC);
                File.Copy(template + ".h", newfileH);
                Helpers.ReplaceLine("#define MOTION_BLUR", "#define MOTION_BLUR " + "true", newfileC);

                string newfileCtxt = File.ReadAllText(newfileC);
                string newfileHtxt = File.ReadAllText(newfileH);
                newfileCtxt = newfileCtxt.Replace("[[ACTOR_ID_PLACEHOLDER]]", ".id           = 0x" + ActorID.ToString("X4") + ",");
                newfileCtxt = newfileCtxt.Replace("[[OBJECT_ID_PLACEHOLDER]]", ".objectId     = 0x" + ObjectID.ToString("X4") + ",");
                newfileCtxt = newfileCtxt.Replace("EnActor", CreateActorTextBox.Text);
                newfileCtxt = newfileCtxt.Replace("ActorTemplate.h", CreateActorTextBox.Text + ".h");
                newfileHtxt = newfileHtxt.Replace("__EN_ACTOR_H__", "__ACT_" + CreateActorTextBox.Text.ToUpper().Replace("-","") + "_H__");
                newfileHtxt = newfileHtxt.Replace("EnActor", CreateActorTextBox.Text);

                File.WriteAllText(newfileC,newfileCtxt);
                File.WriteAllText(newfileH, newfileHtxt);

                z64romactors.Add(new CustomActorz64rom(ActorID, CreateActorTextBox.Text, -1));
                if (MainForm.ActorCache.ContainsKey(ActorID))
                {
                    MainForm.ActorCache[ActorID].name = CreateActorTextBox.Text;
                    MainForm.ActorCache[ActorID].actorproperties = new List<ActorProperty>();
                    MainForm.ActorCache[ActorID].objects = "" + ObjectID.ToString("X4");
                }
                else
                    MainForm.ActorCache.Add(ActorID, new ActorInfo(CreateActorTextBox.Text, new List<ActorProperty>(), "" + ObjectID.ToString("X4")));
                if (z64romobjects.FindIndex(x => x.ID == ObjectID) == -1 && !MainForm.ObjectCache.ContainsKey(ObjectID))
                {
                    z64romobjects.Add(new CustomObjectz64rom(ObjectID, "NewObject" + ObjectID.ToString("X4")));
                    MainForm.ObjectCache.Add(ObjectID, new ObjectInfo(1, CreateActorTextBox.Text, "" + ActorID.ToString("X4")));
                }

                if (MessageBox.Show("Done! created directory in " + destination + "\nOpen directory in explorer?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.Start(destination);
                }
            }
        }

        private void OpenActorDirectoryButton_Click(object sender, EventArgs e)
        {
            if (VanillaActorList.SelectedIndex != -1)
            {
                HookActor target = (VanillaActorList.SelectedItem as HookActor);
                string z64ootpath = rom64.getPath() + "\\z64oot\\src\\overlays\\actors\\ovl_" + target.DebugName + "\\";
                if (Directory.Exists(z64ootpath))
                {
                    Process.Start(z64ootpath);
                }
            }
        }
    }

    public class FunctionHook
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public bool isLibCode { get; set; }

        public FunctionHook(string _Name, string _FileName, bool _isLibCode)
        {
            Name = _Name;
            FileName = _FileName;
            isLibCode = _isLibCode;
        }
    }
    public class CData
    {
        public string name;
        public string line;

        public CData(string _name, string _line)
        {
            name = _name;
            line = _line;
        }
    }
    public class CFile
    {
        public string FileName;
        public List<CData> includes = new List<CData>();
        public List<CData> defines = new List<CData>();
        public List<CData> predeclarations = new List<CData>();
        public List<CData> structs = new List<CData>();
        public List<CData> variables = new List<CData>();
        public List<CData> functions = new List<CData>();

        public CFile()
        {

        }
    }

    public class HookActor
    {
        public string Name;
        public string DebugName;
        public ushort Value;
        public HookActor(ushort Value, string Name, string DebugName)
        {
            this.Name = Name;
            this.DebugName = DebugName;
            this.Value = Value;
        }
        public override string ToString()
        {
            return $"{Value.ToString("X4")} - {Name} / {DebugName}";
        }
    }


    #region Parser
    // This scans declarations rather than lines. Original text is retained for output;
    // comments, literals and directives are masked only while locating C punctuation.
    internal static class AutoHookerParser
    {
        internal sealed class Declaration
        {
            public string Kind;
            public string Name;
            public string Text;
            public string Code;
            public string Signature;
            public string Conditions;
            public int ConditionCount;
            public int Start;
            public int End;

            public string Key { get { return Kind + "|" + Name + "|" + Conditions; } }

            public string Wrap(string text)
            {
                return Conditions + text.Trim() + "\n" + string.Concat(Enumerable.Repeat("#endif\n", ConditionCount));
            }
        }

        // Keep offsets and newlines intact so slices always refer to the original text.
        internal static string Mask(string text, bool maskLiterals = true)
        {
            char[] code = text.ToCharArray();
            int state = 0; // code, block comment, line comment, string, character
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                char next = i + 1 < text.Length ? text[i + 1] : '\0';
                if (state == 0)
                {
                    if (c == '/' && (next == '*' || next == '/'))
                    {
                        state = next == '*' ? 1 : 2;
                        code[i] = code[++i] = ' ';
                    }
                    else if (c == '"' || c == '\'')
                    {
                        state = c == '"' ? 3 : 4;
                        if (maskLiterals) code[i] = ' ';
                    }
                }
                else
                {
                    if ((state < 3 || maskLiterals) && c != '\r' && c != '\n') code[i] = ' ';
                    if (state == 1 && c == '*' && next == '/')
                    {
                        code[++i] = ' ';
                        state = 0;
                    }
                    else if (state == 2 && c == '\n' && (i == 0 || text[i - 1] != '\\') &&
                             (i < 2 || text[i - 1] != '\r' || text[i - 2] != '\\')) state = 0;
                    else if (state >= 3 && c == '\\' && i + 1 < text.Length)
                    {
                        i++;
                        if (maskLiterals && text[i] != '\r' && text[i] != '\n') code[i] = ' ';
                    }
                    else if ((state == 3 && c == '"') || (state == 4 && c == '\'')) state = 0;
                }
            }
            return new string(code);
        }

        private static string FunctionName(string header)
        {
            // The opening parameter list must follow an identifier, with no initializer.
            if (header.Contains("=")) return "";
            Match match = Regex.Match(header, @"\b([A-Za-z_]\w*)\s*\(");
            if (!match.Success || match.Groups[1].Value == "__attribute__") return "";
            return match.Groups[1].Value;
        }

        private static string DeclarationName(string code)
        {
            // Names for typedefs, arrays, pointer objects and function-pointer objects.
            Match pointer = Regex.Match(code, @"\(\s*\*\s*([A-Za-z_]\w*)\s*\)");
            if (pointer.Success) return pointer.Groups[1].Value;
            int initializer = code.IndexOf('=');
            if (initializer >= 0) code = code.Substring(0, initializer);
            int brace = code.LastIndexOf('}');
            string tail = brace >= 0 ? code.Substring(brace + 1) : code;
            tail = Regex.Replace(tail, @"\[[^\]]*\]", " ");
            int equals = tail.IndexOf('=');
            if (equals >= 0) tail = tail.Substring(0, equals);
            MatchCollection names = Regex.Matches(tail, @"\b[A-Za-z_]\w*\b");
            if (names.Count > 0) return names[names.Count - 1].Value;
            Match tag = Regex.Match(code, @"\b(?:struct|union|enum)\s+([A-Za-z_]\w*)");
            return tag.Success ? tag.Groups[1].Value : code.Trim();
        }

        private static HashSet<string> References(string code, List<Declaration> declarations)
        {
            // Member names belong to their struct, not to file-scope declarations.
            code = Regex.Replace(code, @"(?:->|\.)\s*[A-Za-z_]\w*", " ");
            var identifiers = new HashSet<string>(Regex.Matches(code, @"\b[A-Za-z_]\w*\b")
                .Cast<Match>().Select(m => m.Value), StringComparer.Ordinal);
            var expanded = new HashSet<string>(StringComparer.Ordinal);
            bool changed;
            do
            {
                changed = false;
                foreach (Declaration d in declarations.Where(d => d.Kind == "directive"))
                {
                    string masked = Mask(d.Text);
                    Match define = Regex.Match(masked, @"^#\s*define\s+([A-Za-z_]\w*)");
                    if (!define.Success || !identifiers.Contains(define.Groups[1].Value) || !expanded.Add(d.Key)) continue;
                    foreach (Match token in Regex.Matches(masked, @"\b[A-Za-z_]\w*\b"))
                        if (identifiers.Add(token.Value)) changed = true;
                }
            }
            while (changed);
            return identifiers;
        }

        private static IEnumerable<string> Symbols(Declaration declaration)
        {
            if (declaration.Kind == "condition") yield break;
            if (declaration.Kind == "directive")
            {
                Match macro = Regex.Match(Mask(declaration.Text), @"^#\s*(?:define|undef)\s+([A-Za-z_]\w*)");
                if (macro.Success) yield return macro.Groups[1].Value;
                yield break;
            }
            yield return declaration.Name;
            if (declaration.Kind != "type" && declaration.Kind != "variable") yield break;
            string code = declaration.Code;
            foreach (Match tag in Regex.Matches(code, @"^\s*(?:typedef\s+)?(?:struct|union|enum)\s+([A-Za-z_]\w*)\s*(?:\{|;)"))
                if (declaration.Kind == "type") yield return tag.Groups[1].Value;
            // Enumerators are also exported by their enclosing type declaration.
            Match enumeration = Regex.Match(code, @"\benum\s*\w*\s*\{(?<body>[^}]*)\}");
            if (enumeration.Success)
                foreach (string entry in enumeration.Groups["body"].Value.Split(','))
                {
                    Match name = Regex.Match(entry, @"^\s*([A-Za-z_]\w*)");
                    if (name.Success) yield return name.Groups[1].Value;
                }
            // Multiple objects/aliases can share one declaration. Ignore commas inside
            // initializers, parameter lists, and array bounds when identifying them.
            int start = 0, depth = 0;
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == '(' || code[i] == '[' || code[i] == '{') depth++;
                if (code[i] == ')' || code[i] == ']' || code[i] == '}') depth--;
                if (depth == 0 && (code[i] == ',' || code[i] == ';'))
                {
                    yield return DeclarationName(code.Substring(start, i - start));
                    start = i + 1;
                }
            }
        }

        internal static List<Declaration> Parse(string text)
        {
            var result = new List<Declaration>();
            var conditions = new List<string>();
            char[] code = Mask(text).ToCharArray();
            int start = -1, braces = 0, parens = 0, brackets = 0;
            bool function = false;
            string signature = "", functionName = "";
            for (int i = 0; i < code.Length; i++)
            {
                char c = code[i];
                // Directives can occur inside declarations too; they must not affect depth.
                if (c == '#' && (i == 0 || new string(code, text.LastIndexOf('\n', i) + 1,
                    i - text.LastIndexOf('\n', i) - 1).Trim().Length == 0))
                {
                    int end = i;
                    do
                    {
                        int newline = text.IndexOf('\n', end);
                        end = newline < 0 ? text.Length : newline + 1;
                    }
                    while (end < text.Length && text.Substring(i, end - i).TrimEnd('\r', '\n').EndsWith("\\"));
                    string directive = text.Substring(i, end - i).Trim();
                    string command = Regex.Match(new string(code, i, end - i), @"#\s*(\w+)").Groups[1].Value;
                    if (start < 0)
                    {
                        bool conditional = command == "if" || command == "ifdef" || command == "ifndef" ||
                            command == "elif" || command == "else" || command == "endif";
                        if (conditional)
                            result.Add(new Declaration { Kind = "condition", Name = directive, Text = directive,
                                Code = "", Conditions = string.Concat(conditions), ConditionCount = conditions.Count, Start = i, End = end });
                        if (command == "if" || command == "ifdef" || command == "ifndef") conditions.Add(directive + "\n");
                        else if (command == "elif" || command == "else")
                        {
                            if (conditions.Count == 0) throw new FormatException("Unmatched #" + command);
                            conditions[conditions.Count - 1] += directive + "\n";
                        }
                        else if (command == "endif")
                        {
                            if (conditions.Count == 0) throw new FormatException("Unmatched #endif");
                            conditions.RemoveAt(conditions.Count - 1);
                        }
                        else
                        {
                            result.Add(new Declaration { Kind = "directive", Name = directive, Text = directive,
                                Code = "", Conditions = string.Concat(conditions), ConditionCount = conditions.Count, Start = i, End = end });
                        }
                    }
                    for (int j = i; j < end; j++) if (code[j] != '\n' && code[j] != '\r') code[j] = ' ';
                    i = end - 1;
                    continue;
                }
                if (start < 0)
                {
                    if (char.IsWhiteSpace(c)) continue;
                    start = i;
                }
                if (c == '(') parens++;
                if (c == ')') parens--;
                if (c == '[') brackets++;
                if (c == ']') brackets--;
                if (c == '{')
                {
                    if (braces == 0 && parens == 0 && brackets == 0)
                    {
                        string header = new string(code, start, i - start);
                        functionName = FunctionName(header);
                        function = functionName.Length > 0 && !Regex.IsMatch(header, @"\btypedef\b");
                        if (function) signature = text.Substring(start, i - start).Trim();
                    }
                    braces++;
                }
                if (c == '}') braces--;
                if ((c == ';' || (c == '}' && function)) && braces == 0 && parens == 0 && brackets == 0)
                {
                    string parsed = new string(code, start, i - start + 1);
                    bool type = Regex.IsMatch(parsed, @"^\s*typedef\b") ||
                        Regex.IsMatch(parsed, @"^\s*(struct|union|enum)\s+\w*\s*\{") &&
                        parsed.Substring(parsed.LastIndexOf('}') + 1).Trim() == ";";
                    string prototypeName = FunctionName(parsed);
                    bool prototype = !function && !type && prototypeName.Length > 0 && !parsed.Contains("{") &&
                        !Regex.IsMatch(parsed, @"\(\s*\*");
                    string name = function ? functionName : prototype ? prototypeName : DeclarationName(parsed);
                    if (name != "Asm_VanillaHook")
                        result.Add(new Declaration { Kind = function ? "function" : type ? "type" : prototype ? "prototype" : "variable",
                            Name = name, Text = text.Substring(start, i - start + 1), Code = parsed,
                            Signature = signature, Conditions = string.Concat(conditions), ConditionCount = conditions.Count, Start = start, End = i + 1 });
                    start = -1;
                    function = false;
                    signature = "";
                }
            }
            if (start >= 0 || braces != 0 || parens != 0 || brackets != 0 || conditions.Count != 0)
                throw new FormatException("Unterminated C declaration or preprocessor condition.");
            return result;
        }

        private static string RenameVariables(string text, Dictionary<string, string> names)
        {
            string code = Mask(text);
            // Struct members have their own namespace, including inline/local structs.
            char[] identifiers = code.ToCharArray();
            foreach (Match aggregate in Regex.Matches(code, @"\b(?:struct|union)\s*(?:[A-Za-z_]\w*\s*)?\{"))
            {
                int depth = 1;
                for (int i = aggregate.Index + aggregate.Length; i < code.Length && depth > 0; i++)
                {
                    if (code[i] == '{') depth++;
                    if (code[i] == '}') depth--;
                    identifiers[i] = ' ';
                }
            }
            var replacements = new List<Match>();
            foreach (Match token in Regex.Matches(new string(identifiers), @"\b[A-Za-z_]\w*\b"))
            {
                if (!names.ContainsKey(token.Value)) continue;
                string before = code.Substring(0, token.Index).TrimEnd();
                if (before.EndsWith(".") || before.EndsWith("->")) continue;
                replacements.Add(token);
            }
            var output = new StringBuilder(text);
            foreach (Match token in replacements.AsEnumerable().Reverse())
                output.Remove(token.Index, token.Length).Insert(token.Index, names[token.Value]);
            return output.ToString();
        }

        private static string RefactorVariables(string text, Dictionary<string, string> names)
        {
            // Rewrite code tokens only; preserve strings, comments, includes and member
            // declarations. References inside function bodies and macro bodies follow
            // the same rename as the file-scope declaration.
            var result = new StringBuilder(text);
            foreach (Declaration d in Parse(text).AsEnumerable().Reverse())
            {
                if (d.Kind == "type" || d.Kind == "condition" ||
                    d.Kind == "directive" && Regex.IsMatch(d.Text, @"^#\s*include\b")) continue;
                result.Remove(d.Start, d.End - d.Start).Insert(d.Start,
                    RenameVariables(text.Substring(d.Start, d.End - d.Start), names));
            }
            return result.ToString();
        }

        private sealed class SectionItem
        {
            public string Text;
            public int Section;
            public List<Declaration> Declarations;
        }

        private static int Section(Declaration d)
        {
            if (d.Kind == "directive") return Regex.IsMatch(d.Text, @"^#\s*include\b") ? 0 : 1;
            if (d.Kind == "prototype") return 2;
            if (d.Kind == "type") return 3;
            if (d.Kind == "variable") return 4;
            return 5;
        }

        private static string OrderSections(string text)
        {
            List<Declaration> declarations = Parse(text);
            var items = new List<SectionItem>();
            int previousEnd = 0;
            for (int i = 0; i < declarations.Count; i++)
            {
                Declaration first = declarations[i];
                int end = i;
                // A conditional block or a temporary macro's lifetime is indivisible:
                // sorting its contents independently would change the generated C.
                if (first.Kind == "condition")
                {
                    int depth = 0;
                    for (; end < declarations.Count; end++)
                    {
                        Declaration d = declarations[end];
                        if (d.Kind != "condition") continue;
                        if (Regex.IsMatch(d.Text, @"^#\s*(if|ifdef|ifndef)\b")) depth++;
                        if (Regex.IsMatch(d.Text, @"^#\s*endif\b") && --depth == 0) break;
                    }
                }
                else if (first.Kind == "directive")
                {
                    Match define = Regex.Match(first.Text, @"^#\s*define\s+([A-Za-z_]\w*)");
                    if (define.Success)
                        for (int j = i + 1; j < declarations.Count; j++)
                            if (Regex.IsMatch(declarations[j].Text, @"^#\s*undef\s+" + Regex.Escape(define.Groups[1].Value) + @"\b"))
                            {
                                end = j;
                                break;
                            }
                }
                int section = declarations.Skip(i).Take(end - i + 1).Where(d => d.Kind != "condition").Select(Section).DefaultIfEmpty(1).Max();
                items.Add(new SectionItem { Section = section, Text = text.Substring(previousEnd, declarations[end].End - previousEnd).Trim(),
                    Declarations = declarations.Skip(i).Take(end - i + 1).ToList() });
                previousEnd = declarations[end].End;
                i = end;
            }
            string trailing = text.Substring(previousEnd).Trim();
            // C requires typedefs used in signatures to precede those prototypes. Move
            // only such prerequisites ahead of the requested section ordering.
            var ordered = new List<SectionItem>();
            var emitted = new HashSet<SectionItem>();
            var visiting = new HashSet<SectionItem>();
            Action<SectionItem> append = null;
            append = item =>
            {
                if (emitted.Contains(item) || !visiting.Add(item)) return;
                if (item.Section == 2 || item.Section == 3)
                {
                    var references = References(Mask(item.Text), declarations);
                    foreach (SectionItem type in items.Where(t => t.Section == 3 && t != item))
                        if (type.Declarations.Where(d => d.Kind == "type").SelectMany(Symbols).Any(references.Contains)) append(type);
                }
                visiting.Remove(item);
                emitted.Add(item);
                ordered.Add(item);
            };
            // LINQ's stable ordering preserves every existing function's relative order.
            foreach (SectionItem item in items.OrderBy(item => item.Section)) append(item);
            var output = new StringBuilder();
            SectionItem previous = null;
            foreach (SectionItem item in ordered)
            {
                if (previous != null) output.Append(previous.Section == item.Section && item.Section < 3 ? "\n" : "\n\n");
                output.Append(item.Text);
                previous = item;
            }
            return output + (trailing.Length == 0 ? "\n" : "\n" + trailing + "\n");
        }

        internal static string Copy(string source, string destination, string requested, IEnumerable<string> vanillaFunctions)
        {
            List<Declaration> declarations = Parse(source);
            List<Declaration> existing = Parse(destination);
            var globals = new HashSet<string>(vanillaFunctions, StringComparer.Ordinal);
            if (!declarations.Any(d => d.Kind == "function" && d.Name == requested))
                throw new FormatException("Could not find the definition of " + requested + ".");
            if (existing.Any(d => d.Kind == "function" && d.Name == requested))
                throw new FormatException("The destination already defines " + requested + ".");

            // Start exclusively from the hook, then traverse types, data, macros and
            // local functions reached by its references. Unrelated declarations must
            // never become roots merely because they exist in the source file.
            var symbols = new Dictionary<string, List<Declaration>>(StringComparer.Ordinal);
            foreach (Declaration d in declarations)
                foreach (string name in Symbols(d).Distinct())
                {
                    List<Declaration> definitions;
                    if (!symbols.TryGetValue(name, out definitions)) symbols.Add(name, definitions = new List<Declaration>());
                    definitions.Add(d);
                }
            var selected = new HashSet<Declaration>();
            var pending = new Queue<string>();
            var visited = new HashSet<string>(StringComparer.Ordinal);
            Action<Declaration> select = d =>
            {
                if (!selected.Add(d)) return;
                foreach (string reference in References(Mask(d.Text + "\n" + d.Conditions), declarations)) pending.Enqueue(reference);
            };
            foreach (Declaration d in declarations.Where(d => d.Kind == "function" && d.Name == requested)) select(d);
            while (pending.Count > 0)
            {
                string reference = pending.Dequeue();
                if (!visited.Add(reference)) continue;
                List<Declaration> definitions;
                if (!symbols.TryGetValue(reference, out definitions)) continue;
                foreach (Declaration d in definitions)
                {
                    if (d.Kind == "function" && globals.Contains(d.Name) && d.Name != requested)
                    {
                        // Only the signature's types are dependencies of an external call.
                        foreach (string name in References(Mask(d.Signature + "\n" + d.Conditions), declarations)) pending.Enqueue(name);
                    }
                    else select(d);
                }
            }

            // Header contents are provided by the build environment. Keep header
            // includes, but never pull in another translation unit through an include.
            foreach (Declaration d in declarations.Where(d => d.Kind == "directive"))
            {
                Match include = Regex.Match(Mask(d.Text, false), @"^#\s*include\s*[<""]([^>""]+)[>""]");
                if (include.Success && !include.Groups[1].Value.EndsWith(".c", StringComparison.OrdinalIgnoreCase)) selected.Add(d);
            }
            // Retain only conditional blocks containing selected declarations, keeping
            // their #elif/#else chain intact so alternatives remain mutually exclusive.
            var conditionStack = new Stack<List<Declaration>>();
            foreach (Declaration d in declarations)
            {
                if (d.Kind == "condition")
                {
                    string command = Regex.Match(d.Text, @"^#\s*(\w+)").Groups[1].Value;
                    if (command == "if" || command == "ifdef" || command == "ifndef") conditionStack.Push(new List<Declaration>());
                    conditionStack.Peek().Add(d);
                    if (command == "endif")
                    {
                        List<Declaration> block = conditionStack.Pop();
                        if (block.Any(selected.Contains))
                        {
                            foreach (Declaration condition in block.Where(c => c.Kind == "condition")) selected.Add(condition);
                            if (conditionStack.Count > 0) conditionStack.Peek().Add(d);
                        }
                    }
                }
                else if (conditionStack.Count > 0 && selected.Contains(d)) conditionStack.Peek().Add(d);
            }

            var renames = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (Declaration d in selected.Concat(existing).Where(d => d.Kind == "variable" && !Regex.IsMatch(d.Code, @"^\s*extern\b")))
                foreach (string name in Symbols(d).Distinct())
                    if (!name.EndsWith("Custom", StringComparison.Ordinal)) renames[name] = name + "Custom";
            destination = RefactorVariables(destination, renames);
            existing = Parse(destination);
            // Compare identities after renaming, so later hooks reuse existing data.
            foreach (Declaration d in declarations.Where(selected.Contains))
            {
                if (d.Kind == "type" || d.Kind == "condition" || d.Kind == "directive" && Regex.IsMatch(d.Text, @"^#\s*include\b")) continue;
                d.Text = RenameVariables(d.Text, renames);
                d.Code = Mask(d.Text);
                if (renames.ContainsKey(d.Name)) d.Name = renames[d.Name];
            }
            var keys = new HashSet<string>(existing.Select(d => d.Key), StringComparer.Ordinal);
            var output = new StringBuilder(destination);
            if (output.Length > 0) output.AppendLine();
            if (!existing.Any(d => d.Kind == "directive" && Regex.IsMatch(d.Text, @"#\s*include\s*[<""]uLib\.h[>""]")))
                output.AppendLine("#include <uLib.h>");

            // Preserve directive and declaration ordering, including #undef and include
            // guards. Insert forward declarations at first use, before data callbacks or
            // mutually recursive functions that need them.
            var prototypes = new HashSet<string>(existing.Where(d => d.Kind == "prototype" || d.Kind == "function")
                .Select(d => d.Name + "|" + d.Conditions), StringComparer.Ordinal);
            var forwards = new HashSet<string>(StringComparer.Ordinal);
            foreach (Declaration d in declarations)
            {
                if (!selected.Contains(d)) continue;
                if (d.Kind == "condition")
                {
                    output.AppendLine(d.Text);
                    continue;
                }
                if (d.Kind == "function")
                {
                    if (keys.Contains(d.Key)) continue;
                }
                else if (d.Kind == "directive")
                {
                    // Includes may be shared, but #define/#undef and pragmas are ordered
                    // operations. Replaying them keeps macro lifetimes intact on import.
                    if (Regex.IsMatch(d.Text, @"^#\s*include\b") && !keys.Add(d.Key)) continue;
                }
                else if (!keys.Add(d.Key)) continue;

                if (d.Kind == "function" || d.Kind == "variable")
                {
                    var references = References(Mask(d.Text), declarations);
                    foreach (Declaration helper in declarations.Where(h => h.Kind == "function" && references.Contains(h.Name)))
                    {
                        if (helper.Name == d.Name || (!selected.Contains(helper) && !globals.Contains(helper.Name))) continue;
                        if (!prototypes.Contains(helper.Name + "|") && !prototypes.Contains(helper.Name + "|" + d.Conditions) &&
                            forwards.Add(helper.Name + "|" + d.Conditions + "|" + helper.Conditions))
                            output.Append(helper.Wrap(helper.Signature + ";"));
                    }
                }
                if (d.Kind == "function" && d.Name == requested) output.AppendLine("Asm_VanillaHook(" + requested + ");");
                output.AppendLine(d.Text.Trim());
                if (d.Kind == "prototype" || d.Kind == "function") prototypes.Add(d.Name + "|" + d.Conditions);
            }
            return OrderSections(output.ToString());
        }
    }
    #endregion

}
