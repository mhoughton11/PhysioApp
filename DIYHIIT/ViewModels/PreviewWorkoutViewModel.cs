using System;
using System.Collections.Generic;
using DIYHIIT.Models;
using DIYHIIT.Models.Exercise;
using DIYHIIT.Models.Workout;
using DIYHIIT.Views;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class PreviewWorkoutViewModel
    {
        public string BodyFocus { get; set; }
        public int WorkoutMoves { get; set; }
        public double WorkoutLength { get; set; }
        public string Name { get; set; }

        public List<Exercise> Exercises { get; set; }

        public Command BeginWorkoutCommand { get; set; }

        public INavigation Navigation;

        Workout workout;

        public PreviewWorkoutViewModel(Workout _workout)
        {
            workout = _workout;
            Name = _workout.Name;
            BodyFocus = workout.Focus;
            WorkoutMoves = workout.ExerciseCount;
            WorkoutLength = workout.Duration;

            Exercises = new List<Exercise>();

            BeginWorkoutCommand = new Command(() => ExecuteBeginWorkoutCommand());

            Init();
        }

        private async void Init()
        {
            var rest = await App.ExerciseDatabase.GetItemAsync("Rest");
            rest.Duration = workout.RestInterval;

            // Add exercises
            foreach (var exercise in workout.Exercises)
            {
                exercise.Duration = workout.ActiveInterval;
                Exercises.Add(exercise);
                Exercises.Add(rest);
            }
        }

        private async void ExecuteBeginWorkoutCommand()
        {
            await Navigation.PushAsync(new ExecuteWorkoutPage(workout));
        }
    }
}
