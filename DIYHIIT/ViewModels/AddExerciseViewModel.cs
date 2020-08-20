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

        private bool _indicatorEnabled;
        public bool IndicatorEnabled
        {
            get => _indicatorEnabled;
            set
            {
                _indicatorEnabled = value;
                RaisePropertyChanged(() => IndicatorEnabled);
            }
        }

        private ObservableCollection<IExercise> _exercises;

        public Command RefreshCommand => new Command(OnRefreshCommand);

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

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged(() => SelectedIndex);

                GetExercises(value);
            }
        }

        public override async Task InitializeAsync(object data)
        {
            ExerciseTypes = Enum.GetNames(typeof(WorkoutType)).ToList();
            SelectedIndex = 0;

            await base.InitializeAsync(data);
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

        private async void GetExercises(int? type = null)
        {
            var items = await _exeriseDataService.GetAllExercisesAsync(type);

            if (items == null)
            {
                await _dialogService.ShowAlertAsync("Unable to fetch exercises from database.", "Connection error.", "OK");
                return;
            }

            FlowExercises = new ObservableCollection<IExercise>(items);
        }

        private void OnRefreshCommand()
        {
            IndicatorEnabled = true;

            GetExercises(SelectedIndex);

            IndicatorEnabled = false;
        }
    }
}
