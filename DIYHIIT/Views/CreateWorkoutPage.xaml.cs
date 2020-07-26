using System;
using System.Collections.Generic;
using System.Diagnostics;
using DIYHIIT.Models.Exercise;
using DIYHIIT.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.Views
{
    public partial class CreateWorkoutPage : ContentPage
    {
        CreateWorkoutViewModel viewModel;

        public CreateWorkoutPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CreateWorkoutViewModel
            {
                Navigation = Navigation
            };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            viewModel.RemoveObject((int)button.CommandParameter);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            viewModel.ShiftItemUp((int)button.CommandParameter);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            viewModel.ShiftItemDown((int)button.CommandParameter);
        }
    }
}
 