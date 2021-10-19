using SixLabors.ImageSharp.Web;
using SixLabors.ImageSharp.Web.Caching;
using SixLabors.ImageSharp.Web.Resolvers;
using System.IO;
using System.Threading.Tasks;

namespace ImageSharp.Web.AutoCrop.Demo.Infrastructure.Caching
{
    public class NullCache : IImageCache
    {
        public Task<IImageCacheResolver> GetAsync(string key)
        {
            return Task.FromResult<IImageCacheResolver>(null);
        }

        public Task SetAsync(string key, Stream stream, ImageCacheMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}
