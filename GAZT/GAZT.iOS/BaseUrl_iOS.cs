using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Foundation;
using UIKit;
using GAZT.iOS;
//using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using EGAZT.Views.NewDesign.TaxpayerCorrespondancePages;
[assembly: Dependency(typeof(BaseUrl_iOS))]
namespace GAZT.iOS
{
	[Xamarin.Forms.Internals.Preserve(AllMembers = true)]
	public class BaseUrl_iOS : IBaseUrl
	{
		public string Get()
		{
			return NSBundle.MainBundle.BundlePath;
		}
	}


}