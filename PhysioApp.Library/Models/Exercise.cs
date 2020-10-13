using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Windows.Input;
using PhysioApp.Library.Contracts;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Library.Models
{
    // Dependent Entity
    public class Exercise : IExercise
    {
        [Key]
        public int ID { get; set; }

        public int? Index { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public double? Duration { get; set; }
        
        public int WorkoutID { get; set; }

        [Required]
        public WorkoutType Type { get; set; }

        // Navigation Properties
        public virtual Workout Workout { get; set; }
    }
}
