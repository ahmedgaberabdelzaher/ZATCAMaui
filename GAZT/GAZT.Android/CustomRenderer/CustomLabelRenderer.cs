using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using GAZT;
using GAZT.Droid.CustomRenderer;
using EGAZT;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace GAZT.Droid.CustomRenderer
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
