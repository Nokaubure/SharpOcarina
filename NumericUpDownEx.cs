﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SharpOcarina
{
    public class NumericUpDownEx : NumericUpDown
    {
        int displayDigits = 1;
        int incrementMouseWheel = 3;
        int shiftMultiplier = 1;

        bool doValueRollover = true;

        EventHandler eventHandler = null;
        bool alwaysFireValueChanged = false;

        private bool nofire = false;
        private decimal prevval = 0;

        public NumericUpDownEx()
        {
            prevval = this.Value;
        }

        [Category("Appearance")]
        [Description("Amount of digits displayed.")]
        public int DisplayDigits
        {
            set
            {
                displayDigits = value;
            }
            get
            {
                return this.displayDigits;
            }
        }

        protected override void UpdateEditText()
        {
            if (base.Hexadecimal == true)
                /* if (Minimum >= -32768 && Minimum != 0)
                  {
                         base.Text = string.Format(@"{0:X" + displayDigits + "}", (Int32)base.Value);

                     }
                    else*/
                base.Text = string.Format(@"{0:X" + displayDigits + "}", (Int32)base.Value);
            else
            {
                if (base.DecimalPlaces > 0)
                    base.Text = base.Value.ToString($"F{this.DecimalPlaces}");
                else
                    base.Text = string.Format(@"{0:D" + displayDigits + "}", (Int32)base.Value);
            }
        }

        [Category("Behavior")]
        [Description("Amount to increase/decrease value when mouse wheel is used.")]
        public int IncrementMouseWheel
        {
            set
            {
                incrementMouseWheel = value;
            }
            get
            {
                return this.incrementMouseWheel;
            }
        }

        [Category("Behavior")]
        [Description("Multiplier for value increment when shift is held.")]
        public int ShiftMultiplier
        {
            set
            {
                this.shiftMultiplier = value;
            }
            get
            {
                return this.shiftMultiplier;
            }
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
          //  int multiplier = (System.Windows.Forms.Control.ModifierKeys == Keys.Shift) ? this.shiftMultiplier : 1;
            int sign = Helpers.Clamp(e.Delta, -1, 1);

            this.Value = Helpers.Clamp(this.Value + (this.incrementMouseWheel * 1) * sign, this.Minimum, this.Maximum);
            base.OnValueChanged(new EventArgs());
        }

        [Category("Behavior")]
        [Description("Roll over value when minimum or maximum is hit.")]
        public bool DoValueRollover
        {
            set
            {
                doValueRollover = value;
            }
            get
            {
                return this.doValueRollover;
            }
        }

        [Category("Behavior")]
        [Description("Always fire the ValueChanged event, regardless of the value being changed by the user or code.")]
        public bool AlwaysFireValueChanged
        {
            set
            {
                alwaysFireValueChanged = value;
            }
            get
            {
                return this.alwaysFireValueChanged;
            }
        }

        public new event EventHandler ValueChanged
        {
            add
            {
                eventHandler += value;
                base.ValueChanged += value;
            }

            remove
            {
                eventHandler -= value;
                base.ValueChanged -= value;
            }
        }

        public new decimal Value
        {
            get
            {
                return base.Value;
            }
            set
            {
    

                if (alwaysFireValueChanged == true)
                {
                    base.Value = value;
                }
                else
                {
                    base.ValueChanged -= eventHandler;
                    if (value > base.Maximum)
                        value = base.Maximum;
                    else if (value < base.Minimum)
                        value = base.Minimum;
                    base.Value = value;
                    base.ValueChanged += eventHandler;
                }
                
            }
        }

        public new decimal Maximum
        {
            get
            {
                if (doValueRollover)
                    return base.Maximum - 1;
                else
                    return base.Maximum;
            }
            set
            {
                if (doValueRollover)
                    base.Maximum = value + 1;
                else
                    base.Maximum = value;
            }
        }

        public new decimal Minimum
        {
            get
            {
                if (doValueRollover)
                    return base.Minimum + 1;
                else
                    return base.Minimum;
            }
            set
            {
                if (doValueRollover)
                    base.Minimum = value - 1;
                else
                    base.Minimum = value;
            }
        }


        protected override void OnValueChanged(EventArgs e)
        {
    

            if (doValueRollover)
            {
                if (Value > base.Maximum - 1)
                    Value = base.Minimum + 1;
                else if (Value < base.Minimum + 1)
                    Value = base.Maximum - 1;
            }

           

            base.OnValueChanged(e);

            prevval = this.Value;


        }
    }
}
