using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallCollision
{
    public partial class MainForm : Form
    {

        public Scene Scene { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Start();

        }
        private void Start()
        {
            Scene = new Scene(this.Width, this.Height);
            GeneratePlayerScreen();

            Generator.Interval = 5000;
            Generator.Start();

            BallShrink.Interval = 3000;
            BallShrink.Start();

            status.Interval = 1000;
            status.Start();
            DoubleBuffered = true;
        }
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Scene.DrawAll(e.Graphics);
            lblPoints.Text = Scene.Points.ToString();
           
        }
        private void GeneratePlayerScreen()
        {
            Scene.GenerateBalls();
            Invalidate();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            Scene.AddPlayerBall(e.Location);
            Invalidate();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Scene.PlayerBall == null)
                return;
            char pressed_key = Char.ToLower(e.KeyChar);

            switch(pressed_key)
            {
                case 'w':
                    Scene.MoveUp();
                    break;
                case 's':
                    Scene.MoveDown();
                    break;
                case 'a':
                    Scene.MoveLeft();
                    break;
                case 'd':
                    Scene.MoveRight();
                    break;
                default: break;
            }
            Invalidate();
        }

        private void Generator_Tick(object sender, EventArgs e)
        {
            Scene.GenerateBall();
            Invalidate();
        }

        private void BallShrink_Tick(object sender, EventArgs e)
        {
            Scene.Shrink();
            Invalidate();
        }

        private void checkStatus(object sender, EventArgs e)
        {
            if (Scene.GameOver)
            {
                Scene.GameOver = false;
                status.Stop();
                var result = MessageBox.Show("Играта заврши, изгубивте","Game Over",MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Cancel)
                {
                    this.Close();
                }
                else {
                    NewGame();
                }
            }
        }
        private void Reset()
        {
            lblPoints.Text = "0";
            Scene = new Scene(this.Width, this.Height);
            Generator.Stop();
            BallShrink.Stop();
        }
        private void NewGame()
        {
            Reset();
            Start();
            Invalidate();
        }
    }
}
