using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Platforms.Android.DependencyServices;

[assembly: Dependency(typeof(ForceUpdate))]
namespace ZATCAMAUI.Platforms.Android.DependencyServices
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
            FirebaseRemoteConfigSettings configSettings = new FirebaseRemoteConfigSettings.Builder()
               .SetDeveloperModeEnabled(false)
               .Build();
            FirebaseRemoteConfig.Instance.SetConfigSettings(configSettings);

            Dictionary<string, Java.Lang.Object> dic = new Dictionary<string, Java.Lang.Object>
            {
                { "IsForceUpdate", false },
                 { "BuildNumber_Android", string.Empty },
                { "BuildNumber_iOS",  string.Empty },

            };

            FirebaseRemoteConfig.Instance.SetDefaults(dic);
        }

        public async Task FetchAndActivateAsync()
        {
            //Fetch remote values
            await FirebaseRemoteConfig.Instance.FetchAsync(0);

            //Activate new values
            FirebaseRemoteConfig.Instance.ActivateFetched();
        }



        public string GetValue(string key)
        {
            var settings = FirebaseRemoteConfig.Instance.GetString(key);
            return settings;
        }

    }
}
