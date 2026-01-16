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
                //MessageBox.Show("Directory " + rom64.getPath() + "\\z64oot\\src\\" + " doesn't exists! download z64oot decomp project from github and place it in that path!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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
           // string functionstext = File.ReadAllText(functionsh);

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

            /*
            var vanillafunctionSource = new AutoCompleteStringCollection();
            vanillafunctionSource.AddRange(VanillaFunctions.Select(h => h.Name).ToArray());
            FunctionNameTextbox.AutoCompleteCustomSource = vanillafunctionSource;
            */
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
                            //HookListBox.Items.Add("[U] " + Path.GetFileName(file));
                            target[i].Add(file);
                            string[] lines = File.ReadAllLines(file);
                            bool inMultiLineComment = false;
                            
                            foreach (string rawline in lines)
                            {
                                string line = rawline.Trim();
                                if (line.StartsWith("/*"))
                                {
                                    inMultiLineComment = true;
                                }
                                if (inMultiLineComment)
                                {
                                    if (line.Contains("*/"))
                                    {
                                        inMultiLineComment = false;
                                        line = line.SubstringTill(0, "*/");
                                    }
                                    else continue;
                                }
                                if (line.StartsWith("//"))
                                {
                                    continue;
                                }
                                if (line.Contains("//"))
                                    line = line.SubstringTill(0, "//");

                                if (line.Contains("Asm_VanillaHook"))
                                {
                                    string funcname = line.SubstringTill(line.IndexOf('(')+1, ')');
                                    FunctionHooks.Add(new FunctionHook(funcname, file, false));
                                }

                                
                            }
                        }
                        else if (i == 1 && Path.GetExtension(file) == ".o")
                        {
                            FunctionHooks.Add(new FunctionHook(Path.GetFileNameWithoutExtension(file), file.Replace("\\rom\\","\\src\\").Replace(".o",".c"), true));
                        }
                }
                
            }
            /*
            foreach(FunctionHook hook in FunctionHooks)
            {
                HookListBox.Items.Add((hook.isLibCode ? "[C] " : "[U] ") + hook.Name + " - " + Path.GetFileName(hook.FileName));
            }*/
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

                string srcpath = basepath + "z64oot\\src\\";


                int index2 = VanillaFunctions.FindIndex(x => x.Name == functionname);
                if (index2 == -1)
                {
                    if (warning) MessageBox.Show("This function doesn't exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DebugConsole.WriteLine("Function " + functionname + " doesn't exists!");
                    return "";
                }
                List<CFile> Files = new List<CFile>();
                CFile vanillafile = new CFile();
                vanillafile.FileName = VanillaFunctions[index2].FileName;
                vanillafile.includes.Add(new CData("uLib.h", "#include <uLib.h>"));
                Files.Add(vanillafile);

                CFile targetfile = new CFile();

                if (FileNameOriginalRadioButton.Checked)
                {
                    targetfile.FileName = basepath + "src\\lib_user\\library\\" + Path.GetFileName(vanillafile.FileName);
                }
                else if (FileNameFunctionRadioButton.Checked)
                {
                    targetfile.FileName = basepath + "src\\lib_user\\library\\" + functionname + ".c";
                }
                else
                {
                    targetfile.FileName = basepath + "src\\lib_user\\library\\" + CustomFileName.Text + (CustomFileName.Text.Contains(".c") ? "" : ".c");
                }
                if (!File.Exists(targetfile.FileName))
                {
                    using (File.Create(targetfile.FileName)) ;
                }

                Files.Add(targetfile);
                bool inMultiLineComment = false;
                bool inStruct = false;
                bool inVariable = false;
                bool inFunction = false;
                bool inPredeclaration = false;
                int block = 0;
                foreach (CFile file in Files)
                {
                    string text = File.ReadAllText(file.FileName);
                    text = Regex.Replace(text, @"(?m)^((?:(?:(?!//).)*)),\r?\n", "$1@#?");
                    text = Regex.Replace(text, @"(?m)^((?:(?:(?!//).)*))(?<!\*)\\\r?\n", "$1@#!");
                    var lineslist = new List<string>();
                    using (var reader = new StringReader(text))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                            lineslist.Add(line);
                    }
                    string[] lines = lineslist.ToArray();
                    inMultiLineComment = false;
                    inStruct = false;
                    inVariable = false;
                    inFunction = false;
                    inPredeclaration = false;
                    block = 0;
                    int cline = 1;
                    List<string> references = new List<string>();
                    foreach (string rawline in lines)
                    {
                        if (warning) DebugConsole.WriteLine($"{cline} - inVariable: {inVariable}, inFunction: {inFunction}, inStruct: {inStruct} ,inMultiLineComment: {inMultiLineComment}, inPredeclaration: {inPredeclaration}, block {block}");
                        cline++;
                        string line = rawline.Trim();
                        if (line.StartsWith("/*"))
                        {
                            inMultiLineComment = true;
                        }
                        if (inMultiLineComment)
                        {
                            if (line.Contains("*/"))
                            {
                                inMultiLineComment = false;
                                line = line.SubstringTill(0, "*/");
                            }
                            else continue;
                        }
                        if (line.StartsWith("//"))
                        {
                            continue;
                        }
                        if (line.Contains("//"))
                            line = line.SubstringTill(0, "//");

                        if (line.Contains("Asm_VanillaHook"))
                        {
                            continue;
                        }

                        if (inStruct)
                        {
                            file.structs[file.structs.Count - 1].line += "\n" + rawline;
                            if (inMultiLineComment)
                            {
                                continue;
                            }
                            if (line.Contains("}") && line.Contains(";"))
                            {
                                inStruct = false;
                            }
                            continue;
                        }

                        if (inVariable)
                        {
                            file.variables[file.variables.Count - 1].line += "\n" + rawline;
                            if (inMultiLineComment)
                            {
                                continue;
                            }
                            if (line.Contains("}") && line.Contains(";"))
                            {
                                inVariable = false;
                            }
                            continue;
                        }

                        if (inFunction)
                        {
                            int prevblock = block;
                            file.functions[file.functions.Count - 1].line += "\n" + rawline;
                            if (inMultiLineComment)
                            {
                                continue;
                            }
                            //lets find the references
                            if (line.Contains("("))
                            {
                                string testline = line.Replace(")", ") ");
                                string[] testsplit = testline.Split(' ');
                                foreach (string split in testsplit)
                                {
                                    if (split.Contains("("))
                                    {
                                        string reference = split.SubstringTill(0, '(');
                                        if (VanillaFunctions.FindIndex(x => x.Name == reference) != -1)
                                        {
                                            references.Add(reference);
                                        }

                                    }
                                }
                            }


                            block += line.Count(x => x == '{') - line.Count(x => x == '}');
                            if (block == 0 && prevblock != 0)
                            {
                                inFunction = false;
                            }
                            continue;
                        }

                        if (line.Contains("#include"))
                        {
                            string include = line.Contains('"') ? line.Substring(line.IndexOf('"') + 1, line.LastIndexOf('"') - (line.IndexOf('"') + 1))
                            : line.Substring(line.IndexOf('<') + 1, line.LastIndexOf('>') - (line.IndexOf('<') + 1));
                            file.includes.Add(new CData(include, rawline));
                        }
                        else if (line.Contains("#define"))
                        {
                            string define = line.Replace("#define", "").TrimStart().SubstringTill(0, ' ');
                            file.defines.Add(new CData(define, rawline));
                        }
                        else if (line.Contains("typedef"))
                        {
                            inStruct = true;
                            file.structs.Add(new CData("", rawline));
                        }
                        else if (line.Contains(");") && line.SubstringTill(0, '(').Contains(" ") && !line.Contains("=")) 
                        {
                            string predeclaration = line.Substring(line.IndexOf(' ') + 1, line.IndexOf('(') - line.IndexOf(' ') - 1);
                            while (predeclaration.Contains(' '))
                                predeclaration = predeclaration.Substring(predeclaration.IndexOf(' ') + 1).Trim();
                            file.predeclarations.Add(new CData(predeclaration, rawline));
                        }
                        else if (line.Contains("="))
                        {
                            if (!line.Contains("};") && !line.EndsWith(";")) inVariable = true;
                            string variable = line.SubstringTill(line.IndexOf(' ') + 1, '=').Trim();
                            while (variable.Contains(' '))
                                variable = variable.Substring(variable.IndexOf(' ') + 1).Trim();
                            //string variable = line.Substring(line.IndexOf(' ') + 1).Split(' ')[1];
                            file.variables.Add(new CData(variable, rawline));
                        }
                        else if ((line.Contains("{") || line.EndsWith(",")) && line.Contains("("))
                        {
                            inFunction = true;
                            string function = line.SubstringTill(line.IndexOf(' ') + 1, '(');
                            while (function.Contains(' '))
                                function = function.Substring(function.IndexOf(' ') + 1).Trim();
                            file.functions.Add(new CData(function, rawline));
                            block = line.Contains("{") ? 1 : 0;
                        }
                    }
                    foreach (string reference in references)
                    {
                        CData targetfunc = file.functions.Find(x => x.name == reference);
                        CData predeclaration = file.predeclarations.Find(x => x.name == reference);
                        //TODO check if predeclaration exists in other files?
                        if (predeclaration == null && targetfunc != null)
                        {
                            file.predeclarations.Add(new CData(reference, targetfunc.line.SubstringTill(0, ')') + ");"));
                        }
                    }
                }
                vanillafile = Files[0];
                targetfile = Files[1];
                string output = File.ReadAllText(targetfile.FileName);
                List<CData>[] sourcelists = { vanillafile.includes, vanillafile.defines, vanillafile.predeclarations, vanillafile.structs, vanillafile.variables, vanillafile.functions };
                List<CData>[] targetlists = { targetfile.includes, targetfile.defines, targetfile.predeclarations, targetfile.structs, targetfile.variables, targetfile.functions };
                bool hookset = false;
                for (int i = 0; i < sourcelists.Length; i++)
                {
                    foreach (CData include in sourcelists[i])
                    {
                        if (i == 5 && include.name.Contains("BgCheck_CheckWallImpl"))
                        {
                            int a = 0;
                        }
                        if (i != 5 || (i == 5 && include.name == functionname))
                            if (targetlists[i].FindIndex(x => x.name == include.name) == -1)
                            {
                                if (i == 5)
                                {
                                    output += "Asm_VanillaHook(" + include.name + ");\n";
                                    hookset = true;
                                }
                                output += include.line + "\n";
                            }
                    }
                    output += "\n";
                }

                if (!hookset)
                {
#if !DEBUG

                    MessageBox.Show("An error occured and the hook has not been set! " + functionname + " in " + vanillafile.FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    DebugConsole.WriteLine(functionname + $" has NOT been set! inVariable: {inVariable}, inFunction: {inFunction}, inStruct: {inStruct} ,inMultiLineComment: {inMultiLineComment}, block {block}");
                }
                else
                {
                    DebugConsole.WriteLine(functionname + " +");
                    File.WriteAllText(targetfile.FileName, output.Replace("@#?",",\n").Replace("@#!", "\\\n"));
                    return targetfile.FileName;
                }
                return "";

                


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

                            string[] lines = File.ReadAllLines(file);
                            string text = File.ReadAllText(file);
                            bool inMultiLineComment = false;

                            foreach (string rawline in lines)
                            {
                                string line = rawline.Trim();
                                if (line.StartsWith("/*"))
                                {
                                    inMultiLineComment = true;
                                }
                                if (inMultiLineComment)
                                {
                                    if (line.Contains("*/"))
                                    {
                                        inMultiLineComment = false;
                                        line = line.SubstringTill(0, "*/");
                                    }
                                    else continue;
                                }
                                if (line.StartsWith("//"))
                                {
                                    continue;
                                }
                                if (line.Contains("//"))
                                    line = line.SubstringTill(0, "//");

                                if (line.Contains("#include") && line.Contains("assets/overlays/"))
                                {
                                    string include = line.Contains('"') ? line.Substring(line.IndexOf('"') + 1, line.LastIndexOf('"') - (line.IndexOf('"') + 1))
                                                                        : line.Substring(line.IndexOf('<') + 1, line.LastIndexOf('>') - (line.IndexOf('<') + 1));
                                    includes.Add(new CData(include, rawline));
                                }
                                
                            }
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
                                        string includepath = include.line.Replace("#include", "").Replace("\"", "").Trim();
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

                        /*
                        if (File.Exists(newpath + "\\z_" + target.DebugName + ".c"))
                        {
                            if (MessageBox.Show("Done! created directory in " + newpath + "\nOpen .c file?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                Process.Start(newpath + "\\z_" + target.DebugName + ".c");
                            }
                        }
                        else
                        {*/
                        if (MessageBox.Show("Done! created directory in " + newpath + "\nOpen directory in explorer?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Process.Start(newpath);
                        }
                        //}


                        

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


            //Debug Name add

            /*
            string[] lines = File.ReadAllLines(@"Z:\rawactortableMM");
            Dictionary<string,string> DebugNames = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                DebugNames.Add(line.Split('\t')[0], line.Split('\t')[1]);
            }

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML/MM/ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Actor");

            foreach (XmlNode node in nodes)
            {

                if (node is XmlElement element && element.HasAttribute("Name"))
                {
                    XmlAttributeCollection attributes = element.Attributes;
                    XmlAttribute nameAttr = attributes["Name"];

                    if (!DebugNames.ContainsKey(attributes["Key"].Value)) continue;

                    string DebugName = DebugNames[attributes["Key"].Value];

                    XmlAttribute newAttr = doc.CreateAttribute("DebugName");
                    newAttr.Value = DebugName;

                    attributes.InsertAfter(newAttr, nameAttr);
                }

            }

            doc.Save(@"Z:\ActorNames.xml");
            */


            /*
            // Hook all functions
            foreach(FunctionHook func in VanillaFunctions)
            {
                HookFunction(func.Name);
            }*/
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


}
