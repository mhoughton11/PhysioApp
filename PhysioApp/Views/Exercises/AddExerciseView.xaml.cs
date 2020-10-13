using System;
using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.ViewModels;
using PhysioApp.ViewModels.Exercises;
using Xamarin.Forms;

namespace PhysioApp.Views.Exercises
{
    public partial class AddExerciseView : ContentPage
    {
        AddExerciseViewModel viewModel;

        public AddExerciseView()
        {
            InitializeComponent();

            var exerciseDataService = AppContainer.Container.Resolve<IExerciseDataService>();
            var dialogService = AppContainer.Container.Resolve<IDialogService>();

            BindingContext = viewModel = new AddExerciseViewModel(0, Navigation, dialogService, exerciseDataService);
        }
    }
}
