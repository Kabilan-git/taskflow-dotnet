using System.Reflection.Metadata.Ecma335;
using TaskService.Common.Mediator;
using TaskService.Domain.Entities;
using TaskService.Domain.Enums;
using TaskService.Infrastructure.Persistence.Context;

namespace TaskService.Features.Tasks.CreateTask
{
    public sealed class CreateTaskHandler(TaskDbContext dbContext) : IRequestHandler<CreateTaskCommand, CreateTaskResponse>
    {
        public async Task<CreateTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Status = TaskItemStatus.Pending,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Category = request.Category,
                IsDeleted = false,
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "System"
            };

            await dbContext.Tasks.AddAsync(task, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateTaskResponse(
                task.Id,
                request.Title,
                request.Description,
                DateTime.UtcNow
            );
        }
    }
}