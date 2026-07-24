using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskService.Common.Mediator;

namespace TaskService.Features.Tasks.CreateTask
{
    public sealed record CreateTaskCommand(
        string Title,
        string? Description,
        DateTime StartDate,
        DateTime EndDate,
        string Category) : IRequest<CreateTaskResponse>;
}