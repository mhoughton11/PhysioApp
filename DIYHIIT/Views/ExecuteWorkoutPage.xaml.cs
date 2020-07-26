using DIYHIIT.Models.Workout;
using DIYHIIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIYHIIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExecuteWorkoutPage : ContentPage
    {
        ExecuteWorkoutViewModel viewModel;

        public ExecuteWorkoutPage(Workout workout)
        {
            InitializeComponent();

            BindingContext = viewModel = new ExecuteWorkoutViewModel(workout)
            {
                Navigation = Navigation
            };
        }
    }
}