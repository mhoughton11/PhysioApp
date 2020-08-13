using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Models.Workout;

namespace DIYHIIT.ViewModels
{
    public class HomeViewModel: BaseViewModel
    {
        public HomeViewModel(INavigationService navigationService, IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            RecentWorkoutsLabel = "False";

            WorkoutList = new List<Workout>();
        }

        private List<Workout> _workoutList;
        public List<Workout> WorkoutList
        {
            get => _workoutList;
            set
            {
                _workoutList = value;
                RaisePropertyChanged(() => WorkoutList);
            }
        }

        private string _recentWorkoutsLabel;
        public string RecentWorkoutsLabel
        {
            get => _recentWorkoutsLabel;
            set
            {
                _recentWorkoutsLabel = value;
                RaisePropertyChanged(() => RecentWorkoutsLabel);
            }
        }

        public async void OnAppearing()
        {
            WorkoutList = await GetWorkouts();
        }

        private async Task<List<Workout>> GetWorkouts()
        {
            var workoutList = await App.RecentWorkouts.GetItemsAsync();
            // Get workouts
            try
            {
                for (int i = 0; i < workoutList.Count; i++)
                {
                    var workout = workoutList[i];

                    if (workout.DateUsed != null)
                    {
                        // if workout is greater than 7 days old, don't include in list.
                        var d1 = workout.DateUsed;
                        var d2 = DateTime.Now;

                        if ((d2 - d1).TotalDays > 7)
                            workoutList.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get recent workouts.");
                Debug.WriteLine(ex.Message);
                return null;
            }        
            
            try
            {
                foreach (var workout in workoutList)
                    await workout.GetExercises(workout.ExercisesString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to retrieve exercises for recent workouts.");
                Debug.WriteLine(ex.Message);
                return null;
            }

            RecentWorkoutsLabel = (workoutList.Count > 0) ? "False" : "True";

            return workoutList.OrderByDescending(o => o.DateUsed).ToList();
        }
    }
}
