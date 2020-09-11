using System;
using System.Threading.Tasks;

namespace DIYHIIT.Contracts.Services.General
{
    public interface IDialogService
    {
        void Popup(string message);
        void ShowLoading(string message = "Loading...");
        void HideLoading();
        Task ShowAlertAsync(string message);
        Task ShowAlertAsync(string message, string title);
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task<bool> ShowConfirmAsync(string message, string title, string ok = "OK", string cancel = "Cancel");
        Task<string> ShowPromptAsync(string message, string title, string ok = "OK", string cancel = "Cancel");
    }
}
