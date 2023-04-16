namespace SDP_WebAPI.Repositories;

internal static class TSQLQueries
{
    private const string GetOrdersByCustIdQuery = @"SELECT 
        o.orderid [Orderid],
        o.requireddate [Requireddate],
        o.shippeddate [Shippeddate],
        o.shipname [Shipname],
        o.shipaddress [Shipaddress],
        o.shipcity [Shipcity]
        FROM [StoreSample].[Sales].[Orders] o
        WHERE o.custid = {0};";

    public const string GetEmployeesQuery = @"SELECT
	    e.empid [Empid],
	    CONCAT(e.firstname,' ',e.lastname) [FullName] 
        FROM [StoreSample].[HR].[Employees] e;";

    public const string GetShippersQuery = @"SELECT
	    s.shipperid [Shipperid],
	    s.companyname [Companyname]
        FROM [StoreSample].[Sales].[Shippers] s;";

    public const string GetProductsQuery = @"SELECT
	    p.productid [Productid],
	    p.productname [Productname]
        FROM [StoreSample].[Production].[Products] p;";

    public const string AddOrderQuery = @"[StoreSample].[Sales].[AddOrder]";

    public const string GetSalePredictions = @"SELECT 
        CustomerName, 
        LastOrderDate, 
        NextPredictedOrder
        FROM [StoreSample].[Sales].[LastAndNextOrderDate];";

    public static string GetOrdersByCustomerIdQuery(int Id)
    {
        return string.Format(GetOrdersByCustIdQuery, Id);
    }
}