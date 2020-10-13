using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhysioApp.Library.Contracts;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Library.Models
{
    public class AuditTrail
    {
        public AuditTrail()
        {
            DOE = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        public int UserId { get; set; }

        public DateTime DOE { get; set; }

        public WorkoutType? Type { get; set; }

        //public int? AuditWorkoutId { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
    }
}