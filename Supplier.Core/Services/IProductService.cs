using Supplier.Core.Models;

namespace Supplier.Core.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts(CancellationToken cancellationToken);
}