using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using DIYHIIT.Views;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class WorkoutListViewModel : BaseViewModel
    {
        public WorkoutListViewModel(object data,
                                    IWorkoutDataService workoutDataService,
                                    INavigation navigationService,
                                    IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _workoutDataService = workoutDataService;
        }

        #region Private Fields

        private List<Workout> _workoutList;
        private Workout _selectedWorkout;
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

        public Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                RaisePropertyChanged(() => SelectedWorkout);

                WorkoutSelected(value);
            }
        }

        public ICommand AddWorkoutCommand => new Command(OnAddWorkoutCommand);
        public ICommand BeginWorkoutCommand => new Command<Workout>(OnBeginWorkoutCommand);

        #endregion

        #region Public Methods

        public async void OnBeginWorkoutCommand(Workout workout)
        {
            try
            {
                await _navigation.PushAsync(new PreviewWorkoutView(workout));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }          
        }

        public override async void InitializeAsync(object data)
        {
            WorkoutList = new List<Workout>();

            IsBusy = true;

            try
            {
                WorkoutList = await _workoutDataService.GetWorkoutsAsync(App.AppHostOptions) as List<Workout>;
            }
            catch (Exception ex)
            {
                _dialogService.Popup("Loading workouts failed.");
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }

        #endregion

        #region Private Methods

        private void OnAddWorkoutCommand()
        {
            await _navigation.PushAsync(new CreateWorkoutView());
        }

        private void WorkoutSelected(Workout workout)
        {
            await _navigation.PushAsync(new PreviewWorkoutView(workout));
        }

        #endregion
    }
}
