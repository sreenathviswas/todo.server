using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Api.Persistence
{
    public class ToDoDbContext:DbContext
    {
        public ToDoDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public virtual DbSet<ToDo> Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
