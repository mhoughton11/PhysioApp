using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
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

        public ICommand BeginWorkoutCommand => new Command(OnBeginWorkoutCommand);

        public INavigation Navigation;

        private Workout _workout;
        private readonly IExerciseDataService _exerciseDataService;

        public PreviewWorkoutViewModel(Workout workout, IExerciseDataService exerciseDataService, INavigation navigationService, IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            InitializeAsync(workout);
            _exerciseDataService = exerciseDataService;
        }

        private void Init()
        {
            //var rest = await App.ExerciseDatabase.GetItemAsync("Rest");
            //rest.Duration = _workout.RestInterval;
        }

        public override void InitializeAsync(object workout)
        {
            _workout = workout as Workout;

            //_exerciseDataService

            Name = _workout.Name;
            //WorkoutMoves = _workout.ExerciseIDs.Count;
            BodyFocus = _workout.BodyFocus;
            WorkoutLength = _workout.Duration ?? 0;

            base.InitializeAsync(workout);
        }

        private async void OnBeginWorkoutCommand()
        {
            await _navigation.PushAsync(new ExecuteWorkoutView(_workout));
        }
    }
}
