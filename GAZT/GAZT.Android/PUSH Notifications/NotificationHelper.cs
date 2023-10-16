using Android.App;
using Android.Content;
using Android.Support.V4.App;
using GAZT.Droid;
using GAZT.PUSH_Notification;
using System;
using Xamarin.Forms;
[assembly: Dependency(typeof(NotificationHelper))]
namespace GAZT.PUSH_Notification
{
    class NotificationHelper : INotification
    {
        private Context mContext;
        private NotificationCompat.Builder mBuilder;
        public static string NOTIFICATION_CHANNEL_ID = "10023";
        public NotificationHelper()
        {
            mContext = global::Android.App.Application.Context;
        }
        public void CreateNotification(string title, string message)
        {
            try
            {
                mBuilder = new NotificationCompat.Builder(mContext);
                mBuilder.SetSmallIcon(Resource.Drawable.icon);
                var notificationResponse = new NotificationResponse();
                var intent = new Intent(mContext, typeof(MainActivity));
                //Prerequistics of action buttons
                var actionintent1 = new Intent();
                actionintent1.SetAction("Cancel");
                var pintent1 = PendingIntent.GetBroadcast(mContext, 0, actionintent1, PendingIntentFlags.UpdateCurrent);
                var actionintent2 = new Intent();
                actionintent2.SetAction("Okay");
                var pintent2 = PendingIntent.GetBroadcast(mContext, 0, actionintent2, PendingIntentFlags.UpdateCurrent);
                //Notification Building
                mBuilder.SetSmallIcon(Resource.Drawable.notification_icon_background);
                var pendingIntent = PendingIntent.GetActivity(mContext, 0, intent, PendingIntentFlags.OneShot);
                mBuilder.SetContentTitle(title)
                    .SetAutoCancel(true)
                    .SetContentText(message)
                    //.SetStyle( new NotificationCompat.BigPictureStyle().BigPicture(webImage))
                    .SetChannelId(NOTIFICATION_CHANNEL_ID)
                    .SetPriority((int)NotificationPriority.High)
                    .SetContentIntent(pendingIntent)
                    .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                    //Add Action Buttons
                    .AddAction(Resource.Drawable.notification_bg_normal, "Cancel", pintent1)
                    .AddAction(Resource.Drawable.notification_bg_normal, "Okay", pintent2)
                    .SetVisibility((int)NotificationVisibility.Public);
                //Action Button Intents
                var intentFilter = new IntentFilter();
                intentFilter.AddAction("Cancel");
                intentFilter.AddAction("Okay");
                mContext.RegisterReceiver(notificationResponse, intentFilter);
                NotificationManager notificationManager = (NotificationManager)mContext.GetSystemService(Context.NotificationService);
                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                {
                    NotificationImportance importance = global::Android.App.NotificationImportance.High;
                    NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, title, importance);
                    notificationChannel.EnableLights(true);
                    notificationChannel.EnableVibration(true);
                    notificationChannel.SetShowBadge(true);
                    notificationChannel.Importance = importance;
                    if (notificationManager != null)
                    {
                        mBuilder.SetChannelId(NOTIFICATION_CHANNEL_ID);
                        notificationManager.CreateNotificationChannel(notificationChannel);
                    }
                }
                //publish notification
                notificationManager.Notify(0, mBuilder.Build());
            }
            catch (Exception) { }
        }
    }
}