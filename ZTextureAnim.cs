using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharpOcarina
{
    

    public class ZTextureAnim
    {
        [XmlIgnore]
        public const int scroll = 1, blending = 2, texswap = 3, texframe = 4, camera = 5, condition = 6;
        [XmlIgnore]
        private byte _Width1, _Width2, _Height1, _Height2;
        [XmlIgnore]
        private sbyte _XVelocity1, _XVelocity2, _YVelocity1, _YVelocity2;
        [XmlIgnore]
        private bool _UseSecondLayer;
        [XmlIgnore]
        private uint _Type;
        [XmlIgnore]
        private uint  _FlagValue, _FlagBitwise;
        [XmlIgnore]
        private byte _FlagType;
        [XmlIgnore]
        private bool _FlagReverse;
        [XmlIgnore]
        private bool _Preview;
        [XmlIgnore]
        private byte _TempType;
        [XmlIgnore]
        private string _TextureSwap, _TextureSwap2;
        [XmlIgnore]
        private List<ZTextureAnimImage> _TextureSwapList;
        [XmlIgnore]
        private List<ZTextureAnimColor> _ColorList;
        [XmlIgnore]
        private bool _Freeze;
        [XmlIgnore]
        private bool _FreezeAtEnd;
        [XmlIgnore]
        private byte _CameraEffect;


        public ZTextureAnim()
        {
            _TextureSwapList = new List<ZTextureAnimImage>();
            _ColorList = new List<ZTextureAnimColor>();
        }

        public ZTextureAnim(uint type)
        {
            _Type = type;
            _Width1 = 32;
            _Height1 = 32;
            _Width2 = 32;
            _Height2 = 32;
            _Preview = true;
            _FlagType = 0xFF;
            _TextureSwapList = new List<ZTextureAnimImage>();
            _ColorList = new List<ZTextureAnimColor>();
            _Freeze = true;
            _FreezeAtEnd = false;

        }

        public sbyte XVelocity1
        {
            get { return _XVelocity1; }
            set { _XVelocity1 = value; }
        }

        public sbyte XVelocity2
        {
            get { return _XVelocity2; }
            set { _XVelocity2 = value; }
        }

        public sbyte YVelocity1
        {
            get { return _YVelocity1; }
            set { _YVelocity1 = value; }
        }

        public sbyte YVelocity2
        {
            get { return _YVelocity2; }
            set { _YVelocity2 = value; }
        }

        public byte Width1
        {
            get { return _Width1; }
            set { _Width1 = value; }
        }

        public byte Width2
        {
            get { return _Width2; }
            set { _Width2 = value; }
        }

        public byte Height1
        {
            get { return _Height1; }
            set { _Height1 = value; }
        }

        public byte Height2
        {
            get { return _Height2; }
            set { _Height2 = value; }
        }

        public bool UseSecondLayer
        {
            get { return (_XVelocity2 != 0 || _YVelocity2 != 0); }
        }

        public uint Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public bool FlagReverse
        {
            get { return _FlagReverse; }
            set { _FlagReverse = value; }
        }

        public uint FlagValue
        {
            get { return _FlagValue; }
            set { _FlagValue = value; }
        }


        public uint FlagBitwise
        {
            get { return _FlagBitwise; }
            set { _FlagBitwise = value; }
        }

        public byte FlagType
        {
            get { return _FlagType; }
            set { _FlagType = value; }
        }

        public bool Preview
        {
            get { return _Preview; }
            set { _Preview = value; }
        }

        public bool Freeze
        {
            get { return _Freeze; }
            set { _Freeze = value; }
        }

        public bool FreezeAtEnd
        {
            get { return _FreezeAtEnd; }
            set { _FreezeAtEnd = value; }
        }

        [XmlIgnore]
        public byte TempType
        {
            get { return _TempType; }
            set { _TempType = value; }
        }

        public string TextureSwap
        {
            get { return _TextureSwap; }
            set { _TextureSwap = value; }
        }

        public string TextureSwap2
        {
            get { return _TextureSwap2; }
            set { _TextureSwap2 = value; }
        }

        public List<ZTextureAnimImage> TextureSwapList
        {
            get { return _TextureSwapList; }
            set { _TextureSwapList = value; }
        }

        public List<ZTextureAnimColor> ColorList
        {
            get { return _ColorList; }
            set { _ColorList = value; }
        }

        public byte CameraEffect
        {
            get { return _CameraEffect; }
            set { _CameraEffect = value; }
        }
        /*
        public ZTextureAnim Clone()
        {
            ZTextureAnim clone = (ZTextureAnim)this.MemberwiseClone();
            
            return clone;
        }*/
    }

    public class ZSegmentFunction
    {
        [XmlIgnore]
        private List<ZTextureAnim> _Functions;

        public ZSegmentFunction()
        {
            _Functions = new List<ZTextureAnim>();
        }

        public List<ZTextureAnim> Functions
        {
            get { return _Functions; }
            set { _Functions = value; }
        }

        public bool HasScroll()
        {
            return _Functions.Exists(x => x.Type == ZTextureAnim.scroll);
        }

        public bool HasBlending()
        {
            return _Functions.Exists(x => x.Type == ZTextureAnim.blending);
        }

        public bool HasPointer()
        {
            return _Functions.Exists(x => x.Type == ZTextureAnim.texswap || x.Type == ZTextureAnim.texframe);
        }

        public bool HasConditional()
        {
            return _Functions.Exists(x => x.Type == ZTextureAnim.condition);
        }

    }

    public class ZTextureAnimImage
    {
        [XmlIgnore]
        private ushort _Duration;
        [XmlIgnore]
        private string _Texture;

        public ZTextureAnimImage()
        {
            _Duration = 1;
        }


        public ushort Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }

        public string Texture
        {
            get { return _Texture; }
            set { _Texture = value; }
        }
    }
    public class ZTextureAnimColor
    {
        [XmlIgnore]
        private ushort _Duration;
        [XmlIgnore]
        private ColorWrapper _Color;

        public ZTextureAnimColor()
        {
            _Duration = 1;
            _Color = new ColorWrapper();
            C1C = System.Drawing.Color.Black;
        }

        public ushort Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }

        public int Color
        {
            get { return _Color.Argb; }
            set { _Color.Argb = value; }
        }

        [XmlIgnore]
        public Color C1C
        {
            get { return _Color.Color; }
            set { _Color.Color = value; }
        }
    }
}
