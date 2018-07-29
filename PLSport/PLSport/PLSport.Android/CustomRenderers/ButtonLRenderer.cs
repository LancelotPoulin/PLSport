using PLSport.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using PLSport.Android.CustomRenderers;

[assembly: ExportRenderer(typeof(ButtonL), typeof(ButtonLRenderer))]
namespace PLSport.Android.CustomRenderers
{
    class ButtonLRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gradientDrawable = new GradientDrawable();
                gradientDrawable.SetColor(Element.BorderColor.ToAndroid(Color.FromHex("#4169E1")));
                gradientDrawable.SetCornerRadius(15);
                Control.SetBackground(gradientDrawable);
            }
        }
    }
}