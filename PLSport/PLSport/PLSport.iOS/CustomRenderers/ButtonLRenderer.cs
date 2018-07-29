using PLSport.CustomControls;
using PLSport.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ButtonL), typeof(ButtonLRenderer))]
namespace PLSport.iOS.CustomRenderers
{
    class ButtonLRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                
            }
        }
    }
}