using System;
using ImageSharp.Processing.AutoCrop.Extensions;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageSharp.Processing.AutoCrop.Analyzers
{
    public abstract class ThresholdAnalyzer<TPixel> : ICropAnalyzer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
    {
        protected abstract IBorderAnalysis GetBorderAnalysis(Image<TPixel> image, Rectangle rectangle, int colorThreshold, float bucketTreshold);
        protected abstract Rectangle GetBoundingBox(Image<TPixel> image, Rectangle rectangle, IBorderAnalysis borderAnalysis, int colorThreshold);

        public virtual ICropAnalysis GetAnalysis(Image<TPixel> image, int colorThreshold, float bucketTreshold)
        {
            var outerBox = new Rectangle(0, 0, image.Width, image.Height);
            var imageBox = outerBox;

            var borderInspection = GetBorderAnalysis(image, imageBox, colorThreshold, bucketTreshold);
            if (borderInspection.Success == false)
            {
                colorThreshold = (int)Math.Round(colorThreshold * 0.5);
                bucketTreshold = 1.0f;
                imageBox = imageBox.Contract(10);

                var additionalInspection = GetBorderAnalysis(image, imageBox, colorThreshold, bucketTreshold);
                if (additionalInspection.Success)
                {
                    borderInspection = additionalInspection;
                }
            }

            Rectangle boundingBox;
            bool _foundBoundingBox;

            if (borderInspection.Success == false)
            {
                boundingBox = outerBox;
                _foundBoundingBox = false;
            }
            else
            {
                boundingBox = GetBoundingBox(image, imageBox, borderInspection, colorThreshold);
                _foundBoundingBox = ValidateRectangle(boundingBox);
            }

            return new CropAnalysis
            {
                Background = borderInspection.Background,
                BoundingBox = boundingBox,
                Success = _foundBoundingBox
            };
        }

        private static bool ValidateRectangle(Rectangle rectangle)
        {
            if (rectangle.Width < 3) return false;
            if (rectangle.Height < 3)  return false;

            return true;
        }

        /*
         Bounding box
        */
        
        
    }
}
