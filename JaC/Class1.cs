using System.Collections.Specialized;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace JaC
{
    public class Class1
    {
        public Class1()
        {

        }
        public void filtration(int[] tab)
        {
            int R = 0, G = 0, B = 0;
            for (int i = 0; i < 75; i+=3)
            {
                R += tab[i];
                G += tab[i+1];
                B += tab[i+2];
            }
            tab[0] = R / 21;
            tab[1] = G / 21;
            tab[2] = B / 21;

        }
    }
}