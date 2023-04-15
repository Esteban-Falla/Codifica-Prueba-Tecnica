using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using SDP_WebAPI.Interfaces;

namespace SDP_WebAPI.Models;

public class OrderModel : IElement
{
    [Required]
    [Range(0, int.MaxValue)]
    [JsonPropertyName("OrderId")]
    public int Id { get; init; }

    [Required]
    [JsonPropertyName("RequiredDate")]
    public DateTime ReqDate { get; init; }

    [Required]
    [JsonPropertyName("ShippedDate")]
    public DateTime ShipDate { get; init; }

    [Required]
    [JsonPropertyName("ShipName")]
    public string Name { get; init; }

    [Required]
    [JsonPropertyName("ShipAddress")]
    public string Address { get; init; }

    [Required]
    [JsonPropertyName("ShipCity")]
    public string City { get; init; }

    private protected OrderModel()
    {
    }

    public static IElement FromADOReader(SqlDataReader reader)
    {
        return new OrderModel()
        {
            Id = reader.GetInt32(
                reader.GetOrdinal("Orderid")),
            ReqDate = reader.GetDateTime(
                reader.GetOrdinal("Requireddate")),
            ShipDate = reader.GetDateTime(
                reader.GetOrdinal("Shippeddate")),
            Name = reader.GetString(
                reader.GetOrdinal("Shipname")),
            Address = reader.GetString(
                reader.GetOrdinal("Shipaddress")),
            City = reader.GetString(
                reader.GetOrdinal("Shipcity"))
        };
    }
}