using System.Data;
using Microsoft.Data.SqlClient;
using SDP_WebAPI.Models;

namespace SDP_WebAPI.Repositories;

public class EmployeeRepository : BaseRepository<EmployeeModel>
{
    public EmployeeRepository(IConfiguration config, ILogger logger) : base(config, logger)
    {
    }

    public override async Task<IEnumerable<EmployeeModel>> GetAll()
    {
        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(TSQLQueries.GetEmployeesQuery, connection);

        try
        {
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            List<EmployeeModel> result = null;

            if (reader.HasRows)
            {
                result = new List<EmployeeModel>();
                while (await reader.ReadAsync())
                {
                    result.Add(EmployeeModel.FromADOReader<EmployeeModel>(reader));
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

    public override async Task<EmployeeModel> GetById(object id)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Add(EmployeeModel element)
    {
        throw new NotImplementedException();
    }

    public override async Task<EmployeeModel> Update(EmployeeModel element)
    {
        throw new NotImplementedException();
    }

    public override async Task<int> Delete(EmployeeModel element)
    {
        throw new NotImplementedException();
    }
}