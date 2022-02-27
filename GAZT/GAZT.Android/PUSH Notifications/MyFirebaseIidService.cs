using System;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Iid;
using Newtonsoft.Json;
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
            System.Console.WriteLine("Debug: Refreshed Token: " + refreshedToken);
            SendRegistrationToserver(refreshedToken);          
        }
        async void SendRegistrationToserver(string token)
        {
            //try
            //{
            //    var refreshedToken = FirebaseInstanceId.Instance.Token;
            //    Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            //    HttpClient client = new HttpClient();
            //    string uri = "https://192.168.0.103:44305/api/TokenRegistration?registrationToken=" + token;
            //    //var json = JsonConvert.SerializeObject(token);
            //    //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //    //var response = await client.PostAsync(uri, content);
            //    var response = await client.GetAsync(uri);
            //    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            //        System.Console.WriteLine("Error 404");
            //    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            //        System.Console.WriteLine("Error: Bad Request");
            //    else
            //        System.Console.WriteLine("Another Error");
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine("Exception: " + e.Message);
            //}
        } 
    }
}