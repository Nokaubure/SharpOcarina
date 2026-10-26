using ExtendedXmlSerializer.Core.Sprache;
using OpenTK;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

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
        [XmlIgnore]
        public List<ZCutscenePosition> CachePoints;
        [XmlIgnore]
        public bool previewed = false;

        public const int CS_CAMERA = 0x1;
        public const int CS_CAMERAFOCUS = 0x2;
        public const int CS_ACTOR = 0x3;
        public const int CS_ENV = 0x4;
        public const int CS_CAMERARELATIVE = 0x5;
        public const int CS_CAMERARELATIVEFOCUS = 0x6;
        public const int CS_UNK = 0x9;
        public const int CS_LINK = 0xA;
        public const int CS_TEXTBOX = 0x13;
        public const int CS_PLAYBGM = 0x56;
        public const int CS_STOPBGM = 0x57;
        public const int CS_FADEBGM = 0x7C;
        public const int CS_TRANSITION = 0x2D;
        public const int CS_TIME = 0x8C;
        public const int CS_ASM = 0x3E8;
        //z64rom IDs
        public const int CS_EXIT = 0xC000;
        public const int CS_MOTIONBLUR = 0xC001;
        public const int CS_PLAYSOUND = 0xC002;
        public const int CS_SETFLAG = 0xC003;

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
            if (Marker == 0x13 && Textboxes.Count > 0)
            {
                result = Textboxes[Textboxes.Count - 1].StartFrame + Textboxes[Textboxes.Count - 1].Frames - 1;
            }
            else
            {
                foreach (ZCutscenePosition point in Points) result += point.Frames;
                foreach (ZTextbox textbox in Textboxes) result += textbox.Frames;
                foreach (ZCutsceneActor actor in CutsceneActors) result += actor.Frames;
            }
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

        public string ConvertToC()
        {
            StringBuilder output = new StringBuilder();
            Action<string, object[]> command = (name, args) => output.AppendFormat(System.Globalization.CultureInfo.InvariantCulture,
                "    {0}({1}),\n", name, string.Join(", ", args.Select(x => Convert.ToString(x, System.Globalization.CultureInfo.InvariantCulture))));
            if (Marker == CS_CAMERA || Marker == CS_CAMERARELATIVE)
            {
                List<ZCutscenePosition> points = Points.ConvertAll(x => x.Clone());
                if (points.Count == 0) throw new InvalidOperationException("A camera cutscene command has no points.");
                int end = StartFrame + GetTotalFrames();
                if (!MainForm.settings.NoDummyPoints)
                {
                    ZCutscenePosition first = points[0];
                    ZCutscenePosition last = points[points.Count - 1];
                    while (points.Count < 4) points.Add(new ZCutscenePosition(last.Cameraroll, 0, 60, last.Position, last.Position2));
                    points.Add(new ZCutscenePosition(0, 0, last.Angle, last.Position, last.Position2));
                    points.Add(new ZCutscenePosition(0, 0, last.Angle, last.Position, last.Position2));
                    points.Insert(0, new ZCutscenePosition(0, 1, first.Angle, first.Position, first.Position2));
                    points.Insert(0, new ZCutscenePosition(0, 1, first.Angle, first.Position, first.Position2));
                }
                for (int focus = 0; focus < 2; focus++)
                {
                    string macro = focus == 0 ? "CS_CAM_POS" : "CS_CAM_FOCUS_POINT";
                    if (Marker == CS_CAMERARELATIVE) macro += "_PLAYER";
                    command(macro + "_LIST", new object[] { StartFrame, end });
                    for (int i = 0; i < points.Count; i++)
                    {
                        ZCutscenePosition point = points[i];
                        Vector3 pos = focus == 0 ? point.Position : point.Position2;
                        command("    " + macro, new object[] { i == points.Count - 1 ? "CS_CMD_STOP" : "CS_CMD_CONTINUE",
                            focus == 0 ? 0 : point.Cameraroll, focus == 0 ? 0 : point.Frames,
                            focus == 0 ? "0.0f" : CFloat(point.Angle), (short)pos.X, (short)pos.Y, (short)pos.Z, 0 });
                    }
                }
            }
            else if (Marker == CS_TEXTBOX)
            {
                command("CS_TEXT_LIST", new object[] { Textboxes.Count * 2 });
                int lastEnd = 0;
                foreach (ZTextbox text in Textboxes)
                {
                    command("    CS_TEXT_NONE", new object[] { lastEnd, text.StartFrame });
                    lastEnd = text.StartFrame + text.Frames;
                    command("    CS_TEXT_DISPLAY_TEXTBOX", new object[] { "0x" + text.Message.ToString("X4"), text.StartFrame,
                        lastEnd, text.Type, text.Type < 2 ? text.TopMessage : 0xFFFF, text.Type < 2 ? text.BottomMessage : 0xFFFF });
                }
            }
            else if (Marker == CS_TRANSITION || Marker == CS_ASM)
                command(Marker == CS_TRANSITION ? "CS_SCENE_TRANS_FX" : "CS_TERMINATOR", new object[] { Data[0], StartFrame, EndFrame });
            else if (Marker == CS_TIME)
            {
                command("CS_TIME_LIST", new object[] { 1 });
                command("    CS_TIME", new object[] { 0, StartFrame, 0, Data[0], Data[1], 0 });
            }
            else if (Marker >= CS_EXIT)
            {
                List<byte> data = new List<byte>();
                Helpers.Append32(ref data, Marker);
                Helpers.Append32(ref data, 1);
                ushort value = Data[0];
                if (Marker == CS_SETFLAG) value = (ushort)(((byte)(Data[1] | (Data[2] << 7)) << 8) | (byte)Data[0]);
                Helpers.Append16(ref data, value);
                Helpers.Append16(ref data, StartFrame);
                Helpers.Append16(ref data, Marker == CS_EXIT || Marker == CS_SETFLAG ? StartFrame : EndFrame);
                if (Marker == CS_PLAYSOUND)
                {
                    data.Add((byte)(Data[1] | (Data[5] << 7)));
                    data.Add((byte)Data[2]);
                    Helpers.Append32(ref data, Helpers.FloatToHex(Data[3] / 100.0f));
                    Helpers.Append32(ref data, Helpers.FloatToHex(Data[4] / 100.0f));
                    data.AddRange(new byte[8]);
                }
                // Binary cutscene commands are padded to eight-byte boundaries.
                while (data.Count % 8 != 0) data.Add(0);
                output.Append("    /*0x" + Marker.ToString("X4") + @"*\ ");
                for (int i = 0; i < data.Count; i += 4)
                    output.AppendFormat("0x{0:X8}, ", Helpers.Read32(data, i));
                output.AppendLine();
            }
            else
            {
                string macro;
                switch (Marker)
                {
                    case CS_ACTOR: macro = "CS_MISC"; break;
                    case CS_ENV: macro = "CS_LIGHTING"; break;
                    case CS_PLAYBGM: macro = "CS_PLAY_BGM"; break;
                    case CS_STOPBGM: macro = "CS_STOP_BGM"; break;
                    case CS_FADEBGM: macro = "CS_FADE_BGM"; break;
                    case CS_LINK: macro = "CS_PLAYER_ACTION"; break;
                    default: macro = "CS_NPC_ACTION"; break;
                }
                command(macro + "_LIST", macro == "CS_NPC_ACTION" ? new object[] { "0x" + Marker.ToString("X"), CutsceneActors.Count } : new object[] { CutsceneActors.Count });
                int frame = StartFrame;
                foreach (ZCutsceneActor actor in CutsceneActors)
                {
                    List<object> args = new List<object> { actor.Animation, frame, frame + actor.Frames,
                        "0x" + ((ushort)actor.Rotation.X).ToString("X4") };
                    if (macro == "CS_NPC_ACTION" || macro == "CS_PLAYER_ACTION")
                    {
                        args.Add("0x" + ((ushort)actor.Rotation.Y).ToString("X4"));
                        args.Add("0x" + ((ushort)actor.Rotation.Z).ToString("X4"));
                    }
                    else args.Add("0x" + (((uint)(ushort)actor.Rotation.Y << 16) | (ushort)actor.Rotation.Z).ToString("X8"));
                    args.AddRange(new object[] { (int)actor.Position.X, (int)actor.Position.Y, (int)actor.Position.Z,
                        (int)actor.Position2.X, (int)actor.Position2.Y, (int)actor.Position2.Z });
                    if (macro == "CS_NPC_ACTION" || macro == "CS_PLAYER_ACTION") args.AddRange(new object[] { "0.0f", "0.0f", CFloat(BitConverter.ToSingle(BitConverter.GetBytes(1), 0)) });
                    else if (macro == "CS_MISC") args.AddRange(new object[] { 0, 0, 1 });
                    command("    " + macro, args.ToArray());
                    frame += actor.Frames;
                }
            }
            return output.ToString();
        }

        private static string CFloat(float value)
        {
            string result = value.ToString("R", System.Globalization.CultureInfo.InvariantCulture);
            if (!result.Contains(".") && !result.Contains("E") && !result.Contains("e")) result += ".0";
            return result + "f";
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
        private ushort _StartFrame;
        [XmlIgnore]
        private ushort _Message;
        [XmlIgnore]
        private ushort _TopMessage;
        [XmlIgnore]
        private ushort _BottomMessage;
        [XmlIgnore]
        public Bitmap[] PreviewImage;
        [XmlIgnore]
        public ZeldaMsgPreview.Message MsgPreview;
        [XmlIgnore]
        public int[] FadeDuration;
        [XmlIgnore]
        public bool previewed;

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

        public ushort StartFrame
        {
            get { return _StartFrame; }
            set { _StartFrame = value; }
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
