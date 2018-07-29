using CoreGraphics;
using PLSport.CustomControls;
using PLSport.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryL), typeof(EntryLRenderer))]
namespace PLSport.iOS.CustomRenderers
{
    class EntryLRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.LeftView = new UIView(new CGRect(0, 0, 47.5, 0));
                Control.LeftViewMode = UITextFieldViewMode.Always;
            }
        }
    }
}