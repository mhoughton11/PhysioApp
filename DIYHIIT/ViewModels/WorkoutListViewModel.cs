using System;
using System.Collections.Generic;
using System.Diagnostics;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Models.Workout;
using DIYHIIT.Views;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class WorkoutListViewModel : BaseViewModel
    {
        private List<Workout> _workoutList;
        public List<Workout> WorkoutList
        {
            get => _workoutList;
            set
            {
                _workoutList = value;
                RaisePropertyChanged(() => WorkoutList);
            }
        }

        public Command AddWorkoutCommand { get; set; }

        private readonly INavigationService navigation;

        public WorkoutListViewModel(INavigationService navigationService, IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            WorkoutList = new List<Workout>();

            AddWorkoutCommand = new Command(() => ExecuteAddWorkoutCommand());
        }

        public async void OnAppearing()
        {
            try
            {
                // Get workouts
                var workoutList = await App.WorkoutDatabase.GetItemsAsync();

                foreach (var workout in workoutList)
                {
                    await workout.GetExercises(workout.ExercisesString);
                }
                
                WorkoutList = workoutList;
            }
            catch (Exception ex)
            {
                _dialogService.Popup("Loading workouts failed.");
                Debug.WriteLine(ex);
            }
        }

        public async void WorkoutSelected(Workout workout)
        {
            try
            {
                //await Navigation.PushAsync(new PreviewWorkoutView(workout));
            }
            catch (Exception ex)
            {
                _dialogService.Popup("Failed to load workout.");
                Debug.WriteLine(ex);
                Debug.WriteLine("Workout:");
                workout.DebugObject();
            }          
        }

        private async void ExecuteAddWorkoutCommand()
        {
            await _navigationService.NavigateToAsync<CreateWorkoutViewModel>();
        }
    }
}
