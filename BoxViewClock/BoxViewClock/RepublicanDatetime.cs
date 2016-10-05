using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxViewClock
{
    public class RepublicanDatetime
    {
        private const decimal SECONDS_RATIO = 0.864M;
        private const int REPUBLICAN_HOURS_IN_DAY = 10;
        private const int REPUBLICAN_MINUTES_IN_HOUR = 100;
        private const int REPUBLICAN_SECONDS_IN_MINUTE = 100;

        private DateTime FIRST_DATETIME = new DateTime(2016, 9, 22);
        private const int FIRST_YEAR = 225;
        private static List<Int32> bisestili = new List<int>();
        public DateTime datetime { get; private set; }
        private int totalRepublicanSecondsInDay = 0;
        private int totalRepublicanDays = 0;
        private int totalMilliSeconds = 0;
        private int DaysInYear(int year)
        {
            var result = 365;
            if (bisestili.Contains(year))
            {
                result = 366;
            }
            return result;
        }
        public RepublicanDatetime(DateTime datetime)
        {
            if (!bisestili.Any())
            {
                InitBisestili();
            }
            this.datetime = datetime;
            totalMilliSeconds = (((((datetime.Hour * 60) + datetime.Minute) * 60) + datetime.Second) * 1000) + datetime.Millisecond;
            totalRepublicanSecondsInDay = Decimal.ToInt32((totalMilliSeconds / SECONDS_RATIO) / 1000);
            totalRepublicanDays = (int)Math.Ceiling(datetime.Subtract(FIRST_DATETIME).TotalDays);
            this.InitDate();
        }
        private void InitDate()
        {
            int totalDays = totalRepublicanDays;
            int year = FIRST_YEAR;
            while (totalDays > this.DaysInYear(year))
            {
                totalDays = totalDays - this.DaysInYear(year);
                year++;
            }
            this.RepublicanYear = year;
            if (totalDays > 360)
            {
                this.RepublicanMonth = 13;
                this.RepublicanDay = totalDays - 360;
            }
            else
            {
                this.RepublicanMonth = ((totalDays - 1) / 30) + 1;
                this.RepublicanDay = totalDays % 30;
                if (RepublicanDay == 0) RepublicanDay = 30;
            }
        }
        private void InitBisestili()
        {
            bisestili.Add(226);
            bisestili.Add(230);
        }
        public decimal Milliseconds
        {
            get
            {
                var totalRepMilli = this.totalMilliSeconds / SECONDS_RATIO;
                var subtract = (((((this.RepublicanHours * REPUBLICAN_MINUTES_IN_HOUR) + this.RepublicanMinutes) * REPUBLICAN_SECONDS_IN_MINUTE) + this.RepublicanSeconds) * 1000);
                var mill = totalRepMilli - subtract;
                    //(this.totalMilliSeconds / SECONDS_RATIO) % (REPUBLICAN_SECONDS_IN_MINUTE * REPUBLICAN_MINUTES_IN_HOUR * REPUBLICAN_HOURS_IN_DAY * 1000);
                return mill;
            }
        }
        public int RepublicanSeconds
        {
            get
            {
                return this.totalRepublicanSecondsInDay % (REPUBLICAN_SECONDS_IN_MINUTE);
            }
        }
        public int RepublicanMinutes
        {
            get
            {
                var totalRepMinutes = this.totalRepublicanSecondsInDay / REPUBLICAN_SECONDS_IN_MINUTE;
                return totalRepMinutes % REPUBLICAN_MINUTES_IN_HOUR;
            }
        }
        public int RepublicanHours
        {
            get
            {
                return this.totalRepublicanSecondsInDay / (REPUBLICAN_MINUTES_IN_HOUR * REPUBLICAN_SECONDS_IN_MINUTE);
            }
        }
        public int RepublicanDay { get; private set; }
        public int RepublicanMonth { get; private set; }
        public int RepublicanYear { get; private set; }
        public override string ToString()
        {
            return $"{this.RepublicanDay.ToString("00")}-{this.RepublicanMonth.ToString("00")}-{this.RepublicanYear}";
        }
    }
}
