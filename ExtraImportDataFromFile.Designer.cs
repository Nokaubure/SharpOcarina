namespace SharpOcarina
{
    partial class ImportDataFromFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RoomID = new SharpOcarina.NumericUpDownEx();
            this.CancelButton = new System.Windows.Forms.Button();
            this.HeaderID = new SharpOcarina.NumericUpDownEx();
            this.label2 = new System.Windows.Forms.Label();
            this.CollisionCheckbox = new System.Windows.Forms.CheckBox();
            this.CameraCheckbox = new System.Windows.Forms.CheckBox();
            this.WaterboxCheckbox = new System.Windows.Forms.CheckBox();
            this.PathwayCheckbox = new System.Windows.Forms.CheckBox();
            this.TransitionCheckbox = new System.Windows.Forms.CheckBox();
            this.SpawnCheckbox = new System.Windows.Forms.CheckBox();
            this.EnvironmentCheckbox = new System.Windows.Forms.CheckBox();
            this.ActorCheckbox = new System.Windows.Forms.CheckBox();
            this.ObjectCheckbox = new System.Windows.Forms.CheckBox();
            this.CutsceneCheckbox = new System.Windows.Forms.CheckBox();
            this.TextureAnimCheckbox = new System.Windows.Forms.CheckBox();
            this.ActorCutsceneCheckbox = new System.Windows.Forms.CheckBox();
            this.ExitCheckbox = new System.Windows.Forms.CheckBox();
            this.AllRoomsCheckbox = new System.Windows.Forms.CheckBox();
            this.AllHeadersCheckbox = new System.Windows.Forms.CheckBox();
            this.overrideCheckbox = new System.Windows.Forms.CheckBox();
            this.RoomSettingsCheckbox = new System.Windows.Forms.CheckBox();
            this.SceneSettingsCheckbox = new System.Windows.Forms.CheckBox();
            this.AdditionalTexturesCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.RoomID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderID)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(55, 420);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "Import";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Room:";
            // 
            // RoomID
            // 
            this.RoomID.AlwaysFireValueChanged = false;
            this.RoomID.DisplayDigits = 1;
            this.RoomID.DoValueRollover = true;
            this.RoomID.IncrementMouseWheel = 1;
            this.RoomID.Location = new System.Drawing.Point(200, 12);
            this.RoomID.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RoomID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.RoomID.Name = "RoomID";
            this.RoomID.ShiftMultiplier = 1;
            this.RoomID.Size = new System.Drawing.Size(73, 20);
            this.RoomID.TabIndex = 2;
            this.RoomID.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(136, 420);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // HeaderID
            // 
            this.HeaderID.AlwaysFireValueChanged = false;
            this.HeaderID.DisplayDigits = 1;
            this.HeaderID.DoValueRollover = true;
            this.HeaderID.IncrementMouseWheel = 1;
            this.HeaderID.Location = new System.Drawing.Point(200, 38);
            this.HeaderID.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.HeaderID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.HeaderID.Name = "HeaderID";
            this.HeaderID.ShiftMultiplier = 1;
            this.HeaderID.Size = new System.Drawing.Size(73, 20);
            this.HeaderID.TabIndex = 7;
            this.HeaderID.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Header:";
            // 
            // CollisionCheckbox
            // 
            this.CollisionCheckbox.AutoSize = true;
            this.CollisionCheckbox.Location = new System.Drawing.Point(12, 9);
            this.CollisionCheckbox.Name = "CollisionCheckbox";
            this.CollisionCheckbox.Size = new System.Drawing.Size(88, 17);
            this.CollisionCheckbox.TabIndex = 8;
            this.CollisionCheckbox.Text = "Collision data";
            this.CollisionCheckbox.UseVisualStyleBackColor = true;
            this.CollisionCheckbox.CheckedChanged += new System.EventHandler(this.CollisionCheckbox_CheckedChanged);
            // 
            // CameraCheckbox
            // 
            this.CameraCheckbox.AutoSize = true;
            this.CameraCheckbox.Location = new System.Drawing.Point(12, 32);
            this.CameraCheckbox.Name = "CameraCheckbox";
            this.CameraCheckbox.Size = new System.Drawing.Size(67, 17);
            this.CameraCheckbox.TabIndex = 9;
            this.CameraCheckbox.Text = "Cameras";
            this.CameraCheckbox.UseVisualStyleBackColor = true;
            this.CameraCheckbox.CheckedChanged += new System.EventHandler(this.CameraCheckbox_CheckedChanged);
            // 
            // WaterboxCheckbox
            // 
            this.WaterboxCheckbox.AutoSize = true;
            this.WaterboxCheckbox.Location = new System.Drawing.Point(12, 55);
            this.WaterboxCheckbox.Name = "WaterboxCheckbox";
            this.WaterboxCheckbox.Size = new System.Drawing.Size(83, 17);
            this.WaterboxCheckbox.TabIndex = 10;
            this.WaterboxCheckbox.Text = "Waterboxes";
            this.WaterboxCheckbox.UseVisualStyleBackColor = true;
            this.WaterboxCheckbox.CheckedChanged += new System.EventHandler(this.WaterboxCheckbox_CheckedChanged);
            // 
            // PathwayCheckbox
            // 
            this.PathwayCheckbox.AutoSize = true;
            this.PathwayCheckbox.Location = new System.Drawing.Point(12, 78);
            this.PathwayCheckbox.Name = "PathwayCheckbox";
            this.PathwayCheckbox.Size = new System.Drawing.Size(72, 17);
            this.PathwayCheckbox.TabIndex = 11;
            this.PathwayCheckbox.Text = "Pathways";
            this.PathwayCheckbox.UseVisualStyleBackColor = true;
            this.PathwayCheckbox.CheckedChanged += new System.EventHandler(this.PathwayCheckbox_CheckedChanged);
            // 
            // TransitionCheckbox
            // 
            this.TransitionCheckbox.AutoSize = true;
            this.TransitionCheckbox.Location = new System.Drawing.Point(12, 124);
            this.TransitionCheckbox.Name = "TransitionCheckbox";
            this.TransitionCheckbox.Size = new System.Drawing.Size(77, 17);
            this.TransitionCheckbox.TabIndex = 12;
            this.TransitionCheckbox.Text = "Transitions";
            this.TransitionCheckbox.UseVisualStyleBackColor = true;
            this.TransitionCheckbox.CheckedChanged += new System.EventHandler(this.TransitionCheckbox_CheckedChanged);
            // 
            // SpawnCheckbox
            // 
            this.SpawnCheckbox.AutoSize = true;
            this.SpawnCheckbox.Location = new System.Drawing.Point(12, 147);
            this.SpawnCheckbox.Name = "SpawnCheckbox";
            this.SpawnCheckbox.Size = new System.Drawing.Size(64, 17);
            this.SpawnCheckbox.TabIndex = 13;
            this.SpawnCheckbox.Text = "Spawns";
            this.SpawnCheckbox.UseVisualStyleBackColor = true;
            this.SpawnCheckbox.CheckedChanged += new System.EventHandler(this.SpawnCheckbox_CheckedChanged);
            // 
            // EnvironmentCheckbox
            // 
            this.EnvironmentCheckbox.AutoSize = true;
            this.EnvironmentCheckbox.Location = new System.Drawing.Point(12, 170);
            this.EnvironmentCheckbox.Name = "EnvironmentCheckbox";
            this.EnvironmentCheckbox.Size = new System.Drawing.Size(90, 17);
            this.EnvironmentCheckbox.TabIndex = 14;
            this.EnvironmentCheckbox.Text = "Environments";
            this.EnvironmentCheckbox.UseVisualStyleBackColor = true;
            this.EnvironmentCheckbox.CheckedChanged += new System.EventHandler(this.EnvironmentCheckbox_CheckedChanged);
            // 
            // ActorCheckbox
            // 
            this.ActorCheckbox.AutoSize = true;
            this.ActorCheckbox.Location = new System.Drawing.Point(12, 193);
            this.ActorCheckbox.Name = "ActorCheckbox";
            this.ActorCheckbox.Size = new System.Drawing.Size(56, 17);
            this.ActorCheckbox.TabIndex = 15;
            this.ActorCheckbox.Text = "Actors";
            this.ActorCheckbox.UseVisualStyleBackColor = true;
            this.ActorCheckbox.CheckedChanged += new System.EventHandler(this.ActorCheckbox_CheckedChanged);
            // 
            // ObjectCheckbox
            // 
            this.ObjectCheckbox.AutoSize = true;
            this.ObjectCheckbox.Location = new System.Drawing.Point(12, 216);
            this.ObjectCheckbox.Name = "ObjectCheckbox";
            this.ObjectCheckbox.Size = new System.Drawing.Size(62, 17);
            this.ObjectCheckbox.TabIndex = 16;
            this.ObjectCheckbox.Text = "Objects";
            this.ObjectCheckbox.UseVisualStyleBackColor = true;
            this.ObjectCheckbox.CheckedChanged += new System.EventHandler(this.ObjectCheckbox_CheckedChanged);
            // 
            // CutsceneCheckbox
            // 
            this.CutsceneCheckbox.AutoSize = true;
            this.CutsceneCheckbox.Location = new System.Drawing.Point(12, 239);
            this.CutsceneCheckbox.Name = "CutsceneCheckbox";
            this.CutsceneCheckbox.Size = new System.Drawing.Size(71, 17);
            this.CutsceneCheckbox.TabIndex = 17;
            this.CutsceneCheckbox.Text = "Cutscene";
            this.CutsceneCheckbox.UseVisualStyleBackColor = true;
            this.CutsceneCheckbox.CheckedChanged += new System.EventHandler(this.CutsceneCheckbox_CheckedChanged);
            // 
            // TextureAnimCheckbox
            // 
            this.TextureAnimCheckbox.AutoSize = true;
            this.TextureAnimCheckbox.Location = new System.Drawing.Point(12, 262);
            this.TextureAnimCheckbox.Name = "TextureAnimCheckbox";
            this.TextureAnimCheckbox.Size = new System.Drawing.Size(116, 17);
            this.TextureAnimCheckbox.TabIndex = 18;
            this.TextureAnimCheckbox.Text = "Texture Animations";
            this.TextureAnimCheckbox.UseVisualStyleBackColor = true;
            this.TextureAnimCheckbox.CheckedChanged += new System.EventHandler(this.TextureAnimCheckbox_CheckedChanged);
            // 
            // ActorCutsceneCheckbox
            // 
            this.ActorCutsceneCheckbox.AutoSize = true;
            this.ActorCutsceneCheckbox.Location = new System.Drawing.Point(12, 354);
            this.ActorCutsceneCheckbox.Name = "ActorCutsceneCheckbox";
            this.ActorCutsceneCheckbox.Size = new System.Drawing.Size(131, 17);
            this.ActorCutsceneCheckbox.TabIndex = 19;
            this.ActorCutsceneCheckbox.Text = "Actor Cutscenes (MM)";
            this.ActorCutsceneCheckbox.UseVisualStyleBackColor = true;
            this.ActorCutsceneCheckbox.CheckedChanged += new System.EventHandler(this.ActorCutsceneCheckbox_CheckedChanged);
            // 
            // ExitCheckbox
            // 
            this.ExitCheckbox.AutoSize = true;
            this.ExitCheckbox.Location = new System.Drawing.Point(12, 101);
            this.ExitCheckbox.Name = "ExitCheckbox";
            this.ExitCheckbox.Size = new System.Drawing.Size(48, 17);
            this.ExitCheckbox.TabIndex = 20;
            this.ExitCheckbox.Text = "Exits";
            this.ExitCheckbox.UseVisualStyleBackColor = true;
            this.ExitCheckbox.CheckedChanged += new System.EventHandler(this.ExitCheckbox_CheckedChanged);
            // 
            // AllRoomsCheckbox
            // 
            this.AllRoomsCheckbox.AutoSize = true;
            this.AllRoomsCheckbox.Location = new System.Drawing.Point(185, 64);
            this.AllRoomsCheckbox.Name = "AllRoomsCheckbox";
            this.AllRoomsCheckbox.Size = new System.Drawing.Size(73, 17);
            this.AllRoomsCheckbox.TabIndex = 21;
            this.AllRoomsCheckbox.Text = "All Rooms";
            this.AllRoomsCheckbox.UseVisualStyleBackColor = true;
            this.AllRoomsCheckbox.CheckedChanged += new System.EventHandler(this.AllRoomsCheckbox_CheckedChanged);
            // 
            // AllHeadersCheckbox
            // 
            this.AllHeadersCheckbox.AutoSize = true;
            this.AllHeadersCheckbox.Location = new System.Drawing.Point(185, 87);
            this.AllHeadersCheckbox.Name = "AllHeadersCheckbox";
            this.AllHeadersCheckbox.Size = new System.Drawing.Size(80, 17);
            this.AllHeadersCheckbox.TabIndex = 22;
            this.AllHeadersCheckbox.Text = "All Headers";
            this.AllHeadersCheckbox.UseVisualStyleBackColor = true;
            this.AllHeadersCheckbox.CheckedChanged += new System.EventHandler(this.AllHeadersCheckbox_CheckedChanged);
            // 
            // overrideCheckbox
            // 
            this.overrideCheckbox.AutoSize = true;
            this.overrideCheckbox.Checked = true;
            this.overrideCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.overrideCheckbox.Location = new System.Drawing.Point(185, 397);
            this.overrideCheckbox.Name = "overrideCheckbox";
            this.overrideCheckbox.Size = new System.Drawing.Size(92, 17);
            this.overrideCheckbox.TabIndex = 23;
            this.overrideCheckbox.Text = "Override Data";
            this.overrideCheckbox.UseVisualStyleBackColor = true;
            this.overrideCheckbox.Visible = false;
            this.overrideCheckbox.CheckedChanged += new System.EventHandler(this.overrideCheckbox_CheckedChanged);
            // 
            // RoomSettingsCheckbox
            // 
            this.RoomSettingsCheckbox.AutoSize = true;
            this.RoomSettingsCheckbox.Location = new System.Drawing.Point(12, 308);
            this.RoomSettingsCheckbox.Name = "RoomSettingsCheckbox";
            this.RoomSettingsCheckbox.Size = new System.Drawing.Size(95, 17);
            this.RoomSettingsCheckbox.TabIndex = 25;
            this.RoomSettingsCheckbox.Text = "Room Settings";
            this.RoomSettingsCheckbox.UseVisualStyleBackColor = true;
            this.RoomSettingsCheckbox.CheckedChanged += new System.EventHandler(this.RoomSettingsCheckbox_CheckedChanged);
            // 
            // SceneSettingsCheckbox
            // 
            this.SceneSettingsCheckbox.AutoSize = true;
            this.SceneSettingsCheckbox.Location = new System.Drawing.Point(12, 285);
            this.SceneSettingsCheckbox.Name = "SceneSettingsCheckbox";
            this.SceneSettingsCheckbox.Size = new System.Drawing.Size(98, 17);
            this.SceneSettingsCheckbox.TabIndex = 24;
            this.SceneSettingsCheckbox.Text = "Scene Settings";
            this.SceneSettingsCheckbox.UseVisualStyleBackColor = true;
            this.SceneSettingsCheckbox.CheckedChanged += new System.EventHandler(this.SceneSettingsCheckbox_CheckedChanged);
            // 
            // AdditionalTexturesCheckbox
            // 
            this.AdditionalTexturesCheckbox.AutoSize = true;
            this.AdditionalTexturesCheckbox.Location = new System.Drawing.Point(12, 331);
            this.AdditionalTexturesCheckbox.Name = "AdditionalTexturesCheckbox";
            this.AdditionalTexturesCheckbox.Size = new System.Drawing.Size(116, 17);
            this.AdditionalTexturesCheckbox.TabIndex = 26;
            this.AdditionalTexturesCheckbox.Text = "Additional Textures";
            this.AdditionalTexturesCheckbox.UseVisualStyleBackColor = true;
            this.AdditionalTexturesCheckbox.CheckedChanged += new System.EventHandler(this.AdditionalTexturesCheckbox_CheckedChanged);
            // 
            // ImportDataFromFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 455);
            this.Controls.Add(this.AdditionalTexturesCheckbox);
            this.Controls.Add(this.RoomSettingsCheckbox);
            this.Controls.Add(this.SceneSettingsCheckbox);
            this.Controls.Add(this.overrideCheckbox);
            this.Controls.Add(this.AllHeadersCheckbox);
            this.Controls.Add(this.AllRoomsCheckbox);
            this.Controls.Add(this.ExitCheckbox);
            this.Controls.Add(this.ActorCutsceneCheckbox);
            this.Controls.Add(this.TextureAnimCheckbox);
            this.Controls.Add(this.CutsceneCheckbox);
            this.Controls.Add(this.ObjectCheckbox);
            this.Controls.Add(this.ActorCheckbox);
            this.Controls.Add(this.EnvironmentCheckbox);
            this.Controls.Add(this.SpawnCheckbox);
            this.Controls.Add(this.TransitionCheckbox);
            this.Controls.Add(this.PathwayCheckbox);
            this.Controls.Add(this.WaterboxCheckbox);
            this.Controls.Add(this.CameraCheckbox);
            this.Controls.Add(this.CollisionCheckbox);
            this.Controls.Add(this.HeaderID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.RoomID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(298, 494);
            this.MinimumSize = new System.Drawing.Size(298, 494);
            this.Name = "ImportDataFromFile";
            this.Text = "Import data from";
            ((System.ComponentModel.ISupportInitialize)(this.RoomID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label label1;
        private NumericUpDownEx RoomID;
        private System.Windows.Forms.Button CancelButton;
        private NumericUpDownEx HeaderID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CollisionCheckbox;
        private System.Windows.Forms.CheckBox CameraCheckbox;
        private System.Windows.Forms.CheckBox WaterboxCheckbox;
        private System.Windows.Forms.CheckBox PathwayCheckbox;
        private System.Windows.Forms.CheckBox TransitionCheckbox;
        private System.Windows.Forms.CheckBox SpawnCheckbox;
        private System.Windows.Forms.CheckBox EnvironmentCheckbox;
        private System.Windows.Forms.CheckBox ActorCheckbox;
        private System.Windows.Forms.CheckBox ObjectCheckbox;
        private System.Windows.Forms.CheckBox CutsceneCheckbox;
        private System.Windows.Forms.CheckBox TextureAnimCheckbox;
        private System.Windows.Forms.CheckBox ActorCutsceneCheckbox;
        private System.Windows.Forms.CheckBox ExitCheckbox;
        private System.Windows.Forms.CheckBox AllRoomsCheckbox;
        private System.Windows.Forms.CheckBox AllHeadersCheckbox;
        private System.Windows.Forms.CheckBox overrideCheckbox;
        private System.Windows.Forms.CheckBox RoomSettingsCheckbox;
        private System.Windows.Forms.CheckBox SceneSettingsCheckbox;
        private System.Windows.Forms.CheckBox AdditionalTexturesCheckbox;
    }
}