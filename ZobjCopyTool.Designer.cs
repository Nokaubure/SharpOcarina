namespace SharpOcarina
{
    partial class ZobjCopyToolForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SourceFilename = new System.Windows.Forms.TextBox();
            this.SourceButton = new System.Windows.Forms.Button();
            this.TargetButton = new System.Windows.Forms.Button();
            this.TargetFilename = new System.Windows.Forms.TextBox();
            this.SourceListBox = new System.Windows.Forms.ListBox();
            this.TargetListBox = new System.Windows.Forms.ListBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveAsButton = new System.Windows.Forms.Button();
            this.MoveButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.TargetOffset = new System.Windows.Forms.Button();
            this.TargetOffsetFilename = new System.Windows.Forms.TextBox();
            this.SourceOffset = new System.Windows.Forms.Button();
            this.SourceOffsetFilename = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SourceTextureFormatComboBox = new System.Windows.Forms.ComboBox();
            this.ImageFormatLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.GenerateLdButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.TargetBankNumeric = new SharpOcarina.NumericUpDownEx();
            this.SourceBankNumeric = new SharpOcarina.NumericUpDownEx();
            this.SourceTextureHeight = new SharpOcarina.NumericUpDownEx();
            this.SourceTextureWidth = new SharpOcarina.NumericUpDownEx();
            this.AlsoUpdateHfileCheckbox = new System.Windows.Forms.CheckBox();
            this.DontReloadTargetCheckbox = new System.Windows.Forms.CheckBox();
            this.AutomaticallyUpdateLDCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.TargetBankNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceBankNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceTextureHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceTextureWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source (.zobj)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Target (.zobj, can be blank)";
            // 
            // SourceFilename
            // 
            this.SourceFilename.Location = new System.Drawing.Point(68, 29);
            this.SourceFilename.Name = "SourceFilename";
            this.SourceFilename.ReadOnly = true;
            this.SourceFilename.Size = new System.Drawing.Size(609, 20);
            this.SourceFilename.TabIndex = 2;
            // 
            // SourceButton
            // 
            this.SourceButton.Location = new System.Drawing.Point(16, 29);
            this.SourceButton.Name = "SourceButton";
            this.SourceButton.Size = new System.Drawing.Size(46, 20);
            this.SourceButton.TabIndex = 3;
            this.SourceButton.Text = "Open";
            this.SourceButton.UseVisualStyleBackColor = true;
            this.SourceButton.Click += new System.EventHandler(this.SourceButton_Click);
            // 
            // TargetButton
            // 
            this.TargetButton.Location = new System.Drawing.Point(16, 107);
            this.TargetButton.Name = "TargetButton";
            this.TargetButton.Size = new System.Drawing.Size(46, 20);
            this.TargetButton.TabIndex = 5;
            this.TargetButton.Text = "Open";
            this.TargetButton.UseVisualStyleBackColor = true;
            this.TargetButton.Click += new System.EventHandler(this.TargetButton_Click);
            // 
            // TargetFilename
            // 
            this.TargetFilename.Location = new System.Drawing.Point(68, 107);
            this.TargetFilename.Name = "TargetFilename";
            this.TargetFilename.ReadOnly = true;
            this.TargetFilename.Size = new System.Drawing.Size(609, 20);
            this.TargetFilename.TabIndex = 4;
            // 
            // SourceListBox
            // 
            this.SourceListBox.FormattingEnabled = true;
            this.SourceListBox.HorizontalExtent = 1;
            this.SourceListBox.HorizontalScrollbar = true;
            this.SourceListBox.Location = new System.Drawing.Point(16, 282);
            this.SourceListBox.Name = "SourceListBox";
            this.SourceListBox.Size = new System.Drawing.Size(286, 251);
            this.SourceListBox.TabIndex = 6;
            this.toolTip1.SetToolTip(this.SourceListBox, "-");
            this.SourceListBox.Click += new System.EventHandler(this.SourceListBox_Click);
            this.SourceListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceListBox_KeyDown);
            // 
            // TargetListBox
            // 
            this.TargetListBox.FormattingEnabled = true;
            this.TargetListBox.HorizontalExtent = 1;
            this.TargetListBox.HorizontalScrollbar = true;
            this.TargetListBox.Location = new System.Drawing.Point(379, 282);
            this.TargetListBox.Name = "TargetListBox";
            this.TargetListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.TargetListBox.Size = new System.Drawing.Size(298, 251);
            this.TargetListBox.TabIndex = 7;
            this.toolTip1.SetToolTip(this.TargetListBox, "-");
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(220, 612);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Save zobj";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.Enabled = false;
            this.SaveAsButton.Location = new System.Drawing.Point(301, 612);
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(83, 23);
            this.SaveAsButton.TabIndex = 9;
            this.SaveAsButton.Text = "Save zobj As";
            this.SaveAsButton.UseVisualStyleBackColor = true;
            this.SaveAsButton.Click += new System.EventHandler(this.SaveAsButton_Click);
            // 
            // MoveButton
            // 
            this.MoveButton.Enabled = false;
            this.MoveButton.Location = new System.Drawing.Point(308, 389);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(65, 23);
            this.MoveButton.TabIndex = 10;
            this.MoveButton.Text = "Copy >>";
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TargetOffset
            // 
            this.TargetOffset.Enabled = false;
            this.TargetOffset.Location = new System.Drawing.Point(16, 146);
            this.TargetOffset.Name = "TargetOffset";
            this.TargetOffset.Size = new System.Drawing.Size(46, 20);
            this.TargetOffset.TabIndex = 18;
            this.TargetOffset.Text = "Open";
            this.TargetOffset.UseVisualStyleBackColor = true;
            this.TargetOffset.Click += new System.EventHandler(this.TargetOffset_Click);
            // 
            // TargetOffsetFilename
            // 
            this.TargetOffsetFilename.Location = new System.Drawing.Point(68, 146);
            this.TargetOffsetFilename.Name = "TargetOffsetFilename";
            this.TargetOffsetFilename.ReadOnly = true;
            this.TargetOffsetFilename.Size = new System.Drawing.Size(609, 20);
            this.TargetOffsetFilename.TabIndex = 17;
            // 
            // SourceOffset
            // 
            this.SourceOffset.Enabled = false;
            this.SourceOffset.Location = new System.Drawing.Point(16, 68);
            this.SourceOffset.Name = "SourceOffset";
            this.SourceOffset.Size = new System.Drawing.Size(46, 20);
            this.SourceOffset.TabIndex = 16;
            this.SourceOffset.Text = "Open";
            this.SourceOffset.UseVisualStyleBackColor = true;
            this.SourceOffset.Click += new System.EventHandler(this.SourceOffset_Click);
            // 
            // SourceOffsetFilename
            // 
            this.SourceOffsetFilename.Location = new System.Drawing.Point(68, 68);
            this.SourceOffsetFilename.Name = "SourceOffsetFilename";
            this.SourceOffsetFilename.ReadOnly = true;
            this.SourceOffsetFilename.Size = new System.Drawing.Size(609, 20);
            this.SourceOffsetFilename.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Target Offsets  (z64rom linker .ld, decomp .xml)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(227, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Source Offsets (z64rom linker .ld, decomp .xml)";
            // 
            // SearchButton
            // 
            this.SearchButton.Enabled = false;
            this.SearchButton.Location = new System.Drawing.Point(287, 225);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(97, 23);
            this.SearchButton.TabIndex = 19;
            this.SearchButton.Text = "Search Elements";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchLabel
            // 
            this.SearchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(193, 198);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(292, 13);
            this.SearchLabel.TabIndex = 20;
            this.SearchLabel.Text = "Source offsets will be guessed, target offsets will be guessed";
            this.SearchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SourceTextureFormatComboBox
            // 
            this.SourceTextureFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SourceTextureFormatComboBox.Enabled = false;
            this.SourceTextureFormatComboBox.FormattingEnabled = true;
            this.SourceTextureFormatComboBox.Location = new System.Drawing.Point(106, 539);
            this.SourceTextureFormatComboBox.Name = "SourceTextureFormatComboBox";
            this.SourceTextureFormatComboBox.Size = new System.Drawing.Size(141, 21);
            this.SourceTextureFormatComboBox.TabIndex = 22;
            this.SourceTextureFormatComboBox.SelectionChangeCommitted += new System.EventHandler(this.SourceTextureFormatComboBox_SelectionChangeCommitted);
            // 
            // ImageFormatLabel
            // 
            this.ImageFormatLabel.AutoSize = true;
            this.ImageFormatLabel.Location = new System.Drawing.Point(20, 542);
            this.ImageFormatLabel.Name = "ImageFormatLabel";
            this.ImageFormatLabel.Size = new System.Drawing.Size(78, 13);
            this.ImageFormatLabel.TabIndex = 21;
            this.ImageFormatLabel.Text = "Texture format:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 566);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Resolution:";
            // 
            // GenerateLdButton
            // 
            this.GenerateLdButton.Enabled = false;
            this.GenerateLdButton.Location = new System.Drawing.Point(390, 612);
            this.GenerateLdButton.Name = "GenerateLdButton";
            this.GenerateLdButton.Size = new System.Drawing.Size(83, 23);
            this.GenerateLdButton.TabIndex = 28;
            this.GenerateLdButton.Text = "Generate .ld";
            this.GenerateLdButton.UseVisualStyleBackColor = true;
            this.GenerateLdButton.Click += new System.EventHandler(this.GenerateLdButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 256);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Source Bank:";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(555, 256);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(69, 13);
            this.label55.TabIndex = 31;
            this.label55.Text = "Target Bank:";
            // 
            // TargetBankNumeric
            // 
            this.TargetBankNumeric.AlwaysFireValueChanged = false;
            this.TargetBankNumeric.DisplayDigits = 1;
            this.TargetBankNumeric.DoValueRollover = true;
            this.TargetBankNumeric.Hexadecimal = true;
            this.TargetBankNumeric.IncrementMouseWheel = 3;
            this.TargetBankNumeric.Location = new System.Drawing.Point(633, 254);
            this.TargetBankNumeric.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.TargetBankNumeric.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.TargetBankNumeric.Name = "TargetBankNumeric";
            this.TargetBankNumeric.ShiftMultiplier = 1;
            this.TargetBankNumeric.Size = new System.Drawing.Size(44, 20);
            this.TargetBankNumeric.TabIndex = 32;
            this.TargetBankNumeric.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TargetBankNumeric.ValueChanged += new System.EventHandler(this.TargetBankNumeric_ValueChanged);
            // 
            // SourceBankNumeric
            // 
            this.SourceBankNumeric.AlwaysFireValueChanged = false;
            this.SourceBankNumeric.DisplayDigits = 1;
            this.SourceBankNumeric.DoValueRollover = true;
            this.SourceBankNumeric.Enabled = false;
            this.SourceBankNumeric.Hexadecimal = true;
            this.SourceBankNumeric.IncrementMouseWheel = 3;
            this.SourceBankNumeric.Location = new System.Drawing.Point(94, 254);
            this.SourceBankNumeric.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.SourceBankNumeric.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.SourceBankNumeric.Name = "SourceBankNumeric";
            this.SourceBankNumeric.ShiftMultiplier = 1;
            this.SourceBankNumeric.Size = new System.Drawing.Size(44, 20);
            this.SourceBankNumeric.TabIndex = 30;
            this.SourceBankNumeric.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SourceBankNumeric.ValueChanged += new System.EventHandler(this.SourceBankNumeric_ValueChanged);
            // 
            // SourceTextureHeight
            // 
            this.SourceTextureHeight.AlwaysFireValueChanged = false;
            this.SourceTextureHeight.DisplayDigits = 1;
            this.SourceTextureHeight.DoValueRollover = true;
            this.SourceTextureHeight.Enabled = false;
            this.SourceTextureHeight.IncrementMouseWheel = 3;
            this.SourceTextureHeight.Location = new System.Drawing.Point(156, 566);
            this.SourceTextureHeight.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.SourceTextureHeight.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.SourceTextureHeight.Name = "SourceTextureHeight";
            this.SourceTextureHeight.ShiftMultiplier = 1;
            this.SourceTextureHeight.Size = new System.Drawing.Size(44, 20);
            this.SourceTextureHeight.TabIndex = 27;
            this.SourceTextureHeight.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SourceTextureHeight.ValueChanged += new System.EventHandler(this.SourceTextureHeight_ValueChanged);
            // 
            // SourceTextureWidth
            // 
            this.SourceTextureWidth.AlwaysFireValueChanged = false;
            this.SourceTextureWidth.DisplayDigits = 1;
            this.SourceTextureWidth.DoValueRollover = true;
            this.SourceTextureWidth.Enabled = false;
            this.SourceTextureWidth.IncrementMouseWheel = 3;
            this.SourceTextureWidth.Location = new System.Drawing.Point(106, 566);
            this.SourceTextureWidth.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.SourceTextureWidth.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.SourceTextureWidth.Name = "SourceTextureWidth";
            this.SourceTextureWidth.ShiftMultiplier = 1;
            this.SourceTextureWidth.Size = new System.Drawing.Size(44, 20);
            this.SourceTextureWidth.TabIndex = 26;
            this.SourceTextureWidth.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SourceTextureWidth.ValueChanged += new System.EventHandler(this.SourceTextureWidth_ValueChanged);
            // 
            // AlsoUpdateHfileCheckbox
            // 
            this.AlsoUpdateHfileCheckbox.AutoSize = true;
            this.AlsoUpdateHfileCheckbox.Checked = true;
            this.AlsoUpdateHfileCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AlsoUpdateHfileCheckbox.Location = new System.Drawing.Point(479, 616);
            this.AlsoUpdateHfileCheckbox.Name = "AlsoUpdateHfileCheckbox";
            this.AlsoUpdateHfileCheckbox.Size = new System.Drawing.Size(196, 17);
            this.AlsoUpdateHfileCheckbox.TabIndex = 33;
            this.AlsoUpdateHfileCheckbox.Text = "Also update .h file in the same folder";
            this.AlsoUpdateHfileCheckbox.UseVisualStyleBackColor = true;
            // 
            // DontReloadTargetCheckbox
            // 
            this.DontReloadTargetCheckbox.AutoSize = true;
            this.DontReloadTargetCheckbox.Location = new System.Drawing.Point(390, 225);
            this.DontReloadTargetCheckbox.Name = "DontReloadTargetCheckbox";
            this.DontReloadTargetCheckbox.Size = new System.Drawing.Size(201, 17);
            this.DontReloadTargetCheckbox.TabIndex = 34;
            this.DontReloadTargetCheckbox.Text = "Don\'t reload target list on next search";
            this.DontReloadTargetCheckbox.UseVisualStyleBackColor = true;
            // 
            // AutomaticallyUpdateLDCheckbox
            // 
            this.AutomaticallyUpdateLDCheckbox.AutoSize = true;
            this.AutomaticallyUpdateLDCheckbox.Checked = true;
            this.AutomaticallyUpdateLDCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutomaticallyUpdateLDCheckbox.Location = new System.Drawing.Point(479, 593);
            this.AutomaticallyUpdateLDCheckbox.Name = "AutomaticallyUpdateLDCheckbox";
            this.AutomaticallyUpdateLDCheckbox.Size = new System.Drawing.Size(196, 17);
            this.AutomaticallyUpdateLDCheckbox.TabIndex = 35;
            this.AutomaticallyUpdateLDCheckbox.Text = "Automatically update .ld in /include/";
            this.toolTip1.SetToolTip(this.AutomaticallyUpdateLDCheckbox, "Automatically update .ld in /include/object/ folder if the zobj is inside a z64ro" +
        "m structure");
            this.AutomaticallyUpdateLDCheckbox.UseVisualStyleBackColor = true;
            // 
            // ZobjCopyToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 647);
            this.Controls.Add(this.AutomaticallyUpdateLDCheckbox);
            this.Controls.Add(this.DontReloadTargetCheckbox);
            this.Controls.Add(this.AlsoUpdateHfileCheckbox);
            this.Controls.Add(this.TargetBankNumeric);
            this.Controls.Add(this.label55);
            this.Controls.Add(this.SourceBankNumeric);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.GenerateLdButton);
            this.Controls.Add(this.SourceTextureHeight);
            this.Controls.Add(this.SourceTextureWidth);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SourceTextureFormatComboBox);
            this.Controls.Add(this.ImageFormatLabel);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.TargetOffset);
            this.Controls.Add(this.TargetOffsetFilename);
            this.Controls.Add(this.SourceOffset);
            this.Controls.Add(this.SourceOffsetFilename);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MoveButton);
            this.Controls.Add(this.SaveAsButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TargetListBox);
            this.Controls.Add(this.SourceListBox);
            this.Controls.Add(this.TargetButton);
            this.Controls.Add(this.TargetFilename);
            this.Controls.Add(this.SourceButton);
            this.Controls.Add(this.SourceFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ZobjCopyToolForm";
            this.Text = "Copy data between .zobj\'s";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CopyAnimationsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.TargetBankNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceBankNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceTextureHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceTextureWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SourceFilename;
        private System.Windows.Forms.Button SourceButton;
        private System.Windows.Forms.Button TargetButton;
        private System.Windows.Forms.TextBox TargetFilename;
        private System.Windows.Forms.ListBox SourceListBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SaveAsButton;
        private System.Windows.Forms.Button MoveButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button TargetOffset;
        private System.Windows.Forms.TextBox TargetOffsetFilename;
        private System.Windows.Forms.Button SourceOffset;
        private System.Windows.Forms.TextBox SourceOffsetFilename;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.ComboBox SourceTextureFormatComboBox;
        private System.Windows.Forms.Label ImageFormatLabel;
        private System.Windows.Forms.Label label7;
        private NumericUpDownEx SourceTextureWidth;
        private NumericUpDownEx SourceTextureHeight;
        private System.Windows.Forms.Button GenerateLdButton;
        private NumericUpDownEx SourceBankNumeric;
        private System.Windows.Forms.Label label8;
        private NumericUpDownEx TargetBankNumeric;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListBox TargetListBox;
        private System.Windows.Forms.CheckBox AlsoUpdateHfileCheckbox;
        private System.Windows.Forms.CheckBox DontReloadTargetCheckbox;
        private System.Windows.Forms.CheckBox AutomaticallyUpdateLDCheckbox;
    }
}