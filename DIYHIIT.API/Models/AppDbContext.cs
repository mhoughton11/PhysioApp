using DIYHIIT.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace DIYHIIT.API.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
