using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskService.Common.Mediator;
using TaskService.Infrastructure.Persistence.Context;

namespace TaskService.Features.Tasks.GetTask
{
    public class GetTaskHandler(TaskDbContext dbContext) : IRequestHandler<GetTaskQuery, GetTaskResponse?>
    {
        public async Task<GetTaskResponse?> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var getTaskResponse = await dbContext.Tasks.AsNoTracking().Where(x => x.Id == request.Id).ProjectToType<GetTaskResponse>().FirstOrDefaultAsync(cancellationToken);
            return getTaskResponse;
        }
    }
}