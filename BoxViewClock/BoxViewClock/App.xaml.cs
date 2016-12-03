using BoxViewClock.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BoxViewClock
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.Android)
            {
                MainPage = new BoxViewClockPage(); // eventuale splash
            }
            else
            {
                MainPage = new BoxViewClockPage();
            }
        }
    }
}
