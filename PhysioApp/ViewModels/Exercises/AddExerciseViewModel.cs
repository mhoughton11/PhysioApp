﻿using Xamarin.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.Library.Models;
using PhysioApp.Library.Contracts;
using System.Threading.Tasks;
using System.Windows.Input;
using static PhysioApp.Library.Settings.Settings;
using static PhysioApp.Constants.Messages;
using PhysioApp.ViewModels.Base;

namespace PhysioApp.ViewModels.Exercises
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
        private ObservableCollection<IExercise> _exercises;
        private IExercise _selectedExercise;
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

        public ObservableCollection<IExercise> Exercises 
        {
            get => _exercises; 
            set
            {
                _exercises = value;
                RaisePropertyChanged(() => Exercises);
            }
        }

        public IExercise SelectedExercise
        {
            get => _selectedExercise;
            set
            {
                _selectedExercise = value;
                RaisePropertyChanged(() => SelectedExercise);

                if (value != null)
                {
                    OnExerciseTapped((Exercise)value);
                    SelectedExercise = null;
                }
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

        public override Task InitializeAsync(object data)
        {
            _dialogService.ShowLoading("Loading exericses...");

            ExerciseTypes = Enum.GetNames(typeof(WorkoutType)).Cast<string>().ToList();
            Exercises = new ObservableCollection<IExercise>();

            SelectedIndex = 0;
            _dialogService.HideLoading();

            return base.InitializeAsync(data);
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

            Exercises = new ObservableCollection<IExercise>(items);

            IndicatorEnabled = false;
        }

        private void OnRefreshCommand()
        {
            GetExercises(SelectedIndex);   
        }

        private void OnExerciseTapped(Exercise selectedExercise)
        {
            if (selectedExercise != null)
            {
                MessagingCenter.Send(this, ExerciseAdded, selectedExercise);
            }   
        }

        #endregion
    }
}
