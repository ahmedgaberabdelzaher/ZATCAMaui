
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerProfile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerSubsidyRequest : ContentPage
    {
        #region Variable
        TaxpayerSubsidyViewModel viewModel;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public TaxpayerSubsidyRequest()
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.TaxpayerSubsidyRequest;
                On<iOS>().SetUseSafeArea(true);
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;
                ChangeAeroIcon();
                BindingContext = viewModel;
                viewModel.WebUrl = ZATCAConstants.TaxpayerSubsidyRequest;

            }
            catch (Exception)
            {
            }
        }
        #endregion
        #region Method
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;


        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (SubsidyWebView.CanGoBack)
            {
                SubsidyWebView.GoBack();
            }
            else
            {
                viewModel._navigationService.GoBack();
            }
        }

        void SubsidyWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        void SubsidyWebView_Navigated(object sender,WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
        #endregion
    }
}