using Backend.Contracts.DataContracts;
using Microsoft.EntityFrameworkCore;

namespace Backend.Contracts.DatabaseContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Stocks> Stocks { get; set; }
        public DbSet<Stores> Stores { get; set; }
    }
}
