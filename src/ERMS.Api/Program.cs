using ERMS.Api.Endpoints;
using ERMS.Application;
using ERMS.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddOpenApi(); 
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpoints(); 
var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapEndpoints();
app.Run();