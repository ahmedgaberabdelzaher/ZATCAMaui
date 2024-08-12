using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.NewModelAPI.AbsherOTP
{
    public class AbsherOTPResponse
    {
        public Header header { get; set; }
        public Result result { get; set; }
    }
    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
    }

    public class Result
    {
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string taxpayerBirthDate { get; set; }
        public string formBundleGUID { get; set; }
        public string captcha { get; set; }
    }

    public class Status
    {
        public string code { get; set; }
        public string description { get; set; }
    }
    public class ValidateAbhserOTPModel
    {
        public Header header { get; set; }
        public Result1 result { get; set; }
      
    }
    public class Result1
    {
        public string idNumber { get; set; }
        public string captcha { get; set; }
        public string OTPCode { get; set; }
        public string OTPCheck { get; set; }
        public string formBundleGUID { get; set; }
    }
}
