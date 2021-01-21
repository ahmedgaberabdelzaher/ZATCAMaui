using GAZT;
using GAZT.iOS.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(GAZTBorderlessEditor), typeof(GAZTBorderlessEditorRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    class GAZTBorderlessEditorRenderer : EditorRenderer
    {
        double fontSize;
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                this.Control.AutocapitalizationType = UIKit.UITextAutocapitalizationType.Sentences;
            }
        }
    }
}