using System;
using imagegallery.Helpers;
using imagegallery.Interfaces;
using imagegallery.Models;
using imagegallery.Services;
using imagegallery.ViewModels;
using imagegallery.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Lato-Thin.ttf", Alias = "Thin")]
[assembly: ExportFont("Lato-ThinItalic.ttf", Alias = "ThinItalic")]
[assembly: ExportFont("Lato-SemiboldItalic.ttf", Alias = "SemiboldItalic")]
[assembly: ExportFont("Lato-Semibold.ttf", Alias = "Semibold")]
[assembly: ExportFont("Lato-Regular.ttf", Alias = "Regular")]
[assembly: ExportFont("Lato-MediumItalic.ttf", Alias = "MediumItalic")]
[assembly: ExportFont("Lato-Medium.ttf", Alias = "Medium")]
[assembly: ExportFont("Lato-LightItalic.ttf", Alias = "LightItalic")]
[assembly: ExportFont("Lato-Light.ttf", Alias = "Light")]
[assembly: ExportFont("Lato-Italic.ttf", Alias = "Italic")]
[assembly: ExportFont("Lato-HeavyItalic.ttf", Alias = "HeavyItalic")]
[assembly: ExportFont("Lato-Heavy.ttf", Alias = "Heavy")]
[assembly: ExportFont("Lato-HairlineItalic.ttf", Alias = "HairlineItalic")]
[assembly: ExportFont("Lato-Hairline.ttf", Alias = "Hairline")]
[assembly: ExportFont("Lato-BoldItalic.ttf", Alias = "BoldItalic")]
[assembly: ExportFont("Lato-Bold.ttf", Alias = "Bold")]
[assembly: ExportFont("Lato-BlackItalic.ttf", Alias = "BlackItalic")]
[assembly: ExportFont("Lato-Black.ttf", Alias = "Black")]

namespace imagegallery
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            DBHelper.InitializeDatabase();
            NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<GalleryPage, GalleryPageViewModel>();
            containerRegistry.RegisterForNavigation<DetailedImagePage, DetailedImagePageViewModel>();
            containerRegistry.RegisterForNavigation<ImageDetailPage, ImageDetailPageViewModel>();

            containerRegistry.RegisterSingleton<IImageService, ImageService>();
            containerRegistry.RegisterSingleton<IDBService<ImageModel>, DBService<ImageModel>>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}