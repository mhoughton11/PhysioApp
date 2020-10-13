using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhysioApp.Library.Contracts;

namespace PhysioApp.Library.Models
{
    public class User : IUser
    {
        public User()
        {
            Workouts = new HashSet<Workout>();
            WorkoutAuditTrails = new HashSet<AuditTrail>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Uid { get; set; }

        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }

        public double? Height { get; set; }
        public double? Weight { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
        public virtual ICollection<AuditTrail> WorkoutAuditTrails { get; set; }
    }
}
