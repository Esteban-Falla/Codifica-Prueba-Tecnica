namespace SDP_WebAPI.Controllers;

[ApiController]
[Route("Sales")]
public class SalesPredictionController : BaseController<SalePredictionModel>
{
    public SalesPredictionController(ILogger logger, IRepository<SalePredictionModel> repository)
        : base(logger, repository)
    {
    }

    // GET
    [HttpGet]
    [Route("")]
    [Route("Index")]
    [Route("Prediction")]
    public async Task<IEnumerable<SalePredictionModel>> Get()
    {
        _logger.LogInformation(nameof(Get));
        return await _repository.GetAll();
    }
}