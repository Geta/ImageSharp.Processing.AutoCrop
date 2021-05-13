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

            using (var image = Image.Load("TestImages/shoe.jpg"))
            {
                image.Mutate(ctx => ctx.AutoCrop(settings));
                image.SaveAsJpeg("shoe_result.jpg");
            }
        }
    }
}
