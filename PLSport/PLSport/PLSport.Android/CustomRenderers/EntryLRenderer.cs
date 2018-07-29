using PLSport.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using PLSport.Android.CustomRenderers;

[assembly: ExportRenderer(typeof(EntryL), typeof(EntryLRenderer))]
namespace PLSport.Android.CustomRenderers
{
    class EntryLRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gradientDrawable = new GradientDrawable();
                gradientDrawable.SetColor(Element.BackgroundColor.ToAndroid(Color.White));
                gradientDrawable.SetCornerRadius(15);
                Control.SetBackground(gradientDrawable);
                Control.SetPadding(185, 60, 50, 50);
            }
        }
        
    }
}