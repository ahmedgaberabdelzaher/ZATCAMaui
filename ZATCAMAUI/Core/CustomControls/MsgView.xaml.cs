
namespace ZATCAMAUI.Core.CustomControls
{
    public partial class MsgView : ContentView
    {
        public MsgView()
        {
            InitializeComponent();
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }
    }
}
