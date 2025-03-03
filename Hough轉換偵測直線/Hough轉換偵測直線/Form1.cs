namespace Hough轉換偵測直線
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
                        pixS[y - 1, x - 1] = pixS_h + pixS_v;
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
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        pixS[y, x] = (pixS[y, x] - min) * 255 / (max - min);
                        if (pixS[y, x] >= 80)
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
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Bitmsp Image|*.bmp";
                saveFileDialog1.Title = "儲存圖片";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            pictureBox3.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }
        struct XYPoint
        {
            public short X;
            public short Y;
        }
        struct LineParameters
        {
            public int Angle;
            public int Distance;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int height = pictureBox2.Image.Height;
                int width = pictureBox2.Image.Width;
                Bitmap oldBitmap = (Bitmap)pictureBox2.Image;
                int EdgeNum = 0;
                XYPoint[] EdgePoint = new XYPoint[height * width];
                LineParameters[] Line = new LineParameters[height * width]; 
                for (short x = 0; x < width; x++)
                {
                    for(short y = 0; y < height; y++)
                    {
                        if (oldBitmap.GetPixel(x,y).G == 255)
                        {
                            EdgePoint[EdgeNum].X = x;
                            EdgePoint[EdgeNum].Y = y;
                            EdgeNum++;
                        }
                    }
                }
                int AngleNum = 360;
                int DistNum = (int)Math.Sqrt(width * width + height * height) * 2;
                int Threshold = Math.Min(width, height) / 5;
                int HoughSpaceMax = 0;
                Bitmap newBitmap = new Bitmap(AngleNum, DistNum);
                int pixH;
                double DeltaAngle, DeltaDist;
                double MaxDist, MinDist;
                double Angle, Dist;
                int LineCount;
                int[,] HoughSpace = new int[AngleNum, DistNum];
                MaxDist = Math.Sqrt(width * width + height * height);
                MinDist = (double)-width;
                DeltaAngle = Math.PI / AngleNum;
                DeltaDist = (MaxDist - MinDist) / DistNum;

                for (int i = 0; i < AngleNum; i++)
                {
                    for (int j = 0; j < DistNum; j++)
                    {
                        HoughSpace[i, j] = 0;
                    }
                }
                for (int i = 0; i < EdgeNum; i++)
                {
                    for (int j = 0; j < AngleNum; j++)
                    {
                        Angle = j * DeltaAngle;
                        Dist = EdgePoint[i].X * Math.Cos(Angle) + EdgePoint[i].Y * Math.Sin(Angle);
                        HoughSpace[j, (int)((Dist - MinDist) / DeltaDist)]++;
                    }
                }
                LineCount = 0;
                for (int i = 0; i < AngleNum; i++)
                {
                    for (int j = 0; j < DistNum; j++)
                    {
                        if (HoughSpace[i,j] > HoughSpaceMax)
                        {
                            HoughSpaceMax = HoughSpace[i, j];
                        }
                        if (HoughSpace[i, j] >= Threshold)
                        {
                            Line[LineCount].Angle = i;
                            Line[LineCount].Distance = j;
                            LineCount++;
                        }
                    }
                }
                for (int x = 0; x < AngleNum; x++)
                {
                    for (int y = 0; y < DistNum; y++)
                    {
                        pixH = 255 - (HoughSpaceMax - HoughSpace[x, y]) * 255 / HoughSpaceMax;
                        if (HoughSpace[x,y]>Threshold)
                        {
                            newBitmap.SetPixel(x, y, Color.FromArgb(pixH, 0, 0));
                        }
                        else
                        {
                            newBitmap.SetPixel(x, y, Color.FromArgb(pixH, pixH, pixH));
                        }
                    }
                }
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox3.Image = newBitmap;
                for (int i = 0; i < LineCount & i < width * height; i++) 
                {
                    for (int x = 0; x < width; x++)
                    {
                        int y = (int)((Line[i].Distance * DeltaDist + MinDist - x * Math.Cos(Line[i].Angle * DeltaAngle)) / Math.Sin(Line[i].Angle * DeltaAngle));
                        if (y >= 0 & y < height) 
                        {
                            pixH = oldBitmap.GetPixel(x, y).G;
                            oldBitmap.SetPixel(x, y, Color.FromArgb(pixH ^ 255, pixH, pixH));
                        }
                    }
                    for (int y = 0; y < height; y++)
                    {
                        int x = (int)((Line[i].Distance * DeltaDist + MinDist - y * Math.Sin(Line[i].Angle * DeltaAngle)) / Math.Cos(Line[i].Angle * DeltaAngle));
                        if (x >= 0 & x < width)
                        {
                            pixH = oldBitmap.GetPixel(x, y).G;
                            oldBitmap.SetPixel(x, y, Color.FromArgb(pixH ^ 255, pixH, pixH));
                        }
                    }
                }
                pictureBox2.Image = oldBitmap;
                label1.Text = "Houh Transform 完成";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }
    }
}