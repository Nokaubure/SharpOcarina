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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CategoriesButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.AutoSetCheckbox = new System.Windows.Forms.CheckBox();
            this.DebugNamesCheckbox = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SetActorButton
            // 
            this.SetActorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SetActorButton.Enabled = false;
            this.SetActorButton.Location = new System.Drawing.Point(49, 629);
            this.SetActorButton.Name = "SetActorButton";
            this.SetActorButton.Size = new System.Drawing.Size(75, 23);
            this.SetActorButton.TabIndex = 0;
            this.SetActorButton.Text = "Set Actor";
            this.SetActorButton.UseVisualStyleBackColor = true;
            this.SetActorButton.Click += new System.EventHandler(this.SetActorButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.Location = new System.Drawing.Point(220, 629);
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
            this.ActorView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.NotesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NotesTextBox.Location = new System.Drawing.Point(12, 417);
            this.NotesTextBox.Name = "NotesTextBox";
            this.NotesTextBox.ReadOnly = true;
            this.NotesTextBox.Size = new System.Drawing.Size(333, 183);
            this.NotesTextBox.TabIndex = 6;
            this.NotesTextBox.Text = "";
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
            // AutoSetCheckbox
            // 
            this.AutoSetCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoSetCheckbox.AutoSize = true;
            this.AutoSetCheckbox.Location = new System.Drawing.Point(49, 606);
            this.AutoSetCheckbox.Name = "AutoSetCheckbox";
            this.AutoSetCheckbox.Size = new System.Drawing.Size(90, 17);
            this.AutoSetCheckbox.TabIndex = 8;
            this.AutoSetCheckbox.Text = "Set on Select";
            this.AutoSetCheckbox.UseVisualStyleBackColor = true;
            this.AutoSetCheckbox.CheckedChanged += new System.EventHandler(this.AutoSetCheckbox_CheckedChanged);
            // 
            // DebugNamesCheckbox
            // 
            this.DebugNamesCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DebugNamesCheckbox.AutoSize = true;
            this.DebugNamesCheckbox.Location = new System.Drawing.Point(175, 606);
            this.DebugNamesCheckbox.Name = "DebugNamesCheckbox";
            this.DebugNamesCheckbox.Size = new System.Drawing.Size(120, 17);
            this.DebugNamesCheckbox.TabIndex = 9;
            this.DebugNamesCheckbox.Text = "Show debug names";
            this.DebugNamesCheckbox.UseVisualStyleBackColor = true;
            this.DebugNamesCheckbox.CheckedChanged += new System.EventHandler(this.DebugNamesCheckbox_CheckedChanged);
            // 
            // ActorDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 664);
            this.Controls.Add(this.DebugNamesCheckbox);
            this.Controls.Add(this.AutoSetCheckbox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.NotesTextBox);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.ActorView);
            this.Controls.Add(this.Filter);
            this.Controls.Add(this.FilterTextBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SetActorButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(373, 2080);
            this.MinimumSize = new System.Drawing.Size(373, 703);
            this.Name = "ActorDatabase";
            this.Text = "Actor Database";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ActorDatabase_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton CategoriesButton;
        private System.Windows.Forms.CheckBox AutoSetCheckbox;
        private System.Windows.Forms.CheckBox DebugNamesCheckbox;
    }
}