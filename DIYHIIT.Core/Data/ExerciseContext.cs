using System;
using DIYHIIT.Models.Exercise;
using Microsoft.EntityFrameworkCore;

namespace DIYHIIT.CORE.Data
{
    public class ExerciseContext : DbContext
    {
        public ExerciseContext(DbContextOptions<ExerciseContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
    }
}
