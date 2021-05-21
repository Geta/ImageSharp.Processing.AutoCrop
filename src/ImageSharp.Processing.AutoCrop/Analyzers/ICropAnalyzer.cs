using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop.Analyzers
{
    public interface ICropAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
    {
        ICropAnalysis GetAnalysis(Image<TPixel> image, int? colorThreshold, float? bucketTreshold);
    }
}
