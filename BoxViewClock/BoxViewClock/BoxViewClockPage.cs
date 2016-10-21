using System;
using Xamarin.Forms;

namespace BoxViewClock
{
    class BoxViewClockPage : ContentPage
    {
        static readonly Color dateColor = Color.White;

        private Label dayLabel;
        private ClockView clockView;

        public BoxViewClockPage()
        {
            RepublicanDatetime repTime = new RepublicanDatetime(DateTime.Now);
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            
            this.BackgroundImage = $"m{repTime.RepublicanMonth.ToString("00")}.jpg";

            // create and add the date
            dayLabel = new Label();
            dayLabel.FontSize = 30;
            dayLabel.TextColor = dateColor;
            dayLabel.HorizontalTextAlignment = TextAlignment.Center;
            dayLabel.Margin = 10;
            dayLabel.Text = repTime.ToString();
            absoluteLayout.Children.Add(dayLabel);

            clockView = new ClockView();
            absoluteLayout.Children.Add(clockView);
            Content = absoluteLayout;

            // Attach a couple event handlers.
            Device.StartTimer(TimeSpan.FromMilliseconds(432), OnTimerTick);
            SizeChanged += OnPageSizeChanged;
        }

        void OnPageSizeChanged(object sender, EventArgs args)
        {
            AbsoluteLayout.SetLayoutBounds(clockView, new Rectangle(0, 0, Width, Height));
            this.clockView.OnPageSizeChanged(sender, args);
        }

        bool OnTimerTick()
        {
            // Set rotation angles for hour and minute hands.
            RepublicanDatetime repTime = new RepublicanDatetime(DateTime.Now);
            if(repTime.RepublicanHours.Equals(0) && repTime.RepublicanMinutes.Equals(0) && repTime.RepublicanSeconds.Equals(0))
            {
                dayLabel.Text = repTime.ToString();
                if (repTime.RepublicanDay.Equals(1))
                {
                    this.BackgroundImage = $"m{repTime.RepublicanMonth.ToString("00")}.jpg";
                }
            }
            return clockView.OnTimerTick();
        }
    }
}
