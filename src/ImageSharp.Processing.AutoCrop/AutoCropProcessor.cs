using ImageSharp.Processing.AutoCrop.Extensions;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using System;

namespace ImageSharp.Processing.AutoCrop
{
    public class AutoCropProcessor : CloningImageProcessor
    {
        private readonly IAutoCropSettings _settings;

        public AutoCropProcessor(IAutoCropSettings settings)
        {
            _settings = settings;
        }

        public override ICloningImageProcessor<TPixel> CreatePixelSpecificCloningProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        {
            if (source is Image<Rgb24> rgbSource)
            {
                return (ICloningImageProcessor<TPixel>)new RgbAutoCropProcessor(configuration, _settings, rgbSource, sourceRectangle);
            }
            else if (source is Image<Rgba32> rgbaSource)
            {
                return (ICloningImageProcessor<TPixel>)new RgbaAutoCropProcessor(configuration, _settings, rgbaSource, sourceRectangle);
            }

            throw new NotSupportedException("Unsupported pixel type");
        }
    }

    public abstract class AutoCropProcessor<TPixel> : ICloningImageProcessor<TPixel> where TPixel : unmanaged, IPixel<TPixel>
    {
        protected readonly Configuration Configuration;
        protected readonly IAutoCropSettings Settings;
        protected readonly Image<TPixel> Source;
        protected readonly Rectangle SourceRectangle;

        protected AutoCropProcessor(Configuration configuration, IAutoCropSettings settings, Image<TPixel> source, Rectangle sourceRectangle)
        {
            Configuration = configuration;
            Source = source;
            Settings = settings;
            SourceRectangle = sourceRectangle;
        }

        public Image<TPixel> CloneAndExecute()
        {
            try
            {
                var analysis = GetAnalysis();
                if (analysis.Success)
                {
                    var target = CreateTarget();
                    ApplySource(target);

                    return target;
                }

                return Source;
            }
            catch (Exception innerException)
            {
                throw new ImageProcessingException("An error occurred when processing the image using " + GetType().Name + ". See the inner exception for more detail.", innerException);
            }
        }

        public void Execute()
        {
            Image<TPixel> image = null;
            try
            {
                image = CloneAndExecute();
                Source.SwapPixelBuffersFrom(image);
            }
            finally
            {
                image?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected abstract ICropAnalysis GetAnalysis();

        protected virtual void Dispose(bool disposing)
        {

        }

        protected virtual void ApplySource(Image<TPixel> image)
        {
            var analysis = GetAnalysis();
            var sourceBox = analysis.BoundingBox;
            var targetBox = GetTargetBounds(sourceBox);
            var offset = GetOffset(sourceBox, targetBox);

            image.CopyFrom(Source, sourceBox, offset);
        }

        protected virtual Size GetDestinationSize()
        {
            var analysis = GetAnalysis();
            var bounds = GetTargetBounds(analysis.BoundingBox);
            return bounds.Size();
        }

        protected virtual Point GetOffset(Rectangle source, Rectangle target)
        {
            var x = (target.Width - source.Width) / 2 - source.Left;
            var y = (target.Height - source.Height) / 2 - source.Top;

            return new Point(x, y);
        }

        protected virtual Rectangle GetTargetBounds(Rectangle boundingBox)
        {
            var bounds = boundingBox;
            var dimension = (bounds.Width + bounds.Height) / 2;

            var px = Settings.PadX / 100.0;
            var py = Settings.PadY / 100.0;

            var paddedBounds = bounds.Expand((int)(dimension * px), (int)(dimension * py));

            return new Rectangle(0, 0, paddedBounds.Width, paddedBounds.Height);
        }

        private Image<TPixel> CreateTarget()
        {
            var analysis = GetAnalysis();
            var destinationSize = GetDestinationSize();
            var background = analysis.Background.ToPixel<TPixel>();

            return new Image<TPixel>(Configuration, destinationSize.Width, destinationSize.Height, background);
        }       
    }
}
