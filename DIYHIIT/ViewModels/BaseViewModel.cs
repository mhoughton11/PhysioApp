using System;
using DIYHIIT.Services.Dialog;
using MvvmCross.ViewModels;

namespace DIYHIIT.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private readonly IDialogService dialogService;

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public BaseViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
    }
}
