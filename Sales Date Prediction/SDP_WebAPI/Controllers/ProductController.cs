namespace SDP_WebAPI.Controllers;

public class ProductController : BaseController<ProductModel>
{
    public ProductController(ILogger<IRepository<ProductModel>> logger, IRepository<ProductModel> repository)
        : base(logger, repository)
    {
    }

    // GET
    [HttpGet]
    [Produces("application/json", Type = typeof(IEnumerable<ProductModel>))]
    public async Task<ActionResult<IEnumerable<ProductModel>>> Get()
    {
        _logger.LogInformation(nameof(Get));

        var result = await _repository.GetAll();

        if (result != null)
            return Ok(result);

        _logger.LogInformation("No Products found");
        return NotFound();
    }
}