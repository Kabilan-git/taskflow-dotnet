using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TaskService.Features.Tasks.GetTasks
{
    public record GetTasksResponse(
        Guid Id,
        string Title,
        string Description,
        string Category,
        string Status

    );
}