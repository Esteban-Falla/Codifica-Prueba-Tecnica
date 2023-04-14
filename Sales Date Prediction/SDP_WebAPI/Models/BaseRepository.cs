using System.Formats.Asn1;
using SDP_WebAPI.Interfaces;

namespace SDP_WebAPI.Models;

public abstract class BaseRepository<T> : IRepository<T>
    where T : IElement, new()
{
    private string connectionString { get; }

    protected virtual string getAllQuery { get; }
    protected virtual string getByIdQuery { get; }
    protected virtual string addElementQuery { get; }
    protected virtual string updateElementQuery { get; }
    protected virtual string deleteElementQuery { get; }

    protected BaseRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("SalesDB");
    }

    public virtual Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }

    public virtual Task<T> GetById(object id)
    {
        throw new NotImplementedException();
    }

    public virtual Task<int> Add(T element)
    {
        throw new NotImplementedException();
    }

    public virtual Task<T> Update(T element)
    {
        throw new NotImplementedException();
    }

    public virtual Task<int> Delete(T element)
    {
        throw new NotImplementedException();
    }

    protected virtual bool ValidateParams(params object[] Args)
    {
        foreach (var arg in Args)
        {
            if (arg != null)
                continue;
            return false;
        }

        return true;
    }
}