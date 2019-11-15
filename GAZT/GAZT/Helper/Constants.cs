using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Helper
{
    public static class Constants
    {
        public static string JSONContentType = "application/json";
        public static string GAZTSOAPWebRequestForAuthenticationService = "https://tstdp1as1.mygazt.gov.sa:50001/local~mblgapi/AuthenticatedService";
        public static string GAZTSendAndReceiveOTP = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GAZTValidateOTP = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GaZTVerifyMobileNumber = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateOTPForMobile = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateAndChangePassword = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='P',";
        public static string GAZTGetPdf = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/Corr_detSet?$filter=";
        public static string GAZTGetTP = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_TP_PROFILE_DEMO_SRV/TPFL_HEADERSet(Taxpayerz";
        public static string GAZTGetOTPForEmail = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='E',";
        public static string GAZTValidateOTPForEmail = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='E',";
                public static string GAZTZakatGetPdf = "https://10.50.15.51:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_ZAKAT_SRV/Corr_detSet?$filter=Gpartz eq'";

        
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
