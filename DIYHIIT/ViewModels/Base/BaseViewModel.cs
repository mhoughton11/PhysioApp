using System.Threading.Tasks;
using AutoMapper;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Persistance.Models;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Base
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
                cfg.CreateMap<DB_User, User>();
                cfg.CreateMap<User, DB_User>();
                cfg.CreateMap<DB_Exercise, Exercise>();
                cfg.CreateMap<Exercise, DB_Exercise>();
            });

            Mapper = config.CreateMapper();
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }
    }
}
