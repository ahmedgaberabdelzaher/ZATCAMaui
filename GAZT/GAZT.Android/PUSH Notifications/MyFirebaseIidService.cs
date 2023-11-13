using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Iid;
namespace GAZT.PUSH_Notification
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    class MyFirebaseIIdService: FirebaseInstanceIdService 
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
           //App.token = refreshedToken;
            SendRegistrationToserver(refreshedToken);          
        }
        async void SendRegistrationToserver(string token)
        {
            
        } 
    }
}