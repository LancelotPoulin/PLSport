using Xamarin.Forms;

// Animations used in mutiple pages

namespace PLSport
{
    class Animations
    {
        // Fade animation on button click
        public static async void ButtonFadeAnimation(Button ButtonToFade)
        {
            if (Device.RuntimePlatform == "Android") // Animation already added to iOS
            {
                ButtonToFade.Opacity = 0;
                await ButtonToFade.FadeTo(1, 250);
            }
        }


        // Fade animation when ListView Appear
        public static async void ListViewFadeAnimation(ListView ListViewToFade)
        {
            ListViewToFade.Opacity = 0;
            await ListViewToFade.FadeTo(1, 250);
        }


        // Fade animation when TableView appear
        public static async void TableViewFadeAnimation(TableView TableViewToFade)
        {
            TableViewToFade.Opacity = 0;
            await TableViewToFade.FadeTo(1, 250);
        }
    }
}
