using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskService.Common.Mediator;
using TaskService.Common.Models;
using TaskService.Domain.Entities;
using TaskService.Domain.Enums;
using TaskService.Infrastructure.Persistence.Context;

namespace TaskService.Features.Tasks.GetTasks
{
    public class GetTasksHandler(TaskDbContext dbContext) : IRequestHandler<GetTasksQuery, PagedResponse<GetTasksResponse>>
    {
        public async Task<PagedResponse<GetTasksResponse>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            if (request.page <= 0 || request.pageSize <= 0)
            {
                throw new ArgumentException("page and pageSize must be greater than 0.");
            }

            IQueryable<TaskItem> query = dbContext.Tasks
                    .AsNoTracking();

            if (request.Status is not null)
            {
                query = query.Where(x => x.Status == request.Status);
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    EF.Functions.ILike(
                        x.Title, $"%{request.Search}%")
                    ||
                    EF.Functions.ILike(
                        x.Description, $"%{request.Search}%"
                    ));
            }

            bool descending = request.sortOrder == "desc";

            if (request.sortBy is not null)
            {
                query = request.sortBy switch
                {
                    TaskSortBy.CreatedDate =>
                        descending ? query.OrderByDescending(x => x.CreatedDate)
                                    : query.OrderBy(x => x.CreatedDate),
                    TaskSortBy.Title =>
                        descending ? query.OrderByDescending(x => x.Title)
                                    : query.OrderBy(x => x.Title),
                    TaskSortBy.Status =>
                        descending ? query.OrderByDescending(x => x.Status)
                                    : query.OrderBy(x => x.Status),
                    TaskSortBy.Category =>
                        descending ? query.OrderByDescending(x => x.Category)
                                    : query.OrderBy(x => x.Category),
                    _ => query
                };
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedDate);
            }

            int totalcount = await query.CountAsync();

            int skip = (request.page - 1) * request.pageSize;
            query = query.Skip(skip);

            int take = request.pageSize;
            query = query.Take(take);

            var tasks = await query.ProjectToType<GetTasksResponse>().ToListAsync(cancellationToken);

            return new PagedResponse<GetTasksResponse>
            {
                Items = tasks,
                CurrentPage = request.page,
                PageSize = request.pageSize,
                TotalCount = totalcount,
                TotalPages = (int)Math.Ceiling(totalcount / (double)request.pageSize)
            };
        }
    }
}