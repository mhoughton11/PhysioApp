using System;
using PhysioApp.Library.Models;

namespace PhysioApp.Authentication
{
    public class AuthenticationResponse
    {
        public string UserUid { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ResponseToken { get; set; }
    }
}
