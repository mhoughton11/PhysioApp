using DIYHIIT.Library.Models;
using DIYHIIT.Library.Models.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace DIYHIIT.API.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> DB_Users { get; set; }
        public DbSet<Workout> DB_Workouts { get; set; }
        public DbSet<Exercise> DB_Exercises { get; set; }
        public DbSet<FeedItem> FeedItems { get; set; }

        public AppDbContext(DbContextOptions options)
            :base(options)
        {
        }
    }
}
