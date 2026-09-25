using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskService.Common.Endpoints;
using TaskService.Common.Mediator;
using TaskService.Common.Models;
using TaskService.Domain.Entities;
using TaskService.Domain.Enums;
using TaskService.Features.Tasks.CreateTask;

namespace TaskService.Features.Tasks.GetTasks
{
    public class GetTasksEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/tasks", async (
                    TaskItemStatus? status,
                    string? search,
                    TaskSortBy? sortBy,
                    string? sortOrder,
                    int page,
                    int pageSize,
                    IMediator mediator,
                    CancellationToken cancellationToken) =>
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return Results.BadRequest("page and pageSize must be greater than 0.");
                }

                var getTasksQuery = new GetTasksQuery(status, search, sortBy, sortOrder, page, pageSize);

                PagedResponse<GetTasksResponse> getTasksResponses = await mediator.Send(getTasksQuery, cancellationToken);

                return Results.Ok(getTasksResponses);
            });
        }
    }
}