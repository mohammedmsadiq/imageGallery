using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using imagegallery.Controls;
using imagegallery.Interfaces;
using imagegallery.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace imagegallery.ViewModels
{
    public class GalleryPageViewModel : ViewModelBase
    {
        protected readonly IImageService imageService;
        private ObservableCollection<ImageModel> imagesSource = new ObservableCollection<ImageModel>();
        public Command<object> ItemTapCommand { get; set; }

        public GalleryPageViewModel(INavigationService navigationService, IImageService imageService, IPageDialogService pageDialogService, IDeviceService deviceService, IEventAggregator eventAggregator) : base(navigationService, pageDialogService, deviceService, eventAggregator)
        {
            this.imageService = imageService;
            loadDataAsync().ConfigureAwait(false);
            this.ItemTapCommand = new Command<object>(ItemTapExcute);
        }

        private void ItemTapExcute(object obj)
        {
            var result = obj as ImageModel;
            var navigationParams = new NavigationParameters();
            navigationParams.Add("item", result);
            navigationService.NavigateAsync("ImageDetailPage", navigationParams);
        }

        public override void OnAppearing()
        {
            this.ExecuteAsyncTask(async () =>
            {
                await loadDataAsync();
            });
            base.OnAppearing();
        }

        private async Task loadDataAsync()
        {
            var result = imageService.GetAllItemsAsync();
            ImagesSource = new ObservableCollection<ImageModel>(result);
        }

        public ObservableCollection<ImageModel> ImagesSource
        {
            get => imagesSource;
            set => SetProperty(ref imagesSource, value);
        }
    }
}