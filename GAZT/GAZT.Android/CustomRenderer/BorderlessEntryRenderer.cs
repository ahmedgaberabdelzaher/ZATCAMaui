using Android.Content;
using EGAZT;
using GAZT;
using GAZT.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace GAZT.Droid.CustomRenderer
{
    public class BorderlessEntryRenderer : EntryRenderer
	{
		public BorderlessEntryRenderer(Context context) : base(context)
		{
		}
		public static void Init() { }
		double fontSize;
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			if (e.OldElement == null)
			{
				Control.Background = null;
				//fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
				//Control.TextSize = (float)fontSize;
				//Control.SetTextColor(global::Android.Graphics.Color.Black);
				var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
				layoutParams.SetMargins(0, 0, 0, 0);
				LayoutParameters = layoutParams;
				Control.LayoutParameters = layoutParams;
				Control.SetPadding(0, 0, 0, 0);
				SetPadding(0, 0, 0, 0);

				if(e?.NewElement.Keyboard == Keyboard.Numeric)
                {
					this.Control.KeyListener = Android.Text.Method.DigitsKeyListener.GetInstance(string.Format("1234567890{0}", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
					this.Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
                }
               

                if (Control != null)
                {
                    Control.Gravity = Android.Views.GravityFlags.CenterVertical;
                }
                if (App.IsArabic)
				{
                    Control.TextDirection = Android.Views.TextDirection.Rtl;
                    Control.Gravity = Android.Views.GravityFlags.CenterVertical;
                }
                if (App.IsOTPiew)
                {
                    Control.TextDirection = Android.Views.TextDirection.Ltr;
                    Control.Gravity = Android.Views.GravityFlags.Left;
                }
            }
		}
	}
}
