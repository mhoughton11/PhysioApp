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
using System.Windows.Input;

namespace DIYHIIT.ViewModels
{
    public class AddExerciseViewModel : BaseViewModel
    {
        #region Constructor
        public AddExerciseViewModel(INavigationService navigationService,
            IDialogService dialogService,
            IExerciseDataService exerciseDataService)
            : base(navigationService, dialogService)
        {
            _exeriseDataService = exerciseDataService;

            FlowExercises = new ObservableCollection<IExercise>();
            ExerciseTypes = Enum.GetNames(typeof(WorkoutType)).Cast<string>().ToList();
        }
        #endregion

        #region Private fields

        private readonly IExerciseDataService _exeriseDataService;

        private bool _indicatorEnabled;
        private ObservableCollection<IExercise> _flowExercises;
        private List<string> _exerciseTypes;
        private int _selectedIndex;

        #endregion

        #region Public Fields and Commands

        public ICommand RefreshCommand => new Command(OnRefreshCommand);
        public ICommand TappedCommand => new Command(OnExerciseTapped);
        
        public bool IndicatorEnabled
        {
            get => _indicatorEnabled;
            set
            {
                _indicatorEnabled = value;
                RaisePropertyChanged(() => IndicatorEnabled);
            }
        }

        public ObservableCollection<IExercise> FlowExercises 
        {
            get => _flowExercises; 
            set
            {
                _flowExercises = value;
                RaisePropertyChanged(() => FlowExercises);
            }
        }
 
        public List<string> ExerciseTypes
        {
            get => _exerciseTypes;
            set
            {
                _exerciseTypes = value;
                RaisePropertyChanged(() => ExerciseTypes);
            }
        }

        
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

        #endregion

        #region Public Methods

        public override async Task InitializeAsync(object data)
        {
            ExerciseTypes = Enum.GetNames(typeof(WorkoutType)).ToList();
            SelectedIndex = 0;

            await base.InitializeAsync(data);
        }

        public void ItemTapped(Exercise item)
        {
            
        }

        #endregion

        #region Private Methods

        private async void GetExercises(int? type = null)
        {
            IndicatorEnabled = true;

            var items = await _exeriseDataService.GetAllExercisesAsync(type);

            if (items == null)
            {
                await _dialogService.ShowAlertAsync("Unable to fetch exercises from database.", "Connection error.", "OK");
                return;
            }

            FlowExercises = new ObservableCollection<IExercise>(items);

            IndicatorEnabled = false;
        }

        private void OnRefreshCommand()
        {
            GetExercises(SelectedIndex);   
        }

        private void OnExerciseTapped()
        {
            /*
            var ex = FlowExercises

            try
            {
                MessagingCenter.Send(this, "ExerciseAdded", item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to send item using MC.");
                Debug.WriteLine(ex.Message);
            }
            */
        }

        #endregion
    }
}
