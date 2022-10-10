using ImageSharp.Web.AutoCrop.Demo.Infrastructure.Caching;
using ImageSharp.Web.AutoCrop.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp.Web.DependencyInjection;

namespace ImageSharp.Web.AutoCrop.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure ImageSharp.Web for service container
            services.AddImageSharp()
                    .SetCache<NullCache>() // This is for realtime updates of demo
                    .AddAutoCropProcessor();

            // Configure rest of services
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Configure ImageSharp.Web
            app.UseImageSharp();

            // Rest of web configuration
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
