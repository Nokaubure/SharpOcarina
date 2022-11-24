namespace SharpOcarina
{
    partial class AddObjectAllRooms
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
            this.ObjectID = new SharpOcarina.NumericUpDownEx();
            this.PositionID = new SharpOcarina.NumericUpDownEx();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionID)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(69, 155);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "Add";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Object:";
            // 
            // ObjectID
            // 
            this.ObjectID.AlwaysFireValueChanged = false;
            this.ObjectID.DisplayDigits = 4;
            this.ObjectID.DoValueRollover = true;
            this.ObjectID.Hexadecimal = true;
            this.ObjectID.IncrementMouseWheel = 3;
            this.ObjectID.Location = new System.Drawing.Point(69, 27);
            this.ObjectID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjectID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.ObjectID.Name = "ObjectID";
            this.ObjectID.Size = new System.Drawing.Size(73, 20);
            this.ObjectID.TabIndex = 2;
            this.ObjectID.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // PositionID
            // 
            this.PositionID.AlwaysFireValueChanged = false;
            this.PositionID.DisplayDigits = 1;
            this.PositionID.DoValueRollover = true;
            this.PositionID.IncrementMouseWheel = 3;
            this.PositionID.Location = new System.Drawing.Point(69, 64);
            this.PositionID.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.PositionID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.PositionID.Name = "PositionID";
            this.PositionID.Size = new System.Drawing.Size(73, 20);
            this.PositionID.TabIndex = 4;
            this.PositionID.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Position:";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(150, 155);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AddObjectAllRooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 185);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.PositionID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ObjectID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddObjectAllRooms";
            this.Text = "Add Object to All Rooms";
            ((System.ComponentModel.ISupportInitialize)(this.ObjectID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label label1;
        private NumericUpDownEx ObjectID;
        private NumericUpDownEx PositionID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CancelButton;
    }
}