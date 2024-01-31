using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common
{
    public partial class PaymentWebView : BaseContentPage
    {
        CustomsPaymentViewModel viewModel;
        public PaymentWebView(string paymentCode)
        {
            viewModel = App.Locator.CustomsPaymentViewModel;
            viewModel.PaymentCode = paymentCode;
            BindingContext = viewModel;

            InitializeComponent();


        }

        void PaymentView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Contains("EDeclarationPages/EDeclarationStartPage.aspx"))
            {
                viewModel._navigationService.NavigateTo("/Home", "0");

            }
        }
    }
}

