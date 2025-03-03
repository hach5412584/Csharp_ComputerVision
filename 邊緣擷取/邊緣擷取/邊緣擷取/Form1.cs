using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace 邊緣擷取
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openfiledialog = new OpenFileDialog();
                openfiledialog.Filter = "圖像文件(JPge, Gif, Bmp, etc.)|*.jpg;*.jpge;*.gif;*.bmp;*.tif;*.tiff;*.png;|所有文件(*.*)|*.*";
                if (openfiledialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap MyBitmap = new Bitmap(openfiledialog.FileName);
                    pictureBox1.Image = MyBitmap;
                }
                int height = pictureBox1.Image.Height;
                int width = pictureBox1.Image.Width;
                Bitmap newBitmap = new Bitmap(width, height);
                Bitmap oldBitmap = (Bitmap)pictureBox1.Image;
                Color pixel;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        pixel = oldBitmap.GetPixel(x, y);
                        int r, g, b, Result = 0;
                        r = pixel.R;
                        b = pixel.B;
                        g = pixel.G;
                        Result = (299 * r + 587 * g + 114 * b) / 1000;
                        newBitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    }
                }
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = newBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int[] horizontal = new int[] { 1, 0, -1, 2, 0, -2, 1, 0, -1 };
                int[] vertical = new int[] { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
                int height = pictureBox1.Image.Height;
                int width = pictureBox1.Image.Width;
                int[,] pixS = new int[height, width];
                int[] pixel_mask = new int[9];
                Bitmap newBitmap = new Bitmap(width, height);
                Bitmap oldBitmap = (Bitmap)pictureBox1.Image;
                int pixS_h = 0, pixS_v = 0;
                int max = 0;
                int min = 0;
                for (int x = 1; x < width - 1; x++)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        pixel_mask[0] = oldBitmap.GetPixel(x - 1, y - 1).G;
                        pixel_mask[1] = oldBitmap.GetPixel(x, y - 1).G;
                        pixel_mask[2] = oldBitmap.GetPixel(x + 1, y - 1).G;
                        pixel_mask[3] = oldBitmap.GetPixel(x - 1, y).G;
                        pixel_mask[4] = oldBitmap.GetPixel(x, y).G;
                        pixel_mask[5] = oldBitmap.GetPixel(x + 1, y).G;
                        pixel_mask[6] = oldBitmap.GetPixel(x - 1, y + 1).G;
                        pixel_mask[7] = oldBitmap.GetPixel(x, y + 1).G;
                        pixel_mask[8] = oldBitmap.GetPixel(x + 1, y + 1).G;
                        for (int i = 0; i < 9; i++)
                        {
                            pixS_h += (pixel_mask[i] * horizontal[i]);
                            pixS_v += (pixel_mask[i] * vertical[i]);
                        }
                        pixS_h = pixS_h / 9;
                        pixS_v = pixS_v / 9;
                        pixS_v = Math.Abs(pixS_v);
                        pixS_h = Math.Abs(pixS_h);
                        pixS[y-1,x-1] = pixS_h + pixS_v;
                        if (max < pixS[y - 1, x - 1])
                        {
                            max = pixS[y - 1, x - 1];
                        } 
                        if (min > pixS[y - 1, x - 1])
                        {
                            min = pixS[y - 1, x - 1];
                        }
                    }
                }
                for (int x = 0; x < width ; x++)
                {
                    for (int y = 0; y < height ; y++)
                    {
                        pixS[y, x] = (pixS[y, x] - min) * 255 / (max - min);
                        if (pixS[y, x] >= Convert.ToInt32(textBox1.Text))
                        {
                            pixS[y, x] = 255;
                        }
                        else
                        {
                            pixS[y, x] = 0;
                        }
                        newBitmap.SetPixel(x, y, Color.FromArgb(pixS[y, x], pixS[y, x], pixS[y, x]));
                    }
                }
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = newBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
