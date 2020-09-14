using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
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

            MessagingCenter.Subscribe<CreateWorkoutViewModel>(this, WorkoutsUpdated, (sender) =>
            {
                _workoutsUpdated = true;
            });

            Task.Run(async () => await InitializeAsync(data));
        }

        #region Private Fields

        private ObservableCollection<Workout> _workoutList;
        private bool _workoutsUpdated = true;
        private string _isRefreshing;
        private Workout _selectedItem;
        private readonly IWorkoutDataService _workoutDataService;
        private readonly IExerciseDataService _exerciseDataService;

        #endregion

        #region Public Members and Commands

        public ObservableCollection<Workout> WorkoutList
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

            if (_workoutsUpdated)
            {
                try
                {
                    _dialogService.ShowLoading("Loading workouts...");
                    WorkoutList = await _workoutDataService.GetWorkoutsAsync() as ObservableCollection<Workout>;
                    _dialogService.HideLoading();
                }
                catch (Exception ex)
                {
                    _dialogService.Popup("Loading workouts failed.");
                    Debug.WriteLine(ex);
                }
            }

            _workoutsUpdated = false;

            IsBusy = false;
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
