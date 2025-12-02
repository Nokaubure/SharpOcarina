namespace SharpOcarina
{
    partial class PickSceneSetting
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
            this.SceneSettingComboBox = new System.Windows.Forms.ComboBox();
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
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scene Setting: ";
            // 
            // SceneSettingComboBox
            // 
            this.SceneSettingComboBox.FormattingEnabled = true;
            this.SceneSettingComboBox.Location = new System.Drawing.Point(110, 64);
            this.SceneSettingComboBox.Name = "SceneSettingComboBox";
            this.SceneSettingComboBox.Size = new System.Drawing.Size(141, 21);
            this.SceneSettingComboBox.TabIndex = 2;
            this.SceneSettingComboBox.SelectionChangeCommitted += new System.EventHandler(this.SceneSettingComboBox_SelectionChangeCommitted);
            // 
            // PickSceneSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 207);
            this.Controls.Add(this.SceneSettingComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(300, 246);
            this.MinimumSize = new System.Drawing.Size(300, 246);
            this.Name = "PickSceneSetting";
            this.Text = "Pick Scene Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SceneSettingComboBox;
    }
}