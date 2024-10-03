
using ZATCAMAUI.Models.EstablishmentRegistration;

namespace ZATCAMAUI.Core.Helper
{
    public static class ZATCAConstants
    {
        public static string CustomUserNameAuthorization = "mobile_dev";
        public static string CustomPasswordAuthorization = "mobile@dev";

        public static string ContentType = "application/json";

        public static string DevUrlPort = ":8080";
        public static string DevUrlAuthenticationPort = ":50001";

        public static string QAUrlPort = ":443";
        public static string PreProdUrlPort = ":443";
        public static string ProdUrlPort = ":443";

        public static string DevPaymentSapClinet = "100";
        public static string QAPaymentSapClinet = "300";
        public static string PreProdPaymentSapClinet = "500";
        public static string ProdPaymentSapClinet = "500";
        public static int NafathAPICallTimer = 15;

        public static string PaymentDevBaseUrlForODataServices = BaseUrlOfODataServices;
        public static string PaymentQABaseUrlForODataServices = BaseUrlOfODataServices;
        public static string UatBaseUrlForODataServices = BaseUrlOfODataServices;
        public static string PreProdBaseUrlForODataServices = BaseUrlOfODataServices;
        public static string ProdBaseUrlForODataServices = BaseUrlOfODataServices;
        #region Cookie Info

        public static string DevDomainForCookies = "tstdp1as1.zatca.gov.sa";
        public static string QADomainForCookies = "loginqa.zatca.gov.sa";
        public static string PreprodDomainForCookies = "logint.zatca.gov.sa";
        public static string UatDomainForCookies = "loginu.zatca.gov.sa";

        public static string ProdDomainForCookies = "login.zatca.gov.sa";

        public static string DevPartialDomainForCookies = ".zatca.gov.sa";
        public static string QAPrepprodProdPartialDomainForCookies = ".zatca.gov.sa";

        public static string LanguageCookieNameForLogin = "langMobile";

        public static string LanguageCookieNameForLoginX = "langMobile";

        #endregion
        /* Captcha Application codes  */
        public static string VTIA = "C1"; //VAT Signup
        public static string PUSR = "C2"; //Establishment Signup
        public static string FPWD = "C3"; //Forgot Password and Unlock Account
        public static string FUSR = "C4"; //Forgot Username

        //Dev
        //public static string BaseUrlOfODataServices = "https://test-api.zatca.gov.sa/dev/third-party";
        //public static string BaseUrlForSSLCheck = "https://test-api.zatca.gov.sa";
        //public static string ClientId = "72ecc8e63ad24460b8892fa973c8cdb1";
        //public static string ClientSecret = "6390a04151da534a221764190fd8d30c";
        //public static string publicKeyStr = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCJMtl5sh5y3FjSasKyyfcAWLvhjSkECAm3sJmkyJbCg/PN3olhTqknqecmZ8qQw4MNbyfEUkWjQfV+0fJXtRJOUIoeVJxQRDTOZ10abWGOenXj8IC5ETxnpVZ6XqKAYUGGQRtSBU4U3Uk+78gKw68pCNJCcgq/Z88rbLQ+KB//dwIDAQAB";


        //QA
        public static string BaseUrlOfODataServices = "https://test-api.zatca.gov.sa/test/third-party";
        public static string BaseUrlForSSLCheck = "https://test-api.zatca.gov.sa";
        public static string ClientId = "30ba76941a06e8b1141131800cd87139";
        public static string ClientSecret = "3bfa87a6be6b87fcbc2fad2f6690c4e5";
        public static string publicKeyStr = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCJMtl5sh5y3FjSasKyyfcAWLvhjSkECAm3sJmkyJbCg/PN3olhTqknqecmZ8qQw4MNbyfEUkWjQfV+0fJXtRJOUIoeVJxQRDTOZ10abWGOenXj8IC5ETxnpVZ6XqKAYUGGQRtSBU4U3Uk+78gKw68pCNJCcgq/Z88rbLQ+KB//dwIDAQAB";


        //UAT
        //public static string BaseUrlOfODataServices = "https://test-api.zatca.gov.sa/uat/third-party";
        //public static string BaseUrlForSSLCheck = "https://test-api.zatca.gov.sa";
        //public static string ClientId = "f4a2fde61a8115d5cfc989ada521f58e";
        //public static string ClientSecret = "24ad1c31fc1a9372119d4bb5934c92e1";

        //ECT
        //public static string BaseUrlOfODataServices = "https://test-api.zatca.gov.sa/pre-production/third-party";
        //public static string BaseUrlForSSLCheck = "https://test-api.zatca.gov.sa";
        //public static string ClientId = "f2050eb80c977daa819f806ad7820c8e";
        //public static string ClientSecret = "5a6d51d98aa856c572322ae3b99418ff";
        //public static string publicKeyStr = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqx7E3lJHpDDjrO4JsxpBllhkDm9YhMJpE64k0plrZoZ+f4jpmsP6+yFwWXwO7hnrD0WcWHyoQANfUtAe08p1m841p2TZH+ieRE8oxMK+mNEpYMM+7tXEe1gIR14aOrnjbjzdtdLGy/DTz4J2BJOVNFkgQN5OqHzFRP4KyGEUVUn3Qd8XG/+izXJ0YNdQDWrW5KQBC/2TPWhGC9HIPHWNrfxqndAR0fGfI4pEnLgbOAUXFk/Oi88oSg9mbhQWlpak46K8SE2R148xzEXvEb5QrYFmI11O87kZoa9CCfG2lmKnP8oZx9EDYfh4dHfzFItF9TLTe9e0MTFkfU4v2+uKQQIDAQAB";
        ////PRD
        //public static string BaseUrlOfODataServices = "";
        //public static string BaseUrlForSSLCheck = "";
        //public static string ClientId = "";
        //public static string ClientSecret = "";


        public static string DevBaseUrlForAuthentication = "https://tstdp1as1.mygazt.gov.sa:50001";
        public static string QABaseUrlForAuthentication = "https://loginqa.gazt.gov.sa:443";
        public static string UatBaseUrlForAuthentication = "https://loginu.gazt.gov.sa";
        public static string PreProdBaseUrlForAuthentication = "https://logint.gazt.gov.sa";
        public static string ProdBaseUrlForAuthentication = "https://login.gazt.gov.sa:443";



        public static string GAZTSAMLLoginServicePart = "/SAP/ZTP_ACCOUNT_SRV/GetInfoSet";
        public static string GAZTSAMLLoginService = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZTP_ACCOUNT_SRV/GetInfoSet";

        public static string GAZTSAMLLogoutService = BaseUrlOfODataServices + "/v1/taxpayer/auth/logout";
        //https://sapgatewayqa.gazt.gov.sa/sap/public/bc/icf/logoff?keepMYSAPSSO2Cookie=true&dsmguid=1588829910165

        public static string CaptchaAndGUID = BaseUrlOfODataServices + "/v1/captcha";
        public static string GetAbsherPassword = BaseUrlOfODataServices + "/v1/taxpayer-signup/absher-otp/send";//PenTest
        public static string ValidateAbsher = BaseUrlOfODataServices + "/v1/taxpayer-signup/absher-otp/validate";
        public static string GAZTSiguupValidateIDTypesDeclZakat = BaseUrlOfODataServices + "/v1/taxpayer-signup/taxpayer-information";//PENTEST Chnage

        public static string ForgotPasswordServiceName = "ZDP_FRGT_USRNM_PWD_SRV";// service name has been used in Metadata in ForgotUserNamePassword Page
        public static string JSONContentType = "application/json";
        public static string GAZTSOAPWebRequestForAuthenticationService = DevBaseUrlForAuthentication + "/local~mblgapi/AuthenticatedService";
        public static string GAZTSendAndReceiveOTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GAZTValidateOTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GaZTVerifyMobileNumber = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateOTPForMobile = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateAndChangePassword = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='P',";
        public static string GAZTGetPdf = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/Corr_detSet?$filter=";
        public static string GAZTGetTP = BaseUrlOfODataServices + "/v1/taxpayers/profile?TIN=";
        public static string GAZTGetTPAccountDetails = BaseUrlOfODataServices + "/v1/taxpayers/accounts/details";
        public static string GAZTGetOTPForEmail = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='E',";
        public static string GAZTValidateOTPForEmail = BaseUrlOfODataServices + "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='E',";
        public static string GAZTZakatGetPdf = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_ZAKAT_SRV/Corr_detSet?$filter=Gpartz eq'";
        public static string GetAllTin = BaseUrlOfODataServices + "/v1/taxpayers?email=";
        public static string GetAllTinsByEmail = BaseUrlOfODataServices + "/v1/erad/taxpayers/verify-account?email=";

        public static string GetAllCertificate = BaseUrlOfODataServices + "/v1/taxpayers/certificates?TIN=";
        public static string GetMyBills = BaseUrlOfODataServices + "/v1/users/bills?serialNumber=";
        public static string GetMyBillsFilterDropdown = BaseUrlOfODataServices + "/v1/bills/types?language=";
        public static string FogotPasswordSendOTP = BaseUrlOfODataServices + "/v1/passwords/forgot-password/otp";
        public static string SendUserNameToEmail = BaseUrlOfODataServices + "/v1/passwords/forgot-password/username-sending";
        public static string ValidateOTP = BaseUrlOfODataServices + "/v1/passwords/forgot-password/otp/verification";
        public static string ChangePassword = BaseUrlOfODataServices + "/v1/passwords/forgot-password/password-changing";
        public static string GetTinStatus = BaseUrlOfODataServices + "/v1/taxpayers/status?language=";
        public static string GetVATLookUpDetails = BaseUrlOfODataServices + "/v1/vat/lookups";
        public static string GAZTGetAllVATDeclarationReturnData = BaseUrlOfODataServices + "/v1/vat/returns/details?authenticationUser=";
        public static string GAZTGetNotifyAuditorForAddingAttachments = BaseUrlOfODataServices + "/v1/vat/returns/details";
        public static string GetMyICRs = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_WI_SRV/ICR_HDRSet(Fbnum='',Lang='";
        public static string SaveVATDeclarationData = BaseUrlOfODataServices + "/v1/vat/returns/details";
        public static string GAZTSaveAttachment = BaseUrlOfODataServices + "/v1/vat-deregistration/attachments?outletReference=";
        public static string GAZTESTSaveAttachment = BaseUrlOfODataServices + "/v1/establishment-signup/attachments?outletReference=";
        public static string GAZTDeteleAttachment = BaseUrlOfODataServices + "/v1/vat-deregistration/attachments/deletion";
        public static string GAZTDeteleAttachmentNew = BaseUrlOfODataServices + "/v1/attachments/deletion";
        public static string GAZTGetVATDeclarationCalculationDataUrl = BaseUrlOfODataServices + "/v1/vat/returns/forms/details?TIN=";
        public static string GAZTGetSADADNumber = BaseUrlOfODataServices + "/v1/vat/sadad-bills?language=";
        public static string GAZTGetZakatReturn = BaseUrlOfODataServices + "/v1/estimated-returns/zakat-returns/details?TIN=";
        public static string GAZTGetZakatReturnList = BaseUrlOfODataServices + "/v1/estimated-zakat/returns?businessPartnerNumber=";
        public static string GAZTSaveEstimatedZaktReturn = BaseUrlOfODataServices + "/v1/estimated-returns/zakat-returns/details?language=";
        public static string GAZTVATReturnGetApplicableButtons = BaseUrlOfODataServices + "/v1/vat/returns/forms/details?formBundleNumber=";
        public static string GAZTGetEstimatedZAKATSADADNumber = BaseUrlOfODataServices + "/v1/estimated-returns/zakat-returns/details?TIN=";
        public static string GAZTSaveEstimatedZAKATAttachement = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(RetGuid='";
        public static string GAZTGetIdNumber = BaseUrlOfODataServices + "/v1/vat-registration/id-number?TIN=";
        public static string GAZTCheckIBANNumber = BaseUrlOfODataServices + "/v1/accounts/iban/validation";
        public static string GAZTGetEstimatedZAKATReturnInvoicePdf = BaseUrlOfODataServices + "/v1/estimated-returns/zakat-returns/invoices/attachments?correspondenceKey=";
        public static string ZATCAAccStmtsStatusDtls = BaseUrlOfODataServices + "/v1/bills/payments/status";
        public static string ZATCAAccStmtsDetails = BaseUrlOfODataServices + "/v1/taxpayers/accounts/statments/lineItem?documentNumber=";
        public static string VATGetRevokeBtnSet = BaseUrlOfODataServices + "/v1/forms/vtia-forms/details";

        //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_TAX01RET_WI_SRV/HeaderSet(Bpnum='3300088482',Auditor='',Lang='EN',UserTin='330088482')?saml2=disabled&sap-language='EN'&$expand=listSet
        //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_TP_PROFILE_DEMO_SRV/TPFL_HEADERSet(Taxpayerz='3102289241',Langz='E')?&$expand=TPOC_LIST&saml2=enabled&$format=json

        #region CorrespondenceAPIs
        public static string GAZTGetCorrespondence = BaseUrlOfODataServices + "/v1/correspondences?TIN=";
        public static string GAZTSetFavCorrespondence = BaseUrlOfODataServices + "/v1/correspondences/favourite-status";
        public static string GAZTGetCorrespondenceDetails = BaseUrlOfODataServices + "/v1/correspondences/details?TIN=";
        public static string GAZTGetCorrespondenceAttach = BaseUrlOfODataServices + "/v1/estimated-returns/zakat-returns/invoices/attachments?correspondenceKey=";
        public static string ZATCACRPDCATT = BaseUrlOfODataServices + "/v1/correspondences/officers/attachments";
        #endregion
        #region FormBundleAPIs
        public static string GAZTGetFormBundleModel = BaseUrlOfODataServices + "/v1/returns/status?TIN=";
        public static string GAZTGetFormBunleAccountNumberModel = BaseUrlOfODataServices + "/v1/forms/returns/status?TIN=";
        #endregion
        #region SignUp
        public static string GAZTGetCityListForSignUp = BaseUrlOfODataServices + "/v1/vat-signup/cities?country=SA&";
        public static string GAZTSiguupValidateIDTypes = BaseUrlOfODataServices + "/v1/taxpayer-signup/taxpayer-information";
        public static string GAZTSiguupValidateGCCIDType = BaseUrlOfODataServices + "/v1/taxpayer-signup/taxpayer-information";
        public static string GAZTSiguupValidateCR = BaseUrlOfODataServices + "/v1/commercial-registration/validation?CRNumber=";
        public static string GAZTSiguupCheckDuplicate = BaseUrlOfODataServices + "/v1/permits/details";
        public static string GAZTSiguupIssuedByList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZTP_MOBILE_SRV/ConsumeSet?$filter=Request eq ";
        //  public static string GAZTSignUpFirstSubmit = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_PUSR_SIGNUP_SRV/signup_headerSet";

        // "/sap/opu/odata/SAP/ZVTIA_SIGNUP_SRV/signup_headerSet";
        public static string GAZTSignUpFirstSubmit = BaseUrlOfODataServices + "/v1/signup/cases";
        public static string GAZTSignUpGetGuid = BaseUrlOfODataServices + "/v1/signup/cases?type=1";
        #endregion
        #region NEW DASHBOARD
        //Dashboard - get the set of Unpaid Amounts
        public static string GetDashboardData = BaseUrlOfODataServices + "/v1/taxpayers/dashboard?TIN=";
        public static string GetDashboardInstalmentPlanData = BaseUrlOfODataServices + "/v1/installments/plans/dashboard?TIN=";
        public static string GAZTGetTheSetOfUnpaidAmounts = BaseUrlOfODataServices + "sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/PaymentOverdueSet?$filter=Langz eq ";
        public static string GAZTGetTheSetOfUnsubmittedReturns = BaseUrlOfODataServices + "sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/UnSubmittedReturnSet?$filter=Langz eq ";
        public static string GAZTGetUnSubmittedReturnSetForDashboard = BaseUrlOfODataServices + "/v1/taxpayers/dashboard/unsubmitted-returns?TIN=";
        public static string GAZTGetPaymentOverdueSetForDashboard = BaseUrlOfODataServices + "/v1/taxpayers/dashboard/overdue-payments?TIN=";
        public static string GAZTGetReturnList = BaseUrlOfODataServices + "/v1/icr/returns?TIN=";
        public static string GAZTGetTpActivityStatus = BaseUrlOfODataServices + "/v1/taxpayers/activities/status?TIN=";
        public static string GAZTPostActivityStatus = BaseUrlOfODataServices + "/v1/taxpayers/activities/status";
        public static string GAZTTilesSet = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZTAXPAYERDBNEW_SRV/HeaderSet(";
        public static string GAZTLogin = BaseUrlOfODataServices + "/v1/taxpayer/auth/login";
        public static string GAZTValidateLoginOTP = BaseUrlOfODataServices + "/v1/taxpayer/auth/token";
        public static string GAZTResendOTP = BaseUrlOfODataServices + "/v1/taxpayer/auth/resend-token";
        #endregion
        #region TES
        public static string GAZTGetFAQ = "http://tstcrmmwintg1.mygazt.gov.sa:82/IntegrationServices.svc/FAQRetrieveAll";
        #endregion
        #region DownloadAttachment
        public static string GAZTGetAllAttachments = BaseUrlOfODataServices + "/v1/attachments?returnGUID=";
        #endregion

        public static string GetLoginDetaialsSSO = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZVTIA_SIGNUP_SRV/signup_headerSet?$filter=Type eq '1' and Mguid eq '";//6094
        public static string GetGstcCaseDetailsApi = BaseUrlOfODataServices + "/v1/gstc/escalated-cases?TIN=";
        public static string GetRMContactDetails = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_VOC_SURVEY_SRV/RelationManagerSurveySet(Gpart";
        public static string CheckVocAvailability = "https://vocstg.gazt.gov.sa/v1/response/survey/availability";
        public static string ComplaintsEngUrl = "https://gazt.gov.sa/en/ContactUs/Pages/default.aspx#topic_complaint";
        public static string ComplaintsARUrl = "https://gazt.gov.sa/ar/ContactUs/Pages/default.aspx#topic_complaint";

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
        public static string GAZTGetVATSignUpCaseId = BaseUrlOfODataServices + "/v1/vat-signup";
        public static string GAZTVATSignUpValidateId = BaseUrlOfODataServices + "/v1/vat-signup/taxpayers/validation";//(Tin='',Idtype='ZS0015',Idnum='1048089609',Country='',PassExpDt='',TaxpDob='19650224')?sap-language=A&$format=json&saml2=enabled
        public static string GAZTGetVATSignUpCityAndRegionList = BaseUrlOfODataServices + "/v1/vat-signup/dropdowns?language=";
        public static string GAZTGetCreateVATSignUp = BaseUrlOfODataServices + "/v1/vat-signup/cases";
        #endregion

        #region VATRegistration
        public static string GAZTGetVATRegistrationData = BaseUrlOfODataServices + "/v1/vat-registration/details?TIN=";
        public static string GAZTGetVATRegistrationOtherDetails = BaseUrlOfODataServices + "/v1/vat-registration/details-with-buttons?formBundleNumber=";
        public static string SaveVATRegistrationData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/VRNHSet";
        public static string SaveVATRegistration = BaseUrlOfODataServices + "/v1/vat-registration/details";
        public static string GetRequestedUpdateVatEffDates = BaseUrlOfODataServices + "/v1/vat/effective-dates?TIN=";
        public static string GAZTGetValidIBanNumbers = BaseUrlOfODataServices + "/v1/accounts/iban/details?TIN=";
        #endregion

        #region GAZTUnlock Account
        public static string GAZTUnlockAccountAllOperations = BaseUrlOfODataServices + "/v1/taxpayers/accounts/unlocking";
        public static string GZATVALIDATEOTP = BaseUrlOfODataServices + "/v1/taxpayers/accounts/unlocking/otp/verification";
        public static string GAZTUNLOCKChangePWD = BaseUrlOfODataServices + "/v1/taxpayers/accounts/unlocking/password-changing";
        #endregion

        #region InternationalMobileNumber
        public static string GAZTInternationalMobileData = BaseUrlOfODataServices + "/v1/countries?language=";
        #endregion

        #region Form5
        #endregion
        #region ZakatForm5
        public static string Z_RET_F05_ZKTE = BaseUrlOfODataServices + "/v1/forms/zakat-forms/details?formBundleGUID=";
        public static string Z_RET_F05_City = BaseUrlOfODataServices + "/v1/forms/zakat-forms/cities?TIN=";
        public static string Z_ZKTE_SUMMARY = BaseUrlOfODataServices + "/v1/forms/zakat-forms/summary?TIN=";
        #endregion


        #region VATRefunds
        public static string VatRefundList = BaseUrlOfODataServices + "/v1/vat-refund/items/work-items/details?TIN=";
        public static string VatRefundDisplayData = BaseUrlOfODataServices + "/v1/vat-refund/details?TIN=";
        public static string VatRefundGetIbanData = BaseUrlOfODataServices + "/v1/vat-refund/users/details?TIN=";
        public static string VatRefundSubmitData = BaseUrlOfODataServices + "/v1/vat-refund/details";

        #endregion


        //public static string Z_ZKTE_SUMMARY = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_ZKTE_SUMMARY_SRV/HeadSet";
        #region VAT DeRegistration 
        public static string GAZTGETVATDeregReasonDropdownList = BaseUrlOfODataServices + "/v1/vat-deregistration/reasons?";
        public static string GAZTGETVATDeregAttachmentsDropdownList = BaseUrlOfODataServices + "/v1/vat-deregistration/attachments?TIN=";
        public static string GAZTGETVATAttachments = BaseUrlOfODataServices + "/v1/vat-deregistration/attachments?";
        public static string GAZTGETVATDeregSuspensionDate = BaseUrlOfODataServices + "/v1/icr/last-dates?TIN=";
        public static string GAZTGETVATDeregReturnFilingDateList = BaseUrlOfODataServices + "/v1/suspensions?TIN=";
        public static string GAZTGetVATDeRegistrationData = BaseUrlOfODataServices + "/v1/vat-deregistration/details?isReviewed=false";
        public static string GAZTSaveVATDeregAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachMedSet(OutletRef=";
        public static string GAZTVATDeregDeteleAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachMedSet(OutletRef=";
        public static string GAZTVATDeregNotesSet = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/NotesSet('002')";
        public static string SaveVATDeRegistration = BaseUrlOfODataServices + "/v1/vat-deregistration/details";

        #endregion

        #region Establishment Registration
        public static string ESTBranchesDropDown = BaseUrlOfODataServices + "/v1/establishment-signup/branches?language=";
        public static string ESTTaxPayerDetails = BaseUrlOfODataServices + "/v1/establishment-signup/taxpayers/details?TIN=";
        public static string ESTTaxPayerDetailsPost = BaseUrlOfODataServices + "/v1/establishment-signup/taxpayers/details";
        public static string ESTTaxPayerNationality = BaseUrlOfODataServices + "/v1/establishment-signup/taxpayers/nationalities?nationalityCode=";
        public static string ESTPostAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_SAVE_ATTACH_SRV_01/AttachSet";
        public static string ESTDeleteAttachment = BaseUrlOfODataServices + "/v1/establishment-signup/attachments/deletion";
        public static string ESTOutletNumber = BaseUrlOfODataServices + "/v1/outlets/next-number?formBundleNumber=";
        public static string ESTOutletCityStateCountryDropDown = BaseUrlOfODataServices + "/v1/vat-signup/dropdowns?language=";
        public static string ESTActiivtyGroupSubGroupList = BaseUrlOfODataServices + "/v1/establishment-signup/activities?language=";
        public static string ESTValidateCRNum = BaseUrlOfODataServices + "/v1/commercial-registration/validation";
        public static string ESTOutletList = BaseUrlOfODataServices + "/v1/establishment-signup/outlets?formBundleNumber=";
        public static string ESTDeleteOutlet = BaseUrlOfODataServices + "/v1/establishment-signup/outlets/items/deletion";
        public static string ESTOutletAddressFetch = BaseUrlOfODataServices + "/v1/taxpayers/addresses";
        public static string ESTFinancialMaxDate = BaseUrlOfODataServices + "/v1/establishment-signup/financial-end-date";
        public static string UpdateLicenseAndCR = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_NEW_REGISTRATON_SRV/ISIC4Set";
        #endregion

        #region VATInstalment
        public static string GetVATInstalmentdata = BaseUrlOfODataServices + "/v1/vat-installments/plans/details?TIN=";
        public static string VATInstalmentSaveData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VTIA_SRV/VTIA_HEADERSet";
        public static string GetReqVATInstalmentdata = BaseUrlOfODataServices + "/v1/vat-installments/plans/requests?TIN=";
        public static string GetRequestToVATInstalmentPlanById = BaseUrlOfODataServices + "/v1/vat-installments/plans/details?TIN=";
        public static string GetDisplayInstallmentAgreementSchedule = BaseUrlOfODataServices + "/v1/vat-installments/plans/agreements?TIN=";
        public static string GetInstallmentSchedule = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VTIA_DISPLAY_SRV/VTIA_HEADERSet(";
        public static string VATGetFormGUIDURL = BaseUrlOfODataServices + "/v1/forms/summary/details?";
        public static string downloadFile = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";
        public static string SaveVATInstalmentdata = BaseUrlOfODataServices + "/v1/vat-installments/plans/details?";
        public static string VATObjectionsNotesSet = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/NotesSet('001')";
        public static string ZakatObjectionsNotesSet = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotesSet(1)";


        #endregion

        #region ZAKAT
        public static string ZakatListOfInstalmentplanRequestUrl = BaseUrlOfODataServices + "/v1/installments/zakat-installments/plans/requests?TIN=";
        public static string ZakatRevokeRequestListUrl = BaseUrlOfODataServices + "/v1/revokes/zakat-revokes?TIN=";
        public static string ZakatOldInstalmentsListUrl = BaseUrlOfODataServices + "/v1/installments/old-zakat-installments/plans/requests?TIN=";
        public static string ZakatOldInstalmentsSummarytUrl = BaseUrlOfODataServices + "/v1/installments/old-zakat-installments/details?TIN=";
        public static string ZakatRequestDisplayUrl = BaseUrlOfODataServices + "/v1/installments/plans/details?TIN=";
        public static string ZakatValidateRevokeListUrl = BaseUrlOfODataServices + "/v1/zakat/revoke/validation?formBundleNumber=";
        public static string ZakateRevokeSendOTPUrl = BaseUrlOfODataServices + "/v1/revokes/zakat-revokes/otp/status?TIN=";
        #endregion



        #region ZakatInstalment
        public static string ZakatInstalmentInvoiceURL = BaseUrlOfODataServices + "/v1/invoices/zakat-invoices?TIN=";
        public static string GetZAKATInstalmentdata = BaseUrlOfODataServices + "/v1/installments/plans/details?TIN=";
        public static string GetZAKATPostdata = BaseUrlOfODataServices + "/v1/installments/plans/details";
        public static string GetZAKATInvoices = BaseUrlOfODataServices + "/v1/invoices/zakat-invoices?TIN=";
        public static string GetZAKATSummaryInputURL = BaseUrlOfODataServices + "/v1/installments/plans/forms/summary?TIN=";
        public static string GetOldZAKATSummaryInputURL = BaseUrlOfODataServices + "/v1/forms/summary/details?TIN=";
        public static string GetOldZAKATPostdata = BaseUrlOfODataServices + "/v1/installments/old-zakat-installments/details";
        public static string ZakatInstalmentValidateNewRequestURL = BaseUrlOfODataServices + "/v1/installments/zakat-installments/plans/requests?TIN=";
        public static string ZAKATinstalmentPlanResonsList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_UH_SRV/InsRsnSet?";
        #endregion
        #region ZakatIExemptions
        public static string ZakatExemtionRequestInit = BaseUrlOfODataServices + "/v1/zakat/exemptions/requests/details?";
        public static string ZakatExemtionRequestList = BaseUrlOfODataServices + "/v1/zakat/exemptions/requests?";
        public static string ZakatExemtionPostRequest = BaseUrlOfODataServices + "/v1/zakat/exemptions/requests";
        public static string ZakatExemtionDownloadCert = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";
        #endregion
        #region Contract Release
        public static string ContractReleaseApplicationFormUrl = BaseUrlOfODataServices + "/v1/contracts/releases?TIN=";
        public static string ContractReleaseRequestUrl = BaseUrlOfODataServices + "/v1/contract-releases/requests/details?TIN=";
        public static string ContractReleaseSubmitUrl = BaseUrlOfODataServices + "/v1/contract-releases/requests/details";
        public static string ContractReleaseAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_SAVE_ATTACH_SRV/AttachSet(";
        public static string ContractReleaseSummaryData = BaseUrlOfODataServices + "/v1/contract-releases/requests/details?TIN=";
        public static string GAZTSaveAttachmentGeneric = BaseUrlOfODataServices + "/v1/attachments?outletReference=";
        public static string GAZTDeteleAttachmentGeneric = BaseUrlOfODataServices + "/sap/opu/odata/SAP/attachmentServiceurl/AttachMedSet(OutletRef=";
        public static string downloadFormFile = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";
        public static string CRDownloadCoverFormFile = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";
        public static string CRDownloadAcknowledementFile = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";
        #endregion

        #region Change Filling Period
        public static string VATChangeFillingPeriodGetURL = BaseUrlOfODataServices + "/v1/forms/filling-frequency/requests/details?TIN=";
        public static string VATChangeFillingPeriodPostURL = BaseUrlOfODataServices + "/v1/forms/filling-frequency";
        public static string VATChangeFillingPeriodGetDropdownURL = BaseUrlOfODataServices + "/v1/forms/dynamic-forms/details?TIN=";
        public static string VATChangeFillingPeriodWorkItemsURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/HEADERSet(";
        public static string VATChangeFillingPeriodValidateIDnumberURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_GET_TAXPAYER_SRV/taxpayer_nameSet(";
        public static string VATChangeFillingPeriodAcknowledgementdownloadURL = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";
        public static string VATChangeFillingListURL = BaseUrlOfODataServices + "/v1/vat-installments/plans/requests?TIN=";
        public static string VATChangeFillingSummaryURL = BaseUrlOfODataServices + "/v1/forms/filling-frequency/requests/details?TIN=";
        public static string VATChangeFillingSummaryInputsURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/TPFILLSet(";
        public static string VATChangeFillingPostATTTYSetURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_TPCV_SRV/ATT_TYPSet(0)";

        #endregion

        #region TIN Deregistration

        public static string TinDeregistrationNewRequestUrl = BaseUrlOfODataServices + "/v1/taxpayer-deregistration/details";
        public static string TinDeregistrationReasonSetUrl = BaseUrlOfODataServices + "/v1/taxpayer-deregistration/reasons?TIN=";

        #endregion
        #region TIN/Outlet Deregistration
        public static string TinOutletDeregistrationPreousRequestsUrl = BaseUrlOfODataServices + "/v1/taxpayer-deregistration/requests";
        public static string TinOutletDeregistrationPreousRequestsPostUrl = BaseUrlOfODataServices + "/v1/taxpayer-deregistration/requests/cancel";
        public static string OutletDeregistrationNewRequestUrl = BaseUrlOfODataServices + "/v1/taxpayer-deregistration/details?TIN=";
        #endregion
        #region VATObjection
        public static string GetVATObjectionListURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_ITAP_SRV/HEADERSet(";

        //Excel sheet API1  for fetching intial Action Called on the initial load of the application to fetch all the form data to be displayed on the screen
        public static string GetVATObjectionSummaryURL = BaseUrlOfODataServices + "/v1/vat-objections/summary?TIN=";
        //Excel sheet API2 Called to get the on screen button codes and form mode(editable/uneditable). 
        public static string GetVATObjectionButtonFormModeURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_REVIEW_UI_SRV/VR_UI_HDRSet(";
        //Excel sheet API3 Called to get the list of rejected forms
        public static string GetVATObjectionRejectedFormURL = BaseUrlOfODataServices + "/v1/vat-objections/rejections?TIN=";
        //Excel sheet API4 for Attachment API
        public static string GetVATObjectionAttachmentURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(";
        //Excel sheet API5 to get security amount
        public static string GetVATObjectionSecurityURL = BaseUrlOfODataServices + "/v1/vat-objections/security-amounts?TIN=";
        //Excel sheet API6  To decide whether to enable to submit button or not
        public static string GetVATObjectionEnableSubmitURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/GetSubmitEnabledSet(";
        //Excel sheet API7 To validate ID and fetch taxpayer name
        public static string GetVATObjectionValidateTaxPayerNameURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_GET_TAXPAYER_SRV/taxpayer_nameSet(";
        //Excel sheet API9 To Called on the press of Download Acknowledgementr Button
        public static string GetVATObjectionDownloadAckURL = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?TIN=";
        //API10 To generate SADAD && API11 On press of refresh button for SADAD
        public static string GetVATObjectionGenrateorRefreshSADADURL = BaseUrlOfODataServices + "/v1/bills/sadad-number";
        public static string GetVATObjectionViewBillURL = BaseUrlOfODataServices + "/v1/vat-objections/bills?TIN=";
        public static string PostVATObjectionURL = BaseUrlOfODataServices + "/v1/taxpayers/reviews/requests";
        public static string VATObjectionSummaryInputURL = BaseUrlOfODataServices + "/v1/forms/summary/details?TIN=";
        public static string GetVATReviewRequestTPFVURL = BaseUrlOfODataServices + "/v1/deduction-methods/requests/details?TIN=";
        public static string GetVATReviewRequestTPFVOn1stAPISuccessURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_TPFV_UH_SRV/UI_HDRSet(";
        public static string GetVATReviewRequestVTGRURL = BaseUrlOfODataServices + "/v1/vat-registration/groups/requests/details?TIN=";
        public static string GetVATReviewRequestVTGROn1stAPISuccessURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VGR_UH_SRV/VR_UI_HDRSet(";
        public static string GetVATObjViewApplicationDREGURL = BaseUrlOfODataServices + "/v1/vat-deregistration/details?TIN=";

        public static string GetVATObjViewApplicationDREGReasonSetURL = BaseUrlOfODataServices + "/v1/vat-deregistration/reasons?transactionType=";
        public static string GetVATObjViewApplicationDREGSuspensionReasonSetURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/GetReasonSet?$filter=TxnTp eq 'VT_SUSP' and Lang eq 'E'&$format=json";

        public static string GetVATObjSuspensionDetailSetURL = BaseUrlOfODataServices + "/v1/suspensions?TIN=";
        public static string GetVATDeRegistrationDeclaration = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_DECLARE_SRV/DeclareSet(Fbnum=";

        #endregion


        #region ZAKATObjections
        public static string GetZAKATObjectionListURL = BaseUrlOfODataServices + "/v1/objections/zakat-objections?TIN=";
        public static string GetZakatObjectionDataURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set(";

        //API-2
        public static string GetZAKATObjectionCreateNewURL = BaseUrlOfODataServices + "/v1/forms/summary/details?display=1&authenticationUser1=";
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

        public static string ZakatObjectionLoadBankListURL = BaseUrlOfODataServices + "/v1/banks?language=";
        public static string ZakatObjectionIntialLoadURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_OBJ_ZNOB_SRV/ZNOB_HeaderSet(";
        public static string ZakatObjectionRemoveobjectionURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_RMV_OBJ_SRV/HeaderSet(";
        public static string ZakatObjectionRemoveObjAckURL = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";
        public static string ZakatdownloadCoverFormFile = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";
        public static string OldZakatdownloadCoverFormFile = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";


        public static string ZakatObjectionWDMaindataRL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set(";
        public static string ZakatObjectionWDListRL = BaseUrlOfODataServices + "/v1/withdrawals/zakat-withdrawals?TIN=";
        // get the data of objection number when the objection number is selected from the dropdown
        public static string ZakatObjectionWDSelectedDDURL = BaseUrlOfODataServices + "/v1/zakat/withdrawals/details?";
        //attachment API
        public static string ZakatObjectionWDAttachmentURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_SAVE_ATTACH_SRV/AttachSet(";
        public static string ZakatObjectionRequestSummaryURL = BaseUrlOfODataServices + "/v1/initial-load/details?formBundleNumber=";
        public static string ZakatObjectionWDPostURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set";
        public static string ZakatObjectionSummaryURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set(";
        public static string ZakatObjectionSummaryURLTP10 = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_NOTES_TP10_SRV/znotes_tp10Set(";

        // * TP PROFILE API - V2
        public static string TPProfileURL = BaseUrlOfODataServices + "/v1/taxpayers/profile";
        public static string TPProfilePasswordUpdate = BaseUrlOfODataServices + "/v1/taxpayers/profile/password-update";
        public static string GetTPProfileChangePWDURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_PROFILE_N_SRV/ChangePasswordSet";
        public static string ZOdownloadAckLetter = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";
        public static string ZOdownloadCoverFormFile = BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=";

        //Taxpayer subsidy 
        public static string TaxPayerSubsidyPost = BaseUrlOfODataServices + "/v1/taxpayers/expense-contracts/requests";
        #endregion

        #region
        //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_ACCOUNT_STATEMENT_SRV/TabIdentificationSet(Euser='00001000000008337092',Fbguid='005056B1F8FB1EDB80ECD20B24BEA571')
        public static string AccountStatementTabIdentification = BaseUrlOfODataServices + "/v1/taxpayers/accounts/statements/tabs/identifications?authenticationUser=";

        //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_ACCOUNT_STATEMENT_SRV/RevenueDropdownSet?$filter=Euser eq '00001000000008337092' and Fbguid eq '005056B1F8FB1EDB80ECD20B24BEA571' and TaxType eq 'D' and Langz eq 'E'
        public static string AccountStatementRevenueDropDownSet = BaseUrlOfODataServices + "/v1/taxpayers/accounts/statements/revenues/types?portalUser=";

        //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_ACCOUNT_STATEMENT_SRV/StatementHeaderSet(Euser='00001000000008337102',Fbguid='005056B1F8FB1EDB80EE42BD20B90982',StatementFilter='',FiscalYear='',TaxType='D',Lang='E')?&$expand=StatmenetLineItemsSet
        public static string AccountStatementGetHeaderSet = BaseUrlOfODataServices + "/v1/taxpayers/accounts/statements?load=";

        //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_ACCOUNT_STATEMENT_srv/YearValueSet?$filter=Euser eq '00000000001008337188' and Fguid eq '005056B1F8FB1EDB80EED6C67D924B34' and TaxType eq 'D' and StatementFilter eq '01'
        public static string AccountStatementGetYearValues = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_ACCOUNT_STATEMENT_srv/YearValueSet?$filter=Euser eq '' and ";

        //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_ACCOUNT_STATEMENT_SRV/zpdfDownloadSet(Euser='00000000000001088319',Fguid='005056B1365C1EDB82951CC479769E28',Taxtype='D',FiscalYear='2020',StatementFilter='04',FromDt=datetime'2020-8-1T00:00:00',ToDt=datetime'2020-10-31T00:00:00',Langz='E')/$value
        public static string AccountStatementDownloadPdf = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_ACCOUNT_STATEMENT_srv/zpdfDownloadSet(Euser='',";

        #endregion

        //vatGoodsprofitsmargins Cr6264
        public static string TaxpayervatgoodsAmrgin = BaseUrlOfODataServices + "/v1/vat/profit-goods";

        #region Payment Integration

        public static string ValidatePaymentInformation = BaseUrlOfODataServices + "/v1/payments/mada-payments/validation";
        public static string CancelPaymentService = BaseUrlOfODataServices + "/v1/payments/mada-payments/cancelation";



        public static string PaymentUrl = BaseUrlOfODataServices + "/sap/bc/ui5_ui5/sap/zuibmobilepay/index.html?sap-ui-xx-devmode=true&guid=";

        public static string UpdateMadaPaymentInformation = BaseUrlOfODataServices + "/v1/payments/mada-payments/details";
        public static string CreateMadaPaymentInformation = BaseUrlOfODataServices + "/v1/payments/mada-payments";
        public static string ApplePayGenerateGuid = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDGW_APPLE_PAY_SRV/GuidEntrySet";
        public static string UpdateApplePayGuid = BaseUrlOfODataServices + "/v1/payments/apple-pay/details";



        #endregion
        //New URL 
        public static string GAZTGetVATObjectionURL = BaseUrlOfODataServices + "/v1/vat-installments/plans/requests?TIN=";
        public static string VatSignUPURL = BaseUrlOfODataServices + "/v1/vat-signup/cases";
        public static string GAZTGetVATSignUpCaseIdURL = BaseUrlOfODataServices + "/v1/vat-signup/cases?type=1";

        #region Change Mob Number
        public static string ChangeMobNumberGetIDTypes = BaseUrlOfODataServices + "/v1/mobile-number/changing/details";
        public static string SaveChangeMobNumber = BaseUrlOfODataServices + "/v1/mobile-number/changing";
        public static string ChangeMobPostAttachment = BaseUrlOfODataServices + "/v1/signup/attachments";
        public static string ChangeMobDeleteAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_SIGNUP_ATTACH_SRV/AttachMedSet";
        public static string PrintFormUrl = BaseUrlOfODataServices + "/v1/forms/cover-forms/download?formBundleNumber=";// Fbnum='40000006834')/$value";

        public static string GetTpManagersList = BaseUrlOfODataServices + "/v1/taxpayers/profile/managers";
        public static string PostTpManagersList = BaseUrlOfODataServices + "/v1/taxpayers/profile/manager-details";

        #endregion
        #region Bank Account Managmnt
        public static string GetBankAccountInformation = BaseUrlOfODataServices + "/v1/accounts/iban";
        public static string PostBankAccountIBAN = BaseUrlOfODataServices + "/v1/accounts/iban";
        public static string GetIBANAcoountFormGUID = BaseUrlOfODataServices + "/v1/accounts/iban/status";
        #endregion

        #region Image captcha
        public static string GAZTGetCaptchaImage = BaseUrlOfODataServices + "/v1/captcha?formBundleTypeId=";
        #endregion
        public static string GAZTChatPartialUrlen = "https://chatbot.zatca.gov.sa/maker/GaztProd/Main/en/index_PROD.html";
        public static string GAZTChatPartialUrlar = "https://chatbot.zatca.gov.sa/maker/GaztProd/Main/ar/index_PROD.html";

        //public static string GAZTChatPartialUrlen = "https://tstchatbot.gazt.gov.sa/GaztTesting/en/index_test.html";
        //public static string GAZTChatPartialUrlar = "https://tstchatbot.gazt.gov.sa/GaztTesting/ar/index_test.html";

        public static string GAZTFAQEnUrl = "https://zatca.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservicesMV.aspx";
        public static string GAZTFAQARUrl = "https://zatca.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservicesMV.aspx";

        public static string GAZTSuggestURLen = "https://zatca.gov.sa/en/ContactUs/Pages/SuggestAndComplaintMV.aspx";
        public static string GAZTSuggestURLar = "https://zatca.gov.sa/ar/ContactUs/Pages/SuggestAndComplaintMV.aspx";

        public static string GAZTVisitPortalUrlEN = "https://login.zatca.gov.sa/irj/portal?ume.logon.locale=en&login=X";
        public static string GAZTVisitPortalUrlAR = "https://login.zatca.gov.sa/irj/portal?ume.logon.locale=ar&login=X";


        public static string ZAtcaCustomsTarrifsEN = "https://www.customs.gov.sa/en/Integrated-Tariff-appview";
        public static string ZAtcaCustomsTarrifsAr = "https://www.customs.gov.sa/ar/Integrated-Tariff-appview";
        // public static string ZAtcaCustomsTarrifsAr = "http://10.112.42.23/ar/Integrated-Tariff-appView";

        public static string ZAtcaCustomsdeclarationsEN = "https://eservices.zatca.gov.sa/sites/sc/en/app-view/pages/checkBayan.aspx";
        public static string ZAtcaCustomsdeclarationsAr = "https://eservices.zatca.gov.sa/sites/sc/ar/app-view/pages/checkBayan.aspx";
        //public static string ZAtcaCustomsdeclarationsAr = " https://esvc-web1-stg/sites/sc/ar/app-view/Pages/checkBayan.aspx";
        // public static string ZAtcaCustomsdeclarationsAr = "http://esvc-web1-stg.ga.customs.gov.sa/sites/sc/ar/app-view/Pages/checkBayan.aspx";
        // public static string ZAtcaCustomsdeclarationsAr = "https://10.113.98.41/sites/sc/ar/app-view/Pages/checkBayan.aspx";

        public static string TaxpayerSubsidyRequest = string.Empty;
        public static string GetVatEligilibilityDate = BaseUrlOfODataServices + "/v1/vat-registration/commencement-date?TIN=";
        public static string GAZTVATSignUpValidateIdDeclaration = BaseUrlOfODataServices + "/v1/taxpayers/information";//BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_REG_GET_TP_NAME_SRV/taxpayer_nameSet";//CRPENTEST(Tin='',Idtype='ZS0015',Idnum='1048089609',Country='',PassExpDt='',TaxpDob='19650224')?sap-language=A&$format=json&saml2=enabled
        public static string GAZTSiguupValidateIDTypesDecl = BaseUrlOfODataServices + "/v1/taxpayer-signup/taxpayer-information";//PENTEST Chnage

        public static string ZAtcaContactUsEN = "https://zatca.gov.sa/en/contactus/Pages/default.aspx";
        public static string ZAtcaContactUsAR = "https://zatca.gov.sa/ar/contactus/Pages/default.aspx";

        public static string ChangeMobNafath = string.Empty;

        public static string WebKeyChangeMobCompanay = "IsCHGMCMPY=Y";
        public static string WebKeyChangeMobCompanayNafath = "IsCompany=Y&guid=";

        public static string AppChangeMobCompanay = "ChangeMobCompanay";
        public static string AppChangeMobCompanayNafath = "ChangeMobCompanayNafath";


        #region NAFATH Authentication
        public static string NafathAuthentication = BaseUrlOfODataServices + "/v1/erad/nafath/sendAuth";
        public static string NafathAuthenticationVerification = BaseUrlOfODataServices + "/v1/erad/nafath/verification";
        public static string NafathChangeMobileNumber = BaseUrlOfODataServices + "/v1/taxpayers/registration-types";
        public static string NafathChangeMobileNumberSendOTP = BaseUrlOfODataServices + "/v1/mobile-number/changing/otp/sending";
        public static string NafathChangeMobileNumberCheckOTP = BaseUrlOfODataServices + "/v1/mobile-number/changing/otp/verification";
        public static string NafathLogin = BaseUrlOfODataServices + "/v1/taxpayer/auth/nafath/login";
        public static string NafathSSOUserAccountsInquiry = BaseUrlOfODataServices + "/v1/identity-management/sso-users/accounts";
        public const string NAFATH_LOGIN = "01";
        public const string NAFATH_SIGNUP = "02";
        public const string NAFATH_CHANGE_MOBILE_NUMBER = "03";
        public const string NAFATH_COMPANY_CHANGE_MOBILE_NUMBER = "04";
        #endregion

        //Api Keys data
        public static string IosMapsApiKey = "AIzaSyCnIhK1NNzYNX-pZ1JjZpsLAXzHPgQOgSM";
        public static string IosMapsApi = "IosMapsApiKey";

        public static List<Nreg_IdItem> IdSet = new List<Nreg_IdItem>();

        public static Dictionary<string, string> EnIssueBy = new Dictionary<string, string>()
        {
            {"",""},
            {"90701", "Communications, Space and Technology Commission" },
            {"90702", "Ministry of Commerce" },
            {"90703", "Ministry of Health" },
            {"90704", "Ministry of Media" },
            {"90705", "Ministry of Environment Water & Agriculture" },
            {"90706", "Ministry of Municipal and Rural Affairs" },
            {"90707", "Ministry of Education" },
            {"90708", "Technical and Vocational Training Corporation" },
            {"90709", "Ministry of Human Resources and Social Development" },
            {"90710", "Ministry of Islamic Affairs Dawah and Guidance" },
            {"90711", "Ministry of Hajj and Umrah" },
            {"90712", "Ministry of Investment" },
            {"90713", "Saudi Electricity Company" },
            {"90714", "Saudi Arabian Monetary Agency" },
            {"90715", "General Authority of Civil Aviation" },
            {"90716", "Ministry of Interior" },
            {"90717", "Ministry of Transportation" },
            {"90719", "Same Government Agency" },
            {"90721", "Municipality" },
            {"90722", "Saudi Organization for Certified public Accountants" },
            {"90723", "Ministry of Tourism" },
            {"90725", "Ministry Of Justice" },
            {"90729", "Saudi Council of Engineers" },
            {"90724", "Ministry of Industry and Mineral Resources" },

            {"90740", "Ministry of Sports" },
            {"90731", "Saudi Wildlife Authority" },
            {"90732", "Saudi Authority for Industrial Cities and Technology Zones" },
            {"90733", "The General Authority of Meteorology and Environmental Protection" },
            {"90735", "Saudi Food and Drug Authority" },
            {"90736", "Saudi Ports Authority" },
            {"90737", "Capital Markets Authority" },
            {"90738", "Electricity & CoGeneration Regulatory Authority" },
            {"90739", "Ministry of Housing" },
            {"90741", "Ministry of Energy" },
            {"90742", "General Commission For Audiovisual Media" },
            {"90718", "Other" },
        };

        public static Dictionary<string, string> ArIssueBy = new Dictionary<string, string>()
        {
            {"",""},
            {"90701",
  "هيئة الاتصالات والفضاء والتقنية"
            },
            {"90702",
 "وزارة التجارة"
            },
            {"90703",
 "وزارة الصحة"
            },
            {"90704",
 " وزارة الإعلام"
            },
            {"90705",
 " وزارة البيئة والمياه والزراعة"
             },
             {"90706",
 " وزارة الشؤون البلدية والقروية"
             },
             {"90707",
 " وزارة التعليم"
             },
             {"90708",
 " المؤسسة العامة للتدريب التقني والمهني"
              },
              {"90709",
 " وزارة الموارد البشرية والتنمية الاجتماعية"
              },
              {"90710",
 " وزارة الشؤون الإسلامية والأوقاف والدعوة والإرشاد"
               },
               {"90711",
 " وزارة الحج والعمرة"
               },
               {"90712",
 " وزارة الاستثمار"
               },
               {"90713",
 " الشركة السعودية للكهرباء"
                },
                {"90714",
 " مؤسسة النقد العربي السعودي"
                },
                {"90715",
 " الهيئة العامة للطيران المدني"
                },
                {"90716",
 " وزارة الداخلية"
                },
                {"90717",
 " وزارة النقل"
                },
                {"90719",
 " نفس الجهة الحكومية"
                },
                {"90721",
 " الأمانات"
                },
                {"90722",
 " الهيئة السعودية للمحاسبين القانونيين"
                },
                {"90723",
 " وزارة السياحة"
                },
                {"90725",
 " وزارة العدل"
                },
                {"90729",
 " الهيئة السعودية للمهندسين"
                },
                {"90724",
 " وزارة الصناعة والثروة المعدنية"
                },
                {"90740",
 " وزارة الرياضة"
                },
                {"90731",
 " الهيئة السعودية للحياة الفطرية"
                },
                {"90732",
 " الهيئة السعودية للمدن الصناعية ومناطق التقنية"
                },
                {"90733",
 " الهيئة العامة للأرصاد وحماية البيئة"
                },
                {"90735",
 "الهيئة العامة للغذاء والدواء"
                },
                {"90736",
 " الهيئة العامة للموانئ"
                },
                {"90737",
 " هيئة السوق المالية"
                },
                {"90738",
 " هيئة تنظيم الكهرباء والإنتاج المزدوج"
                },
                {"90739",
 " وزارة الأسكان"
                },
                {"90741",
 " وزارة الطاقة"
                },
                {"90742",
 "هيئة الإعلام المرئي والمسموع"
                },
                {"90718",
 " أخرى"
                },
        };


    }
}
