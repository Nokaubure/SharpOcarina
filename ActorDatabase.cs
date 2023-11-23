using System;
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
using Tommy;

namespace SharpOcarina
{
    public partial class ActorDatabase : Form
    {
        public List<DatabaseActor> Database;
        public XmlNodeList nodes;
        public string filter;
        public byte target;
        public ushort initialvalue, initialvariable;
        public byte firsttime = 0;
        MainForm parent;
        string[] actor_categories = { "Switch", "Prop (1)", "Player", "Bomb", "NPC", "Enemy", "Prop (2)", "Item/Action", "Misc", "Boss", "Transitions", "Chests" };
        public ActorDatabase(MainForm _parent, byte _target, string _filter, ushort _initialvalue = 0xFFFF, ushort _initialvariable = 0xFFFF)
        {
            InitializeComponent();
            parent = _parent;
            target = _target;
            filter = _filter;
            initialvalue = _initialvalue;
            initialvariable = _initialvariable;
            Init();
            UpdateWindow();
        }

        public void Init()
        {
            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            nodes = XMLreader.getXMLNodes(gameprefix + "ActorNames", "Actor");
            Database = new List<DatabaseActor>();
            FilterTextBox.Text = filter;

            if (initialvalue == 0xFFFF) firsttime = 3;

            List<ushort> index_list = new List<ushort>();

            if (rom64.isSet()){
                List<String> actors = rom64.getList("src\\actor\\");

                foreach(String str in actors) {
                    string basename = "";
                    ushort index = 0;

                    if (!rom64.getNameAndIndex(str, ref basename, ref index))
                        continue;

                    var variables = new Dictionary<ushort, string>();
                    index_list.Add(index);

                    int exists = Database.FindIndex(x => x.Value == index);
                    if(exists != -1)
                    {
                      //  Console.WriteLine("Removed " + Database[exists].Value.ToString("X2"));
                        Database.RemoveAt(exists); //removes vanilla actor from db
                        
                    }

                    TomlTable toml = rom64.parseToml(str + "\\actor.toml");
                    TomlArray var_arr = null;
                    byte cat = 0;

                    if (toml != null) {
                        var_arr = toml["Variables"].AsArray;
                        if (toml.HasKey("Name"))
                            basename = toml["Name"].AsString;
                        if (toml.HasKey("Category")) {
                            string catname = toml["Category"].AsString;

                            switch (catname.ToLower()) {
                                case "switch":     cat = 0; break;
                                case "bg":         cat = 1; break;
                                case "player":     cat = 2; break;
                                case "explosive":  cat = 3; break;
                                case "npc":        cat = 4; break;
                                case "enemy":      cat = 5; break;
                                case "prop":       cat = 6; break;
                                case "itemaction": cat = 7; break;
                                case "misc":       cat = 8; break;
                                case "boss":       cat = 9; break;
                                case "door":       cat = 10; break;
                                case "chest":      cat = 11; break;
                            }
                        }
                    }

                    if (var_arr != null) {
                        foreach (TomlArray arr in var_arr) {
                            ushort var_index = (ushort)arr[0].AsInteger.Value;
                            string name = arr[1].AsString;

                            variables.Add(var_index, name);
                        }
                    } else {
                        foreach (XmlNode node in nodes) {
                            XmlAttributeCollection nodeAtt = node.Attributes;

                            if ((ushort)Convert.ToInt16(nodeAtt["Key"].Value, 16) != index)
                                continue;
                            
                            cat = Convert.ToByte(nodeAtt["Category"].Value);

                            XMLactor xmlactor = XMLreader.getFullActor(nodeAtt["Key"].Value);
                            Dictionary<ushort,string> vars = xmlactor.variables;

                            foreach(KeyValuePair<ushort,string> s in vars)
                                variables.Add(s.Key, s.Value);
                            break;
                        }
                    }

                    Database.Add(new DatabaseActor(index ,variables, basename, "", cat));
                }
            }

            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection nodeAtt = node.Attributes;
                var values = new Dictionary<ushort, string>(); 

                XMLactor xmlactor = XMLreader.getFullActor(nodeAtt["Key"].Value);


                if (index_list.Contains((Convert.ToUInt16(nodeAtt["Key"].Value, 16)))) continue;



                Dictionary<ushort,string> vars = xmlactor.variables;

                foreach(KeyValuePair<ushort,string> s in vars)
                {
                    values.Add(s.Key, s.Value);
                }

                string warning = "";
                if ((MainForm.CurrentScene.SpecialObject == 0x0003 && nodeAtt["Object"] != null && (ushort)Convert.ToInt16(nodeAtt["Object"].Value.Split(',')[0].Trim(), 16) == 0x0002) ||
                     (MainForm.CurrentScene.SpecialObject == 0x0002 && nodeAtt["Object"] != null && (ushort)Convert.ToInt16(nodeAtt["Object"].Value.Split(',')[0].Trim(), 16) == 0x0003)) warning = "\n WARNING! Change special object setting before using this actor!";


                Database.Add(new DatabaseActor((ushort)Convert.ToInt16(nodeAtt["Key"].Value, 16),values, nodeAtt["Name"].Value, xmlactor.notes + warning,xmlactor.category));
            }
        }

        public void UpdateWindow()
        {
            ActorView.Nodes.Clear();
            SetActorButton.Enabled = false;
            NotesTextBox.Text = "";
            bool show;

          //  ushort[] transitions = { 0x0009, 0x0023, 0x002E };

            foreach (DatabaseActor actor in Database)
            {
                show = false;
                string specialfilter = "";
                if (filter.Contains("#")) specialfilter = filter.Replace("#", "");

              //  Console.WriteLine(actor.Category + " - " + Array.IndexOf(actor_categories, specialfilter));

                ActorView.Nodes.Add(new ActorNode(actor.Value, 0x0000, actor.Value.ToString("X4") + " - " + actor.Name, actor.Notes));
                if (filter == "" || actor.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) || (specialfilter != "" && actor.Category == Array.IndexOf(actor_categories, specialfilter)))
                    show = true;

                if (firsttime == 0 && actor.Value == initialvalue) {ActorView.SelectedNode = ActorView.Nodes[ActorView.Nodes.Count - 1]; firsttime++; ActorView.Focus(); }

                if (actor.Variables.Count >= 1)
                {
                    foreach (KeyValuePair<ushort, string> variable in actor.Variables)
                    {
                        string newname = variable.Value;
                        string newnotes = actor.Notes;
                        if (newname.Contains("//"))
                        {
                            string[] piezes = newname.Split(new string[] { "//" }, StringSplitOptions.None);
                            newnotes = newnotes.Replace(Environment.NewLine, "\\par ");
                            newnotes = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red0\green0\blue255;} \cf2" + piezes[1] + @"\line" + @"\cf1 " + newnotes + "}";
                            newname = piezes[0];
                        }

                        if (variable.Value.Contains(filter, StringComparison.OrdinalIgnoreCase)) show = true;
                        ActorView.Nodes[ActorView.Nodes.Count - 1].Nodes.Add(new ActorNode(actor.Value, variable.Key, variable.Key.ToString("X4") + " - " + newname, newnotes));

                        if (firsttime == 1 && actor.Value == initialvalue && variable.Key == initialvariable) { ActorView.SelectedNode = ActorView.Nodes[ActorView.Nodes.Count - 1].Nodes[ActorView.Nodes[ActorView.Nodes.Count - 1].Nodes.Count - 1]; firsttime++;}
                    }
                }

                if (!show) ActorView.Nodes[ActorView.Nodes.Count - 1].Remove();
            }

            firsttime = 3;
        }

        private void SetActorButton_Click(object sender, EventArgs e)
        {
            parent.SetActorFromDatabase(((ActorNode)ActorView.SelectedNode).Value, ((ActorNode)ActorView.SelectedNode).Variable, target);
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActorView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SetActorButton.Enabled = true;
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            filter = FilterTextBox.Text;
            UpdateWindow();
        }

        private void FilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GoButton_Click(sender,e);
            }
        }

        private void ActorView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (((ActorNode)ActorView.SelectedNode).Notes.Contains("rtf1"))
            {
                NotesTextBox.Text = "";
                NotesTextBox.SelectedRtf = (((ActorNode)ActorView.SelectedNode).Notes);
            }
            else NotesTextBox.Text = (((ActorNode)ActorView.SelectedNode).Notes);
        }
    }

    public class ActorNode : TreeNode
    {
        public ushort Value { get; set; }
        public ushort Variable { get; set; }
        public string Notes { get; set; }
        public ActorNode(ushort _Value, ushort _Variable, string text, string _Notes) : base(text)
        {
            Value = _Value;
            Variable = _Variable;
            Notes = _Notes;
        }
    }
    public class DatabaseActor
    {
        public Dictionary<ushort, string> Variables;
        public string Notes;
        public string Name;
        public ushort Value;
        public byte Category;
        public DatabaseActor(ushort _Value, Dictionary<ushort, string> _Variables, string _Name, string _Notes, byte _Category)
        {
            Value = _Value;
            Variables = _Variables;
            Notes = _Notes;
            Name = _Name;
            Category = _Category;
        }

    }

    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
