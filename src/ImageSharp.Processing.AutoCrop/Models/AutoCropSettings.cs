namespace ImageSharp.Processing.AutoCrop.Models
{
    public class AutoCropSettings : IAutoCropSettings
    {
        public int PadX { get; set; }
        public int PadY { get; set; }
        public int? ColorThreshold { get; set; }
        public float? BucketThreshold { get; set; }
        public CropMode Mode { get; set; } = CropMode.Pad;
    }
}
