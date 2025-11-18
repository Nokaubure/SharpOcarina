namespace SharpOcarina
{
    partial class PickBlenderVersion
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
            this.BlenderVersionComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(99, 158);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "OK";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Blender Version:";
            // 
            // BlenderVersionComboBox
            // 
            this.BlenderVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlenderVersionComboBox.FormattingEnabled = true;
            this.BlenderVersionComboBox.Location = new System.Drawing.Point(110, 64);
            this.BlenderVersionComboBox.Name = "BlenderVersionComboBox";
            this.BlenderVersionComboBox.Size = new System.Drawing.Size(141, 21);
            this.BlenderVersionComboBox.TabIndex = 2;
            this.BlenderVersionComboBox.SelectionChangeCommitted += new System.EventHandler(this.SceneSettingComboBox_SelectionChangeCommitted);
            // 
            // PickBlenderVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 207);
            this.Controls.Add(this.BlenderVersionComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok);
            this.Name = "PickBlenderVersion";
            this.Text = "Pick Installed Blender Version";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PickBlenderVersion_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox BlenderVersionComboBox;
    }
}