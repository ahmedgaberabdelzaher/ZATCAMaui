
using Mopups.Services;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReturnNewSuccessPageView : ContentPage
    {
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel viewModel;
        public VatReturnNewSuccessPageView(PaymentSucess paymentInfo)
        {
            InitializeComponent();

            viewModel = App.Locator.GAZTNewDesignVATReturnUpdatedUIPageView;



            viewModel.ReferenceNumber = paymentInfo.Paymentref;
            if (paymentInfo.Period != null)
            {
                viewModel.TaxablePeriod = paymentInfo.Period;
            }
            else
            {
                viewModel.TaxablePeriod = viewModel.VATDeclarationData.data.Persl;
            }

            BindingContext = viewModel;

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

        private void GotodashboardClicked(object sender, EventArgs e)
        {

            var _navigation = Application.Current.MainPage.Navigation;


            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.GAZTNewDesignMyReturnsNewPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.GAZTNewDesignVATReturnUpdatedUIPageView)
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
                if (item.GetType().Name == App.VATReturnSuccessfullPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReturnNewSuccessPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
        }
    }
}