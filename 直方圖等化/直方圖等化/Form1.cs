namespace 直方圖等化
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
                int[] histogram = new int[256];
                int[] cumulative_histogram = new int[256];
                int[] xferFunc = new int[256];
                int resolution = height * width;

                for (int i = 0; i < 256; i++)
                {
                    histogram[i] = 0;
                }
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        histogram[oldbitmap.GetPixel(x, y).G]++;
                    }
                }
                cumulative_histogram[0] = histogram[0];
                
                for (int x = 1; x < 256; x++)
                {
                    cumulative_histogram[x] = cumulative_histogram[x - 1] + histogram[x];
                }

                for (int x = 0; x < 256; x++)
                {
                    xferFunc[x] = (255 * cumulative_histogram[x]) / resolution;
                }

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int Result = xferFunc[oldbitmap.GetPixel(x, y).G];
                        newbitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
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