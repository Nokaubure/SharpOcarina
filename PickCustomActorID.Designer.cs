namespace SharpOcarina
{
    partial class PickCustomActorID
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
            this.ActorIDNumeric = new SharpOcarina.NumericUpDownEx();
            this.ObjectIDNumeric = new SharpOcarina.NumericUpDownEx();
            this.ObjectLabel = new System.Windows.Forms.Label();
            this.FindEmptyIDs = new System.Windows.Forms.CheckBox();
            this.ActorInUseLabel = new System.Windows.Forms.Label();
            this.ObjectInUseLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ActorIDNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectIDNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(98, 134);
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
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Actor ID:";
            // 
            // ActorIDNumeric
            // 
            this.ActorIDNumeric.AlwaysFireValueChanged = false;
            this.ActorIDNumeric.DisplayDigits = 4;
            this.ActorIDNumeric.DoValueRollover = true;
            this.ActorIDNumeric.Hexadecimal = true;
            this.ActorIDNumeric.IncrementMouseWheel = 3;
            this.ActorIDNumeric.Location = new System.Drawing.Point(65, 12);
            this.ActorIDNumeric.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ActorIDNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ActorIDNumeric.Name = "ActorIDNumeric";
            this.ActorIDNumeric.ShiftMultiplier = 1;
            this.ActorIDNumeric.Size = new System.Drawing.Size(59, 20);
            this.ActorIDNumeric.TabIndex = 2;
            this.ActorIDNumeric.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorIDNumeric.ValueChanged += new System.EventHandler(this.ActorIDNumeric_ValueChanged);
            // 
            // ObjectIDNumeric
            // 
            this.ObjectIDNumeric.AlwaysFireValueChanged = false;
            this.ObjectIDNumeric.DisplayDigits = 4;
            this.ObjectIDNumeric.DoValueRollover = true;
            this.ObjectIDNumeric.Hexadecimal = true;
            this.ObjectIDNumeric.IncrementMouseWheel = 3;
            this.ObjectIDNumeric.Location = new System.Drawing.Point(65, 47);
            this.ObjectIDNumeric.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectIDNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ObjectIDNumeric.Name = "ObjectIDNumeric";
            this.ObjectIDNumeric.ShiftMultiplier = 1;
            this.ObjectIDNumeric.Size = new System.Drawing.Size(59, 20);
            this.ObjectIDNumeric.TabIndex = 4;
            this.ObjectIDNumeric.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ObjectIDNumeric.ValueChanged += new System.EventHandler(this.ObjectIDNumeric_ValueChanged);
            // 
            // ObjectLabel
            // 
            this.ObjectLabel.AutoSize = true;
            this.ObjectLabel.Location = new System.Drawing.Point(10, 49);
            this.ObjectLabel.Name = "ObjectLabel";
            this.ObjectLabel.Size = new System.Drawing.Size(55, 13);
            this.ObjectLabel.TabIndex = 3;
            this.ObjectLabel.Text = "Object ID:";
            // 
            // FindEmptyIDs
            // 
            this.FindEmptyIDs.AutoSize = true;
            this.FindEmptyIDs.Location = new System.Drawing.Point(13, 83);
            this.FindEmptyIDs.Name = "FindEmptyIDs";
            this.FindEmptyIDs.Size = new System.Drawing.Size(160, 17);
            this.FindEmptyIDs.TabIndex = 5;
            this.FindEmptyIDs.Text = "Find empty IDs automatically";
            this.FindEmptyIDs.UseVisualStyleBackColor = true;
            this.FindEmptyIDs.CheckedChanged += new System.EventHandler(this.FindEmptyIDs_CheckedChanged);
            // 
            // ActorInUseLabel
            // 
            this.ActorInUseLabel.AutoSize = true;
            this.ActorInUseLabel.ForeColor = System.Drawing.Color.Red;
            this.ActorInUseLabel.Location = new System.Drawing.Point(130, 14);
            this.ActorInUseLabel.Name = "ActorInUseLabel";
            this.ActorInUseLabel.Size = new System.Drawing.Size(76, 13);
            this.ActorInUseLabel.TabIndex = 6;
            this.ActorInUseLabel.Text = "Already in use!";
            this.ActorInUseLabel.Visible = false;
            // 
            // ObjectInUseLabel
            // 
            this.ObjectInUseLabel.AutoSize = true;
            this.ObjectInUseLabel.ForeColor = System.Drawing.Color.Red;
            this.ObjectInUseLabel.Location = new System.Drawing.Point(130, 49);
            this.ObjectInUseLabel.Name = "ObjectInUseLabel";
            this.ObjectInUseLabel.Size = new System.Drawing.Size(76, 13);
            this.ObjectInUseLabel.TabIndex = 7;
            this.ObjectInUseLabel.Text = "Already in use!";
            this.ObjectInUseLabel.Visible = false;
            // 
            // PickCustomActorID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 169);
            this.Controls.Add(this.ObjectInUseLabel);
            this.Controls.Add(this.ActorInUseLabel);
            this.Controls.Add(this.FindEmptyIDs);
            this.Controls.Add(this.ObjectIDNumeric);
            this.Controls.Add(this.ObjectLabel);
            this.Controls.Add(this.ActorIDNumeric);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok);
            this.Name = "PickCustomActorID";
            this.Text = "Pick Actor ID";
            ((System.ComponentModel.ISupportInitialize)(this.ActorIDNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectIDNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label label1;
        private NumericUpDownEx ActorIDNumeric;
        private NumericUpDownEx ObjectIDNumeric;
        private System.Windows.Forms.Label ObjectLabel;
        private System.Windows.Forms.CheckBox FindEmptyIDs;
        private System.Windows.Forms.Label ActorInUseLabel;
        private System.Windows.Forms.Label ObjectInUseLabel;
    }
}