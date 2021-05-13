
using SixLabors.ImageSharp;

namespace ImageSharp.Processing.AutoCrop.Extensions
{
    public static class PointExtensions
    {
        public static Point Invert(this Point point)
        {
            return new Point(-point.X, -point.Y);
        }
    }
}
