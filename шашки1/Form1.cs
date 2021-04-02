using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace шашки1
{
    public partial class Form1 : Form
    {
        public int[,] map = new int[8, 8]
        {
             {20,0,20,0,20,0,20,0 },
             {0,20,0,20,0,20,0,20 },
             {20,0,20,0,20,0,20,0 },
             {0,0,0,0,0,0,0,0 },
             {0,0,0,0,0,0,0,0 },
             {0,10,0,10,0,10,0,10 },
             {10,0,10,0,10,0,10,0 },
             {0,10,0,10,0,10,0,10 },
        };
        public Button[,] butt = new Button[8, 8];
        public Button prevButton = new Button();
        public bool isMooving = false;
        public int currPlayer = 1;
        public static int pl1score = 0;
        public static int pl2score = 0;
        public bool Cutfig = false;
        public Image whiteSprite;
        public Image blackSprite;
        public bool Destroy = false;
        public bool Destroy2 = false;
        public bool Destroy3 = false;
        public bool DestroyDama = false;
        public bool prov = false;
        public Form1()
        {
            InitializeComponent();
            whiteSprite = new Bitmap("C:\\Users\\pante\\Downloads\\White.png");
            blackSprite = new Bitmap("C:\\Users\\pante\\Downloads\\White.png");
            Image part = new Bitmap(50, 50);
            Graphics e = Graphics.FromImage(part);
            e.DrawImage(blackSprite, new Rectangle(0, 0, 50, 50), 0, 0, 150, 150, GraphicsUnit.Pixel);
            Image part1 = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(part1);
            g.DrawImage(whiteSprite, new Rectangle(0, 0, 50, 50), 0, 0, 150, 150, GraphicsUnit.Pixel);
            Init();
        }
        public void ShowWin()
        {
            if(pl1score >= 12)
            {
                MessageBox.Show("Игрок 1 победил", "Result");
            }
            if (pl2score >= 12)
            {
                MessageBox.Show("Игрок 2 победил", "Result");
            }
        }
        public void CreateNewButt()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(50, 50);
                    button.Location = new Point(j * 50,i* 50);
                    button.BackColor = Color.White;
                    switch (map[i, j]/10)
                    {
                        case 2:
                            Image part = new Bitmap(50, 50);
                            Graphics e = Graphics.FromImage(part);
                            e.DrawImage(blackSprite, new Rectangle(0, 0, 50, 50),280, -75, 300, 230, GraphicsUnit.Pixel);
                            button.BackgroundImage = part;
                            break;
                        case 1:
                            Image part1 = new Bitmap(50, 50);
                            Graphics g = Graphics.FromImage(part1);
                            g.DrawImage(whiteSprite, new Rectangle(0, 0, 50, 50), -15, -75, 300, 230, GraphicsUnit.Pixel);
                            button.BackgroundImage = part1;
                            break;
                    }
                    button.Click += new EventHandler(OnPress);
                    this.Controls.Add(button);
                    butt[i, j] = button;
                }
            }
        }
        public void CutDama(int icurrFigure, int jcurrFigure)
        {
            if (InsideBorder(icurrFigure - 2 , jcurrFigure - 2))
            {
                if((map[icurrFigure - 1 , jcurrFigure - 1] % 10 == 1 || map[icurrFigure - 1, jcurrFigure - 1] / 10 == 1)&& currPlayer == 2 && map[icurrFigure - 2 , jcurrFigure - 2] == 0)
                {
                    butt[icurrFigure - 2, jcurrFigure - 2].BackColor = Color.Yellow;
                    butt[icurrFigure - 2, jcurrFigure - 2].Enabled = true;
                    DestroyDama = true;
                }
            }
            if (InsideBorder(icurrFigure + 2, jcurrFigure - 2))
            {
                if ((map[icurrFigure + 1, jcurrFigure - 1] % 10 == 1 || map[icurrFigure + 1, jcurrFigure - 1] / 10 == 1) && currPlayer == 2 && map[icurrFigure + 2, jcurrFigure - 2] == 0)
                {
                    butt[icurrFigure + 2, jcurrFigure - 2].BackColor = Color.Yellow;
                    butt[icurrFigure + 2, jcurrFigure - 2].Enabled = true;
                    DestroyDama = true;
                }
            }
            if (InsideBorder(icurrFigure - 2, jcurrFigure + 2))
            {
                if ((map[icurrFigure - 1, jcurrFigure + 1] % 10 == 1 || map[icurrFigure - 1, jcurrFigure + 1] / 10 == 1) && currPlayer == 2 && map[icurrFigure - 2, jcurrFigure + 2] == 0)
                {
                    butt[icurrFigure - 2, jcurrFigure + 2].BackColor = Color.Yellow;
                    butt[icurrFigure - 2, jcurrFigure + 2].Enabled = true;
                    DestroyDama = true;
                }
            }
            if (InsideBorder(icurrFigure + 2, jcurrFigure + 2))
            {
                if ((map[icurrFigure + 1, jcurrFigure + 1] % 10 == 1 || map[icurrFigure + 1, jcurrFigure + 1] / 10 == 1) && currPlayer == 2 && map[icurrFigure + 2, jcurrFigure + 2] == 0)
                {
                    butt[icurrFigure + 2, jcurrFigure + 2].BackColor = Color.Yellow;
                    butt[icurrFigure + 2, jcurrFigure + 2].Enabled = true;
                    DestroyDama = true;
                }
            }
            if (InsideBorder(icurrFigure - 2, jcurrFigure - 2))
            {
                if ((map[icurrFigure - 1, jcurrFigure - 1] % 10 == 2 || map[icurrFigure - 1, jcurrFigure - 1] / 10 == 2) && currPlayer == 1 && map[icurrFigure - 2, jcurrFigure - 2] == 0)
                {
                    butt[icurrFigure - 2, jcurrFigure - 2].BackColor = Color.Yellow;
                    butt[icurrFigure - 2, jcurrFigure - 2].Enabled = true;
                    DestroyDama = true;
                }
            }
            if (InsideBorder(icurrFigure + 2, jcurrFigure - 2))
            {
                if ((map[icurrFigure + 1, jcurrFigure - 1] % 10 == 2 || map[icurrFigure + 1, jcurrFigure - 1] / 10 == 2) && currPlayer == 1 && map[icurrFigure + 2, jcurrFigure - 2] == 0)
                {
                    butt[icurrFigure + 2, jcurrFigure - 2].BackColor = Color.Yellow;
                    butt[icurrFigure + 2, jcurrFigure - 2].Enabled = true;
                    DestroyDama = true;
                }
            }
            if (InsideBorder(icurrFigure - 2, jcurrFigure + 2))
            {
                if ((map[icurrFigure - 1, jcurrFigure + 1] % 10 == 2 || map[icurrFigure - 1, jcurrFigure + 1] / 10 == 2) && currPlayer == 1 && map[icurrFigure - 2, jcurrFigure + 2] == 0)
                {
                    butt[icurrFigure - 2, jcurrFigure + 2].BackColor = Color.Yellow;
                    butt[icurrFigure - 2, jcurrFigure + 2].Enabled = true;
                    DestroyDama = true;
                }
            }
            if (InsideBorder(icurrFigure + 2, jcurrFigure + 2))
            {
                if ((map[icurrFigure + 1, jcurrFigure + 1] % 10 == 2 || map[icurrFigure + 1, jcurrFigure + 1] / 10 == 2) && currPlayer == 1 && map[icurrFigure + 2, jcurrFigure + 2] == 0)
                {
                    butt[icurrFigure + 2, jcurrFigure + 2].BackColor = Color.Yellow;
                    butt[icurrFigure + 2, jcurrFigure + 2].Enabled = true;
                    DestroyDama = true;
                }
            }
        }
        public void CutFigure(int icurrFigure, int jcurrFigure)
        {
            int dir;
            if (currPlayer == 1)
            {
                dir = 1;
            }
            else
                dir = -1;
            if (InsideBorder(icurrFigure - 2 * dir, jcurrFigure - 2))
            {
                if ((map[icurrFigure - 1 * dir, jcurrFigure - 1]/10 == 1 || map[icurrFigure - 1 * dir, jcurrFigure - 1] % 10 == 1) && currPlayer == 2 && map[icurrFigure - 2 * dir, jcurrFigure - 2] == 0)
                {
                    butt[icurrFigure - 2 * dir, jcurrFigure - 2].BackColor = Color.Yellow;
                    butt[icurrFigure - 2 * dir, jcurrFigure - 2].Enabled = true;
                    Cutfig = true;
                    if(DestroyDama == false)
                    {
                        Destroy = true;
                    }
                }
                if ((map[icurrFigure - 1 * dir, jcurrFigure - 1] / 10 == 2 || map[icurrFigure - 1 * dir, jcurrFigure - 1] % 10 == 2)&& currPlayer == 1 && map[icurrFigure - 2 * dir, jcurrFigure - 2] == 0)
                {
                    butt[icurrFigure - 2 * dir, jcurrFigure - 2].BackColor = Color.Yellow;
                    butt[icurrFigure - 2 * dir, jcurrFigure - 2].Enabled = true;
                    Cutfig = true;
                    if (DestroyDama == false)
                    {
                        Destroy = true;
                    }
                }  
            }
            if (InsideBorder(icurrFigure - 2 * dir, jcurrFigure + 2))
            {
                if ((map[icurrFigure - 1 * dir, jcurrFigure + 1]/10 == 1 || map[icurrFigure - 1 * dir, jcurrFigure + 1] % 10 == 1)  && currPlayer == 2 && map[icurrFigure - 2 * dir, jcurrFigure + 2] == 0)
                {
                        butt[icurrFigure - 2 * dir, jcurrFigure + 2].BackColor = Color.Yellow;
                        butt[icurrFigure - 2 * dir, jcurrFigure + 2].Enabled = true;
                        Cutfig = true;
                    if (DestroyDama == false)
                    {
                        Destroy2 = true;
                    }
                }
                if ((map[icurrFigure - 1 * dir, jcurrFigure + 1] / 10 == 2 || map[icurrFigure - 1 * dir, jcurrFigure + 1] % 10 == 2)&& currPlayer == 1 && map[icurrFigure - 2 * dir, jcurrFigure + 2] == 0)
                {
                        butt[icurrFigure - 2 * dir, jcurrFigure + 2].BackColor = Color.Yellow;
                        butt[icurrFigure - 2 * dir, jcurrFigure + 2].Enabled = true;
                        Cutfig = true;
                    if (DestroyDama == false)
                    {
                        Destroy2 = true;
                    }
                }
            }

        }
        public void Dama()
        {
            for(int j = 0; j < 8; j++)
            {
                if(map[0,j]/10 == 1)
                {
                    butt[0, j].Text = "Дама белые";
                    map[0, j] = 41;
                }
                if (map[7, j]/10 == 2)
                {
                    butt[7, j].Text = "Дама черные";
                    map[7, j] = 42;
                }
            }
        }
        public void ShowStep(int icurrFigure,int jcurrFigure, int currFigure)
        {
            int dir;
            if (currPlayer == 1)
            {
                dir = 1;
            }
            else
                dir = -1;
            switch(currFigure )
            {
                case 1:
                    if (InsideBorder(icurrFigure - 1, jcurrFigure - 1))
                    {
                        if (map[icurrFigure - 1, jcurrFigure - 1] == 0)
                        {
                            butt[icurrFigure - 1, jcurrFigure - 1].BackColor = Color.Yellow;
                            butt[icurrFigure - 1, jcurrFigure - 1].Enabled = true;
                        }
                        else
                        {
                            CutDama( icurrFigure, jcurrFigure);
                        }
                    }
                    if (InsideBorder(icurrFigure + 1, jcurrFigure - 1))
                    {
                        if (map[icurrFigure + 1, jcurrFigure - 1] == 0)
                        {
                            butt[icurrFigure + 1, jcurrFigure - 1].BackColor = Color.Yellow;
                            butt[icurrFigure + 1, jcurrFigure - 1].Enabled = true;
                        }
                        else
                        {
                            CutDama(icurrFigure, jcurrFigure);
                        }
                    }
                    if (InsideBorder(icurrFigure - 1, jcurrFigure + 1))
                    {
                        if (map[icurrFigure - 1, jcurrFigure + 1] == 0)
                        {
                            butt[icurrFigure - 1, jcurrFigure + 1].BackColor = Color.Yellow;
                            butt[icurrFigure - 1, jcurrFigure + 1].Enabled = true;
                        }
                        else
                        {
                            CutDama(icurrFigure, jcurrFigure);
                        }
                    }
                    if (InsideBorder(icurrFigure + 1, jcurrFigure + 1))
                    {
                        if (map[icurrFigure + 1, jcurrFigure + 1] == 0)
                        {
                            butt[icurrFigure + 1, jcurrFigure + 1].BackColor = Color.Yellow;
                            butt[icurrFigure + 1, jcurrFigure + 1].Enabled = true;
                        }
                        else
                        {
                            CutDama(icurrFigure, jcurrFigure);
                        }
                    }
                    break;
                case 2:
                    if (InsideBorder(icurrFigure - 1, jcurrFigure - 1))
                    {
                        if (map[icurrFigure - 1, jcurrFigure - 1] == 0)
                        {
                            butt[icurrFigure - 1, jcurrFigure - 1].BackColor = Color.Yellow;
                            butt[icurrFigure - 1, jcurrFigure - 1].Enabled = true;
                        }
                        else
                        {
                            CutDama(icurrFigure, jcurrFigure);
                        }
                    }
                    if (InsideBorder(icurrFigure + 1, jcurrFigure - 1))
                    {
                        if (map[icurrFigure + 1, jcurrFigure - 1] == 0)
                        {
                            butt[icurrFigure + 1, jcurrFigure - 1].BackColor = Color.Yellow;
                            butt[icurrFigure + 1, jcurrFigure - 1].Enabled = true;
                        }
                        else
                        {
                            CutDama(icurrFigure, jcurrFigure);
                        }
                    }
                    if (InsideBorder(icurrFigure - 1, jcurrFigure + 1))
                    {
                        if (map[icurrFigure - 1, jcurrFigure + 1] == 0)
                        {
                            butt[icurrFigure - 1, jcurrFigure + 1].BackColor = Color.Yellow;
                            butt[icurrFigure - 1, jcurrFigure + 1].Enabled = true;
                        }
                        else
                        {
                            CutDama(icurrFigure, jcurrFigure);
                        }
                    }
                    if (InsideBorder(icurrFigure + 1, jcurrFigure + 1))
                    {
                        if (map[icurrFigure + 1, jcurrFigure + 1] == 0)
                        {
                            butt[icurrFigure + 1, jcurrFigure + 1].BackColor = Color.Yellow;
                            butt[icurrFigure + 1, jcurrFigure + 1].Enabled = true;
                        }
                        else
                        {
                            CutDama(icurrFigure, jcurrFigure);
                        }
                    }
                    break;
            }
            if (InsideBorder(icurrFigure - 1 * dir, jcurrFigure - 1))
            {
                if (map[icurrFigure - 1 * dir, jcurrFigure - 1] == 0)
                {
                    butt[icurrFigure - 1 * dir, jcurrFigure - 1].BackColor = Color.Yellow;
                    butt[icurrFigure - 1 * dir, jcurrFigure - 1].Enabled = true;
                }
                else
                {
                    CutFigure(icurrFigure, jcurrFigure);
                }
            }
            if (InsideBorder(icurrFigure - 1 * dir, jcurrFigure + 1))
            {
                if (map[icurrFigure - 1 * dir, jcurrFigure + 1] == 0)
                {
                    butt[icurrFigure - 1 * dir, jcurrFigure + 1].BackColor = Color.Yellow;
                    butt[icurrFigure - 1 * dir, jcurrFigure + 1].Enabled = true;
                    if (Cutfig)
                    {
                        butt[icurrFigure - 1 * dir, jcurrFigure + 1].BackColor = Color.White;
                        butt[icurrFigure - 1 * dir, jcurrFigure + 1].Enabled = false;
                        Cutfig = false;
                    }
                }
                else
                {
                    CutFigure(icurrFigure, jcurrFigure);
                    if (InsideBorder(icurrFigure - 1 * dir, jcurrFigure - 1))
                    {
                        if (Cutfig)
                        {

                            butt[icurrFigure - 1 * dir, jcurrFigure - 1].BackColor = Color.White;
                            butt[icurrFigure - 1 * dir, jcurrFigure - 1].Enabled = false;
                            Cutfig = false;
                        }
                    }
                }
            }
        }  
        public bool InsideBorder(int t1, int t2)
        {
            if (t1 < 0 || t1 >= 8 || t2 < 0 || t2 >= 8)
                return false;
            return true;
        }
        public void CloseSteps()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    butt[i, j].BackColor = Color.White;
                }
            }
        }
        public void SwitchPlayer()
        {
            if (currPlayer == 1)
                currPlayer = 2;
            else
                currPlayer = 1;
        }
        private void OnPress(object sender, EventArgs e)
        {
            if(map[prevButton.Location.Y / 50, prevButton.Location.X / 50] != 0)
            {
                prevButton.BackColor = Color.White;
                
            }
            Button pressedButton =  sender as Button;
            
            if (map[pressedButton.Location.Y / 50, pressedButton.Location.X / 50] != 0 && (map[pressedButton.Location.Y / 50, pressedButton.Location.X / 50]%10 == currPlayer || map[pressedButton.Location.Y / 50, pressedButton.Location.X / 50]/10 == currPlayer))
            {
                pressedButton.BackColor = Color.Red;
                DeactivateButtons();
                pressedButton.Enabled = true;
                ShowStep(pressedButton.Location.Y/50, pressedButton.Location.X/50, map[pressedButton.Location.Y / 50, pressedButton.Location.X / 50]% 10);
                if (isMooving)
                {
                    pressedButton.BackColor = Color.White;
                    ActivateButtons();
                    CloseSteps();
                    isMooving = false;
                }
                else
                  isMooving = true;
            }
            else
            {
                if (isMooving)
                {
                    int dir;
                    if (currPlayer == 1)
                    {
                        dir = 1;
                    }
                    else
                        dir = -1;
                    int temp = map[pressedButton.Location.Y / 50, pressedButton.Location.X / 50];
                    map[pressedButton.Location.Y / 50, pressedButton.Location.X / 50] = map[prevButton.Location.Y / 50, prevButton.Location.X / 50];
                    map[prevButton.Location.Y / 50, prevButton.Location.X / 50] = temp;
                    pressedButton.BackgroundImage = prevButton.BackgroundImage;
                    pressedButton.Text = prevButton.Text;
                    prevButton.Text = null;
                    prevButton.BackgroundImage = null;
                    isMooving = false;
                    Dama();
                    if (DestroyDama)
                    {
                        if (InsideBorder(pressedButton.Location.Y / 50 + 1, pressedButton.Location.X / 50 + 1))
                        {
                            if (InsideBorder(prevButton.Location.Y / 50 - 2, prevButton.Location.X / 50 - 2))
                            {
                                if (map[prevButton.Location.Y / 50 - 2, prevButton.Location.X / 50 - 2]% 10 != 0)
                                {
                                    butt[pressedButton.Location.Y / 50 + 1 , pressedButton.Location.X / 50 + 1].BackgroundImage = null;
                                    butt[pressedButton.Location.Y / 50 + 1, pressedButton.Location.X / 50 + 1].Text = "";
                                    map[pressedButton.Location.Y / 50 + 1 , pressedButton.Location.X / 50 + 1] = 0;
                                    if (currPlayer == 1)
                                    {
                                        pl1score++;
                                        label1.Text = "Score 1 :" + pl1score;
                                    }
                                    else
                                    {
                                        pl2score++;
                                        label2.Text = "Score 2 :" + pl2score;
                                    }
                                    DestroyDama = false;
                                }
                            }
                        }
                        if (InsideBorder(pressedButton.Location.Y / 50 - 1, pressedButton.Location.X / 50 + 1))
                        {
                            if (InsideBorder(prevButton.Location.Y / 50 + 2, prevButton.Location.X / 50 - 2))
                            {
                                if (map[prevButton.Location.Y / 50 + 2, prevButton.Location.X / 50 - 2] % 10 != 0)
                                {
                                    butt[pressedButton.Location.Y / 50 - 1, pressedButton.Location.X / 50 + 1].BackgroundImage = null;
                                    butt[pressedButton.Location.Y / 50 - 1, pressedButton.Location.X / 50 + 1].Text = "";
                                    map[pressedButton.Location.Y / 50 - 1, pressedButton.Location.X / 50 + 1] = 0;
                                    if (currPlayer == 1)
                                    {
                                        pl1score++;
                                        label1.Text = "Score 1 :" + pl1score;
                                    }
                                    else
                                    {
                                        pl2score++;
                                        label2.Text = "Score 2 :" + pl2score;
                                    }
                                    DestroyDama = false;
                                }
                            }
                        }
                        if (InsideBorder(pressedButton.Location.Y / 50 + 1 , pressedButton.Location.X / 50 - 1))
                        {
                            if (InsideBorder(prevButton.Location.Y / 50 - 2 , prevButton.Location.X / 50 + 2))
                            {
                                if (map[prevButton.Location.Y / 50 - 2 , prevButton.Location.X / 50 + 2] % 10 != 0)
                                {
                                    butt[pressedButton.Location.Y / 50 + 1, pressedButton.Location.X / 50 - 1].BackgroundImage = null;
                                    butt[pressedButton.Location.Y / 50 + 1 , pressedButton.Location.X / 50 - 1].Text = "";
                                    map[pressedButton.Location.Y / 50 + 1 , pressedButton.Location.X / 50 - 1] = 0;
                                    if (currPlayer == 1)
                                    {
                                        pl1score++;
                                        label1.Text = "Score 1 :" + pl1score;
                                    }
                                    else
                                    {
                                        pl2score++;
                                        label2.Text = "Score 2 :" + pl2score;
                                    }
                                    DestroyDama = false;
                                }
                            }
                        }
                        if (InsideBorder(pressedButton.Location.Y / 50 - 1, pressedButton.Location.X / 50 - 1))
                        {
                            if (InsideBorder(prevButton.Location.Y / 50 + 2, prevButton.Location.X / 50 + 2))
                            {
                                if (map[prevButton.Location.Y / 50 + 2, prevButton.Location.X / 50 + 2] % 10 != 0)
                                {
                                    butt[pressedButton.Location.Y / 50 - 1, pressedButton.Location.X / 50 - 1].BackgroundImage = null;
                                    butt[pressedButton.Location.Y / 50 - 1, pressedButton.Location.X / 50 - 1].Text = "";
                                    map[pressedButton.Location.Y / 50 - 1, pressedButton.Location.X / 50 - 1] = 0;
                                    if (currPlayer == 1)
                                    {
                                        pl1score++;
                                        label1.Text = "Score 1 :" + pl1score;
                                    }
                                    else
                                    {
                                        pl2score++;
                                        label2.Text = "Score 2 :" + pl2score;
                                    }
                                    DestroyDama = false;
                                }
                            }
                        }
                        DestroyDama = false;
                    }
                    if (Destroy)
                    {
                        if(InsideBorder(pressedButton.Location.Y / 50 + 1 * dir, pressedButton.Location.X / 50 + 1) )
                        {
                            if (InsideBorder(prevButton.Location.Y / 50 - 2 * dir, prevButton.Location.X / 50 - 2))
                            {
                                if (map[prevButton.Location.Y / 50 - 2 * dir, prevButton.Location.X / 50 - 2] != 0 )
                                {
                                    butt[pressedButton.Location.Y / 50 + 1 * dir, pressedButton.Location.X / 50 + 1].BackgroundImage = null;
                                    butt[pressedButton.Location.Y / 50 + 1 * dir, pressedButton.Location.X / 50 + 1].Text = "";
                                    map[pressedButton.Location.Y / 50 + 1 * dir, pressedButton.Location.X / 50 + 1] = 0;
                                }
                            }
                            if (currPlayer == 1)
                            {
                                pl1score++;
                                label1.Text = "Score 1 :" + pl1score;
                            }
                            else
                            {
                                pl2score++;
                                label2.Text = "Score 2 :" + pl2score;
                            }
                        }
                        Destroy = false;
                    }
                    if (Destroy2)
                    {
                        if (InsideBorder(pressedButton.Location.Y / 50 + 1 * dir, pressedButton.Location.X / 50 - 1) )
                        {
                            if (InsideBorder(prevButton.Location.Y / 50 - 2 * dir, prevButton.Location.X / 50 + 2))
                            {
                                if (map[prevButton.Location.Y / 50 - 2 * dir, prevButton.Location.X / 50 + 2] != 0 )
                                {
                                    butt[pressedButton.Location.Y / 50 + 1 * dir, pressedButton.Location.X / 50 - 1].BackgroundImage = null;
                                    butt[pressedButton.Location.Y / 50 + 1 * dir, pressedButton.Location.X / 50 - 1].Text = "";
                                    map[pressedButton.Location.Y / 50 + 1 * dir, pressedButton.Location.X / 50 - 1] = 0;
                                }
                            }
                            if (currPlayer == 1)
                            {
                                pl1score++;
                                label1.Text = "Score 1 :" + pl1score;
                            }
                            else
                            {
                                pl2score++;
                                label2.Text = "Score 2 :" + pl2score;
                            }
                        }
                        Destroy2 = false;
                    }
                    ActivateButtons(); 
                    CloseSteps();
                    SwitchPlayer();
                    ShowWin();
                }
            }
            prevButton = pressedButton;
        }
        public void DeactivateButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    butt[i, j].Enabled = false;
                }
            }
        }
        public void ActivateButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    butt[i, j].Enabled = true;
                }
            }
        }
        private void Init()
        {
            label1.Text = "Score 1 :" + pl1score;
            label2.Text = "Score 2 :" + pl2score;
            map = new int[8, 8]
            {
                 {20,0,20,0,20,0,20,0 },
                 {0,20,0,20,0,20,0,20 },
                 {20,0,20,0,20,0,20,0 },
                 {0,0,0,0,0,0,0,0 },
                 {0,0,0,0,0,0,0,0 },
                 {0,10,0,10,0,10,0,10 },
                 {10,0,10,0,10,0,10,0 },
                 {0,10,0,10,0,10,0,10 },
            };
            CreateNewButt();
        }
    }
}
