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
    }
}
