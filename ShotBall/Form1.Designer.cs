namespace ShotBall
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.count = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.st4 = new System.Windows.Forms.RadioButton();
            this.st3 = new System.Windows.Forms.RadioButton();
            this.st2 = new System.Windows.Forms.RadioButton();
            this.st1 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(680, 680);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Shot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Мат. ожидание:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Дисперсия:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(9, 70);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.count);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(708, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 142);
            this.panel1.TabIndex = 6;
            // 
            // count
            // 
            this.count.Location = new System.Drawing.Point(135, 20);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(100, 20);
            this.count.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Кол-во точек:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(132, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Machine gun";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(708, 377);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(135, 147);
            this.listBox1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(708, 361);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "X, Y:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.st4);
            this.panel2.Controls.Add(this.st3);
            this.panel2.Controls.Add(this.st2);
            this.panel2.Controls.Add(this.st1);
            this.panel2.Location = new System.Drawing.Point(711, 216);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 142);
            this.panel2.TabIndex = 10;
            // 
            // st4
            // 
            this.st4.AutoSize = true;
            this.st4.Location = new System.Drawing.Point(9, 110);
            this.st4.Name = "st4";
            this.st4.Size = new System.Drawing.Size(163, 17);
            this.st4.TabIndex = 3;
            this.st4.TabStop = true;
            this.st4.Text = "Сдвиг без зазеркаливания";
            this.st4.UseVisualStyleBackColor = true;
            this.st4.CheckedChanged += new System.EventHandler(this.RadiobuttonClick);
            // 
            // st3
            // 
            this.st3.AutoSize = true;
            this.st3.Location = new System.Drawing.Point(9, 82);
            this.st3.Name = "st3";
            this.st3.Size = new System.Drawing.Size(191, 17);
            this.st3.TabIndex = 2;
            this.st3.TabStop = true;
            this.st3.Text = "Сдвиг с зазеркаливанием по XY";
            this.st3.UseVisualStyleBackColor = true;
            this.st3.CheckedChanged += new System.EventHandler(this.RadiobuttonClick);
            // 
            // st2
            // 
            this.st2.AutoSize = true;
            this.st2.Location = new System.Drawing.Point(9, 48);
            this.st2.Name = "st2";
            this.st2.Size = new System.Drawing.Size(207, 17);
            this.st2.TabIndex = 1;
            this.st2.TabStop = true;
            this.st2.Text = "Сдвиг чередуя зазеркаливание X/Y";
            this.st2.UseVisualStyleBackColor = true;
            this.st2.CheckedChanged += new System.EventHandler(this.RadiobuttonClick);
            // 
            // st1
            // 
            this.st1.AutoSize = true;
            this.st1.Location = new System.Drawing.Point(9, 16);
            this.st1.Name = "st1";
            this.st1.Size = new System.Drawing.Size(135, 17);
            this.st1.TabIndex = 0;
            this.st1.TabStop = true;
            this.st1.Text = "Относительно центра";
            this.st1.UseVisualStyleBackColor = true;
            this.st1.CheckedChanged += new System.EventHandler(this.RadiobuttonClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(883, 178);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Refresh";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(708, 530);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(219, 95);
            this.listBox2.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 704);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton st4;
        private System.Windows.Forms.RadioButton st3;
        private System.Windows.Forms.RadioButton st2;
        private System.Windows.Forms.RadioButton st1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox count;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox2;
    }
}

