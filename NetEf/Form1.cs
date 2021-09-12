using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetEf
{
    public partial class FormMain : Form
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

        Graphics Canvas;
        Random rnd;
        Bitmap Back;

        Point[] Points;

        int LineCount = 0;

        readonly int PointCount = 15;
        readonly float FadeDist = 200f;

        public FormMain()
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

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            //584, 561
            this.Width = 600;
            this.Height = 600;

            Canvas_Box.Invalidate();
        }

        private void Canvas_Box_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush PointBrush = new SolidBrush(Color.Violet);


            Canvas.Clear(Color.Black);

            LineCount = 0;

            for (int j = 0; j < Points.Length; j++)
            {
                //AB = √(xb - xa)2 + (yb - ya)2

                for (int a = 0; a < Points.Length; a++)
                {
                    var A = new Point(Points[j].X, Points[j].Y, 0, 0);
                    var B = new Point(Points[a].X, Points[a].Y, 0, 0);

                    double FadeD = (((Math.Sqrt      (Math.Pow(B.X - A.X, 2) + Math.Pow(B.Y - A.Y, 2))    - FadeDist))   /   FadeDist) * -1;
                    float Fade = Single.Parse(FadeD.ToString());
                    

                    if (Fade > 0)
                    {
                        Canvas.DrawLine(new Pen(Color.Yellow, Fade * 2 + 1), B.X, B.Y, A.X, A.Y);
                    }
                    LineCount += 1;
                    //FormMain.ActiveForm.Text = LineCount.ToString();
                }
            }

            //Canvas.DrawImage(Back, -240, 0);

            /*
                    Point1 := TPointF.Create(Points[I].X, Points[I].Y);
                    Point2 := TPointF.Create(Points[J].X, Points[J].Y);

                    // вычисляем расстояние между линиями в -00 -> 0..1
                    Fade := -(Point1.Distance(Point2) - FadeDistance) / FadeDistance;
             */

            for (int i = 0; i < PointCount; i++)
            {
                Points[i].X += Points[i].Dx * 1.2f;
                Points[i].Y += Points[i].Dy * 1.2f;

                //X
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

                Canvas.FillEllipse(PointBrush, Points[i].X, Points[i].Y, 12.6f, 12.6f);

                //Canvas.FillEllipse(PointBrush, Points[i].X - 12.5f, Points[i].Y - 12.5f, 12.5f, 12.5f);

                //Canvas.FillEllipse(PointBrush, Points[i].X - 20, Points[i].Y - 20, 20, 20);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
