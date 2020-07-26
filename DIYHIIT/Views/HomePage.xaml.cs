using System;
using System.Collections.Generic;
using System.Diagnostics;
using DIYHIIT.Models.Workout;
using DIYHIIT.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.Views
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.OnAppearing();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as Workout;
                // item.DebugObject();
                await Navigation.PushAsync(new PreviewWorkoutPage(item));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to push new preview workout page.");
                Debug.WriteLine(ex.Message);
            }
            
        }
    }
}
