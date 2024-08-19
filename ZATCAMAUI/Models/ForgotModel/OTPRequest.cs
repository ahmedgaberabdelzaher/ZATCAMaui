using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.ForgotModel
{
    public class OTPRequest
    {
        public string TIN { get; set; }
        public string email { get; set; }
        public string birthDate { get; set; }
        public string language { get; set; }
        public string captchaCode { get; set; }
        public string GUID { get; set; }
    }
}
