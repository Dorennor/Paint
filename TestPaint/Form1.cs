using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TestPaint
{
    public partial class Form1 : Form
    {
        
        [DllImport("User32.dll", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        private static extern IntPtr LoadCursorFromFile(String str);
        private bool mouseDown = false;
        private Point lastPoint = Point.Empty;
        private Graphics graghics;
        private Graphics graphicsBmp;
        private Pen pen;
        private Bitmap bmp;
        private Pen lastPen;

        public Form1()
        {
            InitializeComponent();
            Width = SystemInformation.PrimaryMonitorSize.Width;
            Height = SystemInformation.PrimaryMonitorSize.Height;  
            WindowState = FormWindowState.Maximized;

            panel1.Width = Convert.ToInt32(SystemInformation.PrimaryMonitorSize.Width * 0.988);
            panel1.Height = Convert.ToInt32(SystemInformation.PrimaryMonitorSize.Height * 0.15);

            pictureBox1.Width = Convert.ToInt32(SystemInformation.PrimaryMonitorSize.Width * 0.988);
            pictureBox1.Height = Convert.ToInt32(SystemInformation.PrimaryMonitorSize.Height * 0.8);

            button1.Width = Convert.ToInt32(panel1.Width * 0.1);
            button1.Height = Convert.ToInt32((panel1.Height - 10) * 0.3);
            button1.Location = panel1.Location;

            button2.Width = Convert.ToInt32(panel1.Width * 0.1);
            button2.Height = Convert.ToInt32((panel1.Height - 10) * 0.3);
            button2.Location = new Point(panel1.Location.X, panel1.Location.Y + button2.Height);

            button3.Width = Convert.ToInt32(panel1.Width * 0.1);
            button3.Height = Convert.ToInt32((panel1.Height - 10) * 0.3);
            button3.Location = new Point(panel1.Location.X, panel1.Location.Y + (button2.Height * 2));

            numericUpDown1.Width = Convert.ToInt32(panel1.Width * 0.05);
            numericUpDown1.Height = Convert.ToInt32((panel1.Height - 10) * 0.3);
            numericUpDown1.Location = new Point(Convert.ToInt32(panel1.Location.X + button1.Width * 1.1), panel1.Location.Y);

            button4.Width = Convert.ToInt32(panel1.Width * 0.05);
            button4.Height = Convert.ToInt32((panel1.Height - 10) * 0.3);
            button4.Location = new Point(Convert.ToInt32(panel1.Location.X + button1.Width * 1.1), panel1.Location.Y + button2.Height);

            button5.Width = Convert.ToInt32(panel1.Width * 0.05);
            button5.Height = Convert.ToInt32((panel1.Height - 10) * 0.3);
            button5.Location = new Point(Convert.ToInt32(panel1.Location.X + button1.Width * 1.1), panel1.Location.Y + (button2.Height * 2));

            groupBox1.Height = Convert.ToInt32(panel1.Height * 0.65);
            groupBox1.Width = Convert.ToInt32(panel1.Width * 0.7);
            groupBox1.Location = new Point(Convert.ToInt32(panel1.Location.X + (button1.Width * 2)), Convert.ToInt32(panel1.Location.Y * 1.2));

            radioButton1.Text = Color.Black.ToString();
            radioButton2.Text = Color.Bisque.ToString();
            radioButton3.Text = Color.Yellow.ToString();
            radioButton4.Text = Color.Violet.ToString();
            radioButton5.Text = Color.Green.ToString();
            radioButton6.Text = Color.Gold.ToString();
            radioButton7.Text = Color.Aqua.ToString();
            radioButton8.Text = Color.DarkRed.ToString();
            radioButton9.Text = Color.Red.ToString();
            radioButton10.Text = Color.Blue.ToString();
            radioButton11.Text = Color.Crimson.ToString();
            radioButton12.Text = Color.DarkKhaki.ToString();
            radioButton13.Text = Color.DarkViolet.ToString();
            radioButton14.Text = Color.Fuchsia.ToString();
            radioButton15.Text = Color.GreenYellow.ToString();
            radioButton16.Text = Color.LightGreen.ToString();
            radioButton17.Text = Color.Orange.ToString();
            radioButton18.Text = Color.Pink.ToString();
            radioButton19.Text = Color.Purple.ToString();
            radioButton20.Text = Color.Olive.ToString();
            radioButton21.Text = Color.Navy.ToString();
            radioButton22.Text = Color.Moccasin.ToString();
            radioButton23.Text = Color.OldLace.ToString();
            radioButton24.Text = Color.Lime.ToString();

            radioButton1.Checked = true;
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 15;

            graghics = pictureBox1.CreateGraphics();
            graghics.SmoothingMode = SmoothingMode.HighQuality;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphicsBmp = Graphics.FromImage(bmp);
            graphicsBmp.SmoothingMode = SmoothingMode.HighQuality;
            graphicsBmp.Clear(Color.White);
            pictureBox1.Cursor = new Cursor(LoadCursorFromFile(@"Pen.cur"));
            pen = new Pen(Color.Black, Convert.ToInt32(numericUpDown1.Value));
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pen.Width = Convert.ToInt32(numericUpDown1.Value);
            if (lastPoint.Equals(Point.Empty)) lastPoint = new Point(e.X, e.Y);
            if (mouseDown)
            {
                Point mousePosition = new Point(e.X, e.Y);
                graghics.DrawLine(pen, mousePosition, lastPoint);
                graphicsBmp.DrawLine(pen, mousePosition, lastPoint);
            }
            lastPoint = new Point(e.X, e.Y);
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            Pen p = new Pen(Color.Black, 8);
            e.Graphics.DrawRectangle(p, r);
            pictureBox1.Image = bmp;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(0, 0, panel1.Width, panel1.Height);
            Pen p = new Pen(Color.Black, 8);
            e.Graphics.DrawRectangle(p, r);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            graphicsBmp.Clear(Color.White);
            pictureBox1.Cursor = new Cursor(LoadCursorFromFile(@"Pen.cur"));
            pen = new Pen(Color.Black, Convert.ToInt32(numericUpDown1.Value));
            groupBox1.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save image as: ";
            save.OverwritePrompt = true;
            save.CheckPathExists = true;

            save.Filter = "Image Files(*.JPG)|*.JPG|All files (*.*)|*.*";
            save.ShowHelp = true;
            
            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    bmp.Save(save.FileName, ImageFormat.Jpeg);
                }
                catch
                {
                    MessageBox.Show("Can't save this file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.PNG; *.JPG)|*.PNG; *.JPG|All files (*.*)|*.*";

            if(open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap temp = new Bitmap(open.FileName);
                    pictureBox1.Image = new Bitmap(open.FileName);
                    bmp = new Bitmap(open.FileName);
                    graphicsBmp = Graphics.FromImage(bmp);
                    graphicsBmp.SmoothingMode = SmoothingMode.HighQuality;
                }
                catch
                {
                    MessageBox.Show("Can't open this file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (lastPen != null)
            {
                pen = new Pen(lastPen.Brush, Convert.ToInt32(numericUpDown1.Value));
            }
            else
            {
                pen = new Pen(Color.Black, Convert.ToInt32(numericUpDown1.Value));
            }
            pictureBox1.Cursor = new Cursor(LoadCursorFromFile(@"Pen.cur"));
            groupBox1.Enabled = true;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            lastPen = pen;
            pen = new Pen(Color.White, Convert.ToInt32(numericUpDown1.Value));
            pictureBox1.Cursor = new Cursor(LoadCursorFromFile(@"Rubber.cur"));
            groupBox1.Enabled = false;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Black, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Bisque, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Yellow, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Violet, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Green, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Gold, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Aqua, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.DarkRed, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton9_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Red, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton10_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Blue, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton11_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Crimson, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton12_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.DarkKhaki, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton13_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.DarkViolet, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton14_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Fuchsia, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton15_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.GreenYellow, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton16_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.LightGreen, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton17_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Orange, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton18_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Pink, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton19_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Purple, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton20_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Olive, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton21_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Navy, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton22_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Moccasin, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton23_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.OldLace, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }

        private void RadioButton24_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Lime, Convert.ToInt32(numericUpDown1.Value));
            lastPen = pen;
        }
    }
}
