using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SharpOcarina
{
    public class ZActorCutscene
    {

        [XmlIgnore]
        private ushort _Unk = 0x350, _CamID = 0;
        [XmlIgnore]
        private short _Length = 1, _CsID = -1, _ActorCsID = -1, _HUDFade;
        [XmlIgnore]
        private byte _Sound, _ChestCs, _ReturnCamType, _Letterbox;


        public ushort Unk
        {
            get { return _Unk; }
            set { _Unk = value; }
        }

        public short Length
        {
            get { return _Length; }
            set { _Length = value; }
        }


        public ushort CamID
        {
            get { return _CamID; }
            set { _CamID = value; }
        }


        public short CsID
        {
            get { return _CsID; }
            set { _CsID = value; }
        }

        public short ActorCsID
        {
            get { return _ActorCsID; }
            set { _ActorCsID = value; }
        }


        public short HUDFade
        {
            get { return _HUDFade; }
            set { _HUDFade = value; }
        }

        public byte Sound
        {
            get { return _Sound; }
            set { _Sound = value; }
        }

        public byte ChestCs
        {
            get { return _ChestCs; }
            set { _ChestCs = value; }
        }

        public byte ReturnCamType
        {
            get { return _ReturnCamType; }
            set { _ReturnCamType = value; }
        }

        public byte Letterbox
        {
            get { return _Letterbox; }
            set { _Letterbox = value; }
        }



        public ZActorCutscene() { }

        public ZActorCutscene Clone()
        {
            return (ZActorCutscene)this.MemberwiseClone();
        }
    }
}
