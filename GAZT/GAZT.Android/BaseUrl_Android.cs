using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GAZT.Droid;
using GAZT.Views.NewViews;

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