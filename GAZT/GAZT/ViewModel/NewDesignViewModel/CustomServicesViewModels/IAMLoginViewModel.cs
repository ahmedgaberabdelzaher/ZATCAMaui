using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Input;
using EGAZT.AppConfigurations;
using EGAZT.Converters;
using EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception;
using EGAZT.Views.NewDesign.EDeclaration;
using EGAZT.Views.NewDesign.MyReports;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using ZXing.Aztec.Internal;

namespace EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class IAMLoginViewModel: BaseViewModel
    {
        string iAMWbViewSrc;
        public string IAMWbViewSrc { get { return iAMWbViewSrc; } set { iAMWbViewSrc = value; RaisePropertyChanged(); } }
        public int CommingFrom { get; set; }

        string priceText;
        public string PriceText { get { return priceText; } set { priceText = value; RaisePropertyChanged(); } }

        bool isNoUserShowMsg;
        public bool IsNoUserShowMsg { get { return isNoUserShowMsg; } set { isNoUserShowMsg = value; RaisePropertyChanged(); } }


        public IAMLoginViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            IAMWbViewSrc = PageSettings.IAMLoginBaseUrl;
        }

        public ICommand OpenIAMRegistrationUrlCommand
        {
            get
            {
                return new Command(() =>
                {

                    var url = PageSettings.IAMRegistration;
                    Xamarin.Essentials.Launcher.OpenAsync(url);
                    IsNoUserShowMsg = false;
                    _navigationService.GoBack();
                   
                });
            }
        }

        public void GetIAMToken(string url)
        {
            string token = HttpUtility.ParseQueryString(new Uri(url).Query).Get("token");
            if (token == "UserNotFound")
            {
                IsNoUserShowMsg = true;
                MessageTxt = AppResources.IAMUsernNotFoundMSg;
                IAMWbViewSrc = PageSettings.IAMLoginBaseUrl;
                return;
            }
            var navigation = Application.Current.MainPage.Navigation;
            var currentPage = navigation.NavigationStack.LastOrDefault();
           
            if (CommingFrom == 1)
            {
                navigation.InsertPageBefore(new NewDeclarationPage(token), currentPage);
                _navigationService.GoBack();
            }
            else
            {
                navigation.InsertPageBefore(new TransactionReceptionView(token), currentPage);
                _navigationService.GoBack();
            }

        }

        // Not Used
        //private static IDictionary<string, object> GetTokenData(string token)
        //{
        //    try
        //    {
        //        if (!String.IsNullOrEmpty(token))
        //        {
        //        token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6InNhYmR1bG1vaXpAemF0Y2EuZ292LnNhIiwiRW1haWwiOiJzYWJkdWxtb2l6QHphdGNhLmdvdi5zYSIsIk1vYmlsZSI6IjUwOTMzOTM2NCIsIk5hdGlvbmxJZCI6IjEwMzExNjQ0NTAiLCJJZCI6IjIyODE3NDIiLCJGaXJzdE5hbWUiOiLYrdiz2KfZhSIsIk1pZGRsZU5hbWUiOiLYudmE2YoiLCJMYXN0TmFtZSI6Itin2YTYsdmB2KfYudmKIiwiTmF0aW9uYWxpdHkiOiLYp9mE2YXZhdmE2YPYqSDYp9mE2LnYsdio2YrYqSDYp9mE2LPYudmI2K_ZitipIiwiR2VuZGVyIjoiTWFsZSIsIlJlbGVhc2VEYXRlIjoiMTQzOS8wMi8yNyIsIkVuZERhdGUiOiIiLCJJdHNTb3VyY2UiOiIiLCJleHAiOjE2Nzk2NTY4NzgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjA2MDQiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjYwNjA0In0.wtcTe9eJ9wWiHe2_3d4JXbEmlWfX3yH9_IYHBQfeQrk";
        //        }
        //        // token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6InNhYmR1bG1vaXpAemF0Y2EuZ292LnNhIiwiRW1haWwiOiJzYWJkdWxtb2l6QHphdGNhLmdvdi5zYSIsIk1vYmlsZSI6IjUwOTMzOTM2NCIsIk5hdGlvbmxJZCI6IjEwMzExNjQ0NTAiLCJJZCI6IjIyODE3NDIiLCJleHAiOjE2Njk3MDk3ODgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjA2MDQiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjYwNjA0In0.vBgCVsCqKOSJobIOXqfeLFhVl9dBYe8-dGAxEtEPfew";
        //        string secretKey = "ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM";
        //        var payload = JWT.JsonWebToken.DecodeToObject(token, secretKey) as IDictionary<string, object>;
        //        return payload;
        //        //  var mobile = payload["Mobile"];
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
         

        //}
    }
}

