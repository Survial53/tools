using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinus
{
    public partial class Form1 : Form
    {
        private double[] X;
        private double[] Y;
        private double[] P = { 0, 1, 1, 0 };
        private double[] P_prev = { 0, 0, 0, 0 };
        private string path = @"./points.txt";
        private double eps = 0.0001;
        public delegate double funcD(double x, double y, double[] p);
                
        public void readDAta(string s)
        {
            string[] data;
            List<double> xv = new List<double>();
            List<double> yv = new List<double>();
            data = File.ReadAllLines(s);

            int src = 1;

            int i = 0;
            while (i < data.Length)
            {
                if (data[i].CompareTo("") == 0)
                {
                    src = 2;
                    i++;
                }
                
                switch (src)
                { 
                    case 1:
                        xv.Add((Convert.ToDouble(data[i].Replace('.', ','))) / 10);
                        break;
                    case 2:
                        yv.Add((Convert.ToDouble(data[i].Replace('.', ','))) / 10);
                        break;
                }
                i++;
            }
            
            Y = yv.ToArray();

            /*double t = 0;
            for (i = 0; i < Y.Length; i++)
            {
                xv.Add(t);
                t += 0.1;
            }*/
            X = xv.ToArray();


        }

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "0,01";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            textBox3.Text = (10).ToString();
            textBox2.Text = (-10).ToString();
        }

        public double func(double x, double[] p)
        {
            return p[0] + p[1] * Math.Sin(p[2] * x + p[3]);
        }

        /* dS/dA */
        public double func_da(double x, double y, double[] p)
        {
            return 2 * (p[0] + p[1] * Math.Sin(p[2] * x + p[3]) - y);
        }

        /* dS/dB */
        public double func_db(double x, double y, double[] p)
        {
            return 2 * Math.Sin(p[2] * x + p[3]) * (p[0] + p[1] * Math.Sin(p[2] * x + p[3]) - y);
        }

        /* dS/dC */
        public double func_dc(double x, double y, double[] p)
        {
            return 2 * p[1] * x * Math.Cos(p[2] * x + p[3]) * (p[0] + p[1] * Math.Sin(p[2] * x + p[3]) - y);
        }

        /* dS/dD */
        public double func_dd(double x, double y, double[] p)
        {
            return 2 * p[1] * Math.Cos(p[2] * x + p[3]) * (p[0] + p[1] * Math.Sin(p[2] * x + p[3]) - y);
        }

        /* S */
        public double func_S(double x, double y, double[] p)
        {
            //return y * y - 2 * y * p[0] - 2 * y * p[1] * Math.Sin(p[2] * x + p[3]) + p[0] * p[0] + 2 * p[0] * p[1] * Math.Sin(p[2] * x + p[3]) + p[1] * p[1] * Math.Pow(Math.Sin(p[2] * x + p[3]), 2) ;
            return Math.Pow(y - p[0] - p[1] * Math.Sin(p[2] * x + p[3]), 2);
        }

        public double Sum(double[] p, funcD f)
        {
            double sum = 0;
            for (int i = 0; i < X.Length; i++)
            {
                sum += f(X[i], Y[i], p);
            }
            return sum;
        }

        public double[] CalculateGrad()
        {
            double[] t = new double[4];
         
            t[0] = Sum(P, func_da);
            t[1] = Sum(P, func_db);
            t[2] = Sum(P, func_dc);
            t[3] = Sum(P, func_dd);
            return t;
        }

        public double CalculateLambda(double[] grad)
        {
            double lamb = double.Epsilon, lamb_prev = double.Epsilon;
            double dsum = 0, ddsum = 0;
            double[] pr = new double[4];
            double[] dpr = new double[4];
            double sin, cos;

            do
            {
                dsum = 0;
                ddsum = 0;
                lamb_prev = lamb;

                for (int i = 0; i < pr.Length; i++)
                {
                    pr[i] = P[i] - lamb * grad[i];
                    dpr[i] = -grad[i];
                }

                for (int i = 0; i < X.Length; i++)
                {
                    sin = Math.Sin(pr[2] * X[i] + pr[3]);
                    cos = Math.Cos(pr[2] * X[i] + pr[3]);
                    dsum += 2 * (-Y[i] + pr[0] + pr[1] * sin) * (dpr[0] + dpr[1] * sin + pr[1] * cos * (dpr[2] * X[i] + dpr[3]));
                }

                for (int i = 0; i < X.Length; i++)
                {
                    sin = Math.Sin(pr[2] * X[i] + pr[3]);
                    cos = Math.Cos(pr[2] * X[i] + pr[3]);
                    ddsum += 2 * (Math.Pow(dpr[0] + dpr[1] * sin + pr[1] * cos * (dpr[2] * X[i] + dpr[3]), 2) + (-Y[i] + pr[0] + pr[1] * sin) * (2 * dpr[1] * (dpr[2] * X[i] + dpr[3]) * cos - Math.Pow(dpr[2] * X[i] + dpr[3], 2) * pr[1] * sin));
                }

                lamb = lamb_prev - dsum / ddsum;
            }
            while (lamb - lamb_prev > 0.001);
            return lamb;
        }

        public void Calculate()
        {
            button1.Enabled = false;
            double[] gradient;
            double lambda = 0;
            double sum1, sum2;
            double comp = Convert.ToDouble(textBox1.Text);

            int mxi = Array.IndexOf(Y, Y.Max());
            int mni = Array.IndexOf(Y, Y.Min());

            P[0] = (Y.Max() + Y.Min()) / 2.0;
            P[1] = (Y.Max() - Y.Min()) / 2.0;
            P[2] = (Math.Asin((Y.Max() - P[0]) / P[1]) - (Math.Asin((Y.Min() - P[0]) / P[1]))) / (X[mxi] - X[mni]);
            P[3] = Math.Asin((Y.Min() - P[0]) / P[1]) - P[2] * X[mni];

            do
            {
                P.CopyTo(P_prev, 0);
                gradient = CalculateGrad();
                lambda = CalculateLambda(gradient);
                for (int i = 0; i < P.Length; i++)
                {
                    P[i] -= lambda * gradient[i];
                }
                sum1 = Sum(P_prev, func_S);
                sum2 = Sum(P, func_S);
            } while (sum1 - sum2 > comp);
            button1.Enabled = true;
        }
        
        public void chartBuild(double[] x, double[] y, int series)
        {
            chart1.Series[series].Points.Clear();
            chart1.Series[series].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            
            for (int j = 0; j < x.Count(); j++)
            {
                chart1.Series[series].Points.AddXY(x[j], y[j]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double mx = X.Max();
            double mi = X.Min();
            chartBuild(X, Y, 0);
            Calculate();
            chart1.Series[1].Points.Clear();
            for (double v = mi, step = (mx - mi) / 10000; v < mx; v += step)
            {
                chart1.Series[1].Points.AddXY(v, func(v, P));
            }
            
        }

        public void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        private void generatePoints()
        { 
            double max = 0, min = 0;
            max = Convert.ToDouble(textBox3.Text);
            min = Convert.ToDouble(textBox2.Text);
            if (max < min)
            {
                Swap(ref max, ref min);
            }

           /* for (double v = min, step = (max - min) / 100; v < max; v += step)
            {
                
            }*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            readDAta(path);
            dataGridView1.Rows.Clear();

            for (int i = 0; i < X.Length; i++)
            {
                dataGridView1.Rows.Add(X[i], Y[i]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            generatePoints();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (X != null && Y != null)
            {
                int col = e.ColumnIndex;
                switch (col)
                {
                    case 0:
                        {
                            X[e.RowIndex] = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[col].Value);
                            break;
                        }
                    case 1:
                        {
                            Y[e.RowIndex] = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[col].Value);
                            break;
                        }
                    default:
                        break;
                }
            }
           

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                panel1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                panel1.Enabled = true;
                button2.Enabled = false; 
            }

            
        }
    }
}
