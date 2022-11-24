namespace SharpOcarina
{
    partial class TitleScreenEditor
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.LoadFromFile = new System.Windows.Forms.Button();
            this.ExtractButton = new System.Windows.Forms.Button();
            this.TextureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.VScale = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.XScale = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.XPos = new System.Windows.Forms.NumericUpDown();
            this.YPos = new System.Windows.Forms.NumericUpDown();
            this.Save = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.TextureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CancelButton.Location = new System.Drawing.Point(483, 691);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Close";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // LoadFromFile
            // 
            this.LoadFromFile.Location = new System.Drawing.Point(321, 508);
            this.LoadFromFile.Name = "LoadFromFile";
            this.LoadFromFile.Size = new System.Drawing.Size(88, 23);
            this.LoadFromFile.TabIndex = 5;
            this.LoadFromFile.Text = "Load from file";
            this.LoadFromFile.UseVisualStyleBackColor = true;
            this.LoadFromFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExtractButton
            // 
            this.ExtractButton.Location = new System.Drawing.Point(483, 508);
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
            this.TextureBox.Location = new System.Drawing.Point(138, 12);
            this.TextureBox.Name = "TextureBox";
            this.TextureBox.Size = new System.Drawing.Size(640, 480);
            this.TextureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TextureBox.TabIndex = 4;
            this.TextureBox.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // VScale
            // 
            this.VScale.DecimalPlaces = 2;
            this.VScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.VScale.Location = new System.Drawing.Point(562, 557);
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
            this.label1.Location = new System.Drawing.Point(454, 559);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Scale: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // XScale
            // 
            this.XScale.DecimalPlaces = 2;
            this.XScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.XScale.Location = new System.Drawing.Point(500, 557);
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
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 559);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Position:";
            // 
            // XPos
            // 
            this.XPos.Location = new System.Drawing.Point(335, 557);
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
            // 
            // YPos
            // 
            this.YPos.Location = new System.Drawing.Point(387, 557);
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
            // 
            // Save
            // 
            this.Save.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Save.Location = new System.Drawing.Point(372, 691);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 18;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(22, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Logo";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(22, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "Flame Mask";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(22, 70);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(95, 23);
            this.button5.TabIndex = 21;
            this.button5.Text = "Flame Texture";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(22, 99);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(95, 23);
            this.button6.TabIndex = 22;
            this.button6.Text = "Copyright";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(22, 128);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(95, 23);
            this.button7.TabIndex = 23;
            this.button7.Text = "The Legend Of";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(22, 157);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(95, 23);
            this.button8.TabIndex = 24;
            this.button8.Text = "Ocarina of Time";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(22, 186);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(95, 23);
            this.button9.TabIndex = 25;
            this.button9.Text = "Master Quest";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.Color.PaleGreen;
            this.numericUpDown1.Location = new System.Drawing.Point(458, 599);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown1.TabIndex = 28;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.BackColor = System.Drawing.Color.LightCoral;
            this.numericUpDown2.Location = new System.Drawing.Point(406, 599);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown2.TabIndex = 27;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(358, 601);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Color:";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.BackColor = System.Drawing.Color.PaleTurquoise;
            this.numericUpDown3.Location = new System.Drawing.Point(510, 599);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown3.TabIndex = 29;
            this.numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TitleScreenEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 726);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.YPos);
            this.Controls.Add(this.XPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.XScale);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VScale);
            this.Controls.Add(this.ExtractButton);
            this.Controls.Add(this.LoadFromFile);
            this.Controls.Add(this.TextureBox);
            this.Controls.Add(this.CancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TitleScreenEditor";
            this.Text = "Title Screen Editor";
            ((System.ComponentModel.ISupportInitialize)(this.TextureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.PictureBox TextureBox;
        private System.Windows.Forms.Button LoadFromFile;
        private System.Windows.Forms.Button ExtractButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.NumericUpDown VScale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown XScale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown XPos;
        private System.Windows.Forms.NumericUpDown YPos;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
    }
}