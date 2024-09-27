namespace SharpOcarina
{
    partial class CustomActorDatabase
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
            this.InstallButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.Filter = new System.Windows.Forms.Label();
            this.ActorView = new System.Windows.Forms.TreeView();
            this.GoButton = new System.Windows.Forms.Button();
            this.ActorDescription = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CategoriesButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ActorImage = new System.Windows.Forms.PictureBox();
            this.ActorName = new System.Windows.Forms.TextBox();
            this.ActorProperties = new System.Windows.Forms.TextBox();
            this.HideInstalledActors = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActorImage)).BeginInit();
            this.SuspendLayout();
            // 
            // InstallButton
            // 
            this.InstallButton.Enabled = false;
            this.InstallButton.Location = new System.Drawing.Point(49, 499);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(75, 23);
            this.InstallButton.TabIndex = 0;
            this.InstallButton.Text = "Install Actor";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(220, 499);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Location = new System.Drawing.Point(49, 12);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(196, 20);
            this.FilterTextBox.TabIndex = 2;
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
            this.ActorView.Size = new System.Drawing.Size(333, 444);
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
            // ActorDescription
            // 
            this.ActorDescription.Location = new System.Drawing.Point(351, 183);
            this.ActorDescription.Name = "ActorDescription";
            this.ActorDescription.ReadOnly = true;
            this.ActorDescription.Size = new System.Drawing.Size(348, 310);
            this.ActorDescription.TabIndex = 6;
            this.ActorDescription.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CategoriesButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(298, 10);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(20, 19);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CategoriesButton
            // 
            this.CategoriesButton.BackColor = System.Drawing.Color.Transparent;
            this.CategoriesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CategoriesButton.Image = global::SharpOcarina.Properties.Resources.options;
            this.CategoriesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CategoriesButton.Name = "CategoriesButton";
            this.CategoriesButton.ShowDropDownArrow = false;
            this.CategoriesButton.Size = new System.Drawing.Size(20, 20);
            this.CategoriesButton.Text = "toolStripButton1";
            this.CategoriesButton.ToolTipText = "Categories";
            // 
            // ActorImage
            // 
            this.ActorImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActorImage.Location = new System.Drawing.Point(571, 49);
            this.ActorImage.Name = "ActorImage";
            this.ActorImage.Size = new System.Drawing.Size(128, 128);
            this.ActorImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActorImage.TabIndex = 8;
            this.ActorImage.TabStop = false;
            // 
            // ActorName
            // 
            this.ActorName.Location = new System.Drawing.Point(351, 49);
            this.ActorName.Name = "ActorName";
            this.ActorName.ReadOnly = true;
            this.ActorName.Size = new System.Drawing.Size(214, 20);
            this.ActorName.TabIndex = 9;
            // 
            // ActorProperties
            // 
            this.ActorProperties.Location = new System.Drawing.Point(351, 75);
            this.ActorProperties.Multiline = true;
            this.ActorProperties.Name = "ActorProperties";
            this.ActorProperties.ReadOnly = true;
            this.ActorProperties.Size = new System.Drawing.Size(214, 102);
            this.ActorProperties.TabIndex = 10;
            // 
            // HideInstalledActors
            // 
            this.HideInstalledActors.AutoSize = true;
            this.HideInstalledActors.Location = new System.Drawing.Point(333, 14);
            this.HideInstalledActors.Name = "HideInstalledActors";
            this.HideInstalledActors.Size = new System.Drawing.Size(121, 17);
            this.HideInstalledActors.TabIndex = 11;
            this.HideInstalledActors.Text = "Hide installed actors";
            this.HideInstalledActors.UseVisualStyleBackColor = true;
            this.HideInstalledActors.CheckedChanged += new System.EventHandler(this.HideInstalledActors_CheckedChanged);
            // 
            // CustomActorDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 531);
            this.Controls.Add(this.HideInstalledActors);
            this.Controls.Add(this.ActorProperties);
            this.Controls.Add(this.ActorName);
            this.Controls.Add(this.ActorImage);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ActorDescription);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.ActorView);
            this.Controls.Add(this.Filter);
            this.Controls.Add(this.FilterTextBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.InstallButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CustomActorDatabase";
            this.Text = "Custom Actor Database";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActorImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.TextBox FilterTextBox;
        private System.Windows.Forms.Label Filter;
        private System.Windows.Forms.TreeView ActorView;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.RichTextBox ActorDescription;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton CategoriesButton;
        private System.Windows.Forms.PictureBox ActorImage;
        private System.Windows.Forms.TextBox ActorName;
        private System.Windows.Forms.TextBox ActorProperties;
        private System.Windows.Forms.CheckBox HideInstalledActors;
    }
}