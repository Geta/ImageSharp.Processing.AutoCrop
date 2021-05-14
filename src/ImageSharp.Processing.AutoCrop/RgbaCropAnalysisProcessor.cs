using ImageSharp.Processing.AutoCrop.Analyzers;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop
{
    public class RgbaCropAnalysisProcessor : CropAnalysisProcessor<Rgba32>
    {
        public RgbaCropAnalysisProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgba32> source) : base(configuration, settings, source)
        {

        }

        public override ICropAnalysis GetAnalysis()
        {
            var analyzer = new RgbaThresholdAnalyzer();
            return analyzer.GetAnalysis(Source, Settings.ColorThreshold, Settings.BucketThreshold);
        }
    }
}
