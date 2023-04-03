using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace imagegallery.Controls
{	
	public partial class GetCameraPermissions : PopupPage
    {	
		public GetCameraPermissions ()
		{
			InitializeComponent ();
		}

        async void CameraButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Permissions.RequestAsync<Permissions.Camera>();
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}

