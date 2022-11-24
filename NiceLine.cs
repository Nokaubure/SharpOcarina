// http://www.codeproject.com/KB/miscctrl/NiceLine.aspx

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SharpOcarina
{
    /// <summary>
    /// "NiceLine" draws a shaded line separator. Can have an aligned text caption.
    /// </summary>
    [DefaultProperty("Caption")]
    [ToolboxBitmap(typeof(System.Windows.Forms.GroupBox))]
    public class NiceLine : System.Windows.Forms.UserControl
    {
        private System.ComponentModel.Container components = null;
        private string _Caption = "";
        private int _CaptionMarginSpace = 16;
        private int _CaptionPadding = 2;
        private LineVerticalAlign _LineVerticalAlign = LineVerticalAlign.Middle;
        private CaptionOrizontalAlign _CaptionOrizontalAlign = CaptionOrizontalAlign.Left;
        private LineOrientation _LineOrientation = LineOrientation.Horizontal;

        public NiceLine()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // Cannot focus via tab
            this.TabStop = false;
        }

        /// <summary>
        /// The caption text displayed on the line. 
        /// If the caption is "" (the default) the line is not broken
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("")]
        [Description("The caption text displayed on the line. If the caption is \"\" (the default) the line is not broken")]
        public string Caption
        {
            get { return _Caption; }
            set
            {
                _Caption = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The distance in pixels form the control margin to caption text
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(16)]
        [Description("The distance in pixels form the control margin to caption text")]
        public int CaptionMarginSpace
        {
            get { return _CaptionMarginSpace; }
            set
            {
                _CaptionMarginSpace = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The space in pixels around text caption
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(2)]
        [Description("The space in pixels around text caption")]
        public int CaptionPadding
        {
            get { return _CaptionPadding; }
            set
            {
                _CaptionPadding = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The vertical alignment of the line within the space of the control
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(LineVerticalAlign.Middle)]
        [Description("The vertical alignment of the line within the space of the control")]
        public LineVerticalAlign LineVerticalAlign
        {
            get { return _LineVerticalAlign; }
            set
            {
                _LineVerticalAlign = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Tell where the text caption is aligned in the control
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(CaptionOrizontalAlign.Left)]
        [Description("Tell where the text caption is aligned in the control")]
        public CaptionOrizontalAlign CaptionOrizontalAlign
        {
            get { return _CaptionOrizontalAlign; }
            set
            {
                _CaptionOrizontalAlign = value;
                Invalidate();
            }
        }

        /// <summary>
        /// ---
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(LineOrientation.Horizontal)]
        [Description("")]
        public LineOrientation LineOrientation
        {
            get { return _LineOrientation; }
            set
            {
                _LineOrientation = value;
                Invalidate();
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // NiceLine
            // 
            this.Name = "NiceLine";
            this.Size = new System.Drawing.Size(100, this.Font.Height);
        }
        #endregion

        //		protected override CreateParams CreateParams
        //		{
        //			get
        //			{
        //				CreateParams cp = base.CreateParams;
        //				cp.ExStyle |= 0x20;
        //				return cp;
        //			}
        //		}
        //
        //		protected override void OnMove(EventArgs e)
        //		{
        //			RecreateHandle();
        //		}
        //
        //		protected override void OnPaintBackground(PaintEventArgs e)
        //		{
        //			// do nothing
        //		}

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            int ym;
            switch (LineVerticalAlign)
            {
                case LineVerticalAlign.Top:
                    ym = 0;
                    break;
                case LineVerticalAlign.Middle:
                    ym = Convert.ToInt32(Math.Ceiling((decimal)Size.Height / 2)) - 1;
                    break;
                case LineVerticalAlign.Bottom:
                    ym = Size.Height - 2;
                    break;
                default:
                    ym = 0;
                    break;
            }

            SizeF captionSizeF = e.Graphics.MeasureString(Caption, this.Font, this.Width - CaptionMarginSpace * 2, StringFormat.GenericDefault);
            int captionLength = Convert.ToInt32(captionSizeF.Width);

            int beforeCaption;
            int afterCaption;

            if (Caption == "")
            {
                beforeCaption = CaptionMarginSpace;
                afterCaption = CaptionMarginSpace;
            }
            else
            {
                switch (CaptionOrizontalAlign)
                {
                    case CaptionOrizontalAlign.Left:
                        beforeCaption = CaptionMarginSpace;
                        afterCaption = CaptionMarginSpace + CaptionPadding * 2 + captionLength;
                        break;
                    case CaptionOrizontalAlign.Center:
                        beforeCaption = (Width - captionLength) / 2 - CaptionPadding;
                        afterCaption = (Width - captionLength) / 2 + captionLength + CaptionPadding;
                        break;
                    case CaptionOrizontalAlign.Right:
                        beforeCaption = Width - CaptionMarginSpace * 2 - captionLength;
                        afterCaption = Width - CaptionMarginSpace;
                        break;
                    default:
                        beforeCaption = CaptionMarginSpace;
                        afterCaption = CaptionMarginSpace;
                        break;
                }
            }

            // Lines
            if (_LineOrientation == LineOrientation.Horizontal)
            {
                // ------- 
                // |      ...caption...
                e.Graphics.DrawLines(new Pen(SystemColors.ControlDark, 1),
                    new Point[] { 
								new Point(0, ym + 1), 
								new Point(0, ym), 
								new Point(beforeCaption, ym)
							}
                    );

                //                  -------
                //	      ...caption... 
                e.Graphics.DrawLines(new Pen(SystemColors.ControlDark, 1),
                    new Point[] { 
								new Point(afterCaption, ym), 
								new Point(this.Width, ym)
							}
                    );

                //        ...caption...
                // -------
                e.Graphics.DrawLines(new Pen(SystemColors.ControlLightLight, 1),
                    new Point[] { 
								new Point(0, ym + 1), 
								new Point(beforeCaption, ym + 1)
							}
                    );

                //        ...caption...       |
                //                  -------
                e.Graphics.DrawLines(new Pen(SystemColors.ControlLightLight, 1),
                    new Point[] { 
								new Point(afterCaption, ym + 1), 
								new Point(this.Width, ym + 1), 
								new Point(this.Width, ym) 
							}
                    );
            }
            else if (_LineOrientation == LineOrientation.Vertical)
            {
                System.Drawing.Drawing2D.HatchBrush aHatchBrush = new
                    System.Drawing.Drawing2D.HatchBrush
                    (System.Drawing.Drawing2D.HatchStyle.Vertical,
                    SystemColors.ControlLightLight,
                    SystemColors.ControlDark);

                Pen myPen = new Pen(aHatchBrush, 2);
                e.Graphics.DrawLine(myPen, beforeCaption / 2, 0, beforeCaption / 2, ClientRectangle.Height);
                myPen.Dispose(); aHatchBrush.Dispose();
            }

            // Render caption
            if (Caption != "")
            {
                switch (_LineOrientation)
                {
                    case LineOrientation.Horizontal:
                        e.Graphics.FillRectangle(new SolidBrush(BackColor),
                            beforeCaption + CaptionPadding, 1,
                            e.Graphics.MeasureString(Caption, this.Font).Width,
                            e.Graphics.MeasureString(Caption, this.Font).Height
                            );
                        e.Graphics.DrawString(Caption, this.Font, new SolidBrush(this.ForeColor), beforeCaption + CaptionPadding, 1);
                        break;
                    case LineOrientation.Vertical:
                        e.Graphics.FillRectangle(new SolidBrush(BackColor),
                            0, beforeCaption + CaptionPadding,
                            e.Graphics.MeasureString(Caption, this.Font).Height,
                            e.Graphics.MeasureString(Caption, this.Font).Width
                            );
                        e.Graphics.DrawString(Caption, this.Font, new SolidBrush(this.ForeColor), 0, beforeCaption + CaptionPadding, new StringFormat(StringFormatFlags.DirectionVertical));
                        break;
                }
            }

            //			e.Graphics.DrawLines(new Pen(Color.Red, 1), 
            //				new Point[] { 
            //								new Point(0, 0), 
            //								new Point(this.Width-1, 0), 
            //								new Point(this.Width-1, this.Height-1),
            //								new Point(0, this.Height-1),
            //								new Point(0, 0)
            //							} 
            //				);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            switch (_LineOrientation)
            {
                case LineOrientation.Horizontal:
                    this.Height = this.Font.Height + 2;
                    break;
                case LineOrientation.Vertical:
                    this.Width = this.Font.Height + 4;
                    break;
            }
            this.Invalidate();
        }

        protected override void OnFontChanged(System.EventArgs e)
        {
            this.OnResize(e);
            base.OnFontChanged(e);
        }
    }

    public enum LineOrientation
    {
        Horizontal,
        Vertical
    }

    public enum LineVerticalAlign
    {
        Top,
        Middle,
        Bottom
    }

    public enum CaptionOrizontalAlign
    {
        Left,
        Center,
        Right
    }
}
