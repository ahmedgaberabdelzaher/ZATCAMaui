using System;
using Android.Content;
using GAZT.CustomControl;
using GAZT.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace GAZT.Droid.CustomRenderer
{
    public class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context) { }
        double fontSize;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                Control.SetTextColor(global::Android.Graphics.Color.ParseColor("#042e66"));
                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 20, 0, 0);
                SetPadding(0, 0, 0, 0);
                if (Control != null)
                {
                    Control.Gravity = Android.Views.GravityFlags.CenterVertical;
                }
                Control.TextDirection = Android.Views.TextDirection.Ltr;
                Control.Gravity = Android.Views.GravityFlags.Left;
            }
        }
    }
}
