using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Rama
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            pictureBox1.Image = (Bitmap)bmp;
            Pen azul = new Pen(Color.Blue, 2);
            Pen roja = new Pen(Color.Red, 3);
            g.Clear(Color.White);
            g.DrawLine(roja, 10, pictureBox1.Height / 2, pictureBox1.Width - 10, pictureBox1.Height / 2);
            g.DrawLine(azul, 10, 10, 15, 300);
            g.DrawLine(azul, 15, 300, 20, 50);
            g.DrawLine(azul, 20, 50, 25, 250);
            g.DrawLine(azul, 25, 250, 30, 120);
            g.DrawLine(azul, 30, 120, 35, 230);
            pictureBox1.Image.Save("Grafico.png", ImageFormat.Png);
        }

        private void Form6_ResizeEnd(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
