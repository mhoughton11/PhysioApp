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
using DIYHIIT.Library.Helpers;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels.Base;
using DIYHIIT.ViewModels.Exercises;
using DIYHIIT.Views;
using DIYHIIT.Views.Exercises;
using Newtonsoft.Json;
using Xamarin.Forms;

using static DIYHIIT.Constants.Messages;


namespace DIYHIIT.ViewModels.Workouts
{
    public class CreateWorkoutViewModel : BaseViewModel
    {
        public CreateWorkoutViewModel(object data,
                                      IWorkoutDataService workoutDataService,
                                      IUserDataService userDataService,
                                      INavigation navigation,
                                      IDialogService dialogService)
            : base(navigation, dialogService)
        {
            InitializeAsync(data);

            _workoutDataService = workoutDataService;
            _userDataService = userDataService;
        }

        #region Private Fields

        private Random rand;
        private int _selectedWorkoutType;
        private List<string> _workoutTypes;
        private ObservableCollection<Exercise> _exercises;
        private readonly IWorkoutDataService _workoutDataService;
        private readonly IUserDataService _userDataService;

        #endregion

        #region Public Members and Commands

        public string ActiveEntry { get; set; }
        public string RestEntry { get; set; }

        public int SelectedWorkoutType
        {
            get => _selectedWorkoutType;
            set
            {
                _selectedWorkoutType = value;
                RaisePropertyChanged(() => SelectedWorkoutType);
            }
        }

        public ObservableCollection<Exercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                RaisePropertyChanged(() => Exercises);
            }
        }

        public List<string> WorkoutTypes
        {
            get => _workoutTypes;
            set
            {
                _workoutTypes = value;
                RaisePropertyChanged(() => WorkoutTypes);
            }
        }

        public ICommand DoneCommand => new Command(OnDoneCommand);
        public ICommand AddExerciseCommand => new Command(OnAddExerciseCommand);

        #endregion

        #region Public Methods

        public void RemoveObject(int index)
        {
            Exercise ex = Exercises.Where(e => e.Index == index) as Exercise;
            Exercises.Remove(ex);
        }

        public override Task InitializeAsync(object data)
        {
            Exercises = new ObservableCollection<Exercise>();
            rand = new Random();

            WorkoutTypes = Enum.GetNames(typeof(WorkoutType)).ToList();

            MessagingCenter.Subscribe<AddExerciseViewModel, Exercise>(this, ExerciseAdded, (sender, arg) =>
            {
                // Check how many times this exercise already exists in workout
                var count = _exercises.Where(e => e.Name == arg.Name).Count();
                arg.Index = count;

                Exercises.Add(arg);
            });

            return base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void OnDoneCommand()
        {
            double activeInterval, restInterval;
            int workoutType;

            // Check all required params aren't null.
            try
            {
                activeInterval = double.Parse(ActiveEntry);
            }
            catch (Exception)
            {
                await _dialogService.ShowAlertAsync("Empty field.", "Please enter a value for the active interval.", "OK");
                return;
            }

            try
            {
                restInterval = double.Parse(RestEntry);
            }
            catch (Exception)
            {
                await _dialogService.ShowAlertAsync("Empty field.", "Please enter a value for the rest interval.", "OK");
                return;
            }

            try
            {
                workoutType = SelectedWorkoutType;
            }
            catch (Exception)
            {
                await _dialogService.ShowAlertAsync("Empty field.", "Please enter a value for the workout type.", "OK");
                return;
            }

            if (_exercises.Count == 0 || _exercises == null)
            {
                await _dialogService.ShowAlertAsync("No exercises.", "Pleases add at least one exercise.", "OK");
                return;
            }

            var ids = new List<string>();
            foreach (var ex in _exercises) { ids.Add(ex.Id.ToString()); }

            var name = await GetWorkoutName();

            // Create workout with the specified parameters/exercises.
            var workout = new Workout()
            {
                Name = name,
                ActiveInterval = activeInterval,
                RestInterval = restInterval,
                Type = (WorkoutType)workoutType,
                BackgroundImage = _exercises[0].ImageURL,
                DateAdded = DateTime.Now,
                ExerciseIds = JsonConvert.SerializeObject(ids),
                Duration = Helpers.GetWorkoutDuration(_exercises.ToList(), activeInterval, restInterval),
                ExerciseCount = Helpers.GetWorkoutCountString(_exercises.ToList()),
                UserId = App.CurrentUser.Id
            };

            await _workoutDataService.SaveWorkout(workout);

            MessagingCenter.Send(this, WorkoutsUpdated);

            await _navigation.PopAsync();           
        }

        private async void OnAddExerciseCommand()
        {
            await _navigation.PushAsync(new AddExerciseView());
        }

        private async Task<string> GetWorkoutName()
        {
            var getName = await _dialogService.ShowConfirmAsync("Workout name", "Do you wish to name your workout for easier reference?", "Yes", "No");

            var name = string.Empty;

            if (getName)
            {
                string input = await _dialogService.ShowPromptAsync("Workout Name", "Enter workout name below", "OK", "Cancel");

                if (!string.IsNullOrWhiteSpace(input) || !string.IsNullOrEmpty(input))
                {
                    name = input;
                }
                else
                {
                    _dialogService.Popup("Please type a workout name when prompted.");
                    var t = Enum.GetName(typeof(WorkoutType), SelectedWorkoutType);
                    name = t + " Workout";
                }
            }
            else
            {
                name =  "My Workout";
            }

            return name;
        }

        #endregion
    }
}
