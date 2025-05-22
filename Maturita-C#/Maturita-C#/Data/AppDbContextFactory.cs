using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Maturita_C_.Data;

namespace Maturita_C_.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=users.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
