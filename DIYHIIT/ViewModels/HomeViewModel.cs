using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class HomeViewModel: BaseViewModel
    {
        public HomeViewModel(IWorkoutDataService workoutDataService,
                             INavigation navigationService,
                             IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _workoutDataService = workoutDataService;

            InitializeAsync(null);
        }

        #region Private Members

        private string _recentWorkoutsLabel;
        private List<Workout> _workoutList;
        private int _selectedTab;
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

        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                SetProperty(ref _selectedTab, value);
            }
        }

        #endregion

        #region Public Methods

        public void SetSelectedTab(int tabIndex)
        {
            SelectedTab = tabIndex;
        }

        public override async void InitializeAsync(object data)
        {
            base.InitializeAsync(data);
            RecentWorkoutsLabel = "False";

            WorkoutList = await GetWorkouts();
        }

        #endregion

        #region Private Methods

        private async Task<List<Workout>> GetWorkouts()
        {
            var workouts = await _workoutDataService.GetWorkoutsAsync(App.AppHostOptions);

            var result = from workout in workouts
                         where workout.DateAdded <= DateTime.Today.AddDays(-7)
                         select workout;

            return result as List<Workout>;
        }

        #endregion
    }
}
