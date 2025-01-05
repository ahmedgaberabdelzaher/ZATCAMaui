using Foundation;
using WebKit; 
namespace ZATCAMAUI.Platforms.iOS.Helper
{
    // Self signed Certificate 
    public class WebViewDelegate : WKNavigationDelegate, INSUrlConnectionDataDelegate
    {
        public override void DidReceiveAuthenticationChallenge(WKWebView webView, NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler)
        {
            completionHandler(NSUrlSessionAuthChallengeDisposition.UseCredential, new NSUrlCredential(challenge.ProtectionSpace.ServerSecTrust));

            return;
        }
    }
}

