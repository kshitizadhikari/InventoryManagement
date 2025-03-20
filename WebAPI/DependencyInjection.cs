using Application;
using Infrastructure;

namespace WebAPI;
public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors();
        services.AddInfrastructureDI(configuration);
        services.AddApplicationDI(configuration);
        return services;
    }
}
