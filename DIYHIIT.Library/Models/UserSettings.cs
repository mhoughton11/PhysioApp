using System;
using DIYHIIT.Library.Contracts;
using System.ComponentModel.DataAnnotations;

namespace DIYHIIT.Library.Models
{
    public class UserSettings : IUserSettings
    {
        [Key]
        public int Id { get; set; }

        public bool PostToFeed { get; set; }

        public virtual User User { get; set; }
        public int UserKey { get; set; }
    }
}
