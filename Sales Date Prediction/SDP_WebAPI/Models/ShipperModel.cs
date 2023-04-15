using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using SDP_WebAPI.Interfaces;

namespace SDP_WebAPI.Models;

public class ShipperModel : IElement
{
    [Required]
    [Range(0, int.MaxValue)]
    [JsonPropertyName("ShipperId")]
    public int Id { get; init; }

    [Required]
    [JsonPropertyName("CompanyName")]
    public string Name { get; init; }

    public static T FromADOReader<T>(SqlDataReader reader) where T : IElement
    {
        var result = new ShipperModel()
        {
            Id = reader.GetInt32(
                reader.GetOrdinal("Shipperid")),
            Name = reader.GetString(
                reader.GetOrdinal("Companyname"))
        };

        return (T)Convert.ChangeType(result, typeof(T));
    }
}