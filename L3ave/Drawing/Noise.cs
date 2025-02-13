using System;

namespace L3ave.Drawing
{
    public static class Noise
    {
        public static double[,] Generate(int width, int height, double freq)
        {
            var array = new double[width, height];
            var random = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (random.NextDouble() < freq)
                    {
                        array[i, j] = random.NextDouble();
                    }
                }
            }

            return array;
        }
    }
}
