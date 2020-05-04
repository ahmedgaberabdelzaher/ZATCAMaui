using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using GAZT.Helper;
using GAZT.iOS.DependencyServices;
using Xamarin.Forms;
[assembly: Dependency(typeof(DeviceInfo))]
namespace GAZT.iOS.DependencyServices
{
    public class DeviceInfo : IDeviceInfo
    {
        public double GetDeviceHeight()
        {
            double height = 0;
            height = (double)UIScreen.MainScreen.Bounds.Height;
            return height;
        }
        public double GetDeviceWidth()
        {
            double width = 0;
            width = (double)UIScreen.MainScreen.Bounds.Width;
            return width;
        }
    }
}
