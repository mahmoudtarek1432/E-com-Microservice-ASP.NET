using Ordering.API.Extensions;
using Ordering.Application.Services;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Service;
using Ordering.Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration);
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

app.MigrateDB<OrderContext>((context, service) =>
{
    var Logger = service.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.seedAsync(context, Logger).Wait();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
