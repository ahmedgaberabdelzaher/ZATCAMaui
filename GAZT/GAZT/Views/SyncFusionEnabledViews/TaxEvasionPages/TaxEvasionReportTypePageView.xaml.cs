using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportTypePage_ViewModel;
using GAZT.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.TaxEvasionReportType
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportTypePageView : ContentPage
    {
        //FormBundleStatusPageViewModel viewModel;
        TaxEvasionReportTypePageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public TaxEvasionReportTypePageView(string MobileNumber)
        {
            viewModel = App.Locator.TaxEvasionReportTypePageView;
            InitializeComponent();
            ChangeAeroIcon();
            MainLayout.Padding = new Thickness(0, 0, 0, 0);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            viewModel.TaxEvasionListobj = new TaxEvasionReportDetails();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;
            viewModel.CategorySelected_Index = "0";
            viewModel.MobileNumber = MobileNumber;
            SetLTR();
            viewModel.IsnextbuttonEnable = false;
            //viewModel.onPageLoad();
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                if (App.IsArabic)
                {
                    if (width > height)
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        MainLayout.Padding = new Thickness(40, 0, 40, 0);
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        MainLayout.Padding = new Thickness(0, 0, 0, 0);
                    }
                }
                //reconfigure layout
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;
            viewModel.CategorySelected_Index = "0";
            viewModel.NextbuttonDisableColor= Color.FromHex("#9EA4A9");
            viewModel.IsnextbuttonEnable = false;
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.IsLoading = false;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
        }
        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (!viewModel.IsLoading)
            {
                Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                Navigation.RemovePage(pg);
            }
           // viewModel._navigationService.NavigateTo(App.TaxEvasionReportListPageView, viewModel.MobileNumber);
            //for (int index = Navigation.NavigationStack.Count - 2; index > 1; index--)
            //{
            //}
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = true;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;
            viewModel.NextbuttonDisableColor = Color.FromHex("#005e4b");
            viewModel.IsnextbuttonEnable = true;
        }
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = true;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;
            viewModel.NextbuttonDisableColor = Color.FromHex("#005e4b");
            viewModel.IsnextbuttonEnable = true;
        }
        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = true;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;
            viewModel.NextbuttonDisableColor = Color.FromHex("#005e4b");
            viewModel.IsnextbuttonEnable = true;
        }
        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = true;
            viewModel.IsimgVisiblec5 = false;
            viewModel.NextbuttonDisableColor = Color.FromHex("#005e4b");
            viewModel.IsnextbuttonEnable = true;
        }
        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = true;
            viewModel.NextbuttonDisableColor = Color.FromHex("#005e4b");
            viewModel.IsnextbuttonEnable = true;
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
    }
}