using System.Windows.Forms;

namespace SharpOcarina
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.glControl1 = new OpenTK.GLControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveScenetoolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.OpenGlobalROM = new System.Windows.Forms.ToolStripMenuItem();
            this.injectToROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LaunchRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildAndLaunchZ64romToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildAndLaunchZ64romWarpToSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.NewZ64romProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.openZmapToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenSceneFromRoomToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.AdvancedTextureAnimationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnableNexExitFormatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoReload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.majorasMaskModeexperimentalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IgnoreMajorasMaskDaySystem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.showCollisionModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRoomModelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayAxisMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorBlindMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyEnvironmentLightingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenderActorstoolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.RenderChildLinkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceRGBATexturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dontConvertMultitextureToRGBAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisableRGBA32ToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.RenderSelectedCutsceneCommandsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenderWaterboxesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisableTextureWarningsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisableCutscenePreviewBlackBarsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetGroupSettingsReloadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.consecutiveRoomInjectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoaddGroupsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printOffsetsOnInjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateCRCMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckEmptyOffsetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZmapOffsetNames = new System.Windows.Forms.ToolStripMenuItem();
            this.noDummyPointsInCutsceneCamerasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dListCullingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triplicateCollisionBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoFixErrorsStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.DegreesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableWaterboxMouseMovementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRotationValuesAsHexadecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autosaveSceneXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpPixelatedTexturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showReadmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iGotACrashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnemyTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.puzzleTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EasterEggToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createPathwaysForEachBoundingBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCutsceneRawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCutsceneRawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAszobjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bank0x06ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bank0x05ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bank0x04ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeCollisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importEnvironmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importActorsAndObjectsOfZmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCollisionFromzsceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCamerasAndWaterboxFromzsceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTransitionsAndSpawnsFromzsceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPathwaysFromzsceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importActorCutscenesFromzsceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEmptySpaceInSceneHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displaySwitchFlagsUsedByAllRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddObjectToAllRoomsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReloadXMLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DefaultEnvironmentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HWWindMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyFirstRoomSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setRoomsToUseEnvironment1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoplaceDoorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllGroupSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nokaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.additionalLightsFixOoTDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedTextureAnimationsOoTDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtendDynapolyCountStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entranceTableEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutsceneTableEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileCreationEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseScreenMapEditorOoTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dropTableEditorOoTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restrictionFlagsTableEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSceneDmatableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllRomScenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebuildDmaTableallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decompressROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectTableEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceSceneTitleCardTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.SetTitlecard = new System.Windows.Forms.Button();
            this.SetRestrictionFlags = new System.Windows.Forms.Button();
            this.AutoInjectOffsetCheckBox = new System.Windows.Forms.CheckBox();
            this.ScenenumberTextbox = new SharpOcarina.NumericUpDownEx();
            this.CamerasGroupBox = new System.Windows.Forms.GroupBox();
            this.CameraPanel = new System.Windows.Forms.Panel();
            this.CameraPage2 = new System.Windows.Forms.Button();
            this.CameraCopyViewport = new System.Windows.Forms.Button();
            this.CameraView = new System.Windows.Forms.Button();
            this.label104 = new System.Windows.Forms.Label();
            this.CameraUnk1 = new SharpOcarina.NumericUpDownEx();
            this.label103 = new System.Windows.Forms.Label();
            this.CameraUnk2 = new SharpOcarina.NumericUpDownEx();
            this.label78 = new System.Windows.Forms.Label();
            this.CameraFov = new SharpOcarina.NumericUpDownEx();
            this.CameraType = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.CameraZRot = new SharpOcarina.NumericUpDownEx();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.CameraZPos = new SharpOcarina.NumericUpDownEx();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.CameraXRot = new SharpOcarina.NumericUpDownEx();
            this.label75 = new System.Windows.Forms.Label();
            this.CameraYPos = new SharpOcarina.NumericUpDownEx();
            this.CameraYRot = new SharpOcarina.NumericUpDownEx();
            this.label76 = new System.Windows.Forms.Label();
            this.CameraXPos = new SharpOcarina.NumericUpDownEx();
            this.DeleteCameraButton = new System.Windows.Forms.Button();
            this.AddCameraButton = new System.Windows.Forms.Button();
            this.CameraSelect = new System.Windows.Forms.NumericUpDown();
            this.niceLine8 = new SharpOcarina.NiceLine();
            this.CameraPanel2 = new System.Windows.Forms.Panel();
            this.label121 = new System.Windows.Forms.Label();
            this.CameraUnk22 = new SharpOcarina.NumericUpDownEx();
            this.CameraUnk1E = new SharpOcarina.NumericUpDownEx();
            this.label122 = new System.Windows.Forms.Label();
            this.CameraUnk20 = new SharpOcarina.NumericUpDownEx();
            this.label123 = new System.Windows.Forms.Label();
            this.CameraPage1 = new System.Windows.Forms.Button();
            this.label124 = new System.Windows.Forms.Label();
            this.CameraUnk1C = new SharpOcarina.NumericUpDownEx();
            this.label126 = new System.Windows.Forms.Label();
            this.CameraUnk16 = new SharpOcarina.NumericUpDownEx();
            this.label127 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.CameraUnk18 = new SharpOcarina.NumericUpDownEx();
            this.label129 = new System.Windows.Forms.Label();
            this.CameraUnk14 = new SharpOcarina.NumericUpDownEx();
            this.CameraUnk1A = new SharpOcarina.NumericUpDownEx();
            this.label130 = new System.Windows.Forms.Label();
            this.CameraUnk12 = new SharpOcarina.NumericUpDownEx();
            this.SceneSettingsComboBox = new System.Windows.Forms.ComboBox();
            this.SceneFunctionLabel = new System.Windows.Forms.Label();
            this.ElfMessageComboBox = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.SpecialObjectComboBox = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.SimulateN64CheckBox = new System.Windows.Forms.CheckBox();
            this.label33 = new System.Windows.Forms.Label();
            this.WaterboxGroupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.WaterboxRoom = new SharpOcarina.NumericUpDownEx();
            this.WaterboxCam = new SharpOcarina.NumericUpDownEx();
            this.label94 = new System.Windows.Forms.Label();
            this.WaterboxEnv = new SharpOcarina.NumericUpDownEx();
            this.WaterboxRoomLabel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.WaterboxZPos = new SharpOcarina.NumericUpDownEx();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.WaterboxXSize = new SharpOcarina.NumericUpDownEx();
            this.label18 = new System.Windows.Forms.Label();
            this.WaterboxYPos = new SharpOcarina.NumericUpDownEx();
            this.WaterboxYSize = new SharpOcarina.NumericUpDownEx();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.WaterboxXPos = new SharpOcarina.NumericUpDownEx();
            this.DeletewaterboxButton = new System.Windows.Forms.Button();
            this.AddwaterboxButton = new System.Windows.Forms.Button();
            this.WaterboxSelect = new System.Windows.Forms.NumericUpDown();
            this.niceLine2 = new SharpOcarina.NiceLine();
            this.InjectoffsetTextbox = new SharpOcarina.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.CollisionTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ScaleNumericbox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.NameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextureAnimsGroupBox = new System.Windows.Forms.GroupBox();
            this.label98 = new System.Windows.Forms.Label();
            this.TextureAnimHeight2 = new SharpOcarina.NumericUpDownEx();
            this.label99 = new System.Windows.Forms.Label();
            this.TextureAnimWidth2 = new SharpOcarina.NumericUpDownEx();
            this.label100 = new System.Windows.Forms.Label();
            this.TextureAnimYVelocity2 = new SharpOcarina.NumericUpDownEx();
            this.label101 = new System.Windows.Forms.Label();
            this.TextureAnimXVelocity2 = new SharpOcarina.NumericUpDownEx();
            this.niceLine14 = new SharpOcarina.NiceLine();
            this.label96 = new System.Windows.Forms.Label();
            this.TextureAnimHeight1 = new SharpOcarina.NumericUpDownEx();
            this.label97 = new System.Windows.Forms.Label();
            this.TextureAnimWidth1 = new SharpOcarina.NumericUpDownEx();
            this.label95 = new System.Windows.Forms.Label();
            this.TextureAnimYVelocity1 = new SharpOcarina.NumericUpDownEx();
            this.label59 = new System.Windows.Forms.Label();
            this.DeleteTextureAnim = new System.Windows.Forms.Button();
            this.TextureAnimXVelocity1 = new SharpOcarina.NumericUpDownEx();
            this.AddTextureAnim = new System.Windows.Forms.Button();
            this.TextureAnimSelect = new System.Windows.Forms.NumericUpDown();
            this.niceLine13 = new SharpOcarina.NiceLine();
            this.tabRooms = new System.Windows.Forms.TabPage();
            this.AdditionalTexturesGroupBox = new System.Windows.Forms.GroupBox();
            this.AdditionalTextureLabel = new System.Windows.Forms.Label();
            this.DeleteAdditionalTexture = new System.Windows.Forms.Button();
            this.AddAdditionalTexture = new System.Windows.Forms.Button();
            this.AdditionalTextureList = new System.Windows.Forms.NumericUpDown();
            this.ReloadRoomButton = new System.Windows.Forms.Button();
            this.AddMultipleRooms = new System.Windows.Forms.Button();
            this.ContinualInject = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupScaledNormals = new System.Windows.Forms.CheckBox();
            this.GroupCustomizeButton = new System.Windows.Forms.Button();
            this.GroupCustom = new System.Windows.Forms.CheckBox();
            this.GroupVertexNormals = new System.Windows.Forms.CheckBox();
            this.GroupAlphaMask = new System.Windows.Forms.CheckBox();
            this.GroupEnvColor = new System.Windows.Forms.CheckBox();
            this.GroupRenderLast = new System.Windows.Forms.CheckBox();
            this.GroupLODDIstance = new System.Windows.Forms.NumericUpDown();
            this.label102 = new System.Windows.Forms.Label();
            this.GroupLODGroup = new System.Windows.Forms.NumericUpDown();
            this.GroupLod = new System.Windows.Forms.CheckBox();
            this.GroupSmoothRgbaEdges = new System.Windows.Forms.CheckBox();
            this.AnimationLabel = new System.Windows.Forms.Label();
            this.GroupAnimatedBank = new System.Windows.Forms.NumericUpDown();
            this.GroupIgnoreFog = new System.Windows.Forms.CheckBox();
            this.Group2AxisBillboard = new System.Windows.Forms.CheckBox();
            this.GroupBillboard = new System.Windows.Forms.CheckBox();
            this.GroupPixelated = new System.Windows.Forms.CheckBox();
            this.GroupDecal = new System.Windows.Forms.CheckBox();
            this.label93 = new System.Windows.Forms.Label();
            this.ShiftTNumeric = new System.Windows.Forms.NumericUpDown();
            this.ShiftSNumeric = new System.Windows.Forms.NumericUpDown();
            this.ReverseLightCheckBox = new System.Windows.Forms.CheckBox();
            this.GroupMultitextureAlpha = new System.Windows.Forms.NumericUpDown();
            this.label79 = new System.Windows.Forms.Label();
            this.GroupMetallic = new System.Windows.Forms.CheckBox();
            this.GroupAnimated = new System.Windows.Forms.CheckBox();
            this.label34 = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.MultiTextureComboBox = new System.Windows.Forms.ComboBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.GroupPolygonType = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.RoomInjectionOffset = new SharpOcarina.NumericTextBox();
            this.GroupList = new System.Windows.Forms.ListBox();
            this.DeleteRoom = new System.Windows.Forms.Button();
            this.AddRoom = new System.Windows.Forms.Button();
            this.RoomList = new System.Windows.Forms.ListBox();
            this.tabSceneEnv = new System.Windows.Forms.TabPage();
            this.PrerenderedGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteJFIF = new System.Windows.Forms.Button();
            this.niceLine15 = new SharpOcarina.NiceLine();
            this.JFIFLabel = new System.Windows.Forms.Label();
            this.PrerenderedList = new System.Windows.Forms.NumericUpDown();
            this.LoadJFIF = new System.Windows.Forms.Button();
            this.AlternateHeadersGroupBox = new System.Windows.Forms.GroupBox();
            this.SceneHeaderCopyList = new SharpOcarina.NumericUpDownEx();
            this.SceneHeaderUsedLabel = new System.Windows.Forms.Label();
            this.SceneHeaderSameCheckbox = new System.Windows.Forms.CheckBox();
            this.DeleteSceneHeaderButton = new System.Windows.Forms.Button();
            this.AddSceneHeaderButton = new System.Windows.Forms.Button();
            this.SceneHeaderList = new System.Windows.Forms.NumericUpDown();
            this.niceLine9 = new SharpOcarina.NiceLine();
            this.PrerenderedCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.WorldMapComboBox = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.CameraMovementComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.SoundSpec = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.CloudyCheckBox = new System.Windows.Forms.CheckBox();
            this.SkyboxComboBox = new System.Windows.Forms.ComboBox();
            this.NightSFXComboBox = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SongComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.UnusedCommandCheckBox = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.ViewNormalCopyEnvB = new System.Windows.Forms.Button();
            this.ViewNormalCopyEnvA = new System.Windows.Forms.Button();
            this.EnvironmentDirectionBZ = new SharpOcarina.NumericUpDownEx();
            this.EnvironmentDirectionBY = new SharpOcarina.NumericUpDownEx();
            this.EnvironmentDirectionAZ = new SharpOcarina.NumericUpDownEx();
            this.EnvironmentDirectionAY = new SharpOcarina.NumericUpDownEx();
            this.EnvironmentDirectionBX = new SharpOcarina.NumericUpDownEx();
            this.EnvironmentDirectionAX = new SharpOcarina.NumericUpDownEx();
            this.DrawDistance = new SharpOcarina.NumericUpDownEx();
            this.FogUnknown = new SharpOcarina.NumericUpDownEx();
            this.FogDistance = new SharpOcarina.NumericUpDownEx();
            this.label85 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.LightingE = new System.Windows.Forms.PictureBox();
            this.FogColor = new System.Windows.Forms.PictureBox();
            this.label28 = new System.Windows.Forms.Label();
            this.LightingC = new System.Windows.Forms.PictureBox();
            this.LightingA = new System.Windows.Forms.PictureBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.EnvironmentSelect = new System.Windows.Forms.NumericUpDown();
            this.niceLine3 = new SharpOcarina.NiceLine();
            this.tabRoomEnv = new System.Windows.Forms.TabPage();
            this.Roomaffectedpointlightscheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.PointLightCheckBox = new System.Windows.Forms.CheckBox();
            this.AdditionalLightColor = new System.Windows.Forms.PictureBox();
            this.AdditionalLightDirectionLabel3 = new System.Windows.Forms.Label();
            this.AdditionalLightRadius = new SharpOcarina.NumericUpDownEx();
            this.AdditionalLightLabel1 = new System.Windows.Forms.Label();
            this.AdditionalLightPointLabel1 = new System.Windows.Forms.Label();
            this.AdditionalLightZPos = new SharpOcarina.NumericUpDownEx();
            this.AdditionalLightPointLabel3 = new System.Windows.Forms.Label();
            this.AdditionalLightPointLabel2 = new System.Windows.Forms.Label();
            this.AdditionalLightNS = new SharpOcarina.NumericUpDownEx();
            this.AdditionalLightDirectionLabel2 = new System.Windows.Forms.Label();
            this.AdditionalLightYPos = new SharpOcarina.NumericUpDownEx();
            this.AdditionalLightEW = new SharpOcarina.NumericUpDownEx();
            this.AdditionalLightDirectionLabel1 = new System.Windows.Forms.Label();
            this.AdditionalLightXPos = new SharpOcarina.NumericUpDownEx();
            this.AdditionalLightDelete = new System.Windows.Forms.Button();
            this.AdditionalLightAdd = new System.Windows.Forms.Button();
            this.AdditionalLightSelect = new System.Windows.Forms.NumericUpDown();
            this.niceLine5 = new SharpOcarina.NiceLine();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.WarpsongsCheckBox = new System.Windows.Forms.CheckBox();
            this.InvisibleActorsCheckBox = new System.Windows.Forms.CheckBox();
            this.label52 = new System.Windows.Forms.Label();
            this.IdleAnimComboBox = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.RestrictionComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.WindStrength = new SharpOcarina.NumericUpDownEx();
            this.WindSouth = new SharpOcarina.NumericUpDownEx();
            this.WindVertical = new SharpOcarina.NumericUpDownEx();
            this.WindWest = new SharpOcarina.NumericUpDownEx();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.SoundEcho = new SharpOcarina.NumericUpDownEx();
            this.TimeSpeed = new SharpOcarina.NumericUpDownEx();
            this.DisableStartTime = new System.Windows.Forms.CheckBox();
            this.TimeMinute = new SharpOcarina.NumericUpDownEx();
            this.label105 = new System.Windows.Forms.Label();
            this.TimeHour = new SharpOcarina.NumericUpDownEx();
            this.label43 = new System.Windows.Forms.Label();
            this.SunmoonCheckBox = new System.Windows.Forms.CheckBox();
            this.SkyboxCheckBox = new System.Windows.Forms.CheckBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.tabCollision = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.AddexitButton = new System.Windows.Forms.Button();
            this.DeleteexitButton = new System.Windows.Forms.Button();
            this.ExitGroupBox = new System.Windows.Forms.GroupBox();
            this.ExitHeaderIndex = new SharpOcarina.NumericUpDownEx();
            this.ExitSceneIndex = new SharpOcarina.NumericUpDownEx();
            this.label134 = new System.Windows.Forms.Label();
            this.ExitMusicOn = new System.Windows.Forms.CheckBox();
            this.ExitSpawnIndex = new SharpOcarina.NumericUpDownEx();
            this.ExitShowTitlecard = new System.Windows.Forms.CheckBox();
            this.label133 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.ExitFadeOut = new SharpOcarina.NumericUpDownEx();
            this.label1331 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.ExitFadeIn = new SharpOcarina.NumericUpDownEx();
            this.ExitListLabel = new System.Windows.Forms.Label();
            this.ExitList = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PolytypeExitLabel = new System.Windows.Forms.Label();
            this.BlockEponaCheckBox = new System.Windows.Forms.CheckBox();
            this.Lower1UnitChecbox = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.AutograbClimbRadioButton = new System.Windows.Forms.RadioButton();
            this.DiveRadioButton = new System.Windows.Forms.RadioButton();
            this.SmallVoidRadioButton = new System.Windows.Forms.RadioButton();
            this.NoLedgeJumpRadio = new System.Windows.Forms.RadioButton();
            this.NoMiscRadioButton = new System.Windows.Forms.RadioButton();
            this.VoidCheckBox = new System.Windows.Forms.RadioButton();
            this.WallDamageCheck = new System.Windows.Forms.CheckBox();
            this.label92 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.niceLine12 = new SharpOcarina.NiceLine();
            this.GroupDetectionB8 = new System.Windows.Forms.CheckBox();
            this.label90 = new System.Windows.Forms.Label();
            this.GroupDetectionB4 = new System.Windows.Forms.CheckBox();
            this.PolytypeUnk2 = new SharpOcarina.NumericUpDownEx();
            this.GroupDetectionB2 = new System.Windows.Forms.CheckBox();
            this.label89 = new System.Windows.Forms.Label();
            this.PolytypeUnk1 = new SharpOcarina.NumericUpDownEx();
            this.label88 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.GroupDetectionA8 = new System.Windows.Forms.CheckBox();
            this.GroupDetectionA4 = new System.Windows.Forms.CheckBox();
            this.GroupDetectionA2 = new System.Windows.Forms.CheckBox();
            this.niceLine7 = new SharpOcarina.NiceLine();
            this.CameraAngleNumeric = new SharpOcarina.NumericUpDownEx();
            this.CameraAngleLabel = new System.Windows.Forms.Label();
            this.HookshotableCheckbox = new System.Windows.Forms.CheckBox();
            this.ExitNumber = new SharpOcarina.NumericUpDownEx();
            this.label17 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.IceRadioButton = new System.Windows.Forms.RadioButton();
            this.KillingQuicksand2Radio = new System.Windows.Forms.RadioButton();
            this.JabuJabuRadio = new System.Windows.Forms.RadioButton();
            this.NoFallDamageRadio = new System.Windows.Forms.RadioButton();
            this.KillingLavaRadioButton = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.NoLedgeClimbRadio = new System.Windows.Forms.RadioButton();
            this.CrawlSpaceRadio = new System.Windows.Forms.RadioButton();
            this.LadderTopRadioButton = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SteepterrainCheckbox = new System.Windows.Forms.CheckBox();
            this.TerrainType = new SharpOcarina.NumericUpDownEx();
            this.label15 = new System.Windows.Forms.Label();
            this.GroundType = new SharpOcarina.NumericUpDownEx();
            this.label16 = new System.Windows.Forms.Label();
            this.EnvironmentType = new SharpOcarina.NumericUpDownEx();
            this.label14 = new System.Windows.Forms.Label();
            this.EchoRange = new SharpOcarina.NumericUpDownEx();
            this.label13 = new System.Windows.Forms.Label();
            this.niceLine4 = new SharpOcarina.NiceLine();
            this.PolygonRawdata = new SharpOcarina.NumericTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.niceLine10 = new SharpOcarina.NiceLine();
            this.niceLine11 = new SharpOcarina.NiceLine();
            this.niceLine1 = new SharpOcarina.NiceLine();
            this.DeletepolygonButton = new System.Windows.Forms.Button();
            this.AddpolygonButton = new System.Windows.Forms.Button();
            this.PolygonSelect = new System.Windows.Forms.NumericUpDown();
            this.tabTransitions = new System.Windows.Forms.TabPage();
            this.actorEditControl3 = new SharpOcarina.ActorEditControl();
            this.actorEditControl2 = new SharpOcarina.ActorEditControl();
            this.tabPathways = new System.Windows.Forms.TabPage();
            this.ActorCutsceneGroupBox = new System.Windows.Forms.GroupBox();
            this.label143 = new System.Windows.Forms.Label();
            this.ActorCutsceneUnknown = new SharpOcarina.NumericUpDownEx();
            this.ActorCutsceneCamIndex = new System.Windows.Forms.ComboBox();
            this.label142 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.ActorCutsceneRetCamera = new System.Windows.Forms.ComboBox();
            this.ActorCutsceneHudFade = new System.Windows.Forms.ComboBox();
            this.ActorCutsceneBlackBars = new System.Windows.Forms.CheckBox();
            this.ActorCutscenePuzzleSound = new System.Windows.Forms.CheckBox();
            this.label140 = new System.Windows.Forms.Label();
            this.ActorCutsceneAdditionalActorCs = new SharpOcarina.NumericUpDownEx();
            this.ActorCutsceneDeleteButton = new System.Windows.Forms.Button();
            this.ActorCutsceneAddButton = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.ActorCutsceneCsIndex = new SharpOcarina.NumericUpDownEx();
            this.ActorCutsceneNumber = new System.Windows.Forms.NumericUpDown();
            this.label138 = new System.Windows.Forms.Label();
            this.niceLine18 = new SharpOcarina.NiceLine();
            this.label139 = new System.Windows.Forms.Label();
            this.ActorCutsceneLength = new SharpOcarina.NumericUpDownEx();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.PathwayDown = new System.Windows.Forms.Button();
            this.PathwayUp = new System.Windows.Forms.Button();
            this.PathwayZPosStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.StickToZplus = new System.Windows.Forms.ToolStripMenuItem();
            this.StickToZminus = new System.Windows.Forms.ToolStripMenuItem();
            this.PathwayYPosStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.StickToYplus = new System.Windows.Forms.ToolStripMenuItem();
            this.StickToYminus = new System.Windows.Forms.ToolStripMenuItem();
            this.PathwayXPosStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.StickToXplus = new System.Windows.Forms.ToolStripMenuItem();
            this.StickToXminus = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePointButton = new System.Windows.Forms.Button();
            this.AddPointButton = new System.Windows.Forms.Button();
            this.PathwayListBox = new System.Windows.Forms.ListBox();
            this.PathwayDeleteButton = new System.Windows.Forms.Button();
            this.PathwayAddButton = new System.Windows.Forms.Button();
            this.PathwayLabel1 = new System.Windows.Forms.Label();
            this.PathwayZPos = new SharpOcarina.NumericUpDownEx();
            this.PathwayNumber = new System.Windows.Forms.NumericUpDown();
            this.PathwayLabel3 = new System.Windows.Forms.Label();
            this.niceLine6 = new SharpOcarina.NiceLine();
            this.PathwayLabel2 = new System.Windows.Forms.Label();
            this.PathwayXPos = new SharpOcarina.NumericUpDownEx();
            this.PathwayYPos = new SharpOcarina.NumericUpDownEx();
            this.tabActors = new System.Windows.Forms.TabPage();
            this.ObjectTabMenu = new System.Windows.Forms.TabControl();
            this.RoomObjectPage = new System.Windows.Forms.TabPage();
            this.RoomObjectListBox = new System.Windows.Forms.ListBox();
            this.RoomObjectDescription = new System.Windows.Forms.Label();
            this.RoomObjectDeleteButton = new System.Windows.Forms.Button();
            this.RoomObjectAddButton = new System.Windows.Forms.Button();
            this.SceneObjectPage = new System.Windows.Forms.TabPage();
            this.SceneObjectDescription = new System.Windows.Forms.Label();
            this.SceneObjectListBox = new System.Windows.Forms.ListBox();
            this.SceneObjectDeleteButton = new System.Windows.Forms.Button();
            this.SceneObjectAddButton = new System.Windows.Forms.Button();
            this.RoomObjectSpace = new System.Windows.Forms.Label();
            this.actorEditControl1 = new SharpOcarina.ActorEditControl();
            this.tabCutscene = new System.Windows.Forms.TabPage();
            this.DebugTextBox = new System.Windows.Forms.RichTextBox();
            this.CutsceneTableEntryLabel = new System.Windows.Forms.Label();
            this.CutsceneFlagLabel = new System.Windows.Forms.Label();
            this.CutsceneSpawnLabel = new System.Windows.Forms.Label();
            this.CutsceneEntranceLabel = new System.Windows.Forms.Label();
            this.CutsceneGroupBox = new System.Windows.Forms.GroupBox();
            this.MarkerEndFrame = new SharpOcarina.NumericTextBox();
            this.CutsceneTabs = new System.Windows.Forms.TabControl();
            this.CameraPositions = new System.Windows.Forms.TabPage();
            this.CutscenePositionPlayMode = new System.Windows.Forms.Button();
            this.CutscenePositionDown = new System.Windows.Forms.Button();
            this.CutscenePositionUp = new System.Windows.Forms.Button();
            this.CutscenePositionCopyCamera = new System.Windows.Forms.Button();
            this.CutscenePositionViewMode = new System.Windows.Forms.Button();
            this.CutsceneAbsolutePositionAngleView = new SharpOcarina.NumericUpDownEx();
            this.CutscenePositionFrameDuration = new SharpOcarina.NumericUpDownEx();
            this.CutsceneAbsolutePositionCameraRoll = new SharpOcarina.NumericUpDownEx();
            this.label60 = new System.Windows.Forms.Label();
            this.CutscenePositionXFocusLabel = new System.Windows.Forms.Label();
            this.CutscenePositionZFocus = new SharpOcarina.NumericUpDownEx();
            this.CutscenePositionZFocusLabel = new System.Windows.Forms.Label();
            this.CutscenePositionYFocusLabel = new System.Windows.Forms.Label();
            this.CutscenePositionXFocus = new SharpOcarina.NumericUpDownEx();
            this.CutscenePositionYFocus = new SharpOcarina.NumericUpDownEx();
            this.CutsceneDeleteAbsolutePosition = new System.Windows.Forms.Button();
            this.CutsceneAddAbsolutePosition = new System.Windows.Forms.Button();
            this.CutsceneAbsolutePositionListBox = new System.Windows.Forms.ListBox();
            this.label56 = new System.Windows.Forms.Label();
            this.CutsceneAbsolutePositionZ = new SharpOcarina.NumericUpDownEx();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.CutsceneAbsolutePositionX = new SharpOcarina.NumericUpDownEx();
            this.CutsceneAbsolutePositionY = new SharpOcarina.NumericUpDownEx();
            this.label55 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.SpecialExecution = new System.Windows.Forms.TabPage();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.CutsceneSetTimeHours = new SharpOcarina.NumericUpDownEx();
            this.CutsceneSetTimeMinutes = new SharpOcarina.NumericUpDownEx();
            this.Unknown = new System.Windows.Forms.TabPage();
            this.Textbox = new System.Windows.Forms.TabPage();
            this.CutsceneTextboxDown = new System.Windows.Forms.Button();
            this.CutsceneTextboxUp = new System.Windows.Forms.Button();
            this.label63 = new System.Windows.Forms.Label();
            this.CutsceneTextboxFramesLabel = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.CutsceneTextboxType = new System.Windows.Forms.ComboBox();
            this.CutsceneTextboxMessageIdLabel = new System.Windows.Forms.Label();
            this.CutsceneDeleteTextbox = new System.Windows.Forms.Button();
            this.CutsceneAddTextbox = new System.Windows.Forms.Button();
            this.CutsceneTextboxBottomMessageID = new SharpOcarina.NumericTextBox();
            this.CutsceneTextboxFrames = new SharpOcarina.NumericUpDownEx();
            this.CutsceneTextboxTopMessageID = new SharpOcarina.NumericTextBox();
            this.CutsceneTextboxMessageId = new SharpOcarina.NumericTextBox();
            this.CutsceneTextboxList = new System.Windows.Forms.ListBox();
            this.TransitionEffect = new System.Windows.Forms.TabPage();
            this.label70 = new System.Windows.Forms.Label();
            this.CutsceneTransitionComboBox = new System.Windows.Forms.ComboBox();
            this.AsmExecution = new System.Windows.Forms.TabPage();
            this.CutsceneAsmLabel = new System.Windows.Forms.Label();
            this.CutsceneAsmComboBox = new System.Windows.Forms.ComboBox();
            this.ActorCommand = new System.Windows.Forms.TabPage();
            this.CutsceneActorDown = new System.Windows.Forms.Button();
            this.CutsceneActorUp = new System.Windows.Forms.Button();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.CutsceneActorAnimation = new System.Windows.Forms.ComboBox();
            this.CutsceneActorAnimLabel = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.CutsceneActorDeleteAction = new System.Windows.Forms.Button();
            this.CutsceneActorAddAction = new System.Windows.Forms.Button();
            this.CutsceneActorFrameDuration = new SharpOcarina.NumericUpDownEx();
            this.CutsceneActorZRot = new SharpOcarina.NumericUpDownEx();
            this.CutsceneActorXRot = new SharpOcarina.NumericUpDownEx();
            this.CutsceneActorYRot = new SharpOcarina.NumericUpDownEx();
            this.CutsceneActorZEnd = new SharpOcarina.NumericUpDownEx();
            this.CutsceneActorXEnd = new SharpOcarina.NumericUpDownEx();
            this.CutsceneActorYEnd = new SharpOcarina.NumericUpDownEx();
            this.CutsceneActorListBox = new System.Windows.Forms.ListBox();
            this.CutsceneActorZStart = new SharpOcarina.NumericUpDownEx();
            this.CutsceneActorXStart = new SharpOcarina.NumericUpDownEx();
            this.CutsceneActorYStart = new SharpOcarina.NumericUpDownEx();
            this.label51 = new System.Windows.Forms.Label();
            this.MarkerTypeLabel = new System.Windows.Forms.Label();
            this.MarkerType = new System.Windows.Forms.ComboBox();
            this.MarkerDown = new System.Windows.Forms.Button();
            this.MarkerUp = new System.Windows.Forms.Button();
            this.DeleteMarker = new System.Windows.Forms.Button();
            this.AddMarker = new System.Windows.Forms.Button();
            this.MarkerSelect = new System.Windows.Forms.ListBox();
            this.label54 = new System.Windows.Forms.Label();
            this.MarkerStartFrame = new SharpOcarina.NumericTextBox();
            this.CutsceneTableEntry = new SharpOcarina.NumericUpDownEx();
            this.CutsceneFlag = new SharpOcarina.NumericUpDownEx();
            this.CutsceneSpawn = new SharpOcarina.NumericUpDownEx();
            this.CutsceneEntrance = new SharpOcarina.NumericUpDownEx();
            this.tabAnimations = new System.Windows.Forms.TabPage();
            this.RenderFunctionInherit = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RenderFunctionWarningLabel = new System.Windows.Forms.Label();
            this.RenderFunctionPreview = new System.Windows.Forms.Button();
            this.RenderFunctionGroupBoxFlag = new System.Windows.Forms.GroupBox();
            this.RenderFunctionFlagFreezeCheckBox = new System.Windows.Forms.CheckBox();
            this.RenderFunctionFlagPresetToolStrip = new System.Windows.Forms.ToolStrip();
            this.RenderFunctionFlagPresetButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.RenderFunctionFlagBitwiseLabel = new System.Windows.Forms.Label();
            this.RenderFunctionFlagBitwise = new SharpOcarina.NumericTextBox();
            this.RenderFunctionFlagReverseCheckbox = new System.Windows.Forms.CheckBox();
            this.RenderFunctionFlagLabel = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.RenderFunctionFlagID = new SharpOcarina.NumericUpDownEx();
            this.RenderFunctionFlagType = new System.Windows.Forms.ComboBox();
            this.label137 = new System.Windows.Forms.Label();
            this.RenderFunctionID = new System.Windows.Forms.NumericUpDown();
            this.RenderFunctionTabs = new System.Windows.Forms.TabControl();
            this.tabTextureScroll = new System.Windows.Forms.TabPage();
            this.label106 = new System.Windows.Forms.Label();
            this.FunctionTextureScrollHeight2 = new SharpOcarina.NumericUpDownEx();
            this.FunctionTextureScrollXVelocity = new SharpOcarina.NumericUpDownEx();
            this.label107 = new System.Windows.Forms.Label();
            this.niceLine17 = new SharpOcarina.NiceLine();
            this.FunctionTextureScrollWidth2 = new SharpOcarina.NumericUpDownEx();
            this.label113 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.FunctionTextureScrollYVelocity = new SharpOcarina.NumericUpDownEx();
            this.FunctionTextureScrollYVelocity2 = new SharpOcarina.NumericUpDownEx();
            this.label112 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.FunctionTextureScrollWidth = new SharpOcarina.NumericUpDownEx();
            this.FunctionTextureScrollXVelocity2 = new SharpOcarina.NumericUpDownEx();
            this.label111 = new System.Windows.Forms.Label();
            this.niceLine16 = new SharpOcarina.NiceLine();
            this.FunctionTextureScrollHeight = new SharpOcarina.NumericUpDownEx();
            this.label110 = new System.Windows.Forms.Label();
            this.tabColorBlending = new System.Windows.Forms.TabPage();
            this.label125 = new System.Windows.Forms.Label();
            this.FunctionColorBlendColor = new System.Windows.Forms.PictureBox();
            this.label119 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.FunctionColorBlendDown = new System.Windows.Forms.Button();
            this.FunctionColorBlendUp = new System.Windows.Forms.Button();
            this.FunctionColorBlendDelete = new System.Windows.Forms.Button();
            this.FunctionColorBlendAdd = new System.Windows.Forms.Button();
            this.FunctionColorBlendAlpha = new SharpOcarina.NumericUpDownEx();
            this.FunctionColorBlendFrames = new SharpOcarina.NumericUpDownEx();
            this.FunctionColorBlendList = new System.Windows.Forms.ListBox();
            this.tabTextureSwap = new System.Windows.Forms.TabPage();
            this.FunctionTextureSwapPictureBox2 = new System.Windows.Forms.PictureBox();
            this.FunctionTextureSwapPictureBox = new System.Windows.Forms.PictureBox();
            this.label116 = new System.Windows.Forms.Label();
            this.FunctionTextureSwapTextureID2 = new System.Windows.Forms.ComboBox();
            this.label115 = new System.Windows.Forms.Label();
            this.FunctionTextureSwapTextureID = new System.Windows.Forms.ComboBox();
            this.tabTextureSwapFrames = new System.Windows.Forms.TabPage();
            this.FunctionTextureSwapAnimationPictureBox = new System.Windows.Forms.PictureBox();
            this.FunctionTextureSwapAnimationImage = new System.Windows.Forms.ComboBox();
            this.FunctionTextureSwapAnimationDown = new System.Windows.Forms.Button();
            this.FunctionTextureSwapAnimationUp = new System.Windows.Forms.Button();
            this.label117 = new System.Windows.Forms.Label();
            this.FunctionTextureSwapAnimationDelete = new System.Windows.Forms.Button();
            this.FunctionTextureSwapAnimationAdd = new System.Windows.Forms.Button();
            this.FunctionTextureSwapAnimationDuration = new SharpOcarina.NumericUpDownEx();
            this.FunctionTextureSwapAnimationList = new System.Windows.Forms.ListBox();
            this.tabCameraEffect = new System.Windows.Forms.TabPage();
            this.FunctionCameraEffectDropdown = new System.Windows.Forms.ComboBox();
            this.label120 = new System.Windows.Forms.Label();
            this.tabConditionalDraw = new System.Windows.Forms.TabPage();
            this.label135 = new System.Windows.Forms.Label();
            this.RenderFunctionType = new System.Windows.Forms.ComboBox();
            this.RenderFunctionDown = new System.Windows.Forms.Button();
            this.RenderFunctionUp = new System.Windows.Forms.Button();
            this.DeleteRenderFunction = new System.Windows.Forms.Button();
            this.AddRenderFunction = new System.Windows.Forms.Button();
            this.RenderFunctionSelect = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.EnvironmentControlTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.GlobalRomRefresh = new System.Windows.Forms.Button();
            this.Z64RomPlay = new System.Windows.Forms.Button();
            this.label86 = new System.Windows.Forms.Label();
            this.CDILink = new System.Windows.Forms.PictureBox();
            this.labelcamerapos = new System.Windows.Forms.Label();
            this.UpdateLabel = new System.Windows.Forms.Label();
            this.RomModeLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.RoomSelector = new SharpOcarina.NumericUpDownEx();
            this.ViewportFOV = new SharpOcarina.NumericUpDownEx();
            this.SceneHeaderSelector = new SharpOcarina.NumericUpDownEx();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScenenumberTextbox)).BeginInit();
            this.CamerasGroupBox.SuspendLayout();
            this.CameraPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraFov)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraZRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraXRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraYPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraYRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraXPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraSelect)).BeginInit();
            this.CameraPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk1E)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk1C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk1A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk12)).BeginInit();
            this.WaterboxGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxRoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxCam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxEnv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxXSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxYPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxYSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxXPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumericbox)).BeginInit();
            this.TextureAnimsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimHeight2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimWidth2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimYVelocity2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimXVelocity2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimHeight1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimWidth1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimYVelocity1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimXVelocity1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimSelect)).BeginInit();
            this.tabRooms.SuspendLayout();
            this.AdditionalTexturesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalTextureList)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupLODDIstance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupLODGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupAnimatedBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftTNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftSNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupMultitextureAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupPolygonType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.tabSceneEnv.SuspendLayout();
            this.PrerenderedGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrerenderedList)).BeginInit();
            this.AlternateHeadersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SceneHeaderCopyList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SceneHeaderList)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionBZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionBY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionAZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionAY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionBX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionAX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrawDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FogUnknown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FogDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightingE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FogColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightingC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightingA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentSelect)).BeginInit();
            this.tabRoomEnv.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightNS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightYPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightEW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightXPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightSelect)).BeginInit();
            this.groupBox11.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WindStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindSouth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindWest)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SoundEcho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeHour)).BeginInit();
            this.tabCollision.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.ExitGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExitHeaderIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitSceneIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitSpawnIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitFadeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitFadeIn)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PolytypeUnk2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PolytypeUnk1)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraAngleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitNumber)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TerrainType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroundType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EchoRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PolygonSelect)).BeginInit();
            this.tabTransitions.SuspendLayout();
            this.tabPathways.SuspendLayout();
            this.ActorCutsceneGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneUnknown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneAdditionalActorCs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneCsIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneLength)).BeginInit();
            this.groupBox13.SuspendLayout();
            this.PathwayZPosStrip.SuspendLayout();
            this.PathwayYPosStrip.SuspendLayout();
            this.PathwayXPosStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PathwayZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathwayNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathwayXPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathwayYPos)).BeginInit();
            this.tabActors.SuspendLayout();
            this.ObjectTabMenu.SuspendLayout();
            this.RoomObjectPage.SuspendLayout();
            this.SceneObjectPage.SuspendLayout();
            this.tabCutscene.SuspendLayout();
            this.CutsceneGroupBox.SuspendLayout();
            this.CutsceneTabs.SuspendLayout();
            this.CameraPositions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionAngleView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutscenePositionFrameDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionCameraRoll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutscenePositionZFocus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutscenePositionXFocus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutscenePositionYFocus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionY)).BeginInit();
            this.SpecialExecution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneSetTimeHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneSetTimeMinutes)).BeginInit();
            this.Textbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneTextboxFrames)).BeginInit();
            this.TransitionEffect.SuspendLayout();
            this.AsmExecution.SuspendLayout();
            this.ActorCommand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorFrameDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorZRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorXRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorYRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorZEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorXEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorYEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorZStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorXStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorYStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneTableEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneSpawn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneEntrance)).BeginInit();
            this.tabAnimations.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.RenderFunctionGroupBoxFlag.SuspendLayout();
            this.RenderFunctionFlagPresetToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RenderFunctionFlagID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RenderFunctionID)).BeginInit();
            this.RenderFunctionTabs.SuspendLayout();
            this.tabTextureScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollHeight2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollXVelocity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollWidth2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollYVelocity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollYVelocity2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollXVelocity2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollHeight)).BeginInit();
            this.tabColorBlending.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionColorBlendColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionColorBlendAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionColorBlendFrames)).BeginInit();
            this.tabTextureSwap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureSwapPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureSwapPictureBox)).BeginInit();
            this.tabTextureSwapFrames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureSwapAnimationPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureSwapAnimationDuration)).BeginInit();
            this.tabCameraEffect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CDILink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoomSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewportFOV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SceneHeaderSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControl1.Location = new System.Drawing.Point(12, 27);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(720, 720);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = true;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyUp);
            this.glControl1.Leave += new System.EventHandler(this.glControl1_Leave);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            this.glControl1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseWheel);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.extraToolStripMenuItem,
            this.nokaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1161, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSceneToolStripMenuItem,
            this.openSceneToolStripMenuItem,
            this.toolStripSeparator2,
            this.SaveScenetoolStripMenuItem3,
            this.saveSceneToolStripMenuItem,
            this.saveBinaryToolStripMenuItem,
            this.toolStripMenuItem2,
            this.OpenGlobalROM,
            this.injectToROMToolStripMenuItem,
            this.LaunchRomToolStripMenuItem,
            this.buildAndLaunchZ64romToolStripMenuItem,
            this.buildAndLaunchZ64romWarpToSceneToolStripMenuItem,
            this.toolStripSeparator8,
            this.NewZ64romProject,
            this.toolStripSeparator6,
            this.openZmapToolstrip,
            this.OpenSceneFromRoomToolStrip,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newSceneToolStripMenuItem
            // 
            this.newSceneToolStripMenuItem.Name = "newSceneToolStripMenuItem";
            this.newSceneToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.newSceneToolStripMenuItem.Text = "&New Scene";
            this.newSceneToolStripMenuItem.Click += new System.EventHandler(this.newSceneToolStripMenuItem_Click);
            // 
            // openSceneToolStripMenuItem
            // 
            this.openSceneToolStripMenuItem.Name = "openSceneToolStripMenuItem";
            this.openSceneToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.openSceneToolStripMenuItem.Text = "&Open Scene";
            this.openSceneToolStripMenuItem.Click += new System.EventHandler(this.openSceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(321, 6);
            // 
            // SaveScenetoolStripMenuItem3
            // 
            this.SaveScenetoolStripMenuItem3.Enabled = false;
            this.SaveScenetoolStripMenuItem3.Name = "SaveScenetoolStripMenuItem3";
            this.SaveScenetoolStripMenuItem3.Size = new System.Drawing.Size(324, 22);
            this.SaveScenetoolStripMenuItem3.Text = "&Save Scene";
            this.SaveScenetoolStripMenuItem3.Click += new System.EventHandler(this.SaveScenetoolStripMenuItem3_Click);
            // 
            // saveSceneToolStripMenuItem
            // 
            this.saveSceneToolStripMenuItem.Enabled = false;
            this.saveSceneToolStripMenuItem.Name = "saveSceneToolStripMenuItem";
            this.saveSceneToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.saveSceneToolStripMenuItem.Text = "Save Scene &As...";
            this.saveSceneToolStripMenuItem.Click += new System.EventHandler(this.saveSceneToolStripMenuItem_Click);
            // 
            // saveBinaryToolStripMenuItem
            // 
            this.saveBinaryToolStripMenuItem.Enabled = false;
            this.saveBinaryToolStripMenuItem.Name = "saveBinaryToolStripMenuItem";
            this.saveBinaryToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.saveBinaryToolStripMenuItem.Text = "Save &Binary";
            this.saveBinaryToolStripMenuItem.Click += new System.EventHandler(this.saveBinaryToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(321, 6);
            // 
            // OpenGlobalROM
            // 
            this.OpenGlobalROM.Name = "OpenGlobalROM";
            this.OpenGlobalROM.Size = new System.Drawing.Size(324, 22);
            this.OpenGlobalROM.Text = "Open &Global ROM / z64rom project";
            this.OpenGlobalROM.Click += new System.EventHandler(this.OpenGlobalROM_Click);
            // 
            // injectToROMToolStripMenuItem
            // 
            this.injectToROMToolStripMenuItem.Enabled = false;
            this.injectToROMToolStripMenuItem.Name = "injectToROMToolStripMenuItem";
            this.injectToROMToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.injectToROMToolStripMenuItem.Text = "&Inject to ROM";
            this.injectToROMToolStripMenuItem.Click += new System.EventHandler(this.injectToROMToolStripMenuItem_Click);
            // 
            // LaunchRomToolStripMenuItem
            // 
            this.LaunchRomToolStripMenuItem.Enabled = false;
            this.LaunchRomToolStripMenuItem.Name = "LaunchRomToolStripMenuItem";
            this.LaunchRomToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.LaunchRomToolStripMenuItem.Text = "Launch ROM";
            this.LaunchRomToolStripMenuItem.Click += new System.EventHandler(this.LaunchRomToolStripMenuItem_Click);
            // 
            // buildAndLaunchZ64romToolStripMenuItem
            // 
            this.buildAndLaunchZ64romToolStripMenuItem.Enabled = false;
            this.buildAndLaunchZ64romToolStripMenuItem.Name = "buildAndLaunchZ64romToolStripMenuItem";
            this.buildAndLaunchZ64romToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.buildAndLaunchZ64romToolStripMenuItem.Text = "Build and launch z64rom";
            this.buildAndLaunchZ64romToolStripMenuItem.Visible = false;
            this.buildAndLaunchZ64romToolStripMenuItem.Click += new System.EventHandler(this.buildAndLaunchZ64romToolStripMenuItem_Click);
            // 
            // buildAndLaunchZ64romWarpToSceneToolStripMenuItem
            // 
            this.buildAndLaunchZ64romWarpToSceneToolStripMenuItem.Enabled = false;
            this.buildAndLaunchZ64romWarpToSceneToolStripMenuItem.Name = "buildAndLaunchZ64romWarpToSceneToolStripMenuItem";
            this.buildAndLaunchZ64romWarpToSceneToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.buildAndLaunchZ64romWarpToSceneToolStripMenuItem.Text = "Send, build and launch z64rom + warp to scene";
            this.buildAndLaunchZ64romWarpToSceneToolStripMenuItem.Visible = false;
            this.buildAndLaunchZ64romWarpToSceneToolStripMenuItem.Click += new System.EventHandler(this.buildAndLaunchZ64romWarpToSceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(321, 6);
            // 
            // NewZ64romProject
            // 
            this.NewZ64romProject.Name = "NewZ64romProject";
            this.NewZ64romProject.Size = new System.Drawing.Size(324, 22);
            this.NewZ64romProject.Text = "New z64rom project";
            this.NewZ64romProject.Click += new System.EventHandler(this.NewZ64romProject_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(321, 6);
            // 
            // openZmapToolstrip
            // 
            this.openZmapToolstrip.Name = "openZmapToolstrip";
            this.openZmapToolstrip.Size = new System.Drawing.Size(324, 22);
            this.openZmapToolstrip.Text = "View .zscene (beta)";
            this.openZmapToolstrip.Visible = false;
            this.openZmapToolstrip.Click += new System.EventHandler(this.openZmapToolstrip_Click);
            // 
            // OpenSceneFromRoomToolStrip
            // 
            this.OpenSceneFromRoomToolStrip.Name = "OpenSceneFromRoomToolStrip";
            this.OpenSceneFromRoomToolStrip.Size = new System.Drawing.Size(324, 22);
            this.OpenSceneFromRoomToolStrip.Text = "View scene from ROM (beta)";
            this.OpenSceneFromRoomToolStrip.Visible = false;
            this.OpenSceneFromRoomToolStrip.Click += new System.EventHandler(this.OpenSceneFromRoomToolStrip_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(321, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveOptionsToolStripMenuItem,
            this.toolStripSeparator7,
            this.AdvancedTextureAnimationsMenuItem,
            this.EnableNexExitFormatMenuItem,
            this.AutoReload,
            this.toolStripSeparator3,
            this.majorasMaskModeexperimentalToolStripMenuItem,
            this.IgnoreMajorasMaskDaySystem,
            this.toolStripSeparator4,
            this.showCollisionModelToolStripMenuItem,
            this.showRoomModelsToolStripMenuItem,
            this.DisplayAxisMenuItem,
            this.ColorBlindMenuItem,
            this.applyEnvironmentLightingToolStripMenuItem,
            this.RenderActorstoolStrip,
            this.RenderChildLinkMenuItem,
            this.forceRGBATexturesToolStripMenuItem,
            this.dontConvertMultitextureToRGBAToolStripMenuItem,
            this.DisableRGBA32ToolStrip,
            this.RenderSelectedCutsceneCommandsMenuItem,
            this.RenderWaterboxesMenuItem,
            this.DisableTextureWarningsMenuItem,
            this.DisableCutscenePreviewBlackBarsMenuItem,
            this.ResetGroupSettingsReloadMenuItem,
            this.toolStripSeparator1,
            this.consecutiveRoomInjectionToolStripMenuItem,
            this.AutoaddGroupsMenuItem,
            this.printOffsetsOnInjectToolStripMenuItem,
            this.AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem,
            this.updateCRCMenuItem,
            this.CheckEmptyOffsetItem,
            this.ZmapOffsetNames,
            this.noDummyPointsInCutsceneCamerasToolStripMenuItem,
            this.dListCullingMenuItem,
            this.triplicateCollisionBoundsToolStripMenuItem,
            this.AutoFixErrorsStripMenuItem3,
            this.toolStripSeparator5,
            this.DegreesMenuItem,
            this.disableWaterboxMouseMovementToolStripMenuItem,
            this.showRotationValuesAsHexadecimalToolStripMenuItem,
            this.autosaveSceneXmlToolStripMenuItem,
            this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem,
            this.sharpPixelatedTexturesToolStripMenuItem});
            this.optionsToolStripMenuItem.Enabled = false;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // saveOptionsToolStripMenuItem
            // 
            this.saveOptionsToolStripMenuItem.Name = "saveOptionsToolStripMenuItem";
            this.saveOptionsToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.saveOptionsToolStripMenuItem.Text = "Save Options";
            this.saveOptionsToolStripMenuItem.Click += new System.EventHandler(this.saveOptionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(308, 6);
            // 
            // AdvancedTextureAnimationsMenuItem
            // 
            this.AdvancedTextureAnimationsMenuItem.CheckOnClick = true;
            this.AdvancedTextureAnimationsMenuItem.Name = "AdvancedTextureAnimationsMenuItem";
            this.AdvancedTextureAnimationsMenuItem.Size = new System.Drawing.Size(311, 22);
            this.AdvancedTextureAnimationsMenuItem.Text = "Enable advanced texture animations";
            this.AdvancedTextureAnimationsMenuItem.Click += new System.EventHandler(this.AdvancedTextureAnimationsMenuItem_Click);
            // 
            // EnableNexExitFormatMenuItem
            // 
            this.EnableNexExitFormatMenuItem.CheckOnClick = true;
            this.EnableNexExitFormatMenuItem.Name = "EnableNexExitFormatMenuItem";
            this.EnableNexExitFormatMenuItem.Size = new System.Drawing.Size(311, 22);
            this.EnableNexExitFormatMenuItem.Text = "Enable new exit format (z64rom)";
            this.EnableNexExitFormatMenuItem.Click += new System.EventHandler(this.EnableNexExitFormatMenuItem_Click);
            // 
            // AutoReload
            // 
            this.AutoReload.CheckOnClick = true;
            this.AutoReload.Name = "AutoReload";
            this.AutoReload.Size = new System.Drawing.Size(311, 22);
            this.AutoReload.Text = "Auto Reload";
            this.AutoReload.Click += new System.EventHandler(this.AutoReload_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(308, 6);
            // 
            // majorasMaskModeexperimentalToolStripMenuItem
            // 
            this.majorasMaskModeexperimentalToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.majorasMaskModeexperimentalToolStripMenuItem.CheckOnClick = true;
            this.majorasMaskModeexperimentalToolStripMenuItem.Name = "majorasMaskModeexperimentalToolStripMenuItem";
            this.majorasMaskModeexperimentalToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.majorasMaskModeexperimentalToolStripMenuItem.Text = "Majora\'s Mask mode";
            this.majorasMaskModeexperimentalToolStripMenuItem.Click += new System.EventHandler(this.majorasMaskModeexperimentalToolStripMenuItem_Click);
            // 
            // IgnoreMajorasMaskDaySystem
            // 
            this.IgnoreMajorasMaskDaySystem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.IgnoreMajorasMaskDaySystem.CheckOnClick = true;
            this.IgnoreMajorasMaskDaySystem.Name = "IgnoreMajorasMaskDaySystem";
            this.IgnoreMajorasMaskDaySystem.Size = new System.Drawing.Size(311, 22);
            this.IgnoreMajorasMaskDaySystem.Text = "Ignore Majora\'s Mask day system";
            this.IgnoreMajorasMaskDaySystem.Click += new System.EventHandler(this.IgnoreMajorasMaskDaySystem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(308, 6);
            // 
            // showCollisionModelToolStripMenuItem
            // 
            this.showCollisionModelToolStripMenuItem.CheckOnClick = true;
            this.showCollisionModelToolStripMenuItem.Name = "showCollisionModelToolStripMenuItem";
            this.showCollisionModelToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.showCollisionModelToolStripMenuItem.Text = "Show &Collision Model";
            this.showCollisionModelToolStripMenuItem.Click += new System.EventHandler(this.showCollisionModelToolStripMenuItem_Click);
            // 
            // showRoomModelsToolStripMenuItem
            // 
            this.showRoomModelsToolStripMenuItem.CheckOnClick = true;
            this.showRoomModelsToolStripMenuItem.Name = "showRoomModelsToolStripMenuItem";
            this.showRoomModelsToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.showRoomModelsToolStripMenuItem.Text = "Show &Room Models";
            this.showRoomModelsToolStripMenuItem.Click += new System.EventHandler(this.showRoomModelsToolStripMenuItem_Click);
            // 
            // DisplayAxisMenuItem
            // 
            this.DisplayAxisMenuItem.CheckOnClick = true;
            this.DisplayAxisMenuItem.Name = "DisplayAxisMenuItem";
            this.DisplayAxisMenuItem.Size = new System.Drawing.Size(311, 22);
            this.DisplayAxisMenuItem.Text = "Show Selected Instance Axis";
            this.DisplayAxisMenuItem.Click += new System.EventHandler(this.DisplayAxisMenuItem_Click);
            // 
            // ColorBlindMenuItem
            // 
            this.ColorBlindMenuItem.CheckOnClick = true;
            this.ColorBlindMenuItem.Name = "ColorBlindMenuItem";
            this.ColorBlindMenuItem.Size = new System.Drawing.Size(311, 22);
            this.ColorBlindMenuItem.Text = "Color Blind Axis";
            this.ColorBlindMenuItem.Click += new System.EventHandler(this.ColorBlindMenuItem_Click);
            // 
            // applyEnvironmentLightingToolStripMenuItem
            // 
            this.applyEnvironmentLightingToolStripMenuItem.CheckOnClick = true;
            this.applyEnvironmentLightingToolStripMenuItem.Name = "applyEnvironmentLightingToolStripMenuItem";
            this.applyEnvironmentLightingToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.applyEnvironmentLightingToolStripMenuItem.Text = "&Apply Environment Lighting";
            this.applyEnvironmentLightingToolStripMenuItem.Visible = false;
            this.applyEnvironmentLightingToolStripMenuItem.Click += new System.EventHandler(this.applyEnvironmentLightingToolStripMenuItem_Click);
            // 
            // RenderActorstoolStrip
            // 
            this.RenderActorstoolStrip.CheckOnClick = true;
            this.RenderActorstoolStrip.Name = "RenderActorstoolStrip";
            this.RenderActorstoolStrip.Size = new System.Drawing.Size(311, 22);
            this.RenderActorstoolStrip.Text = "Render Actors (most of them)";
            this.RenderActorstoolStrip.Click += new System.EventHandler(this.renderActorslMenuItem_Click);
            // 
            // RenderChildLinkMenuItem
            // 
            this.RenderChildLinkMenuItem.CheckOnClick = true;
            this.RenderChildLinkMenuItem.Name = "RenderChildLinkMenuItem";
            this.RenderChildLinkMenuItem.Size = new System.Drawing.Size(311, 22);
            this.RenderChildLinkMenuItem.Text = "Render Child Link instead of Adult";
            this.RenderChildLinkMenuItem.Click += new System.EventHandler(this.RenderChildLinkMenuItem_Click);
            // 
            // forceRGBATexturesToolStripMenuItem
            // 
            this.forceRGBATexturesToolStripMenuItem.CheckOnClick = true;
            this.forceRGBATexturesToolStripMenuItem.Name = "forceRGBATexturesToolStripMenuItem";
            this.forceRGBATexturesToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.forceRGBATexturesToolStripMenuItem.Text = "&Force RGBA Textures";
            this.forceRGBATexturesToolStripMenuItem.Click += new System.EventHandler(this.forceRGBATexturesToolStripMenuItem_Click);
            // 
            // dontConvertMultitextureToRGBAToolStripMenuItem
            // 
            this.dontConvertMultitextureToRGBAToolStripMenuItem.CheckOnClick = true;
            this.dontConvertMultitextureToRGBAToolStripMenuItem.Name = "dontConvertMultitextureToRGBAToolStripMenuItem";
            this.dontConvertMultitextureToRGBAToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.dontConvertMultitextureToRGBAToolStripMenuItem.Text = "Don\'t convert multitextures to RGBA";
            this.dontConvertMultitextureToRGBAToolStripMenuItem.Click += new System.EventHandler(this.dontConvertMultitextureToRGBAToolStripMenuItem_Click);
            // 
            // DisableRGBA32ToolStrip
            // 
            this.DisableRGBA32ToolStrip.CheckOnClick = true;
            this.DisableRGBA32ToolStrip.Name = "DisableRGBA32ToolStrip";
            this.DisableRGBA32ToolStrip.Size = new System.Drawing.Size(311, 22);
            this.DisableRGBA32ToolStrip.Text = "Disable RGBA32 detection";
            this.DisableRGBA32ToolStrip.Click += new System.EventHandler(this.DisableRGBA32ToolStrip_Click);
            // 
            // RenderSelectedCutsceneCommandsMenuItem
            // 
            this.RenderSelectedCutsceneCommandsMenuItem.CheckOnClick = true;
            this.RenderSelectedCutsceneCommandsMenuItem.Name = "RenderSelectedCutsceneCommandsMenuItem";
            this.RenderSelectedCutsceneCommandsMenuItem.Size = new System.Drawing.Size(311, 22);
            this.RenderSelectedCutsceneCommandsMenuItem.Text = "Only render selected cutscene commands";
            this.RenderSelectedCutsceneCommandsMenuItem.Click += new System.EventHandler(this.RenderSelectedCutsceneCommandsMenuItem_Click);
            // 
            // RenderWaterboxesMenuItem
            // 
            this.RenderWaterboxesMenuItem.CheckOnClick = true;
            this.RenderWaterboxesMenuItem.Name = "RenderWaterboxesMenuItem";
            this.RenderWaterboxesMenuItem.Size = new System.Drawing.Size(311, 22);
            this.RenderWaterboxesMenuItem.Text = "Only render waterboxes in general tab";
            this.RenderWaterboxesMenuItem.Click += new System.EventHandler(this.RenderWaterboxesMenuItem_Click);
            // 
            // DisableTextureWarningsMenuItem
            // 
            this.DisableTextureWarningsMenuItem.CheckOnClick = true;
            this.DisableTextureWarningsMenuItem.Name = "DisableTextureWarningsMenuItem";
            this.DisableTextureWarningsMenuItem.Size = new System.Drawing.Size(311, 22);
            this.DisableTextureWarningsMenuItem.Text = "Disable texture warnings";
            this.DisableTextureWarningsMenuItem.Click += new System.EventHandler(this.DisableTextureWarningsMenuItem_Click);
            // 
            // DisableCutscenePreviewBlackBarsMenuItem
            // 
            this.DisableCutscenePreviewBlackBarsMenuItem.CheckOnClick = true;
            this.DisableCutscenePreviewBlackBarsMenuItem.Name = "DisableCutscenePreviewBlackBarsMenuItem";
            this.DisableCutscenePreviewBlackBarsMenuItem.Size = new System.Drawing.Size(311, 22);
            this.DisableCutscenePreviewBlackBarsMenuItem.Text = "Disable cutscene preview black bars";
            this.DisableCutscenePreviewBlackBarsMenuItem.Click += new System.EventHandler(this.DisableCutscenePreviewBlackBarsMenuItem_Click);
            // 
            // ResetGroupSettingsReloadMenuItem
            // 
            this.ResetGroupSettingsReloadMenuItem.CheckOnClick = true;
            this.ResetGroupSettingsReloadMenuItem.Name = "ResetGroupSettingsReloadMenuItem";
            this.ResetGroupSettingsReloadMenuItem.Size = new System.Drawing.Size(311, 22);
            this.ResetGroupSettingsReloadMenuItem.Text = "Reset group settings on reload";
            this.ResetGroupSettingsReloadMenuItem.Click += new System.EventHandler(this.ResetGroupSettingsReloadMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(308, 6);
            // 
            // consecutiveRoomInjectionToolStripMenuItem
            // 
            this.consecutiveRoomInjectionToolStripMenuItem.CheckOnClick = true;
            this.consecutiveRoomInjectionToolStripMenuItem.Enabled = false;
            this.consecutiveRoomInjectionToolStripMenuItem.Name = "consecutiveRoomInjectionToolStripMenuItem";
            this.consecutiveRoomInjectionToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.consecutiveRoomInjectionToolStripMenuItem.Text = "&Consecutive Room Injection";
            this.consecutiveRoomInjectionToolStripMenuItem.Visible = false;
            this.consecutiveRoomInjectionToolStripMenuItem.Click += new System.EventHandler(this.consecutiveRoomInjectionToolStripMenuItem_Click);
            // 
            // AutoaddGroupsMenuItem
            // 
            this.AutoaddGroupsMenuItem.CheckOnClick = true;
            this.AutoaddGroupsMenuItem.Name = "AutoaddGroupsMenuItem";
            this.AutoaddGroupsMenuItem.Size = new System.Drawing.Size(311, 22);
            this.AutoaddGroupsMenuItem.Text = "Add required actor objects on save";
            this.AutoaddGroupsMenuItem.Click += new System.EventHandler(this.AutoaddGroupsClick);
            // 
            // printOffsetsOnInjectToolStripMenuItem
            // 
            this.printOffsetsOnInjectToolStripMenuItem.CheckOnClick = true;
            this.printOffsetsOnInjectToolStripMenuItem.Name = "printOffsetsOnInjectToolStripMenuItem";
            this.printOffsetsOnInjectToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.printOffsetsOnInjectToolStripMenuItem.Text = "Print Offsets on Inject";
            this.printOffsetsOnInjectToolStripMenuItem.Click += new System.EventHandler(this.printOffsetsOnInjectToolStripMenuItem_Click);
            // 
            // AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem
            // 
            this.AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem.CheckOnClick = true;
            this.AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem.Name = "AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem";
            this.AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem.Text = "Always generate custom DMA table on inject";
            this.AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem.Click += new System.EventHandler(this.AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem_Click);
            // 
            // updateCRCMenuItem
            // 
            this.updateCRCMenuItem.CheckOnClick = true;
            this.updateCRCMenuItem.Name = "updateCRCMenuItem";
            this.updateCRCMenuItem.Size = new System.Drawing.Size(311, 22);
            this.updateCRCMenuItem.Text = "Update CRC on room inject";
            this.updateCRCMenuItem.Click += new System.EventHandler(this.updateCRCMenuItem_Click);
            // 
            // CheckEmptyOffsetItem
            // 
            this.CheckEmptyOffsetItem.CheckOnClick = true;
            this.CheckEmptyOffsetItem.Enabled = false;
            this.CheckEmptyOffsetItem.Name = "CheckEmptyOffsetItem";
            this.CheckEmptyOffsetItem.Size = new System.Drawing.Size(311, 22);
            this.CheckEmptyOffsetItem.Text = "Check if offset is empty before inject";
            this.CheckEmptyOffsetItem.Visible = false;
            this.CheckEmptyOffsetItem.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // ZmapOffsetNames
            // 
            this.ZmapOffsetNames.CheckOnClick = true;
            this.ZmapOffsetNames.Name = "ZmapOffsetNames";
            this.ZmapOffsetNames.Size = new System.Drawing.Size(311, 22);
            this.ZmapOffsetNames.Text = "Change binary zmap names to their offsets";
            this.ZmapOffsetNames.Visible = false;
            this.ZmapOffsetNames.Click += new System.EventHandler(this.changeBinaryZmapNamesToTheirOffsetsToolStripMenuItem_Click);
            // 
            // noDummyPointsInCutsceneCamerasToolStripMenuItem
            // 
            this.noDummyPointsInCutsceneCamerasToolStripMenuItem.CheckOnClick = true;
            this.noDummyPointsInCutsceneCamerasToolStripMenuItem.Name = "noDummyPointsInCutsceneCamerasToolStripMenuItem";
            this.noDummyPointsInCutsceneCamerasToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.noDummyPointsInCutsceneCamerasToolStripMenuItem.Text = "No dummy points in cutscene cameras";
            this.noDummyPointsInCutsceneCamerasToolStripMenuItem.Click += new System.EventHandler(this.noDummyPointsInCutsceneCamerasToolStripMenuItem_Click);
            // 
            // dListCullingMenuItem
            // 
            this.dListCullingMenuItem.CheckOnClick = true;
            this.dListCullingMenuItem.Name = "dListCullingMenuItem";
            this.dListCullingMenuItem.Size = new System.Drawing.Size(311, 22);
            this.dListCullingMenuItem.Text = "DList culling command";
            this.dListCullingMenuItem.Click += new System.EventHandler(this.dListCullingMenuItem_Click);
            // 
            // triplicateCollisionBoundsToolStripMenuItem
            // 
            this.triplicateCollisionBoundsToolStripMenuItem.CheckOnClick = true;
            this.triplicateCollisionBoundsToolStripMenuItem.Name = "triplicateCollisionBoundsToolStripMenuItem";
            this.triplicateCollisionBoundsToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.triplicateCollisionBoundsToolStripMenuItem.Text = "Triplicate Collision Bounds";
            this.triplicateCollisionBoundsToolStripMenuItem.Click += new System.EventHandler(this.triplicateCollisionBoundsToolStripMenuItem_Click);
            // 
            // AutoFixErrorsStripMenuItem3
            // 
            this.AutoFixErrorsStripMenuItem3.CheckOnClick = true;
            this.AutoFixErrorsStripMenuItem3.Name = "AutoFixErrorsStripMenuItem3";
            this.AutoFixErrorsStripMenuItem3.Size = new System.Drawing.Size(311, 22);
            this.AutoFixErrorsStripMenuItem3.Text = "Auto Fix Common Errors on Inject";
            this.AutoFixErrorsStripMenuItem3.ToolTipText = "Fixes some errors on inject that would crash ingame otherwise";
            this.AutoFixErrorsStripMenuItem3.Click += new System.EventHandler(this.AutoFixErrorsStripMenuItem3_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(308, 6);
            // 
            // DegreesMenuItem
            // 
            this.DegreesMenuItem.CheckOnClick = true;
            this.DegreesMenuItem.Name = "DegreesMenuItem";
            this.DegreesMenuItem.Size = new System.Drawing.Size(311, 22);
            this.DegreesMenuItem.Text = "Multiply degrees by 182.04~";
            this.DegreesMenuItem.Click += new System.EventHandler(this.DegreesMenuItemClick);
            // 
            // disableWaterboxMouseMovementToolStripMenuItem
            // 
            this.disableWaterboxMouseMovementToolStripMenuItem.CheckOnClick = true;
            this.disableWaterboxMouseMovementToolStripMenuItem.Name = "disableWaterboxMouseMovementToolStripMenuItem";
            this.disableWaterboxMouseMovementToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.disableWaterboxMouseMovementToolStripMenuItem.Text = "Disable waterbox mouse movement";
            this.disableWaterboxMouseMovementToolStripMenuItem.Click += new System.EventHandler(this.disableWaterboxMouseMovementToolStripMenuItem_Click);
            // 
            // showRotationValuesAsHexadecimalToolStripMenuItem
            // 
            this.showRotationValuesAsHexadecimalToolStripMenuItem.CheckOnClick = true;
            this.showRotationValuesAsHexadecimalToolStripMenuItem.Name = "showRotationValuesAsHexadecimalToolStripMenuItem";
            this.showRotationValuesAsHexadecimalToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.showRotationValuesAsHexadecimalToolStripMenuItem.Text = "Show rotation values as hexadecimal";
            this.showRotationValuesAsHexadecimalToolStripMenuItem.Click += new System.EventHandler(this.showRotationValuesAsHexadecimalToolStripMenuItem_Click);
            // 
            // autosaveSceneXmlToolStripMenuItem
            // 
            this.autosaveSceneXmlToolStripMenuItem.CheckOnClick = true;
            this.autosaveSceneXmlToolStripMenuItem.Name = "autosaveSceneXmlToolStripMenuItem";
            this.autosaveSceneXmlToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.autosaveSceneXmlToolStripMenuItem.Text = "Enable autosave.xml";
            this.autosaveSceneXmlToolStripMenuItem.Click += new System.EventHandler(this.autosaveSceneXmlToolStripMenuItem_Click);
            // 
            // dIsplayUndocumentedCutsceneVarsToolStripMenuItem
            // 
            this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem.CheckOnClick = true;
            this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem.Enabled = false;
            this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem.Name = "dIsplayUndocumentedCutsceneVarsToolStripMenuItem";
            this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem.Text = "DIsplay undocumented Cutscene Vars";
            this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem.Visible = false;
            this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem.Click += new System.EventHandler(this.dIsplayUndocumentedCutsceneVarsToolStripMenuItem_Click);
            // 
            // sharpPixelatedTexturesToolStripMenuItem
            // 
            this.sharpPixelatedTexturesToolStripMenuItem.CheckOnClick = true;
            this.sharpPixelatedTexturesToolStripMenuItem.Enabled = false;
            this.sharpPixelatedTexturesToolStripMenuItem.Name = "sharpPixelatedTexturesToolStripMenuItem";
            this.sharpPixelatedTexturesToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.sharpPixelatedTexturesToolStripMenuItem.Text = "Sharp (Pixelated) Textures";
            this.sharpPixelatedTexturesToolStripMenuItem.Visible = false;
            this.sharpPixelatedTexturesToolStripMenuItem.Click += new System.EventHandler(this.sharpPixelatedTexturesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showReadmeToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.showControlsToolStripMenuItem,
            this.iGotACrashToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // showReadmeToolStripMenuItem
            // 
            this.showReadmeToolStripMenuItem.Name = "showReadmeToolStripMenuItem";
            this.showReadmeToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.showReadmeToolStripMenuItem.Text = "&Show Readme";
            this.showReadmeToolStripMenuItem.Click += new System.EventHandler(this.showReadmeToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // showControlsToolStripMenuItem
            // 
            this.showControlsToolStripMenuItem.Name = "showControlsToolStripMenuItem";
            this.showControlsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.showControlsToolStripMenuItem.Text = "Show Controls";
            this.showControlsToolStripMenuItem.Click += new System.EventHandler(this.showControlsToolStripMenuItem_Click);
            // 
            // iGotACrashToolStripMenuItem
            // 
            this.iGotACrashToolStripMenuItem.Name = "iGotACrashToolStripMenuItem";
            this.iGotACrashToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.iGotACrashToolStripMenuItem.Text = "Getting crashes? go here";
            this.iGotACrashToolStripMenuItem.Click += new System.EventHandler(this.iGotACrashToolStripMenuItem_Click);
            // 
            // extraToolStripMenuItem
            // 
            this.extraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem,
            this.displaySwitchFlagsUsedByAllRoomsToolStripMenuItem,
            this.AddObjectToAllRoomsMenuItem,
            this.ReloadXMLMenuItem,
            this.DefaultEnvironmentMenuItem,
            this.HWWindMenuItem,
            this.CopyFirstRoomSettingsMenuItem,
            this.setRoomsToUseEnvironment1ToolStripMenuItem,
            this.autoplaceDoorsToolStripMenuItem,
            this.clearAllGroupSettingsToolStripMenuItem,
            this.dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem,
            this.dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem,
            this.dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem});
            this.extraToolStripMenuItem.Enabled = false;
            this.extraToolStripMenuItem.Name = "extraToolStripMenuItem";
            this.extraToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.extraToolStripMenuItem.Text = "Extra";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickTestToolStripMenuItem,
            this.createPathwaysForEachBoundingBoxToolStripMenuItem,
            this.openCutsceneRawDataToolStripMenuItem,
            this.exportCutsceneRawDataToolStripMenuItem,
            this.exportAszobjToolStripMenuItem,
            this.importEnvironmentsToolStripMenuItem,
            this.importActorsAndObjectsOfZmapToolStripMenuItem,
            this.importCollisionFromzsceneToolStripMenuItem,
            this.importCamerasAndWaterboxFromzsceneToolStripMenuItem,
            this.importTransitionsAndSpawnsFromzsceneToolStripMenuItem,
            this.importPathwaysFromzsceneToolStripMenuItem,
            this.importActorCutscenesFromzsceneToolStripMenuItem,
            this.addEmptySpaceInSceneHeaderToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(356, 22);
            this.debugToolStripMenuItem.Text = "Related to external files / zobj exporting";
            // 
            // quickTestToolStripMenuItem
            // 
            this.quickTestToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnemyTestToolStripMenuItem,
            this.puzzleTestToolStripMenuItem,
            this.EasterEggToolStripMenuItem});
            this.quickTestToolStripMenuItem.Name = "quickTestToolStripMenuItem";
            this.quickTestToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.quickTestToolStripMenuItem.Text = "Fill room with actors...";
            // 
            // EnemyTestToolStripMenuItem
            // 
            this.EnemyTestToolStripMenuItem.Name = "EnemyTestToolStripMenuItem";
            this.EnemyTestToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.EnemyTestToolStripMenuItem.Text = "Enemies (Clear Flag)";
            this.EnemyTestToolStripMenuItem.Click += new System.EventHandler(this.EnemyTest);
            // 
            // puzzleTestToolStripMenuItem
            // 
            this.puzzleTestToolStripMenuItem.Name = "puzzleTestToolStripMenuItem";
            this.puzzleTestToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.puzzleTestToolStripMenuItem.Text = "Silver Rupees (Switch Flag)";
            this.puzzleTestToolStripMenuItem.Click += new System.EventHandler(this.EnemyTest);
            // 
            // EasterEggToolStripMenuItem
            // 
            this.EasterEggToolStripMenuItem.Enabled = false;
            this.EasterEggToolStripMenuItem.Name = "EasterEggToolStripMenuItem";
            this.EasterEggToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.EasterEggToolStripMenuItem.Text = "????";
            this.EasterEggToolStripMenuItem.Click += new System.EventHandler(this.EnemyTest);
            // 
            // createPathwaysForEachBoundingBoxToolStripMenuItem
            // 
            this.createPathwaysForEachBoundingBoxToolStripMenuItem.Name = "createPathwaysForEachBoundingBoxToolStripMenuItem";
            this.createPathwaysForEachBoundingBoxToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.createPathwaysForEachBoundingBoxToolStripMenuItem.Text = "Create pathway for selected DList BBox";
            this.createPathwaysForEachBoundingBoxToolStripMenuItem.Click += new System.EventHandler(this.createPathwaysForEachBoundingBoxToolStripMenuItem_Click);
            // 
            // openCutsceneRawDataToolStripMenuItem
            // 
            this.openCutsceneRawDataToolStripMenuItem.Name = "openCutsceneRawDataToolStripMenuItem";
            this.openCutsceneRawDataToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.openCutsceneRawDataToolStripMenuItem.Text = "Open cutscene raw data (OoT)";
            this.openCutsceneRawDataToolStripMenuItem.Click += new System.EventHandler(this.openCutsceneRawDataToolStripMenuItem_Click);
            // 
            // exportCutsceneRawDataToolStripMenuItem
            // 
            this.exportCutsceneRawDataToolStripMenuItem.Name = "exportCutsceneRawDataToolStripMenuItem";
            this.exportCutsceneRawDataToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.exportCutsceneRawDataToolStripMenuItem.Text = "Export cutscene raw data";
            this.exportCutsceneRawDataToolStripMenuItem.Click += new System.EventHandler(this.exportCutsceneRawDataToolStripMenuItem_DisplayStyleChanged);
            // 
            // exportAszobjToolStripMenuItem
            // 
            this.exportAszobjToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bank0x06ToolStripMenuItem,
            this.bank0x05ToolStripMenuItem,
            this.bank0x04ToolStripMenuItem,
            this.writeCollisionToolStripMenuItem});
            this.exportAszobjToolStripMenuItem.Name = "exportAszobjToolStripMenuItem";
            this.exportAszobjToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.exportAszobjToolStripMenuItem.Text = "Export current room as .zobj";
            // 
            // bank0x06ToolStripMenuItem
            // 
            this.bank0x06ToolStripMenuItem.Name = "bank0x06ToolStripMenuItem";
            this.bank0x06ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.bank0x06ToolStripMenuItem.Text = "Bank 0x06 (most zobj)";
            this.bank0x06ToolStripMenuItem.Click += new System.EventHandler(this.exportAszobjToolStripMenuItem_Click);
            // 
            // bank0x05ToolStripMenuItem
            // 
            this.bank0x05ToolStripMenuItem.Name = "bank0x05ToolStripMenuItem";
            this.bank0x05ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.bank0x05ToolStripMenuItem.Text = "Bank 0x05 (0002, 0003)";
            this.bank0x05ToolStripMenuItem.Click += new System.EventHandler(this.exportAszobjToolStripMenuItem_Click);
            // 
            // bank0x04ToolStripMenuItem
            // 
            this.bank0x04ToolStripMenuItem.Name = "bank0x04ToolStripMenuItem";
            this.bank0x04ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.bank0x04ToolStripMenuItem.Text = "Bank 0x04 (0001)";
            this.bank0x04ToolStripMenuItem.Click += new System.EventHandler(this.exportAszobjToolStripMenuItem_Click);
            // 
            // writeCollisionToolStripMenuItem
            // 
            this.writeCollisionToolStripMenuItem.Checked = true;
            this.writeCollisionToolStripMenuItem.CheckOnClick = true;
            this.writeCollisionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.writeCollisionToolStripMenuItem.Name = "writeCollisionToolStripMenuItem";
            this.writeCollisionToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.writeCollisionToolStripMenuItem.Text = "Write Collision";
            this.writeCollisionToolStripMenuItem.Click += new System.EventHandler(this.writeCollisionToolStripMenuItem_Click);
            // 
            // importEnvironmentsToolStripMenuItem
            // 
            this.importEnvironmentsToolStripMenuItem.Name = "importEnvironmentsToolStripMenuItem";
            this.importEnvironmentsToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.importEnvironmentsToolStripMenuItem.Text = "Import Environments from .zscene";
            this.importEnvironmentsToolStripMenuItem.Click += new System.EventHandler(this.importEnvironmentsToolStripMenuItem_Click);
            // 
            // importActorsAndObjectsOfZmapToolStripMenuItem
            // 
            this.importActorsAndObjectsOfZmapToolStripMenuItem.Name = "importActorsAndObjectsOfZmapToolStripMenuItem";
            this.importActorsAndObjectsOfZmapToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.importActorsAndObjectsOfZmapToolStripMenuItem.Text = "Import Actors and objects from .zmap";
            this.importActorsAndObjectsOfZmapToolStripMenuItem.Click += new System.EventHandler(this.importActorsAndObjectsOfZmapToolStripMenuItem_Click);
            // 
            // importCollisionFromzsceneToolStripMenuItem
            // 
            this.importCollisionFromzsceneToolStripMenuItem.Name = "importCollisionFromzsceneToolStripMenuItem";
            this.importCollisionFromzsceneToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.importCollisionFromzsceneToolStripMenuItem.Text = "Import Collision from .zscene";
            this.importCollisionFromzsceneToolStripMenuItem.Click += new System.EventHandler(this.importCollisionFromzsceneToolStripMenuItem_Click);
            // 
            // importCamerasAndWaterboxFromzsceneToolStripMenuItem
            // 
            this.importCamerasAndWaterboxFromzsceneToolStripMenuItem.Name = "importCamerasAndWaterboxFromzsceneToolStripMenuItem";
            this.importCamerasAndWaterboxFromzsceneToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.importCamerasAndWaterboxFromzsceneToolStripMenuItem.Text = "Import Cameras and Waterbox from .zscene";
            this.importCamerasAndWaterboxFromzsceneToolStripMenuItem.Click += new System.EventHandler(this.importCamerasAndWaterboxFromzsceneToolStripMenuItem_Click);
            // 
            // importTransitionsAndSpawnsFromzsceneToolStripMenuItem
            // 
            this.importTransitionsAndSpawnsFromzsceneToolStripMenuItem.Name = "importTransitionsAndSpawnsFromzsceneToolStripMenuItem";
            this.importTransitionsAndSpawnsFromzsceneToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.importTransitionsAndSpawnsFromzsceneToolStripMenuItem.Text = "Import Transitions and Spawns from .zscene";
            this.importTransitionsAndSpawnsFromzsceneToolStripMenuItem.Click += new System.EventHandler(this.importTransitionsAndSpawnsFromzsceneToolStripMenuItem_Click);
            // 
            // importPathwaysFromzsceneToolStripMenuItem
            // 
            this.importPathwaysFromzsceneToolStripMenuItem.Name = "importPathwaysFromzsceneToolStripMenuItem";
            this.importPathwaysFromzsceneToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.importPathwaysFromzsceneToolStripMenuItem.Text = "Import Pathways from .zscene";
            this.importPathwaysFromzsceneToolStripMenuItem.Click += new System.EventHandler(this.importPathwaysFromzsceneToolStripMenuItem_Click);
            // 
            // importActorCutscenesFromzsceneToolStripMenuItem
            // 
            this.importActorCutscenesFromzsceneToolStripMenuItem.Name = "importActorCutscenesFromzsceneToolStripMenuItem";
            this.importActorCutscenesFromzsceneToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.importActorCutscenesFromzsceneToolStripMenuItem.Text = "Import Actor Cutscenes from .zscene (MM)";
            this.importActorCutscenesFromzsceneToolStripMenuItem.Click += new System.EventHandler(this.importActorCutscenesFromzsceneToolStripMenuItem_Click);
            // 
            // addEmptySpaceInSceneHeaderToolStripMenuItem
            // 
            this.addEmptySpaceInSceneHeaderToolStripMenuItem.CheckOnClick = true;
            this.addEmptySpaceInSceneHeaderToolStripMenuItem.Name = "addEmptySpaceInSceneHeaderToolStripMenuItem";
            this.addEmptySpaceInSceneHeaderToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.addEmptySpaceInSceneHeaderToolStripMenuItem.Text = "Add empty space in scene header / zobj";
            this.addEmptySpaceInSceneHeaderToolStripMenuItem.Click += new System.EventHandler(this.addEmptySpaceInSceneHeaderToolStripMenuItem_Click);
            // 
            // displaySwitchFlagsUsedByAllRoomsToolStripMenuItem
            // 
            this.displaySwitchFlagsUsedByAllRoomsToolStripMenuItem.Name = "displaySwitchFlagsUsedByAllRoomsToolStripMenuItem";
            this.displaySwitchFlagsUsedByAllRoomsToolStripMenuItem.Size = new System.Drawing.Size(356, 22);
            this.displaySwitchFlagsUsedByAllRoomsToolStripMenuItem.Text = "Display Flags used by all rooms (Flag Log)";
            this.displaySwitchFlagsUsedByAllRoomsToolStripMenuItem.Click += new System.EventHandler(this.displaySwitchFlagsUsedByAllRoomsToolStripMenuItem_Click);
            // 
            // AddObjectToAllRoomsMenuItem
            // 
            this.AddObjectToAllRoomsMenuItem.Name = "AddObjectToAllRoomsMenuItem";
            this.AddObjectToAllRoomsMenuItem.Size = new System.Drawing.Size(356, 22);
            this.AddObjectToAllRoomsMenuItem.Text = "Add object to all rooms";
            this.AddObjectToAllRoomsMenuItem.Click += new System.EventHandler(this.AddObjectToAllRoomsMenuItem_Click);
            // 
            // ReloadXMLMenuItem
            // 
            this.ReloadXMLMenuItem.Name = "ReloadXMLMenuItem";
            this.ReloadXMLMenuItem.Size = new System.Drawing.Size(356, 22);
            this.ReloadXMLMenuItem.Text = "Reload current game XMLs";
            this.ReloadXMLMenuItem.Click += new System.EventHandler(this.ReloadXMLMenuItem_Click);
            // 
            // DefaultEnvironmentMenuItem
            // 
            this.DefaultEnvironmentMenuItem.Name = "DefaultEnvironmentMenuItem";
            this.DefaultEnvironmentMenuItem.Size = new System.Drawing.Size(356, 22);
            this.DefaultEnvironmentMenuItem.Text = "Set default environment list values";
            this.DefaultEnvironmentMenuItem.Click += new System.EventHandler(this.DefaultEnvironmentMenuItem_Click);
            // 
            // HWWindMenuItem
            // 
            this.HWWindMenuItem.Name = "HWWindMenuItem";
            this.HWWindMenuItem.Size = new System.Drawing.Size(356, 22);
            this.HWWindMenuItem.Text = "Reset Wind Values";
            this.HWWindMenuItem.Click += new System.EventHandler(this.HWWindMenuItem_Click);
            // 
            // CopyFirstRoomSettingsMenuItem
            // 
            this.CopyFirstRoomSettingsMenuItem.Name = "CopyFirstRoomSettingsMenuItem";
            this.CopyFirstRoomSettingsMenuItem.Size = new System.Drawing.Size(356, 22);
            this.CopyFirstRoomSettingsMenuItem.Text = "Copy first room env. settings to all rooms";
            this.CopyFirstRoomSettingsMenuItem.Click += new System.EventHandler(this.CopyFirstRoomSettingsMenuItem_Click);
            // 
            // setRoomsToUseEnvironment1ToolStripMenuItem
            // 
            this.setRoomsToUseEnvironment1ToolStripMenuItem.Name = "setRoomsToUseEnvironment1ToolStripMenuItem";
            this.setRoomsToUseEnvironment1ToolStripMenuItem.Size = new System.Drawing.Size(356, 22);
            this.setRoomsToUseEnvironment1ToolStripMenuItem.Text = "Set all rooms to use environment 1 only";
            this.setRoomsToUseEnvironment1ToolStripMenuItem.Click += new System.EventHandler(this.setRoomsToUseEnvironment1ToolStripMenuItem_Click);
            // 
            // autoplaceDoorsToolStripMenuItem
            // 
            this.autoplaceDoorsToolStripMenuItem.Name = "autoplaceDoorsToolStripMenuItem";
            this.autoplaceDoorsToolStripMenuItem.Size = new System.Drawing.Size(356, 22);
            this.autoplaceDoorsToolStripMenuItem.Text = "Auto-place transition actors";
            this.autoplaceDoorsToolStripMenuItem.Click += new System.EventHandler(this.autoplaceDoorsToolStripMenuItem_Click);
            // 
            // clearAllGroupSettingsToolStripMenuItem
            // 
            this.clearAllGroupSettingsToolStripMenuItem.Name = "clearAllGroupSettingsToolStripMenuItem";
            this.clearAllGroupSettingsToolStripMenuItem.Size = new System.Drawing.Size(356, 22);
            this.clearAllGroupSettingsToolStripMenuItem.Text = "Clear all group settings";
            this.clearAllGroupSettingsToolStripMenuItem.Click += new System.EventHandler(this.clearAllGroupSettingsToolStripMenuItem_Click);
            // 
            // dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem
            // 
            this.dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem.Name = "dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem";
            this.dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem.Size = new System.Drawing.Size(356, 22);
            this.dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem.Text = "[DEBUG] Print room actors to clipboard (DunGen)";
            this.dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem.Visible = false;
            this.dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem.Click += new System.EventHandler(this.dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem_Click);
            // 
            // dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem
            // 
            this.dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem.Name = "dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem";
            this.dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem.Size = new System.Drawing.Size(356, 22);
            this.dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem.Text = "[DEBUG] Print environments to clipboard (DunGen)";
            this.dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem.Visible = false;
            this.dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem.Click += new System.EventHandler(this.dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem_Click);
            // 
            // dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem
            // 
            this.dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem.Name = "dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem";
            this.dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem.Size = new System.Drawing.Size(356, 22);
            this.dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem.Text = "[DEBUG] Print room actor rendering to clipboard (SO)";
            this.dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem.Visible = false;
            this.dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem.Click += new System.EventHandler(this.dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem_Click);
            // 
            // nokaToolStripMenuItem
            // 
            this.nokaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patchROMToolStripMenuItem,
            this.entranceTableEditorToolStripMenuItem,
            this.cutsceneTableEditorToolStripMenuItem,
            this.fileCreationEditorToolStripMenuItem,
            this.pauseScreenMapEditorOoTToolStripMenuItem,
            this.dropTableEditorOoTToolStripMenuItem,
            this.restrictionFlagsTableEditorToolStripMenuItem,
            this.clearSceneDmatableToolStripMenuItem,
            this.removeAllRomScenesToolStripMenuItem,
            this.rebuildDmaTableallToolStripMenuItem,
            this.decompressROMToolStripMenuItem,
            this.objectTableEditorToolStripMenuItem,
            this.replaceSceneTitleCardTextureToolStripMenuItem});
            this.nokaToolStripMenuItem.Name = "nokaToolStripMenuItem";
            this.nokaToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.nokaToolStripMenuItem.Text = "Tools";
            // 
            // patchROMToolStripMenuItem
            // 
            this.patchROMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1,
            this.patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1,
            this.additionalLightsFixOoTDebugToolStripMenuItem,
            this.advancedTextureAnimationsOoTDebugToolStripMenuItem,
            this.ExtendDynapolyCountStripMenuItem});
            this.patchROMToolStripMenuItem.Name = "patchROMToolStripMenuItem";
            this.patchROMToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.patchROMToolStripMenuItem.Text = "Patch ROM...";
            // 
            // patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1
            // 
            this.patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1.Name = "patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1";
            this.patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1.Size = new System.Drawing.Size(335, 22);
            this.patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1.Text = "Extend RAM and fix crashing bugs (OoT Debug)";
            this.patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1.Click += new System.EventHandler(this.patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1_Click);
            // 
            // patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1
            // 
            this.patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1.Name = "patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1";
            this.patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1.Size = new System.Drawing.Size(335, 22);
            this.patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1.Text = "Remove debug features (OoT Debug)";
            this.patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1.Click += new System.EventHandler(this.patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1_Click);
            // 
            // additionalLightsFixOoTDebugToolStripMenuItem
            // 
            this.additionalLightsFixOoTDebugToolStripMenuItem.Name = "additionalLightsFixOoTDebugToolStripMenuItem";
            this.additionalLightsFixOoTDebugToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.additionalLightsFixOoTDebugToolStripMenuItem.Text = "2-axis billboard + additional light fix (OoT Debug)";
            this.additionalLightsFixOoTDebugToolStripMenuItem.Click += new System.EventHandler(this.additionalLightsFixOoTDebugToolStripMenuItem_Click);
            // 
            // advancedTextureAnimationsOoTDebugToolStripMenuItem
            // 
            this.advancedTextureAnimationsOoTDebugToolStripMenuItem.Name = "advancedTextureAnimationsOoTDebugToolStripMenuItem";
            this.advancedTextureAnimationsOoTDebugToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.advancedTextureAnimationsOoTDebugToolStripMenuItem.Text = "Advanced texture animations (OoT Debug)";
            this.advancedTextureAnimationsOoTDebugToolStripMenuItem.Click += new System.EventHandler(this.advancedTextureAnimationsOoTDebugToolStripMenuItem_Click);
            // 
            // ExtendDynapolyCountStripMenuItem
            // 
            this.ExtendDynapolyCountStripMenuItem.Name = "ExtendDynapolyCountStripMenuItem";
            this.ExtendDynapolyCountStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.ExtendDynapolyCountStripMenuItem.Text = "Extend Dynapoly Vertex Limit (OoT Debug)";
            this.ExtendDynapolyCountStripMenuItem.Click += new System.EventHandler(this.ExtendDynapolyCountStripMenuItem_Click);
            // 
            // entranceTableEditorToolStripMenuItem
            // 
            this.entranceTableEditorToolStripMenuItem.Name = "entranceTableEditorToolStripMenuItem";
            this.entranceTableEditorToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.entranceTableEditorToolStripMenuItem.Text = "Entrance Table Editor (OoT/MM//z64rom)";
            this.entranceTableEditorToolStripMenuItem.Click += new System.EventHandler(this.rOMEntranceTableEditorToolStripMenuItem_Click);
            // 
            // cutsceneTableEditorToolStripMenuItem
            // 
            this.cutsceneTableEditorToolStripMenuItem.Name = "cutsceneTableEditorToolStripMenuItem";
            this.cutsceneTableEditorToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.cutsceneTableEditorToolStripMenuItem.Text = "Cutscene Table Editor (OoT)";
            this.cutsceneTableEditorToolStripMenuItem.Click += new System.EventHandler(this.cutsceneTableEditorToolStripMenuItem_Click);
            // 
            // fileCreationEditorToolStripMenuItem
            // 
            this.fileCreationEditorToolStripMenuItem.Name = "fileCreationEditorToolStripMenuItem";
            this.fileCreationEditorToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.fileCreationEditorToolStripMenuItem.Text = "Filesave Creation Editor (OoT/z64rom)";
            this.fileCreationEditorToolStripMenuItem.Click += new System.EventHandler(this.fileCreationEditorToolStripMenuItem_Click);
            // 
            // pauseScreenMapEditorOoTToolStripMenuItem
            // 
            this.pauseScreenMapEditorOoTToolStripMenuItem.Name = "pauseScreenMapEditorOoTToolStripMenuItem";
            this.pauseScreenMapEditorOoTToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.pauseScreenMapEditorOoTToolStripMenuItem.Text = "Pause Screen Map Editor (OoT/z64rom)";
            this.pauseScreenMapEditorOoTToolStripMenuItem.Click += new System.EventHandler(this.pauseScreenMapEditorOoTToolStripMenuItem_Click);
            // 
            // dropTableEditorOoTToolStripMenuItem
            // 
            this.dropTableEditorOoTToolStripMenuItem.Name = "dropTableEditorOoTToolStripMenuItem";
            this.dropTableEditorOoTToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.dropTableEditorOoTToolStripMenuItem.Text = "Drop Table Editor (OoT/z64rom)";
            this.dropTableEditorOoTToolStripMenuItem.Click += new System.EventHandler(this.dropTableEditorOoTToolStripMenuItem_Click);
            // 
            // restrictionFlagsTableEditorToolStripMenuItem
            // 
            this.restrictionFlagsTableEditorToolStripMenuItem.Name = "restrictionFlagsTableEditorToolStripMenuItem";
            this.restrictionFlagsTableEditorToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.restrictionFlagsTableEditorToolStripMenuItem.Text = "Restriction flags Table Editor (OoT/MM)";
            this.restrictionFlagsTableEditorToolStripMenuItem.Click += new System.EventHandler(this.restrictionFlagsTableEditorToolStripMenuItem_Click);
            // 
            // clearSceneDmatableToolStripMenuItem
            // 
            this.clearSceneDmatableToolStripMenuItem.Name = "clearSceneDmatableToolStripMenuItem";
            this.clearSceneDmatableToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.clearSceneDmatableToolStripMenuItem.Text = "Clear scene DMA table (OoT/MM)";
            this.clearSceneDmatableToolStripMenuItem.Click += new System.EventHandler(this.clearSceneDmatableToolStripMenuItem_Click);
            // 
            // removeAllRomScenesToolStripMenuItem
            // 
            this.removeAllRomScenesToolStripMenuItem.Name = "removeAllRomScenesToolStripMenuItem";
            this.removeAllRomScenesToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.removeAllRomScenesToolStripMenuItem.Text = "Remove all ROM scenes (OoT/MM/z64rom)";
            this.removeAllRomScenesToolStripMenuItem.Click += new System.EventHandler(this.removeAllRomScenesToolStripMenuItem_Click);
            // 
            // rebuildDmaTableallToolStripMenuItem
            // 
            this.rebuildDmaTableallToolStripMenuItem.Name = "rebuildDmaTableallToolStripMenuItem";
            this.rebuildDmaTableallToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.rebuildDmaTableallToolStripMenuItem.Text = "Rebuild DMA table (OoT/MM)";
            this.rebuildDmaTableallToolStripMenuItem.Click += new System.EventHandler(this.rebuildDmaTableallToolStripMenuItem_Click);
            // 
            // decompressROMToolStripMenuItem
            // 
            this.decompressROMToolStripMenuItem.Name = "decompressROMToolStripMenuItem";
            this.decompressROMToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.decompressROMToolStripMenuItem.Text = "Decompress ROM (OoT/MM)";
            this.decompressROMToolStripMenuItem.Click += new System.EventHandler(this.decompressROMToolStripMenuItem_Click);
            // 
            // objectTableEditorToolStripMenuItem
            // 
            this.objectTableEditorToolStripMenuItem.Enabled = false;
            this.objectTableEditorToolStripMenuItem.Name = "objectTableEditorToolStripMenuItem";
            this.objectTableEditorToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.objectTableEditorToolStripMenuItem.Text = "Object Table Editor";
            this.objectTableEditorToolStripMenuItem.Visible = false;
            this.objectTableEditorToolStripMenuItem.Click += new System.EventHandler(this.objectTableEditorToolStripMenuItem_Click);
            // 
            // replaceSceneTitleCardTextureToolStripMenuItem
            // 
            this.replaceSceneTitleCardTextureToolStripMenuItem.Enabled = false;
            this.replaceSceneTitleCardTextureToolStripMenuItem.Name = "replaceSceneTitleCardTextureToolStripMenuItem";
            this.replaceSceneTitleCardTextureToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.replaceSceneTitleCardTextureToolStripMenuItem.Text = "Replace scene title card texture (OoT)";
            this.replaceSceneTitleCardTextureToolStripMenuItem.Visible = false;
            this.replaceSceneTitleCardTextureToolStripMenuItem.Click += new System.EventHandler(this.replaceSceneTitleCardTextureToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabRooms);
            this.tabControl1.Controls.Add(this.tabSceneEnv);
            this.tabControl1.Controls.Add(this.tabRoomEnv);
            this.tabControl1.Controls.Add(this.tabCollision);
            this.tabControl1.Controls.Add(this.tabTransitions);
            this.tabControl1.Controls.Add(this.tabPathways);
            this.tabControl1.Controls.Add(this.tabActors);
            this.tabControl1.Controls.Add(this.tabCutscene);
            this.tabControl1.Controls.Add(this.tabAnimations);
            this.tabControl1.Enabled = false;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(48, 18);
            this.tabControl1.Location = new System.Drawing.Point(739, 27);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 3);
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(419, 720);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 3;
            // 
            // tabGeneral
            // 
            this.tabGeneral.BackColor = System.Drawing.Color.White;
            this.tabGeneral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabGeneral.Controls.Add(this.SetTitlecard);
            this.tabGeneral.Controls.Add(this.SetRestrictionFlags);
            this.tabGeneral.Controls.Add(this.AutoInjectOffsetCheckBox);
            this.tabGeneral.Controls.Add(this.ScenenumberTextbox);
            this.tabGeneral.Controls.Add(this.CamerasGroupBox);
            this.tabGeneral.Controls.Add(this.SceneSettingsComboBox);
            this.tabGeneral.Controls.Add(this.SceneFunctionLabel);
            this.tabGeneral.Controls.Add(this.ElfMessageComboBox);
            this.tabGeneral.Controls.Add(this.label42);
            this.tabGeneral.Controls.Add(this.SpecialObjectComboBox);
            this.tabGeneral.Controls.Add(this.label38);
            this.tabGeneral.Controls.Add(this.SimulateN64CheckBox);
            this.tabGeneral.Controls.Add(this.label33);
            this.tabGeneral.Controls.Add(this.WaterboxGroupBox);
            this.tabGeneral.Controls.Add(this.InjectoffsetTextbox);
            this.tabGeneral.Controls.Add(this.label6);
            this.tabGeneral.Controls.Add(this.button4);
            this.tabGeneral.Controls.Add(this.CollisionTextbox);
            this.tabGeneral.Controls.Add(this.label4);
            this.tabGeneral.Controls.Add(this.ScaleNumericbox);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.NameTextbox);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.TextureAnimsGroupBox);
            this.tabGeneral.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabGeneral.Location = new System.Drawing.Point(4, 40);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(411, 676);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            // 
            // SetTitlecard
            // 
            this.SetTitlecard.BackColor = System.Drawing.Color.Transparent;
            this.SetTitlecard.Enabled = false;
            this.SetTitlecard.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SetTitlecard.Location = new System.Drawing.Point(135, 642);
            this.SetTitlecard.Name = "SetTitlecard";
            this.SetTitlecard.Size = new System.Drawing.Size(120, 23);
            this.SetTitlecard.TabIndex = 76;
            this.SetTitlecard.Text = "Set Titlecard";
            this.SetTitlecard.UseVisualStyleBackColor = false;
            this.SetTitlecard.Click += new System.EventHandler(this.SetTitlecard_Click);
            // 
            // SetRestrictionFlags
            // 
            this.SetRestrictionFlags.BackColor = System.Drawing.Color.Transparent;
            this.SetRestrictionFlags.Enabled = false;
            this.SetRestrictionFlags.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SetRestrictionFlags.Location = new System.Drawing.Point(9, 642);
            this.SetRestrictionFlags.Name = "SetRestrictionFlags";
            this.SetRestrictionFlags.Size = new System.Drawing.Size(120, 23);
            this.SetRestrictionFlags.TabIndex = 75;
            this.SetRestrictionFlags.Text = "Set Restriction Flags";
            this.EnvironmentControlTooltip.SetToolTip(this.SetRestrictionFlags, "Note: Scene IDs 65-6C can\'t have restriction flags without having a row in the re" +
        "striction flag table");
            this.SetRestrictionFlags.UseVisualStyleBackColor = false;
            this.SetRestrictionFlags.Click += new System.EventHandler(this.SetRestrictionFlags_Click);
            // 
            // AutoInjectOffsetCheckBox
            // 
            this.AutoInjectOffsetCheckBox.AutoSize = true;
            this.AutoInjectOffsetCheckBox.Location = new System.Drawing.Point(360, 32);
            this.AutoInjectOffsetCheckBox.Name = "AutoInjectOffsetCheckBox";
            this.AutoInjectOffsetCheckBox.Size = new System.Drawing.Size(48, 17);
            this.AutoInjectOffsetCheckBox.TabIndex = 45;
            this.AutoInjectOffsetCheckBox.Text = "Auto";
            this.EnvironmentControlTooltip.SetToolTip(this.AutoInjectOffsetCheckBox, "Enable to get a suggested inject offset");
            this.AutoInjectOffsetCheckBox.UseVisualStyleBackColor = true;
            this.AutoInjectOffsetCheckBox.CheckedChanged += new System.EventHandler(this.AutoInjectOffsetCheckBox_CheckedChanged);
            // 
            // ScenenumberTextbox
            // 
            this.ScenenumberTextbox.AlwaysFireValueChanged = false;
            this.ScenenumberTextbox.DisplayDigits = 1;
            this.ScenenumberTextbox.DoValueRollover = false;
            this.ScenenumberTextbox.Hexadecimal = true;
            this.ScenenumberTextbox.IncrementMouseWheel = 1;
            this.ScenenumberTextbox.Location = new System.Drawing.Point(322, 58);
            this.ScenenumberTextbox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ScenenumberTextbox.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ScenenumberTextbox.Name = "ScenenumberTextbox";
            this.ScenenumberTextbox.ShiftMultiplier = 1;
            this.ScenenumberTextbox.Size = new System.Drawing.Size(60, 20);
            this.ScenenumberTextbox.TabIndex = 42;
            this.EnvironmentControlTooltip.SetToolTip(this.ScenenumberTextbox, "High values will render the rom unusable (max 6D for debug ROM)");
            this.ScenenumberTextbox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ScenenumberTextbox.ValueChanged += new System.EventHandler(this.ScenenumberTextbox_ValueChanged);
            // 
            // CamerasGroupBox
            // 
            this.CamerasGroupBox.Controls.Add(this.CameraPanel);
            this.CamerasGroupBox.Controls.Add(this.DeleteCameraButton);
            this.CamerasGroupBox.Controls.Add(this.AddCameraButton);
            this.CamerasGroupBox.Controls.Add(this.CameraSelect);
            this.CamerasGroupBox.Controls.Add(this.niceLine8);
            this.CamerasGroupBox.Controls.Add(this.CameraPanel2);
            this.CamerasGroupBox.Location = new System.Drawing.Point(6, 397);
            this.CamerasGroupBox.Name = "CamerasGroupBox";
            this.CamerasGroupBox.Size = new System.Drawing.Size(399, 239);
            this.CamerasGroupBox.TabIndex = 29;
            this.CamerasGroupBox.TabStop = false;
            this.CamerasGroupBox.Text = "Cameras";
            // 
            // CameraPanel
            // 
            this.CameraPanel.Controls.Add(this.CameraPage2);
            this.CameraPanel.Controls.Add(this.CameraCopyViewport);
            this.CameraPanel.Controls.Add(this.CameraView);
            this.CameraPanel.Controls.Add(this.label104);
            this.CameraPanel.Controls.Add(this.CameraUnk1);
            this.CameraPanel.Controls.Add(this.label103);
            this.CameraPanel.Controls.Add(this.CameraUnk2);
            this.CameraPanel.Controls.Add(this.label78);
            this.CameraPanel.Controls.Add(this.CameraFov);
            this.CameraPanel.Controls.Add(this.CameraType);
            this.CameraPanel.Controls.Add(this.label77);
            this.CameraPanel.Controls.Add(this.CameraZRot);
            this.CameraPanel.Controls.Add(this.label71);
            this.CameraPanel.Controls.Add(this.label72);
            this.CameraPanel.Controls.Add(this.CameraZPos);
            this.CameraPanel.Controls.Add(this.label73);
            this.CameraPanel.Controls.Add(this.label74);
            this.CameraPanel.Controls.Add(this.CameraXRot);
            this.CameraPanel.Controls.Add(this.label75);
            this.CameraPanel.Controls.Add(this.CameraYPos);
            this.CameraPanel.Controls.Add(this.CameraYRot);
            this.CameraPanel.Controls.Add(this.label76);
            this.CameraPanel.Controls.Add(this.CameraXPos);
            this.CameraPanel.Location = new System.Drawing.Point(3, 58);
            this.CameraPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CameraPanel.Name = "CameraPanel";
            this.CameraPanel.Size = new System.Drawing.Size(370, 173);
            this.CameraPanel.TabIndex = 21;
            // 
            // CameraPage2
            // 
            this.CameraPage2.Location = new System.Drawing.Point(18, 141);
            this.CameraPage2.Name = "CameraPage2";
            this.CameraPage2.Size = new System.Drawing.Size(133, 23);
            this.CameraPage2.TabIndex = 73;
            this.CameraPage2.Text = "Go to Page 2";
            this.CameraPage2.UseVisualStyleBackColor = true;
            this.CameraPage2.Visible = false;
            this.CameraPage2.Click += new System.EventHandler(this.CameraPage2_Click);
            // 
            // CameraCopyViewport
            // 
            this.CameraCopyViewport.Location = new System.Drawing.Point(157, 141);
            this.CameraCopyViewport.Name = "CameraCopyViewport";
            this.CameraCopyViewport.Size = new System.Drawing.Size(133, 23);
            this.CameraCopyViewport.TabIndex = 72;
            this.CameraCopyViewport.Text = "Copy viewport position";
            this.CameraCopyViewport.UseVisualStyleBackColor = true;
            this.CameraCopyViewport.Click += new System.EventHandler(this.CameraCopyViewport_Click);
            // 
            // CameraView
            // 
            this.CameraView.Enabled = false;
            this.CameraView.Location = new System.Drawing.Point(296, 138);
            this.CameraView.Name = "CameraView";
            this.CameraView.Size = new System.Drawing.Size(68, 28);
            this.CameraView.TabIndex = 71;
            this.CameraView.Text = "View";
            this.CameraView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CameraView.UseVisualStyleBackColor = true;
            this.CameraView.Click += new System.EventHandler(this.CameraView_Click);
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Enabled = false;
            this.label104.Location = new System.Drawing.Point(3, 112);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(68, 13);
            this.label104.TabIndex = 43;
            this.label104.Text = "Background:";
            // 
            // CameraUnk1
            // 
            this.CameraUnk1.AlwaysFireValueChanged = false;
            this.CameraUnk1.DisplayDigits = 1;
            this.CameraUnk1.DoValueRollover = false;
            this.CameraUnk1.Enabled = false;
            this.CameraUnk1.Hexadecimal = true;
            this.CameraUnk1.IncrementMouseWheel = 1;
            this.CameraUnk1.Location = new System.Drawing.Point(74, 110);
            this.CameraUnk1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk1.Name = "CameraUnk1";
            this.CameraUnk1.ShiftMultiplier = 1;
            this.CameraUnk1.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk1.TabIndex = 42;
            this.CameraUnk1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk1.ValueChanged += new System.EventHandler(this.CameraUnk1_ValueChanged);
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Enabled = false;
            this.label103.Location = new System.Drawing.Point(196, 114);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(30, 13);
            this.label103.TabIndex = 41;
            this.label103.Text = "Unk:";
            // 
            // CameraUnk2
            // 
            this.CameraUnk2.AlwaysFireValueChanged = false;
            this.CameraUnk2.DisplayDigits = 1;
            this.CameraUnk2.DoValueRollover = false;
            this.CameraUnk2.Enabled = false;
            this.CameraUnk2.Hexadecimal = true;
            this.CameraUnk2.IncrementMouseWheel = 1;
            this.CameraUnk2.Location = new System.Drawing.Point(267, 112);
            this.CameraUnk2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk2.Name = "CameraUnk2";
            this.CameraUnk2.ShiftMultiplier = 1;
            this.CameraUnk2.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk2.TabIndex = 40;
            this.CameraUnk2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk2.ValueChanged += new System.EventHandler(this.CameraUnk2_ValueChanged);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Enabled = false;
            this.label78.Location = new System.Drawing.Point(196, 86);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(31, 13);
            this.label78.TabIndex = 21;
            this.label78.Text = "FOV:";
            // 
            // CameraFov
            // 
            this.CameraFov.AlwaysFireValueChanged = false;
            this.CameraFov.DisplayDigits = 1;
            this.CameraFov.DoValueRollover = false;
            this.CameraFov.Enabled = false;
            this.CameraFov.IncrementMouseWheel = 1;
            this.CameraFov.Location = new System.Drawing.Point(267, 84);
            this.CameraFov.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CameraFov.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CameraFov.Name = "CameraFov";
            this.CameraFov.ShiftMultiplier = 1;
            this.CameraFov.Size = new System.Drawing.Size(100, 20);
            this.CameraFov.TabIndex = 20;
            this.CameraFov.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraFov.ValueChanged += new System.EventHandler(this.CameraFov_ValueChanged);
            // 
            // CameraType
            // 
            this.CameraType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CameraType.FormattingEnabled = true;
            this.CameraType.Location = new System.Drawing.Point(44, 83);
            this.CameraType.Name = "CameraType";
            this.CameraType.Size = new System.Drawing.Size(130, 21);
            this.CameraType.TabIndex = 39;
            this.CameraType.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.CameraType.SelectionChangeCommitted += new System.EventHandler(this.CameraType_SelectionChangeCommitted);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Enabled = false;
            this.label77.Location = new System.Drawing.Point(196, 59);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(60, 13);
            this.label77.TabIndex = 21;
            this.label77.Text = "Z Rotation:";
            // 
            // CameraZRot
            // 
            this.CameraZRot.AlwaysFireValueChanged = false;
            this.CameraZRot.DisplayDigits = 1;
            this.CameraZRot.DoValueRollover = false;
            this.CameraZRot.Enabled = false;
            this.CameraZRot.Increment = new decimal(new int[] {
            182,
            0,
            0,
            0});
            this.CameraZRot.IncrementMouseWheel = 182;
            this.CameraZRot.Location = new System.Drawing.Point(267, 57);
            this.CameraZRot.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CameraZRot.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CameraZRot.Name = "CameraZRot";
            this.CameraZRot.ShiftMultiplier = 1;
            this.CameraZRot.Size = new System.Drawing.Size(100, 20);
            this.CameraZRot.TabIndex = 20;
            this.CameraZRot.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraZRot.ValueChanged += new System.EventHandler(this.CameraZRot_ValueChanged);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Enabled = false;
            this.label71.Location = new System.Drawing.Point(3, 88);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(34, 13);
            this.label71.TabIndex = 18;
            this.label71.Text = "Type:";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Enabled = false;
            this.label72.Location = new System.Drawing.Point(3, 7);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(57, 13);
            this.label72.TabIndex = 7;
            this.label72.Text = "X Position:";
            // 
            // CameraZPos
            // 
            this.CameraZPos.AlwaysFireValueChanged = false;
            this.CameraZPos.DisplayDigits = 1;
            this.CameraZPos.DoValueRollover = false;
            this.CameraZPos.Enabled = false;
            this.CameraZPos.IncrementMouseWheel = 1;
            this.CameraZPos.Location = new System.Drawing.Point(74, 57);
            this.CameraZPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CameraZPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CameraZPos.Name = "CameraZPos";
            this.CameraZPos.ShiftMultiplier = 20;
            this.CameraZPos.Size = new System.Drawing.Size(100, 20);
            this.CameraZPos.TabIndex = 12;
            this.CameraZPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraZPos.ValueChanged += new System.EventHandler(this.CameraZPos_ValueChanged);
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Enabled = false;
            this.label73.Location = new System.Drawing.Point(3, 59);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(57, 13);
            this.label73.TabIndex = 11;
            this.label73.Text = "Z Position:";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Enabled = false;
            this.label74.Location = new System.Drawing.Point(3, 33);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(57, 13);
            this.label74.TabIndex = 9;
            this.label74.Text = "Y Position:";
            // 
            // CameraXRot
            // 
            this.CameraXRot.AlwaysFireValueChanged = false;
            this.CameraXRot.DisplayDigits = 1;
            this.CameraXRot.DoValueRollover = false;
            this.CameraXRot.Enabled = false;
            this.CameraXRot.Increment = new decimal(new int[] {
            182,
            0,
            0,
            0});
            this.CameraXRot.IncrementMouseWheel = 182;
            this.CameraXRot.Location = new System.Drawing.Point(267, 5);
            this.CameraXRot.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CameraXRot.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CameraXRot.Name = "CameraXRot";
            this.CameraXRot.ShiftMultiplier = 1;
            this.CameraXRot.Size = new System.Drawing.Size(100, 20);
            this.CameraXRot.TabIndex = 13;
            this.CameraXRot.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraXRot.ValueChanged += new System.EventHandler(this.CameraXRot_ValueChanged);
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Enabled = false;
            this.label75.Location = new System.Drawing.Point(196, 33);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(60, 13);
            this.label75.TabIndex = 17;
            this.label75.Text = "Y Rotation:";
            // 
            // CameraYPos
            // 
            this.CameraYPos.AlwaysFireValueChanged = false;
            this.CameraYPos.DisplayDigits = 1;
            this.CameraYPos.DoValueRollover = false;
            this.CameraYPos.Enabled = false;
            this.CameraYPos.IncrementMouseWheel = 1;
            this.CameraYPos.Location = new System.Drawing.Point(74, 31);
            this.CameraYPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CameraYPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CameraYPos.Name = "CameraYPos";
            this.CameraYPos.ShiftMultiplier = 20;
            this.CameraYPos.Size = new System.Drawing.Size(100, 20);
            this.CameraYPos.TabIndex = 11;
            this.CameraYPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraYPos.ValueChanged += new System.EventHandler(this.CameraYPos_ValueChanged);
            // 
            // CameraYRot
            // 
            this.CameraYRot.AlwaysFireValueChanged = false;
            this.CameraYRot.DisplayDigits = 1;
            this.CameraYRot.DoValueRollover = false;
            this.CameraYRot.Enabled = false;
            this.CameraYRot.Increment = new decimal(new int[] {
            182,
            0,
            0,
            0});
            this.CameraYRot.IncrementMouseWheel = 182;
            this.CameraYRot.Location = new System.Drawing.Point(267, 31);
            this.CameraYRot.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CameraYRot.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CameraYRot.Name = "CameraYRot";
            this.CameraYRot.ShiftMultiplier = 1;
            this.CameraYRot.Size = new System.Drawing.Size(100, 20);
            this.CameraYRot.TabIndex = 14;
            this.CameraYRot.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraYRot.ValueChanged += new System.EventHandler(this.CameraYRot_ValueChanged);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Enabled = false;
            this.label76.Location = new System.Drawing.Point(196, 7);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(60, 13);
            this.label76.TabIndex = 13;
            this.label76.Text = "X Rotation:";
            // 
            // CameraXPos
            // 
            this.CameraXPos.AlwaysFireValueChanged = false;
            this.CameraXPos.DisplayDigits = 1;
            this.CameraXPos.DoValueRollover = false;
            this.CameraXPos.Enabled = false;
            this.CameraXPos.IncrementMouseWheel = 1;
            this.CameraXPos.Location = new System.Drawing.Point(74, 5);
            this.CameraXPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CameraXPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CameraXPos.Name = "CameraXPos";
            this.CameraXPos.ShiftMultiplier = 20;
            this.CameraXPos.Size = new System.Drawing.Size(100, 20);
            this.CameraXPos.TabIndex = 10;
            this.CameraXPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraXPos.ValueChanged += new System.EventHandler(this.CameraXPos_ValueChanged);
            // 
            // DeleteCameraButton
            // 
            this.DeleteCameraButton.Location = new System.Drawing.Point(250, 19);
            this.DeleteCameraButton.Name = "DeleteCameraButton";
            this.DeleteCameraButton.Size = new System.Drawing.Size(120, 23);
            this.DeleteCameraButton.TabIndex = 9;
            this.DeleteCameraButton.Text = "Delete Camera";
            this.DeleteCameraButton.UseVisualStyleBackColor = true;
            this.DeleteCameraButton.Click += new System.EventHandler(this.DeleteCameraButton_Click);
            // 
            // AddCameraButton
            // 
            this.AddCameraButton.BackColor = System.Drawing.Color.Transparent;
            this.AddCameraButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddCameraButton.Location = new System.Drawing.Point(124, 19);
            this.AddCameraButton.Name = "AddCameraButton";
            this.AddCameraButton.Size = new System.Drawing.Size(120, 23);
            this.AddCameraButton.TabIndex = 8;
            this.AddCameraButton.Text = "Add Camera";
            this.EnvironmentControlTooltip.SetToolTip(this.AddCameraButton, "Hold SHIFT to add in front of camera");
            this.AddCameraButton.UseVisualStyleBackColor = false;
            this.AddCameraButton.Click += new System.EventHandler(this.AddCameraButton_Click);
            // 
            // CameraSelect
            // 
            this.CameraSelect.Hexadecimal = true;
            this.CameraSelect.Location = new System.Drawing.Point(9, 22);
            this.CameraSelect.Name = "CameraSelect";
            this.CameraSelect.Size = new System.Drawing.Size(65, 20);
            this.CameraSelect.TabIndex = 7;
            this.CameraSelect.ValueChanged += new System.EventHandler(this.CameraSelect_ValueChanged);
            // 
            // niceLine8
            // 
            this.niceLine8.Location = new System.Drawing.Point(9, 46);
            this.niceLine8.Name = "niceLine8";
            this.niceLine8.Size = new System.Drawing.Size(384, 15);
            this.niceLine8.TabIndex = 20;
            this.niceLine8.TabStop = false;
            // 
            // CameraPanel2
            // 
            this.CameraPanel2.Controls.Add(this.label121);
            this.CameraPanel2.Controls.Add(this.CameraUnk22);
            this.CameraPanel2.Controls.Add(this.CameraUnk1E);
            this.CameraPanel2.Controls.Add(this.label122);
            this.CameraPanel2.Controls.Add(this.CameraUnk20);
            this.CameraPanel2.Controls.Add(this.label123);
            this.CameraPanel2.Controls.Add(this.CameraPage1);
            this.CameraPanel2.Controls.Add(this.label124);
            this.CameraPanel2.Controls.Add(this.CameraUnk1C);
            this.CameraPanel2.Controls.Add(this.label126);
            this.CameraPanel2.Controls.Add(this.CameraUnk16);
            this.CameraPanel2.Controls.Add(this.label127);
            this.CameraPanel2.Controls.Add(this.label128);
            this.CameraPanel2.Controls.Add(this.CameraUnk18);
            this.CameraPanel2.Controls.Add(this.label129);
            this.CameraPanel2.Controls.Add(this.CameraUnk14);
            this.CameraPanel2.Controls.Add(this.CameraUnk1A);
            this.CameraPanel2.Controls.Add(this.label130);
            this.CameraPanel2.Controls.Add(this.CameraUnk12);
            this.CameraPanel2.Location = new System.Drawing.Point(3, 58);
            this.CameraPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.CameraPanel2.Name = "CameraPanel2";
            this.CameraPanel2.Size = new System.Drawing.Size(370, 173);
            this.CameraPanel2.TabIndex = 74;
            this.CameraPanel2.Visible = false;
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Enabled = false;
            this.label121.Location = new System.Drawing.Point(196, 137);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(56, 13);
            this.label121.TabIndex = 79;
            this.label121.Text = "Unk 0x22:";
            // 
            // CameraUnk22
            // 
            this.CameraUnk22.AlwaysFireValueChanged = false;
            this.CameraUnk22.DisplayDigits = 1;
            this.CameraUnk22.DoValueRollover = false;
            this.CameraUnk22.Enabled = false;
            this.CameraUnk22.Hexadecimal = true;
            this.CameraUnk22.IncrementMouseWheel = 1;
            this.CameraUnk22.Location = new System.Drawing.Point(267, 135);
            this.CameraUnk22.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk22.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk22.Name = "CameraUnk22";
            this.CameraUnk22.ShiftMultiplier = 1;
            this.CameraUnk22.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk22.TabIndex = 78;
            this.CameraUnk22.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk22.ValueChanged += new System.EventHandler(this.CameraUnk22_ValueChanged);
            // 
            // CameraUnk1E
            // 
            this.CameraUnk1E.AlwaysFireValueChanged = false;
            this.CameraUnk1E.DisplayDigits = 1;
            this.CameraUnk1E.DoValueRollover = false;
            this.CameraUnk1E.Enabled = false;
            this.CameraUnk1E.Hexadecimal = true;
            this.CameraUnk1E.IncrementMouseWheel = 1;
            this.CameraUnk1E.Location = new System.Drawing.Point(267, 83);
            this.CameraUnk1E.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk1E.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk1E.Name = "CameraUnk1E";
            this.CameraUnk1E.ShiftMultiplier = 1;
            this.CameraUnk1E.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk1E.TabIndex = 74;
            this.CameraUnk1E.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk1E.ValueChanged += new System.EventHandler(this.CameraUnk1E_ValueChanged);
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Enabled = false;
            this.label122.Location = new System.Drawing.Point(196, 111);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(56, 13);
            this.label122.TabIndex = 77;
            this.label122.Text = "Unk 0x20:";
            // 
            // CameraUnk20
            // 
            this.CameraUnk20.AlwaysFireValueChanged = false;
            this.CameraUnk20.DisplayDigits = 1;
            this.CameraUnk20.DoValueRollover = false;
            this.CameraUnk20.Enabled = false;
            this.CameraUnk20.Hexadecimal = true;
            this.CameraUnk20.IncrementMouseWheel = 1;
            this.CameraUnk20.Location = new System.Drawing.Point(267, 109);
            this.CameraUnk20.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk20.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk20.Name = "CameraUnk20";
            this.CameraUnk20.ShiftMultiplier = 1;
            this.CameraUnk20.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk20.TabIndex = 76;
            this.CameraUnk20.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk20.ValueChanged += new System.EventHandler(this.CameraUnk20_ValueChanged);
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Enabled = false;
            this.label123.Location = new System.Drawing.Point(196, 85);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(57, 13);
            this.label123.TabIndex = 75;
            this.label123.Text = "Unk 0x1E:";
            // 
            // CameraPage1
            // 
            this.CameraPage1.Location = new System.Drawing.Point(18, 141);
            this.CameraPage1.Name = "CameraPage1";
            this.CameraPage1.Size = new System.Drawing.Size(133, 23);
            this.CameraPage1.TabIndex = 73;
            this.CameraPage1.Text = "Go to Page 1";
            this.CameraPage1.UseVisualStyleBackColor = true;
            this.CameraPage1.Visible = false;
            this.CameraPage1.Click += new System.EventHandler(this.CameraPage1_Click);
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Enabled = false;
            this.label124.Location = new System.Drawing.Point(196, 59);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(57, 13);
            this.label124.TabIndex = 21;
            this.label124.Text = "Unk 0x1C:";
            // 
            // CameraUnk1C
            // 
            this.CameraUnk1C.AlwaysFireValueChanged = false;
            this.CameraUnk1C.DisplayDigits = 1;
            this.CameraUnk1C.DoValueRollover = false;
            this.CameraUnk1C.Enabled = false;
            this.CameraUnk1C.Hexadecimal = true;
            this.CameraUnk1C.IncrementMouseWheel = 1;
            this.CameraUnk1C.Location = new System.Drawing.Point(267, 57);
            this.CameraUnk1C.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk1C.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk1C.Name = "CameraUnk1C";
            this.CameraUnk1C.ShiftMultiplier = 1;
            this.CameraUnk1C.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk1C.TabIndex = 20;
            this.CameraUnk1C.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk1C.ValueChanged += new System.EventHandler(this.CameraUnk1C_ValueChanged);
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Enabled = false;
            this.label126.Location = new System.Drawing.Point(3, 7);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(56, 13);
            this.label126.TabIndex = 7;
            this.label126.Text = "Unk 0x12:";
            // 
            // CameraUnk16
            // 
            this.CameraUnk16.AlwaysFireValueChanged = false;
            this.CameraUnk16.DisplayDigits = 1;
            this.CameraUnk16.DoValueRollover = false;
            this.CameraUnk16.Enabled = false;
            this.CameraUnk16.Hexadecimal = true;
            this.CameraUnk16.IncrementMouseWheel = 1;
            this.CameraUnk16.Location = new System.Drawing.Point(74, 57);
            this.CameraUnk16.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk16.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk16.Name = "CameraUnk16";
            this.CameraUnk16.ShiftMultiplier = 1;
            this.CameraUnk16.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk16.TabIndex = 12;
            this.CameraUnk16.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk16.ValueChanged += new System.EventHandler(this.CameraUnk16_ValueChanged);
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Enabled = false;
            this.label127.Location = new System.Drawing.Point(3, 59);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(56, 13);
            this.label127.TabIndex = 11;
            this.label127.Text = "Unk 0x16:";
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Enabled = false;
            this.label128.Location = new System.Drawing.Point(3, 33);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(56, 13);
            this.label128.TabIndex = 9;
            this.label128.Text = "Unk 0x14:";
            // 
            // CameraUnk18
            // 
            this.CameraUnk18.AlwaysFireValueChanged = false;
            this.CameraUnk18.DisplayDigits = 1;
            this.CameraUnk18.DoValueRollover = false;
            this.CameraUnk18.Enabled = false;
            this.CameraUnk18.Hexadecimal = true;
            this.CameraUnk18.IncrementMouseWheel = 1;
            this.CameraUnk18.Location = new System.Drawing.Point(267, 5);
            this.CameraUnk18.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk18.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk18.Name = "CameraUnk18";
            this.CameraUnk18.ShiftMultiplier = 1;
            this.CameraUnk18.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk18.TabIndex = 13;
            this.CameraUnk18.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk18.ValueChanged += new System.EventHandler(this.CameraUnk18_ValueChanged);
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Enabled = false;
            this.label129.Location = new System.Drawing.Point(196, 33);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(57, 13);
            this.label129.TabIndex = 17;
            this.label129.Text = "Unk 0x1A:";
            // 
            // CameraUnk14
            // 
            this.CameraUnk14.AlwaysFireValueChanged = false;
            this.CameraUnk14.DisplayDigits = 1;
            this.CameraUnk14.DoValueRollover = false;
            this.CameraUnk14.Enabled = false;
            this.CameraUnk14.Hexadecimal = true;
            this.CameraUnk14.IncrementMouseWheel = 1;
            this.CameraUnk14.Location = new System.Drawing.Point(74, 31);
            this.CameraUnk14.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk14.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk14.Name = "CameraUnk14";
            this.CameraUnk14.ShiftMultiplier = 1;
            this.CameraUnk14.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk14.TabIndex = 11;
            this.CameraUnk14.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk14.ValueChanged += new System.EventHandler(this.CameraUnk14_ValueChanged);
            // 
            // CameraUnk1A
            // 
            this.CameraUnk1A.AlwaysFireValueChanged = false;
            this.CameraUnk1A.DisplayDigits = 1;
            this.CameraUnk1A.DoValueRollover = false;
            this.CameraUnk1A.Enabled = false;
            this.CameraUnk1A.Hexadecimal = true;
            this.CameraUnk1A.IncrementMouseWheel = 1;
            this.CameraUnk1A.Location = new System.Drawing.Point(267, 31);
            this.CameraUnk1A.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk1A.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk1A.Name = "CameraUnk1A";
            this.CameraUnk1A.ShiftMultiplier = 1;
            this.CameraUnk1A.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk1A.TabIndex = 14;
            this.CameraUnk1A.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk1A.ValueChanged += new System.EventHandler(this.CameraUnk1A_ValueChanged);
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Enabled = false;
            this.label130.Location = new System.Drawing.Point(196, 7);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(56, 13);
            this.label130.TabIndex = 13;
            this.label130.Text = "Unk 0x18:";
            // 
            // CameraUnk12
            // 
            this.CameraUnk12.AlwaysFireValueChanged = false;
            this.CameraUnk12.DisplayDigits = 1;
            this.CameraUnk12.DoValueRollover = false;
            this.CameraUnk12.Enabled = false;
            this.CameraUnk12.Hexadecimal = true;
            this.CameraUnk12.IncrementMouseWheel = 1;
            this.CameraUnk12.Location = new System.Drawing.Point(74, 5);
            this.CameraUnk12.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CameraUnk12.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk12.Name = "CameraUnk12";
            this.CameraUnk12.ShiftMultiplier = 1;
            this.CameraUnk12.Size = new System.Drawing.Size(100, 20);
            this.CameraUnk12.TabIndex = 10;
            this.CameraUnk12.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraUnk12.ValueChanged += new System.EventHandler(this.CameraUnk12_ValueChanged);
            // 
            // SceneSettingsComboBox
            // 
            this.SceneSettingsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SceneSettingsComboBox.FormattingEnabled = true;
            this.SceneSettingsComboBox.Location = new System.Drawing.Point(92, 153);
            this.SceneSettingsComboBox.Name = "SceneSettingsComboBox";
            this.SceneSettingsComboBox.Size = new System.Drawing.Size(260, 21);
            this.SceneSettingsComboBox.TabIndex = 38;
            this.SceneSettingsComboBox.DropDown += new System.EventHandler(this.AdjustWidthAnimationBox_DropDown);
            this.SceneSettingsComboBox.SelectedValueChanged += new System.EventHandler(this.SceneSettingsChanged);
            // 
            // SceneFunctionLabel
            // 
            this.SceneFunctionLabel.AutoSize = true;
            this.SceneFunctionLabel.Location = new System.Drawing.Point(7, 156);
            this.SceneFunctionLabel.Name = "SceneFunctionLabel";
            this.SceneFunctionLabel.Size = new System.Drawing.Size(85, 13);
            this.SceneFunctionLabel.TabIndex = 37;
            this.SceneFunctionLabel.Text = "Scene Function:";
            // 
            // ElfMessageComboBox
            // 
            this.ElfMessageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ElfMessageComboBox.FormattingEnabled = true;
            this.ElfMessageComboBox.Location = new System.Drawing.Point(92, 88);
            this.ElfMessageComboBox.Name = "ElfMessageComboBox";
            this.ElfMessageComboBox.Size = new System.Drawing.Size(141, 21);
            this.ElfMessageComboBox.TabIndex = 36;
            this.ElfMessageComboBox.SelectedValueChanged += new System.EventHandler(this.ElfMessageChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(7, 91);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(76, 13);
            this.label42.TabIndex = 35;
            this.label42.Text = "Navi C-up tips:";
            // 
            // SpecialObjectComboBox
            // 
            this.SpecialObjectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpecialObjectComboBox.FormattingEnabled = true;
            this.SpecialObjectComboBox.Location = new System.Drawing.Point(92, 57);
            this.SpecialObjectComboBox.Name = "SpecialObjectComboBox";
            this.SpecialObjectComboBox.Size = new System.Drawing.Size(141, 21);
            this.SpecialObjectComboBox.TabIndex = 34;
            this.SpecialObjectComboBox.SelectedValueChanged += new System.EventHandler(this.SpecialObjectChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(7, 61);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(79, 13);
            this.label38.TabIndex = 33;
            this.label38.Text = "Special Object:";
            // 
            // SimulateN64CheckBox
            // 
            this.SimulateN64CheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.SimulateN64CheckBox.AutoSize = true;
            this.SimulateN64CheckBox.Location = new System.Drawing.Point(248, 88);
            this.SimulateN64CheckBox.Name = "SimulateN64CheckBox";
            this.SimulateN64CheckBox.Size = new System.Drawing.Size(125, 23);
            this.SimulateN64CheckBox.TabIndex = 32;
            this.SimulateN64CheckBox.Text = "Simulate N64 Graphics";
            this.SimulateN64CheckBox.UseVisualStyleBackColor = true;
            this.SimulateN64CheckBox.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            this.SimulateN64CheckBox.Click += new System.EventHandler(this.checkBox5_Click);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(261, 61);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(55, 13);
            this.label33.TabIndex = 31;
            this.label33.Text = "Scene ID:";
            // 
            // WaterboxGroupBox
            // 
            this.WaterboxGroupBox.Controls.Add(this.panel1);
            this.WaterboxGroupBox.Controls.Add(this.DeletewaterboxButton);
            this.WaterboxGroupBox.Controls.Add(this.AddwaterboxButton);
            this.WaterboxGroupBox.Controls.Add(this.WaterboxSelect);
            this.WaterboxGroupBox.Controls.Add(this.niceLine2);
            this.WaterboxGroupBox.Location = new System.Drawing.Point(6, 222);
            this.WaterboxGroupBox.Name = "WaterboxGroupBox";
            this.WaterboxGroupBox.Size = new System.Drawing.Size(399, 175);
            this.WaterboxGroupBox.TabIndex = 28;
            this.WaterboxGroupBox.TabStop = false;
            this.WaterboxGroupBox.Text = "Waterboxes";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.WaterboxRoom);
            this.panel1.Controls.Add(this.WaterboxCam);
            this.panel1.Controls.Add(this.label94);
            this.panel1.Controls.Add(this.WaterboxEnv);
            this.panel1.Controls.Add(this.WaterboxRoomLabel);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.WaterboxZPos);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.WaterboxXSize);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.WaterboxYPos);
            this.panel1.Controls.Add(this.WaterboxYSize);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.WaterboxXPos);
            this.panel1.Location = new System.Drawing.Point(3, 58);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(370, 114);
            this.panel1.TabIndex = 21;
            // 
            // WaterboxRoom
            // 
            this.WaterboxRoom.AlwaysFireValueChanged = false;
            this.WaterboxRoom.DisplayDigits = 1;
            this.WaterboxRoom.DoValueRollover = false;
            this.WaterboxRoom.Enabled = false;
            this.WaterboxRoom.Hexadecimal = true;
            this.WaterboxRoom.IncrementMouseWheel = 1;
            this.WaterboxRoom.Location = new System.Drawing.Point(73, 83);
            this.WaterboxRoom.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.WaterboxRoom.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxRoom.Name = "WaterboxRoom";
            this.WaterboxRoom.ShiftMultiplier = 1;
            this.WaterboxRoom.Size = new System.Drawing.Size(47, 20);
            this.WaterboxRoom.TabIndex = 23;
            this.EnvironmentControlTooltip.SetToolTip(this.WaterboxRoom, "3F = all rooms");
            this.WaterboxRoom.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxRoom.ValueChanged += new System.EventHandler(this.WaterboxRoom_Leave);
            // 
            // WaterboxCam
            // 
            this.WaterboxCam.AlwaysFireValueChanged = false;
            this.WaterboxCam.DisplayDigits = 1;
            this.WaterboxCam.DoValueRollover = false;
            this.WaterboxCam.Enabled = false;
            this.WaterboxCam.Hexadecimal = true;
            this.WaterboxCam.IncrementMouseWheel = 1;
            this.WaterboxCam.Location = new System.Drawing.Point(267, 86);
            this.WaterboxCam.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.WaterboxCam.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxCam.Name = "WaterboxCam";
            this.WaterboxCam.ShiftMultiplier = 1;
            this.WaterboxCam.Size = new System.Drawing.Size(47, 20);
            this.WaterboxCam.TabIndex = 22;
            this.WaterboxCam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxCam.ValueChanged += new System.EventHandler(this.WaterboxCam_ValueChanged);
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Enabled = false;
            this.label94.Location = new System.Drawing.Point(196, 88);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(46, 13);
            this.label94.TabIndex = 21;
            this.label94.Text = "Camera:";
            // 
            // WaterboxEnv
            // 
            this.WaterboxEnv.AlwaysFireValueChanged = false;
            this.WaterboxEnv.DisplayDigits = 1;
            this.WaterboxEnv.DoValueRollover = false;
            this.WaterboxEnv.Enabled = false;
            this.WaterboxEnv.Hexadecimal = true;
            this.WaterboxEnv.IncrementMouseWheel = 1;
            this.WaterboxEnv.Location = new System.Drawing.Point(267, 57);
            this.WaterboxEnv.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.WaterboxEnv.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxEnv.Name = "WaterboxEnv";
            this.WaterboxEnv.ShiftMultiplier = 1;
            this.WaterboxEnv.Size = new System.Drawing.Size(47, 20);
            this.WaterboxEnv.TabIndex = 20;
            this.WaterboxEnv.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxEnv.ValueChanged += new System.EventHandler(this.WaterboxEnv_ValueChanged);
            // 
            // WaterboxRoomLabel
            // 
            this.WaterboxRoomLabel.AutoSize = true;
            this.WaterboxRoomLabel.Enabled = false;
            this.WaterboxRoomLabel.Location = new System.Drawing.Point(3, 88);
            this.WaterboxRoomLabel.Name = "WaterboxRoomLabel";
            this.WaterboxRoomLabel.Size = new System.Drawing.Size(38, 13);
            this.WaterboxRoomLabel.TabIndex = 18;
            this.WaterboxRoomLabel.Text = "Room:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Enabled = false;
            this.label22.Location = new System.Drawing.Point(3, 7);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(57, 13);
            this.label22.TabIndex = 7;
            this.label22.Text = "X Position:";
            // 
            // WaterboxZPos
            // 
            this.WaterboxZPos.AlwaysFireValueChanged = false;
            this.WaterboxZPos.DisplayDigits = 1;
            this.WaterboxZPos.DoValueRollover = false;
            this.WaterboxZPos.Enabled = false;
            this.WaterboxZPos.IncrementMouseWheel = 1;
            this.WaterboxZPos.Location = new System.Drawing.Point(74, 57);
            this.WaterboxZPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.WaterboxZPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.WaterboxZPos.Name = "WaterboxZPos";
            this.WaterboxZPos.ShiftMultiplier = 20;
            this.WaterboxZPos.Size = new System.Drawing.Size(100, 20);
            this.WaterboxZPos.TabIndex = 12;
            this.WaterboxZPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxZPos.ValueChanged += new System.EventHandler(this.WaterboxTransformZ_ChangeValue);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Enabled = false;
            this.label25.Location = new System.Drawing.Point(3, 59);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(57, 13);
            this.label25.TabIndex = 11;
            this.label25.Text = "Z Position:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Enabled = false;
            this.label24.Location = new System.Drawing.Point(3, 33);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(57, 13);
            this.label24.TabIndex = 9;
            this.label24.Text = "Y Position:";
            // 
            // WaterboxXSize
            // 
            this.WaterboxXSize.AlwaysFireValueChanged = false;
            this.WaterboxXSize.DisplayDigits = 1;
            this.WaterboxXSize.DoValueRollover = false;
            this.WaterboxXSize.Enabled = false;
            this.WaterboxXSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.WaterboxXSize.IncrementMouseWheel = 2;
            this.WaterboxXSize.Location = new System.Drawing.Point(267, 5);
            this.WaterboxXSize.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.WaterboxXSize.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.WaterboxXSize.Name = "WaterboxXSize";
            this.WaterboxXSize.ShiftMultiplier = 10;
            this.WaterboxXSize.Size = new System.Drawing.Size(100, 20);
            this.WaterboxXSize.TabIndex = 13;
            this.WaterboxXSize.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxXSize.ValueChanged += new System.EventHandler(this.WaterboxSizeX_ChangeValue);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Enabled = false;
            this.label18.Location = new System.Drawing.Point(196, 33);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 13);
            this.label18.TabIndex = 17;
            this.label18.Text = "Z Size:";
            // 
            // WaterboxYPos
            // 
            this.WaterboxYPos.AlwaysFireValueChanged = false;
            this.WaterboxYPos.DisplayDigits = 1;
            this.WaterboxYPos.DoValueRollover = false;
            this.WaterboxYPos.Enabled = false;
            this.WaterboxYPos.IncrementMouseWheel = 1;
            this.WaterboxYPos.Location = new System.Drawing.Point(74, 31);
            this.WaterboxYPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.WaterboxYPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.WaterboxYPos.Name = "WaterboxYPos";
            this.WaterboxYPos.ShiftMultiplier = 20;
            this.WaterboxYPos.Size = new System.Drawing.Size(100, 20);
            this.WaterboxYPos.TabIndex = 11;
            this.WaterboxYPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxYPos.ValueChanged += new System.EventHandler(this.WaterboxTransformY_ChangeValue);
            // 
            // WaterboxYSize
            // 
            this.WaterboxYSize.AlwaysFireValueChanged = false;
            this.WaterboxYSize.DisplayDigits = 1;
            this.WaterboxYSize.DoValueRollover = false;
            this.WaterboxYSize.Enabled = false;
            this.WaterboxYSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.WaterboxYSize.IncrementMouseWheel = 2;
            this.WaterboxYSize.Location = new System.Drawing.Point(267, 31);
            this.WaterboxYSize.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.WaterboxYSize.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.WaterboxYSize.Name = "WaterboxYSize";
            this.WaterboxYSize.ShiftMultiplier = 10;
            this.WaterboxYSize.Size = new System.Drawing.Size(100, 20);
            this.WaterboxYSize.TabIndex = 14;
            this.WaterboxYSize.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxYSize.ValueChanged += new System.EventHandler(this.WaterboxSizeY_ChangeValue);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Enabled = false;
            this.label23.Location = new System.Drawing.Point(196, 7);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(40, 13);
            this.label23.TabIndex = 13;
            this.label23.Text = "X Size:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Enabled = false;
            this.label20.Location = new System.Drawing.Point(196, 59);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 13);
            this.label20.TabIndex = 4;
            this.label20.Text = "Environment:";
            // 
            // WaterboxXPos
            // 
            this.WaterboxXPos.AlwaysFireValueChanged = false;
            this.WaterboxXPos.DisplayDigits = 1;
            this.WaterboxXPos.DoValueRollover = false;
            this.WaterboxXPos.Enabled = false;
            this.WaterboxXPos.IncrementMouseWheel = 1;
            this.WaterboxXPos.Location = new System.Drawing.Point(74, 5);
            this.WaterboxXPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.WaterboxXPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.WaterboxXPos.Name = "WaterboxXPos";
            this.WaterboxXPos.ShiftMultiplier = 20;
            this.WaterboxXPos.Size = new System.Drawing.Size(100, 20);
            this.WaterboxXPos.TabIndex = 10;
            this.WaterboxXPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WaterboxXPos.ValueChanged += new System.EventHandler(this.WaterboxTransformX_ChangeValue);
            // 
            // DeletewaterboxButton
            // 
            this.DeletewaterboxButton.Location = new System.Drawing.Point(250, 19);
            this.DeletewaterboxButton.Name = "DeletewaterboxButton";
            this.DeletewaterboxButton.Size = new System.Drawing.Size(120, 23);
            this.DeletewaterboxButton.TabIndex = 9;
            this.DeletewaterboxButton.Text = "Delete Waterbox";
            this.DeletewaterboxButton.UseVisualStyleBackColor = true;
            this.DeletewaterboxButton.Click += new System.EventHandler(this.DeleteWaterboxButton_Click);
            // 
            // AddwaterboxButton
            // 
            this.AddwaterboxButton.Location = new System.Drawing.Point(124, 19);
            this.AddwaterboxButton.Name = "AddwaterboxButton";
            this.AddwaterboxButton.Size = new System.Drawing.Size(120, 23);
            this.AddwaterboxButton.TabIndex = 8;
            this.AddwaterboxButton.Text = "Add Waterbox";
            this.EnvironmentControlTooltip.SetToolTip(this.AddwaterboxButton, "Hold SHIFT to add in front of camera");
            this.AddwaterboxButton.UseVisualStyleBackColor = true;
            this.AddwaterboxButton.Click += new System.EventHandler(this.AddWaterboxButton_Click);
            // 
            // WaterboxSelect
            // 
            this.WaterboxSelect.Hexadecimal = true;
            this.WaterboxSelect.Location = new System.Drawing.Point(9, 22);
            this.WaterboxSelect.Name = "WaterboxSelect";
            this.WaterboxSelect.Size = new System.Drawing.Size(65, 20);
            this.WaterboxSelect.TabIndex = 7;
            this.WaterboxSelect.ValueChanged += new System.EventHandler(this.WaterboxSelect_ValueChanged);
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(9, 46);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(384, 15);
            this.niceLine2.TabIndex = 20;
            this.niceLine2.TabStop = false;
            // 
            // InjectoffsetTextbox
            // 
            this.InjectoffsetTextbox.AllowHex = true;
            this.InjectoffsetTextbox.Digits = 8;
            this.InjectoffsetTextbox.Location = new System.Drawing.Point(254, 31);
            this.InjectoffsetTextbox.Name = "InjectoffsetTextbox";
            this.InjectoffsetTextbox.Size = new System.Drawing.Size(98, 20);
            this.InjectoffsetTextbox.TabIndex = 2;
            this.InjectoffsetTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericTextBox3_KeyDown);
            this.InjectoffsetTextbox.Leave += new System.EventHandler(this.InjectoffsetTextbox_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(167, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Injection Offset:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(358, 121);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(44, 20);
            this.button4.TabIndex = 6;
            this.button4.Text = "Load";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.LoadCollision_Click);
            // 
            // CollisionTextbox
            // 
            this.CollisionTextbox.Location = new System.Drawing.Point(93, 121);
            this.CollisionTextbox.Name = "CollisionTextbox";
            this.CollisionTextbox.Size = new System.Drawing.Size(259, 20);
            this.CollisionTextbox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Collision Model:";
            // 
            // ScaleNumericbox
            // 
            this.ScaleNumericbox.DecimalPlaces = 2;
            this.ScaleNumericbox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            this.ScaleNumericbox.Location = new System.Drawing.Point(50, 32);
            this.ScaleNumericbox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            131072});
            this.ScaleNumericbox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            this.ScaleNumericbox.Name = "ScaleNumericbox";
            this.ScaleNumericbox.Size = new System.Drawing.Size(98, 20);
            this.ScaleNumericbox.TabIndex = 1;
            this.ScaleNumericbox.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.ScaleNumericbox.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scale:";
            // 
            // NameTextbox
            // 
            this.NameTextbox.Location = new System.Drawing.Point(50, 6);
            this.NameTextbox.Name = "NameTextbox";
            this.NameTextbox.Size = new System.Drawing.Size(332, 20);
            this.NameTextbox.TabIndex = 0;
            this.EnvironmentControlTooltip.SetToolTip(this.NameTextbox, "Optional field, only used by SharpOcarina");
            this.NameTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SceneName_KeyDown);
            this.NameTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SceneName_KeyDown);
            this.NameTextbox.Leave += new System.EventHandler(this.NameTextbox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // TextureAnimsGroupBox
            // 
            this.TextureAnimsGroupBox.Controls.Add(this.label98);
            this.TextureAnimsGroupBox.Controls.Add(this.TextureAnimHeight2);
            this.TextureAnimsGroupBox.Controls.Add(this.label99);
            this.TextureAnimsGroupBox.Controls.Add(this.TextureAnimWidth2);
            this.TextureAnimsGroupBox.Controls.Add(this.label100);
            this.TextureAnimsGroupBox.Controls.Add(this.TextureAnimYVelocity2);
            this.TextureAnimsGroupBox.Controls.Add(this.label101);
            this.TextureAnimsGroupBox.Controls.Add(this.TextureAnimXVelocity2);
            this.TextureAnimsGroupBox.Controls.Add(this.niceLine14);
            this.TextureAnimsGroupBox.Controls.Add(this.label96);
            this.TextureAnimsGroupBox.Controls.Add(this.TextureAnimHeight1);
            this.TextureAnimsGroupBox.Controls.Add(this.label97);
            this.TextureAnimsGroupBox.Controls.Add(this.TextureAnimWidth1);
            this.TextureAnimsGroupBox.Controls.Add(this.label95);
            this.TextureAnimsGroupBox.Controls.Add(this.TextureAnimYVelocity1);
            this.TextureAnimsGroupBox.Controls.Add(this.label59);
            this.TextureAnimsGroupBox.Controls.Add(this.DeleteTextureAnim);
            this.TextureAnimsGroupBox.Controls.Add(this.TextureAnimXVelocity1);
            this.TextureAnimsGroupBox.Controls.Add(this.AddTextureAnim);
            this.TextureAnimsGroupBox.Controls.Add(this.TextureAnimSelect);
            this.TextureAnimsGroupBox.Controls.Add(this.niceLine13);
            this.TextureAnimsGroupBox.Location = new System.Drawing.Point(6, 147);
            this.TextureAnimsGroupBox.Name = "TextureAnimsGroupBox";
            this.TextureAnimsGroupBox.Size = new System.Drawing.Size(399, 170);
            this.TextureAnimsGroupBox.TabIndex = 44;
            this.TextureAnimsGroupBox.TabStop = false;
            this.TextureAnimsGroupBox.Text = "Texture Animations";
            this.TextureAnimsGroupBox.Visible = false;
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Enabled = false;
            this.label98.Location = new System.Drawing.Point(150, 151);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(41, 13);
            this.label98.TabIndex = 39;
            this.label98.Text = "Height:";
            // 
            // TextureAnimHeight2
            // 
            this.TextureAnimHeight2.AlwaysFireValueChanged = false;
            this.TextureAnimHeight2.DisplayDigits = 1;
            this.TextureAnimHeight2.DoValueRollover = false;
            this.TextureAnimHeight2.Enabled = false;
            this.TextureAnimHeight2.IncrementMouseWheel = 1;
            this.TextureAnimHeight2.Location = new System.Drawing.Point(221, 147);
            this.TextureAnimHeight2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.TextureAnimHeight2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimHeight2.Name = "TextureAnimHeight2";
            this.TextureAnimHeight2.ShiftMultiplier = 1;
            this.TextureAnimHeight2.Size = new System.Drawing.Size(44, 20);
            this.TextureAnimHeight2.TabIndex = 40;
            this.TextureAnimHeight2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimHeight2.ValueChanged += new System.EventHandler(this.TextureAnimHeight2_ValueChanged);
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Enabled = false;
            this.label99.Location = new System.Drawing.Point(150, 125);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(38, 13);
            this.label99.TabIndex = 37;
            this.label99.Text = "Width:";
            // 
            // TextureAnimWidth2
            // 
            this.TextureAnimWidth2.AlwaysFireValueChanged = false;
            this.TextureAnimWidth2.DisplayDigits = 1;
            this.TextureAnimWidth2.DoValueRollover = false;
            this.TextureAnimWidth2.Enabled = false;
            this.TextureAnimWidth2.IncrementMouseWheel = 1;
            this.TextureAnimWidth2.Location = new System.Drawing.Point(221, 121);
            this.TextureAnimWidth2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.TextureAnimWidth2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimWidth2.Name = "TextureAnimWidth2";
            this.TextureAnimWidth2.ShiftMultiplier = 1;
            this.TextureAnimWidth2.Size = new System.Drawing.Size(44, 20);
            this.TextureAnimWidth2.TabIndex = 38;
            this.TextureAnimWidth2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimWidth2.ValueChanged += new System.EventHandler(this.TextureAnimWidth2_ValueChanged);
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Enabled = false;
            this.label100.Location = new System.Drawing.Point(12, 149);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(57, 13);
            this.label100.TabIndex = 35;
            this.label100.Text = "Y Velocity:";
            // 
            // TextureAnimYVelocity2
            // 
            this.TextureAnimYVelocity2.AlwaysFireValueChanged = false;
            this.TextureAnimYVelocity2.DisplayDigits = 1;
            this.TextureAnimYVelocity2.DoValueRollover = false;
            this.TextureAnimYVelocity2.Enabled = false;
            this.TextureAnimYVelocity2.IncrementMouseWheel = 1;
            this.TextureAnimYVelocity2.Location = new System.Drawing.Point(83, 147);
            this.TextureAnimYVelocity2.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.TextureAnimYVelocity2.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.TextureAnimYVelocity2.Name = "TextureAnimYVelocity2";
            this.TextureAnimYVelocity2.ShiftMultiplier = 1;
            this.TextureAnimYVelocity2.Size = new System.Drawing.Size(44, 20);
            this.TextureAnimYVelocity2.TabIndex = 36;
            this.TextureAnimYVelocity2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimYVelocity2.ValueChanged += new System.EventHandler(this.TextureAnimYVelocity2_ValueChanged);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Enabled = false;
            this.label101.Location = new System.Drawing.Point(12, 123);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(57, 13);
            this.label101.TabIndex = 33;
            this.label101.Text = "X Velocity:";
            // 
            // TextureAnimXVelocity2
            // 
            this.TextureAnimXVelocity2.AlwaysFireValueChanged = false;
            this.TextureAnimXVelocity2.DisplayDigits = 1;
            this.TextureAnimXVelocity2.DoValueRollover = false;
            this.TextureAnimXVelocity2.Enabled = false;
            this.TextureAnimXVelocity2.IncrementMouseWheel = 1;
            this.TextureAnimXVelocity2.Location = new System.Drawing.Point(83, 121);
            this.TextureAnimXVelocity2.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.TextureAnimXVelocity2.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.TextureAnimXVelocity2.Name = "TextureAnimXVelocity2";
            this.TextureAnimXVelocity2.ShiftMultiplier = 1;
            this.TextureAnimXVelocity2.Size = new System.Drawing.Size(44, 20);
            this.TextureAnimXVelocity2.TabIndex = 34;
            this.TextureAnimXVelocity2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimXVelocity2.ValueChanged += new System.EventHandler(this.TextureAnimXVelocity2_ValueChanged);
            // 
            // niceLine14
            // 
            this.niceLine14.Location = new System.Drawing.Point(5, 105);
            this.niceLine14.Name = "niceLine14";
            this.niceLine14.Size = new System.Drawing.Size(384, 15);
            this.niceLine14.TabIndex = 22;
            this.niceLine14.TabStop = false;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Enabled = false;
            this.label96.Location = new System.Drawing.Point(150, 88);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(41, 13);
            this.label96.TabIndex = 30;
            this.label96.Text = "Height:";
            // 
            // TextureAnimHeight1
            // 
            this.TextureAnimHeight1.AlwaysFireValueChanged = false;
            this.TextureAnimHeight1.DisplayDigits = 1;
            this.TextureAnimHeight1.DoValueRollover = false;
            this.TextureAnimHeight1.Enabled = false;
            this.TextureAnimHeight1.IncrementMouseWheel = 1;
            this.TextureAnimHeight1.Location = new System.Drawing.Point(221, 84);
            this.TextureAnimHeight1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.TextureAnimHeight1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimHeight1.Name = "TextureAnimHeight1";
            this.TextureAnimHeight1.ShiftMultiplier = 1;
            this.TextureAnimHeight1.Size = new System.Drawing.Size(44, 20);
            this.TextureAnimHeight1.TabIndex = 31;
            this.TextureAnimHeight1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimHeight1.ValueChanged += new System.EventHandler(this.TextureAnimHeight1_ValueChanged);
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Enabled = false;
            this.label97.Location = new System.Drawing.Point(150, 62);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(38, 13);
            this.label97.TabIndex = 28;
            this.label97.Text = "Width:";
            // 
            // TextureAnimWidth1
            // 
            this.TextureAnimWidth1.AlwaysFireValueChanged = false;
            this.TextureAnimWidth1.DisplayDigits = 1;
            this.TextureAnimWidth1.DoValueRollover = false;
            this.TextureAnimWidth1.Enabled = false;
            this.TextureAnimWidth1.IncrementMouseWheel = 1;
            this.TextureAnimWidth1.Location = new System.Drawing.Point(221, 58);
            this.TextureAnimWidth1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.TextureAnimWidth1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimWidth1.Name = "TextureAnimWidth1";
            this.TextureAnimWidth1.ShiftMultiplier = 1;
            this.TextureAnimWidth1.Size = new System.Drawing.Size(44, 20);
            this.TextureAnimWidth1.TabIndex = 29;
            this.TextureAnimWidth1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimWidth1.ValueChanged += new System.EventHandler(this.TextureAnimWidth1_ValueChanged);
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Enabled = false;
            this.label95.Location = new System.Drawing.Point(12, 86);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(57, 13);
            this.label95.TabIndex = 26;
            this.label95.Text = "Y Velocity:";
            // 
            // TextureAnimYVelocity1
            // 
            this.TextureAnimYVelocity1.AlwaysFireValueChanged = false;
            this.TextureAnimYVelocity1.DisplayDigits = 1;
            this.TextureAnimYVelocity1.DoValueRollover = false;
            this.TextureAnimYVelocity1.Enabled = false;
            this.TextureAnimYVelocity1.IncrementMouseWheel = 1;
            this.TextureAnimYVelocity1.Location = new System.Drawing.Point(83, 84);
            this.TextureAnimYVelocity1.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.TextureAnimYVelocity1.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.TextureAnimYVelocity1.Name = "TextureAnimYVelocity1";
            this.TextureAnimYVelocity1.ShiftMultiplier = 1;
            this.TextureAnimYVelocity1.Size = new System.Drawing.Size(44, 20);
            this.TextureAnimYVelocity1.TabIndex = 27;
            this.TextureAnimYVelocity1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimYVelocity1.ValueChanged += new System.EventHandler(this.TextureAnimYVelocity1_ValueChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Enabled = false;
            this.label59.Location = new System.Drawing.Point(12, 60);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(57, 13);
            this.label59.TabIndex = 23;
            this.label59.Text = "X Velocity:";
            // 
            // DeleteTextureAnim
            // 
            this.DeleteTextureAnim.Location = new System.Drawing.Point(247, 19);
            this.DeleteTextureAnim.Name = "DeleteTextureAnim";
            this.DeleteTextureAnim.Size = new System.Drawing.Size(120, 23);
            this.DeleteTextureAnim.TabIndex = 24;
            this.DeleteTextureAnim.Text = "Delete Animation";
            this.DeleteTextureAnim.UseVisualStyleBackColor = true;
            this.DeleteTextureAnim.Click += new System.EventHandler(this.DeleteTextureAnim_Click);
            // 
            // TextureAnimXVelocity1
            // 
            this.TextureAnimXVelocity1.AlwaysFireValueChanged = false;
            this.TextureAnimXVelocity1.DisplayDigits = 1;
            this.TextureAnimXVelocity1.DoValueRollover = false;
            this.TextureAnimXVelocity1.Enabled = false;
            this.TextureAnimXVelocity1.IncrementMouseWheel = 1;
            this.TextureAnimXVelocity1.Location = new System.Drawing.Point(83, 58);
            this.TextureAnimXVelocity1.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.TextureAnimXVelocity1.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.TextureAnimXVelocity1.Name = "TextureAnimXVelocity1";
            this.TextureAnimXVelocity1.ShiftMultiplier = 1;
            this.TextureAnimXVelocity1.Size = new System.Drawing.Size(44, 20);
            this.TextureAnimXVelocity1.TabIndex = 24;
            this.TextureAnimXVelocity1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TextureAnimXVelocity1.ValueChanged += new System.EventHandler(this.TextureAnimXVelocity1_ValueChanged);
            // 
            // AddTextureAnim
            // 
            this.AddTextureAnim.Location = new System.Drawing.Point(121, 19);
            this.AddTextureAnim.Name = "AddTextureAnim";
            this.AddTextureAnim.Size = new System.Drawing.Size(120, 23);
            this.AddTextureAnim.TabIndex = 23;
            this.AddTextureAnim.Text = "Add Animation";
            this.AddTextureAnim.UseVisualStyleBackColor = true;
            this.AddTextureAnim.Click += new System.EventHandler(this.AddTextureAnim_Click);
            // 
            // TextureAnimSelect
            // 
            this.TextureAnimSelect.Hexadecimal = true;
            this.TextureAnimSelect.Location = new System.Drawing.Point(6, 22);
            this.TextureAnimSelect.Name = "TextureAnimSelect";
            this.TextureAnimSelect.Size = new System.Drawing.Size(65, 20);
            this.TextureAnimSelect.TabIndex = 22;
            this.TextureAnimSelect.ValueChanged += new System.EventHandler(this.TextureAnimSelect_ValueChanged);
            // 
            // niceLine13
            // 
            this.niceLine13.Location = new System.Drawing.Point(6, 46);
            this.niceLine13.Name = "niceLine13";
            this.niceLine13.Size = new System.Drawing.Size(384, 15);
            this.niceLine13.TabIndex = 25;
            this.niceLine13.TabStop = false;
            // 
            // tabRooms
            // 
            this.tabRooms.Controls.Add(this.AdditionalTexturesGroupBox);
            this.tabRooms.Controls.Add(this.ReloadRoomButton);
            this.tabRooms.Controls.Add(this.AddMultipleRooms);
            this.tabRooms.Controls.Add(this.ContinualInject);
            this.tabRooms.Controls.Add(this.groupBox1);
            this.tabRooms.Controls.Add(this.RoomInjectionOffset);
            this.tabRooms.Controls.Add(this.GroupList);
            this.tabRooms.Controls.Add(this.DeleteRoom);
            this.tabRooms.Controls.Add(this.AddRoom);
            this.tabRooms.Controls.Add(this.RoomList);
            this.tabRooms.Location = new System.Drawing.Point(4, 40);
            this.tabRooms.Name = "tabRooms";
            this.tabRooms.Padding = new System.Windows.Forms.Padding(3);
            this.tabRooms.Size = new System.Drawing.Size(411, 676);
            this.tabRooms.TabIndex = 1;
            this.tabRooms.Text = "Rooms";
            this.tabRooms.UseVisualStyleBackColor = true;
            // 
            // AdditionalTexturesGroupBox
            // 
            this.AdditionalTexturesGroupBox.Controls.Add(this.AdditionalTextureLabel);
            this.AdditionalTexturesGroupBox.Controls.Add(this.DeleteAdditionalTexture);
            this.AdditionalTexturesGroupBox.Controls.Add(this.AddAdditionalTexture);
            this.AdditionalTexturesGroupBox.Controls.Add(this.AdditionalTextureList);
            this.AdditionalTexturesGroupBox.Location = new System.Drawing.Point(6, 604);
            this.AdditionalTexturesGroupBox.Name = "AdditionalTexturesGroupBox";
            this.AdditionalTexturesGroupBox.Size = new System.Drawing.Size(393, 66);
            this.AdditionalTexturesGroupBox.TabIndex = 44;
            this.AdditionalTexturesGroupBox.TabStop = false;
            this.AdditionalTexturesGroupBox.Text = "Additional Textures";
            // 
            // AdditionalTextureLabel
            // 
            this.AdditionalTextureLabel.AutoSize = true;
            this.AdditionalTextureLabel.Location = new System.Drawing.Point(6, 45);
            this.AdditionalTextureLabel.Name = "AdditionalTextureLabel";
            this.AdditionalTextureLabel.Size = new System.Drawing.Size(49, 13);
            this.AdditionalTextureLabel.TabIndex = 43;
            this.AdditionalTextureLabel.Text = "Filename";
            // 
            // DeleteAdditionalTexture
            // 
            this.DeleteAdditionalTexture.Location = new System.Drawing.Point(250, 19);
            this.DeleteAdditionalTexture.Name = "DeleteAdditionalTexture";
            this.DeleteAdditionalTexture.Size = new System.Drawing.Size(120, 23);
            this.DeleteAdditionalTexture.TabIndex = 9;
            this.DeleteAdditionalTexture.Text = "Delete Texture";
            this.DeleteAdditionalTexture.UseVisualStyleBackColor = true;
            this.DeleteAdditionalTexture.Click += new System.EventHandler(this.DeleteAdditionalTexture_Click);
            // 
            // AddAdditionalTexture
            // 
            this.AddAdditionalTexture.BackColor = System.Drawing.Color.Transparent;
            this.AddAdditionalTexture.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddAdditionalTexture.Location = new System.Drawing.Point(124, 19);
            this.AddAdditionalTexture.Name = "AddAdditionalTexture";
            this.AddAdditionalTexture.Size = new System.Drawing.Size(120, 23);
            this.AddAdditionalTexture.TabIndex = 8;
            this.AddAdditionalTexture.Text = "Add Texture";
            this.AddAdditionalTexture.UseVisualStyleBackColor = false;
            this.AddAdditionalTexture.Click += new System.EventHandler(this.AddAdditionalTexture_Click);
            // 
            // AdditionalTextureList
            // 
            this.AdditionalTextureList.Location = new System.Drawing.Point(9, 22);
            this.AdditionalTextureList.Name = "AdditionalTextureList";
            this.AdditionalTextureList.Size = new System.Drawing.Size(65, 20);
            this.AdditionalTextureList.TabIndex = 7;
            this.AdditionalTextureList.ValueChanged += new System.EventHandler(this.AdditionalTextureList_ValueChanged);
            // 
            // ReloadRoomButton
            // 
            this.ReloadRoomButton.Location = new System.Drawing.Point(240, 6);
            this.ReloadRoomButton.Name = "ReloadRoomButton";
            this.ReloadRoomButton.Size = new System.Drawing.Size(66, 23);
            this.ReloadRoomButton.TabIndex = 17;
            this.ReloadRoomButton.Text = "Reload";
            this.EnvironmentControlTooltip.SetToolTip(this.ReloadRoomButton, "Hold SHIFT to load a new model while keeping actors and everything else");
            this.ReloadRoomButton.UseVisualStyleBackColor = true;
            this.ReloadRoomButton.Click += new System.EventHandler(this.ReloadRoomButton_Click);
            // 
            // AddMultipleRooms
            // 
            this.AddMultipleRooms.Location = new System.Drawing.Point(119, 6);
            this.AddMultipleRooms.Name = "AddMultipleRooms";
            this.AddMultipleRooms.Size = new System.Drawing.Size(115, 23);
            this.AddMultipleRooms.TabIndex = 16;
            this.AddMultipleRooms.Text = "Add Multiple Rooms";
            this.AddMultipleRooms.UseVisualStyleBackColor = true;
            this.AddMultipleRooms.Click += new System.EventHandler(this.AddMultipleRooms_Click);
            // 
            // ContinualInject
            // 
            this.ContinualInject.AutoSize = true;
            this.ContinualInject.Checked = true;
            this.ContinualInject.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ContinualInject.Enabled = false;
            this.ContinualInject.Location = new System.Drawing.Point(206, 157);
            this.ContinualInject.Name = "ContinualInject";
            this.ContinualInject.Size = new System.Drawing.Size(108, 17);
            this.ContinualInject.TabIndex = 15;
            this.ContinualInject.Text = "Inject after scene";
            this.ContinualInject.UseVisualStyleBackColor = true;
            this.ContinualInject.Visible = false;
            this.ContinualInject.CheckedChanged += new System.EventHandler(this.ContinualInject_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GroupScaledNormals);
            this.groupBox1.Controls.Add(this.GroupCustomizeButton);
            this.groupBox1.Controls.Add(this.GroupCustom);
            this.groupBox1.Controls.Add(this.GroupVertexNormals);
            this.groupBox1.Controls.Add(this.GroupAlphaMask);
            this.groupBox1.Controls.Add(this.GroupEnvColor);
            this.groupBox1.Controls.Add(this.GroupRenderLast);
            this.groupBox1.Controls.Add(this.GroupLODDIstance);
            this.groupBox1.Controls.Add(this.label102);
            this.groupBox1.Controls.Add(this.GroupLODGroup);
            this.groupBox1.Controls.Add(this.GroupLod);
            this.groupBox1.Controls.Add(this.GroupSmoothRgbaEdges);
            this.groupBox1.Controls.Add(this.AnimationLabel);
            this.groupBox1.Controls.Add(this.GroupAnimatedBank);
            this.groupBox1.Controls.Add(this.GroupIgnoreFog);
            this.groupBox1.Controls.Add(this.Group2AxisBillboard);
            this.groupBox1.Controls.Add(this.GroupBillboard);
            this.groupBox1.Controls.Add(this.GroupPixelated);
            this.groupBox1.Controls.Add(this.GroupDecal);
            this.groupBox1.Controls.Add(this.label93);
            this.groupBox1.Controls.Add(this.ShiftTNumeric);
            this.groupBox1.Controls.Add(this.ShiftSNumeric);
            this.groupBox1.Controls.Add(this.ReverseLightCheckBox);
            this.groupBox1.Controls.Add(this.GroupMultitextureAlpha);
            this.groupBox1.Controls.Add(this.label79);
            this.groupBox1.Controls.Add(this.GroupMetallic);
            this.groupBox1.Controls.Add(this.GroupAnimated);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.numericUpDown6);
            this.groupBox1.Controls.Add(this.numericUpDown5);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.MultiTextureComboBox);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.GroupPolygonType);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.pictureBox7);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(6, 302);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 296);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "a";
            this.groupBox1.Text = "Group Settings";
            // 
            // GroupScaledNormals
            // 
            this.GroupScaledNormals.AutoSize = true;
            this.GroupScaledNormals.Location = new System.Drawing.Point(306, 220);
            this.GroupScaledNormals.Name = "GroupScaledNormals";
            this.GroupScaledNormals.Size = new System.Drawing.Size(100, 17);
            this.GroupScaledNormals.TabIndex = 45;
            this.GroupScaledNormals.Text = "Scaled Normals";
            this.EnvironmentControlTooltip.SetToolTip(this.GroupScaledNormals, "Uses grayscale vertex color data to customize normals");
            this.GroupScaledNormals.UseVisualStyleBackColor = true;
            this.GroupScaledNormals.CheckedChanged += new System.EventHandler(this.GroupVibrant_CheckedChanged);
            // 
            // GroupCustomizeButton
            // 
            this.GroupCustomizeButton.BackColor = System.Drawing.Color.Transparent;
            this.GroupCustomizeButton.Enabled = false;
            this.GroupCustomizeButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GroupCustomizeButton.Location = new System.Drawing.Point(73, 269);
            this.GroupCustomizeButton.Name = "GroupCustomizeButton";
            this.GroupCustomizeButton.Size = new System.Drawing.Size(120, 23);
            this.GroupCustomizeButton.TabIndex = 44;
            this.GroupCustomizeButton.Text = "Customize";
            this.GroupCustomizeButton.UseVisualStyleBackColor = false;
            this.GroupCustomizeButton.Click += new System.EventHandler(this.GroupCustomizeButton_Click);
            // 
            // GroupCustom
            // 
            this.GroupCustom.AutoSize = true;
            this.GroupCustom.Location = new System.Drawing.Point(9, 273);
            this.GroupCustom.Name = "GroupCustom";
            this.GroupCustom.Size = new System.Drawing.Size(61, 17);
            this.GroupCustom.TabIndex = 44;
            this.GroupCustom.Text = "Custom";
            this.GroupCustom.UseVisualStyleBackColor = true;
            this.GroupCustom.CheckedChanged += new System.EventHandler(this.GroupCustom_CheckedChanged);
            // 
            // GroupVertexNormals
            // 
            this.GroupVertexNormals.AutoSize = true;
            this.GroupVertexNormals.Location = new System.Drawing.Point(306, 202);
            this.GroupVertexNormals.Name = "GroupVertexNormals";
            this.GroupVertexNormals.Size = new System.Drawing.Size(97, 17);
            this.GroupVertexNormals.TabIndex = 43;
            this.GroupVertexNormals.Text = "Vertex Normals";
            this.EnvironmentControlTooltip.SetToolTip(this.GroupVertexNormals, "Uses vertex colors to calculate normals");
            this.GroupVertexNormals.UseVisualStyleBackColor = true;
            this.GroupVertexNormals.CheckedChanged += new System.EventHandler(this.GroupVertexNormals_CheckedChanged);
            // 
            // GroupAlphaMask
            // 
            this.GroupAlphaMask.AutoSize = true;
            this.GroupAlphaMask.Location = new System.Drawing.Point(192, 183);
            this.GroupAlphaMask.Name = "GroupAlphaMask";
            this.GroupAlphaMask.Size = new System.Drawing.Size(86, 17);
            this.GroupAlphaMask.TabIndex = 42;
            this.GroupAlphaMask.Text = "Alpha Mask*";
            this.EnvironmentControlTooltip.SetToolTip(this.GroupAlphaMask, "You must enable \"Don\'t convert multitextures to RGBA\"");
            this.GroupAlphaMask.UseVisualStyleBackColor = true;
            this.GroupAlphaMask.CheckedChanged += new System.EventHandler(this.GroupAlphaMask_CheckedChanged);
            // 
            // GroupEnvColor
            // 
            this.GroupEnvColor.AutoSize = true;
            this.GroupEnvColor.Location = new System.Drawing.Point(151, 211);
            this.GroupEnvColor.Name = "GroupEnvColor";
            this.GroupEnvColor.Size = new System.Drawing.Size(76, 17);
            this.GroupEnvColor.TabIndex = 41;
            this.GroupEnvColor.Text = "Env Color*";
            this.EnvironmentControlTooltip.SetToolTip(this.GroupEnvColor, "Only useful for scene render funcs that has color change animations");
            this.GroupEnvColor.UseVisualStyleBackColor = true;
            this.GroupEnvColor.CheckedChanged += new System.EventHandler(this.EnvColor_CheckedChanged);
            // 
            // GroupRenderLast
            // 
            this.GroupRenderLast.AutoSize = true;
            this.GroupRenderLast.Location = new System.Drawing.Point(306, 185);
            this.GroupRenderLast.Name = "GroupRenderLast";
            this.GroupRenderLast.Size = new System.Drawing.Size(84, 17);
            this.GroupRenderLast.TabIndex = 40;
            this.GroupRenderLast.Text = "Render Last";
            this.GroupRenderLast.UseVisualStyleBackColor = true;
            this.GroupRenderLast.CheckedChanged += new System.EventHandler(this.GroupRenderLast_CheckedChanged);
            // 
            // GroupLODDIstance
            // 
            this.GroupLODDIstance.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.GroupLODDIstance.Location = new System.Drawing.Point(182, 240);
            this.GroupLODDIstance.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.GroupLODDIstance.Name = "GroupLODDIstance";
            this.GroupLODDIstance.Size = new System.Drawing.Size(80, 20);
            this.GroupLODDIstance.TabIndex = 39;
            this.GroupLODDIstance.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.GroupLODDIstance.ValueChanged += new System.EventHandler(this.GroupLODDIstance_ValueChanged);
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(127, 242);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(49, 13);
            this.label102.TabIndex = 38;
            this.label102.Text = "Distance";
            // 
            // GroupLODGroup
            // 
            this.GroupLODGroup.Enabled = false;
            this.GroupLODGroup.Hexadecimal = true;
            this.GroupLODGroup.Location = new System.Drawing.Point(63, 240);
            this.GroupLODGroup.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GroupLODGroup.Name = "GroupLODGroup";
            this.GroupLODGroup.Size = new System.Drawing.Size(58, 20);
            this.GroupLODGroup.TabIndex = 37;
            this.GroupLODGroup.ValueChanged += new System.EventHandler(this.GroupLODGroup_ValueChanged);
            // 
            // GroupLod
            // 
            this.GroupLod.AutoSize = true;
            this.GroupLod.Location = new System.Drawing.Point(9, 241);
            this.GroupLod.Name = "GroupLod";
            this.GroupLod.Size = new System.Drawing.Size(48, 17);
            this.GroupLod.TabIndex = 36;
            this.GroupLod.Text = "LOD";
            this.GroupLod.UseVisualStyleBackColor = true;
            this.GroupLod.CheckedChanged += new System.EventHandler(this.GroupLod_CheckedChanged);
            // 
            // GroupSmoothRgbaEdges
            // 
            this.GroupSmoothRgbaEdges.AutoSize = true;
            this.GroupSmoothRgbaEdges.Location = new System.Drawing.Point(272, 256);
            this.GroupSmoothRgbaEdges.Name = "GroupSmoothRgbaEdges";
            this.GroupSmoothRgbaEdges.Size = new System.Drawing.Size(131, 17);
            this.GroupSmoothRgbaEdges.TabIndex = 35;
            this.GroupSmoothRgbaEdges.Text = "Smooth RGBA edges*";
            this.EnvironmentControlTooltip.SetToolTip(this.GroupSmoothRgbaEdges, "Graphical issues unless there\'s skybox / void behind");
            this.GroupSmoothRgbaEdges.UseVisualStyleBackColor = true;
            this.GroupSmoothRgbaEdges.CheckedChanged += new System.EventHandler(this.GroupSmoothRgbaEdges_CheckedChanged);
            // 
            // AnimationLabel
            // 
            this.AnimationLabel.AutoSize = true;
            this.AnimationLabel.Location = new System.Drawing.Point(6, 247);
            this.AnimationLabel.Name = "AnimationLabel";
            this.AnimationLabel.Size = new System.Drawing.Size(16, 13);
            this.AnimationLabel.TabIndex = 34;
            this.AnimationLabel.Text = "   ";
            // 
            // GroupAnimatedBank
            // 
            this.GroupAnimatedBank.Enabled = false;
            this.GroupAnimatedBank.Hexadecimal = true;
            this.GroupAnimatedBank.Location = new System.Drawing.Point(85, 210);
            this.GroupAnimatedBank.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.GroupAnimatedBank.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.GroupAnimatedBank.Name = "GroupAnimatedBank";
            this.GroupAnimatedBank.Size = new System.Drawing.Size(58, 20);
            this.GroupAnimatedBank.TabIndex = 33;
            this.GroupAnimatedBank.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.GroupAnimatedBank.ValueChanged += new System.EventHandler(this.GroupAnimatedBank_ValueChanged);
            // 
            // GroupIgnoreFog
            // 
            this.GroupIgnoreFog.AutoSize = true;
            this.GroupIgnoreFog.Location = new System.Drawing.Point(306, 166);
            this.GroupIgnoreFog.Name = "GroupIgnoreFog";
            this.GroupIgnoreFog.Size = new System.Drawing.Size(77, 17);
            this.GroupIgnoreFog.TabIndex = 32;
            this.GroupIgnoreFog.Text = "Ignore Fog";
            this.GroupIgnoreFog.UseVisualStyleBackColor = true;
            this.GroupIgnoreFog.CheckedChanged += new System.EventHandler(this.GroupIgnoreFog_CheckedChanged);
            // 
            // Group2AxisBillboard
            // 
            this.Group2AxisBillboard.AutoSize = true;
            this.Group2AxisBillboard.Location = new System.Drawing.Point(306, 148);
            this.Group2AxisBillboard.Name = "Group2AxisBillboard";
            this.Group2AxisBillboard.Size = new System.Drawing.Size(99, 17);
            this.Group2AxisBillboard.TabIndex = 31;
            this.Group2AxisBillboard.Text = "2-axis billboard*";
            this.EnvironmentControlTooltip.SetToolTip(this.Group2AxisBillboard, "Majoras Mask only. For debug Rom, apply the patch in Tools. Other OoT Roms are un" +
        "supported.\r\nThe group has to face -Y axis for billboard effect to work!");
            this.Group2AxisBillboard.UseVisualStyleBackColor = true;
            this.Group2AxisBillboard.CheckedChanged += new System.EventHandler(this.Group2AxisBillboard_CheckedChanged);
            // 
            // GroupBillboard
            // 
            this.GroupBillboard.AutoSize = true;
            this.GroupBillboard.Location = new System.Drawing.Point(306, 130);
            this.GroupBillboard.Name = "GroupBillboard";
            this.GroupBillboard.Size = new System.Drawing.Size(70, 17);
            this.GroupBillboard.TabIndex = 30;
            this.GroupBillboard.Text = "Billboard*";
            this.EnvironmentControlTooltip.SetToolTip(this.GroupBillboard, "The group has to face -Y axis for billboard effect to work!");
            this.GroupBillboard.UseVisualStyleBackColor = true;
            this.GroupBillboard.CheckedChanged += new System.EventHandler(this.GroupBillboard_CheckedChanged);
            // 
            // GroupPixelated
            // 
            this.GroupPixelated.AutoSize = true;
            this.GroupPixelated.Location = new System.Drawing.Point(306, 112);
            this.GroupPixelated.Name = "GroupPixelated";
            this.GroupPixelated.Size = new System.Drawing.Size(69, 17);
            this.GroupPixelated.TabIndex = 29;
            this.GroupPixelated.Text = "Pixelated";
            this.GroupPixelated.UseVisualStyleBackColor = true;
            this.GroupPixelated.CheckedChanged += new System.EventHandler(this.GroupPixelated_CheckedChanged);
            // 
            // GroupDecal
            // 
            this.GroupDecal.AutoSize = true;
            this.GroupDecal.Location = new System.Drawing.Point(306, 94);
            this.GroupDecal.Name = "GroupDecal";
            this.GroupDecal.Size = new System.Drawing.Size(54, 17);
            this.GroupDecal.TabIndex = 28;
            this.GroupDecal.Text = "Decal";
            this.GroupDecal.UseVisualStyleBackColor = true;
            this.GroupDecal.CheckedChanged += new System.EventHandler(this.GroupDecal_CheckedChanged);
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(6, 101);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(92, 13);
            this.label93.TabIndex = 27;
            this.label93.Text = "Texture Shift X/Y:";
            // 
            // ShiftTNumeric
            // 
            this.ShiftTNumeric.Location = new System.Drawing.Point(220, 97);
            this.ShiftTNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ShiftTNumeric.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.ShiftTNumeric.Name = "ShiftTNumeric";
            this.ShiftTNumeric.Size = new System.Drawing.Size(80, 20);
            this.ShiftTNumeric.TabIndex = 26;
            this.ShiftTNumeric.ValueChanged += new System.EventHandler(this.ShiftTNumeric_ValueChanged);
            // 
            // ShiftSNumeric
            // 
            this.ShiftSNumeric.Location = new System.Drawing.Point(122, 97);
            this.ShiftSNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ShiftSNumeric.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.ShiftSNumeric.Name = "ShiftSNumeric";
            this.ShiftSNumeric.Size = new System.Drawing.Size(80, 20);
            this.ShiftSNumeric.TabIndex = 25;
            this.ShiftSNumeric.ValueChanged += new System.EventHandler(this.ShiftSNumeric_ValueChanged);
            // 
            // ReverseLightCheckBox
            // 
            this.ReverseLightCheckBox.AutoSize = true;
            this.ReverseLightCheckBox.Location = new System.Drawing.Point(272, 272);
            this.ReverseLightCheckBox.Name = "ReverseLightCheckBox";
            this.ReverseLightCheckBox.Size = new System.Drawing.Size(103, 17);
            this.ReverseLightCheckBox.TabIndex = 24;
            this.ReverseLightCheckBox.Text = "Use vertex color";
            this.ReverseLightCheckBox.UseVisualStyleBackColor = true;
            this.ReverseLightCheckBox.CheckedChanged += new System.EventHandler(this.ReverseLightCheckBox_CheckedChanged);
            // 
            // GroupMultitextureAlpha
            // 
            this.GroupMultitextureAlpha.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.GroupMultitextureAlpha.Location = new System.Drawing.Point(106, 180);
            this.GroupMultitextureAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GroupMultitextureAlpha.Name = "GroupMultitextureAlpha";
            this.GroupMultitextureAlpha.Size = new System.Drawing.Size(80, 20);
            this.GroupMultitextureAlpha.TabIndex = 23;
            this.GroupMultitextureAlpha.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GroupMultitextureAlpha.ValueChanged += new System.EventHandler(this.GroupMultitextureAlpha_ValueChanged);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(6, 182);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(94, 13);
            this.label79.TabIndex = 22;
            this.label79.Text = "Multitexture Alpha:";
            // 
            // GroupMetallic
            // 
            this.GroupMetallic.AutoSize = true;
            this.GroupMetallic.Location = new System.Drawing.Point(306, 76);
            this.GroupMetallic.Name = "GroupMetallic";
            this.GroupMetallic.Size = new System.Drawing.Size(62, 17);
            this.GroupMetallic.TabIndex = 21;
            this.GroupMetallic.Text = "Metallic";
            this.GroupMetallic.UseVisualStyleBackColor = true;
            this.GroupMetallic.CheckedChanged += new System.EventHandler(this.GroupMetallic_CheckedChanged);
            // 
            // GroupAnimated
            // 
            this.GroupAnimated.AutoSize = true;
            this.GroupAnimated.Location = new System.Drawing.Point(9, 211);
            this.GroupAnimated.Name = "GroupAnimated";
            this.GroupAnimated.Size = new System.Drawing.Size(70, 17);
            this.GroupAnimated.TabIndex = 20;
            this.GroupAnimated.Text = "Animated";
            this.GroupAnimated.UseVisualStyleBackColor = true;
            this.GroupAnimated.CheckedChanged += new System.EventHandler(this.AnimatedCheckBox);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(4, 156);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(110, 13);
            this.label34.TabIndex = 19;
            this.label34.Text = "Multitexture Shift X/Y:";
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(218, 152);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown6.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown6.TabIndex = 18;
            this.numericUpDown6.ValueChanged += new System.EventHandler(this.numericUpDown6_ValueChanged);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(120, 152);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown5.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown5.TabIndex = 17;
            this.numericUpDown5.ValueChanged += new System.EventHandler(this.numericUpDown5_ValueChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(4, 130);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(104, 13);
            this.label30.TabIndex = 16;
            this.label30.Text = "Multitexture Material:";
            // 
            // MultiTextureComboBox
            // 
            this.MultiTextureComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MultiTextureComboBox.FormattingEnabled = true;
            this.MultiTextureComboBox.Location = new System.Drawing.Point(114, 125);
            this.MultiTextureComboBox.Name = "MultiTextureComboBox";
            this.MultiTextureComboBox.Size = new System.Drawing.Size(186, 21);
            this.MultiTextureComboBox.TabIndex = 15;
            this.MultiTextureComboBox.DropDown += new System.EventHandler(this.AdjustWidthMaterials_DropDown);
            this.MultiTextureComboBox.SelectionChangeCommitted += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(194, 73);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(106, 17);
            this.checkBox3.TabIndex = 14;
            this.checkBox3.Text = "Backface Culling";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // GroupPolygonType
            // 
            this.GroupPolygonType.Location = new System.Drawing.Point(96, 72);
            this.GroupPolygonType.Name = "GroupPolygonType";
            this.GroupPolygonType.Size = new System.Drawing.Size(80, 20);
            this.GroupPolygonType.TabIndex = 13;
            this.GroupPolygonType.ValueChanged += new System.EventHandler(this.GroupPolygonType_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Polygon Type:";
            // 
            // pictureBox7
            // 
            this.pictureBox7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox7.Location = new System.Drawing.Point(290, 19);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(80, 20);
            this.pictureBox7.TabIndex = 11;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.DoubleClick += new System.EventHandler(this.pictureBox7_DoubleClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(200, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Tint:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(200, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Texture Tiling Y:";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Wrap",
            "Mirror",
            "Clamp"});
            this.comboBox2.Location = new System.Drawing.Point(290, 45);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(80, 21);
            this.comboBox2.TabIndex = 8;
            this.comboBox2.SelectionChangeCommitted += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Wrap",
            "Mirror",
            "Clamp"});
            this.comboBox1.Location = new System.Drawing.Point(96, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(80, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Texture Tiling X:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(96, 19);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown2.TabIndex = 5;
            this.numericUpDown2.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Alpha:";
            // 
            // RoomInjectionOffset
            // 
            this.RoomInjectionOffset.AllowHex = true;
            this.RoomInjectionOffset.Digits = 8;
            this.RoomInjectionOffset.Enabled = false;
            this.RoomInjectionOffset.Location = new System.Drawing.Point(101, 155);
            this.RoomInjectionOffset.Name = "RoomInjectionOffset";
            this.RoomInjectionOffset.Size = new System.Drawing.Size(98, 20);
            this.RoomInjectionOffset.TabIndex = 3;
            this.RoomInjectionOffset.Visible = false;
            this.RoomInjectionOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoomInjectionOffset_KeyDown);
            this.RoomInjectionOffset.Leave += new System.EventHandler(this.RoomInjectionOffset_Leave);
            // 
            // GroupList
            // 
            this.GroupList.FormattingEnabled = true;
            this.GroupList.IntegralHeight = false;
            this.GroupList.Location = new System.Drawing.Point(6, 158);
            this.GroupList.Name = "GroupList";
            this.GroupList.Size = new System.Drawing.Size(397, 138);
            this.GroupList.TabIndex = 4;
            this.GroupList.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            this.GroupList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox2_MouseDown);
            // 
            // DeleteRoom
            // 
            this.DeleteRoom.Location = new System.Drawing.Point(312, 6);
            this.DeleteRoom.Name = "DeleteRoom";
            this.DeleteRoom.Size = new System.Drawing.Size(91, 23);
            this.DeleteRoom.TabIndex = 1;
            this.DeleteRoom.Text = "Delete Room";
            this.DeleteRoom.UseVisualStyleBackColor = true;
            this.DeleteRoom.Click += new System.EventHandler(this.DeleteRoom_Click);
            // 
            // AddRoom
            // 
            this.AddRoom.Location = new System.Drawing.Point(6, 6);
            this.AddRoom.Name = "AddRoom";
            this.AddRoom.Size = new System.Drawing.Size(107, 23);
            this.AddRoom.TabIndex = 0;
            this.AddRoom.Text = "Add Single Room";
            this.AddRoom.UseVisualStyleBackColor = true;
            this.AddRoom.Click += new System.EventHandler(this.AddRoom_Click);
            // 
            // RoomList
            // 
            this.RoomList.FormattingEnabled = true;
            this.RoomList.IntegralHeight = false;
            this.RoomList.Location = new System.Drawing.Point(6, 35);
            this.RoomList.Name = "RoomList";
            this.RoomList.Size = new System.Drawing.Size(397, 117);
            this.RoomList.TabIndex = 2;
            this.RoomList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.RoomList.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // tabSceneEnv
            // 
            this.tabSceneEnv.Controls.Add(this.PrerenderedGroupBox);
            this.tabSceneEnv.Controls.Add(this.AlternateHeadersGroupBox);
            this.tabSceneEnv.Controls.Add(this.PrerenderedCheckbox);
            this.tabSceneEnv.Controls.Add(this.groupBox10);
            this.tabSceneEnv.Controls.Add(this.groupBox7);
            this.tabSceneEnv.Controls.Add(this.groupBox5);
            this.tabSceneEnv.Location = new System.Drawing.Point(4, 40);
            this.tabSceneEnv.Name = "tabSceneEnv";
            this.tabSceneEnv.Size = new System.Drawing.Size(411, 676);
            this.tabSceneEnv.TabIndex = 7;
            this.tabSceneEnv.Text = "Scene Env.";
            this.tabSceneEnv.UseVisualStyleBackColor = true;
            // 
            // PrerenderedGroupBox
            // 
            this.PrerenderedGroupBox.Controls.Add(this.DeleteJFIF);
            this.PrerenderedGroupBox.Controls.Add(this.niceLine15);
            this.PrerenderedGroupBox.Controls.Add(this.JFIFLabel);
            this.PrerenderedGroupBox.Controls.Add(this.PrerenderedList);
            this.PrerenderedGroupBox.Controls.Add(this.LoadJFIF);
            this.PrerenderedGroupBox.Location = new System.Drawing.Point(10, 550);
            this.PrerenderedGroupBox.Name = "PrerenderedGroupBox";
            this.PrerenderedGroupBox.Size = new System.Drawing.Size(393, 88);
            this.PrerenderedGroupBox.TabIndex = 44;
            this.PrerenderedGroupBox.TabStop = false;
            this.PrerenderedGroupBox.Text = "Prerendered Scene";
            // 
            // DeleteJFIF
            // 
            this.DeleteJFIF.Location = new System.Drawing.Point(250, 19);
            this.DeleteJFIF.Name = "DeleteJFIF";
            this.DeleteJFIF.Size = new System.Drawing.Size(120, 23);
            this.DeleteJFIF.TabIndex = 45;
            this.DeleteJFIF.Text = "Delete background";
            this.DeleteJFIF.UseVisualStyleBackColor = true;
            this.DeleteJFIF.Click += new System.EventHandler(this.DeleteJFIF_Click);
            // 
            // niceLine15
            // 
            this.niceLine15.Location = new System.Drawing.Point(9, 48);
            this.niceLine15.Name = "niceLine15";
            this.niceLine15.Size = new System.Drawing.Size(384, 15);
            this.niceLine15.TabIndex = 44;
            this.niceLine15.TabStop = false;
            // 
            // JFIFLabel
            // 
            this.JFIFLabel.AutoSize = true;
            this.JFIFLabel.Location = new System.Drawing.Point(9, 66);
            this.JFIFLabel.Name = "JFIFLabel";
            this.JFIFLabel.Size = new System.Drawing.Size(27, 13);
            this.JFIFLabel.TabIndex = 41;
            this.JFIFLabel.Text = "JFIF";
            // 
            // PrerenderedList
            // 
            this.PrerenderedList.Location = new System.Drawing.Point(9, 22);
            this.PrerenderedList.Name = "PrerenderedList";
            this.PrerenderedList.Size = new System.Drawing.Size(65, 20);
            this.PrerenderedList.TabIndex = 44;
            this.PrerenderedList.ValueChanged += new System.EventHandler(this.PrerenderedList_ValueChanged);
            // 
            // LoadJFIF
            // 
            this.LoadJFIF.Location = new System.Drawing.Point(124, 19);
            this.LoadJFIF.Name = "LoadJFIF";
            this.LoadJFIF.Size = new System.Drawing.Size(120, 23);
            this.LoadJFIF.TabIndex = 22;
            this.LoadJFIF.Text = "Load background";
            this.LoadJFIF.UseVisualStyleBackColor = true;
            this.LoadJFIF.Click += new System.EventHandler(this.LoadJFIF_Click);
            // 
            // AlternateHeadersGroupBox
            // 
            this.AlternateHeadersGroupBox.Controls.Add(this.SceneHeaderCopyList);
            this.AlternateHeadersGroupBox.Controls.Add(this.SceneHeaderUsedLabel);
            this.AlternateHeadersGroupBox.Controls.Add(this.SceneHeaderSameCheckbox);
            this.AlternateHeadersGroupBox.Controls.Add(this.DeleteSceneHeaderButton);
            this.AlternateHeadersGroupBox.Controls.Add(this.AddSceneHeaderButton);
            this.AlternateHeadersGroupBox.Controls.Add(this.SceneHeaderList);
            this.AlternateHeadersGroupBox.Controls.Add(this.niceLine9);
            this.AlternateHeadersGroupBox.Location = new System.Drawing.Point(10, 452);
            this.AlternateHeadersGroupBox.Name = "AlternateHeadersGroupBox";
            this.AlternateHeadersGroupBox.Size = new System.Drawing.Size(393, 92);
            this.AlternateHeadersGroupBox.TabIndex = 43;
            this.AlternateHeadersGroupBox.TabStop = false;
            this.AlternateHeadersGroupBox.Text = "Alternate Scene Headers";
            // 
            // SceneHeaderCopyList
            // 
            this.SceneHeaderCopyList.AlwaysFireValueChanged = false;
            this.SceneHeaderCopyList.DisplayDigits = 1;
            this.SceneHeaderCopyList.DoValueRollover = false;
            this.SceneHeaderCopyList.Enabled = false;
            this.SceneHeaderCopyList.Hexadecimal = true;
            this.SceneHeaderCopyList.IncrementMouseWheel = 1;
            this.SceneHeaderCopyList.Location = new System.Drawing.Point(307, 61);
            this.SceneHeaderCopyList.Maximum = new decimal(new int[] {
            16777215,
            0,
            0,
            0});
            this.SceneHeaderCopyList.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SceneHeaderCopyList.Name = "SceneHeaderCopyList";
            this.SceneHeaderCopyList.ShiftMultiplier = 1;
            this.SceneHeaderCopyList.Size = new System.Drawing.Size(80, 20);
            this.SceneHeaderCopyList.TabIndex = 27;
            this.SceneHeaderCopyList.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SceneHeaderCopyList.ValueChanged += new System.EventHandler(this.SceneHeaderCopyList_ValueChanged);
            // 
            // SceneHeaderUsedLabel
            // 
            this.SceneHeaderUsedLabel.AutoSize = true;
            this.SceneHeaderUsedLabel.Location = new System.Drawing.Point(6, 65);
            this.SceneHeaderUsedLabel.Name = "SceneHeaderUsedLabel";
            this.SceneHeaderUsedLabel.Size = new System.Drawing.Size(98, 13);
            this.SceneHeaderUsedLabel.TabIndex = 43;
            this.SceneHeaderUsedLabel.Text = "Used in: Child (day)";
            // 
            // SceneHeaderSameCheckbox
            // 
            this.SceneHeaderSameCheckbox.AutoSize = true;
            this.SceneHeaderSameCheckbox.Checked = true;
            this.SceneHeaderSameCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SceneHeaderSameCheckbox.Location = new System.Drawing.Point(188, 62);
            this.SceneHeaderSameCheckbox.Name = "SceneHeaderSameCheckbox";
            this.SceneHeaderSameCheckbox.Size = new System.Drawing.Size(106, 17);
            this.SceneHeaderSameCheckbox.TabIndex = 41;
            this.SceneHeaderSameCheckbox.Text = "Same as header:";
            this.SceneHeaderSameCheckbox.UseVisualStyleBackColor = true;
            this.SceneHeaderSameCheckbox.CheckedChanged += new System.EventHandler(this.SceneHeaderSameCheckbox_CheckedChanged);
            // 
            // DeleteSceneHeaderButton
            // 
            this.DeleteSceneHeaderButton.Location = new System.Drawing.Point(250, 19);
            this.DeleteSceneHeaderButton.Name = "DeleteSceneHeaderButton";
            this.DeleteSceneHeaderButton.Size = new System.Drawing.Size(120, 23);
            this.DeleteSceneHeaderButton.TabIndex = 9;
            this.DeleteSceneHeaderButton.Text = "Delete Header";
            this.DeleteSceneHeaderButton.UseVisualStyleBackColor = true;
            this.DeleteSceneHeaderButton.Click += new System.EventHandler(this.DeleteSceneHeaderButton_Click);
            // 
            // AddSceneHeaderButton
            // 
            this.AddSceneHeaderButton.BackColor = System.Drawing.Color.Transparent;
            this.AddSceneHeaderButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddSceneHeaderButton.Location = new System.Drawing.Point(124, 19);
            this.AddSceneHeaderButton.Name = "AddSceneHeaderButton";
            this.AddSceneHeaderButton.Size = new System.Drawing.Size(120, 23);
            this.AddSceneHeaderButton.TabIndex = 8;
            this.AddSceneHeaderButton.Text = "Add Header";
            this.AddSceneHeaderButton.UseVisualStyleBackColor = false;
            this.AddSceneHeaderButton.Click += new System.EventHandler(this.AddSceneHeaderButton_Click);
            // 
            // SceneHeaderList
            // 
            this.SceneHeaderList.Location = new System.Drawing.Point(9, 22);
            this.SceneHeaderList.Name = "SceneHeaderList";
            this.SceneHeaderList.Size = new System.Drawing.Size(65, 20);
            this.SceneHeaderList.TabIndex = 7;
            this.SceneHeaderList.ValueChanged += new System.EventHandler(this.SceneHeaderList_ValueChanged);
            // 
            // niceLine9
            // 
            this.niceLine9.Location = new System.Drawing.Point(9, 46);
            this.niceLine9.Name = "niceLine9";
            this.niceLine9.Size = new System.Drawing.Size(384, 15);
            this.niceLine9.TabIndex = 20;
            this.niceLine9.TabStop = false;
            // 
            // PrerenderedCheckbox
            // 
            this.PrerenderedCheckbox.AutoSize = true;
            this.PrerenderedCheckbox.Location = new System.Drawing.Point(24, 656);
            this.PrerenderedCheckbox.Name = "PrerenderedCheckbox";
            this.PrerenderedCheckbox.Size = new System.Drawing.Size(118, 17);
            this.PrerenderedCheckbox.TabIndex = 40;
            this.PrerenderedCheckbox.Text = "Prerendered Scene";
            this.PrerenderedCheckbox.UseVisualStyleBackColor = true;
            this.PrerenderedCheckbox.Visible = false;
            this.PrerenderedCheckbox.Click += new System.EventHandler(this.PrerenderedCheckbox_CheckedChanged);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label45);
            this.groupBox10.Controls.Add(this.WorldMapComboBox);
            this.groupBox10.Controls.Add(this.label44);
            this.groupBox10.Controls.Add(this.CameraMovementComboBox);
            this.groupBox10.Location = new System.Drawing.Point(10, 122);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(393, 90);
            this.groupBox10.TabIndex = 42;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Camera && Worldmap";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(9, 57);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(104, 13);
            this.label45.TabIndex = 41;
            this.label45.Text = "World map location: ";
            // 
            // WorldMapComboBox
            // 
            this.WorldMapComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WorldMapComboBox.FormattingEnabled = true;
            this.WorldMapComboBox.Location = new System.Drawing.Point(121, 54);
            this.WorldMapComboBox.Name = "WorldMapComboBox";
            this.WorldMapComboBox.Size = new System.Drawing.Size(208, 21);
            this.WorldMapComboBox.TabIndex = 42;
            this.WorldMapComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.WorldMapComboBox.SelectedValueChanged += new System.EventHandler(this.WorldmapChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(9, 27);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(98, 13);
            this.label44.TabIndex = 39;
            this.label44.Text = "Camera movement:";
            // 
            // CameraMovementComboBox
            // 
            this.CameraMovementComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CameraMovementComboBox.FormattingEnabled = true;
            this.CameraMovementComboBox.Location = new System.Drawing.Point(121, 24);
            this.CameraMovementComboBox.Name = "CameraMovementComboBox";
            this.CameraMovementComboBox.Size = new System.Drawing.Size(208, 21);
            this.CameraMovementComboBox.TabIndex = 40;
            this.CameraMovementComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.CameraMovementComboBox.SelectedValueChanged += new System.EventHandler(this.CameraMovementChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label36);
            this.groupBox7.Controls.Add(this.SoundSpec);
            this.groupBox7.Controls.Add(this.label39);
            this.groupBox7.Controls.Add(this.CloudyCheckBox);
            this.groupBox7.Controls.Add(this.SkyboxComboBox);
            this.groupBox7.Controls.Add(this.NightSFXComboBox);
            this.groupBox7.Controls.Add(this.label35);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.SongComboBox);
            this.groupBox7.Location = new System.Drawing.Point(10, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(393, 104);
            this.groupBox7.TabIndex = 30;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Skybox && Sound";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(254, 45);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(75, 13);
            this.label36.TabIndex = 41;
            this.label36.Text = "Sound Space:";
            // 
            // SoundSpec
            // 
            this.SoundSpec.BackColor = System.Drawing.SystemColors.Window;
            this.SoundSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SoundSpec.FormattingEnabled = true;
            this.SoundSpec.Location = new System.Drawing.Point(254, 69);
            this.SoundSpec.Name = "SoundSpec";
            this.SoundSpec.Size = new System.Drawing.Size(133, 21);
            this.SoundSpec.TabIndex = 40;
            this.SoundSpec.SelectedIndexChanged += new System.EventHandler(this.SoundSpec_SelectedIndexChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(9, 20);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(68, 13);
            this.label39.TabIndex = 4;
            this.label39.Text = "Skybox type:";
            // 
            // CloudyCheckBox
            // 
            this.CloudyCheckBox.AutoSize = true;
            this.CloudyCheckBox.Location = new System.Drawing.Point(254, 19);
            this.CloudyCheckBox.Name = "CloudyCheckBox";
            this.CloudyCheckBox.Size = new System.Drawing.Size(58, 17);
            this.CloudyCheckBox.TabIndex = 39;
            this.CloudyCheckBox.Text = "Cloudy";
            this.CloudyCheckBox.UseVisualStyleBackColor = true;
            this.CloudyCheckBox.CheckedChanged += new System.EventHandler(this.CloudyCheckBox_CheckedChanged);
            // 
            // SkyboxComboBox
            // 
            this.SkyboxComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SkyboxComboBox.FormattingEnabled = true;
            this.SkyboxComboBox.Location = new System.Drawing.Point(92, 15);
            this.SkyboxComboBox.Name = "SkyboxComboBox";
            this.SkyboxComboBox.Size = new System.Drawing.Size(152, 21);
            this.SkyboxComboBox.TabIndex = 33;
            this.SkyboxComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.SkyboxComboBox.SelectedValueChanged += new System.EventHandler(this.SkyboxComboBox_SelectedValueChanged);
            // 
            // NightSFXComboBox
            // 
            this.NightSFXComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NightSFXComboBox.FormattingEnabled = true;
            this.NightSFXComboBox.Location = new System.Drawing.Point(92, 69);
            this.NightSFXComboBox.Name = "NightSFXComboBox";
            this.NightSFXComboBox.Size = new System.Drawing.Size(152, 21);
            this.NightSFXComboBox.TabIndex = 35;
            this.NightSFXComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.NightSFXComboBox.SelectedValueChanged += new System.EventHandler(this.NightSFXChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(9, 72);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(77, 13);
            this.label35.TabIndex = 34;
            this.label35.Text = "Nighttime SFX:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "BGM:";
            // 
            // SongComboBox
            // 
            this.SongComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SongComboBox.FormattingEnabled = true;
            this.SongComboBox.Location = new System.Drawing.Point(92, 42);
            this.SongComboBox.Name = "SongComboBox";
            this.SongComboBox.Size = new System.Drawing.Size(152, 21);
            this.SongComboBox.TabIndex = 33;
            this.SongComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.SongComboBox.SelectedValueChanged += new System.EventHandler(this.SongOnChange);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.UnusedCommandCheckBox);
            this.groupBox5.Controls.Add(this.panel3);
            this.groupBox5.Controls.Add(this.button11);
            this.groupBox5.Controls.Add(this.button12);
            this.groupBox5.Controls.Add(this.EnvironmentSelect);
            this.groupBox5.Controls.Add(this.niceLine3);
            this.groupBox5.Location = new System.Drawing.Point(10, 218);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(393, 228);
            this.groupBox5.TabIndex = 29;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Environment Settings";
            // 
            // UnusedCommandCheckBox
            // 
            this.UnusedCommandCheckBox.AutoSize = true;
            this.UnusedCommandCheckBox.Checked = true;
            this.UnusedCommandCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UnusedCommandCheckBox.Location = new System.Drawing.Point(6, 201);
            this.UnusedCommandCheckBox.Name = "UnusedCommandCheckBox";
            this.UnusedCommandCheckBox.Size = new System.Drawing.Size(169, 17);
            this.UnusedCommandCheckBox.TabIndex = 40;
            this.UnusedCommandCheckBox.Text = "Use lights A B (overworld light)";
            this.UnusedCommandCheckBox.UseVisualStyleBackColor = true;
            this.UnusedCommandCheckBox.CheckedChanged += new System.EventHandler(this.UnusedCommandCheckBox_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label29);
            this.panel3.Controls.Add(this.label27);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.ViewNormalCopyEnvB);
            this.panel3.Controls.Add(this.ViewNormalCopyEnvA);
            this.panel3.Controls.Add(this.EnvironmentDirectionBZ);
            this.panel3.Controls.Add(this.EnvironmentDirectionBY);
            this.panel3.Controls.Add(this.EnvironmentDirectionAZ);
            this.panel3.Controls.Add(this.EnvironmentDirectionAY);
            this.panel3.Controls.Add(this.EnvironmentDirectionBX);
            this.panel3.Controls.Add(this.EnvironmentDirectionAX);
            this.panel3.Controls.Add(this.DrawDistance);
            this.panel3.Controls.Add(this.FogUnknown);
            this.panel3.Controls.Add(this.FogDistance);
            this.panel3.Controls.Add(this.label85);
            this.panel3.Controls.Add(this.label32);
            this.panel3.Controls.Add(this.label31);
            this.panel3.Controls.Add(this.LightingE);
            this.panel3.Controls.Add(this.FogColor);
            this.panel3.Controls.Add(this.label28);
            this.panel3.Controls.Add(this.LightingC);
            this.panel3.Controls.Add(this.LightingA);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Location = new System.Drawing.Point(3, 58);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(384, 136);
            this.panel3.TabIndex = 21;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 60);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(44, 13);
            this.label29.TabIndex = 35;
            this.label29.Text = "Color B:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Enabled = false;
            this.label27.Location = new System.Drawing.Point(192, 60);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(33, 13);
            this.label27.TabIndex = 34;
            this.label27.Text = "Dir B:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Enabled = false;
            this.label21.Location = new System.Drawing.Point(192, 35);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(33, 13);
            this.label21.TabIndex = 33;
            this.label21.Text = "Dir A:";
            // 
            // ViewNormalCopyEnvB
            // 
            this.ViewNormalCopyEnvB.Location = new System.Drawing.Point(341, 56);
            this.ViewNormalCopyEnvB.Name = "ViewNormalCopyEnvB";
            this.ViewNormalCopyEnvB.Size = new System.Drawing.Size(26, 22);
            this.ViewNormalCopyEnvB.TabIndex = 32;
            this.ViewNormalCopyEnvB.UseVisualStyleBackColor = true;
            this.ViewNormalCopyEnvB.Click += new System.EventHandler(this.ViewNormalCopyEnvB_Click);
            // 
            // ViewNormalCopyEnvA
            // 
            this.ViewNormalCopyEnvA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ViewNormalCopyEnvA.Location = new System.Drawing.Point(341, 31);
            this.ViewNormalCopyEnvA.Name = "ViewNormalCopyEnvA";
            this.ViewNormalCopyEnvA.Size = new System.Drawing.Size(26, 22);
            this.ViewNormalCopyEnvA.TabIndex = 31;
            this.ViewNormalCopyEnvA.UseVisualStyleBackColor = true;
            this.ViewNormalCopyEnvA.Click += new System.EventHandler(this.ViewNormalCopyEnvA_Click);
            // 
            // EnvironmentDirectionBZ
            // 
            this.EnvironmentDirectionBZ.AlwaysFireValueChanged = false;
            this.EnvironmentDirectionBZ.DisplayDigits = 2;
            this.EnvironmentDirectionBZ.DoValueRollover = false;
            this.EnvironmentDirectionBZ.Enabled = false;
            this.EnvironmentDirectionBZ.Hexadecimal = true;
            this.EnvironmentDirectionBZ.IncrementMouseWheel = 1;
            this.EnvironmentDirectionBZ.Location = new System.Drawing.Point(304, 57);
            this.EnvironmentDirectionBZ.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.EnvironmentDirectionBZ.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionBZ.Name = "EnvironmentDirectionBZ";
            this.EnvironmentDirectionBZ.ShiftMultiplier = 20;
            this.EnvironmentDirectionBZ.Size = new System.Drawing.Size(39, 20);
            this.EnvironmentDirectionBZ.TabIndex = 30;
            this.EnvironmentDirectionBZ.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionBZ.ValueChanged += new System.EventHandler(this.EnvironmentDirectionBZ_ValueChanged);
            // 
            // EnvironmentDirectionBY
            // 
            this.EnvironmentDirectionBY.AlwaysFireValueChanged = false;
            this.EnvironmentDirectionBY.DisplayDigits = 2;
            this.EnvironmentDirectionBY.DoValueRollover = false;
            this.EnvironmentDirectionBY.Enabled = false;
            this.EnvironmentDirectionBY.Hexadecimal = true;
            this.EnvironmentDirectionBY.IncrementMouseWheel = 1;
            this.EnvironmentDirectionBY.Location = new System.Drawing.Point(266, 57);
            this.EnvironmentDirectionBY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.EnvironmentDirectionBY.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionBY.Name = "EnvironmentDirectionBY";
            this.EnvironmentDirectionBY.ShiftMultiplier = 20;
            this.EnvironmentDirectionBY.Size = new System.Drawing.Size(39, 20);
            this.EnvironmentDirectionBY.TabIndex = 29;
            this.EnvironmentDirectionBY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionBY.ValueChanged += new System.EventHandler(this.EnvironmentDirectionBY_ValueChanged);
            // 
            // EnvironmentDirectionAZ
            // 
            this.EnvironmentDirectionAZ.AlwaysFireValueChanged = false;
            this.EnvironmentDirectionAZ.DisplayDigits = 2;
            this.EnvironmentDirectionAZ.DoValueRollover = false;
            this.EnvironmentDirectionAZ.Enabled = false;
            this.EnvironmentDirectionAZ.Hexadecimal = true;
            this.EnvironmentDirectionAZ.IncrementMouseWheel = 1;
            this.EnvironmentDirectionAZ.Location = new System.Drawing.Point(304, 32);
            this.EnvironmentDirectionAZ.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.EnvironmentDirectionAZ.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionAZ.Name = "EnvironmentDirectionAZ";
            this.EnvironmentDirectionAZ.ShiftMultiplier = 20;
            this.EnvironmentDirectionAZ.Size = new System.Drawing.Size(39, 20);
            this.EnvironmentDirectionAZ.TabIndex = 28;
            this.EnvironmentDirectionAZ.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionAZ.ValueChanged += new System.EventHandler(this.EnvironmentDirectionAZ_ValueChanged);
            // 
            // EnvironmentDirectionAY
            // 
            this.EnvironmentDirectionAY.AlwaysFireValueChanged = false;
            this.EnvironmentDirectionAY.DisplayDigits = 2;
            this.EnvironmentDirectionAY.DoValueRollover = false;
            this.EnvironmentDirectionAY.Enabled = false;
            this.EnvironmentDirectionAY.Hexadecimal = true;
            this.EnvironmentDirectionAY.IncrementMouseWheel = 1;
            this.EnvironmentDirectionAY.Location = new System.Drawing.Point(266, 32);
            this.EnvironmentDirectionAY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.EnvironmentDirectionAY.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionAY.Name = "EnvironmentDirectionAY";
            this.EnvironmentDirectionAY.ShiftMultiplier = 20;
            this.EnvironmentDirectionAY.Size = new System.Drawing.Size(39, 20);
            this.EnvironmentDirectionAY.TabIndex = 27;
            this.EnvironmentDirectionAY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionAY.ValueChanged += new System.EventHandler(this.EnvironmentDirectionAY_ValueChanged);
            // 
            // EnvironmentDirectionBX
            // 
            this.EnvironmentDirectionBX.AlwaysFireValueChanged = false;
            this.EnvironmentDirectionBX.DisplayDigits = 2;
            this.EnvironmentDirectionBX.DoValueRollover = false;
            this.EnvironmentDirectionBX.Enabled = false;
            this.EnvironmentDirectionBX.Hexadecimal = true;
            this.EnvironmentDirectionBX.IncrementMouseWheel = 1;
            this.EnvironmentDirectionBX.Location = new System.Drawing.Point(228, 57);
            this.EnvironmentDirectionBX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.EnvironmentDirectionBX.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionBX.Name = "EnvironmentDirectionBX";
            this.EnvironmentDirectionBX.ShiftMultiplier = 20;
            this.EnvironmentDirectionBX.Size = new System.Drawing.Size(39, 20);
            this.EnvironmentDirectionBX.TabIndex = 26;
            this.EnvironmentDirectionBX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionBX.ValueChanged += new System.EventHandler(this.EnvironmentDirectionB_ValueChanged);
            // 
            // EnvironmentDirectionAX
            // 
            this.EnvironmentDirectionAX.AlwaysFireValueChanged = false;
            this.EnvironmentDirectionAX.DisplayDigits = 2;
            this.EnvironmentDirectionAX.DoValueRollover = false;
            this.EnvironmentDirectionAX.Enabled = false;
            this.EnvironmentDirectionAX.Hexadecimal = true;
            this.EnvironmentDirectionAX.IncrementMouseWheel = 1;
            this.EnvironmentDirectionAX.Location = new System.Drawing.Point(227, 32);
            this.EnvironmentDirectionAX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.EnvironmentDirectionAX.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionAX.Name = "EnvironmentDirectionAX";
            this.EnvironmentDirectionAX.ShiftMultiplier = 20;
            this.EnvironmentDirectionAX.Size = new System.Drawing.Size(39, 20);
            this.EnvironmentDirectionAX.TabIndex = 25;
            this.EnvironmentDirectionAX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentDirectionAX.ValueChanged += new System.EventHandler(this.EnvironmentDirectionA_ValueChanged);
            // 
            // DrawDistance
            // 
            this.DrawDistance.AlwaysFireValueChanged = false;
            this.DrawDistance.DisplayDigits = 1;
            this.DrawDistance.DoValueRollover = false;
            this.DrawDistance.Enabled = false;
            this.DrawDistance.Hexadecimal = true;
            this.DrawDistance.IncrementMouseWheel = 1;
            this.DrawDistance.Location = new System.Drawing.Point(287, 107);
            this.DrawDistance.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.DrawDistance.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DrawDistance.Name = "DrawDistance";
            this.DrawDistance.ShiftMultiplier = 20;
            this.DrawDistance.Size = new System.Drawing.Size(80, 20);
            this.DrawDistance.TabIndex = 24;
            this.DrawDistance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DrawDistance.ValueChanged += new System.EventHandler(this.DrawDistance_Leave);
            // 
            // FogUnknown
            // 
            this.FogUnknown.AlwaysFireValueChanged = false;
            this.FogUnknown.DisplayDigits = 1;
            this.FogUnknown.DoValueRollover = false;
            this.FogUnknown.Enabled = false;
            this.FogUnknown.Hexadecimal = true;
            this.FogUnknown.IncrementMouseWheel = 1;
            this.FogUnknown.Location = new System.Drawing.Point(287, 82);
            this.FogUnknown.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.FogUnknown.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FogUnknown.Name = "FogUnknown";
            this.FogUnknown.ShiftMultiplier = 20;
            this.FogUnknown.Size = new System.Drawing.Size(80, 20);
            this.FogUnknown.TabIndex = 23;
            this.FogUnknown.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FogUnknown.ValueChanged += new System.EventHandler(this.FogDistance_Leave);
            // 
            // FogDistance
            // 
            this.FogDistance.AlwaysFireValueChanged = false;
            this.FogDistance.DisplayDigits = 1;
            this.FogDistance.DoValueRollover = false;
            this.FogDistance.Enabled = false;
            this.FogDistance.Hexadecimal = true;
            this.FogDistance.IncrementMouseWheel = 1;
            this.FogDistance.Location = new System.Drawing.Point(85, 107);
            this.FogDistance.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.FogDistance.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FogDistance.Name = "FogDistance";
            this.FogDistance.ShiftMultiplier = 20;
            this.FogDistance.Size = new System.Drawing.Size(80, 20);
            this.FogDistance.TabIndex = 22;
            this.FogDistance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FogDistance.ValueChanged += new System.EventHandler(this.FogDistance_Leave);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Enabled = false;
            this.label85.Location = new System.Drawing.Point(191, 85);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(90, 13);
            this.label85.TabIndex = 21;
            this.label85.Text = "Transition Speed:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Enabled = false;
            this.label32.Location = new System.Drawing.Point(192, 110);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(80, 13);
            this.label32.TabIndex = 15;
            this.label32.Text = "Draw Distance:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Enabled = false;
            this.label31.Location = new System.Drawing.Point(5, 110);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(73, 13);
            this.label31.TabIndex = 13;
            this.label31.Text = "Fog Distance:";
            // 
            // LightingE
            // 
            this.LightingE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LightingE.Location = new System.Drawing.Point(85, 57);
            this.LightingE.Name = "LightingE";
            this.LightingE.Size = new System.Drawing.Size(80, 20);
            this.LightingE.TabIndex = 12;
            this.LightingE.TabStop = false;
            this.LightingE.DoubleClick += new System.EventHandler(this.pictureBox5_DoubleClick);
            // 
            // FogColor
            // 
            this.FogColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FogColor.Location = new System.Drawing.Point(85, 82);
            this.FogColor.Name = "FogColor";
            this.FogColor.Size = new System.Drawing.Size(80, 20);
            this.FogColor.TabIndex = 11;
            this.FogColor.TabStop = false;
            this.FogColor.DoubleClick += new System.EventHandler(this.pictureBox6_DoubleClick);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(5, 85);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(55, 13);
            this.label28.TabIndex = 10;
            this.label28.Text = "Fog Color:";
            // 
            // LightingC
            // 
            this.LightingC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LightingC.Location = new System.Drawing.Point(85, 32);
            this.LightingC.Name = "LightingC";
            this.LightingC.Size = new System.Drawing.Size(80, 20);
            this.LightingC.TabIndex = 7;
            this.LightingC.TabStop = false;
            this.LightingC.DoubleClick += new System.EventHandler(this.pictureBox3_DoubleClick);
            // 
            // LightingA
            // 
            this.LightingA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LightingA.Location = new System.Drawing.Point(85, 7);
            this.LightingA.Name = "LightingA";
            this.LightingA.Size = new System.Drawing.Size(80, 20);
            this.LightingA.TabIndex = 3;
            this.LightingA.TabStop = false;
            this.LightingA.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(5, 35);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(44, 13);
            this.label26.TabIndex = 2;
            this.label26.Text = "Color A:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(5, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Env Color:";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(250, 19);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(120, 23);
            this.button11.TabIndex = 18;
            this.button11.Text = "Delete Environment";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.DeleteEnvironmentButton_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(124, 19);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(120, 23);
            this.button12.TabIndex = 17;
            this.button12.Text = "Add Environment";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.AddEnvironmentButton_Click);
            // 
            // EnvironmentSelect
            // 
            this.EnvironmentSelect.Hexadecimal = true;
            this.EnvironmentSelect.Location = new System.Drawing.Point(9, 22);
            this.EnvironmentSelect.Name = "EnvironmentSelect";
            this.EnvironmentSelect.Size = new System.Drawing.Size(65, 20);
            this.EnvironmentSelect.TabIndex = 16;
            this.EnvironmentSelect.ValueChanged += new System.EventHandler(this.EnvironmentSelect_ValueChanged);
            // 
            // niceLine3
            // 
            this.niceLine3.Location = new System.Drawing.Point(9, 45);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(381, 15);
            this.niceLine3.TabIndex = 20;
            this.niceLine3.TabStop = false;
            // 
            // tabRoomEnv
            // 
            this.tabRoomEnv.Controls.Add(this.Roomaffectedpointlightscheckbox);
            this.tabRoomEnv.Controls.Add(this.groupBox12);
            this.tabRoomEnv.Controls.Add(this.groupBox11);
            this.tabRoomEnv.Controls.Add(this.groupBox9);
            this.tabRoomEnv.Controls.Add(this.groupBox8);
            this.tabRoomEnv.Location = new System.Drawing.Point(4, 40);
            this.tabRoomEnv.Name = "tabRoomEnv";
            this.tabRoomEnv.Size = new System.Drawing.Size(411, 676);
            this.tabRoomEnv.TabIndex = 8;
            this.tabRoomEnv.Text = "Room Env.";
            this.tabRoomEnv.UseVisualStyleBackColor = true;
            // 
            // Roomaffectedpointlightscheckbox
            // 
            this.Roomaffectedpointlightscheckbox.AutoSize = true;
            this.Roomaffectedpointlightscheckbox.Location = new System.Drawing.Point(14, 469);
            this.Roomaffectedpointlightscheckbox.Name = "Roomaffectedpointlightscheckbox";
            this.Roomaffectedpointlightscheckbox.Size = new System.Drawing.Size(170, 17);
            this.Roomaffectedpointlightscheckbox.TabIndex = 44;
            this.Roomaffectedpointlightscheckbox.Text = "Room affected by point lights *";
            this.EnvironmentControlTooltip.SetToolTip(this.Roomaffectedpointlightscheckbox, "For debug Rom, apply the patch in Tools. Other OoT Roms are unsupported.");
            this.Roomaffectedpointlightscheckbox.UseVisualStyleBackColor = true;
            this.Roomaffectedpointlightscheckbox.CheckedChanged += new System.EventHandler(this.roomaffectedbypointlights_CheckedChanged);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.panel6);
            this.groupBox12.Controls.Add(this.AdditionalLightDelete);
            this.groupBox12.Controls.Add(this.AdditionalLightAdd);
            this.groupBox12.Controls.Add(this.AdditionalLightSelect);
            this.groupBox12.Controls.Add(this.niceLine5);
            this.groupBox12.Location = new System.Drawing.Point(9, 298);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(396, 165);
            this.groupBox12.TabIndex = 42;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Additional lights  (unused)";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.PointLightCheckBox);
            this.panel6.Controls.Add(this.AdditionalLightColor);
            this.panel6.Controls.Add(this.AdditionalLightDirectionLabel3);
            this.panel6.Controls.Add(this.AdditionalLightRadius);
            this.panel6.Controls.Add(this.AdditionalLightLabel1);
            this.panel6.Controls.Add(this.AdditionalLightPointLabel1);
            this.panel6.Controls.Add(this.AdditionalLightZPos);
            this.panel6.Controls.Add(this.AdditionalLightPointLabel3);
            this.panel6.Controls.Add(this.AdditionalLightPointLabel2);
            this.panel6.Controls.Add(this.AdditionalLightNS);
            this.panel6.Controls.Add(this.AdditionalLightDirectionLabel2);
            this.panel6.Controls.Add(this.AdditionalLightYPos);
            this.panel6.Controls.Add(this.AdditionalLightEW);
            this.panel6.Controls.Add(this.AdditionalLightDirectionLabel1);
            this.panel6.Controls.Add(this.AdditionalLightXPos);
            this.panel6.Location = new System.Drawing.Point(5, 53);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(388, 109);
            this.panel6.TabIndex = 21;
            // 
            // PointLightCheckBox
            // 
            this.PointLightCheckBox.AutoSize = true;
            this.PointLightCheckBox.Location = new System.Drawing.Point(10, 7);
            this.PointLightCheckBox.Name = "PointLightCheckBox";
            this.PointLightCheckBox.Size = new System.Drawing.Size(76, 17);
            this.PointLightCheckBox.TabIndex = 43;
            this.PointLightCheckBox.Text = "Point-Light";
            this.PointLightCheckBox.UseVisualStyleBackColor = true;
            this.PointLightCheckBox.CheckedChanged += new System.EventHandler(this.PointLightCheckBox_CheckedChanged);
            // 
            // AdditionalLightColor
            // 
            this.AdditionalLightColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AdditionalLightColor.Enabled = false;
            this.AdditionalLightColor.Location = new System.Drawing.Point(261, 5);
            this.AdditionalLightColor.Name = "AdditionalLightColor";
            this.AdditionalLightColor.Size = new System.Drawing.Size(100, 20);
            this.AdditionalLightColor.TabIndex = 21;
            this.AdditionalLightColor.TabStop = false;
            this.AdditionalLightColor.DoubleClick += new System.EventHandler(this.AdditionalLightColor_DoubleClick);
            // 
            // AdditionalLightDirectionLabel3
            // 
            this.AdditionalLightDirectionLabel3.AutoSize = true;
            this.AdditionalLightDirectionLabel3.Enabled = false;
            this.AdditionalLightDirectionLabel3.Location = new System.Drawing.Point(190, 84);
            this.AdditionalLightDirectionLabel3.Name = "AdditionalLightDirectionLabel3";
            this.AdditionalLightDirectionLabel3.Size = new System.Drawing.Size(43, 13);
            this.AdditionalLightDirectionLabel3.TabIndex = 19;
            this.AdditionalLightDirectionLabel3.Text = "Radius:";
            // 
            // AdditionalLightRadius
            // 
            this.AdditionalLightRadius.AlwaysFireValueChanged = false;
            this.AdditionalLightRadius.DisplayDigits = 1;
            this.AdditionalLightRadius.DoValueRollover = false;
            this.AdditionalLightRadius.Enabled = false;
            this.AdditionalLightRadius.IncrementMouseWheel = 1;
            this.AdditionalLightRadius.Location = new System.Drawing.Point(261, 82);
            this.AdditionalLightRadius.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.AdditionalLightRadius.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightRadius.Name = "AdditionalLightRadius";
            this.AdditionalLightRadius.ShiftMultiplier = 10;
            this.AdditionalLightRadius.Size = new System.Drawing.Size(100, 20);
            this.AdditionalLightRadius.TabIndex = 18;
            this.AdditionalLightRadius.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightRadius.ValueChanged += new System.EventHandler(this.AdditionalLightRadius_ValueChanged);
            // 
            // AdditionalLightLabel1
            // 
            this.AdditionalLightLabel1.AutoSize = true;
            this.AdditionalLightLabel1.Enabled = false;
            this.AdditionalLightLabel1.Location = new System.Drawing.Point(194, 7);
            this.AdditionalLightLabel1.Name = "AdditionalLightLabel1";
            this.AdditionalLightLabel1.Size = new System.Drawing.Size(34, 13);
            this.AdditionalLightLabel1.TabIndex = 20;
            this.AdditionalLightLabel1.Text = "Color:";
            // 
            // AdditionalLightPointLabel1
            // 
            this.AdditionalLightPointLabel1.AutoSize = true;
            this.AdditionalLightPointLabel1.Enabled = false;
            this.AdditionalLightPointLabel1.Location = new System.Drawing.Point(3, 34);
            this.AdditionalLightPointLabel1.Name = "AdditionalLightPointLabel1";
            this.AdditionalLightPointLabel1.Size = new System.Drawing.Size(57, 13);
            this.AdditionalLightPointLabel1.TabIndex = 7;
            this.AdditionalLightPointLabel1.Text = "X Position:";
            // 
            // AdditionalLightZPos
            // 
            this.AdditionalLightZPos.AlwaysFireValueChanged = false;
            this.AdditionalLightZPos.DisplayDigits = 1;
            this.AdditionalLightZPos.DoValueRollover = false;
            this.AdditionalLightZPos.Enabled = false;
            this.AdditionalLightZPos.IncrementMouseWheel = 1;
            this.AdditionalLightZPos.Location = new System.Drawing.Point(74, 84);
            this.AdditionalLightZPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.AdditionalLightZPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.AdditionalLightZPos.Name = "AdditionalLightZPos";
            this.AdditionalLightZPos.ShiftMultiplier = 20;
            this.AdditionalLightZPos.Size = new System.Drawing.Size(100, 20);
            this.AdditionalLightZPos.TabIndex = 12;
            this.AdditionalLightZPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightZPos.ValueChanged += new System.EventHandler(this.AdditionalLightZPos_ValueChanged);
            // 
            // AdditionalLightPointLabel3
            // 
            this.AdditionalLightPointLabel3.AutoSize = true;
            this.AdditionalLightPointLabel3.Enabled = false;
            this.AdditionalLightPointLabel3.Location = new System.Drawing.Point(3, 86);
            this.AdditionalLightPointLabel3.Name = "AdditionalLightPointLabel3";
            this.AdditionalLightPointLabel3.Size = new System.Drawing.Size(57, 13);
            this.AdditionalLightPointLabel3.TabIndex = 11;
            this.AdditionalLightPointLabel3.Text = "Z Position:";
            // 
            // AdditionalLightPointLabel2
            // 
            this.AdditionalLightPointLabel2.AutoSize = true;
            this.AdditionalLightPointLabel2.Enabled = false;
            this.AdditionalLightPointLabel2.Location = new System.Drawing.Point(3, 60);
            this.AdditionalLightPointLabel2.Name = "AdditionalLightPointLabel2";
            this.AdditionalLightPointLabel2.Size = new System.Drawing.Size(57, 13);
            this.AdditionalLightPointLabel2.TabIndex = 9;
            this.AdditionalLightPointLabel2.Text = "Y Position:";
            // 
            // AdditionalLightNS
            // 
            this.AdditionalLightNS.AlwaysFireValueChanged = false;
            this.AdditionalLightNS.DisplayDigits = 1;
            this.AdditionalLightNS.DoValueRollover = false;
            this.AdditionalLightNS.Enabled = false;
            this.AdditionalLightNS.IncrementMouseWheel = 1;
            this.AdditionalLightNS.Location = new System.Drawing.Point(261, 30);
            this.AdditionalLightNS.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.AdditionalLightNS.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightNS.Name = "AdditionalLightNS";
            this.AdditionalLightNS.ShiftMultiplier = 1;
            this.AdditionalLightNS.Size = new System.Drawing.Size(100, 20);
            this.AdditionalLightNS.TabIndex = 13;
            this.AdditionalLightNS.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightNS.ValueChanged += new System.EventHandler(this.AdditionalLightNS_ValueChanged);
            // 
            // AdditionalLightDirectionLabel2
            // 
            this.AdditionalLightDirectionLabel2.AutoSize = true;
            this.AdditionalLightDirectionLabel2.Enabled = false;
            this.AdditionalLightDirectionLabel2.Location = new System.Drawing.Point(190, 58);
            this.AdditionalLightDirectionLabel2.Name = "AdditionalLightDirectionLabel2";
            this.AdditionalLightDirectionLabel2.Size = new System.Drawing.Size(59, 13);
            this.AdditionalLightDirectionLabel2.TabIndex = 17;
            this.AdditionalLightDirectionLabel2.Text = "East-West:";
            // 
            // AdditionalLightYPos
            // 
            this.AdditionalLightYPos.AlwaysFireValueChanged = false;
            this.AdditionalLightYPos.DisplayDigits = 1;
            this.AdditionalLightYPos.DoValueRollover = false;
            this.AdditionalLightYPos.Enabled = false;
            this.AdditionalLightYPos.IncrementMouseWheel = 1;
            this.AdditionalLightYPos.Location = new System.Drawing.Point(74, 58);
            this.AdditionalLightYPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.AdditionalLightYPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.AdditionalLightYPos.Name = "AdditionalLightYPos";
            this.AdditionalLightYPos.ShiftMultiplier = 20;
            this.AdditionalLightYPos.Size = new System.Drawing.Size(100, 20);
            this.AdditionalLightYPos.TabIndex = 11;
            this.AdditionalLightYPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightYPos.ValueChanged += new System.EventHandler(this.AdditionalLightYPos_ValueChanged);
            // 
            // AdditionalLightEW
            // 
            this.AdditionalLightEW.AlwaysFireValueChanged = false;
            this.AdditionalLightEW.DisplayDigits = 1;
            this.AdditionalLightEW.DoValueRollover = false;
            this.AdditionalLightEW.Enabled = false;
            this.AdditionalLightEW.IncrementMouseWheel = 1;
            this.AdditionalLightEW.Location = new System.Drawing.Point(261, 56);
            this.AdditionalLightEW.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.AdditionalLightEW.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightEW.Name = "AdditionalLightEW";
            this.AdditionalLightEW.ShiftMultiplier = 1;
            this.AdditionalLightEW.Size = new System.Drawing.Size(100, 20);
            this.AdditionalLightEW.TabIndex = 14;
            this.AdditionalLightEW.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightEW.ValueChanged += new System.EventHandler(this.AdditionalLightEW_ValueChanged);
            // 
            // AdditionalLightDirectionLabel1
            // 
            this.AdditionalLightDirectionLabel1.AutoSize = true;
            this.AdditionalLightDirectionLabel1.Enabled = false;
            this.AdditionalLightDirectionLabel1.Location = new System.Drawing.Point(190, 32);
            this.AdditionalLightDirectionLabel1.Name = "AdditionalLightDirectionLabel1";
            this.AdditionalLightDirectionLabel1.Size = new System.Drawing.Size(67, 13);
            this.AdditionalLightDirectionLabel1.TabIndex = 13;
            this.AdditionalLightDirectionLabel1.Text = "North-South:";
            // 
            // AdditionalLightXPos
            // 
            this.AdditionalLightXPos.AlwaysFireValueChanged = false;
            this.AdditionalLightXPos.DisplayDigits = 1;
            this.AdditionalLightXPos.DoValueRollover = false;
            this.AdditionalLightXPos.Enabled = false;
            this.AdditionalLightXPos.IncrementMouseWheel = 1;
            this.AdditionalLightXPos.Location = new System.Drawing.Point(74, 32);
            this.AdditionalLightXPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.AdditionalLightXPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.AdditionalLightXPos.Name = "AdditionalLightXPos";
            this.AdditionalLightXPos.ShiftMultiplier = 20;
            this.AdditionalLightXPos.Size = new System.Drawing.Size(100, 20);
            this.AdditionalLightXPos.TabIndex = 10;
            this.AdditionalLightXPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightXPos.ValueChanged += new System.EventHandler(this.AdditionalLightXPos_ValueChanged);
            // 
            // AdditionalLightDelete
            // 
            this.AdditionalLightDelete.Location = new System.Drawing.Point(250, 19);
            this.AdditionalLightDelete.Name = "AdditionalLightDelete";
            this.AdditionalLightDelete.Size = new System.Drawing.Size(120, 23);
            this.AdditionalLightDelete.TabIndex = 9;
            this.AdditionalLightDelete.Text = "Delete Light";
            this.AdditionalLightDelete.UseVisualStyleBackColor = true;
            this.AdditionalLightDelete.Click += new System.EventHandler(this.AdditionalLightDelete_Click);
            // 
            // AdditionalLightAdd
            // 
            this.AdditionalLightAdd.Location = new System.Drawing.Point(124, 19);
            this.AdditionalLightAdd.Name = "AdditionalLightAdd";
            this.AdditionalLightAdd.Size = new System.Drawing.Size(120, 23);
            this.AdditionalLightAdd.TabIndex = 8;
            this.AdditionalLightAdd.Text = "Add Light";
            this.EnvironmentControlTooltip.SetToolTip(this.AdditionalLightAdd, "Hold SHIFT to add in front of camera");
            this.AdditionalLightAdd.UseVisualStyleBackColor = true;
            this.AdditionalLightAdd.Click += new System.EventHandler(this.AdditionalLightAdd_Click);
            // 
            // AdditionalLightSelect
            // 
            this.AdditionalLightSelect.Location = new System.Drawing.Point(9, 22);
            this.AdditionalLightSelect.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AdditionalLightSelect.Name = "AdditionalLightSelect";
            this.AdditionalLightSelect.Size = new System.Drawing.Size(65, 20);
            this.AdditionalLightSelect.TabIndex = 7;
            this.AdditionalLightSelect.ValueChanged += new System.EventHandler(this.AdditionalLightSelect_ValueChanged);
            // 
            // niceLine5
            // 
            this.niceLine5.Location = new System.Drawing.Point(6, 42);
            this.niceLine5.Name = "niceLine5";
            this.niceLine5.Size = new System.Drawing.Size(384, 15);
            this.niceLine5.TabIndex = 20;
            this.niceLine5.TabStop = false;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.WarpsongsCheckBox);
            this.groupBox11.Controls.Add(this.InvisibleActorsCheckBox);
            this.groupBox11.Controls.Add(this.label52);
            this.groupBox11.Controls.Add(this.IdleAnimComboBox);
            this.groupBox11.Controls.Add(this.label50);
            this.groupBox11.Controls.Add(this.RestrictionComboBox);
            this.groupBox11.Location = new System.Drawing.Point(10, 202);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(395, 89);
            this.groupBox11.TabIndex = 41;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Behaviour";
            // 
            // WarpsongsCheckBox
            // 
            this.WarpsongsCheckBox.AutoSize = true;
            this.WarpsongsCheckBox.Location = new System.Drawing.Point(265, 54);
            this.WarpsongsCheckBox.Name = "WarpsongsCheckBox";
            this.WarpsongsCheckBox.Size = new System.Drawing.Size(118, 17);
            this.WarpsongsCheckBox.TabIndex = 49;
            this.WarpsongsCheckBox.Text = "Disable warp songs";
            this.WarpsongsCheckBox.UseVisualStyleBackColor = true;
            this.WarpsongsCheckBox.CheckedChanged += new System.EventHandler(this.WarpsongsCheckBox_CheckedChanged);
            // 
            // InvisibleActorsCheckBox
            // 
            this.InvisibleActorsCheckBox.AutoSize = true;
            this.InvisibleActorsCheckBox.Location = new System.Drawing.Point(265, 24);
            this.InvisibleActorsCheckBox.Name = "InvisibleActorsCheckBox";
            this.InvisibleActorsCheckBox.Size = new System.Drawing.Size(131, 17);
            this.InvisibleActorsCheckBox.TabIndex = 43;
            this.InvisibleActorsCheckBox.Text = "Enable invisible actors";
            this.InvisibleActorsCheckBox.UseVisualStyleBackColor = true;
            this.InvisibleActorsCheckBox.CheckedChanged += new System.EventHandler(this.InvisibleActorsCheckBox_CheckedChanged);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(5, 55);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(90, 13);
            this.label52.TabIndex = 47;
            this.label52.Text = "Temp && idle anim:";
            // 
            // IdleAnimComboBox
            // 
            this.IdleAnimComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IdleAnimComboBox.FormattingEnabled = true;
            this.IdleAnimComboBox.Location = new System.Drawing.Point(95, 52);
            this.IdleAnimComboBox.Name = "IdleAnimComboBox";
            this.IdleAnimComboBox.Size = new System.Drawing.Size(141, 21);
            this.IdleAnimComboBox.TabIndex = 48;
            this.IdleAnimComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.IdleAnimComboBox.SelectedValueChanged += new System.EventHandler(this.IdleAnimComboBox_SelectedValueChanged);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(5, 25);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(63, 13);
            this.label50.TabIndex = 43;
            this.label50.Text = "Restriction: ";
            // 
            // RestrictionComboBox
            // 
            this.RestrictionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RestrictionComboBox.FormattingEnabled = true;
            this.RestrictionComboBox.Location = new System.Drawing.Point(95, 22);
            this.RestrictionComboBox.Name = "RestrictionComboBox";
            this.RestrictionComboBox.Size = new System.Drawing.Size(141, 21);
            this.RestrictionComboBox.TabIndex = 44;
            this.RestrictionComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.RestrictionComboBox.SelectedValueChanged += new System.EventHandler(this.RestrictionComboBox_SelectedValueChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.WindStrength);
            this.groupBox9.Controls.Add(this.WindSouth);
            this.groupBox9.Controls.Add(this.WindVertical);
            this.groupBox9.Controls.Add(this.WindWest);
            this.groupBox9.Controls.Add(this.label48);
            this.groupBox9.Controls.Add(this.label49);
            this.groupBox9.Controls.Add(this.label47);
            this.groupBox9.Controls.Add(this.label46);
            this.groupBox9.Location = new System.Drawing.Point(9, 115);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(396, 81);
            this.groupBox9.TabIndex = 40;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Wind";
            // 
            // WindStrength
            // 
            this.WindStrength.AlwaysFireValueChanged = false;
            this.WindStrength.DisplayDigits = 2;
            this.WindStrength.DoValueRollover = false;
            this.WindStrength.Hexadecimal = true;
            this.WindStrength.IncrementMouseWheel = 1;
            this.WindStrength.Location = new System.Drawing.Point(257, 47);
            this.WindStrength.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.WindStrength.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WindStrength.Name = "WindStrength";
            this.WindStrength.ShiftMultiplier = 4;
            this.WindStrength.Size = new System.Drawing.Size(50, 20);
            this.WindStrength.TabIndex = 53;
            this.WindStrength.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WindStrength.ValueChanged += new System.EventHandler(this.WindStrength_Leave);
            // 
            // WindSouth
            // 
            this.WindSouth.AlwaysFireValueChanged = false;
            this.WindSouth.DisplayDigits = 2;
            this.WindSouth.DoValueRollover = false;
            this.WindSouth.Hexadecimal = true;
            this.WindSouth.IncrementMouseWheel = 1;
            this.WindSouth.Location = new System.Drawing.Point(257, 23);
            this.WindSouth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.WindSouth.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WindSouth.Name = "WindSouth";
            this.WindSouth.ShiftMultiplier = 4;
            this.WindSouth.Size = new System.Drawing.Size(50, 20);
            this.WindSouth.TabIndex = 52;
            this.WindSouth.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WindSouth.ValueChanged += new System.EventHandler(this.WindSouth_Leave);
            // 
            // WindVertical
            // 
            this.WindVertical.AlwaysFireValueChanged = false;
            this.WindVertical.DisplayDigits = 2;
            this.WindVertical.DoValueRollover = false;
            this.WindVertical.Hexadecimal = true;
            this.WindVertical.IncrementMouseWheel = 1;
            this.WindVertical.Location = new System.Drawing.Point(83, 47);
            this.WindVertical.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.WindVertical.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WindVertical.Name = "WindVertical";
            this.WindVertical.ShiftMultiplier = 4;
            this.WindVertical.Size = new System.Drawing.Size(50, 20);
            this.WindVertical.TabIndex = 51;
            this.WindVertical.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WindVertical.ValueChanged += new System.EventHandler(this.WindVertical_Leave);
            // 
            // WindWest
            // 
            this.WindWest.AlwaysFireValueChanged = false;
            this.WindWest.DisplayDigits = 2;
            this.WindWest.DoValueRollover = false;
            this.WindWest.Hexadecimal = true;
            this.WindWest.IncrementMouseWheel = 1;
            this.WindWest.Location = new System.Drawing.Point(83, 23);
            this.WindWest.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.WindWest.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WindWest.Name = "WindWest";
            this.WindWest.ShiftMultiplier = 4;
            this.WindWest.Size = new System.Drawing.Size(50, 20);
            this.WindWest.TabIndex = 50;
            this.WindWest.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WindWest.ValueChanged += new System.EventHandler(this.WindWest_Leave);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(195, 50);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(50, 13);
            this.label48.TabIndex = 49;
            this.label48.Text = "Strength:";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(195, 25);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(38, 13);
            this.label49.TabIndex = 47;
            this.label49.Text = "South:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(12, 50);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(45, 13);
            this.label47.TabIndex = 45;
            this.label47.Text = "Vertical:";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(12, 25);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(35, 13);
            this.label46.TabIndex = 43;
            this.label46.Text = "West:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.SoundEcho);
            this.groupBox8.Controls.Add(this.TimeSpeed);
            this.groupBox8.Controls.Add(this.DisableStartTime);
            this.groupBox8.Controls.Add(this.TimeMinute);
            this.groupBox8.Controls.Add(this.label105);
            this.groupBox8.Controls.Add(this.TimeHour);
            this.groupBox8.Controls.Add(this.label43);
            this.groupBox8.Controls.Add(this.SunmoonCheckBox);
            this.groupBox8.Controls.Add(this.SkyboxCheckBox);
            this.groupBox8.Controls.Add(this.label41);
            this.groupBox8.Controls.Add(this.label40);
            this.groupBox8.Location = new System.Drawing.Point(10, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(396, 103);
            this.groupBox8.TabIndex = 39;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Skybox && Time Settings";
            // 
            // SoundEcho
            // 
            this.SoundEcho.AlwaysFireValueChanged = false;
            this.SoundEcho.DisplayDigits = 2;
            this.SoundEcho.DoValueRollover = false;
            this.SoundEcho.Hexadecimal = true;
            this.SoundEcho.IncrementMouseWheel = 1;
            this.SoundEcho.Location = new System.Drawing.Point(85, 71);
            this.SoundEcho.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.SoundEcho.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SoundEcho.Name = "SoundEcho";
            this.SoundEcho.ShiftMultiplier = 4;
            this.SoundEcho.Size = new System.Drawing.Size(50, 20);
            this.SoundEcho.TabIndex = 49;
            this.SoundEcho.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SoundEcho.ValueChanged += new System.EventHandler(this.SoundEcho_Leave);
            // 
            // TimeSpeed
            // 
            this.TimeSpeed.AlwaysFireValueChanged = false;
            this.TimeSpeed.DisplayDigits = 2;
            this.TimeSpeed.DoValueRollover = false;
            this.TimeSpeed.Hexadecimal = true;
            this.TimeSpeed.IncrementMouseWheel = 1;
            this.TimeSpeed.Location = new System.Drawing.Point(85, 47);
            this.TimeSpeed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.TimeSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TimeSpeed.Name = "TimeSpeed";
            this.TimeSpeed.ShiftMultiplier = 4;
            this.TimeSpeed.Size = new System.Drawing.Size(50, 20);
            this.TimeSpeed.TabIndex = 48;
            this.TimeSpeed.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TimeSpeed.ValueChanged += new System.EventHandler(this.TimeSpeed_Leave);
            // 
            // DisableStartTime
            // 
            this.DisableStartTime.AutoSize = true;
            this.DisableStartTime.Location = new System.Drawing.Point(270, 28);
            this.DisableStartTime.Name = "DisableStartTime";
            this.DisableStartTime.Size = new System.Drawing.Size(98, 17);
            this.DisableStartTime.TabIndex = 47;
            this.DisableStartTime.Text = "Use global time";
            this.DisableStartTime.UseVisualStyleBackColor = true;
            this.DisableStartTime.CheckedChanged += new System.EventHandler(this.DisableStartTime_CheckedChanged);
            // 
            // TimeMinute
            // 
            this.TimeMinute.AlwaysFireValueChanged = false;
            this.TimeMinute.DisplayDigits = 1;
            this.TimeMinute.DoValueRollover = false;
            this.TimeMinute.Enabled = false;
            this.TimeMinute.IncrementMouseWheel = 1;
            this.TimeMinute.Location = new System.Drawing.Point(194, 22);
            this.TimeMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.TimeMinute.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TimeMinute.Name = "TimeMinute";
            this.TimeMinute.ShiftMultiplier = 10;
            this.TimeMinute.Size = new System.Drawing.Size(50, 20);
            this.TimeMinute.TabIndex = 46;
            this.TimeMinute.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TimeMinute.ValueChanged += new System.EventHandler(this.TimeMinute_ValueChanged);
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(144, 25);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(42, 13);
            this.label105.TabIndex = 45;
            this.label105.Text = "Minute:";
            // 
            // TimeHour
            // 
            this.TimeHour.AlwaysFireValueChanged = false;
            this.TimeHour.DisplayDigits = 1;
            this.TimeHour.DoValueRollover = false;
            this.TimeHour.Enabled = false;
            this.TimeHour.IncrementMouseWheel = 1;
            this.TimeHour.Location = new System.Drawing.Point(85, 22);
            this.TimeHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.TimeHour.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TimeHour.Name = "TimeHour";
            this.TimeHour.ShiftMultiplier = 4;
            this.TimeHour.Size = new System.Drawing.Size(50, 20);
            this.TimeHour.TabIndex = 44;
            this.TimeHour.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TimeHour.ValueChanged += new System.EventHandler(this.TimeHour_ValueChanged);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(14, 76);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(35, 13);
            this.label43.TabIndex = 38;
            this.label43.Text = "Echo:";
            // 
            // SunmoonCheckBox
            // 
            this.SunmoonCheckBox.AutoSize = true;
            this.SunmoonCheckBox.Location = new System.Drawing.Point(270, 74);
            this.SunmoonCheckBox.Name = "SunmoonCheckBox";
            this.SunmoonCheckBox.Size = new System.Drawing.Size(115, 17);
            this.SunmoonCheckBox.TabIndex = 42;
            this.SunmoonCheckBox.Text = "Disable Sun/Moon";
            this.SunmoonCheckBox.UseVisualStyleBackColor = true;
            this.SunmoonCheckBox.CheckedChanged += new System.EventHandler(this.SunmoonCheckBox_CheckedChanged);
            // 
            // SkyboxCheckBox
            // 
            this.SkyboxCheckBox.AutoSize = true;
            this.SkyboxCheckBox.Location = new System.Drawing.Point(270, 51);
            this.SkyboxCheckBox.Name = "SkyboxCheckBox";
            this.SkyboxCheckBox.Size = new System.Drawing.Size(99, 17);
            this.SkyboxCheckBox.TabIndex = 38;
            this.SkyboxCheckBox.Text = "Disable Skybox";
            this.SkyboxCheckBox.UseVisualStyleBackColor = true;
            this.SkyboxCheckBox.CheckedChanged += new System.EventHandler(this.SkyboxCheckBox_CheckedChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(14, 51);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(65, 13);
            this.label41.TabIndex = 40;
            this.label41.Text = "Time speed:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(14, 25);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(56, 13);
            this.label40.TabIndex = 39;
            this.label40.Text = "Start hour:";
            // 
            // tabCollision
            // 
            this.tabCollision.Controls.Add(this.groupBox6);
            this.tabCollision.Controls.Add(this.groupBox3);
            this.tabCollision.Location = new System.Drawing.Point(4, 40);
            this.tabCollision.Name = "tabCollision";
            this.tabCollision.Size = new System.Drawing.Size(411, 676);
            this.tabCollision.TabIndex = 4;
            this.tabCollision.Text = "Collision & Exits";
            this.tabCollision.UseVisualStyleBackColor = true;
            this.tabCollision.Click += new System.EventHandler(this.tabCollision_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.AddexitButton);
            this.groupBox6.Controls.Add(this.DeleteexitButton);
            this.groupBox6.Controls.Add(this.ExitGroupBox);
            this.groupBox6.Controls.Add(this.ExitListLabel);
            this.groupBox6.Controls.Add(this.ExitList);
            this.groupBox6.Enabled = false;
            this.groupBox6.Location = new System.Drawing.Point(6, 526);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(400, 136);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Exit List";
            // 
            // AddexitButton
            // 
            this.AddexitButton.Location = new System.Drawing.Point(138, 19);
            this.AddexitButton.Name = "AddexitButton";
            this.AddexitButton.Size = new System.Drawing.Size(120, 23);
            this.AddexitButton.TabIndex = 1;
            this.AddexitButton.Text = "Add Exit";
            this.AddexitButton.UseVisualStyleBackColor = true;
            this.AddexitButton.Click += new System.EventHandler(this.AddExit_Click);
            // 
            // DeleteexitButton
            // 
            this.DeleteexitButton.Location = new System.Drawing.Point(264, 19);
            this.DeleteexitButton.Name = "DeleteexitButton";
            this.DeleteexitButton.Size = new System.Drawing.Size(120, 23);
            this.DeleteexitButton.TabIndex = 2;
            this.DeleteexitButton.Text = "Delete Exit";
            this.DeleteexitButton.UseVisualStyleBackColor = true;
            this.DeleteexitButton.Click += new System.EventHandler(this.DeleteExit_Click);
            // 
            // ExitGroupBox
            // 
            this.ExitGroupBox.Controls.Add(this.ExitHeaderIndex);
            this.ExitGroupBox.Controls.Add(this.ExitSceneIndex);
            this.ExitGroupBox.Controls.Add(this.label134);
            this.ExitGroupBox.Controls.Add(this.ExitMusicOn);
            this.ExitGroupBox.Controls.Add(this.ExitSpawnIndex);
            this.ExitGroupBox.Controls.Add(this.ExitShowTitlecard);
            this.ExitGroupBox.Controls.Add(this.label133);
            this.ExitGroupBox.Controls.Add(this.label132);
            this.ExitGroupBox.Controls.Add(this.ExitFadeOut);
            this.ExitGroupBox.Controls.Add(this.label1331);
            this.ExitGroupBox.Controls.Add(this.label131);
            this.ExitGroupBox.Controls.Add(this.ExitFadeIn);
            this.ExitGroupBox.Location = new System.Drawing.Point(127, 38);
            this.ExitGroupBox.Name = "ExitGroupBox";
            this.ExitGroupBox.Size = new System.Drawing.Size(267, 109);
            this.ExitGroupBox.TabIndex = 117;
            this.ExitGroupBox.TabStop = false;
            // 
            // ExitHeaderIndex
            // 
            this.ExitHeaderIndex.AlwaysFireValueChanged = false;
            this.ExitHeaderIndex.DisplayDigits = 1;
            this.ExitHeaderIndex.DoValueRollover = true;
            this.ExitHeaderIndex.Hexadecimal = true;
            this.ExitHeaderIndex.IncrementMouseWheel = 1;
            this.ExitHeaderIndex.Location = new System.Drawing.Point(221, 9);
            this.ExitHeaderIndex.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.ExitHeaderIndex.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.ExitHeaderIndex.Name = "ExitHeaderIndex";
            this.ExitHeaderIndex.ShiftMultiplier = 1;
            this.ExitHeaderIndex.Size = new System.Drawing.Size(40, 20);
            this.ExitHeaderIndex.TabIndex = 112;
            this.ExitHeaderIndex.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ExitHeaderIndex.ValueChanged += new System.EventHandler(this.ExitHeaderIndex_ValueChanged);
            // 
            // ExitSceneIndex
            // 
            this.ExitSceneIndex.AlwaysFireValueChanged = false;
            this.ExitSceneIndex.DisplayDigits = 1;
            this.ExitSceneIndex.DoValueRollover = true;
            this.ExitSceneIndex.Hexadecimal = true;
            this.ExitSceneIndex.IncrementMouseWheel = 1;
            this.ExitSceneIndex.Location = new System.Drawing.Point(90, 9);
            this.ExitSceneIndex.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ExitSceneIndex.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.ExitSceneIndex.Name = "ExitSceneIndex";
            this.ExitSceneIndex.ShiftMultiplier = 1;
            this.ExitSceneIndex.Size = new System.Drawing.Size(40, 20);
            this.ExitSceneIndex.TabIndex = 116;
            this.ExitSceneIndex.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ExitSceneIndex.ValueChanged += new System.EventHandler(this.ExitSceneIndex_ValueChanged);
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(4, 11);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(70, 13);
            this.label134.TabIndex = 115;
            this.label134.Text = "Scene Index:";
            // 
            // ExitMusicOn
            // 
            this.ExitMusicOn.AutoSize = true;
            this.ExitMusicOn.Location = new System.Drawing.Point(146, 35);
            this.ExitMusicOn.Name = "ExitMusicOn";
            this.ExitMusicOn.Size = new System.Drawing.Size(119, 17);
            this.ExitMusicOn.TabIndex = 107;
            this.ExitMusicOn.Text = "Keep Playing Music";
            this.ExitMusicOn.UseVisualStyleBackColor = true;
            this.ExitMusicOn.CheckedChanged += new System.EventHandler(this.ExitMusicOn_CheckedChanged);
            // 
            // ExitSpawnIndex
            // 
            this.ExitSpawnIndex.AlwaysFireValueChanged = false;
            this.ExitSpawnIndex.DisplayDigits = 1;
            this.ExitSpawnIndex.DoValueRollover = true;
            this.ExitSpawnIndex.Hexadecimal = true;
            this.ExitSpawnIndex.IncrementMouseWheel = 1;
            this.ExitSpawnIndex.Location = new System.Drawing.Point(90, 32);
            this.ExitSpawnIndex.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.ExitSpawnIndex.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.ExitSpawnIndex.Name = "ExitSpawnIndex";
            this.ExitSpawnIndex.ShiftMultiplier = 1;
            this.ExitSpawnIndex.Size = new System.Drawing.Size(40, 20);
            this.ExitSpawnIndex.TabIndex = 114;
            this.ExitSpawnIndex.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ExitSpawnIndex.ValueChanged += new System.EventHandler(this.ExitSpawnIndex_ValueChanged);
            // 
            // ExitShowTitlecard
            // 
            this.ExitShowTitlecard.AutoSize = true;
            this.ExitShowTitlecard.Location = new System.Drawing.Point(146, 58);
            this.ExitShowTitlecard.Name = "ExitShowTitlecard";
            this.ExitShowTitlecard.Size = new System.Drawing.Size(97, 17);
            this.ExitShowTitlecard.TabIndex = 108;
            this.ExitShowTitlecard.Text = "Show Titlecard";
            this.ExitShowTitlecard.UseVisualStyleBackColor = true;
            this.ExitShowTitlecard.CheckedChanged += new System.EventHandler(this.ExitShowTitlecard_CheckedChanged);
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(4, 34);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(72, 13);
            this.label133.TabIndex = 113;
            this.label133.Text = "Spawn Index:";
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(135, 11);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(74, 13);
            this.label132.TabIndex = 111;
            this.label132.Text = "Header Index:";
            // 
            // ExitFadeOut
            // 
            this.ExitFadeOut.AlwaysFireValueChanged = false;
            this.ExitFadeOut.DisplayDigits = 1;
            this.ExitFadeOut.DoValueRollover = true;
            this.ExitFadeOut.Hexadecimal = true;
            this.ExitFadeOut.IncrementMouseWheel = 1;
            this.ExitFadeOut.Location = new System.Drawing.Point(90, 78);
            this.ExitFadeOut.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.ExitFadeOut.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.ExitFadeOut.Name = "ExitFadeOut";
            this.ExitFadeOut.ShiftMultiplier = 1;
            this.ExitFadeOut.Size = new System.Drawing.Size(40, 20);
            this.ExitFadeOut.TabIndex = 110;
            this.EnvironmentControlTooltip.SetToolTip(this.ExitFadeOut, resources.GetString("ExitFadeOut.ToolTip"));
            this.ExitFadeOut.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ExitFadeOut.ValueChanged += new System.EventHandler(this.ExitFadeOut_ValueChanged);
            // 
            // label1331
            // 
            this.label1331.AutoSize = true;
            this.label1331.Location = new System.Drawing.Point(4, 57);
            this.label1331.Name = "label1331";
            this.label1331.Size = new System.Drawing.Size(46, 13);
            this.label1331.TabIndex = 106;
            this.label1331.Text = "Fade In:";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(4, 80);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(54, 13);
            this.label131.TabIndex = 109;
            this.label131.Text = "Fade Out:";
            // 
            // ExitFadeIn
            // 
            this.ExitFadeIn.AlwaysFireValueChanged = false;
            this.ExitFadeIn.DisplayDigits = 1;
            this.ExitFadeIn.DoValueRollover = true;
            this.ExitFadeIn.Hexadecimal = true;
            this.ExitFadeIn.IncrementMouseWheel = 1;
            this.ExitFadeIn.Location = new System.Drawing.Point(90, 55);
            this.ExitFadeIn.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.ExitFadeIn.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.ExitFadeIn.Name = "ExitFadeIn";
            this.ExitFadeIn.ShiftMultiplier = 1;
            this.ExitFadeIn.Size = new System.Drawing.Size(40, 20);
            this.ExitFadeIn.TabIndex = 107;
            this.EnvironmentControlTooltip.SetToolTip(this.ExitFadeIn, resources.GetString("ExitFadeIn.ToolTip"));
            this.ExitFadeIn.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ExitFadeIn.ValueChanged += new System.EventHandler(this.ExitFadeIn_ValueChanged);
            // 
            // ExitListLabel
            // 
            this.ExitListLabel.AutoSize = true;
            this.ExitListLabel.Location = new System.Drawing.Point(138, 50);
            this.ExitListLabel.Name = "ExitListLabel";
            this.ExitListLabel.Size = new System.Drawing.Size(0, 13);
            this.ExitListLabel.TabIndex = 4;
            // 
            // ExitList
            // 
            this.ExitList.FormattingEnabled = true;
            this.ExitList.Location = new System.Drawing.Point(3, 19);
            this.ExitList.Name = "ExitList";
            this.ExitList.Size = new System.Drawing.Size(120, 108);
            this.ExitList.TabIndex = 0;
            this.ExitList.Click += new System.EventHandler(this.ExitList_Click);
            this.ExitList.SelectedIndexChanged += new System.EventHandler(this.ExitList_SelectedIndexChanged);
            this.ExitList.DoubleClick += new System.EventHandler(this.listBox4_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.niceLine1);
            this.groupBox3.Controls.Add(this.DeletepolygonButton);
            this.groupBox3.Controls.Add(this.AddpolygonButton);
            this.groupBox3.Controls.Add(this.PolygonSelect);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 514);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Polygon Types";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.PolytypeExitLabel);
            this.panel2.Controls.Add(this.BlockEponaCheckBox);
            this.panel2.Controls.Add(this.Lower1UnitChecbox);
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.WallDamageCheck);
            this.panel2.Controls.Add(this.label92);
            this.panel2.Controls.Add(this.label91);
            this.panel2.Controls.Add(this.niceLine12);
            this.panel2.Controls.Add(this.GroupDetectionB8);
            this.panel2.Controls.Add(this.label90);
            this.panel2.Controls.Add(this.GroupDetectionB4);
            this.panel2.Controls.Add(this.PolytypeUnk2);
            this.panel2.Controls.Add(this.GroupDetectionB2);
            this.panel2.Controls.Add(this.label89);
            this.panel2.Controls.Add(this.PolytypeUnk1);
            this.panel2.Controls.Add(this.label88);
            this.panel2.Controls.Add(this.label87);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.niceLine7);
            this.panel2.Controls.Add(this.CameraAngleNumeric);
            this.panel2.Controls.Add(this.CameraAngleLabel);
            this.panel2.Controls.Add(this.HookshotableCheckbox);
            this.panel2.Controls.Add(this.ExitNumber);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.SteepterrainCheckbox);
            this.panel2.Controls.Add(this.TerrainType);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.GroundType);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.EnvironmentType);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.EchoRange);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.niceLine4);
            this.panel2.Controls.Add(this.PolygonRawdata);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.niceLine10);
            this.panel2.Controls.Add(this.niceLine11);
            this.panel2.Location = new System.Drawing.Point(3, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(391, 509);
            this.panel2.TabIndex = 22;
            // 
            // PolytypeExitLabel
            // 
            this.PolytypeExitLabel.AutoSize = true;
            this.PolytypeExitLabel.Location = new System.Drawing.Point(135, 58);
            this.PolytypeExitLabel.Name = "PolytypeExitLabel";
            this.PolytypeExitLabel.Size = new System.Drawing.Size(16, 13);
            this.PolytypeExitLabel.TabIndex = 5;
            this.PolytypeExitLabel.Text = "   ";
            // 
            // BlockEponaCheckBox
            // 
            this.BlockEponaCheckBox.AutoSize = true;
            this.BlockEponaCheckBox.Location = new System.Drawing.Point(197, 136);
            this.BlockEponaCheckBox.Name = "BlockEponaCheckBox";
            this.BlockEponaCheckBox.Size = new System.Drawing.Size(87, 17);
            this.BlockEponaCheckBox.TabIndex = 105;
            this.BlockEponaCheckBox.Text = "Block Epona";
            this.BlockEponaCheckBox.UseVisualStyleBackColor = true;
            this.BlockEponaCheckBox.CheckedChanged += new System.EventHandler(this.BlockEponaCheckBox_CheckedChanged);
            // 
            // Lower1UnitChecbox
            // 
            this.Lower1UnitChecbox.AutoSize = true;
            this.Lower1UnitChecbox.Location = new System.Drawing.Point(107, 136);
            this.Lower1UnitChecbox.Name = "Lower1UnitChecbox";
            this.Lower1UnitChecbox.Size = new System.Drawing.Size(57, 17);
            this.Lower1UnitChecbox.TabIndex = 104;
            this.Lower1UnitChecbox.Text = "Priority";
            this.Lower1UnitChecbox.UseVisualStyleBackColor = true;
            this.Lower1UnitChecbox.CheckedChanged += new System.EventHandler(this.Lower1UnitChecbox_CheckedChanged);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.AutograbClimbRadioButton);
            this.panel8.Controls.Add(this.DiveRadioButton);
            this.panel8.Controls.Add(this.SmallVoidRadioButton);
            this.panel8.Controls.Add(this.NoLedgeJumpRadio);
            this.panel8.Controls.Add(this.NoMiscRadioButton);
            this.panel8.Controls.Add(this.VoidCheckBox);
            this.panel8.Location = new System.Drawing.Point(6, 158);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(382, 44);
            this.panel8.TabIndex = 91;
            // 
            // AutograbClimbRadioButton
            // 
            this.AutograbClimbRadioButton.AutoSize = true;
            this.AutograbClimbRadioButton.Location = new System.Drawing.Point(53, 24);
            this.AutograbClimbRadioButton.Name = "AutograbClimbRadioButton";
            this.AutograbClimbRadioButton.Size = new System.Drawing.Size(115, 17);
            this.AutograbClimbRadioButton.TabIndex = 94;
            this.AutograbClimbRadioButton.Text = "Autograb climbable";
            this.AutograbClimbRadioButton.UseVisualStyleBackColor = true;
            this.AutograbClimbRadioButton.CheckedChanged += new System.EventHandler(this.AutograbClimbRadioButton_CheckedChanged);
            // 
            // DiveRadioButton
            // 
            this.DiveRadioButton.AutoSize = true;
            this.DiveRadioButton.Location = new System.Drawing.Point(0, 24);
            this.DiveRadioButton.Name = "DiveRadioButton";
            this.DiveRadioButton.Size = new System.Drawing.Size(47, 17);
            this.DiveRadioButton.TabIndex = 93;
            this.DiveRadioButton.Text = "Dive";
            this.DiveRadioButton.UseVisualStyleBackColor = true;
            this.DiveRadioButton.CheckedChanged += new System.EventHandler(this.DiveRadioButton_CheckedChanged);
            // 
            // SmallVoidRadioButton
            // 
            this.SmallVoidRadioButton.AutoSize = true;
            this.SmallVoidRadioButton.Location = new System.Drawing.Point(171, 3);
            this.SmallVoidRadioButton.Name = "SmallVoidRadioButton";
            this.SmallVoidRadioButton.Size = new System.Drawing.Size(78, 17);
            this.SmallVoidRadioButton.TabIndex = 92;
            this.SmallVoidRadioButton.Text = "Void (small)";
            this.SmallVoidRadioButton.UseVisualStyleBackColor = true;
            this.SmallVoidRadioButton.CheckedChanged += new System.EventHandler(this.SmallVoidRadioButton_CheckedChanged);
            // 
            // NoLedgeJumpRadio
            // 
            this.NoLedgeJumpRadio.AutoSize = true;
            this.NoLedgeJumpRadio.Location = new System.Drawing.Point(65, 3);
            this.NoLedgeJumpRadio.Name = "NoLedgeJumpRadio";
            this.NoLedgeJumpRadio.Size = new System.Drawing.Size(100, 17);
            this.NoLedgeJumpRadio.TabIndex = 86;
            this.NoLedgeJumpRadio.Text = "No Ledge Jump";
            this.NoLedgeJumpRadio.UseVisualStyleBackColor = true;
            this.NoLedgeJumpRadio.CheckedChanged += new System.EventHandler(this.NoLedgeJumpRadio_CheckedChanged);
            // 
            // NoMiscRadioButton
            // 
            this.NoMiscRadioButton.AutoSize = true;
            this.NoMiscRadioButton.Checked = true;
            this.NoMiscRadioButton.Location = new System.Drawing.Point(0, 3);
            this.NoMiscRadioButton.Name = "NoMiscRadioButton";
            this.NoMiscRadioButton.Size = new System.Drawing.Size(59, 17);
            this.NoMiscRadioButton.TabIndex = 81;
            this.NoMiscRadioButton.TabStop = true;
            this.NoMiscRadioButton.Text = "Default";
            this.NoMiscRadioButton.UseVisualStyleBackColor = true;
            this.NoMiscRadioButton.CheckedChanged += new System.EventHandler(this.NoMiscRadioButton_CheckedChanged);
            // 
            // VoidCheckBox
            // 
            this.VoidCheckBox.AutoSize = true;
            this.VoidCheckBox.Location = new System.Drawing.Point(255, 3);
            this.VoidCheckBox.Name = "VoidCheckBox";
            this.VoidCheckBox.Size = new System.Drawing.Size(78, 17);
            this.VoidCheckBox.TabIndex = 91;
            this.VoidCheckBox.Text = "Void (large)";
            this.VoidCheckBox.UseVisualStyleBackColor = true;
            this.VoidCheckBox.CheckedChanged += new System.EventHandler(this.VoidCheckBox_CheckedChanged);
            // 
            // WallDamageCheck
            // 
            this.WallDamageCheck.AutoSize = true;
            this.WallDamageCheck.Location = new System.Drawing.Point(293, 136);
            this.WallDamageCheck.Name = "WallDamageCheck";
            this.WallDamageCheck.Size = new System.Drawing.Size(90, 17);
            this.WallDamageCheck.TabIndex = 103;
            this.WallDamageCheck.Text = "Wall Damage";
            this.WallDamageCheck.UseVisualStyleBackColor = true;
            this.WallDamageCheck.CheckedChanged += new System.EventHandler(this.WallDamageCheck_CheckedChanged);
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(3, 202);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(42, 13);
            this.label92.TabIndex = 102;
            this.label92.Text = "Actions";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(4, 260);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(54, 13);
            this.label91.TabIndex = 101;
            this.label91.Text = "Properties";
            // 
            // niceLine12
            // 
            this.niceLine12.Location = new System.Drawing.Point(3, 261);
            this.niceLine12.Name = "niceLine12";
            this.niceLine12.Size = new System.Drawing.Size(385, 15);
            this.niceLine12.TabIndex = 100;
            this.niceLine12.TabStop = false;
            // 
            // GroupDetectionB8
            // 
            this.GroupDetectionB8.AutoSize = true;
            this.GroupDetectionB8.Location = new System.Drawing.Point(168, 431);
            this.GroupDetectionB8.Name = "GroupDetectionB8";
            this.GroupDetectionB8.Size = new System.Drawing.Size(76, 17);
            this.GroupDetectionB8.TabIndex = 97;
            this.GroupDetectionB8.Text = "Nothing(C)";
            this.GroupDetectionB8.UseVisualStyleBackColor = true;
            this.GroupDetectionB8.CheckedChanged += new System.EventHandler(this.GroupDetectionB8_CheckedChanged);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(6, 388);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(57, 13);
            this.label90.TabIndex = 98;
            this.label90.Text = "Movement";
            // 
            // GroupDetectionB4
            // 
            this.GroupDetectionB4.AutoSize = true;
            this.GroupDetectionB4.Location = new System.Drawing.Point(80, 431);
            this.GroupDetectionB4.Name = "GroupDetectionB4";
            this.GroupDetectionB4.Size = new System.Drawing.Size(79, 17);
            this.GroupDetectionB4.TabIndex = 96;
            this.GroupDetectionB4.Text = "Nothing (B)";
            this.GroupDetectionB4.UseVisualStyleBackColor = true;
            this.GroupDetectionB4.CheckedChanged += new System.EventHandler(this.GroupDetectionB4_CheckedChanged);
            // 
            // PolytypeUnk2
            // 
            this.PolytypeUnk2.AlwaysFireValueChanged = false;
            this.PolytypeUnk2.DisplayDigits = 1;
            this.PolytypeUnk2.DoValueRollover = true;
            this.PolytypeUnk2.Hexadecimal = true;
            this.PolytypeUnk2.IncrementMouseWheel = 3;
            this.PolytypeUnk2.Location = new System.Drawing.Point(169, 405);
            this.PolytypeUnk2.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.PolytypeUnk2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.PolytypeUnk2.Name = "PolytypeUnk2";
            this.PolytypeUnk2.ShiftMultiplier = 1;
            this.PolytypeUnk2.Size = new System.Drawing.Size(40, 20);
            this.PolytypeUnk2.TabIndex = 97;
            this.EnvironmentControlTooltip.SetToolTip(this.PolytypeUnk2, "asd");
            this.PolytypeUnk2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PolytypeUnk2.ValueChanged += new System.EventHandler(this.numericUpDownEx2_ValueChanged_1);
            // 
            // GroupDetectionB2
            // 
            this.GroupDetectionB2.AutoSize = true;
            this.GroupDetectionB2.Location = new System.Drawing.Point(12, 431);
            this.GroupDetectionB2.Name = "GroupDetectionB2";
            this.GroupDetectionB2.Size = new System.Drawing.Size(59, 17);
            this.GroupDetectionB2.TabIndex = 95;
            this.GroupDetectionB2.Text = "Enable";
            this.GroupDetectionB2.UseVisualStyleBackColor = true;
            this.GroupDetectionB2.CheckedChanged += new System.EventHandler(this.GroupDetectionB2_CheckedChanged);
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(122, 407);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(41, 13);
            this.label89.TabIndex = 96;
            this.label89.Text = "Speed:";
            // 
            // PolytypeUnk1
            // 
            this.PolytypeUnk1.AlwaysFireValueChanged = false;
            this.PolytypeUnk1.DisplayDigits = 1;
            this.PolytypeUnk1.DoValueRollover = true;
            this.PolytypeUnk1.Hexadecimal = true;
            this.PolytypeUnk1.IncrementMouseWheel = 3;
            this.PolytypeUnk1.Location = new System.Drawing.Point(64, 405);
            this.PolytypeUnk1.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.PolytypeUnk1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.PolytypeUnk1.Name = "PolytypeUnk1";
            this.PolytypeUnk1.ShiftMultiplier = 1;
            this.PolytypeUnk1.Size = new System.Drawing.Size(40, 20);
            this.PolytypeUnk1.TabIndex = 95;
            this.EnvironmentControlTooltip.SetToolTip(this.PolytypeUnk1, "asd");
            this.PolytypeUnk1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PolytypeUnk1.ValueChanged += new System.EventHandler(this.numericUpDownEx1_ValueChanged_1);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(6, 407);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(52, 13);
            this.label88.TabIndex = 94;
            this.label88.Text = "Direction:";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(4, 341);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(113, 13);
            this.label87.TabIndex = 92;
            this.label87.Text = "Disable detection for...";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.GroupDetectionA8);
            this.panel7.Controls.Add(this.GroupDetectionA4);
            this.panel7.Controls.Add(this.GroupDetectionA2);
            this.panel7.Location = new System.Drawing.Point(10, 358);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(382, 28);
            this.panel7.TabIndex = 86;
            // 
            // GroupDetectionA8
            // 
            this.GroupDetectionA8.AutoSize = true;
            this.GroupDetectionA8.Location = new System.Drawing.Point(228, 3);
            this.GroupDetectionA8.Name = "GroupDetectionA8";
            this.GroupDetectionA8.Size = new System.Drawing.Size(135, 17);
            this.GroupDetectionA8.TabIndex = 94;
            this.GroupDetectionA8.Text = "Projectiles + bombchus";
            this.GroupDetectionA8.UseVisualStyleBackColor = true;
            this.GroupDetectionA8.CheckedChanged += new System.EventHandler(this.GroupDetectionA8_CheckedChanged);
            // 
            // GroupDetectionA4
            // 
            this.GroupDetectionA4.AutoSize = true;
            this.GroupDetectionA4.Location = new System.Drawing.Point(71, 3);
            this.GroupDetectionA4.Name = "GroupDetectionA4";
            this.GroupDetectionA4.Size = new System.Drawing.Size(154, 17);
            this.GroupDetectionA4.TabIndex = 93;
            this.GroupDetectionA4.Text = "All actors except projectiles";
            this.GroupDetectionA4.UseVisualStyleBackColor = true;
            this.GroupDetectionA4.CheckedChanged += new System.EventHandler(this.GroupDetectionA4_CheckedChanged);
            // 
            // GroupDetectionA2
            // 
            this.GroupDetectionA2.AutoSize = true;
            this.GroupDetectionA2.Location = new System.Drawing.Point(3, 3);
            this.GroupDetectionA2.Name = "GroupDetectionA2";
            this.GroupDetectionA2.Size = new System.Drawing.Size(62, 17);
            this.GroupDetectionA2.TabIndex = 92;
            this.GroupDetectionA2.Text = "Camera";
            this.GroupDetectionA2.UseVisualStyleBackColor = true;
            this.GroupDetectionA2.CheckedChanged += new System.EventHandler(this.GroupDetectionA2_CheckedChanged);
            // 
            // niceLine7
            // 
            this.niceLine7.Location = new System.Drawing.Point(6, 202);
            this.niceLine7.Name = "niceLine7";
            this.niceLine7.Size = new System.Drawing.Size(385, 15);
            this.niceLine7.TabIndex = 90;
            this.niceLine7.TabStop = false;
            // 
            // CameraAngleNumeric
            // 
            this.CameraAngleNumeric.AlwaysFireValueChanged = false;
            this.CameraAngleNumeric.DisplayDigits = 1;
            this.CameraAngleNumeric.DoValueRollover = true;
            this.CameraAngleNumeric.Hexadecimal = true;
            this.CameraAngleNumeric.IncrementMouseWheel = 1;
            this.CameraAngleNumeric.Location = new System.Drawing.Point(324, 56);
            this.CameraAngleNumeric.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.CameraAngleNumeric.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.CameraAngleNumeric.Name = "CameraAngleNumeric";
            this.CameraAngleNumeric.ShiftMultiplier = 1;
            this.CameraAngleNumeric.Size = new System.Drawing.Size(40, 20);
            this.CameraAngleNumeric.TabIndex = 89;
            this.EnvironmentControlTooltip.SetToolTip(this.CameraAngleNumeric, "Changes the camera ID when Link steps on it");
            this.CameraAngleNumeric.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraAngleNumeric.ValueChanged += new System.EventHandler(this.CameraAngleNumeric_ValueChanged);
            // 
            // CameraAngleLabel
            // 
            this.CameraAngleLabel.AutoSize = true;
            this.CameraAngleLabel.Location = new System.Drawing.Point(238, 58);
            this.CameraAngleLabel.Name = "CameraAngleLabel";
            this.CameraAngleLabel.Size = new System.Drawing.Size(60, 13);
            this.CameraAngleLabel.TabIndex = 88;
            this.CameraAngleLabel.Text = "Camera ID:";
            // 
            // HookshotableCheckbox
            // 
            this.HookshotableCheckbox.AutoSize = true;
            this.HookshotableCheckbox.Location = new System.Drawing.Point(6, 136);
            this.HookshotableCheckbox.Name = "HookshotableCheckbox";
            this.HookshotableCheckbox.Size = new System.Drawing.Size(95, 17);
            this.HookshotableCheckbox.TabIndex = 87;
            this.HookshotableCheckbox.Text = "Hookshot-able";
            this.HookshotableCheckbox.UseVisualStyleBackColor = true;
            this.HookshotableCheckbox.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // ExitNumber
            // 
            this.ExitNumber.AlwaysFireValueChanged = false;
            this.ExitNumber.DisplayDigits = 1;
            this.ExitNumber.DoValueRollover = true;
            this.ExitNumber.Hexadecimal = true;
            this.ExitNumber.IncrementMouseWheel = 1;
            this.ExitNumber.Location = new System.Drawing.Point(89, 56);
            this.ExitNumber.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.ExitNumber.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.ExitNumber.Name = "ExitNumber";
            this.ExitNumber.ShiftMultiplier = 1;
            this.ExitNumber.Size = new System.Drawing.Size(40, 20);
            this.ExitNumber.TabIndex = 1;
            this.EnvironmentControlTooltip.SetToolTip(this.ExitNumber, "Exit ID that will be triggered when Link steps or is slightly above of it.");
            this.ExitNumber.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ExitNumber.ValueChanged += new System.EventHandler(this.numericUpDownEx10_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 13);
            this.label17.TabIndex = 86;
            this.label17.Text = "Exit ID:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.IceRadioButton);
            this.panel5.Controls.Add(this.KillingQuicksand2Radio);
            this.panel5.Controls.Add(this.JabuJabuRadio);
            this.panel5.Controls.Add(this.NoFallDamageRadio);
            this.panel5.Controls.Add(this.KillingLavaRadioButton);
            this.panel5.Controls.Add(this.radioButton7);
            this.panel5.Controls.Add(this.radioButton6);
            this.panel5.Controls.Add(this.radioButton5);
            this.panel5.Controls.Add(this.radioButton4);
            this.panel5.Location = new System.Drawing.Point(6, 273);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(382, 68);
            this.panel5.TabIndex = 85;
            // 
            // IceRadioButton
            // 
            this.IceRadioButton.AutoSize = true;
            this.IceRadioButton.Location = new System.Drawing.Point(0, 43);
            this.IceRadioButton.Name = "IceRadioButton";
            this.IceRadioButton.Size = new System.Drawing.Size(40, 17);
            this.IceRadioButton.TabIndex = 8;
            this.IceRadioButton.Text = "Ice";
            this.IceRadioButton.UseVisualStyleBackColor = true;
            this.IceRadioButton.CheckedChanged += new System.EventHandler(this.IceRadioButton_CheckedChanged);
            // 
            // KillingQuicksand2Radio
            // 
            this.KillingQuicksand2Radio.AutoSize = true;
            this.KillingQuicksand2Radio.Location = new System.Drawing.Point(190, 21);
            this.KillingQuicksand2Radio.Name = "KillingQuicksand2Radio";
            this.KillingQuicksand2Radio.Size = new System.Drawing.Size(104, 17);
            this.KillingQuicksand2Radio.TabIndex = 7;
            this.KillingQuicksand2Radio.Text = "Kill-sand no jump";
            this.KillingQuicksand2Radio.UseVisualStyleBackColor = true;
            this.KillingQuicksand2Radio.CheckedChanged += new System.EventHandler(this.KillingQuicksand2Radio_CheckedChanged);
            // 
            // JabuJabuRadio
            // 
            this.JabuJabuRadio.AutoSize = true;
            this.JabuJabuRadio.Location = new System.Drawing.Point(305, 22);
            this.JabuJabuRadio.Name = "JabuJabuRadio";
            this.JabuJabuRadio.Size = new System.Drawing.Size(74, 17);
            this.JabuJabuRadio.TabIndex = 6;
            this.JabuJabuRadio.Text = "Jabu-Jabu";
            this.JabuJabuRadio.UseVisualStyleBackColor = true;
            this.JabuJabuRadio.CheckedChanged += new System.EventHandler(this.JabuJabuRadio_CheckedChanged);
            // 
            // NoFallDamageRadio
            // 
            this.NoFallDamageRadio.AutoSize = true;
            this.NoFallDamageRadio.Location = new System.Drawing.Point(83, 22);
            this.NoFallDamageRadio.Name = "NoFallDamageRadio";
            this.NoFallDamageRadio.Size = new System.Drawing.Size(110, 17);
            this.NoFallDamageRadio.TabIndex = 5;
            this.NoFallDamageRadio.Text = "No falling damage";
            this.NoFallDamageRadio.UseVisualStyleBackColor = true;
            this.NoFallDamageRadio.CheckedChanged += new System.EventHandler(this.NoFallDamageRadio_CheckedChanged);
            // 
            // KillingLavaRadioButton
            // 
            this.KillingLavaRadioButton.AutoSize = true;
            this.KillingLavaRadioButton.Location = new System.Drawing.Point(0, 22);
            this.KillingLavaRadioButton.Name = "KillingLavaRadioButton";
            this.KillingLavaRadioButton.Size = new System.Drawing.Size(79, 17);
            this.KillingLavaRadioButton.TabIndex = 4;
            this.KillingLavaRadioButton.Text = "Killing Lava";
            this.KillingLavaRadioButton.UseVisualStyleBackColor = true;
            this.KillingLavaRadioButton.CheckedChanged += new System.EventHandler(this.KillingLavaRadioButton_CheckedChanged);
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Checked = true;
            this.radioButton7.Location = new System.Drawing.Point(0, 3);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(59, 17);
            this.radioButton7.TabIndex = 3;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "Default";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(291, 3);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(91, 17);
            this.radioButton6.TabIndex = 2;
            this.radioButton6.Text = "Floor Damage";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(187, 3);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(106, 17);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.Text = "Killing Quicksand";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(65, 3);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(116, 17);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.Text = "Shallow Quicksand";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.NoLedgeClimbRadio);
            this.panel4.Controls.Add(this.CrawlSpaceRadio);
            this.panel4.Controls.Add(this.LadderTopRadioButton);
            this.panel4.Controls.Add(this.radioButton1);
            this.panel4.Controls.Add(this.radioButton3);
            this.panel4.Controls.Add(this.radioButton2);
            this.panel4.Location = new System.Drawing.Point(9, 220);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(382, 44);
            this.panel4.TabIndex = 84;
            // 
            // NoLedgeClimbRadio
            // 
            this.NoLedgeClimbRadio.AutoSize = true;
            this.NoLedgeClimbRadio.Location = new System.Drawing.Point(86, 24);
            this.NoLedgeClimbRadio.Name = "NoLedgeClimbRadio";
            this.NoLedgeClimbRadio.Size = new System.Drawing.Size(100, 17);
            this.NoLedgeClimbRadio.TabIndex = 87;
            this.NoLedgeClimbRadio.Text = "No Ledge Climb";
            this.NoLedgeClimbRadio.UseVisualStyleBackColor = true;
            this.NoLedgeClimbRadio.CheckedChanged += new System.EventHandler(this.NoLedgeClimbRadio_CheckedChanged);
            // 
            // CrawlSpaceRadio
            // 
            this.CrawlSpaceRadio.AutoSize = true;
            this.CrawlSpaceRadio.Location = new System.Drawing.Point(0, 24);
            this.CrawlSpaceRadio.Name = "CrawlSpaceRadio";
            this.CrawlSpaceRadio.Size = new System.Drawing.Size(85, 17);
            this.CrawlSpaceRadio.TabIndex = 85;
            this.CrawlSpaceRadio.Text = "Crawl Space";
            this.CrawlSpaceRadio.UseVisualStyleBackColor = true;
            this.CrawlSpaceRadio.CheckedChanged += new System.EventHandler(this.CrawlSpaceRadio_CheckedChanged);
            // 
            // LadderTopRadioButton
            // 
            this.LadderTopRadioButton.AutoSize = true;
            this.LadderTopRadioButton.Location = new System.Drawing.Point(135, 3);
            this.LadderTopRadioButton.Name = "LadderTopRadioButton";
            this.LadderTopRadioButton.Size = new System.Drawing.Size(80, 17);
            this.LadderTopRadioButton.TabIndex = 84;
            this.LadderTopRadioButton.Text = "Ladder Top";
            this.LadderTopRadioButton.UseVisualStyleBackColor = true;
            this.LadderTopRadioButton.CheckedChanged += new System.EventHandler(this.LadderTopRadioButton_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(0, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 17);
            this.radioButton1.TabIndex = 81;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Nothing";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(221, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(110, 17);
            this.radioButton3.TabIndex = 83;
            this.radioButton3.Text = "Climbable Surface";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(71, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(58, 17);
            this.radioButton2.TabIndex = 82;
            this.radioButton2.Text = "Ladder";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // SteepterrainCheckbox
            // 
            this.SteepterrainCheckbox.AutoSize = true;
            this.SteepterrainCheckbox.Location = new System.Drawing.Point(135, 111);
            this.SteepterrainCheckbox.Name = "SteepterrainCheckbox";
            this.SteepterrainCheckbox.Size = new System.Drawing.Size(60, 17);
            this.SteepterrainCheckbox.TabIndex = 33;
            this.SteepterrainCheckbox.Text = "Steep?";
            this.SteepterrainCheckbox.UseVisualStyleBackColor = true;
            this.SteepterrainCheckbox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // TerrainType
            // 
            this.TerrainType.AlwaysFireValueChanged = false;
            this.TerrainType.DisplayDigits = 1;
            this.TerrainType.DoValueRollover = true;
            this.TerrainType.Hexadecimal = true;
            this.TerrainType.IncrementMouseWheel = 3;
            this.TerrainType.Location = new System.Drawing.Point(89, 108);
            this.TerrainType.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.TerrainType.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.TerrainType.Name = "TerrainType";
            this.TerrainType.ShiftMultiplier = 1;
            this.TerrainType.Size = new System.Drawing.Size(40, 20);
            this.TerrainType.TabIndex = 32;
            this.EnvironmentControlTooltip.SetToolTip(this.TerrainType, resources.GetString("TerrainType.ToolTip"));
            this.TerrainType.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TerrainType.ValueChanged += new System.EventHandler(this.numericUpDownEx8_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 112);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Terrain Type:";
            // 
            // GroundType
            // 
            this.GroundType.AlwaysFireValueChanged = false;
            this.GroundType.DisplayDigits = 1;
            this.GroundType.DoValueRollover = true;
            this.GroundType.Hexadecimal = true;
            this.GroundType.IncrementMouseWheel = 3;
            this.GroundType.Location = new System.Drawing.Point(324, 108);
            this.GroundType.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.GroundType.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.GroundType.Name = "GroundType";
            this.GroundType.ShiftMultiplier = 1;
            this.GroundType.Size = new System.Drawing.Size(40, 20);
            this.GroundType.TabIndex = 80;
            this.EnvironmentControlTooltip.SetToolTip(this.GroundType, resources.GetString("GroundType.ToolTip"));
            this.GroundType.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GroundType.ValueChanged += new System.EventHandler(this.numericUpDownEx9_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(238, 112);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "Ground Type:";
            // 
            // EnvironmentType
            // 
            this.EnvironmentType.AlwaysFireValueChanged = false;
            this.EnvironmentType.DisplayDigits = 1;
            this.EnvironmentType.DoValueRollover = true;
            this.EnvironmentType.Hexadecimal = true;
            this.EnvironmentType.IncrementMouseWheel = 1;
            this.EnvironmentType.Location = new System.Drawing.Point(324, 82);
            this.EnvironmentType.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.EnvironmentType.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.EnvironmentType.Name = "EnvironmentType";
            this.EnvironmentType.ShiftMultiplier = 1;
            this.EnvironmentType.Size = new System.Drawing.Size(40, 20);
            this.EnvironmentType.TabIndex = 26;
            this.EnvironmentControlTooltip.SetToolTip(this.EnvironmentType, "0: Default, loads the first 4 environments\r\n1-7: Loads environments X to X+3, X b" +
        "eing flag * 4\r\n8-F: Same as 0-7\r\nNote: Each one of the 4 environments is used fo" +
        "r a different time of the day");
            this.EnvironmentType.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnvironmentType.ValueChanged += new System.EventHandler(this.numericUpDownEx7_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(238, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "Environment:";
            // 
            // EchoRange
            // 
            this.EchoRange.AlwaysFireValueChanged = false;
            this.EchoRange.DisplayDigits = 1;
            this.EchoRange.DoValueRollover = true;
            this.EchoRange.Hexadecimal = true;
            this.EchoRange.IncrementMouseWheel = 3;
            this.EchoRange.Location = new System.Drawing.Point(89, 82);
            this.EchoRange.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.EchoRange.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            -2147483648});
            this.EchoRange.Name = "EchoRange";
            this.EchoRange.ShiftMultiplier = 1;
            this.EchoRange.Size = new System.Drawing.Size(40, 20);
            this.EchoRange.TabIndex = 24;
            this.EnvironmentControlTooltip.SetToolTip(this.EchoRange, "Sound echo when Link steps on it\r\n0: Use map value\r\n1-F: Echo ranges");
            this.EchoRange.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EchoRange.ValueChanged += new System.EventHandler(this.numericUpDownEx3_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Echo Range:";
            // 
            // niceLine4
            // 
            this.niceLine4.Location = new System.Drawing.Point(3, 35);
            this.niceLine4.Name = "niceLine4";
            this.niceLine4.Size = new System.Drawing.Size(385, 15);
            this.niceLine4.TabIndex = 22;
            this.niceLine4.TabStop = false;
            // 
            // PolygonRawdata
            // 
            this.PolygonRawdata.AllowHex = true;
            this.PolygonRawdata.Digits = 16;
            this.PolygonRawdata.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PolygonRawdata.Location = new System.Drawing.Point(89, 9);
            this.PolygonRawdata.Name = "PolygonRawdata";
            this.PolygonRawdata.Size = new System.Drawing.Size(275, 20);
            this.PolygonRawdata.TabIndex = 0;
            this.PolygonRawdata.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PolygonRawData_KeyDown);
            this.PolygonRawdata.Leave += new System.EventHandler(this.PolygonRawdata_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Raw Data:";
            // 
            // niceLine10
            // 
            this.niceLine10.Location = new System.Drawing.Point(7, 339);
            this.niceLine10.Name = "niceLine10";
            this.niceLine10.Size = new System.Drawing.Size(385, 15);
            this.niceLine10.TabIndex = 93;
            this.niceLine10.TabStop = false;
            // 
            // niceLine11
            // 
            this.niceLine11.Location = new System.Drawing.Point(6, 384);
            this.niceLine11.Name = "niceLine11";
            this.niceLine11.Size = new System.Drawing.Size(385, 15);
            this.niceLine11.TabIndex = 99;
            this.niceLine11.TabStop = false;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(9, 46);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(382, 15);
            this.niceLine1.TabIndex = 21;
            this.niceLine1.TabStop = false;
            // 
            // DeletepolygonButton
            // 
            this.DeletepolygonButton.Location = new System.Drawing.Point(250, 19);
            this.DeletepolygonButton.Name = "DeletepolygonButton";
            this.DeletepolygonButton.Size = new System.Drawing.Size(120, 23);
            this.DeletepolygonButton.TabIndex = 20;
            this.DeletepolygonButton.Text = "Delete Polygon Type";
            this.DeletepolygonButton.UseVisualStyleBackColor = true;
            this.DeletepolygonButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddpolygonButton
            // 
            this.AddpolygonButton.Location = new System.Drawing.Point(124, 19);
            this.AddpolygonButton.Name = "AddpolygonButton";
            this.AddpolygonButton.Size = new System.Drawing.Size(120, 23);
            this.AddpolygonButton.TabIndex = 19;
            this.AddpolygonButton.Text = "Add Polygon Type";
            this.AddpolygonButton.UseVisualStyleBackColor = true;
            this.AddpolygonButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // PolygonSelect
            // 
            this.PolygonSelect.Enabled = false;
            this.PolygonSelect.Location = new System.Drawing.Point(9, 22);
            this.PolygonSelect.Name = "PolygonSelect";
            this.PolygonSelect.Size = new System.Drawing.Size(65, 20);
            this.PolygonSelect.TabIndex = 2;
            this.PolygonSelect.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // tabTransitions
            // 
            this.tabTransitions.Controls.Add(this.actorEditControl3);
            this.tabTransitions.Controls.Add(this.actorEditControl2);
            this.tabTransitions.Location = new System.Drawing.Point(4, 40);
            this.tabTransitions.Name = "tabTransitions";
            this.tabTransitions.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransitions.Size = new System.Drawing.Size(411, 676);
            this.tabTransitions.TabIndex = 3;
            this.tabTransitions.Text = "Transitions & Spawns";
            this.tabTransitions.UseVisualStyleBackColor = true;
            // 
            // actorEditControl3
            // 
            this.actorEditControl3.ActorNumber = -1;
            this.actorEditControl3.Enabled = false;
            this.actorEditControl3.Location = new System.Drawing.Point(3, 328);
            this.actorEditControl3.Name = "actorEditControl3";
            this.actorEditControl3.Size = new System.Drawing.Size(402, 319);
            this.actorEditControl3.TabIndex = 1;
            // 
            // actorEditControl2
            // 
            this.actorEditControl2.ActorNumber = -1;
            this.actorEditControl2.Enabled = false;
            this.actorEditControl2.Location = new System.Drawing.Point(3, 3);
            this.actorEditControl2.Name = "actorEditControl2";
            this.actorEditControl2.Size = new System.Drawing.Size(402, 319);
            this.actorEditControl2.TabIndex = 0;
            // 
            // tabPathways
            // 
            this.tabPathways.Controls.Add(this.ActorCutsceneGroupBox);
            this.tabPathways.Controls.Add(this.groupBox13);
            this.tabPathways.Location = new System.Drawing.Point(4, 40);
            this.tabPathways.Name = "tabPathways";
            this.tabPathways.Padding = new System.Windows.Forms.Padding(3);
            this.tabPathways.Size = new System.Drawing.Size(411, 676);
            this.tabPathways.TabIndex = 6;
            this.tabPathways.Text = "Pathways";
            this.tabPathways.UseVisualStyleBackColor = true;
            // 
            // ActorCutsceneGroupBox
            // 
            this.ActorCutsceneGroupBox.Controls.Add(this.label143);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneUnknown);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneCamIndex);
            this.ActorCutsceneGroupBox.Controls.Add(this.label142);
            this.ActorCutsceneGroupBox.Controls.Add(this.label141);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneRetCamera);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneHudFade);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneBlackBars);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutscenePuzzleSound);
            this.ActorCutsceneGroupBox.Controls.Add(this.label140);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneAdditionalActorCs);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneDeleteButton);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneAddButton);
            this.ActorCutsceneGroupBox.Controls.Add(this.label37);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneCsIndex);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneNumber);
            this.ActorCutsceneGroupBox.Controls.Add(this.label138);
            this.ActorCutsceneGroupBox.Controls.Add(this.niceLine18);
            this.ActorCutsceneGroupBox.Controls.Add(this.label139);
            this.ActorCutsceneGroupBox.Controls.Add(this.ActorCutsceneLength);
            this.ActorCutsceneGroupBox.Location = new System.Drawing.Point(3, 281);
            this.ActorCutsceneGroupBox.Name = "ActorCutsceneGroupBox";
            this.ActorCutsceneGroupBox.Size = new System.Drawing.Size(396, 269);
            this.ActorCutsceneGroupBox.TabIndex = 98;
            this.ActorCutsceneGroupBox.TabStop = false;
            this.ActorCutsceneGroupBox.Text = "Actor Cutscene";
            this.ActorCutsceneGroupBox.Visible = false;
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Enabled = false;
            this.label143.Location = new System.Drawing.Point(11, 143);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(56, 13);
            this.label143.TabIndex = 50;
            this.label143.Text = "Unknown:";
            // 
            // ActorCutsceneUnknown
            // 
            this.ActorCutsceneUnknown.AlwaysFireValueChanged = false;
            this.ActorCutsceneUnknown.DisplayDigits = 1;
            this.ActorCutsceneUnknown.DoValueRollover = false;
            this.ActorCutsceneUnknown.Enabled = false;
            this.ActorCutsceneUnknown.Hexadecimal = true;
            this.ActorCutsceneUnknown.IncrementMouseWheel = 1;
            this.ActorCutsceneUnknown.Location = new System.Drawing.Point(100, 141);
            this.ActorCutsceneUnknown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ActorCutsceneUnknown.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorCutsceneUnknown.Name = "ActorCutsceneUnknown";
            this.ActorCutsceneUnknown.ShiftMultiplier = 20;
            this.ActorCutsceneUnknown.Size = new System.Drawing.Size(53, 20);
            this.ActorCutsceneUnknown.TabIndex = 51;
            this.EnvironmentControlTooltip.SetToolTip(this.ActorCutsceneUnknown, "Value 0 causes bugs");
            this.ActorCutsceneUnknown.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorCutsceneUnknown.ValueChanged += new System.EventHandler(this.ActorCutsceneUnknown_ValueChanged);
            // 
            // ActorCutsceneCamIndex
            // 
            this.ActorCutsceneCamIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActorCutsceneCamIndex.FormattingEnabled = true;
            this.ActorCutsceneCamIndex.Location = new System.Drawing.Point(244, 62);
            this.ActorCutsceneCamIndex.Name = "ActorCutsceneCamIndex";
            this.ActorCutsceneCamIndex.Size = new System.Drawing.Size(141, 21);
            this.ActorCutsceneCamIndex.TabIndex = 49;
            this.ActorCutsceneCamIndex.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.ActorCutsceneCamIndex.SelectionChangeCommitted += new System.EventHandler(this.ActorCutsceneCamIndex_SelectionChangeCommitted);
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Enabled = false;
            this.label142.Location = new System.Drawing.Point(168, 117);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(69, 13);
            this.label142.TabIndex = 48;
            this.label142.Text = "Ret. Camera:";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Enabled = false;
            this.label141.Location = new System.Drawing.Point(168, 91);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(61, 13);
            this.label141.TabIndex = 47;
            this.label141.Text = "HUD Fade:";
            // 
            // ActorCutsceneRetCamera
            // 
            this.ActorCutsceneRetCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActorCutsceneRetCamera.FormattingEnabled = true;
            this.ActorCutsceneRetCamera.Location = new System.Drawing.Point(244, 120);
            this.ActorCutsceneRetCamera.Name = "ActorCutsceneRetCamera";
            this.ActorCutsceneRetCamera.Size = new System.Drawing.Size(141, 21);
            this.ActorCutsceneRetCamera.TabIndex = 46;
            this.EnvironmentControlTooltip.SetToolTip(this.ActorCutsceneRetCamera, "Behaviour of the camera after cutscene ends");
            this.ActorCutsceneRetCamera.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.ActorCutsceneRetCamera.SelectionChangeCommitted += new System.EventHandler(this.ActorCutsceneRetCamera_SelectionChangeCommitted);
            // 
            // ActorCutsceneHudFade
            // 
            this.ActorCutsceneHudFade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActorCutsceneHudFade.FormattingEnabled = true;
            this.ActorCutsceneHudFade.Location = new System.Drawing.Point(244, 91);
            this.ActorCutsceneHudFade.Name = "ActorCutsceneHudFade";
            this.ActorCutsceneHudFade.Size = new System.Drawing.Size(141, 21);
            this.ActorCutsceneHudFade.TabIndex = 45;
            this.ActorCutsceneHudFade.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.ActorCutsceneHudFade.SelectionChangeCommitted += new System.EventHandler(this.ActorCutsceneHudFade_SelectionChangeCommitted);
            // 
            // ActorCutsceneBlackBars
            // 
            this.ActorCutsceneBlackBars.AutoSize = true;
            this.ActorCutsceneBlackBars.Location = new System.Drawing.Point(9, 193);
            this.ActorCutsceneBlackBars.Name = "ActorCutsceneBlackBars";
            this.ActorCutsceneBlackBars.Size = new System.Drawing.Size(77, 17);
            this.ActorCutsceneBlackBars.TabIndex = 24;
            this.ActorCutsceneBlackBars.Text = "Black Bars";
            this.ActorCutsceneBlackBars.UseVisualStyleBackColor = true;
            this.ActorCutsceneBlackBars.CheckedChanged += new System.EventHandler(this.ActorCutsceneBlackBars_CheckedChanged);
            // 
            // ActorCutscenePuzzleSound
            // 
            this.ActorCutscenePuzzleSound.AutoSize = true;
            this.ActorCutscenePuzzleSound.Location = new System.Drawing.Point(9, 170);
            this.ActorCutscenePuzzleSound.Name = "ActorCutscenePuzzleSound";
            this.ActorCutscenePuzzleSound.Size = new System.Drawing.Size(91, 17);
            this.ActorCutscenePuzzleSound.TabIndex = 23;
            this.ActorCutscenePuzzleSound.Text = "Puzzle Sound";
            this.EnvironmentControlTooltip.SetToolTip(this.ActorCutscenePuzzleSound, "Plays Puzzle Sound");
            this.ActorCutscenePuzzleSound.UseVisualStyleBackColor = true;
            this.ActorCutscenePuzzleSound.CheckedChanged += new System.EventHandler(this.ActorCutscenePuzzleSound_CheckedChanged);
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Enabled = false;
            this.label140.Location = new System.Drawing.Point(11, 117);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(75, 13);
            this.label140.TabIndex = 21;
            this.label140.Text = "Add. Actor Cs:";
            // 
            // ActorCutsceneAdditionalActorCs
            // 
            this.ActorCutsceneAdditionalActorCs.AlwaysFireValueChanged = false;
            this.ActorCutsceneAdditionalActorCs.DisplayDigits = 1;
            this.ActorCutsceneAdditionalActorCs.DoValueRollover = false;
            this.ActorCutsceneAdditionalActorCs.Enabled = false;
            this.ActorCutsceneAdditionalActorCs.IncrementMouseWheel = 1;
            this.ActorCutsceneAdditionalActorCs.Location = new System.Drawing.Point(100, 115);
            this.ActorCutsceneAdditionalActorCs.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ActorCutsceneAdditionalActorCs.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.ActorCutsceneAdditionalActorCs.Name = "ActorCutsceneAdditionalActorCs";
            this.ActorCutsceneAdditionalActorCs.ShiftMultiplier = 20;
            this.ActorCutsceneAdditionalActorCs.Size = new System.Drawing.Size(53, 20);
            this.ActorCutsceneAdditionalActorCs.TabIndex = 22;
            this.EnvironmentControlTooltip.SetToolTip(this.ActorCutsceneAdditionalActorCs, "Additional Actor Cutscene ID (needed for chests)");
            this.ActorCutsceneAdditionalActorCs.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorCutsceneAdditionalActorCs.ValueChanged += new System.EventHandler(this.ActorCutsceneAdditionalActorCs_ValueChanged);
            // 
            // ActorCutsceneDeleteButton
            // 
            this.ActorCutsceneDeleteButton.Location = new System.Drawing.Point(250, 19);
            this.ActorCutsceneDeleteButton.Name = "ActorCutsceneDeleteButton";
            this.ActorCutsceneDeleteButton.Size = new System.Drawing.Size(120, 23);
            this.ActorCutsceneDeleteButton.TabIndex = 9;
            this.ActorCutsceneDeleteButton.Text = "Delete Actor Cs";
            this.ActorCutsceneDeleteButton.UseVisualStyleBackColor = true;
            this.ActorCutsceneDeleteButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActorCutsceneDeleteButton_MouseClick);
            // 
            // ActorCutsceneAddButton
            // 
            this.ActorCutsceneAddButton.Location = new System.Drawing.Point(124, 19);
            this.ActorCutsceneAddButton.Name = "ActorCutsceneAddButton";
            this.ActorCutsceneAddButton.Size = new System.Drawing.Size(120, 23);
            this.ActorCutsceneAddButton.TabIndex = 8;
            this.ActorCutsceneAddButton.Text = "Add Actor Cs";
            this.ActorCutsceneAddButton.UseVisualStyleBackColor = true;
            this.ActorCutsceneAddButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActorCutsceneAddButton_MouseClick);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Enabled = false;
            this.label37.Location = new System.Drawing.Point(11, 65);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(83, 13);
            this.label37.TabIndex = 7;
            this.label37.Text = "Length (frames):";
            // 
            // ActorCutsceneCsIndex
            // 
            this.ActorCutsceneCsIndex.AlwaysFireValueChanged = false;
            this.ActorCutsceneCsIndex.DisplayDigits = 1;
            this.ActorCutsceneCsIndex.DoValueRollover = false;
            this.ActorCutsceneCsIndex.Enabled = false;
            this.ActorCutsceneCsIndex.IncrementMouseWheel = 1;
            this.ActorCutsceneCsIndex.Location = new System.Drawing.Point(100, 89);
            this.ActorCutsceneCsIndex.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ActorCutsceneCsIndex.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.ActorCutsceneCsIndex.Name = "ActorCutsceneCsIndex";
            this.ActorCutsceneCsIndex.ShiftMultiplier = 20;
            this.ActorCutsceneCsIndex.Size = new System.Drawing.Size(53, 20);
            this.ActorCutsceneCsIndex.TabIndex = 12;
            this.EnvironmentControlTooltip.SetToolTip(this.ActorCutsceneCsIndex, "As of now, SharpOcarina can only have 1 cutscene, so only ID 0 or NULL are possib" +
        "le values");
            this.ActorCutsceneCsIndex.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorCutsceneCsIndex.ValueChanged += new System.EventHandler(this.ActorCutsceneCsIndex_ValueChanged);
            // 
            // ActorCutsceneNumber
            // 
            this.ActorCutsceneNumber.Hexadecimal = true;
            this.ActorCutsceneNumber.Location = new System.Drawing.Point(9, 22);
            this.ActorCutsceneNumber.Name = "ActorCutsceneNumber";
            this.ActorCutsceneNumber.Size = new System.Drawing.Size(65, 20);
            this.ActorCutsceneNumber.TabIndex = 7;
            this.ActorCutsceneNumber.ValueChanged += new System.EventHandler(this.ActorCutsceneNumber_ValueChanged);
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Enabled = false;
            this.label138.Location = new System.Drawing.Point(11, 91);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(88, 13);
            this.label138.TabIndex = 11;
            this.label138.Text = "Cutscene Index*:";
            // 
            // niceLine18
            // 
            this.niceLine18.Location = new System.Drawing.Point(6, 42);
            this.niceLine18.Name = "niceLine18";
            this.niceLine18.Size = new System.Drawing.Size(384, 15);
            this.niceLine18.TabIndex = 20;
            this.niceLine18.TabStop = false;
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Enabled = false;
            this.label139.Location = new System.Drawing.Point(164, 65);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(74, 13);
            this.label139.TabIndex = 9;
            this.label139.Text = "Camera index:";
            // 
            // ActorCutsceneLength
            // 
            this.ActorCutsceneLength.AlwaysFireValueChanged = false;
            this.ActorCutsceneLength.DisplayDigits = 1;
            this.ActorCutsceneLength.DoValueRollover = false;
            this.ActorCutsceneLength.Enabled = false;
            this.ActorCutsceneLength.IncrementMouseWheel = 1;
            this.ActorCutsceneLength.Location = new System.Drawing.Point(100, 63);
            this.ActorCutsceneLength.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ActorCutsceneLength.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.ActorCutsceneLength.Name = "ActorCutsceneLength";
            this.ActorCutsceneLength.ShiftMultiplier = 20;
            this.ActorCutsceneLength.Size = new System.Drawing.Size(53, 20);
            this.ActorCutsceneLength.TabIndex = 10;
            this.EnvironmentControlTooltip.SetToolTip(this.ActorCutsceneLength, "Use 135 for Chest FFF6 cutscene");
            this.ActorCutsceneLength.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ActorCutsceneLength.ValueChanged += new System.EventHandler(this.ActorCutsceneLength_ValueChanged);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.PathwayDown);
            this.groupBox13.Controls.Add(this.PathwayUp);
            this.groupBox13.Controls.Add(this.PathwayZPosStrip);
            this.groupBox13.Controls.Add(this.PathwayYPosStrip);
            this.groupBox13.Controls.Add(this.PathwayXPosStrip);
            this.groupBox13.Controls.Add(this.DeletePointButton);
            this.groupBox13.Controls.Add(this.AddPointButton);
            this.groupBox13.Controls.Add(this.PathwayListBox);
            this.groupBox13.Controls.Add(this.PathwayDeleteButton);
            this.groupBox13.Controls.Add(this.PathwayAddButton);
            this.groupBox13.Controls.Add(this.PathwayLabel1);
            this.groupBox13.Controls.Add(this.PathwayZPos);
            this.groupBox13.Controls.Add(this.PathwayNumber);
            this.groupBox13.Controls.Add(this.PathwayLabel3);
            this.groupBox13.Controls.Add(this.niceLine6);
            this.groupBox13.Controls.Add(this.PathwayLabel2);
            this.groupBox13.Controls.Add(this.PathwayXPos);
            this.groupBox13.Controls.Add(this.PathwayYPos);
            this.groupBox13.Location = new System.Drawing.Point(3, 6);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(396, 269);
            this.groupBox13.TabIndex = 43;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Pathways";
            // 
            // PathwayDown
            // 
            this.PathwayDown.Location = new System.Drawing.Point(183, 79);
            this.PathwayDown.Name = "PathwayDown";
            this.PathwayDown.Size = new System.Drawing.Size(26, 23);
            this.PathwayDown.TabIndex = 97;
            this.PathwayDown.Text = "▼";
            this.PathwayDown.UseVisualStyleBackColor = true;
            this.PathwayDown.Click += new System.EventHandler(this.PathwayDown_Click);
            // 
            // PathwayUp
            // 
            this.PathwayUp.Location = new System.Drawing.Point(183, 53);
            this.PathwayUp.Name = "PathwayUp";
            this.PathwayUp.Size = new System.Drawing.Size(26, 23);
            this.PathwayUp.TabIndex = 96;
            this.PathwayUp.Text = "▲";
            this.PathwayUp.UseVisualStyleBackColor = true;
            this.PathwayUp.Click += new System.EventHandler(this.PathwayUp_Click);
            // 
            // PathwayZPosStrip
            // 
            this.PathwayZPosStrip.AutoSize = false;
            this.PathwayZPosStrip.BackColor = System.Drawing.Color.Transparent;
            this.PathwayZPosStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.PathwayZPosStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton3});
            this.PathwayZPosStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.PathwayZPosStrip.Location = new System.Drawing.Point(370, 139);
            this.PathwayZPosStrip.Name = "PathwayZPosStrip";
            this.PathwayZPosStrip.Size = new System.Drawing.Size(20, 19);
            this.PathwayZPosStrip.TabIndex = 49;
            this.PathwayZPosStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StickToZplus,
            this.StickToZminus});
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.ShowDropDownArrow = false;
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(4, 4);
            this.toolStripDropDownButton3.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton3.ToolTipText = "Actions";
            // 
            // StickToZplus
            // 
            this.StickToZplus.Name = "StickToZplus";
            this.StickToZplus.Size = new System.Drawing.Size(185, 22);
            this.StickToZplus.Text = "Stick to positive wall";
            this.StickToZplus.Click += new System.EventHandler(this.PathwayStickToWall);
            // 
            // StickToZminus
            // 
            this.StickToZminus.Name = "StickToZminus";
            this.StickToZminus.Size = new System.Drawing.Size(185, 22);
            this.StickToZminus.Text = "Stick to negative wall";
            this.StickToZminus.Click += new System.EventHandler(this.PathwayStickToWall);
            // 
            // PathwayYPosStrip
            // 
            this.PathwayYPosStrip.AutoSize = false;
            this.PathwayYPosStrip.BackColor = System.Drawing.Color.Transparent;
            this.PathwayYPosStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.PathwayYPosStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton2});
            this.PathwayYPosStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.PathwayYPosStrip.Location = new System.Drawing.Point(370, 113);
            this.PathwayYPosStrip.Name = "PathwayYPosStrip";
            this.PathwayYPosStrip.Size = new System.Drawing.Size(20, 19);
            this.PathwayYPosStrip.TabIndex = 48;
            this.PathwayYPosStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StickToYplus,
            this.StickToYminus});
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.ShowDropDownArrow = false;
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(4, 4);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton2.ToolTipText = "Actions";
            // 
            // StickToYplus
            // 
            this.StickToYplus.Name = "StickToYplus";
            this.StickToYplus.Size = new System.Drawing.Size(155, 22);
            this.StickToYplus.Text = "Stick to ceiling";
            this.StickToYplus.Click += new System.EventHandler(this.PathwayStickToWall);
            // 
            // StickToYminus
            // 
            this.StickToYminus.Name = "StickToYminus";
            this.StickToYminus.Size = new System.Drawing.Size(155, 22);
            this.StickToYminus.Text = "Stick to ground";
            this.StickToYminus.Click += new System.EventHandler(this.PathwayStickToWall);
            // 
            // PathwayXPosStrip
            // 
            this.PathwayXPosStrip.AutoSize = false;
            this.PathwayXPosStrip.BackColor = System.Drawing.Color.Transparent;
            this.PathwayXPosStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.PathwayXPosStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.PathwayXPosStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.PathwayXPosStrip.Location = new System.Drawing.Point(370, 87);
            this.PathwayXPosStrip.Name = "PathwayXPosStrip";
            this.PathwayXPosStrip.Size = new System.Drawing.Size(20, 19);
            this.PathwayXPosStrip.TabIndex = 47;
            this.PathwayXPosStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StickToXplus,
            this.StickToXminus});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ShowDropDownArrow = false;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(4, 4);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ToolTipText = "Actions";
            // 
            // StickToXplus
            // 
            this.StickToXplus.Name = "StickToXplus";
            this.StickToXplus.Size = new System.Drawing.Size(185, 22);
            this.StickToXplus.Text = "Stick to positive wall";
            this.StickToXplus.Click += new System.EventHandler(this.PathwayStickToWall);
            // 
            // StickToXminus
            // 
            this.StickToXminus.Name = "StickToXminus";
            this.StickToXminus.Size = new System.Drawing.Size(185, 22);
            this.StickToXminus.Text = "Stick to negative wall";
            this.StickToXminus.Click += new System.EventHandler(this.PathwayStickToWall);
            // 
            // DeletePointButton
            // 
            this.DeletePointButton.Location = new System.Drawing.Point(303, 53);
            this.DeletePointButton.Name = "DeletePointButton";
            this.DeletePointButton.Size = new System.Drawing.Size(82, 23);
            this.DeletePointButton.TabIndex = 46;
            this.DeletePointButton.Text = "Delete Point";
            this.DeletePointButton.UseVisualStyleBackColor = true;
            this.DeletePointButton.Click += new System.EventHandler(this.DeletePointButton_Click);
            // 
            // AddPointButton
            // 
            this.AddPointButton.Location = new System.Drawing.Point(215, 53);
            this.AddPointButton.Name = "AddPointButton";
            this.AddPointButton.Size = new System.Drawing.Size(82, 23);
            this.AddPointButton.TabIndex = 45;
            this.AddPointButton.Text = "Add Point";
            this.EnvironmentControlTooltip.SetToolTip(this.AddPointButton, "Hold SHIFT to add in front of camera");
            this.AddPointButton.UseVisualStyleBackColor = true;
            this.AddPointButton.Click += new System.EventHandler(this.AddPointButton_Click);
            // 
            // PathwayListBox
            // 
            this.PathwayListBox.FormattingEnabled = true;
            this.PathwayListBox.IntegralHeight = false;
            this.PathwayListBox.Location = new System.Drawing.Point(6, 53);
            this.PathwayListBox.Name = "PathwayListBox";
            this.PathwayListBox.Size = new System.Drawing.Size(171, 210);
            this.PathwayListBox.TabIndex = 44;
            this.PathwayListBox.Click += new System.EventHandler(this.PathwayListBox_Click);
            // 
            // PathwayDeleteButton
            // 
            this.PathwayDeleteButton.Location = new System.Drawing.Point(250, 19);
            this.PathwayDeleteButton.Name = "PathwayDeleteButton";
            this.PathwayDeleteButton.Size = new System.Drawing.Size(120, 23);
            this.PathwayDeleteButton.TabIndex = 9;
            this.PathwayDeleteButton.Text = "Delete Pathway";
            this.PathwayDeleteButton.UseVisualStyleBackColor = true;
            this.PathwayDeleteButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DeletePathwayButton_Click);
            // 
            // PathwayAddButton
            // 
            this.PathwayAddButton.Location = new System.Drawing.Point(124, 19);
            this.PathwayAddButton.Name = "PathwayAddButton";
            this.PathwayAddButton.Size = new System.Drawing.Size(120, 23);
            this.PathwayAddButton.TabIndex = 8;
            this.PathwayAddButton.Text = "Add Pathway";
            this.PathwayAddButton.UseVisualStyleBackColor = true;
            this.PathwayAddButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddPathwayButton_Click);
            // 
            // PathwayLabel1
            // 
            this.PathwayLabel1.AutoSize = true;
            this.PathwayLabel1.Enabled = false;
            this.PathwayLabel1.Location = new System.Drawing.Point(215, 89);
            this.PathwayLabel1.Name = "PathwayLabel1";
            this.PathwayLabel1.Size = new System.Drawing.Size(57, 13);
            this.PathwayLabel1.TabIndex = 7;
            this.PathwayLabel1.Text = "X Position:";
            // 
            // PathwayZPos
            // 
            this.PathwayZPos.AlwaysFireValueChanged = false;
            this.PathwayZPos.DisplayDigits = 1;
            this.PathwayZPos.DoValueRollover = false;
            this.PathwayZPos.Enabled = false;
            this.PathwayZPos.IncrementMouseWheel = 1;
            this.PathwayZPos.Location = new System.Drawing.Point(286, 139);
            this.PathwayZPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.PathwayZPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.PathwayZPos.Name = "PathwayZPos";
            this.PathwayZPos.ShiftMultiplier = 20;
            this.PathwayZPos.Size = new System.Drawing.Size(84, 20);
            this.PathwayZPos.TabIndex = 12;
            this.PathwayZPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PathwayZPos.ValueChanged += new System.EventHandler(this.PathwayTransformZ_ValueChanged);
            // 
            // PathwayNumber
            // 
            this.PathwayNumber.Hexadecimal = true;
            this.PathwayNumber.Location = new System.Drawing.Point(9, 22);
            this.PathwayNumber.Name = "PathwayNumber";
            this.PathwayNumber.Size = new System.Drawing.Size(65, 20);
            this.PathwayNumber.TabIndex = 7;
            this.PathwayNumber.ValueChanged += new System.EventHandler(this.PathwayList_ValueChanged);
            // 
            // PathwayLabel3
            // 
            this.PathwayLabel3.AutoSize = true;
            this.PathwayLabel3.Enabled = false;
            this.PathwayLabel3.Location = new System.Drawing.Point(215, 141);
            this.PathwayLabel3.Name = "PathwayLabel3";
            this.PathwayLabel3.Size = new System.Drawing.Size(57, 13);
            this.PathwayLabel3.TabIndex = 11;
            this.PathwayLabel3.Text = "Z Position:";
            // 
            // niceLine6
            // 
            this.niceLine6.Location = new System.Drawing.Point(6, 42);
            this.niceLine6.Name = "niceLine6";
            this.niceLine6.Size = new System.Drawing.Size(384, 15);
            this.niceLine6.TabIndex = 20;
            this.niceLine6.TabStop = false;
            // 
            // PathwayLabel2
            // 
            this.PathwayLabel2.AutoSize = true;
            this.PathwayLabel2.Enabled = false;
            this.PathwayLabel2.Location = new System.Drawing.Point(215, 115);
            this.PathwayLabel2.Name = "PathwayLabel2";
            this.PathwayLabel2.Size = new System.Drawing.Size(57, 13);
            this.PathwayLabel2.TabIndex = 9;
            this.PathwayLabel2.Text = "Y Position:";
            // 
            // PathwayXPos
            // 
            this.PathwayXPos.AlwaysFireValueChanged = false;
            this.PathwayXPos.DisplayDigits = 1;
            this.PathwayXPos.DoValueRollover = false;
            this.PathwayXPos.Enabled = false;
            this.PathwayXPos.IncrementMouseWheel = 1;
            this.PathwayXPos.Location = new System.Drawing.Point(286, 87);
            this.PathwayXPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.PathwayXPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.PathwayXPos.Name = "PathwayXPos";
            this.PathwayXPos.ShiftMultiplier = 20;
            this.PathwayXPos.Size = new System.Drawing.Size(84, 20);
            this.PathwayXPos.TabIndex = 10;
            this.PathwayXPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PathwayXPos.ValueChanged += new System.EventHandler(this.PathwayTransformX_ValueChanged);
            // 
            // PathwayYPos
            // 
            this.PathwayYPos.AlwaysFireValueChanged = false;
            this.PathwayYPos.DisplayDigits = 1;
            this.PathwayYPos.DoValueRollover = false;
            this.PathwayYPos.Enabled = false;
            this.PathwayYPos.IncrementMouseWheel = 1;
            this.PathwayYPos.Location = new System.Drawing.Point(286, 113);
            this.PathwayYPos.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.PathwayYPos.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.PathwayYPos.Name = "PathwayYPos";
            this.PathwayYPos.ShiftMultiplier = 20;
            this.PathwayYPos.Size = new System.Drawing.Size(84, 20);
            this.PathwayYPos.TabIndex = 11;
            this.PathwayYPos.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PathwayYPos.ValueChanged += new System.EventHandler(this.PathwayTransformY_ValueChanged);
            // 
            // tabActors
            // 
            this.tabActors.Controls.Add(this.ObjectTabMenu);
            this.tabActors.Controls.Add(this.RoomObjectSpace);
            this.tabActors.Controls.Add(this.actorEditControl1);
            this.tabActors.Location = new System.Drawing.Point(4, 40);
            this.tabActors.Name = "tabActors";
            this.tabActors.Padding = new System.Windows.Forms.Padding(3);
            this.tabActors.Size = new System.Drawing.Size(411, 676);
            this.tabActors.TabIndex = 2;
            this.tabActors.Text = "Objects & Actors";
            this.tabActors.UseVisualStyleBackColor = true;
            // 
            // ObjectTabMenu
            // 
            this.ObjectTabMenu.Controls.Add(this.RoomObjectPage);
            this.ObjectTabMenu.Controls.Add(this.SceneObjectPage);
            this.ObjectTabMenu.Enabled = false;
            this.ObjectTabMenu.Location = new System.Drawing.Point(3, 3);
            this.ObjectTabMenu.Name = "ObjectTabMenu";
            this.ObjectTabMenu.SelectedIndex = 0;
            this.ObjectTabMenu.Size = new System.Drawing.Size(402, 141);
            this.ObjectTabMenu.TabIndex = 28;
            // 
            // RoomObjectPage
            // 
            this.RoomObjectPage.Controls.Add(this.RoomObjectListBox);
            this.RoomObjectPage.Controls.Add(this.RoomObjectDescription);
            this.RoomObjectPage.Controls.Add(this.RoomObjectDeleteButton);
            this.RoomObjectPage.Controls.Add(this.RoomObjectAddButton);
            this.RoomObjectPage.Location = new System.Drawing.Point(4, 22);
            this.RoomObjectPage.Name = "RoomObjectPage";
            this.RoomObjectPage.Padding = new System.Windows.Forms.Padding(3);
            this.RoomObjectPage.Size = new System.Drawing.Size(394, 115);
            this.RoomObjectPage.TabIndex = 0;
            this.RoomObjectPage.Text = "Room Objects";
            this.RoomObjectPage.UseVisualStyleBackColor = true;
            // 
            // RoomObjectListBox
            // 
            this.RoomObjectListBox.FormattingEnabled = true;
            this.RoomObjectListBox.Location = new System.Drawing.Point(3, 4);
            this.RoomObjectListBox.Name = "RoomObjectListBox";
            this.RoomObjectListBox.Size = new System.Drawing.Size(112, 108);
            this.RoomObjectListBox.TabIndex = 0;
            this.RoomObjectListBox.Click += new System.EventHandler(this.RoomObjectListBox_Click);
            this.RoomObjectListBox.DoubleClick += new System.EventHandler(this.RoomObjectListBox_DoubleClick);
            // 
            // RoomObjectDescription
            // 
            this.RoomObjectDescription.Location = new System.Drawing.Point(121, 30);
            this.RoomObjectDescription.Name = "RoomObjectDescription";
            this.RoomObjectDescription.Size = new System.Drawing.Size(246, 82);
            this.RoomObjectDescription.TabIndex = 3;
            this.RoomObjectDescription.Text = "ObjectDescription";
            // 
            // RoomObjectDeleteButton
            // 
            this.RoomObjectDeleteButton.Location = new System.Drawing.Point(247, 4);
            this.RoomObjectDeleteButton.Name = "RoomObjectDeleteButton";
            this.RoomObjectDeleteButton.Size = new System.Drawing.Size(120, 23);
            this.RoomObjectDeleteButton.TabIndex = 2;
            this.RoomObjectDeleteButton.Text = "Delete Object";
            this.RoomObjectDeleteButton.UseVisualStyleBackColor = true;
            this.RoomObjectDeleteButton.Click += new System.EventHandler(this.RoomObjectDeleteButton_Click);
            // 
            // RoomObjectAddButton
            // 
            this.RoomObjectAddButton.Location = new System.Drawing.Point(121, 4);
            this.RoomObjectAddButton.Name = "RoomObjectAddButton";
            this.RoomObjectAddButton.Size = new System.Drawing.Size(120, 23);
            this.RoomObjectAddButton.TabIndex = 1;
            this.RoomObjectAddButton.Text = "Add Object";
            this.RoomObjectAddButton.UseVisualStyleBackColor = true;
            this.RoomObjectAddButton.Click += new System.EventHandler(this.RoomObjectAddButton_Click);
            // 
            // SceneObjectPage
            // 
            this.SceneObjectPage.Controls.Add(this.SceneObjectDescription);
            this.SceneObjectPage.Controls.Add(this.SceneObjectListBox);
            this.SceneObjectPage.Controls.Add(this.SceneObjectDeleteButton);
            this.SceneObjectPage.Controls.Add(this.SceneObjectAddButton);
            this.SceneObjectPage.Location = new System.Drawing.Point(4, 22);
            this.SceneObjectPage.Name = "SceneObjectPage";
            this.SceneObjectPage.Padding = new System.Windows.Forms.Padding(3);
            this.SceneObjectPage.Size = new System.Drawing.Size(394, 115);
            this.SceneObjectPage.TabIndex = 1;
            this.SceneObjectPage.Text = "Scene Objects";
            this.SceneObjectPage.UseVisualStyleBackColor = true;
            // 
            // SceneObjectDescription
            // 
            this.SceneObjectDescription.Location = new System.Drawing.Point(121, 30);
            this.SceneObjectDescription.Name = "SceneObjectDescription";
            this.SceneObjectDescription.Size = new System.Drawing.Size(246, 82);
            this.SceneObjectDescription.TabIndex = 32;
            this.SceneObjectDescription.Text = "ObjectDescription";
            // 
            // SceneObjectListBox
            // 
            this.SceneObjectListBox.FormattingEnabled = true;
            this.SceneObjectListBox.Location = new System.Drawing.Point(3, 4);
            this.SceneObjectListBox.Name = "SceneObjectListBox";
            this.SceneObjectListBox.Size = new System.Drawing.Size(112, 108);
            this.SceneObjectListBox.TabIndex = 29;
            this.SceneObjectListBox.Click += new System.EventHandler(this.SceneObjectListBox_Click);
            this.SceneObjectListBox.DoubleClick += new System.EventHandler(this.SceneObjectListBox_DoubleClick);
            // 
            // SceneObjectDeleteButton
            // 
            this.SceneObjectDeleteButton.Location = new System.Drawing.Point(247, 4);
            this.SceneObjectDeleteButton.Name = "SceneObjectDeleteButton";
            this.SceneObjectDeleteButton.Size = new System.Drawing.Size(120, 23);
            this.SceneObjectDeleteButton.TabIndex = 31;
            this.SceneObjectDeleteButton.Text = "Delete Object";
            this.SceneObjectDeleteButton.UseVisualStyleBackColor = true;
            this.SceneObjectDeleteButton.Click += new System.EventHandler(this.SceneObjectDeleteButton_Click);
            // 
            // SceneObjectAddButton
            // 
            this.SceneObjectAddButton.Location = new System.Drawing.Point(121, 4);
            this.SceneObjectAddButton.Name = "SceneObjectAddButton";
            this.SceneObjectAddButton.Size = new System.Drawing.Size(120, 23);
            this.SceneObjectAddButton.TabIndex = 30;
            this.SceneObjectAddButton.Text = "Add Object";
            this.SceneObjectAddButton.UseVisualStyleBackColor = true;
            this.SceneObjectAddButton.Click += new System.EventHandler(this.SceneObjectAddButton_Click);
            // 
            // RoomObjectSpace
            // 
            this.RoomObjectSpace.AutoSize = true;
            this.RoomObjectSpace.Location = new System.Drawing.Point(9, 147);
            this.RoomObjectSpace.Name = "RoomObjectSpace";
            this.RoomObjectSpace.Size = new System.Drawing.Size(69, 13);
            this.RoomObjectSpace.TabIndex = 27;
            this.RoomObjectSpace.Text = "ObjectSpace";
            // 
            // actorEditControl1
            // 
            this.actorEditControl1.ActorNumber = -1;
            this.actorEditControl1.Enabled = false;
            this.actorEditControl1.Location = new System.Drawing.Point(3, 163);
            this.actorEditControl1.Name = "actorEditControl1";
            this.actorEditControl1.Size = new System.Drawing.Size(405, 319);
            this.actorEditControl1.TabIndex = 3;
            this.actorEditControl1.Load += new System.EventHandler(this.actorEditControl1_Load);
            // 
            // tabCutscene
            // 
            this.tabCutscene.Controls.Add(this.DebugTextBox);
            this.tabCutscene.Controls.Add(this.CutsceneTableEntryLabel);
            this.tabCutscene.Controls.Add(this.CutsceneFlagLabel);
            this.tabCutscene.Controls.Add(this.CutsceneSpawnLabel);
            this.tabCutscene.Controls.Add(this.CutsceneEntranceLabel);
            this.tabCutscene.Controls.Add(this.CutsceneGroupBox);
            this.tabCutscene.Controls.Add(this.CutsceneTableEntry);
            this.tabCutscene.Controls.Add(this.CutsceneFlag);
            this.tabCutscene.Controls.Add(this.CutsceneSpawn);
            this.tabCutscene.Controls.Add(this.CutsceneEntrance);
            this.tabCutscene.Location = new System.Drawing.Point(4, 40);
            this.tabCutscene.Name = "tabCutscene";
            this.tabCutscene.Size = new System.Drawing.Size(411, 676);
            this.tabCutscene.TabIndex = 9;
            this.tabCutscene.Text = "Cutscene";
            this.tabCutscene.UseVisualStyleBackColor = true;
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.Location = new System.Drawing.Point(172, 584);
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.ReadOnly = true;
            this.DebugTextBox.Size = new System.Drawing.Size(223, 86);
            this.DebugTextBox.TabIndex = 73;
            this.DebugTextBox.Text = "";
            this.DebugTextBox.Visible = false;
            // 
            // CutsceneTableEntryLabel
            // 
            this.CutsceneTableEntryLabel.AutoSize = true;
            this.CutsceneTableEntryLabel.Location = new System.Drawing.Point(0, 584);
            this.CutsceneTableEntryLabel.Name = "CutsceneTableEntryLabel";
            this.CutsceneTableEntryLabel.Size = new System.Drawing.Size(77, 13);
            this.CutsceneTableEntryLabel.TabIndex = 71;
            this.CutsceneTableEntryLabel.Text = "Table Entry (*):";
            this.EnvironmentControlTooltip.SetToolTip(this.CutsceneTableEntryLabel, "If -1, cutscene table is not updated, otherwise edits the respective row number i" +
        "n the table");
            // 
            // CutsceneFlagLabel
            // 
            this.CutsceneFlagLabel.AutoSize = true;
            this.CutsceneFlagLabel.Location = new System.Drawing.Point(300, 559);
            this.CutsceneFlagLabel.Name = "CutsceneFlagLabel";
            this.CutsceneFlagLabel.Size = new System.Drawing.Size(30, 13);
            this.CutsceneFlagLabel.TabIndex = 69;
            this.CutsceneFlagLabel.Text = "Flag:";
            // 
            // CutsceneSpawnLabel
            // 
            this.CutsceneSpawnLabel.AutoSize = true;
            this.CutsceneSpawnLabel.Location = new System.Drawing.Point(154, 559);
            this.CutsceneSpawnLabel.Name = "CutsceneSpawnLabel";
            this.CutsceneSpawnLabel.Size = new System.Drawing.Size(68, 13);
            this.CutsceneSpawnLabel.TabIndex = 67;
            this.CutsceneSpawnLabel.Text = "Spawn Num:";
            this.CutsceneSpawnLabel.Visible = false;
            // 
            // CutsceneEntranceLabel
            // 
            this.CutsceneEntranceLabel.AutoSize = true;
            this.CutsceneEntranceLabel.Location = new System.Drawing.Point(18, 559);
            this.CutsceneEntranceLabel.Name = "CutsceneEntranceLabel";
            this.CutsceneEntranceLabel.Size = new System.Drawing.Size(53, 13);
            this.CutsceneEntranceLabel.TabIndex = 65;
            this.CutsceneEntranceLabel.Text = "Entrance:";
            // 
            // CutsceneGroupBox
            // 
            this.CutsceneGroupBox.Controls.Add(this.MarkerEndFrame);
            this.CutsceneGroupBox.Controls.Add(this.CutsceneTabs);
            this.CutsceneGroupBox.Controls.Add(this.label51);
            this.CutsceneGroupBox.Controls.Add(this.MarkerTypeLabel);
            this.CutsceneGroupBox.Controls.Add(this.MarkerType);
            this.CutsceneGroupBox.Controls.Add(this.MarkerDown);
            this.CutsceneGroupBox.Controls.Add(this.MarkerUp);
            this.CutsceneGroupBox.Controls.Add(this.DeleteMarker);
            this.CutsceneGroupBox.Controls.Add(this.AddMarker);
            this.CutsceneGroupBox.Controls.Add(this.MarkerSelect);
            this.CutsceneGroupBox.Controls.Add(this.label54);
            this.CutsceneGroupBox.Controls.Add(this.MarkerStartFrame);
            this.CutsceneGroupBox.Location = new System.Drawing.Point(3, 3);
            this.CutsceneGroupBox.MaximumSize = new System.Drawing.Size(403, 999);
            this.CutsceneGroupBox.MinimumSize = new System.Drawing.Size(403, 462);
            this.CutsceneGroupBox.Name = "CutsceneGroupBox";
            this.CutsceneGroupBox.Size = new System.Drawing.Size(403, 547);
            this.CutsceneGroupBox.TabIndex = 0;
            this.CutsceneGroupBox.TabStop = false;
            this.CutsceneGroupBox.Text = "Entrance Cutscene";
            this.CutsceneGroupBox.Enter += new System.EventHandler(this.CutsceneGroupBox_Enter);
            // 
            // MarkerEndFrame
            // 
            this.MarkerEndFrame.AllowHex = false;
            this.MarkerEndFrame.Digits = 5;
            this.MarkerEndFrame.Enabled = false;
            this.MarkerEndFrame.Location = new System.Drawing.Point(355, 201);
            this.MarkerEndFrame.MaxLength = 255;
            this.MarkerEndFrame.Name = "MarkerEndFrame";
            this.MarkerEndFrame.Size = new System.Drawing.Size(37, 20);
            this.MarkerEndFrame.TabIndex = 64;
            this.MarkerEndFrame.Text = "0";
            this.MarkerEndFrame.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MarkerEndFrame_KeyDown);
            this.MarkerEndFrame.Leave += new System.EventHandler(this.MarkerEndFrame_Leave);
            // 
            // CutsceneTabs
            // 
            this.CutsceneTabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.CutsceneTabs.Controls.Add(this.CameraPositions);
            this.CutsceneTabs.Controls.Add(this.SpecialExecution);
            this.CutsceneTabs.Controls.Add(this.Unknown);
            this.CutsceneTabs.Controls.Add(this.Textbox);
            this.CutsceneTabs.Controls.Add(this.TransitionEffect);
            this.CutsceneTabs.Controls.Add(this.AsmExecution);
            this.CutsceneTabs.Controls.Add(this.ActorCommand);
            this.CutsceneTabs.ItemSize = new System.Drawing.Size(96, 7);
            this.CutsceneTabs.Location = new System.Drawing.Point(6, 229);
            this.CutsceneTabs.Multiline = true;
            this.CutsceneTabs.Name = "CutsceneTabs";
            this.CutsceneTabs.Padding = new System.Drawing.Point(0, 0);
            this.CutsceneTabs.SelectedIndex = 0;
            this.CutsceneTabs.Size = new System.Drawing.Size(391, 312);
            this.CutsceneTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.CutsceneTabs.TabIndex = 54;
            // 
            // CameraPositions
            // 
            this.CameraPositions.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CameraPositions.Controls.Add(this.CutscenePositionPlayMode);
            this.CameraPositions.Controls.Add(this.CutscenePositionDown);
            this.CameraPositions.Controls.Add(this.CutscenePositionUp);
            this.CameraPositions.Controls.Add(this.CutscenePositionCopyCamera);
            this.CameraPositions.Controls.Add(this.CutscenePositionViewMode);
            this.CameraPositions.Controls.Add(this.CutsceneAbsolutePositionAngleView);
            this.CameraPositions.Controls.Add(this.CutscenePositionFrameDuration);
            this.CameraPositions.Controls.Add(this.CutsceneAbsolutePositionCameraRoll);
            this.CameraPositions.Controls.Add(this.label60);
            this.CameraPositions.Controls.Add(this.CutscenePositionXFocusLabel);
            this.CameraPositions.Controls.Add(this.CutscenePositionZFocus);
            this.CameraPositions.Controls.Add(this.CutscenePositionZFocusLabel);
            this.CameraPositions.Controls.Add(this.CutscenePositionYFocusLabel);
            this.CameraPositions.Controls.Add(this.CutscenePositionXFocus);
            this.CameraPositions.Controls.Add(this.CutscenePositionYFocus);
            this.CameraPositions.Controls.Add(this.CutsceneDeleteAbsolutePosition);
            this.CameraPositions.Controls.Add(this.CutsceneAddAbsolutePosition);
            this.CameraPositions.Controls.Add(this.CutsceneAbsolutePositionListBox);
            this.CameraPositions.Controls.Add(this.label56);
            this.CameraPositions.Controls.Add(this.CutsceneAbsolutePositionZ);
            this.CameraPositions.Controls.Add(this.label57);
            this.CameraPositions.Controls.Add(this.label58);
            this.CameraPositions.Controls.Add(this.CutsceneAbsolutePositionX);
            this.CameraPositions.Controls.Add(this.CutsceneAbsolutePositionY);
            this.CameraPositions.Controls.Add(this.label55);
            this.CameraPositions.Controls.Add(this.label53);
            this.CameraPositions.Location = new System.Drawing.Point(4, 31);
            this.CameraPositions.Name = "CameraPositions";
            this.CameraPositions.Padding = new System.Windows.Forms.Padding(3);
            this.CameraPositions.Size = new System.Drawing.Size(383, 277);
            this.CameraPositions.TabIndex = 0;
            this.CameraPositions.Text = "Camera Positions";
            // 
            // CutscenePositionPlayMode
            // 
            this.CutscenePositionPlayMode.Enabled = false;
            this.CutscenePositionPlayMode.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CutscenePositionPlayMode.Location = new System.Drawing.Point(296, 227);
            this.CutscenePositionPlayMode.Name = "CutscenePositionPlayMode";
            this.CutscenePositionPlayMode.Size = new System.Drawing.Size(68, 28);
            this.CutscenePositionPlayMode.TabIndex = 74;
            this.CutscenePositionPlayMode.Text = "► Play";
            this.CutscenePositionPlayMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CutscenePositionPlayMode.UseVisualStyleBackColor = true;
            this.CutscenePositionPlayMode.Click += new System.EventHandler(this.CutscenePositionPlayMode_Click);
            // 
            // CutscenePositionDown
            // 
            this.CutscenePositionDown.Location = new System.Drawing.Point(175, 32);
            this.CutscenePositionDown.Name = "CutscenePositionDown";
            this.CutscenePositionDown.Size = new System.Drawing.Size(26, 23);
            this.CutscenePositionDown.TabIndex = 73;
            this.CutscenePositionDown.Text = "▼";
            this.CutscenePositionDown.UseVisualStyleBackColor = true;
            this.CutscenePositionDown.Click += new System.EventHandler(this.CutscenePositionDown_Click);
            // 
            // CutscenePositionUp
            // 
            this.CutscenePositionUp.Location = new System.Drawing.Point(175, 6);
            this.CutscenePositionUp.Name = "CutscenePositionUp";
            this.CutscenePositionUp.Size = new System.Drawing.Size(26, 23);
            this.CutscenePositionUp.TabIndex = 72;
            this.CutscenePositionUp.Text = "▲";
            this.CutscenePositionUp.UseVisualStyleBackColor = true;
            this.CutscenePositionUp.Click += new System.EventHandler(this.CutscenePositionUp_Click);
            // 
            // CutscenePositionCopyCamera
            // 
            this.CutscenePositionCopyCamera.Location = new System.Drawing.Point(222, 198);
            this.CutscenePositionCopyCamera.Name = "CutscenePositionCopyCamera";
            this.CutscenePositionCopyCamera.Size = new System.Drawing.Size(133, 23);
            this.CutscenePositionCopyCamera.TabIndex = 71;
            this.CutscenePositionCopyCamera.Text = "Copy viewport position";
            this.CutscenePositionCopyCamera.UseVisualStyleBackColor = true;
            this.CutscenePositionCopyCamera.Click += new System.EventHandler(this.CutscenePositionCopyCamera_Click);
            // 
            // CutscenePositionViewMode
            // 
            this.CutscenePositionViewMode.Enabled = false;
            this.CutscenePositionViewMode.Location = new System.Drawing.Point(222, 227);
            this.CutscenePositionViewMode.Name = "CutscenePositionViewMode";
            this.CutscenePositionViewMode.Size = new System.Drawing.Size(68, 28);
            this.CutscenePositionViewMode.TabIndex = 70;
            this.CutscenePositionViewMode.Text = "View";
            this.CutscenePositionViewMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CutscenePositionViewMode.UseVisualStyleBackColor = true;
            this.CutscenePositionViewMode.Click += new System.EventHandler(this.CutscenePositionViewMode_Click);
            // 
            // CutsceneAbsolutePositionAngleView
            // 
            this.CutsceneAbsolutePositionAngleView.AlwaysFireValueChanged = false;
            this.CutsceneAbsolutePositionAngleView.DecimalPlaces = 7;
            this.CutsceneAbsolutePositionAngleView.DisplayDigits = 1;
            this.CutsceneAbsolutePositionAngleView.DoValueRollover = false;
            this.CutsceneAbsolutePositionAngleView.Enabled = false;
            this.CutsceneAbsolutePositionAngleView.IncrementMouseWheel = 1;
            this.CutsceneAbsolutePositionAngleView.Location = new System.Drawing.Point(98, 250);
            this.CutsceneAbsolutePositionAngleView.Maximum = new decimal(new int[] {
            179,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionAngleView.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionAngleView.Name = "CutsceneAbsolutePositionAngleView";
            this.CutsceneAbsolutePositionAngleView.ShiftMultiplier = 10;
            this.CutsceneAbsolutePositionAngleView.Size = new System.Drawing.Size(84, 20);
            this.CutsceneAbsolutePositionAngleView.TabIndex = 69;
            this.CutsceneAbsolutePositionAngleView.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionAngleView.ValueChanged += new System.EventHandler(this.CutsceneAbsolutePositionAngleView_ValueChanged);
            // 
            // CutscenePositionFrameDuration
            // 
            this.CutscenePositionFrameDuration.AlwaysFireValueChanged = false;
            this.CutscenePositionFrameDuration.DisplayDigits = 1;
            this.CutscenePositionFrameDuration.DoValueRollover = false;
            this.CutscenePositionFrameDuration.Enabled = false;
            this.CutscenePositionFrameDuration.IncrementMouseWheel = 1;
            this.CutscenePositionFrameDuration.Location = new System.Drawing.Point(98, 198);
            this.CutscenePositionFrameDuration.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CutscenePositionFrameDuration.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutscenePositionFrameDuration.Name = "CutscenePositionFrameDuration";
            this.CutscenePositionFrameDuration.ShiftMultiplier = 20;
            this.CutscenePositionFrameDuration.Size = new System.Drawing.Size(84, 20);
            this.CutscenePositionFrameDuration.TabIndex = 67;
            this.CutscenePositionFrameDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutscenePositionFrameDuration.ValueChanged += new System.EventHandler(this.CutscenePositionFrameDuration_Leave);
            // 
            // CutsceneAbsolutePositionCameraRoll
            // 
            this.CutsceneAbsolutePositionCameraRoll.AlwaysFireValueChanged = false;
            this.CutsceneAbsolutePositionCameraRoll.DisplayDigits = 1;
            this.CutsceneAbsolutePositionCameraRoll.DoValueRollover = false;
            this.CutsceneAbsolutePositionCameraRoll.Enabled = false;
            this.CutsceneAbsolutePositionCameraRoll.IncrementMouseWheel = 1;
            this.CutsceneAbsolutePositionCameraRoll.Location = new System.Drawing.Point(98, 224);
            this.CutsceneAbsolutePositionCameraRoll.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionCameraRoll.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.CutsceneAbsolutePositionCameraRoll.Name = "CutsceneAbsolutePositionCameraRoll";
            this.CutsceneAbsolutePositionCameraRoll.ShiftMultiplier = 20;
            this.CutsceneAbsolutePositionCameraRoll.Size = new System.Drawing.Size(84, 20);
            this.CutsceneAbsolutePositionCameraRoll.TabIndex = 68;
            this.CutsceneAbsolutePositionCameraRoll.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionCameraRoll.ValueChanged += new System.EventHandler(this.CutsceneAbsolutePositionCameraRoll_ValueChanged);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(7, 200);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(85, 13);
            this.label60.TabIndex = 66;
            this.label60.Text = "Frame Duration: ";
            // 
            // CutscenePositionXFocusLabel
            // 
            this.CutscenePositionXFocusLabel.AutoSize = true;
            this.CutscenePositionXFocusLabel.Enabled = false;
            this.CutscenePositionXFocusLabel.Location = new System.Drawing.Point(225, 121);
            this.CutscenePositionXFocusLabel.Name = "CutscenePositionXFocusLabel";
            this.CutscenePositionXFocusLabel.Size = new System.Drawing.Size(49, 13);
            this.CutscenePositionXFocusLabel.TabIndex = 57;
            this.CutscenePositionXFocusLabel.Text = "X Focus:";
            // 
            // CutscenePositionZFocus
            // 
            this.CutscenePositionZFocus.AlwaysFireValueChanged = false;
            this.CutscenePositionZFocus.DisplayDigits = 1;
            this.CutscenePositionZFocus.DoValueRollover = false;
            this.CutscenePositionZFocus.Enabled = false;
            this.CutscenePositionZFocus.IncrementMouseWheel = 1;
            this.CutscenePositionZFocus.Location = new System.Drawing.Point(289, 171);
            this.CutscenePositionZFocus.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutscenePositionZFocus.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CutscenePositionZFocus.Name = "CutscenePositionZFocus";
            this.CutscenePositionZFocus.ShiftMultiplier = 20;
            this.CutscenePositionZFocus.Size = new System.Drawing.Size(84, 20);
            this.CutscenePositionZFocus.TabIndex = 62;
            this.CutscenePositionZFocus.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutscenePositionZFocus.ValueChanged += new System.EventHandler(this.CutscenePositionZFocus_ValueChanged);
            // 
            // CutscenePositionZFocusLabel
            // 
            this.CutscenePositionZFocusLabel.AutoSize = true;
            this.CutscenePositionZFocusLabel.Enabled = false;
            this.CutscenePositionZFocusLabel.Location = new System.Drawing.Point(227, 173);
            this.CutscenePositionZFocusLabel.Name = "CutscenePositionZFocusLabel";
            this.CutscenePositionZFocusLabel.Size = new System.Drawing.Size(52, 13);
            this.CutscenePositionZFocusLabel.TabIndex = 60;
            this.CutscenePositionZFocusLabel.Text = "Z Focus: ";
            // 
            // CutscenePositionYFocusLabel
            // 
            this.CutscenePositionYFocusLabel.AutoSize = true;
            this.CutscenePositionYFocusLabel.Enabled = false;
            this.CutscenePositionYFocusLabel.Location = new System.Drawing.Point(225, 147);
            this.CutscenePositionYFocusLabel.Name = "CutscenePositionYFocusLabel";
            this.CutscenePositionYFocusLabel.Size = new System.Drawing.Size(52, 13);
            this.CutscenePositionYFocusLabel.TabIndex = 58;
            this.CutscenePositionYFocusLabel.Text = "Y Focus: ";
            // 
            // CutscenePositionXFocus
            // 
            this.CutscenePositionXFocus.AlwaysFireValueChanged = false;
            this.CutscenePositionXFocus.DisplayDigits = 1;
            this.CutscenePositionXFocus.DoValueRollover = false;
            this.CutscenePositionXFocus.Enabled = false;
            this.CutscenePositionXFocus.IncrementMouseWheel = 1;
            this.CutscenePositionXFocus.Location = new System.Drawing.Point(289, 119);
            this.CutscenePositionXFocus.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutscenePositionXFocus.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CutscenePositionXFocus.Name = "CutscenePositionXFocus";
            this.CutscenePositionXFocus.ShiftMultiplier = 20;
            this.CutscenePositionXFocus.Size = new System.Drawing.Size(84, 20);
            this.CutscenePositionXFocus.TabIndex = 59;
            this.CutscenePositionXFocus.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutscenePositionXFocus.ValueChanged += new System.EventHandler(this.CutscenePositionXFocus_ValueChanged);
            // 
            // CutscenePositionYFocus
            // 
            this.CutscenePositionYFocus.AlwaysFireValueChanged = false;
            this.CutscenePositionYFocus.DisplayDigits = 1;
            this.CutscenePositionYFocus.DoValueRollover = false;
            this.CutscenePositionYFocus.Enabled = false;
            this.CutscenePositionYFocus.IncrementMouseWheel = 1;
            this.CutscenePositionYFocus.Location = new System.Drawing.Point(289, 145);
            this.CutscenePositionYFocus.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutscenePositionYFocus.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CutscenePositionYFocus.Name = "CutscenePositionYFocus";
            this.CutscenePositionYFocus.ShiftMultiplier = 20;
            this.CutscenePositionYFocus.Size = new System.Drawing.Size(84, 20);
            this.CutscenePositionYFocus.TabIndex = 61;
            this.CutscenePositionYFocus.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutscenePositionYFocus.ValueChanged += new System.EventHandler(this.CutscenePositionYFocus_ValueChanged);
            // 
            // CutsceneDeleteAbsolutePosition
            // 
            this.CutsceneDeleteAbsolutePosition.Location = new System.Drawing.Point(291, 6);
            this.CutsceneDeleteAbsolutePosition.Name = "CutsceneDeleteAbsolutePosition";
            this.CutsceneDeleteAbsolutePosition.Size = new System.Drawing.Size(86, 23);
            this.CutsceneDeleteAbsolutePosition.TabIndex = 56;
            this.CutsceneDeleteAbsolutePosition.Text = "Delete Position";
            this.CutsceneDeleteAbsolutePosition.UseVisualStyleBackColor = true;
            this.CutsceneDeleteAbsolutePosition.Click += new System.EventHandler(this.CutsceneDeleteAbsolutePosition_Click);
            // 
            // CutsceneAddAbsolutePosition
            // 
            this.CutsceneAddAbsolutePosition.Location = new System.Drawing.Point(203, 6);
            this.CutsceneAddAbsolutePosition.Name = "CutsceneAddAbsolutePosition";
            this.CutsceneAddAbsolutePosition.Size = new System.Drawing.Size(82, 23);
            this.CutsceneAddAbsolutePosition.TabIndex = 55;
            this.CutsceneAddAbsolutePosition.Text = "Add Position";
            this.EnvironmentControlTooltip.SetToolTip(this.CutsceneAddAbsolutePosition, "Hold SHIFT to add in front of camera");
            this.CutsceneAddAbsolutePosition.UseVisualStyleBackColor = true;
            this.CutsceneAddAbsolutePosition.Click += new System.EventHandler(this.CutsceneAddAbsolutePosition_Click);
            // 
            // CutsceneAbsolutePositionListBox
            // 
            this.CutsceneAbsolutePositionListBox.FormattingEnabled = true;
            this.CutsceneAbsolutePositionListBox.IntegralHeight = false;
            this.CutsceneAbsolutePositionListBox.Location = new System.Drawing.Point(9, 6);
            this.CutsceneAbsolutePositionListBox.Name = "CutsceneAbsolutePositionListBox";
            this.CutsceneAbsolutePositionListBox.Size = new System.Drawing.Size(160, 180);
            this.CutsceneAbsolutePositionListBox.TabIndex = 54;
            this.CutsceneAbsolutePositionListBox.Click += new System.EventHandler(this.CutsceneAbsolutePositionListBox_Click);
            this.CutsceneAbsolutePositionListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CutsceneAbsolutePositionListBox_KeyDown);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Enabled = false;
            this.label56.Location = new System.Drawing.Point(217, 42);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(57, 13);
            this.label56.TabIndex = 48;
            this.label56.Text = "X Position:";
            // 
            // CutsceneAbsolutePositionZ
            // 
            this.CutsceneAbsolutePositionZ.AlwaysFireValueChanged = false;
            this.CutsceneAbsolutePositionZ.DisplayDigits = 1;
            this.CutsceneAbsolutePositionZ.DoValueRollover = false;
            this.CutsceneAbsolutePositionZ.Enabled = false;
            this.CutsceneAbsolutePositionZ.IncrementMouseWheel = 1;
            this.CutsceneAbsolutePositionZ.Location = new System.Drawing.Point(289, 92);
            this.CutsceneAbsolutePositionZ.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionZ.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CutsceneAbsolutePositionZ.Name = "CutsceneAbsolutePositionZ";
            this.CutsceneAbsolutePositionZ.ShiftMultiplier = 20;
            this.CutsceneAbsolutePositionZ.Size = new System.Drawing.Size(84, 20);
            this.CutsceneAbsolutePositionZ.TabIndex = 53;
            this.CutsceneAbsolutePositionZ.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionZ.ValueChanged += new System.EventHandler(this.CutsceneAbsolutePositionZ_ValueChanged);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Enabled = false;
            this.label57.Location = new System.Drawing.Point(219, 94);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(57, 13);
            this.label57.TabIndex = 51;
            this.label57.Text = "Z Position:";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Enabled = false;
            this.label58.Location = new System.Drawing.Point(217, 68);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(57, 13);
            this.label58.TabIndex = 49;
            this.label58.Text = "Y Position:";
            // 
            // CutsceneAbsolutePositionX
            // 
            this.CutsceneAbsolutePositionX.AlwaysFireValueChanged = false;
            this.CutsceneAbsolutePositionX.DisplayDigits = 1;
            this.CutsceneAbsolutePositionX.DoValueRollover = false;
            this.CutsceneAbsolutePositionX.Enabled = false;
            this.CutsceneAbsolutePositionX.IncrementMouseWheel = 1;
            this.CutsceneAbsolutePositionX.Location = new System.Drawing.Point(289, 40);
            this.CutsceneAbsolutePositionX.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionX.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CutsceneAbsolutePositionX.Name = "CutsceneAbsolutePositionX";
            this.CutsceneAbsolutePositionX.ShiftMultiplier = 20;
            this.CutsceneAbsolutePositionX.Size = new System.Drawing.Size(84, 20);
            this.CutsceneAbsolutePositionX.TabIndex = 50;
            this.CutsceneAbsolutePositionX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionX.ValueChanged += new System.EventHandler(this.CutsceneAbsolutePositionX_ChangeValue);
            // 
            // CutsceneAbsolutePositionY
            // 
            this.CutsceneAbsolutePositionY.AlwaysFireValueChanged = false;
            this.CutsceneAbsolutePositionY.DisplayDigits = 1;
            this.CutsceneAbsolutePositionY.DoValueRollover = false;
            this.CutsceneAbsolutePositionY.Enabled = false;
            this.CutsceneAbsolutePositionY.IncrementMouseWheel = 1;
            this.CutsceneAbsolutePositionY.Location = new System.Drawing.Point(289, 66);
            this.CutsceneAbsolutePositionY.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionY.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.CutsceneAbsolutePositionY.Name = "CutsceneAbsolutePositionY";
            this.CutsceneAbsolutePositionY.ShiftMultiplier = 20;
            this.CutsceneAbsolutePositionY.Size = new System.Drawing.Size(84, 20);
            this.CutsceneAbsolutePositionY.TabIndex = 52;
            this.CutsceneAbsolutePositionY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneAbsolutePositionY.ValueChanged += new System.EventHandler(this.CutsceneAbsolutePositionY_ValueChanged);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(15, 226);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(67, 13);
            this.label55.TabIndex = 47;
            this.label55.Text = "Camera Roll:";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(43, 250);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(31, 13);
            this.label53.TabIndex = 43;
            this.label53.Text = "FOV:";
            // 
            // SpecialExecution
            // 
            this.SpecialExecution.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SpecialExecution.Controls.Add(this.label83);
            this.SpecialExecution.Controls.Add(this.label84);
            this.SpecialExecution.Controls.Add(this.CutsceneSetTimeHours);
            this.SpecialExecution.Controls.Add(this.CutsceneSetTimeMinutes);
            this.SpecialExecution.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SpecialExecution.Location = new System.Drawing.Point(4, 31);
            this.SpecialExecution.Name = "SpecialExecution";
            this.SpecialExecution.Size = new System.Drawing.Size(383, 277);
            this.SpecialExecution.TabIndex = 2;
            this.SpecialExecution.Text = "SetTime";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(14, 14);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(38, 13);
            this.label83.TabIndex = 62;
            this.label83.Text = "Hours:";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(14, 40);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(47, 13);
            this.label84.TabIndex = 63;
            this.label84.Text = "Minutes:";
            // 
            // CutsceneSetTimeHours
            // 
            this.CutsceneSetTimeHours.AlwaysFireValueChanged = false;
            this.CutsceneSetTimeHours.DisplayDigits = 1;
            this.CutsceneSetTimeHours.DoValueRollover = false;
            this.CutsceneSetTimeHours.IncrementMouseWheel = 1;
            this.CutsceneSetTimeHours.Location = new System.Drawing.Point(78, 12);
            this.CutsceneSetTimeHours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.CutsceneSetTimeHours.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneSetTimeHours.Name = "CutsceneSetTimeHours";
            this.CutsceneSetTimeHours.ShiftMultiplier = 1;
            this.CutsceneSetTimeHours.Size = new System.Drawing.Size(84, 20);
            this.CutsceneSetTimeHours.TabIndex = 64;
            this.CutsceneSetTimeHours.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneSetTimeHours.ValueChanged += new System.EventHandler(this.CutsceneSetTimeHours_ValueChanged);
            // 
            // CutsceneSetTimeMinutes
            // 
            this.CutsceneSetTimeMinutes.AlwaysFireValueChanged = false;
            this.CutsceneSetTimeMinutes.DisplayDigits = 1;
            this.CutsceneSetTimeMinutes.DoValueRollover = false;
            this.CutsceneSetTimeMinutes.IncrementMouseWheel = 1;
            this.CutsceneSetTimeMinutes.Location = new System.Drawing.Point(78, 38);
            this.CutsceneSetTimeMinutes.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.CutsceneSetTimeMinutes.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneSetTimeMinutes.Name = "CutsceneSetTimeMinutes";
            this.CutsceneSetTimeMinutes.ShiftMultiplier = 1;
            this.CutsceneSetTimeMinutes.Size = new System.Drawing.Size(84, 20);
            this.CutsceneSetTimeMinutes.TabIndex = 65;
            this.CutsceneSetTimeMinutes.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneSetTimeMinutes.ValueChanged += new System.EventHandler(this.CutsceneSetTimeMinutes_ValueChanged);
            // 
            // Unknown
            // 
            this.Unknown.Location = new System.Drawing.Point(4, 31);
            this.Unknown.Name = "Unknown";
            this.Unknown.Size = new System.Drawing.Size(383, 277);
            this.Unknown.TabIndex = 3;
            this.Unknown.Text = "Unknown";
            this.Unknown.UseVisualStyleBackColor = true;
            // 
            // Textbox
            // 
            this.Textbox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox.Controls.Add(this.CutsceneTextboxDown);
            this.Textbox.Controls.Add(this.CutsceneTextboxUp);
            this.Textbox.Controls.Add(this.label63);
            this.Textbox.Controls.Add(this.CutsceneTextboxFramesLabel);
            this.Textbox.Controls.Add(this.label61);
            this.Textbox.Controls.Add(this.CutsceneTextboxType);
            this.Textbox.Controls.Add(this.CutsceneTextboxMessageIdLabel);
            this.Textbox.Controls.Add(this.CutsceneDeleteTextbox);
            this.Textbox.Controls.Add(this.CutsceneAddTextbox);
            this.Textbox.Controls.Add(this.CutsceneTextboxBottomMessageID);
            this.Textbox.Controls.Add(this.CutsceneTextboxFrames);
            this.Textbox.Controls.Add(this.CutsceneTextboxTopMessageID);
            this.Textbox.Controls.Add(this.CutsceneTextboxMessageId);
            this.Textbox.Controls.Add(this.CutsceneTextboxList);
            this.Textbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Textbox.Location = new System.Drawing.Point(4, 31);
            this.Textbox.Name = "Textbox";
            this.Textbox.Size = new System.Drawing.Size(383, 277);
            this.Textbox.TabIndex = 4;
            this.Textbox.Text = "Textbox";
            // 
            // CutsceneTextboxDown
            // 
            this.CutsceneTextboxDown.Location = new System.Drawing.Point(171, 32);
            this.CutsceneTextboxDown.Name = "CutsceneTextboxDown";
            this.CutsceneTextboxDown.Size = new System.Drawing.Size(26, 23);
            this.CutsceneTextboxDown.TabIndex = 95;
            this.CutsceneTextboxDown.Text = "▼";
            this.CutsceneTextboxDown.UseVisualStyleBackColor = true;
            this.CutsceneTextboxDown.Click += new System.EventHandler(this.CutsceneTextboxDown_Click);
            // 
            // CutsceneTextboxUp
            // 
            this.CutsceneTextboxUp.Location = new System.Drawing.Point(171, 6);
            this.CutsceneTextboxUp.Name = "CutsceneTextboxUp";
            this.CutsceneTextboxUp.Size = new System.Drawing.Size(26, 23);
            this.CutsceneTextboxUp.TabIndex = 94;
            this.CutsceneTextboxUp.Text = "▲";
            this.CutsceneTextboxUp.UseVisualStyleBackColor = true;
            this.CutsceneTextboxUp.Click += new System.EventHandler(this.CutsceneTextboxUp_Click);
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(200, 124);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(77, 13);
            this.label63.TabIndex = 71;
            this.label63.Text = "Bottom Option:";
            // 
            // CutsceneTextboxFramesLabel
            // 
            this.CutsceneTextboxFramesLabel.AutoSize = true;
            this.CutsceneTextboxFramesLabel.Enabled = false;
            this.CutsceneTextboxFramesLabel.Location = new System.Drawing.Point(6, 242);
            this.CutsceneTextboxFramesLabel.Name = "CutsceneTextboxFramesLabel";
            this.CutsceneTextboxFramesLabel.Size = new System.Drawing.Size(80, 13);
            this.CutsceneTextboxFramesLabel.TabIndex = 68;
            this.CutsceneTextboxFramesLabel.Text = "Frame duration:";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(214, 96);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(63, 13);
            this.label61.TabIndex = 67;
            this.label61.Text = "Top Option:";
            // 
            // CutsceneTextboxType
            // 
            this.CutsceneTextboxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CutsceneTextboxType.FormattingEnabled = true;
            this.CutsceneTextboxType.Location = new System.Drawing.Point(203, 35);
            this.CutsceneTextboxType.Name = "CutsceneTextboxType";
            this.CutsceneTextboxType.Size = new System.Drawing.Size(176, 21);
            this.CutsceneTextboxType.TabIndex = 65;
            this.CutsceneTextboxType.Tag = "";
            this.CutsceneTextboxType.SelectedValueChanged += new System.EventHandler(this.CutsceneTextboxType_SelectedValueChanged);
            // 
            // CutsceneTextboxMessageIdLabel
            // 
            this.CutsceneTextboxMessageIdLabel.AutoSize = true;
            this.CutsceneTextboxMessageIdLabel.Location = new System.Drawing.Point(210, 68);
            this.CutsceneTextboxMessageIdLabel.Name = "CutsceneTextboxMessageIdLabel";
            this.CutsceneTextboxMessageIdLabel.Size = new System.Drawing.Size(67, 13);
            this.CutsceneTextboxMessageIdLabel.TabIndex = 61;
            this.CutsceneTextboxMessageIdLabel.Text = "Message ID:";
            // 
            // CutsceneDeleteTextbox
            // 
            this.CutsceneDeleteTextbox.Location = new System.Drawing.Point(291, 6);
            this.CutsceneDeleteTextbox.Name = "CutsceneDeleteTextbox";
            this.CutsceneDeleteTextbox.Size = new System.Drawing.Size(88, 23);
            this.CutsceneDeleteTextbox.TabIndex = 59;
            this.CutsceneDeleteTextbox.Text = "Delete Textbox";
            this.CutsceneDeleteTextbox.UseVisualStyleBackColor = true;
            this.CutsceneDeleteTextbox.Click += new System.EventHandler(this.CutsceneDeleteTextbox_Click);
            // 
            // CutsceneAddTextbox
            // 
            this.CutsceneAddTextbox.Location = new System.Drawing.Point(203, 6);
            this.CutsceneAddTextbox.Name = "CutsceneAddTextbox";
            this.CutsceneAddTextbox.Size = new System.Drawing.Size(82, 23);
            this.CutsceneAddTextbox.TabIndex = 58;
            this.CutsceneAddTextbox.Text = "Add Textbox";
            this.CutsceneAddTextbox.UseVisualStyleBackColor = true;
            this.CutsceneAddTextbox.Click += new System.EventHandler(this.CutsceneAddTextbox_Click);
            // 
            // CutsceneTextboxBottomMessageID
            // 
            this.CutsceneTextboxBottomMessageID.AllowHex = true;
            this.CutsceneTextboxBottomMessageID.Digits = 4;
            this.CutsceneTextboxBottomMessageID.Location = new System.Drawing.Point(283, 121);
            this.CutsceneTextboxBottomMessageID.MaxLength = 255;
            this.CutsceneTextboxBottomMessageID.Name = "CutsceneTextboxBottomMessageID";
            this.CutsceneTextboxBottomMessageID.Size = new System.Drawing.Size(91, 20);
            this.CutsceneTextboxBottomMessageID.TabIndex = 70;
            this.CutsceneTextboxBottomMessageID.Text = "00";
            this.CutsceneTextboxBottomMessageID.Leave += new System.EventHandler(this.CutsceneTextboxBottomMessageID_Leave);
            // 
            // CutsceneTextboxFrames
            // 
            this.CutsceneTextboxFrames.AlwaysFireValueChanged = false;
            this.CutsceneTextboxFrames.DisplayDigits = 1;
            this.CutsceneTextboxFrames.DoValueRollover = false;
            this.CutsceneTextboxFrames.Enabled = false;
            this.CutsceneTextboxFrames.IncrementMouseWheel = 1;
            this.CutsceneTextboxFrames.Location = new System.Drawing.Point(92, 240);
            this.CutsceneTextboxFrames.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CutsceneTextboxFrames.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneTextboxFrames.Name = "CutsceneTextboxFrames";
            this.CutsceneTextboxFrames.ShiftMultiplier = 1;
            this.CutsceneTextboxFrames.Size = new System.Drawing.Size(84, 20);
            this.CutsceneTextboxFrames.TabIndex = 69;
            this.CutsceneTextboxFrames.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneTextboxFrames.ValueChanged += new System.EventHandler(this.CutsceneTextboxFrames_ValueChanged);
            // 
            // CutsceneTextboxTopMessageID
            // 
            this.CutsceneTextboxTopMessageID.AllowHex = true;
            this.CutsceneTextboxTopMessageID.Digits = 4;
            this.CutsceneTextboxTopMessageID.Location = new System.Drawing.Point(283, 93);
            this.CutsceneTextboxTopMessageID.MaxLength = 255;
            this.CutsceneTextboxTopMessageID.Name = "CutsceneTextboxTopMessageID";
            this.CutsceneTextboxTopMessageID.Size = new System.Drawing.Size(91, 20);
            this.CutsceneTextboxTopMessageID.TabIndex = 66;
            this.CutsceneTextboxTopMessageID.Text = "00";
            this.CutsceneTextboxTopMessageID.Leave += new System.EventHandler(this.CutsceneTextboxTopMessageID_Leave);
            // 
            // CutsceneTextboxMessageId
            // 
            this.CutsceneTextboxMessageId.AllowHex = true;
            this.CutsceneTextboxMessageId.Digits = 4;
            this.CutsceneTextboxMessageId.Location = new System.Drawing.Point(283, 65);
            this.CutsceneTextboxMessageId.MaxLength = 255;
            this.CutsceneTextboxMessageId.Name = "CutsceneTextboxMessageId";
            this.CutsceneTextboxMessageId.Size = new System.Drawing.Size(91, 20);
            this.CutsceneTextboxMessageId.TabIndex = 60;
            this.CutsceneTextboxMessageId.Text = "00";
            this.CutsceneTextboxMessageId.Leave += new System.EventHandler(this.CutsceneTextboxMessageId_Leave);
            // 
            // CutsceneTextboxList
            // 
            this.CutsceneTextboxList.FormattingEnabled = true;
            this.CutsceneTextboxList.IntegralHeight = false;
            this.CutsceneTextboxList.Location = new System.Drawing.Point(9, 6);
            this.CutsceneTextboxList.Name = "CutsceneTextboxList";
            this.CutsceneTextboxList.Size = new System.Drawing.Size(155, 228);
            this.CutsceneTextboxList.TabIndex = 57;
            this.CutsceneTextboxList.Click += new System.EventHandler(this.CutsceneTextboxList_Click);
            // 
            // TransitionEffect
            // 
            this.TransitionEffect.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TransitionEffect.Controls.Add(this.label70);
            this.TransitionEffect.Controls.Add(this.CutsceneTransitionComboBox);
            this.TransitionEffect.Location = new System.Drawing.Point(4, 31);
            this.TransitionEffect.Name = "TransitionEffect";
            this.TransitionEffect.Size = new System.Drawing.Size(383, 277);
            this.TransitionEffect.TabIndex = 5;
            this.TransitionEffect.Text = "Transition Effect";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(3, 14);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(56, 13);
            this.label70.TabIndex = 71;
            this.label70.Text = "Transition:";
            // 
            // CutsceneTransitionComboBox
            // 
            this.CutsceneTransitionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CutsceneTransitionComboBox.FormattingEnabled = true;
            this.CutsceneTransitionComboBox.Location = new System.Drawing.Point(66, 11);
            this.CutsceneTransitionComboBox.Name = "CutsceneTransitionComboBox";
            this.CutsceneTransitionComboBox.Size = new System.Drawing.Size(304, 21);
            this.CutsceneTransitionComboBox.TabIndex = 70;
            this.CutsceneTransitionComboBox.Tag = "";
            this.CutsceneTransitionComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.CutsceneTransitionComboBox.SelectionChangeCommitted += new System.EventHandler(this.CutsceneTransitionComboBox_SelectionChangeCommitted);
            // 
            // AsmExecution
            // 
            this.AsmExecution.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.AsmExecution.Controls.Add(this.CutsceneAsmLabel);
            this.AsmExecution.Controls.Add(this.CutsceneAsmComboBox);
            this.AsmExecution.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AsmExecution.Location = new System.Drawing.Point(4, 31);
            this.AsmExecution.Name = "AsmExecution";
            this.AsmExecution.Size = new System.Drawing.Size(383, 277);
            this.AsmExecution.TabIndex = 6;
            this.AsmExecution.Text = "Asm Execution";
            // 
            // CutsceneAsmLabel
            // 
            this.CutsceneAsmLabel.AutoSize = true;
            this.CutsceneAsmLabel.Location = new System.Drawing.Point(3, 14);
            this.CutsceneAsmLabel.Name = "CutsceneAsmLabel";
            this.CutsceneAsmLabel.Size = new System.Drawing.Size(57, 13);
            this.CutsceneAsmLabel.TabIndex = 69;
            this.CutsceneAsmLabel.Text = "Command:";
            // 
            // CutsceneAsmComboBox
            // 
            this.CutsceneAsmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CutsceneAsmComboBox.FormattingEnabled = true;
            this.CutsceneAsmComboBox.Location = new System.Drawing.Point(66, 11);
            this.CutsceneAsmComboBox.Name = "CutsceneAsmComboBox";
            this.CutsceneAsmComboBox.Size = new System.Drawing.Size(304, 21);
            this.CutsceneAsmComboBox.TabIndex = 66;
            this.CutsceneAsmComboBox.Tag = "";
            this.CutsceneAsmComboBox.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.CutsceneAsmComboBox.SelectionChangeCommitted += new System.EventHandler(this.CutsceneAsmComboBox_SelectionChangeCommitted);
            // 
            // ActorCommand
            // 
            this.ActorCommand.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ActorCommand.Controls.Add(this.CutsceneActorDown);
            this.ActorCommand.Controls.Add(this.CutsceneActorUp);
            this.ActorCommand.Controls.Add(this.label80);
            this.ActorCommand.Controls.Add(this.label81);
            this.ActorCommand.Controls.Add(this.label82);
            this.ActorCommand.Controls.Add(this.CutsceneActorAnimation);
            this.ActorCommand.Controls.Add(this.CutsceneActorAnimLabel);
            this.ActorCommand.Controls.Add(this.label62);
            this.ActorCommand.Controls.Add(this.label64);
            this.ActorCommand.Controls.Add(this.label65);
            this.ActorCommand.Controls.Add(this.label66);
            this.ActorCommand.Controls.Add(this.label67);
            this.ActorCommand.Controls.Add(this.label68);
            this.ActorCommand.Controls.Add(this.label69);
            this.ActorCommand.Controls.Add(this.CutsceneActorDeleteAction);
            this.ActorCommand.Controls.Add(this.CutsceneActorAddAction);
            this.ActorCommand.Controls.Add(this.CutsceneActorFrameDuration);
            this.ActorCommand.Controls.Add(this.CutsceneActorZRot);
            this.ActorCommand.Controls.Add(this.CutsceneActorXRot);
            this.ActorCommand.Controls.Add(this.CutsceneActorYRot);
            this.ActorCommand.Controls.Add(this.CutsceneActorZEnd);
            this.ActorCommand.Controls.Add(this.CutsceneActorXEnd);
            this.ActorCommand.Controls.Add(this.CutsceneActorYEnd);
            this.ActorCommand.Controls.Add(this.CutsceneActorListBox);
            this.ActorCommand.Controls.Add(this.CutsceneActorZStart);
            this.ActorCommand.Controls.Add(this.CutsceneActorXStart);
            this.ActorCommand.Controls.Add(this.CutsceneActorYStart);
            this.ActorCommand.Location = new System.Drawing.Point(4, 31);
            this.ActorCommand.Name = "ActorCommand";
            this.ActorCommand.Size = new System.Drawing.Size(383, 277);
            this.ActorCommand.TabIndex = 7;
            this.ActorCommand.Text = "Actor Command";
            // 
            // CutsceneActorDown
            // 
            this.CutsceneActorDown.Location = new System.Drawing.Point(172, 32);
            this.CutsceneActorDown.Name = "CutsceneActorDown";
            this.CutsceneActorDown.Size = new System.Drawing.Size(26, 23);
            this.CutsceneActorDown.TabIndex = 93;
            this.CutsceneActorDown.Text = "▼";
            this.CutsceneActorDown.UseVisualStyleBackColor = true;
            this.CutsceneActorDown.Click += new System.EventHandler(this.CutsceneActorDown_Click);
            // 
            // CutsceneActorUp
            // 
            this.CutsceneActorUp.Location = new System.Drawing.Point(172, 6);
            this.CutsceneActorUp.Name = "CutsceneActorUp";
            this.CutsceneActorUp.Size = new System.Drawing.Size(26, 23);
            this.CutsceneActorUp.TabIndex = 92;
            this.CutsceneActorUp.Text = "▲";
            this.CutsceneActorUp.UseVisualStyleBackColor = true;
            this.CutsceneActorUp.Click += new System.EventHandler(this.CutsceneActorUp_Click);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Enabled = false;
            this.label80.Location = new System.Drawing.Point(227, 199);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(37, 13);
            this.label80.TabIndex = 88;
            this.label80.Text = "X Rot:";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Enabled = false;
            this.label81.Location = new System.Drawing.Point(229, 251);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(37, 13);
            this.label81.TabIndex = 90;
            this.label81.Text = "Z Rot:";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Enabled = false;
            this.label82.Location = new System.Drawing.Point(227, 225);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(37, 13);
            this.label82.TabIndex = 89;
            this.label82.Text = "Y Rot:";
            // 
            // CutsceneActorAnimation
            // 
            this.CutsceneActorAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CutsceneActorAnimation.FormattingEnabled = true;
            this.CutsceneActorAnimation.Location = new System.Drawing.Point(71, 239);
            this.CutsceneActorAnimation.Name = "CutsceneActorAnimation";
            this.CutsceneActorAnimation.Size = new System.Drawing.Size(143, 21);
            this.CutsceneActorAnimation.TabIndex = 65;
            this.CutsceneActorAnimation.Tag = "";
            this.CutsceneActorAnimation.DropDown += new System.EventHandler(this.AdjustWidthComboBox_DropDown);
            this.CutsceneActorAnimation.SelectionChangeCommitted += new System.EventHandler(this.CutsceneActorAnimation_SelectionChangeCommitted);
            // 
            // CutsceneActorAnimLabel
            // 
            this.CutsceneActorAnimLabel.AutoSize = true;
            this.CutsceneActorAnimLabel.Location = new System.Drawing.Point(6, 242);
            this.CutsceneActorAnimLabel.Name = "CutsceneActorAnimLabel";
            this.CutsceneActorAnimLabel.Size = new System.Drawing.Size(59, 13);
            this.CutsceneActorAnimLabel.TabIndex = 84;
            this.CutsceneActorAnimLabel.Text = "Animation: ";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(6, 211);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(85, 13);
            this.label62.TabIndex = 82;
            this.label62.Text = "Frame Duration: ";
            this.label62.Click += new System.EventHandler(this.label62_Click);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Enabled = false;
            this.label64.Location = new System.Drawing.Point(225, 121);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(39, 13);
            this.label64.TabIndex = 79;
            this.label64.Text = "X End:";
            this.label64.Click += new System.EventHandler(this.label64_Click);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Enabled = false;
            this.label65.Location = new System.Drawing.Point(227, 173);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(39, 13);
            this.label65.TabIndex = 81;
            this.label65.Text = "Z End:";
            this.label65.Click += new System.EventHandler(this.label65_Click);
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Enabled = false;
            this.label66.Location = new System.Drawing.Point(225, 147);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(39, 13);
            this.label66.TabIndex = 80;
            this.label66.Text = "Y End:";
            this.label66.Click += new System.EventHandler(this.label66_Click);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Enabled = false;
            this.label67.Location = new System.Drawing.Point(222, 42);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(42, 13);
            this.label67.TabIndex = 76;
            this.label67.Text = "X Start:";
            this.label67.Click += new System.EventHandler(this.label67_Click);
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Enabled = false;
            this.label68.Location = new System.Drawing.Point(224, 94);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(42, 13);
            this.label68.TabIndex = 78;
            this.label68.Text = "Z Start:";
            this.label68.Click += new System.EventHandler(this.label68_Click);
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Enabled = false;
            this.label69.Location = new System.Drawing.Point(222, 68);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(42, 13);
            this.label69.TabIndex = 77;
            this.label69.Text = "Y Start:";
            this.label69.Click += new System.EventHandler(this.label69_Click);
            // 
            // CutsceneActorDeleteAction
            // 
            this.CutsceneActorDeleteAction.Location = new System.Drawing.Point(291, 6);
            this.CutsceneActorDeleteAction.Name = "CutsceneActorDeleteAction";
            this.CutsceneActorDeleteAction.Size = new System.Drawing.Size(86, 23);
            this.CutsceneActorDeleteAction.TabIndex = 71;
            this.CutsceneActorDeleteAction.Text = "Delete Action";
            this.CutsceneActorDeleteAction.UseVisualStyleBackColor = true;
            this.CutsceneActorDeleteAction.Click += new System.EventHandler(this.CutsceneActorDeleteAction_Click);
            // 
            // CutsceneActorAddAction
            // 
            this.CutsceneActorAddAction.Location = new System.Drawing.Point(203, 6);
            this.CutsceneActorAddAction.Name = "CutsceneActorAddAction";
            this.CutsceneActorAddAction.Size = new System.Drawing.Size(82, 23);
            this.CutsceneActorAddAction.TabIndex = 70;
            this.CutsceneActorAddAction.Text = "Add Action";
            this.EnvironmentControlTooltip.SetToolTip(this.CutsceneActorAddAction, "Hold SHIFT to add in front of camera");
            this.CutsceneActorAddAction.UseVisualStyleBackColor = true;
            this.CutsceneActorAddAction.Click += new System.EventHandler(this.CutsceneActorAddAction_Click);
            // 
            // CutsceneActorFrameDuration
            // 
            this.CutsceneActorFrameDuration.AlwaysFireValueChanged = false;
            this.CutsceneActorFrameDuration.DisplayDigits = 1;
            this.CutsceneActorFrameDuration.DoValueRollover = false;
            this.CutsceneActorFrameDuration.Enabled = false;
            this.CutsceneActorFrameDuration.IncrementMouseWheel = 1;
            this.CutsceneActorFrameDuration.Location = new System.Drawing.Point(97, 209);
            this.CutsceneActorFrameDuration.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CutsceneActorFrameDuration.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorFrameDuration.Name = "CutsceneActorFrameDuration";
            this.CutsceneActorFrameDuration.ShiftMultiplier = 1;
            this.CutsceneActorFrameDuration.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorFrameDuration.TabIndex = 91;
            this.CutsceneActorFrameDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorFrameDuration.ValueChanged += new System.EventHandler(this.CutsceneActorFrameDuration_Leave);
            // 
            // CutsceneActorZRot
            // 
            this.CutsceneActorZRot.AlwaysFireValueChanged = false;
            this.CutsceneActorZRot.DisplayDigits = 1;
            this.CutsceneActorZRot.DoValueRollover = false;
            this.CutsceneActorZRot.Enabled = false;
            this.CutsceneActorZRot.Increment = new decimal(new int[] {
            182,
            0,
            0,
            0});
            this.CutsceneActorZRot.IncrementMouseWheel = 182;
            this.CutsceneActorZRot.Location = new System.Drawing.Point(289, 249);
            this.CutsceneActorZRot.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneActorZRot.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CutsceneActorZRot.Name = "CutsceneActorZRot";
            this.CutsceneActorZRot.ShiftMultiplier = 1;
            this.CutsceneActorZRot.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorZRot.TabIndex = 87;
            this.CutsceneActorZRot.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorZRot.ValueChanged += new System.EventHandler(this.CutsceneActorZRot_ValueChanged);
            this.CutsceneActorZRot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CutsceneActorZRot_KeyDown);
            // 
            // CutsceneActorXRot
            // 
            this.CutsceneActorXRot.AlwaysFireValueChanged = false;
            this.CutsceneActorXRot.DisplayDigits = 1;
            this.CutsceneActorXRot.DoValueRollover = false;
            this.CutsceneActorXRot.Enabled = false;
            this.CutsceneActorXRot.Increment = new decimal(new int[] {
            182,
            0,
            0,
            0});
            this.CutsceneActorXRot.IncrementMouseWheel = 182;
            this.CutsceneActorXRot.Location = new System.Drawing.Point(289, 197);
            this.CutsceneActorXRot.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneActorXRot.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CutsceneActorXRot.Name = "CutsceneActorXRot";
            this.CutsceneActorXRot.ShiftMultiplier = 1;
            this.CutsceneActorXRot.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorXRot.TabIndex = 85;
            this.CutsceneActorXRot.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorXRot.ValueChanged += new System.EventHandler(this.CutsceneActorXRot_ValueChanged);
            this.CutsceneActorXRot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CutsceneActorXRot_KeyDown);
            // 
            // CutsceneActorYRot
            // 
            this.CutsceneActorYRot.AlwaysFireValueChanged = false;
            this.CutsceneActorYRot.DisplayDigits = 1;
            this.CutsceneActorYRot.DoValueRollover = false;
            this.CutsceneActorYRot.Enabled = false;
            this.CutsceneActorYRot.Increment = new decimal(new int[] {
            182,
            0,
            0,
            0});
            this.CutsceneActorYRot.IncrementMouseWheel = 182;
            this.CutsceneActorYRot.Location = new System.Drawing.Point(289, 223);
            this.CutsceneActorYRot.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneActorYRot.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CutsceneActorYRot.Name = "CutsceneActorYRot";
            this.CutsceneActorYRot.ShiftMultiplier = 1;
            this.CutsceneActorYRot.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorYRot.TabIndex = 86;
            this.CutsceneActorYRot.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorYRot.ValueChanged += new System.EventHandler(this.CutsceneActorYRot_ValueChanged);
            this.CutsceneActorYRot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CutsceneActorYRot_KeyDown);
            // 
            // CutsceneActorZEnd
            // 
            this.CutsceneActorZEnd.AlwaysFireValueChanged = false;
            this.CutsceneActorZEnd.DisplayDigits = 1;
            this.CutsceneActorZEnd.DoValueRollover = false;
            this.CutsceneActorZEnd.Enabled = false;
            this.CutsceneActorZEnd.IncrementMouseWheel = 1;
            this.CutsceneActorZEnd.Location = new System.Drawing.Point(289, 171);
            this.CutsceneActorZEnd.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneActorZEnd.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CutsceneActorZEnd.Name = "CutsceneActorZEnd";
            this.CutsceneActorZEnd.ShiftMultiplier = 1;
            this.CutsceneActorZEnd.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorZEnd.TabIndex = 74;
            this.CutsceneActorZEnd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorZEnd.ValueChanged += new System.EventHandler(this.CutsceneActorEnd_ValueChanged);
            // 
            // CutsceneActorXEnd
            // 
            this.CutsceneActorXEnd.AlwaysFireValueChanged = false;
            this.CutsceneActorXEnd.DisplayDigits = 1;
            this.CutsceneActorXEnd.DoValueRollover = false;
            this.CutsceneActorXEnd.Enabled = false;
            this.CutsceneActorXEnd.IncrementMouseWheel = 1;
            this.CutsceneActorXEnd.Location = new System.Drawing.Point(289, 119);
            this.CutsceneActorXEnd.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneActorXEnd.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CutsceneActorXEnd.Name = "CutsceneActorXEnd";
            this.CutsceneActorXEnd.ShiftMultiplier = 1;
            this.CutsceneActorXEnd.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorXEnd.TabIndex = 72;
            this.CutsceneActorXEnd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorXEnd.ValueChanged += new System.EventHandler(this.CutsceneActorEnd_ValueChanged);
            // 
            // CutsceneActorYEnd
            // 
            this.CutsceneActorYEnd.AlwaysFireValueChanged = false;
            this.CutsceneActorYEnd.DisplayDigits = 1;
            this.CutsceneActorYEnd.DoValueRollover = false;
            this.CutsceneActorYEnd.Enabled = false;
            this.CutsceneActorYEnd.IncrementMouseWheel = 1;
            this.CutsceneActorYEnd.Location = new System.Drawing.Point(289, 145);
            this.CutsceneActorYEnd.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneActorYEnd.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CutsceneActorYEnd.Name = "CutsceneActorYEnd";
            this.CutsceneActorYEnd.ShiftMultiplier = 1;
            this.CutsceneActorYEnd.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorYEnd.TabIndex = 73;
            this.CutsceneActorYEnd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorYEnd.ValueChanged += new System.EventHandler(this.CutsceneActorEnd_ValueChanged);
            // 
            // CutsceneActorListBox
            // 
            this.CutsceneActorListBox.FormattingEnabled = true;
            this.CutsceneActorListBox.IntegralHeight = false;
            this.CutsceneActorListBox.Location = new System.Drawing.Point(9, 6);
            this.CutsceneActorListBox.Name = "CutsceneActorListBox";
            this.CutsceneActorListBox.Size = new System.Drawing.Size(157, 197);
            this.CutsceneActorListBox.TabIndex = 69;
            this.CutsceneActorListBox.Click += new System.EventHandler(this.CutsceneActorListBox_Click);
            this.CutsceneActorListBox.SelectedIndexChanged += new System.EventHandler(this.CutsceneActorListBox_SelectedIndexChanged);
            // 
            // CutsceneActorZStart
            // 
            this.CutsceneActorZStart.AlwaysFireValueChanged = false;
            this.CutsceneActorZStart.DisplayDigits = 1;
            this.CutsceneActorZStart.DoValueRollover = false;
            this.CutsceneActorZStart.Enabled = false;
            this.CutsceneActorZStart.IncrementMouseWheel = 1;
            this.CutsceneActorZStart.Location = new System.Drawing.Point(289, 92);
            this.CutsceneActorZStart.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneActorZStart.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CutsceneActorZStart.Name = "CutsceneActorZStart";
            this.CutsceneActorZStart.ShiftMultiplier = 1;
            this.CutsceneActorZStart.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorZStart.TabIndex = 68;
            this.CutsceneActorZStart.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorZStart.ValueChanged += new System.EventHandler(this.CutsceneActorStart_ValueChanged);
            // 
            // CutsceneActorXStart
            // 
            this.CutsceneActorXStart.AlwaysFireValueChanged = false;
            this.CutsceneActorXStart.DisplayDigits = 1;
            this.CutsceneActorXStart.DoValueRollover = false;
            this.CutsceneActorXStart.Enabled = false;
            this.CutsceneActorXStart.IncrementMouseWheel = 1;
            this.CutsceneActorXStart.Location = new System.Drawing.Point(289, 40);
            this.CutsceneActorXStart.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneActorXStart.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CutsceneActorXStart.Name = "CutsceneActorXStart";
            this.CutsceneActorXStart.ShiftMultiplier = 1;
            this.CutsceneActorXStart.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorXStart.TabIndex = 66;
            this.CutsceneActorXStart.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorXStart.ValueChanged += new System.EventHandler(this.CutsceneActorStart_ValueChanged);
            // 
            // CutsceneActorYStart
            // 
            this.CutsceneActorYStart.AlwaysFireValueChanged = false;
            this.CutsceneActorYStart.DisplayDigits = 1;
            this.CutsceneActorYStart.DoValueRollover = false;
            this.CutsceneActorYStart.Enabled = false;
            this.CutsceneActorYStart.IncrementMouseWheel = 1;
            this.CutsceneActorYStart.Location = new System.Drawing.Point(289, 66);
            this.CutsceneActorYStart.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CutsceneActorYStart.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.CutsceneActorYStart.Name = "CutsceneActorYStart";
            this.CutsceneActorYStart.ShiftMultiplier = 1;
            this.CutsceneActorYStart.Size = new System.Drawing.Size(84, 20);
            this.CutsceneActorYStart.TabIndex = 67;
            this.CutsceneActorYStart.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneActorYStart.ValueChanged += new System.EventHandler(this.CutsceneActorStart_ValueChanged);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(339, 205);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(16, 13);
            this.label51.TabIndex = 63;
            this.label51.Text = " - ";
            // 
            // MarkerTypeLabel
            // 
            this.MarkerTypeLabel.AutoSize = true;
            this.MarkerTypeLabel.Location = new System.Drawing.Point(4, 205);
            this.MarkerTypeLabel.Name = "MarkerTypeLabel";
            this.MarkerTypeLabel.Size = new System.Drawing.Size(34, 13);
            this.MarkerTypeLabel.TabIndex = 52;
            this.MarkerTypeLabel.Text = "Type:";
            // 
            // MarkerType
            // 
            this.MarkerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MarkerType.FormattingEnabled = true;
            this.MarkerType.Location = new System.Drawing.Point(41, 200);
            this.MarkerType.Name = "MarkerType";
            this.MarkerType.Size = new System.Drawing.Size(206, 21);
            this.MarkerType.TabIndex = 53;
            this.MarkerType.Tag = "";
            this.MarkerType.SelectionChangeCommitted += new System.EventHandler(this.MarkerType_SelectionChangeCommitted);
            // 
            // MarkerDown
            // 
            this.MarkerDown.Location = new System.Drawing.Point(371, 22);
            this.MarkerDown.Name = "MarkerDown";
            this.MarkerDown.Size = new System.Drawing.Size(26, 23);
            this.MarkerDown.TabIndex = 51;
            this.MarkerDown.Text = "▼";
            this.MarkerDown.UseVisualStyleBackColor = true;
            this.MarkerDown.Click += new System.EventHandler(this.MarkerDown_Click);
            // 
            // MarkerUp
            // 
            this.MarkerUp.Location = new System.Drawing.Point(339, 22);
            this.MarkerUp.Name = "MarkerUp";
            this.MarkerUp.Size = new System.Drawing.Size(26, 23);
            this.MarkerUp.TabIndex = 50;
            this.MarkerUp.Text = "▲";
            this.MarkerUp.UseVisualStyleBackColor = true;
            this.MarkerUp.Click += new System.EventHandler(this.MarkerUp_Click);
            // 
            // DeleteMarker
            // 
            this.DeleteMarker.Location = new System.Drawing.Point(108, 22);
            this.DeleteMarker.Name = "DeleteMarker";
            this.DeleteMarker.Size = new System.Drawing.Size(99, 23);
            this.DeleteMarker.TabIndex = 49;
            this.DeleteMarker.Text = "Delete Command";
            this.DeleteMarker.UseVisualStyleBackColor = true;
            this.DeleteMarker.Click += new System.EventHandler(this.DeleteMarker_Click);
            // 
            // AddMarker
            // 
            this.AddMarker.Location = new System.Drawing.Point(6, 22);
            this.AddMarker.Name = "AddMarker";
            this.AddMarker.Size = new System.Drawing.Size(96, 23);
            this.AddMarker.TabIndex = 48;
            this.AddMarker.Text = "Add Command";
            this.AddMarker.UseVisualStyleBackColor = true;
            this.AddMarker.Click += new System.EventHandler(this.AddMarker_Click);
            // 
            // MarkerSelect
            // 
            this.MarkerSelect.FormattingEnabled = true;
            this.MarkerSelect.IntegralHeight = false;
            this.MarkerSelect.Location = new System.Drawing.Point(6, 51);
            this.MarkerSelect.Name = "MarkerSelect";
            this.MarkerSelect.Size = new System.Drawing.Size(391, 145);
            this.MarkerSelect.TabIndex = 47;
            this.MarkerSelect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MarkerSelect_MouseClick);
            this.MarkerSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MarkerSelect_KeyDown);
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(253, 204);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(47, 13);
            this.label54.TabIndex = 45;
            this.label54.Text = "Frames: ";
            // 
            // MarkerStartFrame
            // 
            this.MarkerStartFrame.AllowHex = false;
            this.MarkerStartFrame.Digits = 5;
            this.MarkerStartFrame.Enabled = false;
            this.MarkerStartFrame.Location = new System.Drawing.Point(301, 201);
            this.MarkerStartFrame.MaxLength = 255;
            this.MarkerStartFrame.Name = "MarkerStartFrame";
            this.MarkerStartFrame.Size = new System.Drawing.Size(36, 20);
            this.MarkerStartFrame.TabIndex = 44;
            this.MarkerStartFrame.Text = "0";
            this.MarkerStartFrame.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MarkerStartFrame_KeyDown);
            this.MarkerStartFrame.Leave += new System.EventHandler(this.MarkerStartFrame_Leave);
            // 
            // CutsceneTableEntry
            // 
            this.CutsceneTableEntry.AlwaysFireValueChanged = false;
            this.CutsceneTableEntry.DisplayDigits = 1;
            this.CutsceneTableEntry.DoValueRollover = false;
            this.CutsceneTableEntry.IncrementMouseWheel = 1;
            this.CutsceneTableEntry.Location = new System.Drawing.Point(80, 582);
            this.CutsceneTableEntry.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CutsceneTableEntry.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CutsceneTableEntry.Name = "CutsceneTableEntry";
            this.CutsceneTableEntry.ShiftMultiplier = 1;
            this.CutsceneTableEntry.Size = new System.Drawing.Size(60, 20);
            this.CutsceneTableEntry.TabIndex = 72;
            this.EnvironmentControlTooltip.SetToolTip(this.CutsceneTableEntry, "If -1, cutscene table is not updated, otherwise edits the respective row number i" +
        "n the table");
            this.CutsceneTableEntry.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CutsceneTableEntry.ValueChanged += new System.EventHandler(this.CutsceneTableEntry_ValueChanged);
            // 
            // CutsceneFlag
            // 
            this.CutsceneFlag.AlwaysFireValueChanged = false;
            this.CutsceneFlag.DisplayDigits = 1;
            this.CutsceneFlag.DoValueRollover = false;
            this.CutsceneFlag.Hexadecimal = true;
            this.CutsceneFlag.IncrementMouseWheel = 1;
            this.CutsceneFlag.Location = new System.Drawing.Point(345, 556);
            this.CutsceneFlag.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.CutsceneFlag.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneFlag.Name = "CutsceneFlag";
            this.CutsceneFlag.ShiftMultiplier = 1;
            this.CutsceneFlag.Size = new System.Drawing.Size(60, 20);
            this.CutsceneFlag.TabIndex = 70;
            this.CutsceneFlag.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneFlag.ValueChanged += new System.EventHandler(this.CutsceneFlag_ValueChanged);
            // 
            // CutsceneSpawn
            // 
            this.CutsceneSpawn.AlwaysFireValueChanged = false;
            this.CutsceneSpawn.DisplayDigits = 1;
            this.CutsceneSpawn.DoValueRollover = false;
            this.CutsceneSpawn.Hexadecimal = true;
            this.CutsceneSpawn.IncrementMouseWheel = 1;
            this.CutsceneSpawn.Location = new System.Drawing.Point(228, 556);
            this.CutsceneSpawn.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.CutsceneSpawn.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneSpawn.Name = "CutsceneSpawn";
            this.CutsceneSpawn.ShiftMultiplier = 1;
            this.CutsceneSpawn.Size = new System.Drawing.Size(60, 20);
            this.CutsceneSpawn.TabIndex = 68;
            this.CutsceneSpawn.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneSpawn.Visible = false;
            this.CutsceneSpawn.ValueChanged += new System.EventHandler(this.CutsceneSpawn_ValueChanged);
            // 
            // CutsceneEntrance
            // 
            this.CutsceneEntrance.AlwaysFireValueChanged = false;
            this.CutsceneEntrance.DisplayDigits = 1;
            this.CutsceneEntrance.DoValueRollover = false;
            this.CutsceneEntrance.Hexadecimal = true;
            this.CutsceneEntrance.IncrementMouseWheel = 1;
            this.CutsceneEntrance.Location = new System.Drawing.Point(80, 556);
            this.CutsceneEntrance.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CutsceneEntrance.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneEntrance.Name = "CutsceneEntrance";
            this.CutsceneEntrance.ShiftMultiplier = 1;
            this.CutsceneEntrance.Size = new System.Drawing.Size(60, 20);
            this.CutsceneEntrance.TabIndex = 66;
            this.CutsceneEntrance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CutsceneEntrance.ValueChanged += new System.EventHandler(this.CutsceneEntrance_ValueChanged);
            // 
            // tabAnimations
            // 
            this.tabAnimations.Controls.Add(this.RenderFunctionInherit);
            this.tabAnimations.Controls.Add(this.groupBox4);
            this.tabAnimations.Location = new System.Drawing.Point(4, 40);
            this.tabAnimations.Name = "tabAnimations";
            this.tabAnimations.Size = new System.Drawing.Size(411, 676);
            this.tabAnimations.TabIndex = 10;
            this.tabAnimations.Text = "Tex.Anim";
            this.tabAnimations.UseVisualStyleBackColor = true;
            // 
            // RenderFunctionInherit
            // 
            this.RenderFunctionInherit.AutoSize = true;
            this.RenderFunctionInherit.Location = new System.Drawing.Point(108, 5);
            this.RenderFunctionInherit.Name = "RenderFunctionInherit";
            this.RenderFunctionInherit.Size = new System.Drawing.Size(139, 17);
            this.RenderFunctionInherit.TabIndex = 73;
            this.RenderFunctionInherit.Text = "Inherit from main header";
            this.EnvironmentControlTooltip.SetToolTip(this.RenderFunctionInherit, "Enable to use the render functions of the main header");
            this.RenderFunctionInherit.UseVisualStyleBackColor = true;
            this.RenderFunctionInherit.Visible = false;
            this.RenderFunctionInherit.CheckedChanged += new System.EventHandler(this.RenderFunctionInherit_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.RenderFunctionWarningLabel);
            this.groupBox4.Controls.Add(this.RenderFunctionPreview);
            this.groupBox4.Controls.Add(this.RenderFunctionGroupBoxFlag);
            this.groupBox4.Controls.Add(this.label137);
            this.groupBox4.Controls.Add(this.RenderFunctionID);
            this.groupBox4.Controls.Add(this.RenderFunctionTabs);
            this.groupBox4.Controls.Add(this.label135);
            this.groupBox4.Controls.Add(this.RenderFunctionType);
            this.groupBox4.Controls.Add(this.RenderFunctionDown);
            this.groupBox4.Controls.Add(this.RenderFunctionUp);
            this.groupBox4.Controls.Add(this.DeleteRenderFunction);
            this.groupBox4.Controls.Add(this.AddRenderFunction);
            this.groupBox4.Controls.Add(this.RenderFunctionSelect);
            this.groupBox4.Location = new System.Drawing.Point(5, 3);
            this.groupBox4.MaximumSize = new System.Drawing.Size(403, 999);
            this.groupBox4.MinimumSize = new System.Drawing.Size(403, 462);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(403, 620);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Texture Animations";
            // 
            // RenderFunctionWarningLabel
            // 
            this.RenderFunctionWarningLabel.AutoSize = true;
            this.RenderFunctionWarningLabel.BackColor = System.Drawing.Color.MistyRose;
            this.RenderFunctionWarningLabel.Location = new System.Drawing.Point(2, 51);
            this.RenderFunctionWarningLabel.Name = "RenderFunctionWarningLabel";
            this.RenderFunctionWarningLabel.Size = new System.Drawing.Size(399, 13);
            this.RenderFunctionWarningLabel.TabIndex = 72;
            this.RenderFunctionWarningLabel.Text = "    Check \"enabled advanced texture animations\" in options to use this feature   " +
    "     ";
            this.RenderFunctionWarningLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RenderFunctionWarningLabel.Visible = false;
            // 
            // RenderFunctionPreview
            // 
            this.RenderFunctionPreview.Enabled = false;
            this.RenderFunctionPreview.Location = new System.Drawing.Point(329, 197);
            this.RenderFunctionPreview.Name = "RenderFunctionPreview";
            this.RenderFunctionPreview.Size = new System.Drawing.Size(68, 28);
            this.RenderFunctionPreview.TabIndex = 71;
            this.RenderFunctionPreview.Text = "View";
            this.RenderFunctionPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RenderFunctionPreview.UseVisualStyleBackColor = true;
            this.RenderFunctionPreview.Click += new System.EventHandler(this.RenderFunctionPreview_Click);
            // 
            // RenderFunctionGroupBoxFlag
            // 
            this.RenderFunctionGroupBoxFlag.Controls.Add(this.RenderFunctionFlagFreezeCheckBox);
            this.RenderFunctionGroupBoxFlag.Controls.Add(this.RenderFunctionFlagPresetToolStrip);
            this.RenderFunctionGroupBoxFlag.Controls.Add(this.RenderFunctionFlagBitwiseLabel);
            this.RenderFunctionGroupBoxFlag.Controls.Add(this.RenderFunctionFlagBitwise);
            this.RenderFunctionGroupBoxFlag.Controls.Add(this.RenderFunctionFlagReverseCheckbox);
            this.RenderFunctionGroupBoxFlag.Controls.Add(this.RenderFunctionFlagLabel);
            this.RenderFunctionGroupBoxFlag.Controls.Add(this.label114);
            this.RenderFunctionGroupBoxFlag.Controls.Add(this.RenderFunctionFlagID);
            this.RenderFunctionGroupBoxFlag.Controls.Add(this.RenderFunctionFlagType);
            this.RenderFunctionGroupBoxFlag.Location = new System.Drawing.Point(13, 433);
            this.RenderFunctionGroupBoxFlag.Name = "RenderFunctionGroupBoxFlag";
            this.RenderFunctionGroupBoxFlag.Size = new System.Drawing.Size(380, 111);
            this.RenderFunctionGroupBoxFlag.TabIndex = 67;
            this.RenderFunctionGroupBoxFlag.TabStop = false;
            this.RenderFunctionGroupBoxFlag.Text = "Flag Setting";
            // 
            // RenderFunctionFlagFreezeCheckBox
            // 
            this.RenderFunctionFlagFreezeCheckBox.AutoSize = true;
            this.RenderFunctionFlagFreezeCheckBox.Location = new System.Drawing.Point(147, 81);
            this.RenderFunctionFlagFreezeCheckBox.Name = "RenderFunctionFlagFreezeCheckBox";
            this.RenderFunctionFlagFreezeCheckBox.Size = new System.Drawing.Size(131, 17);
            this.RenderFunctionFlagFreezeCheckBox.TabIndex = 74;
            this.RenderFunctionFlagFreezeCheckBox.Text = "Freeze if flag is not set";
            this.EnvironmentControlTooltip.SetToolTip(this.RenderFunctionFlagFreezeCheckBox, "The texture frame or color will be frozen if the flag is disabled");
            this.RenderFunctionFlagFreezeCheckBox.UseVisualStyleBackColor = true;
            this.RenderFunctionFlagFreezeCheckBox.CheckedChanged += new System.EventHandler(this.RenderFunctionFlagFreezeCheckBox_CheckedChanged);
            // 
            // RenderFunctionFlagPresetToolStrip
            // 
            this.RenderFunctionFlagPresetToolStrip.AutoSize = false;
            this.RenderFunctionFlagPresetToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.RenderFunctionFlagPresetToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.RenderFunctionFlagPresetToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RenderFunctionFlagPresetButton});
            this.RenderFunctionFlagPresetToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.RenderFunctionFlagPresetToolStrip.Location = new System.Drawing.Point(293, 49);
            this.RenderFunctionFlagPresetToolStrip.Name = "RenderFunctionFlagPresetToolStrip";
            this.RenderFunctionFlagPresetToolStrip.Size = new System.Drawing.Size(59, 19);
            this.RenderFunctionFlagPresetToolStrip.TabIndex = 73;
            this.RenderFunctionFlagPresetToolStrip.Text = "Presets";
            // 
            // RenderFunctionFlagPresetButton
            // 
            this.RenderFunctionFlagPresetButton.BackColor = System.Drawing.Color.Transparent;
            this.RenderFunctionFlagPresetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RenderFunctionFlagPresetButton.Name = "RenderFunctionFlagPresetButton";
            this.RenderFunctionFlagPresetButton.ShowDropDownArrow = false;
            this.RenderFunctionFlagPresetButton.Size = new System.Drawing.Size(43, 19);
            this.RenderFunctionFlagPresetButton.Text = "Preset";
            this.RenderFunctionFlagPresetButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.RenderFunctionFlagPresetButton.ToolTipText = "Actions";
            // 
            // RenderFunctionFlagBitwiseLabel
            // 
            this.RenderFunctionFlagBitwiseLabel.AutoSize = true;
            this.RenderFunctionFlagBitwiseLabel.Enabled = false;
            this.RenderFunctionFlagBitwiseLabel.Location = new System.Drawing.Point(164, 51);
            this.RenderFunctionFlagBitwiseLabel.Name = "RenderFunctionFlagBitwiseLabel";
            this.RenderFunctionFlagBitwiseLabel.Size = new System.Drawing.Size(36, 13);
            this.RenderFunctionFlagBitwiseLabel.TabIndex = 72;
            this.RenderFunctionFlagBitwiseLabel.Text = "Mask:";
            // 
            // RenderFunctionFlagBitwise
            // 
            this.RenderFunctionFlagBitwise.AllowHex = true;
            this.RenderFunctionFlagBitwise.Digits = 8;
            this.RenderFunctionFlagBitwise.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RenderFunctionFlagBitwise.Location = new System.Drawing.Point(213, 48);
            this.RenderFunctionFlagBitwise.Name = "RenderFunctionFlagBitwise";
            this.RenderFunctionFlagBitwise.Size = new System.Drawing.Size(64, 20);
            this.RenderFunctionFlagBitwise.TabIndex = 71;
            this.RenderFunctionFlagBitwise.Text = "00000000";
            this.RenderFunctionFlagBitwise.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenderFunctionFlagBitwise_KeyDown);
            this.RenderFunctionFlagBitwise.Leave += new System.EventHandler(this.RenderFunctionFlagBitwise_Leave);
            // 
            // RenderFunctionFlagReverseCheckbox
            // 
            this.RenderFunctionFlagReverseCheckbox.AutoSize = true;
            this.RenderFunctionFlagReverseCheckbox.Location = new System.Drawing.Point(8, 81);
            this.RenderFunctionFlagReverseCheckbox.Name = "RenderFunctionFlagReverseCheckbox";
            this.RenderFunctionFlagReverseCheckbox.Size = new System.Drawing.Size(133, 17);
            this.RenderFunctionFlagReverseCheckbox.TabIndex = 70;
            this.RenderFunctionFlagReverseCheckbox.Text = "Check if Flag is not set";
            this.EnvironmentControlTooltip.SetToolTip(this.RenderFunctionFlagReverseCheckbox, "Reverses the condition");
            this.RenderFunctionFlagReverseCheckbox.UseVisualStyleBackColor = true;
            this.RenderFunctionFlagReverseCheckbox.CheckedChanged += new System.EventHandler(this.RenderFunctionFlagReverseCheckbox_CheckedChanged);
            // 
            // RenderFunctionFlagLabel
            // 
            this.RenderFunctionFlagLabel.AutoSize = true;
            this.RenderFunctionFlagLabel.Enabled = false;
            this.RenderFunctionFlagLabel.Location = new System.Drawing.Point(2, 51);
            this.RenderFunctionFlagLabel.Name = "RenderFunctionFlagLabel";
            this.RenderFunctionFlagLabel.Size = new System.Drawing.Size(44, 13);
            this.RenderFunctionFlagLabel.TabIndex = 41;
            this.RenderFunctionFlagLabel.Text = "Flag ID:";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(2, 24);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(34, 13);
            this.label114.TabIndex = 68;
            this.label114.Text = "Type:";
            // 
            // RenderFunctionFlagID
            // 
            this.RenderFunctionFlagID.AlwaysFireValueChanged = false;
            this.RenderFunctionFlagID.DisplayDigits = 1;
            this.RenderFunctionFlagID.DoValueRollover = false;
            this.RenderFunctionFlagID.Enabled = false;
            this.RenderFunctionFlagID.Hexadecimal = true;
            this.RenderFunctionFlagID.IncrementMouseWheel = 1;
            this.RenderFunctionFlagID.Location = new System.Drawing.Point(56, 49);
            this.RenderFunctionFlagID.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RenderFunctionFlagID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RenderFunctionFlagID.Name = "RenderFunctionFlagID";
            this.RenderFunctionFlagID.ShiftMultiplier = 1;
            this.RenderFunctionFlagID.Size = new System.Drawing.Size(85, 20);
            this.RenderFunctionFlagID.TabIndex = 42;
            this.RenderFunctionFlagID.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RenderFunctionFlagID.ValueChanged += new System.EventHandler(this.RenderFunctionFlagID_ValueChanged);
            // 
            // RenderFunctionFlagType
            // 
            this.RenderFunctionFlagType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RenderFunctionFlagType.FormattingEnabled = true;
            this.RenderFunctionFlagType.Location = new System.Drawing.Point(39, 19);
            this.RenderFunctionFlagType.Name = "RenderFunctionFlagType";
            this.RenderFunctionFlagType.Size = new System.Drawing.Size(206, 21);
            this.RenderFunctionFlagType.TabIndex = 69;
            this.RenderFunctionFlagType.Tag = "";
            this.RenderFunctionFlagType.SelectionChangeCommitted += new System.EventHandler(this.RenderFunctionFlagType_SelectionChangeCommitted);
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(7, 24);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(55, 13);
            this.label137.TabIndex = 66;
            this.label137.Text = "Segment: ";
            // 
            // RenderFunctionID
            // 
            this.RenderFunctionID.Hexadecimal = true;
            this.RenderFunctionID.Location = new System.Drawing.Point(64, 22);
            this.RenderFunctionID.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.RenderFunctionID.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.RenderFunctionID.Name = "RenderFunctionID";
            this.RenderFunctionID.Size = new System.Drawing.Size(55, 20);
            this.RenderFunctionID.TabIndex = 65;
            this.RenderFunctionID.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.RenderFunctionID.ValueChanged += new System.EventHandler(this.RenderFunctionID_ValueChanged);
            // 
            // RenderFunctionTabs
            // 
            this.RenderFunctionTabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.RenderFunctionTabs.Controls.Add(this.tabTextureScroll);
            this.RenderFunctionTabs.Controls.Add(this.tabColorBlending);
            this.RenderFunctionTabs.Controls.Add(this.tabTextureSwap);
            this.RenderFunctionTabs.Controls.Add(this.tabTextureSwapFrames);
            this.RenderFunctionTabs.Controls.Add(this.tabCameraEffect);
            this.RenderFunctionTabs.Controls.Add(this.tabConditionalDraw);
            this.RenderFunctionTabs.ItemSize = new System.Drawing.Size(96, 7);
            this.RenderFunctionTabs.Location = new System.Drawing.Point(6, 229);
            this.RenderFunctionTabs.Multiline = true;
            this.RenderFunctionTabs.Name = "RenderFunctionTabs";
            this.RenderFunctionTabs.Padding = new System.Drawing.Point(0, 0);
            this.RenderFunctionTabs.SelectedIndex = 0;
            this.RenderFunctionTabs.Size = new System.Drawing.Size(391, 198);
            this.RenderFunctionTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.RenderFunctionTabs.TabIndex = 54;
            // 
            // tabTextureScroll
            // 
            this.tabTextureScroll.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabTextureScroll.Controls.Add(this.label106);
            this.tabTextureScroll.Controls.Add(this.FunctionTextureScrollHeight2);
            this.tabTextureScroll.Controls.Add(this.FunctionTextureScrollXVelocity);
            this.tabTextureScroll.Controls.Add(this.label107);
            this.tabTextureScroll.Controls.Add(this.niceLine17);
            this.tabTextureScroll.Controls.Add(this.FunctionTextureScrollWidth2);
            this.tabTextureScroll.Controls.Add(this.label113);
            this.tabTextureScroll.Controls.Add(this.label108);
            this.tabTextureScroll.Controls.Add(this.FunctionTextureScrollYVelocity);
            this.tabTextureScroll.Controls.Add(this.FunctionTextureScrollYVelocity2);
            this.tabTextureScroll.Controls.Add(this.label112);
            this.tabTextureScroll.Controls.Add(this.label109);
            this.tabTextureScroll.Controls.Add(this.FunctionTextureScrollWidth);
            this.tabTextureScroll.Controls.Add(this.FunctionTextureScrollXVelocity2);
            this.tabTextureScroll.Controls.Add(this.label111);
            this.tabTextureScroll.Controls.Add(this.niceLine16);
            this.tabTextureScroll.Controls.Add(this.FunctionTextureScrollHeight);
            this.tabTextureScroll.Controls.Add(this.label110);
            this.tabTextureScroll.Location = new System.Drawing.Point(4, 21);
            this.tabTextureScroll.Name = "tabTextureScroll";
            this.tabTextureScroll.Padding = new System.Windows.Forms.Padding(3);
            this.tabTextureScroll.Size = new System.Drawing.Size(383, 173);
            this.tabTextureScroll.TabIndex = 0;
            this.tabTextureScroll.Text = "Texture Scroll";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Enabled = false;
            this.label106.Location = new System.Drawing.Point(148, 134);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(41, 13);
            this.label106.TabIndex = 39;
            this.label106.Text = "Height:";
            // 
            // FunctionTextureScrollHeight2
            // 
            this.FunctionTextureScrollHeight2.AlwaysFireValueChanged = false;
            this.FunctionTextureScrollHeight2.DisplayDigits = 1;
            this.FunctionTextureScrollHeight2.DoValueRollover = false;
            this.FunctionTextureScrollHeight2.Enabled = false;
            this.FunctionTextureScrollHeight2.IncrementMouseWheel = 1;
            this.FunctionTextureScrollHeight2.Location = new System.Drawing.Point(219, 130);
            this.FunctionTextureScrollHeight2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FunctionTextureScrollHeight2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollHeight2.Name = "FunctionTextureScrollHeight2";
            this.FunctionTextureScrollHeight2.ShiftMultiplier = 5;
            this.FunctionTextureScrollHeight2.Size = new System.Drawing.Size(44, 20);
            this.FunctionTextureScrollHeight2.TabIndex = 40;
            this.FunctionTextureScrollHeight2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollHeight2.ValueChanged += new System.EventHandler(this.FunctionTextureScrollHeight2_ValueChanged);
            // 
            // FunctionTextureScrollXVelocity
            // 
            this.FunctionTextureScrollXVelocity.AlwaysFireValueChanged = false;
            this.FunctionTextureScrollXVelocity.DisplayDigits = 1;
            this.FunctionTextureScrollXVelocity.DoValueRollover = false;
            this.FunctionTextureScrollXVelocity.Enabled = false;
            this.FunctionTextureScrollXVelocity.IncrementMouseWheel = 1;
            this.FunctionTextureScrollXVelocity.Location = new System.Drawing.Point(80, 23);
            this.FunctionTextureScrollXVelocity.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.FunctionTextureScrollXVelocity.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.FunctionTextureScrollXVelocity.Name = "FunctionTextureScrollXVelocity";
            this.FunctionTextureScrollXVelocity.ShiftMultiplier = 5;
            this.FunctionTextureScrollXVelocity.Size = new System.Drawing.Size(44, 20);
            this.FunctionTextureScrollXVelocity.TabIndex = 24;
            this.FunctionTextureScrollXVelocity.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollXVelocity.ValueChanged += new System.EventHandler(this.FunctionTextureScrollXVelocity_ValueChanged);
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Enabled = false;
            this.label107.Location = new System.Drawing.Point(148, 108);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(38, 13);
            this.label107.TabIndex = 37;
            this.label107.Text = "Width:";
            // 
            // niceLine17
            // 
            this.niceLine17.Caption = "Main Texture";
            this.niceLine17.Location = new System.Drawing.Point(6, 4);
            this.niceLine17.Name = "niceLine17";
            this.niceLine17.Size = new System.Drawing.Size(371, 15);
            this.niceLine17.TabIndex = 25;
            this.niceLine17.TabStop = false;
            // 
            // FunctionTextureScrollWidth2
            // 
            this.FunctionTextureScrollWidth2.AlwaysFireValueChanged = false;
            this.FunctionTextureScrollWidth2.DisplayDigits = 1;
            this.FunctionTextureScrollWidth2.DoValueRollover = false;
            this.FunctionTextureScrollWidth2.Enabled = false;
            this.FunctionTextureScrollWidth2.IncrementMouseWheel = 1;
            this.FunctionTextureScrollWidth2.Location = new System.Drawing.Point(219, 104);
            this.FunctionTextureScrollWidth2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FunctionTextureScrollWidth2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollWidth2.Name = "FunctionTextureScrollWidth2";
            this.FunctionTextureScrollWidth2.ShiftMultiplier = 5;
            this.FunctionTextureScrollWidth2.Size = new System.Drawing.Size(44, 20);
            this.FunctionTextureScrollWidth2.TabIndex = 38;
            this.FunctionTextureScrollWidth2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollWidth2.ValueChanged += new System.EventHandler(this.FunctionTextureScrollWidth2_ValueChanged);
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Enabled = false;
            this.label113.Location = new System.Drawing.Point(9, 25);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(57, 13);
            this.label113.TabIndex = 23;
            this.label113.Text = "X Velocity:";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Enabled = false;
            this.label108.Location = new System.Drawing.Point(10, 132);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(57, 13);
            this.label108.TabIndex = 35;
            this.label108.Text = "Y Velocity:";
            // 
            // FunctionTextureScrollYVelocity
            // 
            this.FunctionTextureScrollYVelocity.AlwaysFireValueChanged = false;
            this.FunctionTextureScrollYVelocity.DisplayDigits = 1;
            this.FunctionTextureScrollYVelocity.DoValueRollover = false;
            this.FunctionTextureScrollYVelocity.Enabled = false;
            this.FunctionTextureScrollYVelocity.IncrementMouseWheel = 1;
            this.FunctionTextureScrollYVelocity.Location = new System.Drawing.Point(80, 49);
            this.FunctionTextureScrollYVelocity.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.FunctionTextureScrollYVelocity.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.FunctionTextureScrollYVelocity.Name = "FunctionTextureScrollYVelocity";
            this.FunctionTextureScrollYVelocity.ShiftMultiplier = 5;
            this.FunctionTextureScrollYVelocity.Size = new System.Drawing.Size(44, 20);
            this.FunctionTextureScrollYVelocity.TabIndex = 27;
            this.FunctionTextureScrollYVelocity.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollYVelocity.ValueChanged += new System.EventHandler(this.FunctionTextureScrollYVelocity_ValueChanged);
            // 
            // FunctionTextureScrollYVelocity2
            // 
            this.FunctionTextureScrollYVelocity2.AlwaysFireValueChanged = false;
            this.FunctionTextureScrollYVelocity2.DisplayDigits = 1;
            this.FunctionTextureScrollYVelocity2.DoValueRollover = false;
            this.FunctionTextureScrollYVelocity2.Enabled = false;
            this.FunctionTextureScrollYVelocity2.IncrementMouseWheel = 1;
            this.FunctionTextureScrollYVelocity2.Location = new System.Drawing.Point(81, 130);
            this.FunctionTextureScrollYVelocity2.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.FunctionTextureScrollYVelocity2.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.FunctionTextureScrollYVelocity2.Name = "FunctionTextureScrollYVelocity2";
            this.FunctionTextureScrollYVelocity2.ShiftMultiplier = 5;
            this.FunctionTextureScrollYVelocity2.Size = new System.Drawing.Size(44, 20);
            this.FunctionTextureScrollYVelocity2.TabIndex = 36;
            this.FunctionTextureScrollYVelocity2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollYVelocity2.ValueChanged += new System.EventHandler(this.FunctionTextureScrollYVelocity2_ValueChanged);
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Enabled = false;
            this.label112.Location = new System.Drawing.Point(9, 51);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(57, 13);
            this.label112.TabIndex = 26;
            this.label112.Text = "Y Velocity:";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Enabled = false;
            this.label109.Location = new System.Drawing.Point(10, 106);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(57, 13);
            this.label109.TabIndex = 33;
            this.label109.Text = "X Velocity:";
            // 
            // FunctionTextureScrollWidth
            // 
            this.FunctionTextureScrollWidth.AlwaysFireValueChanged = false;
            this.FunctionTextureScrollWidth.DisplayDigits = 1;
            this.FunctionTextureScrollWidth.DoValueRollover = false;
            this.FunctionTextureScrollWidth.Enabled = false;
            this.FunctionTextureScrollWidth.IncrementMouseWheel = 1;
            this.FunctionTextureScrollWidth.Location = new System.Drawing.Point(218, 23);
            this.FunctionTextureScrollWidth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FunctionTextureScrollWidth.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollWidth.Name = "FunctionTextureScrollWidth";
            this.FunctionTextureScrollWidth.ShiftMultiplier = 5;
            this.FunctionTextureScrollWidth.Size = new System.Drawing.Size(44, 20);
            this.FunctionTextureScrollWidth.TabIndex = 29;
            this.FunctionTextureScrollWidth.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollWidth.ValueChanged += new System.EventHandler(this.FunctionTextureScrollWidth_ValueChanged);
            // 
            // FunctionTextureScrollXVelocity2
            // 
            this.FunctionTextureScrollXVelocity2.AlwaysFireValueChanged = false;
            this.FunctionTextureScrollXVelocity2.DisplayDigits = 1;
            this.FunctionTextureScrollXVelocity2.DoValueRollover = false;
            this.FunctionTextureScrollXVelocity2.Enabled = false;
            this.FunctionTextureScrollXVelocity2.IncrementMouseWheel = 1;
            this.FunctionTextureScrollXVelocity2.Location = new System.Drawing.Point(81, 104);
            this.FunctionTextureScrollXVelocity2.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.FunctionTextureScrollXVelocity2.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.FunctionTextureScrollXVelocity2.Name = "FunctionTextureScrollXVelocity2";
            this.FunctionTextureScrollXVelocity2.ShiftMultiplier = 5;
            this.FunctionTextureScrollXVelocity2.Size = new System.Drawing.Size(44, 20);
            this.FunctionTextureScrollXVelocity2.TabIndex = 34;
            this.FunctionTextureScrollXVelocity2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollXVelocity2.ValueChanged += new System.EventHandler(this.FunctionTextureScrollXVelocity2_ValueChanged);
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Enabled = false;
            this.label111.Location = new System.Drawing.Point(147, 27);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(38, 13);
            this.label111.TabIndex = 28;
            this.label111.Text = "Width:";
            // 
            // niceLine16
            // 
            this.niceLine16.Caption = "Secondary Texture";
            this.niceLine16.Location = new System.Drawing.Point(3, 88);
            this.niceLine16.Name = "niceLine16";
            this.niceLine16.Size = new System.Drawing.Size(372, 15);
            this.niceLine16.TabIndex = 22;
            this.niceLine16.TabStop = false;
            // 
            // FunctionTextureScrollHeight
            // 
            this.FunctionTextureScrollHeight.AlwaysFireValueChanged = false;
            this.FunctionTextureScrollHeight.DisplayDigits = 1;
            this.FunctionTextureScrollHeight.DoValueRollover = false;
            this.FunctionTextureScrollHeight.Enabled = false;
            this.FunctionTextureScrollHeight.IncrementMouseWheel = 1;
            this.FunctionTextureScrollHeight.Location = new System.Drawing.Point(218, 49);
            this.FunctionTextureScrollHeight.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FunctionTextureScrollHeight.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollHeight.Name = "FunctionTextureScrollHeight";
            this.FunctionTextureScrollHeight.ShiftMultiplier = 5;
            this.FunctionTextureScrollHeight.Size = new System.Drawing.Size(44, 20);
            this.FunctionTextureScrollHeight.TabIndex = 31;
            this.FunctionTextureScrollHeight.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextureScrollHeight.ValueChanged += new System.EventHandler(this.FunctionTextureScrollHeight_ValueChanged);
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Enabled = false;
            this.label110.Location = new System.Drawing.Point(147, 53);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(41, 13);
            this.label110.TabIndex = 30;
            this.label110.Text = "Height:";
            // 
            // tabColorBlending
            // 
            this.tabColorBlending.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabColorBlending.Controls.Add(this.label125);
            this.tabColorBlending.Controls.Add(this.FunctionColorBlendColor);
            this.tabColorBlending.Controls.Add(this.label119);
            this.tabColorBlending.Controls.Add(this.label118);
            this.tabColorBlending.Controls.Add(this.FunctionColorBlendDown);
            this.tabColorBlending.Controls.Add(this.FunctionColorBlendUp);
            this.tabColorBlending.Controls.Add(this.FunctionColorBlendDelete);
            this.tabColorBlending.Controls.Add(this.FunctionColorBlendAdd);
            this.tabColorBlending.Controls.Add(this.FunctionColorBlendAlpha);
            this.tabColorBlending.Controls.Add(this.FunctionColorBlendFrames);
            this.tabColorBlending.Controls.Add(this.FunctionColorBlendList);
            this.tabColorBlending.Location = new System.Drawing.Point(4, 21);
            this.tabColorBlending.Name = "tabColorBlending";
            this.tabColorBlending.Size = new System.Drawing.Size(383, 173);
            this.tabColorBlending.TabIndex = 1;
            this.tabColorBlending.Text = "Color Blend";
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Enabled = false;
            this.label125.Location = new System.Drawing.Point(206, 90);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(37, 13);
            this.label125.TabIndex = 91;
            this.label125.Text = "Alpha:";
            // 
            // FunctionColorBlendColor
            // 
            this.FunctionColorBlendColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FunctionColorBlendColor.Enabled = false;
            this.FunctionColorBlendColor.Location = new System.Drawing.Point(273, 62);
            this.FunctionColorBlendColor.Name = "FunctionColorBlendColor";
            this.FunctionColorBlendColor.Size = new System.Drawing.Size(100, 20);
            this.FunctionColorBlendColor.TabIndex = 89;
            this.FunctionColorBlendColor.TabStop = false;
            this.FunctionColorBlendColor.DoubleClick += new System.EventHandler(this.FunctionColorBlendColor_DoubleClick);
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Enabled = false;
            this.label119.Location = new System.Drawing.Point(206, 64);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(34, 13);
            this.label119.TabIndex = 88;
            this.label119.Text = "Color:";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(205, 36);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(85, 13);
            this.label118.TabIndex = 86;
            this.label118.Text = "Frame Duration: ";
            // 
            // FunctionColorBlendDown
            // 
            this.FunctionColorBlendDown.Location = new System.Drawing.Point(173, 33);
            this.FunctionColorBlendDown.Name = "FunctionColorBlendDown";
            this.FunctionColorBlendDown.Size = new System.Drawing.Size(26, 23);
            this.FunctionColorBlendDown.TabIndex = 85;
            this.FunctionColorBlendDown.Text = "▼";
            this.FunctionColorBlendDown.UseVisualStyleBackColor = true;
            this.FunctionColorBlendDown.Click += new System.EventHandler(this.FunctionColorBlendDown_Click);
            // 
            // FunctionColorBlendUp
            // 
            this.FunctionColorBlendUp.Location = new System.Drawing.Point(173, 7);
            this.FunctionColorBlendUp.Name = "FunctionColorBlendUp";
            this.FunctionColorBlendUp.Size = new System.Drawing.Size(26, 23);
            this.FunctionColorBlendUp.TabIndex = 84;
            this.FunctionColorBlendUp.Text = "▲";
            this.FunctionColorBlendUp.UseVisualStyleBackColor = true;
            this.FunctionColorBlendUp.Click += new System.EventHandler(this.FunctionColorBlendUp_Click);
            // 
            // FunctionColorBlendDelete
            // 
            this.FunctionColorBlendDelete.Location = new System.Drawing.Point(289, 7);
            this.FunctionColorBlendDelete.Name = "FunctionColorBlendDelete";
            this.FunctionColorBlendDelete.Size = new System.Drawing.Size(86, 23);
            this.FunctionColorBlendDelete.TabIndex = 83;
            this.FunctionColorBlendDelete.Text = "Delete Color";
            this.FunctionColorBlendDelete.UseVisualStyleBackColor = true;
            this.FunctionColorBlendDelete.Click += new System.EventHandler(this.FunctionColorBlendDelete_Click);
            // 
            // FunctionColorBlendAdd
            // 
            this.FunctionColorBlendAdd.Location = new System.Drawing.Point(201, 7);
            this.FunctionColorBlendAdd.Name = "FunctionColorBlendAdd";
            this.FunctionColorBlendAdd.Size = new System.Drawing.Size(82, 23);
            this.FunctionColorBlendAdd.TabIndex = 82;
            this.FunctionColorBlendAdd.Text = "Add Color";
            this.FunctionColorBlendAdd.UseVisualStyleBackColor = true;
            this.FunctionColorBlendAdd.Click += new System.EventHandler(this.FunctionColorBlendAdd_Click);
            // 
            // FunctionColorBlendAlpha
            // 
            this.FunctionColorBlendAlpha.AlwaysFireValueChanged = false;
            this.FunctionColorBlendAlpha.DisplayDigits = 1;
            this.FunctionColorBlendAlpha.DoValueRollover = false;
            this.FunctionColorBlendAlpha.Enabled = false;
            this.FunctionColorBlendAlpha.IncrementMouseWheel = 1;
            this.FunctionColorBlendAlpha.Location = new System.Drawing.Point(273, 88);
            this.FunctionColorBlendAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FunctionColorBlendAlpha.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionColorBlendAlpha.Name = "FunctionColorBlendAlpha";
            this.FunctionColorBlendAlpha.ShiftMultiplier = 1;
            this.FunctionColorBlendAlpha.Size = new System.Drawing.Size(44, 20);
            this.FunctionColorBlendAlpha.TabIndex = 90;
            this.FunctionColorBlendAlpha.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionColorBlendAlpha.ValueChanged += new System.EventHandler(this.FunctionColorBlendAlpha_ValueChanged);
            // 
            // FunctionColorBlendFrames
            // 
            this.FunctionColorBlendFrames.AlwaysFireValueChanged = false;
            this.FunctionColorBlendFrames.DisplayDigits = 1;
            this.FunctionColorBlendFrames.DoValueRollover = false;
            this.FunctionColorBlendFrames.Enabled = false;
            this.FunctionColorBlendFrames.IncrementMouseWheel = 1;
            this.FunctionColorBlendFrames.Location = new System.Drawing.Point(296, 36);
            this.FunctionColorBlendFrames.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.FunctionColorBlendFrames.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FunctionColorBlendFrames.Name = "FunctionColorBlendFrames";
            this.FunctionColorBlendFrames.ShiftMultiplier = 1;
            this.FunctionColorBlendFrames.Size = new System.Drawing.Size(59, 20);
            this.FunctionColorBlendFrames.TabIndex = 87;
            this.FunctionColorBlendFrames.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FunctionColorBlendFrames.ValueChanged += new System.EventHandler(this.FunctionColorBlendFrames_ValueChanged);
            // 
            // FunctionColorBlendList
            // 
            this.FunctionColorBlendList.FormattingEnabled = true;
            this.FunctionColorBlendList.IntegralHeight = false;
            this.FunctionColorBlendList.Location = new System.Drawing.Point(7, 5);
            this.FunctionColorBlendList.Name = "FunctionColorBlendList";
            this.FunctionColorBlendList.Size = new System.Drawing.Size(160, 162);
            this.FunctionColorBlendList.TabIndex = 81;
            this.FunctionColorBlendList.Click += new System.EventHandler(this.FunctionColorBlendList_Click);
            // 
            // tabTextureSwap
            // 
            this.tabTextureSwap.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabTextureSwap.Controls.Add(this.FunctionTextureSwapPictureBox2);
            this.tabTextureSwap.Controls.Add(this.FunctionTextureSwapPictureBox);
            this.tabTextureSwap.Controls.Add(this.label116);
            this.tabTextureSwap.Controls.Add(this.FunctionTextureSwapTextureID2);
            this.tabTextureSwap.Controls.Add(this.label115);
            this.tabTextureSwap.Controls.Add(this.FunctionTextureSwapTextureID);
            this.tabTextureSwap.Location = new System.Drawing.Point(4, 21);
            this.tabTextureSwap.Name = "tabTextureSwap";
            this.tabTextureSwap.Size = new System.Drawing.Size(383, 173);
            this.tabTextureSwap.TabIndex = 2;
            this.tabTextureSwap.Text = "tabTextureSwap";
            // 
            // FunctionTextureSwapPictureBox2
            // 
            this.FunctionTextureSwapPictureBox2.Location = new System.Drawing.Point(257, 68);
            this.FunctionTextureSwapPictureBox2.Name = "FunctionTextureSwapPictureBox2";
            this.FunctionTextureSwapPictureBox2.Size = new System.Drawing.Size(98, 90);
            this.FunctionTextureSwapPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FunctionTextureSwapPictureBox2.TabIndex = 22;
            this.FunctionTextureSwapPictureBox2.TabStop = false;
            // 
            // FunctionTextureSwapPictureBox
            // 
            this.FunctionTextureSwapPictureBox.Location = new System.Drawing.Point(59, 68);
            this.FunctionTextureSwapPictureBox.Name = "FunctionTextureSwapPictureBox";
            this.FunctionTextureSwapPictureBox.Size = new System.Drawing.Size(98, 90);
            this.FunctionTextureSwapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FunctionTextureSwapPictureBox.TabIndex = 21;
            this.FunctionTextureSwapPictureBox.TabStop = false;
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(1, 33);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(47, 13);
            this.label116.TabIndex = 20;
            this.label116.Text = "Flag On:";
            // 
            // FunctionTextureSwapTextureID2
            // 
            this.FunctionTextureSwapTextureID2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FunctionTextureSwapTextureID2.FormattingEnabled = true;
            this.FunctionTextureSwapTextureID2.Location = new System.Drawing.Point(59, 30);
            this.FunctionTextureSwapTextureID2.Name = "FunctionTextureSwapTextureID2";
            this.FunctionTextureSwapTextureID2.Size = new System.Drawing.Size(301, 21);
            this.FunctionTextureSwapTextureID2.TabIndex = 19;
            this.FunctionTextureSwapTextureID2.SelectionChangeCommitted += new System.EventHandler(this.FunctionTextureSwapTextureID2_SelectionChangeCommitted);
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(1, 6);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(47, 13);
            this.label115.TabIndex = 18;
            this.label115.Text = "Flag Off:";
            // 
            // FunctionTextureSwapTextureID
            // 
            this.FunctionTextureSwapTextureID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FunctionTextureSwapTextureID.FormattingEnabled = true;
            this.FunctionTextureSwapTextureID.Location = new System.Drawing.Point(59, 3);
            this.FunctionTextureSwapTextureID.Name = "FunctionTextureSwapTextureID";
            this.FunctionTextureSwapTextureID.Size = new System.Drawing.Size(301, 21);
            this.FunctionTextureSwapTextureID.TabIndex = 17;
            this.FunctionTextureSwapTextureID.SelectionChangeCommitted += new System.EventHandler(this.FunctionTextureSwapTextureID_SelectionChangeCommitted);
            // 
            // tabTextureSwapFrames
            // 
            this.tabTextureSwapFrames.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabTextureSwapFrames.Controls.Add(this.FunctionTextureSwapAnimationPictureBox);
            this.tabTextureSwapFrames.Controls.Add(this.FunctionTextureSwapAnimationImage);
            this.tabTextureSwapFrames.Controls.Add(this.FunctionTextureSwapAnimationDown);
            this.tabTextureSwapFrames.Controls.Add(this.FunctionTextureSwapAnimationUp);
            this.tabTextureSwapFrames.Controls.Add(this.label117);
            this.tabTextureSwapFrames.Controls.Add(this.FunctionTextureSwapAnimationDelete);
            this.tabTextureSwapFrames.Controls.Add(this.FunctionTextureSwapAnimationAdd);
            this.tabTextureSwapFrames.Controls.Add(this.FunctionTextureSwapAnimationDuration);
            this.tabTextureSwapFrames.Controls.Add(this.FunctionTextureSwapAnimationList);
            this.tabTextureSwapFrames.Location = new System.Drawing.Point(4, 21);
            this.tabTextureSwapFrames.Name = "tabTextureSwapFrames";
            this.tabTextureSwapFrames.Size = new System.Drawing.Size(383, 173);
            this.tabTextureSwapFrames.TabIndex = 3;
            this.tabTextureSwapFrames.Text = "tabPage1";
            // 
            // FunctionTextureSwapAnimationPictureBox
            // 
            this.FunctionTextureSwapAnimationPictureBox.Location = new System.Drawing.Point(290, 89);
            this.FunctionTextureSwapAnimationPictureBox.Name = "FunctionTextureSwapAnimationPictureBox";
            this.FunctionTextureSwapAnimationPictureBox.Size = new System.Drawing.Size(85, 76);
            this.FunctionTextureSwapAnimationPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FunctionTextureSwapAnimationPictureBox.TabIndex = 83;
            this.FunctionTextureSwapAnimationPictureBox.TabStop = false;
            // 
            // FunctionTextureSwapAnimationImage
            // 
            this.FunctionTextureSwapAnimationImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FunctionTextureSwapAnimationImage.FormattingEnabled = true;
            this.FunctionTextureSwapAnimationImage.Location = new System.Drawing.Point(177, 62);
            this.FunctionTextureSwapAnimationImage.Name = "FunctionTextureSwapAnimationImage";
            this.FunctionTextureSwapAnimationImage.Size = new System.Drawing.Size(202, 21);
            this.FunctionTextureSwapAnimationImage.TabIndex = 81;
            this.FunctionTextureSwapAnimationImage.SelectionChangeCommitted += new System.EventHandler(this.FunctionTextureSwapAnimationImage_SelectionChangeCommitted);
            // 
            // FunctionTextureSwapAnimationDown
            // 
            this.FunctionTextureSwapAnimationDown.Location = new System.Drawing.Point(174, 31);
            this.FunctionTextureSwapAnimationDown.Name = "FunctionTextureSwapAnimationDown";
            this.FunctionTextureSwapAnimationDown.Size = new System.Drawing.Size(26, 23);
            this.FunctionTextureSwapAnimationDown.TabIndex = 80;
            this.FunctionTextureSwapAnimationDown.Text = "▼";
            this.FunctionTextureSwapAnimationDown.UseVisualStyleBackColor = true;
            this.FunctionTextureSwapAnimationDown.Click += new System.EventHandler(this.FunctionTextureSwapAnimationDown_Click);
            // 
            // FunctionTextureSwapAnimationUp
            // 
            this.FunctionTextureSwapAnimationUp.Location = new System.Drawing.Point(174, 5);
            this.FunctionTextureSwapAnimationUp.Name = "FunctionTextureSwapAnimationUp";
            this.FunctionTextureSwapAnimationUp.Size = new System.Drawing.Size(26, 23);
            this.FunctionTextureSwapAnimationUp.TabIndex = 79;
            this.FunctionTextureSwapAnimationUp.Text = "▲";
            this.FunctionTextureSwapAnimationUp.UseVisualStyleBackColor = true;
            this.FunctionTextureSwapAnimationUp.Click += new System.EventHandler(this.FunctionTextureSwapAnimationUp_Click);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(209, 36);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(85, 13);
            this.label117.TabIndex = 77;
            this.label117.Text = "Frame Duration: ";
            // 
            // FunctionTextureSwapAnimationDelete
            // 
            this.FunctionTextureSwapAnimationDelete.Location = new System.Drawing.Point(290, 5);
            this.FunctionTextureSwapAnimationDelete.Name = "FunctionTextureSwapAnimationDelete";
            this.FunctionTextureSwapAnimationDelete.Size = new System.Drawing.Size(86, 23);
            this.FunctionTextureSwapAnimationDelete.TabIndex = 76;
            this.FunctionTextureSwapAnimationDelete.Text = "Delete Image";
            this.FunctionTextureSwapAnimationDelete.UseVisualStyleBackColor = true;
            this.FunctionTextureSwapAnimationDelete.Click += new System.EventHandler(this.FunctionTextureSwapAnimationDelete_Click);
            // 
            // FunctionTextureSwapAnimationAdd
            // 
            this.FunctionTextureSwapAnimationAdd.Location = new System.Drawing.Point(202, 5);
            this.FunctionTextureSwapAnimationAdd.Name = "FunctionTextureSwapAnimationAdd";
            this.FunctionTextureSwapAnimationAdd.Size = new System.Drawing.Size(82, 23);
            this.FunctionTextureSwapAnimationAdd.TabIndex = 75;
            this.FunctionTextureSwapAnimationAdd.Text = "Add Image";
            this.FunctionTextureSwapAnimationAdd.UseVisualStyleBackColor = true;
            this.FunctionTextureSwapAnimationAdd.Click += new System.EventHandler(this.FunctionTextureSwapAnimationAdd_Click);
            // 
            // FunctionTextureSwapAnimationDuration
            // 
            this.FunctionTextureSwapAnimationDuration.AlwaysFireValueChanged = false;
            this.FunctionTextureSwapAnimationDuration.DisplayDigits = 1;
            this.FunctionTextureSwapAnimationDuration.DoValueRollover = false;
            this.FunctionTextureSwapAnimationDuration.Enabled = false;
            this.FunctionTextureSwapAnimationDuration.IncrementMouseWheel = 1;
            this.FunctionTextureSwapAnimationDuration.Location = new System.Drawing.Point(300, 36);
            this.FunctionTextureSwapAnimationDuration.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.FunctionTextureSwapAnimationDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FunctionTextureSwapAnimationDuration.Name = "FunctionTextureSwapAnimationDuration";
            this.FunctionTextureSwapAnimationDuration.ShiftMultiplier = 1;
            this.FunctionTextureSwapAnimationDuration.Size = new System.Drawing.Size(59, 20);
            this.FunctionTextureSwapAnimationDuration.TabIndex = 78;
            this.FunctionTextureSwapAnimationDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FunctionTextureSwapAnimationDuration.ValueChanged += new System.EventHandler(this.FunctionTextureSwapAnimationDuration_ValueChanged);
            // 
            // FunctionTextureSwapAnimationList
            // 
            this.FunctionTextureSwapAnimationList.FormattingEnabled = true;
            this.FunctionTextureSwapAnimationList.IntegralHeight = false;
            this.FunctionTextureSwapAnimationList.Location = new System.Drawing.Point(8, 3);
            this.FunctionTextureSwapAnimationList.Name = "FunctionTextureSwapAnimationList";
            this.FunctionTextureSwapAnimationList.Size = new System.Drawing.Size(160, 162);
            this.FunctionTextureSwapAnimationList.TabIndex = 74;
            this.FunctionTextureSwapAnimationList.Click += new System.EventHandler(this.FunctionTextureSwapAnimationList_Click);
            // 
            // tabCameraEffect
            // 
            this.tabCameraEffect.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabCameraEffect.Controls.Add(this.FunctionCameraEffectDropdown);
            this.tabCameraEffect.Controls.Add(this.label120);
            this.tabCameraEffect.Location = new System.Drawing.Point(4, 21);
            this.tabCameraEffect.Name = "tabCameraEffect";
            this.tabCameraEffect.Size = new System.Drawing.Size(383, 173);
            this.tabCameraEffect.TabIndex = 4;
            this.tabCameraEffect.Text = "tabPage1";
            // 
            // FunctionCameraEffectDropdown
            // 
            this.FunctionCameraEffectDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FunctionCameraEffectDropdown.FormattingEnabled = true;
            this.FunctionCameraEffectDropdown.Location = new System.Drawing.Point(96, 14);
            this.FunctionCameraEffectDropdown.Name = "FunctionCameraEffectDropdown";
            this.FunctionCameraEffectDropdown.Size = new System.Drawing.Size(202, 21);
            this.FunctionCameraEffectDropdown.TabIndex = 83;
            this.FunctionCameraEffectDropdown.SelectionChangeCommitted += new System.EventHandler(this.FunctionCameraEffectDropdown_SelectionChangeCommitted);
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(5, 17);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(77, 13);
            this.label120.TabIndex = 82;
            this.label120.Text = "Camera Effect:";
            // 
            // tabConditionalDraw
            // 
            this.tabConditionalDraw.Location = new System.Drawing.Point(4, 21);
            this.tabConditionalDraw.Name = "tabConditionalDraw";
            this.tabConditionalDraw.Size = new System.Drawing.Size(383, 173);
            this.tabConditionalDraw.TabIndex = 5;
            this.tabConditionalDraw.Text = "tabPage1";
            this.tabConditionalDraw.UseVisualStyleBackColor = true;
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(4, 205);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(34, 13);
            this.label135.TabIndex = 52;
            this.label135.Text = "Type:";
            // 
            // RenderFunctionType
            // 
            this.RenderFunctionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RenderFunctionType.FormattingEnabled = true;
            this.RenderFunctionType.Location = new System.Drawing.Point(41, 200);
            this.RenderFunctionType.Name = "RenderFunctionType";
            this.RenderFunctionType.Size = new System.Drawing.Size(206, 21);
            this.RenderFunctionType.TabIndex = 53;
            this.RenderFunctionType.Tag = "";
            this.RenderFunctionType.SelectionChangeCommitted += new System.EventHandler(this.RenderFunctionType_SelectionChangeCommitted);
            // 
            // RenderFunctionDown
            // 
            this.RenderFunctionDown.Location = new System.Drawing.Point(371, 22);
            this.RenderFunctionDown.Name = "RenderFunctionDown";
            this.RenderFunctionDown.Size = new System.Drawing.Size(26, 23);
            this.RenderFunctionDown.TabIndex = 51;
            this.RenderFunctionDown.Text = "▼";
            this.RenderFunctionDown.UseVisualStyleBackColor = true;
            this.RenderFunctionDown.Click += new System.EventHandler(this.RenderFunctionDown_Click);
            // 
            // RenderFunctionUp
            // 
            this.RenderFunctionUp.Location = new System.Drawing.Point(339, 22);
            this.RenderFunctionUp.Name = "RenderFunctionUp";
            this.RenderFunctionUp.Size = new System.Drawing.Size(26, 23);
            this.RenderFunctionUp.TabIndex = 50;
            this.RenderFunctionUp.Text = "▲";
            this.RenderFunctionUp.UseVisualStyleBackColor = true;
            this.RenderFunctionUp.Click += new System.EventHandler(this.RenderFunctionUp_Click);
            // 
            // DeleteRenderFunction
            // 
            this.DeleteRenderFunction.Location = new System.Drawing.Point(227, 22);
            this.DeleteRenderFunction.Name = "DeleteRenderFunction";
            this.DeleteRenderFunction.Size = new System.Drawing.Size(99, 23);
            this.DeleteRenderFunction.TabIndex = 49;
            this.DeleteRenderFunction.Text = "Delete Function";
            this.DeleteRenderFunction.UseVisualStyleBackColor = true;
            this.DeleteRenderFunction.Click += new System.EventHandler(this.DeleteRenderFunction_Click);
            // 
            // AddRenderFunction
            // 
            this.AddRenderFunction.Location = new System.Drawing.Point(125, 22);
            this.AddRenderFunction.Name = "AddRenderFunction";
            this.AddRenderFunction.Size = new System.Drawing.Size(96, 23);
            this.AddRenderFunction.TabIndex = 48;
            this.AddRenderFunction.Text = "Add Function";
            this.AddRenderFunction.UseVisualStyleBackColor = true;
            this.AddRenderFunction.Click += new System.EventHandler(this.AddRenderFunction_Click);
            // 
            // RenderFunctionSelect
            // 
            this.RenderFunctionSelect.FormattingEnabled = true;
            this.RenderFunctionSelect.IntegralHeight = false;
            this.RenderFunctionSelect.Location = new System.Drawing.Point(6, 51);
            this.RenderFunctionSelect.Name = "RenderFunctionSelect";
            this.RenderFunctionSelect.Size = new System.Drawing.Size(391, 145);
            this.RenderFunctionSelect.TabIndex = 47;
            this.RenderFunctionSelect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RenderFunctionSelect_MouseClick);
            this.RenderFunctionSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenderFunctionSelect_KeyDown);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // GlobalRomRefresh
            // 
            this.GlobalRomRefresh.Image = ((System.Drawing.Image)(resources.GetObject("GlobalRomRefresh.Image")));
            this.GlobalRomRefresh.Location = new System.Drawing.Point(583, 2);
            this.GlobalRomRefresh.Name = "GlobalRomRefresh";
            this.GlobalRomRefresh.Size = new System.Drawing.Size(22, 22);
            this.GlobalRomRefresh.TabIndex = 49;
            this.EnvironmentControlTooltip.SetToolTip(this.GlobalRomRefresh, "Reload actor, object and exit cache");
            this.GlobalRomRefresh.UseVisualStyleBackColor = true;
            this.GlobalRomRefresh.Visible = false;
            this.GlobalRomRefresh.Click += new System.EventHandler(this.DatabaseButton_Click);
            // 
            // Z64RomPlay
            // 
            this.Z64RomPlay.Image = global::SharpOcarina.Properties.Resources.play;
            this.Z64RomPlay.Location = new System.Drawing.Point(555, 2);
            this.Z64RomPlay.Name = "Z64RomPlay";
            this.Z64RomPlay.Size = new System.Drawing.Size(22, 22);
            this.Z64RomPlay.TabIndex = 54;
            this.EnvironmentControlTooltip.SetToolTip(this.Z64RomPlay, "Inject and launch the ROM");
            this.Z64RomPlay.UseVisualStyleBackColor = true;
            this.Z64RomPlay.Visible = false;
            this.Z64RomPlay.Click += new System.EventHandler(this.Z64RomPlay_Click);
            // 
            // label86
            // 
            this.label86.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(1028, 6);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(79, 13);
            this.label86.TabIndex = 44;
            this.label86.Text = "Scene Header:";
            // 
            // CDILink
            // 
            this.CDILink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CDILink.BackColor = System.Drawing.Color.Black;
            this.CDILink.Location = new System.Drawing.Point(13, 507);
            this.CDILink.Name = "CDILink";
            this.CDILink.Size = new System.Drawing.Size(1, 1);
            this.CDILink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.CDILink.TabIndex = 45;
            this.CDILink.TabStop = false;
            this.CDILink.Visible = false;
            // 
            // labelcamerapos
            // 
            this.labelcamerapos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelcamerapos.AutoSize = true;
            this.labelcamerapos.Location = new System.Drawing.Point(1120, 746);
            this.labelcamerapos.Name = "labelcamerapos";
            this.labelcamerapos.Size = new System.Drawing.Size(34, 13);
            this.labelcamerapos.TabIndex = 46;
            this.labelcamerapos.Text = "X Y Z";
            this.labelcamerapos.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelcamerapos.Visible = false;
            // 
            // UpdateLabel
            // 
            this.UpdateLabel.AutoSize = true;
            this.UpdateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateLabel.Location = new System.Drawing.Point(384, 2);
            this.UpdateLabel.Name = "UpdateLabel";
            this.UpdateLabel.Size = new System.Drawing.Size(150, 20);
            this.UpdateLabel.TabIndex = 47;
            this.UpdateLabel.Text = "Update Available!";
            this.UpdateLabel.Visible = false;
            // 
            // RomModeLabel
            // 
            this.RomModeLabel.AutoSize = true;
            this.RomModeLabel.Location = new System.Drawing.Point(611, 6);
            this.RomModeLabel.Name = "RomModeLabel";
            this.RomModeLabel.Size = new System.Drawing.Size(121, 13);
            this.RomModeLabel.TabIndex = 48;
            this.RomModeLabel.Text = "Global ROM Mode: OFF";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(799, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "Viewport FOV:";
            // 
            // label136
            // 
            this.label136.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label136.AutoSize = true;
            this.label136.Location = new System.Drawing.Point(937, 7);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(38, 13);
            this.label136.TabIndex = 53;
            this.label136.Text = "Room:";
            // 
            // RoomSelector
            // 
            this.RoomSelector.AlwaysFireValueChanged = false;
            this.RoomSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RoomSelector.DisplayDigits = 1;
            this.RoomSelector.DoValueRollover = false;
            this.RoomSelector.IncrementMouseWheel = 1;
            this.RoomSelector.Location = new System.Drawing.Point(981, 4);
            this.RoomSelector.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RoomSelector.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RoomSelector.Name = "RoomSelector";
            this.RoomSelector.ShiftMultiplier = 1;
            this.RoomSelector.Size = new System.Drawing.Size(41, 20);
            this.RoomSelector.TabIndex = 52;
            this.RoomSelector.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RoomSelector.ValueChanged += new System.EventHandler(this.RoomSelector_ValueChanged);
            // 
            // ViewportFOV
            // 
            this.ViewportFOV.AlwaysFireValueChanged = false;
            this.ViewportFOV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewportFOV.DecimalPlaces = 7;
            this.ViewportFOV.DisplayDigits = 1;
            this.ViewportFOV.DoValueRollover = false;
            this.ViewportFOV.IncrementMouseWheel = 1;
            this.ViewportFOV.Location = new System.Drawing.Point(878, 4);
            this.ViewportFOV.Maximum = new decimal(new int[] {
            179,
            0,
            0,
            0});
            this.ViewportFOV.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ViewportFOV.Name = "ViewportFOV";
            this.ViewportFOV.ShiftMultiplier = 1;
            this.ViewportFOV.Size = new System.Drawing.Size(53, 20);
            this.ViewportFOV.TabIndex = 50;
            this.ViewportFOV.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.ViewportFOV.ValueChanged += new System.EventHandler(this.numericUpDownEx1_ValueChanged_2);
            // 
            // SceneHeaderSelector
            // 
            this.SceneHeaderSelector.AlwaysFireValueChanged = false;
            this.SceneHeaderSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SceneHeaderSelector.DisplayDigits = 1;
            this.SceneHeaderSelector.DoValueRollover = false;
            this.SceneHeaderSelector.Hexadecimal = true;
            this.SceneHeaderSelector.IncrementMouseWheel = 1;
            this.SceneHeaderSelector.Location = new System.Drawing.Point(1113, 4);
            this.SceneHeaderSelector.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SceneHeaderSelector.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SceneHeaderSelector.Name = "SceneHeaderSelector";
            this.SceneHeaderSelector.ShiftMultiplier = 1;
            this.SceneHeaderSelector.Size = new System.Drawing.Size(41, 20);
            this.SceneHeaderSelector.TabIndex = 43;
            this.SceneHeaderSelector.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SceneHeaderSelector.ValueChanged += new System.EventHandler(this.SceneHeaderSelector_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1161, 759);
            this.Controls.Add(this.Z64RomPlay);
            this.Controls.Add(this.label136);
            this.Controls.Add(this.RoomSelector);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ViewportFOV);
            this.Controls.Add(this.GlobalRomRefresh);
            this.Controls.Add(this.RomModeLabel);
            this.Controls.Add(this.UpdateLabel);
            this.Controls.Add(this.labelcamerapos);
            this.Controls.Add(this.CDILink);
            this.Controls.Add(this.label86);
            this.Controls.Add(this.SceneHeaderSelector);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1094, 568);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "a";
            this.Text = "---";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScenenumberTextbox)).EndInit();
            this.CamerasGroupBox.ResumeLayout(false);
            this.CameraPanel.ResumeLayout(false);
            this.CameraPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraFov)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraZRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraXRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraYPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraYRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraXPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraSelect)).EndInit();
            this.CameraPanel2.ResumeLayout(false);
            this.CameraPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk1E)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk1C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk1A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraUnk12)).EndInit();
            this.WaterboxGroupBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxRoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxCam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxEnv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxXSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxYPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxYSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxXPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaterboxSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleNumericbox)).EndInit();
            this.TextureAnimsGroupBox.ResumeLayout(false);
            this.TextureAnimsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimHeight2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimWidth2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimYVelocity2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimXVelocity2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimHeight1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimWidth1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimYVelocity1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimXVelocity1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextureAnimSelect)).EndInit();
            this.tabRooms.ResumeLayout(false);
            this.tabRooms.PerformLayout();
            this.AdditionalTexturesGroupBox.ResumeLayout(false);
            this.AdditionalTexturesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalTextureList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupLODDIstance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupLODGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupAnimatedBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftTNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShiftSNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupMultitextureAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupPolygonType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.tabSceneEnv.ResumeLayout(false);
            this.tabSceneEnv.PerformLayout();
            this.PrerenderedGroupBox.ResumeLayout(false);
            this.PrerenderedGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrerenderedList)).EndInit();
            this.AlternateHeadersGroupBox.ResumeLayout(false);
            this.AlternateHeadersGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SceneHeaderCopyList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SceneHeaderList)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionBZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionBY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionAZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionAY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionBX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentDirectionAX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrawDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FogUnknown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FogDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightingE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FogColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightingC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightingA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentSelect)).EndInit();
            this.tabRoomEnv.ResumeLayout(false);
            this.tabRoomEnv.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightNS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightYPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightEW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightXPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalLightSelect)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WindStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindSouth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindWest)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SoundEcho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeHour)).EndInit();
            this.tabCollision.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ExitGroupBox.ResumeLayout(false);
            this.ExitGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExitHeaderIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitSceneIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitSpawnIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitFadeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitFadeIn)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PolytypeUnk2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PolytypeUnk1)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraAngleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitNumber)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TerrainType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroundType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EchoRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PolygonSelect)).EndInit();
            this.tabTransitions.ResumeLayout(false);
            this.tabPathways.ResumeLayout(false);
            this.ActorCutsceneGroupBox.ResumeLayout(false);
            this.ActorCutsceneGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneUnknown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneAdditionalActorCs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneCsIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActorCutsceneLength)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.PathwayZPosStrip.ResumeLayout(false);
            this.PathwayZPosStrip.PerformLayout();
            this.PathwayYPosStrip.ResumeLayout(false);
            this.PathwayYPosStrip.PerformLayout();
            this.PathwayXPosStrip.ResumeLayout(false);
            this.PathwayXPosStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PathwayZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathwayNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathwayXPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathwayYPos)).EndInit();
            this.tabActors.ResumeLayout(false);
            this.tabActors.PerformLayout();
            this.ObjectTabMenu.ResumeLayout(false);
            this.RoomObjectPage.ResumeLayout(false);
            this.SceneObjectPage.ResumeLayout(false);
            this.tabCutscene.ResumeLayout(false);
            this.tabCutscene.PerformLayout();
            this.CutsceneGroupBox.ResumeLayout(false);
            this.CutsceneGroupBox.PerformLayout();
            this.CutsceneTabs.ResumeLayout(false);
            this.CameraPositions.ResumeLayout(false);
            this.CameraPositions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionAngleView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutscenePositionFrameDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionCameraRoll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutscenePositionZFocus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutscenePositionXFocus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutscenePositionYFocus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneAbsolutePositionY)).EndInit();
            this.SpecialExecution.ResumeLayout(false);
            this.SpecialExecution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneSetTimeHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneSetTimeMinutes)).EndInit();
            this.Textbox.ResumeLayout(false);
            this.Textbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneTextboxFrames)).EndInit();
            this.TransitionEffect.ResumeLayout(false);
            this.TransitionEffect.PerformLayout();
            this.AsmExecution.ResumeLayout(false);
            this.AsmExecution.PerformLayout();
            this.ActorCommand.ResumeLayout(false);
            this.ActorCommand.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorFrameDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorZRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorXRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorYRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorZEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorXEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorYEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorZStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorXStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneActorYStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneTableEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneSpawn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CutsceneEntrance)).EndInit();
            this.tabAnimations.ResumeLayout(false);
            this.tabAnimations.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.RenderFunctionGroupBoxFlag.ResumeLayout(false);
            this.RenderFunctionGroupBoxFlag.PerformLayout();
            this.RenderFunctionFlagPresetToolStrip.ResumeLayout(false);
            this.RenderFunctionFlagPresetToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RenderFunctionFlagID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RenderFunctionID)).EndInit();
            this.RenderFunctionTabs.ResumeLayout(false);
            this.tabTextureScroll.ResumeLayout(false);
            this.tabTextureScroll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollHeight2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollXVelocity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollWidth2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollYVelocity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollYVelocity2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollXVelocity2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureScrollHeight)).EndInit();
            this.tabColorBlending.ResumeLayout(false);
            this.tabColorBlending.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionColorBlendColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionColorBlendAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionColorBlendFrames)).EndInit();
            this.tabTextureSwap.ResumeLayout(false);
            this.tabTextureSwap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureSwapPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureSwapPictureBox)).EndInit();
            this.tabTextureSwapFrames.ResumeLayout(false);
            this.tabTextureSwapFrames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureSwapAnimationPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctionTextureSwapAnimationDuration)).EndInit();
            this.tabCameraEffect.ResumeLayout(false);
            this.tabCameraEffect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CDILink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoomSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewportFOV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SceneHeaderSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectToROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBinaryToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabRooms;
        private System.Windows.Forms.TabPage tabActors;
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ScaleNumericbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DeleteRoom;
        private System.Windows.Forms.Button AddRoom;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox CollisionTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCollisionModelToolStripMenuItem;
        private System.Windows.Forms.ListBox GroupList;
        private System.Windows.Forms.ListBox RoomList;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private SharpOcarina.NumericTextBox InjectoffsetTextbox;
        private System.Windows.Forms.Label label6;
        private SharpOcarina.NumericTextBox RoomInjectionOffset;
        private System.Windows.Forms.ToolStripMenuItem showRoomModelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoReload;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabPage tabTransitions;
        private System.Windows.Forms.GroupBox WaterboxGroupBox;
        private System.Windows.Forms.Button DeletewaterboxButton;
        private System.Windows.Forms.Button AddwaterboxButton;
        private System.Windows.Forms.NumericUpDown WaterboxSelect;
        private System.Windows.Forms.Label label18;
        private SharpOcarina.NumericUpDownEx WaterboxYSize;
        private System.Windows.Forms.Label label20;
        private SharpOcarina.NumericUpDownEx WaterboxXPos;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private SharpOcarina.NumericUpDownEx WaterboxYPos;
        private SharpOcarina.NumericUpDownEx WaterboxXSize;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private SharpOcarina.NumericUpDownEx WaterboxZPos;
        private SharpOcarina.NiceLine niceLine2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem applyEnvironmentLightingToolStripMenuItem;
        private System.Windows.Forms.Label label33;
        private ActorEditControl actorEditControl1;
        private ActorEditControl actorEditControl3;
        private ActorEditControl actorEditControl2;
        private System.Windows.Forms.ToolStripMenuItem consecutiveRoomInjectionToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private NumericTextBox PolygonRawdata;
        private System.Windows.Forms.NumericUpDown PolygonSelect;
        private System.Windows.Forms.Button DeletepolygonButton;
        private System.Windows.Forms.Button AddpolygonButton;
        private System.Windows.Forms.TabPage tabCollision;
        private NiceLine niceLine1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown GroupPolygonType;
        private System.Windows.Forms.Label label12;
        private NiceLine niceLine4;
        private NumericUpDownEx EnvironmentType;
        private System.Windows.Forms.Label label14;
        private NumericUpDownEx EchoRange;
        private System.Windows.Forms.Label label13;
        private NumericUpDownEx GroundType;
        private System.Windows.Forms.Label label16;
        private NumericUpDownEx TerrainType;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox SteepterrainCheckbox;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button AddexitButton;
        private System.Windows.Forms.Button DeleteexitButton;
        private System.Windows.Forms.ToolStripMenuItem showReadmeToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox3;
        private NumericUpDownEx ExitNumber;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox HookshotableCheckbox;
        private System.Windows.Forms.ComboBox MultiTextureComboBox;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.ToolStripMenuItem forceRGBATexturesToolStripMenuItem;
        private System.Windows.Forms.CheckBox SimulateN64CheckBox;
        private System.Windows.Forms.ComboBox SongComboBox;
        private System.Windows.Forms.ToolStripMenuItem DegreesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoaddGroupsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extraToolStripMenuItem;
        private System.Windows.Forms.Label RoomObjectSpace;
        private TabPage tabPathways;
        private TabPage tabSceneEnv;
        private GroupBox groupBox5;
        private Panel panel3;
        private Label label32;
        private Label label31;
        private PictureBox LightingE;
        private PictureBox FogColor;
        private Label label28;
        private PictureBox LightingC;
        private PictureBox LightingA;
        private Label label26;
        private Label label19;
        private Button button11;
        private Button button12;
        private NumericUpDown EnvironmentSelect;
        private NiceLine niceLine3;
        private GroupBox groupBox7;
        private CheckBox SkyboxCheckBox;
        private ComboBox NightSFXComboBox;
        private Label label35;
        private GroupBox groupBox8;
        private Label label41;
        private Label label40;
        private Label label39;
        private ComboBox SkyboxComboBox;
        private CheckBox CloudyCheckBox;
        private ComboBox SpecialObjectComboBox;
        private Label label38;
        private ComboBox ElfMessageComboBox;
        private Label label42;
        private CheckBox SunmoonCheckBox;
        private Label label43;
        private ToolTip EnvironmentControlTooltip;
        private GroupBox groupBox10;
        private Label label45;
        private ComboBox WorldMapComboBox;
        private Label label44;
        private ComboBox CameraMovementComboBox;
        private TabPage tabRoomEnv;
        private GroupBox groupBox11;
        private CheckBox WarpsongsCheckBox;
        private CheckBox InvisibleActorsCheckBox;
        private Label label52;
        private ComboBox IdleAnimComboBox;
        private Label label50;
        private ComboBox RestrictionComboBox;
        private GroupBox groupBox9;
        private Label label48;
        private Label label49;
        private Label label47;
        private Label label46;
        private GroupBox groupBox12;
        private Panel panel6;
        private PictureBox AdditionalLightColor;
        private Label AdditionalLightDirectionLabel3;
        private NumericUpDownEx AdditionalLightRadius;
        private Label AdditionalLightLabel1;
        private Label AdditionalLightPointLabel1;
        private NumericUpDownEx AdditionalLightZPos;
        private Label AdditionalLightPointLabel3;
        private Label AdditionalLightPointLabel2;
        private NumericUpDownEx AdditionalLightNS;
        private Label AdditionalLightDirectionLabel2;
        private NumericUpDownEx AdditionalLightYPos;
        private NumericUpDownEx AdditionalLightEW;
        private Label AdditionalLightDirectionLabel1;
        private NumericUpDownEx AdditionalLightXPos;
        private Button AdditionalLightDelete;
        private Button AdditionalLightAdd;
        private NumericUpDown AdditionalLightSelect;
        private NiceLine niceLine5;
        private GroupBox groupBox13;
        private ListBox PathwayListBox;
        private Button PathwayDeleteButton;
        private Button PathwayAddButton;
        private Label PathwayLabel1;
        private NumericUpDownEx PathwayZPos;
        private NumericUpDown PathwayNumber;
        private Label PathwayLabel3;
        private NiceLine niceLine6;
        private Label PathwayLabel2;
        private NumericUpDownEx PathwayXPos;
        private NumericUpDownEx PathwayYPos;
        private CheckBox PointLightCheckBox;
        private Button DeletePointButton;
        private Button AddPointButton;
        private ToolStrip PathwayZPosStrip;
        private ToolStripDropDownButton toolStripDropDownButton3;
        private ToolStripMenuItem StickToZplus;
        private ToolStripMenuItem StickToZminus;
        private ToolStrip PathwayYPosStrip;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem StickToYplus;
        private ToolStripMenuItem StickToYminus;
        private ToolStrip PathwayXPosStrip;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem StickToXplus;
        private ToolStripMenuItem StickToXminus;
        private Label SceneFunctionLabel;
        private ToolStripMenuItem DisplayAxisMenuItem;
        private ToolStripMenuItem DefaultEnvironmentMenuItem;
        private ToolStripMenuItem HWWindMenuItem;
        private ToolStripMenuItem CopyFirstRoomSettingsMenuItem;
        private CheckBox UnusedCommandCheckBox;
        private CheckBox GroupAnimated;
        private ToolStripMenuItem setRoomsToUseEnvironment1ToolStripMenuItem;
        private RadioButton KillingLavaRadioButton;
        private RadioButton LadderTopRadioButton;
        private NumericUpDownEx CameraAngleNumeric;
        private Label CameraAngleLabel;
        private RadioButton CrawlSpaceRadio;
        private RadioButton NoFallDamageRadio;
        private RadioButton JabuJabuRadio;
        private NiceLine niceLine7;
        private RadioButton VoidCheckBox;
        private ToolStripMenuItem showControlsToolStripMenuItem;
        private Label WaterboxRoomLabel;
        private ToolStripMenuItem autoplaceDoorsToolStripMenuItem;
        private TabPage tabCutscene;
        private GroupBox CutsceneGroupBox;
        private Label MarkerTypeLabel;
        private ComboBox MarkerType;
        private Button MarkerDown;
        private Button MarkerUp;
        private Button DeleteMarker;
        private Button AddMarker;
        private ListBox MarkerSelect;
        private TabControl CutsceneTabs;
        private TabPage CameraPositions;
        private TabPage SpecialExecution;
        private TabPage Unknown;
        private TabPage Textbox;
        private TabPage TransitionEffect;
        private TabPage AsmExecution;
        private TabPage ActorCommand;
        private Button CutsceneDeleteAbsolutePosition;
        private Button CutsceneAddAbsolutePosition;
        private ListBox CutsceneAbsolutePositionListBox;
        private Label label56;
        private NumericUpDownEx CutsceneAbsolutePositionZ;
        private Label label57;
        private Label label58;
        private NumericUpDownEx CutsceneAbsolutePositionX;
        private NumericUpDownEx CutsceneAbsolutePositionY;
        private Label label55;
        private NumericTextBox MarkerStartFrame;
        private Label label54;
        private Label label53;
        private ToolStripMenuItem nokaToolStripMenuItem;
        private ToolStripMenuItem objectTableEditorToolStripMenuItem;
        private Label CutscenePositionXFocusLabel;
        private NumericUpDownEx CutscenePositionZFocus;
        private Label CutscenePositionZFocusLabel;
        private Label CutscenePositionYFocusLabel;
        private NumericUpDownEx CutscenePositionXFocus;
        private NumericUpDownEx CutscenePositionYFocus;
        private NumericTextBox MarkerEndFrame;
        private Label label51;
        private Label label60;
        private Button CutsceneDeleteTextbox;
        private Button CutsceneAddTextbox;
        private ListBox CutsceneTextboxList;
        private NumericTextBox CutsceneTextboxMessageId;
        private Label CutsceneTextboxMessageIdLabel;
        private NumericTextBox CutsceneTextboxBottomMessageID;
        private Label label63;
        private Label CutsceneTextboxFramesLabel;
        private NumericUpDownEx CutsceneTextboxFrames;
        private NumericTextBox CutsceneTextboxTopMessageID;
        private Label label61;
        private ComboBox CutsceneTextboxType;
        private ToolStripMenuItem entranceTableEditorToolStripMenuItem;
        private ToolStripMenuItem clearSceneDmatableToolStripMenuItem;
        private ToolStripMenuItem saveOptionsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem disableWaterboxMouseMovementToolStripMenuItem;
        private ToolStripMenuItem cutsceneTableEditorToolStripMenuItem;
        private CheckBox GroupMetallic;
        private ToolStripMenuItem debugToolStripMenuItem;
        private ToolStripMenuItem createPathwaysForEachBoundingBoxToolStripMenuItem;
        private ComboBox SceneSettingsComboBox;
        private ToolStripMenuItem quickTestToolStripMenuItem;
        private ToolStripMenuItem EnemyTestToolStripMenuItem;
        private ToolStripMenuItem puzzleTestToolStripMenuItem;
        private ToolStripMenuItem dListCullingMenuItem;
        private ToolStripMenuItem updateCRCMenuItem;
        private ToolStripMenuItem RenderChildLinkMenuItem;
        private CheckBox ContinualInject;
        private Button AddMultipleRooms;
        private ComboBox CutsceneActorAnimation;
        private Label CutsceneActorAnimLabel;
        private Label label62;
        private Label label64;
        private Label label65;
        private Label label66;
        private Label label67;
        private Label label68;
        private Label label69;
        private NumericUpDownEx CutsceneActorZEnd;
        private NumericUpDownEx CutsceneActorXEnd;
        private NumericUpDownEx CutsceneActorYEnd;
        private Button CutsceneActorDeleteAction;
        private Button CutsceneActorAddAction;
        private ListBox CutsceneActorListBox;
        private NumericUpDownEx CutsceneActorZStart;
        private NumericUpDownEx CutsceneActorXStart;
        private NumericUpDownEx CutsceneActorYStart;
        private Label label70;
        private ComboBox CutsceneTransitionComboBox;
        private Label CutsceneAsmLabel;
        private ComboBox CutsceneAsmComboBox;
        private ToolStripMenuItem openCutsceneRawDataToolStripMenuItem;
        private ToolStripMenuItem majorasMaskModeexperimentalToolStripMenuItem;
        private GroupBox CamerasGroupBox;
        private Panel CameraPanel;
        private Label label78;
        private NumericUpDownEx CameraFov;
        public ComboBox CameraType;
        private Label label77;
        private NumericUpDownEx CameraZRot;
        private Label label71;
        private Label label72;
        private NumericUpDownEx CameraZPos;
        private Label label73;
        private Label label74;
        private NumericUpDownEx CameraXRot;
        private Label label75;
        private NumericUpDownEx CameraYPos;
        private NumericUpDownEx CameraYRot;
        private Label label76;
        private NumericUpDownEx CameraXPos;
        private Button DeleteCameraButton;
        private Button AddCameraButton;
        private NumericUpDown CameraSelect;
        private NiceLine niceLine8;
        private ToolStripMenuItem replaceSceneTitleCardTextureToolStripMenuItem;
        private NumericUpDown GroupMultitextureAlpha;
        private Label label79;
        private ToolStripMenuItem SaveScenetoolStripMenuItem3;
        private ToolStripMenuItem autosaveSceneXmlToolStripMenuItem;
        private GroupBox AlternateHeadersGroupBox;
        private Button DeleteSceneHeaderButton;
        private Button AddSceneHeaderButton;
        private NumericUpDown SceneHeaderList;
        private NiceLine niceLine9;
        private Label SceneHeaderUsedLabel;
        private CheckBox SceneHeaderSameCheckbox;
        private ToolStripMenuItem LaunchRomToolStripMenuItem;
        private Button ReloadRoomButton;
        private ToolStripMenuItem EasterEggToolStripMenuItem;
        private ToolStripMenuItem dIsplayUndocumentedCutsceneVarsToolStripMenuItem;
        private ToolStripMenuItem exportAszobjToolStripMenuItem;
        private ToolStripMenuItem exportCutsceneRawDataToolStripMenuItem;
        private NumericUpDownEx CutsceneActorFrameDuration;
        private Label label80;
        private Label label81;
        private Label label82;
        private NumericUpDownEx CutsceneActorZRot;
        private NumericUpDownEx CutsceneActorXRot;
        private NumericUpDownEx CutsceneActorYRot;
        private NumericUpDownEx CutsceneAbsolutePositionAngleView;
        private NumericUpDownEx CutscenePositionFrameDuration;
        private NumericUpDownEx CutsceneAbsolutePositionCameraRoll;
        private Label label83;
        private Label label84;
        private NumericUpDownEx CutsceneSetTimeHours;
        private NumericUpDownEx CutsceneSetTimeMinutes;
        private ToolStripMenuItem restrictionFlagsTableEditorToolStripMenuItem;
        private GroupBox AdditionalTexturesGroupBox;
        private Label AdditionalTextureLabel;
        private Button DeleteAdditionalTexture;
        private Button AddAdditionalTexture;
        private NumericUpDown AdditionalTextureList;
        private NumericUpDownEx DrawDistance;
        private NumericUpDownEx FogUnknown;
        private NumericUpDownEx FogDistance;
        private Label label85;
        private NumericUpDownEx ScenenumberTextbox;
        private ToolStripMenuItem noDummyPointsInCutsceneCamerasToolStripMenuItem;
        private ToolStripMenuItem importEnvironmentsToolStripMenuItem;
        private ToolStripMenuItem importActorsAndObjectsOfZmapToolStripMenuItem;
        private NumericUpDownEx SceneHeaderSelector;
        private Label label86;
        private ToolStripMenuItem printOffsetsOnInjectToolStripMenuItem;
        private Label label87;
        private Panel panel7;
        private CheckBox GroupDetectionA8;
        private CheckBox GroupDetectionA4;
        private CheckBox GroupDetectionA2;
        private NiceLine niceLine10;
        private ToolStripMenuItem bank0x06ToolStripMenuItem;
        private ToolStripMenuItem bank0x05ToolStripMenuItem;
        private ToolStripMenuItem bank0x04ToolStripMenuItem;
        private ToolStripMenuItem sharpPixelatedTexturesToolStripMenuItem;
        private NumericUpDownEx PolytypeUnk2;
        private Label label89;
        private NumericUpDownEx PolytypeUnk1;
        private Label label88;
        private CheckBox GroupDetectionB8;
        private Label label90;
        private CheckBox GroupDetectionB4;
        private CheckBox GroupDetectionB2;
        private RadioButton KillingQuicksand2Radio;
        private NiceLine niceLine11;
        private Label label92;
        private Label label91;
        private NiceLine niceLine12;
        private RadioButton IceRadioButton;
        private ToolStripMenuItem triplicateCollisionBoundsToolStripMenuItem;
        private ToolStripMenuItem addEmptySpaceInSceneHeaderToolStripMenuItem;
        private CheckBox WallDamageCheck;
        private RadioButton NoLedgeClimbRadio;
        private RadioButton NoLedgeJumpRadio;
        private ToolStripMenuItem fileCreationEditorToolStripMenuItem;
        private CheckBox ReverseLightCheckBox;
        private Label label93;
        private NumericUpDown ShiftTNumeric;
        private NumericUpDown ShiftSNumeric;
        private ToolStripMenuItem importCollisionFromzsceneToolStripMenuItem;
        private ToolStripMenuItem AlwaysGenerateCustomDMATableOnInjectToolStripMenuItem;
        private ToolStripMenuItem removeAllRomScenesToolStripMenuItem;
        private ToolStripMenuItem rebuildDmaTableallToolStripMenuItem;
        private ToolStripMenuItem writeCollisionToolStripMenuItem;
        private NumericUpDownEx WaterboxCam;
        private Label label94;
        private NumericUpDownEx WaterboxEnv;
        private NumericUpDownEx EnvironmentDirectionBX;
        private NumericUpDownEx EnvironmentDirectionAX;
        private CheckBox GroupDecal;
        private ToolStripMenuItem decompressROMToolStripMenuItem;
        private NumericUpDownEx CutsceneEntrance;
        private Label CutsceneEntranceLabel;
        private NumericUpDownEx CutsceneFlag;
        private Label CutsceneFlagLabel;
        private NumericUpDownEx CutsceneSpawn;
        private Label CutsceneSpawnLabel;
        private ToolStripMenuItem ZmapOffsetNames;
        private ToolStripMenuItem importCamerasAndWaterboxFromzsceneToolStripMenuItem;
        private CheckBox GroupPixelated;
        private ToolStripMenuItem showRotationValuesAsHexadecimalToolStripMenuItem;
        private PictureBox CDILink;
        private Panel panel8;
        private RadioButton NoMiscRadioButton;
        private RadioButton AutograbClimbRadioButton;
        private RadioButton DiveRadioButton;
        private RadioButton SmallVoidRadioButton;
        private CheckBox BlockEponaCheckBox;
        private CheckBox Lower1UnitChecbox;
        private Label labelcamerapos;
        private Button CutscenePositionViewMode;
        private Button CutscenePositionDown;
        private Button CutscenePositionUp;
        private Button CutscenePositionCopyCamera;
        private Button CutsceneActorDown;
        private Button CutsceneActorUp;
        private Button PathwayDown;
        private Button PathwayUp;
        private Button CutsceneTextboxDown;
        private Button CutsceneTextboxUp;
        private CheckBox Group2AxisBillboard;
        private CheckBox GroupBillboard;
        private NumericUpDownEx CutsceneTableEntry;
        private Label CutsceneTableEntryLabel;
        private ToolStripMenuItem iGotACrashToolStripMenuItem;
        private ToolStripMenuItem clearAllGroupSettingsToolStripMenuItem;
        private ToolStripMenuItem importTransitionsAndSpawnsFromzsceneToolStripMenuItem;
        private ToolStripMenuItem importPathwaysFromzsceneToolStripMenuItem;
        private Label UpdateLabel;
        private ToolStripMenuItem displaySwitchFlagsUsedByAllRoomsToolStripMenuItem;
        private CheckBox GroupIgnoreFog;
        private ToolStripMenuItem pauseScreenMapEditorOoTToolStripMenuItem;
        private NumericUpDown GroupAnimatedBank;
        private GroupBox TextureAnimsGroupBox;
        private Label label98;
        private NumericUpDownEx TextureAnimHeight2;
        private Label label99;
        private NumericUpDownEx TextureAnimWidth2;
        private Label label100;
        private NumericUpDownEx TextureAnimYVelocity2;
        private Label label101;
        private NumericUpDownEx TextureAnimXVelocity2;
        private NiceLine niceLine14;
        private Label label96;
        private NumericUpDownEx TextureAnimHeight1;
        private Label label97;
        private NumericUpDownEx TextureAnimWidth1;
        private Label label95;
        private NumericUpDownEx TextureAnimYVelocity1;
        private Label label59;
        private Button DeleteTextureAnim;
        private NumericUpDownEx TextureAnimXVelocity1;
        private Button AddTextureAnim;
        private NiceLine niceLine13;
        private NumericUpDown TextureAnimSelect;
        private ToolStripMenuItem patchROMToolStripMenuItem;
        private ToolStripMenuItem patchROMToExtendRAMAndFixBugsOoTDebugToolStripMenuItem1;
        private ToolStripMenuItem patchROMToRemoveDebugFeaturesOoTDebugToolStripMenuItem1;
        private Label RomModeLabel;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem openZmapToolstrip;
        private Label ExitListLabel;
        private Label PolytypeExitLabel;
        private Button GlobalRomRefresh;
        private Label AnimationLabel;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem dontConvertMultitextureToRGBAToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem CheckEmptyOffsetItem;
        private Label label7;
        private NumericUpDownEx ViewportFOV;
        private ToolStripMenuItem RenderActorstoolStrip;
        private CheckBox GroupSmoothRgbaEdges;
        private NumericUpDown GroupLODDIstance;
        private Label label102;
        private NumericUpDown GroupLODGroup;
        private CheckBox GroupLod;
        private CheckBox GroupRenderLast;
        private ToolStripMenuItem IgnoreMajorasMaskDaySystem;
        private CheckBox GroupEnvColor;
        private Label label104;
        private NumericUpDownEx CameraUnk1;
        private Label label103;
        private NumericUpDownEx CameraUnk2;
        private ToolStripMenuItem RenderWaterboxesMenuItem;
        private ToolStripMenuItem dEBUGPrintRoomActorsToClipboardDunGenToolStripMenuItem;
        private ToolStripMenuItem dEBUGPrintEnvironmentsToClipboardDunGenToolStripMenuItem;
        private ToolStripMenuItem dEBUGPrintRoomActorRenderingToClipboardToolStripMenuItem;
        private CheckBox GroupAlphaMask;
        private CheckBox GroupVertexNormals;
        private ToolStripMenuItem DisableRGBA32ToolStrip;
        private Button CutscenePositionPlayMode;
        private ToolStripMenuItem OpenGlobalROM;
        private ToolStripSeparator toolStripSeparator6;
        private RichTextBox DebugTextBox;
        private GroupBox PrerenderedGroupBox;
        private Button DeleteJFIF;
        private NiceLine niceLine15;
        private Label JFIFLabel;
        private NumericUpDown PrerenderedList;
        private Button LoadJFIF;
        private CheckBox PrerenderedCheckbox;
        private Button CameraCopyViewport;
        private Button CameraView;
        private ToolStripMenuItem AddObjectToAllRoomsMenuItem;
        private ToolStripMenuItem additionalLightsFixOoTDebugToolStripMenuItem;
        private CheckBox Roomaffectedpointlightscheckbox;
        private CheckBox DisableStartTime;
        private NumericUpDownEx TimeMinute;
        private Label label105;
        private NumericUpDownEx TimeHour;
        private ToolStripMenuItem OpenSceneFromRoomToolStrip;
        private ToolStripMenuItem AutoFixErrorsStripMenuItem3;
        private CheckBox AutoInjectOffsetCheckBox;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem AdvancedTextureAnimationsMenuItem;
        private ToolStripMenuItem advancedTextureAnimationsOoTDebugToolStripMenuItem;
        private TabPage tabAnimations;
        private GroupBox groupBox4;
        private Label label137;
        private NumericUpDown RenderFunctionID;
        private TabControl RenderFunctionTabs;
        private TabPage tabTextureScroll;
        private Label label135;
        private ComboBox RenderFunctionType;
        private Button RenderFunctionDown;
        private Button RenderFunctionUp;
        private Button DeleteRenderFunction;
        private Button AddRenderFunction;
        private ListBox RenderFunctionSelect;
        private TabPage tabColorBlending;
        private Label label106;
        private NumericUpDownEx FunctionTextureScrollHeight2;
        private NumericUpDownEx FunctionTextureScrollXVelocity;
        private Label label107;
        private NiceLine niceLine17;
        private NumericUpDownEx FunctionTextureScrollWidth2;
        private Label label113;
        private Label label108;
        private NumericUpDownEx FunctionTextureScrollYVelocity;
        private NumericUpDownEx FunctionTextureScrollYVelocity2;
        private Label label112;
        private Label label109;
        private NumericUpDownEx FunctionTextureScrollWidth;
        private NumericUpDownEx FunctionTextureScrollXVelocity2;
        private Label label111;
        private NiceLine niceLine16;
        private NumericUpDownEx FunctionTextureScrollHeight;
        private Label label110;
        private TabPage tabTextureSwap;
        private TabPage tabTextureSwapFrames;
        private GroupBox RenderFunctionGroupBoxFlag;
        private Label RenderFunctionFlagBitwiseLabel;
        private NumericTextBox RenderFunctionFlagBitwise;
        private CheckBox RenderFunctionFlagReverseCheckbox;
        private Label RenderFunctionFlagLabel;
        private Label label114;
        private NumericUpDownEx RenderFunctionFlagID;
        private ComboBox RenderFunctionFlagType;
        private ToolStrip RenderFunctionFlagPresetToolStrip;
        private ToolStripDropDownButton RenderFunctionFlagPresetButton;
        private Label label115;
        private ComboBox FunctionTextureSwapTextureID;
        private Label label116;
        private ComboBox FunctionTextureSwapTextureID2;
        private PictureBox FunctionTextureSwapPictureBox2;
        private PictureBox FunctionTextureSwapPictureBox;
        private Button FunctionTextureSwapAnimationDown;
        private Button FunctionTextureSwapAnimationUp;
        private NumericUpDownEx FunctionTextureSwapAnimationDuration;
        private Label label117;
        private Button FunctionTextureSwapAnimationDelete;
        private Button FunctionTextureSwapAnimationAdd;
        private ListBox FunctionTextureSwapAnimationList;
        private PictureBox FunctionTextureSwapAnimationPictureBox;
        private ComboBox FunctionTextureSwapAnimationImage;
        private PictureBox FunctionColorBlendColor;
        private Label label119;
        private NumericUpDownEx FunctionColorBlendFrames;
        private Label label118;
        private Button FunctionColorBlendDown;
        private Button FunctionColorBlendUp;
        private Button FunctionColorBlendDelete;
        private Button FunctionColorBlendAdd;
        private ListBox FunctionColorBlendList;
        private ToolStripMenuItem ExtendDynapolyCountStripMenuItem;
        private TabPage tabCameraEffect;
        private ComboBox FunctionCameraEffectDropdown;
        private Label label120;
        private TabPage tabConditionalDraw;
        private CheckBox RenderFunctionFlagFreezeCheckBox;
        private Button RenderFunctionPreview;
        private ToolStripMenuItem ReloadXMLMenuItem;
        private Label RenderFunctionWarningLabel;
        private Button CameraPage2;
        private NumericUpDownEx SceneHeaderCopyList;
        private Panel CameraPanel2;
        private Label label121;
        private NumericUpDownEx CameraUnk22;
        private NumericUpDownEx CameraUnk1E;
        private Label label122;
        private Label label123;
        private Button CameraPage1;
        private Label label124;
        private NumericUpDownEx CameraUnk1C;
        private Label label126;
        private NumericUpDownEx CameraUnk16;
        private Label label127;
        private Label label128;
        private NumericUpDownEx CameraUnk18;
        private Label label129;
        private NumericUpDownEx CameraUnk14;
        private NumericUpDownEx CameraUnk1A;
        private Label label130;
        private NumericUpDownEx CameraUnk12;
        private NumericUpDownEx CameraUnk20;
        private CheckBox RenderFunctionInherit;
        private Button GroupCustomizeButton;
        private CheckBox GroupCustom;
        private Label label125;
        private NumericUpDownEx FunctionColorBlendAlpha;
        private ToolStripMenuItem ColorBlindMenuItem;
        private ToolStripMenuItem RenderSelectedCutsceneCommandsMenuItem;
        private ToolStripMenuItem DisableTextureWarningsMenuItem;
        private ToolStripMenuItem EnableNexExitFormatMenuItem;
        private NumericUpDownEx ExitSceneIndex;
        private Label label134;
        private NumericUpDownEx ExitSpawnIndex;
        private Label label133;
        private NumericUpDownEx ExitHeaderIndex;
        private Label label132;
        private NumericUpDownEx ExitFadeOut;
        private Label label131;
        private NumericUpDownEx ExitFadeIn;
        private CheckBox ExitShowTitlecard;
        private Label label1331;
        private CheckBox ExitMusicOn;
        private GroupBox ExitGroupBox;
        private NumericUpDownEx EnvironmentDirectionAZ;
        private NumericUpDownEx EnvironmentDirectionAY;
        private NumericUpDownEx EnvironmentDirectionBZ;
        private NumericUpDownEx EnvironmentDirectionBY;
        private Button SetTitlecard;
        private Button SetRestrictionFlags;
        private Label label136;
        private NumericUpDownEx RoomSelector;
        private Button ViewNormalCopyEnvA;
        private Button ViewNormalCopyEnvB;
        private Label label27;
        private Label label21;
        private Label label29;
        private ComboBox SoundSpec;
        private ListBox ExitList;
        private Label label36;
        private TabControl ObjectTabMenu;
        private TabPage RoomObjectPage;
        private ListBox RoomObjectListBox;
        private Label RoomObjectDescription;
        private Button RoomObjectDeleteButton;
        private Button RoomObjectAddButton;
        private TabPage SceneObjectPage;
        private Label SceneObjectDescription;
        private ListBox SceneObjectListBox;
        private Button SceneObjectDeleteButton;
        private Button SceneObjectAddButton;
        private ToolStripMenuItem buildAndLaunchZ64romToolStripMenuItem;
        private ToolStripMenuItem buildAndLaunchZ64romWarpToSceneToolStripMenuItem;
        private ToolStripMenuItem DisableCutscenePreviewBlackBarsMenuItem;
        private GroupBox ActorCutsceneGroupBox;
        private ComboBox ActorCutsceneCamIndex;
        private Label label142;
        private Label label141;
        private ComboBox ActorCutsceneRetCamera;
        private ComboBox ActorCutsceneHudFade;
        private CheckBox ActorCutsceneBlackBars;
        private CheckBox ActorCutscenePuzzleSound;
        private Label label140;
        private NumericUpDownEx ActorCutsceneAdditionalActorCs;
        private Button ActorCutsceneDeleteButton;
        private Button ActorCutsceneAddButton;
        private Label label37;
        private NumericUpDownEx ActorCutsceneCsIndex;
        private NumericUpDown ActorCutsceneNumber;
        private Label label138;
        private NiceLine niceLine18;
        private Label label139;
        private NumericUpDownEx ActorCutsceneLength;
        private Label label143;
        private NumericUpDownEx ActorCutsceneUnknown;
        private ToolStripMenuItem importActorCutscenesFromzsceneToolStripMenuItem;
        private ToolStripMenuItem ResetGroupSettingsReloadMenuItem;
        private ToolStripMenuItem dropTableEditorOoTToolStripMenuItem;
        private CheckBox GroupScaledNormals;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem NewZ64romProject;
        private NumericUpDownEx TimeSpeed;
        private NumericUpDownEx SoundEcho;
        private NumericUpDownEx WaterboxRoom;
        private NumericUpDownEx WindStrength;
        private NumericUpDownEx WindSouth;
        private NumericUpDownEx WindVertical;
        private NumericUpDownEx WindWest;
        private Button Z64RomPlay;
    }
    }

