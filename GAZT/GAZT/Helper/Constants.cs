using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Helper
{
    public static class Constants
    {
        public static string JSONContentType = "application/json";
        public static string GAZTSOAPWebRequestForAuthenticationService = "https://10.50.15.31:50001/local~mblgapi/AuthenticatedService";
        public static string GAZTSendAndReceiveOTP = "https://10.50.15.51:8080/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GAZTValidateOTP = "https://10.50.15.51:8080/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='";
        public static string GaZTVerifyMobileNumber = "https://10.50.15.51:8080/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateOTPForMobile = "https://10.50.15.51:8080/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='M',";
        public static string GAZTValidateAndChangePassword = "https://10.50.15.51:8080/sap/opu/odata/sap/Z_TP_CHANGE_PROFILE_SRV/ZDS_TPCHPROFILESet(Flag='P',";
        public static string GAZTGetPdf = "https://10.50.15.51:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/Corr_detSet?$filter=";

    }
}
