using System;
using System.Windows.Forms;

namespace courses_work___viruses_game
{
    public partial class Rules : Form
    {
        public Rules()
        {
            InitializeComponent();
            
            }

        
            

            private void button1_Click(object sender, EventArgs e)
            {
                Form1 menu = new Form1();
                this.Close();
                menu.Show();
            }
    }
}