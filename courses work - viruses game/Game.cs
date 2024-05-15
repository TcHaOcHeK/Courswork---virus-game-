using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace courses_work___viruses_game
{
    public partial class Game : Form
    {
        public static int[,] desk = new int[10, 10];
        private static int xClick, yClick, clickCount = 0;
        private bool whoTurn = true;
        private Point startDot = new Point(62, 44);
        

        struct MinLengthDot
        {
            public int xi;
            public int yi;
            public int xj;
            public int yj;
            public int len;
        }

        List<MinLengthDot> MinLengthDotList = new List<MinLengthDot>();

        public Game()
        {
            InitializeComponent();
            desk[0, 0] = 1;
            desk[9, 9] = 2;
        }
        
        private int foo( int n )
        {
            return n > 0 ? 1 : n==0 ? 0: -1;
        }

        private bool inRange(int value, int left, int right)
        {
            return value >= left && value < right;
        }

        private void CloseForm()
        {
            Form1 menu = new Form1();
            this.Close();
            menu.Show();
            for (int i = 0; i < 100; i++)
                desk[i / 10, i % 10] = 0;
            clickCount = 0;
        }

        private void changePlayer(bool click = false)
        {
            if (clickCount == 3 || click)
            {
                clickCount = 0;
                whoTurn = !whoTurn;
            }

            panel1.BackColor = whoTurn ? Color.Green: Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (clickCount == 0)
                changePlayer(true);
        }
        
        
        private void logicPaint()
        {
            
            switch (desk[xClick, yClick])
            {
                case 1:
                    if (whoTurn == true)
                    {
                        clickCount++;
                        desk[xClick, yClick] = 5;
                    }
                    break;
                case 2:
                    if (whoTurn == false)
                    {
                        clickCount++;
                        desk[xClick, yClick] = 4;
                    }
                    break;
                case 0 :
                    if (whoTurn)
                    {
                        clickCount++;
                        desk[xClick, yClick] = 2;
                    }
                    else {
                        clickCount++;
                        desk[xClick, yClick] = 1;
                    }
                    break;
            }
        }
//поменяй названия методов(нужно понимать для чего они и что делают)

        private bool logicClick()
        {
            bool logic = false;

            logic = inRange(xClick + 1, 0, 10)
                ? logic || desk[xClick + 1, yClick] % 3 == 1 + Convert.ToInt32(whoTurn)
                : logic;
            
            logic =inRange(yClick + 1, 0, 10)
                ? logic || desk[xClick, yClick + 1] % 3 == 1 + Convert.ToInt32(whoTurn)
                : logic;
            
            logic = inRange(xClick - 1, 0, 10)
                ? logic || desk[xClick - 1, yClick] % 3 == 1 + Convert.ToInt32(whoTurn)
                : logic;
            
            logic = inRange(yClick - 1, 0, 10)
                 ? logic || desk[xClick, yClick - 1] % 3 == 1 + Convert.ToInt32(whoTurn)
                 : logic;
            
            return logic ;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            xClick = e.X / 46 - 1;
            yClick = e.Y / 46 - 1;

            if (inRange(xClick, 0, 10) && inRange(yClick, 0, 10))
            {
                if (whoTurn){
                    if (logicClick())
                        logicPaint();
                    
                }
                
            }
        }
        
      //Здесь ошибка, нужно чтобы он выводил последнее из наименьшей длинны, сейчас он так не делает и бот топчется на месте
      private void
          BOTDoing() // Получить положение ближайшей к боту клетки игрока и Найти кротчайшее растояние от клеток бота до клетки игрока
      {
          List<Tuple<Tuple<int, int>, Tuple<int, int>, int>> minDistances =
              new List<Tuple<Tuple<int, int>, Tuple<int, int>, int>>();

          int minDistance = int.MaxValue;

          for (int i = 0; i < desk.GetLength(0); i++)
          {
              for (int j = 0; j < desk.GetLength(1); j++)
              {
                  if (desk[i, j] % 3 == 1)
                  {
                      for (int x = 0; x < desk.GetLength(0); x++)
                      {
                          for (int y = 0; y < desk.GetLength(1); y++)
                          {
                              if (desk[x, y] == 2)
                              {
                                  int distance = Math.Abs(i - x) + Math.Abs(j - y);
                                  if (distance <= minDistance)
                                  {
                                      minDistance = distance;
                                      minDistances.Add(
                                          Tuple.Create(Tuple.Create(i, j), Tuple.Create(x, y), minDistance));
                                  }
                              }
                          }
                      }
                  }
              }
          }

          // Сортировка minDistances по увеличению расстояния между точками
          minDistances = minDistances.OrderBy(d => d.Item3).ToList();

          MinLengthDotList.Clear();
          foreach (var distance in minDistances)
          {
              MinLengthDot dot = new MinLengthDot()
              {
                  xi = distance.Item1.Item1,
                  yi = distance.Item1.Item2,
                  xj = distance.Item2.Item1,
                  yj = distance.Item2.Item2,
                  len = distance.Item3
              };
              MinLengthDotList.Add(dot);
          }

          BOTClick(MinLengthDotList);
      }

      private void BOTClick( List<MinLengthDot> op) //Поставить клетки чтобы уменьшить растаяние от бота до игрока
        {
            int n = 0;
            bool flag = false;
            int dx = op[0].xj - op[0].xi != 0 ? foo(op[0].xj - op[0].xi) :1 ;
            int dy = op[0].yj - op[0].yi != 0 ? foo(op[0].yj - op[0].yi) :1 ;
            
            xClick = Math.Abs(op[n].xj - op[n].xi) >= Math.Abs(op[n].yj - op[n].yi) ? op[n].xi + dx : op[n].xi;
            yClick = Math.Abs(op[n].xj - op[n].xi) < Math.Abs(op[n].yj - op[n].yi) ? op[n].yi + dy : op[n].yi;
            if(desk[xClick, yClick] != 2 && desk[xClick, yClick] % 3 != 0)
            {
                xClick = Math.Abs(op[n].xj - op[n].xi) < Math.Abs(op[n].yj - op[n].yi) ? op[n].xi + dx : op[n].xi;
                yClick = Math.Abs(op[n].xj - op[n].xi) >= Math.Abs(op[n].yj - op[n].yi) ? op[n].yi + dy : op[n].yi;
            }
            
            while (n < op.Count)
            {
                
                if (logicClick() && (desk[xClick, yClick] == 2 || desk[xClick, yClick] == 0))
                {
                    logicPaint();
                    break;
                }
                else
                {
                    n++;
                    if (n >= op.Count)
                    {
                        flag = !flag;
                        break;
                    }

                    xClick = Math.Abs(op[n].xj - op[n].xi) >= Math.Abs(op[n].yj - op[n].yi) ? op[n].xi + dx : op[n].xi;
                    yClick = Math.Abs(op[n].xj - op[n].xi) < Math.Abs(op[n].yj - op[n].yi) ? op[n].yi + dy : op[n].yi;
                    if(desk[xClick, yClick] != 2 && desk[xClick, yClick] % 3 != 0)
                    {
                        xClick = Math.Abs(op[n].xj - op[n].xi) < Math.Abs(op[n].yj - op[n].yi) ? op[n].xi + dx : op[n].xi;
                        yClick = Math.Abs(op[n].xj - op[n].xi) >= Math.Abs(op[n].yj - op[n].yi) ? op[n].yi + dy : op[n].yi;
                    }
                    
                }
            }
            if(flag)
                changePlayer(true);

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int cPlayer = 0, cBot = 0; 
            changePlayer();
            
            if (!whoTurn)
            {
                BOTDoing();
            }
            
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    switch (desk[i, j])
                    {
                        case 1:
                            e.Graphics.FillEllipse(Brushes.Red, startDot.X + i * 45, startDot.Y + j * 45, 41, 41);
                            cPlayer ++;
                            break;
                        case 2:
                            e.Graphics.FillEllipse(Brushes.Green, startDot.X + i * 45, startDot.Y + j*45, 41, 41);
                            cBot ++;
                            break;
                        case 4:
                            e.Graphics.FillRectangle(Brushes.DarkRed, startDot.X + i * 45, startDot.Y + j*45, 41, 41);
                            break;
                        case 5:
                            e.Graphics.FillRectangle(Brushes.DarkGreen, startDot.X + i * 45, startDot.Y + j*45, 41, 41);
                            break;
                    }
                }

            if (cPlayer == 0)
            {
                Form1.WhoWin = 1;
                CloseForm();
            }
            
            if (cBot == 0)
            {
                Form1.WhoWin = 2;
                CloseForm();
            }
                


            Refresh();

        }

       

      

        
    }
    
    
}