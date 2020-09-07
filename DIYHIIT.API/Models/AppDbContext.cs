using DIYHIIT.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace DIYHIIT.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> ExerciseCatalog { get; set; }
        public DbSet<Workout> WorkoutCatalog { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
