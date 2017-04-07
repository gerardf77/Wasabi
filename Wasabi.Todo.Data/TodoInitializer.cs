using System.Data.Entity;

namespace Wasabi.Todo.Data
{
    public class TodoInitializer : DropCreateDatabaseIfModelChanges<TodoDbContext>
    {
        /// <summary>
        /// A method that should be overridden to actually add data to the dbContext for seeding.
        /// The default implementation does nothing.
        /// </summary>
        /// <param name = "dbContext" > The dbContext to seed.</param>
        protected override void Seed(TodoDbContext dbContext)
        {
        }
    }
}
