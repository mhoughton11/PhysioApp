using System;
using System.ComponentModel.DataAnnotations;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Library.Persistance.Models
{
    public class DB_Exercise : IExercise
    {
        [Key]
        public int Id { get; set; }
        public int? Index { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public string BodyFocus { get; set; }
        public string ImageURL { get; set; }

        public double? Duration { get; set; }

        public WorkoutType Type { get; set; }
        
        public Workout Workout { get; set; }
        public int? WorkoutId { get; set; }
    }
}
