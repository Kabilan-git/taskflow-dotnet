using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskService.Common.Endpoints;
using TaskService.Common.Mediator;
using TaskService.Features.Tasks.CreateTask;

namespace TaskService.Features.Tasks.GetTasks
{
    public class GetTasksEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/tasks", async (IMediator mediator, CancellationToken cancellationToken) =>
            {

                var getTasksQuery = new GetTasksQuery();

                List<GetTasksResponse> getTasksResponses = await mediator.Send(getTasksQuery, cancellationToken);

                return Results.Ok(getTasksResponses);
            });
        }
    }
}