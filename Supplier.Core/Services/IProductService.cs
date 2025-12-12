using Supplier.Core.Models;

namespace Supplier.Core.Services;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetProducts(CancellationToken cancellationToken);
}