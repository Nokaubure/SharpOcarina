using System.Windows.Forms;

namespace SharpOcarina
{
    partial class RestrictionFlagEditor
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
            this.RestrictionFlagGrid = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scene = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bottles = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HealthGauge = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.WarpSongs = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Ocarina = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Hookshots = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TradeItems = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Global = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DinNayru = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Farore = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SunSong = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Debug_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Close = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumEntriesLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RestrictionFlagGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RestrictionFlagGrid
            // 
            this.RestrictionFlagGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RestrictionFlagGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RestrictionFlagGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RestrictionFlagGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Scene,
            this.Bottles,
            this.AButton,
            this.BButton,
            this.HealthGauge,
            this.WarpSongs,
            this.Ocarina,
            this.Hookshots,
            this.TradeItems,
            this.Global,
            this.DinNayru,
            this.Farore,
            this.SunSong,
            this.Debug_Name});
            this.RestrictionFlagGrid.Enabled = false;
            this.RestrictionFlagGrid.Location = new System.Drawing.Point(12, 27);
            this.RestrictionFlagGrid.Name = "RestrictionFlagGrid";
            this.RestrictionFlagGrid.Size = new System.Drawing.Size(1064, 300);
            this.RestrictionFlagGrid.TabIndex = 0;
            this.RestrictionFlagGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.EntranceGrid_CellFormatting);
            this.RestrictionFlagGrid.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.EntranceGrid_CellParsing);
            this.RestrictionFlagGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.ObjectGrid_CellValidating);
            this.RestrictionFlagGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.ObjectGrid_RowsAdded);
            this.RestrictionFlagGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.ObjectGrid_RowsRemoved);
            this.RestrictionFlagGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.EntranceGrid_RowValidating);
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
            // Bottles
            // 
            this.Bottles.FillWeight = 75F;
            this.Bottles.HeaderText = "Bottles";
            this.Bottles.Name = "Bottles";
            this.Bottles.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Bottles.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // AButton
            // 
            this.AButton.FillWeight = 75F;
            this.AButton.HeaderText = "AButton";
            this.AButton.Name = "AButton";
            this.AButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // BButton
            // 
            this.BButton.FillWeight = 75F;
            this.BButton.HeaderText = "BButton";
            this.BButton.Name = "BButton";
            this.BButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // HealthGauge
            // 
            this.HealthGauge.FillWeight = 75F;
            this.HealthGauge.HeaderText = "Health";
            this.HealthGauge.Name = "HealthGauge";
            this.HealthGauge.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.HealthGauge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // WarpSongs
            // 
            this.WarpSongs.FillWeight = 75F;
            this.WarpSongs.HeaderText = "WarpSongs";
            this.WarpSongs.Name = "WarpSongs";
            this.WarpSongs.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.WarpSongs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Ocarina
            // 
            this.Ocarina.FillWeight = 75F;
            this.Ocarina.HeaderText = "Ocarina";
            this.Ocarina.Name = "Ocarina";
            this.Ocarina.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Ocarina.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Hookshots
            // 
            this.Hookshots.FillWeight = 75F;
            this.Hookshots.HeaderText = "Hookshots";
            this.Hookshots.Name = "Hookshots";
            this.Hookshots.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Hookshots.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // TradeItems
            // 
            this.TradeItems.FillWeight = 75F;
            this.TradeItems.HeaderText = "TradeItems";
            this.TradeItems.Name = "TradeItems";
            this.TradeItems.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TradeItems.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Global
            // 
            this.Global.FillWeight = 75F;
            this.Global.HeaderText = "Global";
            this.Global.Name = "Global";
            this.Global.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Global.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DinNayru
            // 
            this.DinNayru.FillWeight = 75F;
            this.DinNayru.HeaderText = "DinNayru";
            this.DinNayru.Name = "DinNayru";
            this.DinNayru.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DinNayru.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Farore
            // 
            this.Farore.FillWeight = 75F;
            this.Farore.HeaderText = "Farore";
            this.Farore.Name = "Farore";
            this.Farore.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Farore.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // SunSong
            // 
            this.SunSong.FillWeight = 75F;
            this.SunSong.HeaderText = "SunSong";
            this.SunSong.Name = "SunSong";
            this.SunSong.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SunSong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Debug_Name
            // 
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Gray;
            this.Debug_Name.DefaultCellStyle = dataGridViewCellStyle4;
            this.Debug_Name.FillWeight = 300F;
            this.Debug_Name.HeaderText = "Scene Name";
            this.Debug_Name.Name = "Debug_Name";
            this.Debug_Name.ReadOnly = true;
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(590, 361);
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
            this.menuStrip1.Size = new System.Drawing.Size(1088, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenRomToolStripMenuItem,
            this.saveROMToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // OpenRomToolStripMenuItem
            // 
            this.OpenRomToolStripMenuItem.Name = "OpenRomToolStripMenuItem";
            this.OpenRomToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.OpenRomToolStripMenuItem.Text = "Open ROM...";
            this.OpenRomToolStripMenuItem.Click += new System.EventHandler(this.OpenRomToolStripMenuItem_Click);
            // 
            // saveROMToolStripMenuItem
            // 
            this.saveROMToolStripMenuItem.Name = "saveROMToolStripMenuItem";
            this.saveROMToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.saveROMToolStripMenuItem.Text = "Save ROM...";
            this.saveROMToolStripMenuItem.Visible = false;
            this.saveROMToolStripMenuItem.Click += new System.EventHandler(this.saveROMToolStripMenuItem_Click);
            // 
            // NumEntriesLabel
            // 
            this.NumEntriesLabel.AutoSize = true;
            this.NumEntriesLabel.Location = new System.Drawing.Point(954, 340);
            this.NumEntriesLabel.Name = "NumEntriesLabel";
            this.NumEntriesLabel.Size = new System.Drawing.Size(106, 13);
            this.NumEntriesLabel.TabIndex = 3;
            this.NumEntriesLabel.Text = "Number of Entries:  ?";
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
            this.Save.Location = new System.Drawing.Point(497, 361);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // RestrictionFlagEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 396);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.NumEntriesLabel);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.RestrictionFlagGrid);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RestrictionFlagEditor";
            this.Text = "Restriction Flag Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RestrictionFlagEditor_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.RestrictionFlagGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView RestrictionFlagGrid;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label NumEntriesLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem OpenRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveROMToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Health;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Hookshot;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Scene;
        private DataGridViewCheckBoxColumn Bottles;
        private DataGridViewCheckBoxColumn AButton;
        private DataGridViewCheckBoxColumn BButton;
        private DataGridViewCheckBoxColumn HealthGauge;
        private DataGridViewCheckBoxColumn WarpSongs;
        private DataGridViewCheckBoxColumn Ocarina;
        private DataGridViewCheckBoxColumn Hookshots;
        private DataGridViewCheckBoxColumn TradeItems;
        private DataGridViewCheckBoxColumn Global;
        private DataGridViewCheckBoxColumn DinNayru;
        private DataGridViewCheckBoxColumn Farore;
        private DataGridViewCheckBoxColumn SunSong;
        private DataGridViewTextBoxColumn Debug_Name;
        private Button Save;
    }
}