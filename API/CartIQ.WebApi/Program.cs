using CartIQ.Application;
using CartIQ.Infrastructure;
using CartIQ.Infrastructure.Data;
using CartIQ.WebApi.Infrastructure;
using CartIQ.WebApi.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddHealthChecks();

builder.AddInfrastructureServices();
builder.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(opt => app.ConfigureCors(opt));
app.UseExceptionHandler(appError => { });

app.MapEndpoints(typeof(Program).Assembly);
app.MapHealthChecks("/health");

app.Run();

