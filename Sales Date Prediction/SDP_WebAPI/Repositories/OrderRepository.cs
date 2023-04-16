using System.Data;

namespace SDP_WebAPI.Repositories;

public class OrderRepository : BaseRepository<OrderModel>, IOrderRepository
{
    public OrderRepository(IOptions<DatabaseOptions> databaseOptions, ILogger<OrderModel> logger)
        : base(databaseOptions, logger)
    {
    }

    public override async Task<IEnumerable<OrderModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public override async Task<OrderModel> GetById(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrderModel>> GetByClientId(int clientId)
    {
        ValidateParams(clientId);

        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(
            TSQLQueries.GetOrdersByCustomerIdQuery(clientId),
            connection);

        try
        {
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            List<OrderModel> results = null;

            if (reader.HasRows)
            {
                results = new List<OrderModel>();
                while (await reader.ReadAsync())
                {
                    results.Add(OrderModel.FromADOReader<OrderModel>(reader));
                }
            }

            reader.CloseAsync();
            connection.CloseAsync();
            return results;
        }
        catch (Exception e)
        {
            if (connection.State != ConnectionState.Closed)
                await connection.CloseAsync();

            logger.LogError(e, null);
            return null;
        }
    }

    public override async Task<int> Add(OrderModel element)
    {
        ValidateParams(element);
        return await Add(AddOrderModel.Build(element));
    }

    public async Task<int> Add(AddOrderModel element)
    {
        ValidateParams(element);

        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(TSQLQueries.AddOrderQuery);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("EmpId", element.EmpId);
        command.Parameters.AddWithValue("ShipperId", element.ShipId);
        command.Parameters.AddWithValue("ShipName", element.Name);
        command.Parameters.AddWithValue("ShipAddr", element.Address);
        command.Parameters.AddWithValue("ShipCity", element.City);
        command.Parameters.AddWithValue("OrderDate", element.Date);
        command.Parameters.AddWithValue("ReqDate", element.ReqDate);
        command.Parameters.AddWithValue("ShipDate", element.ShipDate);
        command.Parameters.AddWithValue("Freight", element.Freight);
        command.Parameters.AddWithValue("ShipCountry", element.Country);
        command.Parameters.AddWithValue("ProdId", element.ProdId);
        command.Parameters.AddWithValue("UnitPrice", element.UnitPrice);
        command.Parameters.AddWithValue("Qty", element.Qty);
        command.Parameters.AddWithValue("Discount", element.Discount);

        try
        {
            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
            return rowsAffected;
        }
        catch (Exception e)
        {
            if (connection.State != ConnectionState.Closed)
                await connection.CloseAsync();

            logger.LogError(e, null);
            return 0;
        }
    }

    public override async Task<OrderModel> Update(OrderModel element)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Delete(OrderModel element)
    {
        throw new NotImplementedException();
    }
}