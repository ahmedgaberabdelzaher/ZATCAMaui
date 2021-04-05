using Android.Content;
using Android.Graphics;
using Android.Views.InputMethods;
using EGAZT;
using GAZT;
using GAZT.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace GAZT.Droid.CustomRenderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        double fs;
        public CustomEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            string StyleId = e.NewElement.StyleId;

            e.NewElement.IsTextPredictionEnabled = false;
            e.NewElement.IsSpellCheckEnabled = false;
            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
                fs = Device.GetNamedSize(NamedSize.Medium, typeof(Entry));
                Control.TextSize = (float)fs;//for android size is  float 
                Control.SetTextColor(global::Android.Graphics.Color.Black);
                Control.ImeOptions = (ImeAction)ImeFlags.NoExtractUi;
                if (App.IsArabic)
                {
                    Typeface font1 = Typeface.CreateFromAsset(Forms.Context.Assets, "Cairo-Regular.ttf");
                    Control.Typeface = font1;
                }
                else
                {
                    Typeface font1 = Typeface.CreateFromAsset(Forms.Context.Assets, "HelveticaNormal.ttf");
                    Control.Typeface = font1;
                }
                //      Control.SetBackgroundColor(Android.Graphics.Color.LightGray);
                //  Control.SetBackgroundResource(global::Android.Graphics.Color.LightGray);
            }
        }
    }
}
