using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Helper;
using Firebase.RemoteConfig;
using GAZT.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(ForceUpdate))]
namespace GAZT.Droid.DependencyServices
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
