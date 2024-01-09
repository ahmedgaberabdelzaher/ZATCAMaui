using Android.Graphics;
using Android.Views;
using Microsoft.Maui.Controls.Platform;
using System.ComponentModel;
using ZATCAMAUI.Core.CustomControls;
using View = Android.Views.View;

[assembly: ResolutionGroupName("MyComany")]
namespace ZATCAMAUI.Platforms.Android.CustomRenderer
{
    public class RoundCornersEffectDroid : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                PrepareContainer();
                SetCornerRadius();
            }
            catch { }
        }
        protected override void OnDetached()
        {
            try
            {
                Container.OutlineProvider = ViewOutlineProvider.Background;
            }
            catch { }
        }
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == RoundCornersEffect.CornerRadiusProperty.PropertyName)
                SetCornerRadius();
        }
        private void PrepareContainer()
        {
            Container.ClipToOutline = true;
        }
        private void SetCornerRadius()
        {
            try
            {
                var cornerRadius = RoundCornersEffect.GetCornerRadius(Element) * GetDensity();
                Container.OutlineProvider = new RoundedOutlineProvider(cornerRadius);
            }
            catch (Exception)
            {
            }
           
        }
        //TODO
        private static float GetDensity() =>
           (float)DeviceDisplay.Current.MainDisplayInfo.Density;
        private class RoundedOutlineProvider : ViewOutlineProvider
        {
            private readonly float _radius;
            public RoundedOutlineProvider(float radius)
            {
                _radius = radius;
            }
            public override void GetOutline(View view, Outline outline)
            {
                outline?.SetRoundRect(0, 0, view.Width, view.Height, _radius);
            }
        }
    }
}
