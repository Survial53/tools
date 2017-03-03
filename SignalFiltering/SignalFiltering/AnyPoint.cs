using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalFiltering
{
    public class AnyPoint
    {
        public List<double> points { get; set; }

        public AnyPoint(List<double> pts)
        {
            points = pts;
        }

        public void Replace(List<double> pt)
        {
            this.points = pt;
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < points.Count; i++)
            {
                res += Math.Round(points[i], 4) + "      ";
            }
            return res;
        }

        public void Add(List<double> pts)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] += pts[i];
            }
        }
    }
}
