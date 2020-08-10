﻿using MvvmCross.ViewModels;
using Xamarin.Forms;
using DIYHIIT.Models.Exercise;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using MvvmCross.Binding.Extensions;
using DIYHIIT.Models;
using DIYHIIT.Services.Data;
using DIYHIIT.Contracts.Data;

namespace DIYHIIT.ViewModels
{
    public class AddExerciseViewModel : MvxViewModel
    {
        private readonly IExerciseDataService _exeriseDataService;

        private ObservableCollection<Exercise> _flowExercises;
        public ObservableCollection<Exercise> FlowExercises 
        {
            get => _flowExercises; 
            set
            {
                _flowExercises = value;
                RaisePropertyChanged(() => FlowExercises);
            }
        }

        private List<string> _exerciseTypes;
        public List<string> ExerciseTypes
        {
            get => _exerciseTypes;
            set
            {
                _exerciseTypes = value;
                RaisePropertyChanged(() => ExerciseTypes);
            }
        }

        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged(() => SelectedIndex);

                var ex = Enum.GetName(typeof(WorkoutType), value);
                GetExercises(ex);
            }
        }

        private string _selectedFilter = "All";
        public string SelectedFilter
        {
            get => _selectedFilter;
            set 
            {
                _selectedFilter = value;
                RaisePropertyChanged(() => SelectedFilter);

                GetExercises();
            }
        }

        public AddExerciseViewModel(IExerciseDataService exerciseDataService)
        {
            _exeriseDataService = exerciseDataService;

            FlowExercises = new ObservableCollection<Exercise>();
            ExerciseTypes = Enum.GetNames(typeof(WorkoutType)).Cast<string>().ToList();

            SelectedFilter = ExerciseTypes[0];
        }

        public void OnAppearing()
        {
            GetExercises();
        }

        public void ItemTapped(Exercise item)
        {
            try
            {
                MessagingCenter.Send(this, "ExerciseAdded", item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to send item using MC.");
                Debug.WriteLine(ex.Message);
            }
        }

        private async void GetExercises(string t = null)
        {
            if (t == null) { return; }

            var items = await _exeriseDataService.GetAllExercisesAsync();

            FlowExercises = new ObservableCollection<Exercise>(items);
        }
    }
}
