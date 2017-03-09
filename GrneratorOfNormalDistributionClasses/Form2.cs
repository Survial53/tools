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
    public partial class Form2 : Form
    {
        private Form1 main;
        public ClassItem cls;
        public bool bt;
        public Form2(ref ClassItem c)
        {
            InitializeComponent();
            cls = c;
            bt = false;
            pictureBox1.BackColor = c.color;
            FillFields();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            main = this.Owner as Form1;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void FillFields()
        {
            textBox1.Text += cls.probability.ToString();
            textBox4.Text = cls.sample.Count.ToString();
            for (int i = 0; i < cls.matMath.Count; i++)
            {
                if (i == cls.matMath.Count - 1)
                {
                    textBox2.Text += cls.matMath[i].ToString();
                    textBox3.Text += cls.matDisp[i].ToString();
                }
                else
                {
                    textBox2.Text += cls.matMath[i].ToString() + " ";
                    textBox3.Text += cls.matDisp[i].ToString() + " ";
                }
            }
            bt = true;

            for(int i = 0; i < cls.matMath.Count; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), (i + 1).ToString());
                dataGridView1.Rows.Add();
            }

            for (int i = 0; i < cls.matMath.Count; i++)
            {
                for (int j = 0; j < cls.matMath.Count; j++)
                {
                    dataGridView1[j, i].Value = cls.matCor[i][j];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (main != null)
            {
                cls.probability = double.Parse(textBox1.Text.ToString());
                cls.matMath = main.ParseStringToList(textBox2.Text.ToString());
                cls.matDisp = main.ParseStringToList(textBox3.Text.ToString());
                int size = int.Parse(textBox4.Text);
                cls.sample = new List<AnyPoint>(size);
                for (int i = 0; i < size; i++)
                {
                    cls.sample.Add(null);
                }
                    for (int i = 0; i < cls.matMath.Count; i++)
                    {
                        for (int j = 0; j < cls.matMath.Count; j++)
                        {
                            cls.matCor[i][j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                        }
                    }
                main.ClassUpdate();
            }
            this.Close();
        }

        private void pictureBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pictureBox1.BackColor = colorDialog1.Color;
            cls.color = colorDialog1.Color;
        }
    }
}
