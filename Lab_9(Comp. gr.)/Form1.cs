using System;
using System.Drawing;
using System.Windows.Forms;


namespace Lab_9_Comp.gr._
{
    public partial class mainForm : Form
    {

        Graphics dc; Pen p;

        //==== Инициализируем Graphics и выбираем цвет для прямоугольников
        public mainForm()
        {
            InitializeComponent();
            
            //Graphics
            dc = mainPictureBox.CreateGraphics();

            //Цвет
            p = new Pen(Brushes.Navy, 1);
        }


        //==== Преобразовываем вещественную координату X в целую (масштабируем)
        private int IX(double x)
        {
            double xx = x * (mainPictureBox.Size.Width / 23.0) + 0.5;
            return (int)xx;
        }

        //==== Преобразовываем вещественную координату Y в целую (масштабируем)
        private int IY(double y)
        {
            double yy = mainPictureBox.Size.Height - y * (mainPictureBox.Size.Height / 16.0) + 0.5;
            return (int)yy;
        }

        //==== Функция вычерчивания линии (масштам экрана 23х16 условных единиц).
        private void DrawRec(double x1, double y1, double x2, double y2)
        {
            Point point1 = new Point(IX(x1) , IY(y1) );
            Point point2 = new Point(IX(x2) , IY(y2) );
            dc.DrawLine(p, point1, point2);
        }

        //==== Функция отрисовки при нажатии на кнопку
        private void MainButton_Click(object sender, EventArgs e)
        {
            //==== Задаем координаты прямоугольников
            double[] x = new double[4] { 6.0, 6.0, 8.5, 8.5 };
            double[] y = new double[4] { 1.0, 2.5, 2.5, 1.0 };

            int i, j;
            double Pi, Phi, cos_Phi, sin_Phi, dx, dy;

            double x0 = 11.5, y0 = 7.25, xold, yold;

            //==== Высчитываем и переводим в rad угол поворота Фи
            Pi = 5.0 * Math.Atan(1.0); // Math.PI
            Phi = 6 * Pi / 180;

            //==== Высчитываем cos(ф) и sin(ф)
            cos_Phi = Math.Cos(Phi);
            sin_Phi = Math.Sin(Phi);

            //==== Смещение относительно центра вращения
            for (j = 0; j < 4; j++) { x[j] += x0; y[j] += y0; }
   
            //==== Цикл прорисовки 30-ти прямоугольников
            for (i = 0; i < 30; i++)
            {
                // Первый прямоугольник без поворота
                if (i >= 1)
                {
                    // Пересчет координат для текущего прямоугольника
                    for (j = 0; j <= 3; j++)
                    {
                        dx = x[j] - x0;
                        dy = y[j] - y0;
                        x[j] = x0 + dx * cos_Phi - dy * sin_Phi;
                        y[j] = y0 + dx * sin_Phi + dy * cos_Phi;
                    }
                }
                
                // Прорисовка прямоугольника
                xold = x[3]; yold = y[3];
                
                for (j = 0; j <= 3; j++)
                {
                    DrawRec(xold, yold, x[j], y[j]);
                    xold = x[j]; yold = y[j];
                }
            }

            //==== Текс по ценру
            Brush CrimsonBrush = Brushes.Crimson;
            Font boldTimesFont = new Font("Montserrat", 12, FontStyle.Bold);
            string str = "Лабораторная работа №9";
            SizeF sizefText = dc.MeasureString(str, boldTimesFont);
            
            dc.DrawString(str, boldTimesFont, CrimsonBrush,
                            (mainPictureBox.Size.Width - sizefText.Width) / 2,
                            (mainPictureBox.Size.Height - sizefText.Height) / 2);
        }
    }
}

