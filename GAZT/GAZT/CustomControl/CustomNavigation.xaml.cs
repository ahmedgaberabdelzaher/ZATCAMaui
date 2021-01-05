using Xamarin.Forms;
using EGAZT.Enums;
using Xamarin.Forms.Internals;

namespace GAZT.CustomControl
{
    [Preserve(AllMembers = true)]
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
            BarBackgroundColor = Color.FromHex("#005e4b");
            BarTextColor = Color.White;
        }



        //public CustomNavigation(Page root) : base(root)
        //{
        //    InitializeComponent();
        //    BarBackgroundColor = Color.FromHex("#005e4b");
        //    BarTextColor = Color.White;
            
        //    //if (App.IsArabic)
        //    //{
        //    //    NavPage.FlowDirection = FlowDirection.RightToLeft;
        //    //}
        //    //else
        //    //{
        //    //    NavPage.FlowDirection = FlowDirection.LeftToRight;
        //    //}


        //}



    }
}
