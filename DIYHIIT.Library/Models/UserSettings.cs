using System;
using DIYHIIT.Library.Contracts;
using System.ComponentModel.DataAnnotations;

namespace DIYHIIT.Library.Models
{
    public class UserSettings : IUserSettings
    {
        public bool PostToFeed { get; set; }
    }
}
