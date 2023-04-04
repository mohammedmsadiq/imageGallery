using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace imagegallery.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string passCode = "Password123";
        public DelegateCommand LoginCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService, IEventAggregator eventAggregator) : base(navigationService, pageDialogService, deviceService, eventAggregator)
        {
            this.LoginCommand = new DelegateCommand(() =>
            {
                this.ExecuteAsyncTask(this.LoginAction).ConfigureAwait(false);
            });
        }

        private async Task LoginAction()
        {
            if (LoginCode == passCode)
            {
                await navigationService.NavigateAsync("GalleryPage");
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Error", "Incorrect Passcode", "OK");
            }
        }

        public bool CanValidate
        {
            get
            {
                return !string.IsNullOrEmpty(this.LoginCode);
            }
        }

        private string loginCode;
        public string LoginCode
        {
            get { return this.loginCode; }
            set { this.SetProperty(ref this.loginCode, value); }
        }
    }
}


