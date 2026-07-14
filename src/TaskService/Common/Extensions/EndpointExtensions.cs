using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TaskService.Common.Endpoints;

namespace TaskService.Common.Extensions
{
    public static class EndpointExtensions
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services, params Assembly[] assemblies)
        {
            var endpointType = typeof(IEndpoint);

            var endpoints = assemblies
                    .SelectMany(x => x.DefinedTypes)
                    .Where(type => endpointType.IsAssignableFrom(type)
                                && !type.IsInterface
                                && !type.IsAbstract);

            // var endpoints = Assembly.GetExecutingAssembly()
            //     .DefinedTypes
            //     .Where(type => endpointType.IsAssignableFrom(type)
            //                     && !type.IsInterface
            //                     && !type.IsAbstract);

            foreach (var endpoint in endpoints)
            {
                services.AddTransient(endpointType, endpoint);
            }
            return services;
        }

        public static WebApplication MapEndpoints(this WebApplication app)
        {
            var enpointType = typeof(IEndpoint);

            var endpoints = app.Services
                .GetServices(enpointType)
                .Cast<IEndpoint>();

            foreach (var endpoint in endpoints)
            {
                endpoint.MapEndpoint(app);
            }
            return app;
        }
    }
}