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
                PadY = 5,
                ColorThreshold = 35,
                BucketThreshold = 0.945f
            };

            using var image = Image.Load("TestImages/test.png");

            image.Mutate(ctx => ctx.AutoCrop(settings));

            Assert.Equal(301, image.Width);
            Assert.Equal(251, image.Height);
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

            Assert.Equal(377, image.Width);
            Assert.Equal(327, image.Height);
        }

        [Fact]
        public void CanPadOnRightEdge()
        {
            var settings = new AutoCropSettings
            {
                PadX = 20,
                PadY = 20
            };

            using var image = Image.Load("TestImages/test_edge.png");

            image.Mutate(ctx => ctx.AutoCrop(settings));

            image.SaveAsPng("TestImages/test_edge_right_result.png");

            Assert.Equal(363, image.Width);
            Assert.Equal(325, image.Height);
        }

        [Fact]
        public void CanPadOnLeftEdge()
        {
            var settings = new AutoCropSettings
            {
                PadX = 20,
                PadY = 20
            };

            using var image = Image.Load("TestImages/test_edge_left.png");

            image.Mutate(ctx => ctx.AutoCrop(settings));

            image.SaveAsPng("TestImages/test_edge_left_result.png");

            Assert.Equal(377, image.Width);
            Assert.Equal(327, image.Height);
        }


        [Fact]
        public void CanAnalyzeCrop()
        {
            var settings = new AutoCropSettings();
            var cropAnalysis = (ICropAnalysis)null;

            using var image = Image.Load("TestImages/test.png");

            image.Mutate(ctx => ctx.AnalyzeCrop(settings, out cropAnalysis));

            Assert.NotNull(cropAnalysis);
            Assert.Equal(Color.White, cropAnalysis.Background);
            Assert.True(cropAnalysis.Success);
        }

        [Fact]
        public void CanReuseAnalysis()
        {
            var settings = new AutoCropSettings();
            var cropAnalysis = (ICropAnalysis)null;
            var weightAnalysis = (IWeightAnalysis)null;

            using var sourceImage = Image.Load("TestImages/test.png");

            using var firstImage = sourceImage.Clone(ctx => ctx.AutoCrop(settings, out cropAnalysis, out weightAnalysis));
            using var secondImage = sourceImage.Clone(ctx => ctx.AutoCropKnown(settings, cropAnalysis, weightAnalysis));

            Assert.True(cropAnalysis.Success);
            Assert.NotNull(secondImage);
            Assert.Equal(secondImage.Size(), firstImage.Size());
        }

        [Fact]
        public void CanAnalyzeWeights()
        {
            var settings = new AutoCropSettings
            {
                AnalyzeWeights = true
            };

            var weightAnalysis = (IWeightAnalysis)null;

            using var image = Image.Load("TestImages/test.png");

            image.Mutate(ctx => ctx.AutoCrop(settings, out _, out weightAnalysis));

            Assert.NotNull(weightAnalysis);
            Assert.Equal(new PointF(-0.0002448041f, -0.0009081508f), weightAnalysis.Weight);
        }
    }
}
