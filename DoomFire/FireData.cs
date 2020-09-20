using System;
using System.Runtime.CompilerServices;

namespace DoomFire
{
    public class FireData
    {
        public int Cols { get; }
        public int Rows { get; }

        readonly int[] data;
        public FireWind Wind { get; private set; } = FireWind.None;

        readonly Random random = new Random();

        public int this[int x, int y] => data[x * Cols + y];

        public FireData(int cols, int rows)
        {
            Cols = cols;
            Rows = rows;
            data = new int[Rows * Cols];

            defineBaseState();
        }

        void defineBaseState()
        {
            var max = ColorPalete.Colors.Length - 1;
            for (var j = 0; j < Cols; j++)
            {
                var overflowPixelIndex = Cols * Rows;
                var pixelIndex = (overflowPixelIndex - Cols) + j;
                data[pixelIndex] = max;
            }
        }

        public void Update()
        {
            for (var column = 0; column < Cols; column++)
                for (var row = 0; row < Rows; row++)
                {
                    var pixelIndex = column + (Cols * row);
                    updateFire(pixelIndex);
                }
        }

        void updateFire(int currentPixelIndex)
        {
            var belowPixelIndex = currentPixelIndex + Cols;

            if (belowPixelIndex >= Cols * Rows)
                return;

            var decay = random.Next(3 + 1);

            if (currentPixelIndex - decay < 0)
                return;

            var belowPixelFireIntensity = data[belowPixelIndex];
            var tempIntensity = belowPixelFireIntensity - (decay & 1);
            var newFireIntensity = tempIntensity >= 0 ? tempIntensity : 0;

            switch (Wind)
            {
                case FireWind.None:
                    var rndNeg = (decay & 1) == 0 ? -decay : decay;
                    data[currentPixelIndex + rndNeg ] = newFireIntensity;
                    break;
                case FireWind.Left:
                    data[currentPixelIndex - decay] = newFireIntensity;
                    break;
                case FireWind.Right:
                    data[currentPixelIndex + decay] = newFireIntensity;
                    break;
                default:
                    break;
            }

        }

        public void SetWind(FireWind wind) => this.Wind = wind;
    }


    public enum FireWind
    {
        None,
        Left,
        Right
    }
}
