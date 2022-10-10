using SixLabors.ImageSharp;

namespace ImageSharp.Processing.AutoCrop.Models
{
    public sealed class WeightAnalysis : IWeightAnalysis
    {
        public PointF Weight { get; set; }
    }
}
