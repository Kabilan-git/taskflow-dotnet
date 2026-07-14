using Scalar.AspNetCore;
using TaskService.Common.Extensions;
using TaskService.Common.Mediator;

var builder = WebApplication.CreateBuilder(args);
// Service Registration
builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddSingleton<IMediator, Mediator>();
builder.Services.AddOpenApi();

var app = builder.Build();

// Runtime Configuration
app.MapOpenApi();
app.MapScalarApiReference();
app.MapEndpoints();


app.MapGet("/", () => "Hello World!");

app.Run();
