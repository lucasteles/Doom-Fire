using System;

namespace DoomFire
{
    public class FireData
    {
        public int Cols { get; }
        public int Rows { get; }
        public int[] Data { get; }
        public FireWind Wind { get; private set; } = FireWind.Left;

        readonly Random random = new Random();

        public FireData(int cols, int rows)
        {
            Cols = cols;
            Rows = rows;
            Data = new int[Rows * Cols];

            defineBaseState();
        }

        void defineBaseState()
        {
            var max = ColorPalete.Colors.Length - 1;
            for (var j = 0; j < Cols; j++)
            {
                var overflowPixelIndex = Cols * Rows;
                var pixelIndex = (overflowPixelIndex - Cols) + j;
                Data[pixelIndex] = max;
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

            var decay = random.Next(3);

            if (currentPixelIndex - decay < 0)
                return;


            var belowPixelFireIntensity = Data[belowPixelIndex];
            var newFireIntensity =
              belowPixelFireIntensity - decay >= 0 ? belowPixelFireIntensity - decay : 0;

            switch (Wind)
            {
                case FireWind.None:
                    Data[currentPixelIndex] = newFireIntensity;
                    break;
                case FireWind.Left:
                    Data[currentPixelIndex - decay] = newFireIntensity;
                    break;
                case FireWind.Right:
                    Data[currentPixelIndex + decay] = newFireIntensity;
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
