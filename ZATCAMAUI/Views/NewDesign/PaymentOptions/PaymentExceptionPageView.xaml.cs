using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;

namespace ZATCAMAUI.Views.NewDesign.PaymentOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class PaymentExceptionPageView : PopupPage
    {




        public PaymentExceptionPageView()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await CancelPaymentService();

        }

        public async Task CancelPaymentService()
        {
            try
            {


                var platform = "";

                if (Device.RuntimePlatform == Device.iOS)
                {
                    platform = "C4";
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    platform = "C3";
                }
                var CancelAPI = await WebServiceManager.GAZTCancelPayment(App.PaymentGuid, platform);

                //    Device.BeginInvokeOnMainThread(() =>
                //{
                //    _navigationService.GoBack();
                //});


            }
            catch (GAZTValidatePaymentInProcessException ex)
            {

            }
            catch (InternetException ex)
            {

            }

        }

        async void TryAgainClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}