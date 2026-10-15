using System;
using TaskService.Common.Endpoints;
using TaskService.Common.Mediator;

namespace TaskService.Features.Tasks.GetTask;

public class GetTaskEndpoint : IEndpoint
{
  public void MapEndpoint(IEndpointRouteBuilder app)
  {
    app.MapGet("tasks/{id:guid}", async (
      Guid id,
      IMediator mediator,
      CancellationToken cancellationToken) =>
    {

      var getTaskResponse = await mediator.Send(new GetTaskQuery(id), cancellationToken);

      return getTaskResponse is null
        ? Results.NotFound()
        : Results.Ok(getTaskResponse);
    });
  }
}
