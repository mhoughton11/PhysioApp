using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
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

        #endregion

        #region Public Fields and Commands


        #endregion

        #region Public Methods

        public override async Task InitializeAsync(object data)
        {
            await base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        

        #endregion
    }
}
