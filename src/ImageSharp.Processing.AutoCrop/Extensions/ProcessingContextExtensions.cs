using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp.Processing;
using System;

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

        public static bool TryAutoCrop(this IImageProcessingContext context, IAutoCropSettings settings)
        {
            try
            {
                AutoCrop(context, settings);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool TryAutoCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis analysis)
        {
            try
            {
                AutoCrop(context, settings, out analysis);
                return true;
            }
            catch (Exception)
            {
                analysis = null;
                return false;
            }
        }

        public static bool TryAnalyzeCrop(this IImageProcessingContext context, IAutoCropSettings settings, out ICropAnalysis analysis)
        {
            try
            {
                AnalyzeCrop(context, settings, out analysis);
                return true;
            }
            catch (Exception)
            {
                analysis = null;
                return false;
            }
        }
    }
}
