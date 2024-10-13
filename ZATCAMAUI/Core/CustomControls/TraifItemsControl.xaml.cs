namespace ZATCAMAUI.Core.CustomControls
{
    public partial class TraifItemsControl : ContentView
    {
        public TraifItemsControl()
        {
            InitializeComponent();
            arrow.Rotation = App.IsArabic ? 180 : 0;
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }




    }
}
