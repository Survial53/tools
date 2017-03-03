using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalFiltering
{
    public partial class ChartForm : Form
    {
        public ChartForm(string name)
        {
            //mode.Text = name;
            InitializeComponent();
        }

        private void Chart_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.IsMarksNextToAxis = false;
            chart1.ChartAreas[0].AxisY.IsMarksNextToAxis = false;
            chart1.ChartAreas[0].AxisX.Crossing = 0;
            chart1.ChartAreas[0].AxisY.Crossing = 0;
            chart1.ChartAreas[0].AxisX.IsLabelAutoFit = true;
            chart1.Legends[0].Enabled = false;
            mode.Text = "";
        }

        public void Push(double[] x, double[] y, int series)
        {
            chart1.Series[series].Points.Clear();
            for (int j = 0; j < x.Count(); j++)
            {
                chart1.Series[series].Points.AddXY(x[j], y[j]);
            }
        }

        public void Push(List<AnyPoint> pts, int series)
        {
            chart1.Series[series].Points.Clear();
            for (int j = 0; j < pts.Count(); j++)
            {
                chart1.Series[series].Points.AddXY(pts[j].points[0], pts[j].points[1]);
            }
        }
    }
}
