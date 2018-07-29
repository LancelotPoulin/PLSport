using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using PLSport.Data.Model;

// Add new event page

namespace PLSport
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailPage : ContentPage
	{
        User ConnectedUser;

		public EventDetailPage (User _ConnectedUser)
		{
			InitializeComponent ();

            ConnectedUser = _ConnectedUser;
            CoachEntry.Text = ConnectedUser.Surname;

            // Complete picker
            TypePicker.ItemsSource = new List<string>() { "Musculation", "Fitness", "Exterieur" };
            TypePicker.SelectedIndex = 0;
		}


        // Add new event, and subscribe the coach to the event
        private async void AddButton_ClickedAsync(object sender, EventArgs e)
        {
            Animations.ButtonFadeAnimation(sender as Button);
            if (String.IsNullOrWhiteSpace(NameEntry.Text))
            {
                DisplayAlert("Attention", "Vous n'avez pas donnez de nom a l'événement.", "OK");
            }
            else
            {
                var Content = new StringContent(JsonConvert.SerializeObject(new Event()
                {
                    Name = NameEntry.Text,
                    Description = DescriptionEntry.Text,
                    Date = DatePicker.Date.Add(TimePicker.Time),
                    Place = Convert.ToInt32(PlaceEntry.Text) + 1, // + 1 for the coach
                    Type_ID = TypePicker.SelectedIndex + 1,
                    Owner_ID = ConnectedUser.ID
                }), Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage Response = await App.Client.PostAsync($"http://{App.ServerAddress}/PLSport/WebService/PostEvent.php", Content);
                    if (Response.IsSuccessStatusCode)
                    {
                        var Content2 = new StringContent(JsonConvert.SerializeObject(new UserHasEvent()
                        {
                            User_ID = ConnectedUser.ID,
                            Event_ID = Convert.ToInt32((await Response.Content.ReadAsStringAsync()).Split('|')[1]),
                            Type = "Insert"
                        }), Encoding.UTF8, "application/json");

                        try
                        {
                            HttpResponseMessage Response2 = await App.Client.PostAsync($"http://{App.ServerAddress}/PLSport/WebService/PostUserHasEvent.php", Content2);
                            if (Response2.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Succès", "Ajout de l'événement effectué.", "OK");
                                await Navigation.PopAsync(true);
                            }
                            else { DisplayAlert("Erreur", "Impossible d'ajouter un événement. [UHE]", "OK"); }
                        }
                        catch { DisplayAlert("Erreur", "Aucune réponse du serveur. [UHE]", "OK"); }
                    }
                    else { DisplayAlert("Erreur", "Impossible d'ajouter un événement. [PE]", "OK"); }
                }
                catch { DisplayAlert("Erreur", "Aucune réponse du serveur. [PE]", "OK"); }
            }
        }
    }
}