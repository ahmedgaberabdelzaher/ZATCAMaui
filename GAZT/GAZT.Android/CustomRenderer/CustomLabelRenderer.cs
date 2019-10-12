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
            //base.OnElementChanged(e);
            //if (Control != null)
            //{
            //    if (App.IsArabic)
            //    {
            //        Typeface font = Typeface.CreateFromAsset(_context.Assets, "Cairo-Light.ttf");
            //        Control.Typeface = font;
            //        //Control.TextAlignment = Android.Views.TextAlignment.TextEnd;
            //        //Control.Gravity = Android.Views.GravityFlags.Right;
            //        Control.TextDirection = Android.Views.TextDirection.Rtl;
            //        Control.Gravity = Android.Views.GravityFlags.Right;
            //    }
            //    else
            //    {
            //        Typeface font = Typeface.CreateFromAsset(_context.Assets, "HelvLight.ttf");
            //        Control.Typeface = font;
            //    }
            //    var fs = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            //    Control.TextSize = (float)fs;

            //    string StyleId = e.NewElement.StyleId;
            //    if (!string.IsNullOrEmpty(StyleId))
            //    {
            //        if (StyleId.Equals("Medium"))
            //        {
            //            Control.TextSize = (float)Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            //            if (App.IsArabic)
            //            {
            //                Typeface font1 = Typeface.CreateFromAsset(_context.Assets, "Cairo-Regular.ttf");
            //                Control.Typeface = font1;
            //            }
            //            else
            //            {
            //                Typeface font1 = Typeface.CreateFromAsset(_context.Assets, "HelveticaNormal.ttf");
            //                Control.Typeface = font1;
            //            }

            //        }
            //        else if (StyleId.Equals("SmallBold"))
            //        {

            //            if (App.IsArabic)
            //            {
            //                Typeface font1 = Typeface.CreateFromAsset(_context.Assets, "Cairo-Regular.ttf");
            //                Control.Typeface = font1;
            //            }
            //            else
            //            {
            //                Typeface font1 = Typeface.CreateFromAsset(_context.Assets, "HelveticaNormal.ttf");
            //                Control.Typeface = font1;
            //            }
            //        }
            //        else if (StyleId.Equals("TwoLineText"))
            //        {

            //            Control.Ellipsize = Android.Text.TextUtils.TruncateAt.End;
            //            Control.SetLines(2);
            //        }
            //        else if (StyleId.Equals("FourLineText"))
            //        {

            //            Control.Ellipsize = Android.Text.TextUtils.TruncateAt.End;
            //            if (Device.Idiom == TargetIdiom.Phone)
            //            {
            //                if (App.IsArabic)
            //                    Control.SetLines(3);
            //                else
            //                    Control.SetLines(6);
            //            }
            //            else
            //            {
            //                if (App.IsArabic)
            //                    Control.SetLines(5);
            //                else
            //                    Control.SetLines(8);
            //            }

            //        }

            //        else if (StyleId.Equals("Micro"))
            //        {

            //            if (App.IsArabic)
            //            {
            //                Typeface font = Typeface.CreateFromAsset(_context.Assets, "Cairo-Light.ttf");
            //                Control.Typeface = font;
            //            }
            //            else
            //            {
            //                Typeface font = Typeface.CreateFromAsset(_context.Assets, "HelvLight.ttf");
            //                Control.Typeface = font;
            //            }
            //            var fonts = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            //            Control.TextSize = (float)fonts;


            //        }
            //        else if (StyleId.Equals("MicroTabBar"))
            //        {
            //            if (App.IsArabic)
            //            {
            //                Typeface font = Typeface.CreateFromAsset(_context.Assets, "Cairo-Light.ttf");
            //                Control.Typeface = font;
            //                var fonts = 10;
            //                Control.TextSize = (float)fonts;
            //            }
            //            else
            //            {
            //                Typeface font = Typeface.CreateFromAsset(_context.Assets, "HelvLight.ttf");
            //                Control.Typeface = font;
            //                var fonts = 12;
            //                Control.TextSize = (float)fonts;
            //            }

            //        }

            //        else if (StyleId.Equals("MicroTableText"))
            //        {
            //            if (App.IsArabic)
            //            {
            //                Typeface font = Typeface.CreateFromAsset(_context.Assets, "Cairo-Light.ttf");
            //                Control.Typeface = font;
            //                var fonts = 11;
            //                Control.TextSize = (float)fonts;
            //            }
            //            else
            //            {
            //                //Typeface font = Typeface.CreateFromAsset(_context.Assets, "HelvLight.ttf");
            //                //Control.Typeface = font;
            //                //var fonts = 10;
            //                //Control.TextSize = (float)fonts;
            //            }

            //        }
            //    }



            //}
        }
        #endregion Method
    }
}
