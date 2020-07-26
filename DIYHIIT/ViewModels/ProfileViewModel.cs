using System;
using MvvmCross.ViewModels;

namespace DIYHIIT.ViewModels
{
    public class ProfileViewModel : MvxViewModel
    {
        public string TextLabel { get; set; }

        public ProfileViewModel()
        {
            TextLabel = "Profile";
        }
    }
}
