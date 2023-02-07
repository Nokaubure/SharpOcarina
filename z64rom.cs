using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using Tommy;



namespace SharpOcarina
{

public class rom64 {
    static string pathRomCfg = "";
    static string pathRomDir = "";

    static public bool isSet() {
        return pathRomCfg != "";
    }
    
    static public bool set(string file) {
        if (file != "" && File.Exists(file)) {
            pathRomCfg = file;
            pathRomDir = Path.GetDirectoryName(file);
            return true;
        }

        pathRomCfg = "";
        pathRomDir = "";
        
        return false;
    }
    
    static public string getRomCfg() {
        return pathRomCfg;
    }

    static public string getPath() {
        return pathRomDir;
    }

    static public List<String> getList(string path) {
        List<String> fileList = new List<String>();
        string fullpath = pathRomDir + "\\" + path;

        Console.WriteLine("Walk: " + fullpath);
        foreach (string f in Directory.GetDirectories(fullpath)) {
            fileList.Add(f);
        }

        return fileList;
    }

    static public String getItem(string path, int index) {
        List<String> list = getList(path);
        string index_str = "0x" + index.ToString("X4") + "-";

        foreach (var f in list) {
            if (f.Contains(index_str))
                return f + "\\object.zobj";
        }

        return "";
    }

    static public String openFile(string path) {
        string file = getPath() + "\\" + path;

        if (File.Exists(file))
            return File.ReadAllText(file);
        return "";
    }

    static public uint getActorObjID(string path) {
        if (File.Exists(path + "\\overlay.zovl")) {
            FileStream zovl = File.Open(path + "\\overlay.zovl", FileMode.Open, FileAccess.Read);
            TomlTable toml = parseToml(path + "\\config.toml");

            uint vramaddr = (uint)toml["vram_addr"].AsInteger;
            uint initvar = (uint)toml["init_vars"].AsInteger;
            int offset = (int)(initvar - vramaddr);
            byte[] data = new byte[2];

            zovl.Seek(offset + 8, SeekOrigin.Begin);
            zovl.Read(data, 0, 2);
            uint r = (uint)(data[0] << 8 | data[1]);
            zovl.Close();

            return r;
        }

        return 1;
    }

    static public bool getNameAndIndex(string input, ref string name, ref ushort index) {
        var basename = Path.GetFileNameWithoutExtension(input + ".exe");

        if (!basename.StartsWith("0x"))
            return false;
        

        var indexname = basename.Substring(2, basename.IndexOf("-") - 2);

        if (!ushort.TryParse(indexname, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out index))
            return false;
        
        basename = basename.Substring(basename.IndexOf("-") + 1);

        name = basename;

        return true;
    }

    static public TomlTable parseToml(string file) {
        if (File.Exists(file)) {
            StreamReader actor_toml = File.OpenText(file);
            TomlTable t = TOML.Parse(actor_toml);
            actor_toml.Close();

            return t;
        }

        return null;
    }
}
}
