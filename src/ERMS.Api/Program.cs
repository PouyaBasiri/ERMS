using ERMS.Application;
using ERMS.Infrastructure;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(); 

builder.Services.AddApplication(); 

builder.Services.AddInfrastructure(builder.Configuration); 

var app = builder.Build(); 

app.MapOpenApi(); 

app.MapScalarApiReference();

app.Run();