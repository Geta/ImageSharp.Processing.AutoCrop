using ImageSharp.Web.AutoCrop.Processors;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Processors;

namespace ImageSharp.Web.AutoCrop.Extensions
{
    public static class ImageSharpBuilderExtensions
    {
        public static IImageSharpBuilder AddAutoCropProcessor(this IImageSharpBuilder builder) 
        {
            if (builder == null)
                return builder;

            var index = IndexOfProcessor<ResizeWebProcessor>(builder.Services);
            if (index < 0)
            {
                builder.AddProcessor<AutoCropWebProcessor>();
            }
            else
            {
                builder.Services.Insert(index, ServiceDescriptor.Singleton<IImageWebProcessor, AutoCropWebProcessor>());
            } 

            return builder;
        }

        private static int IndexOfProcessor<TProcessor>(IServiceCollection services) where TProcessor : class, IImageWebProcessor
        {
            for (var i = 0; i < services.Count; i++)
            {
                var service = services[i];
                if (service.Lifetime != ServiceLifetime.Singleton)
                    continue;

                if (service.ServiceType != typeof(IImageWebProcessor))
                    continue;

                if (service.ImplementationType != typeof(TProcessor))
                    continue;

                return i;
            }

            return -1;
        }
    }
}
