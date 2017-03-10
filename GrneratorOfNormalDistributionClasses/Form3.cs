using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ShotBall
{
    enum Rows 
    {
        MATH,
        DISP
    }
}

namespace ShotBall
{
    public partial class Form3 : Form
    {
        public static Random rand = new Random();

        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChartsClear();
        }

        private void InitData()
        {
            int vectorSize = int.Parse(vec.Text);
            InitVectors(vectorSize);
            InitCorrTables(vectorSize);
        }
        
        private MultidimNormDist InitTarget()
        {
            MultidimNormDist target = new MultidimNormDist();
            double pr = double.Parse(prob.Text);
            int count = (int)(int.Parse(start.Text) * pr);
            int vectorSize = int.Parse(vec.Text);
            List<double> math = RowToList(Rows.MATH);
            List<double> disp = RowToList(Rows.DISP);
            List<List<double>> corrMatr = ReadCorrMatrix();
            target.Init(math, disp, corrMatr, vectorSize);
            SetTabItems();
            return target;
        }

        private void InitVectors(int size)
        {
            for (int i = 0; i < size; i++)
            {
                dataGridView2.Columns.Add(i.ToString(), (i + 1).ToString());
            }
            //row 0 - MATH
            //row 1 - DISP

            dataGridView2.Rows.Add();

            for (int i = 0; i < size; i++)
            {
                dataGridView2[i, (int)Rows.MATH].Value = 3;
            }

            for (int i = 0; i < size; i++)
            {
                dataGridView2[i, (int)Rows.DISP].Value = 1;
            }
        }

        private void InitCorrTables(int size)
        {
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), (i + 1).ToString());
                dataGridView1.Rows.Add();
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        dataGridView1[i, i].Value = 1;
                    }
                    else
                    {
                        dataGridView1[j, i].Value = 0;
                    }
                }
            }
        }

        public void SetTabItems()
        {
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.IsMarksNextToAxis = false;
            chart1.ChartAreas[0].AxisY.IsMarksNextToAxis = false;

            chart1.Series[1].MarkerStyle = MarkerStyle.Circle;
            chart1.Series[1].MarkerSize = 2;
            chart1.Series[1].Color = Color.Red;
            chart1.Legends[0].Enabled = false;
        }

        private Series newSeries()
        {
            Series ser = new Series();
            ser.MarkerColor = Color.Green;
            ser.MarkerStyle = MarkerStyle.Circle;
            ser.MarkerSize = 5;
            ser.ChartType = SeriesChartType.Point;
            return ser;
        }

        private List<List<double>> ReadCorrMatrix()
        {
            int size = dataGridView2.ColumnCount;
            List<List<double>> corr = GetZeroMatrix(size);

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    corr[i][j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            return corr;
        }

        private List<double> RowToList(Rows row)
        {
            List<double> mylist = new List<double>();

            for (int i = 0; i < dataGridView2.ColumnCount; ++i)
            {
                mylist.Add(double.Parse(dataGridView2[i, (int)row].Value.ToString()));    
            }
            return mylist;
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

        private List<AnyPoint> SampleGenerate(int size, MultidimNormDist target)
        {
            List<AnyPoint> pts = new List<AnyPoint>(size);
            for (int i = 0; i < size; ++i)
            {
                pts.Add(new AnyPoint(target.GeneratePoint()));
            }
           
            return pts;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            start.Text = "10";
            end.Text = "1000";
            step.Text = "5";
            prob.Text = "0,5";
            vec.Text = "2";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            InitData();
            chart1.ChartAreas[0].AxisX.Interval = 30;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        public List<Distribution> DataGenerate()
        {
            List<Distribution> pack = new List<Distribution>();
            MultidimNormDist target = InitTarget();
            int vectorDimensions = int.Parse(vec.Text);
            int startCount = int.Parse(start.Text);
            int st = int.Parse(step.Text);
            int endCount = int.Parse(end.Text);
            double realProb;
            double teorProb = double.Parse(prob.Text);
            List<double> realMath;
            List<double> teorMath = RowToList(Rows.MATH);
            List<double> teorDisp = RowToList(Rows.DISP);
            List<List<double>> range = new List<List<double>>();

            int x = st;
            int p = 0;

            ChartsClear();
            for (int i = startCount; i < endCount; i += st)
            {
                Distribution d = new Distribution();
                List<AnyPoint> pts = SampleGenerate(i, target);
                int count = 0;
                for (int j = 0; j < i; j++)
                {
                    if (rand.NextDouble() <= teorProb)
                        count++;
                }
                DataCalculate(vectorDimensions, i, count, pts, out realProb, out realMath);
                List<double> realDisp = Dispersion(pts, vectorDimensions);
                double das = RowToList(Rows.MATH)[0];
                range.Add(new List<double> { i, RowToList(Rows.MATH)[0] });

                chart1.Series[1].Points.AddXY(x, realMath[0]);
                chart3.Series[1].Points.AddXY(x, realDisp[0]);
                chart2.Series[1].Points.AddXY(x, realProb);
                x += st;
                p++;

                d.realMath = realMath;
                d.teorMath = teorMath;
                d.realDisp = realDisp;
                d.teorDisp = teorDisp;
                d.realProbability = realProb;
                d.teorProbability = teorProb;
                d.vectorSize = i;
                d.points = pts;
                d.range = range;
                pack.Add(d);
            }
            return pack;
        }

        public void ChartsClear()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart3.Series[1].Points.Clear();
        }

        public void DrawHall(List<Distribution> dists, int mode)
        {
            List<double> real = new List<double>();
            List<double> teor = new List<double>();
            Chart chrt = new Chart();
            for (int i = 0; i < dists.Count; ++i)
            {
                if (mode == 1) 
                {
                    real = dists[i].realMath;
                    teor = dists[i].teorMath;
                    chrt = chart1;
                }
                else if (mode == 2)
                {
                    real = dists[i].realDisp;
                    teor = dists[i].teorDisp;
                    chrt = chart3;
                }
                else if (mode == 3)
                {
                    real.Add(dists[i].realProbability);
                    teor.Add(dists[i].teorProbability);
                    chrt = chart2;
                }
                else
                {
                    return;
                }

                chrt.Series[0].Points.AddXY(dists[i].vectorSize, (teor[0] + teor[0] * 0.05), ((teor[0] - teor[0] * 0.05)));
            }
        }

        private void Generate(object sender, EventArgs e)
        {
            List<Distribution> dists = DataGenerate();
            DrawHall(dists, 1);
            DrawHall(dists, 2);
            DrawHall(dists, 3);
        }

        public List<double> Dispersion(List<AnyPoint> pts, int vectorDimensions) 
        {
            List<double> disp = new List<double>(vectorDimensions);
            double sumPow = 0.0, sum = 0.0;
            int n = pts.Count;

            for (int j = 0; j < vectorDimensions; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    sumPow += Math.Pow(pts[i].points[j], 2);
                    sum += pts[i].points[j];
                }
                disp.Add((double)(sumPow - Math.Pow(sum, 2) / n) / (n - 1));
            }
            return disp;
        }

        public void DataCalculate(int vectorDimensions, int vectorCount, int count, List<AnyPoint> pts, out double probability, out List<double> math)
        {
            probability = (double)count / vectorCount;
            math = GetZeroList(vectorDimensions);

            for (int i = 0; i < vectorDimensions; i++)
            {
                for (int j= 0; j < count; j++)
                {
                    math[i] += pts[j].points[i];
                }
                math[i] /= count;
            }

            List<List<double>> corr = GetZeroMatrix(vectorDimensions);

            for (int i = 0; i < vectorDimensions; i++)
            {
                double sum = 0;
                double sum1 = 0;
                for (int j = 0; j < count; j++)
                {
                    sum += Math.Pow(pts[j].points[i], 2);
                    sum1 += Math.Pow(pts[j].points[i] - math[i], 2);
                }

                //corr[i][i] = (double)Math.Sqrt(sum / count - Math.Pow(math[i], 2));
                corr[i][i] = (double)sum1 / (count - 1);

                for (int k = 0; k < i; k++)
                {
                    sum = 0;
                    for (int j = 0; j < count; j++)
                        sum += (pts[j].points[i] - math[i]) * (pts[j].points[k] - math[k]);
                    corr[i][k] = sum / count / corr[i][i] / corr[k][k];
                }
            }
        }

        public List<double> GetZeroList(int size)
        {
            List<double> lst = new List<double>(size);
            for (int i = 0; i < size; i++)
            {
                lst.Add(0);
            }
            return lst;
        }

        private List<List<double>> GetZeroMatrix(int size)
        {
            List<List<double>> matrix = new List<List<double>>(size);
            for (int i = 0; i < size; ++i)
            {
                matrix.Add(new List<double>(size));
                for (int j = 0; j < size; ++j)
                {
                    matrix[i].Add(0);
                }
            }
            return matrix;
        }
    }
}
