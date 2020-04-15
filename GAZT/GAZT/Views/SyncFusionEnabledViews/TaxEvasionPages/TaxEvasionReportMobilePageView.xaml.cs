using GAZT.Helper;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Foundation;

namespace GAZT.Views.SyncFusionEnabledViews.TaxEvasionPages
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
                StacklayoutEn.IsVisible = false;
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                StackLayoutAr.IsVisible = false;
            }
        }

        private void Mobile_Entry_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.MobileNumber))

            {
                if (viewModel.MobileNumber.Length != 8)
                {
                    frmMobile.HasError = true;
                    frmMobileAr.HasError = true;
                    viewModel.IsVerifyEnable = false;

                }
                else
                {
                    viewModel.IsVerifyEnable = true;
                    frmMobile.HasError = false;
                    frmMobileAr.HasError = false;
                }
            }
            

        }
    }
    

}