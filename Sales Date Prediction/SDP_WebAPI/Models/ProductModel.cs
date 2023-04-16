namespace SDP_WebAPI.Models;

public class ProductModel : IElement
{
    [Required]
    [Range(0, int.MaxValue)]
    [JsonPropertyName("ProductId")]
    public int Id { get; init; }

    [Required]
    [JsonPropertyName("ProductName")]
    public string Name { get; init; }

    public static T FromADOReader<T>(SqlDataReader reader) where T : IElement
    {
        var result = new ProductModel()
        {
            Id = reader.GetInt32(
                reader.GetOrdinal("Productid")),
            Name = reader.GetString(
                reader.GetOrdinal("Productname"))
        };

        return (T)Convert.ChangeType(result, typeof(T));
    }
}