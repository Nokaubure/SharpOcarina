namespace SharpOcarina
{
    partial class ObjectTableEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ObjectGrid = new System.Windows.Forms.DataGridView();
            this.Close = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumObjectsLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extractFromRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ObjectGrid
            // 
            this.ObjectGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ObjectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ObjectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ObjectStart,
            this.ObjectEnd});
            this.ObjectGrid.Enabled = false;
            this.ObjectGrid.Location = new System.Drawing.Point(12, 27);
            this.ObjectGrid.Name = "ObjectGrid";
            this.ObjectGrid.Size = new System.Drawing.Size(326, 300);
            this.ObjectGrid.TabIndex = 0;
            this.ObjectGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.ObjectGrid_CellValidating);
            this.ObjectGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.ObjectGrid_RowsAdded);
            this.ObjectGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.ObjectGrid_RowsRemoved);
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(137, 365);
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
            this.menuStrip1.Size = new System.Drawing.Size(350, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.extractFromRomToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.openFileToolStripMenuItem.Text = "Open File...";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveFileToolStripMenuItem.Text = "Save File...";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // NumObjectsLabel
            // 
            this.NumObjectsLabel.AutoSize = true;
            this.NumObjectsLabel.Location = new System.Drawing.Point(196, 330);
            this.NumObjectsLabel.Name = "NumObjectsLabel";
            this.NumObjectsLabel.Size = new System.Drawing.Size(122, 13);
            this.NumObjectsLabel.TabIndex = 3;
            this.NumObjectsLabel.Text = "Number of Objects:  402";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.CreatePrompt = true;
            // 
            // ID
            // 
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle16.Format = "X04";
            dataGridViewCellStyle16.NullValue = "0";
            this.ID.DefaultCellStyle = dataGridViewCellStyle16;
            this.ID.FillWeight = 68.52792F;
            this.ID.HeaderText = "ID";
            this.ID.MaxInputLength = 4;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // ObjectStart
            // 
            dataGridViewCellStyle17.Format = "X08";
            dataGridViewCellStyle17.NullValue = "00000000";
            this.ObjectStart.DefaultCellStyle = dataGridViewCellStyle17;
            this.ObjectStart.FillWeight = 115.736F;
            this.ObjectStart.HeaderText = "ObjectStart";
            this.ObjectStart.MaxInputLength = 8;
            this.ObjectStart.Name = "ObjectStart";
            // 
            // ObjectEnd
            // 
            dataGridViewCellStyle18.Format = "X08";
            dataGridViewCellStyle18.NullValue = "00000000";
            this.ObjectEnd.DefaultCellStyle = dataGridViewCellStyle18;
            this.ObjectEnd.FillWeight = 115.736F;
            this.ObjectEnd.HeaderText = "ObjectEnd";
            this.ObjectEnd.MaxInputLength = 8;
            this.ObjectEnd.Name = "ObjectEnd";
            // 
            // extractFromRomToolStripMenuItem
            // 
            this.extractFromRomToolStripMenuItem.Name = "extractFromRomToolStripMenuItem";
            this.extractFromRomToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.extractFromRomToolStripMenuItem.Text = "Extract from Rom...";
            this.extractFromRomToolStripMenuItem.Click += new System.EventHandler(this.extractFromRomToolStripMenuItem_Click);
            // 
            // ObjectTableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 396);
            this.Controls.Add(this.NumObjectsLabel);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.ObjectGrid);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ObjectTableEditor";
            this.Text = "Object Table Editor";
            ((System.ComponentModel.ISupportInitialize)(this.ObjectGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ObjectGrid;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label NumObjectsLabel;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectEnd;
        private System.Windows.Forms.ToolStripMenuItem extractFromRomToolStripMenuItem;
    }
}