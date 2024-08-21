
using Foundation;
using ZATCAMAUI.Platforms.iOS.DependencyServices;
using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;

[assembly: Dependency(typeof(BaseUrl_iOS))]
namespace ZATCAMAUI.Platforms.iOS.DependencyServices
{
    public class BaseUrl_iOS : IBaseUrl
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}
