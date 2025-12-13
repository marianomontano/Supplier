using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Supplier.Core.Persistence;
using Supplier.Core.Services;

namespace Supplier.Core.Configuration;

public static class ConfigurationExtensions
{
    private static string AppDataFolder =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SupplierApp");

    private static string DatabasePath =>
        Path.Combine(AppDataFolder, "settings.db");

    public static IServiceCollection AddCoreConfiguration(this IServiceCollection services)
    {
        if (!Directory.Exists(AppDataFolder))
            Directory.CreateDirectory(AppDataFolder);

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={DatabasePath}"));

        services.AddScoped<IProductService, ProductService>();
        services.AddSingleton<ICacheService, CacheService>();
        services.AddScoped<IExportService, ExportService>();

        return services;
    }
}
