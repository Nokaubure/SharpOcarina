using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpOcarina
{
    public partial class Z64romInstallPostOperations : Form
    {
        public List<Byte> Data;
        public bool removeallscenes;
        public bool removen64logo;

        public Z64romInstallPostOperations()
        {
            InitializeComponent();
            removeallscenes = true;
            removen64logo = true;

        }


        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            removeallscenes = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            removen64logo = checkBox2.Checked;
        }
    }
}
