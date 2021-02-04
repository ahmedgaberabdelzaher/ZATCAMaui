using System;
using applepay.iOS;
using Foundation;
using ObjCRuntime;
using PassKit;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ApplePayAuthorizer))]
namespace applepay.iOS
{
    public class ApplePayAuthorizer :
          PKPaymentAuthorizationViewControllerDelegate,
          IApplePayAuthorizer
     {
        void HandleAction()
        {
        }

        public ApplePayAuthorizer()
       {
      }

       public bool AuthorizePayment()
       {
          NSString[] paymentNetworks = new NSString[] { PKPaymentNetwork.Visa,
                PKPaymentNetwork.MasterCard, PKPaymentNetwork.Mada };
          var merchantID = "merchant.com.swayam.applepay";
                // Enter merchant ID registered in apple.developer.com

             PKPaymentRequest paymentRequest = new PKPaymentRequest();
             paymentRequest.MerchantIdentifier = merchantID;
             paymentRequest.SupportedNetworks = paymentNetworks;
             paymentRequest.MerchantCapabilities = PKMerchantCapability.ThreeDS;
             paymentRequest.CountryCode = "US";
             paymentRequest.CurrencyCode = "USD";

             paymentRequest.PaymentSummaryItems = new PKPaymentSummaryItem[]{
                   new PKPaymentSummaryItem()
        {
            Label = "Sample Purchase Item" ,
                      Amount = new NSDecimalNumber("100")
                   }
    };

             PKPaymentAuthorizationViewController controller = new
                   PKPaymentAuthorizationViewController(paymentRequest);
             controller.Delegate = (PassKit.IPKPaymentAuthorizationViewControllerDelegate) Self;
             var rootController = UIApplication.SharedApplication.
                   KeyWindow.RootViewController;
             rootController.PresentViewController(controller,
                   true, null);
             return false;
          }

        public override void DidAuthorizePayment(PKPaymentAuthorizationViewController controller, PKPayment payment,Action<PKPaymentAuthorizationStatus> completion)
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
