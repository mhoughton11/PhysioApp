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
            Workouts = new List<Workout>();
        }

        [Key]
        public int Id { get; set; }
        public string Uid { get; set; }

        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Workout> Workouts { get; set; }
    }
}
