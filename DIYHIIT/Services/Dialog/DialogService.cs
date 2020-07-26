using Acr.UserDialogs;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIYHIIT.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public void Popup(string message)
        {
            CrossToastPopUp.Current.ShowToastMessage(message);
        }
        public Task ShowAlertAsync(string message)
        {
            return UserDialogs.Instance.AlertAsync(message);
        }
        public Task ShowAlertAsync(string message, string title)
        {
            return UserDialogs.Instance.AlertAsync(message, title);
        }

        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public async Task<string> ShowPromptAsync(string message, string title, string ok = "OK", string cancel = "Cancel")
        {
            var result = await UserDialogs.Instance.PromptAsync(message, title, okText: ok, cancelText: cancel);
            return result.Text;
        }
    }
}
