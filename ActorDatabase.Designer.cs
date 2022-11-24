namespace SharpOcarina
{
    partial class ActorDatabase
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
            this.SetActorButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.Filter = new System.Windows.Forms.Label();
            this.ActorView = new System.Windows.Forms.TreeView();
            this.GoButton = new System.Windows.Forms.Button();
            this.NotesTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // SetActorButton
            // 
            this.SetActorButton.Enabled = false;
            this.SetActorButton.Location = new System.Drawing.Point(49, 617);
            this.SetActorButton.Name = "SetActorButton";
            this.SetActorButton.Size = new System.Drawing.Size(75, 23);
            this.SetActorButton.TabIndex = 0;
            this.SetActorButton.Text = "Set Actor";
            this.SetActorButton.UseVisualStyleBackColor = true;
            this.SetActorButton.Click += new System.EventHandler(this.SetActorButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(220, 617);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Location = new System.Drawing.Point(49, 12);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(196, 20);
            this.FilterTextBox.TabIndex = 2;
            this.FilterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilterTextBox_KeyDown);
            // 
            // Filter
            // 
            this.Filter.AutoSize = true;
            this.Filter.Location = new System.Drawing.Point(12, 15);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(32, 13);
            this.Filter.TabIndex = 3;
            this.Filter.Text = "Filter:";
            // 
            // ActorView
            // 
            this.ActorView.HideSelection = false;
            this.ActorView.Location = new System.Drawing.Point(12, 49);
            this.ActorView.Name = "ActorView";
            this.ActorView.Size = new System.Drawing.Size(333, 362);
            this.ActorView.TabIndex = 4;
            this.ActorView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ActorView_AfterSelect);
            this.ActorView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ActorView_NodeMouseClick);
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(254, 10);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(41, 23);
            this.GoButton.TabIndex = 5;
            this.GoButton.Text = "Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // NotesTextBox
            // 
            this.NotesTextBox.Location = new System.Drawing.Point(12, 428);
            this.NotesTextBox.Name = "NotesTextBox";
            this.NotesTextBox.ReadOnly = true;
            this.NotesTextBox.Size = new System.Drawing.Size(333, 183);
            this.NotesTextBox.TabIndex = 6;
            this.NotesTextBox.Text = "";
            // 
            // ActorDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 652);
            this.Controls.Add(this.NotesTextBox);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.ActorView);
            this.Controls.Add(this.Filter);
            this.Controls.Add(this.FilterTextBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SetActorButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ActorDatabase";
            this.Text = "Actor Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetActorButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TextBox FilterTextBox;
        private System.Windows.Forms.Label Filter;
        private System.Windows.Forms.TreeView ActorView;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.RichTextBox NotesTextBox;
    }
}