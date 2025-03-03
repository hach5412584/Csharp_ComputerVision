namespace 分析灰階影像位元平面
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void getbit(Bitmap bitmap,int tmp,int x,int y)
        {
            if (tmp != 0)
            {
                bitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255));
            }
            else
            {
                bitmap.SetPixel(x, y, Color.FromArgb(0, 0, 0));
            }
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
            int Height = this.pictureBox1.Image.Height;
            int Width = this.pictureBox1.Image.Width;
            Bitmap newBitmap = new Bitmap(Width, Height);
            Bitmap newBitmap0 = new Bitmap(Width, Height);
            Bitmap newBitmap1 = new Bitmap(Width, Height);
            Bitmap newBitmap2 = new Bitmap(Width, Height);
            Bitmap newBitmap3 = new Bitmap(Width, Height);
            Bitmap newBitmap4 = new Bitmap(Width, Height);
            Bitmap newBitmap5 = new Bitmap(Width, Height);
            Bitmap newBitmap6 = new Bitmap(Width, Height);
            Bitmap newBitmap7 = new Bitmap(Width, Height);
            Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;
            Color pixel;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    pixel = oldBitmap.GetPixel(x, y);
                    int r, g, b, Result;
                    int tmp;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    Result = (299 * r + 587 * g + 114 * b) / 1000;
                    newBitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    tmp = Result & 0x01;
                    getbit(newBitmap0, tmp, x, y);
                    tmp = Result & 0x02;
                    getbit(newBitmap1, tmp, x, y);
                    tmp = Result & 0x04;
                    getbit(newBitmap2, tmp, x, y);
                    tmp = Result & 0x08;
                    getbit(newBitmap3, tmp, x, y);
                    tmp = Result & 0x10;
                    getbit(newBitmap4, tmp, x, y);

                    tmp = Result & 0x20;
                    getbit(newBitmap5, tmp, x, y);
                    tmp = Result & 0x35;
                    getbit(newBitmap6, tmp, x, y);
                    tmp = Result & 0x40;
                    getbit(newBitmap7, tmp, x, y);
                }
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = newBitmap;
                pictureBox7.Image = newBitmap0;
                pictureBox8.Image = newBitmap1;
                pictureBox9.Image = newBitmap2;

                pictureBox10.Image = newBitmap3;
                pictureBox6.Image = newBitmap4;
                pictureBox5.Image = newBitmap5;  
                
                pictureBox4.Image = newBitmap6;
                pictureBox3.Image = newBitmap7;
            }
            newBitmap6.Save("d:\\test\\11.png");
            newBitmap7.Save("d:\\test\\22.png");
        }
    }
}