using Android.Graphics;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;
using Microsoft.Maui.Controls.Platform;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Platforms.Android.CustomRenderer;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using Resources = Android.Content.Res.Resources;
using View = Android.Views.View;
using AndroidX.AppCompat.Widget;
using Android.Content.Res;
using Android.Content;

[assembly: ExportRenderer(typeof(CustomNavigation), typeof(CustomNavigationRenderer))]
namespace ZATCAMAUI.Platforms.Android.CustomRenderer
{
    public class CustomNavigationRenderer : NavigationPageRenderer
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
            catch (Exception)
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
        private Toolbar _toolbar;
        public override void OnViewAdded( View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Toolbar))
            {
                _toolbar = (Toolbar)child;
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
            System.Diagnostics.Debug.WriteLine("e.Child.GetType() =" + e.Child.GetType().ToString());
            AppCompatTextView textView;
            if (e.Child.GetType() == typeof(AppCompatTextView))
            {
                textView = (AppCompatTextView)e.Child;
                // TODO: CHANGE VALUES HERE
                if (App.IsArabic)
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
            if (e.Child.GetType() == typeof(AppCompatTextView))
            {
                MessagingCenter.Subscribe<string>(this, "SetFont", message => {
                    textView = (AppCompatTextView)e.Child;
                    if (App.IsArabic)
                    {
                        var spaceFont = Typeface.CreateFromAsset(this.Context.Assets, "Somar-SemiBold.otf");
                        textView.Typeface = spaceFont;
                    }
                    else
                    {
                        var spaceFont = Typeface.CreateFromAsset(this.Context.Assets, "Somar-SemiBold.otf");
                        textView.Typeface = spaceFont;
                    }
                });
                textView = (AppCompatTextView)e.Child;
                if (App.IsArabic)
                {
                    var spaceFont = Typeface.CreateFromAsset(this.Context.Assets, "Somar-SemiBold.otf");
                    textView.Typeface = spaceFont;
                }
                else
                {
                    var spaceFont = Typeface.CreateFromAsset(this.Context.Assets, "Somar-SemiBold.otf");
                    textView.Typeface = spaceFont;
                }
                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}
