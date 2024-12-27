using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Xml;
using System.Drawing;

namespace SharpOcarina
{
    static class IO
    {
        public static void Export<T>(T Object, string Filename)
        {
            XmlWriterSettings XWS = new XmlWriterSettings();
            XWS.NamespaceHandling = NamespaceHandling.OmitDuplicates;
            XWS.NewLineChars = Environment.NewLine;
            XWS.Indent = true;

            //XmlSerializer XS = new XmlSerializer(Object.GetType());
            var XS = new ConfigurationContainer().ConfigureType<ZScene>()
                                             .EnableReferences(p => p.cloneid).Create();
            StreamWriter SW = new StreamWriter(Filename);
            XmlWriter XW = XmlWriter.Create(SW, XWS);

            

            XW.WriteStartDocument();
            XW.WriteComment("Created with " + Program.ApplicationTitle);
            XS.Serialize(XW, Object);
            XW.WriteEndDocument();
            XW.Flush();

            SW.Close();
        }

        public static T Import<T>(string Filename)
        {
            try
            {
                var XS = new ConfigurationContainer().ConfigureType<ZScene>()
                                 .EnableReferences(p => p.cloneid).Create();
                XmlReader XR = XmlReader.Create(Filename);
                return (T)XS.Deserialize(XR);
            }
            catch (Exception e)
            {
                DebugConsole.WriteLine("Old XML detected, using old format to import it");
                XmlSerializer XS = new XmlSerializer(typeof(T));
                StreamReader SR = new StreamReader(Filename);

                return (T)XS.Deserialize(SR);
            }


        }
        /*
        public static void BinExport<T>(T Object, string Filename)
        {
            Stream stream = File.Open(Filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, Object);
            stream.Close();
        }*/
    }
}
