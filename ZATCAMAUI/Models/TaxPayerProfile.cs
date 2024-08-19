using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    //ideally it should be UserId, TIN etc. but based on API responsethe naming of variable has been matched 
    public class TaxPayerProfile
    {
        public string UserId { get; set; }
        public string Auditorz { get; set; }
        public string userId { get; set; }
        public string auditor { get; set; }
        public string PrevEmail { get; set; }
        public string PreviousPwd { get; set; }
        public string MobileCountry { get; set; }
        public string authenticationUser1 { get; set; }
        public string taxpayerType { get; set; }
        public string verifyEmail { get; set; }
        public string authenticationUser { get; set; }
        public string authenticationUser2 { get; set; }
        public string VerifyMobile { get; set; }
        public string authenticationUser3 { get; set; }
        public string typeCheck { get; set; }
        public string authenticationUser4 { get; set; }
        public string formBundleGUID { get; set; }
        public string authenticationUser5 { get; set; }
        public string Taxpayerz { get; set; }
        public string RegIdz { get; set; }
        public string periodKey { get; set; }
        public string submit { get; set; }
        public string save { get; set; }
        public string formBundleNumber { get; set; }
        public string language { get; set; }
        public string portalUser { get; set; }
        public string month { get; set; }
        public string OfficerUidz { get; set; }
        public string approve { get; set; }
        public string reject { get; set; }
        public string createTaxAssessment { get; set; }
        [JsonProperty("void")]
        public string Void { get; set; }
        public string amendmentReason { get; set; }
        public string objectSubmit { get; set; }
        public string mode { get; set; }
        public string skipBilling { get; set; }
        public string partner { get; set; }
        public string nameCheck { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        [JsonProperty("taxpayerTitle")] 
        public string TpTitle { get; set; }
        public string organizationName { get; set; }
        public string activityNameCheck { get; set; }
        public string activityName { get; set; }
        public string emailCheck { get; set; }
        public string email { get; set; }
        public string emailLoginCode { get; set; }
        public string mobileCheck { get; set; }
        public string mobile { get; set; }
        public string mobileLoginCode { get; set; }
        public string passwordCheck { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string edit { get; set; }
        public string cancel { get; set; }
        public string confirm { get; set; }
        public string login { get; set; }
        [JsonProperty("VATProfitMarginFlag")]
        public string VtpmFg { get; set; }
        // * For UI + Other API Calls
        public string Name
        {
            get
            {
                if (0 == String.Compare(typeCheck, "X"))
                    return TpTitle + firstName + lastName;
                else
                    return organizationName;

            }
        }

        // Need to check
        private string _NewEmail = string.Empty;
        public string NewEmail
        {
            get { return _NewEmail; }
            set { _NewEmail = value; }
        }

        public string TIN
        {
            get { return Taxpayerz; }
            set { Taxpayerz = partner = value; }
        }

        // Need to check
        private string _NewMobile = string.Empty;
        public string NewMobile
        {
            get { return _NewMobile; }
            set { _NewMobile = value; }
        }

        // Need to check
        private string _NewPassword = string.Empty;
        public string NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; }
        }

        // * Missmatch With Updated TPProfile Data
        public int Attempts { get; set; }
        public string Password { get; set; }
        public string Result { get; set; }
        public string Partner { get; set; }

        public string Tin
        {
            get { return Taxpayerz; }
            set { Taxpayerz = Partner = value; }
        }
    }

    public class TaxPayerAccountDetail
    {

        public string typeCheck { get; set; }
        public string firstName { get; set; }
        public string emailId { get; set; }
        public string lastName { get; set; }
        public string organizationName { get; set; }
        public string deviceToken { get; set; }
        public string authenticationUser { get; set; }
        public string messageTitle { get; set; }
        public string applicationMessage { get; set; }
        public string expectedDate { get; set; }
        public string applicationVerion { get; set; }

        public string deviceId { get; set; }
        public string TIN { get; set; }
        public string fcmId { get; set; }
        public string deviceType { get; set; }
        public string taxpayerVip { get; set; }
        public string device { get; set; }
        public string devicezakatSignup { get; set; }
        public string zakatSignup { get; set; }
        public string eligiblePersonSignup { get; set; }
        public string VATRegisteration { get; set; }
        public string exciseSignup { get; set; }
        public string VATSignup { get; set; }
        public string zakatRegisteration { get; set; }
        public string exciseTaxRegisteration { get; set; }
        public string GUID { get; set; }
        public string cozatcaTileFlag { get; set; }
    }
}
