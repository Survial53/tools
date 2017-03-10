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
                dataGridView2[i, (int)Rows.MATH].Value = 0;
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
                        dataGridView1[j, i].Value = 1;
                    }
                    else
                    {
                        dataGridView1[j, i].Value = 0;
                    }
                }
            }
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
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Generate(object sender, EventArgs e)
        {
            MultidimNormDist target = InitTarget();
            int vectorDimensions = int.Parse(vec.Text);
            int startCount = int.Parse(start.Text);
            int st = int.Parse(step.Text);
            int endCount = int.Parse(end.Text);
            double probability = double.Parse(prob.Text);
            List<double> math;
            
            List<List<AnyPoint>> samples = new List<List<AnyPoint>>();

            int x = st;

            for (int i = startCount; i < endCount; i += st)
            {
                List<AnyPoint> pts = SampleGenerate(i, target);

                int count = 0;
                for (int j = 0; j < i; j++)
                {
                    if (rand.NextDouble() <= probability)
                        count++;
                }
                DataCalculate(vectorDimensions, i, count, pts, out probability, out math);
                List<double> disp = Dispersion(pts, vectorDimensions);
                double sum = 0;
                for (int k = 0; k < vectorDimensions; ++k)
                {
                    sum += Math.Pow(math[k], 2);  
                }
                chart1.Series[0].Points.AddXY(x, sum);
               // samples.Add(pts);
                x += st;
            }
        }

        public List<double> Dispersion(List<AnyPoint> pts, int vectorDimensions) //несмещенная дисперсия
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
            int s = 0;
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
