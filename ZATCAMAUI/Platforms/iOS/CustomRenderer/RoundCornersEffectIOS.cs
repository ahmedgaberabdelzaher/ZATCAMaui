using CoreAnimation;
using Microsoft.Maui.Controls.Platform;
using System.ComponentModel;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Platforms.iOS.CustomRenderer;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(RoundCornersEffectIOS), nameof(RoundCornersEffect))]
namespace ZATCAMAUI.Platforms.iOS.CustomRenderer
{
    public class RoundCornersEffectIOS : PlatformEffect
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
                Container.Layer.CornerRadius = new nfloat(0);
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
            Container.ClipsToBounds = true;
            Container.Layer.AllowsEdgeAntialiasing = true;
            Container.Layer.EdgeAntialiasingMask = CAEdgeAntialiasingMask.All;
        }
        private void SetCornerRadius()
        {
            var cornerRadius = RoundCornersEffect.GetCornerRadius(Element);
            Container.Layer.CornerRadius = new nfloat(cornerRadius);
        }
    }
}
