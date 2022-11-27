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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Threading;


namespace KG_1_852
{
    public partial class Form1 : Form
    {
        private Double Xc, Yc, alpha;
        private Double r;

        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.xlsx)|*.xlsx|All files(*.*)|*.*";

            chart1.Series.Clear();

            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            chart1.ChartAreas[0].CursorX.AutoScroll = true;
            chart1.ChartAreas[0].CursorY.AutoScroll = true;

        }

        //Запуск графика 
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

            progressBar1.Minimum = 0;
            progressBar1.Maximum = Convert.ToInt32(2 * r * 100);
            progressBar1.Step = 1; 

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

                progressBar1.PerformStep();
            }

            progressBar1.Value = 0; 
        }
        //Очистить график
        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
        }
        //Масштаб
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].AxisX.Title = "x";
                chart1.ChartAreas[0].AxisY.Title = "y";

                if (textBox5.Text != "")
                    chart1.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(textBox5.Text);
                if (textBox6.Text != "")
                    chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(textBox6.Text);

                if (textBox7.Text != "")
                    chart1.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(textBox7.Text);
                if (textBox8.Text != "")
                    chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(textBox8.Text);
            }
            catch 
            {
                MessageBox.Show("Проверьте корректность введенных данных. Нецелочисленные данные стоит писать через ,");
            }

        }

     
        //Загрузить из excel
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            try
            {
                //Файл
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                String filename = openFileDialog1.FileName;

                chart1.Series.Clear();

                chart1.Series.Add("График из файла");
                chart1.Series[0].Color = Color.Red;
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.ChartAreas[0].AxisX.Title = "x";
                chart1.ChartAreas[0].AxisY.Title = "y";

                Excel.Application app = new Excel.Application
                {
                    //Отобразить Excel
                    //Visible = true,
                    //Количество листов в рабочей книге
                    SheetsInNewWorkbook = 1
                };


                //Добавить рабочую книгу
                Excel.Workbook workBook = app.Workbooks.Open(filename);
                //Получаем первый лист документа (счет начинается с 1)
                Excel.Worksheet sheet = (Excel.Worksheet)app.ActiveSheet;

                int count = sheet.Cells[sheet.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;

                progressBar1.Minimum = 0;
                progressBar1.Maximum = count;
                progressBar1.Step = 1; 

                
                for (int i = 1; i <= count; i++)
                {
                    Double x1 = Convert.ToDouble(sheet.Cells[i, 1].Text);
                    Double y1 = Convert.ToDouble(sheet.Cells[i, 2].Text);

                    chart1.Series["График из файла"].Points.AddXY(x1, y1);
                    progressBar1.PerformStep();
                }

                workBook.Close();
                app.Quit();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workBook = null;
                sheet = null;
                app = null;


                progressBar1.Value = 0;
            }
            catch 
            {
                MessageBox.Show("При загрузке данных произошла ошибкаа");
            }

        }

        //Выгрузить в excel
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (chart1.Series.Count == 0)
            {
                MessageBox.Show("Нет точек графика");
                return; 
            }
            if (chart1.Series.Count > 1)
            {
                MessageBox.Show("Выгрузить точки в файл невозможно, поскольку на диаграмме более одного графика. Повторите попытку позже");
                return;
            }
            
            try
            {
           
                Excel.Application app = new Excel.Application
                {
                    //Отобразить Excel
                    //Visible = true,
                    //Количество листов в рабочей книге
                    SheetsInNewWorkbook = 1
                };
                //Добавить рабочую книгу
                Excel.Workbook workBook = app.Workbooks.Add(Type.Missing);
                //Отключить отображение окон с сообщениями
                app.DisplayAlerts = false;
                //Получаем первый лист документа (счет начинается с 1)
                Excel.Worksheet sheet = (Excel.Worksheet)app.Worksheets.get_Item(1);
                //Название листа (вкладки снизу)
                sheet.Name = "Точки";

                String NameGr = chart1.Series[0].Name;

                for (int i = 1; i < chart1.Series[NameGr].Points.Count; i ++)
                {
                    /*
                    sheet.Cells[i, 1] = chart1.Series["Окружность"].Points[i].XValue;
                    sheet.Cells[i, 2] = chart1.Series["Окружность"].Points[i].YValues;
                     */

                    sheet.Cells[i, 1] = chart1.Series[NameGr].Points[i].XValue;
                    sheet.Cells[i, 2] = chart1.Series[NameGr].Points[i].YValues;
                    
                }

                

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                // получаем выбранный файл
                String filename = saveFileDialog1.FileName;

                app.Application.ActiveWorkbook.SaveAs(filename, Type.Missing, Type.Missing);

                workBook.Close();
                app.Quit();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workBook = null;
                sheet = null;
                app = null;
                GC.Collect();
            }
            catch
            {
                MessageBox.Show("При выгрузке данных произошла ошибка");
            }
        }

        //Поиск графика по имени и возвращение его индекса 
        private int FindIndex(String Name)
        {
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                if (chart1.Series[i].Name == Name)
                    return i;
            }
            return -1;
        }
        //Обновить график
        private void button4_Click(object sender, EventArgs e)
        {
            
                if (checkBox1.Checked == true)
                {
                    int ind = FindIndex("Окружность");
                    if (ind == -1)
                    {
                        MessageBox.Show("Такого элемента нет");
                        checkBox1.Checked = false;
                        return;
                    }
                    chart1.Series["Окружность"].Points.Clear();
                    chart1.Series.RemoveAt(ind);
                    checkBox1.Checked = false;
                }
                if (checkBox2.Checked == true)
                {
                    int ind = FindIndex("1 спираль");
                    if (ind == -1)
                    {
                        MessageBox.Show("Такого элемента нет");
                        checkBox2.Checked = false;
                        return;
                    }
                    chart1.Series["1 спираль"].Points.Clear();
                    chart1.Series.RemoveAt(ind);
                    checkBox2.Checked = false;
                }
                if (checkBox3.Checked == true)
                {
                    int ind = FindIndex("2 спираль");
                    if (ind == -1)
                    {
                        MessageBox.Show("Такого элемента нет");
                        checkBox3.Checked = false;
                        return;
                    }
                    chart1.Series["2 спираль"].Points.Clear();
                    chart1.Series.RemoveAt(ind);
                    checkBox3.Checked = false;
                }
                if (checkBox4.Checked == true)
                {
                    int ind = FindIndex("3 спираль");
                    if (ind == -1)
                    {
                        MessageBox.Show("Такого элемента нет");
                        checkBox4.Checked = false;
                        return;
                    }
                    chart1.Series["3 спираль"].Points.Clear();
                    chart1.Series.RemoveAt(ind);
                    checkBox4.Checked = false;
                }
                if (checkBox5.Checked == true)
                {
                    int ind = FindIndex("4 спираль");
                    if (ind == -1)
                    {
                        MessageBox.Show("Такого элемента нет");
                        checkBox5.Checked = false;
                        return;
                    }
                    chart1.Series["4 спираль"].Points.Clear();
                    chart1.Series.RemoveAt(ind);
                    checkBox5.Checked = false;
                }
           

            chart1.Update();
        }

        

    }
}
