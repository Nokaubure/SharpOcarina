using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SharpOcarina
{
    public class ZActor
    {
        [XmlIgnore]
        public bool IsTransition = false;
        [XmlIgnore]
        private ushort _Number, _Variable;
        [XmlIgnore]
        private float _XPos, _YPos, _ZPos;
        [XmlIgnore]
        private float _XRot, _YRot, _ZRot;
        [XmlIgnore]
        private byte _FrontSwitchTo, _FrontCamera, _BackSwitchTo, _BackCamera, _SpawnRoom = 0xFF;
        [XmlIgnore]
        private bool[] _IgnoreMMRot = { false, false, false };


        [XmlIgnore]
        private ZActor _Linked;

        public ushort Number
        {
            get { return _Number; }
            set { _Number = value; }
        }

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

        public float XRot
        {
            get { return _XRot; }
            set { _XRot = value; }
        }

        public float YRot
        {
            get { return _YRot; }
            set { _YRot = value; }
        }

        public float ZRot
        {
            get { return _ZRot; }
            set { _ZRot = value; }
        }

        public ushort Variable
        {
            get { return _Variable; }
            set { _Variable = value; }
        }

        public byte FrontSwitchTo
        {
            get { return _FrontSwitchTo; }
            set { _FrontSwitchTo = value; }
        }

        public byte FrontCamera
        {
            get { return _FrontCamera; }
            set { _FrontCamera = value; }
        }

        public byte BackSwitchTo
        {
            get { return _BackSwitchTo; }
            set { _BackSwitchTo = value; }
        }

        public byte BackCamera
        {
            get { return _BackCamera; }
            set { _BackCamera = value; }
        }

        public byte SpawnRoom
        {
            get { return _SpawnRoom; }
            set { _SpawnRoom = value; }
        }

        public bool[] IgnoreMMRot
        {
            get { return _IgnoreMMRot; }
            set { _IgnoreMMRot = value; }
        }
        /*
        [XmlIgnore]
        public sbyte NoMMYRot
        {
            get {

                if (_NoMMYRot == -1)
                {
                    _NoMMYRot = (sbyte)XMLreader.getActorNoMMYRot(Number.ToString("X4"));
                }
                return _NoMMYRot;
            }
            set { _NoMMYRot = value; }
        }

    */
        

        public ZActor Linked
        {
            get { return _Linked; }
            set { _Linked = value; }
        }

        public ZActor() { }

        public ZActor(byte frontswitchto, byte frontcamera, byte backswitchto, byte backcamera, ushort number, float xpos, float ypos, float zpos, float yrot, ushort variable)
        {
            IsTransition = true;
            _Number = number;
            _FrontSwitchTo = frontswitchto;
            _FrontCamera = frontcamera;
            _BackSwitchTo = backswitchto;
            _BackCamera = backcamera;
            _XPos = xpos;
            _YPos = ypos;
            _ZPos = zpos;
            _XRot = 0.0f;
            _YRot = yrot;
            _ZRot = 0.0f;
            _Variable = variable;
        }

        public ZActor(ushort number, float xpos, float ypos, float zpos, float xrot, float yrot, float zrot, ushort variable)
        {
            IsTransition = false;
            _FrontSwitchTo = 0x00;
            _FrontCamera = 0xFF;
            _BackSwitchTo = 0x01;
            _BackCamera = 0xFF;
            _Number = number;
            _XPos = xpos;
            _YPos = ypos;
            _ZPos = zpos;
            _XRot = xrot;
            _YRot = yrot;
            _ZRot = zrot;
            _Variable = variable;
        }

        public ZActor(byte spawnroom, ushort number, float xpos, float ypos, float zpos, float xrot, float yrot, float zrot, ushort variable)
        {
            _SpawnRoom = spawnroom;
            IsTransition = false;
            _FrontSwitchTo = 0x00;
            _FrontCamera = 0xFF;
            _BackSwitchTo = 0x01;
            _BackCamera = 0xFF;
            _Number = number;
            _XPos = xpos;
            _YPos = ypos;
            _ZPos = zpos;
            _XRot = xrot;
            _YRot = yrot;
            _ZRot = zrot;
            _Variable = variable;
        }

        public ZActor Clone()
        {
            return (ZActor)this.MemberwiseClone();
        }

        public int GetPropertyValue(ActorProperty property)
        {
            if (property.Target == "Var")
            {
                return ((Variable & property.Mask) >> property.Position);
            }
            else if (property.Target == "XRot")
            {
                return (((ushort)XRot & property.Mask) >> property.Position);
            }
            else if (property.Target == "YRot")
            {
                return (((ushort)YRot & property.Mask) >> property.Position);
            }
            else if (property.Target == "ZRot")
            {
                return (((ushort)ZRot & property.Mask) >> property.Position);
            }
            return 0;
        }
    }
}
