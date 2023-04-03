using System;
using imagegallery.Controls;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;
using imagegallery.Models;
using imagegallery.Services;
using imagegallery.Interfaces;

namespace imagegallery.ViewModels
{
    public class DetailedImagePageViewModel : ViewModelBase
    {
        protected readonly IImageService ImageService; 
        public DelegateCommand LibraryCommand { get; set; }
        public DelegateCommand CameraRollCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        private ImageSource mainImage;
        private FileResult photo;
        private string description;
        private string annotations;
        private bool hasImageData = false;

        public DetailedImagePageViewModel(INavigationService navigationService, IImageService imageService, IPageDialogService pageDialogService, IDeviceService deviceService, IEventAggregator eventAggregator) : base(navigationService, pageDialogService, deviceService, eventAggregator)
        {
            this.ImageService = imageService;
            this.LibraryCommand = new DelegateCommand(() =>
            {
                this.ExecuteAsyncTask(this.LibraryAction).ConfigureAwait(false);
            });
            this.CameraRollCommand = new DelegateCommand(() =>
            {
                this.ExecuteAsyncTask(this.CameraRollAction).ConfigureAwait(false);
            });
            this.SaveCommand = new DelegateCommand(() =>
            {
                this.ExecuteAsyncTask(this.SaveAction).ConfigureAwait(false);
            });
        }

        private async Task SaveAction()
        {
            if (Photo != null)
            {
                var request = new ImageModel();
                request.FileName = Photo.FileName;
                request.Content = await getBytes(Photo);
                request.Annotations = Annotations;
                request.Description = Description;
                request.DateCreated = DateTime.Now;
                ImageService.SaveItemToDbAsync(request);
                await navigationService.NavigateAsync("GalleryPage");
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Error", "there is no data to save", "OK");
            }
        }

        private async Task CameraRollAction()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                await PopupNavigation.Instance.PushAsync(new GetCameraPermissions());
            }

            if (status == PermissionStatus.Granted)
            {
                Photo = await MediaPicker.CapturePhotoAsync();
                await loadimage(photo);
            }
        }

        private async Task LibraryAction()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Photos>();
            if (status != PermissionStatus.Granted)
            {
                await PopupNavigation.Instance.PushAsync(new GetLibraryPermissions());
            }

            var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Photos>();
            if (cameraStatus == PermissionStatus.Granted)
            {
                Photo = await MediaPicker.PickPhotoAsync();
                await loadimage(photo);
            }
        }

        private async Task loadimage(FileResult photo)
        {
            if (photo != null)
            {
                byte[] bytes = await getBytes(photo);
                MainImage = ImageSource.FromStream(() => new MemoryStream(bytes));
            }
        }

        private static async Task<byte[]> getBytes(FileResult photo)
        {
            byte[] bytes;
            using (var stream = await photo.OpenReadAsync())
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                bytes = ms.ToArray();
            }

            return bytes;
        }

        public FileResult Photo
        {
            get => photo;
            set
            {
                photo = value;
            }
        }

        public ImageSource MainImage
        {
            get => mainImage;
            set
            {
                mainImage = value;
                if (MainImage != null)
                {
                    HasImageData = true;
                }
                else
                {
                    HasImageData = true;
                }
                OnPropertyChanged("MainImage");
            }
        }

        public string Annotations
        {
            get => annotations;
            set
            {
                annotations = value;
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
            }
        }
        public bool HasImageData
        {
            get => hasImageData;
            set
            {
                hasImageData = value;
                OnPropertyChanged("HasImageData");
            }
        }
    }
}