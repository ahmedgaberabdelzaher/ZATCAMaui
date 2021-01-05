using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinFormsSearchBarIOS.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace XamarinFormsSearchBarIOS.iOS.CustomRenderers
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Override needed, otherwise the original Xamarin code will force show the Cancel button on the right side of the entry field
            if (e.PropertyName == SearchBar.TextProperty.PropertyName)
            {
                Control.Text = Element.Text;
            }

            if (e.PropertyName != SearchBar.CancelButtonColorProperty.PropertyName && e.PropertyName != SearchBar.TextProperty.PropertyName)
                base.OnElementPropertyChanged(sender, e);
        }
    }
}