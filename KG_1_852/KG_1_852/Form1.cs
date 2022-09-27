using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace KG_1_852
{
    public partial class Form1 : Form
    {
        private Double Xc, Yc, alpha;
        private Double r;
 
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Xc = Convert.ToDouble(textBox1.Text);
            Yc = Convert.ToDouble(textBox2.Text);
            alpha = Convert.ToDouble(textBox3.Text);
            r = Convert.ToDouble(textBox4.Text);

            if (r <= 0)
                MessageBox.Show("Параметр r должен быть больше");
            if (alpha < 0)
                MessageBox.Show("Параметр alpha должен быть больше нуля");

            chart1.Series.Clear();

            chart1.Series.Add("Окружность");
            chart1.Series[0].Color = Color.Red;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart1.Series.Add("1 спираль");
            chart1.Series["1 спираль"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["1 спираль"].Color = Color.Blue;
            chart1.Series.Add("2 спираль");
            chart1.Series["2 спираль"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["2 спираль"].Color = Color.Green;
            chart1.Series.Add("3 спираль");
            chart1.Series["3 спираль"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["3 спираль"].Color = Color.BlueViolet;
            chart1.Series.Add("4 спираль");
            chart1.Series["4 спираль"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["4 спираль"].Color = Color.LightSeaGreen;

            chart1.ChartAreas[0].AxisX.Title = "x";
            chart1.ChartAreas[0].AxisY.Title = "y";
            
            
            chart1.ChartAreas[0].AxisX.Minimum = Xc - r - 1;
            chart1.ChartAreas[0].AxisX.Maximum = Xc + r + 1;
            //chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Minimum = Yc - r - 1;
            chart1.ChartAreas[0].AxisY.Maximum = Yc + r + 1;
            //chart1.ChartAreas[0].AxisY.Interval = 1;
            

            Double a1 = alpha * Math.PI / 180;
            Double a2 = (alpha + 90) * Math.PI / 180;
            Double a3 = (alpha + 180) * Math.PI / 180;
            Double a4 = (alpha + 270) * Math.PI / 180;

            for (double t = 0; t <= 2 * r ; t += 0.01)
            {
                chart1.Series["Окружность"].Points.AddXY(Xc + r * Math.Cos(t), Yc + r * Math.Sin(t));

                Double x = t / 2 * Math.Cos(t);
                Double y = t / 2 * Math.Sin(t);

                Double x1 = Xc + x * Math.Cos(a1) - y * Math.Sin(a1);
                Double y1 = Yc + x * Math.Sin(a1) + y * Math.Cos(a1);
                chart1.Series["1 спираль"].Points.AddXY(x1, y1);

                Double x2 = Xc + x * Math.Cos(a2) - y * Math.Sin(a2);
                Double y2 = Yc + x * Math.Sin(a2) + y * Math.Cos(a2);
                chart1.Series["2 спираль"].Points.AddXY(x2, y2);


                Double x3 = Xc + x * Math.Cos(a3) - y * Math.Sin(a3);
                Double y3 = Yc + x * Math.Sin(a3) + y * Math.Cos(a3);
                chart1.Series["3 спираль"].Points.AddXY(x3, y3);

                Double x4 = Xc + x * Math.Cos(a4) - y * Math.Sin(a4);
                Double y4 = Yc + x * Math.Sin(a4) + y * Math.Cos(a4);
                chart1.Series["4 спираль"].Points.AddXY(x4, y4); 
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
        }

       

        
    }
}
