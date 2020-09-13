using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels.Base;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Tabs
{
    public class HomeViewModel: BaseViewModel
    {
        public HomeViewModel(IWorkoutDataService workoutDataService,
                             INavigation navigationService,
                             IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _workoutDataService = workoutDataService;

            Task.Run(() => InitializeAsync(null));
        }

        #region Private Members

        // View Components
        private string _recentWorkoutsLabel;
        private string _welcomeText;
        private List<Workout> _workoutList;

        // Services
        private readonly IWorkoutDataService _workoutDataService;

        #endregion

        #region Public Fields and Commands

        public List<Workout> WorkoutList
        {
            get => _workoutList;
            set
            {
                _workoutList = value;
                RaisePropertyChanged(() => WorkoutList);
            }
        }
        
        public string RecentWorkoutsLabel
        {
            get => _recentWorkoutsLabel;
            set
            {
                _recentWorkoutsLabel = value;
                RaisePropertyChanged(() => RecentWorkoutsLabel);
            }
        }

        public string WelcomeText
        {
            get => _welcomeText;
            set
            {
                _welcomeText = value;
                RaisePropertyChanged(() => WelcomeText);
            }
        }

        #endregion

        #region Public Methods

        public override async Task InitializeAsync(object data)
        {
            RecentWorkoutsLabel = "False";

            WelcomeText = $"Welcome back, {App.CurrentUser.FirstName}";

            WorkoutList = await GetWorkouts();
            await base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async Task<List<Workout>> GetWorkouts()
        {
            var workouts = await _workoutDataService.GetWorkoutsAsync();

            var result = from workout in workouts
                         where workout.DateAdded <= DateTime.Today.AddDays(-7)
                         select workout;

            return result as List<Workout>;
        }

        #endregion
    }
}
