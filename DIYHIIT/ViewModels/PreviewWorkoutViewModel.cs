using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using DIYHIIT.Views;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class PreviewWorkoutViewModel : BaseViewModel
    {
        public string BodyFocus { get; set; }
        public int WorkoutMoves { get; set; }
        public double WorkoutLength { get; set; }
        public string Name { get; set; }

        public List<IExercise> Exercises { get; set; }

        public Command BeginWorkoutCommand { get; set; }

        public INavigation Navigation;

        private IWorkout _workout;

        public PreviewWorkoutViewModel(INavigation navigationService, IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Exercises = new List<IExercise>();

            BeginWorkoutCommand = new Command(async() => await ExecuteBeginWorkoutCommand());

            Init();
        }

        private void Init()
        {
            //var rest = await App.ExerciseDatabase.GetItemAsync("Rest");
            //rest.Duration = _workout.RestInterval;
        }

        public override void InitializeAsync(object workout)
        {
            _workout = (IWorkout)workout;



            Name = _workout.Name;
            //WorkoutMoves = _workout.ExerciseIDs.Count;
            BodyFocus = _workout.BodyFocus;
            WorkoutLength = _workout.Duration ?? 0;

            base.InitializeAsync(workout);
        }

        private async Task ExecuteBeginWorkoutCommand()
        {
            await _navigation.PushAsync(new ExecuteWorkoutView(_workout as Workout));
        }
    }
}
