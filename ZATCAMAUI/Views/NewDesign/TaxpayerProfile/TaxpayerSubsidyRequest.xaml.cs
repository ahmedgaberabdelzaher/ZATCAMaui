using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerProfile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerSubsidyRequest : BaseContentPage
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
                BindingContext = viewModel;
                viewModel.WebUrl = ZATCAConstants.TaxpayerSubsidyRequest;

            }
            catch (Exception)
            {
            }
        }
        #endregion
        #region Method
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