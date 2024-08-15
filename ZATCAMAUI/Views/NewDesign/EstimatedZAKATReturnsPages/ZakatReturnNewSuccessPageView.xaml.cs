

using Mopups.Services;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnNewSuccessPageView : ContentPage
    {
        ZAKATReturnDetailsViewModel viewModel;

        public ZakatReturnNewSuccessPageView(PaymentSucess paymentInfo)
        {
            InitializeComponent();
            viewModel = App.Locator.ZAKATReturnDetailsSuccessView;

            BindingContext = viewModel;

            viewModel.ReferenceNumber = paymentInfo.Paymentref;
            if (paymentInfo.Period != null)
            {
                viewModel.TaxablePeriod = paymentInfo.Period;
            }
            else
            {

                viewModel.TaxablePeriod = viewModel.ZakatReturnDetails.d.Persl;
            }




        }

        private void GoToDashboardClicked(object sender, EventArgs e)
        {
            var _navigation = Application.Current.MainPage.Navigation;

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ZAKATReturnDetailsView)
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
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ZakatReturnDetailsSuccessfullPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ZakatReturnNewSuccessPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            viewModel._navigationService.GoBack();
        }

        public async void OnCopyReferenceNumberButtonClicked(object sender, EventArgs args)
        {
            await Clipboard.SetTextAsync(viewModel.ReferenceNumber);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDReferenceNumber + " " + text));


            }
        }

    }
}