using System;
using System.Linq;
using EGAZT;
using EGAZT.Helper;
using EGAZT.Models.PaymentModel;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using Foundation;
using GAZT.iOS.CustomRenderer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PassKit;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ApplePayAuthorizer))]

namespace GAZT.iOS.CustomRenderer
{

    [Preserve(AllMembers = true)]
    public class ApplePayAuthorizer : PKPaymentAuthorizationViewControllerDelegate,IApplePayAuthorizer
    {
        void HandleAction()
        {
        }

        public ApplePayAuthorizer()
        {
        }

        private bool IsSucess;

        public bool AuthorizePayment(string Amount , string Title)
        {
            NSString[] paymentNetworks = new NSString[] {PKPaymentNetwork.Mada};
            var merchantID = "merchant.gazt.egazt";
            // Enter merchant ID registered in apple.developer.com

            PKPaymentRequest paymentRequest = new PKPaymentRequest();
            paymentRequest.MerchantIdentifier = merchantID;
            paymentRequest.SupportedNetworks = paymentNetworks;
            paymentRequest.MerchantCapabilities = PKMerchantCapability.ThreeDS;
            paymentRequest.CountryCode = "SA";
            paymentRequest.CurrencyCode = "SAR";

            paymentRequest.PaymentSummaryItems = new PKPaymentSummaryItem[]{
                   new PKPaymentSummaryItem(){
            Label = Title , Amount = new NSDecimalNumber(Amount)
                   }

            };

            PKPaymentAuthorizationViewController controller = new
                  PKPaymentAuthorizationViewController(paymentRequest);
            controller.Delegate = (PassKit.IPKPaymentAuthorizationViewControllerDelegate)Self;

            var rootController = UIApplication.SharedApplication.
                     KeyWindow.RootViewController;
            //var rootController = GetTopViewController();


            rootController.PresentViewController(controller,
                  true, null);
            return false;
        }

        public static UIViewController GetTopViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;

            if (vc is UINavigationController navController)
                vc = navController.ViewControllers.Last();

            return vc;
        }

        public override void DidAuthorizePayment(PKPaymentAuthorizationViewController controller, PKPayment payment, Action<PKPaymentAuthorizationStatus> completion)
        {

            
            completion(obj: PKPaymentAuthorizationStatus.Success);
            //var paymentToken = payment.Token.PaymentData;

            if(payment.Token != null) {

                JObject json = JObject.Parse(payment.Token.PaymentData.ToString());
                string paymentToken = json.SelectToken("data").ToString();

                //Preferences.Get("ApplePayString", paymentToken);
                MessagingCenter.Send((App)Xamarin.Forms.Application.Current, "ApplePayData", paymentToken);
            }


           



            //completion(PKPaymentAuthorizationStatus.Success);
        }


        public override void PaymentAuthorizationViewControllerDidFinish(PKPaymentAuthorizationViewController controller)
        {
            controller.DismissViewController(true, null);

        }

        public override void WillAuthorizePayment(PKPaymentAuthorizationViewController controller)
        {
            
        }
    }
}

