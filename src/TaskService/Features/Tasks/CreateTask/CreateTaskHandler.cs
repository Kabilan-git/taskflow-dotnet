using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskService.Common.Mediator;

namespace TaskService.Features.Tasks.CreateTask
{
    public sealed class CreateTaskHandler : IRequestHandler<CreateTaskCommand, CreateTaskResponse>
    {
        public Task<CreateTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateTaskResponse(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                DateTime.UtcNow
            );

            return Task.FromResult(response);
        }
    }
}