using System;
using Xamarin.Forms;

namespace BoxViewClock
{
    class BoxViewClockPage : ContentPage
    {
        static readonly Color dateColor = Color.White;
        private int timerPeriod = Convert.ToInt32(RepublicanDatetime.SECONDS_RATIO * 500);

        private Label dayNameLabel;
        private Label dayLabel;
        private Button infoButton;
        private ClockView clockView;
        private Image backgroundImage;

        public BoxViewClockPage()
        {
            RepublicanDatetime repTime = new RepublicanDatetime(DateTime.Now);
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();

            backgroundImage = new Image
            {
                Source = $"m{repTime.RepublicanMonth.ToString("00")}.jpg",
                Aspect = Aspect.AspectFill
            };
            absoluteLayout.Children.Add(backgroundImage);

            dayNameLabel = new Label();
            dayNameLabel.FontSize = 30;
            dayNameLabel.TextColor = dateColor;
            dayNameLabel.VerticalTextAlignment = TextAlignment.Center;
            dayNameLabel.HorizontalTextAlignment = TextAlignment.Center;
            dayNameLabel.Text = repTime.DayName;
            absoluteLayout.Children.Add(dayNameLabel);

            clockView = new ClockView();
            absoluteLayout.Children.Add(clockView);

            infoButton = new Button();
            infoButton.BackgroundColor = Color.Transparent;
            infoButton.BorderColor = Color.Transparent;
            infoButton.Image = "Icon.png";
            
            absoluteLayout.Children.Add(infoButton);
            infoButton.Clicked += DayButton_Clicked;

            dayLabel = new Label();
            dayLabel.FontSize = 30;
            dayLabel.TextColor = dateColor;
            dayLabel.VerticalTextAlignment = TextAlignment.Center;
            dayLabel.HorizontalTextAlignment = TextAlignment.Center;
            dayLabel.Text = repTime.ToString("d MMMM yyy");
            absoluteLayout.Children.Add(dayLabel);
            
            Content = absoluteLayout;

            Device.StartTimer(TimeSpan.FromMilliseconds(timerPeriod), OnTimerTick);
            SizeChanged += OnPageSizeChanged;
        }

        private void DayButton_Clicked(object sender, EventArgs e)
        {
            RepublicanDatetime repTime = new RepublicanDatetime(DateTime.Now);
            DisplayAlert("Data estesa", repTime.ToString("ddd d MMMM MMM M yyy, hh:mm:ss"), "ok");
        }

        private void OnPageSizeChanged(object sender, EventArgs args)
        {
            AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, Width, Height));
            if (Height > Width)
            {
                AbsoluteLayout.SetLayoutBounds(clockView, new Rectangle(0, 0, Width, Height));
                AbsoluteLayout.SetLayoutBounds(dayLabel, new Rectangle(0, 15, Width, 40));
                AbsoluteLayout.SetLayoutBounds(dayNameLabel, new Rectangle(0, 55, Width, 40));
                AbsoluteLayout.SetLayoutBounds(infoButton, new Rectangle(Width - 70, Height - 70, 70, 70));
            }else
            {
                AbsoluteLayout.SetLayoutBounds(clockView, new Rectangle(Width / 2, 0, Width / 2, Height));
                AbsoluteLayout.SetLayoutBounds(dayLabel, new Rectangle(0, 5, Width/2, 40));
                AbsoluteLayout.SetLayoutBounds(dayNameLabel, new Rectangle(0, 45, Width/2, 40));
                AbsoluteLayout.SetLayoutBounds(infoButton, new Rectangle(Width - 70, Height - 70, 70, 70));
            }
            this.clockView.OnPageSizeChanged(sender, args);
        }

        bool OnTimerTick()
        {
            // Set rotation angles for hour and minute hands.
            RepublicanDatetime repTime = new RepublicanDatetime(DateTime.Now);
            if(repTime.RepublicanHours.Equals(0) && repTime.RepublicanMinutes.Equals(0) && repTime.RepublicanSeconds.Equals(0))
            {
                dayLabel.Text = repTime.ToString("d MMMM yyy");
                dayNameLabel.Text = repTime.DayName;
                if (repTime.RepublicanDay.Equals(1))
                {
                    this.backgroundImage.Source = $"m{repTime.RepublicanMonth.ToString("00")}.jpg";
                }
            }
            return clockView.OnTimerTick();
        }
    }
}
