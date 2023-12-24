using ZATCAMAUI.Platforms.Android.DependencyServices;
using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace ZATCAMAUI.Platforms.Android.DependencyServices
{
    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}
