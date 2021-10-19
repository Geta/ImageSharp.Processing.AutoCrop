using System;

namespace ImageSharp.Processing.AutoCrop.Models
{
    [Flags]
    public enum CropMode
    {
        Contain = 0,
        Pad = 1,
    }
}
