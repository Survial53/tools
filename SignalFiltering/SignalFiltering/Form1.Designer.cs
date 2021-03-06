﻿namespace SignalFiltering
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.f1 = new System.Windows.Forms.RadioButton();
            this.f2 = new System.Windows.Forms.RadioButton();
            this.f3 = new System.Windows.Forms.RadioButton();
            this.filters = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.statusBar = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.mode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.filters.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(12, 25);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series3.Color = System.Drawing.Color.SteelBlue;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Color = System.Drawing.Color.Red;
            series4.Legend = "Legend1";
            series4.Name = "Series2";
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(513, 513);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(736, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(138, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 4);
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
            this.filters.Controls.Add(this.label2);
            this.filters.Controls.Add(this.textBox2);
            this.filters.Controls.Add(this.f2);
            this.filters.Controls.Add(this.label1);
            this.filters.Controls.Add(this.textBox1);
            this.filters.Controls.Add(this.f3);
            this.filters.Controls.Add(this.f1);
            this.filters.Location = new System.Drawing.Point(567, 12);
            this.filters.Name = "filters";
            this.filters.Size = new System.Drawing.Size(244, 100);
            this.filters.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Alpha:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(138, 65);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 8;
            // 
            // statusBar
            // 
            this.statusBar.FormattingEnabled = true;
            this.statusBar.Location = new System.Drawing.Point(567, 183);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(244, 56);
            this.statusBar.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(564, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Status bar:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(570, 267);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(110, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Different windows";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // mode
            // 
            this.mode.AutoSize = true;
            this.mode.Location = new System.Drawing.Point(12, 9);
            this.mode.Name = "mode";
            this.mode.Size = new System.Drawing.Size(37, 13);
            this.mode.TabIndex = 12;
            this.mode.Text = "Mode:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 550);
            this.Controls.Add(this.mode);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusBar);
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
            this.PerformLayout();

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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListBox statusBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label mode;
    }
}

