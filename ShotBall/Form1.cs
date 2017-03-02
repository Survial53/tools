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
    enum State 
    {
        ST_1,
        ST_2,
        ST_3,
        ST_4
    }
}

namespace ShotBall
{
    public partial class Form1 : Form
    {
        private MultidimNormDist target;
        private List<AnyPoint> pts;
        private State switcher;
        private bool xy;
        private AnyPoint focus;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            st1.Checked = true;
            xy = false;
            focus = new AnyPoint(new List<double> {0, 0});
            count.Text = "3000";
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
            chart1.Series[0].MarkerSize = 3;
            textBox1.Text = "0 0";
            textBox2.Text = "1 1";


        }

        private List<double> ParseStringToList(string s)
        {
            List<double> res = new List<double>();
            String[] nums = s.Split(' ');

            for (int i = 0; i < nums.Length; i++)
            {
                res.Add(Convert.ToDouble(nums[i]));
            }
            return res;
        }

        private bool ShadowInitTarget()
        {
            listBox2.Items.Clear();
            List<List<double>> rez = new List<List<double>>();
            List<List<double>> corMat = new List<List<double>>();
            List<double> matMath = new List<double>();
            List<double> matDisp = new List<double>();
            String math = textBox1.Text;
            String disp = textBox2.Text;

            if (math.Length != disp.Length)
            {
                listBox2.Items.Add("Разная длины матриц MAT и DISP");
                return false;
            }

            matMath = ParseStringToList(math);
            matDisp = ParseStringToList(disp);

            for (int i = 0; i < matMath.Count; i++)
            {
                rez.Add(new List<double>());
                corMat.Add(new List<double>());
            }

            for (int i = 0; i < matMath.Count; i++)
            {
                for (int j = 0; j < matMath.Count; j++)
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
            target.Init(matMath, matDisp, corMat, 2);
            return true;
        }

        public void button_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                button1_Click(sender, e);
            }
        }

        public void RangePointsGenerate()
        {
            if (!ShadowInitTarget())
            {
                return;
            }
            RefreshPts();
            int cnt = Convert.ToInt32(count.Text);

            pts = new List<AnyPoint>(cnt);
            for (int i = 0; i < cnt; i++)
            {
                pts.Add(null);
            }

            List<double> point;
            for (int i = 0; i < cnt; i++)
            {
                point = target.GeneratePoint();
                pts[i] = new AnyPoint(point);

                pts[i].Add(focus.points);
                ShiftFocus(pts[i]);

                DrawPoint(pts[i].points);
                listBox1.Items.Add(pts[i].ToString());
            }

            double max = AbsMax(pts);
            chart1.ChartAreas[0].AxisX.Maximum = max;
            chart1.ChartAreas[0].AxisX.Minimum = -max;
            chart1.ChartAreas[0].AxisY.Maximum = max;
            chart1.ChartAreas[0].AxisY.Minimum = -max;

            for (int i = 0; i < cnt; i++)
            {
                chart1.Series[0].Points.AddXY(pts[i].points[0], pts[i].points[1]);
            }
        }

        private double AbsMax(List<AnyPoint> pts)
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

        public AnyPoint OnePointGenerate()
        {
            ShadowInitTarget();
            List<double> point;
            point = target.GeneratePoint();
            return new AnyPoint(point);
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

        private bool Inverse(ref List<double> pt, int mode)
        {
            if (mode == 0)
            {
                pt[0] *= -1;
                return true;
            }

            if (mode == 1)
            {
                pt[1] *= -1;
                return true;
            }

            for (int i = 0; i < pt.Count; i++)
            {   
                pt[i] *= -1;
                return true;
            }
            return false;
        }

        public AnyPoint ShiftFocus(AnyPoint pt)
        {
            List<double> coords = pt.points;
            switch (switcher)
            {
                case State.ST_1:
                    {   
                        break;
                    }
                case State.ST_2:
                    {
                        if (!xy)
                        {
                            Inverse(ref coords, 0);
                            xy = true;
                        }
                        else 
                        {
                            Inverse(ref coords, 1);
                            xy = false;
                        }
                        focus.Replace(coords);
                        break;
                    }
                case State.ST_3:
                    {
                        Inverse(ref coords, 2);
                        focus.Replace(coords);
                        break;
                    }
                case State.ST_4:
                    {
                        focus.Replace(coords);
                        break;
                    }
            }
            return pt;
        }

        private void RadiobuttonClick(object sender, EventArgs e)
        {
            RadioButton selected = (RadioButton)sender;
            if (selected.Checked == true)
            {
                switch (selected.Name)
                {
                    case "st1":
                        {
                            chart1.Series[0].Points.Clear();
                            switcher = State.ST_1;
                            break;
                        }
                    case "st2":
                        {
                            chart1.Series[0].Points.Clear();
                            switcher = State.ST_2;
                            break;
                        }
                    case "st3":
                        {
                            chart1.Series[0].Points.Clear();
                            switcher = State.ST_3;
                            break;
                        }
                    case "st4":
                        {
                            chart1.Series[0].Points.Clear();
                            switcher = State.ST_4;
                            break;
                        } 
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> pts = OnePointGenerate().points;
            DrawPoint(pts);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshPts();
            RangePointsGenerate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshPts();
        }

        private bool RefreshPts()
        {
            chart1.Series[0].Points.Clear();
            xy = false;
            focus = new AnyPoint(new List<double> { 0, 0 });
            pts = null;
            listBox1.Items.Clear();
            return true;
        }
    }
}
