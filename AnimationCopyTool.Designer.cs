namespace SharpOcarina
{
    partial class CopyAnimationsForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source Zobj";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Target Zobj";
            // 
            // SourceFilename
            // 
            this.SourceFilename.Location = new System.Drawing.Point(68, 29);
            this.SourceFilename.Name = "SourceFilename";
            this.SourceFilename.ReadOnly = true;
            this.SourceFilename.Size = new System.Drawing.Size(369, 20);
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
            this.TargetButton.Location = new System.Drawing.Point(16, 73);
            this.TargetButton.Name = "TargetButton";
            this.TargetButton.Size = new System.Drawing.Size(46, 20);
            this.TargetButton.TabIndex = 5;
            this.TargetButton.Text = "Open";
            this.TargetButton.UseVisualStyleBackColor = true;
            this.TargetButton.Click += new System.EventHandler(this.TargetButton_Click);
            // 
            // TargetFilename
            // 
            this.TargetFilename.Location = new System.Drawing.Point(68, 73);
            this.TargetFilename.Name = "TargetFilename";
            this.TargetFilename.ReadOnly = true;
            this.TargetFilename.Size = new System.Drawing.Size(369, 20);
            this.TargetFilename.TabIndex = 4;
            // 
            // SourceListBox
            // 
            this.SourceListBox.FormattingEnabled = true;
            this.SourceListBox.Location = new System.Drawing.Point(12, 149);
            this.SourceListBox.Name = "SourceListBox";
            this.SourceListBox.Size = new System.Drawing.Size(173, 303);
            this.SourceListBox.TabIndex = 6;
            // 
            // TargetListBox
            // 
            this.TargetListBox.FormattingEnabled = true;
            this.TargetListBox.Location = new System.Drawing.Point(262, 149);
            this.TargetListBox.Name = "TargetListBox";
            this.TargetListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.TargetListBox.Size = new System.Drawing.Size(175, 303);
            this.TargetListBox.TabIndex = 7;
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(137, 467);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.Enabled = false;
            this.SaveAsButton.Location = new System.Drawing.Point(218, 467);
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveAsButton.TabIndex = 9;
            this.SaveAsButton.Text = "Save As";
            this.SaveAsButton.UseVisualStyleBackColor = true;
            this.SaveAsButton.Click += new System.EventHandler(this.SaveAsButton_Click);
            // 
            // MoveButton
            // 
            this.MoveButton.Enabled = false;
            this.MoveButton.Location = new System.Drawing.Point(196, 286);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(57, 23);
            this.MoveButton.TabIndex = 10;
            this.MoveButton.Text = "Copy >>";
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Source";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Target";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // CopyAnimationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 511);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
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
            this.Name = "CopyAnimationsForm";
            this.Text = "Copy Animations";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CopyAnimationsForm_FormClosed);
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
        private System.Windows.Forms.ListBox TargetListBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SaveAsButton;
        private System.Windows.Forms.Button MoveButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}