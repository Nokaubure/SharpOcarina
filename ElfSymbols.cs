// ElfSymbols.cs
// .NET Framework 4.5 compatible
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SharpOcarina;
using static SharpOcarina.FaroresPlugin;

public class ElfSymbols
{
    const byte ELFMAG0 = 0x7F;
    const byte ELFMAG1 = (byte)'E';
    const byte ELFMAG2 = (byte)'L';
    const byte ELFMAG3 = (byte)'F';

    const byte ELFCLASS32 = 1;
    const byte ELFCLASS64 = 2;
    const byte ELFDATA2LSB = 1;
    const byte ELFDATA2MSB = 2;
    
    public static List<FunctionName> Start(string path, uint minStart = 0)
    {
        using (var fs = File.OpenRead(path))
        using (var br = new BinaryReader(fs))
        {
            var e = ReadIdent(br);
            if (e == null) { DebugConsole.WriteLine("Not ELF or unsupported."); return null; }

            if (e.Class == ELFCLASS32)
                return Process32(br, e, minStart);
            else if (e.Class == ELFCLASS64)
                Process64(br, e);
            else
                DebugConsole.WriteLine("Unsupported ELF class.");
        }
        return null;
    }

    class Ident { public byte Class; public byte Data; }

    static Ident ReadIdent(BinaryReader br)
    {
        br.BaseStream.Seek(0, SeekOrigin.Begin);
        byte[] magic = br.ReadBytes(4);
        if (magic.Length < 4 || magic[0] != ELFMAG0 || magic[1] != ELFMAG1 || magic[2] != ELFMAG2 || magic[3] != ELFMAG3)
            return null;
        var cls = br.ReadByte();   // EI_CLASS
        var data = br.ReadByte();  // EI_DATA
        // skip EI_VERSION, OSABI, ABIver + padding (we don't need them)
        return new Ident { Class = cls, Data = data };
    }

    // small helper for endianness; for this snippet we only support little-endian
    static bool IsLittleEndian(byte data) => data == ELFDATA2LSB;

    static List<FunctionName> Process32(BinaryReader br, Ident ident, uint minStart)
    {
        List<FunctionName> ret = new List<FunctionName>();
        bool be = ident.Data == ELFDATA2MSB; // TRUE for big-endian

        br.BaseStream.Seek(0, SeekOrigin.Begin);

        // ---- Read ELF32 header ----
        // e_shoff at offset 32 (0x20)
        br.BaseStream.Seek(0x20, SeekOrigin.Begin);
        uint e_shoff = ReadUInt32(br, be);

        // e_shentsize / e_shnum / e_shstrndx at offset 46 (0x2E)
        br.BaseStream.Seek(0x2E, SeekOrigin.Begin);
        ushort e_shentsize = ReadUInt16(br, be);
        ushort e_shnum = ReadUInt16(br, be);
        ushort e_shstrndx = ReadUInt16(br, be);

        // ---- Read section headers ----
        var sections = new List<Section32>();
        for (int i = 0; i < e_shnum; i++)
        {
            br.BaseStream.Seek(e_shoff + (uint)(i * e_shentsize), SeekOrigin.Begin);
            var sh = new Section32
            {
                sh_name = ReadUInt32(br, be),
                sh_type = ReadUInt32(br, be),
                sh_flags = ReadUInt32(br, be),
                sh_addr = ReadUInt32(br, be),
                sh_offset = ReadUInt32(br, be),
                sh_size = ReadUInt32(br, be),
                sh_link = ReadUInt32(br, be),
                sh_info = ReadUInt32(br, be),
                sh_addralign = ReadUInt32(br, be),
                sh_entsize = ReadUInt32(br, be)
            };
            sections.Add(sh);
        }

        // ---- Load section header string table ----
        if (e_shstrndx >= sections.Count)
        {
           DebugConsole.WriteLine("Invalid shstrndx");
            return null;
        }

        var shstr = ReadBytes(br, sections[e_shstrndx].sh_offset, sections[e_shstrndx].sh_size);
        Func<uint, string> GetName = off => ReadStringAt(shstr, off);

        // ---- Find .symtab ----
        int symIdx = -1;
        for (int i = 0; i < sections.Count; i++)
        {
            if (GetName(sections[i].sh_name) == ".symtab")
            {
                symIdx = i;
                break;
            }
        }
        if (symIdx < 0)
        {
           DebugConsole.WriteLine(".symtab not found.");
            return null;
        }

        var symtab = sections[symIdx];

        // ---- Load associated string table (.strtab) ----
        var strtab = sections[(int)symtab.sh_link];
        var strtabBytes = ReadBytes(br, strtab.sh_offset, strtab.sh_size);

        // ---- Parse symbol entries ----
        int count = (int)(symtab.sh_size / symtab.sh_entsize);

        for (int i = 0; i < count; i++)
        {
            long pos = symtab.sh_offset + (i * symtab.sh_entsize);
            br.BaseStream.Seek(pos, SeekOrigin.Begin);

            uint st_name = ReadUInt32(br, be);
            uint st_value = ReadUInt32(br, be);
            uint st_size = ReadUInt32(br, be);
            byte st_info = br.ReadByte();
            byte st_other = br.ReadByte();
            ushort st_shndx = ReadUInt16(br, be);

            int st_type = st_info & 0x0F;
            const int STT_FUNC = 2;
            const int SHN_UNDEF = 0;

            if (st_shndx != SHN_UNDEF && st_type == STT_FUNC)
            {
                //SO specific
                string name = ReadStringAt(strtabBytes, st_name).Replace("__vanilla_hook_","");
                if (!name.StartsWith("__") && st_value >= minStart)
                    ret.Add(new FunctionName(st_value, st_value+st_size, name));
                //DebugConsole.WriteLine($"{name} {st_value.ToString("X8")}");
            }
        }

        return ret;
    }
    struct Section32
    {
        public uint sh_name, sh_type, sh_flags, sh_addr, sh_offset, sh_size, sh_link, sh_info, sh_addralign, sh_entsize;
    }

    static void Process64(BinaryReader br, Ident ident)
    {
        if (!IsLittleEndian(ident.Data)) { DebugConsole.WriteLine("Big-endian ELF64 not supported in this snippet."); return; }

        br.BaseStream.Seek(0, SeekOrigin.Begin);
        // e_shoff at offset 40 (0x28) for ELF64
        br.BaseStream.Seek(0x28, SeekOrigin.Begin);
        ulong e_shoff = br.ReadUInt64();
        br.BaseStream.Seek(0x3A, SeekOrigin.Begin); // e_shentsize (2), e_shnum(2), e_shstrndx(2) at offset 58 (0x3A)
        ushort e_shentsize = br.ReadUInt16();
        ushort e_shnum = br.ReadUInt16();
        ushort e_shstrndx = br.ReadUInt16();

        var sections = new List<Section64>();
        for (int i = 0; i < e_shnum; i++)
        {
            br.BaseStream.Seek((long)(e_shoff + (ulong)(i * e_shentsize)), SeekOrigin.Begin);
            var sh = new Section64
            {
                sh_name = br.ReadUInt32(),
                sh_type = br.ReadUInt32(),
                sh_flags = br.ReadUInt64(),
                sh_addr = br.ReadUInt64(),
                sh_offset = br.ReadUInt64(),
                sh_size = br.ReadUInt64(),
                sh_link = br.ReadUInt32(),
                sh_info = br.ReadUInt32(),
                sh_addralign = br.ReadUInt64(),
                sh_entsize = br.ReadUInt64()
            };
            sections.Add(sh);
        }

        if (e_shstrndx >= sections.Count) { DebugConsole.WriteLine("Invalid shstrndx"); return; }
        var shstr = ReadBytes(br, sections[e_shstrndx].sh_offset, sections[e_shstrndx].sh_size);
        Func<uint, string> getName = idx => ReadStringAt(shstr, idx);

        int symtabIndex = -1;
        for (int i = 0; i < sections.Count; i++)
        {
            var name = getName(sections[i].sh_name);
            if (name == ".symtab") { symtabIndex = i; break; }
        }
        if (symtabIndex < 0) { DebugConsole.WriteLine(".symtab not found"); return; }
        var symtab = sections[symtabIndex];
        var strtab = sections[(int)symtab.sh_link];
        var strtabBytes = ReadBytes(br, strtab.sh_offset, strtab.sh_size);

        int count = (int)(symtab.sh_size / symtab.sh_entsize);
        for (int i = 0; i < count; i++)
        {
            long entryOff = (long)(symtab.sh_offset + (ulong)i * symtab.sh_entsize);
            br.BaseStream.Seek(entryOff, SeekOrigin.Begin);
            uint st_name = br.ReadUInt32();
            byte st_info = br.ReadByte();
            byte st_other = br.ReadByte();
            ushort st_shndx = br.ReadUInt16();
            ulong st_value = br.ReadUInt64();
            ulong st_size = br.ReadUInt64();

            int st_type = st_info & 0x0F;
            const int STT_FUNC = 2;
            if (st_type == STT_FUNC)
            {
                var name = ReadStringAt(strtabBytes, st_name);
                DebugConsole.WriteLine($"{name} {st_value.ToString("X8")}");
            }
        }
    }

    struct Section64
    {
        public uint sh_name;
        public uint sh_type;
        public ulong sh_flags;
        public ulong sh_addr;
        public ulong sh_offset;
        public ulong sh_size;
        public uint sh_link;
        public uint sh_info;
        public ulong sh_addralign;
        public ulong sh_entsize;
    }

    static byte[] ReadBytes(BinaryReader br, ulong offset, ulong size)
    {
        br.BaseStream.Seek((long)offset, SeekOrigin.Begin);
        return br.ReadBytes((int)size);
    }

    static string ReadStringAt(byte[] arr, uint idx)
    {
        if (idx >= arr.Length) return "";
        int i = (int)idx;
        var sb = new StringBuilder();
        while (i < arr.Length && arr[i] != 0)
        {
            sb.Append((char)arr[i]);
            i++;
        }
        return sb.ToString();
    }

    static ushort ReadUInt16(BinaryReader br, bool be)
    {
        var b = br.ReadBytes(2);
        if (be) Array.Reverse(b);
        return BitConverter.ToUInt16(b, 0);
    }

    static uint ReadUInt32(BinaryReader br, bool be)
    {
        var b = br.ReadBytes(4);
        if (be) Array.Reverse(b);
        return BitConverter.ToUInt32(b, 0);
    }

    static ulong ReadUInt64(BinaryReader br, bool be)
    {
        var b = br.ReadBytes(8);
        if (be) Array.Reverse(b);
        return BitConverter.ToUInt64(b, 0);
    }
}