using Xamarin.Forms;

namespace GAZT.CustomControl
{
    public partial class CustomNavigation : NavigationPage
    {
        public CustomNavigation(Page root) : base(root)
        {
            InitializeComponent();
            //BarBackgroundColor =Color.FromHex ("#7CBB47");
            BarBackgroundColor = Color.FromHex("#005e4b");
            BarTextColor = Color.White;

            // App.NavigationBarHeightt = Height;
        }
    }
}
