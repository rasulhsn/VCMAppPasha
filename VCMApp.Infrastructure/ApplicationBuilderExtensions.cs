using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VCMApp.Infrastructure.Persistence;

namespace VCMApp.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void InfrastructureStart(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<VCMDbContext>();

            DbSeeder.Seed(context);
        }
    }
}
