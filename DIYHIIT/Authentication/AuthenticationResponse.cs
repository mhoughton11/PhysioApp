using System;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Authentication
{
    public class AuthenticationResponse
    {
        public string UserUid { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ResponseToken { get; set; }
    }
}
