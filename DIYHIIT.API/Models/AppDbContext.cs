using DIYHIIT.Library.Models;
using DIYHIIT.Library.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace DIYHIIT.API.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> DB_Users { get; set; }
        public DbSet<DB_Exercise> DB_Exercises { get; set; }

        public AppDbContext(DbContextOptions options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.Workouts)
                .WithOne(w => w.User);

            builder.Entity<Workout>()
                .HasMany(w => w.Exercises)
                .WithOne(e => e.Workout);
        }
    }
}
