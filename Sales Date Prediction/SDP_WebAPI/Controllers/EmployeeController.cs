namespace SDP_WebAPI.Controllers;

[ApiController]
[Route("Employee")]
public class EmployeeController : BaseController<EmployeeModel>
{
    public EmployeeController(ILogger logger, IRepository<EmployeeModel> repository)
        : base(logger, repository)
    {
    }

    // GET
    [HttpGet]
    [Route("")]
    [Route("Index")]
    public async Task<IEnumerable<EmployeeModel>> Get()
    {
        _logger.LogInformation(nameof(Get));
        return await _repository.GetAll();
    }
}