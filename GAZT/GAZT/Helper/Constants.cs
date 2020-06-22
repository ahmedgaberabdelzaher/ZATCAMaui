using System;
using System.Collections.Generic;
using System.Text;
namespace GAZT.Helper
{
    public static class Constants
    {
        public static string ContentType = "application/json";

        public static string DevUrlPort = ":8080";
        public static string DevUrlAuthenticationPort = ":50001";

        public static string QAUrlPort = ":443";
        public static string PreProdUrlPort = ":443";
        public static string ProdUrlPort = ":443";

        public static string DevBaseUrlForODataServices = "https://tstdg1as1.mygazt.gov.sa:8080";
        public static string DevBaseUrlForAuthentication = "https://tstdp1as1.mygazt.gov.sa:50001";
        public static string QABaseUrlForODataServices = "https://sapgatewayqa.gazt.gov.sa:443";
        public static string QABaseUrlForAuthentication = "https://loginqa.gazt.gov.sa:443";
        public static string PreProdBaseUrlForODataServices = "https://sapgatewayt.gazt.gov.sa:443";
        public static string PreProdBaseUrlForAuthentication = "https://logint.gazt.gov.sa:443";
        public static string ProdBaseUrlForODataServices = "https://sapgateway.gazt.gov.sa:443";
        public static string ProdBaseUrlForAuthentication = "https://login.gazt.gov.sa:443";

        #region Cookie Info
        public static string DevDomainForCookies = "tstdp1as1.mygazt.gov.sa";
        public static string QADomainForCookies = "loginqa.gazt.gov.sa";
        public static string PreprodDomainForCookies = "logint.gazt.gov.sa";
        public static string ProdDomainForCookies = "login.gazt.gov.sa";

        public static string LanguageCookieNameForLogin = "langMobile";

        //public static string DomainUrlForCookies = QADomainForCookies;

        public static string PartialDomainUrlForCookies = ".gazt.gov.sa";
        #endregion

        //public static string BaseUrlOfODataServices = ProdBaseUrlForODataServices;
        //public static string BaseUrlOfAuthentication = ProdBaseUrlForAuthentication;
        //public static string DomainUrlForCookies = ProdDomainForCookies;


        public static string BaseUrlOfODataServices = QABaseUrlForODataServices;
        public static string BaseUrlOfAuthentication = QABaseUrlForAuthentication;
        public static string DomainUrlForCookies = QADomainForCookies;


        public static string GAZTSAMLLoginServicePart = "/SAP/ZTP_ACCOUNT_SRV/GetInfoSet";
        public static string GAZTSAMLLoginService = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZTP_ACCOUNT_SRV/GetInfoSet";

        public static string GAZTSAMLLogoutService = BaseUrlOfODataServices + "/sap/public/bc/icf/logoff?keepMYSAPSSO2Cookie=true&dsmguid=1588829910165";
        //https://sapgatewayqa.gazt.gov.sa/sap/public/bc/icf/logoff?keepMYSAPSSO2Cookie=true&dsmguid=1588829910165

        public static string ForgotPasswordServiceName = "ZDP_FRGT_USRNM_PWD_SRV";// service name has been used in Metadata in ForgotUserNamePassword Page
        public static string JSONContentType = "application/json";
        public static string GAZTSOAPWebRequestForAuthenticationService = BaseUrlOfAuthentication + "/local~mblgapi/AuthenticatedService";
        public static string GAZTSendAndReceiveOTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GAZTValidateOTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GaZTVerifyMobileNumber = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateOTPForMobile = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateAndChangePassword = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='P',";
        public static string GAZTGetPdf = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/Corr_detSet?$filter=";
        public static string GAZTGetTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_PROFILE_DEMO_SRV/TPFL_HEADERSet(Taxpayerz";
        public static string GAZTGetOTPForEmail = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='E',";
        public static string GAZTValidateOTPForEmail = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='E',";
        public static string GAZTZakatGetPdf = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_ZAKAT_SRV/Corr_detSet?$filter=Gpartz eq'";
        public static string GetAllTin = BaseUrlOfAuthentication + "/prt_logon/GetTINServlet?&emailId=";
        public static string GetAllCertificate = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/headerSet(Gpartz='";
        public static string GetMyBills = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_MYBILLS_SRV/MyBillsSet?$filter=";
        public static string FogotPasswordSendOTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='";
        public static string SendUserNameToEmail = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet?saml2=enabled";
        public static string ValidateOTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet?saml2=enabled";
        public static string ChangePassword = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet?saml2=enabled";
        public static string GetTinStatus = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZTIN_STAT_SRV/HeaderSet(Langz='";
        public static string GetVATLookUpDetails = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZVAT_TAXPAYER_LOOKUP_SRV/TaxpayerSet?saml2=enabled&sap-language='";
        public static string GAZTGetAllVATDeclarationReturnData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/HDRSet(Periodkeyz='";
        public static string GetMyICRs = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_WI_SRV/ICR_HDRSet(Fbnum='',Lang='";
        public static string SaveVATDeclarationData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/HDRSet?saml2=enabled";
        public static string GAZTSaveAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef=";
        public static string GAZTDeteleAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachMedSet(OutletRef=";
        public static string GAZTGetVATDeclarationCalculationDataUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum=";
        public static string GAZTGetSADADNumber = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_SADAD_SRV/SadadSet?&saml2=enabled&sap-language=’";
        public static string GAZTGetZakatReturn = BaseUrlOfODataServices + "/sap/opu/odata/sap/ZDP_FZ12_SRV/HeaderSet(Fbnumz='";
        public static string GAZTGetZakatReturnList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TAX01RET_WI_SRV/HeaderSet(Bpnum='";
        public static string GAZTSaveEstimatedZaktReturn = BaseUrlOfODataServices + "/sap/opu/odata/sap/ZDP_FZ12_SRV/HeaderSet?saml2=enabled&sap-language=";
        public static string GAZTVATReturnGetApplicableButtons = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum=";
        public static string GAZTGetEstimatedZAKATSADADNumber = BaseUrlOfODataServices + "/sap/opu/odata/sap/ZDP_FZ12_SRV/HeaderSet(Fbnumz='";
        public static string GAZTSaveEstimatedZAKATAttachement = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(RetGuid='";
        public static string GAZTGetIdNumber = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/IDNUMBERSet?$filter=Partner eq '";
        public static string GAZTCheckIBANNumber = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_CHECK_IBAN_SRV/HEADERSet('";
        public static string GAZTGetEstimatedZAKATReturnInvoicePdf = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='";

        //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_TAX01RET_WI_SRV/HeaderSet(Bpnum='3300088482',Auditor='',Lang='EN',UserTin='330088482')?saml2=disabled&sap-language='EN'&$expand=listSet
        //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_TP_PROFILE_DEMO_SRV/TPFL_HEADERSet(Taxpayerz='3102289241',Langz='E')?&$expand=TPOC_LIST&saml2=enabled&$format=json

        #region CorrespondenceAPIs
        public static string GAZTGetCorrespondence = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/Corr_detSet?$format=json&saml2=enabled&$filter=Gpartz eq ";
        public static string GAZTSetFavCorrespondence = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/Corr_detSet?saml2=enabled";
        public static string GAZTGetCorrespondenceDetails = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/CorrespondanceTextSet?$format=json&saml2=enabled&$filter=Gpart eq ";
        public static string GAZTGetCorrespondenceAttach = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey=";
        #endregion
        #region FormBundleAPIs
        public static string GAZTGetFormBundleModel = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZTP_RETURN_STATUS_SRV/HEADERSet?&$format=json&saml2=enabled&$filter=Lang eq ";
        public static string GAZTGetFormBunleAccountNumberModel = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZTP_RETURN_STATUS_SRV/ItemSet?&$format=json&saml2=enabled&$filter=Lang eq ";
        #endregion
        #region SignUp
        public static string GAZTGetCityListForSignUp = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_DROPDOWN_SRV/";
        public static string GAZTSiguupValidateIDTypes = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_GET_TAXPAYER_SRV/taxpayer_nameSet";
        public static string GAZTSiguupValidateCR = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_NREG_CR_VALDATE_SRV/validatecrSet";
        public static string GAZTSiguupCheckDuplicate = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_ID_DUPLICAE_CHECK_SRV/permit_detSet";
        public static string GAZTSiguupIssuedByList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZTP_MOBILE_SRV/ConsumeSet?$filter=Request eq ";
        //  public static string GAZTSignUpFirstSubmit = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_PUSR_SIGNUP_SRV/signup_headerSet";
        public static string GAZTSignUpFirstSubmit = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_PUSR_SIGNUP_SRV/signup_headerSet?sap-language=";
        public static string GAZTSignUpGetGuid = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_PUSR_SIGNUP_SRV/signup_headerSet?$format=json&$filter=AType eq  '1'";
        #endregion
        #region NEW DASHBOARD
        //Dashboard - get the set of Unpaid Amounts
        public static string GetDashboardData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/HEADERSet?$filter=Tin eq '";
        public static string GAZTGetTheSetOfUnpaidAmounts = BaseUrlOfODataServices + "sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/PaymentOverdueSet?$filter=Langz eq ";
        public static string GAZTGetTheSetOfUnsubmittedReturns = BaseUrlOfODataServices + "sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/UnSubmittedReturnSet?$filter=Langz eq ";
        public static string GAZTGetUnSubmittedReturnSetForDashboard = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/UnSubmittedReturnSet?$filter=Langz eq '";
        public static string GAZTGetPaymentOverdueSetForDashboard = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/PaymentOverdueSet?$filter=Langz eq '";
        public static string GAZTGetReturnList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/ICR_LISTSet?$filter=Gpart eq '";
        #endregion
        #region TES
        public static string GAZTGetFAQ = "http://tstcrmmwintg1.mygazt.gov.sa:82/IntegrationServices.svc/FAQRetrieveAll";
        #endregion
        #region DownloadAttachment
        public static string GAZTGetAllAttachments = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_DOCUMENT_SRV/AttachSet?$filter=ByPusr eq '";
        #endregion

        #region Tax Evasion New API - Pointing to Prod

        public static string GAZTTaxEvasionSendSms = "https://vat2.gazt.gov.sa/api/v4/sendSms";
        public static string GAZTTaxEvasionVerifySms = "https://vat2.gazt.gov.sa/api/v4/verifySms";
        public static string GAZTTaxEvasionGetAllReports = "https://vat2.gazt.gov.sa/api/v4/get-reports";
        public static string GAZTTaxEvasionGetUserByMobile = "https://vat2.gazt.gov.sa/api/v4/get-user-by-mobile";
        public static string GAZTTaxEvasionGetAllRegions = "https://vat2.gazt.gov.sa/api/v4/list-regions";
        public static string GAZTTaxEvasionGetAllCities = "https://vat2.gazt.gov.sa/api/v4/list-cities?region=";
        public static string GAZTTaxEvasionGetAllCategories = "https://vat2.gazt.gov.sa/api/v4/list-categories";
        public static string GAZTTaxEvasionCreateReport = "https://vat2.gazt.gov.sa/api/v4/add-report";

        #endregion

    }
}
