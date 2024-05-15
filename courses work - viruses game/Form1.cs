using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace courses_work___viruses_game
{
    public partial class Form1 : Form
    {
        public static int WhoWin;
        public Form1()
        {
            InitializeComponent();
            if (WhoWin == 1)
                MessageBox.Show("You win!!");
            else if(WhoWin == 2)
                MessageBox.Show("You loose _-_");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Game gm = new Game();
            gm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rules rl = new Rules();
            rl.Show();
            this.Hide();
        }

        
    }
}