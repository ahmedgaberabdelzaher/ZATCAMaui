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
    }
}
