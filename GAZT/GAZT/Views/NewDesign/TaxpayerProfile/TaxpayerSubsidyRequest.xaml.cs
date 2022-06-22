using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Helper;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [Preserve(AllMembers = true)]
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
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;
                ChangeAeroIcon();
                SetLTR();
                this.BindingContext = viewModel;
                viewModel.WebUrl = Constants.TaxpayerSubsidyRequest;
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        #region Method
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
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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

        void SubsidyWebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        void SubsidyWebView_Navigated(System.Object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
                 viewModel.IsLoading = false;
        }
        #endregion
    }
}