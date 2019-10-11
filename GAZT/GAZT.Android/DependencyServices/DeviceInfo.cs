
using GAZT.Droid.DependencyServices;
using Xamarin.Forms;
using GAZT.Helper;
[assembly: Dependency(typeof(DeviceInfo))]
namespace GAZT.Droid.DependencyServices
{
    public class DeviceInfo : IDeviceInfo
    {
        public double GetDeviceHeight()
        {
            double height = 0;
            height = (double)Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.HeightPixels / (double)Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density;
            return height;
        }

        public double GetDeviceWidth()
        {
            double width = 0;
            width = (double)Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.WidthPixels / (double)Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density;

            return width;
        }
    }
}
