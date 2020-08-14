using System;
namespace DIYHIIT.Library.Auth
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public ApplicationUser User { get; set; }
    }
}
