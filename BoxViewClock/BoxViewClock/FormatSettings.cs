using System;
using Xamarin.Forms;

namespace BoxViewClock
{
    internal static class FormatSettings
    {
        private static String shortFormat;
        private static String longFormat;
        private const string shortFormatKey = "shortFormat";
        private const string longFormatKey = "longFormat";

        public static String ShortFormat
        {
            get
            {
                if (String.IsNullOrEmpty(shortFormat))
                {
                    if (Application.Current.Properties.ContainsKey(shortFormatKey))
                    {
                        shortFormat = Application.Current.Properties[shortFormatKey].ToString();
                    }else
                    {
                        ShortFormat = "d MMMM yyy";
                    }
                }
                
                return shortFormat;
            }
            set
            {
                shortFormat = value;
                if (Application.Current.Properties.ContainsKey(shortFormatKey))
                {
                    Application.Current.Properties[shortFormatKey] = value;
                }else
                {
                    Application.Current.Properties.Add(shortFormatKey, value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }
        public static String LongFormat
        {
            get
            {
                if (String.IsNullOrEmpty(longFormat))
                {
                    if (Application.Current.Properties.ContainsKey(longFormatKey))
                    {
                        longFormat = Application.Current.Properties[longFormatKey].ToString();
                    }
                    else
                    {
                        LongFormat = "ddd d MMMM MMM M yyy, hh:mm:ss";
                    }
                }
                return longFormat;
            }
            set
            {
                longFormat = value;
                if (Application.Current.Properties.ContainsKey(longFormatKey))
                {
                    Application.Current.Properties[longFormatKey] = value;
                }
                else
                {
                    Application.Current.Properties.Add(longFormatKey, value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }
    }
}
