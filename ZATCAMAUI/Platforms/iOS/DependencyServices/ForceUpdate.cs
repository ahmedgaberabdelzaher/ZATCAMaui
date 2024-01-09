using Firebase.RemoteConfig;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Platforms.iOS.DependencyServices;
[assembly: Dependency(typeof(ForceUpdate))]
namespace ZATCAMAUI.Platforms.iOS.DependencyServices
{
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
            RemoteConfig.SharedInstance.ConfigSettings = new RemoteConfigSettings();
        }


        public async Task FetchAndActivateAsync()
        {

            try
            {
                var status = await RemoteConfig.SharedInstance.FetchAsync(0.0d);
                if (status == RemoteConfigFetchStatus.Success)
                {
                    //RemoteConfig.SharedInstance.FetchAndActivate();
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
