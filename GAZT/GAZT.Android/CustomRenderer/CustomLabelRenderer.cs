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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using GAZT;
using GAZT.Droid.CustomRenderer;
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

        #region Method

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
          
            base.OnElementChanged(e);



            //if (Control != null)
            //{
            if (App.IsArabic)
            {
                Typeface font = Typeface.CreateFromAsset(_context.Assets, "SSTArabic-Light.ttf");
                Control.Typeface = font;
                //Control.TextAlignment = Android.Views.TextAlignment.TextEnd;
                //Control.Gravity = Android.Views.GravityFlags.Right;
                Control.TextDirection = Android.Views.TextDirection.Rtl;
                Control.Gravity = Android.Views.GravityFlags.Right;
            }
            else
            {
                Typeface font = Typeface.CreateFromAsset(_context.Assets, "SSTArabic-Light.ttf");
                Control.Typeface = font;
            }
            
        }
        #endregion Method
    }
}
