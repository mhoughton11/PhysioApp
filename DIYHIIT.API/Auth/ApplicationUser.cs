using Microsoft.AspNetCore.Identity;
using System;

namespace DIYHIIT.API.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime birthdate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
