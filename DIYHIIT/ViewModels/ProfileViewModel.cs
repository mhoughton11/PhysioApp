using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;
        Random random;

        public string TextLabel { get; set; }

        public ProfileViewModel(INavigation navigationService, IDialogService dialogService,
            IUserDataService userDataService) : base(navigationService, dialogService)
        {
            TextLabel = "Profile";
            _userDataService = userDataService;
            random = new Random();
        }

        public override Task InitializeAsync(object data)
        {
            random = new Random();
            TextLabel = "Profile" + random.Next(0xFF);

            return base.InitializeAsync(data);
        }

        public ICommand TestPostCommand => new Command(OnTestPostCommand);
        public ICommand TestGetCommand => new Command(OnTestGetCommand);

        private async void OnTestGetCommand()
        {
            var users = await _userDataService.GetUsers();

            if (users != null)
            {
                Debug.WriteLine("Users:");
                foreach (var user in users)
                {
                    Debug.WriteLine(user.Username);
                }
            }
            else
            {
                Debug.WriteLine("Unable to fetch users.");
            }
        }

        private async void OnTestPostCommand()
        {
            User user = new User()
            {
                Username = "TestUser" + random.Next(0xFF).ToString()
            };

            await _userDataService.SaveUser(user);
        }
    }
}
