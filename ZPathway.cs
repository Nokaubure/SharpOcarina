using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OpenTK;

namespace SharpOcarina
{
    public class ZPathway
    {
        [XmlIgnore]
        private List<Vector3> _Points;

        public List<Vector3> Points
        {
            get { return _Points; }
            set { _Points = value; }
        }
        public ZPathway() { }

        public ZPathway(List<Vector3> Points)
        {
            _Points = Points;
        }

        public ZPathway Clone()
        {
            ZPathway clone = new ZPathway();
            clone.Points = Points.ConvertAll(x => (new Vector3(x.X,x.Y,x.Z)));
            return clone;
        }
    }
}
