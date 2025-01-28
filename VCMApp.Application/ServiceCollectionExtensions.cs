using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VCMApp.Application.DTOs.Mappings;
using VCMApp.Infrastructure;

namespace VCMApp.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
  
            services.AddAutoMapper(typeof(DTOsMapping));

            services.AddInfrastructureServices(configuration);
            return services;
        }
    }
}
