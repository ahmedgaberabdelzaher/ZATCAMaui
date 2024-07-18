
namespace ZATCAMAUI.Core.CustomControls
{
    public partial class NavMenu : ContentView
    {
        public NavMenu()
        {
            InitializeComponent();
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }
    }
}

