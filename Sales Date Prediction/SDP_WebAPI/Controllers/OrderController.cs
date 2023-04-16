namespace SDP_WebAPI.Controllers;

[ApiController]
[Route("Order")]
public class OrderController : BaseController<OrderModel>
{
    private new readonly IOrderRepository _repository;

    public OrderController(ILogger logger, IOrderRepository repository)
        : base(logger, repository)
    {
        _repository = repository;
    }

    // GET
    [HttpGet]
    [Route("{clientId:int}")]
    public async Task<IEnumerable<OrderModel>> GetByClientId(int clientId)
    {
        _logger.LogInformation(nameof(GetByClientId));
        ValidateInputs(clientId);
        return await _repository.GetByClientId(clientId);
    }

    [HttpPost]
    [Route("New")]
    [Route("Create")]
    [Route("Add")]
    public async Task<int> Create(AddOrderModel order)
    {
        _logger.LogInformation(nameof(order));
        ValidateInputs(order);
        return await _repository.Add(order);
    }
}