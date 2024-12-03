namespace ZATCAMAUI.Core.AppConfigurations
{
    static class PageSettings
    {
        #region SurveyConfig
        public static string VocBaseUrl = "https://vocstg.gazt.gov.sa/v1/api/";
        public static string SurveyID;
        public static string CollectorId;
        public static string SurveyToken;
        public static string Q1ID;
        public static string Q2ID;
        public static string Q3ID;
        public static string Q1AnsID;
        public static string Q2AnsID;
        public static string Q3AnsID;
        public static string SurveyIDStg = "6307638a380faf6b91c907f4";
        public static string CollectorIdStg = "630b389e380faf6b91c910a7";
        public static string SurveyTokenStg = "I/70744d53";
        public static string Q1IDStg = "6307638b380faf6b91c907f8";
        public static string Q2IDStg = "6307638b380faf6b91c90800";
        public static string Q3IDStg = "6307638b380faf6b91c90814";
        public static string Q1AnsIDStg = "61c32bf2527cacedb5d31931";
        public static string Q2AnsIDStg = "61c32c8a527cacedb5d31970";
        public static string Q3AnsIDStg = "61c32e14aa59caed43d24e7e";

        /// Prod MApping <summary>
        public static string SurveyIDProd = "64087b56419c3e4317132e3b";
        public static string CollectorIdProd = "640880e56527b043165f34d9";
        public static string SurveyTokenProd = "r/b5cce7cb";
        public static string Q1IDProd = "64087eadfe688b43c2945297";
        public static string Q2IDProd = "64087edb11a01beb1c5c0546";
        public static string Q3IDProd = "64087ef19ce394ebab52071c";
        public static string Q1AnsIDProd = "64087eadfe688b43c2945298";
        public static string Q2AnsIDProd = "64087edb11a01beb1c5c0547";
        public static string Q3AnsIDProd = "64087ef19ce394ebab52071d";
        /// </summary>
        #endregion
        public static string TahqaqBaseURl = "https://dts.gazt.gov.sa/ECA/v2/";
        public static bool IsIncludeInquiryVisible = true;
        public static bool IsIncludeTarrif = true;
        public static bool IsIncludeBalagh = false;
        public static bool IsIncludeLapFees = true;
        public static bool IsIncludeExicesTaxs = true;
        public static string XZATCAClientIdProd = "802de35706277041a862927d6c9f1bfd";
        public static string XZATCAClientSecretProd = "50393c1c3eda9bb35a6ea052c6954c79";
        public const string XZATCAClientIdTest = "a867a41eeccbd956b7f279b50d8535a5";
        public const string XZATCAClientSecretTest = "c9487460cd7dd8bc0f16ede707f4dad3";
        public const string XZATCAClientIdQA = "f5ced263efb90b1a770988b3edd3c28c";
        public const string XZATCAClientSecretQA = "54c649e91c60cfb199365765fc82f537";
        public static string Target_Environment = "";
        public static string ExciseTaxUrl = "https://eservices.zatca.gov.sa/sites/sc/ar/app-view/Pages/Disclaimer.aspx";
        public static string CustomDEVBaseUrl = "http://10.112.34.26:8024/";
        public static string CustomSTGBaseUrl = "http://10.112.34.38:8024/";
        public static string VatCustomSTGURL = "https://vatapislb.zatca.gov.sa/api/";
        public static string VatCustomProdURL = "http://172.25.39.60:8443/api/";
        

        public static string DATAPowerProdCustomBaseUrl = "https://api.zatca.gov.sa/integration/third-party/v1/api/customs/";
        public static string DATAPowerSTGZATCABaseUrl = "https://test-api.zatca.gov.sa/test/third-party/";
        public static string DATAPowerProdCZATCABaseUrl = "https://api.zatca.gov.sa/integration/third-party/";
        public static string DATAPowerQAZATCABaseUrl = "https://test-api.zatca.gov.sa/qa/third-party/";
        public static string DATAPowerSTGCustomBaseUrl = $"{DATAPowerSTGZATCABaseUrl}v1/api/customs/";

        public static string IAMLoginSTGBaseUrl = "https://peservices.zatca.gov.sa/Iamext/_iam/Iaminit.aspx?APPID=Mobile";
        public static string IAMLoginProdBaseUrl = "https://eservices.zatca.gov.sa/Iam/_iam/Iaminit.aspx?APPID=Mobile";

        public static string CustomPaymentSTGURl = "https://payments-peservices.zatca.gov.sa/payment/initiate/";
        public static string CustomPaymentProdURl = "https://payments-eservices.zatca.gov.sa/payment/initiate/";

        public static string CustomPeserviceBaseURl = "https://pre-eservices.zatca.gov.sa";
        public static string ZATCAPortalBaseURL = " https://zatca.gov.sa/";
        public static string ZakatyPortalURl = "https://zakaty.gov.sa/";
        public static string ZakatyPlayStoreURl = "https://play.google.com/store/apps/details?id=com.sa.gazt.ZakatCalculator";
        public static string ZakatyAppStoreURl = "https://apps.apple.com/sa/app/zakaty-%D8%B2%D9%83%D8%A7%D8%AA%D9%8A/id1374131337";

        public static string IAMRegistraionProd = "https://eservices.zatca.gov.sa/sites/sc/ar/PublicIAMServices/Pages/TawreedClientPages/NewTRRequest.aspx";
        public static string IAMRegistraionStG = "http://esvc-web1-stg.ga.customs.gov.sa/sites/sc/ar/publiciamservices/Pages/TawreedClientPages/NewTRRequest.aspx";

        public static string CustomsIssuesSTG = "https://peservices.zatca.gov.sa";
        public static string CustomsIssuesProd = "https://eservices.zatca.gov.sa";
        public static string CustomsIssuesStG = "http://esvc-web1-stg.ga.customs.gov.sa/sites/sc/ar/app-view/Pages/NewSettlementRequest.aspx";



        public static string CustomBaseUrl = CustomSTGBaseUrl;
        public static string IAMLoginBaseUrl;
        public static string ZATCABaseURL;
        public static string CustomPaymentBaseUrl;
        public static string VatProdBaseUrl = "https://vatmobile.zatca.gov.sa/api";
        // public static string VatSTGBaseUrl = "http://172.25.39.60:80/api";

        public static string VatSTGBaseUrl = "https://vatapis.zatca.gov.sa/api";
        public static string EinvoiceBaseURl = "https://stgdx1as1.mygazt.gov.sa:50001";
        public static string IAMRegistration;
        public static string FasahBaseUrlProd = "https://soga.fasah.sa/";
        public static string FasahBaseUrlStG = "https://soga.fasah.sa/";
        public static string FasahRedirectUrl = "https://soga.fasah.sa/";
        public static string FasahBaseUrl;
        public static string ZATCAPaymentWebViewStgBaseURL = "https://peservices.zatca.gov.sa";
        public static string ZATCAPaymentWebViewProdBaseURL = "https://eservices.zatca.gov.sa";
        public static string ZATCAPaymentWebViewBaseURL;

        public static string FasahApiKey = "Av549-e756Z-4c29-a16a-287de9c04755";
        public static string CurrentEnvironment = "STG";
        public static string CheckTarget_Environment(string environment = "STG")
        {
            Target_Environment = System.Environment.GetEnvironmentVariable("Target_Environment");
            if (string.IsNullOrWhiteSpace(Target_Environment))
            {
                Target_Environment = environment;
            }
            return Target_Environment;
        }
        public static void GetBaseURL(string environment = "STG")
        {
            // Target_Environment = CheckTarget_Environment(environment);
            switch (Target_Environment)
            {
                case "STG":
                    App.CustomBaseUrl = DATAPowerSTGCustomBaseUrl;
                    App.VatBaseUrl = VatSTGBaseUrl;
                    App.VatCustom = VatCustomSTGURL;
                    ZATCABaseURL = DATAPowerSTGZATCABaseUrl;
                    IAMLoginBaseUrl = IAMLoginSTGBaseUrl;
                    SurveyID = SurveyIDStg;
                    CollectorId = CollectorIdStg;
                    SurveyToken = SurveyTokenStg;
                    Q1ID = Q1IDStg;
                    Q2ID = Q2IDStg;
                    Q3ID = Q3IDStg;
                    Q1AnsID = Q1AnsIDStg;
                    Q2AnsID = Q2AnsIDStg;
                    Q3AnsID = Q3AnsIDStg;
                    IAMRegistration = IAMRegistraionStG;
                    FasahBaseUrl = FasahBaseUrlStG;
                    ZATCAPaymentWebViewBaseURL = ZATCAPaymentWebViewStgBaseURL;
                    break;
                case "Prod":
                    App.CustomBaseUrl = DATAPowerProdCustomBaseUrl;
                    App.VatBaseUrl = VatProdBaseUrl;
                    App.VatCustom = VatCustomProdURL;
                    ZATCABaseURL = DATAPowerProdCZATCABaseUrl;
                    IAMLoginBaseUrl = IAMLoginProdBaseUrl;
                    SurveyID = SurveyIDProd;
                    CollectorId = CollectorIdProd;
                    SurveyToken = SurveyTokenProd;
                    Q1ID = Q1IDProd;
                    Q2ID = Q2IDProd;
                    Q3ID = Q3IDProd;
                    Q1AnsID = Q1AnsIDProd;
                    Q2AnsID = Q2AnsIDProd;
                    Q3AnsID = Q3AnsIDProd;
                    IAMRegistration = IAMRegistraionProd;
                    FasahBaseUrl = FasahBaseUrlProd;
                    ZATCAPaymentWebViewBaseURL = ZATCAPaymentWebViewProdBaseURL;
                    break;
                case "QA":
                    App.CustomBaseUrl = DATAPowerSTGCustomBaseUrl;
                    App.VatBaseUrl = VatSTGBaseUrl;
                    App.VatCustom = VatCustomSTGURL;
                    ZATCABaseURL = DATAPowerQAZATCABaseUrl;
                    IAMLoginBaseUrl = IAMLoginSTGBaseUrl;
                    SurveyID = SurveyIDStg;
                    CollectorId = CollectorIdStg;
                    SurveyToken = SurveyTokenStg;
                    Q1ID = Q1IDStg;
                    Q2ID = Q2IDStg;
                    Q3ID = Q3IDStg;
                    Q1AnsID = Q1AnsIDStg;
                    Q2AnsID = Q2AnsIDStg;
                    Q3AnsID = Q3AnsIDStg;
                    IAMRegistration = IAMRegistraionStG;
                    FasahBaseUrl = FasahBaseUrlStG;
                    ZATCAPaymentWebViewBaseURL = ZATCAPaymentWebViewStgBaseURL;
                    break;
                default:
                    App.CustomBaseUrl = DATAPowerSTGCustomBaseUrl;
                    App.VatBaseUrl = VatSTGBaseUrl;
                    App.VatCustom = VatCustomSTGURL;
                    ZATCABaseURL = DATAPowerSTGZATCABaseUrl;
                    IAMLoginBaseUrl = IAMLoginSTGBaseUrl;
                    SurveyID = SurveyIDStg;
                    CollectorId = CollectorIdStg;
                    SurveyToken = SurveyTokenStg;
                    Q1ID = Q1IDStg;
                    Q2ID = Q2IDStg;
                    Q3ID = Q3IDStg;
                    Q1AnsID = Q1AnsIDStg;
                    Q2AnsID = Q2AnsIDStg;
                    Q3AnsID = Q3AnsIDStg;
                    IAMRegistration = IAMRegistraionStG;
                    FasahBaseUrl = FasahBaseUrlStG;
                    ZATCAPaymentWebViewBaseURL = ZATCAPaymentWebViewStgBaseURL;
                    break;
            }
        }

        public static string GetClientID()
        {
            string key = "";
            switch (Target_Environment)
            {
                case "STG":
                    key = XZATCAClientIdTest;
                    break;
                case "Prod":
                    key = XZATCAClientIdProd;
                    break;
                case "QA":
                    key = XZATCAClientIdQA;
                    break;
                default:
                    key = XZATCAClientIdTest;
                    break;
            }
            return key;
        }
        public static string GetClientSecret()
        {
            string key = "";
            switch (Target_Environment)
            {
                case "STG":
                    key = XZATCAClientSecretTest;
                    break;
                case "Prod":
                    key = XZATCAClientSecretProd;
                    break;
                case "QA":
                    key = XZATCAClientSecretQA;
                    break;
                default:
                    key = XZATCAClientSecretTest;
                    break;
            }
            return key;
        }
        const string TawreedBaseURL = "http://esvc-web1-stg.ga.customs.gov.sa/sites/sc/ar/app-view/Pages/TawreedNewTRRequest.aspx";
        const string FeesCalculatorBaseURL = "https://eservices.zatca.gov.sa/sites/sc/";
        const string EdclerationBaseURL = "https://eservices.zatca.gov.sa/sites/sc/";
        //EdclerationBaseURL = "http://10.112.42.23/"; not Used
        public static string GetNewEDeclarationLinks()
        {
            if (App.IsArabic)
            {
                return $"{EdclerationBaseURL}ar/app-view/Pages/EDeclarationStartPage.aspx";

            }
            else
            {
                return $"{EdclerationBaseURL}en/app-view/Pages/EDeclarationStartPage.aspx";
            }
        }

        public static string GetTawreedLinks()
        {
            if (App.IsArabic)
            {
                return $"{TawreedBaseURL}";

            }
            else
            {
                return $"{TawreedBaseURL}";
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
        public static string GetCustomsPaymentUrl()
        {
            if (Target_Environment == "Prod")
            {
                if (App.IsArabic)
                {
                    return CustomPaymentProdURl;
                }
                else
                {
                    return CustomPaymentProdURl;
                }
            }
            else
            {
                if (App.IsArabic)
                {
                    return CustomPaymentSTGURl;
                }
                else
                {
                    return CustomPaymentSTGURl;
                }
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

        public static string GetCustomFeesCalcLink()
        {
            if (App.IsArabic)
            {
                return $"{FeesCalculatorBaseURL}ar/app-view/Pages/calculatorPage.aspx";
            }
            else
            {
                return $"{FeesCalculatorBaseURL}en/app-view/Pages/calculatorPage.aspx";
            }
        }
        public static string GetProhibitedGoodsLstURl()
        {
            if (App.IsArabic)
            {
                return $"{ZATCAPortalBaseURL}ar/RulesRegulations/Taxes/Pages/customs_individual/Prohibited-goods.aspx";
            }
            else
            {
                return $"{ZATCAPortalBaseURL}en/RulesRegulations/Taxes/Pages/customs_individual/Prohibited-goods.aspx";
            }
        }
        public static string GetCustomDeclarationInformationURl()
        {
            if (App.IsArabic)
            {
                return $"{ZATCAPortalBaseURL}ar/RulesRegulations/Taxes/Pages/customs_individual/Travel_pages/declare.aspx";
            }
            else
            {
                return $"{ZATCAPortalBaseURL}en/RulesRegulations/Taxes/Pages/customs_individual/Travel_pages/declare.aspx";

            }
        }

        public static string GetFasahRefere()
        {
            if (App.IsArabic)
            {
                return $"https://toga.fasah.sa/ar/login/1.0/";
            }
            else
            {
                return $"https://toga.fasah.sa/en/login/1.0/";
            }
        }

        public static string GetFasahRedirectUrl()
        {
            if (App.IsArabic)
            {
                return $"{FasahRedirectUrl}ar/redirection/1.0/?s=Brokers_optionality&t=";
            }
            else
            {
                return $"{FasahRedirectUrl}en/redirection/1.0/?s=Brokers_optionality&t=";
            }
        }

        public static string GetPaymentWebViewURl()
        {
            if (App.IsArabic)
            {
                return $"{ZATCAPaymentWebViewBaseURL}/sites/sc/ar/app-view/Pages/ViewEDeclarationPage.aspx?";
            }
            else
            {
                return $"{ZATCAPaymentWebViewBaseURL}/sites/sc/en/app-view/Pages/ViewEDeclarationPage.aspx?";

            }
        }
        public static string GetCustomsIssueUrl()
        {
            if (App.IsArabic)
            {
                return $"{CustomsIssuesProd}/sites/sc/ar/app-view/Pages/NewSettlementRequest.aspx";
            }
            else
            {
                return $"{CustomsIssuesProd}/sites/sc/ar/app-view/Pages/NewSettlementRequest.aspx";
            }
        }

    }
}
