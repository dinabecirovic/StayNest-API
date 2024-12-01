using Microsoft.EntityFrameworkCore;
using StayNest_API.Data.Models;


namespace StayNest_API.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BungalowOwner> BungalowOwner { get; set; }
        public DbSet<Administrator> Administrator { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        { 

        }
    }
}
