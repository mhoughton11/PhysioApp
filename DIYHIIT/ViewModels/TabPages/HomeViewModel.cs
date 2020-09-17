using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Contracts.ViewComponents;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Models.ViewComponents;
using DIYHIIT.ViewModels.Base;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Tabs
{
    public class HomeViewModel: BaseViewModel
    {
        public HomeViewModel(IUserDataService userDataService,
                             INavigation navigationService,
                             IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _userDataService = userDataService;

            Task.Run(() => InitializeAsync(null));
        }

        #region Private Members

        // Services
        private readonly IUserDataService _userDataService;

        // View Components
        private ObservableCollection<IFeedItem> _feedItems;

        #endregion

        #region Public Fields and Commands

        public ObservableCollection<IFeedItem> FeedItems
        {
            get => _feedItems;
            set
            {
                _feedItems = value;
                RaisePropertyChanged(() => FeedItems);
            }
        }

        #endregion

        #region Public Methods

        public override async Task InitializeAsync(object data)
        {
            FeedItems = new ObservableCollection<IFeedItem>();

            var item = new FeedItem()
            {
                User = App.CurrentUser
            };

            FeedItems.Add(item);

            await base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        

        #endregion
    }
}
