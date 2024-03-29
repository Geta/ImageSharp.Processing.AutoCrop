﻿using SixLabors.ImageSharp;

namespace ImageSharp.Processing.AutoCrop.Models
{
    public interface ICropAnalysis
    {
        Rectangle BoundingBox { get; }
        Color Background { get; }
        bool Success { get; }
    }
}
