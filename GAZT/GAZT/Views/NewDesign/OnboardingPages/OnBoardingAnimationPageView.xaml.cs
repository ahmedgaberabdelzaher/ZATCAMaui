using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.OnboardingPages
{
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignOnBoardingAnimationPageView
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GAZTNewDesignOnBoardingAnimationPageView" /> class.
        /// </summary>
        public GAZTNewDesignOnBoardingAnimationPageView()
        {
            InitializeComponent();

            this.BindingContext = App.Locator.GAZTNewDesignOnBoardingAnimationPageView;
        }
    }
}