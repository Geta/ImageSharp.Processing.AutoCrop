using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp.Processing;

namespace ImageSharp.Processing.AutoCrop.Extensions
{
    public static class ProcessingContextExtensions
    {
        public static void AutoCrop(this IImageProcessingContext context, IAutoCropSettings settings)
        {
            context.ApplyProcessor(new AutoCropProcessor(settings));
        }

        public static void AutoCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis analysis)
        {
            var processor = new AutoCropProcessor(settings);
            context.ApplyProcessor(processor);
            analysis = processor.Analysis;
        }

        public static void AnalyzeCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis analysis)
        {
            var processor = new CropAnalysisProcessor(settings);
            context.ApplyProcessor(processor);
            analysis = processor.Analysis;
        }
    }
}
