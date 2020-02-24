using GAZT.ViewModel;
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
    public partial class CorrespondancePageView : ContentPage
    {
        CorrespondancePageViewModel viewModel;
        public CorrespondancePageView()
        {
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
         
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            NavigationPage.SetBackButtonTitle(this, "");
           
            InitializeComponent();

            viewModel = App.Locator.CorrespondancePageView;
            this.BindingContext = viewModel;
            viewModel.onPageLoad();
        }

        private void ClickGestureRecognizer_ClickedForZakat(object sender, EventArgs e)
        {

            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
          


            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }

        private void ClickGestureRecognizer_ClickedForVAT(object sender, EventArgs e)
        {
            
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
           


            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }
        private void ClickGestureRecognizer_ClickedForET(object sender, EventArgs e)
        {
            
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
         
            Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        }
  
    }
}