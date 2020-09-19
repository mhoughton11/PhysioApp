using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models.ViewComponents;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Workouts
{
    class ExecuteWorkoutViewModel : BaseViewModel
    {
        public ExecuteWorkoutViewModel(Workout workout,
                                       List<IExercise> exercises,
                                       IUserDataService userDataService,
                                       INavigation navigationService,
                                       IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _workout = workout;
            _exercises = exercises;
            _userDataService = userDataService;
            Task.Run(async() => await InitializeAsync(workout));
        }

        #region Private Fields

        // View Components
        private string _currentExercise;
        private string _imageName;
        private double _timeLeft;
        private string _nextExercise;
        private double _exerciseProgress;
        private double _workoutProgress;
        private string _progressLabel;
        private string _timeLeftColour;
        private string _effortDetailEnabled;
        private string _timeLabelEnabled;

        // Model Components
        double curTime;
        double totalTime = 0;
        double duration;
        private WorkoutCountdown _countdown;
        private IWorkout _workout;
        int counter = 0;

        Timer timer;

        private List<IExercise> _exercises;
        private readonly IUserDataService _userDataService;

        #endregion

        #region Public Members and Commands

        public string CurrentExercise 
        {
            get => _currentExercise;
            set
            {
                _currentExercise = value;
                RaisePropertyChanged(() => CurrentExercise);
            }
        }
        
        public string ImageName
        {
            get => _imageName;
            set
            {
                _imageName = value;
                RaisePropertyChanged(() => ImageName);
            }
        }
        
        public double TimeLeft 
        {
            get => _timeLeft;
            set
            {
                _timeLeft = value;
                RaisePropertyChanged(() => TimeLeft);

                GetTimeLeftColour(value);
            }
        }
        
        public string NextExercise 
        {
            get => _nextExercise;
            set
            {
                _nextExercise = value;
                RaisePropertyChanged(() => NextExercise);
            }
        }

        public double WorkoutProgress 
        {
            get => _workoutProgress;
            set
            {
                _workoutProgress = value;
                RaisePropertyChanged(() => WorkoutProgress);
            }
        }
        
        public string WorkoutProgressLabel
        {
            get => _progressLabel;
            set
            {
                _progressLabel = value;
                RaisePropertyChanged(() => WorkoutProgressLabel);
            }
        }
        
        public string TimeLeftColour
        {
            get => _timeLeftColour;
            set
            {
                _timeLeftColour = value;
                RaisePropertyChanged(() => TimeLeftColour);
            }
        }
        
        public string EffortDetailEnabled 
        {
            get => _effortDetailEnabled;
            set
            {
                _effortDetailEnabled = value;
                RaisePropertyChanged(() => EffortDetailEnabled);
            }
        }

        public string TimeLabelEnabled
        {
            get => _timeLabelEnabled;
            set
            {
                _timeLabelEnabled = value;
                RaisePropertyChanged(() => TimeLabelEnabled);
            }
        }

        public double ExerciseProgress
        {
            get => _exerciseProgress;
            set
            {
                _exerciseProgress = value;
                RaisePropertyChanged(() => ExerciseProgress);
            }
        }

        public double EffortSliderValue { get; set; }

        public ICommand DoneCommand => new Command(ExecuteDoneCommand);

        #endregion

        #region Public Methods

        public override async Task InitializeAsync(object workout)
        {
            timer = new Timer(1000);

            EffortDetailEnabled = "False";
            TimeLabelEnabled = "True";

            _countdown.Duration = _workout.Duration.Value;
            _countdown.Ticked += OnExerciseTimerTicked;

            var active = _workout.ActiveInterval.Value;
            var count = _exercises.Count;
            var rest = _workout.RestInterval.Value;

            duration = active * count + rest * count;

            CurrentExercise = _exercises[0].DisplayName;
            TimeLeft = _exercises[0].Duration ?? 0;
            ImageName = _exercises[0].ImageURL;

            try
            {
                NextExercise = _exercises[2].DisplayName;
            }
            catch (Exception)
            {
                NextExercise = "Finished!";
            }

            BeginWorkoutAsync();

            await base.InitializeAsync(workout);
        }

        #endregion

        #region Private Methods

        private async void BeginWorkoutAsync()
        {
            // Initialize the progress bar
            WorkoutProgress = 0.0;
            WorkoutProgressLabel = "0";
            curTime = 0;

            // Start timer on background thread.
            await Task.Run(() => timer.Start());

            timer.Elapsed += WorkoutTimerElapsed;

            _countdown.Ticked += OnExerciseTimerTicked;
            _countdown.Completed += OnExerciseTimerCompleted;

            _countdown.Start(1);
        }

        private void WorkoutTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UpdateUI(_exercises[counter], curTime);
            });

            if (curTime+1 > _exercises[counter].Duration)
            {
                curTime = 0;
                counter++;

                if (counter == _exercises.Count)
                    StopWorkout();
            }

            curTime++;
            totalTime++;
        }

        private void OnExerciseTimerTicked()
        {
            var remaining = _countdown.RemainingTime;
            var total = _exercises[counter].Duration.Value;

            ExerciseProgress = remaining / total;
        }

        private void OnExerciseTimerCompleted()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UpdateUI(_exercises[counter], curTime);
            });

            if (curTime + 1 > _exercises[counter].Duration)
            {
                curTime = 0;
                counter++;

                if (counter == _exercises.Count)
                {
                    StopWorkout();
                }
            }

            curTime++;
            totalTime++;

            _countdown.Start();
        }

        private void UpdateUI(IExercise curEx, double timerValue)
        {
            // Update image
            ImageName = curEx.ImageURL;

            // Update timer
            TimeLeft = (curEx.Duration.Value) - timerValue + 1;

            WorkoutProgress = totalTime / duration;
            WorkoutProgressLabel = string.Format("{0:0}%", WorkoutProgress * 100);

            // Update current exercise
            CurrentExercise = curEx.DisplayName;

            if (CurrentExercise != "Rest")
            {
                // Update next exercise
                try
                {
                    NextExercise = _exercises[counter].DisplayName;
                }
                catch (Exception)
                {
                    NextExercise = "Finished!";
                }
            }
            else
            {
                // Update next exercise
                try
                {
                    NextExercise = _exercises[counter].DisplayName;
                }
                catch (Exception)
                {
                    NextExercise = "Finished!";
                }
            }
        }

        private void GetTimeLeftColour(double time)
        {
            if (time > 5)
                TimeLeftColour = "White";
            else
                TimeLeftColour = "Red";
        }

        private void StopWorkout()
        {
            Debug.WriteLine("Stopping workout.");
            timer.Stop();

            EffortDetailEnabled = "True";
            TimeLabelEnabled = "False";

            TimeLeft = 0;
            CurrentExercise = "Finished!";

            NextExercise = "";
        }

        private async void ExecuteDoneCommand()
        {
            // Make a copy and the recently used date and effort of the workout.
            _workout.DateUsed = DateTime.Now;
            _workout.Effort = Math.Round(EffortSliderValue, 1);

            await _userDataService.UpdateWorkout(_workout as Workout);

            await _navigation.PopAsync();
        }

        #endregion
    }
}
