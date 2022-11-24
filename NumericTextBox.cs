using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpOcarina
{
    public partial class NumericTextBox : TextBox
    {
        bool allowHex = false;
        int digits = 4;

        private bool IsCharValid(char Ch)
        {
            return ((Char.IsNumber(Ch) == true) ||
                (allowHex == true && ((Ch >= 'A' && Ch <= 'F') || (Ch >= 'a' && Ch <= 'f'))) ||
                (Ch == '\b'));
        }

        private bool IsStringValid(string Str)
        {
            foreach (char Ch in Str)
                if (IsCharValid(Ch) == false)
                    return false;

            return true;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            ///* See if the user is trying keyboard-based cut/copy/paste (Ctrl+X/C/V) and handle it */
            //if (this.SelectionLength != 0 && (e.KeyChar == 0x18 || e.KeyChar == 0x3 || e.KeyChar == 0x16))
            //{
            //    switch (e.KeyChar)
            //    {
            //        case (char)0x18:
            //            /* Cut */
            //            Clipboard.SetText(this.SelectedText);
            //            this.SelectedText = string.Empty;
            //            break;
            //        case (char)0x03:
            //            /* Copy */
            //            Clipboard.SetText(this.SelectedText);
            //            break;
            //        case (char)0x16:
            //            /* Paste */
            //            string PasteText = string.Empty;
            //            foreach (char Ch in Clipboard.GetText())
            //            {
            //                if (IsCharValid(Ch) == true)
            //                {
            //                    PasteText += Char.ToUpper(Ch);
            //                }
            //            }
            //            this.Text.Insert(this.SelectionStart, PasteText);
            //            break;
            //    }
            //}

            /* Make sure the user doesn't enter more characters than allowed; also take selection length into account */
            if (Text.Length >= digits + this.SelectionLength && e.KeyChar != '\b')
                e.Handled = true;

            /* Make sure the character is a number, if hex is allowed is between A and F, or is backspace */
            if (IsCharValid(e.KeyChar) == true)
            {
                /* ...and now turn it into upper-case to make the look consistent */
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
            else
            {
                /* Otherwise, ignore this keypress */
                e.Handled = true;
            }
        }

        public int IntValue
        {
            get
            {
                if (Text == "") return 0;
                else
                return Int32.Parse(Text, (allowHex == true ? System.Globalization.NumberStyles.HexNumber : System.Globalization.NumberStyles.Integer));
            }
        }

        public decimal DecimalValue
        {
            get { return Decimal.Parse(Text); }
        }

        public bool AllowHex
        {
            get { return allowHex; }
            set { allowHex = value; }
        }

        public int Digits
        {
            get { return digits; }
            set { digits = Math.Max(1, value); }
        }
    }
}
