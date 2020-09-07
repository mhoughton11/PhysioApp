using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.General;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        protected readonly INavigation _navigation;
        protected readonly IDialogService _dialogService;

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

        public BaseViewModel(INavigation navigation, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigation = navigation;
        }

        public virtual void InitializeAsync(object data)
        {

        }
    }
}
