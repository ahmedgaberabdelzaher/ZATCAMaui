using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.NewModelAPI.AbsherOTP
{
    public class AbsherOTPRequest
    {
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string taxpayerBirthDate { get; set; }
        public string formBundleGUID { get; set; }
        public string captcha { get; set; }
    }
}
