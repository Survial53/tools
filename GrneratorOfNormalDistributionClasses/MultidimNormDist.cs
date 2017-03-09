using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShotBall
{
    public class MultidimNormDist
    {
        public List<List<double>> matCor { get; set; }
        public List<List<double>> matCov { get; set; }
        public List<List<double>> matA { get; set; }
        public List<double> matMath { get; set; }
        public List<double> matDisp { get; set; }
        public int size { get; set; }
        static public Random r = new Random();

        public MultidimNormDist()
        {
        }

        public bool Init(List<double> mean, List<double> disp, List<List<double>> cor, int size)
        {
            matMath = DeepClone(mean);
            matDisp = DeepClone(disp);
            matCor = DeepClone(cor);
            this.size = size;

            int i, j, k;
            double sumA, sumAA;

            List<List<double>> matCov = new List<List<double>>(size);
            for (i = 0; i < size; i++)
            {
                matCov.Add(new List<double>(size));
            }

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    matCov[i].Add(matCor[i][j] * Math.Sqrt(matDisp[i] * matDisp[j]));
                }
            }

            if (DeterminantMatrix(ref matCov) <= 0)
            {
                return false;
            }

            matA = new List<List<double>>(size);           
            for (i = 0; i < size; i++)
            {
                matA.Add(new List<double>(size));
                for (j = 0; j < size; j++)
                {
                    matA[i].Add(0);
                }
            }            

            for (i = 0; i < size; i++)
            {
                for (j = 0; j <= i; j++)
                {
                    sumA = 0;
                    sumAA = 0;
                    for (k = 0; k < j; k++)
                    {
                        sumA += matA[i][k] * matA[j][k];
                        sumAA += matA[j][k] * matA[j][k];
                    }
                    matA[i][j] = (matCov[i][j] - sumA) / Math.Sqrt(matCov[j][j] - sumAA);
                }
            }

            return true;
        }

        public List<double> GeneratePoint()
        {
            int i, k;
            double alfa1, alfa2;
            List<double> matN = new List<double>(size);
            List<double> matRes = new List<double>(size);
            for (i = 0; i < size; i++)
            {
                matN.Add(0);
                matRes.Add(0);
            }

            for (i = 0; i < size; i += 2)
            {
                alfa1 = r.NextDouble();
                alfa2 = r.NextDouble();
                if (alfa1 == 0 || alfa2 == 0)
                {
                    i -= 2;
                }
                else
                {
                    matN[i] = Math.Sqrt(-2 * Math.Log(alfa1)) * Math.Sin(2 * Math.PI * alfa2);
                    if (i + 1 < size) matN[i + 1] = Math.Sqrt(-2 * Math.Log(alfa1)) * Math.Cos(2 * Math.PI * alfa2);
                }
            }
            
            for (i = 0; i < size; i++)
            {
                matRes[i] = matMath[i];
                for (k = 0; k < size; k++)
                {
                    matRes[i] += matA[i][k] * matN[k];
                }
            }
            return matRes;
        }
        
        private double DeterminantMatrix(ref List<List<double>> m)
        {
            double result = 0;
            if (m.Count == 1)
            {
                return m[0][0];
            }
            else if (m.Count == 2)
            {
                return m[0][0] * m[1][1] - m[0][1] * m[1][0];
            }
            else if (m.Count == 3)
            {
                return m[0][0] * m[1][1] * m[2][2] + m[0][1] * m[1][2] * m[2][0] + m[0][2] * m[1][0] * m[2][1] - m[2][0] * m[1][1] * m[0][2] - m[1][0] * m[0][1] * m[2][2] - m[0][0] * m[2][1] * m[1][2];
            }
            else
            {
                List<List<double>> m1 = new List<List<double>>(m.Count - 1);
                for (int i = 0; i < m.Count - 1; i++)
                {
                    m1.Add(new List<double>(m.Count - 1));

                    for (int j = 0; j < m.Count - 1; j++)
                    {
                        m1[i].Add(0);
                    }
                }
                for (int i = 0; i < m.Count; i++)
                {
                    for (int j = 1; j < m.Count; j++)
                    {
                        for (int k = 0; k < m.Count; k++)
                        {
                            if (k < i)
                            {
                                m1[j - 1][k] = m[j][k];
                            }
                            else if (k > i)
                            {
                                m1[j - 1][k - 1] = m[j][k];
                            }
                        }
                    }
                    result += Math.Pow(-1, i) * m[0][i] * DeterminantMatrix(ref m1);
                }
            }
            return result;
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
