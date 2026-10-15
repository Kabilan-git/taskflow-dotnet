using TaskService.Common.Mediator;

namespace TaskService.Features.Tasks.GetTask;

public record class GetTaskQuery(Guid Id) : IRequest<GetTaskResponse>;
