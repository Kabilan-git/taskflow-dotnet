using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskService.Common.Mediator;

namespace TaskService.Features.Tasks.GetTasks
{
    public record GetTasksQuery : IRequest<List<GetTasksResponse>>
    {

    }
}