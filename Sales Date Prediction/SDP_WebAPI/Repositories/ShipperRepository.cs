using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SDP_WebAPI.Models;

namespace SDP_WebAPI.Repositories;

public class ShipperRepository : BaseRepository<ShipperModel>
{
    public ShipperRepository(IOptions<DatabaseOptions> databaseOptions, ILogger logger) : base(databaseOptions, logger)
    {
    }

    public override async Task<IEnumerable<ShipperModel>> GetAll()
    {
        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(TSQLQueries.GetEmployeesQuery, connection);

        try
        {
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            List<ShipperModel> result = null;

            if (reader.HasRows)
            {
                result = new List<ShipperModel>();
                while (await reader.ReadAsync())
                {
                    result.Add(ShipperModel.FromADOReader<ShipperModel>(reader));
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

    public override async Task<ShipperModel> GetById(object id)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Add(ShipperModel element)
    {
        throw new NotImplementedException();
    }

    public override async Task<ShipperModel> Update(ShipperModel element)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Delete(ShipperModel element)
    {
        throw new NotImplementedException();
    }
}