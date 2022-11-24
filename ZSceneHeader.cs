using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SharpOcarina
{
    public class ZSceneHeader
    {
        [XmlIgnore]
        bool _SameAsPrevious;
        [XmlIgnore]
        ZScene _Scene;
        [XmlIgnore]
        public int _InjectOffset;
        [XmlIgnore]
        public int[] _RoomInjectOffset = new int[0];
        [XmlIgnore]
        public int _CloneFromHeader = 0;
        [XmlIgnore]
        public uint _InjectOffsetValue;
        [XmlIgnore]
        public uint[] _RoomInjectOffsetValues = new uint[0];

        public bool SameAsPrevious
        {
            get { return _SameAsPrevious; }
            set { _SameAsPrevious = value; }
        }

        public int CloneFromHeader
        {
            get { return _CloneFromHeader; }
            set { _CloneFromHeader = value; }
        }

        public ZScene Scene
        {
            get { return _Scene; }
            set { _Scene = value; }
        }


        public ZSceneHeader() { }

        public ZSceneHeader(bool SameAsPrevious, ZScene Scene)
        {
            _SameAsPrevious = SameAsPrevious;
            _Scene = Scene;
        }
    }
}
