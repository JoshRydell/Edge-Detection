using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Edge_Detection
{
    class Image
    {
        public Bitmap bmp;
        public Image(string filename)
        {
            bmp = new Bitmap(filename);
        }

        public Bitmap greyScale()
        {
            Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color pixelColour = bmp.GetPixel(x, y);
                    int average = (pixelColour.R + pixelColour.G + pixelColour.B) / 3;
                    Color grayShade = Color.FromArgb(average, average, average);
                    newBmp.SetPixel(x, y, grayShade);
                }
            }



            return newBmp;
        }

        public Bitmap edgeDetection(bool flip)
        {
            Bitmap finalBmp = new Bitmap(bmp.Width, bmp.Height);
            int[,] xMap = new int[bmp.Width, bmp.Height];
            int[,] yMap = new int[bmp.Width, bmp.Height];
            int[,] xKernel = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] yKernel = new int[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            applyKernel(yKernel, yMap);
            applyKernel(xKernel, xMap);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    int mod =  flip ? 255 - (int)Math.Sqrt(xMap[x, y] * xMap[x, y] + yMap[x, y] * yMap[x, y]) : (int)Math.Sqrt(xMap[x, y] * xMap[x, y] + yMap[x, y] * yMap[x, y]);
                    finalBmp.SetPixel(x, y, Color.FromArgb(mod, mod, mod));
                }
            }

            return finalBmp;
        }
        

        private void applyKernel(int[,] kernel, int[,] map)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    int gradient = 0;
                    for (int kernelX = 0; kernelX < 3; kernelX++)
                    {
                        for (int kernelY = 0; kernelY < 3; kernelY++)
                        {
                            int Xloc = x + kernelX - 1;
                            int Yloc = y + kernelY - 1;
                            
                            if(Xloc >= 0 && Xloc < bmp.Width && Yloc >= 0 && Yloc < bmp.Height)
                            {
                                gradient += (int)(255/(4*Math.Sqrt(2))*kernel[kernelX, kernelY] * bmp.GetPixel(Xloc, Yloc).GetBrightness());
                            }
                        }
                    }

                    map[x, y] = gradient;
                }
            }
        }
    }
}
