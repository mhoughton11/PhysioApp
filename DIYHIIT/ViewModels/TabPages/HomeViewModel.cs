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
using DIYHIIT.Library.Settings;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Tabs
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(IUserDataService userDataService,
                             INavigation navigationService,
                             IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            _userDataService = userDataService;

            InitializeAsync(null);
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

        public override Task InitializeAsync(object data)
        {
            FeedItems = new ObservableCollection<IFeedItem>();

            var item = new FeedItem()
            {
                UserName = App.CurrentUser.FirstName + " " + App.CurrentUser.LastName,
                Title = "DIY HIIT 0.1!",
                Message = "Welcome to DIY-HIIT pre-launch. Basic funtionality down, lots of improvements on the way.",
                DateTime = DateTime.Now,
                ImageURL = "https://api.time.com/wp-content/uploads/2020/03/gym-coronavirus.jpg?w=600&quality=85"
                //FeedType = FeedItemTypes.Post
            };

            FeedItems.Add(item);

            return base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        

        #endregion
    }
}
