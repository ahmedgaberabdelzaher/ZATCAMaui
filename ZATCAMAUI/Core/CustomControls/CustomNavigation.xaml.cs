using ZATCAMAUI.Core.Enums;

namespace ZATCAMAUI.Core.CustomControls
{

    public partial class CustomNavigation : NavigationPage
    {

        public static readonly BindableProperty TransitionTypeProperty =
              BindableProperty.Create("TransitionType", typeof(TransitionType), typeof(CustomNavigation), TransitionType.SlideFromLeft);

        public TransitionType TransitionType
        {
            get { return (TransitionType)GetValue(TransitionTypeProperty); }
            set { SetValue(TransitionTypeProperty, value); }
        }

        public CustomNavigation() : base()
        {
        }

        public CustomNavigation(Page root) : base(root)
        {
            InitializeComponent();
            BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
            BarTextColor = Colors.White;
        }




    }
}
