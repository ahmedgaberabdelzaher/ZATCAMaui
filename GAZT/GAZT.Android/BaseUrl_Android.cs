//using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using EGAZT.Views.NewDesign.TaxpayerCorrespondancePages;
using GAZT.Droid;
using Xamarin.Forms;
[assembly: Dependency(typeof(BaseUrl_Android))]
namespace GAZT.Droid
{
    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}