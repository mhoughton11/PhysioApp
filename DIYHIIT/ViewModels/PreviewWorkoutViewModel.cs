using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public string ExerciseCount { get; set; }
        public double WorkoutLength { get; set; }
        public string Name { get; set; }

        public ICommand BeginWorkoutCommand => new Command(OnBeginWorkoutCommand);

        private Workout _workout;
        private List<IExercise> _exercises;
        private readonly IExerciseDataService _exerciseDataService;

        public PreviewWorkoutViewModel(Workout workout,
                                      IExerciseDataService exerciseDataService,
                                      INavigation navigation,
                                      IDialogService dialogService)
            : base(navigation, dialogService)
        {
            _exerciseDataService = exerciseDataService;

            InitializeAsync(workout);
        }

        public List<IExercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                RaisePropertyChanged(() => Exercises);
            }
        }

        private void Init()
        {
            //var rest = await App.ExerciseDatabase.GetItemAsync("Rest");
            //rest.Duration = _workout.RestInterval;
        }

        public override async void InitializeAsync(object workout)
        {
            _workout = workout as Workout;

            var items = await _exerciseDataService.GetExercisesFromList(_workout.ExerciseIDs);
            Exercises = items.ToList();
            
            Name = _workout.Name;
            ExerciseCount = _workout.ExerciseCount;
            BodyFocus = _workout.BodyFocus;
            WorkoutLength = _workout.Duration ?? 0;

            foreach (var ex in Exercises)
            {
                ex.Duration = _workout.ActiveInterval;
            }
        }

        private async void OnBeginWorkoutCommand()
        {
            await _navigation.PushAsync(new ExecuteWorkoutView(_workout));
        }
    }
}
