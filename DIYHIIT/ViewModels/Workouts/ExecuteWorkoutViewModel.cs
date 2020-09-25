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
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.ViewModels.Workouts
{
    class ExecuteWorkoutViewModel : BaseViewModel
    {
        public ExecuteWorkoutViewModel(Workout workout,
                                       List<IExercise> exercises,
                                       IFeedItemService feedItemService,
                                       IWorkoutDataService workoutDataService,
                                       IUserDataService userDataService,
                                       INavigation navigationService,
                                       IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _exercises = exercises;
            _userDataService = userDataService;
            _feedItemService = feedItemService;
            _workoutDataService = workoutDataService;

            Task.Run(async () => await InitializeAsync(workout));
        }

        #region Private Fields

        // View Components
        private string _currentExerciseName;
        private string _imageName;
        private double _exerciseTimeLeft;
        private string _nextExercise;
        private double _exerciseProgress;
        private double _workoutProgress;
        private string _progressLabel;
        private string _timeLeftColour;
        private string _effortDetailEnabled;
        private string _timeLabelEnabled;
        private bool _postToFeed;

        // Model Components
        double currentExerciseTime;     // Time spent in current ex
        double totalTimeElapsed = 0;    // Total time spent in workout

        double _workoutDuration;        // Time to be spent in total in workout
        private IWorkout _workout;
        int counter = 0;

        private List<IExercise> _exercises;
        private readonly IUserDataService _userDataService;
        private readonly IFeedItemService _feedItemService;
        private readonly IWorkoutDataService _workoutDataService;

        #endregion

        #region Public Members and Commands

        public string CurrentExercise 
        {
            get => _currentExerciseName;
            set
            {
                _currentExerciseName = value;
                RaisePropertyChanged(() => CurrentExercise);
            }
        }
        
        public string ImageURL
        {
            get => _imageName;
            set
            {
                _imageName = value;
                RaisePropertyChanged(() => ImageURL);
            }
        }
        
        public double ExerciseTimeLeft 
        {
            get => _exerciseTimeLeft;
            set
            {
                _exerciseTimeLeft = value;
                RaisePropertyChanged(() => ExerciseTimeLeft);

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

        public bool PostToFeed
        {
            get => _postToFeed;
            set
            {
                _postToFeed = value;
                RaisePropertyChanged(() => PostToFeed);
            }
        }

        public double EffortSliderValue { get; set; }

        public ICommand DoneCommand => new Command(ExecuteDoneCommand);

        #endregion

        #region Public Methods

        public override async Task InitializeAsync(object workout)
        {
            _workout = workout as Workout;

            InitViewComponents();

            BeginWorkoutAsync();

            await base.InitializeAsync(workout);
        }

        private void InitViewComponents()
        {
            // Progress bar is 0
            currentExerciseTime = 0;

            PostToFeed = false;

            // Get exercise
            var ex = _exercises[0];
            // Set labels and images
            SetExerciseViews(ex);
            // Set progress bars
            SetProgressBars(ex);

            // Init labels
            EffortDetailEnabled = "False";
            TimeLabelEnabled = "True";
            TimeLeftColour = "White";

            foreach (var _ex in _exercises)
            {
                _workoutDuration += _ex.Duration.GetValueOrDefault();
            }
            _workoutDuration += _exercises.Count-1;

            GetNextExercise(0);
        }

        #endregion

        #region Private Methods

        private void BeginWorkoutAsync()
        {            
            var timeSpan = new TimeSpan(0, 0, 0, 1);

            bool stopping = true;

            Device.StartTimer(timeSpan, () =>
            {
                // Update the UI every second
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Interact with UI elements
                    currentExerciseTime++;
                    totalTimeElapsed++;

                    IExercise ex;

                    try
                    {
                        ex = _exercises[counter];
                    }
                    catch
                    {
                        stopping = false;
                        StopWorkout();
                        return;
                    }

                    SetProgressBars(ex);
                    SetExerciseViews(ex);
                    GetTimeLeftColour(currentExerciseTime);

                    if (currentExerciseTime > ex.Duration)
                    {
                        currentExerciseTime = 0;
                        counter++;

                        GetNextExercise(counter);

                        try
                        {
                            ex = _exercises[counter];
                        }
                        catch (Exception)
                        {
                            stopping = false;
                            StopWorkout();
                            return;
                        }
                    }
                });
                return stopping;
            });
        }

        private void SetProgressBars(IExercise ex)
        {
            WorkoutProgress = totalTimeElapsed / _workoutDuration;
            WorkoutProgressLabel = string.Format("{0:0}%", WorkoutProgress * 100);

            ExerciseProgress = currentExerciseTime / ex.Duration.Value;
        }

        private void SetExerciseViews(IExercise ex)
        {
            ExerciseTimeLeft = (ex.Duration.Value) - currentExerciseTime + 1;
            CurrentExercise = ex.DisplayName;
            ImageURL = ex.ImageURL;
            TimeLeftColour = GetTimeLeftColour(ExerciseTimeLeft);
        }

        private string GetTimeLeftColour(double time)
        {
            if (time > 5)
                return "White";
            else
                return "Red";
        }

        private void GetNextExercise(int count)
        {
            try
            {
                var next = _exercises[count];

                if (next.Name == "Rest")
                {
                    // Update next exercise
                    NextExercise = _exercises[count + 1].DisplayName;
                }
                else
                {
                    NextExercise = _exercises[count + 2].DisplayName;
                }
            }
            catch
            {
                NextExercise = "Finished!";
            }         
        }

        private void StopWorkout()
        {
            Debug.WriteLine("Stopping workout.");

            EffortDetailEnabled = "True";
            TimeLabelEnabled = "False";

            ExerciseTimeLeft = 0;
            CurrentExercise = "Finished!";

            ImageURL = "https://media.istockphoto.com/videos/just-finished-a-great-workout-at-the-gym-video-id827888858?s=640x640";

            NextExercise = "";
        }

        private async void ExecuteDoneCommand()
        {
            _dialogService.ShowLoading("Syncing...");

            // Make a copy and the recently used date and effort of the workout.
            _workout.DateUsed = DateTime.Now;
            _workout.Effort = Math.Round(EffortSliderValue, 1);

            App.CurrentUser.WorkoutAuditTrails.Add( new AuditTrail()
            {
                AuditWorkout = _workout as Workout,
                Notes = $"{App.CurrentUser.Username} completed workout {_workout.Name}"
            });

            await _userDataService.UpdateUser(App.CurrentUser);
            await _workoutDataService.UpdateWorkout(_workout);

            if (App.CurrentUser.UserSettings.PostToFeed)
            {
                var item = new FeedItem()
                {
                    UserName = App.CurrentUser.FirstName + " " + App.CurrentUser.Surname,
                    Title = "Workout completed!",
                    Message = $"I just completed {_workout.Name}. Effout out of 10: {_workout.Effort}",
                    ImageURL = "https://api.time.com/wp-content/uploads/2020/03/gym-coronavirus.jpg?w=600&quality=85",
                    FeedType = FeedItemType.Post
                };

                await _feedItemService.PostFeedItem(item);
                MessagingCenter.Send(this, "WorkoutPosted");
            }

            _dialogService.HideLoading();

            await _navigation.PopAsync();
        }

        #endregion
    }
}
