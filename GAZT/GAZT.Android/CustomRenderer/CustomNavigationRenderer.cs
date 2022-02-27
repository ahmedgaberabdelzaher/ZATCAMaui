using System;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using GAZT.CustomControl;
using GAZT.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Android.Content.Res;
using Support = Android.Support.V7.Widget;
using EGAZT;

[assembly: ExportRenderer(typeof(CustomNavigation), typeof(CustomNavigationRenderer))]
namespace GAZT.Droid.CustomRenderer
{
    public class CustomNavigationRenderer: NavigationPageRenderer
    {
 public CustomNavigationRenderer(Context context) : base(context) 
 {
  }
		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
            try
            {
                base.OnLayout(changed, l, t, r, b);
            }
            catch(Exception ex)
            {
            }
			//var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			//toolbar.LayoutDirection = LayoutDirection.Rtl;
		}
		protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);
            var height = 0;
            Resources resources = Context.Resources;
            int resourceId = resources.GetIdentifier("navigation_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                height = resources.GetDimensionPixelSize(resourceId);
            }
        }
        private Android.Support.V7.Widget.Toolbar _toolbar;
        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                _toolbar = (Android.Support.V7.Widget.Toolbar)child;
                _toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }
        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //    if (disposing)
        //    {
        //        _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
        //    }
        //}
        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType(); 
            System.Diagnostics.Debug.WriteLine("e.Child.GetType() =" +e.Child.GetType().ToString());
            Android.Support.V7.Widget.AppCompatTextView textView;
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                 textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                // TODO: CHANGE VALUES HERE
                if(App.IsArabic)
                {
                    textView.TextSize = 16;
                }
                else
                {
                    textView.TextSize = 16;
                }
                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
            try
            {
                MessagingCenter.Unsubscribe<string>(this, "SetFont");
            }
            catch (Exception exe)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + exe.Message);
            }  
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                MessagingCenter.Subscribe<string>(this, "SetFont", message => {
                      textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                    if (App.IsArabic)
                    {
                        var spaceFont = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, "Somar-SemiBold.otf");
                        textView.Typeface = spaceFont;
                    }
                    else
                    {
                        var spaceFont = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, "Somar-SemiBold.otf");
                        textView.Typeface = spaceFont;
                    }
            });         
                 textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                if (App.IsArabic)
                {
                    var spaceFont = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, "Somar-SemiBold.otf");
                    textView.Typeface = spaceFont;
                }
                else
                {
                    var spaceFont = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, "Somar-SemiBold.otf");
                    textView.Typeface = spaceFont;
                }
                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
    }
    }
}
