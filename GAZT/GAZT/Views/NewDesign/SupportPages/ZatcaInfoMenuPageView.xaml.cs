using EGAZT.Helper;
using EGAZT.Models.EnumModels;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;
using WebView = Xamarin.Forms.WebView;

namespace EGAZT.Views.NewDesign
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZatcaInfoMenuPageView : ContentPage
    {
        ZatcaInfoMenuPageViewModel viewModel;
        public ZatcaInfoMenuPageView()
        {

            InitializeComponent();

            SetLTR();
            viewModel = App.Locator.ZatcaInfoMenuPageView;
            BindingContext = viewModel;

            viewModel.setMenuTab();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;


            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;


                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();



            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }

        protected override bool OnBackButtonPressed()
        {
            GoToBackStep();
            return true;
        }
        private void OnBackArrowTapped(object sender, EventArgs e)
        {

                GoToBackStep();
        }

        public void GoToBackStep()
        {

            viewModel.IsLoading = false;
            viewModel.ChcekCurrentTabCustoms();
            
        }


      
        private void OnMenu1Tapped(object sender, EventArgs e)
        {
            viewModel.setMenu1Tab();
            //var browser = new WebView();
            //var htmlSource = new HtmlWebViewSource();

            if (App.IsArabic)
            {

                CustomsTraffis.Source = Constants.ZAtcaCustomsTarrifsAr;
            }
            else
            {
                CustomsTraffis.Source = Constants.ZAtcaCustomsTarrifsAr;
                // CustomsTraffis.Source = Constants.ZAtcaCustomsTarrifsEN;
            }
        }

        private void OnMenu2Tapped(object sender, EventArgs e)
        {
            viewModel.setMenu2Tab();
            //var browser = new WebView();
            //var htmlSource = new HtmlWebViewSource();

            if (App.IsArabic)
            {

                CustomsView.Source = Constants.ZAtcaCustomsdeclarationsAr;
            }
            else
            {
                CustomsView.Source = Constants.ZAtcaCustomsdeclarationsAr;
                // CustomsView.Source = Constants.ZAtcaCustomsdeclarationsEN;
            }
        }

        private void CustomsView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Contains(Constants.ZAtcaCustomsdeclarationsAr) || e.Url.Contains(Constants.ZAtcaCustomsdeclarationsEN))
            {
                viewModel.IsLoading = false;
            }
            else
            {
                viewModel.IsLoading = true;
            }
        }

        private void CustomsView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }


        private void CustomsTraffis_Navigating(object sender, WebNavigatingEventArgs er)
        {
            if (er.Url.Contains(Constants.ZAtcaCustomsTarrifsAr) || er.Url.Contains(Constants.ZAtcaCustomsTarrifsEN))
            {
                viewModel.IsLoading = false;
            }
            else
            {
                viewModel.IsLoading = true;
            }
        }

        private void CustomsTraffis_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }

    }
}
