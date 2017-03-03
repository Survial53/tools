namespace SignalFiltering
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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.f1 = new System.Windows.Forms.RadioButton();
            this.f2 = new System.Windows.Forms.RadioButton();
            this.f3 = new System.Windows.Forms.RadioButton();
            this.filters = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.filters.SuspendLayout();
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
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = System.Drawing.Color.Black;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(513, 513);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(705, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(110, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Window size:";
            // 
            // f1
            // 
            this.f1.AutoSize = true;
            this.f1.Location = new System.Drawing.Point(3, 3);
            this.f1.Name = "f1";
            this.f1.Size = new System.Drawing.Size(86, 17);
            this.f1.TabIndex = 4;
            this.f1.TabStop = true;
            this.f1.Text = "Window filter";
            this.f1.UseVisualStyleBackColor = true;
            this.f1.CheckedChanged += new System.EventHandler(this.RadiobuttonClick);
            // 
            // f2
            // 
            this.f2.AutoSize = true;
            this.f2.Location = new System.Drawing.Point(3, 37);
            this.f2.Name = "f2";
            this.f2.Size = new System.Drawing.Size(85, 17);
            this.f2.TabIndex = 5;
            this.f2.TabStop = true;
            this.f2.Text = "Median Filter";
            this.f2.UseVisualStyleBackColor = true;
            this.f2.CheckedChanged += new System.EventHandler(this.RadiobuttonClick);
            // 
            // f3
            // 
            this.f3.AutoSize = true;
            this.f3.Location = new System.Drawing.Point(3, 70);
            this.f3.Name = "f3";
            this.f3.Size = new System.Drawing.Size(92, 17);
            this.f3.TabIndex = 6;
            this.f3.TabStop = true;
            this.f3.Text = "Exponent filter";
            this.f3.UseVisualStyleBackColor = true;
            this.f3.CheckedChanged += new System.EventHandler(this.RadiobuttonClick);
            // 
            // filters
            // 
            this.filters.Controls.Add(this.f2);
            this.filters.Controls.Add(this.label1);
            this.filters.Controls.Add(this.textBox1);
            this.filters.Controls.Add(this.f3);
            this.filters.Controls.Add(this.f1);
            this.filters.Location = new System.Drawing.Point(567, 12);
            this.filters.Name = "filters";
            this.filters.Size = new System.Drawing.Size(213, 100);
            this.filters.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 550);
            this.Controls.Add(this.filters);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.filters.ResumeLayout(false);
            this.filters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton f1;
        private System.Windows.Forms.RadioButton f2;
        private System.Windows.Forms.RadioButton f3;
        private System.Windows.Forms.Panel filters;
    }
}

