using Xamarin.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Contracts;
using System.Threading.Tasks;
using System.Windows.Input;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.ViewModels
{
    public class AddExerciseViewModel : BaseViewModel
    {
        #region Constructor
        public AddExerciseViewModel(object data,
                                    INavigation navigation,
                                    IDialogService dialogService,
                                    IExerciseDataService exerciseDataService)
            : base(navigation, dialogService)
        {
            _exeriseDataService = exerciseDataService;

            InitializeAsync(data);
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
        public ICommand ExerciseTapped => new Command<Exercise>(OnExerciseTapped);
        
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

        public override void InitializeAsync(object data)
        {
            ExerciseTypes = Enum.GetNames(typeof(WorkoutType)).Cast<string>().ToList();
            FlowExercises = new ObservableCollection<IExercise>();

            SelectedIndex = 0;
            base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void GetExercises(int? type = null)
        {
            IndicatorEnabled = true;

            var items = await _exeriseDataService.GetAllExercisesAsync(type, HostOptions.LocalHost);

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

        private void OnExerciseTapped(Exercise selectedExercise)
        {
            MessagingCenter.Send(this, "ExerciseAdded", selectedExercise);
        }

        #endregion
    }
}
