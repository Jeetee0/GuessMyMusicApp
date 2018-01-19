using Xamarin.Forms;

using GuessMyMusic.MainPages;

namespace GuessMyMusic
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPages.MainPage();
            else
                MainPage = new NavigationPage(new MainPages.MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
