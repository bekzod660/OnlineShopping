using Application.Interfaces;
using OnlineShopping.Services;

namespace ServiceCatalogUI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();
        return services;
    }
}