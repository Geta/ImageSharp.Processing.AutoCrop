using ImageSharp.Processing.AutoCrop.Analyzers;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop
{
    public class RgbAutoCropProcessor : AutoCropProcessor<Rgb24>
    {
        public RgbAutoCropProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgb24> source) : base(configuration, settings, source)
        {
            var analyzer = new RgbThresholdAnalyzer();
            Analysis = analyzer.GetAnalysis(source, settings.ColorThreshold, settings.BucketThreshold);
        }
    }
}
