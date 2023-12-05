using Android.App;
using Android.Content;
using Android.Widget;
namespace GAZT.PUSH_Notification
{
    [BroadcastReceiver]
    public class NotificationResponse : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            int notificationId = intent.GetIntExtra("notificationId", 0);
            NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            if (intent.Action.Equals("Okay"))
            {
                manager.Cancel(notificationId);
                Toast.MakeText(context, "Received intent Okay! ", ToastLength.Short).Show();
            }
            else
            {
                manager.Cancel(notificationId);
                Toast.MakeText(context, "Received intent Cancel! ", ToastLength.Short).Show();
            }
        }
    }
}