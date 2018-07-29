using System.Net.Http;

using Xamarin.Forms;

// POULIN Lancelot - Last modification on 23/01/2018

namespace PLSport
{
    public partial class App : Application
    {
        public const string ServerAddress = "172.20.10.2";
        public static HttpClient Client = new HttpClient();

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LogInPage());
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
