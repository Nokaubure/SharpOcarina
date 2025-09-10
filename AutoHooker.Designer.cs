namespace SharpOcarina
{
    partial class AutoHookerForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.RefreshListButton = new System.Windows.Forms.Button();
            this.HookButton = new System.Windows.Forms.Button();
            this.FunctionNameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FileNameOriginalRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FileNameCustomRadioButton = new System.Windows.Forms.RadioButton();
            this.FileNameFunctionRadioButton = new System.Windows.Forms.RadioButton();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.HookGrid = new System.Windows.Forms.DataGridView();
            this.Function = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LibType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomFileName = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Hooks = new System.Windows.Forms.TabPage();
            this.ViewFileButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Extra = new System.Windows.Forms.TabPage();
            this.VanillaActorListFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.VanillaActorSort = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ExtraConsole = new System.Windows.Forms.TextBox();
            this.ExtraVanillaActorGoButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.VanillaActorList = new System.Windows.Forms.ListBox();
            this.ExtraGoButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CreateActorTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CreateActorButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HookGrid)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.Hooks.SuspendLayout();
            this.Extra.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // RefreshListButton
            // 
            this.RefreshListButton.Location = new System.Drawing.Point(424, 467);
            this.RefreshListButton.Name = "RefreshListButton";
            this.RefreshListButton.Size = new System.Drawing.Size(83, 23);
            this.RefreshListButton.TabIndex = 10;
            this.RefreshListButton.Text = "Refresh List";
            this.RefreshListButton.UseVisualStyleBackColor = true;
            this.RefreshListButton.Click += new System.EventHandler(this.RefreshListButton_Click);
            // 
            // HookButton
            // 
            this.HookButton.Location = new System.Drawing.Point(62, 64);
            this.HookButton.Name = "HookButton";
            this.HookButton.Size = new System.Drawing.Size(83, 23);
            this.HookButton.TabIndex = 11;
            this.HookButton.Text = "Hook!";
            this.HookButton.UseVisualStyleBackColor = true;
            this.HookButton.Click += new System.EventHandler(this.HookButton_Click);
            // 
            // FunctionNameTextbox
            // 
            this.FunctionNameTextbox.Location = new System.Drawing.Point(8, 26);
            this.FunctionNameTextbox.Name = "FunctionNameTextbox";
            this.FunctionNameTextbox.Size = new System.Drawing.Size(332, 20);
            this.FunctionNameTextbox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Function Name:";
            // 
            // FileNameOriginalRadioButton
            // 
            this.FileNameOriginalRadioButton.AutoSize = true;
            this.FileNameOriginalRadioButton.Checked = true;
            this.FileNameOriginalRadioButton.Location = new System.Drawing.Point(6, 19);
            this.FileNameOriginalRadioButton.Name = "FileNameOriginalRadioButton";
            this.FileNameOriginalRadioButton.Size = new System.Drawing.Size(122, 17);
            this.FileNameOriginalRadioButton.TabIndex = 15;
            this.FileNameOriginalRadioButton.TabStop = true;
            this.FileNameOriginalRadioButton.Text = "Use original filename";
            this.FileNameOriginalRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FileNameCustomRadioButton);
            this.groupBox1.Controls.Add(this.FileNameFunctionRadioButton);
            this.groupBox1.Controls.Add(this.FileNameOriginalRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(346, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 91);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // FileNameCustomRadioButton
            // 
            this.FileNameCustomRadioButton.AutoSize = true;
            this.FileNameCustomRadioButton.Location = new System.Drawing.Point(6, 65);
            this.FileNameCustomRadioButton.Name = "FileNameCustomRadioButton";
            this.FileNameCustomRadioButton.Size = new System.Drawing.Size(102, 17);
            this.FileNameCustomRadioButton.TabIndex = 17;
            this.FileNameCustomRadioButton.Text = "Custom filename";
            this.FileNameCustomRadioButton.UseVisualStyleBackColor = true;
            this.FileNameCustomRadioButton.CheckedChanged += new System.EventHandler(this.FileNameCustomRadioButton_CheckedChanged);
            // 
            // FileNameFunctionRadioButton
            // 
            this.FileNameFunctionRadioButton.AutoSize = true;
            this.FileNameFunctionRadioButton.Location = new System.Drawing.Point(6, 42);
            this.FileNameFunctionRadioButton.Name = "FileNameFunctionRadioButton";
            this.FileNameFunctionRadioButton.Size = new System.Drawing.Size(170, 17);
            this.FileNameFunctionRadioButton.TabIndex = 16;
            this.FileNameFunctionRadioButton.Text = "Use function name as filename";
            this.FileNameFunctionRadioButton.UseVisualStyleBackColor = true;
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(211, 467);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(83, 23);
            this.OpenFileButton.TabIndex = 17;
            this.OpenFileButton.Text = "Open File";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // HookGrid
            // 
            this.HookGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HookGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.HookGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HookGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Function,
            this.FileName,
            this.LibType});
            this.HookGrid.Location = new System.Drawing.Point(8, 102);
            this.HookGrid.Name = "HookGrid";
            this.HookGrid.Size = new System.Drawing.Size(714, 359);
            this.HookGrid.TabIndex = 18;
            this.HookGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HookGrid_CellDoubleClick);
            this.HookGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.HookGrid_ColumnHeaderMouseClick);
            // 
            // Function
            // 
            this.Function.DataPropertyName = "Name";
            this.Function.FillWeight = 300F;
            this.Function.HeaderText = "Function";
            this.Function.Name = "Function";
            this.Function.ReadOnly = true;
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.FillWeight = 300F;
            this.FileName.HeaderText = "Filename";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // LibType
            // 
            this.LibType.DataPropertyName = "isLibCode";
            this.LibType.HeaderText = "Lib";
            this.LibType.Name = "LibType";
            // 
            // CustomFileName
            // 
            this.CustomFileName.Enabled = false;
            this.CustomFileName.Location = new System.Drawing.Point(530, 70);
            this.CustomFileName.Name = "CustomFileName";
            this.CustomFileName.Size = new System.Drawing.Size(192, 20);
            this.CustomFileName.TabIndex = 19;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Hooks);
            this.tabControl1.Controls.Add(this.Extra);
            this.tabControl1.Location = new System.Drawing.Point(0, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(739, 538);
            this.tabControl1.TabIndex = 20;
            // 
            // Hooks
            // 
            this.Hooks.Controls.Add(this.ViewFileButton);
            this.Hooks.Controls.Add(this.button2);
            this.Hooks.Controls.Add(this.CustomFileName);
            this.Hooks.Controls.Add(this.FunctionNameTextbox);
            this.Hooks.Controls.Add(this.HookGrid);
            this.Hooks.Controls.Add(this.RefreshListButton);
            this.Hooks.Controls.Add(this.OpenFileButton);
            this.Hooks.Controls.Add(this.HookButton);
            this.Hooks.Controls.Add(this.groupBox1);
            this.Hooks.Controls.Add(this.label1);
            this.Hooks.Location = new System.Drawing.Point(4, 22);
            this.Hooks.Name = "Hooks";
            this.Hooks.Padding = new System.Windows.Forms.Padding(3);
            this.Hooks.Size = new System.Drawing.Size(731, 512);
            this.Hooks.TabIndex = 0;
            this.Hooks.Text = "Hooks";
            this.Hooks.UseVisualStyleBackColor = true;
            // 
            // ViewFileButton
            // 
            this.ViewFileButton.Location = new System.Drawing.Point(211, 64);
            this.ViewFileButton.Name = "ViewFileButton";
            this.ViewFileButton.Size = new System.Drawing.Size(83, 23);
            this.ViewFileButton.TabIndex = 21;
            this.ViewFileButton.Text = "View File";
            this.ViewFileButton.UseVisualStyleBackColor = true;
            this.ViewFileButton.Click += new System.EventHandler(this.ViewFileButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(607, 467);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Extra
            // 
            this.Extra.Controls.Add(this.CreateActorButton);
            this.Extra.Controls.Add(this.CreateActorTextBox);
            this.Extra.Controls.Add(this.label7);
            this.Extra.Controls.Add(this.label6);
            this.Extra.Controls.Add(this.VanillaActorListFilter);
            this.Extra.Controls.Add(this.label5);
            this.Extra.Controls.Add(this.VanillaActorSort);
            this.Extra.Controls.Add(this.button1);
            this.Extra.Controls.Add(this.ExtraConsole);
            this.Extra.Controls.Add(this.ExtraVanillaActorGoButton);
            this.Extra.Controls.Add(this.label4);
            this.Extra.Controls.Add(this.VanillaActorList);
            this.Extra.Controls.Add(this.ExtraGoButton);
            this.Extra.Controls.Add(this.label3);
            this.Extra.Controls.Add(this.textBox1);
            this.Extra.Controls.Add(this.label2);
            this.Extra.Location = new System.Drawing.Point(4, 22);
            this.Extra.Name = "Extra";
            this.Extra.Padding = new System.Windows.Forms.Padding(3);
            this.Extra.Size = new System.Drawing.Size(731, 512);
            this.Extra.TabIndex = 1;
            this.Extra.Text = "Actors&Extra";
            this.Extra.UseVisualStyleBackColor = true;
            // 
            // VanillaActorListFilter
            // 
            this.VanillaActorListFilter.Location = new System.Drawing.Point(48, 37);
            this.VanillaActorListFilter.Name = "VanillaActorListFilter";
            this.VanillaActorListFilter.Size = new System.Drawing.Size(201, 20);
            this.VanillaActorListFilter.TabIndex = 25;
            this.VanillaActorListFilter.TextChanged += new System.EventHandler(this.VanillaActorListFilter_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Filter:";
            // 
            // VanillaActorSort
            // 
            this.VanillaActorSort.AutoSize = true;
            this.VanillaActorSort.Location = new System.Drawing.Point(13, 359);
            this.VanillaActorSort.Name = "VanillaActorSort";
            this.VanillaActorSort.Size = new System.Drawing.Size(88, 17);
            this.VanillaActorSort.TabIndex = 23;
            this.VanillaActorSort.Text = "Sort by name";
            this.VanillaActorSort.UseVisualStyleBackColor = true;
            this.VanillaActorSort.CheckedChanged += new System.EventHandler(this.VanillaActorSort_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(612, 476);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Hook/Copy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // ExtraConsole
            // 
            this.ExtraConsole.BackColor = System.Drawing.SystemColors.MenuText;
            this.ExtraConsole.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExtraConsole.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ExtraConsole.Location = new System.Drawing.Point(625, 57);
            this.ExtraConsole.Multiline = true;
            this.ExtraConsole.Name = "ExtraConsole";
            this.ExtraConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ExtraConsole.Size = new System.Drawing.Size(100, 408);
            this.ExtraConsole.TabIndex = 21;
            this.ExtraConsole.Visible = false;
            // 
            // ExtraVanillaActorGoButton
            // 
            this.ExtraVanillaActorGoButton.Location = new System.Drawing.Point(166, 359);
            this.ExtraVanillaActorGoButton.Name = "ExtraVanillaActorGoButton";
            this.ExtraVanillaActorGoButton.Size = new System.Drawing.Size(83, 23);
            this.ExtraVanillaActorGoButton.TabIndex = 20;
            this.ExtraVanillaActorGoButton.Text = "Copy";
            this.ExtraVanillaActorGoButton.UseVisualStyleBackColor = true;
            this.ExtraVanillaActorGoButton.Click += new System.EventHandler(this.ExtraVanillaActorGoButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(333, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Copy actor to z64rom source folder (takes care of external assets too)";
            // 
            // VanillaActorList
            // 
            this.VanillaActorList.FormattingEnabled = true;
            this.VanillaActorList.HorizontalScrollbar = true;
            this.VanillaActorList.Location = new System.Drawing.Point(11, 66);
            this.VanillaActorList.Name = "VanillaActorList";
            this.VanillaActorList.Size = new System.Drawing.Size(405, 290);
            this.VanillaActorList.TabIndex = 18;
            // 
            // ExtraGoButton
            // 
            this.ExtraGoButton.Location = new System.Drawing.Point(480, 476);
            this.ExtraGoButton.Name = "ExtraGoButton";
            this.ExtraGoButton.Size = new System.Drawing.Size(83, 23);
            this.ExtraGoButton.TabIndex = 17;
            this.ExtraGoButton.Text = "View";
            this.ExtraGoButton.UseVisualStyleBackColor = true;
            this.ExtraGoButton.Visible = false;
            this.ExtraGoButton.Click += new System.EventHandler(this.ExtraGoButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 394);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(408, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Hook all functions and move all actors that contain calls/references to the follo" +
    "wing...";
            this.label3.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(427, 437);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(332, 20);
            this.textBox1.TabIndex = 14;
            this.textBox1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(424, 419);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Function/Structure Name:";
            this.label2.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 413);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Create empty actor";
            // 
            // CreateActorTextBox
            // 
            this.CreateActorTextBox.Location = new System.Drawing.Point(48, 437);
            this.CreateActorTextBox.Name = "CreateActorTextBox";
            this.CreateActorTextBox.Size = new System.Drawing.Size(201, 20);
            this.CreateActorTextBox.TabIndex = 28;
            this.CreateActorTextBox.TextChanged += new System.EventHandler(this.CreateActorTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 440);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Name:";
            // 
            // CreateActorButton
            // 
            this.CreateActorButton.Location = new System.Drawing.Point(98, 463);
            this.CreateActorButton.Name = "CreateActorButton";
            this.CreateActorButton.Size = new System.Drawing.Size(83, 23);
            this.CreateActorButton.TabIndex = 29;
            this.CreateActorButton.Text = "Create";
            this.CreateActorButton.UseVisualStyleBackColor = true;
            this.CreateActorButton.Click += new System.EventHandler(this.CreateActorButton_Click);
            // 
            // AutoHookerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 542);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AutoHookerForm";
            this.Text = "Auto-Hook&Actors";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AutoHookerForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HookGrid)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.Hooks.ResumeLayout(false);
            this.Hooks.PerformLayout();
            this.Extra.ResumeLayout(false);
            this.Extra.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button RefreshListButton;
        private System.Windows.Forms.Button HookButton;
        private System.Windows.Forms.TextBox FunctionNameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton FileNameOriginalRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton FileNameFunctionRadioButton;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.DataGridView HookGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Function;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LibType;
        private System.Windows.Forms.RadioButton FileNameCustomRadioButton;
        private System.Windows.Forms.TextBox CustomFileName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Hooks;
        private System.Windows.Forms.TabPage Extra;
        private System.Windows.Forms.Button ExtraGoButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ExtraVanillaActorGoButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox VanillaActorList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox ExtraConsole;
        private System.Windows.Forms.CheckBox VanillaActorSort;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox VanillaActorListFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ViewFileButton;
        private System.Windows.Forms.Button CreateActorButton;
        private System.Windows.Forms.TextBox CreateActorTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}