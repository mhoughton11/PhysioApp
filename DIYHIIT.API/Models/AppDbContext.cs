using DIYHIIT.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace DIYHIIT.API.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> DB_Users { get; set; }
        public DbSet<Workout> DB_Workouts { get; set; }
        public DbSet<Exercise> DB_Exercises { get; set; }

        public AppDbContext(DbContextOptions options)
            :base(options)
        {
        }
    }
}
