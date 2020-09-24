using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.Library.Models
{
    public class User : IUser
    {
        public User()
        {
            Workouts = new HashSet<Workout>();
            WorkoutAuditTrails = new HashSet<AuditTrail>();
        }

        [Key]
        public int UserKey { get; set; }
        public string Uid { get; set; }

        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
        public virtual ICollection<AuditTrail> WorkoutAuditTrails { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }

        public virtual UserSettings UserSettings { get; set; }
    }
}
