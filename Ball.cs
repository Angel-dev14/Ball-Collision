using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallCollision
{
    public class Ball
    {
        public Point Center { get; set; }
        public int Radius { get; set; }
        public Color Color { get; set; }
        public static readonly Random random = new Random();
        public static int maxRadius = 50;
        public bool ToBeDeleted { get; set; }

        public Ball(Point point)
        {
            this.Center = point;
            this.Color = Color.Red;
            this.Radius = random.Next(10, 40);
            ToBeDeleted = false;
        }
        public void Draw(Graphics graphics)
        {
            Brush brush = new SolidBrush(Color);
            graphics.FillEllipse(brush, Center.X - Radius, Center.Y - Radius, Radius * 2, Radius * 2);
            brush.Dispose();
        }
        public bool CheckCollision(Ball ball)
        {
            double distance = Math.Pow(Center.X - ball.Center.X, 2) + Math.Pow(Center.Y - ball.Center.Y, 2);
            return distance <= Math.Pow(Radius + ball.Radius,2);
        }
        public bool IsClicked(Point point)
        {
            return Math.Pow(Center.X - point.X, 2) + Math.Pow(Center.Y - point.Y, 2) <= Radius * Radius;
        }
        public void Move(Point location)
        {
            this.Center = location;
        }
        public void Shrink()
        {
            if (Radius > 80)
                this.Radius = 60;
            if (Radius > 60)
                this.Radius -= 15;
            else if (Radius > 40)
                this.Radius -= 10;
            else
                this.Radius -= 2;
        }

    }
}
