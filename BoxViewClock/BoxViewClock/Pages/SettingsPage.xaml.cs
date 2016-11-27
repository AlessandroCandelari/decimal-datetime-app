using System;

using Xamarin.Forms;

namespace BoxViewClock.Pages
{
    public partial class SettingsPage : ContentPage
    {
        private Action refresh;

        public SettingsPage()
        {
            InitializeComponent();
        }

        public SettingsPage(Action refresh) : this()
        {
            this.refresh = refresh;
        }

        void Short_Changed(object sender, EventArgs e)
        {
            var text = ((Entry)sender).Text;
            FormatSettings.ShortFormat = text;
        }
        void Short_Added(object sender, EventArgs e)
        {
            ((Entry)sender).Text = FormatSettings.ShortFormat;
        }

        void Long_Changed(object sender, EventArgs e)
        {
            var text = ((Entry)sender).Text;
            FormatSettings.LongFormat = text;
        }
        void Long_Added(object sender, EventArgs e)
        {
            ((Entry)sender).Text = FormatSettings.LongFormat;
        }

        async void DismissButton_Clicked(object sender, EventArgs args)
        {
            Short.Unfocus();
            Long.Unfocus();

            await Navigation.PopModalAsync();
        }
    }
}
