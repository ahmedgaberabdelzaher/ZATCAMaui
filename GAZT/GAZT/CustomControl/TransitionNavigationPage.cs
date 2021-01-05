using EGAZT.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    public class TransitionNavigationPage : NavigationPage
	{
        public static readonly BindableProperty TransitionTypeProperty =
               BindableProperty.Create("TransitionType", typeof(TransitionType), typeof(TransitionNavigationPage), TransitionType.SlideFromLeft);

        public TransitionType TransitionType
        {
            get { return (TransitionType)GetValue(TransitionTypeProperty); }
            set { SetValue(TransitionTypeProperty, value); }
        }

        public TransitionNavigationPage() : base()
        {
        }

        public TransitionNavigationPage(Page root) : base(root)
        {
        }
    }
}
