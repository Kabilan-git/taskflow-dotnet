using Microsoft.EntityFrameworkCore;
using TaskService.Infrastructure.Persistence.Context;

namespace TaskService.Features.Learning.NPlusOne;

public static class NPlusOneLearningEndpoints
{
    public static void MapNPlusOneLearningEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/learning/n-plus-one")
            .WithTags("N+1 Learning Demo");

        group.MapGet("/explicit-load", async (
            TaskDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var tasks = await dbContext.Tasks
                .OrderBy(task => task.CreatedDate)
                .ThenBy(task => task.Id)
                .Take(6)
                .ToListAsync(cancellationToken);

            foreach (var task in tasks)
            {
                var categoryReference = dbContext.Entry(task).Reference(task => task.Category);

                // Learning trace: shows whether EF already attached the category before we ask it to load.
                Console.WriteLine(
                    $"[N+1] Task '{task.Title}': Category attached = {task.Category is not null}, IsLoaded = {categoryReference.IsLoaded}");

                await categoryReference.LoadAsync(cancellationToken);
            }

            return Results.Ok(tasks.Select(task => new NPlusOneTaskResponse(
                task.Id,
                task.Title,
                task.Category.Name)));
        });

        group.MapGet("/include", async (
            TaskDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var tasks = await dbContext.Tasks
                .AsNoTracking()
                .Include(task => task.Category)
                .OrderBy(task => task.CreatedDate)
                .ThenBy(task => task.Id)
                .Take(5)
                .ToListAsync(cancellationToken);

            return Results.Ok(tasks.Select(task => new NPlusOneTaskResponse(
                task.Id,
                task.Title,
                task.Category.Name)));
        });

        group.MapGet("/projection", async (
            TaskDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var tasks = await dbContext.Tasks
                .AsNoTracking()
                .OrderBy(task => task.CreatedDate)
                .ThenBy(task => task.Id)
                .Take(5)
                .Select(task => new NPlusOneTaskResponse(
                    task.Id,
                    task.Title,
                    task.Category.Name))
                .ToListAsync(cancellationToken);

            return Results.Ok(tasks);
        });
    }

    private sealed record NPlusOneTaskResponse(Guid Id, string Title, string CategoryName);
}
