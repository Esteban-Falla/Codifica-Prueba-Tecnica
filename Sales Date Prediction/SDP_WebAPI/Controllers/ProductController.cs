namespace SDP_WebAPI.Controllers;

[ApiController]
[Route("Order")]
public class ProductController : BaseController<ProductModel>
{
    public ProductController(ILogger logger, IRepository<ProductModel> repository)
        : base(logger, repository)
    {
    }

    // GET
    [HttpGet]
    [Route("")]
    [Route("Index")]
    public async Task<IEnumerable<ProductModel>> Get()
    {
        _logger.LogInformation(nameof(Get));
        return await _repository.GetAll();
    }
}