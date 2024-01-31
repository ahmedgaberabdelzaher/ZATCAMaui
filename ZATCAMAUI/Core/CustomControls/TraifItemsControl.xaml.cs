namespace ZATCAMAUI.Core.CustomControls
{
    public partial class TraifItemsControl : ContentView
    {
        public TraifItemsControl()
        {
            InitializeComponent();
            if (!App.IsArabic)
            {
                arrow.Rotation = 0;


            }
            else
            {
                arrow.Rotation = 180;


                // Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
        }




    }
}
