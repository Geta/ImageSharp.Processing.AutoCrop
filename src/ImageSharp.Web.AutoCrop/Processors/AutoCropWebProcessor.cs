using ImageSharp.Processing.AutoCrop.Extensions;
using ImageSharp.Processing.AutoCrop.Models;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Web;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.Processors;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImageSharp.Web.AutoCrop.Processors
{
    public class AutoCropWebProcessor : IImageWebProcessor
    {
        private const string _autoCropCommandKey = "autocrop";
        private const string _backgroundColorCommandKey = "bgcolor";

        private static readonly IEnumerable<string> _autoCropCommands = new string[1]
        {
            _autoCropCommandKey
        };

        public IEnumerable<string> Commands => _autoCropCommands;

        public FormattedImage Process(FormattedImage image, ILogger logger, IDictionary<string, string> commands, CommandParser parser, CultureInfo culture)
        {
            var parameter = commands.GetValueOrDefault(_autoCropCommandKey);
            var parsed = ParseSettings(parameter, out var settings);

            if (parsed) 
            {
                try
                {
                    ICropAnalysis analysis = null;
                    image.Image.Mutate((ctx) => ctx.AutoCrop(settings, out analysis));

                    if (analysis.Success && !commands.ContainsKey(_backgroundColorCommandKey))
                    {
                        commands.Add(_backgroundColorCommandKey, analysis.Background.ToHex());
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }                
            }

            return image;
        }

        private bool ParseSettings(string parameter, out AutoCropSettings settings)
        {
            settings = null;

            if (string.IsNullOrWhiteSpace(parameter))
                return false;

            var data = parameter.Split(',', ';', '|');
            var parsed = int.TryParse(data[0], out var padX);
            if (!parsed)
                return false;

            settings = new AutoCropSettings
            {
                PadX = Clamp(padX, 0, 100)
            };

            if (data.Length > 3 && int.TryParse(data[3], out var bucketThreshold))
            {
                settings.BucketThreshold = Clamp(bucketThreshold, 0, 100) / 100.0f;
            }
            else
            {
                settings.BucketThreshold = 0.945f;
            }

            if (data.Length > 2 && int.TryParse(data[2], out var colorThreshold))
            {
                settings.ColorThreshold = Clamp(colorThreshold, 0, 254);
            }
            else
            {
                settings.ColorThreshold = 35;
            }

            if (data.Length > 1 && int.TryParse(data[1], out var padY))
            {
                settings.PadY = Clamp(padY, 0, 100);
            }
            else
            {
                settings.PadY = Clamp(padX, 0, 100);
            }

            return true;
        }

        private int Clamp(int value, int min, int max)
        {
            if (value > max)
                return max;

            if (value < min)
                return min;

            return value;
        }
    }
}
