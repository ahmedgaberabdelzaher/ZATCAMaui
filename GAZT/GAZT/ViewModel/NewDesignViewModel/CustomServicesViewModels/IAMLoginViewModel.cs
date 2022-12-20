using System;
using System.Collections.Generic;
using System.Web;
using System.Windows.Input;
using EGAZT.AppConfigurations;
using EGAZT.Converters;
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


        public IAMLoginViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            IAMWbViewSrc = PageSettings.IAMLoginBaseUrl;
            //GetTokenData();
        }
        public ICommand IAMWbViewNavigatingCommand
        {
            get
            {
                return new Command(() =>
                {

                    double no = 22.5;

                    var moneyToWordConverter = new NumberToWord((decimal)no, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                    PriceText = App.IsArabic ? moneyToWordConverter.ConvertToArabic() : moneyToWordConverter.ConvertToEnglish();
                    // IAMWbViewSrc = "http://172.25.39.60:8443/Home/Result?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6InNhYmR1bG1vaXpAemF0Y2EuZ292LnNhIiwiRW1haWwiOiJzYWJkdWxtb2l6QHphdGNhLmdvdi5zYSIsIk1vYmlsZSI6IjUwOTMzOTM2NCIsIk5hdGlvbmxJZCI6IjEwMzExNjQ0NTAiLCJJZCI6IjIyODE3NDIiLCJleHAiOjE2Njk3MDk3ODgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjA2MDQiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjYwNjA0In0.vBgCVsCqKOSJobIOXqfeLFhVl9dBYe8-dGAxEtEPfew";
                    if (IAMWbViewSrc.Contains("token"))
                    {
                        string token = HttpUtility.ParseQueryString(new Uri(IAMWbViewSrc).Query).Get("token");
                       
                         var payload=GetTokenData(token);
                        if (CommingFrom==1)
                        {
                            _navigationService.NavigateTo("NewDeclarationPage", payload);
                        }
                        else
                        {
                            _navigationService.NavigateTo("TransactionReceptionView", payload);
                        }
                       
                        IAMWbViewSrc = PageSettings.IAMLoginBaseUrl;

                    }
                });
            }
        }

        private static IDictionary<string, object> GetTokenData(string token)
        {
            try
            {
                if (!String.IsNullOrEmpty(token))
                {
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6InNhYmR1bG1vaXpAemF0Y2EuZ292LnNhIiwiRW1haWwiOiJzYWJkdWxtb2l6QHphdGNhLmdvdi5zYSIsIk1vYmlsZSI6IjUwOTMzOTM2NCIsIk5hdGlvbmxJZCI6IjEwMzExNjQ0NTAiLCJJZCI6IjIyODE3NDIiLCJGaXJzdE5hbWUiOiLYrdiz2KfZhSIsIk1pZGRsZU5hbWUiOiLYudmE2YoiLCJMYXN0TmFtZSI6Itin2YTYsdmB2KfYudmKIiwiTmF0aW9uYWxpdHkiOiLYp9mE2YXZhdmE2YPYqSDYp9mE2LnYsdio2YrYqSDYp9mE2LPYudmI2K_ZitipIiwiR2VuZGVyIjoiTWFsZSIsIlJlbGVhc2VEYXRlIjoiMTQzOS8wMi8yNyIsIkVuZERhdGUiOiIiLCJJdHNTb3VyY2UiOiIiLCJleHAiOjE2Nzk2NTY4NzgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjA2MDQiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjYwNjA0In0.wtcTe9eJ9wWiHe2_3d4JXbEmlWfX3yH9_IYHBQfeQrk";
                }
                // token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6InNhYmR1bG1vaXpAemF0Y2EuZ292LnNhIiwiRW1haWwiOiJzYWJkdWxtb2l6QHphdGNhLmdvdi5zYSIsIk1vYmlsZSI6IjUwOTMzOTM2NCIsIk5hdGlvbmxJZCI6IjEwMzExNjQ0NTAiLCJJZCI6IjIyODE3NDIiLCJleHAiOjE2Njk3MDk3ODgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjA2MDQiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjYwNjA0In0.vBgCVsCqKOSJobIOXqfeLFhVl9dBYe8-dGAxEtEPfew";
                string secretKey = "ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM";
                var payload = JWT.JsonWebToken.DecodeToObject(token, secretKey) as IDictionary<string, object>;
                return payload;
                //  var mobile = payload["Mobile"];
            }
            catch (Exception ex)
            {
                return null;
            }
         

        }
    }
}

