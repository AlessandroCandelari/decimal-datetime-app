using System;
using Xamarin.Forms;

namespace BoxViewClock
{
    class BoxViewClockPage : ContentPage
    {
        static readonly Color dateColor = Color.White;

        private Label dayLabel;
        private Label dayNameLabel;
        private ClockView clockView;

        public BoxViewClockPage()
        {
            RepublicanDatetime repTime = new RepublicanDatetime(DateTime.Now);
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            
            this.BackgroundImage = $"m{repTime.RepublicanMonth.ToString("00")}.jpg";

            dayLabel = new Label();
            dayLabel.FontSize = 30;
            dayLabel.TextColor = dateColor;
            dayLabel.HorizontalTextAlignment = TextAlignment.Center;
            dayLabel.Margin = 10;
            dayLabel.Text = repTime.ToString("dd-MMMM-yyy");
            absoluteLayout.Children.Add(dayLabel);

            dayNameLabel = new Label();
            dayNameLabel.FontSize = 30;
            dayNameLabel.TextColor = dateColor;
            dayNameLabel.HorizontalTextAlignment = TextAlignment.Center;
            dayNameLabel.Margin = 10;
            dayNameLabel.Text = repTime.DayName;
            absoluteLayout.Children.Add(dayNameLabel);
            AbsoluteLayout.SetLayoutBounds(dayNameLabel, new Rectangle(0, 50, Width, 50));

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
