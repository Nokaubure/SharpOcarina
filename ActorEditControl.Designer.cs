namespace SharpOcarina
{
    partial class ActorEditControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ActorComboBox = new System.Windows.Forms.ComboBox();
            this.DuplicateButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PresetDropdown = new System.Windows.Forms.ComboBox();
            this.ActorPropertyLabel = new System.Windows.Forms.Label();
            this.ActorListBoxValue = new SharpOcarina.NumericUpDownEx();
            this.ActorVariableListBox = new System.Windows.Forms.ListBox();
            this.DatabaseButton = new System.Windows.Forms.Button();
            this.ZPosStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.StickToZplus = new System.Windows.Forms.ToolStripMenuItem();
            this.StickToZminus = new System.Windows.Forms.ToolStripMenuItem();
            this.YPosStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.StickToYplus = new System.Windows.Forms.ToolStripMenuItem();
            this.StickToYminus = new System.Windows.Forms.ToolStripMenuItem();
            this.XPosStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.StickToXplus = new System.Windows.Forms.ToolStripMenuItem();
            this.StickToXminus = new System.Windows.Forms.ToolStripMenuItem();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BackSwitchTextBox = new SharpOcarina.NumericTextBox();
            this.FrontCamTextBox = new SharpOcarina.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FrontRoomLabel = new System.Windows.Forms.Label();
            this.FrontSwitchTextbox = new SharpOcarina.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BackCamTextBox = new SharpOcarina.NumericTextBox();
            this.ActorNumberTextbox = new SharpOcarina.NumericTextBox();
            this.ActorZPos = new SharpOcarina.NumericUpDownEx();
            this.label13 = new System.Windows.Forms.Label();
            this.ActorVariableTextbox = new SharpOcarina.NumericTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ActorXRot = new SharpOcarina.NumericUpDownEx();
            this.ActorYPos = new SharpOcarina.NumericUpDownEx();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ActorZRot = new SharpOcarina.NumericUpDownEx();
            this.ActorYRot = new SharpOcarina.NumericUpDownEx();
            this.ActorXPos = new SharpOcarina.NumericUpDownEx();
            this.label15 = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.niceLine1 = new SharpOcarina.NiceLine();
            this.ToolTipHelper = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActorListBoxValue)).BeginInit();
            this.ZPosStrip.SuspendLayout();
            this.YPosStrip.SuspendLayout();
            this.XPosStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActorZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorXRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorYPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorZRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorYRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorXPos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ActorComboBox);
            this.groupBox3.Controls.Add(this.DuplicateButton);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.DeleteButton);
            this.groupBox3.Controls.Add(this.AddButton);
            this.groupBox3.Controls.Add(this.niceLine1);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 289);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "!!!!";
            // 
            // ActorComboBox
            // 
            this.ActorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActorComboBox.FormattingEnabled = true;
            this.ActorComboBox.Location = new System.Drawing.Point(3, 19);
            this.ActorComboBox.Name = "ActorComboBox";
            this.ActorComboBox.Size = new System.Drawing.Size(121, 21);
            this.ActorComboBox.TabIndex = 24;
            this.ActorComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox2_DropDown);
            this.ActorComboBox.SelectionChangeCommitted += new System.EventHandler(this.ActorComboBox_ValueChanged);
            this.ActorComboBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ActorComboBox_MouseWheel);
            // 
            // DuplicateButton
            // 
            this.DuplicateButton.Enabled = false;
            this.DuplicateButton.Location = new System.Drawing.Point(220, 17);
            this.DuplicateButton.Name = "DuplicateButton";
            this.DuplicateButton.Size = new System.Drawing.Size(80, 23);
            this.DuplicateButton.TabIndex = 23;
            this.DuplicateButton.Text = "Duplicate ####";
            this.ToolTipHelper.SetToolTip(this.DuplicateButton, "Hold SHIFT to duplicate in front of camera");
            this.DuplicateButton.UseVisualStyleBackColor = true;
            this.DuplicateButton.Click += new System.EventHandler(this.Duplicate_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.PresetDropdown);
            this.panel2.Controls.Add(this.ActorPropertyLabel);
            this.panel2.Controls.Add(this.ActorListBoxValue);
            this.panel2.Controls.Add(this.ActorVariableListBox);
            this.panel2.Controls.Add(this.DatabaseButton);
            this.panel2.Controls.Add(this.ZPosStrip);
            this.panel2.Controls.Add(this.YPosStrip);
            this.panel2.Controls.Add(this.XPosStrip);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.BackSwitchTextBox);
            this.panel2.Controls.Add(this.FrontCamTextBox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.FrontRoomLabel);
            this.panel2.Controls.Add(this.FrontSwitchTextbox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.BackCamTextBox);
            this.panel2.Controls.Add(this.ActorNumberTextbox);
            this.panel2.Controls.Add(this.ActorZPos);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.ActorVariableTextbox);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.ActorXRot);
            this.panel2.Controls.Add(this.ActorYPos);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.ActorZRot);
            this.panel2.Controls.Add(this.ActorYRot);
            this.panel2.Controls.Add(this.ActorXPos);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Location = new System.Drawing.Point(3, 62);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(392, 224);
            this.panel2.TabIndex = 21;
            // 
            // PresetDropdown
            // 
            this.PresetDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PresetDropdown.FormattingEnabled = true;
            this.PresetDropdown.Location = new System.Drawing.Point(179, 180);
            this.PresetDropdown.Name = "PresetDropdown";
            this.PresetDropdown.Size = new System.Drawing.Size(207, 21);
            this.PresetDropdown.TabIndex = 25;
            this.PresetDropdown.Visible = false;
            this.PresetDropdown.SelectionChangeCommitted += new System.EventHandler(this.PresetDropdown_SelectionChangeCommitted);
            // 
            // ActorPropertyLabel
            // 
            this.ActorPropertyLabel.AutoSize = true;
            this.ActorPropertyLabel.Enabled = false;
            this.ActorPropertyLabel.Location = new System.Drawing.Point(176, 138);
            this.ActorPropertyLabel.Name = "ActorPropertyLabel";
            this.ActorPropertyLabel.Size = new System.Drawing.Size(37, 13);
            this.ActorPropertyLabel.TabIndex = 31;
            this.ActorPropertyLabel.Text = "Value:";
            // 
            // ActorListBoxValue
            // 
            this.ActorListBoxValue.AlwaysFireValueChanged = false;
            this.ActorListBoxValue.DisplayDigits = 1;
            this.ActorListBoxValue.DoValueRollover = true;
            this.ActorListBoxValue.Enabled = false;
            this.ActorListBoxValue.Hexadecimal = true;
            this.ActorListBoxValue.IncrementMouseWheel = 3;
            this.ActorListBoxValue.Location = new System.Drawing.Point(177, 154);
            this.ActorListBoxValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.ActorListBoxValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.ActorListBoxValue.Name = "ActorListBoxValue";
            this.ActorListBoxValue.ShiftMultiplier = 10;
            this.ActorListBoxValue.Size = new System.Drawing.Size(85, 20);
            this.ActorListBoxValue.TabIndex = 30;
            this.ActorListBoxValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorListBoxValue.ValueChanged += new System.EventHandler(this.ActorListBoxValue_ValueChanged);
            // 
            // ActorVariableListBox
            // 
            this.ActorVariableListBox.Enabled = false;
            this.ActorVariableListBox.FormattingEnabled = true;
            this.ActorVariableListBox.Location = new System.Drawing.Point(6, 132);
            this.ActorVariableListBox.Name = "ActorVariableListBox";
            this.ActorVariableListBox.Size = new System.Drawing.Size(164, 82);
            this.ActorVariableListBox.TabIndex = 29;
            this.ActorVariableListBox.Click += new System.EventHandler(this.ActorVariableListBox_Click);
            // 
            // DatabaseButton
            // 
            this.DatabaseButton.Enabled = false;
            this.DatabaseButton.Image = global::SharpOcarina.Properties.Resources.databaseicon;
            this.DatabaseButton.Location = new System.Drawing.Point(171, 2);
            this.DatabaseButton.Name = "DatabaseButton";
            this.DatabaseButton.Size = new System.Drawing.Size(22, 22);
            this.DatabaseButton.TabIndex = 25;
            this.DatabaseButton.UseVisualStyleBackColor = true;
            this.DatabaseButton.Click += new System.EventHandler(this.DatabaseButton_Click);
            // 
            // ZPosStrip
            // 
            this.ZPosStrip.AutoSize = false;
            this.ZPosStrip.BackColor = System.Drawing.Color.Transparent;
            this.ZPosStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ZPosStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton3});
            this.ZPosStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ZPosStrip.Location = new System.Drawing.Point(173, 81);
            this.ZPosStrip.Name = "ZPosStrip";
            this.ZPosStrip.Size = new System.Drawing.Size(20, 19);
            this.ZPosStrip.TabIndex = 28;
            this.ZPosStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StickToZplus,
            this.StickToZminus});
            this.toolStripDropDownButton3.Image = global::SharpOcarina.Properties.Resources.options;
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.ShowDropDownArrow = false;
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(20, 20);
            this.toolStripDropDownButton3.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton3.ToolTipText = "Actions";
            // 
            // StickToZplus
            // 
            this.StickToZplus.Name = "StickToZplus";
            this.StickToZplus.Size = new System.Drawing.Size(185, 22);
            this.StickToZplus.Text = "Stick to positive wall";
            this.StickToZplus.Click += new System.EventHandler(this.StickToWall);
            // 
            // StickToZminus
            // 
            this.StickToZminus.Name = "StickToZminus";
            this.StickToZminus.Size = new System.Drawing.Size(185, 22);
            this.StickToZminus.Text = "Stick to negative wall";
            this.StickToZminus.Click += new System.EventHandler(this.StickToWall);
            // 
            // YPosStrip
            // 
            this.YPosStrip.AutoSize = false;
            this.YPosStrip.BackColor = System.Drawing.Color.Transparent;
            this.YPosStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.YPosStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton2});
            this.YPosStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.YPosStrip.Location = new System.Drawing.Point(173, 55);
            this.YPosStrip.Name = "YPosStrip";
            this.YPosStrip.Size = new System.Drawing.Size(20, 19);
            this.YPosStrip.TabIndex = 27;
            this.YPosStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StickToYplus,
            this.StickToYminus});
            this.toolStripDropDownButton2.Image = global::SharpOcarina.Properties.Resources.options;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.ShowDropDownArrow = false;
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(20, 20);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton2.ToolTipText = "Actions";
            // 
            // StickToYplus
            // 
            this.StickToYplus.Name = "StickToYplus";
            this.StickToYplus.Size = new System.Drawing.Size(155, 22);
            this.StickToYplus.Text = "Stick to ceiling";
            this.StickToYplus.Click += new System.EventHandler(this.StickToWall);
            // 
            // StickToYminus
            // 
            this.StickToYminus.Name = "StickToYminus";
            this.StickToYminus.Size = new System.Drawing.Size(155, 22);
            this.StickToYminus.Text = "Stick to ground";
            this.StickToYminus.Click += new System.EventHandler(this.StickToWall);
            // 
            // XPosStrip
            // 
            this.XPosStrip.AutoSize = false;
            this.XPosStrip.BackColor = System.Drawing.Color.Transparent;
            this.XPosStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.XPosStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.XPosStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.XPosStrip.Location = new System.Drawing.Point(173, 29);
            this.XPosStrip.Name = "XPosStrip";
            this.XPosStrip.Size = new System.Drawing.Size(20, 19);
            this.XPosStrip.TabIndex = 26;
            this.XPosStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StickToXplus,
            this.StickToXminus});
            this.toolStripDropDownButton1.Image = global::SharpOcarina.Properties.Resources.options;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ShowDropDownArrow = false;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(20, 20);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ToolTipText = "Actions";
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // StickToXplus
            // 
            this.StickToXplus.Name = "StickToXplus";
            this.StickToXplus.Size = new System.Drawing.Size(185, 22);
            this.StickToXplus.Text = "Stick to positive wall";
            this.StickToXplus.Click += new System.EventHandler(this.StickToWall);
            // 
            // StickToXminus
            // 
            this.StickToXminus.Name = "StickToXminus";
            this.StickToXminus.Size = new System.Drawing.Size(185, 22);
            this.StickToXminus.Text = "Stick to negative wall";
            this.StickToXminus.Click += new System.EventHandler(this.StickToWall);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(196, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Variable:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(303, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Cam:";
            // 
            // BackSwitchTextBox
            // 
            this.BackSwitchTextBox.AllowHex = true;
            this.BackSwitchTextBox.Digits = 4;
            this.BackSwitchTextBox.Enabled = false;
            this.BackSwitchTextBox.Location = new System.Drawing.Point(267, 107);
            this.BackSwitchTextBox.Name = "BackSwitchTextBox";
            this.BackSwitchTextBox.Size = new System.Drawing.Size(30, 20);
            this.BackSwitchTextBox.TabIndex = 20;
            this.BackSwitchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericTextBox6_KeyDown);
            this.BackSwitchTextBox.Leave += new System.EventHandler(this.GenericTextbox_Leave);
            // 
            // FrontCamTextBox
            // 
            this.FrontCamTextBox.AllowHex = true;
            this.FrontCamTextBox.Digits = 4;
            this.FrontCamTextBox.Enabled = false;
            this.FrontCamTextBox.Location = new System.Drawing.Point(144, 107);
            this.FrontCamTextBox.Name = "FrontCamTextBox";
            this.FrontCamTextBox.Size = new System.Drawing.Size(30, 20);
            this.FrontCamTextBox.TabIndex = 19;
            this.FrontCamTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericTextBox5_KeyDown);
            this.FrontCamTextBox.Leave += new System.EventHandler(this.GenericTextbox_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(110, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Cam:";
            // 
            // FrontRoomLabel
            // 
            this.FrontRoomLabel.AutoSize = true;
            this.FrontRoomLabel.Enabled = false;
            this.FrontRoomLabel.Location = new System.Drawing.Point(3, 110);
            this.FrontRoomLabel.Name = "FrontRoomLabel";
            this.FrontRoomLabel.Size = new System.Drawing.Size(65, 13);
            this.FrontRoomLabel.TabIndex = 21;
            this.FrontRoomLabel.Text = "Front Room:";
            // 
            // FrontSwitchTextbox
            // 
            this.FrontSwitchTextbox.AllowHex = true;
            this.FrontSwitchTextbox.Digits = 4;
            this.FrontSwitchTextbox.Enabled = false;
            this.FrontSwitchTextbox.Location = new System.Drawing.Point(74, 107);
            this.FrontSwitchTextbox.Name = "FrontSwitchTextbox";
            this.FrontSwitchTextbox.Size = new System.Drawing.Size(30, 20);
            this.FrontSwitchTextbox.TabIndex = 18;
            this.FrontSwitchTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericTextBox4_KeyDown);
            this.FrontSwitchTextbox.Leave += new System.EventHandler(this.GenericTextbox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(196, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Back Room:";
            // 
            // BackCamTextBox
            // 
            this.BackCamTextBox.AllowHex = true;
            this.BackCamTextBox.Digits = 4;
            this.BackCamTextBox.Enabled = false;
            this.BackCamTextBox.Location = new System.Drawing.Point(337, 107);
            this.BackCamTextBox.Name = "BackCamTextBox";
            this.BackCamTextBox.Size = new System.Drawing.Size(30, 20);
            this.BackCamTextBox.TabIndex = 21;
            this.BackCamTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericTextBox3_KeyDown);
            this.BackCamTextBox.Leave += new System.EventHandler(this.GenericTextbox_Leave);
            // 
            // ActorNumberTextbox
            // 
            this.ActorNumberTextbox.AllowHex = true;
            this.ActorNumberTextbox.Digits = 4;
            this.ActorNumberTextbox.Enabled = false;
            this.ActorNumberTextbox.Location = new System.Drawing.Point(74, 3);
            this.ActorNumberTextbox.Name = "ActorNumberTextbox";
            this.ActorNumberTextbox.Size = new System.Drawing.Size(96, 20);
            this.ActorNumberTextbox.TabIndex = 3;
            this.ActorNumberTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericTextBox1_KeyDown);
            this.ActorNumberTextbox.Leave += new System.EventHandler(this.GenericTextbox_Leave);
            // 
            // ActorZPos
            // 
            this.ActorZPos.AlwaysFireValueChanged = false;
            this.ActorZPos.DisplayDigits = 1;
            this.ActorZPos.DoValueRollover = false;
            this.ActorZPos.Enabled = false;
            this.ActorZPos.IncrementMouseWheel = 1;
            this.ActorZPos.Location = new System.Drawing.Point(74, 81);
            this.ActorZPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ActorZPos.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.ActorZPos.Name = "ActorZPos";
            this.ActorZPos.ShiftMultiplier = 20;
            this.ActorZPos.Size = new System.Drawing.Size(100, 20);
            this.ActorZPos.TabIndex = 10;
            this.ToolTipHelper.SetToolTip(this.ActorZPos, "Hold middleclick on the actor in the viewport to move it with the mouse");
            this.ActorZPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorZPos.ValueChanged += new System.EventHandler(this.ActorPos_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Enabled = false;
            this.label13.Location = new System.Drawing.Point(3, 83);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Z Position:";
            // 
            // ActorVariableTextbox
            // 
            this.ActorVariableTextbox.AllowHex = true;
            this.ActorVariableTextbox.Digits = 4;
            this.ActorVariableTextbox.Enabled = false;
            this.ActorVariableTextbox.Location = new System.Drawing.Point(267, 3);
            this.ActorVariableTextbox.Name = "ActorVariableTextbox";
            this.ActorVariableTextbox.Size = new System.Drawing.Size(100, 20);
            this.ActorVariableTextbox.TabIndex = 5;
            this.ActorVariableTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericTextBox2_KeyDown);
            this.ActorVariableTextbox.Leave += new System.EventHandler(this.GenericTextbox_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(3, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Y Position:";
            // 
            // ActorXRot
            // 
            this.ActorXRot.AlwaysFireValueChanged = false;
            this.ActorXRot.DisplayDigits = 1;
            this.ActorXRot.DoValueRollover = false;
            this.ActorXRot.Enabled = false;
            this.ActorXRot.Increment = new decimal(new int[] {
            182,
            0,
            0,
            0});
            this.ActorXRot.IncrementMouseWheel = 182;
            this.ActorXRot.Location = new System.Drawing.Point(267, 29);
            this.ActorXRot.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ActorXRot.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.ActorXRot.Name = "ActorXRot";
            this.ActorXRot.ShiftMultiplier = 9;
            this.ActorXRot.Size = new System.Drawing.Size(100, 20);
            this.ActorXRot.TabIndex = 12;
            this.ActorXRot.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorXRot.ValueChanged += new System.EventHandler(this.ActorRot_ValueChanged);
            this.ActorXRot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XRot_KeyDown);
            // 
            // ActorYPos
            // 
            this.ActorYPos.AlwaysFireValueChanged = false;
            this.ActorYPos.DisplayDigits = 1;
            this.ActorYPos.DoValueRollover = false;
            this.ActorYPos.Enabled = false;
            this.ActorYPos.IncrementMouseWheel = 1;
            this.ActorYPos.Location = new System.Drawing.Point(74, 55);
            this.ActorYPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ActorYPos.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.ActorYPos.Name = "ActorYPos";
            this.ActorYPos.ShiftMultiplier = 20;
            this.ActorYPos.Size = new System.Drawing.Size(100, 20);
            this.ActorYPos.TabIndex = 8;
            this.ToolTipHelper.SetToolTip(this.ActorYPos, "Hold middleclick on the actor in the viewport to move it with the mouse");
            this.ActorYPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorYPos.ValueChanged += new System.EventHandler(this.ActorPos_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Enabled = false;
            this.label14.Location = new System.Drawing.Point(196, 83);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Z Rotation:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Enabled = false;
            this.label16.Location = new System.Drawing.Point(196, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "X Rotation:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(3, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "ID:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Enabled = false;
            this.label11.Location = new System.Drawing.Point(3, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "X Position:";
            // 
            // ActorZRot
            // 
            this.ActorZRot.AlwaysFireValueChanged = false;
            this.ActorZRot.DisplayDigits = 1;
            this.ActorZRot.DoValueRollover = false;
            this.ActorZRot.Enabled = false;
            this.ActorZRot.Increment = new decimal(new int[] {
            182,
            0,
            0,
            0});
            this.ActorZRot.IncrementMouseWheel = 182;
            this.ActorZRot.Location = new System.Drawing.Point(267, 81);
            this.ActorZRot.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ActorZRot.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.ActorZRot.Name = "ActorZRot";
            this.ActorZRot.ShiftMultiplier = 9;
            this.ActorZRot.Size = new System.Drawing.Size(100, 20);
            this.ActorZRot.TabIndex = 16;
            this.ActorZRot.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorZRot.ValueChanged += new System.EventHandler(this.ActorRot_ValueChanged);
            this.ActorZRot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZRot_KeyDown);
            // 
            // ActorYRot
            // 
            this.ActorYRot.AlwaysFireValueChanged = false;
            this.ActorYRot.DisplayDigits = 1;
            this.ActorYRot.DoValueRollover = false;
            this.ActorYRot.Enabled = false;
            this.ActorYRot.Increment = new decimal(new int[] {
            182,
            0,
            0,
            0});
            this.ActorYRot.IncrementMouseWheel = 182;
            this.ActorYRot.Location = new System.Drawing.Point(267, 55);
            this.ActorYRot.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ActorYRot.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.ActorYRot.Name = "ActorYRot";
            this.ActorYRot.ShiftMultiplier = 9;
            this.ActorYRot.Size = new System.Drawing.Size(100, 20);
            this.ActorYRot.TabIndex = 14;
            this.ToolTipHelper.SetToolTip(this.ActorYRot, "You can use the mousewheel in the viewport to rotate the actor");
            this.ActorYRot.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorYRot.ValueChanged += new System.EventHandler(this.ActorRot_ValueChanged);
            this.ActorYRot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.YRot_KeyDown);
            // 
            // ActorXPos
            // 
            this.ActorXPos.AlwaysFireValueChanged = false;
            this.ActorXPos.DisplayDigits = 1;
            this.ActorXPos.DoValueRollover = false;
            this.ActorXPos.Enabled = false;
            this.ActorXPos.IncrementMouseWheel = 1;
            this.ActorXPos.Location = new System.Drawing.Point(74, 29);
            this.ActorXPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ActorXPos.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.ActorXPos.Name = "ActorXPos";
            this.ActorXPos.ShiftMultiplier = 20;
            this.ActorXPos.Size = new System.Drawing.Size(100, 20);
            this.ActorXPos.TabIndex = 6;
            this.ToolTipHelper.SetToolTip(this.ActorXPos, "Hold middleclick on the actor in the viewport to move it with the mouse");
            this.ActorXPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorXPos.ValueChanged += new System.EventHandler(this.ActorPos_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Enabled = false;
            this.label15.Location = new System.Drawing.Point(196, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "Y Rotation:";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Location = new System.Drawing.Point(309, 17);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(80, 23);
            this.DeleteButton.TabIndex = 19;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // AddButton
            // 
            this.AddButton.Enabled = false;
            this.AddButton.Location = new System.Drawing.Point(130, 17);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(80, 23);
            this.AddButton.TabIndex = 18;
            this.AddButton.Text = "Add";
            this.ToolTipHelper.SetToolTip(this.AddButton, "Hold SHIFT to add in front of camera");
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(9, 46);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(383, 15);
            this.niceLine1.TabIndex = 20;
            this.niceLine1.TabStop = false;
            // 
            // ActorEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Name = "ActorEditControl";
            this.Size = new System.Drawing.Size(404, 295);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActorListBoxValue)).EndInit();
            this.ZPosStrip.ResumeLayout(false);
            this.ZPosStrip.PerformLayout();
            this.YPosStrip.ResumeLayout(false);
            this.YPosStrip.PerformLayout();
            this.XPosStrip.ResumeLayout(false);
            this.XPosStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActorZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorXRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorYPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorZRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorYRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorXPos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel2;
        private SharpOcarina.NumericTextBox ActorNumberTextbox;
        private SharpOcarina.NumericUpDownEx ActorZPos;
        private System.Windows.Forms.Label label13;
        private SharpOcarina.NumericTextBox ActorVariableTextbox;
        private System.Windows.Forms.Label label12;
        private SharpOcarina.NumericUpDownEx ActorXRot;
        private SharpOcarina.NumericUpDownEx ActorYPos;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private SharpOcarina.NumericUpDownEx ActorZRot;
        private SharpOcarina.NumericUpDownEx ActorYRot;
        private System.Windows.Forms.Label label10;
        private SharpOcarina.NumericUpDownEx ActorXPos;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private SharpOcarina.NiceLine niceLine1;
        private SharpOcarina.NumericTextBox BackCamTextBox;
        private SharpOcarina.NumericTextBox BackSwitchTextBox;
        private SharpOcarina.NumericTextBox FrontCamTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label FrontRoomLabel;
        private SharpOcarina.NumericTextBox FrontSwitchTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button DuplicateButton;
        public System.Windows.Forms.ComboBox ActorComboBox;
        private System.Windows.Forms.ToolStrip XPosStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem StickToXplus;
        private System.Windows.Forms.ToolStripMenuItem StickToXminus;
        private System.Windows.Forms.ToolStrip YPosStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem StickToYplus;
        private System.Windows.Forms.ToolStripMenuItem StickToYminus;
        private System.Windows.Forms.ToolStrip ZPosStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem StickToZplus;
        private System.Windows.Forms.ToolStripMenuItem StickToZminus;
        private System.Windows.Forms.Button DatabaseButton;
        private NumericUpDownEx ActorListBoxValue;
        private System.Windows.Forms.ListBox ActorVariableListBox;
        private System.Windows.Forms.Label ActorPropertyLabel;
        public System.Windows.Forms.ComboBox PresetDropdown;
        private System.Windows.Forms.ToolTip ToolTipHelper;
    }
}
