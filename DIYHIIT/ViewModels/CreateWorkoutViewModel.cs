using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Models;
using DIYHIIT.Models.Exercise;
using DIYHIIT.Models.Workout;
using DIYHIIT.Views;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class CreateWorkoutViewModel : BaseViewModel
    {
        public string ActiveEntry { get; set; }
        public string RestEntry { get; set; }
        public string SelectedWorkoutType { get; set; }

        private ObservableCollection<Exercise> _exercises;
        public ObservableCollection<Exercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                RaisePropertyChanged(() => Exercises);
            }
        }

        public Command DoneCommand { get; set; }
        public Command AddExerciseCommand { get; set; }

        readonly Random rand;

        public CreateWorkoutViewModel(INavigationService navigationService, IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Exercises = new ObservableCollection<Exercise>();
            rand = new Random();

            DoneCommand = new Command(() => ExecuteDoneCommand());
            AddExerciseCommand = new Command(() => ExecuteAddExerciseCommand());

            MessagingCenter.Subscribe<AddExerciseViewModel, Exercise>(this, "ExerciseAdded", (sender, arg) =>
            {
                _dialogService.Popup($"Adding {arg.DisplayName} to the workout");
                arg.Index = rand.Next(0, 0xFFFF);
                Exercises.Add(arg);
            });
        }

        public void RemoveObject(int index)
        {
            for (int i = 0; i < Exercises.Count; i++)
                if (Exercises[i].Index == index)
                {
                    Exercises.RemoveAt(i);
                    return;
                }
        }

        private async void ExecuteDoneCommand()
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
                ExercisesString = exstring,
                Type = WorkoutType.HIIT,
                DateAdded = DateTime.Now
            };

            var name = await Application.Current.MainPage.DisplayAlert("Workout name", "Do you wish to name your workout for easier reference?", "Yes", "No");

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

            await App.WorkoutDatabase.SaveItemAsync(workout);
            await _navigationService.NavigateBackAsync();
        }

        private async void ExecuteAddExerciseCommand()
        {
            await _navigationService.NavigateToAsync<AddExerciseViewModel>();
        }
    }
}
