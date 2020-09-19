using System;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.ViewModels.Base;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Profile
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel(INavigation navigation,
                                 IDialogService dialogService)
            : base (navigation, dialogService)
        {

        }

        #region Private Fields

        // View Components

        // Model Components

        #endregion
    }
}
