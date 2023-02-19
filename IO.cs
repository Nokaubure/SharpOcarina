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
            XWS.NewLineChars = Environment.NewLine;
            XWS.Indent = true;

            var XS = new ConfigurationContainer().ConfigureType<ZScene>().Create();

            if (Object is ZScene)
            {
                XS = new ConfigurationContainer().ConfigureType<ZScene>()
                                                 .EnableReferences(p => p.cloneid).Create();
            }


            StreamWriter SW = new StreamWriter(Filename);
            XmlWriter XW = XmlWriter.Create(SW, XWS);



            XW.WriteStartDocument();
            XW.WriteComment("Created with " + Program.ApplicationTitle);
            XS.Serialize(XW, Object);
            XW.WriteEndDocument();
            XW.Flush();

            SW.Close();
        }

        public static T Import<T>(string Filename, Type type)
        {
            // This should properly check for the old format instead of relying on an exception, probably...
            if (type == typeof(ZScene))
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
                    Console.WriteLine("Old XML detected, using old format to import it");
                    XmlSerializer XS = new XmlSerializer(typeof(T));
                    StreamReader SR = new StreamReader(Filename);
                    return (T)XS.Deserialize(SR);
                }
            }
            else
            {
                var XS = new ConfigurationContainer().ConfigureType<T>().Create();
                XmlReader XR = XmlReader.Create(Filename);
                return (T)XS.Deserialize(XR);
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
