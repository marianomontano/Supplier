namespace Supplier.Core.Models;

public class ProductDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public decimal Public { get; set; }
    public string Provider { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; }
    public string ImageUrl { get; set; }

    public static ProductDto FromProductModel(ProductModel p, int markup)
    {
        var salePrice = p?.SalePrice ?? 0m;
        var normalizedMarkup = Math.Clamp(markup, 0, 99);
        var publicPrice = normalizedMarkup == 0
            ? salePrice
            : 100m * salePrice / (100m - normalizedMarkup);

        return new ProductDto
        {
            Code = p?.Code ?? string.Empty,
            Name = p?.Name ?? string.Empty,
            Cost = salePrice,
            Public = Math.Round(publicPrice, 2),
            Provider = p?.Category?.Name ?? string.Empty,
            Stock = p?.Stock?.Current ?? 0,
            IsActive = p?.Active ?? false,
            ImageUrl = "https://images-cdn.kyte.site/v0/b/kyte-7c484.appspot.com/o" + p.ImageThumb
        };
    }
}
