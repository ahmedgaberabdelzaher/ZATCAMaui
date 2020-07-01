using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportMobilePage_ViewModel;
using GAZT.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.TaxEvasionReportMobile
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportMobilePageView : ContentPage
    {
        TaxEvasionReportMobilePageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public TaxEvasionReportMobilePageView()
        {
            InitializeComponent();
            MainLayout.Padding = new Thickness(10, 0, 10, 0);
            //viewModel = App.Locator.TaxEvasionReportPhonePageView;
            this.BindingContext = viewModel;
            viewModel = App.Locator.TaxEvasionReportPhonePageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
            viewModel.MobileNumber= string.Empty;

            //  On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            DependencyService.Get<IStatusBar>().HideStatusBar();
            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.MobileNumberPrefix = mobileNumberPrefix.Text;
            Mobile_Entry.Focus();
        }
        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);
        //    if (width != this.width || height != this.height)
        //    {
        //        this.width = width;
        //        this.height = height;
        //        if (width > height)
        //        {
        //            this.BackgroundImageSource = "sf_LoginBackgroundLand.png";
        //        }
        //        else
        //        {
        //            this.BackgroundImageSource = "sf_LoginBackground.png";
        //            //  outerStack.Orientation = StackOrientation.Vertical;
        //        }
        //    }
        //}
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
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
                }
                if (App.IsArabic)
                {
                    if (width > height)
                    {
                        //this.BackgroundImageSource = "sf_LoginBackgroundLand.png";
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        MainLayout.Padding = new Thickness(40, 0, 40, 0);
                    }
                    else
                    {
                      //  this.BackgroundImageSource = "sf_LoginBackground.png";
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        MainLayout.Padding = new Thickness(10, 0, 10, 0);
                    }
                }
                //reconfigure layout
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
        private void Mobile_Entry_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.MobileNumber))
            {
                if (viewModel.MobileNumber.Length != 9)
                {
                    frmMobile.HasError = true;
                    viewModel.IsVerifyEnable = false;
                }
                else
                {
                    viewModel.IsVerifyEnable = true;
                    frmMobile.HasError = false;
                }
            }
        }
    }
}