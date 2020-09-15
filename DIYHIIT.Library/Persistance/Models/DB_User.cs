using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Library.Persistance.Models
{
    public class DB_User : IUser
    {
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
