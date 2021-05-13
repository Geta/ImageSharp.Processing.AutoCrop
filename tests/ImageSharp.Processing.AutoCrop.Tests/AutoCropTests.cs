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
                ColorThreshold = 35,
                PadX = 5,
                PadY = 5
            };

            using (var image = Image.Load("TestImages/test.png"))
            {
                image.Mutate(ctx => ctx.AutoCrop(settings));

                Assert.Equal(300, image.Width);
                Assert.Equal(250, image.Height);

                image.SaveAsPng("test_result.png");
            }
        }
    }
}
