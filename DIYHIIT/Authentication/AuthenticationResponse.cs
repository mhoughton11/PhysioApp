using System;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Authentication
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public string ResponseToken { get; set; }
        public User User { get; set; }
    }
}
