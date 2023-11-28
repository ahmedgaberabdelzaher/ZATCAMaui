using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Text;
using GAZT;
using GAZT.Droid.CustomRenderer;
using EGAZT;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace GAZT.Droid.CustomRenderer
{
    class CustomPickerRenderer : PickerRenderer
    {
        CustomPicker element;
        public CustomPickerRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Color colorPrimary = (Color)App.Current.Resources["Primary"];
                this.Control.SetTextColor(Android.Graphics.Color.ParseColor("#042e66"));
                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
                Control.InputType = InputTypes.TextFlagNoSuggestions;
                Control.SetHintTextColor(Android.Graphics.Color.ParseColor("#042e66"));
                element = (CustomPicker)this.Element;
                //if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                //    Control.Background = AddPickerStyles(element.Image);
                if (App.IsArabic)
                {
                    Control.TextDirection = Android.Views.TextDirection.Rtl;
                    Control.Gravity = Android.Views.GravityFlags.CenterVertical;
                }
            }
        }
        //public LayerDrawable AddPickerStyles(string imagePath)
        //{
        //    ShapeDrawable border = new ShapeDrawable();
        //    border.Paint.Color = Android.Graphics.Color.Gray;
        //    border.SetPadding(10, 10, 10, 10);
        //    border.Paint.SetStyle(Paint.Style.Stroke);
        //    Drawable[] layers = { border, GetDrawable(imagePath) };
        //    LayerDrawable layerDrawable = new LayerDrawable(layers);
        //    layerDrawable.SetLayerInset(0, 0, 0, 0, 0);
        //    return layerDrawable;
        //}
        //private BitmapDrawable GetDrawable(string imagePath)
        //{
        //    int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
        //    var drawable = ContextCompat.GetDrawable(this.Context, resID);
        //    var bitmap = ((BitmapDrawable)drawable).Bitmap;
        //    var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 70, 70, true));
        //    result.Gravity = Android.Views.GravityFlags.Right;
        //    return result;
        //}
    }
}