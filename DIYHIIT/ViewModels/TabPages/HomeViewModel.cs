using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Contracts.ViewComponents;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels.Workouts;
using DIYHIIT.Library.Models.ViewComponents;
using DIYHIIT.ViewModels.Base;
using DIYHIIT.Library.Settings;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Tabs
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(IUserDataService userDataService,
                             IFeedItemService feedItemService,
                             INavigation navigationService,
                             IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _userDataService = userDataService;
            _feedItemService = feedItemService;

            InitializeAsync(null);
        }

        #region Private Members

        // Services
        private readonly IUserDataService _userDataService;
        private readonly IFeedItemService _feedItemService;

        // View Components
        private bool _feedUpdated;
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
            GetFeedItems();

            MessagingCenter.Subscribe<ExecuteWorkoutViewModel>(this, () =>
            {
                GetFeedItems();
            });

            await base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void GetFeedItems()
        {
            var items = await _feedItemService.GetAllItems();
            FeedItems = new ObservableCollection<IFeedItem>(items);
        }

        #endregion
    }
}
