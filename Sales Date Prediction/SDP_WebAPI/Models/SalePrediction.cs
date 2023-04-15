using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using SDP_WebAPI.Interfaces;

namespace SDP_WebAPI.Models;

public class SalePrediction:IElement
{
    [Required]
    [JsonPropertyName("CustomerName")]
    public string Name { get; init; }
    [Required]
    [JsonPropertyName("LastOrderDate")]
    public DateTime LastOrderDate { get; init; }
    [Required]
    [JsonPropertyName("NextPredictedOrder")]
    public DateTime PredictedOrderDate { get;  init; }
    private SalePrediction(){}

    public static IElement FromADOReader(SqlDataReader reader)
    {
        return new SalePrediction()
        {
            Name = reader.GetString(0),
            LastOrderDate = reader.GetDateTime(1),
            PredictedOrderDate = reader.GetDateTime(2)
        };
    }
}