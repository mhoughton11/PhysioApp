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
using DIYHIIT.Views;
using MvvmCross.ViewModels;
using Xamarin.Forms;

using static DIYHIIT.Library.Helpers.Helpers;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.ViewModels
{
    public class CreateWorkoutViewModel : BaseViewModel
    {
        public CreateWorkoutViewModel(INavigationService navigationService,
                                      IDialogService dialogService,
                                      IWorkoutDataService workoutDataService)
            : base(navigationService, dialogService)
        {
            _workoutDataService = workoutDataService;
        }

        private Random rand;
        private List<string> _workoutTypes;
        private ObservableCollection<Exercise> _exercises;
        private readonly IWorkoutDataService _workoutDataService;

        public string ActiveEntry { get; set; }
        public string RestEntry { get; set; }
        public int SelectedWorkoutType { get; set; }

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

            MessagingCenter.Subscribe<AddExerciseViewModel, Exercise>(this, "ExerciseAdded", (sender, arg) =>
            {
                arg.Index = rand.Next(0, 0xFFFF);
                Exercises.Add(arg);
            });

            return base.InitializeAsync(data);
        }

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

            // Create workout with the specified parameters/exercises.
            var workout = new Workout
            {
                Name = Enum.GetName(typeof(WorkoutType), workoutType) + " Workout",
                ActiveInterval = activeInterval,
                RestInterval = restInterval,
                Exercises = Exercises.ToList(),
                Type = (WorkoutType)workoutType,
                DateAdded = DateTime.Now
            };
            workout.Duration = GetWorkoutDuration(workout);

            // Set all ID's to 0 (EntityFramework complains if it tries to set the ID and it's already been given a value)
            foreach (var ex in workout.Exercises)
                ex.Id = 0;

            workout = await GetWorkoutName(workout);

            await _workoutDataService.SaveWorkout(workout);
        }

        private async void OnAddExerciseCommand()
        {
            await _navigationService.NavigateToAsync<AddExerciseViewModel>();
        }

        private async Task<Workout> GetWorkoutName(Workout workout)
        {
            var name = await _dialogService.ShowConfirmAsync("Workout name", "Do you wish to name your workout for easier reference?", "Yes", "No");

            if (name)
            {
                string input = await _dialogService.ShowPromptAsync("Workout Name", "Enter workout name below", "OK", "Cancel");

                if (!string.IsNullOrWhiteSpace(input) || !string.IsNullOrEmpty(input))
                {
                    workout.Name = input;
                }
                else
                {
                    _dialogService.Popup("Please type a workout name when prompted.");
                    return null;
                }
            }

            return workout;
        }
    }
}
