using Xamarin.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using DIYHIIT.Models;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.ViewModels
{
    public class AddExerciseViewModel : BaseViewModel
    {
        public AddExerciseViewModel(INavigationService navigationService,
            IDialogService dialogService,
            IExerciseDataService exerciseDataService)
            : base(navigationService, dialogService)
        {
            _exeriseDataService = exerciseDataService;

            FlowExercises = new ObservableCollection<IExercise>();
            ExerciseTypes = Enum.GetNames(typeof(WorkoutType)).Cast<string>().ToList();

            SelectedFilter = ExerciseTypes[0];

            OnAppearing();
        }

        private readonly IExerciseDataService _exeriseDataService;

        private ObservableCollection<IExercise> _flowExercises;
        public ObservableCollection<IExercise> FlowExercises 
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

            FlowExercises = new ObservableCollection<IExercise>(items);
        }
    }
}
