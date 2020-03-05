using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Foundation;
using UIKit;
using GAZT.Views.NewViews;
using GAZT.iOS;

[assembly: Dependency(typeof(BaseUrl_iOS))]
namespace GAZT.iOS
{
	public class BaseUrl_iOS : IBaseUrl
	{
		public string Get()
		{
			return NSBundle.MainBundle.BundlePath;
		}
	}
}