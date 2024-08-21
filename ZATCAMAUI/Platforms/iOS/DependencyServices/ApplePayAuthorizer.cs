

using Foundation;
using PassKit;
using UIKit;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Platforms.iOS.DependencyServices;

[assembly: Dependency(typeof(ApplePayAuthorizer))]
namespace ZATCAMAUI.Platforms.iOS.DependencyServices
{
    public class ApplePayAuthorizer : PKPaymentAuthorizationViewControllerDelegate, IApplePayAuthorizer
    {
        void HandleAction()
        {
        }

        public ApplePayAuthorizer()
        {
        }

        private bool IsSucess;
        private bool DashboardFlag;


        public bool AuthorizePayment(double Amount, string Title)
        {
            NSString[] paymentNetworks = new NSString[] { PKPaymentNetwork.Mada };
            // var merchantID = "merchant.com.gazt.enterprise.egazt";
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

            if (payment.Token != null)
            {

                string paymentData = payment.Token.PaymentData.ToString();

                if (DashboardFlag)
                {

                    MessagingCenter.Send((App)Application.Current, "DashboardApplePayData", paymentData);

                }
                else
                {
                    MessagingCenter.Send((App)Application.Current, "ApplePayData", paymentData);

                }


            }
        }


        public override void PaymentAuthorizationViewControllerDidFinish(PKPaymentAuthorizationViewController controller)
        {
            controller.DismissViewController(true, null);

        }

        public override void WillAuthorizePayment(PKPaymentAuthorizationViewController controller)
        {

        }

        public bool IsPaymentFromDashboard(bool isDahboard)
        {
            DashboardFlag = isDahboard;
            return isDahboard;
        }
    }
}
