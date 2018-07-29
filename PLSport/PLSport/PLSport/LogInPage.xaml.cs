using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;


// Entry page of the application, user login in or open sign in page

namespace PLSport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        private bool IsAlreadyClicked = false; // Anti Spam Button bug
        private bool IsBGRunning = true; // For stopping/replay background (performance)

        public LogInPage()
        {
            AutoLogIn();

            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, EventArgs e) { IsBGRunning = true; } // ReRun BG Video when disconnect

        // Try connecting with saved settings at launching
        private async void AutoLogIn()
        {
            string[] Content = await Data.Settings.Read(); // Read Settings file to get all entries
            if (Content.Length == 2) // If there are entries
            {
                await LogIn(Content[0], Content[1]);
            }
        }


        // Try connect webservice and ask for user infos : write new settings file + push event page if login in success
        private async Task LogIn(string Mail, string Password)
        {
            try
            {
                HttpResponseMessage Response = await App.Client.GetAsync($"http://{App.ServerAddress}/PLSport/WebService/LogIn.php?Mail={Mail}&Password={Password}");
                if (Response.IsSuccessStatusCode)
                {
                    string JsonData = await Response.Content.ReadAsStringAsync();
                    var Users = JsonConvert.DeserializeObject<IEnumerable<Data.Model.User>>(JsonData);
                    foreach (var User in Users) // There is only one user selected
                    {
                        IsBGRunning = false; // Stop BG Video for performance
                        await Data.Settings.Write(Mail, Password); // Write new entries in Settings for futur (auto login)
                        await Navigation.PushAsync(new TabbedMainPage(User)); //  Push new page
                        MailEntry.Text = ""; PasswordEntry.Text = "";
                    }
                }
                else { DisplayAlert("Erreur", "Mail ou mot de passe incorrect, veuillez réessayer.", "OK"); }
            }
            catch { DisplayAlert("Erreur", "Aucune réponse du serveur. Veuillez réessayer ultérieurement.", "OK"); }
        }


        // Log in button click event
        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            if (!IsAlreadyClicked)
            {
                IsAlreadyClicked = true;
                Animations.ButtonFadeAnimation(sender as Button);
                await LogIn(MailEntry.Text, PasswordEntry.Text);
            }
            IsAlreadyClicked = false;
        }


        // Go to sign in page
        private async void OpenSignInButton_Clicked(object sender, EventArgs e)
        {
            Animations.ButtonFadeAnimation(sender as Button);
            await Navigation.PushModalAsync(new SignInPage(), true);
        }
    }
}