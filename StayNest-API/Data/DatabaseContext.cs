using Microsoft.EntityFrameworkCore;
using StayNest_API.Data.Models;


namespace StayNest_API.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        { 

        }
    }
}
