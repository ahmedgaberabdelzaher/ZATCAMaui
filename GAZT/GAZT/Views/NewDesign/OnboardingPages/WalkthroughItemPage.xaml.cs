using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.OnboardingPages
{
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalkthroughItemPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WalkthroughItemPage" /> class.
        /// </summary>
        public WalkthroughItemPage()
        {
            InitializeComponent();
            SetLTR();
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = Xamarin.Forms.FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = Xamarin.Forms.FlowDirection.LeftToRight; 
            }
        }
    }
}