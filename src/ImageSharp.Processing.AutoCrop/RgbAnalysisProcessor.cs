using ImageSharp.Processing.AutoCrop.Analyzers;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop
{
    public class RgbAnalysisProcessor : AnalysisProcessor<Rgb24>
    {
        private readonly IWeightAnalyzer<Rgb24> _weightAnalyzer;
        private readonly ICropAnalyzer<Rgb24> _cropAnalyzer;

        public RgbAnalysisProcessor(Configuration configuration, IAutoCropSettings settings, Image<Rgb24> source) : base(configuration, settings, source)
        {
            _cropAnalyzer = new RgbThresholdAnalyzer();
            _weightAnalyzer = new RgbWeightAnalyzer();
        }

        public override ICropAnalysis GetCropAnalysis()
        {
            return _cropAnalyzer.GetAnalysis(Source, Settings.ColorThreshold, Settings.BucketThreshold);
        }

        public override IWeightAnalysis GetWeightAnalysis(ICropAnalysis cropAnalysis)
        {
            return _weightAnalyzer.GetAnalysis(Source, cropAnalysis.Background);
        }
    }
}
