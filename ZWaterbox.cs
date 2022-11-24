using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace SharpOcarina
{
    public class ZWaterbox
    {
        [XmlIgnore]
        private float _XPos, _YPos, _ZPos;
        [XmlIgnore]
        private float _XSize, _ZSize;
        [XmlIgnore]
        private byte _Room,_Env,_Camera;
        [XmlIgnore]
        private ushort _Properties;

        public float XPos
        {
            get { return _XPos; }
            set { _XPos = value; }
        }

        public float YPos
        {
            get { return _YPos; }
            set { _YPos = value; }
        }

        public float ZPos
        {
            get { return _ZPos; }
            set { _ZPos = value; }
        }

        public float XSize
        {
            get { return _XSize; }
            set { _XSize = value; }
        }

        public float ZSize
        {
            get { return _ZSize; }
            set { _ZSize = value; }
        }

        public byte Env
        {
            get { return _Env; }
            set { _Env = value; }
        }

        public byte Camera
        {
            get { return _Camera; }
            set { _Camera = value; }
        }

        public byte Room
        {
            get { return _Room; }
            set { _Room = value; }
        }

        public ushort Properties
        {
            get { return _Properties; }
            set { _Properties = value; }
        }

        public ZWaterbox() { }

        public ZWaterbox(float XPos, float YPos, float ZPos, float XSize, float ZSize, byte Env, byte Camera, byte Room)
        {
            _XPos = XPos;
            _YPos = YPos;
            _ZPos = ZPos;
            _XSize = XSize;
            _ZSize = ZSize;
            _Env = Env;
            _Camera = Camera;
            _Room = Room;
        }
    }
}
