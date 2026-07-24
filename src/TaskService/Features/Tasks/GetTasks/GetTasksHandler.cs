using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskService.Common.Mediator;
using TaskService.Domain.Entities;
using TaskService.Infrastructure.Persistence.Context;

namespace TaskService.Features.Tasks.GetTasks
{
    public class GetTasksHandler(TaskDbContext dbContext) : IRequestHandler<GetTasksQuery, List<GetTasksResponse>>
    {
        public async Task<List<GetTasksResponse>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var taskItems = await dbContext.Tasks
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

            return taskItems.Adapt<List<GetTasksResponse>>();
        }
    }
}