using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskService.Common.Endpoints;
using TaskService.Common.Mediator;

namespace TaskService.Features.Tasks.CreateTask
{
    public sealed class CreateTaskEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/tasks", async (CreateTaskCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(command, cancellationToken);

                return Results.Created($"/tasks/{response.Id}", response);
            });
        }
    }
}