using System.Threading.Tasks;

namespace DIYHIIT.Contracts.Services.General
{
    public interface IDialogService
    {
        void Popup(string message);
        Task ShowAlertAsync(string message);
        Task ShowAlertAsync(string message, string title);
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task<string> ShowPromptAsync(string message, string title, string ok = "OK", string cancel = "Cancel");
    }
}
