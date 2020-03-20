using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportListPageView : ContentPage
    {
        TaxEvasionReportListPageViewModel viewModel;
        public TaxEvasionReportListPageView()
        {
            viewModel = App.Locator.TaxEvasionReportListPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
            this.CertificateLst.SelectedItem = null;
            this.CertificateLstClosed.SelectedItem = null;
            NavigationPage.SetBackButtonTitle(this, "");

            SetLTR();
            Resources["searchBarStyleForExcise"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];

            CertificateLst.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;
            };
            CertificateLstClosed.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;
            };

            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            this.BindingContext = viewModel;
            
           
            viewModel.OnPageLoad();
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
    }
}