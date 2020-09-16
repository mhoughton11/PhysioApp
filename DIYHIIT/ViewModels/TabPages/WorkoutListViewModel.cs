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
                                    INavigation navigationService,
                                    IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            MessagingCenter.Subscribe<CreateWorkoutViewModel>(this, WorkoutsUpdated, (sender) =>
            {
                _workoutsUpdated = true;
            });

            Task.Run(async () => await InitializeAsync(data));
            _workoutDataService = workoutDataService;
        }

        #region Private Fields

        private ObservableCollection<IWorkout> _workoutList;
        private bool _workoutsUpdated = true;
        private string _isRefreshing;
        private readonly IWorkoutDataService _workoutDataService;

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
            IsBusy = true;
            _dialogService.ShowLoading("Loading workouts...");

            if (_workoutsUpdated)
            {
                try
                {
                    var workouts = await _workoutDataService.GetWorkoutsForUser(App.CurrentUser.Id);
                    WorkoutList = new ObservableCollection<IWorkout>(workouts);  
                }
                catch (Exception ex)
                {
                    _dialogService.Popup("Loading workouts failed.");
                    _dialogService.HideLoading();
                    Debug.WriteLine(ex);
                }
            }

            _dialogService.HideLoading();
            _workoutsUpdated = false;

            IsBusy = false;

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

            await InitializeAsync(null);

            IsRefreshing = "False";
        }

        #endregion
    }
}
