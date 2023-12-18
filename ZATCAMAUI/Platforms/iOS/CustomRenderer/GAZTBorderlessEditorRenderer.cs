using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;
using ZATCAMAUI.Core.CustomControls;

using ZATCAMAUI.Platforms.iOS.CustomRenderer;

[assembly: ExportRenderer(typeof(GAZTBorderlessEditor), typeof(GAZTBorderlessEditorRenderer))]
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
