using EGAZT.CustomControl;
using Xamarin.Forms.Internals;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AutoFillOtpEntry), typeof(AutoFillOtpEntryRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Preserve(AllMembers = true)]
    public class AutoFillOtpEntryRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.TextContentType = UITextContentType.OneTimeCode;
            }
        }
    }
}
