namespace SDP_WebAPI.Controllers;

[ApiController]
[Route("Shippers")]
public class ShipperController : BaseController<ShipperModel>
{
    public ShipperController(ILogger logger, IRepository<ShipperModel> repository)
        : base(logger, repository)
    {
    }

    // GET
    [HttpGet]
    [Route("")]
    [Route("Index")]
    public async Task<IEnumerable<ShipperModel>> Get()
    {
        _logger.LogInformation(nameof(Get));
        return await _repository.GetAll();
    }
}