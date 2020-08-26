using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.General;
using MvvmCross.ViewModels;

namespace DIYHIIT.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        protected readonly IDialogService _dialogService;
        protected readonly INavigationService _navigationService;

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public BaseViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }
    }
}
