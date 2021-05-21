# ImageSharp.Processing.AutoCrop

## Description

Automatic cropping for images with a flat background.
Works with SixLabors.ImageSharp 1.0.0 and above.

Uses a relative luminance tolerance to determine which area to crop.

## Features

- Crops images with a flat background
- Configurable x and y padding
- Configurable treshold

## How to get started?

### ImageSharp.Web integration

Requires SixLabors.ImageSharp 1.0.1 or above

- `install-package ImageSharp.Web.AutoCrop`

The namespace `ImageSharp.Web.AutoCrop.Extensions` contains some useful extensions to `SixLabors.ImageSharp.Web.DependencyInjection.IImageSharpBuilder`

#### In Startup.cs

Make sure to call the extension `AddAutoCropProcessor` when adding ImageSharp to services, that's all.

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddImageSharp()
            .AddAutoCropProcessor();
    ...
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Configure ImageSharp.Web
    app.UseImageSharp();
    ...
}
```

Check out the [demo](docs/demo.mhtml) page for some examples.
Check out the [demo project](src/ImageSharp.Web.AutoCrop.Demo) for the full source.

### Plain AutoCrop

Requires SixLabors.ImageSharp 1.0.0 or above

- `install-package ImageSharp.Processing.AutoCrop`

The namespace `ImageSharp.Processing.AutoCrop.Extensions` contains some useful extensions to `SixLabors.ImageSharp.Processing.IImageProcessingContext`

```
using var image = Image.Load("image.png");

image.Mutate(ctx => ctx.AutoCrop());
```

Check the different overloads for some options.



## Details

The underlying `AutoCropProcessor` that will handle the actual cropping takes some parameters.

| Parameter | Description | Default value |
| --------- | ----------- | ------- |
| PadX | How much horizontal whitespace in percent (0-100) to apply outside the crop | 0 |
| PadY | How much vertical whitespace in percent (0-100) to apply outside the crop | 0 |
| ColorThreshold | Color divergence to tolerate from analyzed border color (0-255) | 35 |
| BucketThreshold | How many percent of the border that has to belong to the most present color bucket (0.0f-1.0f) | 0.945f |

## How it works
![How it works](docs/how-it-works.png)

## Package maintainer

https://github.com/svenrog

## Changelog

[Changelog](CHANGELOG.md)