using JaC;
using System.Runtime.InteropServices;
using static JaC.Class1;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace JaProj
{
    public partial class Form1 : Form
    {
        [DllImport(@"D:\JaProj\x64\Debug\JaAsm.dll")]
        static extern int FilterASM(int []tab);
        int funcChoice = 0, threadNum = 0, pictureWidth = 0, pictureHeight = 0;
        Bitmap sourceBitmap, outputBitmap;
        byte[,,] sourceBuffer2;
        byte[,,] outputBuffer2;
        int sourceWidth, sourceHeight;
        String filePath;
        //List<int> Temp = new List<int>();
        public Form1()
        {
            InitializeComponent();
            trackBar1.Value = Environment.ProcessorCount;
            threadNum = Environment.ProcessorCount;
            label1.Text = trackBar1.Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    pictureBox1.Image = Image.FromFile(filePath);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(filePath!= null) 
            { 
                sourceBitmap = new Bitmap(filePath);
                outputBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
                sourceWidth = sourceBitmap.Width;
                sourceHeight = sourceBitmap.Height;
                int depth = Bitmap.GetPixelFormatSize(sourceBitmap.PixelFormat) / 8; //bytes per pixel
                int depth2 = 4; //bytes per pixel
                sourceBuffer2 = new byte[sourceWidth, sourceHeight, depth];
                outputBuffer2 = new byte[sourceWidth, sourceHeight, depth2];
                var threads = new Thread[threadNum];
                int blockHeight = sourceHeight / threadNum;
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < sourceWidth; i += 1)
                {
                    for (int j = 0; j < sourceHeight; j += 1)
                    {
                        var pix = sourceBitmap.GetPixel(i, j);
                        sourceBuffer2[i, j, 0] = pix.R;
                        sourceBuffer2[i, j, 1] = pix.G;
                        sourceBuffer2[i, j, 2] = pix.B;
                    }
                }

                for (int i = 0; i < threadNum; i++)
                {
                    int yStart = i * blockHeight;
                    int yEnd = (i + 1) * blockHeight;
                    if (i == threadNum - 1)
                    {
                        yEnd = sourceHeight;
                    }

                    threads[i] = new Thread(() => mashup(yStart, yEnd, depth));
                    threads[i].Start();
                }
                foreach (var thread in threads)
                {
                    thread.Join();

                }
                stopwatch.Stop();

                for (int i = 0; i < sourceWidth; i += 1)
                {
                    for (int j = 0; j < sourceHeight; j += 1)
                    {
                        var color = Color.FromArgb(255, outputBuffer2[i, j, 0], outputBuffer2[i, j, 1], outputBuffer2[i, j, 2]);
                        outputBitmap.SetPixel(i, j, color);
                    }
                }
                stopwatch.Stop();

                pictureBox2.Image = outputBitmap;
                label2.Text = "Czas wykonania:" + stopwatch.ElapsedMilliseconds.ToString() + "ms";
            }
            else
            {
                MessageBox.Show("wybierz zdjêcie");
            }
        }
        private void mashup(int startIndex, int endIndex, int depth)
        {
            int[,] mask = new int[,] {{ 0, 1, 1, 1, 0 },
                                       { 1, 1, 1, 1, 1 },
                                       { 1, 1, 1, 1, 1 },
                                       { 1, 1, 1, 1, 1 },
                                       { 0, 1, 1, 1, 0 }};
            int R = 0, G = 0, B = 0, A = 0;

            for (int i = 0; i < sourceWidth; i += 1)
            {
                for (int j = startIndex; j < endIndex; j += 1)
                {
                    var filter = new Class1();
                    if (i < 2 || i >= (sourceWidth - 2) || j < 2 || j >= (sourceHeight - 2))
                    {
                        outputBuffer2[i, j, 0] = sourceBuffer2[i, j, 0];
                        outputBuffer2[i, j, 1] = sourceBuffer2[i, j, 1];
                        outputBuffer2[i, j, 2] = sourceBuffer2[i, j, 2];
                        outputBuffer2[i, j, 3] = 255;
                        continue;
                    }
                    int []tab = new int[76];
                    int z = 0;
                    for (int x = -2; x <= 2; x++)
                    {
                        for (int y = -2; y <= 2; y++)
                        {
                            tab[z] = ((int)sourceBuffer2[i + x, j + y, 0]) * mask[x + 2, y + 2];
                            tab[z+1] = ((int)sourceBuffer2[i + x, j + y, 1]) * mask[x + 2, y + 2];
                            tab[z+2] = ((int)sourceBuffer2[i + x, j + y, 2]) * mask[x + 2, y + 2];
                            z += 3;
                        }
                    }
                    if (radioButton1.Checked == true)
                    {
                        filter.filtration(tab);
                    }
                    else if (radioButton2.Checked == true)
                    {
                        FilterASM(tab);
                    }
                    R = tab[0];
                    G = tab[1];
                    B = tab[2];
                    A = 255;

                    outputBuffer2[i, j, 0] = (byte)R;
                    outputBuffer2[i, j, 1] = (byte)G;
                    outputBuffer2[i, j, 2] = (byte)B;
                    outputBuffer2[i, j, 3] = (byte)A;
                    R = 0;
                    G = 0;
                    B = 0;
                }
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            threadNum = trackBar1.Value;
        }

    }
}