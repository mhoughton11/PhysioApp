using System;
using System.ComponentModel.DataAnnotations;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.Library.Models
{
    public class User : IUser
    {
        [Key]
        public string Uid { get; set; }

        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
