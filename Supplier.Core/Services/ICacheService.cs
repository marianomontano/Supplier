using Supplier.Core.Models;

namespace Supplier.Core.Services;

public interface ICacheService
{
    void ClearCache();
    Task<List<ProductDto>> GetOrSetProducts(CancellationToken cancellationToken = default);
}