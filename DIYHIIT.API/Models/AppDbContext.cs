using DIYHIIT.Library.Models;
using DIYHIIT.Library.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace DIYHIIT.API.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<DB_User> Users { get; set; }
        public DbSet<DB_Exercise> Exercises { get; set; }

        public AppDbContext(DbContextOptions options)
            :base(options)
        {
        }
    }
}
