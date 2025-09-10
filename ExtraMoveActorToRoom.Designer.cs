namespace SharpOcarina
{
    partial class MoveActorToRoom
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
            this.RoomID = new SharpOcarina.NumericUpDownEx();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RoomID)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(69, 67);
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
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Room:";
            // 
            // RoomID
            // 
            this.RoomID.AlwaysFireValueChanged = false;
            this.RoomID.DisplayDigits = 1;
            this.RoomID.DoValueRollover = true;
            this.RoomID.IncrementMouseWheel = 1;
            this.RoomID.Location = new System.Drawing.Point(69, 27);
            this.RoomID.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RoomID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.RoomID.Name = "RoomID";
            this.RoomID.ShiftMultiplier = 1;
            this.RoomID.Size = new System.Drawing.Size(73, 20);
            this.RoomID.TabIndex = 2;
            this.RoomID.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(150, 67);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // MoveActorToRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 104);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.RoomID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MoveActorToRoom";
            this.Text = "Move Actor to Room";
            ((System.ComponentModel.ISupportInitialize)(this.RoomID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label label1;
        private NumericUpDownEx RoomID;
        private System.Windows.Forms.Button CancelButton;
    }
}