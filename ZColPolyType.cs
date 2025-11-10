using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SharpOcarina
{
    public class ZColPolyType
    {
        public ZColPolyType() { }

        public ZColPolyType(ulong _Raw)
        {
            Raw = _Raw;
        }

        public ZColPolyType(ulong _Raw, ushort _PolyFlagA)
        {
            Raw = _Raw;
            PolyFlagA = _PolyFlagA;
        }

        public ZColPolyType(ulong _Raw, ushort _PolyFlagA, ushort _PolyFlagB)
        {
            Raw = _Raw;
            PolyFlagA = _PolyFlagA;
            PolyFlagB = _PolyFlagB;
        }



        public ulong Raw;
        public ushort PolyFlagA = 0, PolyFlagB = 0;

        [XmlIgnore]
        public long ExitNumber
        {
            get { return (long)((Raw & 0x00001F0000000000) >> 40); }
            set { Raw = ((Raw & 0xFFFFE0FFFFFFFFFF) | ((ulong)(value & 0x1F) << 40)); }
        }

        [XmlIgnore]
        public long ClimbingCrawlingFlags
        {
            get { return (long)((Raw & 0x00F0000000000000) >> 52); }
            set { Raw = ((Raw & 0xFF0FFFFFFFFFFFFF) | ((ulong)(value & 0xF) << 52)); }
        }

        [XmlIgnore]
        public long DamageSurfaceFlags
        {
            get { return (long)((Raw & 0x000FF00000000000) >> 44); }
            set { Raw = ((Raw & 0xFFF00FFFFFFFFFFF) | ((ulong)(value & 0xFF) << 44)); }
        }

        [XmlIgnore]
        public bool IsHookshotable
        {
            get { return ((Raw & 0x0000000000020000) != 0); }
            set { if (value == true) { Raw |= 0x20000; } else { Raw &= ~((ulong)0x20000); } }
        }

        [XmlIgnore]
        public int EchoRange
        {
            get { return (int)((Raw & 0x000000000000F000) >> 12); }
            set { Raw = ((Raw & 0xFFFFFFFFFFFF0FFF) | ((ulong)(value & 0xF) << 12)); }
        }

        [XmlIgnore]
        public int EnvNumber
        {
            get { return (int)((Raw & 0x0000000000000F00) >> 8); }
            set { Raw = ((Raw & 0xFFFFFFFFFFFFF0FF) | ((ulong)(value & 0xF) << 8)); }
        }

        [XmlIgnore]
        public bool IsSteep
        {
            get { return ((Raw & 0x0000000000000030) == 0x10); }
            set { if (value == true) { Raw |= 0x10; } else { Raw &= ~((ulong)0x10); } }
        }

        [XmlIgnore]
        public int TerrainType
        {
            get { return (int)((Raw & 0x00000000000000F0) >> 4); }
            set { Raw = ((Raw & 0xFFFFFFFFFFFFFF0F) | ((ulong)(value & 0xF) << 4)); }
        }

        [XmlIgnore]
        public int SoundEffect
        {
            get { return (int)(Raw & 0x000000000000000F); }
            set { Raw = ((Raw & 0xFFFFFFFFFFFFFFF0) | (ulong)((uint)(value & 0xF))); }
        }

        [XmlIgnore]
        public long CameraAngle
        {
            get { return (long)((Raw & 0x000000FF00000000) >> 32); }
            set { Raw = ((Raw & 0xFFFFFF00FFFFFFFF) | (ulong)((value & 0xFF) << 32)); }
        }

        [XmlIgnore]
        public long FirstByteFlags
        {
            get { return (long)((Raw & 0xFF00000000000000) >> 56); }
            set { Raw = ((Raw & 0x00FFFFFFFFFFFFFF) | ((ulong)(value & 0xFF) << 56)); }
        }

        [XmlIgnore]
        public int Unk1
        {
            get { return (int)((Raw & 0x0000000007E00000) >> 21); }
            set { Raw = ((Raw & 0xFFFFFFFFF81FFFFF) | ((ulong)(value) << 21)); }
        }

        [XmlIgnore]
        public int Unk2
        {
            get { return (int)((Raw & 0x00000000001C0000) >> 18); }
            set { Raw = ((Raw & 0xFFFFFFFFFFE3FFFF) | ((ulong)(value) << 18)); }
        }
        [XmlIgnore]
        public bool IsWallDamage
        {
            get { return ((Raw & 0x0000000008000000) != 0); }
            set { if (value == true) { Raw |= 0x0000000008000000; } else { Raw &= ~((ulong)0x0000000008000000); } }
        }
        [XmlIgnore]
        public bool Lower1Unit
        {
            get { return ((Raw & 0x4000000000000000) != 0); }
            set { if (value == true) { Raw |= 0x4000000000000000; } else { Raw &= ~((ulong)0x4000000000000000); } }
        }
        [XmlIgnore]
        public bool EponaBlock
        {
            get { return ((Raw & 0x8000000000000000) != 0); }
            set { if (value == true) { Raw |= 0x8000000000000000; } else { Raw &= ~((ulong)0x8000000000000000); } }
        }
        [XmlIgnore]
        public int FloorFlags
        {
            get { return (int)((Raw & 0x3C00000000000000) >> 58); }
            set { Raw = ((Raw & 0xC3FFFFFFFFFFFFFF) | ((ulong)(value) << 58)); }
        }

        [XmlIgnore]
        public int WallFlags
        {
            get { return (int)((Raw & 0x03E0000000000000) >> 53); }
            set { Raw = ((Raw & 0xFC1FFFFFFFFFFFFF) | ((ulong)(value) << 53)); }
        }

        [XmlIgnore]
        public int SpecialFlags
        {
            get { return (int)((Raw & 0x0003E00000000000) >> 45); }
            set { Raw = ((Raw & 0xFFFC1FFFFFFFFFFF) | ((ulong)(value) << 45)); }
        }

        public ZColPolyType Clone()
        {
            return (ZColPolyType)this.MemberwiseClone();
        }

    }
}
