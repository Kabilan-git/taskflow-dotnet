using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskService.Common.Mediator
{
    public sealed class Mediator(IServiceProvider serviceProvider) : IMediator
    {
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();

            var responseType = requestType.GetInterfaces()
                .First(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IRequest<>))
                .GetGenericArguments()[0];

            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(requestType, responseType);

            var handler = serviceProvider.GetRequiredService(handlerType);

            var handleMethod = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.Handle));

            var task = (Task<TResponse>)handleMethod!.Invoke(handler, [request, cancellationToken])!;

            return await task;
        }
    }
}