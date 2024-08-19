using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.NewModelAPI.AbsherOTP
{
    public class AbsherValidateRequestOTP
    {
        public string captcha { get; set; }
        public string idNumber { get; set; }
        public string OTPCode { get; set; }
        public string formBundleGUID { get; set; }
    }
    public class TaxPayerInformation
    {
        public string taxpayerBirthDate { get; set; }
        public string idNumber { get; set; }
        public string idType { get; set; }
        public string OTPCode { get; set; }
        public string formBundleGUID { get; set; }
    }
}
