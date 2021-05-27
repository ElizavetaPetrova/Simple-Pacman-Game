using System;
using System.Windows.Forms;

namespace Simple_Pacman_Game
{
    public partial class Form1 : Form
    {
        bool goUp, goDown, goLeft, goRight, isGameOver;

        int score, playerSpeed, redGhostSpeed, orangeGhostSpeed, blueGhostX, blueGhostY, pinkGhostSpeed;

        public Form1()
        {
            InitializeComponent();

            resetGame();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }

        }



        private void MainGameTimer(object sender, EventArgs e)
        {

            txtScore.Text = "Score: " + score;

            //перемещение пакмана

            if (goLeft == true)
            {
                pacman.Left -= playerSpeed;
                pacman.Image = Properties.Resources.left;
            }
            if (goRight == true)
            {
                pacman.Left += playerSpeed;
                pacman.Image = Properties.Resources.right;
            }
            if (goDown == true)
            {
                pacman.Top += playerSpeed;
                pacman.Image = Properties.Resources.down;
            }
            if (goUp == true)
            {
                pacman.Top -= playerSpeed;
                pacman.Image = Properties.Resources.Up;
            }

            if (pacman.Left < -10)
            {
                pacman.Left = 1024;
            }
            if (pacman.Left > 1024)
            {
                pacman.Left = -10;
            }

            if (pacman.Top < -10)
            {
                pacman.Top = 768;
            }
            if (pacman.Top > 768)
            {
                pacman.Top = 0;
            }

            
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }
                    }

                    if ((string)x.Tag == "wall")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("You Lose!, Press Enter to restart");
                        }


                        if (blueGhost.Bounds.IntersectsWith(x.Bounds))
                        {
                            blueGhostX = -blueGhostX;
                        }
                    }


                    if ((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("You Lose! Press Enter to restart");
                        }

                    }
                }
            }


            // движение приведений

            redGhost.Left += redGhostSpeed;

            if (redGhost.Bounds.IntersectsWith(wall1.Bounds) || redGhost.Bounds.IntersectsWith(wall2.Bounds))
            {
                redGhostSpeed = -redGhostSpeed;
            }

            pinkGhost.Left += pinkGhostSpeed;

            if (pinkGhost.Bounds.IntersectsWith(wall5.Bounds) || pinkGhost.Bounds.IntersectsWith(wall6.Bounds))
            {
                pinkGhostSpeed = -pinkGhostSpeed;
            }

            orangeGhost.Left -= orangeGhostSpeed;

            if (orangeGhost.Bounds.IntersectsWith(wall3.Bounds) || orangeGhost.Bounds.IntersectsWith(wall4.Bounds))
            {
                orangeGhostSpeed = -orangeGhostSpeed;
            }


            blueGhost.Left -= blueGhostX;
            blueGhost.Top -= blueGhostY;


            if (blueGhost.Top < 0 || blueGhost.Top > 520)
            {
                blueGhostY = -blueGhostY;
            }

            if (blueGhost.Left < 0 || blueGhost.Left > 620)
            {
                blueGhostX = -blueGhostX;
            }

            if (score == 65)
            {
                gameOver("You Win!");
            }
        }

        private void resetGame()
        {

            txtScore.Text = "Score: 0";
            score = 0;

            redGhostSpeed = 5;
            orangeGhostSpeed = 3;
            pinkGhostSpeed = 2;
            blueGhostX = 5;
            blueGhostY = 5;
            playerSpeed = 8;

            isGameOver = false;

            pacman.Left = 31;
            pacman.Top = 46;

            blueGhost.Left = 525;
            blueGhost.Top = 235;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }

            gameTimer.Start();
        }

        private void gameOver(string message)
        {

            isGameOver = true;

            gameTimer.Stop();

            txtScore.Text = "Score: " + score + Environment.NewLine + message;

        }
    }
}
