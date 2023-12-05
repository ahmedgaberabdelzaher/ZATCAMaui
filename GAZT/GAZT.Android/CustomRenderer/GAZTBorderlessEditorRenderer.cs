using Android.Content;
using Android.Text.Method;
using Android.Views;
using GAZT;
using GAZT.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(GAZTBorderlessEditor), typeof(GAZTBorderlessEditorRenderer))]
namespace GAZT.Droid.CustomRenderer
{
    class GAZTBorderlessEditorRenderer : EditorRenderer
    {
        double fs;
        public GAZTBorderlessEditorRenderer(Context context) : base(context)
        {
        }
        #region Method
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeEditText = (global::Android.Widget.EditText)Control;
                //While scrolling inside Editor stop scrolling parent view.
                nativeEditText.OverScrollMode = OverScrollMode.Always;
                nativeEditText.ScrollBarStyle = ScrollbarStyles.InsideInset;
               // For Scrolling in Editor innner area
                Control.VerticalScrollBarEnabled = true;
                Control.MovementMethod = ScrollingMovementMethod.Instance;
                Control.ScrollBarStyle = Android.Views.ScrollbarStyles.InsideInset;
                //nativeEditText.SetOnTouchListener(new DroidTouchListener());
                //Force scrollbars to be displayed
                Android.Content.Res.TypedArray a = Control.Context.Theme.ObtainStyledAttributes(new int[0]);
                InitializeScrollbars(a);
                a.Recycle();
                Control.Background = null;
                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(5, 5, 5, 5);
                SetPadding(0, 0, 0, 0);
                //if (App.IsArabic)
                //{
                //    Typeface font1 = Typeface.CreateFromAsset(Forms.Context.Assets, "Cairo-Regular.ttf");
                //    Control.Typeface = font1;
                //    this.Control.Gravity = Android.Views.GravityFlags.Right;
                //}
                //else
                //{
                //    Typeface font1 = Typeface.CreateFromAsset(Forms.Context.Assets, "HelveticaNormal.ttf");
                //    Control.Typeface = font1;
                //}
            }
            #endregion
        }
    }
}

