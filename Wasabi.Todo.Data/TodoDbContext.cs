using System.Data.Entity;
using Wasabi.Todo.Data.Migrations;
using Wasabi.Todo.Models;

namespace Wasabi.Todo.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext()
            : base("Todo")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoDbContext, Configuration>());
            Configuration.ProxyCreationEnabled = false;
        }
        /// <summary>
        /// Gets or sets the todos.
        /// </summary>
        /// <value>The todos.</value>
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Models.Location> Locations { get; set; }
    }
}
