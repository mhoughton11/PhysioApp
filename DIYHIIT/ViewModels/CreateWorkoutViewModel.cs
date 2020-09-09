﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Helpers;
using DIYHIIT.Library.Models;
using DIYHIIT.Views;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;

using static DIYHIIT.Library.Helpers.Helpers;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.ViewModels
{
    public class CreateWorkoutViewModel : BaseViewModel
    {
        public CreateWorkoutViewModel(object data,
                                      INavigation navigation,
                                      IDialogService dialogService,
                                      IWorkoutDataService workoutDataService)
            : base(navigation, dialogService)
        {
            _workoutDataService = workoutDataService;

            InitializeAsync(data);
        }

        #region Private Fields

        private Random rand;
        private int _selectedWorkoutType;
        private List<string> _workoutTypes;
        private ObservableCollection<Exercise> _exercises;
        private readonly IWorkoutDataService _workoutDataService;

        #endregion

        #region Public Members and Commands

        public string ActiveEntry { get; set; }
        public string RestEntry { get; set; }

        public int SelectedWorkoutType
        {
            get => _selectedWorkoutType;
            set
            {
                _selectedWorkoutType = value;
                RaisePropertyChanged(() => SelectedWorkoutType);

                Debug.WriteLine($"Selected workout type changed: {value}");
            }
        }

        public ObservableCollection<Exercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                RaisePropertyChanged(() => Exercises);
            }
        }

        public List<string> WorkoutTypes
        {
            get => _workoutTypes;
            set
            {
                _workoutTypes = value;
                RaisePropertyChanged(() => WorkoutTypes);
            }
        }

        public ICommand DoneCommand => new Command(OnDoneCommand);
        public ICommand AddExerciseCommand => new Command(OnAddExerciseCommand);

        #endregion

        #region Public Methods

        public void RemoveObject(int index)
        {
            Exercise ex = Exercises.Where(e => e.Index == index) as Exercise;
            Exercises.Remove(ex);
        }

        public override void InitializeAsync(object data)
        {
            Exercises = new ObservableCollection<Exercise>();
            rand = new Random();

            WorkoutTypes = Enum.GetNames(typeof(WorkoutType)).ToList();

            MessagingCenter.Subscribe<AddExerciseViewModel, Exercise>(this, "ExerciseAdded", (sender, arg) =>
            {
                arg.Index = rand.Next(0, 0xFFFF);
                Exercises.Add(arg);
            });
        }

        #endregion

        #region Private Methods

        private async void OnDoneCommand()
        {
            double activeInterval, restInterval;
            int workoutType;

            // Check all required params aren't null.
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

            try
            {
                workoutType = SelectedWorkoutType;
            }
            catch (Exception)
            {
                await _dialogService.ShowAlertAsync("Empty field.", "Please enter a value for the workout type.", "OK");
                return;
            }

            var Ids = new List<int>();
            foreach (var ex in _exercises)
            {
                Ids.Add(ex.Id);
            }

            // Create workout with the specified parameters/exercises.
            var workout = new Workout
            {
                ActiveInterval = activeInterval,
                RestInterval = restInterval,
                ExerciseIDs = JsonConvert.SerializeObject(Ids),
                Type = (WorkoutType)workoutType,
                DateAdded = DateTime.Now
            };
            workout.Duration = Helpers.GetWorkoutDuration(workout);
            workout.ExerciseCount = Helpers.GetWorkoutCountString(workout);
            workout = await GetWorkoutName(workout);

            await _workoutDataService.SaveWorkout(workout, App.AppHostOptions);
            await _navigation.PopAsync();           
        }

        private async void OnAddExerciseCommand()
        {
            await _navigation.PushAsync(new AddExerciseView());
        }

        private async Task<Workout> GetWorkoutName(Workout workout)
        {
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
                }
            }

            else
            {
                var t = Enum.GetName(typeof(WorkoutType), SelectedWorkoutType);
                workout.Name =  t + " Workout";
            }

            return workout;
        }

        #endregion
    }
}
