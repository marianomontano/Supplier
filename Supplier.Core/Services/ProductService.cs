using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Supplier.Core.Models;
using Supplier.Core.Persistence;
using System.Net.Http.Json;

namespace Supplier.Core.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ProductService> _logger;

    public ProductService(HttpClient httpClient, AppDbContext dbContext, ILogger<ProductService> logger)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductModel>> GetProducts(CancellationToken cancellationToken)
    {
        try
        {
            var settings = await _dbContext.Settings.FirstOrDefaultAsync(cancellationToken);

            if (settings is null)
            {
                throw new Exception("Settings not found in the database.");
            }

            var results = await _httpClient.GetFromJsonAsync<ProductResponse>(
                settings.ApiUrl + "?limit=1000",
                cancellationToken);

            return results?.Products;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
