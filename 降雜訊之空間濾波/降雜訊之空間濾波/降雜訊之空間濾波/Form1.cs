using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 降雜訊之空間濾波
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image image;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = image;
            }
            int height = pictureBox1.Image.Height;
            int width = pictureBox1.Image.Width;
            Bitmap newbitmap = new Bitmap(width, height);
            Bitmap oldbitmap = (Bitmap)pictureBox1.Image;
            Color pixel;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pixel = oldbitmap.GetPixel(x, y);
                    int r, g, b, result;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    result = (299 * r + 587 * g + 114 * b) / 1000;
                    newbitmap.SetPixel(x, y, Color.FromArgb(result, result, result));
                }
            }
            pictureBox1.Image = newbitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            try
            {
                int height = pictureBox1.Image.Height;
                int width = pictureBox1.Image.Width;
                Bitmap newbitmap = new Bitmap(width, height);
                Bitmap oldbitmap = (Bitmap)pictureBox1.Image;
                int[] pixel_mask = new int[9];
                int pixS;

                int[] smoothing = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                for (int x = 1; x < width - 1; x++)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        pixel_mask[0] = oldbitmap.GetPixel(x - 1, y - 1).G;
                        pixel_mask[1] = oldbitmap.GetPixel(x, y - 1).G;
                        pixel_mask[2] = oldbitmap.GetPixel(x + 1, y - 1).G;
                        pixel_mask[3] = oldbitmap.GetPixel(x - 1, y).G;
                        pixel_mask[4] = oldbitmap.GetPixel(x, y).G;
                        pixel_mask[5] = oldbitmap.GetPixel(x + 1, y).G;
                        pixel_mask[6] = oldbitmap.GetPixel(x - 1, y + 1).G;
                        pixel_mask[7] = oldbitmap.GetPixel(x, y + 1).G;
                        pixel_mask[8] = oldbitmap.GetPixel(x + 1, y + 1).G;
                        pixS = 0;
                        Array.Sort(pixel_mask);
                        pixS = pixel_mask[4];
                        newbitmap.SetPixel(x, y, Color.FromArgb(pixS, pixS, pixS));
                    }
                }
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = newbitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int height = pictureBox1.Image.Height;
                int width = pictureBox1.Image.Width;
                Bitmap newbitmap = new Bitmap(width, height);
                Bitmap oldbitmap = (Bitmap)pictureBox1.Image;
                int[] pixel_mask = new int[9];
                int pixS;

                int[] smoothing = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                for (int x = 1; x < width - 1; x++)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        pixel_mask[0] = oldbitmap.GetPixel(x - 1, y - 1).G;
                        pixel_mask[1] = oldbitmap.GetPixel(x, y - 1).G;
                        pixel_mask[2] = oldbitmap.GetPixel(x + 1, y - 1).G;
                        pixel_mask[3] = oldbitmap.GetPixel(x - 1, y).G;
                        pixel_mask[4] = oldbitmap.GetPixel(x, y).G;
                        pixel_mask[5] = oldbitmap.GetPixel(x + 1, y).G;
                        pixel_mask[6] = oldbitmap.GetPixel(x - 1, y + 1).G;
                        pixel_mask[7] = oldbitmap.GetPixel(x, y + 1).G;
                        pixel_mask[8] = oldbitmap.GetPixel(x + 1, y + 1).G;
                        pixS = 0;
                        for (int i = 0; i < 9; i++)
                        {
                            pixS += (pixel_mask[i] * smoothing[i]);
                        }
                        pixS = pixS / 9;
                        newbitmap.SetPixel(x, y, Color.FromArgb(pixS, pixS, pixS));
                    }
                }
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = newbitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }
    }
}
