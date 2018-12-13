using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.InteropServices;


namespace CircleLabel
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);
        public Form1()
        {
            InitializeComponent();

            Scopebox.Width = 1;
            Scopebox.Height = 1;
            Scopebox.Location = Tool.Location;
            Command.Width = 1;
            Command.Height = 1;
            Command.Location = Tool.Location;

            timer1.Stop();
            timer1.Enabled = false;

            Cell_Init[0] = this.Cell.Height;
            Cell_Init[1] = this.Cell.Width;
            Cell_Init[2] = this.Height;
            Cell_Init[3] = this.Width;
            Cell_Init[4] = this.Cell.Location.X;
            Cell_Init[5] = this.Cell.Location.Y;
            Cell_Init[6] = this.Tool.Location.X;
            Cell_Init[7] = this.Tool.Location.Y;

            g = this.Cell.CreateGraphics();
            pen = new Pen(Color.Red);
        }

        private void Cell_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void Cell_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i <= file.Length - 1; i++)
                {
                    int j = 0, A = file[i].IndexOf("\\"), B = file[i].IndexOf(".");
                    while(A>0)
                    {
                        j = A;
                        A = file[i].IndexOf("\\", j+1);
                    }
                    FileDir = file[i].Remove(j);
                    FileName = file[i].Substring(j+1,B-j-1);
                    if (System.IO.File.Exists(file[i]))
                    {
                        if (if_open)
                        {
                            DialogResult dr1 = MessageBox.Show(this,@"结束本次批注并保存批注结果？", @"关闭", MessageBoxButtons.YesNoCancel);
                            if (dr1 == DialogResult.Cancel)
                            {
                                break;
                            }
                            else if (dr1 == DialogResult.Yes)
                            {
                                button3.PerformClick();
                            }
                            else
                            {
                                ;
                            }
                        }
                        // 裁剪图像
                        Originalimg = new Bitmap(Image.FromFile(file[i]));
                        int OriWidth = Originalimg.Width;
                        int OriHeight = Originalimg.Height - 59;
                        OriImg = new int[OriHeight, OriWidth];
                        for (int it1 = 0; it1 < OriHeight; it1++)
                        {
                            for (int it2 = 0; it2 < OriWidth; it2++)
                            {
                                OriImg[it1, it2] = Originalimg.GetPixel(it2, it1).B;
                            }
                        }
                        Originalimg = new Bitmap(OriWidth, OriHeight);
                        for (int it1 = 0; it1 < OriHeight; it1++)
                        {
                            for (int it2 = 0; it2 < OriWidth; it2++)
                            {
                                Originalimg.SetPixel(it2, it1,Color.FromArgb(OriImg[it1, it2] , OriImg[it1, it2] , OriImg[it1, it2] ));
                            }
                        }

                        Circle_Cursor = 0;
                        Edge_Cursor = 0;
                        /*
                         *  * ** ******* ** *
                         *  * C* ******* ** *
                         *  * ** ******* ** *
                         *  * ** Wx...xW ** *
                         *  * ** xx...xx ** *
                         *  . .. ....... .. .
                         *  . .. ....... .. .
                         *  . .. ....... .. .
                         *  * ** xx...xx ** *
                         *  * ** Wx...xW ** *
                         *  * ** ******* ** *
                         *  * ** ******* ** *
                         *  * ** ******* ** *
                         *  (4,4) is upperleft point location
                         */
                        this.Width = Cell_Init[3] - Cell_Init[1] + OriWidth + 2;
                        this.Height = Cell_Init[2] - Cell_Init[0] + OriHeight;//- 59
                        this.Cell.Width = OriWidth + 4;
                        this.Cell.Height = OriHeight + 4;
                        //this.Cell.Width = this.Cell.BackgroundImage.Width + 4;
                        //this.Cell.Height = (this.Cell.BackgroundImage.Height - 59) + 4;//- 59
                        this.CellW.Location  = new Point(2,2);
                        this.CellW.Width = OriWidth;
                        this.CellW.Height = OriHeight;//- 59
                        this.CellW.BackgroundImage = Originalimg;
                        this.Tool.Location = new System.Drawing.Point(this.Cell.Location.X + this.Cell.Width - 1 - this.Tool.Width, this.Cell.Location.Y + this.Cell.Height);//
                        this.Location = new System.Drawing.Point((Screen.GetWorkingArea(this).Width - this.Width) / 2, (Screen.GetWorkingArea(this).Height - this.Height) / 2);
                        if_open = true;
                        break;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Cell.BackgroundImage = global::CircleLabel.Properties.Resources.bk;
            this.Width = Cell_Init[3];
            this.Height = Cell_Init[2];
            this.Cell.Width = this.Cell.BackgroundImage.Width;
            this.Cell.Height = this.Cell.BackgroundImage.Height;
            this.Tool.Location = new System.Drawing.Point(this.Cell.Location.X + this.Cell.BackgroundImage.Width - this.Tool.Width, this.Cell.Location.Y + this.Cell.BackgroundImage.Height);
            if_open = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            Paint_Status = 2;
        }

        private void CellW_MouseDown(object sender, MouseEventArgs e)
        {
            if (Paint_Status == 2)
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {
                    // 左键单击
                    if (Edge_Cursor >= 10)
                    {
                        //大于10个点
                        Paint_Status = 20;
                        CellW.Invalidate();
                    }
                    else
                    {
                        //Edge[Edge_Cursor, 0] = Cell.PointToClient(Control.MousePosition).X;
                        //Edge[Edge_Cursor, 1] = Cell.PointToClient(Control.MousePosition).Y;
                        //修正十字坐标中心位置
                        Edge[Edge_Cursor, 0] = Cell.PointToClient(Control.MousePosition).X - 2;
                        Edge[Edge_Cursor, 1] = Cell.PointToClient(Control.MousePosition).Y - 2;
                        Edge_Cursor += 1;
                        CellW.Invalidate();
                    }
                }
                else if (e.Button == MouseButtons.Right && e.Clicks == 1 && Edge_Cursor>2)
                {
                    // 右键单击
                    Paint_Status = 20;
                    CellW.Invalidate();
                }
            }
            else if (Paint_Status == 3)
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {
                    // 左键单击
                    if ( Circle_Cursor > 0 )
                    {
                        int X = Cell.PointToClient(Control.MousePosition).X - 2;
                        int Y = Cell.PointToClient(Control.MousePosition).Y - 2;
                        int MatchIndex = 0;
                        double MatchPercent = 0;
                        for (int i = 0; i < Circle_Cursor; i++)
                        {
                            double mp = Math.Sqrt((X - Center[i, 0]) * (X - Center[i, 0]) + (Y - Center[i, 1]) * (Y - Center[i, 1]));
                            if (mp <= Radius[i])
                            {
                                mp = 1 - mp / Radius[i];
                            }
                            else
                            {
                                mp = 0.0;
                            }
                            if (MatchPercent < mp)
                            {
                                MatchPercent = mp;
                                MatchIndex = i;
                            }
                        }
                        if (MatchPercent >= 0.01)
                        {
                            if (MatchIndex == Circle_Cursor - 1)
                            {
                                Circle_Cursor--;
                            }
                            else
                            {
                                int[] CMI = {Center[MatchIndex, 0],Center[MatchIndex, 1]};
                                double RMI = Radius[MatchIndex];
                                Center[MatchIndex, 0] = Center[Circle_Cursor - 1, 0];
                                Center[MatchIndex, 1] = Center[Circle_Cursor - 1, 1];
                                Radius[MatchIndex] = Radius[Circle_Cursor - 1];
                                Center[Circle_Cursor-1,0] = CMI[0];
                                Center[Circle_Cursor-1,1] = CMI[1];
                                Radius[--Circle_Cursor] = RMI;
                            }
                        }
                        CellW.Invalidate();
                    }
                }
            }
        }

        public static int[] RollDice(int NumSides)
        {
            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[2];
            int[] rand = new int[3];

            

            // Convert the byte to an integer value to make the modulus operation easier.
            while (true)
            {
                for (int i = 0; i < 3; i++)
                {

                    // Create a new instance of the RNGCryptoServiceProvider. 
                    RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();

                    // Fill the array with a random value.
                    Gen.GetBytes(randomNumber);

                    rand[i] = (Convert.ToInt32(randomNumber[0]) * Convert.ToInt32(randomNumber[1])) % NumSides;
                }
                if (!(rand[0] == rand[1] || rand[1] == rand[2] || rand[0] == rand[2]))
                    break;
            }

            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return rand;
        }

        private void GetCircle(int[] x, int[] y, out int centerx, out int centery, out double radius)
        {
            centerx = (x[1] * x[1] + y[1] * y[1]) * (y[2] - y[3]) + (x[2] * x[2] + y[2] * y[2]) * (y[3] - y[1]) + (x[3] * x[3] + y[3] * y[3]) * (y[1] - y[2]);
            centerx /= (2 * (x[1] * (y[2] - y[3]) - y[1] * (x[2] - x[3]) + x[2] * y[3] - x[3] * y[2]));

            centery = (x[1] * x[1] + y[1] * y[1]) * (x[3] - x[2]) + (x[2] * x[2] + y[2] * y[2]) * (x[1] - x[3]) + (x[3] * x[3] + y[3] * y[3]) * (x[2] - x[1]);
            centery /= (2 * (x[1] * (y[2] - y[3]) - y[1] * (x[2] - x[3]) + x[2] * y[3] - x[3] * y[2]));

            radius = Math.Sqrt((centerx - x[1]) * (centerx - x[1]) + (centery - y[1]) * (centery - y[1]));
        }

        private void DrawCircle(Graphics g, Pen p, int cx, int cy, double r)
        {
            g.DrawEllipse(p, Convert.ToInt32(cx-r), Convert.ToInt32(cy-r), Convert.ToInt32(r*2), Convert.ToInt32(r*2));
        }

        private double Ransc(int x, int y, double r, double thp)
        {

            double[] Rrange = new double[2];
            double ri;
            int count = 0;
            Rrange[0] = r * (1 - thp);
            Rrange[1] = r * (1 + thp);

            for (int i = 0; i < Edge_Cursor; i++)
            {
                ri = Math.Sqrt((Edge[i, 0] - x) * (Edge[i, 0] - x) + (Edge[i, 1] - y) * (Edge[i, 1] - y));
                if (ri <= Rrange[1] && ri >= Rrange[0])
                    count++;
            }
            return 1.0 * count / Edge_Cursor;
        }

        private void CellW_Paint(object sender, PaintEventArgs e)
        {
            mx = this.PointToClient(Control.MousePosition).X;
            my = this.PointToClient(Control.MousePosition).Y;
            if (Paint_Status == 2 || Paint_Status == 3 || Paint_Status == 4 )
            {
                Graphics g = e.Graphics;
                Pen p = new Pen(Color.Blue, 2);
                for (int i = 0; i < Edge_Cursor; i++)
                {
                    DrawCircle(g, p, Edge[i, 0], Edge[i, 1], 1.5);
                    //g.DrawEllipse(p, Edge[i, 0], Edge[i, 1], 3, 3);
                }
                for (int i = 0; i < Circle_Cursor; i++)
                {
                    DrawCircle(g, p, Center[i, 0], Center[i, 1], Convert.ToInt32(Radius[i]));
                }
            }
            else if (Paint_Status == 5)
            {
                Graphics g = e.Graphics;
                Bitmap b = new Bitmap(this.Width, this.Height);
                Pen p = new Pen(Color.Blue, 2);
                for (int i = 0; i < Circle_Cursor; i++)
                {
                    DrawCircle(g, p, Center[i, 0], Center[i, 1], Convert.ToInt32(Radius[i]));
                }
                this.DrawToBitmap(b, new Rectangle(0, 0, this.Width, this.Height));
                b.Save(FileDir+"//____"+FileName+"(Back).jpg", System.Drawing.Imaging.ImageFormat.Jpeg); 
            }
            else if (Paint_Status == 20)
            {
                // RANSC
                int Times = Edge_Cursor;
                int[] Comb = new int[3];
                int[] x = new int[4];
                int[] y = new int[4];
                int centerx, centery;
                int Outcx = 0, Outcy = 0;
                double r;
                double Outr = 0.0;
                double Maxpro = 0.0, pro = 0.0;

                for (int it = 0; it < Times; it++)
                {
                    Comb = RollDice(Edge_Cursor);
                    for (int iit = 0; iit < 3; iit++)
                    {
                        x[iit + 1] = Edge[Comb[iit], 0];
                        y[iit + 1] = Edge[Comb[iit], 1];
                    }
                    GetCircle(x, y, out centerx, out centery, out r);
                    pro = Ransc(centerx, centery, r, cthp);
                    if (Maxpro < pro)
                    {
                        Maxpro = pro;
                        Outcx = centerx;
                        Outcy = centery;
                        Outr = r;
                    }
                }
                Center[Circle_Cursor, 0] = Outcx;
                Center[Circle_Cursor, 1] = Outcy;
                Radius[Circle_Cursor++] = Outr;

                // 重绘已有结果
                Edge_Cursor = 0;
                Paint_Status = 2;
                Graphics g = e.Graphics;
                Pen p = new Pen(Color.Red, 2);
                for (int i = 0; i < Circle_Cursor; i++)
                {
                    DrawCircle(g, p, Center[i, 0], Center[i, 1], Convert.ToInt32(Radius[i]));
                }
            }
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            this.FormX = this.Location.X;
            this.FormY = this.Location.Y;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
                        button4.PerformClick();
                        break;
                    case Keys.S:
                        e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
                        button3.PerformClick();
                        break;
                    case Keys.Z:
                        e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
                        button1.PerformClick();
                        break;
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.B)
            {
                e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
                button2.PerformClick();
            }
            else if (e.KeyCode == Keys.Z)
            {
                if (starflag)
                {
                    // 地毯扫描曼哈顿距离为12以内的最近点
                    int X = winW/2/mag;
                    int Y = winH/2/mag;
                    int rX = X;
                    int rY = Y;
                    bool flag = false;
                    for (int i = 1; i < winW/mag/2 && !flag; i+=2)
                    {
                        for (int j = -i; j <= i && !flag; j++)
                        {
                            if (sobel[X + j, Y - i] > 0.5)
                            {
                                rX = X + j;
                                rY = Y - i;
                                flag = true;
                                break;
                            }
                            if (sobel[Y - i, X + j] > 0.5)
                            {
                                rY = X + j;
                                rX = Y - i;
                                flag = true;
                                break;
                            }
                            if (sobel[X - j, Y + i] > 0.5)
                            {
                                rX = X - j;
                                rY = Y + i;
                                flag = true;
                                break;
                            }
                            if (sobel[Y + i, X - j] > 0.5)
                            {
                                rY = X - j;
                                rX = Y + i;
                                flag = true;
                                break;
                            }
                        }
                    }
                    SetCursorPos(Cursor.Position.X-(X-rX), Cursor.Position.Y-(Y-rY));
                }
            }
            else if (e.KeyCode == Keys.F1)
            {
                Command.Enabled = true;
                Command.Width = 150;
                Command.Height = 150;
                Command.Location = new Point(Tool.Location.X, Tool.Location.Y - 200);
            }
            else if (e.Shift)
            {
                Scopebox.Enabled = true;
                timer1.Enabled = true;
                timer1.Start();
                starflag = true;
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Paint_Status == 2 || Paint_Status == 4 )
            {
                if (Edge_Cursor > 0)
                {
                    // remove a point
                    Edge_Cursor -= 1;
                    if (Circle_Cursor < 0)
                        Circle_Cursor = 0;
                    CellW.Invalidate();
                }
                else
                {
                    // remove a circle
                    Circle_Cursor -= 1;
                    if (Circle_Cursor < 0)
                        Circle_Cursor = 0;
                    CellW.Invalidate();
                }
            }
            if (Paint_Status == 3)
            {
                // restore last circle
                if (Radius[Circle_Cursor] > 0.01)
                {
                    Circle_Cursor++;
                }
                CellW.Invalidate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = FileDir + "//" + FileName + ".txt";
            string splitstr = ",";
            int i = 0;
            //重命名
            while (System.IO.File.Exists(filename))
                filename = FileDir + "//" + FileName + "(" + Convert.ToString(i++) + ").txt";
            
            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(Convert.ToString(Circle_Cursor));
            for (i = 0; i < Circle_Cursor; i++)
            {
                sw.WriteLine(Convert.ToString(i + 1) + splitstr + Convert.ToString(Center[i, 0]) + splitstr + Convert.ToString(Center[i, 1]) + splitstr + Convert.ToString(Radius[i]));
            }

            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = 0;
            string FileCDir = FileDir + "//" + FileName + ".txt"; // no more files
            string FileCDir1 = FileDir + "//" + FileName + "(" + Convert.ToString(i) + ").txt";
            if (if_open)
            {   
                while(System.IO.File.Exists(FileCDir1))
                {
                    FileCDir1 = FileDir + "//" + FileName + "(" + Convert.ToString(++i) + ").txt";
                }
                if ( i > 0 )
                {
                    FileCDir1 = FileDir + "//" + FileName + "(" + Convert.ToString(--i) + ").txt";
                }
                else if (System.IO.File.Exists(FileCDir))
                {
                    FileCDir1 = FileCDir;
                }
                if (System.IO.File.Exists(FileCDir1))
                {
                    FileStream fs = new FileStream(FileCDir1, FileMode.Open);
                    StreamReader sw = new StreamReader(fs);
                    Circle_Cursor = Convert.ToInt32(sw.ReadLine());
                    for (i = 0; i < Circle_Cursor; i++)
                    {
                        string[] p = new string[4];
                        p = sw.ReadLine().Split(',');
                        Center[i, 0] = Convert.ToInt32(p[1]);
                        Center[i, 1] = Convert.ToInt32(p[2]);
                        Radius[i] = Convert.ToDouble(p[3]);
                    }
                    Paint_Status = 4;
                    CellW.Invalidate();
                    sw.Close();
                    fs.Close();
                }
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            Edge_Cursor = 0;
            CellW.Invalidate();
            Paint_Status = 3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Edge_Cursor = 0;
            CellW.Invalidate();
            Paint_Status = 5;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Color nc = new Color();
            double edgemax = -1.0;
            mx = Cursor.Position.X;
            my = Cursor.Position.Y;
            //对图像进行放大显示　        
            Bitmap bt = new Bitmap(winW/mag, winH/mag);
            Graphics g = Graphics.FromImage(bt);
            g.CopyFromScreen(
                     new Point(mx - winW/(2*mag),
                               my - winH/(2*mag)),
                     new Point(0,0),
                     new Size(winW / mag, winH / mag));

            // 灰度+边缘处理
            // 屏幕取图
            //for (int i = 0; i < bt.Height; i++)
            //{
            //    for (int j = 0; j < bt.Width; j++)
            //    {
            //        nc = bt.GetPixel(i, j);
            //        //img[i, j] = (int)(0.3 * nc.R + 0.59 * nc.G + 0.11 * nc.B);
            //        img[i, j] = nc.B; //本身就是灰度图
            //    }
            //}
            // 采原图
            for (int i = 0; i < bt.Height; i++)
            {
                for (int j = 0; j < bt.Width; j++)
                {
                    nc = bt.GetPixel(i, j);
                    //img[i, j] = (int)(0.3 * nc.R + 0.59 * nc.G + 0.11 * nc.B);
                    img[i, j] = nc.B; //本身就是灰度图
                }
            }
            for (int i = 1; i < bt.Height - 1; i++)
            {
                for (int j = 1; j < bt.Width - 1; j++)
                {
                    sobelx[i, j] = img[i - 1, j + 1] + 2 * img[i, j + 1] + img[i + 1, j + 1] - img[i - 1, j - 1] - 2 * img[i, j - 1] - img[i + 1, j - 1];
                    sobely[i, j] = img[i - 1, j - 1] + 2 * img[i - 1, j] + img[i - 1, j + 1] - img[i + 1, j - 1] - img[i + 1, j] * 2 - img[i + 1, j + 1];
                    sobel[i, j] = Math.Sqrt(sobelx[i, j] * sobelx[i, j] + sobely[i, j] * sobely[i, j]);
                    edgemax = sobel[i, j] > edgemax ? sobel[i, j] : edgemax;
                }
            }
            for (int i = 1; i < bt.Height - 1; i++)
            {
                for (int j = 1; j < bt.Width - 1; j++)
                {
                    sobel[i, j] = sobel[i, j] / edgemax;
                    if (sobel[i, j] > 0.4)
                    {
                        sobel[i, j] = 1;
                        bt.SetPixel(i, j, Color.Red);
                    }
                    else
                    {
                        sobel[i, j] = 0;
                    }
                }
            }            

            g.DrawLine(new Pen(Color.Black), winW / (2 * mag) - 5, winH / (2 * mag), winW / (2 * mag) + 5, winH / (2 * mag));
            g.DrawLine(new Pen(Color.Black), winW / (2 * mag), winH / (2 * mag) - 5, winW / (2 * mag), winH / (2 * mag) + 5);
            IntPtr dc1 = g.GetHdc();
            g.ReleaseHdc(dc1);
            this.Scopebox.Image = (Image)bt;
            this.Scopebox.Width = winW;
            this.Scopebox.Height = winH;
            mx = this.PointToClient(Control.MousePosition).X;
            my = this.PointToClient(Control.MousePosition).Y;
            if (!(mx < winX + 2 * winW && my < winY + 2 * winH))
            {
                this.Scopebox.Location = new System.Drawing.Point(winX, winY);
            }
            else
            {
                this.Scopebox.Location = new System.Drawing.Point(this.Cell.Width - winW - winX, winY);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                this.Scopebox.Enabled = false;
                this.timer1.Enabled = false;
                this.Scopebox.Width = 1;
                this.Scopebox.Height = 1;
                this.Scopebox.Location = Tool.Location;
                starflag = false;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                Command.Enabled = false;
                Command.Width = 1;
                Command.Height = 1;
                Command.Location = Tool.Location;
            }
        }
    }
}
