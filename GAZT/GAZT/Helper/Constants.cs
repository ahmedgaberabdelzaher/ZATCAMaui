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

        public static string DevPartialDomainForCookies = ".mygazt.gov.sa";
        public static string QAPrepprodProdPartialDomainForCookies = ".gazt.gov.sa";

        public static string LanguageCookieNameForLogin = "langMobile";

        #endregion

        //public static string BaseUrlOfODataServices = DevBaseUrlForODataServices;
        //public static string BaseUrlOfAuthentication = DevBaseUrlForAuthentication;
        //public static string DomainUrlForCookies = DevDomainForCookies;
        //public static string PartialDomainUrlForCookies = DevPartialDomainForCookies;

        //public static string BaseUrlOfODataServices = QABaseUrlForODataServices;
        //public static string BaseUrlOfAuthentication = QABaseUrlForAuthentication;
        //public static string DomainUrlForCookies = QADomainForCookies;
        //public static string PartialDomainUrlForCookies = QAPrepprodProdPartialDomainForCookies;

        //public static string BaseUrlOfODataServices = PreProdBaseUrlForODataServices;
        //public static string BaseUrlOfAuthentication = PreProdBaseUrlForAuthentication;
        //public static string DomainUrlForCookies = PreprodDomainForCookies;
        //public static string PartialDomainUrlForCookies = QAPrepprodProdPartialDomainForCookies;

        //public static string BaseUrlOfODataServices = PreProdBaseUrlForODataServices;
        //public static string BaseUrlOfAuthentication = PreProdBaseUrlForAuthentication;
        //public static string DomainUrlForCookies = PreprodDomainForCookies;
        //public static string PartialDomainUrlForCookies = QAPrepprodProdPartialDomainForCookies;

        public static string BaseUrlOfODataServices = QABaseUrlForODataServices;
        public static string BaseUrlOfAuthentication = QABaseUrlForAuthentication;
        public static string DomainUrlForCookies = QADomainForCookies;
        public static string PartialDomainUrlForCookies = QAPrepprodProdPartialDomainForCookies;

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

        // "/sap/opu/odata/SAP/ZVTIA_SIGNUP_SRV/signup_headerSet";
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

        public static string GAZTTaxEvasionGetCategories = "https://vat2.gazt.gov.sa/api/v4/list-categories";
        public static string GAZTTaxEvasionSendSms = "https://vat2.gazt.gov.sa/api/v4/sendSms";
        public static string GAZTTaxEvasionVerifySms = "https://vat2.gazt.gov.sa/api/v4/verifySms";
        public static string GAZTTaxEvasionGetAllReports = "https://vat2.gazt.gov.sa/api/v4/get-reports";
        public static string GAZTTaxEvasionGetUserByMobile = "https://vat2.gazt.gov.sa/api/v4/get-user-by-mobile";
        public static string GAZTTaxEvasionGetAllRegions = "https://vat2.gazt.gov.sa/api/v4/list-regions";
        public static string GAZTTaxEvasionGetAllCities = "https://vat2.gazt.gov.sa/api/v4/list-cities?region=";
        public static string GAZTTaxEvasionGetAllCategories = "https://vat2.gazt.gov.sa/api/v4/list-categories";
        public static string GAZTTaxEvasionCreateReport = "https://vat2.gazt.gov.sa/api/v4/add-report";
        public static string GAZTTaxEvasionRegisterUser = "https://vat2.gazt.gov.sa/api/v4/user/register";

        #endregion

        #region VAT Sign Up
        public static string GAZTGetVATSignUpCaseId = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZVTIA_SIGNUP_SRV/signup_headerSet?$format=json";
        public static string GAZTVATSignUpValidateId = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_GET_TAXPAYER_SRV/taxpayer_nameSet";//(Tin='',Idtype='ZS0015',Idnum='1048089609',Country='',PassExpDt='',TaxpDob='19650224')?sap-language=A&$format=json&saml2=enabled
        public static string GAZTGetVATSignUpCityAndRegionList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_DROPDOWN_SRV/";//dropdown_headerSet(Spras='A',Land1='',Bland='',Cityc='')?&$expand=city_dropdownSet,country_dropdownSet,State_dropdownSet&saml2=enabled&$format=json
        public static string GAZTGetCreateVATSignUp = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZVTIA_SIGNUP_SRV/signup_headerSet?sap-language=";
        #endregion

        #region VATRegistration
        public static string GAZTGetVATRegistrationData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/VRNHSet(Fbnumz='";
        public static string GAZTGetVATRegistrationOtherDetails = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VRUH_SRV/VR_UI_HDRSet(Fbnum='";
        public static string SaveVATRegistrationData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/VRNHSet";
        public static string SaveVATRegistration = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/VRNHSet?sap-language=";
        #endregion

        #region GAZTUnlock Account
        public static string GAZTUnlockAccountAllOperations = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDGW_UNLOCK_USER_SRV/HeaderSet";
        #endregion

        #region InternationalMobileNumber
        public static string GAZTInternationalMobileData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_DROPDOWN_SRV/CountryCodeSet?$filter=Spras";
        #endregion

        #region Form5
        #endregion
        #region ZakatForm5
        public static string Z_RET_F05_ZKTE = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_RET_F05_ZKTE_SRV/ZKTE_HEADERSet";
        public static string Z_RET_F05_City = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_F05_DROPDOWN_SRV/HeaderSet";
        public static string Z_ZKTE_SUMMARY = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_ZKTE_SUMMARY_SRV/HeadSet";
        #endregion


        #region VATRefunds
        public static string VatRefundList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ETRF_WI_SRV/WISet";
        public static string VatRefundDisplayData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_RF_SRV/HeaderSet(Euser='',Fbnumx='',";
        public static string VatRefundGetIbanData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_UI_RF_SRV/VR_UI_HDRSet(Fbnum='',Lang='',Officer='',";
        public static string VatRefundSubmitData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_RF_SRV/HeaderSet";
       
        #endregion


        //public static string Z_ZKTE_SUMMARY = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_ZKTE_SUMMARY_SRV/HeadSet";
        #region VAT DeRegistration 
        public static string GAZTGETVATDeregReasonDropdownList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/GetReasonSet?$filter=TxnTp";
        public static string GAZTGETVATDeregAttachmentsDropdownList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VDRUH_SRV/VR_UI_HDRSet(Fbnum='";
        public static string GAZTGETVATDeregSuspensionDate = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/GetLastICRDtSet?$filter=";
        public static string GAZTGETVATDeregReturnFilingDateList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/GetSuspensionDetailSet?$filter=";
        public static string GAZTGetVATDeRegistrationData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/HeaderSet(Fbnumx='";
        public static string GAZTSaveVATDeregAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachMedSet(OutletRef=";
        public static string GAZTVATDeregDeteleAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachMedSet(OutletRef=";

        public static string SaveVATDeRegistration = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/HeaderSet";

        #endregion

        #region Establishment Registration
        public static string ESTBranchesDropDown = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_BRANCH_DROPDOWN_SRV/branch_dropdownSet";
        public static string ESTTaxPayerDetails = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_NEW_REGISTRATON_SRV/Nreg_HeaderSet";
        public static string ESTTaxPayerNationality = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_NATIOANALITY_SRV/nationalitySet";
        public static string ESTPostAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_SAVE_ATTACH_SRV_01/AttachSet";
        public static string ESTDeleteAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_SAVE_ATTACH_SRV_01/AttachMedSet";
        public static string ESTOutletNumber = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_NREG_GET_OUTLET_NUMBER_SRV/OutNumSet";
        public static string ESTOutletCityStateCountryDropDown = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_DROPDOWN_SRV/dropdown_headerSet";
        public static string ESTActiivtyGroupSubGroupList = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_REG_ACTIVITY_SRV/act_headerSet";
        public static string ESTValidateCRNum = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_NREG_CR_VALDATE_SRV/validatecrSet";
        public static string ESTOutletList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_NEW_REGISTRATON_SRV/Nreg_OutletSet";
        public static string ESTOutletAddressFetch = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_ADDR_RETRIEVE_SRV/AddressSet";//
        public static string ESTFinancialMaxDate = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_NREG_FD_MAX_DATE_SRV/fd_end_dateSet";//
        #endregion

        #region VATInstalment
        public static string GetVATInstalmentdata = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VTIA_SRV/VTIA_HEADERSet(";
        public static string VATInstalmentSaveData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VTIA_SRV/VTIA_HEADERSet";
        public static string GetReqVATInstalmentdata = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/HEADERSet(";
        public static string GetRequestToVATInstalmentPlanById = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VTIA_SRV/VTIA_HEADERSet(";
        public static string GetDisplayInstallmentAgreementSchedule = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VTIA_DISPLAY_SRV/VTIA_HEADERSet(";
        public static string GetInstallmentSchedule = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VTIA_DISPLAY_SRV/VTIA_HEADERSet(";
        public static string VATGetFormGUIDURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/TPFILLSet(";
        public static string downloadFile = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=";
        public static string SaveVATInstalmentdata = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VTIA_SRV/VTIA_HEADERSet";
        public static string VATObjectionsNotesSet = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/NotesSet('001')";
        public static string ZakatObjectionsNotesSet = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotesSet(1)";


        #endregion

        #region ZAKAT
        public static string ZakatListOfInstalmentplanRequestUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_WI_SRV/HdrSet(";
        public static string ZakatRevokeRequestListUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_WI_SRV/HdrSet(";
        public static string ZakatOldInstalmentsListUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_TP_DASHBOARD_SRV/HeaderSet(";
        public static string ZakatRequestDisplayUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/iprfhdrSet(";
        public static string ZakatValidateRevokeListUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/RevChkSet(";
        public static string ZakateRevokeSendOTPUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/OtpSendCheckSet?$filter=";
        #endregion



        #region ZakatInstalment
        public static string ZakatInstalmentInvoiceURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/invDtlsSet?$filter=";
        public static string GetZAKATInstalmentdata = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/iprfhdrSet(";
        public static string GetZAKATPostdata = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/iprfhdrSet";
        public static string GetZAKATInvoices = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/invDtlsSet?$filter=";
        public static string GetZAKATSummaryInputURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_WI_SRV/UserFillSet(";
        public static string ZakatInstalmentValidateNewRequestURL = BaseUrlOfODataServices + "/sap/opu/odata//SAP/ZDP_IPRF_WI_SRV/HdrSet(";
        #endregion

        #region Contract Release
        public static string ContractReleaseApplicationFormUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_TP_DASHBOARD_SRV/HeaderSet(";

        public static string ContractReleaseRequestUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP11_SRV/znotes_tp11Set(";
        public static string ContractReleaseSubmitUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP11_SRV/znotes_tp11Set";
        public static string ContractReleaseAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_SAVE_ATTACH_SRV/AttachSet(";
        public static string ContractReleaseSummaryData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP11_SRV/znotes_tp11Set(";
        public static string GAZTSaveAttachmentGeneric = BaseUrlOfODataServices + "/sap/opu/odata/SAP/attachmentServiceurl/AttachSet(OutletRef=";
        public static string GAZTDeteleAttachmentGeneric = BaseUrlOfODataServices + "/sap/opu/odata/SAP/attachmentServiceurl/AttachMedSet(OutletRef=";
        public static string downloadFormFile = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVER_FORM_SRV/cover_formSet(Fbnum=";
        public static string CRDownloadCoverFormFile = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVER_FORM_SRV/cover_formSet(Fbnum=";
        public static string CRDownloadAcknowledementFile = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=";
        #endregion

        #region Change Filling Period
        public static string VATChangeFillingPeriodGetURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_TPCV_SRV/UI_HDRSet(";
        public static string VATChangeFillingPeriodPostURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_TPCV_SRV/UI_HDRSet";
        public static string VATChangeFillingPeriodGetDropdownURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_TPCV_UH_SRV/UI_HDRSet(";
        public static string VATChangeFillingPeriodWorkItemsURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/HEADERSet(";
        public static string VATChangeFillingPeriodValidateIDnumberURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_GET_TAXPAYER_SRV/taxpayer_nameSet(";
        public static string VATChangeFillingPeriodAcknowledgementdownloadURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(";
        public static string VATChangeFillingListURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/HEADERSet(";
        public static string VATChangeFillingSummaryURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_TPCV_SRV/UI_HDRSet(";
        public static string VATChangeFillingSummaryInputsURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/TPFILLSet(";
        public static string VATChangeFillingPostATTTYSetURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_TPCV_SRV/ATT_TYPSet(0)";

        #endregion

        #region TIN Deregistration

        public static string TinDeregistrationNewRequestUrl = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_DEREGISTRATION_NEW_SRV/DRG_HeaderSet";
        public static string TinDeregistrationReasonSetUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_DREGRESN_SRV/ZDS_DETSet(";

        #endregion

        #region VATObjection
        public static string GetVATObjectionListURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/HEADERSet(";

        //Excel sheet API1  for fetching intial Action Called on the initial load of the application to fetch all the form data to be displayed on the screen
        public static string GetVATObjectionSummaryURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/HeaderSet(";
        //Excel sheet API2 Called to get the on screen button codes and form mode(editable/uneditable). 
        public static string GetVATObjectionButtonFormModeURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_REVIEW_UI_SRV/VR_UI_HDRSet(";
        //Excel sheet API3 Called to get the list of rejected forms
        public static string GetVATObjectionRejectedFormURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_GET_REJFRM_SRV/HeaderSet(";
        //Excel sheet API4 for Attachment API
        public static string GetVATObjectionAttachmentURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(";
        //Excel sheet API5 to get security amount
        public static string GetVATObjectionSecurityURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/GetSecurityAmountSet(";
        //Excel sheet API6  To decide whether to enable to submit button or not
        public static string GetVATObjectionEnableSubmitURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/GetSubmitEnabledSet(";
        //Excel sheet API7 To validate ID and fetch taxpayer name
        public static string GetVATObjectionValidateTaxPayerNameURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_GET_TAXPAYER_SRV/taxpayer_nameSet(";
        //Excel sheet API9 To Called on the press of Download Acknowledgement Button
        public static string GetVATObjectionDownloadAckURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(";
        //API10 To generate SADAD && API11 On press of refresh button for SADAD
        public static string GetVATObjectionGenrateorRefreshSADADURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_GEN_SADAD_SRV/GetSADADSet(";
        public static string GetVATObjectionViewBillURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/BillDtlSet?$filter=";
        public static string PostVATObjectionURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/HeaderSet";
        public static string VATObjectionSummaryInputURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/TPFILLSet(";
        public static string GetVATReviewRequestTPFVURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_TPFV_M_SRV/HeaderSet(";
        public static string GetVATReviewRequestTPFVOn1stAPISuccessURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_TPFV_UH_SRV/UI_HDRSet(";
        public static string GetVATReviewRequestVTGRURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_GRP_REG_SRV/VRNHSet(";
        public static string GetVATReviewRequestVTGROn1stAPISuccessURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VGR_UH_SRV/VR_UI_HDRSet(";
        public static string GetVATObjViewApplicationDREGURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/HeaderSet(";

        public static string GetVATObjViewApplicationDREGReasonSetURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/GetReasonSet?$filter=TxnTp eq 'VT_DREG' and Lang eq 'E'&$format=json";
        public static string GetVATObjViewApplicationDREGSuspensionReasonSetURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/GetReasonSet?$filter=TxnTp eq 'VT_SUSP' and Lang eq 'E'&$format=json";

        public static string GetVATObjSuspensionDetailSetURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/GetSuspensionDetailSet?$filter=";

        #endregion

        #region ZAKATObjections
        public static string GetZAKATObjectionListURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_TP_DASHBOARD_SRV/HeaderSet(";
        public static string GetZakatObjectionDataURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set(";
       
        //API-2
        public static string GetZAKATObjectionCreateNewURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/TPFILLSet(";
        //API-5
        public static string GetZAKATObjectionDetailsByReferenceNumberURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_OBJ_ZNOB_AMT_SRV/ZNOB_AmtSet(";
        //API-6
        public static string GetZAKATObjectionDetailsToAmendReturnURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_ZNOBREF_SRV/Ref_NoSet(";
        //API-7 
        public static string GetZAKATObjectionAmendReturnAndCloseURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_OBJ_ZNOB_REV_AMT_SRV/ZNOB_RevAmtSet(";
        //API-8
        public static string GetZAKATObjectionOnPaymentMethodSelectionURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_OBJ_ZNOB_REV_AMT_SRV/secamtSet(";
        //API-9
        public static string GetZAKATObjectionApplicationDetailsIfStatusIP017URL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_ZNOB_FB_DETAILS_SRV/ZFBSet(";
        //API-10
        public static string GetZAKATObjectionGenerateSADADNumberURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_OBJ_ZNOB_REV_AMT_SRV/sadadnumberSet(";
        //API-12
        public static string GetZAKATObjectionBusyIndicatorURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_BUSY_INDICATOR_SRV/ZBUSYINDSet(";

        public static string ZakatObjectionLoadBankListURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_OBJ_ZNOB_REV_AMT_SRV/bankListSet?$filter=Langz eq 'EN'";
        public static string ZakatObjectionIntialLoadURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_OBJ_ZNOB_SRV/ZNOB_HeaderSet(";
        public static string ZakatObjectionRemoveobjectionURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_RMV_OBJ_SRV/HeaderSet(";
        public static string ZakatObjectionRemoveObjAckURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(";



        public static string ZakatObjectionWDMaindataRL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set(";
        public static string ZakatObjectionWDListRL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/zvaluesSet?$filter=";
        // get the data of objection number when the objection number is selected from the dropdown
        public static string ZakatObjectionWDSelectedDDURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/object_itmsSet?$filter=";
        //attachment API
        public static string ZakatObjectionWDAttachmentURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_SAVE_ATTACH_SRV/AttachSet(";
        public static string ZakatObjectionRequestSummaryURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_OBJ_ZNOB_SRV/ZNOB_HeaderSet(";
        public static string ZakatObjectionWDPostURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set";
        public static string ZakatObjectionSummaryURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set(";

        // * TP PROFILE API - V2
        public static string TPProfileURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_PROFILE_N_SRV/TPFL_HEADERSet";
        public static string GetTPProfileChangePWDURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_PROFILE_N_SRV/ChangePasswordSet";
        public static string ZOdownloadAckLetter = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=";
        public static string ZOdownloadCoverFormFile = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVER_FORM_SRV/cover_formSet(Fbnum=";
        #endregion

    }
}
