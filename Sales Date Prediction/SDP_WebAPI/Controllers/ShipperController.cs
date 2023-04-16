namespace SDP_WebAPI.Controllers;

public class ShipperController : BaseController<ShipperModel>
{
    public ShipperController(ILogger<IRepository<ShipperModel>> logger, IRepository<ShipperModel> repository)
        : base(logger, repository)
    {
    }

    // GET
    [HttpGet]
    [Produces("application/json", Type = typeof(IEnumerable<ShipperModel>))]
    public async Task<ActionResult<IEnumerable<ShipperModel>>> Get()
    {
        _logger.LogInformation(nameof(Get));

        var result = await _repository.GetAll();

        if (result != null)
            return Ok(result);

        _logger.LogInformation("No shippers found");
        return NotFound();
    }
}