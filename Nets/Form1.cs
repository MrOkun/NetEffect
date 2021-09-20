using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nets
{
    public partial class Form1 : Form
    {
        public struct Point
        {
            public Point(float x, float y, float dx, float dy)
            {
                this.X = x;
                this.Y = y;
                this.Dx = dx;
                this.Dy = dy;
            }

            public float X { get; set; }
            public float Y { get; set; }
            public float Dx { get; set; }
            public float Dy { get; set; }
        }

        private Graphics Canvas;
        private Random rnd;
        private Point[] Points;

        private readonly int PointCount = 50;
        private readonly float FadeDist = 200;


        public Form1()
        {
            InitializeComponent();

            Start();
        }

        private void MakePoint()
        {
            for (int i = 0; i < PointCount; i++)
            {
                //float addSpeed = 3 + (float)rnd.NextDouble() + (float)rnd.NextDouble() + (float)rnd.NextDouble();
                Points[i] = new Point(rnd.Next(-100, 550), rnd.Next(0, 580), rnd.Next(-10, 10), rnd.Next(-10, 10));
                //Points[0] = new Point(0, 200, 5, 5);
                //Points[1] = new Point(300, 200, 0, 0);
            }
        }

        private void Start()
        {
            rnd = new Random();

            //Back = new Bitmap("backB.png", true);

            //Canvas = Canvas_Box.CreateGraphics();

            Bitmap myBitmap = new Bitmap(Width, Height);

            Canvas_Box.Image = myBitmap;

            Canvas = Graphics.FromImage(Canvas_Box.Image);
            Points = new Point[PointCount];

            MakePoint();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            Canvas_Box.Invalidate();
        }

        private void Canvas_Box_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush PointBrush = new SolidBrush(Color.Violet);

            Canvas.Clear(Color.FromArgb(15, 0, 10));

            for (int i = 0; i < Points.Length; i++) //просчёт движения.
            {
                if (Points[i].X < 0) //584, 561
                {
                    Points[i].Dx = Math.Abs(Points[i].Dx);
                }
                if (Points[i].X > Canvas_Box.Size.Width)
                {
                    Points[i].Dx = -Math.Abs(Points[i].Dx);
                }

                //Y
                if (Points[i].Y < 0) //584, 561
                {
                    Points[i].Dy = Math.Abs(Points[i].Dy);
                }
                if (Points[i].Y > Canvas_Box.Size.Height)
                {
                    Points[i].Dy = -Math.Abs(Points[i].Dy);
                }

                Points[i].X += Points[i].Dx;
                Points[i].Y += Points[i].Dy;
            } 

            for (int i = 0; i < Points.Length; i++) //отрисовка точек
            {
                Canvas.FillEllipse(PointBrush, Points[i].X, Points[i].Y, 12.6f, 12.6f);
            }

            for (int i = 0; i < Points.Length; i++)//отрисовка линий
            {
                for (int j = 0; j < Points.Length; j++)
                {
                    Point A = new Point(Points[i].X, Points[i].Y, 0, 0);
                    Point B = new Point(Points[j].X, Points[j].Y, 0, 0);

                    double AC = B.X - A.X;
                    double BC = B.Y - A.Y;
                    double ABC = Math.Pow(AC, 2) + Math.Pow(BC, 2);
                    float Distance = Single.Parse(Math.Sqrt(ABC).ToString());
                    float Fade = -(Distance - FadeDist) / FadeDist;
                    if (Fade > 0)
                    {
                        Canvas.DrawLine(new Pen(Color.Yellow, Fade * 7 + 1), B.X + 6.3f, B.Y + 6.3f, A.X + 6.3f, A.Y + 6.3f);
                    }
                }
            }


        }
    }
}
