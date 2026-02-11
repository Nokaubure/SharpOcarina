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
    public partial class PickString : Form
    {

        public string result;

        public PickString(string caption, string initialtext)
        {
            InitializeComponent();
            this.Text = caption;
            StringTextBox.Text = initialtext;
        }


        private void Ok_Click(object sender, EventArgs e)
        {
            this.result = StringTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
