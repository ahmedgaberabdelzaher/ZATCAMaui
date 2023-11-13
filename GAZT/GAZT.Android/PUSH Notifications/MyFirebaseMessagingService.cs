using Android.App;
using Android.Content;
using Firebase.Iid;
using Firebase.Messaging;
namespace GAZT.PUSH_Notification
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class MyFirebaseMessagingService:FirebaseMessagingService
    { 
        public MyFirebaseMessagingService()
        {
        }
        public override void OnMessageReceived(RemoteMessage message)
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            base.OnMessageReceived(message);
            var title = message.Data["Title"];
            var body = message.Data["Body"];
            new NotificationHelper().CreateNotification(title,body);
        }
    }
}