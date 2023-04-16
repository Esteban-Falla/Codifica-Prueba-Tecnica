using System.Data;

namespace SDP_WebAPI.Repositories;

public class SalesPredictionRepository : BaseRepository<SalePredictionModel>
{
    public SalesPredictionRepository(IOptions<DatabaseOptions> databaseOptions, ILogger logger)
        : base(databaseOptions, logger)
    {
    }

    public override async Task<IEnumerable<SalePredictionModel>> GetAll()
    {
        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(TSQLQueries.GetSalePredictions, connection);
        try
        {
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            List<SalePredictionModel> results = null;

            if (reader.HasRows)
            {
                results = new List<SalePredictionModel>();
                while (await reader.ReadAsync())
                {
                    results.Add(SalePredictionModel.FromADOReader<SalePredictionModel>(reader));
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

    public override async Task<SalePredictionModel> GetById(object id)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Add(SalePredictionModel element)
    {
        throw new NotImplementedException();
    }

    public override async Task<SalePredictionModel> Update(SalePredictionModel element)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Delete(SalePredictionModel element)
    {
        throw new NotImplementedException();
    }
}