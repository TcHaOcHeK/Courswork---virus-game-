using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace courses_work___viruses_game
{
    public partial class Game : Form
    {
        public static int[,] desk = new int[10,10];
        private static int xClick, yClick, clickCount = 0;
        private Point startDot = new Point(62,44);
        

        private void CloseForm()
        {
            Form1 menu = new Form1();
            this.Close();
            menu.Show();
            for (int i = 0; i < 100; i++)
                desk[i / 10, i % 10] = 0;
            clickCount = 0;
        }
        
        public Game()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
        

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            if ((clickCount / 3) % 2 == 0)
                panel1.BackColor = Color.Green;
            else
                panel1.BackColor = Color.Red;
            
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    
                    switch (desk[i, j])
                    {
                        case 1:
                            e.Graphics.FillRectangle(Brushes.Green, startDot.X + i * 45, startDot.Y + j*45, 40, 40);
                            break;
                        case 2:
                            e.Graphics.FillRectangle(Brushes.DarkGreen, startDot.X + i * 45, startDot.Y + j*45, 40, 40);
                            break;
                        case 3:
                            e.Graphics.FillRectangle(Brushes.Red, startDot.X + i * 45, startDot.Y + j*45, 40, 40);
                            break;
                        case 4:
                            e.Graphics.FillRectangle(Brushes.DarkRed, startDot.X + i * 45, startDot.Y + j*45, 40, 40);
                            break;
                        default:
                            e.Graphics.FillRectangle(Brushes.Yellow, startDot.X + i * 45, startDot.Y + j*45, 40, 40);
                            break;
                    }

                    
                }
        

            Refresh();

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
             
            xClick = e.X / 46 - 1;
            yClick = e.Y / 46 - 1;
            
            if (xClick >= 0 && yClick >= 0)
            {
                clickCount++;
                
                if (panel1.BackColor == Color.Green)
                    desk[xClick, yClick] = 1;
                else
                    desk[xClick, yClick] = 3;
                
                textBox1.Text = Convert.ToString(xClick) + "  " + Convert.ToString(yClick) + "  " +
                                Convert.ToString(e.X) + "  " + Convert.ToString(e.Y);
            }
        }
    }
    
    
}