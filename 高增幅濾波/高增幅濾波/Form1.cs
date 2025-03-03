namespace 高增幅濾波
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
                double[] A_4 = new double[] { 0, -1, 0, -1, 4, -1, 0, -1, 0 };
                int height = pictureBox1.Image.Height;
                int width = pictureBox1.Image.Width;
                int[] pixel_mask = new int[9];
                Bitmap newBitmap = new Bitmap(width, height);
                Bitmap oldBitmap = (Bitmap)pictureBox1.Image;
                int pixS_A4 = 0;
                A_4[4] = A_4[4] + Convert.ToDouble(textBox1.Text);
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
                            pixS_A4 += Convert.ToInt32(pixel_mask[i] * A_4[i]);
                        }
                        pixS_A4 = Convert.ToInt32(pixS_A4 / 2);
                        if (pixS_A4 > 255)
                        {
                            pixS_A4 = 255;
                        }
                        else if (pixS_A4 < 0)
                        {
                            pixS_A4 = 0;
                        }
                        newBitmap.SetPixel(x, y, Color.FromArgb(pixS_A4, pixS_A4, pixS_A4));
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                double[] A_8 = new double[] { -1, -1, -1, -1, 8, -1, -1, -1, -1 };
                int height = pictureBox1.Image.Height;
                int width = pictureBox1.Image.Width;
                int[] pixel_mask = new int[9];
                Bitmap newBitmap = new Bitmap(width, height);
                Bitmap oldBitmap = (Bitmap)pictureBox1.Image;
                int pixS_A8 = 0;
                A_8[4] = A_8[4] + Convert.ToDouble(textBox1.Text);
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
                            pixS_A8 += Convert.ToInt32(pixel_mask[i] * A_8[i]);
                        }
                        pixS_A8 = Convert.ToInt32(pixS_A8 / 2);
                        if (pixS_A8 > 255)
                        {
                            pixS_A8 = 255;
                        }
                        else if (pixS_A8 < 0)
                        {
                            pixS_A8 = 0;
                        }
                        newBitmap.SetPixel(x, y, Color.FromArgb(pixS_A8, pixS_A8, pixS_A8));
                    }
                }
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox3.Image = newBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }
    }
}