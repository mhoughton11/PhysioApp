using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels.Base;
using DIYHIIT.ViewModels.Workouts;
using DIYHIIT.Views;
using DIYHIIT.Views.Workouts;
using Xamarin.Forms;

using static DIYHIIT.Constants.Messages;


namespace DIYHIIT.ViewModels.Tabs
{
    public class WorkoutListViewModel : BaseViewModel
    {
        public WorkoutListViewModel(object data,
                                    IWorkoutDataService workoutDataService,
                                    IExerciseDataService exerciseDataService,
                                    INavigation navigationService,
                                    IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _workoutDataService = workoutDataService;
            _exerciseDataService = exerciseDataService;

            Task.Run(async () => await InitializeAsync(data));
        }

        #region Private Fields

        // View Components
        private ObservableCollection<IWorkout> _workoutList;
        private string _isRefreshing;

        // Model Components
        private bool _workoutsUpdated = true;
        
        private readonly IWorkoutDataService _workoutDataService;
        private readonly IExerciseDataService _exerciseDataService;

        #endregion

        #region Public Members and Commands

        public ObservableCollection<IWorkout> WorkoutList
        {
            get => _workoutList;
            set
            {
                _workoutList = value;
                RaisePropertyChanged(() => WorkoutList);
            }
        }

        public string IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        public ICommand RefreshCommand => new Command(OnRefresh);
        public ICommand AddWorkoutCommand => new Command(OnAddWorkoutCommand);
        public ICommand WorkoutTappedCommand => new Command<Workout>(OnWorkoutTapped);

        #endregion

        #region Public Methods

        public override async Task InitializeAsync(object data)
        {
            MessagingCenter.Subscribe<CreateWorkoutViewModel>(this, WorkoutsUpdated, (sender) =>
            {
                GetWorkouts();
            });

            GetWorkouts();

            _workoutsUpdated = false;

            await base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void OnAddWorkoutCommand()
        {
            await _navigation.PushAsync(new CreateWorkoutView());
        }

        private async void OnWorkoutTapped(Workout workout)
        {
            Debug.WriteLine($"Workout selected: {workout.Name}");

            await _navigation.PushAsync(new PreviewWorkoutView(workout));
        }

        private async void OnRefresh()
        {
            IsRefreshing = "True";

            GetWorkouts();

            IsRefreshing = "False";
        }

        private async void GetWorkouts()
        {
            _dialogService.ShowLoading("Loading workouts...");

                try
                {
                    var workouts = await _workoutDataService.GetWorkoutsForUser(App.CurrentUser.ID);
                    WorkoutList = new ObservableCollection<IWorkout>(workouts);  

                }
                catch (Exception ex)
                {
                    _dialogService.Popup("Loading workouts failed.");
                    Debug.WriteLine(ex);
                }

            _dialogService.HideLoading();
        }

        #endregion
    }
}
