using System;
using System.ComponentModel;
using System.Net.Http;

namespace EGAZT.AppConfigurations
{
     static class PageSettings
    {
        #region SurveyConfig
        public static string VocBaseUrl = "https://vocstg.gazt.gov.sa/v1/api/";
        public static string SurveyID = "6307638a380faf6b91c907f4";
        public static string CollectorId = "630b389e380faf6b91c910a7";
        public static string SurveyToken = "I/70744d53";
        public static string Q1ID = "6307638b380faf6b91c907f8";
        public static string Q2ID = "6307638b380faf6b91c90800";
        public static string Q3ID = "6307638b380faf6b91c90814";
        public static string Q1AnsID = "61c32bf2527cacedb5d31931";
        public static string Q2AnsID = "61c32c8a527cacedb5d31970";
        public static string Q3AnsID = "61c32e14aa59caed43d24e7e";
        #endregion
        public static string TahqaqBaseURl = "https://dts.gazt.gov.sa/ECA/v2/";
        public static bool IsIncludeInquiryVisible= true;
        public static bool IsIncludeTarrif = true;
        public static bool IsIncludeBalagh = false;
        public static bool IsIncludeLapFees = true;
        public static bool IsIncludeExicesTaxs = true;
        public static string XZATCAClientIdProd= "802de35706277041a862927d6c9f1bfd";
        public static string XZATCAClientSecretProd = "50393c1c3eda9bb35a6ea052c6954c79";
        public const string XZATCAClientIdTest = "a867a41eeccbd956b7f279b50d8535a5";
        public const string XZATCAClientSecretTest = "c9487460cd7dd8bc0f16ede707f4dad3";
        public static string Target_Environment = "";
        public  static string ExciseTaxUrl = "https://eservices.zatca.gov.sa/sites/sc/ar/app-view/Pages/Disclaimer.aspx";
        public static string CustomDEVBaseUrl = "http://10.112.34.26:8024/";
        public static string CustomSTGBaseUrl = "http://10.112.34.38:8024/";
        public static string VatCustomSTGURL = "http://172.50.15.39:8443/api/";
        public static string VatCustomProdURL = "http://172.50.15.39:8443/api/";
        public static string DATAPowerSTGCustomBaseUrl = "https://stzgw-apic-gov.gazt.gov.sa/gazt-integration/test-third-party/v1/api/customs/";
        public static string DATAPowerProdCustomBaseUrl = "https://gw-apic-gov.gazt.gov.sa/gazt-integration/third-party/v1/api/customs/";

        public static string CustomBaseUrl;
       public static string VatProdBaseUrl = "https://vatmobile.zatca.gov.sa/api";
        public static string VatSTGBaseUrl = "http://172.50.15.39:80/api";

        public static string GetBaseURL(string environment="")
        {
            Target_Environment = System.Environment.GetEnvironmentVariable("Target_Environment");
#if DEBUG
            Target_Environment = "STG";
#endif
            if (string.IsNullOrEmpty(Target_Environment))
            {
                Target_Environment = environment;
            }
            string key = "";
            switch (Target_Environment)
            {
                case "STG":
                    App.CustomBaseUrl = DATAPowerSTGCustomBaseUrl;
                    App.VatBaseUrl = VatSTGBaseUrl;
                    App.VatCustom = VatCustomSTGURL;
                    break;
                case "Prod":
                    App.CustomBaseUrl = DATAPowerProdCustomBaseUrl;
                    App.VatBaseUrl = VatProdBaseUrl;
                    App.VatCustom = VatCustomProdURL;
                    break;
                default:
                    App.CustomBaseUrl = DATAPowerSTGCustomBaseUrl;
                    App.VatBaseUrl = VatSTGBaseUrl;
                    App.VatCustom = VatCustomSTGURL;
                    break;
            }
            return key;
        }

        public static string GetClientID()
        {
            Target_Environment = System.Environment.GetEnvironmentVariable("Target_Environment");
#if DEBUG
            Target_Environment = "STG";
#endif
            string key = "";
            switch (Target_Environment)
            {
                case"STG":
                    CustomBaseUrl = DATAPowerSTGCustomBaseUrl;
                    key= XZATCAClientIdTest;
                    break;
                case "Prod":
                    key= XZATCAClientIdProd;
                    CustomBaseUrl = DATAPowerProdCustomBaseUrl;
                    break;
                default:
                    key = XZATCAClientIdTest;
                    break;
            }
            return key;
        }
        public static string GetClientSecret()
        {
            Target_Environment = System.Environment.GetEnvironmentVariable("Target_Environment");
#if DEBUG
            Target_Environment = "STG";
#endif
            string key = "";
            switch (Target_Environment)
            {
                case "STG":
                    key = XZATCAClientSecretTest;
                    break;
                case "Prod":
                    key = XZATCAClientSecretProd;
                    break;
                default:
                    key = XZATCAClientSecretTest;
                    break;
            }
            return key;
        }
        const string EdclerationBaseURL = "http://10.112.42.23/";

        public static string GetNewEDeclarationLinks()
        {
            if (App.IsArabic)
            {
                return $"{EdclerationBaseURL}ar/edeclaration?AppViewEDecForm";
            }
            else
            {
                return $"{EdclerationBaseURL}en/edeclaration?AppViewEDecForm";
            }
        }

        public static string GetContactUsUrl()
        {
            if (App.IsArabic)
            {
                return "https://zatca.gov.sa/ar/contactus/Pages/default.aspx";
            }
            else
            {
                return "https://zatca.gov.sa/en/contactus/Pages/default.aspx";
            }
        }


        public static string GetPreviousEDeclarationLink()
        {
            if (App.IsArabic)
            {
                return $"{EdclerationBaseURL}ar/edeclaration?AppViewEDeccheck";
            }
            else
            {
                return $"{EdclerationBaseURL}en/edeclaration?AppViewEDeccheck";
            }
        }

    }
}
