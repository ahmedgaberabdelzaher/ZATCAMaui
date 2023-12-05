using GAZT.Droid.CustomRenderer;
using Xamarin.Forms.Platform.Android.AppCompat;
using System.ComponentModel;
using Android.Content;
using Xamarin.Forms;
using EGAZT.Enums;
using EGAZT.CustomControl;

[assembly: ExportRenderer(typeof(TransitionNavigationPage), typeof(TransitionNavigationPageRenderer))]
namespace GAZT.Droid.CustomRenderer
{
    public class TransitionNavigationPageRenderer : NavigationPageRenderer
    {
        private TransitionType _transitionType = TransitionType.Default;

        public TransitionNavigationPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == EGAZT.CustomControl.TransitionNavigationPage.TransitionTypeProperty.PropertyName)
                UpdateTransitionType();
        }

        private void UpdateTransitionType()
        {
            var transitionNavigationPage = (TransitionNavigationPage)Element;
            _transitionType = transitionNavigationPage.TransitionType;
        }
    }
}
