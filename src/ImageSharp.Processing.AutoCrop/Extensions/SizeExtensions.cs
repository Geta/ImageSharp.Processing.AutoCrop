using System.Drawing;

namespace ImageSharp.Processing.AutoCrop.Extensions
{
    internal static class SizeExtensions
    {
        public static Point ToPoint(this Size size)
        {
            return new Point(size.Width, size.Height);
        }
    }
}
