using System;
using System.Windows.Forms;


namespace courses_work___viruses_game
{
    public partial class Game : Form
    {
        public static int[,] desk = new int[10,10];

        private void CloseForm()
        {
            Form1 menu = new Form1();
            this.Close();
            menu.Show();
            
        }
        
        public Game()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            CloseForm();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            
        }  
    }
}