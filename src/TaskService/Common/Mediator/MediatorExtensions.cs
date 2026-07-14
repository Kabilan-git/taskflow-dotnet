using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TaskService.Common.Mediator
{
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
        {
            var handlerType = typeof(IRequestHandler<,>);

            var handlers = assemblies
                .SelectMany(x => x.DefinedTypes)
                .Where(type => !type.IsAbstract &&
                                !type.IsInterface &&
                                type.ImplementedInterfaces.Any(i =>
                                    i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == handlerType));

            foreach (var handler in handlers)
            {
                var service = handler.ImplementedInterfaces.First(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == handlerType);

                services.AddTransient(service, handler);
            }
            services.AddSingleton<IMediator, Mediator>();
            return services;
        }
    }
}