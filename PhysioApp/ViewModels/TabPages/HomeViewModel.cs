using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.Library.Contracts.ViewComponents;
using PhysioApp.Library.Models;
using PhysioApp.ViewModels.Workouts;
using PhysioApp.Library.Models.ViewComponents;
using PhysioApp.ViewModels.Base;
using PhysioApp.Library.Settings;
using Xamarin.Forms;

namespace PhysioApp.ViewModels.Tabs
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

            MessagingCenter.Subscribe<ExecuteWorkoutViewModel>(this, "WorkoutPosted", async (sender) =>
            {
                await GetFeedItems();
            });

            Task.Run(() => InitializeAsync(null));
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
            await GetFeedItems();

            await base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async Task GetFeedItems()
        {
            var items = await _feedItemService.GetAllItems();
            FeedItems = new ObservableCollection<IFeedItem>(items.OrderByDescending(i => i.Date));
        }

        #endregion
    }
}
