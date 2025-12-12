using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Supplier.Core.Models;

namespace Supplier.Core.Services;

public class CacheService : ICacheService
{
    private List<ProductDto> _products;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IServiceScopeFactory serviceScopeFactory, ILogger<CacheService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task<List<ProductDto>> GetOrSetProducts(CancellationToken cancellationToken)
    {
        try
        {
            if (_products == null)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                _products = (await productService.GetProducts(cancellationToken)).ToList();
            }

            return _products;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public void ClearCache()
    {
        _products = null;
    }
}
