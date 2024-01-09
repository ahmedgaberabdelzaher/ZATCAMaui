using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;

namespace ZATCAMAUI.Platforms.iOS.CustomRenderer
{
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
