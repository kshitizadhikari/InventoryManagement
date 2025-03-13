using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            //services.AddScoped<IMenuService, MenuService>();
            //services.AddScoped<IServiceWrapper, ServiceWrapper>();
            return services;
        }
    }
}
