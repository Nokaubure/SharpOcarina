using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OpenTK;

namespace SharpOcarina
{
    public class ZCamera
    {
        [XmlIgnore]
        private short _XPos, _YPos, _ZPos;
        [XmlIgnore]
        private short _XRot, _YRot, _ZRot;
        [XmlIgnore]
        private byte _Type;
        [XmlIgnore]
        private short _Fov;
        [XmlIgnore]
        private ushort _Unk1 = 0xFFFF, _Unk2 = 0xFFFF;
        [XmlIgnore]
        private ushort _Unk12,_Unk14,_Unk16,_Unk18,_Unk1A,_Unk1C,_Unk1E,_Unk20,_Unk22;


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

        public short XRot
        {
            get { return _XRot; }
            set { _XRot = value; }
        }

        public short YRot
        {
            get { return _YRot; }
            set { _YRot = value; }
        }

        public short ZRot
        {
            get { return _ZRot; }
            set { _ZRot = value; }
        }

        public byte Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public short Fov
        {
            get { return _Fov; }
            set { _Fov = value; }
        }

        public ushort Unk1
        {
            get { return _Unk1; }
            set { _Unk1 = value; }
        }

        public ushort Unk2
        {
            get { return _Unk2; }
            set { _Unk2 = value; }
        }

        public ushort Unk12
        {
            get { return _Unk12; }
            set { _Unk12 = value; }
        }

        public ushort Unk14
        {
            get { return _Unk14; }
            set { _Unk14 = value; }
        }

        public ushort Unk16
        {
            get { return _Unk16; }
            set { _Unk16 = value; }
        }

        public ushort Unk18
        {
            get { return _Unk18; }
            set { _Unk18 = value; }
        }

        public ushort Unk1A
        {
            get { return _Unk1A; }
            set { _Unk1A = value; }
        }

        public ushort Unk1C
        {
            get { return _Unk1C; }
            set { _Unk1C = value; }
        }

        public ushort Unk1E
        {
            get { return _Unk1E; }
            set { _Unk1E = value; }
        }

        public ushort Unk20
        {
            get { return _Unk20; }
            set { _Unk20 = value; }
        }

        public ushort Unk22
        {
            get { return _Unk22; }
            set { _Unk22 = value; }
        }


        public Vector3 Position
        {
            get { return new Vector3(XPos,YPos,ZPos); }
        }

        public ZCamera() { }

        public ZCamera(short XPos, short YPos, short ZPos, short XRot, short YRot, short ZRot, byte Type, short Fov, ushort unk1, ushort unk2)
        {
            _XPos = XPos;
            _YPos = YPos;
            _ZPos = ZPos;
            _XRot = XRot;
            _YRot = YRot;
            _ZRot = ZRot;
            _Type = Type;
            _Fov = Fov;
            _Unk1 = unk1;
            _Unk2 = unk2;
        }


    }
}
