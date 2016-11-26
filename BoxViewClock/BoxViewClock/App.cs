using System;
using BoxViewClock.Pages;
using Xamarin.Forms;

namespace BoxViewClock
{
	public class App : Application
    {
		public App ()
        {
            MainPage = new BoxViewClockPage();
        }
    }
}
