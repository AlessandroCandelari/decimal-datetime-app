using System;
using Xamarin.Forms;

namespace BoxViewClock
{
    class BoxViewClockPage : ContentPage
    {
        static readonly Color dateColor = Color.White;
        
        private Label dayNameLabel;
        private Button dayButton;
        private ClockView clockView;

        public BoxViewClockPage()
        {
            RepublicanDatetime repTime = new RepublicanDatetime(DateTime.Now);
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            
            this.BackgroundImage = $"m{repTime.RepublicanMonth.ToString("00")}.jpg";
            
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

            dayButton = new Button();
            dayButton.FontSize = 30;
            dayButton.TextColor = dateColor;
            dayButton.BackgroundColor = Color.Transparent;
            dayButton.BorderColor = Color.Transparent;
            dayButton.Margin = 10;
            dayButton.Text = repTime.ToString("d MMMM yyy");
            absoluteLayout.Children.Add(dayButton);
            dayButton.Clicked += DayButton_Clicked;
        }

        private void DayButton_Clicked(object sender, EventArgs e)
        {
            RepublicanDatetime repTime = new RepublicanDatetime(DateTime.Now);
            DisplayAlert("Data estesa", repTime.ToString("ddd d MMMM MMM M yyy, hh:mm:ss"), "ok");
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
                dayButton.Text = repTime.ToString("d MMMM yyy");
                dayNameLabel.Text = repTime.DayName;
                if (repTime.RepublicanDay.Equals(1))
                {
                    this.BackgroundImage = $"m{repTime.RepublicanMonth.ToString("00")}.jpg";
                }
            }
            return clockView.OnTimerTick();
        }
    }
}
