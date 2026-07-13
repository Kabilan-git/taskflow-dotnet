using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskService.Features.Tasks.CreateTask
{
    public sealed record CreateTaskResponse(Guid Id, string Title, string? Description, DateTime CreatedAt);
}