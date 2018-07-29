using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Sign in page opened from log in page

namespace PLSport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();

            // Complete pickers
            HeightPicker.ItemsSource = Enumerable.Range(120, 100).ToList();
            HeightPicker.SelectedIndex = 50;
            WeightPicker.ItemsSource = Enumerable.Range(30, 100).ToList();
            WeightPicker.SelectedIndex = 50;
            GenderPicker.ItemsSource = new List<string>() { "Femme", "Homme" };
            GenderPicker.SelectedIndex = 0;
        }

        // Check infos and try sign in
        private async Task SignInButton_ClickedAsync(object sender, EventArgs e)
        {
            Animations.ButtonFadeAnimation(sender as Button);

            var Content = new StringContent(JsonConvert.SerializeObject(new Data.Model.User()
            {
                Name = NameEntry.Text,
                Surname = SurnameEntry.Text,
                Mail = MailEntry.Text,
                Password = PasswordEntry.Text,
                Gender = GenderPicker.SelectedItem.ToString(),
                Birthday = BirthdayPicker.Date,
                Height = Convert.ToInt32(HeightPicker.SelectedItem),
                Weight = Convert.ToInt32(WeightPicker.SelectedItem)
            }), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage Response = await App.Client.PostAsync($"http://{App.ServerAddress}/PLSport/WebService/SignIn.php", Content);
                if (Response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Bienvenue", "Votre compte a bien été créé, vous pouvez désormais vous connecter.", "OK");
                    await Navigation.PopModalAsync(true);
                }
                else { DisplayAlert("Erreur", "Mail déjà utilisé par un autre utilisateur.", "OK"); }
            }
            catch { DisplayAlert("Erreur", "Aucune réponse du serveur. Veuillez réessayer ultérieurement.", "OK"); }
        }
    }
}