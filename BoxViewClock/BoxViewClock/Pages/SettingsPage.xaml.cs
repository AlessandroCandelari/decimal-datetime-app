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
            refresh();
        }
        void Short_Added(object sender, EventArgs e)
        {
            ((Entry)sender).Text = FormatSettings.ShortFormat;
        }
        void Long_Changed(object sender, EventArgs e)
        {
            var text = ((Editor)sender).Text;
            FormatSettings.LongFormat = text;
        }
        void Long_Added(object sender, EventArgs e)
        {
            ((Editor)sender).Text = FormatSettings.LongFormat;
        }
    }
}
