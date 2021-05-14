using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using System;

namespace ImageSharp.Processing.AutoCrop
{
    public class CropAnalysisProcessor : IImageProcessor
    {
        private readonly IAutoCropSettings _settings;

        public ICropAnalysis Analysis { get; set; }

        public CropAnalysisProcessor(IAutoCropSettings settings)
        {
            _settings = settings;
        }

        public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle) where TPixel : unmanaged, IPixel<TPixel>
        {
            if (source is Image<Rgb24> rgbSource)
            {
                var processor = new RgbCropAnalysisProcessor(configuration, _settings, rgbSource);
                Analysis = processor.GetAnalysis();

                return (IImageProcessor<TPixel>)processor;
            }
            else if (source is Image<Rgba32> rgbaSource)
            {
                var processor = new RgbaCropAnalysisProcessor(configuration, _settings, rgbaSource);
                Analysis = processor.GetAnalysis();

                return (IImageProcessor<TPixel>)processor;
            }

            throw new NotSupportedException("Unsupported pixel type");
        }
    }

    public abstract class CropAnalysisProcessor<TPixel> : IImageProcessor<TPixel> where TPixel : unmanaged, IPixel<TPixel>
    {
        protected readonly Configuration Configuration;
        protected readonly IAutoCropSettings Settings;
        protected readonly Image<TPixel> Source;

        public ICropAnalysis Analysis { get; }

        protected CropAnalysisProcessor(Configuration configuration, IAutoCropSettings settings, Image<TPixel> source)
        {
            Configuration = configuration;
            Source = source;
            Settings = settings;
        }

        public void Execute()
        {
            // Do nothing
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public abstract ICropAnalysis GetAnalysis();
       
        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
