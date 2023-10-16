using System;
using EGAZT.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using Xamarin.Forms;
using GAZT.iOS.DependencyServices;

[assembly: Dependency(typeof(ForceUpdate))]

namespace GAZT.iOS.DependencyServices
{
    // REF: https://mookiefumi.com/2019-12-17-firebase-remote-config-in-xamarin-forms
    public class ForceUpdate : IForceUpdate
    {
        public ForceUpdate()
        {

            SetDefault();
        }

        private void SetDefault()
        {

            Dictionary<object, object> dic = new Dictionary<object, object>
            {
                { "IsForceUpdate", false },
                { "BuildNumber_Android", string.Empty },
                { "BuildNumber_iOS",  string.Empty },

            };

            RemoteConfig.SharedInstance.SetDefaults(dic);
            RemoteConfig.SharedInstance.ConfigSettings = new RemoteConfigSettings(true);
        }


        public async Task FetchAndActivateAsync()
        {
           
            try
            {
                var status = await RemoteConfig.SharedInstance.FetchAsync(0.0d);
                if (status == RemoteConfigFetchStatus.Success)
                {
                     RemoteConfig.SharedInstance.ActivateFetched();
                }
            }
            catch (Exception)
            {
               
            }
            
        }

        public string GetValue(string key)
        {
            var settings = RemoteConfig.SharedInstance[key].StringValue;
            return settings;
        }

    }
}

