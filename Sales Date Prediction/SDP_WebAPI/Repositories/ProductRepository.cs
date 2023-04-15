using System.Data;
using Microsoft.Data.SqlClient;
using SDP_WebAPI.Models;

namespace SDP_WebAPI.Repositories;

public class ProductRepository : BaseRepository<ProductModel>
{
    public ProductRepository(IConfiguration config, ILogger logger) : base(config, logger)
    {
    }

    public override async Task<IEnumerable<ProductModel>> GetAll()
    {
        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(TSQLQueries.GetEmployeesQuery, connection);

        try
        {
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            List<ProductModel> result = null;

            if (reader.HasRows)
            {
                result = new List<ProductModel>();
                while (await reader.ReadAsync())
                {
                    result.Add(ProductModel.FromADOReader<ProductModel>(reader));
                }
            }

            await connection.CloseAsync();
            return result;
        }
        catch (Exception e)
        {
            if (connection.State != ConnectionState.Closed)
                await connection.CloseAsync();

            logger.LogError(e, null);
            return null;
        }
    }

    public override async Task<ProductModel> GetById(object id)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Add(ProductModel element)
    {
        throw new NotImplementedException();
    }

    public override async Task<ProductModel> Update(ProductModel element)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Delete(ProductModel element)
    {
        throw new NotImplementedException();
    }
}