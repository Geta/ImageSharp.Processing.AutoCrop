using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop.Extensions
{
    public static class ColorExtensions
    {
        public static byte ToColorBucket(this Color color)
        {
            return color.ToPixel<Rgba32>().ToColorBucket();
        }
    }
}
