using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpOcarina.SayakaGL;

namespace SharpOcarina
{
    public partial class CustomCombiner : Form
    {
        public ulong CombinerCommand = 0;

        public ulong E2Command = 0;

        public ulong D9Command = 0;

        bool OutdoorLight = false;

        bool AffectedByPointLight = false;

        public ObjFile.Group target;

        public MainForm mainf;

        public bool D9lock;

        public CustomCombiner(MainForm _mainf, ObjFile.Group _target, bool _outdoorLight, bool _affectedByPointLight)
        {
            target = _target;
            mainf = _mainf;
            OutdoorLight = _outdoorLight;
            AffectedByPointLight = _affectedByPointLight;

            CombinerCommand = target.CustomDL[0];
            D9Command = target.CustomDL[1];
            E2Command = target.CustomDL[2];



            InitializeComponent();

            CompiledD9.Text = D9Command.ToString("X14");
            CompiledE2.Text = E2Command.ToString("X14");

            Init();

            UpdateCombinerComboBox();
            UpdateGeometryModeCheckboxes();
        }
        public void Init()
        {
            Object[] objs = new[]
            {
                "Combined color",
                "Texture1 color",
                "Texture2 color",
                "Prim color",
                "Vtx color",
                "Env color",
                "1.0",
                "Noise",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
            };

            C1A.Items.AddRange(objs);
            C2A.Items.AddRange(objs);

            objs = new[]
                        {
                "Combined color",
                "Texture1 color",
                "Texture2 color",
                "Prim color",
                "Vtx color",
                "Env color",
                "Keycenter",
                "Convk4",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
            };

            C1B.Items.AddRange(objs);
            C2B.Items.AddRange(objs);

            objs = new[]
                       {
                "Combined color",
                "Texture1 color",
                "Texture2 color",
                "Prim color",
                "Vtx color",
                "Env color",
                "Keyscale",
                "Combined alpha",
                "Texture1 alpha",
                "Texture2 alpha",
                "Prim alpha",
                "Vtx alpha",
                "Env alpha",
                "LOD frac",
                "Prim LOD frac",
                "Convk5",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
                "0.0",
            };

            C1C.Items.AddRange(objs);
            C2C.Items.AddRange(objs);

            objs = new[]
                       {
                "Combined color",
                "Texture1 color",
                "Texture2 color",
                "Prim color",
                "Vtx color",
                "Env color",
                "1.0",
                "0.0",
                       };

            C1D.Items.AddRange(objs);
            C2D.Items.AddRange(objs);

            objs = new[]
                       {
                "Combined alpha",
                "Texture1 alpha",
                "Texture2 alpha",
                "Prim alpha",
                "Vtx alpha",
                "Env alpha",
                "1.0",
                "0.0",
                       };

            A1A.Items.AddRange(objs);
            A2A.Items.AddRange(objs);

            A1B.Items.AddRange(objs);
            A2B.Items.AddRange(objs);

            A1D.Items.AddRange(objs);
            A2D.Items.AddRange(objs);

            objs = new[]
                       {
                "Combined alpha",
                "Texture1 alpha",
                "Texture2 alpha",
                "Prim alpha",
                "Vtx alpha",
                "Env alpha",
                "Prim LOD frac",
                "0.0",
                       };

            A1C.Items.AddRange(objs);
            A2C.Items.AddRange(objs);
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Sync();
            this.Close();
        }

        private void Sync()
        {
            target.CustomDL[0] = CombinerCommand;
            target.CustomDL[1] = D9Command;
            target.CustomDL[2] = E2Command;
            mainf.SetCustomCombinerData(target);
        }

        private void UpdateCombinerComboBox()
        {
            UcodeSimulator.UnpackedCombinerStruct Unpacked = UcodeSimulator.UnpackCombinerMux(((uint)((CombinerCommand >> 32) & 0xFFFFFFFF)), (uint)(CombinerCommand & (ulong)0xFFFFFFFF));

            Console.WriteLine("" + ((uint)((CombinerCommand & (ulong)0xFFFFFF00000000 >> 32) & 0xFFFFFFFF)).ToString("X8"));

            Console.WriteLine("" + ((uint)((CombinerCommand >> 32) & 0xFFFFFFFF)).ToString("X8"));


            //Console.WriteLine("" + ((uint)(CombinerCommand & (ulong)0xFFFFFFFF)).ToString("X8"));

            C1A.SelectedIndex = Unpacked.cA[0];
            C1B.SelectedIndex = Unpacked.cB[0];
            C1C.SelectedIndex = Unpacked.cC[0];
            C1D.SelectedIndex = Unpacked.cD[0];

            C2A.SelectedIndex = Unpacked.cA[1];
            C2B.SelectedIndex = Unpacked.cB[1];
            C2C.SelectedIndex = Unpacked.cC[1];
            C2D.SelectedIndex = Unpacked.cD[1];

            A1A.SelectedIndex = Unpacked.aA[0];
            A1B.SelectedIndex = Unpacked.aB[0];
            A1C.SelectedIndex = Unpacked.aC[0];
            A1D.SelectedIndex = Unpacked.aD[0];

            A2A.SelectedIndex = Unpacked.aA[1];
            A2B.SelectedIndex = Unpacked.aB[1];
            A2C.SelectedIndex = Unpacked.aC[1];
            A2D.SelectedIndex = Unpacked.aD[1];

            CompiledCombiner.Text = CombinerCommand.ToString("X14");

            if (PreviewCheckbox.Checked)
                Sync();
        }


        private void PackCombiner()
        {
            CombinerCommand = 0;

            CombinerCommand = (ulong)(0 | (((ulong)C1A.SelectedIndex << (20+32)))
                | (((ulong)C1B.SelectedIndex << 28))
                | (((ulong)C1C.SelectedIndex << (15+32)))
                | (((ulong)C1D.SelectedIndex << 15))

                | (((ulong)C2A.SelectedIndex << (5+32)))
                | (((ulong)C2B.SelectedIndex << 24))
                | (((ulong)C2C.SelectedIndex << (0+32)))
                | (((ulong)C2D.SelectedIndex << 6))

                | (((ulong)A1A.SelectedIndex << (12+32)))
                | (((ulong)A1B.SelectedIndex << 12))
                | (((ulong)A1C.SelectedIndex << (9+32)))
                | (((ulong)A1D.SelectedIndex << 9))

                | (((ulong)A2A.SelectedIndex << 21))
                | (((ulong)A2B.SelectedIndex << 3))
                | (((ulong)A2C.SelectedIndex << 18))
                | (((ulong)A2D.SelectedIndex << 0)));


            CompiledCombiner.Text = CombinerCommand.ToString("X14");

            if (PreviewCheckbox.Checked)
                Sync();
        }

        private void UpdateGeometryModeCheckboxes()
        {
            D9lock = true;
            G_ZBUFFER.Checked = (((D9Command >> 0) & 0xF) != 0);
            G_SHADE.Checked = (((D9Command >> 2) & 0xF) != 0);
            G_CULL_FRONT.Checked = (((D9Command >> 9) & 0xF) != 0);
            G_CULL_BACK.Checked = (((D9Command >> 10) & 0xF) != 0);
            G_FOG.Checked = (((D9Command >> 16) & 0xF) != 0);
            G_LIGHTING.Checked = (((D9Command >> 17) & 0xF) != 0);
            G_TEXTURE_GEN.Checked = (((D9Command >> 18) & 0xF) != 0);
            G_TEXTURE_GEN_LINEAR.Checked = (((D9Command >> 19) & 0xF) != 0);
            G_SHADING_SMOOTH.Checked = (((D9Command >> 21) & 0xF) != 0);
            G_CLIPPING.Checked = (((D9Command >> 23) & 0xF) != 0);

            CompiledD9.Text = D9Command.ToString("X14");

            Console.WriteLine((D9Command >> 2).ToString("X16"));

            if (PreviewCheckbox.Checked)
                Sync();
            D9lock = false;
        }

        private void PackGeometryMode()
        {
            D9Command = 0;

            D9Command = (ulong)(0 | (((ulong)(G_ZBUFFER.Checked ? 1 : 0) << (0)))
                | (((ulong)(G_SHADE.Checked ? 1 : 0) << 2))
                | (((ulong)(G_CULL_FRONT.Checked ? 1 : 0) << 9))
                | (((ulong)(G_CULL_BACK.Checked ? 1 : 0) << 10))
                | (((ulong)(G_FOG.Checked ? 1 : 0) << 16))
                | (((ulong)(G_LIGHTING.Checked ? 1 : 0) << 17))
                | (((ulong)(G_TEXTURE_GEN.Checked ? 1 : 0) << 18))
                | (((ulong)(G_TEXTURE_GEN_LINEAR.Checked ? 1 : 0) << 19))
                | (((ulong)(G_SHADING_SMOOTH.Checked ? 1 : 0) << 21))
                | (((ulong)(G_CLIPPING.Checked ? 1 : 0) << 23))
                | (((ulong)0xFFFFFF << 32))
                );

            CompiledD9.Text = D9Command.ToString("X14");

            if (PreviewCheckbox.Checked)
                Sync();
        }

        private void CombinerComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PackCombiner();
        }

        private void CustomCombiner_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.customcombiner_visible = false;
        }

        private void CompiledCombiner_KeyDown(object sender, KeyEventArgs e)
        {
            ulong temp = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (!UInt64.TryParse(CompiledCombiner.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp))
                {
                    temp = 0;
                }

                CombinerCommand = temp;

                UpdateCombinerComboBox();

            }
        }

        private void CompiledCombiner_Leave(object sender, EventArgs e)
        {
            ulong temp = 0;

            if (!UInt64.TryParse(CompiledCombiner.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp))
            {
                temp = 0;
            }

            CombinerCommand = temp;

            UpdateCombinerComboBox();
        }

        private void DefaultButton_Click(object sender, EventArgs e)
        {
            ulong[] defaults = GetDefaults(target);

            CombinerCommand = defaults[0];
            D9Command = defaults[1];
            E2Command = defaults[2];

            CompiledCombiner.Text = CombinerCommand.ToString("X14");
            CompiledD9.Text = D9Command.ToString("X14");
            CompiledE2.Text = E2Command.ToString("X14");

            UpdateCombinerComboBox();

            UpdateGeometryModeCheckboxes();

        }

        public ulong[] GetDefaults(ObjFile.Group group)
        {
            bool IsTranslucent = ((group.TintAlpha >> 24) != 255);
            ulong[] output = new ulong[4];

            if (IsTranslucent == true)
            {
                output[0] = 0x167E03FF0FFDFF;
                output[2] = 0x1C00000000 | (ulong)(0x081049D8 | ((group.IgnoreFog) ? 0x00000000 : 0xC0000000) | ((group.Decal) ? 0xC00 : 0x000));
            }

            else
            {
                output[0] = 0x127E03FFFFFDF8;
                output[2] = 0x1C00000000 | (ulong)(0x08110078 | ((group.SmoothRGBAEdges) ? 0x4000 : 0x3000) | ((group.IgnoreFog) ? 0x00000000 : 0xC0000000) | ((group.Decal) ? 0xC00 : 0x000));

            }

            if ((group.ReverseLight) ? !OutdoorLight : OutdoorLight)
            {
                if (IsTranslucent == true || !group.BackfaceCulling)
                {

                    // Helpers.Append64(ref DList, 0xD9F3FBFF00000000);
                    if (!group.Metallic && group.BackfaceCulling) output[1] = 0xFFFFFF00030400 | (ulong)(AffectedByPointLight ? 0x400000 : 0x000000);
                    else if (!group.Metallic && !group.BackfaceCulling) output[1] = 0xFFFFFF00030000 | (ulong)(AffectedByPointLight ? 0x400000 : 0x000000);
                    else output[1] = 0xFFFFFF000E0400;
                }
                else
                {
                    //  Helpers.Append64(ref DList, 0xD9F3FFFF00000000);
                    if (!group.Metallic) output[1] = 0xFFFFFF00030400 | (ulong)(AffectedByPointLight ? 0x400000 : 0x000000);
                    else output[1] = 0xFFFFFF000E0400;

                }
            }
            else
            {
                if (IsTranslucent == true || !group.BackfaceCulling)
                {

                    if (!group.Metallic && group.BackfaceCulling) output[1] = 0xF1FBFF00000000;
                    else if (!group.Metallic && !group.BackfaceCulling) output[1] = 0xFFFFFF00010000;
                    else output[1] = 0xFFFFFF000C0400;
                }
                else
                {

                    if (!group.Metallic) output[1] = 0xFFFFFF00000400;
                    else output[1] = 0xFFFFFF000C0400;
                }
            }

            return output;

        }

        private void CompiledE2_KeyDown(object sender, KeyEventArgs e)
        {

            ulong temp = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (!UInt64.TryParse(CompiledE2.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp))
                {
                    temp = 0;
                }

                E2Command = temp;

                if (PreviewCheckbox.Checked)
                    Sync();

            }

        }

        private void CompiledE2_Leave(object sender, EventArgs e)
        {
            ulong temp = 0;
       
            if (!UInt64.TryParse(CompiledE2.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp))
            {
                temp = 0;
            }

            E2Command = temp;

            if (PreviewCheckbox.Checked)
                Sync();


        }

        private void CompiledD9_KeyDown(object sender, KeyEventArgs e)
        {

            ulong temp = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (!UInt64.TryParse(CompiledD9.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp))
                {
                    temp = 0;
                }

                D9Command = temp;

                UpdateGeometryModeCheckboxes();

            }

        }

        private void CompiledD9_Leave(object sender, EventArgs e)
        {


            ulong temp = 0;
        
            if (!UInt64.TryParse(CompiledD9.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp))
            {
                temp = 0;
            }

            D9Command = temp;

            UpdateGeometryModeCheckboxes();



        }

        private void PreviewCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (PreviewCheckbox.Checked)
                Sync();
        }

        private void GeometryMode_CheckedChange(object sender, EventArgs e)
        {
            if (!D9lock) PackGeometryMode();
        }
    }
}
