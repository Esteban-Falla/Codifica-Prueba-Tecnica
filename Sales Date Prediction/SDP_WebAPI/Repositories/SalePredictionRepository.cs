using System.Data;
using Microsoft.Data.SqlClient;
using SDP_WebAPI.Models;

namespace SDP_WebAPI.Repositories;

public class SalePredictionRepository : BaseRepository<SalePrediction>
{
    public SalePredictionRepository(IConfiguration config, ILogger logger) : base(config, logger)
    {
    }

    public override async Task<IEnumerable<SalePrediction>> GetAll()
    {
        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(TSQLQueries.GetSalePredictions, connection);
        try
        {
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            List<SalePrediction> results = null;

            if (reader.HasRows)
            {
                results = new List<SalePrediction>();
                while (await reader.ReadAsync())
                {
                    results.Add((SalePrediction)SalePrediction.FromADOReader(reader));
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

    public override async Task<SalePrediction> GetById(object id)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Add(SalePrediction element)
    {
        throw new NotImplementedException();
    }

    public override async Task<SalePrediction> Update(SalePrediction element)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Delete(SalePrediction element)
    {
        throw new NotImplementedException();
    }
}