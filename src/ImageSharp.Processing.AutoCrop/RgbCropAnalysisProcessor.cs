using ImageSharp.Processing.AutoCrop.Analyzers;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop
{
    public class RgbCropAnalysisProcessor : CropAnalysisProcessor<Rgb24>
    {
        public RgbCropAnalysisProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgb24> source) : base(configuration, settings, source)
        {

        }

        public override ICropAnalysis GetAnalysis()
        {
            var analyzer = new RgbThresholdAnalyzer();

            return analyzer.GetAnalysis(Source, Settings.ColorThreshold, Settings.BucketThreshold);
        }
    }
}
