using SDP_WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add logging service
builder.Services.AddLogging();

// Add services to the container.
builder.Services
    .Configure<DatabaseOptions>(builder.Configuration.GetSection("DatabaseConnections:SalesDB"))
    .AddScoped<IOrderRepository, OrderRepository>()
    .AddScoped<IRepository<EmployeeModel>, EmployeeRepository>()
    .AddScoped<IRepository<ProductModel>, ProductRepository>()
    .AddScoped<IRepository<ShipperModel>, ShipperRepository>()
    .AddScoped<IRepository<SalePredictionModel>, SalesPredictionRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();