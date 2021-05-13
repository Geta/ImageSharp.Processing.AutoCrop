using ImageSharp.Processing.AutoCrop.Analyzers;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop
{
    public class RgbaAutoCropProcessor : AutoCropProcessor<Rgba32>
    {
        private readonly ICropAnalysis _analysis;

        public RgbaAutoCropProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgba32> source, Rectangle sourceRectangle) : base(configuration, settings, source, sourceRectangle)
        {
            var analyzer = new RgbaThresholdAnalyzer();
            _analysis = analyzer.GetAnalysis(source, settings.ColorThreshold, settings.BucketThreshold);
        }

        protected override ICropAnalysis GetAnalysis()
        {
            return _analysis;
        }
    }
}
