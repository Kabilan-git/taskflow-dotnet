using TaskService.Features.Tasks.GetTasks;

namespace TaskService.Features.Tasks.GetTask;

public record class GetTaskResponse
(
Guid Id,
string Title,
string Description,
CatergoryResponse Category,
string Status
);
