
namespace ZATCAMAUI.Models.SignUP
{
    public class CreateVatSignUPResponse
    {
        public Header header { get; set; }
        public Data data { get; set; }
        public Data Result { get; set; }
    }
    public class Data
    {
        public string taxpayerTitle { get; set; }
        public string activityCategory { get; set; }
        public string URL { get; set; }
        public string captchaCode { get; set; }
        public string GUID { get; set; }
        public string mobileCountry { get; set; }
        public string activityNumber { get; set; }
        public string addressType { get; set; }
        public string external { get; set; }
        public string @internal { get; set; }
        public string authorizationGroup { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime birthDate { get; set; }
        public string buildingCode { get; set; }
        public string caseGUID { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public string CRLicenseNumber { get; set; }
        public string email { get; set; }
        public string emailCode { get; set; }
        public DateTime endDate { get; set; }
        public DateTime finishDate { get; set; }
        public string formBundleNumber { get; set; }
        public string firstName { get; set; }
        public string floor { get; set; }
        public string formGUID { get; set; }
        public string houseNumber1 { get; set; }
        public string houseNumber2 { get; set; }
        public string idNumber { get; set; }
        public string idType { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string password { get; set; }
        public string postCode { get; set; }
        public string region { get; set; }
        public bool isSameAsPhyiscal { get; set; }
        public string SMSCode { get; set; }
        public string source { get; set; }
        public DateTime startDate { get; set; }
        public string street { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string submit { get; set; }
        public string TIN { get; set; }
        public string type { get; set; }
    }

    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
    }


    public class Status
    {
        public string code { get; set; }
        public string description { get; set; }
    }
}
