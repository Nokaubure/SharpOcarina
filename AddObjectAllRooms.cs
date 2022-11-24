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
    public partial class AddObjectAllRooms : Form
    {

        public int position;
        public ushort objectid;

        public AddObjectAllRooms()
        {
            InitializeComponent();
        }


        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            objectid = (ushort) ObjectID.Value;
            position = (int) PositionID.Value;
            this.Close();
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
