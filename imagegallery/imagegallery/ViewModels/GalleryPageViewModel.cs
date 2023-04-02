using System;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace imagegallery.ViewModels
{
    public class GalleryPageViewModel : ViewModelBase
    {
        public GalleryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService, IEventAggregator eventAggregator) : base(navigationService, pageDialogService, deviceService, eventAggregator)
        {
        }
    }
}