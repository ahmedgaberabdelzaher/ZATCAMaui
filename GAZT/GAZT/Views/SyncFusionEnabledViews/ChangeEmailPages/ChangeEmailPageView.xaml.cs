using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeEmailPage_ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.ChangeEmail
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeEmailPageView : ContentPage
    {
        #region Variable
        ChangeEmailPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        #endregion
        #region Constructor
        public ChangeEmailPageView()
        {
            viewModel = App.Locator.ChangeEmailPageView;
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            ChangeAeroIcon();
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.OnPageLoad();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
        }
        #endregion
        #region Method
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    this.BackgroundImageSource = "sf_LoginBackgroundLand.png";
                }
                else
                {
                    this.BackgroundImageSource = "sf_LoginBackground.png";
                    //  outerStack.Orientation = StackOrientation.Vertical;
                }
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ChangeAeroIcon();
        }
            private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        #endregion
    }
}