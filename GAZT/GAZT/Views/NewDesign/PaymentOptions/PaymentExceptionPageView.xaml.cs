using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.PaymentOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
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