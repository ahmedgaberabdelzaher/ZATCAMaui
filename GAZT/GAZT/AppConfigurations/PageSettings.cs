using System;
namespace EGAZT.AppConfigurations
{
    public static class PageSettings
    {
        public static bool IsIncludeInquiryVisible= true;
        public static bool IsIncludeTarrif = true;
        public static bool IsIncludeBalagh = false;
        public static bool IsIncludeLapFees = false;
        public static bool IsIncludeExicesTaxs = true;
        public static string XZATCAClientIdProd= "802de35706277041a862927d6c9f1bfd";
        public static string XZATCAClientSecretProd = "50393c1c3eda9bb35a6ea052c6954c79";
        public const string XZATCAClientIdTest = "a867a41eeccbd956b7f279b50d8535a5";
        public const string XZATCAClientSecretTest = "c9487460cd7dd8bc0f16ede707f4dad3";
        public static Environment environment = Environment.Prod;
        public  static string ExciseTaxUrl = "https://eservices.zatca.gov.sa/sites/sc/ar/app-view/Pages/Disclaimer.aspx";
        public static string GetClientID()
        {
            string key = "";
            switch (environment)
            {
                case Environment.Staging:
                    key= XZATCAClientIdTest;
                    break;
                case Environment.Prod:
                    key= XZATCAClientIdProd;
                    break;
                default:
                    break;
            }
            return key;
        }
        public static string GetClientSecret()
        {
            string key = "";
            switch (environment)
            {
                case Environment.Staging:
                    key = XZATCAClientSecretTest;
                    break;
                case Environment.Prod:
                    key = XZATCAClientSecretProd;
                    break;
                default:
                    break;
            }
            return key;
        }

    }
}
