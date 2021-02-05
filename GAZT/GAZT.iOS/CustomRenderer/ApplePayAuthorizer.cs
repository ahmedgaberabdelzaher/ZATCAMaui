using System;
using System.Linq;
using EGAZT.Helper;
using Foundation;
using GAZT.iOS.CustomRenderer;
using PassKit;
using UIKit;
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

        public bool AuthorizePayment(string Amount , string Title)
        {
            NSString[] paymentNetworks = new NSString[] { PKPaymentNetwork.Visa,
                PKPaymentNetwork.MasterCard, PKPaymentNetwork.Mada };
            var merchantID = "merchant.gazt.egazt";
            // Enter merchant ID registered in apple.developer.com

            PKPaymentRequest paymentRequest = new PKPaymentRequest();
            paymentRequest.MerchantIdentifier = merchantID;
            paymentRequest.SupportedNetworks = paymentNetworks;
            paymentRequest.MerchantCapabilities = PKMerchantCapability.ThreeDS;
            paymentRequest.CountryCode = "US";
            paymentRequest.CurrencyCode = "USD";

            paymentRequest.PaymentSummaryItems = new PKPaymentSummaryItem[]{
                   new PKPaymentSummaryItem(){
            Label = Title , Amount = new NSDecimalNumber(Amount)
                   }

            };

            PKPaymentAuthorizationViewController controller = new
                  PKPaymentAuthorizationViewController(paymentRequest);
            controller.Delegate = (PassKit.IPKPaymentAuthorizationViewControllerDelegate)Self;
            

            var rootController = GetTopViewController();


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

            completion(PKPaymentAuthorizationStatus.Success);
        }


        public override void PaymentAuthorizationViewControllerDidFinish(PKPaymentAuthorizationViewController controller)
        {
            controller.DismissViewController(true, null);

        }

        public override void WillAuthorizePayment(PKPaymentAuthorizationViewController controller)
        {
            throw new NotImplementedException();
        }
    }
}

