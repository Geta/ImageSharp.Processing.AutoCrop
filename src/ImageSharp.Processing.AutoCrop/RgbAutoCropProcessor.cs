using ImageSharp.Processing.AutoCrop.Analyzers;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop
{
    public class RgbAutoCropProcessor : AutoCropProcessor<Rgb24>
    {
        private readonly ICropAnalysis _analysis;

        public RgbAutoCropProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgb24> source, Rectangle sourceRectangle) : base(configuration, settings, source, sourceRectangle)
        {
            var analyzer = new RgbThresholdAnalyzer();
            _analysis = analyzer.GetAnalysis(source, settings.ColorThreshold, settings.BucketThreshold);
        }

        protected override ICropAnalysis GetAnalysis()
        {
            return _analysis;
        }
    }
}
