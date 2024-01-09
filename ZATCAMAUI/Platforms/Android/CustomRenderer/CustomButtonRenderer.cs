using Android.Content;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;
using Microsoft.Maui.Controls.Platform;
using System.ComponentModel;
using Color = Android.Graphics.Color;

namespace ZATCAMAUI.Platforms.Android.CustomRenderer
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        public CustomButtonRenderer(Context context) : base(context)
        {
        }
        #region Method
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                SetColors();
                Control.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
                Control.SetIncludeFontPadding(false);
                Control.SetMinHeight(0);
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);
            if (args.PropertyName == nameof(Button.IsEnabled)) SetColors();
        }
        private void SetColors()
        {
            Control.SetTextColor(Element.IsEnabled ? Element.TextColor.ToAndroid() : Color.Gray);
        }
        #endregion
    }
}
