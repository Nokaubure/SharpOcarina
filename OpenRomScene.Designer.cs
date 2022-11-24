namespace SharpOcarina
{
    partial class OpenRomScene
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
            this.OpenSceneButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SceneView = new System.Windows.Forms.TreeView();
            this.NotesTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // OpenSceneButton
            // 
            this.OpenSceneButton.Enabled = false;
            this.OpenSceneButton.Location = new System.Drawing.Point(49, 617);
            this.OpenSceneButton.Name = "OpenSceneButton";
            this.OpenSceneButton.Size = new System.Drawing.Size(75, 23);
            this.OpenSceneButton.TabIndex = 0;
            this.OpenSceneButton.Text = "Open Scene";
            this.OpenSceneButton.UseVisualStyleBackColor = true;
            this.OpenSceneButton.Click += new System.EventHandler(this.OpenSceneButton_Click);
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
            // SceneView
            // 
            this.SceneView.HideSelection = false;
            this.SceneView.Location = new System.Drawing.Point(12, 12);
            this.SceneView.Name = "SceneView";
            this.SceneView.Size = new System.Drawing.Size(333, 528);
            this.SceneView.TabIndex = 4;
            this.SceneView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SceneView_NodeMouseClick);
            // 
            // NotesTextBox
            // 
            this.NotesTextBox.Location = new System.Drawing.Point(12, 546);
            this.NotesTextBox.Name = "NotesTextBox";
            this.NotesTextBox.ReadOnly = true;
            this.NotesTextBox.Size = new System.Drawing.Size(333, 65);
            this.NotesTextBox.TabIndex = 6;
            this.NotesTextBox.Text = "";
            // 
            // OpenRomScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 652);
            this.Controls.Add(this.NotesTextBox);
            this.Controls.Add(this.SceneView);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OpenSceneButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OpenRomScene";
            this.Text = "Open Scene from ROM";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenSceneButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TreeView SceneView;
        private System.Windows.Forms.RichTextBox NotesTextBox;
    }
}