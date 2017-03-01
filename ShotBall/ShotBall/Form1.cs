using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShotBall
{
    public partial class Form1 : Form
    {
        private MultidimNormDist target;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<List<double>> rez = new List<List<double>>();
            List<List<double>> corMat = new List<List<double>>();
            List<double> matMath = new List<double>();
            List<double> matDisp = new List<double>();

            matMath.Add(0);
            matMath.Add(0);
            matDisp.Add(1);
            matDisp.Add(1);

            for (int i = 0; i < 2; i++)
            {
                rez.Add(new List<double>());
                corMat.Add(new List<double>());
            }

            corMat[0].Add(1);
            corMat[0].Add(0);
            corMat[1].Add(0);
            corMat[1].Add(1);

            rez[0].Add(0);
            rez[0].Add(0);
            rez[1].Add(0);
            rez[1].Add(0);

            string s;
            listBox1.Items.Clear();
            chart1.Series[0].Points.Clear();
            target = new MultidimNormDist();
            List<double> point;
            target.Init(matMath, matDisp, corMat, 2);

            for (int i = 0; i < 3000; i++)
            {
                s = "";
                point = target.GeneratePoint();
                DrawPoint(point);
                for (int j = 0; j < matMath.Count; j++)
                {
                    s += Math.Round(point[j], 4).ToString() + "  ";
                }
                listBox1.Items.Add(s);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        public bool DrawPoint(List<double> xy)
        {
            if(xy.Count == 2)
            { 
                chart1.Series[0].Points.AddXY(xy[0], xy[1]);
                return true;
            }
            return false;
        }


        //Функция моделирования многомерных данных, распределённых по нормальному закону.
        //double matrixMath [mq] - вектор мат. ожидания
        //double matrixDisp [mq] - вектор диперсии
        //vector<vector<double>> - corelMatrix - корреляционная матрица
        //vector<vector<double>> - rezMatrix - массив с результатом
        public bool normalModel(List<double> matrixMath, List<double> matrixDisp, ref List<List<double>> corelMatrix, ref List<List<double>> rezMatrix)
        {
            int mq = rezMatrix[0].Count;//количество переменных
            int count = rezMatrix.Count;//количество значений
            double[,] matrixA = new double[mq, mq]; //треугольная матрица преобразований A
            double[,] matrixN = new double[count, mq]; //матрица случайных чисел, распределенных по нормальному закону с параметрами 0, 1
            int i, j, k;
            double sumA, sumAA;
            double alfa1, alfa2; //углы. Случайные числа, распределенные на интервале (0;1]
            List<List<double>> matrixK = new List<List<double>>(mq); //Ковариационная матрица K
            for (i = 0; i < mq; i++)
            {
                matrixK.Add(new List<double>(mq));
            }
            //Преобразование корреляционной матрицы в ковариационную
            for (i = 0; i < mq; i++)
            {
                for (j = 0; j < mq; j++)
                {
                    matrixK[i].Add(corelMatrix[i][j] * Math.Sqrt(matrixDisp[i] * matrixDisp[j]));
                }
            }

            if (determinantMatrix(ref matrixK) <= 0) return false; // ошибка. Определитель ковариационной матрицы должен быть положительным;
            //Заполнение матрицы A
            for (i = 0; i < mq; i++)
            {
                for (j = 0; j <= i; j++)
                {
                    sumA = 0;
                    sumAA = 0;
                    for (k = 0; k < j; k++)
                    {
                        sumA += matrixA[i, k] * matrixA[j, k];
                        sumAA += matrixA[j, k] * matrixA[j, k];
                    }
                    matrixA[i, j] = (matrixK[i][j] - sumA) / Math.Sqrt(matrixK[j][j] - sumAA);
                }
            }
            //моделирование случайных чисел, распределенных по нормальному закону с параметрами 0, 1
            Random r = new Random();
            for (i = 0; i < count; i += 2)
            {
                for (j = 0; j < mq; j++)
                {
                    alfa1 = r.NextDouble();
                    alfa2 = r.NextDouble();
                    if (alfa1 == 0 || alfa2 == 0)
                    {
                        j--;
                    }
                    else
                    {
                        matrixN[i, j] = Math.Sqrt(-2 * Math.Log(alfa1)) * Math.Sin(2 * Math.PI * alfa2);
                        if (i + 1 < count) matrixN[i + 1, j] = Math.Sqrt(-2 * Math.Log(alfa1)) * Math.Cos(2 * Math.PI * alfa2);
                    }
                }
            }
            //преобразование матрицы случайных чисел, распределенных по нормальному закону с параметрами 0, 1 к матрице с конечными параметрами
            for (i = 0; i < count; i++)
            {
                for (j = 0; j < mq; j++)
                {
                    rezMatrix[i][j] = matrixMath[j];
                    for (k = 0; k < mq; k++)
                    {
                        rezMatrix[i][j] += matrixA[j, k] * matrixN[i, k];
                    }
                }
            }
            return true;
        }

        public double determinantMatrix(ref List<List<double>> m)
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
                    m1[i] = new List<double>(m.Count - 1);
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
                    result += Math.Pow(-1, i) * m[0][i] * determinantMatrix(ref m1);
                }
            }
            return result;
        }
    }
}
