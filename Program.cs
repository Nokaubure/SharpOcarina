using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace SharpOcarina
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        public static string ApplicationTitle;
        public static int ApplicationVersion = 0x1400;

        public static MainForm MF;
        public static bool QuitProgram = false;

        public static string KeyboardLayout = "";

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                KeyboardLayout = GetKeyboardLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not detect keyboard layout, so using QWERTY");
                KeyboardLayout = "QWERTY";
            }

            MF = new MainForm();

            MF.Text = ApplicationTitle;

            MF.FormClosed += new FormClosedEventHandler(MF_FormClosed);
            MF.Show();

            if (System.AppDomain.CurrentDomain.FriendlyName.Contains("x64"))
            {

                MessageBox.Show(
                   "Due to a change in the required libraries, SharpOcarina can't be used if its not named SharpOcarina.exe, meaning that you have to rename this 64 bit executable to use it.",
                   "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

#if DEBUG
            ConsoleWindow.CreateConsole();
#else
            ApplicationTitle = MF.GetType().Namespace + GetVerString(ApplicationVersion);
#endif
            // MF.GetType().Namespace + " 1.3";
            ApplicationTitle = MF.GetType().Namespace + " 1.40";
            MF.BringToFront();
            do
            {
                MF.ProgramMainLoop();
                Application.DoEvents();

                System.Threading.Thread.Sleep(2);
            }
            while (QuitProgram == false);
        }

        public static string GetKeyboardLayout()
        {
            string layout = "";
            uint MAPVK_VSC_TO_VK = 0x01;

            uint[] QWERTYCodes =
                {
                0x00000010,
                0x00000011,
                0x00000012,
                0x00000013,
                0x00000014,
                0x00000015,
            };

            foreach (var code in QWERTYCodes)
            {
                var vk = MapVirtualKey(code, MAPVK_VSC_TO_VK);
                var val = KeyInterop.KeyFromVirtualKey((int)vk);
                layout += val;
            }

            if (layout.EndsWith("PYF"))
                return "DVORAK";

            return layout;
        }

        static void MF_FormClosed(object sender, FormClosedEventArgs e)
        {
            QuitProgram = true;
        }

        public static string GetVerString(int Version)
        {
            string VerString = "";

            VerString = " v" +
                (Version >> 8).ToString() + "." +
                ((Version & 0xF0) >> 4).ToString();

            if ((Version & 0xF) != 0)
                VerString += "." + (Version & 0xF).ToString();

            // VerString += " (Beta)";

            return VerString;
        }
    }
}
