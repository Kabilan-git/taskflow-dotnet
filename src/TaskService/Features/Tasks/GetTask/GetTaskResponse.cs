namespace TaskService.Features.Tasks.GetTask;

public record class GetTaskResponse
(
Guid Id,
string Title,
string Description,
string Category,
string Status
);
