using Acr.UserDialogs;
using DIYHIIT.Contracts.Services.General;
using System;
using System.Threading.Tasks;

namespace DIYHIIT.Services.General.Dialog
{
    public class DialogService : IDialogService
    {
        public void Popup(string message)
        {
            UserDialogs.Instance.Toast(message);
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

        public Task<bool> ShowConfirmAsync(string title, string message, string ok = "OK", string cancel = "Cancel")
        {
            return UserDialogs.Instance.ConfirmAsync(message, title: title, okText: ok, cancelText: cancel);
        }

        public void ShowLoading(string message = "Loading...")
        {
            UserDialogs.Instance.ShowLoading(message);
        }

        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }

        public async Task<string> ShowPromptAsync(string message, string title, string ok = "OK", string cancel = "Cancel")
        {
            var result = await UserDialogs.Instance.PromptAsync(message, title, okText: ok, cancelText: cancel);
            return result.Text;
        }
    }
}
