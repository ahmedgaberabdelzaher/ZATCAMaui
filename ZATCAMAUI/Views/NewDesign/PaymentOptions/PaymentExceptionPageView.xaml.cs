using Mopups.Pages;
using Mopups.Services;
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

                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    platform = "C4";
                }
                else if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    platform = "C3";
                }
                var CancelAPI = await WebServiceManager.GAZTCancelPayment(App.PaymentGuid, platform);

                //    MainThread.BeginInvokeOnMainThread(() =>
                //{
                //    _navigationService.GoBack();
                //});


            }
            catch (GAZTValidatePaymentInProcessException )
            {

            }
            catch (InternetException )
            {

            }

        }

        async void TryAgainClicked(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync();
        }
    }
}