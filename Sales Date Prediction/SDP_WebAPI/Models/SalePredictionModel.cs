using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using SDP_WebAPI.Interfaces;

namespace SDP_WebAPI.Models;

public class SalePredictionModel : IElement
{
    [Required]
    [JsonPropertyName("CustomerName")]
    public string Name { get; init; }

    [Required]
    [JsonPropertyName("LastOrderDate")]
    public DateTime LastOrderDate { get; init; }

    [Required]
    [JsonPropertyName("NextPredictedOrder")]
    public DateTime PredictedOrderDate { get; init; }

    private SalePredictionModel()
    {
    }

    public static IElement FromADOReader(SqlDataReader reader)
    {
        return new SalePredictionModel()
        {
            Name = reader.GetString(0),
            LastOrderDate = reader.GetDateTime(1),
            PredictedOrderDate = reader.GetDateTime(2)
        };
    }
}