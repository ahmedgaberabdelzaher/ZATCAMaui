using Android.Content;
using Android.Content.Res;
using Android.Text.Method;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

namespace ZATCAMAUI.Platforms.Android.CustomRenderer
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
                Control.ScrollBarStyle = ScrollbarStyles.InsideInset;
                //nativeEditText.SetOnTouchListener(new DroidTouchListener());
                //Force scrollbars to be displayed
                TypedArray a = Control.Context.Theme.ObtainStyledAttributes(new int[0]);
                InitializeScrollbars(a);
                a.Recycle();
                Control.Background = null;
                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(5, 5, 5, 5);
                SetPadding(0, 0, 0, 0);
            }
            #endregion
        }
    }
}
