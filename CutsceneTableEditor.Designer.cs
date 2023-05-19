namespace SharpOcarina
{
    partial class CutsceneTableEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CutsceneGrid = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scene = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entrance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fadein_Animation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fadeout_Animation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debug_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Close = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumCutscenesLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CutsceneGrid
            // 
            this.CutsceneGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CutsceneGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CutsceneGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CutsceneGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Scene,
            this.Entrance,
            this.Fadein_Animation,
            this.Fadeout_Animation,
            this.Debug_Name});
            this.CutsceneGrid.Enabled = false;
            this.CutsceneGrid.Location = new System.Drawing.Point(12, 27);
            this.CutsceneGrid.Name = "CutsceneGrid";
            this.CutsceneGrid.Size = new System.Drawing.Size(732, 300);
            this.CutsceneGrid.TabIndex = 0;
            this.CutsceneGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.CutsceneGrid_CellFormatting);
            this.CutsceneGrid.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.CutsceneGrid_CellParsing);
            this.CutsceneGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.ObjectGrid_CellValidating);
            this.CutsceneGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.ObjectGrid_RowsAdded);
            this.CutsceneGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.ObjectGrid_RowsRemoved);
            this.CutsceneGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.EntranceGrid_RowValidating);
            // 
            // ID
            // 
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Format = "X04";
            dataGridViewCellStyle2.NullValue = "0";
            this.ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.ID.FillWeight = 65F;
            this.ID.HeaderText = "ID";
            this.ID.MaxInputLength = 4;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.ToolTipText = "Use this ID in the exit table when creating polygon types";
            // 
            // Scene
            // 
            dataGridViewCellStyle3.Format = "X04";
            dataGridViewCellStyle3.NullValue = "0000";
            this.Scene.DefaultCellStyle = dataGridViewCellStyle3;
            this.Scene.FillWeight = 65F;
            this.Scene.HeaderText = "Entrance";
            this.Scene.MaxInputLength = 4;
            this.Scene.Name = "Scene";
            this.Scene.ToolTipText = "Entrance index used to trigger the cutscene";
            // 
            // Entrance
            // 
            dataGridViewCellStyle4.Format = "X02";
            dataGridViewCellStyle4.NullValue = "00";
            this.Entrance.DefaultCellStyle = dataGridViewCellStyle4;
            this.Entrance.FillWeight = 65F;
            this.Entrance.HeaderText = "Age";
            this.Entrance.MaxInputLength = 2;
            this.Entrance.Name = "Entrance";
            this.Entrance.ToolTipText = "An age restriction code. 00 for adult only cutscenes, 01 for child only cutscenes" +
    ", 02 for no restriction";
            // 
            // Fadein_Animation
            // 
            dataGridViewCellStyle5.Format = "X02";
            dataGridViewCellStyle5.NullValue = "00";
            this.Fadein_Animation.DefaultCellStyle = dataGridViewCellStyle5;
            this.Fadein_Animation.FillWeight = 65F;
            this.Fadein_Animation.HeaderText = "Flag";
            this.Fadein_Animation.MaxInputLength = 2;
            this.Fadein_Animation.Name = "Fadein_Animation";
            this.Fadein_Animation.ToolTipText = "The event flag is used to record whether the cutscene should has already been sho" +
    "wn or not";
            // 
            // Fadeout_Animation
            // 
            dataGridViewCellStyle6.Format = "X08";
            dataGridViewCellStyle6.NullValue = "00000000";
            this.Fadeout_Animation.DefaultCellStyle = dataGridViewCellStyle6;
            this.Fadeout_Animation.FillWeight = 120F;
            this.Fadeout_Animation.HeaderText = "Segment Offset";
            this.Fadeout_Animation.MaxInputLength = 8;
            this.Fadeout_Animation.Name = "Fadeout_Animation";
            this.Fadeout_Animation.ToolTipText = "Segment offset location of the cutscene. Always starts with 02 for scene offset";
            // 
            // Debug_Name
            // 
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Gray;
            this.Debug_Name.DefaultCellStyle = dataGridViewCellStyle7;
            this.Debug_Name.FillWeight = 300F;
            this.Debug_Name.HeaderText = "Scene Name";
            this.Debug_Name.Name = "Debug_Name";
            this.Debug_Name.ReadOnly = true;
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(411, 361);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 1;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Close_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(756, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenRomToolStripMenuItem,
            this.saveROMToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.saveBinaryToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // OpenRomToolStripMenuItem
            // 
            this.OpenRomToolStripMenuItem.Name = "OpenRomToolStripMenuItem";
            this.OpenRomToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.OpenRomToolStripMenuItem.Text = "Open ROM...";
            this.OpenRomToolStripMenuItem.Click += new System.EventHandler(this.OpenRomToolStripMenuItem_Click);
            // 
            // saveROMToolStripMenuItem
            // 
            this.saveROMToolStripMenuItem.Name = "saveROMToolStripMenuItem";
            this.saveROMToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveROMToolStripMenuItem.Text = "Save ROM...";
            this.saveROMToolStripMenuItem.Visible = false;
            this.saveROMToolStripMenuItem.Click += new System.EventHandler(this.saveROMToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Enabled = false;
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.openFileToolStripMenuItem.Text = "Open ZTables File...";
            this.openFileToolStripMenuItem.Visible = false;
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Enabled = false;
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveFileToolStripMenuItem.Text = "Save ZTables File...";
            this.saveFileToolStripMenuItem.Visible = false;
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // saveBinaryToolStripMenuItem
            // 
            this.saveBinaryToolStripMenuItem.Name = "saveBinaryToolStripMenuItem";
            this.saveBinaryToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveBinaryToolStripMenuItem.Text = "Save binary...";
            this.saveBinaryToolStripMenuItem.Click += new System.EventHandler(this.saveBinaryToolStripMenuItem_Click);
            // 
            // NumCutscenesLabel
            // 
            this.NumCutscenesLabel.AutoSize = true;
            this.NumCutscenesLabel.Location = new System.Drawing.Point(608, 341);
            this.NumCutscenesLabel.Name = "NumCutscenesLabel";
            this.NumCutscenesLabel.Size = new System.Drawing.Size(124, 13);
            this.NumCutscenesLabel.TabIndex = 3;
            this.NumCutscenesLabel.Text = "Number of Cutscenes:  ?";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.CreatePrompt = true;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(305, 361);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // CutsceneTableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 396);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.NumCutscenesLabel);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.CutsceneGrid);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CutsceneTableEditor";
            this.Text = "Cutscene Table Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CutsceneTableEditor_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView CutsceneGrid;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label NumCutscenesLabel;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem OpenRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveROMToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scene;
        private System.Windows.Forms.DataGridViewTextBoxColumn Entrance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fadein_Animation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fadeout_Animation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debug_Name;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.ToolStripMenuItem saveBinaryToolStripMenuItem;
    }
}