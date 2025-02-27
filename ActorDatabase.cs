using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
      
        public XmlNodeList nodes;
        public string filter;
        public byte target;
        public ushort initialvalue, initialvariable;
        public byte firsttime = 0;
        MainForm parent;
        string[] actor_categories = { "Switch", "Prop (Bg)", "Player", "Bomb", "NPC", "Enemy", "Prop", "Item/Action", "Misc", "Boss", "Transitions", "Chests", "Custom Actor" };
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

            FilterTextBox.Text = filter;

            AutoSetCheckbox.Checked = MainForm.DatabaseAutoSet;

            DebugNamesCheckbox.Checked = MainForm.DatabaseDebugNames;

            if (initialvalue == 0xFFFF) firsttime = 3;

            foreach(string category in actor_categories)
            {
                ToolStripMenuItem MenuItem = new System.Windows.Forms.ToolStripMenuItem() { Name = category, Text = category };

                    MenuItem.Click += new System.EventHandler(this.SearchCategory);

                if (CategoriesButton.DropDownItems.Count == actor_categories.Length -1)
                {
                    CategoriesButton.DropDownItems.Add(new ToolStripSeparator());
                }

                CategoriesButton.DropDownItems.Add(MenuItem);

                
            }
        }

        public void UpdateWindow()
        {
            ActorView.BeginUpdate();

            ActorView.Nodes.Clear();
            SetActorButton.Enabled = false;
            NotesTextBox.Text = "";
            bool show;

            //  ushort[] transitions = { 0x0009, 0x0023, 0x002E };
            
            foreach (DatabaseActor actor in parent.Database)
            {
                show = false;
                string specialfilter = "";
                if (filter.Contains("#")) specialfilter = filter.Replace("#", "");

                ActorView.Nodes.Add(new ActorNode(actor.Value, 0x0000, actor.Value.ToString("X4") + " - " + (MainForm.DatabaseDebugNames && actor.DebugName != "" ? actor.DebugName : actor.Name), actor.Notes));
                if (filter == "" || actor.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) || (MainForm.DatabaseDebugNames && actor.DebugName.Contains(filter, StringComparison.OrdinalIgnoreCase)) || (specialfilter != "" && specialfilter != "Custom Actor" && actor.Category == Array.IndexOf(actor_categories, specialfilter)) || (specialfilter == "Custom Actor" && actor.IsCustom))
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
            ActorView.EndUpdate();

            firsttime = 3;
        }

        private void SetActorButton_Click(object sender, EventArgs e)
        {
            parent.SetActorFromDatabase(((ActorNode)ActorView.SelectedNode).Value, ((ActorNode)ActorView.SelectedNode).Variable, target);
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
            Go();
        }

        public void Go()
        {
            filter = FilterTextBox.Text;
            UpdateWindow();
        }

        public void Go2()
        {
            FilterTextBox.Text = filter;
            UpdateWindow();
        }

        private void FilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Go();
            }
        }

        private void StickToWall(object sender, EventArgs e)
        {

        }

        private void ActorView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (((ActorNode)ActorView.SelectedNode).Notes.Contains("rtf1"))
            {
                NotesTextBox.Text = "";
                NotesTextBox.SelectedRtf = (((ActorNode)ActorView.SelectedNode).Notes);
            }
            else NotesTextBox.Text = (((ActorNode)ActorView.SelectedNode).Notes);

            if (AutoSetCheckbox.Checked)
            {
                SetActorButton_Click(sender,e);
            }
        }

        private void ActorDatabase_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.actordatabase = null; 
        }

        private void AutoSetCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.DatabaseAutoSet = AutoSetCheckbox.Checked;
            if (AutoSetCheckbox.Checked && ActorView.SelectedNode != null)
            {
                ActorView_AfterSelect(sender,null);
            }
        }

        private void DebugNamesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.DatabaseDebugNames = DebugNamesCheckbox.Checked;
            UpdateWindow();
        }

        private void SearchCategory(object sender, EventArgs e)
        {
            FilterTextBox.Text = "#" + ((ToolStripMenuItem)sender).Text;
            Go();
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
        public string DebugName;
        public ushort Value;
        public byte Category;
        public bool IsCustom;
        public DatabaseActor(ushort _Value, Dictionary<ushort, string> _Variables, string _Name, string _DebugName, string _Notes, byte _Category, bool _isCustom)
        {
            Value = _Value;
            Variables = _Variables;
            Notes = _Notes;
            Name = _Name;
            DebugName = _DebugName;
            Category = _Category;
            IsCustom = _isCustom;
        }
        public override string ToString()
        {
            return $"{Value.ToString("X4")} - {Name} / {DebugName}";
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
