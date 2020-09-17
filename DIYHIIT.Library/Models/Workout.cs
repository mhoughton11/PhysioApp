using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using DIYHIIT.Library.Contracts;

namespace DIYHIIT.Library.Models
{
    // Principle Entity
    public class Workout : IWorkout
    {
        public Workout()
        {
            Exercises = new List<Exercise>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string BodyFocus { get; set; }
        public string ExerciseCount { get; set; }

        public double? RestInterval { get; set; }
        public double? ActiveInterval { get; set; }
        public double? Effort { get; set; }
        public double? Duration { get; set; }

        public DateTime? DateAdded { get; set; }
        public DateTime? DateUsed { get; set; }

        public WorkoutType Type { get; set; }

        public string ExerciseIds { get; set; }
        public List<Exercise> Exercises { get; set; }

        public int UserId { get; set; }

        public string BackgroundImage { get; set; }
    }
}
