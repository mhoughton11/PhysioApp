using System;
using DIYHIIT.Contracts.Services.General;

namespace DIYHIIT.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel(INavigationService navigationService,
            IDialogService dialogService)
            : base (navigationService, dialogService)
        {

        }

        private string _demoLabel;
        public string DemoLabel
        {
            get => _demoLabel;
            set
            {
                _demoLabel = value;
                RaisePropertyChanged(() => DemoLabel);
            }
        }
    }
}
