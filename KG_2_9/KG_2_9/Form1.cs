using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG_2_9
{
    public partial class Form1 : Form
    {
        Bitmap canvas;
        int x, y, z;
        List<List<Point>> All; 

        public Form1()
        {
            InitializeComponent();
            canvas = new Bitmap(pictureBox1.ClientRectangle.Width, pictureBox1.ClientRectangle.Height);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int height = 260;
            int lenght = 300;
            int weight = 200;

            Graphics g = Graphics.FromImage(canvas);
            pictureBox1.Image = canvas;
            g.Clear(Color.White);

            All = new List<List<Point>>();

            if (radioButton1.Checked)
            {
                Draw3D(lenght, height, weight, g);
            }
            if (radioButton2.Checked)
            {
                Draw3D(lenght, height, weight, g);
                g.Clear(Color.White);
                Draw2D_XY(lenght, height, weight, g);
            }

            if (radioButton3.Checked)
            {
                Draw3D(lenght, height, weight, g);
                g.Clear(Color.White);
                Draw2D_XZ(lenght, height, weight, g);
            }
            
        }


         public void Draw2D(int lenght, int height, int weight, Graphics g)
            {
                 x = 50;
                 y = 300;
                 z = 350;
                

                //Фронтальная плоскость - XY
                //Z = 0

                // Фундамент 
                int HFundament = 9 * height / 44;
                int LFundament =  lenght;
                Point[] Foundation = new Point[4];
                
                Foundation[0] = new Point(x, y, 0);
                Foundation[1] = new Point(x + LFundament, y, 0);
                Foundation[2] = new Point(x, y - HFundament, 0);
                Foundation[3] = new Point(x + LFundament, y - HFundament, 0);

                g.DrawLine(Pens.Black, Foundation[0].x, Foundation[0].y, Foundation[1].x, Foundation[1].y);
                g.DrawLine(Pens.Black, Foundation[0].x, Foundation[0].y, Foundation[2].x, Foundation[2].y);
                g.DrawLine(Pens.Black, Foundation[2].x, Foundation[2].y, Foundation[3].x, Foundation[3].y);
                g.DrawLine(Pens.Black, Foundation[1].x, Foundation[1].y, Foundation[3].x, Foundation[3].y);


                //Ступени
                // Сверху вниз, слева направо 
                
                Point[] Ladder = new Point[8];
                Ladder[0] = new Point(x + lenght / 4, y, 0);
                Ladder[1] = new Point(x + 3 * lenght / 4, y , 0);

                Ladder[2] = new Point(x + lenght / 4, y - HFundament / 3, 0);
                Ladder[3] = new Point(x + 3 * lenght / 4, y - HFundament / 3 , 0);

                Ladder[4] = new Point(x + lenght / 4, y - 2 * HFundament / 3, 0);
                Ladder[5] = new Point(x + 3 * lenght / 4,  y - 2 * HFundament / 3, 0);

                Ladder[6] = new Point(x + lenght / 4, y - HFundament, 0);
                Ladder[7] = new Point(x + 3 * lenght / 4, y - HFundament, 0);

                g.DrawLine(Pens.Red, Ladder[0].x, Ladder[0].y, Ladder[1].x, Ladder[1].y);
                g.DrawLine(Pens.Red, Ladder[2].x, Ladder[2].y, Ladder[3].x, Ladder[3].y);
                g.DrawLine(Pens.Red, Ladder[4].x, Ladder[4].y, Ladder[5].x, Ladder[5].y);
                g.DrawLine(Pens.Red, Ladder[6].x, Ladder[6].y, Ladder[7].x, Ladder[7].y);
                g.DrawLine(Pens.Red, Ladder[0].x, Ladder[0].y, Ladder[6].x, Ladder[6].y);
                g.DrawLine(Pens.Red, Ladder[1].x, Ladder[1].y, Ladder[7].x, Ladder[7].y);

                // Само здание 
                Point[] Building = new Point[4];
                int HBuilding = 41 * height / 55;
                int LBuilding = 47 * lenght / 64;

                Building[0] = new Point(x + lenght / 12, y - HFundament , 0);
                Building[1] = new Point(x + lenght / 12, y - HFundament - HBuilding, 0);
                Building[2] = new Point(x + lenght / 6 + LBuilding, y - HFundament , 0);
                Building[3] = new Point(x + lenght / 6 + LBuilding, y - HFundament - HBuilding, 0);
                
                g.DrawLine(Pens.DarkBlue, Building[0].x, Building[0].y, Building[1].x, Building[1].y);
                g.DrawLine(Pens.DarkBlue, Building[2].x, Building[2].y, Building[3].x, Building[3].y);
                g.DrawLine(Pens.DarkBlue, Building[0].x, Building[0].y, Building[2].x, Building[2].y);
                g.DrawLine(Pens.DarkBlue, Building[1].x, Building[1].y, Building[3].x, Building[3].y);
                
                //Крыша 
                Point[] Roof = new Point[4];
                int HRoof = 3 *  height/ 44;
                int LRoof = 25 * lenght / 28;

                Roof[0] = new Point(x + (LFundament - LRoof) / 2, y - HFundament - HBuilding, 0);
                Roof[1] = new Point(x + (LFundament - LRoof) / 2 + LRoof, y - HFundament - HBuilding, 0);
                Roof[2] = new Point(x + (LFundament - LRoof) / 2, y - HFundament - HBuilding - HRoof, 0);
                Roof[3] = new Point(x + (LFundament - LRoof) / 2 + LRoof, y - HFundament - HBuilding - HRoof, 0);

                g.DrawLine(Pens.DarkCyan, Roof[0].x, Roof[0].y, Roof[1].x, Roof[1].y);
                g.DrawLine(Pens.DarkCyan, Roof[2].x, Roof[2].y, Roof[3].x, Roof[3].y);
                g.DrawLine(Pens.DarkCyan, Roof[0].x, Roof[0].y, Roof[2].x, Roof[2].y);
                g.DrawLine(Pens.DarkCyan, Roof[1].x, Roof[1].y, Roof[3].x, Roof[3].y);

                //Коломны
                Point[] Column1 = new Point[4];
                Point[] Column2 = new Point[4];
                int HColumns = 125 * height / 220;
                int LColumns = 15 * lenght / 280;
                int WColumns = LFundament / 6;

                Column1[0] = new Point(x + WColumns, y - HFundament, 0);
                Column1[1] = new Point(x + WColumns - LColumns, y - HFundament, 0);
                Column1[2] = new Point(x + WColumns, y - HFundament - HColumns, 0);
                Column1[3] = new Point(x + WColumns - LColumns, y - HFundament - HColumns, 0);

                Column2[0] = new Point(x + WColumns + (LBuilding - LColumns), y - HFundament, 0);
                Column2[1] = new Point(x + WColumns - LColumns + (LBuilding - LColumns), y - HFundament, 0);
                Column2[2] = new Point(x + WColumns + (LBuilding - LColumns), y - HFundament - HColumns, 0);
                Column2[3] = new Point(x + WColumns - LColumns + (LBuilding - LColumns), y - HFundament - HColumns, 0);

                g.DrawLine(Pens.Red, Column1[0].x, Column1[0].y, Column1[1].x, Column1[1].y);
                g.DrawLine(Pens.Red, Column1[2].x, Column1[2].y, Column1[3].x, Column1[3].y);
                g.DrawLine(Pens.Red, Column1[0].x, Column1[0].y, Column1[2].x, Column1[2].y);
                g.DrawLine(Pens.Red, Column1[1].x, Column1[1].y, Column1[3].x, Column1[3].y);

                g.DrawLine(Pens.Red, Column2[0].x, Column2[0].y, Column2[1].x, Column2[1].y);
                g.DrawLine(Pens.Red, Column2[2].x, Column2[2].y, Column2[3].x, Column2[3].y);
                g.DrawLine(Pens.Red, Column2[0].x, Column2[0].y, Column2[2].x, Column2[2].y);
                g.DrawLine(Pens.Red, Column2[1].x, Column2[1].y, Column2[3].x, Column2[3].y);

                //Дверь 
                Point[] Door = new Point[4];
                int HDoor = height / 2;
                int LDoor = lenght / 4;

                Door[0] = new Point(x + lenght / 4 + lenght / 8, y - HFundament, 0);
                Door[1] = new Point(x + lenght / 4 + lenght / 8 + LDoor, y - HFundament, 0);
                Door[2] = new Point(x + lenght / 4 + lenght / 8, y - HFundament - HDoor, 0);
                Door[3] = new Point(x + lenght / 4 + lenght / 8 + LDoor, y - HFundament - HDoor, 0);

                g.DrawLine(Pens.DarkCyan, Door[0].x, Door[0].y, Door[1].x, Door[1].y);
                g.DrawLine(Pens.DarkCyan, Door[0].x, Door[0].y, Door[2].x, Door[2].y);
                g.DrawLine(Pens.DarkCyan, Door[1].x, Door[1].y, Door[3].x, Door[3].y);
                g.DrawArc(Pens.DarkCyan, Door[2].x, Door[2].y - height / 14, LDoor, 2 * HFundament / 3, 180, 180);

                // Фонарь 
                g.DrawEllipse(Pens.Red, x + lenght / 2 - 5 * LColumns / 4, y - HFundament - HDoor - 65 * HFundament / 64, 
                    HDoor / 3 ,7 * HFundament / 12);


                // Вид сверху - XZ //
             //
                // Y = 0
             //
                // Основа
                Point[] Base = new Point[8];
                int LBase = lenght;
                int WBase = weight;

                Base[0] = new Point(x, 0, z);
                Base[1] = new Point(x + LBase, 0, z);
                Base[2] = new Point(x + LBase, 0, z + WBase);
                Base[3] = new Point(x + 3 * LBase / 4, 0, z + WBase);
                Base[4] = new Point(x + 3 * LBase / 4, 0, z + WBase - 4 * WBase / 21);
                Base[5] = new Point(x + LBase / 4, 0, z + WBase - 4 * WBase / 21);
                Base[6] = new Point(x + LBase / 4, 0, z + WBase);
                Base[7] = new Point(x, 0, z + WBase);

                // 0 - 1 - 2 - 3 - 4 - 5 - 6 - 7 - 0 - обход 
                g.DrawLine(Pens.Black, Base[0].x, Base[0].z, Base[1].x, Base[1].z);
                g.DrawLine(Pens.Black, Base[1].x, Base[1].z, Base[2].x, Base[2].z);
                g.DrawLine(Pens.Black, Base[2].x, Base[2].z, Base[3].x, Base[3].z);
                g.DrawLine(Pens.Black, Base[3].x, Base[3].z, Base[4].x, Base[4].z);
                g.DrawLine(Pens.Black, Base[4].x, Base[4].z, Base[5].x, Base[5].z);
                g.DrawLine(Pens.Black, Base[5].x, Base[5].z, Base[6].x, Base[6].z);
                g.DrawLine(Pens.Black, Base[6].x, Base[6].z, Base[7].x, Base[7].z);
                g.DrawLine(Pens.Black, Base[7].x, Base[7].z, Base[0].x, Base[0].z);

                //Ступени
                Point[] LadderT = new Point[6];
                int LLadder = lenght / 2;
                int WLadder = 2 * weight / 7;

                LadderT[0] = new Point(x + lenght / 4, 0, z + weight + WLadder / 3);
                LadderT[1] = new Point(x + lenght / 4, 0, z + weight + WLadder / 3 - WLadder / 2);
                LadderT[2] = new Point(x + lenght / 4, 0, z + weight + WLadder / 3 - WLadder);
                LadderT[3] = new Point(x + 3 * lenght / 4, 0, z + weight + WLadder / 3 - WLadder);
                LadderT[4] = new Point(x + 3 * lenght / 4, 0, z + weight + WLadder / 3 - WLadder / 2);
                LadderT[5] = new Point(x + 3 * lenght / 4, 0, z + weight + WLadder / 3);

                //
                g.DrawLine(Pens.Black, LadderT[0].x, LadderT[0].z, LadderT[5].x, LadderT[5].z);
                g.DrawLine(Pens.Black, LadderT[1].x, LadderT[1].z, LadderT[4].x, LadderT[4].z);
                g.DrawLine(Pens.Black, LadderT[2].x, LadderT[2].z, LadderT[3].x, LadderT[3].z);
                g.DrawLine(Pens.Black, LadderT[0].x, LadderT[0].z, LadderT[2].x, LadderT[2].z);
                g.DrawLine(Pens.Black, LadderT[3].x, LadderT[3].z, LadderT[5].x, LadderT[5].z);


                //Коломны
                g.DrawEllipse(Pens.Black, x + WColumns - LColumns, z + 3 * weight / 4, 3 * weight / 28, 3 * weight / 28);
                g.DrawEllipse(Pens.Black, x + WColumns - LColumns + (LBuilding - LColumns), z + 3 * weight / 4, 3 * weight / 28, 3 * weight / 28);

                //Прямоугольник
                Point[] Roof2 = new Point[4];
                int WRoof2 = 2 * weight / 7;

                Roof2[0] = new Point(x + lenght / 12, 0, z + 3 * weight / 28);
                Roof2[1] = new Point(x + lenght / 6 + LBuilding, 0, z + 3 * weight / 28);
                Roof2[2] = new Point(x + lenght / 6 + LBuilding, 0, z + weight / 3);
                Roof2[3] = new Point(x + lenght / 12, 0, z + weight / 3);

                g.DrawLine(Pens.Black, Roof2[0].x, Roof2[0].z, Roof2[1].x, Roof2[1].z);
                g.DrawLine(Pens.Black, Roof2[1].x, Roof2[1].z, Roof2[2].x, Roof2[2].z);
                g.DrawLine(Pens.Black, Roof2[2].x, Roof2[2].z, Roof2[3].x, Roof2[3].z);
                g.DrawLine(Pens.Black, Roof2[0].x, Roof2[0].z, Roof2[3].x, Roof2[3].z);

                //Мини приямоугольник 
             Point[] Roof22 = new Point[4];
             int WRoof22 = weight / 14;

             Roof22[0] = new Point(x + (LFundament - LRoof) / 2, 0, z + weight / 3);
             Roof22[1] = new Point(x + (LFundament - LRoof) / 2 + LRoof, 0, z + weight / 3);
             Roof22[2] = new Point(x + (LFundament - LRoof) / 2 + LRoof, 0, z + weight / 3 + WRoof22);
             Roof22[3] = new Point(x + (LFundament - LRoof) / 2, 0, z + weight / 3 + WRoof22);

             g.DrawLine(Pens.Black, Roof22[0].x, Roof22[0].z, Roof22[1].x, Roof22[1].z);
             g.DrawLine(Pens.Black, Roof22[1].x, Roof22[1].z, Roof22[2].x, Roof22[2].z);
             g.DrawLine(Pens.Black, Roof22[2].x, Roof22[2].z, Roof22[3].x, Roof22[3].z);
             g.DrawLine(Pens.Black, Roof22[0].x, Roof22[0].z, Roof22[3].x, Roof22[3].z);

                //Выступ
             g.DrawArc(Pens.Black, x + (LFundament - LRoof) / 2, z + weight / 3 + WRoof22 - 30 * weight / 84, 
                 LRoof, 30 * weight / 42, 
                 0, 180);

             //Дверь и Фонарь 
             Point[] Door2 = new Point[4];

             Door2[0] = new Point(x + lenght / 4 + lenght / 8, 0, z + weight / 3);
             Door2[1] = new Point(x + lenght / 4 + lenght / 8 + LDoor, 0, z + weight / 3);
             Door2[2] = new Point(x + lenght / 4 + lenght / 8 + LDoor, 0, z + weight / 3 + WRoof22);
             Door2[3] = new Point(x + lenght / 4 + lenght / 8, 0, z + weight / 3 + WRoof22);

             Pen pen = new Pen(Brushes.Black, 1);
             pen.DashStyle =  System.Drawing.Drawing2D.DashStyle.Dash;

             g.DrawLine(pen, Door2[0].x, Door2[0].z, Door2[1].x, Door2[1].z);
             g.DrawLine(pen, Door2[1].x, Door2[1].z, Door2[2].x, Door2[2].z);
             g.DrawLine(pen, Door2[2].x, Door2[2].z, Door2[3].x, Door2[3].z);
             g.DrawLine(pen, Door2[0].x, Door2[0].z, Door2[3].x, Door2[3].z);

             Point[] Lamp = new Point[4];

             Lamp[0] = new Point(x + lenght / 2 - 5 * LColumns / 4, 0, z + weight / 3);
             Lamp[1] = new Point(x + lenght / 2 - 5 * LColumns / 4 + HDoor / 3, 0, z + weight / 3);
             Lamp[2] = new Point(x + lenght / 2 - 5 * LColumns / 4 + HDoor / 3, 0, z + weight / 3 + WRoof22);
             Lamp[3] = new Point(x + lenght / 2 - 5 * LColumns / 4, 0, z + weight / 3 + WRoof22);

             g.DrawLine(pen, Lamp[0].x, Lamp[0].z, Lamp[1].x, Lamp[1].z);
             g.DrawLine(pen, Lamp[1].x, Lamp[1].z, Lamp[2].x, Lamp[2].z);
             g.DrawLine(pen, Lamp[2].x, Lamp[2].z, Lamp[3].x, Lamp[3].z);
             g.DrawLine(pen, Lamp[0].x, Lamp[0].z, Lamp[3].x, Lamp[3].z);

         }

         public void Draw3D(int lenght, int height, int weight, Graphics g)
         {
             x = 100;
             y = 500;
             z = 300;

             // Фундамент 
             Double HFundament = 9 * height / 44;
             Double LFundament = lenght;
             Double WFundament = weight;
             List<Point> Fundament = new List<Point>();

             //Ступени
             Double HL = HFundament;
             Double LL = lenght / 2;
             Double WL = weight * 2 / 7;
             List<Point> Ladder = new List<Point>();

             //Здание 
             Double HB = 4 * height / 5;
             Double LB = 5 * lenght / 6;
             Double WB = 2 * weight / 7;
             List<Point> Building = new List<Point>();

             //Крыша 
             Double HR = 3 * height / 44;
             Double LR = 11 * lenght / 12;
             Double WR =  weight / 14;
             List<Point> Roof = new List<Point>();

             //Выступ
             Double HLedge = height / 2;
             Double LLedge = lenght / 4;
             Double WLedge = WR;
             List<Point> Ledge = new List<Point>();
             //Крыша над зданием 
             List<Point> RoofB = new List<Point>();
             //Крыша - полукруг 
             Double WR2 = WLedge + WB;
             List<Point> Roof2 = new List<Point>();

             //Дверь
             Double HD = height / 2;
             Double LD = lenght / 4;
             Double WD = WR;
             List<Point> Door = new List<Point>();
             //Верхушка двери
             List<Point> DoorTop = new List<Point>();
             Double HDT = 2 * HD / 13;

             //Коломны
             Double HColumns = 125 * height / 220;
             Double LColumns = 15 * lenght / 280;
             Double WColumns = WFundament / 12;
             List<Point> Column1 = new List<Point>();
             List<Point> Column2 = new List<Point>();
             //Округление концов 
             Double Otstup = WColumns / 4; 
             List<Point> CE11 = new List<Point>();
             List<Point> CE12 = new List<Point>();
             List<Point> CE13 = new List<Point>();
             List<Point> CE14 = new List<Point>();

             List<Point> CE21 = new List<Point>();
             List<Point> CE22 = new List<Point>();
             List<Point> CE23 = new List<Point>();
             List<Point> CE24 = new List<Point>();

             //Фонарик 
             Double HF = 7 * HFundament / 12;
             Double LF = LD / 2;
             Double WF = WR;
             List<Point> LampDown = new List<Point>();
             List<Point> LampUp = new List<Point>();


             // Задание точек 
             Fundament.Add(new Point(x, y, z));
             Fundament.Add(new Point(x + LFundament, y, z));
             Fundament.Add(new Point(x + LFundament, y, z + WFundament));
             Fundament.Add(new Point(x , y , z + WFundament));

             Fundament.Add(new Point(x, y - HFundament, z));
             Fundament.Add(new Point(x + LFundament, y - HFundament, z));
             Fundament.Add(new Point(x + LFundament, y - HFundament, z + WFundament));
             Fundament.Add(new Point(x, y - HFundament, z + WFundament));

             //Нижняя  ступень 
             Ladder.Add(new Point(x + lenght / 4, y , z - WL / 2));
             Ladder.Add(new Point(x + lenght / 4 + LL , y , z - WL / 2));
             Ladder.Add(new Point(x + lenght / 4 + LL , y , z + WL / 2));
             Ladder.Add(new Point(x + lenght / 4 , y, z + WL / 2));

             Ladder.Add(new Point(x + lenght / 4, y - HL / 3, z - WL / 2));
             Ladder.Add(new Point(x + lenght / 4 + LL, y - HL / 3, z - WL / 2));
             Ladder.Add(new Point(x + lenght / 4 + LL, y - HL / 3, z + WL / 2));
             Ladder.Add(new Point(x + lenght / 4, y - HL / 3, z + WL / 2));

             //Вторая ступень
             Ladder.Add(new Point(x + lenght / 4, y - HL / 3, z ));
             Ladder.Add(new Point(x + lenght / 4 + LL, y - HL / 3, z ));
             Ladder.Add(new Point(x + lenght / 4 + LL, y - HL / 3, z + WL / 2));
             Ladder.Add(new Point(x + lenght / 4, y - HL / 3, z + WL / 2));

             Ladder.Add(new Point(x + lenght / 4, y - 2 * HL / 3, z ));
             Ladder.Add(new Point(x + lenght / 4 + LL, y - 2 * HL / 3, z ));
             Ladder.Add(new Point(x + lenght / 4 + LL, y - 2 * HL / 3, z + WL / 2));
             Ladder.Add(new Point(x + lenght / 4, y - 2 * HL / 3, z + WL / 2));

             //Третья недоступень 
             Ladder.Add(new Point(x + lenght / 4 , y - 2 * HL / 3, z + WL / 2));
             Ladder.Add(new Point(x + lenght / 4 + LL, y - 2 * HL / 3, z + WL / 2));
             Ladder.Add(new Point(x + lenght / 4, y - HL, z + WL / 2));
             Ladder.Add(new Point(x + lenght / 4 + LL, y - HL, z + WL / 2));


             //Обрисуем ступени в фундаменте
             //Верх
             Fundament.Add(new Point(x + lenght / 4, y - HL, z));
             Fundament.Add(Ladder[18]);
             Fundament.Add(Ladder[19]);
             Fundament.Add(new Point(x + lenght / 4 + LL, y - HL, z));

             //Низ
             Fundament.Add(new Point(x + lenght / 4, y , z));
             Fundament.Add(Ladder[3]);
             Fundament.Add(Ladder[2]);
             Fundament.Add(new Point(x + lenght / 4 + LL, y, z));

             // Здание
             Building.Add(new Point(x + lenght / 12, y - HFundament, z + 2 * weight / 3));
             Building.Add(new Point(x + lenght / 12 + LB, y - HFundament, z + 2 * weight / 3));
             Building.Add(new Point(x + lenght / 12 + LB, y - HFundament, z + 2 * weight / 3 + WB));
             Building.Add(new Point(x + lenght / 12, y - HFundament, z + 2 * weight / 3 + WB));

             Building.Add(new Point(x + lenght / 12, y - HFundament - HB, z + 2 * weight / 3));
             Building.Add(new Point(x + lenght / 12 + LB, y - HFundament - HB, z + 2 * weight / 3));
             Building.Add(new Point(x + lenght / 12 + LB, y - HFundament - HB, z + 2 * weight / 3 + WB));
             Building.Add(new Point(x + lenght / 12, y - HFundament - HB, z + 2 * weight / 3 + WB));

             //Крыша - Выступ 
             Roof.Add(new Point(x + lenght / 24, y - HFundament - HB, z + 7 * weight / 12));
             Roof.Add(new Point(x + lenght / 24 + LR, y - HFundament - HB, z + 7 * weight / 12));
             Roof.Add(new Point(x + lenght / 24 + LR, y - HFundament - HB, z + 2 * weight / 3));
             Roof.Add(new Point(x + lenght / 24, y - HFundament - HB, z + 2 * weight / 3));

             Roof.Add(new Point(x + lenght / 24, y - HFundament - HB - HR, z + 7 * weight / 12));
             Roof.Add(new Point(x + lenght / 24 + LR, y - HFundament - HB - HR, z + 7 * weight / 12));
             Roof.Add(new Point(x + lenght / 24 + LR, y - HFundament - HB - HR, z + 2 * weight / 3));
             Roof.Add(new Point(x + lenght / 24, y - HFundament - HB - HR, z + 2 * weight / 3));

             //Крыша - Над зданием
             RoofB.Add(new Point(x + lenght / 12, y - HFundament - HB, z + 2 * weight / 3));
             RoofB.Add(new Point(x + lenght / 12 + LB, y - HFundament - HB, z + 2 * weight / 3));
             RoofB.Add(new Point(x + lenght / 12 + LB, y - HFundament - HB, z + 2 * weight / 3 + WB));
             RoofB.Add(new Point(x + lenght / 12, y - HFundament - HB, z + 2 * weight / 3 + WB));

             RoofB.Add(new Point(x + lenght / 12, y - HFundament - HB - HR, z + 2 * weight / 3));
             RoofB.Add(new Point(x + lenght / 12 + LB, y - HFundament - HB - HR, z + 2 * weight / 3));
             RoofB.Add(new Point(x + lenght / 12 + LB, y - HFundament - HB - HR, z + 2 * weight / 3 + WB));
             RoofB.Add(new Point(x + lenght / 12, y - HFundament - HB - HR, z + 2 * weight / 3 + WB));

             //Крыша - Полукруг 
             Roof2.Add(new Point(x + lenght / 24, y - HFundament - HB, z ));
             Roof2.Add(new Point(x + lenght / 24 + LR, y - HFundament - HB, z ));
             Roof2.Add(new Point(x + lenght / 24 + LR, y - HFundament - HB, z + 7 * weight / 12));
             Roof2.Add(new Point(x + lenght / 24, y - HFundament - HB, z + 7 * weight / 12));

             Roof2.Add(new Point(x + lenght / 24, y - HFundament - HB - HR, z ));
             Roof2.Add(new Point(x + lenght / 24 + LR, y - HFundament - HB - HR, z ));
             Roof2.Add(new Point(x + lenght / 24 + LR, y - HFundament - HB - HR, z + 7 * weight / 12));
             Roof2.Add(new Point(x + lenght / 24, y - HFundament - HB - HR, z + 7 * weight / 12));

             //Дверь
             Door.Add(new Point(x + lenght / 4 + lenght / 8, y - HFundament, z + 7 * weight / 12));
             Door.Add(new Point(x + lenght / 4 + lenght / 8 + LD, y - HFundament, z + 7 * weight / 12));
             Door.Add(new Point(x + lenght / 4 + lenght / 8 + LD, y - HFundament, z + 2 * weight / 3 ));
             Door.Add(new Point(x + lenght / 4 + lenght / 8, y - HFundament, z + 2 * weight / 3 ));

             Door.Add(new Point(x + lenght / 4 + lenght / 8, y - HFundament - HD, z + 7 * weight / 12));
             Door.Add(new Point(x + lenght / 4 + lenght / 8 + LD, y - HFundament - HD, z + 7 * weight / 12));
             Door.Add(new Point(x + lenght / 4 + lenght / 8 + LD, y - HFundament - HD, z + 2 * weight / 3 ));
             Door.Add(new Point(x + lenght / 4 + lenght / 8, y - HFundament - HD, z + 2 * weight / 3 ));
             //Верхушка двери 
             DoorTop.Add(new Point(x + lenght / 4 + lenght / 8, y - HFundament - HD, z + 7 * weight / 12));
             DoorTop.Add(new Point(x + lenght / 4 + lenght / 8 + LD, y - HFundament - HD, z + 7 * weight / 12));
             DoorTop.Add(new Point(x + lenght / 4 + lenght / 8 + LD, y - HFundament - HD, z + 2 * weight / 3));
             DoorTop.Add(new Point(x + lenght / 4 + lenght / 8, y - HFundament - HD, z + 2 * weight / 3));

             DoorTop.Add(new Point(x + lenght / 4 + lenght / 8, y - HFundament - HD - HDT, z + 7 * weight / 12));
             DoorTop.Add(new Point(x + lenght / 4 + lenght / 8 + LD, y - HFundament - HD - HDT, z + 7 * weight / 12));
             DoorTop.Add(new Point(x + lenght / 4 + lenght / 8 + LD, y - HFundament - HD - HDT, z + 2 * weight / 3));
             DoorTop.Add(new Point(x + lenght / 4 + lenght / 8, y - HFundament - HD - HDT, z + 2 * weight / 3));

              // Фонарь 
             //Низ
             LampDown.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4, y - HFundament - HD - HDT - HL / 2, z + 7 * weight / 12));
             LampDown.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4 + LF, y - HFundament - HD - HDT - HL / 2, z + 7 * weight / 12));
             LampDown.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4 + LF, y - HFundament - HD - HDT, z + 7 * weight / 12));
             LampDown.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4, y - HFundament - HD - HDT, z + 7 * weight / 12));

             LampDown.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4, y - HFundament - HD - HDT - HL / 2, z + 2 * weight / 3));
             LampDown.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4 + LF, y - HFundament - HD - HDT - HL / 2, z + 2 * weight / 3));
             LampDown.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4 + LF, y - HFundament - HD - HDT, z + 2 * weight / 3));
             LampDown.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4, y - HFundament - HD - HDT, z + 2 * weight / 3));

             //Верх 
             LampUp.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4, y - HFundament - HD - HDT - HL / 2, z + 7 * weight / 12));
             LampUp.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4 + LF, y - HFundament - HD - HDT - HL / 2, z + 7 * weight / 12));
             LampUp.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4 + LF, y - HFundament - HD - HDT - HL, z + 7 * weight / 12));
             LampUp.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4, y - HFundament - HD - HDT - HL, z + 7 * weight / 12));

             LampUp.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4, y - HFundament - HD - HDT - HL / 2, z + 2 * weight / 3));
             LampUp.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4 + LF, y - HFundament - HD - HDT - HL / 2, z + 2 * weight / 3));
             LampUp.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4 + LF, y - HFundament - HD - HDT - HL, z + 2 * weight / 3));
             LampUp.Add(new Point(x + lenght / 4 + lenght / 8 + LD / 4, y - HFundament - HD - HDT - HL, z + 2 * weight / 3));

             //Коломны
             //1
             Column1.Add(new Point(x + LFundament / 10, y - HFundament, z + WL / 2 - WColumns / 2));
             Column1.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament, z + WL / 2 - WColumns / 2));
             Column1.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament, z + WL / 2 + WColumns / 2));
             Column1.Add(new Point(x + LFundament / 10, y - HFundament, z + WL / 2 + WColumns / 2));

             Column1.Add(new Point(x + LFundament / 10, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             Column1.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             Column1.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             Column1.Add(new Point(x + LFundament / 10, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));

             //2
             //Column2.Add(new Point(x + WColumns + (LB - LColumns), y - HFundament, 0));
             Column2.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament, z + WL / 2 - WColumns / 2));
             Column2.Add(new Point(x + 9 * LFundament / 10 , y - HFundament, z + WL / 2 - WColumns / 2));
             Column2.Add(new Point(x + 9 * LFundament / 10 , y - HFundament, z + WL / 2 + WColumns / 2));
             Column2.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament, z + WL / 2 + WColumns / 2));

             Column2.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             Column2.Add(new Point(x + 9 * LFundament / 10, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             Column2.Add(new Point(x + 9 * LFundament / 10, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             Column2.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));

             //Округления 
             //1-1
             CE11.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament , z + WL / 2 - WColumns / 2));
             CE11.Add(new Point(x + LFundament / 10, y - HFundament, z + WL / 2 - WColumns / 2));
             CE11.Add(new Point(x + LFundament / 10, y - HFundament , z + WL / 2 - WColumns / 2 - Otstup));
             CE11.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament , z + WL / 2 - WColumns / 2 - Otstup));

             CE11.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE11.Add(new Point(x + LFundament / 10, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE11.Add(new Point(x + LFundament / 10, y - HFundament - HColumns, z + WL / 2 - WColumns / 2 - Otstup));
             CE11.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament - HColumns, z + WL / 2 - WColumns / 2 - Otstup));
             //2-1
             CE21.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament, z + WL / 2 - WColumns / 2));
             CE21.Add(new Point(x + 9 * LFundament / 10, y - HFundament, z + WL / 2 - WColumns / 2));
             CE21.Add(new Point(x + 9 * LFundament / 10, y - HFundament, z + WL / 2 - WColumns / 2 - Otstup));
             CE21.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament, z + WL / 2 - WColumns / 2 - Otstup));

             CE21.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE21.Add(new Point(x + 9 * LFundament / 10, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE21.Add(new Point(x + 9 * LFundament / 10, y - HFundament - HColumns, z + WL / 2 - WColumns / 2 - Otstup));
             CE21.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament - HColumns, z + WL / 2 - WColumns / 2 - Otstup));
             //1-2
             CE12.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament, z + WL / 2 - WColumns / 2));
             CE12.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament, z + WL / 2 + WColumns / 2));
             CE12.Add(new Point(x + LFundament / 10 + LColumns + Otstup, y - HFundament, z + WL / 2 + WColumns / 2));
             CE12.Add(new Point(x + LFundament / 10 + LColumns + Otstup, y - HFundament, z + WL / 2 - WColumns / 2));

             CE12.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE12.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             CE12.Add(new Point(x + LFundament / 10 + LColumns + Otstup, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             CE12.Add(new Point(x + LFundament / 10 + LColumns + Otstup, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             //2-2
             CE22.Add(new Point(x + 9 * LFundament / 10, y - HFundament, z + WL / 2 + WColumns / 2));
             CE22.Add(new Point(x + 9 * LFundament / 10, y - HFundament, z + WL / 2 - WColumns / 2));
             CE22.Add(new Point(x + 9 * LFundament / 10 + Otstup, y - HFundament, z + WL / 2 - WColumns / 2));
             CE22.Add(new Point(x + 9 * LFundament / 10 + Otstup, y - HFundament, z + WL / 2 + WColumns / 2));
             
             CE22.Add(new Point(x + 9 * LFundament / 10, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             CE22.Add(new Point(x + 9 * LFundament / 10, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE22.Add(new Point(x + 9 * LFundament / 10 + Otstup, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE22.Add(new Point(x + 9 * LFundament / 10 + Otstup, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             //1-3
             CE13.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament, z + WL / 2 + WColumns / 2));
             CE13.Add(new Point(x + LFundament / 10, y - HFundament, z + WL / 2 + WColumns / 2));
             CE13.Add(new Point(x + LFundament / 10, y - HFundament, z + WL / 2 + WColumns / 2 + Otstup));
             CE13.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament, z + WL / 2 + WColumns / 2 + Otstup));

             CE13.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             CE13.Add(new Point(x + LFundament / 10, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             CE13.Add(new Point(x + LFundament / 10, y - HFundament - HColumns, z + WL / 2 + WColumns / 2 + Otstup));
             CE13.Add(new Point(x + LFundament / 10 + LColumns, y - HFundament - HColumns, z + WL / 2 + WColumns / 2 + Otstup));
             //2-3
             CE23.Add(new Point(x + 9 * LFundament / 10, y - HFundament, z + WL / 2 + WColumns / 2 ));
             CE23.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament, z + WL / 2 + WColumns / 2));
             CE23.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament, z + WL / 2 + WColumns / 2 + Otstup));
             CE23.Add(new Point(x + 9 * LFundament / 10, y - HFundament, z + WL / 2 + WColumns / 2 + Otstup));

             CE23.Add(new Point(x + 9 * LFundament / 10, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             CE23.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             CE23.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament - HColumns, z + WL / 2 + WColumns / 2 + Otstup));
             CE23.Add(new Point(x + 9 * LFundament / 10, y - HFundament - HColumns, z + WL / 2 + WColumns / 2 + Otstup));
             //1-4
             CE14.Add(new Point(x + LFundament / 10, y - HFundament, z + WL / 2 + WColumns / 2));
             CE14.Add(new Point(x + LFundament / 10, y - HFundament, z + WL / 2 - WColumns / 2));
             CE14.Add(new Point(x + LFundament / 10 - Otstup, y - HFundament, z + WL / 2 - WColumns / 2));
             CE14.Add(new Point(x + LFundament / 10 - Otstup, y - HFundament, z + WL / 2 + WColumns / 2));

             CE14.Add(new Point(x + LFundament / 10, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             CE14.Add(new Point(x + LFundament / 10, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE14.Add(new Point(x + LFundament / 10 - Otstup, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE14.Add(new Point(x + LFundament / 10 - Otstup, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             //2-4
             CE24.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament, z + WL / 2 + WColumns / 2));
             CE24.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament, z + WL / 2 - WColumns / 2));
             CE24.Add(new Point(x + 9 * LFundament / 10 - LColumns - Otstup, y - HFundament, z + WL / 2 - WColumns / 2));
             CE24.Add(new Point(x + 9 * LFundament / 10 - LColumns - Otstup, y - HFundament, z + WL / 2 + WColumns / 2));

             CE24.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));
             CE24.Add(new Point(x + 9 * LFundament / 10 - LColumns, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE24.Add(new Point(x + 9 * LFundament / 10 - LColumns - Otstup, y - HFundament - HColumns, z + WL / 2 - WColumns / 2));
             CE24.Add(new Point(x + 9 * LFundament / 10 - LColumns - Otstup, y - HFundament - HColumns, z + WL / 2 + WColumns / 2));

             // Занесем в общий список 
             All.Add(Fundament); //0
             All.Add(Ladder);//1
             All.Add(Building);//2
             All.Add(Roof);//3
             All.Add(Roof);//4
             All.Add(Roof2);//5
             All.Add(Door);//6
             All.Add(DoorTop);//7
             All.Add(Column1);//8
             All.Add(Column2);//9

             All.Add(CE11);//10
             All.Add(CE21);//11
             All.Add(CE12);//12
             All.Add(CE22);//13
             All.Add(CE13);//14
             All.Add(CE23);//15
             All.Add(CE14);//16
             All.Add(CE24);//17

             All.Add(LampDown); //18
             All.Add(LampUp); //19

             // In 3D
             Double a = Convert.ToDouble(textBox1.Text);
             Double a2 = Convert.ToDouble(textBox2.Text);
             List<Point> F3D = new List<Point>();
             List<Point> L3D = new List<Point>();
             List<Point> B3D = new List<Point>();
             List<Point> R3D = new List<Point>();
             List<Point> R3D2 = new List<Point>();
             List<Point> R3D3 = new List<Point>();
             List<Point> D3D = new List<Point>(); 
             List<Point> DT3D = new List<Point>();
             List<Point> C13D = new List<Point>();
             List<Point> C23D = new List<Point>();

             List<Point> CE113D = new List<Point>();
             List<Point> CE213D = new List<Point>();
             List<Point> CE123D = new List<Point>();
             List<Point> CE223D = new List<Point>();
             List<Point> CE133D = new List<Point>();
             List<Point> CE233D = new List<Point>();
             List<Point> CE143D = new List<Point>();
             List<Point> CE243D = new List<Point>();

             List<Point> FLUp3D = new List<Point>();
             List<Point> FLDown3D = new List<Point>();

             for (int i = 0; i < Fundament.Count; i++)
             {
                 F3D.Add(convert_3D_in_2D_Point(Fundament[i], a, a2));
             }
             for (int i = 0; i < Ladder.Count; i++)
             {
                 L3D.Add(convert_3D_in_2D_Point(Ladder[i], a, a2));
             }
             for (int i = 0; i < Building.Count; i++)
             {
                 B3D.Add(convert_3D_in_2D_Point(Building[i], a, a2));
             }
             for (int i = 0; i < Roof.Count; i++)
             {
                 R3D.Add(convert_3D_in_2D_Point(Roof[i], a, a2));
                 R3D2.Add(convert_3D_in_2D_Point(RoofB[i], a, a2));
             }
             for (int i = 0; i < Roof2.Count; i++)
             {
                 R3D3.Add(convert_3D_in_2D_Point(Roof2[i], a, a2));
             }
             for (int i = 0; i < Door.Count; i++)
             {
                 D3D.Add(convert_3D_in_2D_Point(Door[i], a, a2));
             }
             for (int i = 0; i < DoorTop.Count; i++)
             {
                 DT3D.Add(convert_3D_in_2D_Point(DoorTop[i], a, a2));
             }
             for (int i = 0; i < Column1.Count; i++)
             {
                 C13D.Add(convert_3D_in_2D_Point(Column1[i], a, a2));
                 C23D.Add(convert_3D_in_2D_Point(Column2[i], a, a2));
             }
             for (int i = 0; i < CE11.Count; i++)
             {
                 CE113D.Add(convert_3D_in_2D_Point(CE11[i], a, a2));
                 CE213D.Add(convert_3D_in_2D_Point(CE21[i], a, a2));

                 CE123D.Add(convert_3D_in_2D_Point(CE12[i], a, a2));
                 CE223D.Add(convert_3D_in_2D_Point(CE22[i], a, a2));

                 CE133D.Add(convert_3D_in_2D_Point(CE13[i], a, a2));
                 CE233D.Add(convert_3D_in_2D_Point(CE23[i], a, a2));

                 CE143D.Add(convert_3D_in_2D_Point(CE14[i], a, a2));
                 CE243D.Add(convert_3D_in_2D_Point(CE24[i], a, a2));
             }
             for (int i = 0; i < LampUp.Count; i++)
             {
                 FLUp3D.Add(convert_3D_in_2D_Point(LampUp[i], a, a2));
                 FLDown3D.Add(convert_3D_in_2D_Point(LampDown[i], a, a2));
             }

             List<List<Point>> All3D = new List<List<Point>>();
             All3D.Add(F3D);
             All3D.Add(L3D);
             All3D.Add(B3D);
             All3D.Add(R3D);
             All3D.Add(R3D2);
             All3D.Add(R3D3);
             All3D.Add(D3D);
             All3D.Add(DT3D);
             All3D.Add(C13D);
             All3D.Add(C23D);
             All3D.Add(CE113D);
             All3D.Add(CE213D);
             All3D.Add(CE123D);
             All3D.Add(CE223D);
             All3D.Add(CE133D);
             All3D.Add(CE233D);
             All3D.Add(CE143D);
             All3D.Add(CE243D);
             All3D.Add(FLUp3D);
             All3D.Add(FLDown3D);

             //Отрисовка
             Draw(g, "y", All3D);
             
         }

         public void Draw(Graphics g, String coord, List<List<Point>> AT)
         {
             DrawLinesFundament(AT[0], g, coord);
             DrawLinesLadder(AT[1], g, coord);
             DrawRectangle(AT[2], g, coord);
             DrawRectangle(AT[3], g, coord);
             DrawRectangle(AT[4], g, coord);
             DrawRoof2(AT[5], g, coord);
             DrawRectangle(AT[6], g, coord);
             DrawDoorTop(AT[6], AT[7], g, coord);

             if (coord != "z")
             {
                 DrawRectangle(AT[8], g, coord);
                 DrawRectangle(AT[9], g, coord);
             }
             
             DrawRoundingColumns(AT[8], 0, 1, AT[10], g, coord);
             DrawRoundingColumns(AT[9], 0, 1, AT[11], g, coord);
             DrawRoundingColumns(AT[8], 1, 2, AT[12], g, coord);
             DrawRoundingColumns(AT[9], 1, 2, AT[13], g, coord);
             DrawRoundingColumns(AT[8], 2, 3, AT[14], g, coord);
             DrawRoundingColumns(AT[9], 2, 3, AT[15], g, coord);
             DrawRoundingColumns(AT[8], 3, 0, AT[16], g, coord);
             DrawRoundingColumns(AT[9], 3, 0, AT[17], g, coord);
              
             DrawFlashlight(AT[18], g, coord);
             DrawFlashlight(AT[19], g, coord);
         }

         public void Draw2D_XY(int lenght, int height, int weight, Graphics g)
         {

             for (int i = 0; i < All.Count; i++)
             {
                 Draw(g, "y", All);
             }
         }

         public void Draw2D_XZ(int lenght, int height, int weight, Graphics g)
         {

             for (int i = 0; i < All.Count; i++)
             {
                 Draw(g, "z", All);
             }
         }

         public void DrawLines(Point T1, Point T2, Graphics g, String cootdinat)
         {
             
             if (cootdinat == "y")
             {
                 g.DrawLine(Pens.Black, (int)T1.x, (int)T1.y, (int)T2.x, (int)T2.y);
             }
             if (cootdinat == "z")
             {
                 g.DrawLine(Pens.Black, (int)T1.x, (int)T1.z, (int)T2.x, (int)T2.z);
             }
         }

         public void DrawRectangle(List<Point> B, Graphics g, String coord)
         {
             for (int i = 0; i < 3; i++)
             {
                 DrawLines(B[i], B[i + 1], g, coord);
                 DrawLines(B[i + 4], B[i + 5], g, coord);
                 DrawLines(B[i], B[i + 4], g, coord);
             }

             DrawLines(B[0], B[3], g, coord);
             DrawLines(B[4], B[7], g, coord);
             DrawLines(B[3], B[7], g, coord);
         }

         public void DrawLinesFundament(List<Point> Fundament, Graphics g, String c)
         {
             if (c == "y")
             {
                 g.DrawLine(Pens.Black, Fundament[0].x, Fundament[0].y, Fundament[1].x, Fundament[1].y);
                 g.DrawLine(Pens.Black, Fundament[1].x, Fundament[1].y, Fundament[2].x, Fundament[2].y);
                 g.DrawLine(Pens.Black, Fundament[2].x, Fundament[2].y, Fundament[3].x, Fundament[3].y);
                 g.DrawLine(Pens.Black, Fundament[3].x, Fundament[3].y, Fundament[0].x, Fundament[0].y);

                 g.DrawLine(Pens.Black, Fundament[4].x, Fundament[4].y, Fundament[5].x, Fundament[5].y);
                 g.DrawLine(Pens.Black, Fundament[5].x, Fundament[5].y, Fundament[6].x, Fundament[6].y);
                 g.DrawLine(Pens.Black, Fundament[6].x, Fundament[6].y, Fundament[7].x, Fundament[7].y);
                 g.DrawLine(Pens.Black, Fundament[7].x, Fundament[7].y, Fundament[4].x, Fundament[4].y);

                 g.DrawLine(Pens.Black, Fundament[4].x, Fundament[4].y, Fundament[0].x, Fundament[0].y);
                 g.DrawLine(Pens.Black, Fundament[5].x, Fundament[5].y, Fundament[1].x, Fundament[1].y);
                 g.DrawLine(Pens.Black, Fundament[6].x, Fundament[6].y, Fundament[2].x, Fundament[2].y);
                 g.DrawLine(Pens.Black, Fundament[7].x, Fundament[7].y, Fundament[3].x, Fundament[3].y);

                 g.DrawLine(Pens.White, Fundament[0].x, Fundament[0].y, Fundament[1].x, Fundament[1].y);
                 g.DrawLine(Pens.White, Fundament[4].x, Fundament[4].y, Fundament[5].x, Fundament[5].y);

                 g.DrawLine(Pens.Black, Fundament[4].x, Fundament[4].y, Fundament[8].x, Fundament[8].y);
                 g.DrawLine(Pens.Black, Fundament[8].x, Fundament[8].y, Fundament[9].x, Fundament[9].y);
                 g.DrawLine(Pens.Black, Fundament[9].x, Fundament[9].y, Fundament[10].x, Fundament[10].y);
                 g.DrawLine(Pens.Black, Fundament[10].x, Fundament[10].y, Fundament[11].x, Fundament[11].y);
                 g.DrawLine(Pens.Black, Fundament[11].x, Fundament[11].y, Fundament[5].x, Fundament[5].y);

                 g.DrawLine(Pens.Black, Fundament[0].x, Fundament[0].y, Fundament[12].x, Fundament[12].y);
                 g.DrawLine(Pens.Black, Fundament[12].x, Fundament[12].y, Fundament[13].x, Fundament[13].y);
                 g.DrawLine(Pens.Black, Fundament[13].x, Fundament[13].y, Fundament[14].x, Fundament[14].y);
                 g.DrawLine(Pens.Black, Fundament[14].x, Fundament[14].y, Fundament[15].x, Fundament[15].y);
                 g.DrawLine(Pens.Black, Fundament[15].x, Fundament[15].y, Fundament[1].x, Fundament[1].y);

                 g.DrawLine(Pens.Black, Fundament[12].x, Fundament[12].y, Fundament[8].x, Fundament[8].y);
                 g.DrawLine(Pens.Black, Fundament[11].x, Fundament[11].y, Fundament[15].x, Fundament[15].y);
                 g.DrawLine(Pens.Black, Fundament[13].x, Fundament[13].y, Fundament[9].x, Fundament[9].y);
                 g.DrawLine(Pens.Black, Fundament[14].x, Fundament[14].y, Fundament[10].x, Fundament[10].y);
             }
             if (c == "z")
             {
                 for (int i = 0; i < 3; i++)
                 {
                     g.DrawLine(Pens.Black, Fundament[i].x, Fundament[i].z, Fundament[i + 1].x, Fundament[i + 1].z);
                     g.DrawLine(Pens.Black, Fundament[i + 4].x, Fundament[i + 4].z, Fundament[i + 5].x, Fundament[i + 5].z);
                     g.DrawLine(Pens.Black, Fundament[i + 4].x, Fundament[i + 4].z, Fundament[i].x, Fundament[i].z);
                 }
                 g.DrawLine(Pens.Black, Fundament[0].x, Fundament[0].z, Fundament[3].x, Fundament[3].z);
                 g.DrawLine(Pens.Black, Fundament[7].x, Fundament[7].z, Fundament[4].x, Fundament[4].z);
                 g.DrawLine(Pens.Black, Fundament[3].x, Fundament[3].z, Fundament[7].x, Fundament[7].z);

                 g.DrawLine(Pens.White, Fundament[0].x, Fundament[0].z, Fundament[1].x, Fundament[1].z);
                 g.DrawLine(Pens.White, Fundament[4].x, Fundament[4].z, Fundament[5].x, Fundament[5].z);

                 g.DrawLine(Pens.Black, Fundament[4].x, Fundament[4].z, Fundament[8].x, Fundament[8].z);
                 g.DrawLine(Pens.Black, Fundament[8].x, Fundament[8].z, Fundament[9].x, Fundament[9].z);
                 g.DrawLine(Pens.Black, Fundament[9].x, Fundament[9].z, Fundament[10].x, Fundament[10].z);
                 g.DrawLine(Pens.Black, Fundament[10].x, Fundament[10].z, Fundament[11].x, Fundament[11].z);
                 g.DrawLine(Pens.Black, Fundament[11].x, Fundament[11].z, Fundament[5].x, Fundament[5].z);

                 g.DrawLine(Pens.Black, Fundament[0].x, Fundament[0].z, Fundament[12].x, Fundament[12].z);
                 g.DrawLine(Pens.Black, Fundament[12].x, Fundament[12].z, Fundament[13].x, Fundament[13].z);
                 g.DrawLine(Pens.Black, Fundament[13].x, Fundament[13].z, Fundament[14].x, Fundament[14].z);
                 g.DrawLine(Pens.Black, Fundament[14].x, Fundament[14].z, Fundament[15].x, Fundament[15].z);
                 g.DrawLine(Pens.Black, Fundament[15].x, Fundament[15].z, Fundament[1].x, Fundament[1].z);

                 g.DrawLine(Pens.Black, Fundament[12].x, Fundament[12].z, Fundament[8].x, Fundament[8].z);
                 g.DrawLine(Pens.Black, Fundament[11].x, Fundament[11].z, Fundament[15].x, Fundament[15].z);
                 g.DrawLine(Pens.Black, Fundament[13].x, Fundament[13].z, Fundament[9].x, Fundament[9].z);
                 g.DrawLine(Pens.Black, Fundament[14].x, Fundament[14].z, Fundament[10].x, Fundament[10].z);
             }
         }

         public void DrawLinesLadder(List<Point> Ladder, Graphics g, String c)
         {
             if (c == "y")
             {
                 //Нижняя  ступень 

                 g.DrawLine(Pens.Black, Ladder[0].x, Ladder[0].y, Ladder[1].x, Ladder[1].y);
                 g.DrawLine(Pens.Black, Ladder[1].x, Ladder[1].y, Ladder[2].x, Ladder[2].y);
                 g.DrawLine(Pens.Black, Ladder[2].x, Ladder[2].y, Ladder[3].x, Ladder[3].y);
                 g.DrawLine(Pens.Black, Ladder[3].x, Ladder[3].y, Ladder[0].x, Ladder[0].y);


                 for (int i = 4; i < 7; i++)
                 {
                     g.DrawLine(Pens.Black, Ladder[i].x, Ladder[i].y, Ladder[i + 1].x, Ladder[i + 1].y);
                     g.DrawLine(Pens.Black, Ladder[i].x, Ladder[i].y, Ladder[i - 4].x, Ladder[i - 4].y);
                 }

                 g.DrawLine(Pens.Black, Ladder[4].x, Ladder[4].y, Ladder[7].x, Ladder[7].y);
                 g.DrawLine(Pens.Black, Ladder[7].x, Ladder[7].y, Ladder[3].x, Ladder[3].y);

                 //Вторая ступень
                 for (int i = 8; i < 11; i++)
                 {
                     g.DrawLine(Pens.Black, Ladder[i].x, Ladder[i].y, Ladder[i + 1].x, Ladder[i + 1].y);
                     g.DrawLine(Pens.Black, Ladder[i + 4].x, Ladder[i + 4].y, Ladder[i + 5].x, Ladder[i + 5].y);
                     g.DrawLine(Pens.Black, Ladder[i].x, Ladder[i].y, Ladder[i + 4].x, Ladder[i + 4].y);
                 }
                 g.DrawLine(Pens.Black, Ladder[8].x, Ladder[8].y, Ladder[11].x, Ladder[11].y);
                 g.DrawLine(Pens.Black, Ladder[12].x, Ladder[12].y, Ladder[15].x, Ladder[15].y);
                 g.DrawLine(Pens.Black, Ladder[10].x, Ladder[10].y, Ladder[14].x, Ladder[14].y);

                 //Третья недоступень 

                 g.DrawLine(Pens.Black, Ladder[16].x, Ladder[16].y, Ladder[17].x, Ladder[17].y);
                 g.DrawLine(Pens.Black, Ladder[18].x, Ladder[18].y, Ladder[19].x, Ladder[19].y);
                 g.DrawLine(Pens.Black, Ladder[16].x, Ladder[16].y, Ladder[18].x, Ladder[18].y);
                 g.DrawLine(Pens.Black, Ladder[17].x, Ladder[17].y, Ladder[19].x, Ladder[19].y);
             }

             if (c == "z")
             {
                 //Нижняя  ступень 

                 g.DrawLine(Pens.Black, Ladder[0].x, Ladder[0].z, Ladder[1].x, Ladder[1].z);
                 g.DrawLine(Pens.Black, Ladder[1].x, Ladder[1].z, Ladder[2].x, Ladder[2].z);
                 g.DrawLine(Pens.Black, Ladder[2].x, Ladder[2].z, Ladder[3].x, Ladder[3].z);
                 g.DrawLine(Pens.Black, Ladder[3].x, Ladder[3].z, Ladder[0].x, Ladder[0].z);


                 for (int i = 4; i < 7; i++)
                 {
                     g.DrawLine(Pens.Black, Ladder[i].x, Ladder[i].z, Ladder[i + 1].x, Ladder[i + 1].z);
                     g.DrawLine(Pens.Black, Ladder[i].x, Ladder[i].z, Ladder[i - 4].x, Ladder[i - 4].z);
                 }

                 g.DrawLine(Pens.Black, Ladder[4].x, Ladder[4].z, Ladder[7].x, Ladder[7].z);
                 g.DrawLine(Pens.Black, Ladder[7].x, Ladder[7].z, Ladder[3].x, Ladder[3].z);

                 //Вторая ступень
                 for (int i = 8; i < 11; i++)
                 {
                     g.DrawLine(Pens.Black, Ladder[i].x, Ladder[i].z, Ladder[i + 1].x, Ladder[i + 1].z);
                     g.DrawLine(Pens.Black, Ladder[i + 4].x, Ladder[i + 4].z, Ladder[i + 5].x, Ladder[i + 5].z);
                     g.DrawLine(Pens.Black, Ladder[i].x, Ladder[i].z, Ladder[i + 4].x, Ladder[i + 4].z);
                 }
                 g.DrawLine(Pens.Black, Ladder[8].x, Ladder[8].z, Ladder[11].x, Ladder[11].z);
                 g.DrawLine(Pens.Black, Ladder[12].x, Ladder[12].z, Ladder[15].x, Ladder[15].z);
                 g.DrawLine(Pens.Black, Ladder[10].x, Ladder[10].z, Ladder[14].x, Ladder[14].z);

                 //Третья недоступень 

                 g.DrawLine(Pens.Black, Ladder[16].x, Ladder[16].z, Ladder[17].x, Ladder[17].z);
                 g.DrawLine(Pens.Black, Ladder[18].x, Ladder[18].z, Ladder[19].x, Ladder[19].z);
                 g.DrawLine(Pens.Black, Ladder[16].x, Ladder[16].z, Ladder[18].x, Ladder[18].z);
                 g.DrawLine(Pens.Black, Ladder[17].x, Ladder[17].z, Ladder[19].x, Ladder[19].z);
             }
         }

         public void DrawRoof2(List<Point> R3D3, Graphics g, String coord)
         {
             if (coord == "y")
             {
                 PointF p1 = new PointF(R3D3[0].x, R3D3[0].y);
                 PointF p2 = new PointF(R3D3[1].x, R3D3[1].y);
                 PointF p3 = new PointF(R3D3[2].x, R3D3[2].y);
                 PointF p4 = new PointF(R3D3[3].x, R3D3[3].y);

                 PointF p21 = new PointF(R3D3[4].x, R3D3[4].y);
                 PointF p22 = new PointF(R3D3[5].x, R3D3[5].y);
                 PointF p23 = new PointF(R3D3[6].x, R3D3[6].y);
                 PointF p24 = new PointF(R3D3[7].x, R3D3[7].y);

                 g.DrawBezier(Pens.Black, p4, p1, p2, p3);
                 g.DrawBezier(Pens.Black, p24, p21, p22, p23);
             }
             if (coord == "z")
             {
                 PointF p1 = new PointF(R3D3[0].x, R3D3[0].z);
                 PointF p2 = new PointF(R3D3[1].x, R3D3[1].z);
                 PointF p3 = new PointF(R3D3[2].x, R3D3[2].z);
                 PointF p4 = new PointF(R3D3[3].x, R3D3[3].z);

                 PointF p21 = new PointF(R3D3[4].x, R3D3[4].z);
                 PointF p22 = new PointF(R3D3[5].x, R3D3[5].z);
                 PointF p23 = new PointF(R3D3[6].x, R3D3[6].z);
                 PointF p24 = new PointF(R3D3[7].x, R3D3[7].z);

                 g.DrawBezier(Pens.Black, p4, p1, p2, p3);
                 g.DrawBezier(Pens.Black, p24, p21, p22, p23);
             }
             
         }

         public void DrawDoorTop(List<Point> Door, List<Point> R3D3, Graphics g, String coord)
         {
             
             if (coord == "y")
             {
                 g.DrawLine(Pens.White, Door[4].x, Door[4].y, Door[5].x, Door[5].y);
                 g.DrawLine(Pens.White, Door[6].x, Door[6].y, Door[7].x, Door[7].y);

                 PointF p1 = new PointF(R3D3[0].x, R3D3[0].y);
                 PointF p2 = new PointF(R3D3[1].x, R3D3[1].y);
                 PointF p3 = new PointF(R3D3[2].x, R3D3[2].y);
                 PointF p4 = new PointF(R3D3[3].x, R3D3[3].y);

                 PointF p21 = new PointF(R3D3[4].x, R3D3[4].y);
                 PointF p22 = new PointF(R3D3[5].x, R3D3[5].y);
                 PointF p23 = new PointF(R3D3[6].x, R3D3[6].y);
                 PointF p24 = new PointF(R3D3[7].x, R3D3[7].y);

                 g.DrawBezier(Pens.Black, p1, p21, p22, p2);
                 g.DrawBezier(Pens.Black, p4, p24, p23, p3);
             }
             if (coord == "z")
             {
                 g.DrawLine(Pens.White, Door[4].x, Door[4].z, Door[5].x, Door[5].z);
                 g.DrawLine(Pens.White, Door[6].x, Door[6].z, Door[7].x, Door[7].z);

                 PointF p1 = new PointF(R3D3[0].x, R3D3[0].z);
                 PointF p2 = new PointF(R3D3[1].x, R3D3[1].z);
                 PointF p3 = new PointF(R3D3[2].x, R3D3[2].z);
                 PointF p4 = new PointF(R3D3[3].x, R3D3[3].z);

                 PointF p21 = new PointF(R3D3[4].x, R3D3[4].z);
                 PointF p22 = new PointF(R3D3[5].x, R3D3[5].z);
                 PointF p23 = new PointF(R3D3[6].x, R3D3[6].z);
                 PointF p24 = new PointF(R3D3[7].x, R3D3[7].z);

                 g.DrawBezier(Pens.Black, p1, p21, p22, p2);
                 g.DrawBezier(Pens.Black, p4, p24, p23, p3);
             }

         }

         public void DrawRoundingColumns(List<Point> Column, int i, int j,  List<Point> R3D3, Graphics g, String coord)
         {

             if (coord == "y")
             {
                 g.DrawLine(Pens.White, Column[i].x, Column[i].y, Column[j].x, Column[j].y);
                 g.DrawLine(Pens.White, Column[i + 4].x, Column[i + 4].y, Column[j + 4].x, Column[j + 4].y);

                 PointF p1 = new PointF(R3D3[0].x, R3D3[0].y);
                 PointF p2 = new PointF(R3D3[1].x, R3D3[1].y);
                 PointF p3 = new PointF(R3D3[2].x, R3D3[2].y);
                 PointF p4 = new PointF(R3D3[3].x, R3D3[3].y);

                 PointF p21 = new PointF(R3D3[4].x, R3D3[4].y);
                 PointF p22 = new PointF(R3D3[5].x, R3D3[5].y);
                 PointF p23 = new PointF(R3D3[6].x, R3D3[6].y);
                 PointF p24 = new PointF(R3D3[7].x, R3D3[7].y);

                 g.DrawBezier(Pens.Black, p1, p4, p3, p2);
                 g.DrawBezier(Pens.Black, p21, p24, p23, p22);
             }
             if (coord == "z")
             {
                 g.DrawLine(Pens.White, Column[i].x, Column[i].z, Column[j].x, Column[j].z);
                 g.DrawLine(Pens.White, Column[i + 4].x, Column[i + 4].z, Column[j + 4].x, Column[j + 4].z);

                 PointF p1 = new PointF(R3D3[0].x, R3D3[0].z);
                 PointF p2 = new PointF(R3D3[1].x, R3D3[1].z);
                 PointF p3 = new PointF(R3D3[2].x, R3D3[2].z);
                 PointF p4 = new PointF(R3D3[3].x, R3D3[3].z);

                 PointF p21 = new PointF(R3D3[4].x, R3D3[4].z);
                 PointF p22 = new PointF(R3D3[5].x, R3D3[5].z);
                 PointF p23 = new PointF(R3D3[6].x, R3D3[6].z);
                 PointF p24 = new PointF(R3D3[7].x, R3D3[7].z);

                 g.DrawBezier(Pens.Black, p1, p4, p3, p2);
                 g.DrawBezier(Pens.Black, p21, p24, p23, p22);
             }

         }

         public void DrawFlashlight(List<Point> R3D3, Graphics g, String coord)
         {
             if (coord == "y")
             {
                 PointF p1 = new PointF(R3D3[0].x, R3D3[0].y);
                 PointF p2 = new PointF(R3D3[1].x, R3D3[1].y);
                 PointF p3 = new PointF(R3D3[2].x, R3D3[2].y);
                 PointF p4 = new PointF(R3D3[3].x, R3D3[3].y);

                 PointF p21 = new PointF(R3D3[4].x, R3D3[4].y);
                 PointF p22 = new PointF(R3D3[5].x, R3D3[5].y);
                 PointF p23 = new PointF(R3D3[6].x, R3D3[6].y);
                 PointF p24 = new PointF(R3D3[7].x, R3D3[7].y);

                 g.DrawBezier(Pens.Black, p1, p4, p3, p2);
                 g.DrawBezier(Pens.Black, p21, p24, p23, p22);
             }
             if (coord == "z")
             {
                 PointF p1 = new PointF(R3D3[0].x, R3D3[0].z);
                 PointF p2 = new PointF(R3D3[1].x, R3D3[1].z);
                 PointF p3 = new PointF(R3D3[2].x, R3D3[2].z);
                 PointF p4 = new PointF(R3D3[3].x, R3D3[3].z);

                 PointF p21 = new PointF(R3D3[4].x, R3D3[4].z);
                 PointF p22 = new PointF(R3D3[5].x, R3D3[5].z);
                 PointF p23 = new PointF(R3D3[6].x, R3D3[6].z);
                 PointF p24 = new PointF(R3D3[7].x, R3D3[7].z);

                 g.DrawBezier(Pens.Black, p1, p4, p3, p2);
                 g.DrawBezier(Pens.Black, p21, p24, p23, p22);
             }

         }

         // проекция 3D на 2D
         private Point convert_3D_in_2D_Point(Point t, double angel_OXZ, double angel_OXY)
         {
             double xo = pictureBox1.Width / 2; 
             double yo = pictureBox1.Height / 2;

             // проицируем
             double res_x = -t.z * Math.Sin(angel_OXZ) + t.x * Math.Cos(angel_OXZ) + xo;
             double res_y = -(t.z * Math.Cos(angel_OXZ) + t.x * Math.Sin(angel_OXZ)) * 
                 Math.Sin(angel_OXY) + t.y * Math.Cos(angel_OXY) + yo;

             return new Point((int)(res_x), (int)(res_y));
         }

       
    }
}
