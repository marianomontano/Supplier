using System.Text.Json.Serialization;

namespace Supplier.Core.Models;

public record class ProductResponse
{
    public int Count { get; set; }

    [JsonPropertyName("_products")]
    public IEnumerable<ProductModel> Products { get; set; }
}
