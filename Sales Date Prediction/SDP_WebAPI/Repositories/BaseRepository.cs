namespace SDP_WebAPI.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
    where T : IElement
{
    protected string connectionString { get; private set; }
    protected ILogger<T> logger;

    protected virtual string getAllQuery { get; }
    protected virtual string getByIdQuery { get; }
    protected virtual string addElementQuery { get; }
    protected virtual string updateElementQuery { get; }
    protected virtual string deleteElementQuery { get; }

    protected BaseRepository(IOptions<DatabaseOptions> databaseOptions, ILogger<T> logger)
    {
        connectionString = databaseOptions.Value.ConnectionString;
        this.logger = logger;
    }

    public abstract Task<IEnumerable<T>> GetAll();

    public abstract Task<T> GetById(object id);

    public abstract Task<int> Add(T element);

    public abstract Task<T> Update(T element);

    public abstract Task<int> Delete(T element);

    protected virtual void ValidateParams(params object[] Args)
    {
        logger.LogInformation($"{nameof(ValidateParams)}");
        foreach (var arg in Args)
        {
            if (arg != null)
                continue;
            throw new ArgumentNullException();
        }
    }
}