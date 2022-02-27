using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    public partial class TaxpayerSubsidyRequest : ContentPage
    {
        TaxpayerSubsidyViewModel viewModel;

        public TaxpayerSubsidyRequest()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayerSubsidyRequest;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            //  SetLanguage();
            loadingIndicator.IsVisible = true;
            SetLanguage();
        }

        void SetLanguage()
        {
            taxEvasionWebView.Source = Constants.TaxpayerSubsidyRequest;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void TOnBackButtonClicked(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        private void TaxPayerSubsidyWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Task.Run(async () =>
            {
                loadingIndicator.IsVisible = false;
            });
        }

        private void TaxPayerSubsidyWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Task.Run(async () =>
            {
                loadingIndicator.IsVisible = false;
            });
        }
    }

    public class TaxpayerSubsidyViewModel : BaseViewModel
    {
        public string URI { get; set; }
        public TaxpayerSubsidyViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }
    }
}
