namespace SDP_WebAPI.Controllers;

public abstract class BaseController<T> : ControllerBase
    where T : IElement
{
    private protected readonly ILogger _logger;
    private protected readonly IRepository<T> _repository;

    protected BaseController(ILogger logger, IRepository<T> repository)
    {
        _logger = logger ?? throw new ArgumentNullException("logger");
        _repository = repository ?? throw new ArgumentNullException("repository");
    }

    protected internal virtual void ValidateInputs(params object[] Args)
    {
        _logger.LogInformation($"{nameof(ValidateInputs)}");
        foreach (var arg in Args)
        {
            if (arg != null)
                continue;
            throw new ArgumentNullException(nameof(Args));
        }
    }
}