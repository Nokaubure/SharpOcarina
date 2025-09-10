using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenTK.Input;

namespace SharpOcarina
{
    public partial class OpenRomScene : Form
    {
        public List<Byte> Data;

        public ushort sceneid;

        Dictionary<byte, string> SceneNames;

        public OpenRomScene(List<byte> _Data)
        {
            InitializeComponent();

            Data = _Data;

            string gameprefix = (!MainForm.settings.MajorasMask) ? "OOT/" : "MM/";

            XmlNodeList nodes = XMLreader.getXMLNodes("SceneNames", "Scene");
            SceneNames = new Dictionary<byte, string>();
            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection nodeAtt = node.Attributes;
                if (nodeAtt["Key"] != null)
                {
                    SceneNames.Add(Convert.ToByte(nodeAtt["Key"].Value, 16), node.InnerText);
                }
            }


            Init();
           // UpdateWindow();
        }

        public void Init()
        {
            ROM rom = MainForm.CheckVersion(Data);

            string scenename = "";

            int offset = (int)rom.SceneTable;

            int c = 0;

            string notes = "";

            while (offset < ((int)rom.SceneTableEnd) - 1)
            {
               // variable = Helpers.Read16(EntranceTable, offset + 2);

                

                uint romstart = Helpers.Read32(Data, offset);
                uint romend = Helpers.Read32(Data, offset + 4);
                uint titlecardstart  = Helpers.Read32(Data, offset + 8);
                uint titlecardend = Helpers.Read32(Data, offset + 12);

                if (romstart == 0 || romend == 0) scenename = "EMPTY";
                else if (!SceneNames.TryGetValue((byte)c, out scenename)) scenename = "Unknown";

                notes = "Offset: " + romstart.ToString("X8") + " - " + romend.ToString("X8") + Environment.NewLine +
                         "Title card : " + ((titlecardstart == 0) ? "Empty" : titlecardstart.ToString("X8") + " - " + titlecardend.ToString("X8")) + Environment.NewLine;
                //Environment.NewLine

            //    notes = notes.Replace(Environment.NewLine, "\\par ");
              //  notes = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red0\green0\blue255;} \cf2" + @"\line" + @"\cf1 " + notes + "}";

                SceneView.Nodes.Add(new SceneNode((ushort)c, (romstart != 0 && romend != 0), c.ToString("X2") + " - " + scenename, notes));

                if (rom.Game == "OOT")
                    offset += 0x14;
                else
                    offset += 0x10;

                c++;
            }

        }

        

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenSceneButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public class SceneNode : TreeNode
        {
            public ushort Value { get; set; }
            public bool Valid { get; set; }
            public string Notes { get; set; }
            public SceneNode(ushort _Value, bool _Valid, string text, string _Notes) : base(text)
            {
                Value = _Value;
                Valid = _Valid;
                Notes = _Notes;
            }
        }

        private void SceneView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OpenSceneButton.Enabled = ((SceneNode)e.Node).Valid;
            NotesTextBox.Clear();
            NotesTextBox.Text = ((SceneNode)e.Node).Notes;
            sceneid = ((SceneNode)e.Node).Value;
        }
    }

}
