using System;
using System.ComponentModel;
using imagegallery.Animations;
using Xamarin.Forms;

namespace imagegallery.Controls
{
    [DesignTimeVisible(true)]
    public class PillButtonControl : Button
    {
        public PillButtonControl() : base()
        {
            SizeChanged += RoundedButton_SizeChanged;
            Pressed += RoundedButton_Pressed;
            Released += RoundedButton_Released;
            Clicked += RoundedButton_Clicked;
            this.Focused += Handle_Focused;
            this.Unfocused += Handle_Unfocused;
            this.BackgroundColor = Color.FromHex("#F0AA78");
            this.TextColor = Color.White;
            this.FontFamily = "Light";
            this.WidthRequest = 200;
            this.VerticalOptions = LayoutOptions.Center;
        }

        async void Handle_Unfocused(object sender, FocusEventArgs e)
        {
            await this.AnimateReleasedAsync();
        }


        async void Handle_Focused(object sender, FocusEventArgs e)
        {
            await this.AnimatePressedAsync();
        }


        async void RoundedButton_Clicked(object sender, EventArgs e)
        {
            await this.AnimatePressedAsync();
            await this.AnimateReleasedAsync();
        }

        async void RoundedButton_Pressed(object sender, EventArgs e)
        {
            await this.AnimatePressedAsync();
        }

        async void RoundedButton_Released(object sender, EventArgs e)
        {
            await this.AnimateReleasedAsync();
        }

        private void RoundedButton_SizeChanged(object sender, System.EventArgs e)
        {
            CornerRadius = (int)Math.Round(Height / 2.0, 0);
        }
    }
}