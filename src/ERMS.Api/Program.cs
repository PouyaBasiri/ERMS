using ERMS.Api.Endpoints;
using ERMS.Api.Exceptions;
using ERMS.Application;
using ERMS.Infrastructure;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERMS WebAPI", Version = "v1" });
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddProblemDetails();

var app = builder.Build();

// Serve generated OpenAPI JSON and the Swagger UI
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Correct lowercase "v1" — this must match the document name produced by SwaggerGen
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ERMS WebAPI");
    // to expose UI at root use: options.RoutePrefix = string.Empty;
});

app.UseExceptionHandler();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapEndpoints();
app.Run();