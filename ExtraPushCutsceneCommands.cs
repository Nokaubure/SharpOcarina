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
    public partial class PushCutsceneCommands : Form
    {

        public int startframe;
        public int amount;

        public PushCutsceneCommands(string caption)
        {
            InitializeComponent();
            this.Text = caption;
        }


        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            startframe = (ushort) StartFrameVal.Value;
            amount = (int) AmountVal.Value;
            this.Close();
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
