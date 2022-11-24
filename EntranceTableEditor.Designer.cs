namespace SharpOcarina
{
    partial class EntranceTableEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntranceTableEditor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.EntranceGrid = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scene = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entrance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fadein_Animation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fadeout_Animation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unknown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debug_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Close = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumEntrancesLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EntranceGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EntranceGrid
            // 
            this.EntranceGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EntranceGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.EntranceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EntranceGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Scene,
            this.Entrance,
            this.Variable,
            this.Fadein_Animation,
            this.Fadeout_Animation,
            this.Unknown,
            this.Debug_Name});
            this.EntranceGrid.Enabled = false;
            this.EntranceGrid.Location = new System.Drawing.Point(12, 27);
            this.EntranceGrid.Name = "EntranceGrid";
            this.EntranceGrid.Size = new System.Drawing.Size(732, 300);
            this.EntranceGrid.TabIndex = 0;
            this.EntranceGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.EntranceGrid_CellFormatting);
            this.EntranceGrid.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.EntranceGrid_CellParsing);
            this.EntranceGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.ObjectGrid_CellValidating);
            this.EntranceGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.ObjectGrid_RowsAdded);
            this.EntranceGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.ObjectGrid_RowsRemoved);
            this.EntranceGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.EntranceGrid_RowValidating);
            // 
            // ID
            // 
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Format = "X04";
            dataGridViewCellStyle2.NullValue = "0";
            this.ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.ID.FillWeight = 68.52792F;
            this.ID.HeaderText = "ID";
            this.ID.MaxInputLength = 4;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.ToolTipText = "Use this ID in the exit table when creating polygon types";
            // 
            // Scene
            // 
            dataGridViewCellStyle3.Format = "X02";
            dataGridViewCellStyle3.NullValue = "00";
            this.Scene.DefaultCellStyle = dataGridViewCellStyle3;
            this.Scene.FillWeight = 75F;
            this.Scene.HeaderText = "Scene";
            this.Scene.MaxInputLength = 2;
            this.Scene.Name = "Scene";
            this.Scene.ToolTipText = "The scene ID in hex";
            // 
            // Entrance
            // 
            dataGridViewCellStyle4.Format = "X02";
            dataGridViewCellStyle4.NullValue = "00";
            this.Entrance.DefaultCellStyle = dataGridViewCellStyle4;
            this.Entrance.FillWeight = 75F;
            this.Entrance.HeaderText = "Entrance";
            this.Entrance.MaxInputLength = 2;
            this.Entrance.Name = "Entrance";
            this.Entrance.ToolTipText = "The spawner you want to use for this entrance (for example if you have 3 spawns, " +
    "02 would be the third)";
            // 
            // Variable
            // 
            dataGridViewCellStyle5.Format = "X01";
            dataGridViewCellStyle5.NullValue = "0";
            this.Variable.DefaultCellStyle = dataGridViewCellStyle5;
            this.Variable.FillWeight = 75F;
            this.Variable.HeaderText = "Play Music";
            this.Variable.MaxInputLength = 1;
            this.Variable.Name = "Variable";
            this.Variable.ToolTipText = "Continue playing background music when exiting. 0 stops it, 1 continues playing i" +
    "t";
            // 
            // Fadein_Animation
            // 
            dataGridViewCellStyle6.Format = "X02";
            dataGridViewCellStyle6.NullValue = "00";
            this.Fadein_Animation.DefaultCellStyle = dataGridViewCellStyle6;
            this.Fadein_Animation.HeaderText = "Fade-in Animation";
            this.Fadein_Animation.MaxInputLength = 2;
            this.Fadein_Animation.Name = "Fadein_Animation";
            this.Fadein_Animation.ToolTipText = resources.GetString("Fadein_Animation.ToolTipText");
            // 
            // Fadeout_Animation
            // 
            dataGridViewCellStyle7.Format = "X02";
            dataGridViewCellStyle7.NullValue = "00";
            this.Fadeout_Animation.DefaultCellStyle = dataGridViewCellStyle7;
            this.Fadeout_Animation.HeaderText = "Fade-out Animation";
            this.Fadeout_Animation.MaxInputLength = 2;
            this.Fadeout_Animation.Name = "Fadeout_Animation";
            this.Fadeout_Animation.ToolTipText = resources.GetString("Fadeout_Animation.ToolTipText");
            // 
            // Unknown
            // 
            dataGridViewCellStyle8.Format = "X01";
            dataGridViewCellStyle8.NullValue = "0";
            this.Unknown.DefaultCellStyle = dataGridViewCellStyle8;
            this.Unknown.FillWeight = 75F;
            this.Unknown.HeaderText = "Title Card";
            this.Unknown.MaxInputLength = 1;
            this.Unknown.Name = "Unknown";
            this.Unknown.ToolTipText = "Set it to 1 to display the scene title card";
            // 
            // Debug_Name
            // 
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Gray;
            this.Debug_Name.DefaultCellStyle = dataGridViewCellStyle9;
            this.Debug_Name.FillWeight = 300F;
            this.Debug_Name.HeaderText = "Scene Name";
            this.Debug_Name.Name = "Debug_Name";
            this.Debug_Name.ReadOnly = true;
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(399, 361);
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
            this.saveFileToolStripMenuItem});
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
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.openFileToolStripMenuItem.Text = "Open ZTables File...";
            this.openFileToolStripMenuItem.Visible = false;
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveFileToolStripMenuItem.Text = "Save ZTables File...";
            this.saveFileToolStripMenuItem.Visible = false;
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // NumEntrancesLabel
            // 
            this.NumEntrancesLabel.AutoSize = true;
            this.NumEntrancesLabel.Location = new System.Drawing.Point(608, 341);
            this.NumEntrancesLabel.Name = "NumEntrancesLabel";
            this.NumEntrancesLabel.Size = new System.Drawing.Size(122, 13);
            this.NumEntrancesLabel.TabIndex = 3;
            this.NumEntrancesLabel.Text = "Number of Entrances:  ?";
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
            this.Save.Location = new System.Drawing.Point(290, 361);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // EntranceTableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 396);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.NumEntrancesLabel);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.EntranceGrid);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EntranceTableEditor";
            this.Text = "Entrance Table Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EntranceTableEditor_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.EntranceGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView EntranceGrid;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label NumEntrancesLabel;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem OpenRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveROMToolStripMenuItem;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scene;
        private System.Windows.Forms.DataGridViewTextBoxColumn Entrance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fadein_Animation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fadeout_Animation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unknown;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debug_Name;
    }
}