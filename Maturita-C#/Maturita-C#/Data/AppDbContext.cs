using Microsoft.EntityFrameworkCore;
using Maturita_C_.Models;
using System.Collections.Generic;


namespace Maturita_C_.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
