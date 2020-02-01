using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Helper
{
    public static class Constants
    {
        public static string ContentType = "application/json";

        public static string DevBaseUrlForODataServices = "https://tstdg1as1.mygazt.gov.sa:8080";
        public static string DevBaseUrlForAuthentication = "https://tstdp1as1.mygazt.gov.sa:50001";

        public static string QABaseUrlForODataServices = "https://sapgatewayqa.gazt.gov.sa:443";
        public static string QABaseUrlForAuthentication = "https://loginqa.gazt.gov.sa:443";

        public static string PreProdBaseUrlForODataServices = "https://sapgatewayt.gazt.gov.sa:443";
        public static string PreProdBaseUrlForAuthentication = "https://logint.gazt.gov.sa:443";

        public static string ProdBaseUrlForODataServices = "https://sapgateway.gazt.gov.sa:443";
        public static string ProdBaseUrlForAuthentication = "https://login.gazt.gov.sa:443";

        public static string BaseUrlOfODataServices = QABaseUrlForODataServices;
        public static string BaseUrlOfAuthentication = QABaseUrlForAuthentication;


        public static string JSONContentType = "application/json";
        public static string GAZTSOAPWebRequestForAuthenticationService = BaseUrlOfAuthentication+"/local~mblgapi/AuthenticatedService";
        public static string GAZTSendAndReceiveOTP = BaseUrlOfODataServices+"/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GAZTValidateOTP = BaseUrlOfODataServices+"/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GaZTVerifyMobileNumber = BaseUrlOfODataServices+"/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateOTPForMobile = BaseUrlOfODataServices+"/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateAndChangePassword = BaseUrlOfODataServices+ "/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='P',";
        public static string GAZTGetPdf = BaseUrlOfODataServices+"/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/Corr_detSet?$filter=";
        public static string GAZTGetTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_PROFILE_DEMO_SRV/TPFL_HEADERSet(Taxpayerz";
        public static string GAZTGetOTPForEmail = BaseUrlOfODataServices+"/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='E',";
        public static string GAZTValidateOTPForEmail = BaseUrlOfODataServices+"/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='E',";
        public static string GAZTZakatGetPdf = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_ZAKAT_SRV/Corr_detSet?$filter=Gpartz eq'";
        public static string GetAllTin = BaseUrlOfAuthentication + "/prt_logon/GetTINServlet?&emailId=";
        public static string GetAllCertificate = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/headerSet(Gpartz='";
        public static string GetMyBills = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_MYBILLS_SRV/MyBillsSet?$filter=";
        public static string GetDashboardData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/HEADERSet?$filter=Tin eq '";
        public static string FogotPasswordSendOTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='";
        public static string SendUserNameToEmail = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet?saml2=disabled";
        public static string ValidateOTP = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet? saml2 = disabled";
        public static string ChangePassword = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet?saml2=disabled";

       
        public static string GetTinStatus = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZTIN_STAT_SRV/HeaderSet(Langz='";
        public static string GetVATLookUpDetails = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZVAT_TAXPAYER_LOOKUP_SRV/TaxpayerSet?saml2=disabled&sap-language='";
        public static string GAZTGetAllVATDeclarationReturnData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/HDRSet(Periodkeyz='";
        public static string GetMyICRs = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_WI_SRV/ICR_HDRSet(Fbnum='',Lang='";
        public static string SaveVATDeclarationData = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/HDRSet?saml2=disabled";
        public static string GAZTSaveAttachment = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef=";
        public static string GAZTGetVATDeclarationCalculationDataUrl = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum=";
        public static string GAZTGetSADADNumber = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_SADAD_SRV/SadadSet?&saml2=disabled&sap-langauge=’";
        public static string GAZTGetZakatReturn = BaseUrlOfODataServices + "/sap/opu/odata/sap/ZDP_FZ12_SRV/HeaderSet(Fbnumz='";
        public static string GAZTGetZakatReturnList = BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TAX01RET_WI_SRV/HeaderSet(Bpnum='";



        //public static bool IsValidMobileNumber(string mobileNumber)
        //{
        //    if(mobileNumber.Substring(0,1).Equals(5) && mobileNumber.Length == 9)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
    }
}
