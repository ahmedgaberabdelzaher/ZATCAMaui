using System;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    //public class ForgotPasswordOTP
    //{
    //    public ForgotPasswordOTPResult forgotPasswordOTPResult { get; set; }
    //}
    //public class ForgotPasswordOTPResult
    //{
    //    public string Action { get; set; }
    //    public string Tin { get; set; }
    //    public string Langu { get; set; }
    //    public int CurrAttmps { get; set; }
    //    public string EmailId { get; set; }
    //    public string TpType { get; set; }
    //    public string MobileNo { get; set; }
    //    public string SubType { get; set; }
    //    public string Idnumber { get; set; }
    //    public string Otp { get; set; }
    //    public int Minutes { get; set; }
    //    public string Name { get; set; }
    //    public int Attempts { get; set; }
    //    public DateTime Dob { get; set; }
    //    public string NewPwd { get; set; }
    //    public string CnfPwd { get; set; }
    //    public string RdBt { get; set; }
    //    public string Hyperlink { get; set; }
    //}
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class D
    {
        // check metadata while calling for sentOTP
        public Metadata __metadata { get; set; }
        public string Action { get; set; }
        public string Tin { get; set; }
        public string Langu { get; set; }
        public int CurrAttmps { get; set; }
        public string EmailId { get; set; }
        public string TpType { get; set; }
        public string MobileNo { get; set; }
        public string SubType { get; set; }
        public string Idnumber { get; set; }
        public string Otp { get; set; }
        public int Minutes { get; set; }
        public string Name { get; set; }
        public int Attempts { get; set; }
        //public DateTime Dob { get; set; }
        public string NewPwd { get; set; }
        public string CnfPwd { get; set; }
        public string RdBt { get; set; }
        public string Hyperlink { get; set; }
    }
    //public class ValidateOTPD
    //{
    //    // check metadata while calling for sentOTP
    //    public Metadata __metadata { get; set; }
    //    public string Action { get; set; }
    //    public string Tin { get; set; }
    //    public string Langu { get; set; }
    //    public int CurrAttmps { get; set; }
    //    public string EmailId { get; set; }
    //    public string TpType { get; set; }
    //    public string MobileNo { get; set; }
    //    public string SubType { get; set; }
    //    public string Idnumber { get; set; }
    //    public string Otp { get; set; }
    //    public int Minutes { get; set; }
    //    public string Name { get; set; }
    //    public int Attempts { get; set; }
    //    //public DateTime Dob { get; set; }
    //    public string NewPwd { get; set; }
    //    public string CnfPwd { get; set; }
    //    public string RdBt { get; set; }
    //    public string Hyperlink { get; set; }
    //}
    public class ForgotPasswordOTP
    {
        public D d { get; set; }
    }
}
