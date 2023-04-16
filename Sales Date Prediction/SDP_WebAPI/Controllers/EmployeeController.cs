namespace SDP_WebAPI.Controllers;

public class EmployeeController : BaseController<EmployeeModel>
{
    public EmployeeController(ILogger<IRepository<EmployeeModel>> logger, IRepository<EmployeeModel> repository)
        : base(logger, repository)
    {
    }

    // GET
    [HttpGet]
    [Produces("application/json", Type = typeof(IEnumerable<EmployeeModel>))]
    public async Task<ActionResult<IEnumerable<EmployeeModel>>> Get()
    {
        _logger.LogInformation(nameof(Get));
        var result = await _repository.GetAll();

        if (result != null)
            return Ok(result);

        _logger.LogInformation("No Employees found");
        return NotFound();
    }
}