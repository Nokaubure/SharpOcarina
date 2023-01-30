namespace SharpOcarina
{
    partial class SettingsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.TabSettings = new System.Windows.Forms.TabControl();
            this.TabVisualSettings = new System.Windows.Forms.TabPage();
            this.HexRotations = new System.Windows.Forms.CheckBox();
            this.DisableTextureWarnings = new System.Windows.Forms.CheckBox();
            this.OnlyRenderWaterboxesGeneral = new System.Windows.Forms.CheckBox();
            this.DrawSelectedCutsceneCommands = new System.Windows.Forms.CheckBox();
            this.DisableRGBA32 = new System.Windows.Forms.CheckBox();
            this.DontConvertMultitexture = new System.Windows.Forms.CheckBox();
            this.ForceRGBATextures = new System.Windows.Forms.CheckBox();
            this.RenderChildLink = new System.Windows.Forms.CheckBox();
            this.RenderActors = new System.Windows.Forms.CheckBox();
            this.ApplyEnvLighting = new System.Windows.Forms.CheckBox();
            this.colorblindaxis = new System.Windows.Forms.CheckBox();
            this.DisplayAxis = new System.Windows.Forms.CheckBox();
            this.ShowRoomModels = new System.Windows.Forms.CheckBox();
            this.ShowCollisionModel = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Zmapoffsetnames = new System.Windows.Forms.CheckBox();
            this.CheckEmptyOffset = new System.Windows.Forms.CheckBox();
            this.ConsecutiveRoomInject = new System.Windows.Forms.CheckBox();
            this.AutoFixErrors = new System.Windows.Forms.CheckBox();
            this.TriplicateCollisionBounds = new System.Windows.Forms.CheckBox();
            this.DListCulling = new System.Windows.Forms.CheckBox();
            this.NoDummyPoints = new System.Windows.Forms.CheckBox();
            this.UpdateCRC = new System.Windows.Forms.CheckBox();
            this.GenerateCustomDMATable = new System.Windows.Forms.CheckBox();
            this.printoffsets = new System.Windows.Forms.CheckBox();
            this.AutoaddObjects = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.AutoSave = new System.Windows.Forms.CheckBox();
            this.Disablewaterboxmovement = new System.Windows.Forms.CheckBox();
            this.Degrees = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.EnableNewExitFormat = new System.Windows.Forms.CheckBox();
            this.command1AOoT = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.IgnoreMMDaySystem = new System.Windows.Forms.CheckBox();
            this.MajorasMask = new System.Windows.Forms.CheckBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.TabSettings.SuspendLayout();
            this.TabVisualSettings.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.TabSettings);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 440);
            this.panel1.TabIndex = 0;
            // 
            // TabSettings
            // 
            this.TabSettings.Controls.Add(this.TabVisualSettings);
            this.TabSettings.Controls.Add(this.tabPage2);
            this.TabSettings.Controls.Add(this.tabPage3);
            this.TabSettings.Controls.Add(this.tabPage1);
            this.TabSettings.Controls.Add(this.tabPage4);
            this.TabSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabSettings.Location = new System.Drawing.Point(0, 0);
            this.TabSettings.Name = "TabSettings";
            this.TabSettings.SelectedIndex = 0;
            this.TabSettings.Size = new System.Drawing.Size(312, 440);
            this.TabSettings.TabIndex = 0;
            // 
            // TabVisualSettings
            // 
            this.TabVisualSettings.Controls.Add(this.HexRotations);
            this.TabVisualSettings.Controls.Add(this.DisableTextureWarnings);
            this.TabVisualSettings.Controls.Add(this.OnlyRenderWaterboxesGeneral);
            this.TabVisualSettings.Controls.Add(this.DrawSelectedCutsceneCommands);
            this.TabVisualSettings.Controls.Add(this.DisableRGBA32);
            this.TabVisualSettings.Controls.Add(this.DontConvertMultitexture);
            this.TabVisualSettings.Controls.Add(this.ForceRGBATextures);
            this.TabVisualSettings.Controls.Add(this.RenderChildLink);
            this.TabVisualSettings.Controls.Add(this.RenderActors);
            this.TabVisualSettings.Controls.Add(this.ApplyEnvLighting);
            this.TabVisualSettings.Controls.Add(this.colorblindaxis);
            this.TabVisualSettings.Controls.Add(this.DisplayAxis);
            this.TabVisualSettings.Controls.Add(this.ShowRoomModels);
            this.TabVisualSettings.Controls.Add(this.ShowCollisionModel);
            this.TabVisualSettings.Location = new System.Drawing.Point(4, 22);
            this.TabVisualSettings.Name = "TabVisualSettings";
            this.TabVisualSettings.Padding = new System.Windows.Forms.Padding(3);
            this.TabVisualSettings.Size = new System.Drawing.Size(304, 414);
            this.TabVisualSettings.TabIndex = 0;
            this.TabVisualSettings.Text = "Visual";
            this.TabVisualSettings.UseVisualStyleBackColor = true;
            // 
            // HexRotations
            // 
            this.HexRotations.AutoSize = true;
            this.HexRotations.Location = new System.Drawing.Point(6, 305);
            this.HexRotations.Name = "HexRotations";
            this.HexRotations.Size = new System.Drawing.Size(156, 17);
            this.HexRotations.TabIndex = 13;
            this.HexRotations.Text = "Show rotation values in hex";
            this.HexRotations.UseVisualStyleBackColor = true;
            this.HexRotations.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // DisableTextureWarnings
            // 
            this.DisableTextureWarnings.AutoSize = true;
            this.DisableTextureWarnings.Location = new System.Drawing.Point(6, 282);
            this.DisableTextureWarnings.Name = "DisableTextureWarnings";
            this.DisableTextureWarnings.Size = new System.Drawing.Size(141, 17);
            this.DisableTextureWarnings.TabIndex = 12;
            this.DisableTextureWarnings.Text = "Disable texture warnings";
            this.DisableTextureWarnings.UseVisualStyleBackColor = true;
            this.DisableTextureWarnings.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // OnlyRenderWaterboxesGeneral
            // 
            this.OnlyRenderWaterboxesGeneral.AutoSize = true;
            this.OnlyRenderWaterboxesGeneral.Location = new System.Drawing.Point(6, 259);
            this.OnlyRenderWaterboxesGeneral.Name = "OnlyRenderWaterboxesGeneral";
            this.OnlyRenderWaterboxesGeneral.Size = new System.Drawing.Size(204, 17);
            this.OnlyRenderWaterboxesGeneral.TabIndex = 11;
            this.OnlyRenderWaterboxesGeneral.Text = "Only render waterboxes in general tab";
            this.OnlyRenderWaterboxesGeneral.UseVisualStyleBackColor = true;
            this.OnlyRenderWaterboxesGeneral.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // DrawSelectedCutsceneCommands
            // 
            this.DrawSelectedCutsceneCommands.AutoSize = true;
            this.DrawSelectedCutsceneCommands.Location = new System.Drawing.Point(6, 236);
            this.DrawSelectedCutsceneCommands.Name = "DrawSelectedCutsceneCommands";
            this.DrawSelectedCutsceneCommands.Size = new System.Drawing.Size(224, 17);
            this.DrawSelectedCutsceneCommands.TabIndex = 10;
            this.DrawSelectedCutsceneCommands.Text = "Only render selected cutscene commands";
            this.DrawSelectedCutsceneCommands.UseVisualStyleBackColor = true;
            this.DrawSelectedCutsceneCommands.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // DisableRGBA32
            // 
            this.DisableRGBA32.AutoSize = true;
            this.DisableRGBA32.Location = new System.Drawing.Point(6, 213);
            this.DisableRGBA32.Name = "DisableRGBA32";
            this.DisableRGBA32.Size = new System.Drawing.Size(153, 17);
            this.DisableRGBA32.TabIndex = 9;
            this.DisableRGBA32.Text = "Disable RGBA32 detection";
            this.DisableRGBA32.UseVisualStyleBackColor = true;
            this.DisableRGBA32.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // DontConvertMultitexture
            // 
            this.DontConvertMultitexture.AutoSize = true;
            this.DontConvertMultitexture.Location = new System.Drawing.Point(6, 190);
            this.DontConvertMultitexture.Name = "DontConvertMultitexture";
            this.DontConvertMultitexture.Size = new System.Drawing.Size(196, 17);
            this.DontConvertMultitexture.TabIndex = 8;
            this.DontConvertMultitexture.Text = "Don\'t convert multitextures to RGBA";
            this.DontConvertMultitexture.UseVisualStyleBackColor = true;
            this.DontConvertMultitexture.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // ForceRGBATextures
            // 
            this.ForceRGBATextures.AutoSize = true;
            this.ForceRGBATextures.Location = new System.Drawing.Point(6, 167);
            this.ForceRGBATextures.Name = "ForceRGBATextures";
            this.ForceRGBATextures.Size = new System.Drawing.Size(130, 17);
            this.ForceRGBATextures.TabIndex = 7;
            this.ForceRGBATextures.Text = "Force RGBA Textures";
            this.ForceRGBATextures.UseVisualStyleBackColor = true;
            this.ForceRGBATextures.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // RenderChildLink
            // 
            this.RenderChildLink.AutoSize = true;
            this.RenderChildLink.Location = new System.Drawing.Point(6, 144);
            this.RenderChildLink.Name = "RenderChildLink";
            this.RenderChildLink.Size = new System.Drawing.Size(209, 17);
            this.RenderChildLink.TabIndex = 6;
            this.RenderChildLink.Text = "Render Child Link instead of Adult Link";
            this.RenderChildLink.UseVisualStyleBackColor = true;
            this.RenderChildLink.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // RenderActors
            // 
            this.RenderActors.AutoSize = true;
            this.RenderActors.Location = new System.Drawing.Point(6, 121);
            this.RenderActors.Name = "RenderActors";
            this.RenderActors.Size = new System.Drawing.Size(164, 17);
            this.RenderActors.TabIndex = 5;
            this.RenderActors.Text = "Render Actors (Most of them)";
            this.RenderActors.UseVisualStyleBackColor = true;
            this.RenderActors.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // ApplyEnvLighting
            // 
            this.ApplyEnvLighting.AutoSize = true;
            this.ApplyEnvLighting.Location = new System.Drawing.Point(6, 98);
            this.ApplyEnvLighting.Name = "ApplyEnvLighting";
            this.ApplyEnvLighting.Size = new System.Drawing.Size(162, 17);
            this.ApplyEnvLighting.TabIndex = 4;
            this.ApplyEnvLighting.Text = "Apply Environmental Lighting";
            this.ApplyEnvLighting.UseVisualStyleBackColor = true;
            this.ApplyEnvLighting.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // colorblindaxis
            // 
            this.colorblindaxis.AutoSize = true;
            this.colorblindaxis.Location = new System.Drawing.Point(6, 75);
            this.colorblindaxis.Name = "colorblindaxis";
            this.colorblindaxis.Size = new System.Drawing.Size(98, 17);
            this.colorblindaxis.TabIndex = 3;
            this.colorblindaxis.Text = "Color Blind Axis";
            this.colorblindaxis.UseVisualStyleBackColor = true;
            this.colorblindaxis.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // DisplayAxis
            // 
            this.DisplayAxis.AutoSize = true;
            this.DisplayAxis.Location = new System.Drawing.Point(6, 52);
            this.DisplayAxis.Name = "DisplayAxis";
            this.DisplayAxis.Size = new System.Drawing.Size(164, 17);
            this.DisplayAxis.TabIndex = 2;
            this.DisplayAxis.Text = "Show Selected Instance Axis";
            this.DisplayAxis.UseVisualStyleBackColor = true;
            this.DisplayAxis.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // ShowRoomModels
            // 
            this.ShowRoomModels.AutoSize = true;
            this.ShowRoomModels.Location = new System.Drawing.Point(6, 29);
            this.ShowRoomModels.Name = "ShowRoomModels";
            this.ShowRoomModels.Size = new System.Drawing.Size(121, 17);
            this.ShowRoomModels.TabIndex = 1;
            this.ShowRoomModels.Text = "Show Room Models";
            this.ShowRoomModels.UseVisualStyleBackColor = true;
            this.ShowRoomModels.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // ShowCollisionModel
            // 
            this.ShowCollisionModel.AutoSize = true;
            this.ShowCollisionModel.Location = new System.Drawing.Point(6, 6);
            this.ShowCollisionModel.Name = "ShowCollisionModel";
            this.ShowCollisionModel.Size = new System.Drawing.Size(126, 17);
            this.ShowCollisionModel.TabIndex = 0;
            this.ShowCollisionModel.Text = "Show Collision Model";
            this.ShowCollisionModel.UseVisualStyleBackColor = true;
            this.ShowCollisionModel.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Zmapoffsetnames);
            this.tabPage2.Controls.Add(this.CheckEmptyOffset);
            this.tabPage2.Controls.Add(this.ConsecutiveRoomInject);
            this.tabPage2.Controls.Add(this.AutoFixErrors);
            this.tabPage2.Controls.Add(this.TriplicateCollisionBounds);
            this.tabPage2.Controls.Add(this.DListCulling);
            this.tabPage2.Controls.Add(this.NoDummyPoints);
            this.tabPage2.Controls.Add(this.UpdateCRC);
            this.tabPage2.Controls.Add(this.GenerateCustomDMATable);
            this.tabPage2.Controls.Add(this.printoffsets);
            this.tabPage2.Controls.Add(this.AutoaddObjects);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(304, 414);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Saving";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Zmapoffsetnames
            // 
            this.Zmapoffsetnames.AutoSize = true;
            this.Zmapoffsetnames.Location = new System.Drawing.Point(6, 236);
            this.Zmapoffsetnames.Name = "Zmapoffsetnames";
            this.Zmapoffsetnames.Size = new System.Drawing.Size(225, 17);
            this.Zmapoffsetnames.TabIndex = 11;
            this.Zmapoffsetnames.Text = "Change binary zmap names to their offsets";
            this.Zmapoffsetnames.UseVisualStyleBackColor = true;
            this.Zmapoffsetnames.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // CheckEmptyOffset
            // 
            this.CheckEmptyOffset.AutoSize = true;
            this.CheckEmptyOffset.Location = new System.Drawing.Point(6, 213);
            this.CheckEmptyOffset.Name = "CheckEmptyOffset";
            this.CheckEmptyOffset.Size = new System.Drawing.Size(210, 17);
            this.CheckEmptyOffset.TabIndex = 10;
            this.CheckEmptyOffset.Text = "Check if offset is empty before injection";
            this.CheckEmptyOffset.UseVisualStyleBackColor = true;
            this.CheckEmptyOffset.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // ConsecutiveRoomInject
            // 
            this.ConsecutiveRoomInject.AutoSize = true;
            this.ConsecutiveRoomInject.Location = new System.Drawing.Point(6, 190);
            this.ConsecutiveRoomInject.Name = "ConsecutiveRoomInject";
            this.ConsecutiveRoomInject.Size = new System.Drawing.Size(153, 17);
            this.ConsecutiveRoomInject.TabIndex = 9;
            this.ConsecutiveRoomInject.Text = "Consecutive room injection";
            this.ConsecutiveRoomInject.UseVisualStyleBackColor = true;
            this.ConsecutiveRoomInject.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // AutoFixErrors
            // 
            this.AutoFixErrors.AutoSize = true;
            this.AutoFixErrors.Location = new System.Drawing.Point(6, 167);
            this.AutoFixErrors.Name = "AutoFixErrors";
            this.AutoFixErrors.Size = new System.Drawing.Size(176, 17);
            this.AutoFixErrors.TabIndex = 8;
            this.AutoFixErrors.Text = "Auto fix common errors on inject";
            this.AutoFixErrors.UseVisualStyleBackColor = true;
            this.AutoFixErrors.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // TriplicateCollisionBounds
            // 
            this.TriplicateCollisionBounds.AutoSize = true;
            this.TriplicateCollisionBounds.Location = new System.Drawing.Point(6, 144);
            this.TriplicateCollisionBounds.Name = "TriplicateCollisionBounds";
            this.TriplicateCollisionBounds.Size = new System.Drawing.Size(147, 17);
            this.TriplicateCollisionBounds.TabIndex = 7;
            this.TriplicateCollisionBounds.Text = "Triplicate collision bounds";
            this.TriplicateCollisionBounds.UseVisualStyleBackColor = true;
            this.TriplicateCollisionBounds.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // DListCulling
            // 
            this.DListCulling.AutoSize = true;
            this.DListCulling.Location = new System.Drawing.Point(6, 121);
            this.DListCulling.Name = "DListCulling";
            this.DListCulling.Size = new System.Drawing.Size(157, 17);
            this.DListCulling.TabIndex = 6;
            this.DListCulling.Text = "Display list culling command";
            this.DListCulling.UseVisualStyleBackColor = true;
            this.DListCulling.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // NoDummyPoints
            // 
            this.NoDummyPoints.AutoSize = true;
            this.NoDummyPoints.Location = new System.Drawing.Point(6, 98);
            this.NoDummyPoints.Name = "NoDummyPoints";
            this.NoDummyPoints.Size = new System.Drawing.Size(246, 17);
            this.NoDummyPoints.TabIndex = 5;
            this.NoDummyPoints.Text = "Remove dummy points from cutscene cameras";
            this.NoDummyPoints.UseVisualStyleBackColor = true;
            this.NoDummyPoints.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // UpdateCRC
            // 
            this.UpdateCRC.AutoSize = true;
            this.UpdateCRC.Location = new System.Drawing.Point(6, 75);
            this.UpdateCRC.Name = "UpdateCRC";
            this.UpdateCRC.Size = new System.Drawing.Size(129, 17);
            this.UpdateCRC.TabIndex = 4;
            this.UpdateCRC.Text = "Update CRC on inject";
            this.UpdateCRC.UseVisualStyleBackColor = true;
            this.UpdateCRC.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // GenerateCustomDMATable
            // 
            this.GenerateCustomDMATable.AutoSize = true;
            this.GenerateCustomDMATable.Location = new System.Drawing.Point(6, 52);
            this.GenerateCustomDMATable.Name = "GenerateCustomDMATable";
            this.GenerateCustomDMATable.Size = new System.Drawing.Size(227, 17);
            this.GenerateCustomDMATable.TabIndex = 3;
            this.GenerateCustomDMATable.Text = "Always generate new DMA Table on inject";
            this.GenerateCustomDMATable.UseVisualStyleBackColor = true;
            this.GenerateCustomDMATable.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // printoffsets
            // 
            this.printoffsets.AutoSize = true;
            this.printoffsets.Location = new System.Drawing.Point(6, 29);
            this.printoffsets.Name = "printoffsets";
            this.printoffsets.Size = new System.Drawing.Size(124, 17);
            this.printoffsets.TabIndex = 2;
            this.printoffsets.Text = "Print offsets on inject";
            this.printoffsets.UseVisualStyleBackColor = true;
            this.printoffsets.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // AutoaddObjects
            // 
            this.AutoaddObjects.AutoSize = true;
            this.AutoaddObjects.Location = new System.Drawing.Point(6, 6);
            this.AutoaddObjects.Name = "AutoaddObjects";
            this.AutoaddObjects.Size = new System.Drawing.Size(164, 17);
            this.AutoaddObjects.TabIndex = 1;
            this.AutoaddObjects.Text = "Add required objects on save";
            this.AutoaddObjects.UseVisualStyleBackColor = true;
            this.AutoaddObjects.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.AutoSave);
            this.tabPage3.Controls.Add(this.Disablewaterboxmovement);
            this.tabPage3.Controls.Add(this.Degrees);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(304, 414);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Behavior";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // AutoSave
            // 
            this.AutoSave.AutoSize = true;
            this.AutoSave.Location = new System.Drawing.Point(6, 52);
            this.AutoSave.Name = "AutoSave";
            this.AutoSave.Size = new System.Drawing.Size(124, 17);
            this.AutoSave.TabIndex = 4;
            this.AutoSave.Text = "Enable autosave.xml";
            this.AutoSave.UseVisualStyleBackColor = true;
            this.AutoSave.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // Disablewaterboxmovement
            // 
            this.Disablewaterboxmovement.AutoSize = true;
            this.Disablewaterboxmovement.Location = new System.Drawing.Point(6, 29);
            this.Disablewaterboxmovement.Name = "Disablewaterboxmovement";
            this.Disablewaterboxmovement.Size = new System.Drawing.Size(193, 17);
            this.Disablewaterboxmovement.TabIndex = 3;
            this.Disablewaterboxmovement.Text = "Disable waterbox mouse movement";
            this.Disablewaterboxmovement.UseVisualStyleBackColor = true;
            this.Disablewaterboxmovement.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // Degrees
            // 
            this.Degrees.AutoSize = true;
            this.Degrees.Location = new System.Drawing.Point(6, 6);
            this.Degrees.Name = "Degrees";
            this.Degrees.Size = new System.Drawing.Size(152, 17);
            this.Degrees.TabIndex = 2;
            this.Degrees.Text = "Multiply degrees by 182.04";
            this.Degrees.UseVisualStyleBackColor = true;
            this.Degrees.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.EnableNewExitFormat);
            this.tabPage1.Controls.Add(this.command1AOoT);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(304, 414);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Custom code";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // EnableNewExitFormat
            // 
            this.EnableNewExitFormat.AutoSize = true;
            this.EnableNewExitFormat.Location = new System.Drawing.Point(6, 29);
            this.EnableNewExitFormat.Name = "EnableNewExitFormat";
            this.EnableNewExitFormat.Size = new System.Drawing.Size(186, 17);
            this.EnableNewExitFormat.TabIndex = 4;
            this.EnableNewExitFormat.Text = "Enable new exit format (Z64ROM)";
            this.EnableNewExitFormat.UseVisualStyleBackColor = true;
            this.EnableNewExitFormat.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // command1AOoT
            // 
            this.command1AOoT.AutoSize = true;
            this.command1AOoT.Location = new System.Drawing.Point(6, 6);
            this.command1AOoT.Name = "command1AOoT";
            this.command1AOoT.Size = new System.Drawing.Size(198, 17);
            this.command1AOoT.TabIndex = 3;
            this.command1AOoT.Text = "Enable advanced texture animations";
            this.command1AOoT.UseVisualStyleBackColor = true;
            this.command1AOoT.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.IgnoreMMDaySystem);
            this.tabPage4.Controls.Add(this.MajorasMask);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(304, 414);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Majora\'s Mask";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // IgnoreMMDaySystem
            // 
            this.IgnoreMMDaySystem.AutoSize = true;
            this.IgnoreMMDaySystem.Location = new System.Drawing.Point(6, 29);
            this.IgnoreMMDaySystem.Name = "IgnoreMMDaySystem";
            this.IgnoreMMDaySystem.Size = new System.Drawing.Size(138, 17);
            this.IgnoreMMDaySystem.TabIndex = 5;
            this.IgnoreMMDaySystem.Text = "Ignore the 3-day system";
            this.IgnoreMMDaySystem.UseVisualStyleBackColor = true;
            this.IgnoreMMDaySystem.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // MajorasMask
            // 
            this.MajorasMask.AutoSize = true;
            this.MajorasMask.Location = new System.Drawing.Point(6, 6);
            this.MajorasMask.Name = "MajorasMask";
            this.MajorasMask.Size = new System.Drawing.Size(123, 17);
            this.MajorasMask.TabIndex = 4;
            this.MajorasMask.Text = "Majora\'s Mask mode";
            this.MajorasMask.UseVisualStyleBackColor = true;
            this.MajorasMask.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Location = new System.Drawing.Point(12, 454);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(312, 34);
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "Save settings";
            this.BtnSave.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 501);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            this.TabSettings.ResumeLayout(false);
            this.TabVisualSettings.ResumeLayout(false);
            this.TabVisualSettings.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl TabSettings;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TabPage TabVisualSettings;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox DisableTextureWarnings;
        private System.Windows.Forms.CheckBox OnlyRenderWaterboxesGeneral;
        private System.Windows.Forms.CheckBox DrawSelectedCutsceneCommands;
        private System.Windows.Forms.CheckBox DisableRGBA32;
        private System.Windows.Forms.CheckBox DontConvertMultitexture;
        private System.Windows.Forms.CheckBox ForceRGBATextures;
        private System.Windows.Forms.CheckBox RenderChildLink;
        private System.Windows.Forms.CheckBox RenderActors;
        private System.Windows.Forms.CheckBox ApplyEnvLighting;
        private System.Windows.Forms.CheckBox colorblindaxis;
        private System.Windows.Forms.CheckBox DisplayAxis;
        private System.Windows.Forms.CheckBox ShowRoomModels;
        private System.Windows.Forms.CheckBox ShowCollisionModel;
        private System.Windows.Forms.CheckBox HexRotations;
        private System.Windows.Forms.CheckBox AutoFixErrors;
        private System.Windows.Forms.CheckBox TriplicateCollisionBounds;
        private System.Windows.Forms.CheckBox DListCulling;
        private System.Windows.Forms.CheckBox NoDummyPoints;
        private System.Windows.Forms.CheckBox UpdateCRC;
        private System.Windows.Forms.CheckBox GenerateCustomDMATable;
        private System.Windows.Forms.CheckBox printoffsets;
        private System.Windows.Forms.CheckBox AutoaddObjects;
        private System.Windows.Forms.CheckBox AutoSave;
        private System.Windows.Forms.CheckBox Disablewaterboxmovement;
        private System.Windows.Forms.CheckBox Degrees;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox IgnoreMMDaySystem;
        private System.Windows.Forms.CheckBox MajorasMask;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox EnableNewExitFormat;
        private System.Windows.Forms.CheckBox command1AOoT;
        private System.Windows.Forms.CheckBox CheckEmptyOffset;
        private System.Windows.Forms.CheckBox ConsecutiveRoomInject;
        private System.Windows.Forms.CheckBox Zmapoffsetnames;
    }
}