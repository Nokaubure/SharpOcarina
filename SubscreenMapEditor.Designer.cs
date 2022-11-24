namespace SharpOcarina
{
    partial class SubscreenMapEditor
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.TitleCardLoadFromFile = new System.Windows.Forms.Button();
            this.TitleCardExtract = new System.Windows.Forms.Button();
            this.TitleTextureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.TitleCardCopyFromScene = new System.Windows.Forms.Button();
            this.MapTextureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Floor1 = new System.Windows.Forms.Button();
            this.Floor2 = new System.Windows.Forms.Button();
            this.Floor3 = new System.Windows.Forms.Button();
            this.Floor4 = new System.Windows.Forms.Button();
            this.Floor5 = new System.Windows.Forms.Button();
            this.Floor6 = new System.Windows.Forms.Button();
            this.Floor7 = new System.Windows.Forms.Button();
            this.Floor8 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.IconListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.IconX = new System.Windows.Forms.NumericUpDown();
            this.IconY = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.IconChestFlag = new SharpOcarina.NumericUpDownEx();
            this.IconBossCheckbox = new System.Windows.Forms.CheckBox();
            this.MapID = new System.Windows.Forms.NumericUpDown();
            this.SaveToROM = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.MapFloorTextureID = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.FloorBossSkullIcon = new System.Windows.Forms.PictureBox();
            this.MapBossCheckbox = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.AddIcon = new System.Windows.Forms.Button();
            this.DeleteIcon = new System.Windows.Forms.Button();
            this.MapExtract = new System.Windows.Forms.Button();
            this.MapLoadFromFile = new System.Windows.Forms.Button();
            this.MapFloorHeight = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.MapFloorLabel = new System.Windows.Forms.NumericUpDown();
            this.PaletteListBox = new System.Windows.Forms.ListBox();
            this.PaletteIndex = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.RemoveFloorButton = new System.Windows.Forms.Button();
            this.GenerateMapButton = new System.Windows.Forms.Button();
            this.ClearAllMapsButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TitleTextureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapTextureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconChestFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapFloorTextureID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorBossSkullIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapFloorHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapFloorLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaletteIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(549, 31);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(82, 13);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "Map Title Card: ";
            // 
            // TitleCardLoadFromFile
            // 
            this.TitleCardLoadFromFile.Location = new System.Drawing.Point(569, 130);
            this.TitleCardLoadFromFile.Name = "TitleCardLoadFromFile";
            this.TitleCardLoadFromFile.Size = new System.Drawing.Size(88, 23);
            this.TitleCardLoadFromFile.TabIndex = 5;
            this.TitleCardLoadFromFile.Text = "Load from file";
            this.TitleCardLoadFromFile.UseVisualStyleBackColor = true;
            this.TitleCardLoadFromFile.Click += new System.EventHandler(this.TitleCardLoad);
            // 
            // TitleCardExtract
            // 
            this.TitleCardExtract.Location = new System.Drawing.Point(732, 130);
            this.TitleCardExtract.Name = "TitleCardExtract";
            this.TitleCardExtract.Size = new System.Drawing.Size(93, 23);
            this.TitleCardExtract.TabIndex = 6;
            this.TitleCardExtract.Text = "Extract Texture";
            this.TitleCardExtract.UseVisualStyleBackColor = true;
            this.TitleCardExtract.Click += new System.EventHandler(this.ExtractButton_Click);
            // 
            // TitleTextureBox
            // 
            this.TitleTextureBox.BackColor = System.Drawing.Color.Aqua;
            this.TitleTextureBox.Location = new System.Drawing.Point(552, 57);
            this.TitleTextureBox.Name = "TitleTextureBox";
            this.TitleTextureBox.Size = new System.Drawing.Size(288, 48);
            this.TitleTextureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TitleTextureBox.TabIndex = 4;
            this.TitleTextureBox.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Map ID:";
            // 
            // TitleCardCopyFromScene
            // 
            this.TitleCardCopyFromScene.Location = new System.Drawing.Point(617, 111);
            this.TitleCardCopyFromScene.Name = "TitleCardCopyFromScene";
            this.TitleCardCopyFromScene.Size = new System.Drawing.Size(160, 23);
            this.TitleCardCopyFromScene.TabIndex = 18;
            this.TitleCardCopyFromScene.Text = "Use same as scene title card";
            this.TitleCardCopyFromScene.UseVisualStyleBackColor = true;
            this.TitleCardCopyFromScene.Visible = false;
            this.TitleCardCopyFromScene.Click += new System.EventHandler(this.TitleCardCopyFromScene_Click);
            // 
            // MapTextureBox
            // 
            this.MapTextureBox.BackColor = System.Drawing.Color.Aqua;
            this.MapTextureBox.Location = new System.Drawing.Point(133, 57);
            this.MapTextureBox.Name = "MapTextureBox";
            this.MapTextureBox.Size = new System.Drawing.Size(384, 340);
            this.MapTextureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MapTextureBox.TabIndex = 20;
            this.MapTextureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Floors:";
            // 
            // Floor1
            // 
            this.Floor1.BackgroundImage = global::SharpOcarina.Properties.Resources._1F;
            this.Floor1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Floor1.Location = new System.Drawing.Point(15, 80);
            this.Floor1.Name = "Floor1";
            this.Floor1.Size = new System.Drawing.Size(64, 32);
            this.Floor1.TabIndex = 22;
            this.Floor1.Text = "1";
            this.Floor1.UseVisualStyleBackColor = true;
            this.Floor1.Click += new System.EventHandler(this.Floor1_Click);
            // 
            // Floor2
            // 
            this.Floor2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Floor2.Location = new System.Drawing.Point(15, 110);
            this.Floor2.Name = "Floor2";
            this.Floor2.Size = new System.Drawing.Size(64, 32);
            this.Floor2.TabIndex = 23;
            this.Floor2.Text = "2";
            this.Floor2.UseVisualStyleBackColor = true;
            this.Floor2.Click += new System.EventHandler(this.Floor1_Click);
            // 
            // Floor3
            // 
            this.Floor3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Floor3.Location = new System.Drawing.Point(15, 139);
            this.Floor3.Name = "Floor3";
            this.Floor3.Size = new System.Drawing.Size(64, 32);
            this.Floor3.TabIndex = 24;
            this.Floor3.Text = "3";
            this.Floor3.UseVisualStyleBackColor = true;
            this.Floor3.Click += new System.EventHandler(this.Floor1_Click);
            // 
            // Floor4
            // 
            this.Floor4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Floor4.Location = new System.Drawing.Point(15, 168);
            this.Floor4.Name = "Floor4";
            this.Floor4.Size = new System.Drawing.Size(64, 32);
            this.Floor4.TabIndex = 25;
            this.Floor4.Text = "4";
            this.Floor4.UseVisualStyleBackColor = true;
            this.Floor4.Click += new System.EventHandler(this.Floor1_Click);
            // 
            // Floor5
            // 
            this.Floor5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Floor5.Location = new System.Drawing.Point(15, 197);
            this.Floor5.Name = "Floor5";
            this.Floor5.Size = new System.Drawing.Size(64, 32);
            this.Floor5.TabIndex = 26;
            this.Floor5.Text = "5";
            this.Floor5.UseVisualStyleBackColor = true;
            this.Floor5.Click += new System.EventHandler(this.Floor1_Click);
            // 
            // Floor6
            // 
            this.Floor6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Floor6.Location = new System.Drawing.Point(15, 226);
            this.Floor6.Name = "Floor6";
            this.Floor6.Size = new System.Drawing.Size(64, 32);
            this.Floor6.TabIndex = 27;
            this.Floor6.Text = "6";
            this.Floor6.UseVisualStyleBackColor = true;
            this.Floor6.Click += new System.EventHandler(this.Floor1_Click);
            // 
            // Floor7
            // 
            this.Floor7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Floor7.Location = new System.Drawing.Point(15, 255);
            this.Floor7.Name = "Floor7";
            this.Floor7.Size = new System.Drawing.Size(64, 32);
            this.Floor7.TabIndex = 28;
            this.Floor7.Text = "7";
            this.Floor7.UseVisualStyleBackColor = true;
            this.Floor7.Click += new System.EventHandler(this.Floor1_Click);
            // 
            // Floor8
            // 
            this.Floor8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Floor8.Location = new System.Drawing.Point(15, 284);
            this.Floor8.Name = "Floor8";
            this.Floor8.Size = new System.Drawing.Size(64, 32);
            this.Floor8.TabIndex = 29;
            this.Floor8.Text = "8";
            this.Floor8.UseVisualStyleBackColor = true;
            this.Floor8.Click += new System.EventHandler(this.Floor1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(582, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Compass icons:";
            // 
            // IconListBox
            // 
            this.IconListBox.FormattingEnabled = true;
            this.IconListBox.Location = new System.Drawing.Point(552, 227);
            this.IconListBox.Name = "IconListBox";
            this.IconListBox.Size = new System.Drawing.Size(152, 173);
            this.IconListBox.TabIndex = 31;
            this.IconListBox.Click += new System.EventHandler(this.IconListBox_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(775, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "X:";
            // 
            // IconX
            // 
            this.IconX.Location = new System.Drawing.Point(798, 273);
            this.IconX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.IconX.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.IconX.Name = "IconX";
            this.IconX.Size = new System.Drawing.Size(56, 20);
            this.IconX.TabIndex = 33;
            this.IconX.ValueChanged += new System.EventHandler(this.IconX_ValueChanged);
            // 
            // IconY
            // 
            this.IconY.Location = new System.Drawing.Point(798, 299);
            this.IconY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.IconY.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.IconY.Name = "IconY";
            this.IconY.Size = new System.Drawing.Size(56, 20);
            this.IconY.TabIndex = 35;
            this.IconY.ValueChanged += new System.EventHandler(this.IconY_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(775, 301);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(732, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Chest Flag:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // IconChestFlag
            // 
            this.IconChestFlag.AlwaysFireValueChanged = false;
            this.IconChestFlag.DisplayDigits = 1;
            this.IconChestFlag.DoValueRollover = true;
            this.IconChestFlag.Hexadecimal = true;
            this.IconChestFlag.IncrementMouseWheel = 3;
            this.IconChestFlag.Location = new System.Drawing.Point(798, 325);
            this.IconChestFlag.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.IconChestFlag.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.IconChestFlag.Name = "IconChestFlag";
            this.IconChestFlag.Size = new System.Drawing.Size(56, 20);
            this.IconChestFlag.TabIndex = 37;
            this.IconChestFlag.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.IconChestFlag.ValueChanged += new System.EventHandler(this.IconChestFlag_ValueChanged);
            // 
            // IconBossCheckbox
            // 
            this.IconBossCheckbox.AutoSize = true;
            this.IconBossCheckbox.Location = new System.Drawing.Point(779, 351);
            this.IconBossCheckbox.Name = "IconBossCheckbox";
            this.IconBossCheckbox.Size = new System.Drawing.Size(75, 17);
            this.IconBossCheckbox.TabIndex = 40;
            this.IconBossCheckbox.Text = "Boss Skull";
            this.IconBossCheckbox.UseVisualStyleBackColor = true;
            this.IconBossCheckbox.CheckedChanged += new System.EventHandler(this.IconBossCheckbox_CheckedChanged);
            // 
            // MapID
            // 
            this.MapID.Location = new System.Drawing.Point(214, 28);
            this.MapID.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.MapID.Name = "MapID";
            this.MapID.Size = new System.Drawing.Size(56, 20);
            this.MapID.TabIndex = 41;
            this.MapID.ValueChanged += new System.EventHandler(this.MapID_ValueChanged);
            this.MapID.Enter += new System.EventHandler(this.MapID_Enter);
            // 
            // SaveToROM
            // 
            this.SaveToROM.Location = new System.Drawing.Point(274, 552);
            this.SaveToROM.Name = "SaveToROM";
            this.SaveToROM.Size = new System.Drawing.Size(88, 23);
            this.SaveToROM.TabIndex = 42;
            this.SaveToROM.Text = "Save to ROM";
            this.SaveToROM.UseVisualStyleBackColor = true;
            this.SaveToROM.Click += new System.EventHandler(this.SaveToROM_Click);
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(468, 552);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(88, 23);
            this.Close.TabIndex = 43;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(130, 443);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Floor Map Texture ID:";
            // 
            // MapFloorTextureID
            // 
            this.MapFloorTextureID.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MapFloorTextureID.Location = new System.Drawing.Point(246, 441);
            this.MapFloorTextureID.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.MapFloorTextureID.Name = "MapFloorTextureID";
            this.MapFloorTextureID.Size = new System.Drawing.Size(56, 20);
            this.MapFloorTextureID.TabIndex = 45;
            this.MapFloorTextureID.ValueChanged += new System.EventHandler(this.MapFloorTextureID_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(130, 496);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 46;
            this.label9.Text = "Floor Label:";
            // 
            // FloorBossSkullIcon
            // 
            this.FloorBossSkullIcon.Image = global::SharpOcarina.Properties.Resources.BossSkull;
            this.FloorBossSkullIcon.Location = new System.Drawing.Point(85, 80);
            this.FloorBossSkullIcon.Name = "FloorBossSkullIcon";
            this.FloorBossSkullIcon.Size = new System.Drawing.Size(30, 32);
            this.FloorBossSkullIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FloorBossSkullIcon.TabIndex = 49;
            this.FloorBossSkullIcon.TabStop = false;
            // 
            // MapBossCheckbox
            // 
            this.MapBossCheckbox.AutoSize = true;
            this.MapBossCheckbox.Location = new System.Drawing.Point(133, 528);
            this.MapBossCheckbox.Name = "MapBossCheckbox";
            this.MapBossCheckbox.Size = new System.Drawing.Size(91, 17);
            this.MapBossCheckbox.TabIndex = 50;
            this.MapBossCheckbox.Text = "Boss Located";
            this.MapBossCheckbox.UseVisualStyleBackColor = true;
            this.MapBossCheckbox.Click += new System.EventHandler(this.MapBossCheckbox_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SharpOcarina.Properties.Resources.Compass;
            this.pictureBox1.Location = new System.Drawing.Point(552, 199);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 51;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SharpOcarina.Properties.Resources.Map;
            this.pictureBox2.Location = new System.Drawing.Point(133, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 52;
            this.pictureBox2.TabStop = false;
            // 
            // AddIcon
            // 
            this.AddIcon.Location = new System.Drawing.Point(724, 227);
            this.AddIcon.Name = "AddIcon";
            this.AddIcon.Size = new System.Drawing.Size(67, 23);
            this.AddIcon.TabIndex = 53;
            this.AddIcon.Text = "Add";
            this.AddIcon.UseVisualStyleBackColor = true;
            this.AddIcon.Click += new System.EventHandler(this.AddIcon_Click);
            // 
            // DeleteIcon
            // 
            this.DeleteIcon.Location = new System.Drawing.Point(798, 227);
            this.DeleteIcon.Name = "DeleteIcon";
            this.DeleteIcon.Size = new System.Drawing.Size(70, 23);
            this.DeleteIcon.TabIndex = 54;
            this.DeleteIcon.Text = "Delete";
            this.DeleteIcon.UseVisualStyleBackColor = true;
            this.DeleteIcon.Click += new System.EventHandler(this.DeleteIcon_Click);
            // 
            // MapExtract
            // 
            this.MapExtract.Location = new System.Drawing.Point(329, 407);
            this.MapExtract.Name = "MapExtract";
            this.MapExtract.Size = new System.Drawing.Size(93, 23);
            this.MapExtract.TabIndex = 56;
            this.MapExtract.Text = "Extract Texture";
            this.MapExtract.UseVisualStyleBackColor = true;
            this.MapExtract.Click += new System.EventHandler(this.MapExtract_Click);
            // 
            // MapLoadFromFile
            // 
            this.MapLoadFromFile.Location = new System.Drawing.Point(166, 407);
            this.MapLoadFromFile.Name = "MapLoadFromFile";
            this.MapLoadFromFile.Size = new System.Drawing.Size(88, 23);
            this.MapLoadFromFile.TabIndex = 55;
            this.MapLoadFromFile.Text = "Load from file";
            this.MapLoadFromFile.UseVisualStyleBackColor = true;
            this.MapLoadFromFile.Click += new System.EventHandler(this.MapLoadFromFile_Click);
            // 
            // MapFloorHeight
            // 
            this.MapFloorHeight.Location = new System.Drawing.Point(246, 467);
            this.MapFloorHeight.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.MapFloorHeight.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.MapFloorHeight.Name = "MapFloorHeight";
            this.MapFloorHeight.Size = new System.Drawing.Size(56, 20);
            this.MapFloorHeight.TabIndex = 58;
            this.MapFloorHeight.ValueChanged += new System.EventHandler(this.MapFloorHeight_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(130, 469);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 57;
            this.label10.Text = "Floor Height:";
            // 
            // MapFloorLabel
            // 
            this.MapFloorLabel.Location = new System.Drawing.Point(246, 493);
            this.MapFloorLabel.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.MapFloorLabel.Name = "MapFloorLabel";
            this.MapFloorLabel.Size = new System.Drawing.Size(56, 20);
            this.MapFloorLabel.TabIndex = 59;
            this.MapFloorLabel.ValueChanged += new System.EventHandler(this.MapFloorLabel_ValueChanged);
            // 
            // PaletteListBox
            // 
            this.PaletteListBox.FormattingEnabled = true;
            this.PaletteListBox.Location = new System.Drawing.Point(329, 436);
            this.PaletteListBox.Name = "PaletteListBox";
            this.PaletteListBox.Size = new System.Drawing.Size(152, 95);
            this.PaletteListBox.TabIndex = 60;
            this.PaletteListBox.Click += new System.EventHandler(this.PaletteListBox_Click);
            // 
            // PaletteIndex
            // 
            this.PaletteIndex.Location = new System.Drawing.Point(569, 436);
            this.PaletteIndex.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.PaletteIndex.Name = "PaletteIndex";
            this.PaletteIndex.Size = new System.Drawing.Size(56, 20);
            this.PaletteIndex.TabIndex = 62;
            this.PaletteIndex.ValueChanged += new System.EventHandler(this.PaletteIndex_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(491, 438);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 61;
            this.label11.Text = "Palette Index:";
            // 
            // RemoveFloorButton
            // 
            this.RemoveFloorButton.Location = new System.Drawing.Point(15, 357);
            this.RemoveFloorButton.Name = "RemoveFloorButton";
            this.RemoveFloorButton.Size = new System.Drawing.Size(88, 23);
            this.RemoveFloorButton.TabIndex = 63;
            this.RemoveFloorButton.Text = "Remove Floor";
            this.RemoveFloorButton.UseVisualStyleBackColor = true;
            this.RemoveFloorButton.Click += new System.EventHandler(this.RemoveFloorButton_Click);
            // 
            // GenerateMapButton
            // 
            this.GenerateMapButton.Location = new System.Drawing.Point(724, 436);
            this.GenerateMapButton.Name = "GenerateMapButton";
            this.GenerateMapButton.Size = new System.Drawing.Size(101, 23);
            this.GenerateMapButton.TabIndex = 64;
            this.GenerateMapButton.Text = "Mapgen (broken)";
            this.GenerateMapButton.UseVisualStyleBackColor = true;
            this.GenerateMapButton.Visible = false;
            this.GenerateMapButton.Click += new System.EventHandler(this.GenerateMap_Click);
            // 
            // ClearAllMapsButton
            // 
            this.ClearAllMapsButton.Location = new System.Drawing.Point(724, 508);
            this.ClearAllMapsButton.Name = "ClearAllMapsButton";
            this.ClearAllMapsButton.Size = new System.Drawing.Size(88, 23);
            this.ClearAllMapsButton.TabIndex = 65;
            this.ClearAllMapsButton.Text = "Clear All Maps";
            this.ClearAllMapsButton.UseVisualStyleBackColor = true;
            this.ClearAllMapsButton.Click += new System.EventHandler(this.ClearAllMapsButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(724, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 23);
            this.button1.TabIndex = 66;
            this.button1.Text = "Autoplace compass icons";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AutoPlaceIcons_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(724, 465);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 67;
            this.button2.Text = "debug";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SubscreenMapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 587);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ClearAllMapsButton);
            this.Controls.Add(this.GenerateMapButton);
            this.Controls.Add(this.RemoveFloorButton);
            this.Controls.Add(this.PaletteIndex);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.PaletteListBox);
            this.Controls.Add(this.MapFloorLabel);
            this.Controls.Add(this.MapFloorHeight);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.MapExtract);
            this.Controls.Add(this.MapLoadFromFile);
            this.Controls.Add(this.DeleteIcon);
            this.Controls.Add(this.AddIcon);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.MapBossCheckbox);
            this.Controls.Add(this.FloorBossSkullIcon);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.MapFloorTextureID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.SaveToROM);
            this.Controls.Add(this.MapID);
            this.Controls.Add(this.IconBossCheckbox);
            this.Controls.Add(this.IconChestFlag);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.IconY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.IconX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IconListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Floor8);
            this.Controls.Add(this.Floor7);
            this.Controls.Add(this.Floor6);
            this.Controls.Add(this.Floor5);
            this.Controls.Add(this.Floor4);
            this.Controls.Add(this.Floor3);
            this.Controls.Add(this.Floor2);
            this.Controls.Add(this.Floor1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MapTextureBox);
            this.Controls.Add(this.TitleCardCopyFromScene);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TitleCardExtract);
            this.Controls.Add(this.TitleCardLoadFromFile);
            this.Controls.Add(this.TitleTextureBox);
            this.Controls.Add(this.TitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SubscreenMapEditor";
            this.Text = "Subscreen Map Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SubscreenMapEditor_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.TitleTextureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapTextureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconChestFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapFloorTextureID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorBossSkullIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapFloorHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapFloorLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaletteIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.PictureBox TitleTextureBox;
        private System.Windows.Forms.Button TitleCardLoadFromFile;
        private System.Windows.Forms.Button TitleCardExtract;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button TitleCardCopyFromScene;
        private System.Windows.Forms.PictureBox MapTextureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Floor1;
        private System.Windows.Forms.Button Floor2;
        private System.Windows.Forms.Button Floor3;
        private System.Windows.Forms.Button Floor4;
        private System.Windows.Forms.Button Floor5;
        private System.Windows.Forms.Button Floor6;
        private System.Windows.Forms.Button Floor7;
        private System.Windows.Forms.Button Floor8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox IconListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown IconX;
        private System.Windows.Forms.NumericUpDown IconY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private NumericUpDownEx IconChestFlag;
        private System.Windows.Forms.CheckBox IconBossCheckbox;
        private System.Windows.Forms.NumericUpDown MapID;
        private System.Windows.Forms.Button SaveToROM;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown MapFloorTextureID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox FloorBossSkullIcon;
        private System.Windows.Forms.CheckBox MapBossCheckbox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button AddIcon;
        private System.Windows.Forms.Button DeleteIcon;
        private System.Windows.Forms.Button MapExtract;
        private System.Windows.Forms.Button MapLoadFromFile;
        private System.Windows.Forms.NumericUpDown MapFloorHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown MapFloorLabel;
        private System.Windows.Forms.ListBox PaletteListBox;
        private System.Windows.Forms.NumericUpDown PaletteIndex;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button RemoveFloorButton;
        private System.Windows.Forms.Button GenerateMapButton;
        private System.Windows.Forms.Button ClearAllMapsButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}