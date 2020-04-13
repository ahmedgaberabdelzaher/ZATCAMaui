using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportListPageView : ContentPage
    {
        TaxEvasionReportListPageViewModel viewModel;
        public TaxEvasionReportListPageView(string mobno)
        {
            viewModel = App.Locator.TaxEvasionReportListPageView;
            InitializeComponent();
            ChangeAeroIcon();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.CertificateLst.SelectedItem = null;
            this.CertificateLstClosed.SelectedItem = null;
            viewModel.AddIcon = "ic_add1.png";
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");


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
    }
}