
using Mopups.Services;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.MyBillsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBillsSuccessPageView : ContentPage
    {
        GAZTNewDesignMyBillsPageViewModel viewModel;
        GAZTNewDesignDashBoardPageViewModel _dashBoardPageViewModel;
        private bool isDashboard = true;
        private string refNum;
        public MyBillsSuccessPageView(PaymentSucess paymentInfo)
        {
            InitializeComponent();
            refNum = refNum;

            foreach (var item in Application.Current.MainPage.Navigation.NavigationStack)
            {
                if (item.GetType().Name == App.GAZTNewDesignMyBillsPageView)
                {
                    isDashboard = false;
                    break;
                }
            }

            if (!isDashboard)
            {
                viewModel = App.Locator.GAZTNewDesignMyBillsPageView;

                viewModel.ReferenceNumber = paymentInfo.Paymentref;
                viewModel.TaxablePeriod = !string.IsNullOrEmpty(paymentInfo.Period) ? paymentInfo.Period : "N/A";


                BindingContext = viewModel;
            }
            else
            {
                _dashBoardPageViewModel = App.Locator.GAZTNewDesignDashBoardPageView;

                _dashBoardPageViewModel.ReferenceNumber = paymentInfo.Paymentref;
                _dashBoardPageViewModel.TaxablePeriod = !string.IsNullOrEmpty(paymentInfo.Period) ? paymentInfo.Period : "N/A";

                BindingContext = _dashBoardPageViewModel;
            }
        }

        private async void OnCopyReferenceNumberButtonClicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(viewModel.ReferenceNumber);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDReferenceNumber + " " + text));

                // await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);
            }
        }

        private void GotodashboardClicked(object sender, EventArgs e)
        {

            App.isMybillsRefresh = true;

            var _navigation = Application.Current.MainPage.Navigation;

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.GAZTNewDesignMyBillsPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.PaymentProcessWebview)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
            if (isDashboard)
            {

                _dashBoardPageViewModel._navigationService.GoBack();
            }
            else
            {


                viewModel._navigationService.GoBack();
            }



        }


    }
}