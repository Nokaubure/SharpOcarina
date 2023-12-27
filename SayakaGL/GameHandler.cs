/* GameHandler stub to make Sayaka's UcodeSimulator happy
 * Messy, just like the general implementation of the simulator here as i'm basically shoehorning it into SO, but it works!
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TexLib;

namespace SharpOcarina.SayakaGL
{
    public static class GameHandler
    {
        public struct RAMSegmentStruct
        {
            public bool IsSet;
            public byte[] Data;
        }

        public static RAMSegmentStruct[] RAM;
        public static int InvalidTextureID;

        public static void Initialize()
        {
            RAM = new RAMSegmentStruct[16];

            byte[] TextureBuffer = new byte[32 * 32 * 4];
            TextureBuffer.Fill(new byte[] { 0xFF, 0x00, 0xFF, 0xFF });
            InvalidTextureID = TexUtil.CreateRGBATexture(32, 32, TextureBuffer);
        }

        public static bool IsAddressValid(UInt32 Address)
        {
            int Segment = (int)((Address & 0xFF000000) >> 24);
            int Offset = (int)(Address & 0x00FFFFFF);

            if (Segment > 15) return false;

            if (RAM[Segment].IsSet == false ||
                Offset >= RAM[Segment].Data.Length)
            {
                return false;
            }

            return true;
        }

        public static List<uint> GetDisplayLists(uint MeshHeaderOffset)
        {
            if (IsAddressValid(MeshHeaderOffset) == false) return null;

            List<uint> DLOffsets = new List<uint>();

            int Segment = (int)(MeshHeaderOffset >> 24);
            UInt32 Offset = (MeshHeaderOffset & 0x00FFFFFF);
            UInt32 MeshHeader = Read32(RAM[Segment].Data, Offset);

            int MeshType = (int)(MeshHeader >> 24);
            int MeshTotal = (int)((MeshHeader & 0x00FF0000) >> 16);

            DLOffsets = new List<UInt32>();

            DebugConsole.WriteLine(MeshHeaderOffset.ToString("X8"));

            DebugConsole.WriteLine(MeshHeader.ToString("X8"));

            UInt32 DL1, DL2;

            switch (MeshType)
            {
                case 0x00:
                    {
                        Offset += 12;
                        for (int i = 0; i < MeshTotal; i++)
                        {
                            DL1 = Read32(RAM[Segment].Data, Offset);
                            DL2 = Read32(RAM[Segment].Data, Offset + 4);
                            if (DL1 != 0) DLOffsets.Add(DL1);
                            if (DL2 != 0) DLOffsets.Add(DL2);

                            Offset += 8;
                        }
                        break;
                    }
                case 0x01:
                    {
                        Offset += 4;
                        DL1 = Read32(RAM[Segment].Data, Offset);
                        DL1 = Read32(RAM[Segment].Data, (DL1 & 0x00FFFFFF));
                        if (DL1 != 0) DLOffsets.Add(DL1);
                        break;
                    }
                case 0x02:
                    {
                        Offset += 12;
                        List<UInt32> SecondaryDLs = new List<UInt32>();
                        for (int i = 0; i < MeshTotal; i++)
                        {
                            DL1 = Read32(RAM[Segment].Data, Offset + 8);
                            DL2 = Read32(RAM[Segment].Data, Offset + 12);
                            if (DL1 != 0) DLOffsets.Add(DL1);
                            if (DL2 != 0) SecondaryDLs.Add(DL2);

                            Offset += 16;
                        }
                        DLOffsets.AddRange(SecondaryDLs);
                        break;
                    }
            }

            return DLOffsets;
        }

        public static void LoadToRAM(byte[] Data, int Segment)
        {
            RAM[Segment].Data = new byte[Data.Length];

            Buffer.BlockCopy(Data, 0, RAM[Segment].Data, 0, Data.Length);

            RAM[Segment].IsSet = true;
        }

        public static Byte Read8(byte[] Data, UInt32 Offset)
        {
            return (Buffer.GetByte(Data, (int)Offset));
        }

        public static UInt16 Read16(byte[] Data, UInt32 Offset)
        {
            return (UInt16)((Buffer.GetByte(Data, (int)Offset) << 8) | Buffer.GetByte(Data, (int)Offset + 1));
        }

        public static UInt32 Read32(byte[] Data, UInt32 Offset)
        {
            if (Offset >= Data.Length) { DebugConsole.WriteLine("offset out of bounds!" + Offset.ToString("X") );
                return 0;
            }
            return (UInt32)((Buffer.GetByte(Data, (int)Offset) << 24) | (Buffer.GetByte(Data, (int)Offset + 1) << 16) | (Buffer.GetByte(Data, (int)Offset + 2) << 8) | Buffer.GetByte(Data, (int)Offset + 3));
        }

        public static Int16 Read16S(byte[] Data, UInt32 Offset)
        {
            return (Int16)((Buffer.GetByte(Data, (int)Offset) << 8) | Buffer.GetByte(Data, (int)Offset + 1));
        }

        public static Int32 Read32S(byte[] Data, UInt32 Offset)
        {
            return (Int32)((Buffer.GetByte(Data, (int)Offset) << 24) | (Buffer.GetByte(Data, (int)Offset + 1) << 16) | (Buffer.GetByte(Data, (int)Offset + 2) << 8) | Buffer.GetByte(Data, (int)Offset + 3));
        }
    }
}
