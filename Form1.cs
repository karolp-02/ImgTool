using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Image = System.Drawing.Image;

namespace wczytywanieObrazu2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            textBox1.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog oknoWyboru = new OpenFileDialog();
            if (oknoWyboru.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(oknoWyboru.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // wczytywanie obrazka i zamiana na odcienie szarosci metoda YSV
                Image obrazek;
                obrazek = pictureBox1.Image;
                Bitmap szaraBitmapa = new Bitmap(obrazek.Width, obrazek.Height);

                for (int y = 0; y < obrazek.Height; y++)
                {
                    for (int x = 0; x < obrazek.Width; x++)
                    {
                        Color oryginalnyKolor = ((Bitmap)obrazek).GetPixel(x, y);
                        int wartoscSzarosci = (int)(oryginalnyKolor.R * 0.3 + oryginalnyKolor.G * 0.59 + oryginalnyKolor.B * 0.11);
                        Color odcienSzarosci = Color.FromArgb(wartoscSzarosci, wartoscSzarosci, wartoscSzarosci);
                        szaraBitmapa.SetPixel(x, y, odcienSzarosci);
                    }
                }
                pictureBox2.Image = szaraBitmapa;

                // liczenie hitogramu odcieni szarosci
                int[] histogram = new int[256];
                Bitmap bitmapa = (Bitmap)obrazek;
                for (int y = 0; y < obrazek.Height; y++)
                {
                    for (int x = 0; x < obrazek.Width; x++)
                    {
                        Color kolorPixela = bitmapa.GetPixel(x, y);
                        int wartoscSzarosci = (int)(kolorPixela.R * 0.3 + kolorPixela.G * 0.59 + kolorPixela.B * 0.11);
                        histogram[wartoscSzarosci]++;
                    }
                }
                // rysowanie wykresu chart
                chart1.Series.Clear();
                Series serie = chart1.Series.Add("Histogram");
                serie.ChartType = SeriesChartType.Column;
                serie.Color = Color.DarkGreen;

                for (int i = 0; i < histogram.Length; i++)
                {
                    serie.Points.AddXY(i, histogram[i]);
                }

                chart1.ChartAreas[0].AxisX.Interval = 25;
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 255;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // wczytywanie obrazka i zamiana na odcienie szarosci metoda sredniej
                Image obrazek;
                obrazek = pictureBox1.Image;
                Bitmap szaraBitmapa = new Bitmap(obrazek.Width, obrazek.Height);

                for (int y = 0; y < obrazek.Height; y++)
                {
                    for (int x = 0; x < obrazek.Width; x++)
                    {
                        Color oryginalnyKolor = ((Bitmap)obrazek).GetPixel(x, y);
                        int wartoscSzarosci = (int)((oryginalnyKolor.R + oryginalnyKolor.G + oryginalnyKolor.B) / 3);
                        Color odcienSzarosci = Color.FromArgb(wartoscSzarosci, wartoscSzarosci, wartoscSzarosci);
                        szaraBitmapa.SetPixel(x, y, odcienSzarosci);
                    }
                }
                pictureBox2.Image = szaraBitmapa;

                // liczenie hitogramu odcieni szarosci
                int[] histogram = new int[256];
                Bitmap bitmapa = (Bitmap)obrazek;
                for (int y = 0; y < obrazek.Height; y++)
                {
                    for (int x = 0; x < obrazek.Width; x++)
                    {
                        Color kolorPixela = bitmapa.GetPixel(x, y);
                        int wartoscSzarosci = (int)((kolorPixela.R + kolorPixela.G + kolorPixela.B) / 3);
                        histogram[wartoscSzarosci]++;
                    }
                }
                // rysowanie wykresu chart
                chart1.Series.Clear();
                Series serie = chart1.Series.Add("Histogram");
                serie.ChartType = SeriesChartType.Column;
                serie.Color = Color.DarkGreen;

                for (int i = 0; i < histogram.Length; i++)
                {
                    serie.Points.AddXY(i, histogram[i]);
                }

                chart1.ChartAreas[0].AxisX.Interval = 25;
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 255;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Image obrazek = pictureBox1.Image;
                Bitmap szaraBitmapa = new Bitmap(obrazek.Width, obrazek.Height);

                for (int y = 0; y < obrazek.Height; y++)
                {
                    for (int x = 0; x < obrazek.Width; x++)
                    {
                        Color oryginalnyKolor = ((Bitmap)obrazek).GetPixel(x, y);
                        int wartoscSzarosci = (int)((oryginalnyKolor.R + oryginalnyKolor.G + oryginalnyKolor.B) / 3);
                        Color odcienSzarosci = Color.FromArgb(wartoscSzarosci, wartoscSzarosci, wartoscSzarosci);
                        szaraBitmapa.SetPixel(x, y, odcienSzarosci);
                    }
                }

                int[] histogram = new int[256];
                for (int y = 0; y < szaraBitmapa.Height; y++)
                {
                    for (int x = 0; x < szaraBitmapa.Width; x++)
                    {
                        int wartoscSzarosci = szaraBitmapa.GetPixel(x, y).R;
                        histogram[wartoscSzarosci]++;
                    }
                }

                // znajdowanie najwiekszego piku
                int maxPik = 0;
                int prog = 0;
                for (int i = 0; i < histogram.Length; i++)
                {
                    if (histogram[i] > maxPik)
                    {
                        maxPik = histogram[i];
                        prog = i;
                    }
                }

                // binaryzacja
                for (int y = 0; y < szaraBitmapa.Height; y++)
                {
                    for (int x = 0; x < szaraBitmapa.Width; x++)
                    {
                        int wartoscSzarosci = szaraBitmapa.GetPixel(x, y).R;
                        if (wartoscSzarosci >= prog)
                        {
                            szaraBitmapa.SetPixel(x, y, Color.White);
                        }
                        else
                        {
                            szaraBitmapa.SetPixel(x, y, Color.Black);
                        }
                    }
                }
                pictureBox3.Image = szaraBitmapa;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Image obrazek;
                obrazek = pictureBox1.Image;
                Bitmap szaraBitmapa = new Bitmap(obrazek.Width, obrazek.Height);
                int prog = 0;
                prog = int.Parse(textBox1.Text);

                if (prog != 0)
                {
                    for (int y = 0; y < obrazek.Height; y++)
                    {
                        for (int x = 0; x < obrazek.Width; x++)
                        {
                            Color oryginalnyKolor = ((Bitmap)obrazek).GetPixel(x, y);
                            int wartoscSzarosci = (int)((oryginalnyKolor.R + oryginalnyKolor.G + oryginalnyKolor.B) / 3);
                            Color odcienSzarosci = Color.FromArgb(wartoscSzarosci, wartoscSzarosci, wartoscSzarosci);
                            szaraBitmapa.SetPixel(x, y, odcienSzarosci);
                        }
                    }
                    for (int y = 0; y < obrazek.Height; y++)
                    {
                        for (int x = 0; x < obrazek.Width; x++)
                        {
                            Color kolorPixela = szaraBitmapa.GetPixel(x, y);
                            int wartoscSzarosci = (int)((kolorPixela.R + kolorPixela.G + kolorPixela.B) / 3);
                            if (wartoscSzarosci >= prog)
                            {
                                Color bialy = Color.FromArgb(255, 255, 255);
                                szaraBitmapa.SetPixel(x, y, bialy);
                            }
                            else
                            {
                                Color czarny = Color.FromArgb(0, 0, 0);
                                szaraBitmapa.SetPixel(x, y, czarny);
                            }
                        }
                    }
                    pictureBox3.Image = szaraBitmapa;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                int szer = pictureBox1.Image.Width;
                int wys = pictureBox1.Image.Height;

                Bitmap krawedzie = new Bitmap(szer, wys);

                for (int y = 0; y < wys; y++)
                {
                    for (int x = 0; x < szer - 1; x++)
                    {
                        Color pixel1 = ((Bitmap)pictureBox1.Image).GetPixel(x, y);
                        Color pixel2 = ((Bitmap)pictureBox1.Image).GetPixel(x + 1, y);


                        int roznicaR = Math.Abs(pixel1.R - pixel2.R);
                        int roznicaG = Math.Abs(pixel1.G - pixel2.G);
                        int roznicaB = Math.Abs(pixel1.B - pixel2.B);
                        int rozniceO = (roznicaR + roznicaG + roznicaB) / 3;

                        krawedzie.SetPixel(x, y, Color.FromArgb(rozniceO, rozniceO, rozniceO));
                    }
                }
                for (int x = 0; x < szer; x++)
                {
                    for (int y = 0; y < wys - 1; y++)
                    {
                        Color pixel1 = ((Bitmap)pictureBox1.Image).GetPixel(x, y);
                        Color pixel2 = ((Bitmap)pictureBox1.Image).GetPixel(x, y + 1);


                        int roznicaR = Math.Abs(pixel1.R - pixel2.R);
                        int roznicaG = Math.Abs(pixel1.G - pixel2.G);
                        int roznicaB = Math.Abs(pixel1.B - pixel2.B);


                        int rozniceO = (roznicaR + roznicaG + roznicaB) / 3;


                        krawedzie.SetPixel(x, y, Color.FromArgb(rozniceO, rozniceO, rozniceO));
                    }
                }
                pictureBox5.Image = krawedzie;
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap obrazek = new Bitmap(pictureBox1.Image);
                Bitmap wynikBitmapa = new Bitmap(obrazek.Width, obrazek.Height);

                // filtr gornoprzepustowy
                int[,] kernel = new int[3, 3]
                {
            {  -1, -1,  -1 },
            { -1,  9, -1 },
            {  -1, -1,  -1 }
                };

                int kernelWidth = kernel.GetLength(0);
                int kernelHeight = kernel.GetLength(1);
                int kernelOffset = kernelWidth / 2;

                for (int y = kernelOffset; y < obrazek.Height - kernelOffset; y++)
                {
                    for (int x = kernelOffset; x < obrazek.Width - kernelOffset; x++)
                    {
                        int rSum = 0, gSum = 0, bSum = 0;

                        for (int ky = -kernelOffset; ky <= kernelOffset; ky++)
                        {
                            for (int kx = -kernelOffset; kx <= kernelOffset; kx++)
                            {
                                Color pixelColor = obrazek.GetPixel(x + kx, y + ky);
                                int kernelValue = kernel[ky + kernelOffset, kx + kernelOffset];

                                rSum += pixelColor.R * kernelValue;
                                gSum += pixelColor.G * kernelValue;
                                bSum += pixelColor.B * kernelValue;
                            }
                        }
                        rSum = Math.Min(Math.Max(rSum, 0), 255);
                        gSum = Math.Min(Math.Max(gSum, 0), 255);
                        bSum = Math.Min(Math.Max(bSum, 0), 255);

                        wynikBitmapa.SetPixel(x, y, Color.FromArgb(rSum, gSum, bSum));
                    }
                }

                pictureBox7.Image = wynikBitmapa;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap obrazek = new Bitmap(pictureBox1.Image);
                Bitmap wynikBitmapa = new Bitmap(obrazek.Width, obrazek.Height);

                // filtr dolnoprzepustowy
                double[,] kernel = new double[3, 3]
                {
            { 1.0 / 9, 1.0 / 9, 1.0 / 9 },
            { 1.0 / 9, 1.0 / 9, 1.0 / 9 },
            { 1.0 / 9, 1.0 / 9, 1.0 / 9 }
                };

                int kernelWidth = kernel.GetLength(0);
                int kernelHeight = kernel.GetLength(1);
                int kernelOffset = kernelWidth / 2;

                for (int y = kernelOffset; y < obrazek.Height - kernelOffset; y++)
                {
                    for (int x = kernelOffset; x < obrazek.Width - kernelOffset; x++)
                    {
                        double rSum = 0, gSum = 0, bSum = 0;

                        for (int ky = -kernelOffset; ky <= kernelOffset; ky++)
                        {
                            for (int kx = -kernelOffset; kx <= kernelOffset; kx++)
                            {
                                Color pixelColor = obrazek.GetPixel(x + kx, y + ky);
                                double kernelValue = kernel[ky + kernelOffset, kx + kernelOffset];

                                rSum += pixelColor.R * kernelValue;
                                gSum += pixelColor.G * kernelValue;
                                bSum += pixelColor.B * kernelValue;
                            }
                        }
                        int r = Math.Min(Math.Max((int)rSum, 0), 255);
                        int g = Math.Min(Math.Max((int)gSum, 0), 255);
                        int b = Math.Min(Math.Max((int)bSum, 0), 255);

                        wynikBitmapa.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                pictureBox7.Image = wynikBitmapa;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}