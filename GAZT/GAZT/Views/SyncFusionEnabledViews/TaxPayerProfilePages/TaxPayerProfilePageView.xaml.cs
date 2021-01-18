using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxPayerProfilePage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.OTPPage;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.TaxPayerProfile_View
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxPayerProfilePageView : ContentPage
    {
        #region Variable
        TaxPayerProfilePageViewModel viewModel;
        private double width = 0;
      private double height = 0;
        #endregion
        #region Constructor
        public TaxPayerProfilePageView()
        {
            viewModel = App.Locator.TaxPayerProfilePageView;
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            ChangeAeroIcon();
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.OnPageLoad();
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
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ChangeAeroIcon();
            viewModel.SetTP();
            Task.Delay(20000);
            //if (viewModel.IscomingFromOTPViewViaEmail)
            //{
            //    await viewModel._dialogService.ShowMessageBox(AppResources.MandatoryPasswordForEmailUpdatation, AppResources.Information);
            //}
            //for (int index = Navigation.NavigationStack.Count - 2; index > 1; index--)
            //{
            //    Page pg = Navigation.NavigationStack[index];
            //    Navigation.RemovePage(pg);
            //}
            for (int index = 0; index < Navigation.NavigationStack.Count; index++)
            {
                Xamarin.Forms.Page pg = Navigation.NavigationStack[index];
                if (pg.GetType() == typeof(OTPPageView))
                {
                    Navigation.RemovePage(pg);
                }
            }
        }
        //public void OnPasswordVisibilityClicked(object sender, EventArgs args)
        //{
        //    viewModel.PasswordVisibility = !viewModel.PasswordVisibility;
        //}
        //public void OnPasswordFocused(object sender, EventArgs args)
        //{
        //    Password.Unfocus();
        //}
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        #endregion
    }
}