using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Boids
{
    public partial class Form1 : Form
    {
        private Boid[] Boids;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var rnd = new Random();
            Boids = Enumerable.Range(1, 100).Select(_ => new Boid(rnd)).ToArray();
        }

        private void DrawBoids(Boid[] boids)
        {
            using (var g = Airspace.CreateGraphics())
            {
                g.Clear(Airspace.BackColor);

                using (var redBrush = new SolidBrush(Color.Red))
                using (var blueBrush = new SolidBrush(Color.Blue))
                using (var yellowBrush = new SolidBrush(Color.Yellow))
                using (var greenBrush = new SolidBrush(Color.Green))
                using (var purpleBrush = new SolidBrush(Color.Purple))
                {
                    foreach (var boid in boids)
                    {
                        var brush = default(SolidBrush);
                        switch (boid.Colour)
                        {
                            case BoidColour.Red:
                                brush = redBrush;
                                break;
                            case BoidColour.Blue:
                                brush = blueBrush;
                                break;
                            case BoidColour.Yellow:
                                brush = yellowBrush;
                                break;
                            case BoidColour.Green:
                                brush = greenBrush;
                                break;
                            case BoidColour.Purple:
                                brush = purpleBrush;
                                break;
                        }

                        var x = boid.Position.X * (Airspace.Width / 100);
                        var y = boid.Position.Y * (Airspace.Height / 100);

                        g.FillRectangle(brush, x, y, 10, 10);
                    }
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            MoveBoids(Boids);
            DrawBoids(Boids);
        }

        private void MoveBoids(Boid[] boids)
        {
            foreach (var boid in boids)
            {
                var v1 = Rule1(boids, boid);
                var v2 = Rule2(boids, boid);
                var v3 = Rule3(boids, boid);

                boid.Position += boid.Velocity + v1 + v2 + v3;

                BoundPosition(boid, 100, 100);
            }
        }


        private Vector Rule1(Boid[] boids, Boid boid)
        {
            var offset = boids.Where(x => x != boid)
                              .Select(x => x.Position)
                              .Average();

            return offset / 50;
        }

        private Vector Rule2(Boid[] boids, Boid bj)
        {
            var c = Vector.Zero;

            foreach (var b in boids.Where(x => x != bj))
            {
                var d = b.Position - bj.Position;
                if (Math.Abs(d.X) < 3 && Math.Abs(d.Y) < 3)
                {
                    c -= d;
                }
            }

            return c;
        }

        private Vector Rule3(Boid[] boids, Boid boid)
        {
            var v = boids.Where(x => x != boid)
                         .Select(x => x.Velocity)
                         .Average();
            return v / 8;
        }

        private void BoundPosition(Boid boid, int maxX, int maxY)
        {
            if (boid.Position.X < 2)
                boid.Velocity = boid.Velocity.WithX(2);

            if (boid.Position.X >= maxX)
                boid.Velocity = boid.Velocity.WithX(-2);

            if (boid.Position.Y < 2)
                boid.Velocity = boid.Velocity.WithY(2);

            if (boid.Position.Y >= maxY)
                boid.Velocity = boid.Velocity.WithY(-2);
        }
    }
}
