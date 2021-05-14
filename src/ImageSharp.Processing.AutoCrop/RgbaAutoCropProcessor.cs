using ImageSharp.Processing.AutoCrop.Analyzers;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop
{
    public class RgbaAutoCropProcessor : AutoCropProcessor<Rgba32>
    {
        public RgbaAutoCropProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgba32> source) : base(configuration, settings, source)
        {
            var analyzer = new RgbaThresholdAnalyzer();
            Analysis = analyzer.GetAnalysis(source, settings.ColorThreshold, settings.BucketThreshold);
        }
    }
}
