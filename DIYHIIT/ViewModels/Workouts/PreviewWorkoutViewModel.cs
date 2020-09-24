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
using DIYHIIT.Views;
using DIYHIIT.Views.Workouts;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Workouts
{
    public class PreviewWorkoutViewModel : BaseViewModel
    { 
        public PreviewWorkoutViewModel(Workout workout,
                                       IExerciseDataService exerciseDataService,
                                       
                                       INavigation navigation,
                                       IDialogService dialogService)
            : base(navigation, dialogService)
        {
            _exerciseDataService = exerciseDataService;

            Task.Run(async () => await InitializeAsync(workout));
        }

        #region Private Fields

        private string _workoutLength;
        private string _exerciseCount;
        private string _backgroundImage;
        private string _name;
        private Workout _workout;
        private ObservableCollection<IExercise> _exercises;
        private readonly IExerciseDataService _exerciseDataService;

        #endregion

        #region Public Properties and Commands

        public ObservableCollection<IExercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                RaisePropertyChanged(() => Exercises);
            }
        }

        public string BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                RaisePropertyChanged(() => BackgroundImage);
            }
        }

        public string ExerciseCount
        {
            get => _exerciseCount;
            set
            {
                _exerciseCount = value;
                RaisePropertyChanged(() => ExerciseCount);
            }
        }

        public string WorkoutLength
        {
            get => _workoutLength;
            set
            {
                _workoutLength = value;
                RaisePropertyChanged(() => WorkoutLength);
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public ICommand BeginWorkoutCommand => new Command(OnBeginWorkoutCommand);

        #endregion

        #region Public Methods

        public override async Task InitializeAsync(object data)
        {
            _dialogService.ShowLoading("Loading workout...");

            _workout = data as Workout;
            var items = (await _exerciseDataService.GetExercisesFromList(_workout.ExerciseIds)).ToList();

            Exercises = new ObservableCollection<IExercise>();

            BackgroundImage = items[0].ImageURL;
            Name = _workout.Name;
            ExerciseCount = _workout.ExerciseCount;
            WorkoutLength = $"{_workout.Duration ?? 0} Minutes";

            var rest = new Exercise()
            {
                Name = "Rest",
                DisplayName = "Rest",
                Duration = _workout.RestInterval,
                ImageURL = "https://content.thriveglobal.com/wp-content/uploads/2018/09/morning_workout_tired.jpeg?w=1550"
            };

            // Add rest behind all but last exercises.
            for (int i = 0; i < items.Count-1; i++)
            {
                items[i].Duration = _workout.ActiveInterval;
                Exercises.Add(items[i]);
                Exercises.Add(rest);
            }

            items[items.Count - 1].Duration = _workout.ActiveInterval;
            Exercises.Add(items[items.Count - 1]);

            _dialogService.HideLoading();

             base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void OnBeginWorkoutCommand()
        {
            await _navigation.PushAsync(new ExecuteWorkoutView(_workout, _exercises.ToList()));
        }

        #endregion
    }
}
