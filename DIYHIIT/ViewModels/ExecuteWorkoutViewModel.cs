using DIYHIIT.Data;
using DIYHIIT.Models.Exercise;
using DIYHIIT.Models.Workout;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    class ExecuteWorkoutViewModel : MvxViewModel
    {
        private string _currentExercise;
        public string CurrentExercise 
        {
            get => _currentExercise;
            set
            {
                _currentExercise = value;
                RaisePropertyChanged(() => CurrentExercise);
            }
        }

        private string _imageName;
        public string ImageName
        {
            get => _imageName;
            set
            {
                _imageName = value;
                RaisePropertyChanged(() => ImageName);
            }
        }

        private double _timeLeft;
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

        private string _nextExercise;
        public string NextExercise 
        {
            get => _nextExercise;
            set
            {
                _nextExercise = value;
                RaisePropertyChanged(() => NextExercise);
            }
        }

        private double _workoutProgress;
        public double WorkoutProgress 
        {
            get => _workoutProgress;
            set
            {
                _workoutProgress = value;
                RaisePropertyChanged(() => WorkoutProgress);
            }
        }

        private string _progressLabel;
        public string ProgressLabel
        {
            get => _progressLabel;
            set
            {
                _progressLabel = value;
                RaisePropertyChanged(() => ProgressLabel);
            }
        }

        private string _timeLeftColour;
        public string TimeLeftColour
        {
            get => _timeLeftColour;
            set
            {
                _timeLeftColour = value;
                RaisePropertyChanged(() => TimeLeftColour);
            }
        }

        public Command DoneCommand { get; set; }

        private string _effortDetailEnabled;
        public string EffortDetailEnabled 
        {
            get => _effortDetailEnabled;
            set
            {
                _effortDetailEnabled = value;
                RaisePropertyChanged(() => EffortDetailEnabled);
            }
        }

        private string _timeLabelEnabled;
        public string TimeLabelEnabled
        {
            get => _timeLabelEnabled;
            set
            {
                _timeLabelEnabled = value;
                RaisePropertyChanged(() => TimeLabelEnabled);
            }
        }


        public double EffortSliderValue { get; set; }

        public INavigation Navigation { get; set; }

        double curTime;
        double totalTime = 0;
        double duration;

        Workout workout;
        int counter = 0;

        System.Timers.Timer timer;

        List<Exercise> exercises;

        public ExecuteWorkoutViewModel(Workout workout)
        {
            this.workout = workout;
            timer = new System.Timers.Timer(1000);

            EffortDetailEnabled = "False";
            TimeLabelEnabled = "True";

            exercises = new List<Exercise>();

            DoneCommand = new Command(() => ExecuteDoneCommand());

            Init();
            BeginWorkoutAsync();
        }

        private async void Init()
        {
            duration = workout.ActiveInterval * workout.Exercises.Count + workout.RestInterval * workout.Exercises.Count;

            var rest = await App.ExerciseDatabase.GetItemAsync("Rest");
            rest.Duration = workout.RestInterval;

            foreach (var ex in workout.Exercises)
            {
                exercises.Add(ex);
                exercises.Add(rest);
            }

            CurrentExercise = exercises[0].DisplayName;
            TimeLeft = exercises[0].Duration;
            ImageName = exercises[0].ImageURL;

            try
            {
                NextExercise = exercises[2].DisplayName;
            }
            catch (Exception)
            {
                NextExercise = "Finished!";
            }
        }

        private async void BeginWorkoutAsync()
        {
            // Initialize the progress bar
            WorkoutProgress = 0.0;
            ProgressLabel = "0";
            curTime = 0;

            // Start timer on background thread.
            await Task.Run(() => timer.Start());

            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UpdateUI(counter, curTime);
            });

            if (curTime+1 > exercises[counter].Duration)
            {
                curTime = 0;
                counter++;

                if (counter == exercises.Count)
                    StopWorkout();
            }

            curTime++;
            totalTime++;
        }

        private void UpdateUI(int count, double timerValue)
        {
            if (count == exercises.Count)
                return;

            // Update image
            ImageName = exercises[count].ImageURL;

            // Update timer
            TimeLeft = exercises[count].Duration - timerValue + 1;

            WorkoutProgress = totalTime / duration;
            ProgressLabel = string.Format("{0:0}%", WorkoutProgress * 100);

            // Update current exercise
            CurrentExercise = exercises[count].DisplayName;

            if (CurrentExercise != "Rest")
            {
                // Update next exercise
                try
                {
                    NextExercise = exercises[count + 2].DisplayName;
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
                    NextExercise = exercises[count + 1].DisplayName;
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
            var newWorkout = new Workout()
            {
                Name = workout.Name,
                ActiveInterval = workout.ActiveInterval,
                RestInterval = workout.RestInterval,
                ExercisesString = workout.ExercisesString,
                Type = workout.Type,
                BodyFocus = workout.BodyFocus,
                Effort = EffortSliderValue,
                DateAdded = workout.DateAdded,
                DateUsed = DateTime.Now,
            };           

            await App.RecentWorkouts.SaveItemAsync(newWorkout);
            await Navigation.PopAsync();
        }
    }
}
