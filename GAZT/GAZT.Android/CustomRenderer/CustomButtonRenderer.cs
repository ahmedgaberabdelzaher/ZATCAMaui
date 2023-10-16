using System.ComponentModel;
using Android.Content;
using Android.Views;
using GAZT;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace GAZT
{
    public class CustomButtonRenderer : ButtonRenderer
	{
        public CustomButtonRenderer(Context context) : base(context)
        {
        }
        #region Method
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                SetColors();
                Control.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
                Control.SetIncludeFontPadding(false);
                Control.SetMinHeight(0);
                //if (App.IsArabic)
                //{
                //    Typeface font1 = Typeface.CreateFromAsset(Forms.Context.Assets, "Cairo-Regular.ttf");
                //    Control.Typeface = font1;
                //}
                //else
                //{
                //    Typeface font1 = Typeface.CreateFromAsset(Forms.Context.Assets, "HelveticaNormal.ttf");
                //    Control.Typeface = font1;
                //}
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);
            if (args.PropertyName == nameof(Button.IsEnabled)) SetColors();
        }
        private void SetColors()
        {
            Control.SetTextColor(Element.IsEnabled ? Element.TextColor.ToAndroid() : Android.Graphics.Color.Gray);
            //Control.SetBackgroundColor(Element.IsEnabled ? Element.BackgroundColor.ToAndroid() : Android.Graphics.Color.DarkGray);
        }
        #endregion
    }
}
