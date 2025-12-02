using System.Windows.Forms;

namespace SharpOcarina
{
    partial class RandomDropTableEditor
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
            this.Close = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsZ64romPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsCHeaderArrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.DropTableID = new System.Windows.Forms.NumericUpDown();
            this.Item0 = new System.Windows.Forms.ComboBox();
            this.Amount0 = new System.Windows.Forms.NumericUpDown();
            this.Amount1 = new System.Windows.Forms.NumericUpDown();
            this.Item1 = new System.Windows.Forms.ComboBox();
            this.Amount2 = new System.Windows.Forms.NumericUpDown();
            this.Item2 = new System.Windows.Forms.ComboBox();
            this.Amount3 = new System.Windows.Forms.NumericUpDown();
            this.Item3 = new System.Windows.Forms.ComboBox();
            this.Amount4 = new System.Windows.Forms.NumericUpDown();
            this.Item4 = new System.Windows.Forms.ComboBox();
            this.Amount5 = new System.Windows.Forms.NumericUpDown();
            this.Item5 = new System.Windows.Forms.ComboBox();
            this.Amount6 = new System.Windows.Forms.NumericUpDown();
            this.Item6 = new System.Windows.Forms.ComboBox();
            this.Amount7 = new System.Windows.Forms.NumericUpDown();
            this.Item7 = new System.Windows.Forms.ComboBox();
            this.Amount8 = new System.Windows.Forms.NumericUpDown();
            this.Item8 = new System.Windows.Forms.ComboBox();
            this.Amount9 = new System.Windows.Forms.NumericUpDown();
            this.Item9 = new System.Windows.Forms.ComboBox();
            this.Amount10 = new System.Windows.Forms.NumericUpDown();
            this.Item10 = new System.Windows.Forms.ComboBox();
            this.Amount11 = new System.Windows.Forms.NumericUpDown();
            this.Item11 = new System.Windows.Forms.ComboBox();
            this.Amount12 = new System.Windows.Forms.NumericUpDown();
            this.Item12 = new System.Windows.Forms.ComboBox();
            this.Amount13 = new System.Windows.Forms.NumericUpDown();
            this.Item13 = new System.Windows.Forms.ComboBox();
            this.Amount14 = new System.Windows.Forms.NumericUpDown();
            this.Item14 = new System.Windows.Forms.ComboBox();
            this.Amount15 = new System.Windows.Forms.NumericUpDown();
            this.Item15 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DropTableID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount15)).BeginInit();
            this.SuspendLayout();
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(198, 361);
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
            this.menuStrip1.Size = new System.Drawing.Size(507, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveROMToolStripMenuItem,
            this.exportBinaryToolStripMenuItem,
            this.exportAsZ64romPatchToolStripMenuItem,
            this.exportAsCHeaderArrayToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveROMToolStripMenuItem
            // 
            this.saveROMToolStripMenuItem.Name = "saveROMToolStripMenuItem";
            this.saveROMToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.saveROMToolStripMenuItem.Text = "Save to ROM";
            this.saveROMToolStripMenuItem.Click += new System.EventHandler(this.saveROMToolStripMenuItem_Click);
            // 
            // exportBinaryToolStripMenuItem
            // 
            this.exportBinaryToolStripMenuItem.Name = "exportBinaryToolStripMenuItem";
            this.exportBinaryToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exportBinaryToolStripMenuItem.Text = "Export Binary";
            this.exportBinaryToolStripMenuItem.Click += new System.EventHandler(this.exportBinaryToolStripMenuItem_Click);
            // 
            // exportAsZ64romPatchToolStripMenuItem
            // 
            this.exportAsZ64romPatchToolStripMenuItem.Name = "exportAsZ64romPatchToolStripMenuItem";
            this.exportAsZ64romPatchToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exportAsZ64romPatchToolStripMenuItem.Text = "Export as z64rom patch";
            this.exportAsZ64romPatchToolStripMenuItem.Click += new System.EventHandler(this.exportAsZ64romPatchToolStripMenuItem_Click);
            // 
            // exportAsCHeaderArrayToolStripMenuItem
            // 
            this.exportAsCHeaderArrayToolStripMenuItem.Name = "exportAsCHeaderArrayToolStripMenuItem";
            this.exportAsCHeaderArrayToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exportAsCHeaderArrayToolStripMenuItem.Text = "Convert to C header array";
            this.exportAsCHeaderArrayToolStripMenuItem.Click += new System.EventHandler(this.exportAsCHeaderArrayToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.CreatePrompt = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Drop Table ID";
            // 
            // DropTableID
            // 
            this.DropTableID.Location = new System.Drawing.Point(22, 41);
            this.DropTableID.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.DropTableID.Name = "DropTableID";
            this.DropTableID.Size = new System.Drawing.Size(47, 20);
            this.DropTableID.TabIndex = 5;
            this.DropTableID.ValueChanged += new System.EventHandler(this.DropTableID_ValueChanged);
            // 
            // Item0
            // 
            this.Item0.FormattingEnabled = true;
            this.Item0.Location = new System.Drawing.Point(22, 87);
            this.Item0.Name = "Item0";
            this.Item0.Size = new System.Drawing.Size(121, 21);
            this.Item0.TabIndex = 6;
            // 
            // Amount0
            // 
            this.Amount0.Location = new System.Drawing.Point(149, 88);
            this.Amount0.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount0.Name = "Amount0";
            this.Amount0.Size = new System.Drawing.Size(47, 20);
            this.Amount0.TabIndex = 7;
            // 
            // Amount1
            // 
            this.Amount1.Location = new System.Drawing.Point(149, 115);
            this.Amount1.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount1.Name = "Amount1";
            this.Amount1.Size = new System.Drawing.Size(47, 20);
            this.Amount1.TabIndex = 9;
            // 
            // Item1
            // 
            this.Item1.FormattingEnabled = true;
            this.Item1.Location = new System.Drawing.Point(22, 114);
            this.Item1.Name = "Item1";
            this.Item1.Size = new System.Drawing.Size(121, 21);
            this.Item1.TabIndex = 8;
            // 
            // Amount2
            // 
            this.Amount2.Location = new System.Drawing.Point(149, 142);
            this.Amount2.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount2.Name = "Amount2";
            this.Amount2.Size = new System.Drawing.Size(47, 20);
            this.Amount2.TabIndex = 11;
            // 
            // Item2
            // 
            this.Item2.FormattingEnabled = true;
            this.Item2.Location = new System.Drawing.Point(22, 141);
            this.Item2.Name = "Item2";
            this.Item2.Size = new System.Drawing.Size(121, 21);
            this.Item2.TabIndex = 10;
            // 
            // Amount3
            // 
            this.Amount3.Location = new System.Drawing.Point(149, 169);
            this.Amount3.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount3.Name = "Amount3";
            this.Amount3.Size = new System.Drawing.Size(47, 20);
            this.Amount3.TabIndex = 13;
            // 
            // Item3
            // 
            this.Item3.FormattingEnabled = true;
            this.Item3.Location = new System.Drawing.Point(22, 168);
            this.Item3.Name = "Item3";
            this.Item3.Size = new System.Drawing.Size(121, 21);
            this.Item3.TabIndex = 12;
            // 
            // Amount4
            // 
            this.Amount4.Location = new System.Drawing.Point(149, 196);
            this.Amount4.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount4.Name = "Amount4";
            this.Amount4.Size = new System.Drawing.Size(47, 20);
            this.Amount4.TabIndex = 15;
            // 
            // Item4
            // 
            this.Item4.FormattingEnabled = true;
            this.Item4.Location = new System.Drawing.Point(22, 195);
            this.Item4.Name = "Item4";
            this.Item4.Size = new System.Drawing.Size(121, 21);
            this.Item4.TabIndex = 14;
            // 
            // Amount5
            // 
            this.Amount5.Location = new System.Drawing.Point(149, 223);
            this.Amount5.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount5.Name = "Amount5";
            this.Amount5.Size = new System.Drawing.Size(47, 20);
            this.Amount5.TabIndex = 17;
            // 
            // Item5
            // 
            this.Item5.FormattingEnabled = true;
            this.Item5.Location = new System.Drawing.Point(22, 222);
            this.Item5.Name = "Item5";
            this.Item5.Size = new System.Drawing.Size(121, 21);
            this.Item5.TabIndex = 16;
            // 
            // Amount6
            // 
            this.Amount6.Location = new System.Drawing.Point(149, 250);
            this.Amount6.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount6.Name = "Amount6";
            this.Amount6.Size = new System.Drawing.Size(47, 20);
            this.Amount6.TabIndex = 19;
            // 
            // Item6
            // 
            this.Item6.FormattingEnabled = true;
            this.Item6.Location = new System.Drawing.Point(22, 249);
            this.Item6.Name = "Item6";
            this.Item6.Size = new System.Drawing.Size(121, 21);
            this.Item6.TabIndex = 18;
            // 
            // Amount7
            // 
            this.Amount7.Location = new System.Drawing.Point(149, 277);
            this.Amount7.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount7.Name = "Amount7";
            this.Amount7.Size = new System.Drawing.Size(47, 20);
            this.Amount7.TabIndex = 21;
            // 
            // Item7
            // 
            this.Item7.FormattingEnabled = true;
            this.Item7.Location = new System.Drawing.Point(22, 276);
            this.Item7.Name = "Item7";
            this.Item7.Size = new System.Drawing.Size(121, 21);
            this.Item7.TabIndex = 20;
            // 
            // Amount8
            // 
            this.Amount8.Location = new System.Drawing.Point(390, 278);
            this.Amount8.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount8.Name = "Amount8";
            this.Amount8.Size = new System.Drawing.Size(47, 20);
            this.Amount8.TabIndex = 37;
            // 
            // Item8
            // 
            this.Item8.FormattingEnabled = true;
            this.Item8.Location = new System.Drawing.Point(263, 277);
            this.Item8.Name = "Item8";
            this.Item8.Size = new System.Drawing.Size(121, 21);
            this.Item8.TabIndex = 36;
            // 
            // Amount9
            // 
            this.Amount9.Location = new System.Drawing.Point(390, 251);
            this.Amount9.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount9.Name = "Amount9";
            this.Amount9.Size = new System.Drawing.Size(47, 20);
            this.Amount9.TabIndex = 35;
            // 
            // Item9
            // 
            this.Item9.FormattingEnabled = true;
            this.Item9.Location = new System.Drawing.Point(263, 250);
            this.Item9.Name = "Item9";
            this.Item9.Size = new System.Drawing.Size(121, 21);
            this.Item9.TabIndex = 34;
            // 
            // Amount10
            // 
            this.Amount10.Location = new System.Drawing.Point(390, 224);
            this.Amount10.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount10.Name = "Amount10";
            this.Amount10.Size = new System.Drawing.Size(47, 20);
            this.Amount10.TabIndex = 33;
            // 
            // Item10
            // 
            this.Item10.FormattingEnabled = true;
            this.Item10.Location = new System.Drawing.Point(263, 223);
            this.Item10.Name = "Item10";
            this.Item10.Size = new System.Drawing.Size(121, 21);
            this.Item10.TabIndex = 32;
            // 
            // Amount11
            // 
            this.Amount11.Location = new System.Drawing.Point(390, 197);
            this.Amount11.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount11.Name = "Amount11";
            this.Amount11.Size = new System.Drawing.Size(47, 20);
            this.Amount11.TabIndex = 31;
            // 
            // Item11
            // 
            this.Item11.FormattingEnabled = true;
            this.Item11.Location = new System.Drawing.Point(263, 196);
            this.Item11.Name = "Item11";
            this.Item11.Size = new System.Drawing.Size(121, 21);
            this.Item11.TabIndex = 30;
            // 
            // Amount12
            // 
            this.Amount12.Location = new System.Drawing.Point(390, 170);
            this.Amount12.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount12.Name = "Amount12";
            this.Amount12.Size = new System.Drawing.Size(47, 20);
            this.Amount12.TabIndex = 29;
            // 
            // Item12
            // 
            this.Item12.FormattingEnabled = true;
            this.Item12.Location = new System.Drawing.Point(263, 169);
            this.Item12.Name = "Item12";
            this.Item12.Size = new System.Drawing.Size(121, 21);
            this.Item12.TabIndex = 28;
            // 
            // Amount13
            // 
            this.Amount13.Location = new System.Drawing.Point(390, 143);
            this.Amount13.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount13.Name = "Amount13";
            this.Amount13.Size = new System.Drawing.Size(47, 20);
            this.Amount13.TabIndex = 27;
            // 
            // Item13
            // 
            this.Item13.FormattingEnabled = true;
            this.Item13.Location = new System.Drawing.Point(263, 142);
            this.Item13.Name = "Item13";
            this.Item13.Size = new System.Drawing.Size(121, 21);
            this.Item13.TabIndex = 26;
            // 
            // Amount14
            // 
            this.Amount14.Location = new System.Drawing.Point(390, 116);
            this.Amount14.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount14.Name = "Amount14";
            this.Amount14.Size = new System.Drawing.Size(47, 20);
            this.Amount14.TabIndex = 25;
            // 
            // Item14
            // 
            this.Item14.FormattingEnabled = true;
            this.Item14.Location = new System.Drawing.Point(263, 115);
            this.Item14.Name = "Item14";
            this.Item14.Size = new System.Drawing.Size(121, 21);
            this.Item14.TabIndex = 24;
            // 
            // Amount15
            // 
            this.Amount15.Location = new System.Drawing.Point(390, 89);
            this.Amount15.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Amount15.Name = "Amount15";
            this.Amount15.Size = new System.Drawing.Size(47, 20);
            this.Amount15.TabIndex = 23;
            // 
            // Item15
            // 
            this.Item15.FormattingEnabled = true;
            this.Item15.Location = new System.Drawing.Point(263, 88);
            this.Item15.Name = "Item15";
            this.Item15.Size = new System.Drawing.Size(121, 21);
            this.Item15.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 324);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Check this page for more info:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(173, 324);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(493, 13);
            this.linkLabel1.TabIndex = 39;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://discord.com/channels/388361645073629187/388362111534759942/14106679956631" +
    "75813";
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 346);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Click on File to save changes";
            // 
            // RandomDropTableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 396);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Amount8);
            this.Controls.Add(this.Item8);
            this.Controls.Add(this.Amount9);
            this.Controls.Add(this.Item9);
            this.Controls.Add(this.Amount10);
            this.Controls.Add(this.Item10);
            this.Controls.Add(this.Amount11);
            this.Controls.Add(this.Item11);
            this.Controls.Add(this.Amount12);
            this.Controls.Add(this.Item12);
            this.Controls.Add(this.Amount13);
            this.Controls.Add(this.Item13);
            this.Controls.Add(this.Amount14);
            this.Controls.Add(this.Item14);
            this.Controls.Add(this.Amount15);
            this.Controls.Add(this.Item15);
            this.Controls.Add(this.Amount7);
            this.Controls.Add(this.Item7);
            this.Controls.Add(this.Amount6);
            this.Controls.Add(this.Item6);
            this.Controls.Add(this.Amount5);
            this.Controls.Add(this.Item5);
            this.Controls.Add(this.Amount4);
            this.Controls.Add(this.Item4);
            this.Controls.Add(this.Amount3);
            this.Controls.Add(this.Item3);
            this.Controls.Add(this.Amount2);
            this.Controls.Add(this.Item2);
            this.Controls.Add(this.Amount1);
            this.Controls.Add(this.Item1);
            this.Controls.Add(this.Amount0);
            this.Controls.Add(this.Item0);
            this.Controls.Add(this.DropTableID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(523, 435);
            this.MinimumSize = new System.Drawing.Size(523, 435);
            this.Name = "RandomDropTableEditor";
            this.Text = "Random Drop Table Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RandomDropTableEditor_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DropTableID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount15)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem saveROMToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Health;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Hookshot;
        private ToolStripMenuItem exportBinaryToolStripMenuItem;
        private Label label1;
        private NumericUpDown DropTableID;
        private ComboBox Item0;
        private NumericUpDown Amount0;
        private NumericUpDown Amount1;
        private ComboBox Item1;
        private NumericUpDown Amount2;
        private ComboBox Item2;
        private NumericUpDown Amount3;
        private ComboBox Item3;
        private NumericUpDown Amount4;
        private ComboBox Item4;
        private NumericUpDown Amount5;
        private ComboBox Item5;
        private NumericUpDown Amount6;
        private ComboBox Item6;
        private NumericUpDown Amount7;
        private ComboBox Item7;
        private NumericUpDown Amount8;
        private ComboBox Item8;
        private NumericUpDown Amount9;
        private ComboBox Item9;
        private NumericUpDown Amount10;
        private ComboBox Item10;
        private NumericUpDown Amount11;
        private ComboBox Item11;
        private NumericUpDown Amount12;
        private ComboBox Item12;
        private NumericUpDown Amount13;
        private ComboBox Item13;
        private NumericUpDown Amount14;
        private ComboBox Item14;
        private NumericUpDown Amount15;
        private ComboBox Item15;
        private Label label2;
        private LinkLabel linkLabel1;
        private ToolStripMenuItem exportAsZ64romPatchToolStripMenuItem;
        private ToolStripMenuItem exportAsCHeaderArrayToolStripMenuItem;
        private Label label3;
    }
}