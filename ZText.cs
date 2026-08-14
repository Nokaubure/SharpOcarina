using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using ZeldaMsgPreview;
using System.Windows.Forms;

namespace SharpOcarina
{
    public class ZText_TableRecord
    {
        public ushort MessageID;
        public byte BoxType;
        public byte BoxPosition;
        public uint Offset;
        public List<byte> Data = new List<byte>();

        public ZText_TableRecord(ushort _MessageID, byte _BoxType, byte _BoxPosition, uint _Offset, List<byte> _Data)
        {
            MessageID = _MessageID;
            BoxType = _BoxType;
            BoxPosition = _BoxPosition;
            Offset = _Offset;
            Data = _Data;

        }
    }



    /*
    public enum EditorMode
    {
        None,
        ROMMode,
        ZZRPLMode,
        Z64ROMMode,
        ZZRPMode,
        FilesMode,
    }

    public enum OcarinaTextboxType
    {
        Black,
        Wooden,
        Blue,
        Ocarina,
        None_White,
        None_Black,
        Unknown_1,
        Unknown_2,
        Unknown_3,
        Unknown_4,
        Unknown_5,
        Credits,
        Unknown_6,
        Unknown_7,
        Unknown_8,
        Unknown_9
    }

    public enum MajoraTextboxType
    {
        Black,
        Wooden,
        Blue,
        Ocarina,
        None_White,
        None_Black,
        Black2,
        None,
        Blue2,
        Red,
        None2,
        Credits,
        None3,
        Bombers_Notebook,
        None4,
        Red2
    }

    class ZText_Importer
    {
        private List<short> lBomberMsgs;
        private ObservableCollection<Message> m_messageList;
        private readonly MemoryStream m_inputFile;

        public ZText_Importer()
        {
            m_messageList = new ObservableCollection<Message>();
        }

        

        public ZText_Importer(string fileName, EditorMode Mode, ROMVer ROMVersion, bool Credits = false)
        {
            List<ZText_TableRecord> tableRecordList = new List<ZText_TableRecord>();

            long offset = ROMInfo.ZZRPCodeFileTablePostion;
            long msgOffset = 0;
            long mmBombersOffset = 0;

            if (Mode != EditorMode.ZZRPMode)
            {
                offset = ROMInfo.GetTableOffset(ROMVersion, Credits);
                msgOffset = ROMInfo.GetMessagesOffset(ROMVersion, Credits);
                mmBombersOffset = ROMInfo.GetBomberNotebookOffset(ROMVersion);
            }

            string zzrpFolder = Path.GetDirectoryName(fileName);
            string codeFilePath = Path.Combine(zzrpFolder, "system", "code");
            string msgDataPath = Path.Combine(zzrpFolder, "misc", "nes_message_data_static");

            try
            {
                using (FileStream stream = new FileStream(Mode == EditorMode.ZZRPMode ? codeFilePath : fileName, FileMode.Open, FileAccess.Read))
                {
                    m_inputFile = new MemoryStream();
                    stream.CopyTo(m_inputFile);

                    EndianBinaryReader reader = new EndianBinaryReader(stream, Endian.Big);
                    reader.BaseStream.Seek(offset, 0);
              
                    //Read in message table records
                    while (reader.PeekReadInt16() != -1)
                    {
                        ZText_TableRecord mesRecord = new ZText_TableRecord(reader);

                        if (!Properties.Settings.Default.IgnoreDuplicatedMsg && tableRecordList.Find(x => x.MessageID == mesRecord.MessageID) != null)
                            throw new Exception("Duplicate message entry.");

                        tableRecordList.Add(mesRecord);

                        
                    }
                }

                using (FileStream stream = new FileStream(Mode == EditorMode.ZZRPMode ? msgDataPath : fileName, FileMode.Open, FileAccess.Read))
                {
                    m_messageList = new ObservableCollection<Message>();
                    EndianBinaryReader reader = new EndianBinaryReader(stream, Endian.Big);

                    foreach (var mesgRecord in tableRecordList)
                    {
                        reader.BaseStream.Position = msgOffset + mesgRecord.Offset;

                        long savedPos = reader.BaseStream.Position;

                        Message mes = new Message(reader, mesgRecord, Credits, ROMVersion);

                        long byteSz = reader.BaseStream.Position - savedPos;

                        if (byteSz > Properties.Settings.Default.MsgMaxSize)
                            throw new Exception("Entry exceeded maximum message size.");

                        if (App.charMap != null)
                            mes.TextData = Converters.CharMapTextConverter.RemapTextTo(mes.TextData);
    
                        m_messageList.Add(mes);
                    }
                }

                if (ROMInfo.IsMajoraMask(ROMVersion) && mmBombersOffset != 0)
                {
                    using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        lBomberMsgs = new List<short>();
                        EndianBinaryReader reader = new EndianBinaryReader(stream, Endian.Big);

                        reader.BaseStream.Position = mmBombersOffset;

                        while (true)
                        {
                            short MsgID = reader.ReadInt16();

                            if (MsgID == 0)
                                break;
                            else
                                lBomberMsgs.Add(MsgID);
                        }
                    }
                }
                else
                    lBomberMsgs = new List<short>();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception exz)
            {
                MessageBox.Show($"Failed loading messages: {exz.Message} Are you sure your ROM is decompressed?");
                return;
            }
        }


        public ZText_Importer(string tableFileName, string messageDataFileName, ROMVer Version, bool Credits = false)
        {
            m_messageList = new ObservableCollection<Message>();

            List<ZText_TableRecord> tableRecordList = new List<ZText_TableRecord>();

            try
            {

                //Read in message table records
                using (FileStream stream = new FileStream(tableFileName, FileMode.Open))
                {
                    EndianBinaryReader reader = new EndianBinaryReader(stream, Endian.Big);

                    while (reader.BaseStream.Position != reader.BaseStream.Length && reader.PeekReadInt16() != -1)
                    {
                        ZText_TableRecord mesRecord = new ZText_TableRecord(reader);

                        if (!Properties.Settings.Default.IgnoreDuplicatedMsg && tableRecordList.Find(x => x.MessageID == mesRecord.MessageID) != null)
                            throw new Exception("Duplicate message entry.");

                        tableRecordList.Add(mesRecord);
                    }
                }

                //Read in message data
                using (FileStream stream = new FileStream(messageDataFileName, FileMode.Open))
                {
                    EndianBinaryReader reader = new EndianBinaryReader(stream, Endian.Big);

                    foreach (var mesgRecord in tableRecordList)
                    {
                        if (mesgRecord.Offset >= reader.BaseStream.Length)
                            continue;

                        reader.BaseStream.Position = mesgRecord.Offset;
                        Message mes = new Message(reader, mesgRecord, Credits, Version);

                        long byteSz = reader.BaseStream.Position - mesgRecord.Offset;

                        if (byteSz > Properties.Settings.Default.MsgMaxSize)
                            throw new Exception("Entry exceeded maximum message size.");

                        if (App.charMap != null)
                            mes.TextData = Converters.CharMapTextConverter.RemapTextTo(mes.TextData);

                        m_messageList.Add(mes);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception exz)
            {
                MessageBox.Show($"Failed loading messages: {exz.Message} Are you sure you chose the right game?");
                return;
            }
        }

        public ObservableCollection<Message> GetMessageList()
        {
            return m_messageList;
        }

        public List<short> GetBomberMsgsList()
        {
            return lBomberMsgs ?? new List<short>();
        }

        public MemoryStream GetInputFile()
        {
            return m_inputFile;
        }
    }

    public class ZText_TableRecord
    {
        public short MessageID { get; set; }
        public OcarinaTextboxType BoxType { get; set; }
        public TextboxPosition BoxPosition { get; set; }

        public uint Offset
        {
            get { return offset; }
            set { offset = value & 0x00FFFFFF; }
        }
        private uint offset;

        public ZText_TableRecord(EndianBinaryReader reader)
        {
            MessageID = reader.ReadInt16();

            byte typePosField = reader.ReadByte();

            BoxType = (OcarinaTextboxType)((typePosField & 0xF0) >> 4);
            BoxPosition = (TextboxPosition)(typePosField & 0x0F);

            reader.SkipByte();

            Offset = reader.ReadUInt32();
        }

        public ZText_TableRecord()
        {

        }
    }
    */
}
