using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;

namespace imagegallery.Controls
{	
	public partial class GetLibraryPermissions : PopupPage
    {	
		public GetLibraryPermissions ()
		{
			InitializeComponent ();
		}

        async void LibraryButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Permissions.RequestAsync<Permissions.Photos>();
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}

