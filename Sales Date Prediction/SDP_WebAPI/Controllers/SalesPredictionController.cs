namespace SDP_WebAPI.Controllers;

public class SalesPredictionController : BaseController<SalePredictionModel>
{
    public SalesPredictionController(ILogger<IRepository<SalePredictionModel>> logger,
        IRepository<SalePredictionModel> repository)
        : base(logger, repository)
    {
    }

    // GET
    [HttpGet]
    [Produces("application/json", Type = typeof(IEnumerable<SalePredictionModel>))]
    public async Task<ActionResult<IEnumerable<SalePredictionModel>>> Get()
    {
        _logger.LogInformation(nameof(Get));

        var result = await _repository.GetAll();

        if (result != null)
            return Ok(result);

        _logger.LogInformation("No sales prediction found");
        return NotFound();
    }
}