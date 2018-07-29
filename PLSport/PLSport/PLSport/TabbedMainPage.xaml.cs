using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Microcharts;

using PLSport.Data.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

// Main page of the application, 3 tabs: one with subscribed seances and histories (slide left), 
// one with all events to come next, and last one with connected user profil informations/charts with.
// User can add weight, subscribe and unsubscibe from event, add new event (coach only), disconnect...

namespace PLSport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedMainPage
    {
        private User ConnectedUser;
        // Different Lists used in ListViews
        private List<Event> Events;
        private List<Event> History;
        private List<Event> MySeances;

        public TabbedMainPage(User _ConnectedUser)
        {
            InitializeComponent();

            ConnectedUser = _ConnectedUser;
            MySeancePage.SelectedItem = SubscribedSeancePage; // Select 2nd page of carousel

            Events = new List<Event>();
            History = new List<Event>();
            MySeances = new List<Event>();

            FillProfil(); // Fill profil infos at start up
        }


        protected override bool OnBackButtonPressed() { return true; } // Disable Android Physical Back Button


        #region Profil Data

        // Draw the history chart in profil page: count the number of seance done for the last 5 months, write the all time total seance
        private void DrawHistoryChart()
        {
            var HistoryChartEntries = new List<Microcharts.Entry>();
            int Max = 0, TotalSeance = 0;

            for (var i = 4; i >= 0; i--)
            {
                int NumberOfSeance = 0;

                var Month = DateTime.Now.AddMonths(-i).ToString("MMMM");

                foreach (var Event in History)
                {
                    if (Event.Date.ToString("MMMM") == Month)
                        NumberOfSeance++;
                }

                Max = Math.Max(Max, NumberOfSeance);
                TotalSeance += NumberOfSeance;

                HistoryChartEntries.Add(new Microcharts.Entry(NumberOfSeance)
                {
                    Label = Month,
                    ValueLabel = NumberOfSeance.ToString(),
                    Color = SKColor.Parse("#FFFFFF"),
                    TextColor = SKColor.Parse("#FFFFFF")
                });
            }

            var HistoryChart = new BarChart()
            {
                Entries = HistoryChartEntries,
                BackgroundColor = SKColor.Parse("#4169E1"),
                LabelColor = SKColor.Parse("#FFFFFF"),
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                IsAnimated = true,
                AnimationDuration = TimeSpan.FromMilliseconds(2000),
                PointMode = PointMode.Circle,
                LabelTextSize = 40,
                PointSize = 23,
                MinValue = 0,
                MaxValue = Max
            };

            HistoryChartView.Chart = HistoryChart;
            SeanceNumberLabel.Text = $"Total: {TotalSeance}";
        }


        // Fill user informations TableView profil page
        private void FillProfil()
        {
            NameSurnameTextCell.Detail = $"{ConnectedUser.Name} {ConnectedUser.Surname}";
            GenderTextCell.Detail = ConnectedUser.Gender;
            int Age = DateTime.Now.Year - ConnectedUser.Birthday.Year;
            if (DateTime.Now < ConnectedUser.Birthday.AddYears(Age)) Age--;
            YearsOldTextCell.Detail = $"{Age} ans";
            HeightTextCell.Detail = $"{ConnectedUser.Height} cm";
            if (ConnectedUser.Status_ID == 2) { StatusTextCell.Detail = "Coach"; }
            else { StatusTextCell.Detail = "Adhérent"; }
            EmailTextCell.Detail = ConnectedUser.Mail;
            int PasswordLength = ConnectedUser.Password.Length;
            for (int i = 0; i < ConnectedUser.Password.Length; i++) { PasswordTextCell.Detail += "•"; }
        }


        // Get user weights from database and draw a chart
        private async void GetWeights()
        {
            var WeightEntries = new List<Microcharts.Entry>();
            int MinWeight = 1000, MaxWeight = 0;

            try
            {
                HttpResponseMessage Response = await App.Client.GetAsync($"http://{App.ServerAddress}/PLSport/WebService/GetWeights.php?UserID={ConnectedUser.ID}");
                if (Response.IsSuccessStatusCode)
                {
                    string JsonData = await Response.Content.ReadAsStringAsync();
                    var Weights = JsonConvert.DeserializeObject<List<Weight>>(JsonData);
                    for (var i = Weights.Count - 1; i > Weights.Count - 8 && i >= 0; i--)
                    {
                        WeightEntries.Add(new Microcharts.Entry(Weights[i].Value)
                        {
                            Label = Weights[i].Date.ToString("dd/MM"),
                            ValueLabel = Weights[i].Value.ToString(),
                            Color = SKColor.Parse("#FFFFFF"),
                            TextColor = SKColor.Parse("#FFFFFF")
                        });
                        MinWeight = Math.Min(MinWeight, Weights[i].Value);
                        MaxWeight = Math.Max(MaxWeight, Weights[i].Value);
                    }
                    WeightEntries.Reverse();
                }
                else { DisplayAlert("Erreur", "Impossible de charger les poids, veuillez réessayer ultérieurement.", "OK"); }
            }
            catch { DisplayAlert("Erreur", "Aucune réponse du serveur. Veuillez réessayer ultérieurement.", "OK"); }

            var WeightChart = new LineChart()
            {
                Entries = WeightEntries.ToArray(),
                BackgroundColor = SKColor.Parse("#4169E1"),
                LabelColor = SKColor.Parse("#FFFFFF"),
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                IsAnimated = true,
                AnimationDuration = TimeSpan.FromMilliseconds(2000),
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
                LineSize = 5,
                LabelTextSize = 40,
                PointSize = 23,
                MinValue = MinWeight - 3,
                MaxValue = MaxWeight + 3
            };

            WeightChartView.Chart = WeightChart;
        }


        // Add new weight to database and refresh chart
        private async void AddWeightButton_ClickedAsync(object sender, EventArgs e)
        {
            Animations.ButtonFadeAnimation(sender as Button);

            var Content = new StringContent(JsonConvert.SerializeObject(new Data.Model.Weight()
            {
                Value = Convert.ToInt32(WeightEntry.Text),
                User_ID = ConnectedUser.ID
            }), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage Response = await App.Client.PostAsync($"http://{App.ServerAddress}/PLSport/WebService/PostWeight.php", Content);
                if (Response.IsSuccessStatusCode)
                {
                    WeightEntry.Text = "";
                    GetWeights();
                }
                else { DisplayAlert("Erreur", "Impossible d'ajouté un nouveau poids, veuillez réessayer ultérieurement.", "OK"); }
            }
            catch { DisplayAlert("Erreur", "Aucune réponse du serveur. Veuillez réessayer ultérieurement.", "OK"); }
        }

        #endregion


        #region Event Data

        // Get all data used in different listviews, and fill all of them. Method called on page appearing and listview refreshing
        private async void GetData()
        {
            await GetEvent();
            FillHistoryList();
            FillMySeanceList();
            FillEventList();
        }
        

        // First we send http request for all event to come and we sort the list with date, second we get subscribed user seance and sort the divide history/event to come
        private async Task GetEvent()
        {
            try
            {
                HttpResponseMessage Response = await App.Client.GetAsync($"http://{App.ServerAddress}/PLSport/WebService/GetEvent.php");
                if (Response.IsSuccessStatusCode)
                {
                    string JsonData = await Response.Content.ReadAsStringAsync();
                    if (JsonData == "No result found") return;
                    Events = JsonConvert.DeserializeObject<List<Event>>(JsonData);
                    Events.Sort((x, y) => x.Date.CompareTo(y.Date));
                }
                else { DisplayAlert("Erreur", "Impossible de charger les évenements, veuillez réessayer ultérieurement.", "OK"); }
            }
            catch { DisplayAlert("Erreur", "Aucune réponse du serveur. Veuillez réessayer ultérieurement. [GE]", "OK"); }

            try
            {
                HttpResponseMessage Response = await App.Client.GetAsync($"http://{App.ServerAddress}/PLSport/WebService/GetSubscribedEvent.php?UserID={ConnectedUser.ID}");
                if (Response.IsSuccessStatusCode)
                {
                    string JsonData = await Response.Content.ReadAsStringAsync();
                    if (JsonData == "No result found") return;
                    MySeances = JsonConvert.DeserializeObject<List<Event>>(JsonData);
                    History.Clear();
                    for(var i = 0; i < MySeances.Count; i++)
                    {
                        if (MySeances[i].Date < DateTime.Now)
                        {
                            History.Add(MySeances[i]);
                            MySeances.Remove(MySeances[i]);
                            i--;
                        }
                    }
                    MySeances.Sort((x, y) => x.Date.CompareTo(y.Date));
                    History.Sort((x, y) => x.Date.CompareTo(y.Date));
                }
                else { DisplayAlert("Erreur", "Impossible de charger les évenements, veuillez réessayer ultérieurement. [GSE]", "OK"); }
            }
            catch { DisplayAlert("Erreur", "Aucune réponse du serveur. Veuillez réessayer ultérieurement.", "OK"); }
        }


        // Fill the list with GetEvent() data: Event become EventCell and there are grouped in EventCellGroup for each day
        private void FillEventList()
        {
            var EventList = new List<EventCellGroup>();
            var CurrentEventCellGroup = new EventCellGroup() { Title = "Aucun événement" };
            foreach (var Event in Events)
            {
                if (Event.Date.Date.ToString("dddd d MMMM") != CurrentEventCellGroup.Title)
                {
                    if (CurrentEventCellGroup.Count != 0) { EventList.Add(CurrentEventCellGroup); }
                    CurrentEventCellGroup = new EventCellGroup() { Title = Event.Date.Date.ToString("dddd d MMMM") };
                }

                Color StatusColor; ImageSource ImageSource;
                if (MySeances.Exists(p => p.ID == Event.ID)) { StatusColor = Color.FromHex("#4169E1"); } else if (Event.Place == 0) { StatusColor = Color.Red; } else if (Event.Place <= 3) { StatusColor = Color.Orange; } else { StatusColor = Color.LimeGreen; }
                if (Event.Type_ID == 1) { ImageSource = "Musculation.png"; } else if (Event.Type_ID == 2) { ImageSource = "Fitness.png"; } else { ImageSource = "Exterieur.png"; }
                CurrentEventCellGroup.Add(new EventCell()
                {
                    ID = Event.ID,
                    Name = Event.Name,
                    Description = Event.Description,
                    Coach = Event.Surname,
                    Time = Event.Date.ToString("HH:mm"),
                    PlaceStatus = StatusColor,
                    TypePicture = ImageSource,
                    Owner_ID = Event.Owner_ID
                });
            }
            EventList.Add(CurrentEventCellGroup);
            EventListView.ItemsSource = EventList;
        }


        // Fill the list with GetEvent() data: MySeance Event become EventCell and there are grouped in EventCellGroup for each day
        private void FillMySeanceList()
        {
            var MySeanceList = new List<EventCellGroup>();
            var CurrentEventCellGroup = new EventCellGroup() { Title = "Aucune inscription" };
            foreach (var Event in MySeances)
            {
                if (Event.Date.Date.ToString("dddd d MMMM") != CurrentEventCellGroup.Title)
                {
                    if (CurrentEventCellGroup.Count != 0) { MySeanceList.Add(CurrentEventCellGroup); }
                    CurrentEventCellGroup = new EventCellGroup() { Title = Event.Date.Date.ToString("dddd d MMMM") };
                }

                ImageSource ImageSource;
                if (Event.Type_ID == 1) { ImageSource = "Musculation.png"; } else if (Event.Type_ID == 2) { ImageSource = "Fitness.png"; } else { ImageSource = "Exterieur.png"; }
                CurrentEventCellGroup.Add(new EventCell()
                {
                    ID = Event.ID,
                    Name = Event.Name,
                    Description = Event.Description,
                    Coach = Event.Surname,
                    Time = Event.Date.ToString("HH:mm"),
                    PlaceStatus = Color.FromHex("#4169E1"),
                    TypePicture = ImageSource,
                    Owner_ID = Event.Owner_ID
                });
            }
            MySeanceList.Add(CurrentEventCellGroup);
            MySeanceListView.ItemsSource = MySeanceList;
        }


        // Fill the list with GetEvent() data: History Event become EventCell and there are grouped in EventCellGroup for each day
        private void FillHistoryList()
        {
            var HistoryList = new List<EventCellGroup>();
            var CurrentEventCellGroup = new EventCellGroup() { Title = "Aucun historique" };
            foreach (var Event in History)
            {
                if (Event.Date.Date.ToString("dddd d MMMM") != CurrentEventCellGroup.Title)
                {
                    if (CurrentEventCellGroup.Count != 0) { HistoryList.Add(CurrentEventCellGroup); }
                    CurrentEventCellGroup = new EventCellGroup() { Title = Event.Date.Date.ToString("dddd d MMMM") };

                }

                ImageSource ImageSource;
                if (Event.Type_ID == 1) { ImageSource = "Musculation.png"; } else if (Event.Type_ID == 2) { ImageSource = "Fitness.png"; } else { ImageSource = "Exterieur.png"; }
                CurrentEventCellGroup.Add(new EventCell()
                {
                    ID = Event.ID,
                    Name = Event.Name,
                    Description = Event.Description,
                    Coach = Event.Surname,
                    Time = Event.Date.ToString("HH:mm"),
                    PlaceStatus = Color.Gray,
                    TypePicture = ImageSource,
                    Owner_ID = Event.Owner_ID
                });
            }
            HistoryList.Add(CurrentEventCellGroup);
            HistoryListView.ItemsSource = HistoryList;
        }

        #endregion


        #region Event Misc

        // When eventcell is selected, subscribe or unsubscirbe the event if there are places. If it's the coach event, ask for delete
        private async void ListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null || ((sender as ListView).SelectedItem as EventCell).PlaceStatus == Color.Red) { (sender as ListView).SelectedItem = null;  return; }
            if (((sender as ListView).SelectedItem as EventCell).Owner_ID == ConnectedUser.ID)
            {
                if (await DisplayAlert("Attention", "Voulez vous vraiment supprimer votre événement ?", "Oui", "Non"))
                {
                    try
                    {
                        HttpResponseMessage Response = await App.Client.GetAsync($"http://{App.ServerAddress}/PLSport/WebService/DeleteEvent.php?Event_ID={((sender as ListView).SelectedItem as EventCell).ID}");
                        if (Response.IsSuccessStatusCode)
                        {
                            // Success
                        }
                        else { DisplayAlert("Erreur", "Impossible de supprimer votre événement. [DE]", "OK"); }
                    }
                    catch { DisplayAlert("Erreur", "Aucune réponse du serveur. [DE]", "OK"); }
                }
            }
            else
            {
                string _Type = "Insert";
                if (MySeances.Exists(p => p.ID == ((sender as ListView).SelectedItem as EventCell).ID))
                    _Type = "Delete";
                var Content = new StringContent(JsonConvert.SerializeObject(new UserHasEvent()
                {
                    User_ID = ConnectedUser.ID,
                    Event_ID = ((sender as ListView).SelectedItem as EventCell).ID,
                    Type = _Type
                }), Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage Response = await App.Client.PostAsync($"http://{App.ServerAddress}/PLSport/WebService/PostUserHasEvent.php", Content);
                    if (Response.IsSuccessStatusCode)
                    {
                        // Succes
                    }
                    else { DisplayAlert("Erreur", "Impossible de s'inscrire ou se désinscire. [UHE]", "OK"); }
                }
                catch { DisplayAlert("Erreur", "Aucune réponse du serveur. [UHE]", "OK"); }
            }
            GetData();
        }


        private void HistoryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) { (sender as ListView).SelectedItem = null; } // Disable item selection


        // GetData when a listview is refreshed + fade animation
        private void ListView_Refreshing(object sender, EventArgs e)
        {
            (sender as ListView).EndRefresh();
            GetData();
            Animations.ListViewFadeAnimation(sender as ListView);
        }


        //  Draw charts and animations
        private void ProfilPage_Appearing(object sender, EventArgs e)
        {
            GetWeights();
            DrawHistoryChart();
            Animations.TableViewFadeAnimation(ProfilTableView);
        }


        // GetData + fade animation on listview
        private void MySeancePage_Appearing(object sender, EventArgs e)
        {
            GetData();
            Animations.ListViewFadeAnimation(MySeanceListView);
        }


        // GetData + fade animation on listview
        private void HistoryPage_Appearing(object sender, EventArgs e)
        {
            GetData();
            Animations.ListViewFadeAnimation(HistoryListView);
        } 


        // Add event toolbar to add event if coach + GetData fade animation on listview
        private void EventPage_Appearing(object sender, EventArgs e)
        {
            GetData();
            EventNavPage.ToolbarItems.Clear();
            if (ConnectedUser.Status_ID == 2)
                EventNavPage.ToolbarItems.Add(new ToolbarItem("AddEvent", "AddEvent.png", () => { ToolbarItem_Activated(); }));
            Animations.ListViewFadeAnimation(EventListView);
        }


        // Go to add event page
        private void ToolbarItem_Activated()
        {
            EventNavPage.ToolbarItems.Clear();
            EventNavPage.PushAsync(new EventDetailPage(ConnectedUser));
        }

#endregion


        // Disconnect current user, remove auto login informations and back to login page
        private async void DisconnectButton_ClickedAsync(object sender, EventArgs e)
        {
            await Data.Settings.Write(null, null);
            if (Device.RuntimePlatform == Device.Android)
                await DisplayAlert("Attention", "[Android Emulator] Vous serez déconnecté et il faudra redémarrer l'application.", "OK");
            await Navigation.PopAsync(true);
        }


        // Conditions
        private void ConditionsButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Conditions", "Application développé par Lancelot POULIN\nlancelotpoulin.com © 2018", "OK");
        }
    }
}