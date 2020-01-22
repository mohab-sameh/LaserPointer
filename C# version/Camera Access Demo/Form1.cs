using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Camera_Access_Demo
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;
        private bool startedDrawing = false;

        List<draw> dlist = new List<draw>();

        public class draw
        {
            public float x, y;
        }

        public Form1()
        {
            InitializeComponent();
            Run();
        }

        private void Run()
        {
            try
            {
                capture = new VideoCapture();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
            Application.Idle += ProcessFrame;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            Image<Bgr, byte> imgeOrigenal = capture.QueryFrame().ToImage<Bgr, byte>();

            Image<Gray, Byte> grayImage = imgeOrigenal.Convert<Gray, Byte>();

            imageBox1.Image = grayImage;
            getHigherPixel(grayImage);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void getHigherPixel(Image<Gray, Byte> image)
        {
            Bitmap c = new Bitmap(image.Bitmap);
            int px = 0, py = 0;

            double[] maxValues;
            Point[] maxLocations;

            image.MinMax(out _, out maxValues, out _, out maxLocations);
            px = maxLocations[0].X;
            py = maxLocations[0].Y;

            float v = c.GetPixel(px, py).GetBrightness();

            if (v >= 0.933f && v <= 0.936f)
            {
                float radius = 10;
                float x1 = px - radius;
                float y1 = py - radius;
                float width = 2 * radius;
                float height = 2 * radius;

                if (!startedDrawing)
                {
                    startedDrawing = true;
                    // MessageBox.Show("starting");
                    dlist.Clear();
                    this.Text = "starting";
                }

                if(startedDrawing)
                {
                    draw pnn = new draw();
                    pnn.x = px;
                    pnn.y = py;
                    dlist.Add(pnn);
                }

                using (Graphics g = Graphics.FromImage(c))
                {
                    Pen p = new Pen(Color.Red, 2);

                    if(startedDrawing)
                    {
                        for (int i = 1; i < dlist.Count; i++)
                        {
                            g.DrawLine(p, dlist[i - 1].x, dlist[i - 1].y, dlist[i].x, dlist[i].y);
                        }
                    }

                    g.DrawEllipse(p, x1, y1, width, height);
                }

                Image<Bgr, Byte> myImage2 = new Image<Bgr, Byte>(c);
                imageBox1.Image = myImage2;

            }
            else
            {
                if (startedDrawing)
                {
                    startedDrawing = false;
                    this.Text = "stoped";

                   // MessageBox.Show("stoped");
                }
            }

            this.Text = v.ToString();

        }

    }
}
