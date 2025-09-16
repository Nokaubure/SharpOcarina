using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OpenTK;
using OpenTK.Audio.OpenAL;

namespace SharpOcarina
{
    public class ZCutscene
    {
        [XmlIgnore]
        private uint _Marker;

        [XmlIgnore]
        private ushort[] _Data;

        [XmlIgnore]
        private List<ZCutscenePosition> _Points;
        [XmlIgnore]
        private List<ZTextbox> _Textboxes;
        [XmlIgnore]
        private List<ZCutsceneActor> _CutsceneActors;
        [XmlIgnore]
        private ushort _StartFrame;
        [XmlIgnore]
        private ushort _EndFrame;

        public uint Marker
        {
            get { return _Marker; }
            set { _Marker = value; }
        }

        public ushort[] Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        public List<ZCutscenePosition> Points
        {
            get { return _Points; }
            set { _Points = value; }
        }

        public List<ZTextbox> Textboxes
        {
            get { return _Textboxes; }
            set { _Textboxes = value; }
        }

        public List<ZCutsceneActor> CutsceneActors
        {
            get { return _CutsceneActors; }
            set { _CutsceneActors = value; }
        }

        public ushort StartFrame
        {
            get { return _StartFrame; }
            set { _StartFrame = value; }
        }

        public ushort EndFrame
        {
            get { return _EndFrame; }
            set { _EndFrame = value; }
        }


        public ZCutscene() { }

        public ZCutscene(uint Marker, ushort[] Data, List<ZCutscenePosition> Points, List<ZTextbox> Textboxes, List<ZCutsceneActor> CutsceneActors, ushort StartFrame, ushort EndFrame)
        {
            _Marker = Marker;
            _Data = Data;
            _Points = Points;
            _Textboxes = Textboxes;
            _StartFrame = StartFrame;
            _EndFrame = EndFrame;
            _CutsceneActors = CutsceneActors;
        }

        public int GetTotalFrames()
        {
            int result = 0;
            foreach (ZCutscenePosition point in Points) result += point.Frames;
            foreach (ZTextbox textbox in Textboxes) result += textbox.Frames;
            foreach (ZCutsceneActor actor in CutsceneActors) result += actor.Frames;
            if (result == 0) result = EndFrame - StartFrame;
            return result;
        }

        public ZCutscene Clone()
        {
            ZCutscene clone = (ZCutscene)this.MemberwiseClone();
            clone._Points = Points.ConvertAll(x => (x.Clone()));
            clone._Textboxes = Textboxes.ConvertAll(x => (x.Clone()));
            clone._CutsceneActors = CutsceneActors.ConvertAll(x => (x.Clone()));

            return clone;
        }

    }
    public class ZCutscenePosition
    {
        [XmlIgnore]
        private sbyte _Cameraroll;
        [XmlIgnore]
        private ushort _Frames;
        [XmlIgnore]
        private float _Angle;
        [XmlIgnore]
        private Vector3 _Position;
        [XmlIgnore]
        private Vector3 _Position2;

        public sbyte Cameraroll
        {
            get { return _Cameraroll; }
            set { _Cameraroll = value; }
        }

        public ushort Frames
        {
            get { return _Frames; }
            set { _Frames = value; }
        }

        public float Angle
        {
            get { return _Angle; }
            set { _Angle = value; }
        }

        public Vector3 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public Vector3 Position2
        {
            get { return _Position2; }
            set { _Position2 = value; }
        }

        public ZCutscenePosition() { }

        public ZCutscenePosition(sbyte Cameraroll, ushort Frames, float Angle, Vector3 Position, Vector3 Position2)
        {
            _Cameraroll = Cameraroll;
            _Frames = Frames;
            _Angle = Angle;
            _Position = Position;
            _Position2 = Position2;
        }

        public ZCutscenePosition Clone()
        {
            return (ZCutscenePosition)this.MemberwiseClone();
        }
    }
    public class ZTextbox
    {
        [XmlIgnore]
        private byte _Type;
        [XmlIgnore]
        private ushort _Frames;
        [XmlIgnore]
        private ushort _Message;
        [XmlIgnore]
        private ushort _TopMessage;
        [XmlIgnore]
        private ushort _BottomMessage;

        public byte Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public ushort Frames
        {
            get { return _Frames; }
            set { _Frames = value; }
        }

        public ushort Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public ushort TopMessage
        {
            get { return _TopMessage; }
            set { _TopMessage = value; }
        }

        public ushort BottomMessage
        {
            get { return _BottomMessage; }
            set { _BottomMessage = value; }
        }

        public ZTextbox() { }

        public ZTextbox(byte Type, ushort Frames, ushort Message, ushort TopMessage, ushort BottomMessage)
        {
            _Type = Type;
            _Frames = Frames;
            _Message = Message;
            _TopMessage = TopMessage;
            _BottomMessage = BottomMessage;
        }

        public ZTextbox Clone()
        {
            return (ZTextbox)this.MemberwiseClone();
        }
    }
    public class ZCutsceneActor
    {
        [XmlIgnore]
        private ushort _Frames;
        [XmlIgnore]
        private ushort _Animation;
        [XmlIgnore]
        private Vector3 _Position;
        [XmlIgnore]
        private Vector3 _Position2;
        [XmlIgnore]
        private Vector3 _Rotation;


        public ushort Frames
        {
            get { return _Frames; }
            set { _Frames = value; }
        }

        public ushort Animation
        {
            get { return _Animation; }
            set { _Animation = value; }
        }

        public Vector3 Rotation
        {
            get { return _Rotation; }
            set { _Rotation = value; }
        }


        public Vector3 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public Vector3 Position2
        {
            get { return _Position2; }
            set { _Position2 = value; }
        }

        public ZCutsceneActor() { }

        public ZCutsceneActor(ushort Animation, ushort Frames, Vector3 Position, Vector3 Position2, Vector3 Rotation)
        {
            _Animation = Animation;
            _Frames = Frames;
            _Position = Position;
            _Position2 = Position2;
            _Rotation = Rotation;
        }

        public ZCutsceneActor Clone()
        {
            return (ZCutsceneActor)this.MemberwiseClone();
        }
    }
    /*
     Display Text	iiiissss eeeeoooo
yyyynnnn

i = message id
s = start frame
e = end frame
o = Type. Set 0 for standard dialog, 1 for yes/no branching dialog
y = yes/no top option branch
n = yes/no bottom option branch	
Learn
Song	iiiissss eeee0002
mmmmFFFF

i = Ocarina Song Action
s = start frame
e = end frame
m = message id displayed when making a mistake. 088B is used here.
     * */
}
