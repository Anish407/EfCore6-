using EFCore6.Core.Repositories;
using EFCore6.Core.Servics.Implemetations;
using EFCore6.Core.Servics.Interfaces;
using EFCore6.Infra;
using EFCore6.Infra.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContextPool<PubContext>(op => op.UseSqlServer(configuration.GetConnectionString("PubContext")));
builder.Services.AddControllers();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IManyToManyService, ManyToManyService>();
builder.Services.AddScoped<IPubContextUnitOfWork, PubContextUnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    // use third party
    .AddSqlServer(configuration.GetConnectionString("PubContext"));
    // write own (we can write similar ones for service bus, event grid , cosmosdb etc
    //.AddAsyncCheck("sql check", async () => { 
    //    // go to ssms and stop the database and check -> will return unhealthy
    //    using(var connection= new SqlConnection(configuration.GetConnectionString("PubContext")))
    //    {
    //        try
    //        {
    //            await connection.OpenAsync();
    //            return HealthCheckResult.Healthy();
    //        }
    //        catch (Exception ex)
    //        {
    //            return HealthCheckResult.Unhealthy();
    //        }
    //    }

    //});


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

app.UseHealthChecks("/healthy");

app.Run();
