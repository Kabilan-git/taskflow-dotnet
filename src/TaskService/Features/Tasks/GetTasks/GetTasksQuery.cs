using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskService.Common.Mediator;
using TaskService.Common.Models;
using TaskService.Domain.Enums;

namespace TaskService.Features.Tasks.GetTasks
{
    public record GetTasksQuery(
        TaskItemStatus? Status,
        string? Search,
        TaskSortBy? sortBy,
        string? sortOrder,
        int page,
        int pageSize) : IRequest<PagedResponse<GetTasksResponse>>
    {

    }
}