namespace SharpOcarina
{
    partial class CustomCombiner
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
            this.Ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.C1A = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.C1B = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.C1C = new System.Windows.Forms.ComboBox();
            this.C1D = new System.Windows.Forms.ComboBox();
            this.C2D = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.C2C = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.C2B = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.C2A = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.A1D = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.A1C = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.A1B = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.A1A = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.A2D = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.A2C = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.A2B = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.A2A = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.CompiledCombiner = new SharpOcarina.NumericTextBox();
            this.PreviewCheckbox = new System.Windows.Forms.CheckBox();
            this.labelFC = new System.Windows.Forms.Label();
            this.DefaultButton = new System.Windows.Forms.Button();
            this.labelE2 = new System.Windows.Forms.Label();
            this.CompiledE2 = new SharpOcarina.NumericTextBox();
            this.labelD9 = new System.Windows.Forms.Label();
            this.CompiledD9 = new SharpOcarina.NumericTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.FC = new System.Windows.Forms.TabPage();
            this.E2 = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.D9 = new System.Windows.Forms.TabPage();
            this.G_CLIPPING = new System.Windows.Forms.CheckBox();
            this.G_SHADING_SMOOTH = new System.Windows.Forms.CheckBox();
            this.G_TEXTURE_GEN_LINEAR = new System.Windows.Forms.CheckBox();
            this.G_TEXTURE_GEN = new System.Windows.Forms.CheckBox();
            this.G_LIGHTING = new System.Windows.Forms.CheckBox();
            this.G_FOG = new System.Windows.Forms.CheckBox();
            this.G_CULL_BACK = new System.Windows.Forms.CheckBox();
            this.G_CULL_FRONT = new System.Windows.Forms.CheckBox();
            this.G_SHADE = new System.Windows.Forms.CheckBox();
            this.G_ZBUFFER = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.FC.SuspendLayout();
            this.E2.SuspendLayout();
            this.D9.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(183, 254);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "Close";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Color 1st cycle";
            // 
            // C1A
            // 
            this.C1A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C1A.FormattingEnabled = true;
            this.C1A.Location = new System.Drawing.Point(6, 21);
            this.C1A.Name = "C1A";
            this.C1A.Size = new System.Drawing.Size(109, 21);
            this.C1A.TabIndex = 2;
            this.C1A.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(256, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "*";
            // 
            // C1B
            // 
            this.C1B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C1B.FormattingEnabled = true;
            this.C1B.Location = new System.Drawing.Point(141, 21);
            this.C1B.Name = "C1B";
            this.C1B.Size = new System.Drawing.Size(109, 21);
            this.C1B.TabIndex = 4;
            this.C1B.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(389, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "+";
            // 
            // C1C
            // 
            this.C1C.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C1C.FormattingEnabled = true;
            this.C1C.Location = new System.Drawing.Point(275, 21);
            this.C1C.Name = "C1C";
            this.C1C.Size = new System.Drawing.Size(109, 21);
            this.C1C.TabIndex = 6;
            this.C1C.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // C1D
            // 
            this.C1D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C1D.FormattingEnabled = true;
            this.C1D.Location = new System.Drawing.Point(410, 21);
            this.C1D.Name = "C1D";
            this.C1D.Size = new System.Drawing.Size(109, 21);
            this.C1D.TabIndex = 8;
            this.C1D.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // C2D
            // 
            this.C2D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C2D.FormattingEnabled = true;
            this.C2D.Location = new System.Drawing.Point(410, 64);
            this.C2D.Name = "C2D";
            this.C2D.Size = new System.Drawing.Size(109, 21);
            this.C2D.TabIndex = 16;
            this.C2D.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(389, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "+";
            // 
            // C2C
            // 
            this.C2C.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C2C.FormattingEnabled = true;
            this.C2C.Location = new System.Drawing.Point(275, 64);
            this.C2C.Name = "C2C";
            this.C2C.Size = new System.Drawing.Size(109, 21);
            this.C2C.TabIndex = 14;
            this.C2C.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(256, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "*";
            // 
            // C2B
            // 
            this.C2B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C2B.FormattingEnabled = true;
            this.C2B.Location = new System.Drawing.Point(141, 64);
            this.C2B.Name = "C2B";
            this.C2B.Size = new System.Drawing.Size(109, 21);
            this.C2B.TabIndex = 12;
            this.C2B.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(122, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "-";
            // 
            // C2A
            // 
            this.C2A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C2A.FormattingEnabled = true;
            this.C2A.Location = new System.Drawing.Point(6, 64);
            this.C2A.Name = "C2A";
            this.C2A.Size = new System.Drawing.Size(109, 21);
            this.C2A.TabIndex = 10;
            this.C2A.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Color 2nd cycle";
            // 
            // A1D
            // 
            this.A1D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.A1D.FormattingEnabled = true;
            this.A1D.Location = new System.Drawing.Point(410, 108);
            this.A1D.Name = "A1D";
            this.A1D.Size = new System.Drawing.Size(109, 21);
            this.A1D.TabIndex = 24;
            this.A1D.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(389, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 20);
            this.label9.TabIndex = 23;
            this.label9.Text = "+";
            // 
            // A1C
            // 
            this.A1C.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.A1C.FormattingEnabled = true;
            this.A1C.Location = new System.Drawing.Point(275, 108);
            this.A1C.Name = "A1C";
            this.A1C.Size = new System.Drawing.Size(109, 21);
            this.A1C.TabIndex = 22;
            this.A1C.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(256, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 20);
            this.label10.TabIndex = 21;
            this.label10.Text = "*";
            // 
            // A1B
            // 
            this.A1B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.A1B.FormattingEnabled = true;
            this.A1B.Location = new System.Drawing.Point(141, 108);
            this.A1B.Name = "A1B";
            this.A1B.Size = new System.Drawing.Size(109, 21);
            this.A1B.TabIndex = 20;
            this.A1B.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(122, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 20);
            this.label11.TabIndex = 19;
            this.label11.Text = "-";
            // 
            // A1A
            // 
            this.A1A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.A1A.FormattingEnabled = true;
            this.A1A.Location = new System.Drawing.Point(6, 108);
            this.A1A.Name = "A1A";
            this.A1A.Size = new System.Drawing.Size(109, 21);
            this.A1A.TabIndex = 18;
            this.A1A.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Alpha 1st cycle";
            // 
            // A2D
            // 
            this.A2D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.A2D.FormattingEnabled = true;
            this.A2D.Location = new System.Drawing.Point(410, 153);
            this.A2D.Name = "A2D";
            this.A2D.Size = new System.Drawing.Size(109, 21);
            this.A2D.TabIndex = 32;
            this.A2D.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(389, 153);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 20);
            this.label13.TabIndex = 31;
            this.label13.Text = "+";
            // 
            // A2C
            // 
            this.A2C.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.A2C.FormattingEnabled = true;
            this.A2C.Location = new System.Drawing.Point(275, 153);
            this.A2C.Name = "A2C";
            this.A2C.Size = new System.Drawing.Size(109, 21);
            this.A2C.TabIndex = 30;
            this.A2C.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(256, 157);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 20);
            this.label14.TabIndex = 29;
            this.label14.Text = "*";
            // 
            // A2B
            // 
            this.A2B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.A2B.FormattingEnabled = true;
            this.A2B.Location = new System.Drawing.Point(141, 153);
            this.A2B.Name = "A2B";
            this.A2B.Size = new System.Drawing.Size(109, 21);
            this.A2B.TabIndex = 28;
            this.A2B.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(122, 152);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 20);
            this.label15.TabIndex = 27;
            this.label15.Text = "-";
            // 
            // A2A
            // 
            this.A2A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.A2A.FormattingEnabled = true;
            this.A2A.Location = new System.Drawing.Point(6, 153);
            this.A2A.Name = "A2A";
            this.A2A.Size = new System.Drawing.Size(109, 21);
            this.A2A.TabIndex = 26;
            this.A2A.SelectionChangeCommitted += new System.EventHandler(this.CombinerComboBox_SelectionChangeCommitted);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 137);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 13);
            this.label16.TabIndex = 25;
            this.label16.Text = "Alpha 2nd cycle";
            // 
            // CompiledCombiner
            // 
            this.CompiledCombiner.AllowHex = true;
            this.CompiledCombiner.Digits = 14;
            this.CompiledCombiner.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompiledCombiner.Location = new System.Drawing.Point(414, 189);
            this.CompiledCombiner.MaxLength = 255;
            this.CompiledCombiner.Name = "CompiledCombiner";
            this.CompiledCombiner.Size = new System.Drawing.Size(105, 20);
            this.CompiledCombiner.TabIndex = 42;
            this.CompiledCombiner.Text = "00000000000000";
            this.CompiledCombiner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CompiledCombiner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CompiledCombiner_KeyDown);
            this.CompiledCombiner.Leave += new System.EventHandler(this.CompiledCombiner_Leave);
            // 
            // PreviewCheckbox
            // 
            this.PreviewCheckbox.AutoSize = true;
            this.PreviewCheckbox.Checked = true;
            this.PreviewCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PreviewCheckbox.Location = new System.Drawing.Point(426, 258);
            this.PreviewCheckbox.Name = "PreviewCheckbox";
            this.PreviewCheckbox.Size = new System.Drawing.Size(118, 17);
            this.PreviewCheckbox.TabIndex = 43;
            this.PreviewCheckbox.Text = "Preview on change";
            this.PreviewCheckbox.UseVisualStyleBackColor = true;
            this.PreviewCheckbox.CheckedChanged += new System.EventHandler(this.PreviewCheckbox_CheckedChanged);
            // 
            // labelFC
            // 
            this.labelFC.AutoSize = true;
            this.labelFC.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFC.Location = new System.Drawing.Point(393, 192);
            this.labelFC.Name = "labelFC";
            this.labelFC.Size = new System.Drawing.Size(21, 14);
            this.labelFC.TabIndex = 44;
            this.labelFC.Text = "FC";
            // 
            // DefaultButton
            // 
            this.DefaultButton.Location = new System.Drawing.Point(273, 254);
            this.DefaultButton.Name = "DefaultButton";
            this.DefaultButton.Size = new System.Drawing.Size(109, 23);
            this.DefaultButton.TabIndex = 45;
            this.DefaultButton.Text = "Sample Commands";
            this.DefaultButton.UseVisualStyleBackColor = true;
            this.DefaultButton.Click += new System.EventHandler(this.DefaultButton_Click);
            // 
            // labelE2
            // 
            this.labelE2.AutoSize = true;
            this.labelE2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelE2.Location = new System.Drawing.Point(393, 192);
            this.labelE2.Name = "labelE2";
            this.labelE2.Size = new System.Drawing.Size(21, 14);
            this.labelE2.TabIndex = 47;
            this.labelE2.Text = "E2";
            // 
            // CompiledE2
            // 
            this.CompiledE2.AllowHex = true;
            this.CompiledE2.Digits = 14;
            this.CompiledE2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompiledE2.Location = new System.Drawing.Point(414, 189);
            this.CompiledE2.MaxLength = 255;
            this.CompiledE2.Name = "CompiledE2";
            this.CompiledE2.Size = new System.Drawing.Size(105, 20);
            this.CompiledE2.TabIndex = 46;
            this.CompiledE2.Text = "00000000000000";
            this.CompiledE2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CompiledE2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CompiledE2_KeyDown);
            this.CompiledE2.Leave += new System.EventHandler(this.CompiledE2_Leave);
            // 
            // labelD9
            // 
            this.labelD9.AutoSize = true;
            this.labelD9.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelD9.Location = new System.Drawing.Point(393, 192);
            this.labelD9.Name = "labelD9";
            this.labelD9.Size = new System.Drawing.Size(21, 14);
            this.labelD9.TabIndex = 49;
            this.labelD9.Text = "D9";
            // 
            // CompiledD9
            // 
            this.CompiledD9.AllowHex = true;
            this.CompiledD9.Digits = 14;
            this.CompiledD9.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompiledD9.Location = new System.Drawing.Point(414, 189);
            this.CompiledD9.MaxLength = 255;
            this.CompiledD9.Name = "CompiledD9";
            this.CompiledD9.Size = new System.Drawing.Size(105, 20);
            this.CompiledD9.TabIndex = 48;
            this.CompiledD9.Text = "00000000000000";
            this.CompiledD9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CompiledD9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CompiledD9_KeyDown);
            this.CompiledD9.Leave += new System.EventHandler(this.CompiledD9_Leave);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.FC);
            this.tabControl1.Controls.Add(this.E2);
            this.tabControl1.Controls.Add(this.D9);
            this.tabControl1.Location = new System.Drawing.Point(9, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(535, 248);
            this.tabControl1.TabIndex = 50;
            // 
            // FC
            // 
            this.FC.Controls.Add(this.C1A);
            this.FC.Controls.Add(this.label1);
            this.FC.Controls.Add(this.label2);
            this.FC.Controls.Add(this.C1B);
            this.FC.Controls.Add(this.label3);
            this.FC.Controls.Add(this.C1C);
            this.FC.Controls.Add(this.labelFC);
            this.FC.Controls.Add(this.label4);
            this.FC.Controls.Add(this.C1D);
            this.FC.Controls.Add(this.CompiledCombiner);
            this.FC.Controls.Add(this.label8);
            this.FC.Controls.Add(this.A2D);
            this.FC.Controls.Add(this.C2A);
            this.FC.Controls.Add(this.label13);
            this.FC.Controls.Add(this.label7);
            this.FC.Controls.Add(this.A2C);
            this.FC.Controls.Add(this.C2B);
            this.FC.Controls.Add(this.label14);
            this.FC.Controls.Add(this.label6);
            this.FC.Controls.Add(this.A2B);
            this.FC.Controls.Add(this.C2C);
            this.FC.Controls.Add(this.label15);
            this.FC.Controls.Add(this.label5);
            this.FC.Controls.Add(this.A2A);
            this.FC.Controls.Add(this.C2D);
            this.FC.Controls.Add(this.label16);
            this.FC.Controls.Add(this.label12);
            this.FC.Controls.Add(this.A1D);
            this.FC.Controls.Add(this.A1A);
            this.FC.Controls.Add(this.label9);
            this.FC.Controls.Add(this.label11);
            this.FC.Controls.Add(this.A1C);
            this.FC.Controls.Add(this.A1B);
            this.FC.Controls.Add(this.label10);
            this.FC.Location = new System.Drawing.Point(4, 22);
            this.FC.Name = "FC";
            this.FC.Padding = new System.Windows.Forms.Padding(3);
            this.FC.Size = new System.Drawing.Size(527, 222);
            this.FC.TabIndex = 0;
            this.FC.Text = "FC SetCombine";
            this.FC.UseVisualStyleBackColor = true;
            // 
            // E2
            // 
            this.E2.Controls.Add(this.label17);
            this.E2.Controls.Add(this.CompiledE2);
            this.E2.Controls.Add(this.labelE2);
            this.E2.Location = new System.Drawing.Point(4, 22);
            this.E2.Name = "E2";
            this.E2.Padding = new System.Windows.Forms.Padding(3);
            this.E2.Size = new System.Drawing.Size(527, 222);
            this.E2.TabIndex = 1;
            this.E2.Text = "E2 SetOtherMode_L";
            this.E2.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 13);
            this.label17.TabIndex = 48;
            this.label17.Text = "Coming soon...";
            // 
            // D9
            // 
            this.D9.Controls.Add(this.G_CLIPPING);
            this.D9.Controls.Add(this.G_SHADING_SMOOTH);
            this.D9.Controls.Add(this.G_TEXTURE_GEN_LINEAR);
            this.D9.Controls.Add(this.G_TEXTURE_GEN);
            this.D9.Controls.Add(this.G_LIGHTING);
            this.D9.Controls.Add(this.G_FOG);
            this.D9.Controls.Add(this.G_CULL_BACK);
            this.D9.Controls.Add(this.G_CULL_FRONT);
            this.D9.Controls.Add(this.G_SHADE);
            this.D9.Controls.Add(this.G_ZBUFFER);
            this.D9.Controls.Add(this.labelD9);
            this.D9.Controls.Add(this.CompiledD9);
            this.D9.Location = new System.Drawing.Point(4, 22);
            this.D9.Name = "D9";
            this.D9.Size = new System.Drawing.Size(527, 222);
            this.D9.TabIndex = 2;
            this.D9.Text = "D9 GeometryMode";
            this.D9.UseVisualStyleBackColor = true;
            // 
            // G_CLIPPING
            // 
            this.G_CLIPPING.AutoSize = true;
            this.G_CLIPPING.Location = new System.Drawing.Point(131, 59);
            this.G_CLIPPING.Name = "G_CLIPPING";
            this.G_CLIPPING.Size = new System.Drawing.Size(89, 17);
            this.G_CLIPPING.TabIndex = 59;
            this.G_CLIPPING.Text = "G_CLIPPING";
            this.toolTip1.SetToolTip(this.G_CLIPPING, "In F3DLX2, can disable clipping calculations, and is on by default. Ignored by F3" +
        "DEX2, clipping is always on.");
            this.G_CLIPPING.UseVisualStyleBackColor = true;
            this.G_CLIPPING.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // G_SHADING_SMOOTH
            // 
            this.G_SHADING_SMOOTH.AutoSize = true;
            this.G_SHADING_SMOOTH.Location = new System.Drawing.Point(131, 36);
            this.G_SHADING_SMOOTH.Name = "G_SHADING_SMOOTH";
            this.G_SHADING_SMOOTH.Size = new System.Drawing.Size(142, 17);
            this.G_SHADING_SMOOTH.TabIndex = 58;
            this.G_SHADING_SMOOTH.Text = "G_SHADING_SMOOTH";
            this.toolTip1.SetToolTip(this.G_SHADING_SMOOTH, "Enables smooth (Gouraud) shading of primitives. Needs G_SHADE enabled to work.");
            this.G_SHADING_SMOOTH.UseVisualStyleBackColor = true;
            this.G_SHADING_SMOOTH.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // G_TEXTURE_GEN_LINEAR
            // 
            this.G_TEXTURE_GEN_LINEAR.AutoSize = true;
            this.G_TEXTURE_GEN_LINEAR.Location = new System.Drawing.Point(131, 13);
            this.G_TEXTURE_GEN_LINEAR.Name = "G_TEXTURE_GEN_LINEAR";
            this.G_TEXTURE_GEN_LINEAR.Size = new System.Drawing.Size(165, 17);
            this.G_TEXTURE_GEN_LINEAR.TabIndex = 57;
            this.G_TEXTURE_GEN_LINEAR.Text = "G_TEXTURE_GEN_LINEAR";
            this.toolTip1.SetToolTip(this.G_TEXTURE_GEN_LINEAR, "Generates linear texture coordinates, based on acos() of X and Y components of th" +
        "e normal, after projection transformation");
            this.G_TEXTURE_GEN_LINEAR.UseVisualStyleBackColor = true;
            this.G_TEXTURE_GEN_LINEAR.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // G_TEXTURE_GEN
            // 
            this.G_TEXTURE_GEN.AutoSize = true;
            this.G_TEXTURE_GEN.Location = new System.Drawing.Point(11, 151);
            this.G_TEXTURE_GEN.Name = "G_TEXTURE_GEN";
            this.G_TEXTURE_GEN.Size = new System.Drawing.Size(120, 17);
            this.G_TEXTURE_GEN.TabIndex = 56;
            this.G_TEXTURE_GEN.Text = "G_TEXTURE_GEN";
            this.toolTip1.SetToolTip(this.G_TEXTURE_GEN, "Generates spherical texture coordinates, based on X and Y components of the proje" +
        "cted version of the normal");
            this.G_TEXTURE_GEN.UseVisualStyleBackColor = true;
            this.G_TEXTURE_GEN.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // G_LIGHTING
            // 
            this.G_LIGHTING.AutoSize = true;
            this.G_LIGHTING.Location = new System.Drawing.Point(11, 128);
            this.G_LIGHTING.Name = "G_LIGHTING";
            this.G_LIGHTING.Size = new System.Drawing.Size(91, 17);
            this.G_LIGHTING.TabIndex = 55;
            this.G_LIGHTING.Text = "G_LIGHTING";
            this.toolTip1.SetToolTip(this.G_LIGHTING, "Enables lighting calculations to determine vertex color (determines how the color" +
        "/normal part of loaded vertices are interpreted)");
            this.G_LIGHTING.UseVisualStyleBackColor = true;
            this.G_LIGHTING.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // G_FOG
            // 
            this.G_FOG.AutoSize = true;
            this.G_FOG.Location = new System.Drawing.Point(11, 105);
            this.G_FOG.Name = "G_FOG";
            this.G_FOG.Size = new System.Drawing.Size(62, 17);
            this.G_FOG.TabIndex = 54;
            this.G_FOG.Text = "G_FOG";
            this.toolTip1.SetToolTip(this.G_FOG, "Calculates alpha value of primitives for fog effects");
            this.G_FOG.UseVisualStyleBackColor = true;
            this.G_FOG.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // G_CULL_BACK
            // 
            this.G_CULL_BACK.AutoSize = true;
            this.G_CULL_BACK.Location = new System.Drawing.Point(11, 82);
            this.G_CULL_BACK.Name = "G_CULL_BACK";
            this.G_CULL_BACK.Size = new System.Drawing.Size(101, 17);
            this.G_CULL_BACK.TabIndex = 53;
            this.G_CULL_BACK.Text = "G_CULL_BACK";
            this.toolTip1.SetToolTip(this.G_CULL_BACK, "Back-face culling");
            this.G_CULL_BACK.UseVisualStyleBackColor = true;
            this.G_CULL_BACK.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // G_CULL_FRONT
            // 
            this.G_CULL_FRONT.AutoSize = true;
            this.G_CULL_FRONT.Location = new System.Drawing.Point(11, 59);
            this.G_CULL_FRONT.Name = "G_CULL_FRONT";
            this.G_CULL_FRONT.Size = new System.Drawing.Size(110, 17);
            this.G_CULL_FRONT.TabIndex = 52;
            this.G_CULL_FRONT.Text = "G_CULL_FRONT";
            this.toolTip1.SetToolTip(this.G_CULL_FRONT, "Front-face culling");
            this.G_CULL_FRONT.UseVisualStyleBackColor = true;
            this.G_CULL_FRONT.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // G_SHADE
            // 
            this.G_SHADE.AutoSize = true;
            this.G_SHADE.Location = new System.Drawing.Point(11, 36);
            this.G_SHADE.Name = "G_SHADE";
            this.G_SHADE.Size = new System.Drawing.Size(77, 17);
            this.G_SHADE.TabIndex = 51;
            this.G_SHADE.Text = "G_SHADE";
            this.toolTip1.SetToolTip(this.G_SHADE, "Enables calculating vertex color for triangle. Uses flat shading without G_SHADIN" +
        "G_SMOOTH.");
            this.G_SHADE.UseVisualStyleBackColor = true;
            this.G_SHADE.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // G_ZBUFFER
            // 
            this.G_ZBUFFER.AutoSize = true;
            this.G_ZBUFFER.Location = new System.Drawing.Point(11, 13);
            this.G_ZBUFFER.Name = "G_ZBUFFER";
            this.G_ZBUFFER.Size = new System.Drawing.Size(89, 17);
            this.G_ZBUFFER.TabIndex = 50;
            this.G_ZBUFFER.Text = "G_ZBUFFER";
            this.toolTip1.SetToolTip(this.G_ZBUFFER, "Enables depth (Z buffer) calculations in the RSP");
            this.G_ZBUFFER.UseVisualStyleBackColor = true;
            this.G_ZBUFFER.CheckedChanged += new System.EventHandler(this.GeometryMode_CheckedChange);
            // 
            // CustomCombiner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 289);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.DefaultButton);
            this.Controls.Add(this.PreviewCheckbox);
            this.Controls.Add(this.Ok);
            this.Name = "CustomCombiner";
            this.Text = "Custom combiner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CustomCombiner_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.FC.ResumeLayout(false);
            this.FC.PerformLayout();
            this.E2.ResumeLayout(false);
            this.E2.PerformLayout();
            this.D9.ResumeLayout(false);
            this.D9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox C1A;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox C1B;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox C1C;
        private System.Windows.Forms.ComboBox C1D;
        private System.Windows.Forms.ComboBox C2D;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox C2C;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox C2B;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox C2A;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox A1D;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox A1C;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox A1B;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox A1A;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox A2D;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox A2C;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox A2B;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox A2A;
        private System.Windows.Forms.Label label16;
        private NumericTextBox CompiledCombiner;
        private System.Windows.Forms.CheckBox PreviewCheckbox;
        private System.Windows.Forms.Label labelFC;
        private System.Windows.Forms.Button DefaultButton;
        private System.Windows.Forms.Label labelE2;
        private NumericTextBox CompiledE2;
        private System.Windows.Forms.Label labelD9;
        private NumericTextBox CompiledD9;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage FC;
        private System.Windows.Forms.TabPage E2;
        private System.Windows.Forms.TabPage D9;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox G_CLIPPING;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox G_SHADING_SMOOTH;
        private System.Windows.Forms.CheckBox G_TEXTURE_GEN_LINEAR;
        private System.Windows.Forms.CheckBox G_TEXTURE_GEN;
        private System.Windows.Forms.CheckBox G_LIGHTING;
        private System.Windows.Forms.CheckBox G_FOG;
        private System.Windows.Forms.CheckBox G_CULL_BACK;
        private System.Windows.Forms.CheckBox G_CULL_FRONT;
        private System.Windows.Forms.CheckBox G_SHADE;
        private System.Windows.Forms.CheckBox G_ZBUFFER;
    }
}