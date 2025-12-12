namespace Supplier.Core.Models;

public record ProductModel(
    string Id,
    string Code,
    bool Active,
    string Name,
    string Search,
    string Description,
    decimal SalePrice,
    string ImageThumb,
    CategoryModel Category,
    StockModel Stock);
