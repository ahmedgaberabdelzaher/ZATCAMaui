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
            //   string source = "https://vatapislb.zatca.gov.sa/Home/index?arabicFamilyName=%d8%b9%d9%84%d9%8a&englishFatherName=Mohammed&arabicName=%d8%ae%d8%a7%d9%84%d8%af%20%d9%85%d8%ad%d9%85%d8%af%20%d9%81%d9%87%d8%af%20%d8%b9%d9%84%d9%8a&iqamaExpiryDateHijri=1448/09/11&englishFirstName=Khaled&gender=Male&nationality=Egypt&arabicGrandFatherName=%d9%81%d9%87%d8%af&arabicFatherName=%d9%85%d8%ad%d9%85%d8%af&englishName=Khaled%20Mohammed%20Fahed%20Ali&englishGrandFatherName=Fahed&IdNo=2436933796&IdType=3&arabicFirstName=%d9%85%d8%ad%d9%85%d9%88%d8%af&idExpiryDateHijri=1448/09/11&nationalityCode=207&cardIssueDateHijri=1439/02/27&dob=Mon%20Dec%2021%2000:00:00%20AST%201981&englishFamilyName=Ali&dobHijri=1402/02/23&";
            string source = "https://vatapislb.zatca.gov.sa/Home/index?arabicFamilyName=%D8%A7%D9%84%D8%B1%D9%81%D8%A7%D8%B9%D9%8A&englishFatherName=Ali&arabicName=%D8%AD%D8%B3%D8%A7%D9%85%20%D8%B9%D9%84%D9%8A%20%D8%B9%D9%88%D9%8A%D8%B6%20%D8%A7%D9%84%D8%B1%D9%81%D8%A7%D8%B9%D9%8A&iqamaExpiryDateHijri=1448/09/11&englishFirstName=Hosam&gender=Male&nationality=Kingdom%20of%20Saudi%20Arabia&arabicGrandFatherName=%D8%B9%D9%88%D9%8A%D8%B6&arabicFatherName=%D8%B9%D9%84%D9%8A&englishName=Hosam%20Ali%20Owaiedh%20Alrefaai&englishGrandFatherName=Owaiedh&IdNo=1031164450&IdType=1&arabicFirstName=%D8%AD%D8%B3%D8%A7%D9%85&idExpiryDateHijri=1448/09/11&nationalityCode=113&cardIssueDateHijri=1439/02/27&dob=Tue%20Apr%2023%2000:00:00%20AST%201985&englishFamilyName=Alrefaai&dobHijri=1405/08/03&";
          //  viewModel.IAMWbViewSrc = source;
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

