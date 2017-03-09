using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShotBall
{
    public class ClassItem
    {
        public double probability { get; set; }
        public string name { get; set; }
        public Color color { get; set; }
        public List<double> matMath { get; set; }
        public List<double> matDisp { get; set; }
        public List<List<double>> matCor { get; set; }
        public List<AnyPoint> sample { get; set; }
        public int size { get; set; }

        public ClassItem(string name)
        {
            this.name = "Class " + name;
            color = GetRandomBrush();
        }

        public ClassItem()
        {
            color = GetRandomBrush();
        }

        public void Init(List<double> math, List<double> disp, List<List<double>> cor, double prob, List<AnyPoint> sampl)
        {
            matMath = DeepCopy(math);
            matDisp = DeepCopy(disp);
            matCor = DeepCopy(cor);
            probability = DeepCopy(prob);
            sample = CloneList(sampl);
        }

        private List<T> CloneList<T>(IEnumerable<T> oldList)
        {
            return new List<T>(oldList);
        }

        private Color GetRandomBrush()
        {
            Random rand = new Random();
            return Color.FromArgb(MultidimNormDist.r.Next(255), MultidimNormDist.r.Next(255), MultidimNormDist.r.Next(255));
        }

        public override string ToString()
        {
            return name;
        }

        public static T DeepCopy<T>(T other)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, other);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
