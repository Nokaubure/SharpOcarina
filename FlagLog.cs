﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpOcarina
{
    public partial class FlagLog : Form
    {
        public string text;

        public FlagLog(string _text)
        {
            InitializeComponent();
            text = _text;
            Init();
        }
        public void Init()
        {
            TextBox.SelectedRtf = text;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            TextBox.Clear();
            TextBox.SelectedRtf = MainForm.GenerateFlagLog();
        }

        private void FlagLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.flaglog_visible = false;
        }
    }
}
