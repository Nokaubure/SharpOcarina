﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenTK.Input;
using System.IO;
using System.Globalization;
using System.Net;
using System.Reflection;
using Ionic.Zip;
using Tommy;

namespace SharpOcarina
{
    public partial class CustomActorDatabase : Form
    {
        string[] actor_categories = { "Puzzle", "Enemy", "Boss", "NPC", "Utility", "Other", "Dependency" };
        //string path = "Z:\\Users\\Noka\\Documents\\GitHub\\CustomActorDatabase\\";
        string website = "https://raw.githubusercontent.com/Nokaubure/CustomActorDatabase/";
        public List<DatabaseCustomActor> Database;
        public XmlNodeList nodes;
        public string filter = "";
        string tempw = "";
        public MainForm mainform;
        public WebClient client;
        public List<CustomActorz64rom> z64romactors = new List<CustomActorz64rom>();
        public List<CustomObjectz64rom> z64romobjects = new List<CustomObjectz64rom>();
        public bool reload = false;
        public List<CustomActorNode> CustomActorNodes = new List<CustomActorNode>();
        public CustomActorDatabase(MainForm _mainform)
        {
            mainform = _mainform;
            InitializeComponent();
            Init();
            UpdateWindow();
        }

        public void Init()
        {

            foreach (string category in actor_categories)
            {
                ToolStripMenuItem MenuItem = new System.Windows.Forms.ToolStripMenuItem() { Name = category, Text = category };

                MenuItem.Click += new System.EventHandler(this.SearchCategory);

                CategoriesButton.DropDownItems.Add(MenuItem);


            }


            tempw = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Tempw\\");
            if (!Directory.Exists(tempw)) Directory.CreateDirectory(tempw);

            client = new WebClient();


            using (client)
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                client.DownloadFile(website + "master/CustomActors.xml", tempw + "CustomActors.xml");

            }

            XmlDocument doc = new XmlDocument();
#if DEBUG
            var fileName = "Z:\\Users\\Noka\\Documents\\GitHub\\CustomActorDatabase\\CustomActors.xml"; 
#else
            var fileName = tempw + "CustomActors.xml";
#endif
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
#if DEBUG
            XmlNodeList nodes = doc.SelectNodes("Table/Actor | Table/Actor2 | Table/Debug");
#else
            XmlNodeList nodes = doc.SelectNodes("Table/Actor | Table/Actor2");
#endif


            Database = new List<DatabaseCustomActor>();
            // FilterTextBox.Text = filter;

            //List<ushort> index_list = new List<ushort>();

            if (rom64.isSet())
            {
                List<String> actors = rom64.getList("src\\actor\\");

                foreach (String str in actors)
                {
                    string basename = "";
                    ushort index = 0;
                    int version = -1;

                    if (!rom64.getNameAndIndex(str, ref basename, ref index))
                        continue;

                    TomlTable toml = rom64.parseToml(str + "\\actor.toml");
                    TomlArray var_arr = null;

                    if (toml != null)
                    {
                        if (toml.HasKey("Version"))
                            version = toml["Version"].AsInteger;
                        
                    }

                    z64romactors.Add(new CustomActorz64rom(index,basename,version));
                    
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

            }


            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection nodeAtt = node.Attributes;
                var values = new Dictionary<ushort, string>();

                DatabaseCustomActor CustomActor;

                int Key = Convert.ToInt32(nodeAtt["Key"].Value);
                int Version = Convert.ToInt32(nodeAtt["Version"].Value);
                string Name = nodeAtt["Name"].Value;
                string FolderName = nodeAtt["FolderName"].Value;
                bool HasCustomObject = (nodeAtt["HasCustomObject"] != null);
                string ObjectName = (nodeAtt["ObjectName"] != null) ? nodeAtt["ObjectName"].Value : "";
                int ActorID = (nodeAtt["ActorID"] != null) ? Convert.ToInt32(nodeAtt["ActorID"].Value,16) : 0; 
                int ObjectID = (nodeAtt["ObjectID"] != null) ? Convert.ToInt32(nodeAtt["ObjectID"].Value, 16) : 0; 
                string Category = nodeAtt["Category"].Value;
                string Author = nodeAtt["Author"].Value;
                string Notes = node.InnerText.TrimStart();
                bool ForceUpdate = (nodeAtt["ForceUpdate"] != null);
                List<string> Dependencies = ((nodeAtt["Dependencies"] != null) ? (nodeAtt["Dependencies"].Value.Split(',')).ToList() : new List<string>());
                /*
                string[] tmp = (nodeAtt["Dependencies"] != null) ? nodeAtt["Dependencies"].Value.Split(',') : null;
                if (tmp != null)
                {
                    foreach(string s in tmp)
                    {
                        
                        Dependencies.Add(GetName(Convert.ToInt32(s)));
                    }
                }*/

                CustomActor = new DatabaseCustomActor(Key, Version, Name, FolderName, HasCustomObject, ObjectName, ActorID, ObjectID, Dependencies, Category, Author, Notes);
                CustomActorz64rom match = z64romactors.Find(x => x.name == FolderName);
                if (match != null)
                {
                    CustomActor.Installed = true;
                    if (match.version == -1)
                    {
                        if (ForceUpdate) CustomActor.Outdated = true;
                        else CustomActor.Conflict = true;
                    }
                    else if (match.version < Version)
                    {
                        string ObjectFolder = ObjectName == "" ? FolderName : ObjectName; 
                        CustomObjectz64rom z64romobject = z64romobjects.Find(x => x.name == ObjectFolder);
                        if (z64romobject != null || !HasCustomObject)
                        {
                            CustomActor.Outdated = true;
                            CustomActor.ActorID = match.ID;
                            if (HasCustomObject) CustomActor.ObjectID = z64romobject.ID;
                        }
                        else
                        {
                            CustomActor.ObjectConflict = true;
                        }
                    }
                }
                else
                {
                    string ObjectFolder = ObjectName == "" ? FolderName : ObjectName;
                    CustomObjectz64rom z64romobject = z64romobjects.Find(x => x.name == ObjectFolder);
                    if (z64romobject != null)
                    {
                        CustomActor.ObjectConflict = true;
                    }
                }

                Database.Add(CustomActor);

            }

            fs.Close();


        }

        public void UpdateWindow()
        {
            ActorView.BeginUpdate();
            ActorView.Nodes.Clear();
            CustomActorNodes.Clear();
            //SetActorButton.Enabled = false;
            ActorDescription.Text = "";
            bool show;

            //  ushort[] transitions = { 0x0009, 0x0023, 0x002E };

            foreach (DatabaseCustomActor actor in Database)
            {
                show = false;
                string specialfilter = "";
                if (filter.Contains("#")) specialfilter = filter.Replace("#", "");

                ActorView.Nodes.Add(new CustomActorNode(actor));
                CustomActorNodes.Add(new CustomActorNode(actor));
                if ((HideInstalledActors.Checked && actor.Installed) || (HideDependencyActors.Checked && actor.Category == "Dependency")) ;
                else if (filter == "" || actor.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) || (specialfilter != "" && actor.Category == specialfilter))
                    show = true;


                
                if (!show) ActorView.Nodes[ActorView.Nodes.Count - 1].Remove();
            }

            foreach (CustomActorNode child in ActorView.Nodes)
            {
                if (child.Actor.Installed)
                    child.ForeColor = child.Actor.Conflict ? Color.Red : child.Actor.Outdated ? Color.Orange : Color.Green;
            }
            ActorView.EndUpdate();

        }

        private string GetName(int key)
        {
            foreach (CustomActorNode actor in ActorView.Nodes)
            {
                if (actor.Actor.Key == key)
                    return actor.Actor.FolderName;
            }
            return "";
        }

        private void UpdateActorInfo()
        {
            if (ActorView.SelectedNode != null)
            {
                CustomActorNode node = ((CustomActorNode)ActorView.SelectedNode);
                ActorName.Text = node.Actor.Name;
                ActorProperties.Text = "Category: " + node.Actor.Category + Environment.NewLine;
                if (node.Actor.Author != "") ActorProperties.Text += "Author(s): " + node.Actor.Author + Environment.NewLine;
                if (node.Actor.ActorID != 0) ActorProperties.Text += "Suggested ID: " + node.Actor.ActorID.ToString("X4") + Environment.NewLine;
                if (node.Actor.HasCustomObject) ActorProperties.Text += "Uses custom object: " + (node.Actor.ObjectName != "" ? node.Actor.ObjectName : node.Actor.FolderName) + Environment.NewLine;

                ActorDescription.Text = node.Actor.Notes;

#if DEBUG
                string imagefile = "Z:\\Users\\Noka\\Documents\\GitHub\\CustomActorDatabase\\CustomActors\\" + node.Actor.FolderName + "\\image.jpg";
                ActorImage.Image = File.Exists(imagefile) ? Image.FromFile(imagefile) : null;
#else
                try
                {
                    using (client)
                    {
                        byte[] imageBytes = client.DownloadData(website + "master/CustomActors/" + node.Actor.FolderName + "/image.jpg");

                        using (var stream = new System.IO.MemoryStream(imageBytes))
                        {
                            ActorImage.Image = Image.FromStream(stream);
                        }
                    //ActorImage.Image = Image.FromFile(path + "CustomActors\\" + node.Actor.FolderName + "\\image.jpg");
                }
                }
                catch (WebException)
                {
                    ActorImage.Image = null;
                }
#endif


                InstallButton.Enabled = (!node.Actor.Installed || node.Actor.Outdated) && !node.Actor.Conflict;
                InstallButton.Text = (!node.Actor.Outdated) ? "Install Actor" : "Update Actor";
            }
            else
            {
                ActorName.Text = "";
                ActorProperties.Text = "";
            }
        }

        private void GetDependency(CustomActorNode Node, ref List<CustomActorNode> Nodes)
        {
            
            foreach (string s in Node.Actor.Dependencies)
            {
                foreach (CustomActorNode node in CustomActorNodes)
                {
                    if (!Nodes.Contains(node) && node.Actor.Dependencies.Count == 0 && node.Actor.FolderName == s)
                    {
                        Nodes.Add(node);
                    }
                }
            }
            foreach (string s in Node.Actor.Dependencies)
            {
                foreach (CustomActorNode node in CustomActorNodes)
                {
                    if (!Nodes.Contains(node) && node.Actor.Dependencies.Count != 0 && node.Actor.FolderName == s)
                    {
                        if (node.Actor.Dependencies.Count != 0)
                        {
                            GetDependency(node, ref Nodes);
                        }
                        Nodes.Add(node);
                    }
                }
            }
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            bool proceed = false;
            ushort ActorID = 0, ObjectID = 0;
            List<CustomActorNode> installNodes = new List<CustomActorNode>();
            installNodes.Add(((CustomActorNode)ActorView.SelectedNode));
            if (installNodes[0].Actor.Dependencies.Count > 0)
            {
                GetDependency(installNodes[0], ref installNodes);
            }
            installNodes = installNodes.OrderBy(x => x.Actor.Dependencies.Count).ToList();

            foreach (CustomActorNode node in installNodes)
            {
                if (!node.Actor.Outdated)
                {
                    int objID = (ushort)node.Actor.ObjectID;
                    if (node.Actor.HasCustomObject)
                    {
                        string objname = node.Actor.ObjectName != "" ? node.Actor.ObjectName : node.Actor.FolderName;
                        CustomObjectz64rom tmp = z64romobjects.Find(x => x.name == objname);
                        if (tmp != null)
                        {
                            objID = tmp.ID;
                        }
                    }
                    using (PickCustomActorID pickID = new PickCustomActorID((ushort)node.Actor.ActorID, (ushort)objID, node.Actor.ActorID == 0, node.Actor.HasCustomObject, node.Actor.FolderName, this))
                    {
                        if (pickID.ShowDialog() == DialogResult.OK)
                        {
                            ActorID = pickID.ActorID;
                            ObjectID = pickID.ObjectID;
                            proceed = true;
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show($"WARNING! This will delete any modifications you made actor {node.Actor.FolderName}! continue?", "WARNING",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        ActorID = (ushort)node.Actor.ActorID;
                        ObjectID = (ushort)node.Actor.ObjectID;
                        proceed = true;
                    }
                }
                if (proceed)
                {
                    string temppath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Temp\\");

                    if (Directory.Exists(temppath))
                    {

                        DeleteDirectory(temppath);
                        Directory.CreateDirectory(temppath);
                    }
                    else
                    {
                        Directory.CreateDirectory(temppath);
                    }

#if DEBUG
                    var zip = ZipFile.Read("Z:\\Users\\Noka\\Documents\\GitHub\\CustomActorDatabase\\CustomActors\\" + node.Actor.FolderName + "\\data.zip");
                    zip.ExtractAll(temppath, ExtractExistingFileAction.Throw);
#else
                    PleaseWait pleasewait = new PleaseWait(website + "master/CustomActors/" + node.Actor.FolderName + "/data.zip", tempw + "data.zip", temppath, false);
                    pleasewait.ShowDialog();
#endif


                    /*
                    using (var zip = ZipFile.Read(path + "CustomActors\\" + node.Actor.FolderName + "\\data.zip"))
                        zip.ExtractAll(temppath, ExtractExistingFileAction.Throw);*/

                    if (Directory.Exists(temppath + "include\\object\\"))
                    {
                        System.IO.DirectoryInfo di2 = new DirectoryInfo(temppath + "include\\object\\");


                        if (node.Actor.HasCustomObject)
                        {
                            FileInfo[] dirfiles2 = di2.GetFiles();
                            foreach (FileInfo dirfile in dirfiles2)
                            {

                                string sourcefile = Path.Combine(temppath + "include\\object\\", dirfile.Name);
                                string targetfile = Path.Combine(temppath + "include\\object\\", "0x" + ObjectID.ToString("X4") + "-" + (node.Actor.ObjectName != "" ? node.Actor.ObjectName : node.Actor.FolderName) + dirfile.Extension);
                                File.Move(sourcefile, targetfile);

                            }
                        }
                    }



                    string[] renamer = new[] { "src\\actor\\", "rom\\actor\\", "src\\object\\", "rom\\object\\" };
                    ushort[] renamerID = new[] { ActorID, ActorID, ObjectID, ObjectID };

                    for (int i = 0; i < renamer.Length; i++)
                    {
                        if (i > 1 && !node.Actor.HasCustomObject) break;

                        System.IO.DirectoryInfo di = new DirectoryInfo(temppath + renamer[i]);
                        if (Directory.Exists(di.FullName))
                            foreach (DirectoryInfo dir in di.GetDirectories())
                            {

                                if (i == 0)
                                {
                                    FileInfo[] dirfiles = dir.GetFiles();
                                    foreach (FileInfo dirfile in dirfiles)
                                    {
                                        if (dirfile.Extension == ".c" || dirfile.Extension == ".h")
                                        {
                                            string targetfile = Path.Combine(dir.FullName, dirfile.Name);
                                            Helpers.ReplaceLine("#define ACT_ID", "#define ACT_ID 0x" + ActorID.ToString("X4"), targetfile, 100);

                                            int c = 2;
                                            foreach (string dependency in node.Actor.Dependencies)
                                            {
                                                int dependencyID = z64romactors.Find(x => x.name == dependency).ID;
                                                Helpers.ReplaceLine($"#define ACT{c}_ID", $"#define ACT{c}_ID 0x" + dependencyID.ToString("X4"), targetfile, 100);
                                                c++;
                                            }
                                            if (node.Actor.HasCustomObject)
                                            {
                                                Helpers.ReplaceLine("#define OBJ_H", "#define OBJ_H \"object/0x" + ObjectID.ToString("X4") + "-" + (node.Actor.ObjectName != "" ? node.Actor.ObjectName : node.Actor.FolderName) + ".h\"", targetfile, 100);
                                                Helpers.ReplaceLine("#define OBJ_ID", "#define OBJ_ID 0x" + ObjectID.ToString("X4"), targetfile, 100);
                                            }
                                        }
                                    }

                                }

                                string name = dir.Name;
                                string tmpdir = ((i > 1 && node.Actor.ObjectName != "") ? node.Actor.ObjectName : node.Actor.FolderName);
                                name = "0x" + renamerID[i].ToString("X4") + "-" + tmpdir;
                                if (!Directory.Exists(temppath + renamer[i] + name))
                                    dir.MoveTo(temppath + renamer[i] + name);
                            }
                    }


                    DirectoryInfo sourcedir = new DirectoryInfo(temppath);
                    string destinationDir = rom64.getPath() + "\\";

                    // Get the files in the directory and copy them to the new location
                    //FileInfo[] files = sourcedir.GetFiles("",SearchOption.AllDirectories);
                    string[] files = Directory.GetFiles(temppath, "*.*", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        string destination = Path.GetFullPath(file).Replace(temppath, destinationDir);
                        if (!Directory.Exists(Path.GetDirectoryName(destination)))
                            Directory.CreateDirectory(Path.GetDirectoryName(destination));
                        if (File.Exists(destination))
                            File.Delete(destination);
                        File.Move(file, destination);
                    }

                    // Directory.Delete(temppath);

                    if (!node.Actor.Outdated)
                    {
                        z64romactors.Add(new CustomActorz64rom(ActorID, node.Actor.FolderName, node.Actor.Version));
                        if (MainForm.ActorCache.ContainsKey(ActorID)) MainForm.ActorCache.Remove(ActorID);
                        MainForm.ActorCache.Add(ActorID, new ActorInfo(node.Actor.Name, new List<ActorProperty>(), "" + ObjectID.ToString("X4")));
                        if (node.Actor.HasCustomObject)
                        {

                            z64romobjects.Add(new CustomObjectz64rom(ObjectID, node.Actor.ObjectName != "" ? node.Actor.ObjectName : node.Actor.FolderName));
                            if (MainForm.ObjectCache.ContainsKey(ObjectID)) MainForm.ObjectCache.Remove(ObjectID);
                            MainForm.ObjectCache.Add(ObjectID, new ObjectInfo(1, node.Actor.ObjectName != "" ? node.Actor.ObjectName  : node.Actor.FolderName, "" + ActorID.ToString("X4")));
                        }
                    }


                    reload = true;
                    node.Actor.Installed = true;
                    node.Actor.Outdated = false;

                    UpdateWindow();
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {


            this.Close();
        }

        private void ActorView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //InstallButton.Enabled = true;
        }

        private void ActorView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateActorInfo();
        }

        private void SearchCategory(object sender, EventArgs e)
        {
            FilterTextBox.Text = "#" + ((ToolStripMenuItem)sender).Text;
            GoButton_Click(sender, e);
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            filter = FilterTextBox.Text;
            UpdateWindow();
        }

        private void HideInstalledActors_CheckedChanged(object sender, EventArgs e)
        {
            UpdateWindow();
        }

        private void CustomActorDatabase_FormClosed(object sender, FormClosedEventArgs e)
        {
            string tmp = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Temp\\");

            //Directory.Delete(tmp, true);
            //Directory.Delete(tempw, true);
            DeleteDirectory(tmp);
            DeleteDirectory(tempw);
        }

        public void DeleteDirectory(string target_dir)
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

        private void HideDependencyActors_CheckedChanged(object sender, EventArgs e)
        {
            UpdateWindow();
        }
    }

    public class DatabaseCustomActor
    {
        public int Key;
        public int Version;
        public string Name;
        public string FolderName;
        public bool HasCustomObject;
        public int ActorID;
        public int ObjectID;
        public string Category;
        public string Notes;
        public string Author;
        public bool Installed = false;
        public bool Outdated = false;
        public bool Conflict = false;
        public bool ObjectConflict = false;
        public List<string> Dependencies = new List<string>();
        public string ObjectName = "";
        public DatabaseCustomActor(int _Key, int _Version, string _Name, string _FolderName, bool _HasCustomObject, string _ObjectName, int _ActorID, int _ObjectID, List<string> _Dependencies, string _Category, string _Author, string _Notes)
        {
            Key = _Key;
            Name = _Name;
            Version = _Version;
            FolderName = _FolderName;
            HasCustomObject = _HasCustomObject;
            ObjectName = _ObjectName;
            ActorID = _ActorID;
            Category = _Category;
            Author = _Author;
            Notes = _Notes;
            Dependencies = _Dependencies;
        }

    }

    public class CustomActorNode : TreeNode
    {
      
        public int Value { get; set; }

        public DatabaseCustomActor Actor { get; set; }
        public CustomActorNode(DatabaseCustomActor actor) : base(actor.Name)
        {
            Value = actor.Key;
            Actor = actor;
            Text = Actor.Name + " " + (Actor.Conflict ? "[Actor Name Conflict]" : Actor.ObjectConflict ? "[Object Exists]" : Actor.Outdated ? "[Update Available]" : Actor.Installed ? "[Up to date]" : "");
        }
        
    }

    public class CustomActorz64rom
    {
        public string name;
        public int ID;
        public int version;
        public CustomActorz64rom(int _ID, string _name, int _version)
        {
            ID = _ID;
            version = _version;
            name = _name;
        }
    }

    public class CustomObjectz64rom
    {
        public string name;
        public int ID;
        public int version;
        public CustomObjectz64rom(int _ID, string _name)
        {
            ID = _ID;
            name = _name;
        }
    }

}
