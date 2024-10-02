using SkiaSharp;
using System;

namespace PhoneWaveNamespace
{
    public class PhoneWave
    {

        // https://www.youtube.com/watch?v=1Bf0aYBk0OQ | Literally AGI 
        public string GenerateRandomCaptchaWithOverlay(string filePath, int width = 500, int height = 200, int textSize = 40, int characterCount = 6)
        {
            var info = new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);
            string captchaText = GenerateRandomText(characterCount);

            using (var bitmap = new SKBitmap(info))
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.LightGray);

                Random random = new Random();

                var textPaint = new SKPaint
                {
                    Color = SKColors.Red,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextSize = textSize,
                    Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
                };

                // Super complex captcha text positioning
                var textBounds = new SKRect();
                textPaint.MeasureText(captchaText, ref textBounds);
                float xText = (width - textBounds.Width) / 2;
                float yText = (height + textBounds.Height) / 2;

                for (int i = 0; i < captchaText.Length; i++)
                {
                    string character = captchaText[i].ToString();
                    float rotationAngle = (float)(random.NextDouble() * 30 - 15);
                    canvas.Save();
                    canvas.RotateDegrees(rotationAngle, xText + i * textSize, yText);
                    canvas.DrawText(character, xText + i * textSize, yText, textPaint);
                    canvas.Restore();
                }

                // Time to add some noise
                for (int x = 0; x < width; x += 10)
                {
                    for (int y = 0; y < height; y += 10)
                    {
                        // Comically large pixels
                        if (random.NextDouble() > 0.5)
                        {
                            var pixelPaint = new SKPaint
                            {
                                Color = new SKColor(10, 10, 10, 190),
                                Style = SKPaintStyle.Fill
                            };

                            canvas.DrawRect(new SKRect(x, y, x + 10, y + 10), pixelPaint);
                        }
                        else
                        {
                            // Tiny pixels that are barely visible and probably useless
                            for (int tinyX = x; tinyX < x + 10; tinyX++)
                            {
                                for (int tinyY = y; tinyY < y + 10; tinyY++)
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

            Console.WriteLine($"Text: '{captchaText}', saved at {filePath}");

            return captchaText;
        }

        // Generate a random string of characters, can AI come up with a better solution?
        private string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }
    }
}
