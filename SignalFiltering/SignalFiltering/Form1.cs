using System;
using System.Threading;
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
        WINDOW,
        MEDIAN,
        EXPONENT,
    }

    enum Data
    {
        WINDOW,
        ALPHA,
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
        private int size;
        
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
            textBox1.Text = "100";
            f1.Checked = true;
            textBox2.Enabled = false;
            textBox2.Text = "0,02";
        }


        private bool InitSamples(ref List<AnyPoint> pts)
        {
            int i;
            List<double> x = new List<double>();
            List<double> y = readDAta(path);
            List<double> xy;
            double step = 0;
            size = y.Count;
            for (i = 0; i < size; i++)
            {
                x.Add(i + step);
                step += 0.5;
                xy = new List<double>();
                xy.Add(x[i]);
                xy.Add(y[i]);
                pts.Add(new AnyPoint(xy));
            }
            X = x.ToArray();
            return true;
        }

        private bool CheckValid(double value, Data d)
        {
            switch (d)
            {
                case Data.WINDOW:
                    {
                        if (value >= size)
                        {
                            statusBar.Items.Add("Невенрное значение Window");
                            return false;
                        }
                        return true;
                    }
                case Data.ALPHA:
                    {
                        if (switcher != Filters.EXPONENT) return true;
                        if (value < 0 || value > 1.0)
                        {
                            statusBar.Items.Add("Неверное Значение Alpha");
                            return false;
                        }
                        return true;
                    }
                default:
                    {
                        return false; 
                    }
            }
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
            statusBar.Items.Clear();
            List<AnyPoint> xy = new List<AnyPoint>();
            InitSamples(ref xy);
            double[] F = new double[Y.Length];
            int window = Convert.ToInt32(textBox1.Text);
            double alpha = Convert.ToDouble(textBox2.Text);
            if (!CheckValid(window, Data.WINDOW) || !CheckValid(alpha, Data.ALPHA))
                return;
            button1.Enabled = false;
            switch (switcher) 
            {
                case Filters.WINDOW:
                    {
                        SmoothWindow(ref Y, ref F, Y.Length, window);
                        break;
                    }
                case Filters.MEDIAN:
                    {
                        SmoothMedian(ref Y, ref F, Y.Length, window);
                        break;
                    }
                case Filters.EXPONENT:
                    {
                        SmoothExponent(ref Y, ref F, Y.Length, alpha);
                        break;
                    }
            }

            if (checkBox1.Checked)
            {
                ChartForm f = new ChartForm(switcher.ToString());
                f.Push(xy, 0);
                f.Push(X, F, 1);
                f.Show();
            }
            else
            {
                mode.Text = switcher.ToString();
                chart1.Series[0].Points.Clear();
                chartBuild(xy, 0);
                chartBuild(X, F, 1);  
            }
            
            button1.Enabled = true;
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
                    span.Add(input[j]);
                }
                span.Sort();
                int index = span.Count / 2 + 1;
                if (span.Count == index)
                    index = 0;
                output[i] = span[index];
            }
        }

        public void SmoothExponent(ref double[] input, ref double[] output, int n, double alpha)
        {
            int i;
            for (i = 1; i < n; i++)
            {
                output[i] = alpha * input[i] + (1 - alpha) * output[i - 1];
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
                            switcher = Filters.WINDOW;
                            break;
                        }
                    case "f2":
                        {
                            switcher = Filters.MEDIAN;
                            break;
                        }
                    case "f3":
                        {
                            textBox2.Enabled = true;
                            textBox1.Enabled = false;
                            switcher = Filters.EXPONENT;
                            break;
                        }
                }
            }
            else
            {
                if (textBox2.Enabled)
                {
                    textBox2.Enabled = false;
                    textBox1.Enabled = true;
                }
            }
            statusBar.Items.Clear();
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
