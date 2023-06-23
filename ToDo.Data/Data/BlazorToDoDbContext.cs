using Microsoft.EntityFrameworkCore;
using ToDo.Models.Entities;

namespace ToDo.Data.Data
{
    public class BlazorToDoDbContext : DbContext
    {
        public BlazorToDoDbContext(DbContextOptions<BlazorToDoDbContext> options) 
            :base(options)
        {}
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<CheckboxItem> Checkboxes { get; set; }
    }
}
