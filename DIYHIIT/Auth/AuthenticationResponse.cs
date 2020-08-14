using System;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Auth
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public User User { get; set; }
    }
}
