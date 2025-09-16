using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SharpOcarina
{
    public class ZExit
    {
        public ZExit()
        {
            //HeaderIndex = 0xF; //why was 0xF
        }

        public ZExit(uint _Raw)
        {
            Raw = _Raw;
        }

        public uint Raw;

        [XmlIgnore]
        public bool MusicOn
        {
            get { return ((Raw & 0x40000000) != 0); }
            set { if (value == true) { Raw |= 0x40000000; } else { Raw &= ~((uint)0x40000000); } }
        }


        [XmlIgnore]
        public bool ShowTitleCard
        {
            get { return ((Raw & 0x20000000) != 0); }
            set { if (value == true) { Raw |= 0x20000000; } else { Raw &= ~((uint)0x20000000); } }

        }


        [XmlIgnore]
        public uint FadeIn
        {
            get { return (uint)((Raw & 0x1F800000) >> 23); }
            set { Raw = ((Raw & 0xE07FFFFF) | ((uint)(value & 0x3F) << 23)); }
        }

        [XmlIgnore]
        public uint FadeOut
        {
            get { return (uint)((Raw & 0x7E0000) >> 17); }
            set { Raw = ((Raw & 0xFF81FFFF) | ((uint)(value & 0x3F) << 17)); }
        }

        [XmlIgnore]
        public uint HeaderIndex
        {
            get { return (uint)((Raw & 0x1E000) >> 13); }
            set { Raw = ((Raw & 0xFFFE1FFF) | ((uint)(value & 0xF) << 13)); }
        }

        [XmlIgnore]
        public uint SpawnIndex
        {
            get { return (uint)((Raw & 0x1F00) >> 8); }
            set { Raw = ((Raw & 0xFFFFE0FF) | ((uint)(value & 0x1F) << 8)); }
        }

        [XmlIgnore]
        public uint SceneIndex
        {
            get { return (uint)((Raw & 0xFF)); }
            set { Raw = ((Raw & 0xFFFFFF00) | ((uint)(value & 0xFF))); }
        }

        public ZExit Clone()
        {
            return (ZExit)this.MemberwiseClone();
        }

    }
}
