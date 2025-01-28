using Microsoft.AspNetCore.Builder;

namespace VCMApp.Application
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ApplicationStart(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
