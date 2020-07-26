using System;
using System.Collections.Generic;
using DIYHIIT.Models;
using Xamarin.Forms;
using DIYHIIT.ViewModels;
using DIYHIIT.Models.Workout;

namespace DIYHIIT.Views
{
    public partial class PreviewWorkoutPage : ContentPage
    {
        PreviewWorkoutViewModel viewModel;

        public PreviewWorkoutPage(Workout workout)
        {
            InitializeComponent();

            BindingContext = viewModel = new PreviewWorkoutViewModel(workout)
            {
                Navigation = Navigation
            };
        }
    }
}
