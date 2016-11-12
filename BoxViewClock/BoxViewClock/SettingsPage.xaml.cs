using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BoxViewClock
{
    public partial class Settings : ContentPage
    {
        private Action refresh;

        public Settings()
        {
            InitializeComponent();
        }

        public Settings(Action refresh) : this()
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
