using FormulaOne.Application;
using FormulaOne.Application.Interfaces;
using FormulaOne.Core;
using FormulaOne.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer(builder.Configuration)
    .AddDomainLayer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FormulaOneDbContext>();
    await dbContext.Database.MigrateAsync();
    if (!dbContext.RaceResults.Any())
    {
        var databaseSeeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
        await databaseSeeder.Seed();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
