using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShotBall
{
    public class Distribution
    {
        public List<double> realMath { get; set; }
        public List<double> teorMath { get; set; }
        public List<double> realDisp { get; set; }
        public List<double> teorDisp { get; set; }
        public List<List<double>> range { get; set; }
        public double realProbability {get; set;}
        public double teorProbability { get; set; }
        public List<AnyPoint> points { get; set; }
        public int vectorSize { get; set; }
    }
}
