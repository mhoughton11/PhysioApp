using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.Library.Contracts;
using PhysioApp.Library.Models;
using PhysioApp.ViewModels.Base;
using PhysioApp.ViewModels.Workouts;
using PhysioApp.Views;
using PhysioApp.Views.Workouts;
using Xamarin.Forms;

using static PhysioApp.Constants.Messages;


namespace PhysioApp.ViewModels.Tabs
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
            MessagingCenter.Subscribe<CreateWorkoutViewModel>(this, WorkoutsUpdated, async (sender) =>
            {
                await GetWorkouts();
            });

            await GetWorkouts();

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

            await GetWorkouts();

            IsRefreshing = "False";
        }

        private async Task GetWorkouts()
        {
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

        }

        #endregion
    }
}
