using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Scalar.AspNetCore;
using TaskService.Common.Extensions;
using TaskService.Common.Mediator;
using TaskService.Features.Learning.NPlusOne;
using TaskService.Infrastructure.Persistence;
using TaskService.Infrastructure.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);
// Service Registration
builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddMediator(typeof(Program).Assembly);
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TaskDbContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("TaskDb"));
});

var app = builder.Build();

// Runtime Configuration
app.MapOpenApi();
app.MapScalarApiReference();
app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
  app.MapNPlusOneLearningEndpoints();
}

app.MapGet("/", () => "Hello World!");

app.Run();
