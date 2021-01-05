using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT
{
    [Preserve(AllMembers = true)]
    public class HybridWebView : WebView
    {
        public static readonly BindableProperty UrlProperty = BindableProperty.Create(
        propertyName: "Url",
        returnType: typeof(string),
        declaringType: typeof(HybridWebView),
        defaultValue: default(string));

        public static readonly BindableProperty CookiesListProperty = BindableProperty.Create(
       propertyName: "Cookies",
           returnType: typeof(CookieContainer),
           declaringType: typeof(HybridWebView),
         defaultValue: default(string));
        public static BindableProperty RefreshCommandProperty =
        BindableProperty.Create(nameof(RefreshCommand), typeof(Action), typeof(HybridWebView), null, BindingMode.OneWayToSource);

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

        public string Url
        {
            get
            {
                return (string)GetValue(UrlProperty);
            }
            set
            {

                SetValue(UrlProperty, value);
            }
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
    }
}
