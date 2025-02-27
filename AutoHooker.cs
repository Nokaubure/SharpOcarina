using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace SharpOcarina
{


    public partial class AutoHookerForm : Form
    {

        public List<FunctionHook> FunctionHooks = new List<FunctionHook>();
        public List<string> LibUserFileList = new List<string>();
        public List<string> LibCodeFileList = new List<string>();
        public List<FunctionHook> VanillaFunctions = new List<FunctionHook>();
        public List<HookActor> VanillaActors = new List<HookActor>();
        public bool descending;
        public MainForm mainform;
        public AutoHookerForm(MainForm mainform)
        {
            this.mainform = mainform;
            InitializeComponent();
            HookGrid.AutoGenerateColumns = false;

            string linker = rom64.getPath() + "\\include\\z64hdr\\oot_mq_debug\\sym_src.ld";

            if (!File.Exists(linker))
            {
                MessageBox.Show(linker + "Doesn't exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!Directory.Exists(rom64.getPath() + "\\z64oot\\src\\"))
            {
                MessageBox.Show("Directory " + rom64.getPath() + "\\z64oot\\src\\" + " doesn't exists! download z64oot decomp project from github and place it in that path!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
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
                        string str = line.SubstringTill(0, '='); //TODO check
                        VanillaFunctions.Add(new FunctionHook(str.Trim(),fileName,false));
                    }
                }


            }
        

            RefreshLibUser();

            RefreshVanillaActorList();

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
                                    if (line.EndsWith("*/"))
                                    {
                                        inMultiLineComment = false;
                                    }
                                    continue;
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
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/OOT/ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
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
            VanillaActorList.DataSource = VanillaActors;
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
            int index = FunctionHooks.FindIndex(x => x.Name.ToLower() == FunctionNameTextbox.Text.ToLower());
            if (index != -1)
            {
                MessageBox.Show("This function is already hooked in file: " + FunctionHooks[index].Name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                string basepath = rom64.getPath() + Path.DirectorySeparatorChar;

                string srcpath = basepath + "z64oot\\src\\";

                /*
                List<string> FileList = new List<string>();

                foreach (string f in Directory.GetDirectories(srcpath))
                {
                    FileList.Add(f);
                }*/

                int index2 = VanillaFunctions.FindIndex(x => x.Name.ToLower() == FunctionNameTextbox.Text.ToLower());
                if (index2 == -1)
                {
                    MessageBox.Show("This function doesn't exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                List<CFile> Files = new List<CFile>();
                CFile vanillafile = new CFile();
                vanillafile.FileName = VanillaFunctions[index2].FileName;
                vanillafile.includes.Add(new CData("<uLib.h>", "#include <uLib.h>"));
                Files.Add(vanillafile);

                CFile targetfile = new CFile();

                if (FileNameOriginalRadioButton.Checked)
                {
                    targetfile.FileName = basepath + "src\\lib_user\\library\\" + Path.GetFileName(vanillafile.FileName);
                }
                else if (FileNameFunctionRadioButton.Checked)
                {
                    targetfile.FileName = basepath + "src\\lib_user\\library\\" + FunctionNameTextbox.Text + ".c";
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

                foreach (CFile file in Files)
                {
                    string[] lines = File.ReadAllLines(file.FileName);
                    bool inMultiLineComment = false;
                    bool inStruct = false;
                    bool inVariable = false;
                    bool inFunction = false;
                    int block = 0;
                    foreach (string rawline in lines)
                    {
                        string line = rawline.Trim();
                        if (line.StartsWith("/*"))
                        {
                            inMultiLineComment = true;
                        }
                        if (inMultiLineComment && !inStruct && !inVariable && !inFunction)
                        {
                            if (line.EndsWith("*/"))
                            {
                                inMultiLineComment = false;
                            }
                            continue;
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
                            file.functions[file.functions.Count - 1].line += "\n" + rawline;
                            if (inMultiLineComment)
                            {
                                continue;
                            }
                            block += line.Count(x => x == '{') - line.Count(x => x == '}');
                            if (block == 0)
                            {
                                inFunction = false;
                            }
                            continue;
                        }

                        if (line.Contains("#include"))
                        {
                            string include = line.Substring(line.IndexOf('"') + 1, line.LastIndexOf('"') - (line.IndexOf('"') + 1));
                            file.includes.Add(new CData(include,rawline));
                        }
                        else if (line.Contains("#define"))
                        {
                            string define = line.Replace("#define", "").TrimStart().SubstringTill(0, ' ');
                            file.defines.Add(new CData(define, rawline));
                        }
                        else if (line.Contains("typedef"))
                        {
                            inStruct = true;
                            file.structs.Add(new CData("",rawline));
                        }
                        else if (line.Contains(");"))
                        {
                            string predeclaration = line.Substring(line.IndexOf(' ') + 1, line.IndexOf('('));
                            file.predeclarations.Add(new CData(predeclaration, rawline));
                        }
                        else if (line.Contains("="))
                        {
                            if (!line.Contains("};")) inVariable = true;
                            string variable = line.Substring(line.IndexOf(' ') + 1).Split(' ')[1];
                            file.variables.Add(new CData(variable, rawline));
                        }
                        else if (line.Contains("{") && line.Contains("("))
                        {
                            inFunction = true;
                            string function = line.SubstringTill(line.IndexOf(' ') + 1,'(');
                            file.functions.Add(new CData(function, rawline));
                            block = 1;
                        }
                    }
                }
                vanillafile = Files[0];
                targetfile = Files[1];
                string output = "";
                List<CData>[] sourcelists = { vanillafile.includes, vanillafile.defines, vanillafile.predeclarations, vanillafile.structs, vanillafile.variables, vanillafile.functions };
                List<CData>[] targetlists = { targetfile.includes, targetfile.defines, targetfile.predeclarations, targetfile.structs, targetfile.variables, targetfile.functions };

                for (int i = 0; i < sourcelists.Length; i++)
                {
                    foreach (CData include in sourcelists[i])
                    {
                        if (i != 5 || (i == 5 && include.name.ToLower() == FunctionNameTextbox.Text.ToLower()))
                            if (targetlists[i].FindIndex(x => x.name == include.name) == -1)
                            {
                                if (i == 5)
                                    output += "Asm_VanillaHook(" + include.name + ");\n";
                                output += include.line + "\n";
                            }
                    }
                    output += "\n";
                }

                File.WriteAllText(targetfile.FileName,output);

                RefreshLibUser();

                if (MessageBox.Show("Done! open file containing the hook?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    OpenFile(targetfile.FileName);
                }

            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile(string FileName = "")
        {
            if (FunctionHooks.Count != 0)
            {
                if (FileName == "")
                {
                    int index = HookGrid.CurrentRow.Index;
                    if (index > -1 && index < FunctionHooks.Count)
                        index = 0;
                    System.Diagnostics.Process.Start(FunctionHooks[index].FileName);
                }
                else
                {
                    System.Diagnostics.Process.Start(FileName);
                }
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

        }


        /*
        Debug Name add
        string[] lines = File.ReadAllLines(@"Z:\rawactortable");
        Dictionary<string,string> DebugNames = new Dictionary<string, string>();
        foreach (string line in lines)
        {
            DebugNames.Add(line.Split('\t')[0], line.Split('\t')[1]);
        }

        XmlDocument doc = new XmlDocument();
        var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/OOT/ActorNames.xml");
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
