using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OptimizeBersenhams_Line
{
    public partial class Form1 : Form
    {
        public bool clicked = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (clicked)
            {
                Graphics graphics = panel1.CreateGraphics();

                int x1 = Convert.ToInt32(textBox1.Text);
                int y1 = Convert.ToInt32(textBox2.Text);
                int x2 = Convert.ToInt32(textBox3.Text);
                int y2 = Convert.ToInt32(textBox4.Text);

                Point p1 = new Point(x1, y1);
                Point p2 = new Point(x2, y2);

                BersenhamLine(graphics, p1, p2);
            }
        }
        private void AdjustCoordinates(ref int x, ref int y)
        {
            if ((x >= 0 && y >= 0) || (x <= 0 && y >= 0) || (x <= 0 && y <= 0) || (x >= 0 && y <= 0))
            {
                x += (panel1.Width / 2);
                y = (panel1.Height / 2) - y;
            }
        }
        private void MakelineHorizontal(Bitmap pic, int x,int y,int x2,int y2)
        {
            while (x > x2)
            {
                x--;
                pic.SetPixel(x, y, Color.Red);
            }
            while (x < x2 - 1)
            {
                x++;
                pic.SetPixel(x, y, Color.Red);
            }
        }
        private void MakelineVertical(Bitmap pic, int x, int y, int x2, int y2)
        {
            while (y > y2)
            {
                y--;
                pic.SetPixel(x, y, Color.Red);
            }
            while (y < y2 - 1)
            {
                y++;
                pic.SetPixel(x, y, Color.Red);
            }
        }
        private void MakelineDiagonal(Bitmap pic, int x, int y, int x2, int y2,int dx,int dy)
        {
            int p = 2 * dy - dx;
            int i = 2 * dx - dy;
            int j = -2 * dy - dx;
            int c = 2 * dx + dy;
            while (x < x2 - 1 && y < y2)  //x inc,y inc,(dy / dx)<=1
            {
                if ((dy / dx) < 1)
                {
                    x++;
                    if (p >= 0)
                    {
                        y++;
                        p = p + 2 * dy - 2 * dx;
                        pic.SetPixel(x, y, Color.Blue);
                    }
                    else if (p < 0)
                    {
                        p = p + 2 * dy;
                        pic.SetPixel(x, y, Color.Red);
                    }
                }
                else if ((dy / dx) >= 1)
                {
                    y++;
                    if (i >= 0)
                    {
                        x++;
                        i = i + 2 * dx - 2 * dy;
                        pic.SetPixel(x, y, Color.Blue);
                    }
                    else if (i < 0)
                    {
                        i = i + 2 * dx;
                        pic.SetPixel(x, y, Color.Red);
                    }
                }
            }
            while (x > x2 && y > y2)  //x dec,y dec,(dy / dx)<=1
            {
                if ((dy / dx) < 1)
                {
                    x--;
                    if (p >= 0)
                    {
                        p = p + 2 * dy;
                        pic.SetPixel(x, y, Color.Blue);
                    }
                    else if (p < 0)
                    {
                        y--;
                        p = p + 2 * dy - 2 * dx;
                        pic.SetPixel(x, y, Color.Red);
                    }
                }
                else if ((dy / dx) >= 1)
                {
                    y--;
                    if (i > 0)
                    {
                        x--;
                        i = i + 2 * dy - 2 * dx;
                        pic.SetPixel(x, y, Color.Blue);
                    }
                    else if (i <= 0)
                    {
                        i = i - 2 * dx;
                        pic.SetPixel(x, y, Color.Red);
                    }
                }
            }
            while (x < x2 - 1 && y > y2 - 1)  //x inc,y dec,(dy / dx)>=-1
            {
                if ((dy / dx) > -1)
                {
                    x++;
                    if (j >= 0)
                    {
                        y--;
                        j = j - 2 * dy - 2 * dx;
                        pic.SetPixel(x, y, Color.Blue);
                    }
                    else if (j < 0)
                    {
                        j = j - 2 * dy;
                        pic.SetPixel(x, y, Color.Red);
                    }
                }
                else if ((dy / dx) <= -1)
                {
                    y--;
                    if (c >= 0)
                    {
                        x++;
                        c = c + 2 * dy + 2 * dx;
                        pic.SetPixel(x, y, Color.Blue);
                    }
                    else if (c < 0)
                    {
                        c = c + 2 * dx;
                        pic.SetPixel(x, y, Color.Red);
                    }
                }
            }
            while (x > x2 && y < y2 - 1)  //x dec,y inc,(dy / dx) >=-1
            {
                if ((dy / dx) > -1)
                {
                    x--;
                    if (c >= 0)
                    {
                        y++;
                        c = c + 2 * dx + 2 * dy;
                        pic.SetPixel(x, y, Color.Blue);
                    }
                    else if (c < 0)
                    {
                        c = c + 2 * dy;
                        pic.SetPixel(x, y, Color.Red);
                    }
                }
                else if ((dy / dx) <= -1)
                {
                    y++;
                    if (c >= 0) 
                    { 
                        c = c + 2 * dx;
                        pic.SetPixel(x, y, Color.Blue);
                    }
                    else if (c < 0)
                    {
                        x--;
                        c = c + 2 * dy + 2 * dx;
                        pic.SetPixel(x, y, Color.Red);
                    }
                }
            }
        }
        private void BersenhamLine(Graphics graphics, Point p1, Point p2)
        {
            int x, y, x2, y2, dy, dx;
            Bitmap pic = new Bitmap(panel1.Size.Width, panel1.Size.Height);

            x = p1.X;
            y = p1.Y;
            x2 = p2.X;
            y2 = p2.Y;

            AdjustCoordinates(ref x, ref y);
            AdjustCoordinates(ref x2, ref y2);

            dx = x2 - x;
            dy = y2 - y;

            pic.SetPixel(x, y, Color.Black);

            if (dy == 0)
            {
                MakelineHorizontal(pic,x, y, x2, y2);
            }
            else if (dx == 0)
            {
                MakelineVertical(pic, x, y, x2, y2);
            }
            else
            {
                MakelineDiagonal(pic, x, y, x2, y2,dx,dy);
            }
            graphics.DrawImage(pic, 0, 0);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            clicked = true;
            panel1.Invalidate();
        }
    }
}










