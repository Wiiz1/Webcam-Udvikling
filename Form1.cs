using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageStuff
{
    public partial class Form1 : Form
    {
        WebCam webCam;
        Stack<Bitmap> imageStack;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webCam = new WebCam();
            webCam.InitializeWebCam(ref pictureBox1);
            Thread.Sleep(2000);
            imageStack = new Stack<Bitmap>();

            webCam.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Vil du lukke det her pissefede program?", "Er du sikker?", MessageBoxButtons.YesNo);
            if(dr==DialogResult.No)
            {
                e.Cancel = true;
            }

            else
            {
                webCam.Stop();
            }
        }

        private void buttonCapture_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image!=null)
            {
                imageStack.Push(new Bitmap(pictureBox2.Image));
            }
            pictureBox2.Image = (Image)pictureBox1.Image.Clone();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonGray_Click(object sender, EventArgs e)
        {
            imageStack.Push(new Bitmap(pictureBox2.Image));
            Bitmap bt = new Bitmap(pictureBox2.Image);
            for (int y = 0; y < bt.Height; y++)
            {
                for (int x = 0; x < bt.Width; x++)
                {
                    Color c = bt.GetPixel(x, y);
                    int avg = (c.R + c.G + c.B) / 3;
                    bt.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                }
            }
            pictureBox2.Image = bt;
        }
    }
}
