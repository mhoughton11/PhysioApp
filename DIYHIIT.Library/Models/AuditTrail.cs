using System;
using System.ComponentModel.DataAnnotations;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.Library.Models
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

        public int UserID { get; set; }

        public DateTime DOE { get; set; }

        public Workout AuditWorkout { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
    }
}