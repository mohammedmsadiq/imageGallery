using System;

using Xamarin.Forms;

namespace imagegallery.Views
{
	public class DetailedImagePage : ContentPage
	{
		public DetailedImagePage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


