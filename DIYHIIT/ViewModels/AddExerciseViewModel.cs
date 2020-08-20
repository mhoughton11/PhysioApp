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
using System.Threading.Tasks;

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

        public override Task InitializeAsync(object data)
        {
            IsBusy = true;
            // Calls GetExercises() when property changes.
            SelectedIndex = 0;

            ExerciseTypes = Enum.GetNames(typeof(WorkoutType)).ToList();
            IsBusy = false;

            return base.InitializeAsync(data);
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

        private async void GetExercises(string type = null)
        {
            var items = await _exeriseDataService.GetAllExercisesAsync(type);

            if (items == null)
            {
                await _dialogService.ShowAlertAsync("Unable to fetch exercises from database.", "Connection error.", "OK");
                return;
            }

            FlowExercises = new ObservableCollection<IExercise>(items);
        }
    }
}
