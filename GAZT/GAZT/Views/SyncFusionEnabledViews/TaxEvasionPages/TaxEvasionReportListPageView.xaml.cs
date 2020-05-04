using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportListPage_ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.TaxEvasionReportList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportListPageView : ContentPage
    {
        TaxEvasionReportListPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public TaxEvasionReportListPageView(string mobno)
        {
            viewModel = App.Locator.TaxEvasionReportListPageView;
            InitializeComponent();
            ChangeAeroIcon();
            MainLayout.Padding = new Thickness(10, 0, 10, 0);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.CertificateLst.SelectedItem = null;
            this.CertificateLstClosed.SelectedItem = null;
            viewModel.AddIcon = "ic_add1.png";
      //      Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            SetLTR();
            if (!string.IsNullOrEmpty(mobno))
            {
                viewModel.MobileNumber = mobno;
            }
            Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            CertificateLst.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            };
            CertificateLstClosed.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            };
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            this.BindingContext = viewModel;
            viewModel.OnPageLoad();
        }
        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height); //must be called
        //    if (this.width != width || this.height != height)
        //    {
        //        this.width = width;
        //        this.height = height;
        //        if (App.IsArabic)
        //        {
        //            if (width > height)
        //            {
        //                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
        //            }
        //            else
        //            {
        //                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        //            }
        //        }
        //        //reconfigure layout
        //    }
        //}
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
                        MainLayout.Padding = new Thickness(10, 0, 10, 0);
                    }
                }
                //reconfigure layout
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnPageLoad();
            // this.Content = null;
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void TapGestureRecognizer_ForOpenReports(object sender, EventArgs e)
        {
            Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }
        private void TapGestureRecognizer_ForClosedReports(object sender, EventArgs e)
        {
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.TaxEvasionReportTypePageView);
        }
        private void CertificateLstClosed_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            return;
        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    viewModel.SelectedTaxEvasionListItem = null;
        //}
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
        private void CertificateLst_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            return;
        }
        //protected override bool OnBackButtonPressed() => true;
        protected override bool OnBackButtonPressed()
        {
            if (App.TP != null && App.TP.Mobile != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}