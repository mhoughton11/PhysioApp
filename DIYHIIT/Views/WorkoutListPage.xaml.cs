using System;
using System.Collections.Generic;
using DIYHIIT.Models.Workout;
using DIYHIIT.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.Views
{
    public partial class WorkoutListPage : ContentPage
    {
        WorkoutListViewModel viewModel;

        public WorkoutListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new WorkoutListViewModel
            {
                Navigation = Navigation
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

        void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var workout = e.SelectedItem as Workout;
            viewModel.WorkoutSelected(workout);
        }
    }
}
