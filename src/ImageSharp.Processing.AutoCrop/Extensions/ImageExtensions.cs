using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Reflection;

namespace ImageSharp.Processing.AutoCrop.Extensions
{
    public static class ImageExtensions
    {
        public static void CopyRect<TPixel>(this Image<TPixel> target, Image<TPixel> source, Rectangle bounds, Point offset) where TPixel : unmanaged, IPixel<TPixel>
        {
            for (var y = bounds.Top; y < bounds.Bottom; y++)
            {
                var srow = source.GetPixelRowSpan(y);
                var tRow = target.GetPixelRowSpan(y + offset.Y);

                for (var x = bounds.Left; x < bounds.Right; x++)
                {
                    tRow[x + offset.X] = srow[x];
                }
            }
        }

        public static void SwapPixelBuffersFrom<TPixel>(this Image<TPixel> image, Image<TPixel> pixelSource) where TPixel : unmanaged, IPixel<TPixel>
        {
            var imageType = image.GetType();
            var copyMethod = imageType.GetMethod("SwapOrCopyPixelsBuffersFrom", BindingFlags.NonPublic | BindingFlags.Instance);

            copyMethod.Invoke(image, new [] { pixelSource });
        }
    }
}
