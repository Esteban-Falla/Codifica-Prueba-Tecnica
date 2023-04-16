namespace SDP_WebAPI.Models;

public class AddOrderModel : OrderModel
{
    [Required]
    [Range(0,int.MaxValue)]
    [JsonPropertyName("EmployeeId")]
    public int EmpId { get; init; }
    
    [Required]
    [Range(0,int.MaxValue)]
    [JsonPropertyName("ShipperId")]
    public int ShipId { get; init; }
    
    [Required]
    [JsonPropertyName("OrderDate")]
    public DateTime Date { get; init; }
    
    [Required]
    [JsonPropertyName("Freight")]
    public double Freight { get; init; }
    
    [Required]
    [JsonPropertyName("ShipCountry")]
    public string Country { get; init; }
    
    [Required]
    [Range(0,int.MaxValue)]
    [JsonPropertyName("ProductId")]
    public int ProdId { get; init; }
    
    [Required]
    [Range(0.0,double.MaxValue)]
    [JsonPropertyName("UnitPrice")]
    public double UnitPrice { get; init; }
    
    [Required]
    [Range(0,int.MaxValue)]
    [JsonPropertyName("Quantity")]
    public int Qty { get; init; }
    
    [Required]
    [Range(0.0,double.MaxValue)]
    [JsonPropertyName("Discount")]
    public double Discount { get; init; }

    public static AddOrderModel Build(OrderModel baseOrder)
    {
        return new AddOrderModel()
        {
            Id = baseOrder.Id,
            ReqDate = baseOrder.ReqDate,
            ShipDate = baseOrder.ShipDate,
            Name = baseOrder.Name,
            Address = baseOrder.Address,
            City = baseOrder.City
        };
    }
}