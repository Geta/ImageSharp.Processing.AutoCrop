using ImageSharp.Processing.AutoCrop.Extensions;
using ImageSharp.Processing.AutoCrop.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Xunit;

namespace ImageSharp.Processing.AutoCrop.Tests
{
    public class AutoCropTests
    {
        [Fact]
        public void CanCrop()
        {
            var settings = new AutoCropSettings
            { 
                PadX = 5,
                PadY = 5
            };

            using var image = Image.Load("TestImages/test.png");

            image.Mutate(ctx => ctx.AutoCrop(settings));

            Assert.Equal(300, image.Width);
            Assert.Equal(250, image.Height);
        }

        [Fact]
        public void CanPad()
        {
            var cropSettings = new AutoCropSettings
            {
                PadX = 0, 
                PadY = 0
            };

            var padSettings = new AutoCropSettings
            {
                PadX = 20,
                PadY = 20
            };

            using var image = Image.Load("TestImages/test.png");

            image.Mutate(ctx => ctx.AutoCrop(cropSettings));
            image.Mutate(ctx => ctx.AutoCrop(padSettings));

            Assert.Equal(375, image.Width);
            Assert.Equal(325, image.Height);
        }

        [Fact]
        public void CanAnalyze()
        {
            var cropSettings = new AutoCropSettings();
            var cropAnalysis = (ICropAnalysis)null;

            using var image = Image.Load("TestImages/test.png");

            image.Mutate(ctx => ctx.AnalyzeCrop(cropSettings, out cropAnalysis));

            Assert.NotNull(cropAnalysis);
            Assert.Equal(Color.White, cropAnalysis.Background);
            Assert.True(cropAnalysis.Success);
        }

        [Fact]
        public void CanReuseAnalysis()
        {
            var cropSettings = new AutoCropSettings();
            var cropAnalysis = (ICropAnalysis)null;

            using var image = Image.Load("TestImages/test.png");

            image.Mutate(ctx => ctx.AutoCrop(cropSettings, out cropAnalysis));

            Assert.NotNull(cropAnalysis);
            Assert.Equal(Color.White, cropAnalysis.Background);
            Assert.True(cropAnalysis.Success);
        }
    }
}
