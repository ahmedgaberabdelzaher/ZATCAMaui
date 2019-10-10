using System;
using Android.Content;
using Android.Graphics;
using GAZT;
using GAZT.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using GAZT.Views;
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
                if(App.IsArabic)
                {
                    Control.TextDirection = Android.Views.TextDirection.Rtl;
                    Control.Gravity = Android.Views.GravityFlags.Right;
                }
                Control.Background = null;
                fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                Control.TextSize = (float)fontSize;
                Control.SetTextColor(global::Android.Graphics.Color.Black);
                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);
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
    }
}
