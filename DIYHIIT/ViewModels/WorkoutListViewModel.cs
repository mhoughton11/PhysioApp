using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using DIYHIIT.Views;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class WorkoutListViewModel : BaseViewModel
    {
        public WorkoutListViewModel(IWorkoutDataService workoutDataService,
                                    INavigationService navigationService,
                                    IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _workoutDataService = workoutDataService;
        }

        #region Private Fields

        private List<Workout> _workoutList;
        private readonly IWorkoutDataService _workoutDataService;

        #endregion

        #region Public Members and Commands

        public List<Workout> WorkoutList
        {
            get => _workoutList;
            set
            {
                _workoutList = value;
                RaisePropertyChanged(() => WorkoutList);
            }
        }

        public ICommand AddWorkoutCommand => new Command(OnAddWorkoutCommand);

        #endregion

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
            }          
        }

        public override async Task InitializeAsync(object data)
        {
            WorkoutList = new List<Workout>();

            IsBusy = true;

            try
            {
                WorkoutList = await _workoutDataService.GetWorkoutsAsync() as List<Workout>;
                Debug.WriteLine($"Retrieved {WorkoutList.Count} workouts from data service.");
            }
            catch (Exception ex)
            {
                _dialogService.Popup("Loading workouts failed.");
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }

        private async void OnAddWorkoutCommand()
        {
            await _navigationService.NavigateToAsync(typeof(CreateWorkoutViewModel));
        }
    }
}
