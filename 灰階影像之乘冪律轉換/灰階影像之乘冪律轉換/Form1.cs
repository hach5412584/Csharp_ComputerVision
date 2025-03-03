namespace 灰階影像之乘冪律轉換
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int height = pictureBox1.Image.Height;
            int width = pictureBox1.Image.Width;
            Bitmap newbitmap = new Bitmap(width, height);
            Bitmap oldbitmap = (Bitmap)pictureBox1.Image;
            Color pixel;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixel = oldbitmap.GetPixel(i, j);
                    int r, g, b, Result;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    Result = (299 * r + 587 * g + 114 * b) / 1000;
                    newbitmap.SetPixel(i, j, Color.FromArgb(Result, Result, Result));
                }
            }
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = newbitmap;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int height = pictureBox2.Image.Height;
                int width = pictureBox2.Image.Width;
                Bitmap newbitmap = new Bitmap(width, height);
                Bitmap oldbitmap = (Bitmap)pictureBox2.Image;
                int[] xferFunc = new int[256];
                double pow255, gamma;
                gamma = Convert.ToDouble(textBox1.Text);
                if (gamma < 0)
                {
                    gamma = -gamma;
                }
                else if (gamma > 100)
                {
                    gamma = 100;
                }
                pow255 = Math.Pow(255.0, gamma);
                for (int x = 0; x < 256; x++)
                {
                    xferFunc[x] = (int)(Math.Pow((double)x, gamma) / pow255 * 255 + 0.5);
                }
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int Result = xferFunc[oldbitmap.GetPixel(x, y).G];
                        newbitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    } 
                }
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox3.Image = newbitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"信息提示");
            }
        }
    }
}