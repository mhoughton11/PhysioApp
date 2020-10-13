using System;
using PhysioApp.Library.Contracts;
using System.ComponentModel.DataAnnotations;

namespace PhysioApp.Library.Models
{
    public class UserSettings : IUserSettings
    {
        public bool PostToFeed { get; set; }
    }
}
