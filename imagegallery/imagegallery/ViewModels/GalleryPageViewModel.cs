using System;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace imagegallery.ViewModels
{
    public class GalleryPageViewModel : ViewModelBase
    {
        private string login;

        public GalleryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService, IEventAggregator eventAggregator) : base(navigationService, pageDialogService, deviceService, eventAggregator)
        {
            login = "this is binded";
        }


        public string Login
        {
            get { return this.login; }
            set { this.SetProperty(ref this.login, value); }
        }
    }
}