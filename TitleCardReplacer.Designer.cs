namespace SharpOcarina
{
    partial class TitleCardReplacer
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.LoadFromFile = new System.Windows.Forms.Button();
            this.ExtractButton = new System.Windows.Forms.Button();
            this.TextureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.GenerateTextbox = new System.Windows.Forms.TextBox();
            this.FontSize = new System.Windows.Forms.NumericUpDown();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.InjectOffsetTextbox = new SharpOcarina.NumericTextBox();
            this.VScale = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.XScale = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.XPos = new System.Windows.Forms.NumericUpDown();
            this.YPos = new System.Windows.Forms.NumericUpDown();
            this.GenerateGroupBox = new System.Windows.Forms.GroupBox();
            this.NewSaveButton = new System.Windows.Forms.Button();
            this.EnableGenerator = new System.Windows.Forms.CheckBox();
            this.RemoveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TextureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPos)).BeginInit();
            this.GenerateGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(11, 249);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(86, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Inject to offset:";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Visible = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(222, 275);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(95, 13);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "Current Title Card: ";
            // 
            // LoadFromFile
            // 
            this.LoadFromFile.Location = new System.Drawing.Point(49, 89);
            this.LoadFromFile.Name = "LoadFromFile";
            this.LoadFromFile.Size = new System.Drawing.Size(88, 23);
            this.LoadFromFile.TabIndex = 5;
            this.LoadFromFile.Text = "Load from file";
            this.LoadFromFile.UseVisualStyleBackColor = true;
            this.LoadFromFile.Click += new System.EventHandler(this.LoadFromFile_Click);
            // 
            // ExtractButton
            // 
            this.ExtractButton.Location = new System.Drawing.Point(203, 89);
            this.ExtractButton.Name = "ExtractButton";
            this.ExtractButton.Size = new System.Drawing.Size(93, 23);
            this.ExtractButton.TabIndex = 6;
            this.ExtractButton.Text = "Extract Texture";
            this.ExtractButton.UseVisualStyleBackColor = true;
            this.ExtractButton.Click += new System.EventHandler(this.ExtractButton_Click);
            // 
            // TextureBox
            // 
            this.TextureBox.BackColor = System.Drawing.Color.Aqua;
            this.TextureBox.Location = new System.Drawing.Point(34, 35);
            this.TextureBox.Name = "TextureBox";
            this.TextureBox.Size = new System.Drawing.Size(288, 48);
            this.TextureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TextureBox.TabIndex = 4;
            this.TextureBox.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // GenerateTextbox
            // 
            this.GenerateTextbox.AcceptsReturn = true;
            this.GenerateTextbox.AcceptsTab = true;
            this.GenerateTextbox.Enabled = false;
            this.GenerateTextbox.Location = new System.Drawing.Point(6, 44);
            this.GenerateTextbox.Name = "GenerateTextbox";
            this.GenerateTextbox.Size = new System.Drawing.Size(273, 20);
            this.GenerateTextbox.TabIndex = 7;
            this.GenerateTextbox.TextChanged += new System.EventHandler(this.GenerateTextbox_TextChanged);
            // 
            // FontSize
            // 
            this.FontSize.Enabled = false;
            this.FontSize.Location = new System.Drawing.Point(288, 45);
            this.FontSize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.FontSize.Name = "FontSize";
            this.FontSize.Size = new System.Drawing.Size(41, 20);
            this.FontSize.TabIndex = 9;
            this.FontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.FontSize.ValueChanged += new System.EventHandler(this.FontSize_ValueChanged);
            // 
            // InjectOffsetTextbox
            // 
            this.InjectOffsetTextbox.AllowHex = true;
            this.InjectOffsetTextbox.Digits = 8;
            this.InjectOffsetTextbox.Location = new System.Drawing.Point(103, 249);
            this.InjectOffsetTextbox.Name = "InjectOffsetTextbox";
            this.InjectOffsetTextbox.Size = new System.Drawing.Size(98, 20);
            this.InjectOffsetTextbox.TabIndex = 11;
            this.InjectOffsetTextbox.Visible = false;
            this.InjectOffsetTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InjectOffsetTextbox_KeyDown);
            this.InjectOffsetTextbox.Leave += new System.EventHandler(this.InjectOffsetTextbox_Leave);
            // 
            // VScale
            // 
            this.VScale.DecimalPlaces = 2;
            this.VScale.Enabled = false;
            this.VScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.VScale.Location = new System.Drawing.Point(273, 83);
            this.VScale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.VScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.VScale.Name = "VScale";
            this.VScale.Size = new System.Drawing.Size(56, 20);
            this.VScale.TabIndex = 12;
            this.VScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.VScale.ValueChanged += new System.EventHandler(this.VScale_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(165, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Scale: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // XScale
            // 
            this.XScale.DecimalPlaces = 2;
            this.XScale.Enabled = false;
            this.XScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.XScale.Location = new System.Drawing.Point(211, 83);
            this.XScale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.XScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.XScale.Name = "XScale";
            this.XScale.Size = new System.Drawing.Size(56, 20);
            this.XScale.TabIndex = 14;
            this.XScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.XScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.XScale.ValueChanged += new System.EventHandler(this.XScale_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(1, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Position:";
            // 
            // XPos
            // 
            this.XPos.Enabled = false;
            this.XPos.Location = new System.Drawing.Point(50, 83);
            this.XPos.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.XPos.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.XPos.Name = "XPos";
            this.XPos.Size = new System.Drawing.Size(46, 20);
            this.XPos.TabIndex = 16;
            this.XPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.XPos.ValueChanged += new System.EventHandler(this.XPos_ValueChanged);
            // 
            // YPos
            // 
            this.YPos.Enabled = false;
            this.YPos.Location = new System.Drawing.Point(102, 83);
            this.YPos.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.YPos.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.YPos.Name = "YPos";
            this.YPos.Size = new System.Drawing.Size(46, 20);
            this.YPos.TabIndex = 17;
            this.YPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.YPos.ValueChanged += new System.EventHandler(this.YPos_ValueChanged);
            // 
            // GenerateGroupBox
            // 
            this.GenerateGroupBox.Controls.Add(this.EnableGenerator);
            this.GenerateGroupBox.Controls.Add(this.GenerateTextbox);
            this.GenerateGroupBox.Controls.Add(this.YPos);
            this.GenerateGroupBox.Controls.Add(this.FontSize);
            this.GenerateGroupBox.Controls.Add(this.XPos);
            this.GenerateGroupBox.Controls.Add(this.VScale);
            this.GenerateGroupBox.Controls.Add(this.label2);
            this.GenerateGroupBox.Controls.Add(this.label1);
            this.GenerateGroupBox.Controls.Add(this.XScale);
            this.GenerateGroupBox.Location = new System.Drawing.Point(11, 118);
            this.GenerateGroupBox.Name = "GenerateGroupBox";
            this.GenerateGroupBox.Size = new System.Drawing.Size(334, 125);
            this.GenerateGroupBox.TabIndex = 18;
            this.GenerateGroupBox.TabStop = false;
            // 
            // NewSaveButton
            // 
            this.NewSaveButton.Location = new System.Drawing.Point(61, 275);
            this.NewSaveButton.Name = "NewSaveButton";
            this.NewSaveButton.Size = new System.Drawing.Size(75, 23);
            this.NewSaveButton.TabIndex = 19;
            this.NewSaveButton.Text = "Save";
            this.NewSaveButton.UseVisualStyleBackColor = true;
            this.NewSaveButton.Click += new System.EventHandler(this.NewSaveButton_Click);
            // 
            // EnableGenerator
            // 
            this.EnableGenerator.AutoSize = true;
            this.EnableGenerator.Location = new System.Drawing.Point(7, 20);
            this.EnableGenerator.Name = "EnableGenerator";
            this.EnableGenerator.Size = new System.Drawing.Size(157, 17);
            this.EnableGenerator.TabIndex = 18;
            this.EnableGenerator.Text = "Enable Title Card Generator";
            this.EnableGenerator.UseVisualStyleBackColor = true;
            this.EnableGenerator.CheckedChanged += new System.EventHandler(this.EnableGenerator_CheckedChanged);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(142, 275);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 20;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // TitleCardReplacer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 308);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.NewSaveButton);
            this.Controls.Add(this.GenerateGroupBox);
            this.Controls.Add(this.InjectOffsetTextbox);
            this.Controls.Add(this.ExtractButton);
            this.Controls.Add(this.LoadFromFile);
            this.Controls.Add(this.TextureBox);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TitleCardReplacer";
            this.Text = "Title Card Replacer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TitleCardReplacer_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.TextureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPos)).EndInit();
            this.GenerateGroupBox.ResumeLayout(false);
            this.GenerateGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.PictureBox TextureBox;
        private System.Windows.Forms.Button LoadFromFile;
        private System.Windows.Forms.Button ExtractButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox GenerateTextbox;
        private System.Windows.Forms.NumericUpDown FontSize;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private NumericTextBox InjectOffsetTextbox;
        private System.Windows.Forms.NumericUpDown VScale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown XScale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown XPos;
        private System.Windows.Forms.NumericUpDown YPos;
        private System.Windows.Forms.GroupBox GenerateGroupBox;
        private System.Windows.Forms.Button NewSaveButton;
        private System.Windows.Forms.CheckBox EnableGenerator;
        private System.Windows.Forms.Button RemoveButton;
    }
}