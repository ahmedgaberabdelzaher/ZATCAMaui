using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EGAZT;
using GAZT;
using GAZT.CustomControl;
using GAZT.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(GAZTBorderlessEntry), typeof(GAZTBorderlessEntryRenderer))]
namespace GAZT.Droid.CustomRenderer
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
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
            if (App.IsArabic)
            {
               
                Control.TextDirection = Android.Views.TextDirection.Rtl;
                Control.Gravity = Android.Views.GravityFlags.Right;
            }
            
        }
    }
}