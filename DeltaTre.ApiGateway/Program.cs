using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var ocelotJson = string.IsNullOrWhiteSpace(environmentName) ? "ocelot.json" : $"ocelot.{environmentName.ToLower()}.json";
builder.Configuration.AddJsonFile(ocelotJson);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();