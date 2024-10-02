using SkiaSharp;

namespace PhoneWaveNamespace
{
    public class PhoneWave
    {
        public void GenerateRandomImage(string filePath, int width = 500, int height = 500)
        {
            using (var bitmap = new SKBitmap(width, height))
            {
                var random = new Random();

                // Loop through each pixel
                // Probably not the most efficient way to do this, but it's just an example
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        var r = (byte)random.Next(256);
                        var g = (byte)random.Next(256);
                        var b = (byte)random.Next(256);

                        var color = new SKColor(r, g, b);

                        bitmap.SetPixel(x, y, color);
                    }
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = System.IO.File.OpenWrite(filePath))
                {
                    data.SaveTo(stream);
                }
            }

            Console.WriteLine($"gelbana -> {filePath}");
        }
    }
}
