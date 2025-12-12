using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Supplier.Core.Persistence;
using Supplier.Core.Services;

namespace Supplier.Core.Configuration;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddCoreConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=settings.db"));

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
