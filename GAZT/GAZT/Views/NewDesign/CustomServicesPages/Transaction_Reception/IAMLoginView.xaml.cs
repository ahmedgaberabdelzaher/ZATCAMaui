using System;
using System.Collections.Generic;
using EGAZT.Helper;
using System.Net.Http;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception
{
    public partial class IAMLoginView : BaseContentPage
    {
        IAMLoginViewModel viewModel;
        public IAMLoginView(int commingFrom)
        {
             viewModel = App.Locator.IAMLoginViewModel;
            viewModel.CommingFrom = commingFrom;
            BindingContext = viewModel;
            InitializeComponent();
        }

        void WebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            if (e.Url.ToLower().Contains("result?"))
            {
                viewModel.GetIAMToken(e.Url);
            }
        }
        private async void GetCookies(string url)
        {
            var uri = new Uri(url);
            var handler = new HttpClientHandler();
            IAMWebView.Cookies = handler.CookieContainer;
            HttpClient client = new HttpClient(handler);
          var data=  await client.GetAsync(uri);
        }
    }
}

