namespace ZATCAMAUI.Views.NewDesign.OnboardingPages
{
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalkthroughItemPage:ContentView
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
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}