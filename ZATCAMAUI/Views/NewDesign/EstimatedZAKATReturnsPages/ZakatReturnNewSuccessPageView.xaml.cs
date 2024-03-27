

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
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

            // Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);

            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            ChangeAeroIcon();

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
        private async void OnRefreshButtonClicked(object sender, EventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDReferenceNumber + " " + text));

               
            }
        }

    }
}