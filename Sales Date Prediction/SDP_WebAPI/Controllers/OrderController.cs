using System.Net;

namespace SDP_WebAPI.Controllers;

public class OrderController : BaseController<OrderModel>
{
    private new readonly IOrderRepository _repository;

    public OrderController(ILogger<IOrderRepository> logger, IOrderRepository repository)
        : base(logger, repository)
    {
        _repository = repository;
    }

    // GET
    [HttpGet("{clientId:int}")]
    [Produces("application/json", Type = typeof(IEnumerable<OrderModel>))]
    public async Task<ActionResult<IEnumerable<OrderModel>>> GetByClientId(int clientId)
    {
        _logger.LogInformation(nameof(GetByClientId));

        ValidateInputs(clientId);

        var result = await _repository.GetByClientId(clientId);

        if (result != null)
            return Ok(result);

        _logger.LogInformation("No orders found for user {0}", clientId);
        return NotFound();
    }

    [HttpPost("Create/{order}")]
    [Produces("application/json", Type = typeof(int))]
    public async Task<ActionResult<int>> Create(AddOrderModel order)
    {
        _logger.LogInformation(nameof(order));

        ValidateInputs(order);

        var result = await _repository.Add(order);

        if (result > 0)
            return Ok(result);

        _logger.LogWarning(
            "Order could not be created for employee: {0}, shipper: {1} and product: {2}",
            order.EmpId, order.ShipId, order.ProdId);

        return StatusCode((int)HttpStatusCode.InternalServerError);
    }
}