using Microsoft.EntityFrameworkCore;
using StayNest_API.Data.Models;


namespace StayNest_API.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<BungalowOwner> BungalowOwners { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        { 

        }
    }
}
