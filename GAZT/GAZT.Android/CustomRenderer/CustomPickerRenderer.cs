using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Content.Res;
using Android.Content;
using Android.Text;
using GAZT;
using GAZT.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace GAZT.Droid.CustomRenderer
{
    class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                this.Control.SetTextColor(Android.Graphics.Color.Black);
                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
                Control.InputType = InputTypes.TextFlagNoSuggestions;
                Control.SetHintTextColor(Android.Graphics.Color.Black);


               //// int[][] states = new int[][]{
               //// new int[]{}
               ////};
               //// int[] colors = new int[] { Resource.Color.abc_btn_colored_text_material };

               //// ColorStateList myList = new ColorStateList(states, colors);
               //// Control.Focusable = false;
               //// Control.FocusableInTouchMode = false;
               //// //Control.SetHintTextColor(myList);
               //// if (App.IsArabic)
               //// {
               ////     Typeface font1 = Typeface.CreateFromAsset(Forms.Context.Assets, "Cairo-Regular.ttf");
               ////     Control.Typeface = font1;
               ////     this.Control.Gravity = Android.Views.GravityFlags.Right;
               //// }
               //// else
               //// {
               ////     Typeface font1 = Typeface.CreateFromAsset(Forms.Context.Assets, "HelveticaNormal.ttf");
               ////     Control.Typeface = font1;
               //// }

            }
        }
    }
}