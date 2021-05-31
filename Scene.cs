using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallCollision
{
    public class Scene
    {
        public List<Ball> Balls { get; set; }
        public Ball PlayerBall { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public readonly Random random = new Random();
        public bool GameOver { get; set; }
        public int Points { get; set; }
        public int numBalls { get; set; }

        public Scene(int width,int height)
        {
            Balls = new List<Ball>();
            this.Width = width;
            this.Height = height;
            GameOver = false;
        }
        public void AddPlayerBall(Point location)
        {
            Ball ball = new Ball(location);
            if(!DetectCollision(ball) && PlayerBall == null)
            {
                PlayerBall = ball;
                PlayerBall.Radius = 16; // starting size
                PlayerBall.Color = Color.Blue;
            }
        }
        public void GenerateBalls()
        {
            int size = random.Next(20, 35);
            for(int i=0;i<size;i++)
            {
                Point location = GenerateLocation();
                Ball ball = new Ball(location);
                if(!DetectCollision(ball))
                    Balls.Add(ball);
            }

        }
        public Point GenerateLocation()
        {
            return new Point(random.Next(Ball.maxRadius * 2, Width - Ball.maxRadius * 2),
                    random.Next(Ball.maxRadius * 2, Height - Ball.maxRadius * 2));
        }
        public void DrawAll (Graphics graphics)
        {
            foreach(Ball ball in Balls)
            {
                ball.Draw(graphics);
            }
            if (PlayerBall == null)
                return;

            PlayerBall.Draw(graphics);
            GameLogic();
        }
        public bool DetectCollision(Ball target)
        {
            foreach(Ball ball in Balls)
            {
                if(ball.CheckCollision(target))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckClick(Point location)
        {
            foreach(Ball ball in Balls)
            {
                if (ball.IsClicked(location))
                    return true;
            }
            return false;
        }
        public void GameLogic()
        {
            foreach(Ball ball in Balls)
            {
                if(ball.CheckCollision(PlayerBall))
                {
                    if(PlayerBall.Radius >= ball.Radius)
                    {
                        Points++;
                        PlayerBall.Radius += (int) (ball.Radius * 0.30);
                        ball.ToBeDeleted = true;
                    }
                    else
                    {
                        End();
                    }
                }
            }

            for(int i=Balls.Count - 1; i >= 0; i--)
            {
                if (Balls[i].ToBeDeleted)
                    Balls.RemoveAt(i);
            }
            if(Balls.Count < 3)
            {
                GenerateBalls();
            }
        }
        public void End()
        {
            PlayerBall.Center = Point.Empty;
            PlayerBall.Radius = 0;
            GameOver = true;
        }

        public void GenerateBall()
        {
            Ball ball = new Ball(GenerateLocation());
            ball.Radius = 10;
            if(!DetectCollision(ball))
                Balls.Add(ball);
        }

        public void Shrink()
        {
            if (PlayerBall != null)
            {
                if (PlayerBall.Radius <= 0)
                    End();
                PlayerBall.Shrink();
            }
        }
        //MOVING
        public void MoveLeft()
        {
        }
        public void MoveRight()
        {
        }
        public void MoveUp()
        {
            PlayerBall.Move(new Point(PlayerBall.Center.X, PlayerBall.Center.Y - 5));
        }
        public void MoveDown()
        {
            PlayerBall.Move(new Point(PlayerBall.Center.X, PlayerBall.Center.Y + 5));
        }
    }
}
