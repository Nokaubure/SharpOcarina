using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace SharpOcarina
{
    public static class XMLreader
    {
        //   XmlTextReader reader = new XmlTextReader ("ActorNames.xml");

        public static string getActorName(string id)
        {
            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + gameprefix + "ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Actor");
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    if (nodeAtt["Key"].Value == id)
                    {
                        return nodeAtt["Name"].Value;
                    }
                }
            return "Unknown";
        }

        public static int[] getActorObject(string id)
        {
            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + gameprefix + "ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Actor");
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    if (nodeAtt["Key"].Value == id)
                    {
                        if (nodeAtt["Object"] != null)
                        {
     
                            String[] strgroups = nodeAtt["Object"].Value.Split(',');
                            int[] output = new int[strgroups.Length];
                            int incr = 0;
                            foreach (String s in strgroups)
                            {
                                    output[incr] = Convert.ToInt32(s, 16);
                                    incr++;

                            }
                            return output;
                        }
                        else return new int[] {};
                    }

                }
            return new int[] {};
        }

        public static string getActorNamesByObject(string id)
        {
            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + gameprefix + "ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Actor");
            String output = "";
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    if (nodeAtt["Object"] != null && nodeAtt["Object"].Value.Contains(id))
                    {
                        output += nodeAtt["Name"].Value + ", ";
                    }
                }
            output = output.TrimEnd(' ');
            output = output.TrimEnd(',');
            return output;
        }

        public static string[] getObjectSize(string id)
        {
            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + gameprefix + "ObjectData.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Object");
            String[] output = {"0",""};
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    if (nodeAtt["Key"] != null && nodeAtt["Key"].Value.Contains(id))
                    {
                        output[0] = nodeAtt["Size"].Value;
                        output[1] = node.InnerText;
                    }
                }
            return output;
        }

        public static List<ActorProperty> getActorProperties(string id)
        {
            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + gameprefix + "ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Actor");
            List<ActorProperty> output = new List<ActorProperty>();
       
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    if (nodeAtt["Key"] != null && nodeAtt["Key"].Value.Contains(id))
                    {


                        List<SongItem> maindropdownitems = new List<SongItem>();

                        SongItem tempunk = new SongItem();
                        tempunk.Text = "Unknown";
                        tempunk.Value = 0;


                        maindropdownitems.Add(tempunk);

                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (node2.Name == "Variable")
                            {
                                XmlAttributeCollection nodeAtt2 = node2.Attributes;

                                SongItem dropdownitem = new SongItem();
                                dropdownitem.Text = "Unknown";
                                dropdownitem.Value = 0;

                                try
                                {
                                    ushort var = (ushort)Convert.ToInt16(nodeAtt2["Var"].Value, 16);


                                    
                                    dropdownitem.Text = node2.InnerText;
                                    dropdownitem.Value = var;

                                    maindropdownitems.Add(dropdownitem);
                                }
                                catch (System.FormatException e)
                                {
                                    DebugConsole.WriteLine("Error " + nodeAtt["Name"].Value);
                                }
                            }
                        }

                        if (maindropdownitems.Count > 1)
                        {
                            ActorProperty property = new ActorProperty();
                            property.Name = "Main variable";
                            property.Mask = 0x0000;
                            property.DropdItems = maindropdownitems;
                            property.Target = "Var";
                            output.Add(property);
                        }

                        ushort cumulativemask = 0;

                        if (nodeAtt["Properties"] != null)
                        {

                            string[] names = nodeAtt["PropertiesNames"].Value.Split(',');
                            string[] values = nodeAtt["Properties"].Value.Split(',');
                            string[] targets = null;
                            if (nodeAtt["PropertiesTarget"] != null) targets = nodeAtt["PropertiesTarget"].Value.Split(',');

                            string[] dropdownnames = (nodeAtt["DropdownNames"] == null) ? null : nodeAtt["DropdownNames"].Value.Split(',');
                            string[] dropdownvalues = (nodeAtt["Dropdown"] == null) ? null : nodeAtt["Dropdown"].Value.Split(',');

                        

                        for (int i = 0; i < names.Length; i++)
                        {
                            ActorProperty property = new ActorProperty();
                            property.Name = names[i];
                            property.Mask = Convert.ToUInt16(values[i],16);
                            property.DropdItems = new List<SongItem>();
                            
                            if (targets != null) property.Target = targets[i];
                            else property.Target = "Var";

                            if (property.Target == "Var") cumulativemask += property.Mask;

                            property.Max = property.Mask;
                            while ((property.Max & 1) == 0)
                            {
                                property.Max = (ushort) (property.Max >> 1);
                                property.Position += 1;
                            }

                            if(dropdownnames != null)
                            {
                                string[] tempdropdownnames = dropdownnames[i].Split(';');
                                string[] tempdropdownvalues = dropdownvalues[i].Split(';');

                                if (tempdropdownnames != null && tempdropdownnames[0] != "")
                                {

                                    SongItem dropdownitem = new SongItem();
                                    dropdownitem.Text = "Unknown";
                                    dropdownitem.Value = 0;


                                    property.DropdItems.Add(dropdownitem);

                                    for (int y = 0; y < tempdropdownnames.Length; y++)
                                    {
                                        dropdownitem = new SongItem();
                                        dropdownitem.Text = Convert.ToUInt16(tempdropdownvalues[y], 16).ToString("X4") + " - " + tempdropdownnames[y];
                                        dropdownitem.Value = Convert.ToUInt16(tempdropdownvalues[y], 16);


                                        property.DropdItems.Add(dropdownitem);

                                    }
                                }
                            }

                            output.Add(property);

                        }

                        }

                        if (maindropdownitems.Count > 1)
                        {
                            output[0].Mask = (ushort) (cumulativemask ^ 0xFFFF);
                            output[0].Max = output[0].Mask;
                            while ((output[0].Max & 1) == 0 && cumulativemask != 65535)
                            {
                                output[0].Max = (ushort)(output[0].Max >> 1);
                                output[0].Position += 1;
                            }

                            bool first = true;

                            foreach (SongItem item in output[0].DropdItems)
                            {
                                if (!first)
                                {
                                    item.Value = Convert.ToUInt16(item.Value) >> output[0].Position;
                                    item.Text = Convert.ToUInt16(item.Value).ToString("X4") + " - " + item.Text;
                                }
                                first = false;
                            }

                           
                        }




                        return output;


                    }
                }
            return output;
        }


        public static List<SongItem> getActorDropdown(string id)
        {
            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + gameprefix + "ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Actor");
            List<SongItem> output = new List<SongItem>();

            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    if (nodeAtt["Key"] != null && nodeAtt["Key"].Value.Contains(id))
                    {
                        if (nodeAtt["Dropdown"] == null) return output;

                        string[] names = nodeAtt["DropdownNames"].Value.Split(',');
                        string[] values = nodeAtt["Dropdown"].Value.Split(',');

                        for (int i = 0; i < names.Length; i++)
                        {
                            SongItem dropdownitem = new SongItem();
                            dropdownitem.Text = names[i];
                            dropdownitem.Value = Convert.ToUInt16(values[i], 16);


                            output.Add(dropdownitem);

                        }




                        return output;


                    }
                }
            return output;
        }


        public static SongItem[] getXMLItems(String XMLFile, String XMLTag)
        {
            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/"+XMLFile+".xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/" + XMLTag);
            int incr = 0;
            SongItem[] output = new SongItem[nodes.Count];
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    output[incr] = new SongItem();
                    output[incr].Text = nodeAtt["Key"].Value + " - " + node.InnerText;
                    output[incr].Value = Convert.ToInt64(nodeAtt["Key"].Value, 16);
                    incr++;
                }
            return output;
        }

        public static XmlNodeList getXMLNodes(String XMLFile, String XMLTag)
        {
            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + XMLFile + ".xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/" + XMLTag);
            return nodes;
        }

        public static XMLactor getFullActor(string id)
        {
            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlDocument doc = new XmlDocument();
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"XML/" + gameprefix + "ActorNames.xml");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            XmlNodeList nodes = doc.SelectNodes("Table/Actor");
            XMLactor output = new XMLactor();
            output.notes = "";
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    XmlAttributeCollection nodeAtt = node.Attributes;
                    if (nodeAtt["Key"] != null && nodeAtt["Key"].Value.Contains(id))
                    {
                        if (nodeAtt["Name"] != null) output.name = nodeAtt["Name"].Value;
                        if (nodeAtt["Category"] != null) output.category = Convert.ToByte(nodeAtt["Category"].Value);
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (node2.Name == "Variable")
                            { 
                            XmlAttributeCollection nodeAtt2 = node2.Attributes;
                                try
                                {
                                    output.variables.Add((ushort)Convert.ToInt16(nodeAtt2["Var"].Value, 16), node2.InnerText);
                                }
                                catch (System.FormatException e)
                                {
                                    DebugConsole.WriteLine("Error " + nodeAtt["Name"].Value);
                                }
                            }
                            else if (node2.Name == "Notes")
                            {
                                output.notes = node2.InnerText;
                            }
                        }
                    }
                }
            return output;
        }
    


    }

        public class XMLactor
        {
            public string name;
            public Dictionary<ushort, string> variables;
            public string notes;
            public byte category;

            public XMLactor()
            {
                name = "";
                variables = new Dictionary<ushort, string>();
                notes = "";
               category = 0;
            }

            public XMLactor(string _name, Dictionary<ushort, string> _variables, string _notes, byte _category)
            {
            name = _name;
            variables = _variables;
            notes = _notes;
            category = _category;
            }
        }


}
