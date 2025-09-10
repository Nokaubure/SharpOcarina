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
    public partial class MoveActorToRoom : Form
    {

        public int room;

        public MoveActorToRoom()
        {
            InitializeComponent();
            RoomID.Maximum = MainForm.CurrentScene.Rooms.Count - 1;
        }


        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            room = (int) RoomID.Value;
            this.Close();
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
