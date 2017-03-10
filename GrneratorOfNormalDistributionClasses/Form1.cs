using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ShotBall
{
    public partial class Form1 : Form
    {
        private MultidimNormDist target;
        private List<ClassItem> cls;
        private BindingSource bindSrcList = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            count.Text = "500";
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.IsMarksNextToAxis = false;
            chart1.ChartAreas[0].AxisY.IsMarksNextToAxis = false;
            chart1.ChartAreas[0].AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
            chart1.ChartAreas[0].AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
            chart1.ChartAreas[0].AxisX.Crossing = 0;
            chart1.ChartAreas[0].AxisY.Crossing = 0;
            chart1.ChartAreas[0].AxisX.IsLabelAutoFit = true;
            chart1.Legends[0].Enabled = false;
            textBox1.Text = "0 0";
            textBox2.Text = "1 1";
            textBox3.Text = "1";
            cls = new List<ClassItem>();
            bindSrcList.DataSource = cls;
            listBox2.DataSource = bindSrcList;
            listBox2.DisplayMember = "name";
        }

        public List<double> ParseStringToList(string s)
        {
            List<double> res = new List<double>();
            String[] nums = s.Split(' ');

            for (int i = 0; i < nums.Length; i++)
            {
                res.Add(Convert.ToDouble(nums[i]));
            }
            return res;
        }

        private bool VisiblyInitTarget()
        {
            foreach (ClassItem c in cls)
            {
                double prob = c.probability;
                int count = (int)(c.sample.Count * prob); 
                MultidimNormDist d = new MultidimNormDist();
                d.Init(c.matMath, c.matDisp, c.matCor, c.matCor.Count);
                List<AnyPoint> pts = new List<AnyPoint>(count);
                for (int i = 0; i < count; i++)
                {
                    pts.Add(null);
                }
                for (int j = 0; j < count; j++)
                {
                    pts[j] = new AnyPoint(target.GeneratePoint());
                }
                c.sample = pts;
            }
            return true;
        }

        private bool ShadowInitTarget()
        {
            int i, j;
            List<List<double>> rez = new List<List<double>>();
            List<List<double>> corMat = new List<List<double>>();
            List<double> matMath = new List<double>();
            List<double> matDisp = new List<double>();
            String math = textBox1.Text;
            String disp = textBox2.Text;

            matMath = ParseStringToList(math);
            matDisp = ParseStringToList(disp);

            for (i = 0; i < matMath.Count; i++)
            {
                rez.Add(new List<double>());
                corMat.Add(new List<double>());
            }

            for (i = 0; i < matMath.Count; i++)
            {
                for (j = 0; j < matMath.Count; j++)
                {
                    if (i != j)
                    {
                        corMat[i].Add(0);
                    }
                    else 
                    {
                        corMat[i].Add(1);
                    }
                } 
            }
      
            target = new MultidimNormDist();
            target.Init(matMath, matDisp, corMat, matMath.Count);
            return true;
        }

        public void RangePointsGenerate()
        {
            RefreshPts();
            bindSrcList.Clear();
            int cnt = Convert.ToInt32(count.Text);
            int classCount = Convert.ToInt32(textBox3.Text);

            List<AnyPoint> pts = new List<AnyPoint>(cnt);
            for (int i = 0; i < cnt; i++)
            {
                pts.Add(null);
            }

            for (int i = 0; i < classCount; i++)
            {
                if (!ShadowInitTarget())
                {
                    return;
                }

                for (int j = 0; j < cnt; j++)
                {
                    pts[j] = new AnyPoint(target.GeneratePoint());
                }
                ClassGenerate(pts);
            }

            for (int i = 0; i < classCount; i++)
            {
                DrawPoint(cls[i]);
            }
        }

        public void ClassUpdate()
        {
            ClearSeries();
            foreach(ClassItem c in cls)
            {
                ClassSampleGenerate(c);
                DrawPoint(c);
            }
        }

        private void ClassGenerate(List<AnyPoint> pts)
        {
            ClassItem tempCls = new ClassItem((cls.Count() + 1).ToString());
            tempCls.Init(target.matMath, target.matDisp, target.matCor, 1.0, pts);
            bindSrcList.Add(tempCls);
        }

        private List<AnyPoint> ClassSampleGenerate(ClassItem c)
        {
            double prob = c.probability;
                int count = (int)(c.sample.Count * prob);
                MultidimNormDist d = new MultidimNormDist();
                d.Init(c.matMath, c.matDisp, c.matCor, c.matCor.Count);
                List<AnyPoint> pts = new List<AnyPoint>(count);
                for (int j = 0; j < count; j++)
                {
                   pts.Add(new AnyPoint(d.GeneratePoint()));
                }
                c.sample = pts;
                return pts;
        }

        public double AbsMax(List<AnyPoint> pts)
        {
            double max = 0;
            for (int i = 0; i < pts.Count; i++)
            {
                double m;
                for (int j = 0; j < pts[i].points.Count; j++)
                {
                    m = Math.Abs(pts[i].points[j]);
                    if (m > max)
                    {
                        max = m;
                    }
                }
            }
            max = Math.Truncate(max);
            max += 1;
            return max;
        }

        public bool DrawPoint(List<double> xy)
        {
            if (xy.Count == 2)
            {
                chart1.Series[0].Points.AddXY(xy[0], xy[1]);
                return true;
            }
            else
            {
                chart1.Series[0].Points.AddXY(xy[0], xy[1]);
                listBox2.Items.Clear();
                listBox2.Items.Add("Пространство " + xy.Count + " - х мерное!");
                return false;
            }
        }

        public bool DrawPoint(ClassItem c)
        {
            int count = c.sample.Count;
            double max = AbsMax(c.sample);
            if (max > chart1.ChartAreas[0].AxisX.Maximum)
            {
                chart1.ChartAreas[0].AxisX.Maximum = max;
                chart1.ChartAreas[0].AxisX.Minimum = -max;
                chart1.ChartAreas[0].AxisY.Maximum = max;
                chart1.ChartAreas[0].AxisY.Minimum = -max;
            }
            chart1.Series.Add(newSeries(c));
            serCnt.Text = chart1.Series.Count.ToString();
            for (int i = 0; i < count; i++)
            {
                chart1.Series[chart1.Series.Count - 1].Points.AddXY(c.sample[i].points[0], c.sample[i].points[1]);
            }
            return true;
        }

        private Series newSeries(ClassItem c)
        {
            Series ser = new Series();
            ser.MarkerColor = c.color;
            ser.MarkerStyle = MarkerStyle.Circle;
            ser.MarkerSize = 5;
            ser.ChartType = SeriesChartType.Point;
            return ser;
        }

        public bool ClearSeries()
        {
            int count = chart1.Series.Count;
            for (int i = 0; i < count; i++)
            {
                chart1.Series.RemoveAt(0);
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
             RangePointsGenerate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshPts();
        }

        private bool RefreshPts()
        {
            ClearSeries();
            serCnt.Text = chart1.Series.Count.ToString();
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClassItem currReg = (ClassItem)listBox2.SelectedItem;
            if (currReg == null) return;
            Form2 f = new Form2(ref currReg);
            f.Owner = this;
            f.ShowDialog();
            f.Dispose();
        }
    }
}
