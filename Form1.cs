using _14_project2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _14_project2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1; 

        enum enPlayer
        {
            Player1 ,
            Player2
        }

        enum enwinner
        {
            Player1 ,
            Player2 , 
            Draw ,
            GameInProgress
        }

        struct stGameStatus
        {
            public enwinner winner;
            public bool GameOver;
            public short PlayCount;
        }


        void EndGame()
        {
            lblturn.Text = "Game Over"; 
            switch(GameStatus.winner)
            {
                case enwinner.Player1:
                    lblwinner.Text = "Player1";
                    break;

                case enwinner.Player2:
                    lblwinner.Text = "Player2";
                    break;

                default:
                    lblwinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }




        public bool CheckValues(PictureBox pit1 , PictureBox pit2 , PictureBox pit3)
        {
            if(pit1.Tag.ToString() != "?" && pit1.Tag.ToString() == pit2.Tag.ToString() && pit1.Tag.ToString() == pit3.Tag.ToString())
            {
                pit1.BackColor = Color.GreenYellow;
                pit2.BackColor = Color.GreenYellow;
                pit3.BackColor = Color.GreenYellow;

                if(pit1.Tag.ToString() == "X")
                {
                    GameStatus.winner = enwinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }

                else
                {
                    GameStatus.winner = enwinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
            }

            GameStatus.GameOver = false;
            return false;
        }


        public void CheckWinner()
        {
            if (CheckValues(pictureBox1, pictureBox2, pictureBox3))
                return;

            if (CheckValues(pictureBox4, pictureBox5, pictureBox6))
                return;

            if (CheckValues(pictureBox7, pictureBox8, pictureBox9))
                return;

            if (CheckValues(pictureBox1, pictureBox4, pictureBox7))
                return;

            if (CheckValues(pictureBox2, pictureBox5, pictureBox8))
                return;

            if (CheckValues(pictureBox3, pictureBox6, pictureBox9))
                return;

            if (CheckValues(pictureBox1, pictureBox5, pictureBox9))
                return;

            if (CheckValues(pictureBox3, pictureBox5, pictureBox7))
                return;
        }

        public void ChangeImage(PictureBox pit)
        {
            if(pit.Tag.ToString()=="?")
            {
                switch(PlayerTurn)
                {
                    case enPlayer.Player1:
                        pit.Image = Resources.x;
                        PlayerTurn = enPlayer.Player2;
                        lblturn.Text = "Player 2 ";
                        GameStatus.PlayCount++;
                        pit.Tag = "X";
                        CheckWinner();
                        break;

                    case enPlayer.Player2:
                        pit.Image = Resources.o;
                        PlayerTurn = enPlayer.Player1;
                        lblturn.Text = "Player 1 ";
                        GameStatus.PlayCount++;
                        pit.Tag = "O";
                        CheckWinner();
                        break;
                }
            }

            else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if(GameStatus.PlayCount == 0 )
            {
                GameStatus.GameOver = true;
                GameStatus.winner = enwinner.Draw;
                EndGame();
            }
        }

        private void ResetBictureBox(PictureBox pit)
        {
            pit.Image = Resources.Question2;
            pit.Tag = "?";
            pit.BackColor = Color.Transparent; 
        }

        private void RestartGame()
        {
            ResetBictureBox(pictureBox1);
            ResetBictureBox(pictureBox2);
            ResetBictureBox(pictureBox3);
            ResetBictureBox(pictureBox4);
            ResetBictureBox(pictureBox5);
            ResetBictureBox(pictureBox6);
            ResetBictureBox(pictureBox7);
            ResetBictureBox(pictureBox8);
            ResetBictureBox(pictureBox9);

            PlayerTurn = enPlayer.Player1;
            lblturn.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.winner = enwinner.GameInProgress;
            lblwinner.Text = "In Progress";


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);
            Pen whitePen = new Pen(white);
            whitePen.Width = 15;

            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;


            e.Graphics.DrawLine(whitePen, 200, 150, 550, 150);
            e.Graphics.DrawLine(whitePen, 200, 230, 550, 230);

            e.Graphics.DrawLine(whitePen, 300, 70, 305, 310);
            e.Graphics.DrawLine(whitePen, 420, 70, 420, 310);

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            ChangeImage((PictureBox) sender);
        }


        private void btnrestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

       
    }
}
