using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    public class CustomWebview : WebView
    {
        Action<string> action;

        public static readonly BindableProperty UriProperty = BindableProperty.Create(
            propertyName: "Uri",
            returnType: typeof(string),
            declaringType: typeof(CustomWebview),
            defaultValue: default(string));

        public static readonly BindableProperty CookiesListProperty = BindableProperty.Create(
      propertyName: "Cookies",
          returnType: typeof(CookieContainer),
          declaringType: typeof(CustomWebview),
        defaultValue: default(string));
        public static BindableProperty RefreshCommandProperty =
        BindableProperty.Create(nameof(RefreshCommand), typeof(Action), typeof(CustomWebview), null, BindingMode.OneWayToSource);


        public Action RefreshCommand
        {
            get { return (Action)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }

        public CookieContainer CookiesList
        {
            get { return (CookieContainer)GetValue(CookiesProperty); }
            set { SetValue(CookiesProperty, value); }
        }
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }


        public CustomWebview()
        {
            Cookies = new CookieContainer();
        }

        public Action<string> CookieRetrieved { get; set; }

        public void RegisterAction(Action<string> callback)
        {
            CookieRetrieved = callback;
        }

        public void InvokeAction(string data)
        {
            if (CookieRetrieved == null || data == null)
            {
                return;
            }

            CookieRetrieved.Invoke(data);
        }

        public void Cleanup()
        {
            action = null;
        }

       
    }
}
