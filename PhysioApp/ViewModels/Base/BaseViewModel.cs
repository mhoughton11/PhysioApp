using System.Threading.Tasks;
using AutoMapper;
using PhysioApp.Contracts.Services.General;
using PhysioApp.Library.Models;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace PhysioApp.ViewModels.Base
{
    public class BaseViewModel : MvxViewModel
    {
        protected IMapper Mapper { get; }

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

            var config = new MapperConfiguration(cfg =>
            {
                // Put any required mappings here
            });

            Mapper = config.CreateMapper();
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }
    }
}
