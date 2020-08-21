using System;
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

        public string ActiveEntry { get; set; }
        public string RestEntry { get; set; }
        public string SelectedWorkoutType { get; set; }

        private ObservableCollection<IExercise> _exercises;
        public ObservableCollection<IExercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                RaisePropertyChanged(() => Exercises);
            }
        }

        public ICommand DoneCommand => new Command(OnDoneCommand);
        public ICommand AddExerciseCommand => new Command(OnAddExerciseCommand);

        private Random rand;
        private readonly IWorkoutDataService _workoutDataService;

        public void RemoveObject(int index)
        {
            IExercise ex = Exercises.Where(e => e.Index == index) as IExercise;
            Exercises.Remove(ex);
        }

        public override Task InitializeAsync(object data)
        {
            Exercises = new ObservableCollection<IExercise>();
            rand = new Random();

            MessagingCenter.Subscribe<AddExerciseViewModel, Exercise>(this, "ExerciseAdded", (sender, arg) =>
            {
                arg.Index = rand.Next(0, 0xFFFF);
                Exercises.Add(arg);
            });

            return base.InitializeAsync(data);
        }

        private async void OnDoneCommand()
        {
            string exstring = "";

            foreach (var ex in Exercises)
                exstring += $"{ex.Name},";

            double activeInterval, restInterval;

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

            // Create workout with the specified parameters/exercises.
            var workout = new Workout
            {
                Name = SelectedWorkoutType + " Workout",
                ActiveInterval = activeInterval,
                RestInterval = restInterval,
                Type = WorkoutType.HIIT,
                DateAdded = DateTime.Now
            };

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
                    return;
                }
            }

            await _workoutDataService.AddWorkout(workout);
            await _navigationService.NavigateToAsync<WorkoutListViewModel>();
        }

        private async void OnAddExerciseCommand()
        {
            await _navigationService.NavigateToAsync<AddExerciseViewModel>();
        }
    }
}
