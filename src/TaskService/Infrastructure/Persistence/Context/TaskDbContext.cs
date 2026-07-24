using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskService.Domain.Entities;

namespace TaskService.Infrastructure.Persistence.Context
{
    public sealed class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}