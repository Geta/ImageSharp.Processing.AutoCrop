using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop.Detection
{
    public abstract class BorderAnalyzer<TPixel> : IBorderAnalyzer<TPixel> where TPixel: unmanaged, IPixel<TPixel>
    {
        public abstract IBorderAnalysis Analyze(Image<TPixel> image, Rectangle rectangle, int colorThreshold, float bucketThreshold);
    }
}