using System;
using System.Collections.Generic;
using DIYHIIT.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.Views
{
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel viewModel;

        public ProfilePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ProfileViewModel();
        }
    }
}
