namespace SharpOcarina
{
    partial class FileToC
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.SourceButton = new System.Windows.Forms.Button();
            this.SourceFilename = new System.Windows.Forms.TextBox();
            this.ImageFormatComboBox = new System.Windows.Forms.ComboBox();
            this.ImageFormatLabel = new System.Windows.Forms.Label();
            this.TextureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.ConvertToInclude = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SplitCheckBox = new System.Windows.Forms.CheckBox();
            this.SplitWidth = new System.Windows.Forms.NumericUpDown();
            this.SplitHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SplitGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.TextureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitHeight)).BeginInit();
            this.SplitGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(118, 294);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 13;
            this.SaveButton.Text = "Convert";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SourceButton
            // 
            this.SourceButton.Location = new System.Drawing.Point(12, 21);
            this.SourceButton.Name = "SourceButton";
            this.SourceButton.Size = new System.Drawing.Size(80, 20);
            this.SourceButton.TabIndex = 12;
            this.SourceButton.Text = "Select File";
            this.toolTip1.SetToolTip(this.SourceButton, "Or drop a file inside this window");
            this.SourceButton.UseVisualStyleBackColor = true;
            this.SourceButton.Click += new System.EventHandler(this.SourceButton_Click);
            // 
            // SourceFilename
            // 
            this.SourceFilename.Location = new System.Drawing.Point(98, 21);
            this.SourceFilename.Name = "SourceFilename";
            this.SourceFilename.ReadOnly = true;
            this.SourceFilename.Size = new System.Drawing.Size(358, 20);
            this.SourceFilename.TabIndex = 11;
            // 
            // ImageFormatComboBox
            // 
            this.ImageFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ImageFormatComboBox.Enabled = false;
            this.ImageFormatComboBox.FormattingEnabled = true;
            this.ImageFormatComboBox.Location = new System.Drawing.Point(98, 61);
            this.ImageFormatComboBox.Name = "ImageFormatComboBox";
            this.ImageFormatComboBox.Size = new System.Drawing.Size(141, 21);
            this.ImageFormatComboBox.TabIndex = 15;
            this.ImageFormatComboBox.SelectionChangeCommitted += new System.EventHandler(this.ImageFormatComboBox_SelectionChangeCommitted);
            // 
            // ImageFormatLabel
            // 
            this.ImageFormatLabel.AutoSize = true;
            this.ImageFormatLabel.Enabled = false;
            this.ImageFormatLabel.Location = new System.Drawing.Point(12, 64);
            this.ImageFormatLabel.Name = "ImageFormatLabel";
            this.ImageFormatLabel.Size = new System.Drawing.Size(71, 13);
            this.ImageFormatLabel.TabIndex = 14;
            this.ImageFormatLabel.Text = "Image format:";
            // 
            // TextureBox
            // 
            this.TextureBox.BackColor = System.Drawing.Color.Aqua;
            this.TextureBox.Location = new System.Drawing.Point(329, 61);
            this.TextureBox.Name = "TextureBox";
            this.TextureBox.Size = new System.Drawing.Size(256, 256);
            this.TextureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.TextureBox.TabIndex = 18;
            this.TextureBox.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(462, 28);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(67, 13);
            this.SizeLabel.TabIndex = 19;
            this.SizeLabel.Text = "Size: 0 bytes";
            this.SizeLabel.Visible = false;
            this.SizeLabel.Click += new System.EventHandler(this.SizeLabel_Click);
            // 
            // ConvertToInclude
            // 
            this.ConvertToInclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConvertToInclude.AutoSize = true;
            this.ConvertToInclude.Location = new System.Drawing.Point(12, 271);
            this.ConvertToInclude.Name = "ConvertToInclude";
            this.ConvertToInclude.Size = new System.Drawing.Size(187, 17);
            this.ConvertToInclude.TabIndex = 20;
            this.ConvertToInclude.Text = "Save as .inc file in the same folder";
            this.ConvertToInclude.UseVisualStyleBackColor = true;
            // 
            // SplitCheckBox
            // 
            this.SplitCheckBox.AutoSize = true;
            this.SplitCheckBox.Enabled = false;
            this.SplitCheckBox.Location = new System.Drawing.Point(15, 92);
            this.SplitCheckBox.Name = "SplitCheckBox";
            this.SplitCheckBox.Size = new System.Drawing.Size(163, 17);
            this.SplitCheckBox.TabIndex = 21;
            this.SplitCheckBox.Text = "Split into several arrays / files";
            this.SplitCheckBox.UseVisualStyleBackColor = true;
            this.SplitCheckBox.CheckedChanged += new System.EventHandler(this.SplitCheckBox_CheckedChanged);
            // 
            // SplitWidth
            // 
            this.SplitWidth.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.SplitWidth.Location = new System.Drawing.Point(72, 10);
            this.SplitWidth.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.SplitWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.SplitWidth.Name = "SplitWidth";
            this.SplitWidth.Size = new System.Drawing.Size(75, 20);
            this.SplitWidth.TabIndex = 22;
            this.SplitWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.SplitWidth.ValueChanged += new System.EventHandler(this.SplitWidth_ValueChanged);
            // 
            // SplitHeight
            // 
            this.SplitHeight.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.SplitHeight.Location = new System.Drawing.Point(72, 36);
            this.SplitHeight.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.SplitHeight.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.SplitHeight.Name = "SplitHeight";
            this.SplitHeight.Size = new System.Drawing.Size(75, 20);
            this.SplitHeight.TabIndex = 23;
            this.SplitHeight.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.SplitHeight.ValueChanged += new System.EventHandler(this.SplitHeight_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Split Width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Split Height:";
            // 
            // SplitGroupBox
            // 
            this.SplitGroupBox.Controls.Add(this.SplitWidth);
            this.SplitGroupBox.Controls.Add(this.label2);
            this.SplitGroupBox.Controls.Add(this.SplitHeight);
            this.SplitGroupBox.Controls.Add(this.label1);
            this.SplitGroupBox.Enabled = false;
            this.SplitGroupBox.Location = new System.Drawing.Point(12, 115);
            this.SplitGroupBox.Name = "SplitGroupBox";
            this.SplitGroupBox.Size = new System.Drawing.Size(157, 62);
            this.SplitGroupBox.TabIndex = 26;
            this.SplitGroupBox.TabStop = false;
            // 
            // FileToC
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 333);
            this.Controls.Add(this.SplitGroupBox);
            this.Controls.Add(this.SplitCheckBox);
            this.Controls.Add(this.ConvertToInclude);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.TextureBox);
            this.Controls.Add(this.ImageFormatComboBox);
            this.Controls.Add(this.ImageFormatLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.SourceButton);
            this.Controls.Add(this.SourceFilename);
            this.Name = "FileToC";
            this.Text = "Convert File to C array";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileToC_FormClosed);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileToC_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileToC_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.TextureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitHeight)).EndInit();
            this.SplitGroupBox.ResumeLayout(false);
            this.SplitGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SourceButton;
        private System.Windows.Forms.TextBox SourceFilename;
        private System.Windows.Forms.ComboBox ImageFormatComboBox;
        private System.Windows.Forms.Label ImageFormatLabel;
        private System.Windows.Forms.PictureBox TextureBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.CheckBox ConvertToInclude;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox SplitCheckBox;
        private System.Windows.Forms.NumericUpDown SplitWidth;
        private System.Windows.Forms.NumericUpDown SplitHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox SplitGroupBox;
    }
}