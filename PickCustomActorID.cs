using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpOcarina
{
    public partial class PickCustomActorID : Form
    {

        public ushort ActorID;
        public ushort ObjectID;
        public bool AutoFind;
        public bool HasCustomObject;
        public MainForm mainform;
        public CustomActorDatabase parent;
        public string name;

        public PickCustomActorID(ushort _ActorID , ushort _ObjectID, bool _AutoFind, bool _HasCustomObject, string _Name, CustomActorDatabase _parent)
        {
            ActorID = _ActorID;
            ObjectID = _ObjectID;
            AutoFind = _AutoFind;
            HasCustomObject = _HasCustomObject;
            parent = _parent;
            mainform = parent.mainform;
            
            InitializeComponent();
            Init();
            Text = "Pick Actor ID: " + _Name;
            UpdateForm();
        }
        public void Init()
        {
            ActorIDNumeric.Value = ActorID;
            ObjectIDNumeric.Value = ObjectID;
            ObjectLabel.Visible = ObjectIDNumeric.Visible = HasCustomObject;
            if (ActorID == 0) FindEmptyIDs.Checked = true;
        }

        public void UpdateForm()
        {
            ActorIDNumeric.Enabled = ObjectIDNumeric.Enabled = !FindEmptyIDs.Checked;
            if (FindEmptyIDs.Checked)
            {
                ActorInUseLabel.Visible = false;
                ObjectInUseLabel.Visible = false;
                Ok.Enabled = true;

                ushort i;
                for(i = 1; i < 0x999; i++)
                {
                    if (!MainForm.ActorCache.ContainsKey(i)) break;
                }
                ActorIDNumeric.Value = i;

                if (HasCustomObject && ObjectID == 0)
                {
                    for (i = 4; i < 0x999; i++)
                    {
                        if (!MainForm.ObjectCache.ContainsKey(i)) break;
                    }
                    ObjectIDNumeric.Value = i;
                }



            }
            else
            {
                ActorInUseLabel.Visible = false;
                ObjectInUseLabel.Visible = false;

                if (ActorIDNumeric.Value == ActorID)
                {
                    ActorInUseLabel.Visible = true;
                    ActorInUseLabel.ForeColor = Color.Green;
                    ActorInUseLabel.Text = "Recommended by author";
                    Ok.Enabled = true;
                }
                else if (parent.z64romactors.FindIndex(x => x.ID == ActorIDNumeric.Value) != -1)
                {
                    ActorInUseLabel.Visible = true;
                    ActorInUseLabel.ForeColor = Color.Red;
                    ActorInUseLabel.Text = "Already in use!";
                    Ok.Enabled = false;
                }
                else if (MainForm.ActorCache.ContainsKey((ushort)ActorIDNumeric.Value))
                {
                    ActorInUseLabel.Visible = true;
                    ActorInUseLabel.ForeColor = Color.DarkOrange;
                    ActorInUseLabel.Text = "Used by vanilla actor";
                    Ok.Enabled = true;
                }

                if (HasCustomObject)
                {
                    if (ObjectIDNumeric.Value == ObjectID)
                    {
                        ObjectInUseLabel.Visible = true;
                        ObjectInUseLabel.ForeColor = Color.Green;
                        ObjectInUseLabel.Text = "Recommended by author";
                        Ok.Enabled = true;
                    }
                    else if (parent.z64romobjects.FindIndex(x => x.ID == ObjectIDNumeric.Value) != -1)
                    {
                        ObjectInUseLabel.Visible = true;
                        ObjectInUseLabel.ForeColor = Color.Red;
                        ObjectInUseLabel.Text = "Already in use!";
                        Ok.Enabled = false;
                    }
                    else if (MainForm.ObjectCache.ContainsKey((ushort)ObjectIDNumeric.Value))
                    {
                        ObjectInUseLabel.Visible = true;
                        ObjectInUseLabel.ForeColor = Color.Orange;
                        ObjectInUseLabel.Text = "Used by vanilla object";
                        Ok.Enabled = true;
                    }
                }
            }
            
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            ActorID = (ushort)ActorIDNumeric.Value;
            ObjectID = (ushort)ObjectIDNumeric.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FindEmptyIDs_CheckedChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void ActorIDNumeric_ValueChanged(object sender, EventArgs e)
        {
            UpdateForm(); 
        }

        private void ObjectIDNumeric_ValueChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }
    }
}
