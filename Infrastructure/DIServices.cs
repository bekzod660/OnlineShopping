using Application.Abstraction;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Data_Access;
using Infrastructure.DataAccess.Interceptor;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DIServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("PostgresDB")));
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            return services;
        }
    }
}
