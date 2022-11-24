using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SharpOcarina
{
    public class ZAdditionalLight
    {
        [XmlIgnore]
        private short _XPos;
        [XmlIgnore]
        private short _YPos;
        [XmlIgnore]
        private short _ZPos;
        [XmlIgnore]
        private bool _PointLight;
        [XmlIgnore]
        private byte _NSdirection;
        [XmlIgnore]
        private byte _EWdirection;
        [XmlIgnore]
        private byte _Radius;
        [XmlIgnore]
        private ColorWrapper _Color = new ColorWrapper();

        public short XPos
        {
            get { return _XPos; }
            set { _XPos = value; }
        }

        public short YPos
        {
            get { return _YPos; }
            set { _YPos = value; }
        }

        public short ZPos
        {
            get { return _ZPos; }
            set { _ZPos = value; }
        }

        public bool PointLight
        {
            get { return _PointLight; }
            set { _PointLight = value; }
        }

        public byte NSdirection
        {
            get { return _NSdirection; }
            set { _NSdirection = value; }
        }

        public byte EWdirection
        {
            get { return _EWdirection; }
            set { _EWdirection = value; }
        }

        public byte Radius
        {
            get { return _Radius; }
            set { _Radius = value; }
        }

        public int Color
        {
            get { return _Color.Argb; }
            set { _Color.Argb = value; }
        }

        [XmlIgnore]
        public Color ColorC
        {
            get { return _Color.Color; }
            set { _Color.Color = value; }
        }

        public ZAdditionalLight() { }

        public ZAdditionalLight(short XPos, short YPos, short ZPos, bool PointLight, byte NSdirection, byte EWdirection, byte Radius, Color cl)
        {
            _XPos = XPos;
            _YPos = YPos;
            _ZPos = ZPos;
            _PointLight = PointLight;
            _NSdirection = NSdirection;
            _EWdirection = EWdirection;
            _Radius = Radius;
            _Color = new ColorWrapper(cl);
        }

        public ZAdditionalLight Clone()
        {
            return (ZAdditionalLight)this.MemberwiseClone();
        }
    }
}
