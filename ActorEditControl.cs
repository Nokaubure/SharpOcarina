using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenTK;

namespace SharpOcarina
{
    public partial class ActorEditControl : UserControl
    {
        public delegate void UpdateFormDelegate();
        public UpdateFormDelegate UpdateForm = null;

        public int cacheId = 0xFFFF;

        public OpenTK.Vector3d CenterPoint = new OpenTK.Vector3d();

        public MainForm mainform;

        public List<ZActor> Actors = new List<ZActor>();

        public ActorEditControl()
        {
            InitializeComponent();
        }

        public bool IsTransitionActor = false;
        public bool IsSpawnActor = false;

        public int ActorNumber
        {
            get { return ActorComboBox.SelectedIndex; }
            set { ActorComboBox.SelectedValue = value; }
        }

        public void SetUpdateDelegate(UpdateFormDelegate Delegate)
        {
            UpdateForm = Delegate;
            UpdateActorEdit();
        }

        public void SetLabels(string TypeName, string GroupLabel)
        {
            groupBox3.Text = GroupLabel;
           // button6.Text = "Add " + TypeName;
          //  button5.Text = "Delete " + TypeName;
           // DuplicateButton.Text = "Duplicate " + TypeName;
        }

        public void SetActors(ref List<ZActor> ActorList)
        {
            Actors = ActorList;
            UpdateActorEdit();
        }

        public void ClearActors()
        {
            Actors = new List<ZActor>();
            CenterPoint = new OpenTK.Vector3d();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {

            if (Actors == null || UpdateForm == null) throw new Exception("Interface values not set");

            mainform.StoreUndo((IsTransitionActor) ? 2 : (IsSpawnActor) ? 3 : 1);

            ushort number = 0x0015;
            if (IsTransitionActor && MainForm.CurrentScene.SpecialObject == 0x0003) number = 0x002E;
            else if (IsTransitionActor && MainForm.CurrentScene.SpecialObject == 0x0002) number = 0x0023;
            else if (IsSpawnActor) number = 0x0000;

            if (Control.ModifierKeys == Keys.Shift)
            {
                //create in front of the camera

                double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
                double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

                Vector3d truepos = mainform.GetTrueCameraPosition();

                if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
                {
                    truepos.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }
                else
                {
                    truepos.X += (float)Math.Sin(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Z -= (float)Math.Cos(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }

                truepos.X = Helpers.Clamp(truepos.X, -32767, 32767);
                truepos.Y = Helpers.Clamp(truepos.Y, -32767, 32767);
                truepos.Z = Helpers.Clamp(truepos.Z, -32767, 32767);

                Actors.Add(new ZActor(number, (short)truepos.X, (short)truepos.Y, (short)truepos.Z, 0.0f, 0.0f, 0.0f, 0));

            }
            else
            { 

                Actors.Add(new ZActor(0x00, 0x00, 0x00, 0x00, number, 0.0f, 0.0f, 0.0f, 0.0f, 0x0000));

            }


            UpdateActorEdit(true);
            UpdateForm();
          //  numericUpDown3.Value = numericUpDown3.Maximum;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Actors == null || UpdateForm == null) throw new Exception("Interface values not set");

            mainform.StoreUndo((IsTransitionActor) ? 2 : (IsSpawnActor) ? 3 : 1);

            Actors.Remove(Actors[ActorComboBox.SelectedIndex]);
            ActorComboBox.SelectedIndex -= 1;
            UpdateActorEdit();
            UpdateForm();
        }


        private void ShowHideControls()
        {
            if (IsTransitionActor == false)
            {
                // show
                ActorXRot.Visible = true;
                ActorZRot.Visible = true;
                label16.Visible = true;
                label14.Visible = true;

                // hide
                FrontRoomLabel.Visible = false;
                label3.Visible = false;
                label1.Visible = false;
                label4.Visible = false;
                FrontSwitchTextbox.Visible = false;
                FrontCamTextBox.Visible = false;
                BackSwitchTextBox.Visible = false;
                BackCamTextBox.Visible = false;

                if (IsSpawnActor)
                {
                    FrontRoomLabel.Visible = true;
                    FrontRoomLabel.Text = "      Room:";
                    FrontSwitchTextbox.Visible = true;
                }


                

            }
            else
            {
                // show
                FrontRoomLabel.Visible = true;
                label3.Visible = true;
                label1.Visible = true;
                label4.Visible = true;
                FrontSwitchTextbox.Visible = true;
                FrontCamTextBox.Visible = true;
                BackSwitchTextBox.Visible = true;
                BackCamTextBox.Visible = true;

                // hide
                ActorXRot.Visible = false;
                ActorZRot.Visible = false;
                label16.Visible = false;
                label14.Visible = false;

                FrontRoomLabel.Text = "Front Room:";



            }
          //  if (IsSpawnActor) DatabaseButton.Visible = false;
        }

        public void UpdateActorEdit(bool add = false)
        {
            ShowHideControls();

            ActorXRot.Hexadecimal = MainForm.settings.HexRotations;
            ActorYRot.Hexadecimal = MainForm.settings.HexRotations;
            ActorZRot.Hexadecimal = MainForm.settings.HexRotations;

            if (Actors.Count != 0)
            {
                //numericUpDown3.Minimum = 1;
                //numericUpDown3.Maximum = Actors.Count;
                ActorComboBox.Enabled = true;

                int previndex = ActorComboBox.SelectedIndex;
                if (add) previndex = ActorComboBox.Items.Count;
                ActorComboBox.Items.Clear();
                

                int incr = 0;
                foreach (ZActor actor in Actors)
                {
                    ActorItem item = new ActorItem();
                    if (MainForm.ActorCache.ContainsKey(actor.Number))
                        item.Text = incr + "- " + MainForm.ActorCache[actor.Number].name;
                    else
                        item.Text = incr + "- " + XMLreader.getActorName(actor.Number.ToString("X4"));
                    item.Value = incr;
                    ActorComboBox.Items.Add(item);
                    incr++;
                }


                if (previndex == -1) ActorComboBox.SelectedIndex = 0;
                else if (ActorComboBox.Items.Count - 1 >= previndex) ActorComboBox.SelectedIndex = previndex;
                else ActorComboBox.SelectedIndex = 0;


                ActorNumberTextbox.Text = Actors[ActorComboBox.SelectedIndex].Number.ToString("X4");
                ActorVariableTextbox.Text = Actors[ActorComboBox.SelectedIndex].Variable.ToString("X4");
                ActorXPos.Value = (decimal)Actors[ActorComboBox.SelectedIndex].XPos;
                ActorYPos.Value = (decimal)Actors[ActorComboBox.SelectedIndex].YPos;
                ActorZPos.Value = (decimal)Actors[ActorComboBox.SelectedIndex].ZPos;
                ActorYRot.Value = (decimal)Actors[ActorComboBox.SelectedIndex].YRot;

                if (IsTransitionActor == false)
                {
                    ActorXRot.Value = (decimal)Actors[ActorComboBox.SelectedIndex].XRot;
                    ActorZRot.Value = (decimal)Actors[ActorComboBox.SelectedIndex].ZRot;
                    if (IsSpawnActor)
                    {
                        FrontSwitchTextbox.Text = Actors[ActorComboBox.SelectedIndex].SpawnRoom.ToString("X2");
                    }
                }
                else
                {
                    FrontSwitchTextbox.Text = Actors[ActorComboBox.SelectedIndex].FrontSwitchTo.ToString("X2");
                    FrontCamTextBox.Text = Actors[ActorComboBox.SelectedIndex].FrontCamera.ToString("X2");
                    BackSwitchTextBox.Text = Actors[ActorComboBox.SelectedIndex].BackSwitchTo.ToString("X2");
                    BackCamTextBox.Text = Actors[ActorComboBox.SelectedIndex].BackCamera.ToString("X2");
                }


                foreach (Control Ctrl in panel2.Controls)
                    Ctrl.Enabled = true;

                if (Actors[ActorComboBox.SelectedIndex].Number != cacheId)
                {
                    int propertyprevindex = ActorVariableListBox.SelectedIndex;
                    ActorVariableListBox.Items.Clear();
                    List<ActorProperty> properties;
                    if (MainForm.ActorCache.ContainsKey(Actors[ActorComboBox.SelectedIndex].Number))
                        properties = MainForm.ActorCache[Actors[ActorComboBox.SelectedIndex].Number].actorproperties;
                    else
                    {
                        
                        properties = XMLreader.getActorProperties(Actors[ActorComboBox.SelectedIndex].Number.ToString("X4"));
                        if (MainForm.settings.MajorasMask && MainForm.settings.IgnoreMMDaySystem == false && !IsTransitionActor && !IsSpawnActor)
                        {
                            properties.Add(new ActorProperty(0xFF80, "X Rotation (degrees)","XRot"));
                            properties.Add(new ActorProperty(0xFF80, "Y Rotation (degrees)", "YRot"));
                            properties.Add(new ActorProperty(0xFF80, "Z Rotation (degrees)", "ZRot"));
                            properties.Add(new ActorProperty(0x4, "Spawn on Day 0", "XRot"));
                            properties.Add(new ActorProperty(0x2, "Spawn on Night 0", "XRot"));
                            properties.Add(new ActorProperty(0x1, "Spawn on Day 1", "XRot"));
                            properties.Add(new ActorProperty(0x40, "Spawn on Night 1", "ZRot"));
                            properties.Add(new ActorProperty(0x20, "Spawn on Day 2", "ZRot"));
                            properties.Add(new ActorProperty(0x10, "Spawn on Night 2", "ZRot"));
                            properties.Add(new ActorProperty(0x8, "Spawn on Day 3", "ZRot"));
                            properties.Add(new ActorProperty(0x4, "Spawn on Night 3", "ZRot"));
                            properties.Add(new ActorProperty(0x2, "Spawn on Day 4", "ZRot"));
                            properties.Add(new ActorProperty(0x1, "Spawn on Night 4", "ZRot"));
                            //properties.Add(new ActorProperty(0x7F, "Spawn time flags (low bits)", "ZRot"));
                            //properties.Add(new ActorProperty(0x7, "Spawn time flags (high bits)", "XRot"));
                            properties.Add(new ActorProperty(0x7F, "Camera index (unsuported)", "YRot"));
                        }
                    }
                    foreach (ActorProperty property in properties) ActorVariableListBox.Items.Add(property);
                    if (ActorVariableListBox.Items.Count > 0 && ActorVariableListBox.Items.Count - 1 >= propertyprevindex) ActorVariableListBox.SelectedIndex = propertyprevindex;
                    cacheId = Actors[ActorComboBox.SelectedIndex].Number;
                }

                if (ActorVariableListBox.Items.Count > 0)
                {
                    if (ActorVariableListBox.SelectedIndex < 0) ActorVariableListBox.SelectedIndex = 0;
                    ActorProperty prop = (ActorVariableListBox.Items[ActorVariableListBox.SelectedIndex] as ActorProperty);
                    ActorListBoxValue.Enabled = true;
                    ActorVariableListBox.Enabled = true;
                    ActorPropertyLabel.Enabled = true;
                    PresetDropdown.Enabled = PresetDropdown.Visible = (prop.DropdItems != null && prop.DropdItems.Count != null && prop.DropdItems.Count > 0);
                    PresetDropdown.Items.Clear();


                    // SetNumericUpDownValue(ActorListBoxValue, (Actors[ActorComboBox.SelectedIndex].Variable & prop.Mask) >> prop.Position);
                    ActorListBoxValue.Maximum = 0xFFFF;

                    if (prop.Target == "Var")
                        ActorListBoxValue.Value = (Actors[ActorComboBox.SelectedIndex].Variable & prop.Mask) >> prop.Position;
                    else if (prop.Target == "XRot")
                        ActorListBoxValue.Value = ((ushort)Actors[ActorComboBox.SelectedIndex].XRot & prop.Mask) >> prop.Position;
                    else if (prop.Target == "YRot")
                        ActorListBoxValue.Value = ((ushort)Actors[ActorComboBox.SelectedIndex].YRot & prop.Mask) >> prop.Position;
                    else if (prop.Target == "ZRot")
                        ActorListBoxValue.Value = ((ushort)Actors[ActorComboBox.SelectedIndex].ZRot & prop.Mask) >> prop.Position;

                    ActorListBoxValue.Maximum = prop.Max;

                    if (prop.DropdItems.Count > 0)
                    {
                        PresetDropdown.Items.AddRange(prop.DropdItems.ToArray());
                        PresetDropdown.SelectedIndex = 0;
                        
                        foreach (SongItem preset in PresetDropdown.Items)
                        {
                            if (Convert.ToUInt16(preset.Value) == ActorListBoxValue.Value && (preset.Text != "Unknown"))
                            {
                                PresetDropdown.SelectedItem = preset;
                                break;
                            }


                        }
                    }
                }
                else
                {
                    ActorListBoxValue.Enabled = false;
                    ActorVariableListBox.Enabled = false;
                    ActorPropertyLabel.Enabled = false;
                    ActorListBoxValue.Value = 0;
                    PresetDropdown.Enabled = PresetDropdown.Visible = false;

                }


                DeleteButton.Enabled = true;
                AddButton.Enabled = true;
                DuplicateButton.Enabled = true;
                XPosStrip.Enabled = true;
                YPosStrip.Enabled = true;
                ZPosStrip.Enabled = true;
                DatabaseButton.Enabled = true;

                /*
                List<String> variables = XMLreader.getTransitionVars(Actors[ActorComboBox.SelectedIndex].Number.ToString("X4"));
                foreach (String s in variables)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Name = s.Split('|')[0];
                    item.Text = s.Split('|')[1];
                    item.Click += new EventHandler(ChangeTransVariable);
                    TransVarDropdown.DropDownItems.Add(item);
                }
                */

            }
            else
            {
               // numericUpDown3.Minimum = 0;
              //  numericUpDown3.Maximum = 0;
                ActorComboBox.SelectedValue = 0;
                ActorComboBox.Enabled = false;

                ActorNumberTextbox.Text = string.Empty;
                ActorVariableTextbox.Text = string.Empty;
                ActorXPos.Value = 0;
                ActorYPos.Value = 0;
                ActorZPos.Value = 0;
                ActorYRot.Value = 0;

                ActorXRot.Value = 0;
                ActorZRot.Value = 0;

                foreach (Control Ctrl in panel2.Controls)
                    Ctrl.Enabled = false;

                DeleteButton.Enabled = false;
                AddButton.Enabled = true;
                DuplicateButton.Enabled = false;
                XPosStrip.Enabled = false;
                YPosStrip.Enabled = false;
                ZPosStrip.Enabled = false;
                DatabaseButton.Enabled = false;
            }
        }

        private void UpdateActorData()
        {
            if (ActorNumberTextbox.Text == string.Empty || ActorVariableTextbox.Text == string.Empty) return;

            mainform.StoreUndo((IsTransitionActor) ? 2 : (IsSpawnActor) ? 3 : 1);

            Actors[ActorComboBox.SelectedIndex].Number = ushort.Parse(ActorNumberTextbox.Text, System.Globalization.NumberStyles.HexNumber);
            Actors[ActorComboBox.SelectedIndex].Variable = ushort.Parse(ActorVariableTextbox.Text, System.Globalization.NumberStyles.HexNumber);
            Actors[ActorComboBox.SelectedIndex].XPos = (short)ActorXPos.Value;
            Actors[ActorComboBox.SelectedIndex].YPos = (short)ActorYPos.Value;
            Actors[ActorComboBox.SelectedIndex].ZPos = (short)ActorZPos.Value;
            Actors[ActorComboBox.SelectedIndex].YRot = (short)ActorYRot.Value;

            if (IsTransitionActor == false)
            {
                Actors[ActorComboBox.SelectedIndex].XRot = (short)ActorXRot.Value;
                Actors[ActorComboBox.SelectedIndex].ZRot = (short)ActorZRot.Value;
                if (IsSpawnActor)
                {
                    Actors[ActorComboBox.SelectedIndex].SpawnRoom = byte.Parse(FrontSwitchTextbox.Text, System.Globalization.NumberStyles.HexNumber);
                }
            }
            else
            {
                Actors[ActorComboBox.SelectedIndex].FrontSwitchTo = byte.Parse(FrontSwitchTextbox.Text, System.Globalization.NumberStyles.HexNumber);
                Actors[ActorComboBox.SelectedIndex].FrontCamera = byte.Parse(FrontCamTextBox.Text, System.Globalization.NumberStyles.HexNumber);
                Actors[ActorComboBox.SelectedIndex].BackSwitchTo = byte.Parse(BackSwitchTextBox.Text, System.Globalization.NumberStyles.HexNumber);
                Actors[ActorComboBox.SelectedIndex].BackCamera = byte.Parse(BackCamTextBox.Text, System.Globalization.NumberStyles.HexNumber);
            }

            UpdateActorEdit();
            UpdateForm();
        }

        private void ActorPos_ValueChanged(object sender, EventArgs e)
        {
            UpdateActorData();
        }

        private void ActorRot_ValueChanged(object sender, EventArgs e)
        {
            UpdateActorData();
        }

        private void numericTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateActorData();
        }

        private void numericTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateActorData();
        }

        private void numericTextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateActorData();
        }

        private void numericTextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateActorData();
        }

        private void numericTextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateActorData();
        }

        private void numericTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateActorData();
        }

        private void XRot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && MainForm.settings.Degrees)
            {
                ActorXRot.Value = Helpers.Clamp(ActorXRot.Value * (decimal) 182.044444444, ActorXRot.Minimum, ActorXRot.Maximum);
                UpdateActorData();
            }
        }

        private void YRot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && MainForm.settings.Degrees)
            {
                ActorYRot.Value = Helpers.Clamp(ActorYRot.Value * (decimal)182.044444444, ActorYRot.Minimum, ActorYRot.Maximum);
                UpdateActorData();
            }
        }

        private void ZRot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && MainForm.settings.Degrees)
            {
                ActorZRot.Value = Helpers.Clamp(ActorZRot.Value * (decimal)182.044444444, ActorZRot.Minimum, ActorZRot.Maximum);
                UpdateActorData();
            }
            
        }

        private void Duplicate_Click(object sender, EventArgs e)
        {

            if (Actors == null || UpdateForm == null) throw new Exception("Interface values not set");

            mainform.StoreUndo((IsTransitionActor) ? 2 : (IsSpawnActor) ? 3 : 1);

            ZActor from = Actors[ActorComboBox.SelectedIndex];

            if (!MainForm.settings.DisableEasterEgg && MainForm.EasterEggPhase == 0 && from.Number == ((!MainForm.settings.MajorasMask) ? 0x0127 : 0x0092))
            {
                MainForm.EasterEggCounter++;
                if (MainForm.EasterEggCounter >= 10)
                {
                    MainForm.EasterEggPhase++;
                    MessageBox.Show("You unlocked something...", "???", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    mainform.EasterEggPhaseOne();
                }
            }
            else MainForm.EasterEggCounter = 0;

            if (Control.ModifierKeys == Keys.Shift)
            {
                //create in front of the camera

                double RotYRad = (Camera.Rot.Y / 180.0f * Math.PI);
                double RotXRad = (Camera.Rot.X / 180.0f * Math.PI);

                Vector3d truepos = mainform.GetTrueCameraPosition();

                if (Camera.Rot.X >= 90.0f || Camera.Rot.X <= -90.0f)
                {
                    truepos.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }
                else
                {
                    truepos.X += (float)Math.Sin(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Z -= (float)Math.Cos(RotYRad) * Camera.CameraCoeff * 2.0f * 1115f;
                    truepos.Y -= (float)Math.Sin(RotXRad) * Camera.CameraCoeff * 2.0f * 1115f;
                }

                truepos.X = Helpers.Clamp(truepos.X, -32767, 32767);
                truepos.Y = Helpers.Clamp(truepos.Y, -32767, 32767);
                truepos.Z = Helpers.Clamp(truepos.Z, -32767, 32767);

                if (IsTransitionActor)
                    Actors.Add(new ZActor(from.FrontSwitchTo, from.FrontCamera, from.BackSwitchTo, from.BackCamera, from.Number, (short)truepos.X, (short)truepos.Y, (short)truepos.Z, from.YRot, from.Variable));
                else if (IsSpawnActor)
                    Actors.Add(new ZActor(from.SpawnRoom, from.Number, (short)truepos.X, (short)truepos.Y, (short)truepos.Z, from.XRot, from.YRot, from.ZRot, from.Variable));
                else
                    Actors.Add(new ZActor(from.Number, (short)truepos.X, (short)truepos.Y, (short)truepos.Z, from.XRot, from.YRot, from.ZRot, from.Variable));
            }
            else
            {
                if (IsTransitionActor)
                    Actors.Add(new ZActor(from.FrontSwitchTo, from.FrontCamera, from.BackSwitchTo, from.BackCamera, from.Number, from.XPos, from.YPos, from.ZPos, from.YRot, from.Variable));
                else if (IsSpawnActor)
                    Actors.Add(new ZActor(from.SpawnRoom, from.Number, from.XPos, from.YPos, from.ZPos, from.XRot, from.YRot, from.ZRot, from.Variable));
                else
                    Actors.Add(new ZActor(from.Number, from.XPos, from.YPos, from.ZPos, from.XRot, from.YRot, from.ZRot, from.Variable));
            }

            UpdateActorEdit(true);
            UpdateForm();
           // numericUpDown3.Value = numericUpDown3.Maximum;
          //  ActorComboBox.SelectedItem = ActorComboBox.Items[ActorComboBox.Items.Count - 1];
        }

        private void ActorComboBox_ValueChanged(object sender, EventArgs e)
        {
            if (Actors == null || UpdateForm == null) throw new Exception("Interface values not set");

            
            UpdateActorEdit();
            UpdateForm();
        }

        private void ActorComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Actors == null || UpdateForm == null) throw new Exception("Interface values not set");


            UpdateActorEdit();
            UpdateForm();
        }

        private void StickToWall(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem) sender;
            if (Actors == null || UpdateForm == null) throw new Exception("Interface values not set");

            ZActor from = Actors[ActorComboBox.SelectedIndex];

            String operation = obj.Name.Replace("StickTo","");
            float Xdif = 0, Ydif = 0, Zdif = 0;
            if (operation.Contains("X") && operation.Contains("plus")) Xdif = 30000;
            if (operation.Contains("X") && operation.Contains("minus")) Xdif = -30000;
            if (operation.Contains("Y") && operation.Contains("plus")) Ydif = 30000;
            if (operation.Contains("Y") && operation.Contains("minus")) Ydif = -30000;
            if (operation.Contains("Z") && operation.Contains("plus")) Zdif = 30000;
            if (operation.Contains("Z") && operation.Contains("minus")) Zdif = -30000;

            Vector3 newpos = MoveToCollision(new Vector3(from.XPos, from.YPos, from.ZPos), new Vector3(from.XPos + Xdif, from.YPos + Ydif, from.ZPos + Zdif));

            if (newpos != new Vector3(-1, -1, -1))
            {
                from.XPos = newpos.X;
                from.YPos = newpos.Y;
                from.ZPos = newpos.Z;
                UpdateActorEdit();
                UpdateForm();
            }

        }

        private Vector3 MoveToCollision(Vector3 pos, Vector3 dir)
        {
            Vector3 output = new Vector3(-1, -1, -1);
            float dist = 999999;
            foreach (ObjFile.Group Group in MainForm.CurrentScene.ColModel.Groups)
            {
                foreach (ObjFile.Triangle Tri in Group.Triangles)
                {
                    Vector3 collision = ObjFile.RayCollision(
                        MainForm.CurrentScene.ColModel.Vertices[Tri.VertIndex[0]],
                        MainForm.CurrentScene.ColModel.Vertices[Tri.VertIndex[1]],
                        MainForm.CurrentScene.ColModel.Vertices[Tri.VertIndex[2]],
                        pos,
                        dir,
                        MainForm.CurrentScene.Scale);
                    if (collision != new Vector3(-1, -1, -1) && dist > Distance3D(pos,collision))
                    {
                        dist = Distance3D(pos, collision);
                        output = collision;
                    }
                }

            }
            return output;
        }
        public float Distance3D(Vector3 v1, Vector3 v2)
        {
            
            float result = 0;
            double part1 = Math.Pow((v2.X - v1.X), 2);
            double part2 = Math.Pow((v2.Y - v1.Y), 2);
            double part3 = Math.Pow((v2.Z - v1.Z), 2);
            double underRadical = part1 + part2 + part3;
            result = (float)Math.Sqrt(underRadical);
            return result;
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void AdjustWidthComboBox2_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (ActorItem item in ((ComboBox)sender).Items)
            {
                string s = Convert.ToString(item.Text);

                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }



        private void DatabaseButton_Click(object sender, EventArgs e)
        {
            string filter = "";
            byte target = 0;
            if (IsTransitionActor) {filter = "#Transitions"; target = 1; }
            else if (IsSpawnActor) { filter = "Spawn point"; target = 2; }

            ActorDatabase actordabase = new ActorDatabase(mainform, target, filter, Actors[ActorComboBox.SelectedIndex].Number, Actors[ActorComboBox.SelectedIndex].Variable);
            actordabase.ShowDialog();
            
        }

        private void GenericTextbox_Leave(object sender, EventArgs e)
        {
                UpdateActorData();
        }

        private void ActorVariableListBox_Click(object sender, EventArgs e)
        {
                UpdateActorEdit();
        }

        private void ActorListBoxValue_ValueChanged(object sender, EventArgs e)
        {
               if (ActorVariableListBox.Items.Count > 0)
               {
                ActorProperty prop = (ActorVariableListBox.Items[ActorVariableListBox.SelectedIndex] as ActorProperty);
               // Actors[ActorComboBox.SelectedIndex].Variable = (ushort) ((Actors[ActorComboBox.SelectedIndex].Variable & (0xFFFF - prop.Mask)) | ((ushort)(ActorListBoxValue.Value) << prop.Position));

                if (prop.Target == "Var")
                    Actors[ActorComboBox.SelectedIndex].Variable = (ushort)((Actors[ActorComboBox.SelectedIndex].Variable & (0xFFFF - prop.Mask)) | ((ushort)(ActorListBoxValue.Value) << prop.Position));
                else if (prop.Target == "XRot")
                    Actors[ActorComboBox.SelectedIndex].XRot = (short)(((ushort)Actors[ActorComboBox.SelectedIndex].XRot & (0xFFFF - prop.Mask)) | ((ushort)(ActorListBoxValue.Value) << prop.Position));
                else if (prop.Target == "YRot")
                    Actors[ActorComboBox.SelectedIndex].YRot = (short)(((ushort)Actors[ActorComboBox.SelectedIndex].YRot & (0xFFFF - prop.Mask)) | ((ushort)(ActorListBoxValue.Value) << prop.Position));
                else if (prop.Target == "ZRot")
                    Actors[ActorComboBox.SelectedIndex].ZRot = (short)(((ushort)Actors[ActorComboBox.SelectedIndex].ZRot & (0xFFFF - prop.Mask)) | ((ushort)(ActorListBoxValue.Value) << prop.Position));

                UpdateActorEdit();
               }
        }

        private void SetNumericUpDownValue(NumericUpDown control, decimal value)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            var currentValueField = control.GetType().GetField("currentValue", BindingFlags.Instance | BindingFlags.NonPublic);
            if (currentValueField != null)
            {
                currentValueField.SetValue(control, value);
                control.Text = value.ToString();
            }
        }

        private void PresetDropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ActorVariableListBox.Items.Count > 0 && PresetDropdown.SelectedIndex != 0)
            {
                ActorProperty prop = (ActorVariableListBox.Items[ActorVariableListBox.SelectedIndex] as ActorProperty);

                ActorListBoxValue.Value = Convert.ToUInt32((PresetDropdown.SelectedItem as SongItem).Value);

                if (prop.Target == "Var")
                    Actors[ActorComboBox.SelectedIndex].Variable = (ushort)((Actors[ActorComboBox.SelectedIndex].Variable & (0xFFFF - prop.Mask)) | ((ushort)(ActorListBoxValue.Value) << prop.Position));
                else if (prop.Target == "XRot")
                    Actors[ActorComboBox.SelectedIndex].XRot = (short)(((ushort)Actors[ActorComboBox.SelectedIndex].XRot & (0xFFFF - prop.Mask)) | ((ushort)(ActorListBoxValue.Value) << prop.Position));
                else if (prop.Target == "YRot")
                    Actors[ActorComboBox.SelectedIndex].YRot = (short)(((ushort)Actors[ActorComboBox.SelectedIndex].YRot & (0xFFFF - prop.Mask)) | ((ushort)(ActorListBoxValue.Value) << prop.Position));
                else if (prop.Target == "ZRot")
                    Actors[ActorComboBox.SelectedIndex].ZRot = (short)(((ushort)Actors[ActorComboBox.SelectedIndex].ZRot & (0xFFFF - prop.Mask)) | ((ushort)(ActorListBoxValue.Value) << prop.Position));

                UpdateActorEdit();
            }
        }
    }

    public class ActorItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class ActorProperty
    {
        public string Name;
        public ushort Mask;
        public ushort Max;
        public int Position;
        public string Target;
        public List<SongItem> DropdItems;
        public CheckBox Check;


        public override string ToString()
        {
            return Name;
        }

        public ActorProperty()
        {
            DropdItems = new List<SongItem>();
        }

        public ActorProperty(ushort _Mask, string _Name, string _Target)
        {
            Name = _Name;
            Mask = _Mask;
            Target = _Target;
            Max = Mask;
            DropdItems = new List<SongItem>();

            while ((Max & 1) == 0)
            {
                Max = (ushort)(Max >> 1);
                Position += 1;
            }
        }


    }
}
