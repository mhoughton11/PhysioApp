using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Models;
using DIYHIIT.Models.Workout;
using DIYHIIT.Services.Dialog;
using DIYHIIT.Views;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class WorkoutListViewModel : MvxViewModel
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

        public INavigation Navigation;

        private readonly IDialogService dialogService;

        public WorkoutListViewModel()
        {
            WorkoutList = new List<Workout>();

            dialogService = new DialogService();

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
                dialogService.Popup("Loading workouts failed.");
                Debug.WriteLine(ex);
            }
        }

        public async void WorkoutSelected(Workout workout)
        {
            try
            {
                await Navigation.PushAsync(new PreviewWorkoutPage(workout));
            }
            catch (Exception ex)
            {
                dialogService.Popup("Failed to load workout.");
                Debug.WriteLine(ex);
                Debug.WriteLine("Workout:");
                workout.DebugObject();
            }          
        }

        private async void ExecuteAddWorkoutCommand()
        {
            await Navigation.PushAsync(new CreateWorkoutPage());
        }
    }
}
