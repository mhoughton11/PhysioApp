using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using PhysioApp.Library.Contracts;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Library.Models
{
    // Principle Entity
    public class Workout : IWorkout
    {
        public Workout()
        {
            DateAdded = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string BodyFocus { get; set; }
        public string ExerciseCount { get; set; }
        public string BackgroundImage { get; set; }

        public double? RestInterval { get; set; }
        public double? ActiveInterval { get; set; }
        public double? Effort { get; set; }
        public double? Duration { get; set; }

        public DateTime? DateAdded { get; set; }
        public DateTime? DateUsed { get; set; }

        public WorkoutType Type { get; set; }

        public string ExerciseIds { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }

       
    }
}
