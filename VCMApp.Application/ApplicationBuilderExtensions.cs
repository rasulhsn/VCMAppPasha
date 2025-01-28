using Microsoft.AspNetCore.Builder;
using VCMApp.Infrastructure;

namespace VCMApp.Application
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplicationStart(this IApplicationBuilder app)
        {
            app.InfrastructureStart();
        }
    }
}
