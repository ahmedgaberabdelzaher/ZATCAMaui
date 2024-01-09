using Android.Content;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

namespace ZATCAMAUI.Platforms.Android.CustomRenderer
{
    public class GAZTBorderlessEntryRenderer : EntryRenderer
    {
        public GAZTBorderlessEntryRenderer(Context context) : base(context)
        {
        }
        public static void Init() { }
        double fontSize;
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            try
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

                    if (e?.NewElement.Keyboard == Keyboard.Numeric)
                    {
                        this.Control.KeyListener = DigitsKeyListener.GetInstance(string.Format("1234567890{0}", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                        this.Control.InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal;
                    }


                    if (Control != null)
                    {
                        Control.Gravity = GravityFlags.CenterVertical;
                    }
                    if (App.IsArabic)
                    {
                        Control.TextDirection = TextDirection.Rtl;
                        Control.Gravity = GravityFlags.CenterVertical;
                    }
                    if (App.IsOTPiew)
                    {
                        Control.TextDirection = TextDirection.Ltr;
                        Control.Gravity =GravityFlags.Left;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
