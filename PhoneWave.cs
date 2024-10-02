using SkiaSharp;
using System;

namespace PhoneWaveNamespace
{
    public class PhoneWave
    {
        public void GenerateRandomImage(string filePath, int width = 500, int height = 500, int pixelSize = 10)
        {
            var info = new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

            using (var bitmap = new SKBitmap(info))
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.Transparent);

                Random random = new Random();

                for (int x = 0; x < width; x += pixelSize)
                {
                    for (int y = 0; y < height; y += pixelSize)
                    {
                        if (random.NextDouble() > 0.5)
                        {
                            var paint = new SKPaint
                            {
                                Color = new SKColor(10, 10, 10, 255),
                                Style = SKPaintStyle.Fill
                            };

                            canvas.DrawRect(new SKRect(x, y, x + pixelSize, y + pixelSize), paint);
                        }
                        else
                        {
                            for (int tinyX = x; tinyX < x + pixelSize; tinyX++)
                            {
                                for (int tinyY = y; tinyY < y + pixelSize; tinyY++)
                                {
                                    byte tinyR = (byte)random.Next(256);
                                    byte tinyG = (byte)random.Next(256);
                                    byte tinyB = (byte)random.Next(256);

                                    var tinyPaint = new SKPaint
                                    {
                                        Color = new SKColor(tinyR, tinyG, tinyB, 150),
                                        Style = SKPaintStyle.Fill
                                    };

                                    canvas.DrawRect(new SKRect(tinyX, tinyY, tinyX + 1, tinyY + 1), tinyPaint);
                                }
                            }
                        }
                    }
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = System.IO.File.OpenWrite(filePath))
                {
                    data.SaveTo(stream);
                }
            }

            Console.WriteLine($"Random image with both tiny and thicker pixels generated and saved as {filePath}");
        }
    }
}
