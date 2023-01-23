using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpOcarina
{
public class rom64 {
    static string pathRomCfg = "";
    static string pathRomDir = "";

    public static bool isSet() {
        return pathRomCfg != "";
    }
    public static bool set(string file) {
        if (file != "" && File.Exists(file)) {
            pathRomCfg = file;
            pathRomDir = Path.GetDirectoryName(file);
            return true;
        }

        pathRomCfg = "";
        pathRomDir = "";
        
        return false;
    }
    public static string getRomCfg() {
        return pathRomCfg;
    }

    public static string getPath() {
        return pathRomDir;
    }

    public static List<String> getList(string path) {
        List<String> fileList = new List<String>();
        string fullpath = pathRomDir + "\\" + path;

        Console.WriteLine("Walk: " + fullpath);
        foreach (string f in Directory.GetDirectories(fullpath)) {
            fileList.Add(f);
        }

        return fileList;
    }
}
}
