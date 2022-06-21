using DeltaTre.Service.WordApi;
using DeltaTre.Service.WordApi.Data;
using DeltaTre.Service.WordApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// use in memory database for the purpose of this test.
builder.Services.AddDbContext<WordContext>(options => options.UseInMemoryDatabase("WordDatabase"));
builder.Services.AddScoped<IWordRepository, WordRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Testing purposes only
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<WordContext>();
if (context != null)
{

   var defaultWords = new[]
   {
    "hello", "goodbye", "simple", "list", "search", "filter", "yes", "no"
   };

    context.Words.AddRange(defaultWords.Select(word => new Word(word)));
    context.SaveChanges();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

