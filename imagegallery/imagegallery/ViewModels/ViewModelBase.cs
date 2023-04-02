using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Prism;
using Prism.AppModel;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace imagegallery.ViewModels
{
    public class ViewModelBase : BindableBase, IActiveAware, INavigationAware, IDestructible, IConfirmNavigation, IConfirmNavigationAsync, IApplicationLifecycleAware, IPageLifecycleAware, INotifyPropertyChanged
    {
        protected IDeviceService deviceService { get; private set; }
        protected INavigationService navigationService { get; private set; }
        protected IPageDialogService pageDialogService { get; private set; }
        private IEventAggregator eventAggregator;

        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService, IEventAggregator eventAggregator)
        {
            this.pageDialogService = pageDialogService;
            this.deviceService = deviceService;
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion NotifyPropertyChanged     

        #region ExecuteAsyncTask
        public bool IsBusy { get; set; }
        protected string ErrorMessageTitle;

        protected async Task ExecuteAction(Action action)
        {
            Device.BeginInvokeOnMainThread(() => { this.IsBusy = true; });

            try
            {
                action();
            }
            catch (Exception ex)
            {
                await this.ShowErrorMessage(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() => { this.IsBusy = false; });
            }
        }

        protected async Task ExecuteAsyncTask(Func<Task> actionDelegate)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.IsBusy = true;
            });

            try
            {
                await this.ExecuteAsyncTaskWithNoLoading(actionDelegate);
            }
            catch (Exception ex)
            {
                await this.ShowErrorMessage(ex);
            }
            finally
            {
                await Task.Delay(1500);
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.IsBusy = false;
                });
            }
        }

        protected async Task ExecuteAsyncTaskWithNoLoading(Func<Task> actionDelegate)
        {
            try
            {
                await actionDelegate();
            }
            catch (Exception ex)
            {
                await this.ShowErrorMessage(ex);
            }
        }

        protected async Task ShowErrorMessage(Exception ex)
        {
            Debug.WriteLine("--------->" + this.ErrorMessageTitle + "=======>" + ex.Message);

            //Dialog service, show error. 
            await this.pageDialogService.DisplayAlertAsync(this.ErrorMessageTitle, ex.Message, "OK");
        }
        #endregion ExecuteAsyncTask

        #region IActiveAware

        public bool IsActive { get; set; }

        public event EventHandler IsActiveChanged;

        private void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if (IsActive)
            {
                OnIsActive();
            }
            else
            {
                OnIsNotActive();
            }
        }

        protected virtual void OnIsActive() { }

        protected virtual void OnIsNotActive() { }

        #endregion IActiveAware

        #region INavigationAware

        public virtual void Initialize(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        #endregion INavigationAware

        #region IDestructible

        public virtual void Destroy() { }

        #endregion IDestructible

        #region IConfirmNavigation

        public virtual bool CanNavigate(INavigationParameters parameters) => true;

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters) =>
            Task.FromResult(CanNavigate(parameters));

        #endregion IConfirmNavigation

        #region IApplicationLifecycleAware

        public virtual void OnResume() { }

        public virtual void OnSleep() { }

        #endregion IApplicationLifecycleAware

        #region IPageLifecycleAware

        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }

        #endregion IPageLifecycleAware     
    }
}