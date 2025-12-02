namespace SharpOcarina
{
    partial class PushCutsceneCommands
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
            this.StartFrameVal = new SharpOcarina.NumericUpDownEx();
            this.AmountVal = new SharpOcarina.NumericUpDownEx();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.StartFrameVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountVal)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(74, 100);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "Go";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From frame:";
            // 
            // StartFrameVal
            // 
            this.StartFrameVal.AlwaysFireValueChanged = false;
            this.StartFrameVal.DisplayDigits = 1;
            this.StartFrameVal.DoValueRollover = true;
            this.StartFrameVal.IncrementMouseWheel = 3;
            this.StartFrameVal.Location = new System.Drawing.Point(117, 27);
            this.StartFrameVal.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.StartFrameVal.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.StartFrameVal.Name = "StartFrameVal";
            this.StartFrameVal.ShiftMultiplier = 1;
            this.StartFrameVal.Size = new System.Drawing.Size(73, 20);
            this.StartFrameVal.TabIndex = 2;
            this.StartFrameVal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // AmountVal
            // 
            this.AmountVal.AlwaysFireValueChanged = false;
            this.AmountVal.DisplayDigits = 1;
            this.AmountVal.DoValueRollover = true;
            this.AmountVal.IncrementMouseWheel = 3;
            this.AmountVal.Location = new System.Drawing.Point(117, 53);
            this.AmountVal.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.AmountVal.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.AmountVal.Name = "AmountVal";
            this.AmountVal.ShiftMultiplier = 1;
            this.AmountVal.Size = new System.Drawing.Size(73, 20);
            this.AmountVal.TabIndex = 4;
            this.AmountVal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Amount of frames:";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(155, 100);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // PushCutsceneCommands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 135);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AmountVal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StartFrameVal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(318, 174);
            this.MinimumSize = new System.Drawing.Size(318, 174);
            this.Name = "PushCutsceneCommands";
            this.Text = "Move Cutscene Commands";
            ((System.ComponentModel.ISupportInitialize)(this.StartFrameVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountVal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label label1;
        private NumericUpDownEx StartFrameVal;
        private NumericUpDownEx AmountVal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CancelButton;
    }
}