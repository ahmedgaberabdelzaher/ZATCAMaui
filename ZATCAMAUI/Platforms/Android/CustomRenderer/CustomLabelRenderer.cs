using Android.Content;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Platforms.Android.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace ZATCAMAUI.Platforms.Android.CustomRenderer
{
    public class CustomLabelRenderer : LabelRenderer
    {
        Context _context;
        public CustomLabelRenderer(Context context) : base(context)
        {
            _context = context;
        }
        // This is for line Spacing
        protected CustomLabel LineSpacingLabel { get; private set; }
        #region Method
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            //line spacing between two lines of one label
            if (e.OldElement == null)
            {
                this.LineSpacingLabel = (CustomLabel)this.Element;
            }

            var lineSpacing = this.LineSpacingLabel.LineSpacing;

            this.Control.SetLineSpacing(1f, (float)lineSpacing);

            this.UpdateLayout();

        }
        #endregion Method
    }
}
