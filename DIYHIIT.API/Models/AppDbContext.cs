using DIYHIIT.Library.Models;
using DIYHIIT.Library.Models.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace DIYHIIT.API.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<FeedItem> FeedItems { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }

        public AppDbContext(DbContextOptions options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.WorkoutAuditTrails)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserKey)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany(u => u.Workouts)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserKey)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasOne(u => u.UserSettings)
                .WithOne(s => s.User)
                .HasForeignKey<UserSettings>(s => s.UserKey);

            builder.Entity<Workout>()
                .HasMany(w => w.Exercises)
                .WithOne(e => e.Workout)
                .HasForeignKey(e => e.WorkoutKey)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
