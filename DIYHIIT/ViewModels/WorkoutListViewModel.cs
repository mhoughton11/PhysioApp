using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Views;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class WorkoutListViewModel : BaseViewModel
    {
        public WorkoutListViewModel(INavigationService navigationService,
                                    IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            
        }

        private List<IWorkout> _workoutList;
        public List<IWorkout> WorkoutList
        {
            get => _workoutList;
            set
            {
                _workoutList = value;
                RaisePropertyChanged(() => WorkoutList);
            }
        }

        public ICommand AddWorkoutCommand => new Command(OnAddWorkoutCommand);

        public void WorkoutSelected(IWorkout workout)
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

        public override async Task InitializeAsync(object data)
        {
            WorkoutList = new List<IWorkout>();

            try
            {
                // Get workouts
                //var workoutList = await App.WorkoutDatabase.GetItemsAsync();

                //foreach (var workout in workoutList)
                //{
                //    await workout.GetExercises(workout.ExercisesString);
                //}

                //WorkoutList = workoutList;
            }
            catch (Exception ex)
            {
                _dialogService.Popup("Loading workouts failed.");
                Debug.WriteLine(ex);
            }
        }

        private async void OnAddWorkoutCommand()
        {
            await _navigationService.NavigateToAsync(typeof(CreateWorkoutViewModel));
        }
    }
}
