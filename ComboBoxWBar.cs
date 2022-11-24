using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SharpOcarina
{
    public partial class ComboBoxWBar : System.Windows.Forms.ComboBox
    {
        // Example project demonstrating applying a horizontal scrollbar to a standard combobox
        // Original article and more at http://cyotek.com

        #region  Private Member Declarations  

        private const int CB_SETHORIZONTALEXTENT = 0x015E;
        private const int WS_HSCROLL = 0x100000;

        #endregion  Private Member Declarations  

        #region  Private Class Methods  

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        #endregion  Private Class Methods  

        #region  Public Methods  

        public void SetHorizontalExtent()
        {
            int maxWith;

            maxWith = 0;
            foreach (object item in this.Items)
            {
                Size textSize;

                textSize = TextRenderer.MeasureText(item.ToString(), this.Font);
                if (textSize.Width > maxWith)
                    maxWith = textSize.Width;
            }

            this.SetHorizontalExtent(maxWith);
        }

        public void SetHorizontalExtent(int width)
        {
            SendMessage(this.Handle, CB_SETHORIZONTALEXTENT, new IntPtr(width), IntPtr.Zero);
        }

        #endregion  Public Methods  

        #region  Protected Properties  

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams;

                createParams = base.CreateParams;
                createParams.Style |= WS_HSCROLL;

                return createParams;
            }
        }

        #endregion  Protected Properties  
    }
}
