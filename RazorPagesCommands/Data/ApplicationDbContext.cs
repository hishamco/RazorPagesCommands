using Microsoft.EntityFrameworkCore;

namespace RazorPagesCommands.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
    }
}
