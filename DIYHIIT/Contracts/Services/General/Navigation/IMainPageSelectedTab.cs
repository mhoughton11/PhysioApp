using System;

namespace DIYHIIT.Contracts.Services.General.Navigation
{
    public interface IMainPageSelectedTab
    {
        int SelectedTab { get; set; }

        void SetSelectedTab(int tabIndex);
    }
}
