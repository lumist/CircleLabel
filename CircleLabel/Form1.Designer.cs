using System.Drawing;

namespace CircleLabel
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Tool = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Cell = new System.Windows.Forms.Panel();
            this.Command = new System.Windows.Forms.PictureBox();
            this.Scopebox = new System.Windows.Forms.PictureBox();
            this.CellW = new System.Windows.Forms.PictureBox();
            this.Tool.SuspendLayout();
            this.Cell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Command)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scopebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CellW)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 33;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Tool
            // 
            this.Tool.BackgroundImage = global::CircleLabel.Properties.Resources.menu;
            this.Tool.Controls.Add(this.button7);
            this.Tool.Controls.Add(this.button6);
            this.Tool.Controls.Add(this.button4);
            this.Tool.Controls.Add(this.button5);
            this.Tool.Controls.Add(this.button3);
            this.Tool.Controls.Add(this.button2);
            this.Tool.Controls.Add(this.button1);
            this.Tool.Location = new System.Drawing.Point(1, 183);
            this.Tool.Name = "Tool";
            this.Tool.Size = new System.Drawing.Size(180, 33);
            this.Tool.TabIndex = 1;
            // 
            // button7
            // 
            this.button7.BackgroundImage = global::CircleLabel.Properties.Resources.b_sc1;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button7.Location = new System.Drawing.Point(143, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(23, 23);
            this.button7.TabIndex = 6;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.BackgroundImage = global::CircleLabel.Properties.Resources.b_e;
            this.button6.Location = new System.Drawing.Point(120, 5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(23, 23);
            this.button6.TabIndex = 5;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImage = global::CircleLabel.Properties.Resources.b_k;
            this.button4.Location = new System.Drawing.Point(74, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(23, 23);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackgroundImage = global::CircleLabel.Properties.Resources.b_p;
            this.button5.Location = new System.Drawing.Point(5, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(23, 23);
            this.button5.TabIndex = 4;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::CircleLabel.Properties.Resources.b_s;
            this.button3.Location = new System.Drawing.Point(51, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(23, 23);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::CircleLabel.Properties.Resources.b_c;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Location = new System.Drawing.Point(28, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 23);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::CircleLabel.Properties.Resources.b_b;
            this.button1.Location = new System.Drawing.Point(97, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cell
            // 
            this.Cell.AllowDrop = true;
            this.Cell.BackColor = System.Drawing.SystemColors.Control;
            this.Cell.BackgroundImage = global::CircleLabel.Properties.Resources.bk;
            this.Cell.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Cell.Controls.Add(this.Command);
            this.Cell.Controls.Add(this.Scopebox);
            this.Cell.Controls.Add(this.CellW);
            this.Cell.Location = new System.Drawing.Point(1, 1);
            this.Cell.Margin = new System.Windows.Forms.Padding(0);
            this.Cell.Name = "Cell";
            this.Cell.Size = new System.Drawing.Size(180, 180);
            this.Cell.TabIndex = 0;
            this.Cell.DragDrop += new System.Windows.Forms.DragEventHandler(this.Cell_DragDrop);
            this.Cell.DragOver += new System.Windows.Forms.DragEventHandler(this.Cell_DragOver);
            // 
            // Command
            // 
            this.Command.BackgroundImage = global::CircleLabel.Properties.Resources.hotkey;
            this.Command.Enabled = false;
            this.Command.Location = new System.Drawing.Point(43, 77);
            this.Command.Name = "Command";
            this.Command.Size = new System.Drawing.Size(100, 90);
            this.Command.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Command.TabIndex = 2;
            this.Command.TabStop = false;
            // 
            // Scopebox
            // 
            this.Scopebox.Enabled = false;
            this.Scopebox.Location = new System.Drawing.Point(43, 21);
            this.Scopebox.Name = "Scopebox";
            this.Scopebox.Size = new System.Drawing.Size(100, 50);
            this.Scopebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Scopebox.TabIndex = 1;
            this.Scopebox.TabStop = false;
            // 
            // CellW
            // 
            this.CellW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CellW.Location = new System.Drawing.Point(3, 3);
            this.CellW.Margin = new System.Windows.Forms.Padding(0);
            this.CellW.Name = "CellW";
            this.CellW.Size = new System.Drawing.Size(176, 176);
            this.CellW.TabIndex = 0;
            this.CellW.TabStop = false;
            this.CellW.Paint += new System.Windows.Forms.PaintEventHandler(this.CellW_Paint);
            this.CellW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CellW_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 216);
            this.Controls.Add(this.Tool);
            this.Controls.Add(this.Cell);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Label";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.Tool.ResumeLayout(false);
            this.Cell.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Command)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scopebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CellW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Tool;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;

        /* 窗体参数 */
        private string FileDir = "";
        private string FileName = "";
        private int[] Cell_Init = new int[8];
        private bool if_open = false;
        private int Paint_Status = 0;
        private Graphics g;

        /* 倍镜参数 */
        private Bitmap Originalimg = new Bitmap(1,1);
        private int[,] OriImg = new int[1, 1];
        private const int mag = 4;
        private int mx, my = 0;
        private const int winW = 200;
        private const int winH = 200;
        private int winX = 43;
        private int winY = 54;
        private bool starflag = false;

        /* 边缘吸附 */
        private int[,] img = new int[winW / mag, winH / mag];
        private int[,] sobelx = new int[winW / mag, winH / mag];
        private int[,] sobely = new int[winW / mag, winH / mag];
        private double[,] sobel = new double[winW / mag, winH / mag];

        /* 画圆参数 */
        private int FormX = 0;
        private int FormY = 0;
        private double cthp = 0.03;
        private int[,] Edge = new int[11, 2]; // 最多支持10个检测点
        private int[,] Center = new int[500, 2]; // 全图最多label 500个圆
        private double[] Radius = new double[500];
        private int Edge_Cursor = 0;
        private int Circle_Cursor = 0;
        private Pen pen;
        private System.Windows.Forms.PictureBox CellW;
        private System.Windows.Forms.Panel Cell;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.PictureBox Scopebox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox Command;
    }
}

