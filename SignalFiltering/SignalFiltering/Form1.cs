using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalFiltering
{
    enum Filters
    {
        WIND,
        MED,
        EXP,
    }
}

namespace SignalFiltering
{
    public partial class Form1 : Form
    {
        private string path = @"./noize.txt";
        private Filters switcher;
        private double[] X;
        private double[] Y;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.IsMarksNextToAxis = false;
            chart1.ChartAreas[0].AxisY.IsMarksNextToAxis = false;
            chart1.ChartAreas[0].AxisX.Crossing = 0;
            chart1.ChartAreas[0].AxisY.Crossing = 0;
            chart1.ChartAreas[0].AxisX.IsLabelAutoFit = true;
            chart1.Legends[0].Enabled = false;
            textBox1.Text = "10";
            f1.Checked = true;
        }

        public List<double> readDAta(string s)
        {
            string[] data;
            List<double> y = new List<double>();
            data = File.ReadAllLines(s);
            int i = 0;
            while (i < data.Length)
            {
                y.Add((Convert.ToDouble(data[i].Replace('.', ','))) / 10);
                i++;
            }
            Y = y.ToArray();
            return y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<AnyPoint> xy = new List<AnyPoint>();
            InitSamples(ref xy);

            chart1.Series[0].Points.Clear();
          /*  for (int i = 0; i < xy.Count; i++)
            {
                DrawPoint(xy[i].points, 0);
            }*/

            chartBuild(xy, 0);

            
            
            int window = Convert.ToInt32(textBox1.Text);

            double[] F = new double[Y.Length];
            //SmoothWindow(ref Y, ref F, Y.Length, window);
            SmoothMedian(ref Y, ref F, Y.Length, window);
            chartBuild(X, F, 1);
        }

        public void SmoothWindow(ref double[] input, ref double[] output, int n, int window)
        {
            int i, j, z, k1, k2, hw;
            double tmp;
            if (window % 2 == 0) window++;
            hw = (window - 1) / 2;
            output[0] = input[0];

            for (i = 1; i < n; i++)
            {
                tmp = 0;
                if (i < hw)
                {
                    k1 = 0;
                    k2 = 2 * i;
                    z = k2 + 1;
                }
                else if ((i + hw) > (n - 1))
                {
                    k1 = i - n + i + 1;
                    k2 = n - 1;
                    z = k2 - k1 + 1;
                }
                else
                {
                    k1 = i - hw;
                    k2 = i + hw;
                    z = window;
                }

                for (j = k1; j <= k2; j++)
                {
                    tmp = tmp + input[j];
                }
                output[i] = tmp / z;
            }
        }

        public void SmoothMedian(ref double[] input, ref double[] output, int n, int window)
        {
            int i, j, z, k1, k2, hw;

            List<double> span;

            if (window % 2 == 0) window++;
            hw = (window - 1) / 2;
            output[0] = input[0];

            for (i = 1; i < n; i++)
            {
                //tmp = 0;
                if (i < hw)
                {
                    k1 = 0;
                    k2 = 2 * i;
                    z = k2 + 1;
                }
                else if ((i + hw) > (n - 1))
                {
                    k1 = i - n + i + 1;
                    k2 = n - 1;
                    z = k2 - k1 + 1;
                }
                else
                {
                    k1 = i - hw;
                    k2 = i + hw;
                    z = window;
                }

                span = new List<double>(z);
                for (j = k1; j <= k2; j++)
                {
                    //tmp = tmp + input[j];
                    span.Add(input[j]);
                }
                span.Sort();
                int index = span.Count / 2 + 1;
                if (span.Count == index)
                    index = 0;
                output[i] = span[index];
            }
        }

        public void chartBuild(double[] x, double[] y, int series)
        {
            chart1.Series[series].Points.Clear();

            for (int j = 0; j < x.Count(); j++)
            {
                chart1.Series[series].Points.AddXY(x[j], y[j]);
            }
        }

        public void chartBuild(List<AnyPoint> pts, int series)
        {
            chart1.Series[series].Points.Clear();

            for (int j = 0; j < pts.Count(); j++)
            {
                chart1.Series[series].Points.AddXY(pts[j].points[0], pts[j].points[1]);
            }
        }

        private void RadiobuttonClick(object sender, EventArgs e)
        {
            RadioButton selected = (RadioButton)sender;
            if (selected.Checked == true)
            {
                switch (selected.Name)
                {
                    case "f1":
                        {
                            chart1.Series[0].Points.Clear();
                            chart1.Series[1].Points.Clear();
                            switcher = Filters.WIND;
                            break;
                        }
                    case "f2":
                        {
                            chart1.Series[0].Points.Clear();
                            chart1.Series[1].Points.Clear();
                            switcher = Filters.MED;
                            break;
                        }
                    case "f3":
                        {
                            chart1.Series[0].Points.Clear();
                            chart1.Series[1].Points.Clear();
                            switcher = Filters.EXP;
                            break;
                        }
                }
            }
        }

        private bool InitSamples(ref List<AnyPoint> pts)
        {
            int i;
            List<double> x = new List<double>(); 
            List<double> y = readDAta(path);
            double step = 0;
            for (i = 0; i < y.Count; i++)
            {
                x.Add(i + step);
                step += 0.5;
            }

            X = x.ToArray();

            List<double> xy;
            for (i = 0; i < y.Count; i++)
            {
                xy = new List<double>();
                xy.Add(x[i]);
                xy.Add(y[i]);
                pts.Add(new AnyPoint(xy));
            }
            
            return true;
        }

        public bool DrawPoint(List<double> xy, int ser)
        {
            if (xy.Count == 2)
            {
                chart1.Series[ser].Points.AddXY(xy[0], xy[1]);
                return true;
            }
            return false;
        }


    }
}
