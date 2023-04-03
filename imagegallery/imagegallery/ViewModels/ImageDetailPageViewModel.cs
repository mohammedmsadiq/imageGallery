using System;
using System.Threading.Tasks;
using imagegallery.Interfaces;
using imagegallery.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace imagegallery.ViewModels
{
    public class ImageDetailPageViewModel : ViewModelBase
    {
        private ImageModel imageItem;
        private string annotations;
        private string description;
        private string imageSource;

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        protected readonly IImageService imageService;


        public ImageDetailPageViewModel(INavigationService navigationService, IImageService imageService, IPageDialogService pageDialogService, IDeviceService deviceService, IEventAggregator eventAggregator) : base(navigationService, pageDialogService, deviceService, eventAggregator)
        {
            this.imageService = imageService;
            this.DeleteCommand = new DelegateCommand(() =>
            {
                this.ExecuteAsyncTask(this.DeleteExcute).ConfigureAwait(false);
            });

            this.SaveCommand = new DelegateCommand(() =>
            {
                this.ExecuteAsyncTask(this.SaveExcute).ConfigureAwait(false);
            });
        }

        private async Task SaveExcute()
        {
            ImageItem.DateCreated = DateTime.Now;
            imageService.UpdateItemToDbAsync(ImageItem);
            await navigationService.NavigateAsync("GalleryPage");
        }

        private async Task DeleteExcute()
        {
            imageService.DeleteItemToDbAsync(ImageItem.Id);
            await navigationService.NavigateAsync("GalleryPage");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("item") && parameters != null)
            {
                ImageItem = parameters["item"] as ImageModel;
            }
            else
            {
                return;
            }
        }

        public ImageModel ImageItem
        {
            get
            {
                return this.imageItem;
            }
            set
            {
                this.SetProperty(ref this.imageItem, value);
                OnPropertyChanged(nameof(ImageItem));
            }
        }
    }
}