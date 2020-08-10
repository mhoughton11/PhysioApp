using System;
using System.Collections.Generic;
using System.Diagnostics;
using DIYHIIT.Models.Exercise;
using DIYHIIT.ViewModels;

using Xamarin.Forms;

namespace DIYHIIT.Views
{
    public partial class AddExerciseView : ContentPage
    {
        public AddExerciseView()
        {
            InitializeComponent();
        }

        private void FlowListView_FlowItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Exercise;
        }
    }
}
